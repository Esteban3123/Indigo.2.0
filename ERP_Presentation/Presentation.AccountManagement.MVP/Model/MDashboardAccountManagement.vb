Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Domain.AccountManagement.Model

Public Class MDashboardAccountManagement
    Implements IDisposable

    Dim Indigo As SessionValues
    ''' <summary>
    ''' Permite realizar la solicitud de traslado de folios
    ''' </summary>
    ''' <param name="folioTransferRequest"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function RequestFolioTransferToUser(folioTransferRequest As List(Of FolioTransfer), audit As AuditMessage) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.RequestFolioTransferToUserAsync(folioTransferRequest, audit)
    End Function

    ''' <summary>
    ''' Permite suspender (resolver) una alerta creada anteriormente
    ''' </summary>
    ''' <param name="folioAlert"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SuspendFolioAlert(folioAlert As FolioAlert, audit As AuditMessage) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.SuspendFolioAlertAsync(folioAlert, audit)
    End Function

    ''' <summary>
    ''' Permite crear una nueva alerta asociada a un folio
    ''' </summary>
    ''' <param name="folioAlert"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveFolioAlert(folioAlert As FolioAlert, audit As AuditMessage) As Task(Of ActionResult(Of FolioAlert))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.SaveFolioAlertAsync(folioAlert, audit)
    End Function

    ''' <summary>
    ''' Obtiene todas las areas de gestión junto a los usuarios parametrizados
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetManagementAreasWithUsers(managementAreaCode As String, audit As AuditMessage) As Task(Of ActionResult(Of ManagementAreas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetManagementAreasByCodeAsync(managementAreaCode, audit)
    End Function

    ''' <summary>
    ''' Obtiene todas las alertas de traslado de folios
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetAllAlerts() As Task(Of ActionResult(Of List(Of FolioAlert)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetAllAlertsAsync()
    End Function

    ''' <summary>
    ''' Obtiene los eventos de folios por el numero de ingreso
    ''' </summary>
    ''' <param name="attentionCenterCode">Id del centro de atención</param>
    ''' <param name="admissionNumber">Número del ingreso</param>
    ''' <returns></returns>
    Public Async Function ListFolioEventsTraceabilityByAdmission(attentionCenterCode As String, admissionNumber As String, userCode As String) As Task(Of List(Of VFolioTraceabilityProperties))
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.ListFolioEventsAsync(attentionCenterCode, admissionNumber, Nothing, userCode)
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Obtiene los eventos de folios por el código del paciente
    ''' </summary>
    ''' <param name="attentionCenterCode">Id del centro de atención</param>
    ''' <param name="patientCode">Código del paciente</param>
    ''' <returns></returns>
    Public Async Function ListFolioEventsTraceabilityByPatient(attentionCenterCode As String, patientCode As String, userCode As String) As Task(Of List(Of VFolioTraceabilityProperties))
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.ListFolioEventsAsync(attentionCenterCode, Nothing, patientCode, userCode)
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Obtiene la lista de folios asociados a un usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Async Function GetUserFolios(userCode As String) As Task(Of List(Of GetUserFolios))
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetUserFoliosAsync(userCode)
        Return res.ObjectEmbbeded
    End Function
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
