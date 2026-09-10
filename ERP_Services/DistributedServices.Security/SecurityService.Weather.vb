Imports DistributedServices.Weather
Imports Domain.Base.Entities

Partial Class SecurityService

    ''' <summary>
    ''' Funcion para obtener los datos del clima
    ''' </summary>
    ''' <param name="woeidCity">Nombre de la ciudad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWeatherCity(woeidCity As String) As ActionMessageResult(Of WeatherDocument) Implements ISecurityService.GetWeatherCity
        Dim weatherService As New WeatherService()
        Return weatherService.GetWeatherCity(woeidCity)
    End Function

End Class
