#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MaintenanceRepository

#End Region

Public Class PWorkOrder

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
    Public Function ListScheduledMaintenances(strFilters As String) As List(Of ViewWorkOrderScheduledMaintenanceXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.GetCollection(Of ViewWorkOrderScheduledMaintenanceXpo)(Nothing, strFilters).ToList()
    End Function

    ''' <summary>
    ''' Lista los solicitudes con valoración
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListMaintenanceToBeEvaluated(strFilters As String) As List(Of ViewWorkOrderXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.GetCollection(Of ViewWorkOrderXpo)(Nothing, strFilters).ToList()
    End Function

    ''' <summary>
    ''' Lista los solicitudes con valoración
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListUnscheduledMaintenances(strFilters As String) As List(Of ViewWorkOrderUnScheduledMaintenanceXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.GetCollection(Of ViewWorkOrderUnScheduledMaintenanceXpo)(Nothing, strFilters).ToList()
    End Function

    ''' <summary>
    ''' Carga los usuarios que estan registrados en las plantillas de turnos
    ''' </summary>
    Public Function InitializeListMaintenanceResponsible(responsibleRole As Byte, itemCatalogFilters As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListMaintenanceResponsible(responsibleRole, itemCatalogFilters)
    End Function

    ''' <summary>
    ''' Obtiene las notificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetNotificationsByWorkOrderId(workOrderId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListWorkOrderNotificationsByWorkOrderId(workOrderId)
    End Function

    ''' <summary>
    ''' Obtiene las notificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetNotificationsBySource(entityName As String, entityId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListWorkOrderNotificationsBySource(entityName, entityId)
    End Function

#End Region

End Class
