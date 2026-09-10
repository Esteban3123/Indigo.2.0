'***********************************************************************
' Assembly         : Application.Glosas
' Feature          : Circular Externa 003 de 2026 ADRES - PBI 2 (FUR SERVICIOS)
'***********************************************************************

Imports Newtonsoft.Json

''' <summary>
''' Servicio individual reclamado dentro de una factura. Estructura
''' alineada exactamente con la spec ADRES Circular 003.
''' </summary>
Public Class FurServiciosServicioDto

    <JsonProperty("Tipo_de_servicio")>
    Public Property Tipo_de_servicio As String

    <JsonProperty("Codigo_del_servicio")>
    Public Property Codigo_del_servicio As String

    <JsonProperty("Codigo_general_del_procedimiento_quirurgico")>
    Public Property Codigo_general_del_procedimiento_quirurgico As String

    <JsonProperty("Consecutivo_procedimiento_quirurgico")>
    Public Property Consecutivo_procedimiento_quirurgico As String

    <JsonProperty("Codificacion_CUPS")>
    Public Property Codificacion_CUPS As String

    <JsonProperty("Descripcion_del_servicio_o_elemento_reclamado")>
    Public Property Descripcion_del_servicio_o_elemento_reclamado As String

    <JsonProperty("Cantidad_de_servicios")>
    Public Property Cantidad_de_servicios As String

    <JsonProperty("Valor_unitario_facturado")>
    Public Property Valor_unitario_facturado As String

    <JsonProperty("Valor_unitario_reclamado")>
    Public Property Valor_unitario_reclamado As String

    <JsonProperty("Valor_total_facturado")>
    Public Property Valor_total_facturado As String

    <JsonProperty("Valor_total_reclamado")>
    Public Property Valor_total_reclamado As String

End Class
