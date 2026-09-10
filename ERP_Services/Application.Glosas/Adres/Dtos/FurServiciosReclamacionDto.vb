'***********************************************************************
' Assembly         : Application.Glosas
' Feature          : Circular Externa 003 de 2026 ADRES - PBI 2 (FUR SERVICIOS)
'***********************************************************************

Imports Newtonsoft.Json

''' <summary>
''' Reclamación de servicios por factura. Agrupa los servicios bajo el
''' número de factura correspondiente.
''' </summary>
Public Class FurServiciosReclamacionDto

    <JsonProperty("NUM_FACTURA")>
    Public Property NUM_FACTURA As String

    <JsonProperty("Servicios")>
    Public Property Servicios As List(Of FurServiciosServicioDto)

    Public Sub New()
        Me.Servicios = New List(Of FurServiciosServicioDto)()
    End Sub

End Class
