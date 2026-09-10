' ***********************************************************************
' Assembly         : Infrastructure.CrossCutting.AuditServices
' Author           : WalterSierra
' Created          : 10-04-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-27
' ***********************************************************************
' <copyright file="IndigoAuditService.vb" company="">
'     . All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
#Region "Imports"
Imports System.Threading
Imports Infrastructure.CrossCutting.Audit
Imports Infrastructure.CrossCutting.MessageStore
Imports System.Data
Imports System.Text

#End Region

Public Class IndigoAuditService

#Region "Members"

    ''' <summary>
    ''' Bandera que controla si el servicio se ha detenido
    ''' </summary>
    Private _stopping As Boolean
    ''' <summary>
    ''' Cantidad de mensajes eliminados
    ''' </summary>
    Private _countDeletedMessages As Int32 = 0
    ''' <summary>
    ''' Cantidad maxima de mensajes eliminados
    ''' antes de comprimir la base de datos de mensajes
    ''' </summary>
    Private Const MAX_COUNTDELETEDMESSAGES As Int32 = 255
    ''' <summary>
    ''' Valor que indica si de debe eliminar los mensajes con error
    ''' </summary>
    Private _deleteErrorMessage As Boolean = True

#End Region

#Region "Service Methods"

    ''' <summary>
    ''' Called when [start].	
    ''' </summary>
    ''' <param name="args">The args.</param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnStart(ByVal args() As String)
        EventLog.WriteEntry("In OnStart.")
        Me._stopping = False
        Me.LoadConfig()
        INDTimer.Start()
        MyBase.OnStart(args)
    End Sub

    Protected Overrides Sub OnPause()
        Me.INDTimer.Stop()
        Me._stopping = True
        MyBase.OnPause()
    End Sub

    Protected Overrides Sub OnContinue()
        Me._stopping = False
        Me.INDTimer.Start()
        MyBase.OnContinue()
    End Sub

    ''' <summary>
    ''' Called when [stop].	
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub OnStop()
        INDTimer.Stop()
        Me._stopping = True
        EventLog.WriteEntry("In OnStop. " & DateTime.Now)
        MyBase.OnStop()
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' maneja el evento Elapsed del control INDTimer.
    ''' </summary>
    Private Sub INDTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles INDTimer.Elapsed
        EventLog.WriteEntry("INDTimer_Elapsed: " & DateTime.Now)
        Try
            INDTimer.Stop()
            'verifico si ya es la hora aproximada de ejecucion
            Dim nextDate As DateTime
            If MessageService.DateLastExecution Is Nothing Then
                'primera vez que se ejecuta
                nextDate = DateTime.Now
            Else
                nextDate = MessageService.DateLastExecution.GetValueOrDefault
            End If
            If DateTime.Now >= nextDate Then
                RunAudit()
            Else
                MessageService.SetMessage(DateTime.Now & " - Verificando Fecha/Hora. Fecha Aproximada de Proxima Ejecucion: " & MessageService.DateLastExecution)
            End If
            INDTimer.Start()
        Catch ex As Exception
            EventLog.WriteEntry(ex.ToString)
            INDTimer.Start()
        End Try
    End Sub


#End Region

#Region "Metodos"

    Public Sub Start()
        Me.OnStart(Nothing)
    End Sub

    Private Sub LoadConfig()
        Try
            Dim interval As Integer = 0
            Dim conf = Configuration.ConfigurationManager.OpenExeConfiguration(Configuration.ConfigurationUserLevel.None)
            If conf.AppSettings.Settings("Intervalo") Is Nothing Then
                INDTimer.Interval = 5000
                EventLog.WriteEntry("No se ha configurado el intervalo de tiempo, por tanto se usa el valor por defecto, 5 segundos.", EventLogEntryType.Warning)
            Else
                Int32.TryParse(conf.AppSettings.Settings("Intervalo").Value, interval)
                If interval = 0 Then
                    INDTimer.Interval = 5000
                    EventLog.WriteEntry("El intervalo configurado no es válido. Se usa el valor por defecto, 5 segundos.", EventLogEntryType.Warning)
                Else
                    INDTimer.Interval = Double.Parse(interval * 1000)
                    EventLog.WriteEntry("Intervalo configurado correctamente en " & interval & " segundos.", EventLogEntryType.Information)
                End If
            End If
            If conf.AppSettings.Settings("DeleteErrorMessage") Is Nothing Then
                Me._deleteErrorMessage = True
            Else
                Boolean.TryParse(conf.AppSettings.Settings("DeleteErrorMessage").Value, Me._deleteErrorMessage)
            End If
        Catch ex As Exception
            INDTimer.Interval = 5000
            Me._deleteErrorMessage = True
            EventLog.WriteEntry("Error cargando la configuración. Se usa el intervalo por defecto, 5 segundos." & vbCrLf & ex.Message, EventLogEntryType.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Ejecuta el proceso de auditoria (leer los mensajes del MSQM - y grabarlos en a BD).
    ''' </summary>
    Private Sub RunAudit()
        Try
            Dim sb As New StringBuilder()
            sb.AppendLine(DateTime.Now & " - Inicia proceso de auditoria")

            Dim messages As New List(Of IMessage)()
            Dim messagesReaded As New List(Of IMessage)()
            Using store As New MessageStore.MessageStore(New AuditingConfigStore())
                If Me._countDeletedMessages >= MAX_COUNTDELETEDMESSAGES Then
                    store.ShrinkDb()
                    Me._countDeletedMessages = 0
                End If
                messages = store.ListMessages().Where(Function(mm) Not mm.HasError).ToList()
            End Using

            For Each m As IMessage In messages
                If Me._stopping Then 'Si el servicio se detiene, debemos actualizar y descartar los mensajes ya procesados
                    For Each mm As IMessage In messagesReaded
                        Using store As New MessageStore.MessageStore(New IndexingConfigStore())
                            If mm.HasError Then
                                Dim newId = store.UpdateMessage(mm)
                                sb.AppendLine(newId.ToString() & " - ERROR: " & mm.MessageError)
                            Else
                                store.DeleteMessage(mm.IdFile)
                                sb.AppendLine(mm.IdFile.ToString() & " - AUDITED")
                            End If
                        End Using
                    Next
                    Exit Sub
                End If
                If m.Body IsNot Nothing Then
                    Try
                        IndigoAudit.SaveAuditLog(CleanData(m.Body))
                    Catch ex As Exception
                        If Me._deleteErrorMessage Then
                            m.HasError = True
                            m.MessageError = ex.Message & vbCrLf & ex.StackTrace
                        End If
                    End Try
                Else 'Error de cuerpo vacio
                    If Me._deleteErrorMessage Then
                        m.HasError = True
                        m.MessageError = "El cuerpo del mensaje se encuentra nulo o vacio"
                    End If
                End If
                messagesReaded.Add(m)
            Next

            'Debemos actualizar y descartar los mensajes ya procesados
            For Each mm As IMessage In messagesReaded
                Using store As New MessageStore.MessageStore(New AuditingConfigStore())
                    If mm.HasError Then
                        Dim newId = store.UpdateMessage(mm)
                        sb.AppendLine(newId.ToString() & " - ERROR: " & mm.MessageError)
                    Else
                        store.DeleteMessage(mm.IdFile)
                        Me._countDeletedMessages += 1
                        sb.AppendLine(mm.IdFile.ToString() & " - AUDITED")
                    End If
                End Using
            Next
            sb.AppendLine(DateTime.Now & " - Termina proceso de auditoria")
            If messagesReaded.Count > 0 Then 'Si se proceso mensajes
                EventLog.WriteEntry(sb.ToString, EventLogEntryType.Information)
            End If
        Catch ex As Exception
            EventLog.WriteEntry(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - Error auditando documento." & vbCrLf & ex.Message & vbCrLf & ex.StackTrace, EventLogEntryType.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Limpia el dataset de datos que no han cambiado
    ''' </summary>
    ''' <param name="ds">Dataset a limpiar</param>
    ''' <returns>Dataset limpio</returns>
    Private Function CleanData(ByVal ds As DataSet) As DataSet
        Try
            Dim dsResult As New DataSet("Auditoria")
            dsResult.Tables.Add(ds.Tables("Cabecera").Copy())
            Dim Ddt As DataTable = ds.Tables("Detalle").Clone()
            dsResult.Tables.Add(Ddt)
            For Each r As DataRow In ds.Tables("Detalle").Rows
                'Se agrega el registro porque ha cambiado dicha propiedad
                If Not IsDBNull(r("VALOR_ANTERIOR")) And IsDBNull(r("NUEVO_VALOR")) Then
                    dsResult.Tables("Detalle").ImportRow(r)
                End If
                'Se agrega el registro porque ha cambiado dicha propiedad
                If IsDBNull(r("VALOR_ANTERIOR")) And Not IsDBNull(r("NUEVO_VALOR")) Then
                    dsResult.Tables("Detalle").ImportRow(r)
                End If
                'Se agrega el registro porque ha cambiado dicha propiedad
                If Not IsDBNull(r("VALOR_ANTERIOR")) And Not IsDBNull(r("NUEVO_VALOR")) Then
                    If Not r("VALOR_ANTERIOR").ToString().Equals(r("NUEVO_VALOR").ToString()) Then
                        dsResult.Tables("Detalle").ImportRow(r)
                    End If
                End If
            Next
            Return dsResult
        Catch ex As Exception
            EventLog.WriteEntry(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - Error auditando documento." & vbCrLf & ex.Message & vbCrLf & ex.StackTrace, EventLogEntryType.Error)
            Return ds
        End Try
    End Function

#End Region

End Class
