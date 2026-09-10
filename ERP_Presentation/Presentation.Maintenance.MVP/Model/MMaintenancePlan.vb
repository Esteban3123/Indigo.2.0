'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 02-09-2015
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
Imports System.ServiceModel
Imports Domain.Base.Entities

#End Region

Public Class MMaintenancePlan
    Implements IDisposable

    Dim Indigo As SessionValues = SessionValues.Instance

    Private _tagForm As String = "1533"

#Region "Methods"
    Sub New()
        Indigo.AuditMessageWcf.Functional = _tagForm
    End Sub
    ''' <summary>
    ''' Obtener un responsable por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la habitacion</param>
    ''' <returns>objeto tipo habitacion</returns>
    Public Async Function GetMaintenancePlanAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetMaintenancePlanAsync(code, Indigo.AuditMessageWcf, True)
    End Function
    ''' <summary>
    ''' Graba el objeto habitacion en modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo la habitacion</returns>
    Public Async Function SaveMaintenancePlanAsync(ByVal reg As MaintenancePlan, idSequense As Integer) As Task(Of ActionResult(Of MaintenancePlan))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim mess2 As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header2 As System.ServiceModel.Channels.MessageHeader = mess2.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            OperationContext.Current.OutgoingMessageHeaders.Add(header2)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveMaintenancePlanAsync(reg)
        End Using

    End Function
    ''' <summary>
    ''' Elimina el objeto habitacion modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito la habitacion</returns>
    Public Async Function DeleteMaintenancePlanAsync(ByVal reg As MaintenancePlan) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteMaintenancePlanAsync(reg, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todos las Unidades de Medida
    ''' </summary>
    Public Async Function ListAllMeasurementUnit() As Task(Of List(Of MeasurementUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllMeasurementUnitAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Async Function GetNullFields() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULLAsync("MaintenancePlan", Indigo)
    End Function

    'Public Async Function ChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of Responsible))
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ChangeStateResponsibleAsync(Indigo.TransactionalContainer, code, state, Indigo.AuditMessageWcf)
    'End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
