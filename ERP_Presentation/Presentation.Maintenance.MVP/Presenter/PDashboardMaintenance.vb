#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MaintenanceRepository

#End Region

Public Class PDashboardMaintenance

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
    ''' Obtiene el responsable de mantenimiento asociada al usuario
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetMaintenanceResponsibleByUserCoder() As ViewMaintenanceResponsibleUserXpo
        Dim filter As String = String.Format("UserCode = '{0}'", Me.Indigo.AuditMessageWcf.CodeUser)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.GetCollection(Of ViewMaintenanceResponsibleUserXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Carga las unidades funcionales
    ''' </summary>
    Public Function InitializeMaintenanceResponsibleItemCatalog(responsibleId As Integer) As List(Of ViewMaintenanceResponsibleItemCatalogXpo)
        Dim filter As String = $"ResponsibleId = {responsibleId}"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.GetCollection(Of ViewMaintenanceResponsibleItemCatalogXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListRequests(ItemCatalogFilters As String) As List(Of ViewListRequestsXpo)
        Dim filter As String = "WorkOrderId IS NULL AND ItemCatalogId IN (" & ItemCatalogFilters & ")"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.GetCollection(Of ViewListRequestsXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista los solicitudes con valoración
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListRequestsWithWorkOrder(ItemCatalogFilters As String, maintenanceResponsible As ViewMaintenanceResponsibleUserXpo) As List(Of ViewListRequestsXpo)
        Dim filter As String = "WorkOrderId IS NOT NULL AND ItemCatalogId IN (" & ItemCatalogFilters & ")"
        If maintenanceResponsible.ResponsibleRole = 4 Then
            filter = String.Format("{0}  AND (MaintenanceResponsibleId = {1})", filter, maintenanceResponsible.Id)
        ElseIf maintenanceResponsible.ResponsibleRole = 3 Then
            filter = String.Format("{0}  AND (ISNULL(MaintenanceResponsibleId, {1}) = {1} OR ResponsibleRole IN ({2}))", filter, maintenanceResponsible.Id, "4,5")
        ElseIf maintenanceResponsible.ResponsibleRole = 2 Then
            filter = String.Format("{0}  AND (ISNULL(MaintenanceResponsibleId, {1}) = {1} OR ResponsibleRole IN ({2}))", filter, maintenanceResponsible.Id, "3,4,5")
        ElseIf maintenanceResponsible.ResponsibleRole = 1 Then
            filter = String.Format("{0}  AND (ISNULL(MaintenanceResponsibleId, {1}) = {1} OR ResponsibleRole IN ({2}))", filter, maintenanceResponsible.Id, "2,3,4,5")
        End If
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.GetCollection(Of ViewListRequestsXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Carga los usuarios que estan registrados en las plantillas de turnos
    ''' </summary>
    Public Function InitializeListMaintenanceResponsible(responsibleRole As Byte, itemCatalogFilters As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListMaintenanceResponsible(responsibleRole, itemCatalogFilters)
    End Function

#End Region

End Class
