Imports System.Net
Imports System.Security.Cryptography
Imports System.Text
Imports Domain.Base.Entities
Imports Newtonsoft.Json.Linq

Public Class WeatherSingleton

#Region "Properties"


    Private Const cURL As String = "https://api.openweathermap.org/data/2.5/weather"
    Private Const cAppID As String = "6c4e604e2c9270805c54e9c68dd7a157"
    Private cWeatherID As String = String.Empty

    Private Shared singleton As WeatherSingleton
    Private WeatherDictionary As Dictionary(Of String, WeatherDocument)

#End Region

#Region "Builder"

    Private Sub New()
        WeatherDictionary = New Dictionary(Of String, WeatherDocument)()
    End Sub

    Shared ReadOnly Property instance As WeatherSingleton
        Get
            If singleton Is Nothing Then
                singleton = New WeatherSingleton()
            End If
            Return singleton
        End Get
    End Property

#End Region

#Region "Methods"

    Public Function GetWeather(woeidCity As String) As WeatherDocument
        Try
            If WeatherDictionary.ContainsKey(woeidCity) AndAlso DateDiff(DateInterval.Minute, WeatherDictionary(woeidCity).DateReaded, DateTime.Now) < 30 Then
                Return WeatherDictionary(woeidCity)
            End If

            Dim resultsWeather As String = ""

            Dim lURL As String = cURL & "?q=" + woeidCity + "&units=metric" + "&appid=" + cAppID + "&lang=es"
            Using wc As New WebClient()

                resultsWeather = wc.DownloadString(lURL)
            End Using

            Dim dataObject = JObject.Parse(resultsWeather)
            Dim weatherDocument As New WeatherDocument()

            Dim Temperature As Integer = TryCast(dataObject("main"), JObject).Item("temp")
            Dim TemperatureMax As Integer = TryCast(dataObject("main"), JObject).Item("temp_max")
            Dim TemperatureMin As Integer = TryCast(dataObject("main"), JObject).Item("temp_min")

            weatherDocument.DateReaded = DateTime.Now
            weatherDocument.ConditionCode = TryCast(dataObject, JObject).Item("weather")(0)("id")
            weatherDocument.Temperature = Temperature
            weatherDocument.NameCity = TryCast(dataObject, JObject).Item("name")
            weatherDocument.Humidity = TryCast(dataObject("main"), JObject).Item("humidity")
            weatherDocument.Visibility = TryCast(dataObject, JObject).Item("visibility")
            weatherDocument.Pressure = TryCast(dataObject("main"), JObject).Item("pressure")
            weatherDocument.WindSpeed = TryCast(dataObject("wind"), JObject).Item("speed")
            weatherDocument.TempHigh = TemperatureMax
            weatherDocument.TempLow = TemperatureMin
            weatherDocument.CondText = TryCast(dataObject, JObject).Item("weather")(0)("description")

            If WeatherDictionary.ContainsKey(woeidCity) Then
                WeatherDictionary(woeidCity) = weatherDocument
            Else
                WeatherDictionary.Add(woeidCity, weatherDocument)
            End If
            Return WeatherDictionary(woeidCity)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region "Private Methods"

    Private Function _get_timestamp() As String
        Dim lTS As TimeSpan = DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
        Return Convert.ToInt64(lTS.TotalSeconds).ToString()
    End Function

    Private Function _get_nonce() As String
        Return Convert.ToBase64String(New ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()))
    End Function



#End Region

End Class
