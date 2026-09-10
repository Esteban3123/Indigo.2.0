'***********************************************************************
' Assembly         : Application.Glosas
' Feature          : Circular Externa 003 de 2026 ADRES — FUR RG
'***********************************************************************

Imports Newtonsoft.Json

''' <summary>Raíz del JSON FUR RG (Respuesta a Glosa, Circular ADRES 003 de 2026).</summary>
Public Class FurRgRootDto

    <JsonProperty("Datos_IPS")>
    Public Property Datos_IPS As FurDatosIpsDto

    <JsonProperty("Datos_reclamacion")>
    Public Property Datos_reclamacion As List(Of FurRgReclamacionDto)

    Public Sub New()
        Me.Datos_IPS = New FurDatosIpsDto()
        Me.Datos_reclamacion = New List(Of FurRgReclamacionDto)()
    End Sub

End Class

''' <summary>
''' Reclamación FUR RG por factura. Contiene los campos RG específicos
''' (NUM_FACTURA, Numero_radicacion, CreditNote, Valor_reclamado), los
''' sub-objetos FUR (víctima, evento, vehículo, ...) con el subset de
''' campos exigido por la Circular 003 para FUR RG, y el array
''' <c>Datos_de_la_glosa</c> con una entrada por cada item glosado de
''' la factura.
''' </summary>
Public Class FurRgReclamacionDto

    <JsonProperty("NUM_FACTURA")>
    Public Property NUM_FACTURA As String

    <JsonProperty("Num_factura_anterior")>
    Public Property Num_factura_anterior As String

    ''' <summary>Radicado Cliente (alfanumérico) — emitido como string.</summary>
    <JsonProperty("Numero_radicacion")>
    Public Property Numero_radicacion As String

    ''' <summary>Consecutivo electrónico de Nota Crédito asociada, o null.</summary>
    <JsonProperty("CreditNote_o_Debit_note")>
    Public Property CreditNote_o_Debit_note As String

    ''' <summary>Valor pendiente por conciliar acumulado para la factura.</summary>
    <JsonProperty("Valor_reclamado")>
    Public Property Valor_reclamado As Decimal?

    ' --- Secciones FUR (subset FUR RG) ---

    <JsonProperty("Datos_de_la_victima")>
    Public Property Datos_de_la_victima As FurRgDatosVictimaDto

    <JsonProperty("Datos_del_sitio_donde_ocurrio_el_evento")>
    Public Property Datos_del_sitio_donde_ocurrio_el_evento As FurRgDatosSitioEventoDto

    <JsonProperty("Datos_del_Vehiculo_Involucrado_en_el_Accidente_de_Transito")>
    Public Property Datos_del_Vehiculo_Involucrado_en_el_Accidente_de_Transito As FurDatosVehiculoDto

    <JsonProperty("Datos_del_propietario_del_vehiculo")>
    Public Property Datos_del_propietario_del_vehiculo As FurDatosPropietarioDto

    <JsonProperty("Datos_del_conductor_del_vehiculo_involucrado")>
    Public Property Datos_del_conductor_del_vehiculo_involucrado As FurDatosConductorDto

    <JsonProperty("Datos_Relacionados_con_la_Atencion_de_La_Victima")>
    Public Property Datos_Relacionados_con_la_Atencion_de_La_Victima As FurDatosAtencionVictimaDto

    <JsonProperty("Datos_de_remision")>
    Public Property Datos_de_remision As FurRgDatosRemisionDto

    <JsonProperty("Datos_Transporte_y_movilizacion_de_la_victima")>
    Public Property Datos_Transporte_y_movilizacion_de_la_victima As FurRgDatosTransporteDto

    ' --- Array de glosas (específico FUR RG) ---

    <JsonProperty("Datos_de_la_glosa")>
    Public Property Datos_de_la_glosa As List(Of FurRgDatosGlosaDto)

    Public Sub New()
        Me.Datos_de_la_glosa = New List(Of FurRgDatosGlosaDto)()
    End Sub

End Class

''' <summary>
''' Sección "Datos_de_la_victima" del JSON FUR RG (subset Circular 003).
''' </summary>
Public Class FurRgDatosVictimaDto

    <JsonProperty("Direccion_residencia_victima")>
    Public Property Direccion_residencia_victima As String

    <JsonProperty("Telefono_victima")>
    Public Property Telefono_victima As String

End Class

''' <summary>
''' Sección "Datos_del_sitio_donde_ocurrio_el_evento" del JSON FUR RG
''' (subset Circular 003).
''' </summary>
Public Class FurRgDatosSitioEventoDto

    <JsonProperty("Condicion_victima")>
    Public Property Condicion_victima As String

    <JsonProperty("Fecha_de_ocurrencia_evento")>
    Public Property Fecha_de_ocurrencia_evento As String

    <JsonProperty("Zona_de_ocurrencia_evento")>
    Public Property Zona_de_ocurrencia_evento As String

    <JsonProperty("Codigo_municipio_ocurrencia_evento")>
    Public Property Codigo_municipio_ocurrencia_evento As String

    <JsonProperty("Direccion_de_ocurrencia_evento")>
    Public Property Direccion_de_ocurrencia_evento As String

    <JsonProperty("Descripcion_corta_de_lo_ocurrido_en_el_evento")>
    Public Property Descripcion_corta_de_lo_ocurrido_en_el_evento As String

End Class

''' <summary>
''' Sección "Datos_de_remision" del JSON FUR RG (subset Circular 003).
''' </summary>
Public Class FurRgDatosRemisionDto

    ''' <summary>Misma fuente que <c>Placa_ambulancia_que_realiza_el_traslado_secundario</c>; campo exigido por ADRES con nombre distinto.</summary>
    <JsonProperty("Placa_ambulancia_que_realiza_la_remision")>
    Public Property Placa_ambulancia_que_realiza_la_remision As String

    <JsonProperty("Placa_ambulancia_que_realiza_el_traslado_secundario")>
    Public Property Placa_ambulancia_que_realiza_el_traslado_secundario As String

    <JsonProperty("Codigo_de_habilitacion_del_prestador_que_remite")>
    Public Property Codigo_de_habilitacion_del_prestador_que_remite As String

    ''' <summary>Catálogo permitido: CC, CE, CD, PA, PE, DE, PT.</summary>
    <JsonProperty("TIPO_de_documento_Profesional_que_recibe")>
    Public Property TIPO_de_documento_Profesional_que_recibe As String

    <JsonProperty("Numero_de_documento_Profesional_que_recibe")>
    Public Property Numero_de_documento_Profesional_que_recibe As String

    <JsonProperty("Codigo_de_habilitacion_del_prestador_que_recibe")>
    Public Property Codigo_de_habilitacion_del_prestador_que_recibe As String

    ''' <summary>Formato AAAA-MM-DD (10 caracteres).</summary>
    <JsonProperty("Fecha_de_aceptacion")>
    Public Property Fecha_de_aceptacion As String

    ''' <summary>Formato HH:MM 24 horas (5 caracteres).</summary>
    <JsonProperty("Hora_aceptacion")>
    Public Property Hora_aceptacion As String

End Class

''' <summary>
''' Sección "Datos_Transporte_y_movilizacion_de_la_victima" del JSON
''' FUR RG (subset Circular 003).
''' </summary>
Public Class FurRgDatosTransporteDto

    <JsonProperty("Placa_ambulancia_que_realiza_el_traslado")>
    Public Property Placa_ambulancia_que_realiza_el_traslado As String

    <JsonProperty("Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion")>
    Public Property Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion As String

    <JsonProperty("Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS")>
    Public Property Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS As String

End Class

''' <summary>
''' Item del array <c>Datos_de_la_glosa</c> del JSON FUR RG. Una entrada
''' por cada item glosado (GlosaMovementGlosa) de la factura.
''' </summary>
Public Class FurRgDatosGlosaDto

    ''' <summary>Código ADRES de la factura (se repite por cada glosa de la misma factura).</summary>
    <JsonProperty("ID_interno_Glosa")>
    Public Property ID_interno_Glosa As String

    ''' <summary>Código SOAT/CUPS del item glosado.</summary>
    <JsonProperty("itemID_servicio_o_tecnologia_objetado")>
    Public Property itemID_servicio_o_tecnologia_objetado As String

    ''' <summary>Code del ConceptGlosas apuntado por GlosaMovementGlosa.CodeGlosaId.</summary>
    <JsonProperty("Codigo_glosa")>
    Public Property Codigo_glosa As String

    ''' <summary>Primeros 4 caracteres del Code del ConceptGlosas apuntado por IdGlosaEvaluation.</summary>
    <JsonProperty("Tipo_respuesta_a_glosa")>
    Public Property Tipo_respuesta_a_glosa As String

    ''' <summary>JustificationGlosaText (texto plano) del movimiento de glosa.</summary>
    <JsonProperty("Respuesta_a_glosa")>
    Public Property Respuesta_a_glosa As String

    ''' <summary>Cantidad del detalle de factura; null si no hay Nota Crédito o no hay valor aceptado.</summary>
    <JsonProperty("Cantidad_aceptada")>
    Public Property Cantidad_aceptada As Integer?

    ''' <summary>ValueAcceptedIPSconciliation del movimiento; null si no existe.</summary>
    <JsonProperty("Valor_aceptado")>
    Public Property Valor_aceptado As Decimal?

    ''' <summary>FirstName + ' ' + FirstLastName del usuario que confirma la conciliación.</summary>
    <JsonProperty("Primer_nombre_primer_apellido_auditor")>
    Public Property Primer_nombre_primer_apellido_auditor As String

    ''' <summary>Position del usuario que confirma la conciliación.</summary>
    <JsonProperty("Perfil_auditor")>
    Public Property Perfil_auditor As String

End Class
