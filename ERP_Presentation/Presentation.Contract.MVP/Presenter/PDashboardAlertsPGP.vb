#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class PDashboardAlertsPGP

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    Public Sub New()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los grupos de atencion activos de tipo PGP
    ''' </summary>
    Public Function InitializeCareGroupXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCareGroupByLiquidationTypeAndyStatus(5, True)
    End Function

    ''' <summary>
    ''' Obtiene el responsable de mantenimiento asociada al usuario
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListRequests(strFilters As String) As List(Of ViewDashboardAlertsPGPRequestsXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewDashboardAlertsPGPRequestsXpo)(Nothing, strFilters).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el responsable de mantenimiento asociada al usuario
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListServices(strFilters As String) As List(Of ViewDashboardAlertsPGPServicesXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewDashboardAlertsPGPServicesXpo)(Nothing, strFilters).ToList()
    End Function

#End Region

End Class
