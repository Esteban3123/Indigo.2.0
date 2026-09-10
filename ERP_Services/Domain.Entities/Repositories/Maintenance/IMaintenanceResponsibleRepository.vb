#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
#End Region

Public Interface IMaintenanceResponsibleRepository

    Inherits IRepository(Of MaintenanceResponsible)

    ''' <summary>
    ''' Función que obtiene todas las Marcas para los Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllResponsible() As List(Of MaintenanceResponsible)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetResponsibleByCode(Code As String) As MaintenanceResponsible

    Function GetItemCatalogByCode(Code As String) As FixedAssetItemCatalog

    Function GetItemCatalogByIdUser(IdResponsible As Integer) As List(Of ResponsibleCatalogOfArticles)
End Interface
