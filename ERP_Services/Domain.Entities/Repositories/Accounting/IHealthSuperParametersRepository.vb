#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IHealthSuperParametersRepository
    Inherits IRepository(Of HealthSuperParameters)

    ''' <summary>
    ''' Obtiene un formato  por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetHealthSuperParametersById(id As Integer) As HealthSuperParameters

    ''' <summary>
    ''' Obtiene un formato  por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetHealthSuperParametersByCode(code As String) As HealthSuperParameters

    ''' <summary>
    ''' Obtienen todos los datos de la tabla
    ''' </summary>
    ''' <returns></returns>
    Function GetHealthSuperParametersAll() As List(Of HealthSuperParameters)

End Interface
