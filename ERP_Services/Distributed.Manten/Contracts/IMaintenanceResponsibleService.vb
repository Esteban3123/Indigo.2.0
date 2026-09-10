#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IMaintenanceResponsibleService

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllResponsible(audit As AuditMessage) As List(Of Domain.Entities.MaintenanceResponsible)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetResponsibleByCode(Code As String) As MaintenanceResponsible

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Responsible">Objeto MaintenanceResponsible</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveResponsible(Responsible As Domain.Entities.MaintenanceResponsible, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceResponsible)

    ''' <summary>
    ''' eliminar function
    ''' </summary>
    ''' <param name="Responsible"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteResponsible(Responsible As Domain.Entities.MaintenanceResponsible, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetItemCatalogByCode(Code As String) As FixedAssetItemCatalog

    <OperationContract()>
    Function GetItemCatalogByIdUser(IdResponsible As Integer) As List(Of ResponsibleCatalogOfArticles)

    <OperationContract()>
    Function ChangeStateMaintenanceResponsible(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of MaintenanceResponsible)

End Interface
