Imports Domain.AccountManagement.Model
Imports Domain.Base

Public Interface IFolioTransferRepository
    Inherits IRepository(Of FolioTransfer)

    ''' <summary>
    ''' Obtiene un traslado de folio por su Id
    ''' </summary>
    ''' <param name="folioTransferId"></param>
    ''' <returns></returns>
    Function GetFolioTransferById(folioTransferId As Integer) As FolioTransfer

    ''' <summary>
    ''' Obtiene una lista de todos los eventos de folios y también permite filtrar por numero admission, codigo de paciente o centro de atención
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="patientCode">Código del paciente</param>
    ''' <param name="attentionCenter">Centro de atención</param>
    ''' <returns></returns>
    Function ListFolioEvents(attentionCenter As String, Optional admissionNumber As String = Nothing, Optional patientCode As String = Nothing, Optional userCode As String = Nothing) As List(Of VFolioTraceabilityProperties)

    ''' <summary>
    '''  Obtiene una lista de traslados de folios de acuerdo a un centro de atención y un código de usuario
    ''' </summary>
    ''' <param name="attentionCenter"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function ListFolioTransferRequests(attentionCenter As String, userCode As String, managementAreaCode As String) As List(Of VDashboardProperties)

End Interface
