#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
#End Region

Public Interface IMaintenanceManufacturersRepository

    Inherits IRepository(Of MaintenanceManufacturers)

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllMaintenanceManufacturers() As List(Of MaintenanceManufacturers)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>MaintenanceManufacturers</returns>
    ''' <remarks></remarks>
    Function GetMaintenanceManufacturersByCode(Code As String, Optional desatach As Boolean = True) As MaintenanceManufacturers

End Interface
