#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IMaintenanceFailureRequestRepository
    Inherits IRepository(Of MaintenanceFailureRequest)

    ''' <summary>
    ''' Función que obtiene una Falla por Id
    ''' </summary>
    ''' <param name="Id">Id de la Falla</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetMaintenanceFailureRequestById(Id As Integer) As MaintenanceFailureRequest

    ''' <summary>
    ''' Función que obtiene una Falla por Código
    ''' </summary>
    ''' <param name="Code">Código de la Falla</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetMaintenanceFailureRequestByCode(Code As String) As MaintenanceFailureRequest

    ''' <summary>
    ''' Función que obtiene todas las Fallas para los Equipos
    ''' </summary>
    ''' <returns>Lista de Fallas</returns>
    ''' <remarks></remarks>
    Function ListAllMaintenanceFailureRequest() As List(Of MaintenanceFailureRequest)

End Interface
