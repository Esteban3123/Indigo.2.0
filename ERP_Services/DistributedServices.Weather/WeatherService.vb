Imports Newtonsoft.Json.Linq
Imports System.Text
Imports System.Net
Imports Domain.Base.Entities


Public Class WeatherService

    ''' <summary>
    ''' Funcion para obtener los datos del clima
    ''' </summary>
    ''' <param name="woeidCity">Nombre de la ciudad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWeatherCity(woeidCity As String) As ActionMessageResult(Of WeatherDocument)
        Dim result As New ActionMessageResult(Of WeatherDocument)()
        result.StateResult = True
        Try
            result.ObjectEmbbeded = WeatherSingleton.instance.GetWeather(woeidCity)
            Return result
        Catch ex As Exception
            result.ObjectEmbbeded = Nothing
            result.StateResult = False
        End Try
        Return result
    End Function

End Class
