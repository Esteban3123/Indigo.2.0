'***********************************************************************
' Assembly         : Application.Glosas
' Feature          : Circular Externa 003 de 2026 ADRES - PBI 2 (FUR SERVICIOS)
'***********************************************************************

Imports Newtonsoft.Json

''' <summary>
''' Sección "Datos_IPS" del JSON FUR SERVICIOS.
''' </summary>
Public Class FurServiciosDatosIpsDto

    <JsonProperty("NIT_PRESTADOR")>
    Public Property NIT_PRESTADOR As String

End Class
