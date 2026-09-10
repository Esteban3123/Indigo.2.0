#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IMaintenanceResponsibleAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Responsible</returns>
    ''' <remarks></remarks>
    Function ListAllResponsible() As List(Of MaintenanceResponsible)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código del Responsible</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetResponsibleByCode(Code As String) As MaintenanceResponsible

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Responsible">Objeto Responsible</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveResponsible(Responsible As MaintenanceResponsible, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceResponsible)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Responsible">Objeto Responsible</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteResponsible(Responsible As MaintenanceResponsible, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Responsible">Objeto Responsible</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function GetItemCatalogByCode(Code As String) As FixedAssetItemCatalog

    Function GetItemCatalogByIdUser(IdResponsible As Integer) As List(Of ResponsibleCatalogOfArticles)

    Function ChangeStateMaintenanceResponsible(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MaintenanceResponsible)
End Interface
