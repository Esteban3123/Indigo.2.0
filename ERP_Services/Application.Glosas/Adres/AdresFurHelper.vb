'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Felix Camilo Salazar Roldan
' Created          : 2026 24-05-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Newtonsoft.Json

Friend Module AdresFurHelper

    Friend Const MAX_FACTURAS_JSON As Integer = 100

    Private Const MAX_NUM_FACTURA As Integer = 20
    Private Const MAX_DOC As Integer = 25
    Private Const MAX_NOMBRE As Integer = 50
    Private Const MAX_DIRECCION As Integer = 100
    Private Const MAX_TELEFONO As Integer = 10
    Private Const MUNICIPIO_LEN As Integer = 5

    Private ReadOnly NaturalezaPermitida As HashSet(Of String) =
        New HashSet(Of String)(New String() {
            "01","02","03","04","05","06","07","08","09",
            "10","11","12","13","14","15","16","17",
            "25","26","27"})

    Private ReadOnly TipoDocPermitido As HashSet(Of String) =
        New HashSet(Of String)(New String() {"CC", "CE", "CD", "PA", "PE", "DE", "PT", "NI"})

#Region "Mapping DataTable → DTO"

    Friend Function MapDataTableToRows(dt As DataTable) As List(Of SP_GenerateAdresFurData_Result)
        Dim list As New List(Of SP_GenerateAdresFurData_Result)()
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return list

        For Each dr As DataRow In dt.Rows
            list.Add(New SP_GenerateAdresFurData_Result With {
                .InvoiceId = SafeInt(dr, "InvoiceId"),
                .NUM_FACTURA = SafeString(dr, "NUM_FACTURA"),
                .Tipo_documento_identidad_victima = SafeString(dr, "Tipo_documento_identidad_victima"),
                .Numero_documento_identidad_victima = SafeString(dr, "Numero_documento_identidad_victima"),
                .Tipo_de_poblacion_especial = SafeString(dr, "Tipo_de_poblacion_especial"),
                .Primer_nombre_victima = SafeString(dr, "Primer_nombre_victima"),
                .Segundo_nombre_victima = SafeString(dr, "Segundo_nombre_victima"),
                .Primer_apellido_victima = SafeString(dr, "Primer_apellido_victima"),
                .Segundo_apellido_victima = SafeString(dr, "Segundo_apellido_victima"),
                .Direccion_residencia_victima = SafeString(dr, "Direccion_residencia_victima"),
                .Codigo_municipio_residencia_victima = SafeString(dr, "Codigo_municipio_residencia_victima"),
                .Telefono_victima = SafeString(dr, "Telefono_victima"),
                .Naturaleza_del_evento = SafeString(dr, "Naturaleza_del_evento"),
                .Descripcion_del_otro_evento = SafeString(dr, "Descripcion_del_otro_evento"),
                .Condicion_victima = SafeString(dr, "Condicion_victima"),
                .Fecha_de_ocurrencia_evento = SafeString(dr, "Fecha_de_ocurrencia_evento"),
                .Zona_de_ocurrencia_evento = SafeString(dr, "Zona_de_ocurrencia_evento"),
                .Codigo_municipio_ocurrencia_evento = SafeString(dr, "Codigo_municipio_ocurrencia_evento"),
                .Direccion_de_ocurrencia_evento = SafeString(dr, "Direccion_de_ocurrencia_evento"),
                .Descripcion_corta_de_lo_ocurrido_en_el_evento = SafeString(dr, "Descripcion_corta_de_lo_ocurrido_en_el_evento"),
                .Estado_de_aseguramiento = SafeString(dr, "Estado_de_aseguramiento"),
                .Placa_vehiculo = SafeString(dr, "Placa_vehiculo"),
                .Tipo_de_Vehiculo = SafeString(dr, "Tipo_de_Vehiculo"),
                .Codigo_de_la_aseguradora = SafeString(dr, "Codigo_de_la_aseguradora"),
                .Numero_de_poliza_SOAT = SafeString(dr, "Numero_de_poliza_SOAT"),
                .Fecha_de_inicio_de_vigencia_de_la_poliza = SafeString(dr, "Fecha_de_inicio_de_vigencia_de_la_poliza"),
                .Fecha_final_de_vigencia_de_la_poliza = SafeString(dr, "Fecha_final_de_vigencia_de_la_poliza"),
                .Numero_de_radicado_SIRAS = SafeString(dr, "Numero_de_radicado_SIRAS"),
                .Cobro_por_agotamiento_tope_Aseguradora = SafeString(dr, "Cobro_por_agotamiento_tope_Aseguradora"),
                .Tipo_de_documento_de_identidad_del_propietario = SafeString(dr, "Tipo_de_documento_de_identidad_del_propietario"),
                .Numero_de_documento_de_identidad_del_propietario = SafeString(dr, "Numero_de_documento_de_identidad_del_propietario"),
                .Primer_nombre_del_propietario_o_razon_social = SafeString(dr, "Primer_nombre_del_propietario_o_razon_social"),
                .Segundo_nombre_del_propietario = SafeString(dr, "Segundo_nombre_del_propietario"),
                .Primer_apellido_del_propietario = SafeString(dr, "Primer_apellido_del_propietario"),
                .Segundo_apellido_del_propietario = SafeString(dr, "Segundo_apellido_del_propietario"),
                .Direccion_de_residencia_del_propietario = SafeString(dr, "Direccion_de_residencia_del_propietario"),
                .Telefono_de_residencia_del_propietario = SafeString(dr, "Telefono_de_residencia_del_propietario"),
                .Codigo_del_municipio_de_residencia_del_propietario = SafeString(dr, "Codigo_del_municipio_de_residencia_del_propietario"),
                .Tipo_de_documento_de_identidad_del_conductor = SafeString(dr, "Tipo_de_documento_de_identidad_del_conductor"),
                .Numero_de_documento_de_identidad_del_conductor = SafeString(dr, "Numero_de_documento_de_identidad_del_conductor"),
                .Primer_nombre_del_conductor = SafeString(dr, "Primer_nombre_del_conductor"),
                .Segundo_nombre_del_conductor = SafeString(dr, "Segundo_nombre_del_conductor"),
                .Primer_apellido_del_conductor = SafeString(dr, "Primer_apellido_del_conductor"),
                .Segundo_apellido_del_conductor = SafeString(dr, "Segundo_apellido_del_conductor"),
                .Codigo_del_municipio_de_residencia_del_conductor = SafeString(dr, "Codigo_del_municipio_de_residencia_del_conductor"),
                .Direccion_de_residencia_del_conductor = SafeString(dr, "Direccion_de_residencia_del_conductor"),
                .Telefono_de_residencia_del_conductor = SafeString(dr, "Telefono_de_residencia_del_conductor"),
                .Uso_material_de_osteosintesis_en_la_atencion = SafeString(dr, "Uso_material_de_osteosintesis_en_la_atencion"),
                .Es_atencion_inicial_paciente_remitido_o_control = SafeString(dr, "Es_atencion_inicial_paciente_remitido_o_control"),
                .Codigo_de_habilitacion_del_prestador_que_remite = SafeString(dr, "Codigo_de_habilitacion_del_prestador_que_remite"),
                .TIPO_de_documento_Profesional_que_recibe = SafeString(dr, "TIPO_de_documento_Profesional_que_recibe"),
                .Numero_de_documento_Profesional_que_recibe = SafeString(dr, "Numero_de_documento_Profesional_que_recibe"),
                .Codigo_de_habilitacion_del_prestador_que_recibe = SafeString(dr, "Codigo_de_habilitacion_del_prestador_que_recibe"),
                .Fecha_de_aceptacion = SafeString(dr, "Fecha_de_aceptacion"),
                .Hora_aceptacion = SafeString(dr, "Hora_aceptacion"),
                .Placa_ambulancia_que_realiza_el_traslado_secundario = SafeString(dr, "Placa_ambulancia_que_realiza_el_traslado_secundario"),
                .Tipo_de_servicio_del_transporte_secundario = SafeString(dr, "Tipo_de_servicio_del_transporte_secundario"),
                .Placa_ambulancia_que_realiza_el_traslado = SafeString(dr, "Placa_ambulancia_que_realiza_el_traslado"),
                .Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario = SafeString(dr, "Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario"),
                .Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion = SafeString(dr, "Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion"),
                .Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS = SafeString(dr, "Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS"),
                .Tipo_de_servicio_del_transporte = SafeString(dr, "Tipo_de_servicio_del_transporte")
            })
        Next

        Return list
    End Function

#End Region

#Region "Validation"

    ''' <summary>
    ''' Aplica reglas ADRES: NUM_FACTURA obligatorio, Naturaleza_del_evento
    ''' dentro del catálogo. Las filas inválidas se descartan; las
    ''' inconsistencias suaves (longitud, catálogo doc víctima) se reportan
    ''' como warnings sin descartar la fila.
    ''' </summary>
    Friend Function FilterValidRows(rows As List(Of SP_GenerateAdresFurData_Result),
                                    warnings As List(Of String)) As List(Of SP_GenerateAdresFurData_Result)

        Dim result As New List(Of SP_GenerateAdresFurData_Result)()
        If rows Is Nothing Then Return result

        For Each row In rows
            Dim invoice = If(row.NUM_FACTURA, String.Empty).Trim()

            If String.IsNullOrWhiteSpace(row.NUM_FACTURA) Then
                warnings.Add("Se omitió una reclamación con NUM_FACTURA vacío.")
                Continue For
            End If

            If row.NUM_FACTURA.Length > MAX_NUM_FACTURA Then
                warnings.Add($"Factura {invoice}: NUM_FACTURA supera {MAX_NUM_FACTURA} caracteres; reclamación omitida.")
                Continue For
            End If

            If String.IsNullOrWhiteSpace(row.Naturaleza_del_evento) Then
                warnings.Add($"Factura {invoice}: Naturaleza_del_evento vacío; reclamación omitida.")
                Continue For
            End If

            If Not NaturalezaPermitida.Contains(row.Naturaleza_del_evento.Trim()) Then
                warnings.Add($"Factura {invoice}: Naturaleza_del_evento '{row.Naturaleza_del_evento}' fuera del catálogo ADRES; reclamación omitida.")
                Continue For
            End If

            ' --- Warnings suaves (no omiten la fila) ---

            If Not String.IsNullOrEmpty(row.Tipo_documento_identidad_victima) AndAlso
               Not TipoDocPermitido.Contains(row.Tipo_documento_identidad_victima.Trim()) Then
                warnings.Add($"Factura {invoice}: Tipo_documento_identidad_victima '{row.Tipo_documento_identidad_victima}' fuera del catálogo (CC,CE,CD,PA,PE,DE,PT,NI).")
            End If

            If Not String.IsNullOrEmpty(row.Codigo_municipio_residencia_victima) AndAlso
               row.Codigo_municipio_residencia_victima.Length <> MUNICIPIO_LEN Then
                warnings.Add($"Factura {invoice}: Codigo_municipio_residencia_victima debe tener {MUNICIPIO_LEN} caracteres.")
            End If

            If Not String.IsNullOrEmpty(row.Codigo_municipio_ocurrencia_evento) AndAlso
               row.Codigo_municipio_ocurrencia_evento.Length <> MUNICIPIO_LEN Then
                warnings.Add($"Factura {invoice}: Codigo_municipio_ocurrencia_evento debe tener {MUNICIPIO_LEN} caracteres.")
            End If

            result.Add(row)
        Next

        Return result
    End Function

#End Region

#Region "JSON build"

    ''' <summary>
    ''' Serializa el DTO FUR jerárquico. A diferencia de FUR SERVICIOS, la
    ''' spec FUR entrega un único objeto raíz (no envuelto en array).
    ''' </summary>
    Friend Function BuildJson(rows As List(Of SP_GenerateAdresFurData_Result),
                              nitPrestador As String) As String

        Dim root As New FurRootDto()
        root.Datos_IPS.NIT_PRESTADOR = nitPrestador

        For Each row In rows
            root.Datos_reclamacion.Add(New FurReclamacionDto With {
                .NUM_FACTURA = row.NUM_FACTURA,
                .Datos_de_la_victima = New FurDatosVictimaDto With {
                    .Tipo_documento_identidad_victima = NullIfEmpty(row.Tipo_documento_identidad_victima),
                    .Numero_documento_identidad_victima = NullIfEmpty(row.Numero_documento_identidad_victima),
                    .Tipo_de_poblacion_especial = NullIfEmpty(row.Tipo_de_poblacion_especial),
                    .Primer_nombre_victima = NullIfEmpty(row.Primer_nombre_victima),
                    .Segundo_nombre_victima = NullIfEmpty(row.Segundo_nombre_victima),
                    .Primer_apellido_victima = NullIfEmpty(row.Primer_apellido_victima),
                    .Segundo_apellido_victima = NullIfEmpty(row.Segundo_apellido_victima),
                    .Direccion_residencia_victima = NullIfEmpty(row.Direccion_residencia_victima),
                    .Codigo_municipio_residencia_victima = NullIfEmpty(row.Codigo_municipio_residencia_victima),
                    .Telefono_victima = NullIfEmpty(row.Telefono_victima)
                },
                .Datos_del_sitio_donde_ocurrio_el_evento = New FurDatosSitioEventoDto With {
                    .Naturaleza_del_evento = NullIfEmpty(row.Naturaleza_del_evento),
                    .Descripcion_del_otro_evento = NullIfEmpty(row.Descripcion_del_otro_evento),
                    .Condicion_victima = NullIfEmpty(row.Condicion_victima),
                    .Fecha_de_ocurrencia_evento = NullIfEmpty(row.Fecha_de_ocurrencia_evento),
                    .Zona_de_ocurrencia_evento = NullIfEmpty(row.Zona_de_ocurrencia_evento),
                    .Codigo_municipio_ocurrencia_evento = NullIfEmpty(row.Codigo_municipio_ocurrencia_evento),
                    .Direccion_de_ocurrencia_evento = NullIfEmpty(row.Direccion_de_ocurrencia_evento),
                    .Descripcion_corta_de_lo_ocurrido_en_el_evento = NullIfEmpty(row.Descripcion_corta_de_lo_ocurrido_en_el_evento)
                },
                .Datos_del_Vehiculo_Involucrado_en_el_Accidente_de_Transito = New FurDatosVehiculoDto With {
                    .Estado_de_aseguramiento = NullIfEmpty(row.Estado_de_aseguramiento),
                    .Placa_vehiculo = NullIfEmpty(row.Placa_vehiculo),
                    .Tipo_de_Vehiculo = NullIfEmpty(row.Tipo_de_Vehiculo),
                    .Codigo_de_la_aseguradora = NullIfEmpty(row.Codigo_de_la_aseguradora),
                    .Numero_de_poliza_SOAT = NullIfEmpty(row.Numero_de_poliza_SOAT),
                    .Fecha_de_inicio_de_vigencia_de_la_poliza = NullIfEmpty(row.Fecha_de_inicio_de_vigencia_de_la_poliza),
                    .Fecha_final_de_vigencia_de_la_poliza = NullIfEmpty(row.Fecha_final_de_vigencia_de_la_poliza),
                    .Numero_de_radicado_SIRAS = NullIfEmpty(row.Numero_de_radicado_SIRAS),
                    .Cobro_por_agotamiento_tope_Aseguradora = NullIfEmpty(row.Cobro_por_agotamiento_tope_Aseguradora)
                },
                .Datos_del_propietario_del_vehiculo = New FurDatosPropietarioDto With {
                    .Tipo_de_documento_de_identidad_del_propietario = NullIfEmpty(row.Tipo_de_documento_de_identidad_del_propietario),
                    .Numero_de_documento_de_identidad_del_propietario = NullIfEmpty(row.Numero_de_documento_de_identidad_del_propietario),
                    .Primer_nombre_del_propietario_o_razon_social = NullIfEmpty(row.Primer_nombre_del_propietario_o_razon_social),
                    .Segundo_nombre_del_propietario = NullIfEmpty(row.Segundo_nombre_del_propietario),
                    .Primer_apellido_del_propietario = NullIfEmpty(row.Primer_apellido_del_propietario),
                    .Segundo_apellido_del_propietario = NullIfEmpty(row.Segundo_apellido_del_propietario),
                    .Direccion_de_residencia_del_propietario = NullIfEmpty(row.Direccion_de_residencia_del_propietario),
                    .Telefono_de_residencia_del_propietario = NullIfEmpty(row.Telefono_de_residencia_del_propietario),
                    .Codigo_del_municipio_de_residencia_del_propietario = NullIfEmpty(row.Codigo_del_municipio_de_residencia_del_propietario)
                },
                .Datos_del_conductor_del_vehiculo_involucrado = New FurDatosConductorDto With {
                    .Tipo_de_documento_de_identidad_del_conductor = NullIfEmpty(row.Tipo_de_documento_de_identidad_del_conductor),
                    .Numero_de_documento_de_identidad_del_conductor = NullIfEmpty(row.Numero_de_documento_de_identidad_del_conductor),
                    .Primer_nombre_del_conductor = NullIfEmpty(row.Primer_nombre_del_conductor),
                    .Segundo_nombre_del_conductor = NullIfEmpty(row.Segundo_nombre_del_conductor),
                    .Primer_apellido_del_conductor = NullIfEmpty(row.Primer_apellido_del_conductor),
                    .Segundo_apellido_del_conductor = NullIfEmpty(row.Segundo_apellido_del_conductor),
                    .Codigo_del_municipio_de_residencia_del_conductor = NullIfEmpty(row.Codigo_del_municipio_de_residencia_del_conductor),
                    .Direccion_de_residencia_del_conductor = NullIfEmpty(row.Direccion_de_residencia_del_conductor),
                    .Telefono_de_residencia_del_conductor = NullIfEmpty(row.Telefono_de_residencia_del_conductor)
                },
                .Datos_Relacionados_con_la_Atencion_de_La_Victima = New FurDatosAtencionVictimaDto With {
                    .Uso_material_de_osteosintesis_en_la_atencion = NullIfEmpty(row.Uso_material_de_osteosintesis_en_la_atencion),
                    .Es_atencion_inicial_paciente_remitido_o_control = NullIfEmpty(row.Es_atencion_inicial_paciente_remitido_o_control)
                },
                .Datos_de_remision = New FurDatosRemisionDto With {
                    .Codigo_de_habilitacion_del_prestador_que_remite = NullIfEmpty(row.Codigo_de_habilitacion_del_prestador_que_remite),
                    .TIPO_de_documento_Profesional_que_recibe = NullIfEmpty(row.TIPO_de_documento_Profesional_que_recibe),
                    .Numero_de_documento_Profesional_que_recibe = NullIfEmpty(row.Numero_de_documento_Profesional_que_recibe),
                    .Codigo_de_habilitacion_del_prestador_que_recibe = NullIfEmpty(row.Codigo_de_habilitacion_del_prestador_que_recibe),
                    .Fecha_de_aceptacion = NullIfEmpty(row.Fecha_de_aceptacion),
                    .Hora_aceptacion = NullIfEmpty(row.Hora_aceptacion),
                    .Placa_ambulancia_que_realiza_el_traslado_secundario = NullIfEmpty(row.Placa_ambulancia_que_realiza_el_traslado_secundario),
                    .Tipo_de_servicio_del_transporte_secundario = NullIfEmpty(row.Tipo_de_servicio_del_transporte_secundario)
                },
                .Datos_Transporte_y_movilizacion_de_la_victima = New FurDatosTransporteDto With {
                    .Placa_ambulancia_que_realiza_el_traslado = NullIfEmpty(row.Placa_ambulancia_que_realiza_el_traslado),
                    .Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario = NullIfEmpty(row.Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario),
                    .Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion = NullIfEmpty(row.Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion),
                    .Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS = NullIfEmpty(row.Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS),
                    .Tipo_de_servicio_del_transporte = NullIfEmpty(row.Tipo_de_servicio_del_transporte)
                }
            })
        Next

        ' Spec ADRES FUR: objeto raíz único (no envuelto en array, distinto a FUR SERVICIOS).
        Return JsonConvert.SerializeObject(root, Formatting.Indented)
    End Function

#End Region

#Region "DataSet build"

    ''' <summary>
    ''' Construye un DataSet con una única tabla <c>Fur</c>: una fila por
    ''' reclamación, columnas planas (60+) listas para alimentar un GridControl
    ''' y exportar a XLSX.
    ''' </summary>
    Friend Function BuildExcelData(rows As List(Of SP_GenerateAdresFurData_Result),
                                   nitPrestador As String) As DataSet

        Dim ds As New DataSet()
        ds.Tables.Add(BuildFurTable(rows, nitPrestador))
        Return ds
    End Function

    Private Function BuildFurTable(rows As List(Of SP_GenerateAdresFurData_Result),
                                   nitPrestador As String) As DataTable

        Dim dt As New DataTable("Fur")
        dt.Columns.Add("NIT_PRESTADOR", GetType(String))
        dt.Columns.Add("NUM_FACTURA", GetType(String))
        dt.Columns.Add("Tipo_documento_identidad_victima", GetType(String))
        dt.Columns.Add("Numero_documento_identidad_victima", GetType(String))
        dt.Columns.Add("Tipo_de_poblacion_especial", GetType(String))
        dt.Columns.Add("Primer_nombre_victima", GetType(String))
        dt.Columns.Add("Segundo_nombre_victima", GetType(String))
        dt.Columns.Add("Primer_apellido_victima", GetType(String))
        dt.Columns.Add("Segundo_apellido_victima", GetType(String))
        dt.Columns.Add("Direccion_residencia_victima", GetType(String))
        dt.Columns.Add("Codigo_municipio_residencia_victima", GetType(String))
        dt.Columns.Add("Telefono_victima", GetType(String))
        dt.Columns.Add("Naturaleza_del_evento", GetType(String))
        dt.Columns.Add("Descripcion_del_otro_evento", GetType(String))
        dt.Columns.Add("Condicion_victima", GetType(String))
        dt.Columns.Add("Fecha_de_ocurrencia_evento", GetType(String))
        dt.Columns.Add("Zona_de_ocurrencia_evento", GetType(String))
        dt.Columns.Add("Codigo_municipio_ocurrencia_evento", GetType(String))
        dt.Columns.Add("Direccion_de_ocurrencia_evento", GetType(String))
        dt.Columns.Add("Descripcion_corta_de_lo_ocurrido_en_el_evento", GetType(String))
        dt.Columns.Add("Estado_de_aseguramiento", GetType(String))
        dt.Columns.Add("Placa_vehiculo", GetType(String))
        dt.Columns.Add("Tipo_de_Vehiculo", GetType(String))
        dt.Columns.Add("Codigo_de_la_aseguradora", GetType(String))
        dt.Columns.Add("Numero_de_poliza_SOAT", GetType(String))
        dt.Columns.Add("Fecha_de_inicio_de_vigencia_de_la_poliza", GetType(String))
        dt.Columns.Add("Fecha_final_de_vigencia_de_la_poliza", GetType(String))
        dt.Columns.Add("Numero_de_radicado_SIRAS", GetType(String))
        dt.Columns.Add("Cobro_por_agotamiento_tope_Aseguradora", GetType(String))
        dt.Columns.Add("Tipo_de_documento_de_identidad_del_propietario", GetType(String))
        dt.Columns.Add("Numero_de_documento_de_identidad_del_propietario", GetType(String))
        dt.Columns.Add("Primer_nombre_del_propietario_o_razon_social", GetType(String))
        dt.Columns.Add("Segundo_nombre_del_propietario", GetType(String))
        dt.Columns.Add("Primer_apellido_del_propietario", GetType(String))
        dt.Columns.Add("Segundo_apellido_del_propietario", GetType(String))
        dt.Columns.Add("Direccion_de_residencia_del_propietario", GetType(String))
        dt.Columns.Add("Telefono_de_residencia_del_propietario", GetType(String))
        dt.Columns.Add("Codigo_del_municipio_de_residencia_del_propietario", GetType(String))
        dt.Columns.Add("Tipo_de_documento_de_identidad_del_conductor", GetType(String))
        dt.Columns.Add("Numero_de_documento_de_identidad_del_conductor", GetType(String))
        dt.Columns.Add("Primer_nombre_del_conductor", GetType(String))
        dt.Columns.Add("Segundo_nombre_del_conductor", GetType(String))
        dt.Columns.Add("Primer_apellido_del_conductor", GetType(String))
        dt.Columns.Add("Segundo_apellido_del_conductor", GetType(String))
        dt.Columns.Add("Codigo_del_municipio_de_residencia_del_conductor", GetType(String))
        dt.Columns.Add("Direccion_de_residencia_del_conductor", GetType(String))
        dt.Columns.Add("Telefono_de_residencia_del_conductor", GetType(String))
        dt.Columns.Add("Uso_material_de_osteosintesis_en_la_atencion", GetType(String))
        dt.Columns.Add("Es_atencion_inicial_paciente_remitido_o_control", GetType(String))
        dt.Columns.Add("Placa_ambulancia_que_realiza_el_traslado_secundario", GetType(String))
        dt.Columns.Add("Tipo_de_servicio_del_transporte_secundario", GetType(String))
        dt.Columns.Add("Codigo_de_habilitacion_del_prestador_que_remite", GetType(String))
        dt.Columns.Add("Codigo_de_habilitacion_del_prestador_que_recibe", GetType(String))
        dt.Columns.Add("TIPO_de_documento_Profesional_que_recibe", GetType(String))
        dt.Columns.Add("Numero_de_documento_Profesional_que_recibe", GetType(String))
        dt.Columns.Add("Fecha_de_aceptacion", GetType(String))
        dt.Columns.Add("Hora_aceptacion", GetType(String))
        dt.Columns.Add("Tipo_de_servicio_del_transporte", GetType(String))
        dt.Columns.Add("Placa_ambulancia_que_realiza_el_traslado", GetType(String))
        dt.Columns.Add("Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario", GetType(String))
        dt.Columns.Add("Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion", GetType(String))
        dt.Columns.Add("Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS", GetType(String))

        For Each row In rows
            Dim dr = dt.NewRow()
            dr("NIT_PRESTADOR") = AsCell(nitPrestador)
            dr("NUM_FACTURA") = AsCell(row.NUM_FACTURA)
            dr("Tipo_documento_identidad_victima") = AsCell(row.Tipo_documento_identidad_victima)
            dr("Numero_documento_identidad_victima") = AsCell(row.Numero_documento_identidad_victima)
            dr("Tipo_de_poblacion_especial") = AsCell(row.Tipo_de_poblacion_especial)
            dr("Primer_nombre_victima") = AsCell(row.Primer_nombre_victima)
            dr("Segundo_nombre_victima") = AsCell(row.Segundo_nombre_victima)
            dr("Primer_apellido_victima") = AsCell(row.Primer_apellido_victima)
            dr("Segundo_apellido_victima") = AsCell(row.Segundo_apellido_victima)
            dr("Direccion_residencia_victima") = AsCell(row.Direccion_residencia_victima)
            dr("Codigo_municipio_residencia_victima") = AsCell(row.Codigo_municipio_residencia_victima)
            dr("Telefono_victima") = AsCell(row.Telefono_victima)
            dr("Naturaleza_del_evento") = AsCell(row.Naturaleza_del_evento)
            dr("Descripcion_del_otro_evento") = AsCell(row.Descripcion_del_otro_evento)
            dr("Condicion_victima") = AsCell(row.Condicion_victima)
            dr("Fecha_de_ocurrencia_evento") = AsCell(row.Fecha_de_ocurrencia_evento)
            dr("Zona_de_ocurrencia_evento") = AsCell(row.Zona_de_ocurrencia_evento)
            dr("Codigo_municipio_ocurrencia_evento") = AsCell(row.Codigo_municipio_ocurrencia_evento)
            dr("Direccion_de_ocurrencia_evento") = AsCell(row.Direccion_de_ocurrencia_evento)
            dr("Descripcion_corta_de_lo_ocurrido_en_el_evento") = AsCell(row.Descripcion_corta_de_lo_ocurrido_en_el_evento)
            dr("Estado_de_aseguramiento") = AsCell(row.Estado_de_aseguramiento)
            dr("Placa_vehiculo") = AsCell(row.Placa_vehiculo)
            dr("Tipo_de_Vehiculo") = AsCell(row.Tipo_de_Vehiculo)
            dr("Codigo_de_la_aseguradora") = AsCell(row.Codigo_de_la_aseguradora)
            dr("Numero_de_poliza_SOAT") = AsCell(row.Numero_de_poliza_SOAT)
            dr("Fecha_de_inicio_de_vigencia_de_la_poliza") = AsCell(row.Fecha_de_inicio_de_vigencia_de_la_poliza)
            dr("Fecha_final_de_vigencia_de_la_poliza") = AsCell(row.Fecha_final_de_vigencia_de_la_poliza)
            dr("Numero_de_radicado_SIRAS") = AsCell(row.Numero_de_radicado_SIRAS)
            dr("Cobro_por_agotamiento_tope_Aseguradora") = AsCell(row.Cobro_por_agotamiento_tope_Aseguradora)
            dr("Tipo_de_documento_de_identidad_del_propietario") = AsCell(row.Tipo_de_documento_de_identidad_del_propietario)
            dr("Numero_de_documento_de_identidad_del_propietario") = AsCell(row.Numero_de_documento_de_identidad_del_propietario)
            dr("Primer_nombre_del_propietario_o_razon_social") = AsCell(row.Primer_nombre_del_propietario_o_razon_social)
            dr("Segundo_nombre_del_propietario") = AsCell(row.Segundo_nombre_del_propietario)
            dr("Primer_apellido_del_propietario") = AsCell(row.Primer_apellido_del_propietario)
            dr("Segundo_apellido_del_propietario") = AsCell(row.Segundo_apellido_del_propietario)
            dr("Direccion_de_residencia_del_propietario") = AsCell(row.Direccion_de_residencia_del_propietario)
            dr("Telefono_de_residencia_del_propietario") = AsCell(row.Telefono_de_residencia_del_propietario)
            dr("Codigo_del_municipio_de_residencia_del_propietario") = AsCell(row.Codigo_del_municipio_de_residencia_del_propietario)
            dr("Tipo_de_documento_de_identidad_del_conductor") = AsCell(row.Tipo_de_documento_de_identidad_del_conductor)
            dr("Numero_de_documento_de_identidad_del_conductor") = AsCell(row.Numero_de_documento_de_identidad_del_conductor)
            dr("Primer_nombre_del_conductor") = AsCell(row.Primer_nombre_del_conductor)
            dr("Segundo_nombre_del_conductor") = AsCell(row.Segundo_nombre_del_conductor)
            dr("Primer_apellido_del_conductor") = AsCell(row.Primer_apellido_del_conductor)
            dr("Segundo_apellido_del_conductor") = AsCell(row.Segundo_apellido_del_conductor)
            dr("Codigo_del_municipio_de_residencia_del_conductor") = AsCell(row.Codigo_del_municipio_de_residencia_del_conductor)
            dr("Direccion_de_residencia_del_conductor") = AsCell(row.Direccion_de_residencia_del_conductor)
            dr("Telefono_de_residencia_del_conductor") = AsCell(row.Telefono_de_residencia_del_conductor)
            dr("Uso_material_de_osteosintesis_en_la_atencion") = AsCell(row.Uso_material_de_osteosintesis_en_la_atencion)
            dr("Es_atencion_inicial_paciente_remitido_o_control") = AsCell(row.Es_atencion_inicial_paciente_remitido_o_control)
            dr("Codigo_de_habilitacion_del_prestador_que_remite") = AsCell(row.Codigo_de_habilitacion_del_prestador_que_remite)
            dr("TIPO_de_documento_Profesional_que_recibe") = AsCell(row.TIPO_de_documento_Profesional_que_recibe)
            dr("Numero_de_documento_Profesional_que_recibe") = AsCell(row.Numero_de_documento_Profesional_que_recibe)
            dr("Codigo_de_habilitacion_del_prestador_que_recibe") = AsCell(row.Codigo_de_habilitacion_del_prestador_que_recibe)
            dr("Fecha_de_aceptacion") = AsCell(row.Fecha_de_aceptacion)
            dr("Hora_aceptacion") = AsCell(row.Hora_aceptacion)
            dr("Placa_ambulancia_que_realiza_el_traslado_secundario") = AsCell(row.Placa_ambulancia_que_realiza_el_traslado_secundario)
            dr("Tipo_de_servicio_del_transporte_secundario") = AsCell(row.Tipo_de_servicio_del_transporte_secundario)
            dr("Placa_ambulancia_que_realiza_el_traslado") = AsCell(row.Placa_ambulancia_que_realiza_el_traslado)
            dr("Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario") = AsCell(row.Codigo_de_habilitacion_del_prestador_que_recibe_transporte_primario)
            dr("Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion") = AsCell(row.Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion)
            dr("Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS") = AsCell(row.Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS)
            dr("Tipo_de_servicio_del_transporte") = AsCell(row.Tipo_de_servicio_del_transporte)
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

#End Region

#Region "Helpers"

    Private Function SafeString(dr As DataRow, columnName As String) As String
        If Not dr.Table.Columns.Contains(columnName) Then Return Nothing
        If dr.IsNull(columnName) Then Return Nothing
        Return Convert.ToString(dr(columnName))
    End Function

    Private Function SafeInt(dr As DataRow, columnName As String) As Integer
        If Not dr.Table.Columns.Contains(columnName) Then Return 0
        If dr.IsNull(columnName) Then Return 0
        Return Convert.ToInt32(dr(columnName))
    End Function

    Private Function NullIfEmpty(value As String) As String
        If String.IsNullOrEmpty(value) Then Return Nothing
        Return value
    End Function

    Private Function AsCell(value As String) As Object
        If value Is Nothing Then Return CObj(DBNull.Value)
        Return CObj(value)
    End Function

#End Region

End Module
