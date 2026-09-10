#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
#End Region

Public Interface IMaintenanceToolsRepository

    Inherits IRepository(Of MaintenanceTools)

    ''' <summary>
    ''' Función que obtiene todas las Marcas para los Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllTools() As List(Of MaintenanceTools)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetToolsByCode(Code As String) As MaintenanceTools

    Function GetItemByCode(Code As String) As FixedAssetItem

    Function GetItemByIdTool(IdTools As Integer) As List(Of MaintenanceToolsItemDetail)
End Interface
