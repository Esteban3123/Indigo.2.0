'***********************************************************************
' Assembly         : Application.Glosas
' Feature          : Circular Externa 003 de 2026 ADRES - PBI 2 (FUR SERVICIOS)
' Description      : Helpers estáticos del FUR SERVICIOS. Toman la
'                    DataTable cruda devuelta por
'                    [Glosas].[SP_GenerateAdresFurServiciosData] y producen
'                    los artefactos requeridos: lista tipada, validación,
'                    JSON jerárquico y DataSet plano para Excel.
'***********************************************************************

Imports System.Globalization
Imports Domain.Entities
Imports Newtonsoft.Json

Friend Module AdresFurServiciosHelper

    Friend Const MAX_FACTURAS_JSON As Integer = 100

    Private Const MAX_NUM_FACTURA As Integer = 20
    Private Const MAX_CODIGO_SERVICIO As Integer = 20
    Private Const MAX_CUPS As Integer = 6
    Private Const MAX_DESCRIPCION As Integer = 200

    Private ReadOnly TiposServicioPermitidos As HashSet(Of String) =
        New HashSet(Of String)(New String() {"1", "2", "3", "4", "5", "6", "7", "8"})

#Region "Mapping DataTable → DTO"

    Friend Function MapDataTableToRows(dt As DataTable) As List(Of SP_GenerateAdresFurServiciosData_Result)
        Dim list As New List(Of SP_GenerateAdresFurServiciosData_Result)()
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return list

        For Each dr As DataRow In dt.Rows
            list.Add(New SP_GenerateAdresFurServiciosData_Result With {
                .InvoiceId = SafeInt(dr, "InvoiceId"),
                .NUM_FACTURA = SafeString(dr, "NUM_FACTURA"),
                .Tipo_de_servicio = SafeString(dr, "Tipo_de_servicio"),
                .Codigo_del_servicio = SafeString(dr, "Codigo_del_servicio"),
                .Codigo_general_del_procedimiento_quirurgico = SafeString(dr, "Codigo_general_del_procedimiento_quirurgico"),
                .Consecutivo_procedimiento_quirurgico = SafeString(dr, "Consecutivo_procedimiento_quirurgico"),
                .Codificacion_CUPS = SafeString(dr, "Codificacion_CUPS"),
                .Descripcion_del_servicio_o_elemento_reclamado = SafeString(dr, "Descripcion_del_servicio_o_elemento_reclamado"),
                .Cantidad_de_servicios = SafeDecimal(dr, "Cantidad_de_servicios"),
                .Valor_unitario_facturado = SafeDecimal(dr, "Valor_unitario_facturado"),
                .Valor_unitario_reclamado = SafeDecimal(dr, "Valor_unitario_reclamado"),
                .Valor_total_facturado = SafeDecimal(dr, "Valor_total_facturado"),
                .Valor_total_reclamado = SafeDecimal(dr, "Valor_total_reclamado")
            })
        Next

        Return list
    End Function

#End Region

#Region "Validation"

    ''' <summary>
    ''' Filtra las filas que cumplen las reglas ADRES (longitudes, catálogos
    ''' obligatorios). Las filas inválidas se descartan y se agrega un aviso
    ''' al listado <paramref name="warnings"/>.
    ''' </summary>
    Friend Function FilterValidRows(rows As List(Of SP_GenerateAdresFurServiciosData_Result),
                                    warnings As List(Of String)) As List(Of SP_GenerateAdresFurServiciosData_Result)

        Dim result As New List(Of SP_GenerateAdresFurServiciosData_Result)()
        If rows Is Nothing Then Return result

        For Each row In rows
            Dim invoice = If(row.NUM_FACTURA, String.Empty).Trim()

            If String.IsNullOrWhiteSpace(row.NUM_FACTURA) Then
                warnings.Add("Se omitió un servicio con NUM_FACTURA vacío.")
                Continue For
            End If

            If row.NUM_FACTURA.Length > MAX_NUM_FACTURA Then
                warnings.Add($"Factura {invoice}: NUM_FACTURA supera {MAX_NUM_FACTURA} caracteres; servicio omitido.")
                Continue For
            End If

            If String.IsNullOrWhiteSpace(row.Tipo_de_servicio) Then
                warnings.Add($"Factura {invoice}: Tipo_de_servicio vacío; servicio omitido.")
                Continue For
            End If

            If Not TiposServicioPermitidos.Contains(row.Tipo_de_servicio.Trim()) Then
                warnings.Add($"Factura {invoice}: Tipo_de_servicio '{row.Tipo_de_servicio}' fuera del catálogo ADRES (1..8); servicio omitido.")
                Continue For
            End If

            If Not String.IsNullOrEmpty(row.Codigo_del_servicio) AndAlso row.Codigo_del_servicio.Length > MAX_CODIGO_SERVICIO Then
                warnings.Add($"Factura {invoice}: Codigo_del_servicio supera {MAX_CODIGO_SERVICIO} caracteres; servicio omitido.")
                Continue For
            End If

            If Not String.IsNullOrEmpty(row.Codigo_general_del_procedimiento_quirurgico) AndAlso
               row.Codigo_general_del_procedimiento_quirurgico.Length > MAX_CUPS Then
                warnings.Add($"Factura {invoice}: Codigo_general_del_procedimiento_quirurgico supera {MAX_CUPS} caracteres; servicio omitido.")
                Continue For
            End If

            If Not String.IsNullOrEmpty(row.Consecutivo_procedimiento_quirurgico) AndAlso
               row.Consecutivo_procedimiento_quirurgico.Length > 2 Then
                warnings.Add($"Factura {invoice}: Consecutivo_procedimiento_quirurgico supera 2 caracteres; servicio omitido.")
                Continue For
            End If

            If Not String.IsNullOrEmpty(row.Codificacion_CUPS) AndAlso row.Codificacion_CUPS.Length > MAX_CUPS Then
                warnings.Add($"Factura {invoice}: Codificacion_CUPS supera {MAX_CUPS} caracteres; servicio omitido.")
                Continue For
            End If

            If Not String.IsNullOrEmpty(row.Descripcion_del_servicio_o_elemento_reclamado) AndAlso
               row.Descripcion_del_servicio_o_elemento_reclamado.Length > MAX_DESCRIPCION Then
                warnings.Add($"Factura {invoice}: Descripción supera {MAX_DESCRIPCION} caracteres; servicio omitido.")
                Continue For
            End If

            If Not row.Cantidad_de_servicios.HasValue OrElse row.Cantidad_de_servicios.Value <= 0D Then
                warnings.Add($"Factura {invoice}: Cantidad_de_servicios debe ser mayor que cero.")
                ' No se omite la fila por este caso (warning suave).
            End If

            result.Add(row)
        Next

        Return result
    End Function

#End Region

#Region "JSON build"

    Friend Function BuildJson(rows As List(Of SP_GenerateAdresFurServiciosData_Result),
                              nitPrestador As String) As String

        Dim root As New FurServiciosRootDto()
        root.Datos_IPS.NIT_PRESTADOR = nitPrestador

        Dim grouped = rows.
            GroupBy(Function(r) r.NUM_FACTURA).
            Select(Function(g) New With {.NumFactura = g.Key, .Items = g.ToList()}).
            ToList()

        For Each grp In grouped
            Dim reclamacion As New FurServiciosReclamacionDto()
            reclamacion.NUM_FACTURA = grp.NumFactura
            For Each row In grp.Items
                reclamacion.Servicios.Add(New FurServiciosServicioDto With {
                    .Tipo_de_servicio = NullIfEmpty(row.Tipo_de_servicio),
                    .Codigo_del_servicio = NullIfEmpty(row.Codigo_del_servicio),
                    .Codigo_general_del_procedimiento_quirurgico = NullIfEmpty(row.Codigo_general_del_procedimiento_quirurgico),
                    .Consecutivo_procedimiento_quirurgico = NullIfEmpty(row.Consecutivo_procedimiento_quirurgico),
                    .Codificacion_CUPS = NullIfEmpty(row.Codificacion_CUPS),
                    .Descripcion_del_servicio_o_elemento_reclamado = NullIfEmpty(row.Descripcion_del_servicio_o_elemento_reclamado),
                    .Cantidad_de_servicios = FormatNumber(row.Cantidad_de_servicios),
                    .Valor_unitario_facturado = FormatNumber(row.Valor_unitario_facturado),
                    .Valor_unitario_reclamado = FormatNumber(row.Valor_unitario_reclamado),
                    .Valor_total_facturado = FormatNumber(row.Valor_total_facturado),
                    .Valor_total_reclamado = FormatNumber(row.Valor_total_reclamado)
                })
            Next
            root.Datos_servicios_reclamacion.Add(reclamacion)
        Next

        ' Spec ADRES: array con un único objeto raíz.
        Dim wrapped = New List(Of FurServiciosRootDto) From {root}
        Return JsonConvert.SerializeObject(wrapped, Formatting.Indented)
    End Function

#End Region

#Region "DataSet build"

    ''' <summary>
    ''' Construye un DataSet con una única tabla <c>FurServicios</c>: una fila
    ''' por servicio, columnas planas listas para alimentar un GridControl y
    ''' exportar a XLSX.
    ''' </summary>
    Friend Function BuildExcelData(rows As List(Of SP_GenerateAdresFurServiciosData_Result),
                                   nitPrestador As String) As DataSet

        Dim ds As New DataSet()
        ds.Tables.Add(BuildFurServiciosTable(rows, nitPrestador))
        Return ds
    End Function

    Private Function BuildFurServiciosTable(rows As List(Of SP_GenerateAdresFurServiciosData_Result),
                                            nitPrestador As String) As DataTable

        Dim dt As New DataTable("FurServicios")
        dt.Columns.Add("NUM_FACTURA", GetType(String))
        dt.Columns.Add("NIT_PRESTADOR", GetType(String))
        dt.Columns.Add("Tipo_de_servicio", GetType(String))
        dt.Columns.Add("Codigo_general_del_procedimiento_quirurgico", GetType(String))
        dt.Columns.Add("Consecutivo_procedimiento_quirurgico", GetType(String))
        dt.Columns.Add("Codigo_del_servicio", GetType(String))
        dt.Columns.Add("Codificacion_CUPS", GetType(String))
        dt.Columns.Add("Descripcion_del_servicio_o_elemento_reclamado", GetType(String))
        dt.Columns.Add("Cantidad_de_servicios", GetType(Decimal))
        dt.Columns.Add("Valor_unitario_facturado", GetType(Decimal))
        dt.Columns.Add("Valor_unitario_reclamado", GetType(Decimal))
        dt.Columns.Add("Valor_total_facturado", GetType(Decimal))
        dt.Columns.Add("Valor_total_reclamado", GetType(Decimal))

        For Each row In rows
            Dim dr = dt.NewRow()
            dr("NIT_PRESTADOR") = If(nitPrestador, CObj(DBNull.Value))
            dr("NUM_FACTURA") = If(row.NUM_FACTURA, CObj(DBNull.Value))
            dr("Tipo_de_servicio") = If(row.Tipo_de_servicio, CObj(DBNull.Value))
            dr("Codigo_del_servicio") = If(row.Codigo_del_servicio, CObj(DBNull.Value))
            dr("Codigo_general_del_procedimiento_quirurgico") = If(row.Codigo_general_del_procedimiento_quirurgico, CObj(DBNull.Value))
            dr("Consecutivo_procedimiento_quirurgico") = If(row.Consecutivo_procedimiento_quirurgico, CObj(DBNull.Value))
            dr("Codificacion_CUPS") = If(row.Codificacion_CUPS, CObj(DBNull.Value))
            dr("Descripcion_del_servicio_o_elemento_reclamado") = If(row.Descripcion_del_servicio_o_elemento_reclamado, CObj(DBNull.Value))
            dr("Cantidad_de_servicios") = If(row.Cantidad_de_servicios.HasValue, CObj(row.Cantidad_de_servicios.Value), CObj(DBNull.Value))
            dr("Valor_unitario_facturado") = If(row.Valor_unitario_facturado.HasValue, CObj(row.Valor_unitario_facturado.Value), CObj(DBNull.Value))
            dr("Valor_unitario_reclamado") = If(row.Valor_unitario_reclamado.HasValue, CObj(row.Valor_unitario_reclamado.Value), CObj(DBNull.Value))
            dr("Valor_total_facturado") = If(row.Valor_total_facturado.HasValue, CObj(row.Valor_total_facturado.Value), CObj(DBNull.Value))
            dr("Valor_total_reclamado") = If(row.Valor_total_reclamado.HasValue, CObj(row.Valor_total_reclamado.Value), CObj(DBNull.Value))
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

    Private Function SafeDecimal(dr As DataRow, columnName As String) As Decimal?
        If Not dr.Table.Columns.Contains(columnName) Then Return Nothing
        If dr.IsNull(columnName) Then Return Nothing
        Return Convert.ToDecimal(dr(columnName))
    End Function

    Private Function NullIfEmpty(value As String) As String
        If String.IsNullOrEmpty(value) Then Return Nothing
        Return value
    End Function

    ''' <summary>
    ''' Formatea un número decimal sin separador de miles. Si el valor es entero,
    ''' lo emite sin decimales. Devuelve Nothing si el valor es null.
    ''' </summary>
    Private Function FormatNumber(value As Decimal?) As String
        If Not value.HasValue Then Return Nothing
        Dim v As Decimal = value.Value
        If v = Decimal.Truncate(v) Then
            Return Decimal.Truncate(v).ToString(CultureInfo.InvariantCulture)
        End If
        Return v.ToString(CultureInfo.InvariantCulture)
    End Function

#End Region

End Module
