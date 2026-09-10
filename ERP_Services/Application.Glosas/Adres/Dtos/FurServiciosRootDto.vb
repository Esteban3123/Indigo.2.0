'***********************************************************************
' Assembly         : Application.Glosas
' Feature          : Circular Externa 003 de 2026 ADRES - PBI 2 (FUR SERVICIOS)
'***********************************************************************

Imports Newtonsoft.Json

''' <summary>
''' Raíz del JSON FUR SERVICIOS (Circular ADRES 003 de 2026).
''' Coincide 1:1 con la estructura JSON oficial:
'''
'''   [{
'''     "Datos_IPS": { "NIT_PRESTADOR": "..." },
'''     "Datos_servicios_reclamacion": [
'''       { "NUM_FACTURA": "...", "Servicios": [ ... ] }
'''     ]
'''   }]
'''
''' La spec se entrega como un array de un solo objeto raíz; el exporter
''' JSON serializa una lista que contiene este único elemento.
''' </summary>
Public Class FurServiciosRootDto

    <JsonProperty("Datos_IPS")>
    Public Property Datos_IPS As FurServiciosDatosIpsDto

    <JsonProperty("Datos_servicios_reclamacion")>
    Public Property Datos_servicios_reclamacion As List(Of FurServiciosReclamacionDto)

    Public Sub New()
        Me.Datos_IPS = New FurServiciosDatosIpsDto()
        Me.Datos_servicios_reclamacion = New List(Of FurServiciosReclamacionDto)()
    End Sub

End Class
