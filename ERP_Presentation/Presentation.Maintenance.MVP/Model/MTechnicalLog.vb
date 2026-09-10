'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 04-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en los consumibles
''' </summary>
Public Class MTechnicalLog
    Implements IDisposable

    Dim Indigo As SessionValues = SessionValues.Instance

    Private _tagForm As String = "576"

#Region "Methods"
    Sub New()
        Indigo.AuditMessageWcf.Functional = _tagForm
    End Sub

    ''' <summary>
    ''' Obtener un registro tecnico por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del registro.</param>
    ''' <returns>objeto registro tecnico</returns>
    Public Async Function GetTechnicalLogAsync(ByVal code As String) As Task(Of TechnicalLog)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetTechnicalLogAsync(Indigo.TransactionalContainer, code, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Graba registro tecnico en modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro tecnico</returns>
    Public Async Function SaveTechnicalLogAsync(ByVal reg As TechnicalLog, ByVal idSequense As Integer) As Task(Of ActionResult(Of TechnicalLog))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveTechnicalLogAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Elimina registro tecnico modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro tecnico</returns>
    Public Async Function DeleteTechnicalLogAsync(ByVal reg As TechnicalLog) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteTechnicalLogAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todos registros tecnicos
    ''' </summary>
    Public Async Function ListAllTechnicalLog() As Task(Of List(Of TechnicalLog))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllTechnicalLogAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("TechnicalLog", Indigo)
    End Function

    Public Async Function ChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of TechnicalLog))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStateTechnicalLogAsync(Indigo.TransactionalContainer, code, state, Indigo.AuditMessageWcf)
    End Function

    Public Function ListTechnicalLog() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListTechnicalLogByState(True)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
