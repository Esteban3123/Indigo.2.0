#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IMaintenanceToolsService

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllTools(audit As AuditMessage) As List(Of Domain.Entities.MaintenanceTools)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetToolsByCode(Code As String) As MaintenanceTools

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Tools">Objeto MaintenanceTools</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveTools(Tools As Domain.Entities.MaintenanceTools, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceTools)

    ''' <summary>
    ''' eliminar function
    ''' </summary>
    ''' <param name="Tools"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteTools(Tools As Domain.Entities.MaintenanceTools, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetItemByCode(Code As String) As FixedAssetItem

    <OperationContract()>
    Function GetItemDetailByIdUser(IdTools As Integer) As List(Of MaintenanceToolsItemDetail)

    '<OperationContract()>
    'Function ChangeStateMaintenanceTools(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of MaintenanceTools)

End Interface
