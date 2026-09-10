'***********************************************************************
' Assembly         : Application.Glosas
' Feature          : Circular Externa 003 de 2026 ADRES — FUR RG
'***********************************************************************

Imports Domain.Entities
Imports Newtonsoft.Json

''' <summary>
''' Helpers de mapeo, validación, serialización JSON y construcción de
''' DataSet plano (XLSX) para FUR RG (Respuesta a Glosa, Circular ADRES
''' 003 de 2026).
'''
''' El SP <c>SP_GenerateAdresFurRgData</c> devuelve UN result set con una
''' fila por glosa-item (cabecera FUR + RG repetida por glosa). Este
''' helper agrupa por <c>InvoiceId</c> para construir el JSON jerárquico.
''' </summary>
Friend Module AdresFurRgHelper

    Friend Const MAX_FACTURAS_JSON As Integer = 100

    Private Const MAX_NUM_FACTURA As Integer = 50
    Private Const MAX_NUMERO_RADICACION As Integer = 100
    Private Const MUNICIPIO_LEN As Integer = 5

    Private ReadOnly TipoDocPermitido As HashSet(Of String) =
        New HashSet(Of String)(New String() {"CC", "CE", "CD", "PA", "PE", "DE", "PT", "NI"})

#Region "Mapping DataTable → POCO"

    ''' <summary>
    ''' Mapea el DataTable plano del SP a una lista de POCOs
    ''' <see cref="SP_GenerateAdresFurRgData_Result"/>. Cada POCO representa
    ''' una glosa-item; las cabeceras FUR + RG se repiten en cada fila.
    ''' </summary>
    Friend Function MapDataTableToRows(dt As DataTable) As List(Of SP_GenerateAdresFurRgData_Result)
        Dim list As New List(Of SP_GenerateAdresFurRgData_Result)()
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return list

        For Each dr As DataRow In dt.Rows
            list.Add(New SP_GenerateAdresFurRgData_Result With {
                .InvoiceId = SafeInt(dr, "InvoiceId"),
                .NIT_PRESTADOR = SafeString(dr, "NIT_PRESTADOR"),
                .NUM_FACTURA = SafeString(dr, "NUM_FACTURA"),
                .Num_factura_anterior = SafeString(dr, "Num_factura_anterior"),
                .Numero_radicacion = SafeString(dr, "Numero_radicacion"),
                .CreditNote_o_Debit_note = SafeString(dr, "CreditNote_o_Debit_note"),
                .Valor_reclamado = SafeNullableDecimal(dr, "Valor_reclamado"),
                .Direccion_residencia_victima = SafeString(dr, "Direccion_residencia_victima"),
                .Telefono_victima = SafeString(dr, "Telefono_victima"),
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
                .Placa_ambulancia_que_realiza_la_remision = SafeString(dr, "Placa_ambulancia_que_realiza_la_remision"),
                .Placa_ambulancia_que_realiza_el_traslado_secundario = SafeString(dr, "Placa_ambulancia_que_realiza_el_traslado_secundario"),
                .Codigo_de_habilitacion_del_prestador_que_remite = SafeString(dr, "Codigo_de_habilitacion_del_prestador_que_remite"),
                .TIPO_de_documento_Profesional_que_recibe = SafeString(dr, "TIPO_de_documento_Profesional_que_recibe"),
                .Numero_de_documento_Profesional_que_recibe = SafeString(dr, "Numero_de_documento_Profesional_que_recibe"),
                .Codigo_de_habilitacion_del_prestador_que_recibe = SafeString(dr, "Codigo_de_habilitacion_del_prestador_que_recibe"),
                .Fecha_de_aceptacion = SafeString(dr, "Fecha_de_aceptacion"),
                .Hora_aceptacion = SafeString(dr, "Hora_aceptacion"),
                .Placa_ambulancia_que_realiza_el_traslado = SafeString(dr, "Placa_ambulancia_que_realiza_el_traslado"),
                .Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion = SafeString(dr, "Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion"),
                .Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS = SafeString(dr, "Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS"),
                .GlosaId = SafeNullableInt(dr, "GlosaId"),
                .ID_interno_Glosa = SafeString(dr, "ID_interno_Glosa"),
                .itemID_servicio_o_tecnologia_objetado = SafeString(dr, "itemID_servicio_o_tecnologia_objetado"),
                .Codigo_glosa = SafeString(dr, "Codigo_glosa"),
                .Tipo_respuesta_a_glosa = SafeString(dr, "Tipo_respuesta_a_glosa"),
                .Respuesta_a_glosa = SafeString(dr, "Respuesta_a_glosa"),
                .Cantidad_aceptada = SafeNullableInt(dr, "Cantidad_aceptada"),
                .Valor_aceptado = SafeNullableDecimal(dr, "Valor_aceptado"),
                .Primer_nombre_primer_apellido_auditor = SafeString(dr, "Primer_nombre_primer_apellido_auditor"),
                .Perfil_auditor = SafeString(dr, "Perfil_auditor")
            })
        Next

        Return list
    End Function

#End Region

#Region "Validation"

    ''' <summary>
    ''' Aplica reglas ADRES sobre las filas crudas del SP:
    '''   * NUM_FACTURA obligatorio (omite la factura completa si vacío).
    '''   * Inconsistencias suaves (longitud documento, municipio) → warnings
    '''     sin descartar la fila.
    ''' </summary>
    Friend Function FilterValidRows(rows As List(Of SP_GenerateAdresFurRgData_Result),
                                    warnings As List(Of String)) As List(Of SP_GenerateAdresFurRgData_Result)

        Dim result As New List(Of SP_GenerateAdresFurRgData_Result)()
        If rows Is Nothing Then Return result

        Dim invoicesOmitted As New HashSet(Of Integer)()

        For Each row In rows
            Dim invoice = If(row.NUM_FACTURA, String.Empty).Trim()

            If String.IsNullOrWhiteSpace(row.NUM_FACTURA) Then
                If Not invoicesOmitted.Contains(row.InvoiceId) Then
                    warnings.Add("Se omitió una reclamación con NUM_FACTURA vacío.")
                    invoicesOmitted.Add(row.InvoiceId)
                End If
                Continue For
            End If

            If row.NUM_FACTURA.Length > MAX_NUM_FACTURA Then
                If Not invoicesOmitted.Contains(row.InvoiceId) Then
                    warnings.Add($"Factura {invoice}: NUM_FACTURA supera {MAX_NUM_FACTURA} caracteres; reclamación omitida.")
                    invoicesOmitted.Add(row.InvoiceId)
                End If
                Continue For
            End If

            ' --- Warnings suaves (no omiten la fila) ---

            If Not String.IsNullOrEmpty(row.TIPO_de_documento_Profesional_que_recibe) AndAlso
               Not TipoDocPermitido.Contains(row.TIPO_de_documento_Profesional_que_recibe.Trim()) Then
                warnings.Add($"Factura {invoice}: TIPO_de_documento_Profesional_que_recibe '{row.TIPO_de_documento_Profesional_que_recibe}' fuera del catálogo (CC,CE,CD,PA,PE,DE,PT,NI).")
            End If

            If Not String.IsNullOrEmpty(row.Codigo_municipio_ocurrencia_evento) AndAlso
               row.Codigo_municipio_ocurrencia_evento.Length <> MUNICIPIO_LEN Then
                warnings.Add($"Factura {invoice}: Codigo_municipio_ocurrencia_evento debe tener {MUNICIPIO_LEN} caracteres.")
            End If

            If Not String.IsNullOrEmpty(row.Numero_radicacion) AndAlso
               row.Numero_radicacion.Length > MAX_NUMERO_RADICACION Then
                warnings.Add($"Factura {invoice}: Numero_radicacion supera {MAX_NUMERO_RADICACION} caracteres.")
            End If

            result.Add(row)
        Next

        Return result
    End Function

#End Region

#Region "JSON build"

    ''' <summary>
    ''' Construye el JSON FUR RG agrupando las filas planas del SP por
    ''' <c>InvoiceId</c>. Cada grupo se mapea a una <see cref="FurRgReclamacionDto"/>;
    ''' las filas con <c>GlosaId IS NOT NULL</c> se convierten en items
    ''' del array <c>Datos_de_la_glosa</c>.
    ''' </summary>
    Friend Function BuildJson(rows As List(Of SP_GenerateAdresFurRgData_Result),
                              nitPrestador As String) As String

        Dim root As New FurRgRootDto()
        root.Datos_IPS.NIT_PRESTADOR = nitPrestador

        ' Mantener orden estable por aparición (primer InvoiceId encontrado).
        Dim invoiceOrder As New List(Of Integer)()
        Dim groups As New Dictionary(Of Integer, List(Of SP_GenerateAdresFurRgData_Result))()

        For Each row In rows
            If Not groups.ContainsKey(row.InvoiceId) Then
                groups(row.InvoiceId) = New List(Of SP_GenerateAdresFurRgData_Result)()
                invoiceOrder.Add(row.InvoiceId)
            End If
            groups(row.InvoiceId).Add(row)
        Next

        For Each invoiceId In invoiceOrder
            Dim invoiceRows = groups(invoiceId)
            Dim header = invoiceRows(0)

            Dim reclamacion = New FurRgReclamacionDto With {
                .NUM_FACTURA = NullIfEmpty(header.NUM_FACTURA),
                .Num_factura_anterior = NullIfEmpty(header.Num_factura_anterior),
                .Numero_radicacion = NullIfEmpty(header.Numero_radicacion),
                .CreditNote_o_Debit_note = NullIfEmpty(header.CreditNote_o_Debit_note),
                .Valor_reclamado = header.Valor_reclamado,
                .Datos_de_la_victima = New FurRgDatosVictimaDto With {
                    .Direccion_residencia_victima = NullIfEmpty(header.Direccion_residencia_victima),
                    .Telefono_victima = NullIfEmpty(header.Telefono_victima)
                },
                .Datos_del_sitio_donde_ocurrio_el_evento = New FurRgDatosSitioEventoDto With {
                    .Condicion_victima = NullIfEmpty(header.Condicion_victima),
                    .Fecha_de_ocurrencia_evento = NullIfEmpty(header.Fecha_de_ocurrencia_evento),
                    .Zona_de_ocurrencia_evento = NullIfEmpty(header.Zona_de_ocurrencia_evento),
                    .Codigo_municipio_ocurrencia_evento = NullIfEmpty(header.Codigo_municipio_ocurrencia_evento),
                    .Direccion_de_ocurrencia_evento = NullIfEmpty(header.Direccion_de_ocurrencia_evento),
                    .Descripcion_corta_de_lo_ocurrido_en_el_evento = NullIfEmpty(header.Descripcion_corta_de_lo_ocurrido_en_el_evento)
                },
                .Datos_del_Vehiculo_Involucrado_en_el_Accidente_de_Transito = New FurDatosVehiculoDto With {
                    .Estado_de_aseguramiento = NullIfEmpty(header.Estado_de_aseguramiento),
                    .Placa_vehiculo = NullIfEmpty(header.Placa_vehiculo),
                    .Tipo_de_Vehiculo = NullIfEmpty(header.Tipo_de_Vehiculo),
                    .Codigo_de_la_aseguradora = NullIfEmpty(header.Codigo_de_la_aseguradora),
                    .Numero_de_poliza_SOAT = NullIfEmpty(header.Numero_de_poliza_SOAT),
                    .Fecha_de_inicio_de_vigencia_de_la_poliza = NullIfEmpty(header.Fecha_de_inicio_de_vigencia_de_la_poliza),
                    .Fecha_final_de_vigencia_de_la_poliza = NullIfEmpty(header.Fecha_final_de_vigencia_de_la_poliza),
                    .Numero_de_radicado_SIRAS = NullIfEmpty(header.Numero_de_radicado_SIRAS),
                    .Cobro_por_agotamiento_tope_Aseguradora = NullIfEmpty(header.Cobro_por_agotamiento_tope_Aseguradora)
                },
                .Datos_del_propietario_del_vehiculo = New FurDatosPropietarioDto With {
                    .Tipo_de_documento_de_identidad_del_propietario = NullIfEmpty(header.Tipo_de_documento_de_identidad_del_propietario),
                    .Numero_de_documento_de_identidad_del_propietario = NullIfEmpty(header.Numero_de_documento_de_identidad_del_propietario),
                    .Primer_nombre_del_propietario_o_razon_social = NullIfEmpty(header.Primer_nombre_del_propietario_o_razon_social),
                    .Segundo_nombre_del_propietario = NullIfEmpty(header.Segundo_nombre_del_propietario),
                    .Primer_apellido_del_propietario = NullIfEmpty(header.Primer_apellido_del_propietario),
                    .Segundo_apellido_del_propietario = NullIfEmpty(header.Segundo_apellido_del_propietario),
                    .Direccion_de_residencia_del_propietario = NullIfEmpty(header.Direccion_de_residencia_del_propietario),
                    .Telefono_de_residencia_del_propietario = NullIfEmpty(header.Telefono_de_residencia_del_propietario),
                    .Codigo_del_municipio_de_residencia_del_propietario = NullIfEmpty(header.Codigo_del_municipio_de_residencia_del_propietario)
                },
                .Datos_del_conductor_del_vehiculo_involucrado = New FurDatosConductorDto With {
                    .Tipo_de_documento_de_identidad_del_conductor = NullIfEmpty(header.Tipo_de_documento_de_identidad_del_conductor),
                    .Numero_de_documento_de_identidad_del_conductor = NullIfEmpty(header.Numero_de_documento_de_identidad_del_conductor),
                    .Primer_nombre_del_conductor = NullIfEmpty(header.Primer_nombre_del_conductor),
                    .Segundo_nombre_del_conductor = NullIfEmpty(header.Segundo_nombre_del_conductor),
                    .Primer_apellido_del_conductor = NullIfEmpty(header.Primer_apellido_del_conductor),
                    .Segundo_apellido_del_conductor = NullIfEmpty(header.Segundo_apellido_del_conductor),
                    .Codigo_del_municipio_de_residencia_del_conductor = NullIfEmpty(header.Codigo_del_municipio_de_residencia_del_conductor),
                    .Direccion_de_residencia_del_conductor = NullIfEmpty(header.Direccion_de_residencia_del_conductor),
                    .Telefono_de_residencia_del_conductor = NullIfEmpty(header.Telefono_de_residencia_del_conductor)
                },
                .Datos_Relacionados_con_la_Atencion_de_La_Victima = New FurDatosAtencionVictimaDto With {
                    .Uso_material_de_osteosintesis_en_la_atencion = NullIfEmpty(header.Uso_material_de_osteosintesis_en_la_atencion),
                    .Es_atencion_inicial_paciente_remitido_o_control = NullIfEmpty(header.Es_atencion_inicial_paciente_remitido_o_control)
                },
                .Datos_de_remision = New FurRgDatosRemisionDto With {
                    .Placa_ambulancia_que_realiza_la_remision = NullIfEmpty(header.Placa_ambulancia_que_realiza_la_remision),
                    .Placa_ambulancia_que_realiza_el_traslado_secundario = NullIfEmpty(header.Placa_ambulancia_que_realiza_el_traslado_secundario),
                    .Codigo_de_habilitacion_del_prestador_que_remite = NullIfEmpty(header.Codigo_de_habilitacion_del_prestador_que_remite),
                    .TIPO_de_documento_Profesional_que_recibe = NullIfEmpty(header.TIPO_de_documento_Profesional_que_recibe),
                    .Numero_de_documento_Profesional_que_recibe = NullIfEmpty(header.Numero_de_documento_Profesional_que_recibe),
                    .Codigo_de_habilitacion_del_prestador_que_recibe = NullIfEmpty(header.Codigo_de_habilitacion_del_prestador_que_recibe),
                    .Fecha_de_aceptacion = NullIfEmpty(header.Fecha_de_aceptacion),
                    .Hora_aceptacion = NullIfEmpty(header.Hora_aceptacion)
                },
                .Datos_Transporte_y_movilizacion_de_la_victima = New FurRgDatosTransporteDto With {
                    .Placa_ambulancia_que_realiza_el_traslado = NullIfEmpty(header.Placa_ambulancia_que_realiza_el_traslado),
                    .Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion = NullIfEmpty(header.Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion),
                    .Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS = NullIfEmpty(header.Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS)
                }
            }

            ' Solo agregar items de glosa cuando GlosaId no es null (LEFT JOIN
            ' puede haber emitido una fila vacía si la factura no tiene glosas).
            For Each glosaRow In invoiceRows
                If Not glosaRow.GlosaId.HasValue Then Continue For
                reclamacion.Datos_de_la_glosa.Add(New FurRgDatosGlosaDto With {
                    .ID_interno_Glosa = NullIfEmpty(glosaRow.ID_interno_Glosa),
                    .itemID_servicio_o_tecnologia_objetado = NullIfEmpty(glosaRow.itemID_servicio_o_tecnologia_objetado),
                    .Codigo_glosa = NullIfEmpty(glosaRow.Codigo_glosa),
                    .Tipo_respuesta_a_glosa = NullIfEmpty(glosaRow.Tipo_respuesta_a_glosa),
                    .Respuesta_a_glosa = NullIfEmpty(glosaRow.Respuesta_a_glosa),
                    .Cantidad_aceptada = glosaRow.Cantidad_aceptada,
                    .Valor_aceptado = glosaRow.Valor_aceptado,
                    .Primer_nombre_primer_apellido_auditor = NullIfEmpty(glosaRow.Primer_nombre_primer_apellido_auditor),
                    .Perfil_auditor = NullIfEmpty(glosaRow.Perfil_auditor)
                })
            Next

            root.Datos_reclamacion.Add(reclamacion)
        Next

        Return JsonConvert.SerializeObject(root, Formatting.Indented)
    End Function

#End Region

#Region "DataSet build (XLSX)"

    ''' <summary>
    ''' Construye un DataSet con una única tabla <c>FurRg</c>: una fila
    ''' por GLOSA-ITEM (cabecera FUR + RG repetida por glosa). Si una
    ''' factura no tiene glosas, aparece UNA fila con columnas de glosa
    ''' en blanco.
    ''' </summary>
    Friend Function BuildExcelData(rows As List(Of SP_GenerateAdresFurRgData_Result),
                                   nitPrestador As String) As DataSet

        Dim ds As New DataSet()
        ds.Tables.Add(BuildFurRgTable(rows, nitPrestador))
        Return ds
    End Function

    Private Function BuildFurRgTable(rows As List(Of SP_GenerateAdresFurRgData_Result),
                                     nitPrestador As String) As DataTable

        Dim dt As New DataTable("FurRg")
        dt.Columns.Add("NIT_PRESTADOR", GetType(String))
        dt.Columns.Add("NUM_FACTURA", GetType(String))
        dt.Columns.Add("Num_factura_anterior", GetType(String))
        dt.Columns.Add("Numero_radicacion", GetType(String))
        dt.Columns.Add("CreditNote_o_Debit_note", GetType(String))
        dt.Columns.Add("Valor_reclamado", GetType(Decimal))
        dt.Columns.Add("Direccion_residencia_victima", GetType(String))
        dt.Columns.Add("Telefono_victima", GetType(String))
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
        dt.Columns.Add("Placa_ambulancia_que_realiza_la_remision", GetType(String))
        dt.Columns.Add("Placa_ambulancia_que_realiza_el_traslado_secundario", GetType(String))
        dt.Columns.Add("Codigo_de_habilitacion_del_prestador_que_remite", GetType(String))
        dt.Columns.Add("TIPO_de_documento_Profesional_que_recibe", GetType(String))
        dt.Columns.Add("Numero_de_documento_Profesional_que_recibe", GetType(String))
        dt.Columns.Add("Codigo_de_habilitacion_del_prestador_que_recibe", GetType(String))
        dt.Columns.Add("Fecha_de_aceptacion", GetType(String))
        dt.Columns.Add("Hora_aceptacion", GetType(String))
        dt.Columns.Add("Placa_ambulancia_que_realiza_el_traslado", GetType(String))
        dt.Columns.Add("Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion", GetType(String))
        dt.Columns.Add("Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS", GetType(String))
        dt.Columns.Add("ID_interno_Glosa", GetType(String))
        dt.Columns.Add("itemID_servicio_o_tecnologia_objetado", GetType(String))
        dt.Columns.Add("Codigo_glosa", GetType(String))
        dt.Columns.Add("Tipo_respuesta_a_glosa", GetType(String))
        dt.Columns.Add("Respuesta_a_glosa", GetType(String))
        dt.Columns.Add("Cantidad_aceptada", GetType(Integer))
        dt.Columns.Add("Valor_aceptado", GetType(Decimal))
        dt.Columns.Add("Primer_nombre_primer_apellido_auditor", GetType(String))
        dt.Columns.Add("Perfil_auditor", GetType(String))

        For Each row In rows
            Dim dr = dt.NewRow()
            dr("NIT_PRESTADOR") = AsCell(nitPrestador)
            dr("NUM_FACTURA") = AsCell(row.NUM_FACTURA)
            dr("Num_factura_anterior") = AsCell(row.Num_factura_anterior)
            dr("Numero_radicacion") = AsCell(row.Numero_radicacion)
            dr("CreditNote_o_Debit_note") = AsCell(row.CreditNote_o_Debit_note)
            dr("Valor_reclamado") = AsCellDecimal(row.Valor_reclamado)
            dr("Direccion_residencia_victima") = AsCell(row.Direccion_residencia_victima)
            dr("Telefono_victima") = AsCell(row.Telefono_victima)
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
            dr("Placa_ambulancia_que_realiza_la_remision") = AsCell(row.Placa_ambulancia_que_realiza_la_remision)
            dr("Placa_ambulancia_que_realiza_el_traslado_secundario") = AsCell(row.Placa_ambulancia_que_realiza_el_traslado_secundario)
            dr("Codigo_de_habilitacion_del_prestador_que_remite") = AsCell(row.Codigo_de_habilitacion_del_prestador_que_remite)
            dr("TIPO_de_documento_Profesional_que_recibe") = AsCell(row.TIPO_de_documento_Profesional_que_recibe)
            dr("Numero_de_documento_Profesional_que_recibe") = AsCell(row.Numero_de_documento_Profesional_que_recibe)
            dr("Codigo_de_habilitacion_del_prestador_que_recibe") = AsCell(row.Codigo_de_habilitacion_del_prestador_que_recibe)
            dr("Fecha_de_aceptacion") = AsCell(row.Fecha_de_aceptacion)
            dr("Hora_aceptacion") = AsCell(row.Hora_aceptacion)
            dr("Placa_ambulancia_que_realiza_el_traslado") = AsCell(row.Placa_ambulancia_que_realiza_el_traslado)
            dr("Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion") = AsCell(row.Transporte_de_la_victima_desde_el_sitio_del_evento_Direccion)
            dr("Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS") = AsCell(row.Transporte_de_la_victima_hasta_el_fin_del_recorrido_direccion_IPS)
            dr("ID_interno_Glosa") = AsCell(row.ID_interno_Glosa)
            dr("itemID_servicio_o_tecnologia_objetado") = AsCell(row.itemID_servicio_o_tecnologia_objetado)
            dr("Codigo_glosa") = AsCell(row.Codigo_glosa)
            dr("Tipo_respuesta_a_glosa") = AsCell(row.Tipo_respuesta_a_glosa)
            dr("Respuesta_a_glosa") = AsCell(row.Respuesta_a_glosa)
            dr("Cantidad_aceptada") = AsCellInt(row.Cantidad_aceptada)
            dr("Valor_aceptado") = AsCellDecimal(row.Valor_aceptado)
            dr("Primer_nombre_primer_apellido_auditor") = AsCell(row.Primer_nombre_primer_apellido_auditor)
            dr("Perfil_auditor") = AsCell(row.Perfil_auditor)
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

    Private Function SafeNullableInt(dr As DataRow, columnName As String) As Integer?
        If Not dr.Table.Columns.Contains(columnName) Then Return Nothing
        If dr.IsNull(columnName) Then Return Nothing
        Return Convert.ToInt32(dr(columnName))
    End Function

    Private Function SafeNullableDecimal(dr As DataRow, columnName As String) As Decimal?
        If Not dr.Table.Columns.Contains(columnName) Then Return Nothing
        If dr.IsNull(columnName) Then Return Nothing
        Return Convert.ToDecimal(dr(columnName))
    End Function

    Private Function NullIfEmpty(value As String) As String
        If String.IsNullOrEmpty(value) Then Return Nothing
        Return value
    End Function

    Private Function AsCell(value As String) As Object
        If value Is Nothing Then Return CObj(DBNull.Value)
        Return CObj(value)
    End Function

    Private Function AsCellInt(value As Integer?) As Object
        If Not value.HasValue Then Return CObj(DBNull.Value)
        Return CObj(value.Value)
    End Function

    Private Function AsCellDecimal(value As Decimal?) As Object
        If Not value.HasValue Then Return CObj(DBNull.Value)
        Return CObj(value.Value)
    End Function

#End Region

End Module
