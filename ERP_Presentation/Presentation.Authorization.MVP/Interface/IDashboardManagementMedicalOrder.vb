Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.AuthorizationRepository

Public Interface IDashboardManagementMedicalOrder

#Region "Datasource"

    ''' <summary>
    ''' Establece el datasource de los centros de atención
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CentersXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de las solicitudes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListRequestsForManagementMedicalOrderXpo As List(Of ViewListRequestsForManagementMedicalOrderXpo)

#End Region

End Interface
