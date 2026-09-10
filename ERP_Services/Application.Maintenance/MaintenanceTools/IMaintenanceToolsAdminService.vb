#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IMaintenanceToolsAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Tools</returns>
    ''' <remarks></remarks>
    Function ListAllTools() As List(Of MaintenanceTools)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código del Tools</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetToolsByCode(Code As String) As MaintenanceTools

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Tools">Objeto Tools</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveTools(Tools As MaintenanceTools, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceTools)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Tools">Objeto Tools</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteTools(Tools As MaintenanceTools, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function GetItemByCode(Code As String) As FixedAssetItem

    Function GetItemDetailByIdUser(IdTools As Integer) As List(Of MaintenanceToolsItemDetail)

    'Function ChangeStateMaintenanceTools(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MaintenanceTools)
End Interface
