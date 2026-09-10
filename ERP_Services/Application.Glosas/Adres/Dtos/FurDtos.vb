'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Felix Camilo Salazar Roldan
' Created          : 2026 24-05-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Newtonsoft.Json

''' <summary>Raíz del JSON FUR (Circular ADRES 003 de 2026).</summary>
Public Class FurRootDto

    <JsonProperty("Datos_IPS")>
    Public Property Datos_IPS As FurDatosIpsDto

    <JsonProperty("Datos_reclamacion")>
    Public Property Datos_reclamacion As List(Of FurReclamacionDto)

    Public Sub New()
        Me.Datos_IPS = New FurDatosIpsDto()
        Me.Datos_reclamacion = New List(Of FurReclamacionDto)()
    End Sub

End Class

''' <summary>Sección "Datos_IPS" del JSON FUR.</summary>
Public Class FurDatosIpsDto

    <JsonProperty("NIT_PRESTADOR")>
    Public Property NIT_PRESTADOR As String

End Class

''' <summary>
''' Reclamación FUR por factura. Agrupa los 8 sub-objetos exigidos por la
''' Circular ADRES 003 bajo el número de factura correspondiente.
''' </summary>
Public Class FurReclamacionDto

    <JsonProperty("NUM_FACTURA")>
    Public Property NUM_FACTURA As String

    <JsonProperty("Datos_de_la_victima")>
    Public Property Datos_de_la_victima As FurDatosVictimaDto

    <JsonProperty("Datos_del_sitio_donde_ocurrio_el_evento")>
    Public Property Datos_del_sitio_donde_ocurrio_el_evento As FurDatosSitioEventoDto

    <JsonProperty("Datos_del_Vehiculo_Involucrado_en_el_Accidente_de_Transito")>
    Public Property Datos_del_Vehiculo_Involucrado_en_el_Accidente_de_Transito As FurDatosVehiculoDto

    <JsonProperty("Datos_del_propietario_del_vehiculo")>
    Public Property Datos_del_propietario_del_vehiculo As FurDatosPropietarioDto

    <JsonProperty("Datos_del_conductor_del_vehiculo_involucrado")>
    Public Property Datos_del_conductor_del_vehiculo_involucrado As FurDatosConductorDto

    <JsonProperty("Datos_Relacionados_con_la_Atencion_de_La_Victima")>
    Public Property Datos_Relacionados_con_la_Atencion_de_La_Victima As FurDatosAtencionVictimaDto

    <JsonProperty("Datos_de_remision")>
    Public Property Datos_de_remision As FurDatosRemisionDto

    <JsonProperty("Datos_Transporte_y_movilizacion_de_la_victima")>
    Public Property Datos_Transporte_y_movilizacion_de_la_victima As FurDatosTransporteDto

End Class

''' <summary>Sección "Datos_de_la_victima" del JSON FUR.</summary>
Public Class FurDatosVictimaDto

    <JsonProperty("Tipo_documento_identidad_victima")>
    Public Property Tipo_documento_identidad_victima As String

    <JsonProperty("Numero_documento_identidad_victima")>
    Public Property Numero_documento_identidad_victima As String

    <JsonProperty("Tipo_de_poblacion_especial")>
    Public Property Tipo_de_poblacion_especial As String

    <JsonProperty("Primer_nombre_victima")>
    Public Property Primer_nombre_victima As String

    <JsonProperty("Segundo_nombre_victima")>
    Public Property Segundo_nombre_victima As String

    <JsonProperty("Primer_apellido_victima")>
    Public Property Primer_apellido_victima As String

    <JsonProperty("Segundo_apellido_victima")>
    Public Property Segundo_apellido_victima As String

    <JsonProperty("Direccion_residencia_victima")>
    Public Property Direccion_residencia_victima As String

    <JsonProperty("Codigo_municipio_residencia_victima")>
    Public Property Codigo_municipio_residencia_victima As String

    <JsonProperty("Telefono_victima")>
    Public Property Telefono_victima As String

End Class

''' <summary>Sección "Datos_del_sitio_donde_ocurrio_el_evento" del JSON FUR.</summary>
Public Class FurDatosSitioEventoDto

    <JsonProperty("Naturaleza_del_evento")>
    Public Property Naturaleza_del_evento As String

    <JsonProperty("Descripcion_del_otro_evento")>
    Public Property Descripcion_del_otro_evento As String

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
''' Sección "Datos_del_Vehiculo_Involucrado_en_el_Accidente_de_Transito"
''' del JSON FUR. Campos NULL cuando NaturalezaEvento ≠ '01'.
''' </summary>
Public Class FurDatosVehiculoDto

    <JsonProperty("Estado_de_aseguramiento")>
    Public Property Estado_de_aseguramiento As String

    <JsonProperty("Placa_vehiculo")>
    Public Property Placa_vehiculo As String

    <JsonProperty("Tipo_de_Vehiculo")>
    Public Property Tipo_de_Vehiculo As String

    <JsonProperty("Codigo_de_la_aseguradora")>
    Public Property Codigo_de_la_aseguradora As String

    <JsonProperty("Numero_de_poliza_SOAT")>
    Public Property Numero_de_poliza_SOAT As String

    <JsonProperty("Fecha_de_inicio_de_vigencia_de_la_poliza")>
    Public Property Fecha_de_inicio_de_vigencia_de_la_poliza As String

    <JsonProperty("Fecha_final_de_vigencia_de_la_poliza")>
    Public Property Fecha_final_de_vigencia_de_la_poliza As String

    <JsonProperty("Numero_de_radicado_SIRAS")>
    Public Property Numero_de_radicado_SIRAS As String

    <JsonProperty("Cobro_por_agotamiento_tope_Aseguradora")>
    Public Property Cobro_por_agotamiento_tope_Aseguradora As String

End Class

''' <summary>
''' Sección "Datos_del_propietario_del_vehiculo" del JSON FUR. Campos
''' NULL cuando NaturalezaEvento ≠ '01' (Accidente Tránsito).
''' </summary>
Public Class FurDatosPropietarioDto

    <JsonProperty("Tipo_de_documento_de_identidad_del_propietario")>
    Public Property Tipo_de_documento_de_identidad_del_propietario As String

    <JsonProperty("Numero_de_documento_de_identidad_del_propietario")>
    Public Property Numero_de_documento_de_identidad_del_propietario As String

    <JsonProperty("Primer_nombre_del_propietario_o_razon_social")>
    Public Property Primer_nombre_del_propietario_o_razon_social As String

    <JsonProperty("Segundo_nombre_del_propietario")>
    Public Property Segundo_nombre_del_propietario As String

    <JsonProperty("Primer_apellido_del_propietario")>
    Public Property Primer_apellido_del_propietario As String

    <JsonProperty("Segundo_apellido_del_propietario")>
    Public Property Segundo_apellido_del_propietario As String

    <JsonProperty("Direccion_de_residencia_del_propietario")>
    Public Property Direccion_de_residencia_del_propietario As String

    <JsonProperty("Telefono_de_residencia_del_propietario")>
    Public Property Telefono_de_residencia_del_propietario As String

    <JsonProperty("Codigo_del_municipio_de_residencia_del_propietario")>
    Public Property Codigo_del_municipio_de_residencia_del_propietario As String

End Class

''' <summary>
''' Sección "Datos_del_conductor_del_vehiculo_involucrado" del JSON FUR.
''' Campos NULL cuando NaturalezaEvento ≠ '01' (Accidente Tránsito).
''' </summary>
Public Class FurDatosConductorDto

    <JsonProperty("Tipo_de_documento_de_identidad_del_conductor")>
    Public Property Tipo_de_documento_de_identidad_del_conductor As String

    <JsonProperty("Numero_de_documento_de_identidad_del_conductor")>
    Public Property Numero_de_documento_de_identidad_del_conductor As String

    <JsonProperty("Primer_nombre_del_conductor")>
    Public Property Primer_nombre_del_conductor As String

    <JsonProperty("Segundo_nombre_del_conductor")>
    Public Property Segundo_nombre_del_conductor As String

    <JsonProperty("Primer_apellido_del_conductor")>
    Public Property Primer_apellido_del_conductor As String

    <JsonProperty("Segundo_apellido_del_conductor")>
    Public Property Segundo_apellido_del_conductor As String

    <JsonProperty("Codigo_del_municipio_de_residencia_del_conductor")>
    Public Property Codigo_del_municipio_de_residencia_del_conductor As String

    <JsonProperty("Direccion_de_residencia_del_conductor")>
    Public Property Direccion_de_residencia_del_conductor As String

    <JsonProperty("Telefono_de_residencia_del_conductor")>
    Public Property Telefono_de_residencia_del_conductor As String

End Class

''' <summary>Sección "Datos_Relacionados_con_la_Atencion_de_La_Victima" del JSON FUR.</summary>
Public Class FurDatosAtencionVictimaDto

    ''' <summary>Catálogo permitido: SI / NO.</summary>
    <JsonProperty("Uso_material_de_osteosintesis_en_la_atencion")>
    Public Property Uso_material_de_osteosintesis_en_la_atencion As String

    ''' <summary>Catálogo permitido Tipo de atención: 1..8.</summary>
    <JsonProperty("Es_atencion_inicial_paciente_remitido_o_control")>
    Public Property Es_atencion_inicial_paciente_remitido_o_control As String

End Class

''' <summary>Sección "Datos_de_remision" del JSON FUR.</summary>
Public Class FurDatosRemisionDto

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

    <JsonProperty("Placa_ambulancia_que_realiza_el_traslado_secundario")>
    Public Property Placa_ambulancia_que_realiza_el_traslado_secundario As String

    ''' <summary>Catálogo permitido: 1=Básica, 2=Medicalizada.</summary>
    <JsonProperty("Tipo_de_servicio_del_transporte_secundario")>
    Public Property Tipo_de_servicio_del_transporte_secundario As String

End Class

''' <summary>Sección "Datos_Transporte_y_movilizacion_de_la_victima" del JSON FUR.</summary>
Public Class FurDatosTransporteDto

    <JsonProperty("Placa_ambulancia_que_realiza_el_traslado")>
    Public Property Placa_ambulancia_que_realiza_el_traslado As String

    <JsonProperty("Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario")>
    Public Property Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario As String

    <JsonProperty("Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion")>
    Public Property Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion As String

    <JsonProperty("Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS")>
    Public Property Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS As String

    ''' <summary>Catálogo permitido: 1=Básica, 2=Medicalizada.</summary>
    <JsonProperty("Tipo_de_servicio_del_transporte")>
    Public Property Tipo_de_servicio_del_transporte As String

End Class
