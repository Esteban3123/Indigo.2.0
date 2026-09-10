'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Juan F. Tamayo
' Created          : 2013-07-15
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-07-15
' Description      : Frontal customizable base
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Exceptions
Imports System.IO
Imports DevExpress.XtraLayout

#End Region

''' <summary>
''' Frontal customizable base
''' </summary>
Public Class CustomizableFormBase

#Region "Fields"

    ''' <summary>
    ''' Consjuto de datos que contiene los campos personalizables
    ''' </summary>
    Private _customizableFields As DataTable
    ''' <summary>
    ''' Bandera usada para verificar si existe o no una definicion del funcional
    ''' </summary>
    Private _frontDefinicionExists As Boolean
    ''' <summary>
    ''' Ruta de las definiciones del layout
    ''' </summary>
    Private _pathFunctionalDefinitions As String

    Private _pathFunctionalControlDefinition As String
    ''' <summary>
    ''' Hilo para cargar las definiciones del funcional
    ''' </summary>
    Private WithEvents DefinitionsLoader As BackgroundWorker

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el nombre del módulo al que pertenece el frontal
    ''' </summary>
    ''' <value>Nombre del módulo al que pertenece el frontal</value>
    ''' <returns>El nombre del módulo al que pertenece el frontal</returns>
    ''' <remarks>Si se va a habilitar la customización del frontal es necesario especificar ésta propiedad, de lo contario dicha funcionalidad no trabajará.</remarks>
    <Category("Indigo")> _
    <Description("Obtiene o asigna el nombre del módulo al que pertenece el frontal")>
    Public Property OwnerModule As String

    ''' <summary>
    ''' Obtiene o asigna el nombre del esquema al que pertenece la tabla donde se almacena la entidad principal del frontal
    ''' </summary>
    ''' <value>Nombre del esquema</value>
    ''' <returns>El nombre del esquema</returns>
    ''' <remarks>Si se va a habilitar la customización del frontal es necesario especificar ésta propiedad, de lo contario dicha funcionalidad no trabajará.</remarks>
    <Category("Indigo")> _
    <Description("Obtiene o asigna el nombre del esquema al que pertenece la tabla donde se almacena la entidad principal del frontal")>
    Public Property Schema As String

    ''' <summary>
    ''' Obtiene o asigna el nombre de la tabla a la que pertenece la entidad principal del frontal
    ''' </summary>
    ''' <value>Nombre de la tabla</value>
    ''' <returns>El nombre de la tabla</returns>
    ''' <remarks>Si se va a habilitar la customización del frontal es necesario especificar ésta propiedad, de lo contario dicha funcionalidad no trabajará.</remarks>
    <Category("Indigo")> _
    <Description("Obtiene o asigna el nombre de la tabla a la que pertenece la entidad principal del frontal")>
    Public Property TableName As String

#End Region

#Region "Methods"

    ''' <summary>
    ''' Abrir el formulario de personalizacion del frontal
    ''' </summary>
    Protected Sub OpenCustomize()
        Me.INDlycRoot.ShowCustomizationForm()
    End Sub


    ''' <summary>
    ''' Restablecer las definiciones del formulario
    ''' </summary>
    Protected Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(SessionValues.Instance.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(SessionValues.Instance.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            Me.INDlycRoot.RestoreDefaultLayout()
            MessageIndigo.Show(obtenerRecurso(Eresources.ComunesLayoutRestablecido), MessageType.Information, Me.Text)
        End If
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se carga la funcionalidad de customización
    ''' </summary>
    Private Sub CustomizableFormBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.OwnerModule IsNot Nothing AndAlso Not Me.OwnerModule.Trim().Equals(String.Empty) AndAlso Me.Schema IsNot Nothing AndAlso Not Me.Schema.Trim().Equals(String.Empty) AndAlso Me.TableName IsNot Nothing AndAlso Not Me.TableName.Trim().Equals(String.Empty) Then
            'Cargamos de manera asincrona definiciones del funcional
            Me._pathFunctionalDefinitions = String.Concat(SessionValues.Instance.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\" & Me.OwnerModule.Trim() & ".", Me.Name, ".xml")
            Me._pathFunctionalControlDefinition = String.Concat(SessionValues.Instance.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\" & Me.OwnerModule.Trim() & ".", Me.Name, ".Controls", ".dat")
            Me.DefinitionsLoader = New BackgroundWorker()
            If Me.DefinitionsLoader.IsBusy = False Then
                Me.DefinitionsLoader.RunWorkerAsync()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se verifica asincronamente si existe una definicion xml del frontal
    ''' </summary>
    Private Sub DefinitionsLoader_DoWork(sender As Object, e As DoWorkEventArgs) Handles DefinitionsLoader.DoWork
        If Me.OwnerModule IsNot Nothing AndAlso Not Me.OwnerModule.Trim().Equals(String.Empty) AndAlso Me.Schema IsNot Nothing AndAlso Not Me.Schema.Trim().Equals(String.Empty) AndAlso Me.TableName IsNot Nothing AndAlso Not Me.TableName.Trim().Equals(String.Empty) Then
            If CustomizacionFrontales.VerificaExisteDefinicionFrontal(Me._pathFunctionalDefinitions) = True Then
                Me._frontDefinicionExists = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Carga asincronamente la definicion del frontal
    ''' </summary>
    Private Sub DefinitionsLoader_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles DefinitionsLoader.RunWorkerCompleted
        If Me.OwnerModule IsNot Nothing AndAlso Not Me.OwnerModule.Trim().Equals(String.Empty) AndAlso Me.Schema IsNot Nothing AndAlso Not Me.Schema.Trim().Equals(String.Empty) AndAlso Me.TableName IsNot Nothing AndAlso Not Me.TableName.Trim().Equals(String.Empty) Then
            If Me._frontDefinicionExists = True Then
                Dim stf As Stream = File.Open(Me._pathFunctionalControlDefinition, FileMode.Open)
                Dim br As New BinaryReader(stf)
                Dim dat As String = ""
                While (br.PeekChar() <> -1)
                    dat = br.ReadString()
                    Dim nc As String = br.ReadString()
                    Dim t As Type = CustomHelper.ListCustomControls()(dat)
                    Dim cont As Control = Nothing
                    If t IsNot Nothing Then
                        cont = Activator.CreateInstance(t)
                    End If
                    If cont IsNot Nothing Then
                        cont.Name = nc
                        Me.INDlycRoot.Controls.Add(cont)
                    End If
                End While
                Me.INDlycRoot.RestoreLayoutFromXml(Me._pathFunctionalDefinitions)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se guardan las modificaciones realizadas en la definicion del frontal
    ''' </summary>
    Private Sub INDlycRoot_HideCustomization(sender As Object, e As EventArgs) Handles INDlycRoot.HideCustomization
        If Me.OwnerModule IsNot Nothing AndAlso Not Me.OwnerModule.Trim().Equals(String.Empty) AndAlso Me.Schema IsNot Nothing AndAlso Not Me.Schema.Trim().Equals(String.Empty) AndAlso Me.TableName IsNot Nothing AndAlso Not Me.TableName.Trim().Equals(String.Empty) Then
            'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
            If Me.INDlycRoot.IsModified = True Then
                Try
                    If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(SessionValues.Instance.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                        Me.INDlycRoot.SaveLayoutToXml(Me._pathFunctionalDefinitions)
                        If File.Exists(Me._pathFunctionalControlDefinition) Then
                            File.Delete(Me._pathFunctionalControlDefinition)
                        End If
                        Dim br As New BinaryWriter(File.Open(Me._pathFunctionalControlDefinition, FileMode.CreateNew))
                        For Each it In Me.INDlycRoot.Items
                            If it.GetType().Equals(GetType(LayoutControlItem)) Then
                                If it.Control.GetType().Equals(GetType(Presentation.Controls.TextEditCustom)) Then
                                    br.Write(it.Control.GetType().Name.Replace("Custom", "").ToString())
                                    br.Write(DirectCast(it.Control, Presentation.Controls.TextEditCustom).Name)
                                End If
                            End If
                        Next
                        br.Flush()
                        br.Close()
                        'Else
                        '    Me.EscribirVisorEventos(EeventViewerImages.Advertencia, obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion))
                    End If
                Catch ex As Exception
                    IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se evalua si existe el permiso de personalizar el frontal para permitir mostrar el frontal de personalizacion o no
    ''' </summary>
    Private Sub INDlycRoot_ShowCustomization(sender As Object, e As EventArgs) Handles INDlycRoot.ShowCustomization
        'If Me.OwnerModule IsNot Nothing AndAlso Not Me.OwnerModule.Trim().Equals(String.Empty) AndAlso Me.Schema IsNot Nothing AndAlso Not Me.Schema.Trim().Equals(String.Empty) AndAlso Me.TableName IsNot Nothing AndAlso Not Me.TableName.Trim().Equals(String.Empty) Then
        '    Try
        '        'Ejecuatamos la consulta
        '        Dim dsFields As DataSet = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetFieldsNULLAsync(Me.Schema.Trim(), Me.TableName.Trim())
        '        If dsFields IsNot Nothing Then
        '            Me._customizableFields = dsFields.Tables(0)
        '            For i As Integer = 0 To Me._customizableFields.Rows.Count - 1
        '                For j As Integer = 0 To Me.INDlycRoot.Items.Count - 1
        '                    If Object.Equals(Me.INDlycRoot.Items.Item(j).Tag, Nothing) = False Then
        '                        If Me._customizableFields.Rows(i).Item("NAME").ToString.Trim = Me.INDlycRoot.Items.Item(j).Tag.ToString.Trim Then
        '                            Me.INDlycRoot.Items.Item(j).AllowHide = True
        '                        End If
        '                    End If
        '                Next
        '            Next
        '        End If
        '    Catch ex As Exception
        '        IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
        '        Me.EscribirVisorEventos(EeventViewerImages.MensajeError, obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion))
        '    End Try
        'End If

    End Sub

#End Region

End Class