#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls.MVP

#End Region

Public Class FrmReportSingleCircular

#Region "Datasources"

    ''' <summary>
    ''' Diccionario que almacena diferentes criterios 
    ''' </summary>
    Private criterias As Dictionary(Of String, String)

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para libros contables
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla de datos de archivos 
    ''' </summary>
    Private _FillingArchive As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingArchive As List(Of Tuple(Of Integer, String))
        Get
            If _FillingArchive Is Nothing Then
                _FillingArchive = New List(Of Tuple(Of Integer, String))
                _FillingArchive.Add(New Tuple(Of Integer, String)(1, "FT001 - Catalogo Información Financiera"))
                _FillingArchive.Add(New Tuple(Of Integer, String)(3, "FT003 - Cuentas Por Cobrar – Deudores"))
                _FillingArchive.Add(New Tuple(Of Integer, String)(4, "FT004 - Detalle de Pasivos por Tercero"))
                _FillingArchive.Add(New Tuple(Of Integer, String)(6, "FT006 - Bancos y Carteras Colectivas"))
                _FillingArchive.Add(New Tuple(Of Integer, String)(7, "FT007 - Control de Inversiones Inscritas en el Mercado de Valores de Colombia"))
                _FillingArchive.Add(New Tuple(Of Integer, String)(8, "FT008 - Inversiones – Otros Títulos"))
                _FillingArchive.Add(New Tuple(Of Integer, String)(9, "FT009 - Activos y Pasivos en Moneda Extranjera"))
                _FillingArchive.Add(New Tuple(Of Integer, String)(10, "FT010 - Activos No Monetarios"))
                _FillingArchive.Add(New Tuple(Of Integer, String)(25, "FT025 - Reporte de Facturación Radicada por IPS, Gestores Farmacéuticos y Operadores Logísticos de Tecnologías en Salud a Entidades del Aseguramiento en Salud"))
            End If
            Return _FillingArchive
        End Get
    End Property

    ''' <summary>
    ''' Permite acceder a una lista de niveles de cuenta predefinidos (Clase, Grupo, Cuenta, Subcuenta, Auxiliar, SubAuxiliar) 
    ''' </summary>
    Private _FillingNivel As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingNivel As List(Of Tuple(Of Integer, String))
        Get
            If _FillingNivel Is Nothing Then
                _FillingNivel = New List(Of Tuple(Of Integer, String))
                _FillingNivel.Add(New Tuple(Of Integer, String)(1, "Clase"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(2, "Grupo"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(3, "Cuenta"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(4, "Subcuenta"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(5, "Auxiliar"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(6, "SubAuxiliar"))
            End If
            Return _FillingNivel
        End Get
    End Property

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Load de la barra botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportSingleCircular_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit
        Me.INDGleArchive.Properties.DataSource = Me.FillingArchive
        Me.INDGleAccountLevel.Properties.DataSource = Me.FillingNivel

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleArchive.EditValue = 1
        Me.INDGleAccountLevel.EditValue = 5

        LoadXpoBook()
        SetOfficialBook()
    End Sub

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        bookXpcollection = Nothing
        _FillingArchive = Nothing
        _FillingNivel = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' carga el datasource de libros oficiales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleBook.QueryPopUp
        If INDsleBook.Properties.DataSource Is Nothing Then
            LoadXpoBook()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    '''  Ajusta la visibilidad y los valores de algunos controles según la selección realizada en INDGleArchive
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleArchive_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleArchive.EditValueChanged
        If INDGleArchive.EditValue = 1 Then
            INDLciBook.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAccountLevel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGleAccountLevel.EditValue = 5
        ElseIf INDGleArchive.EditValue = 25 Then
            INDLciBook.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciAccountLevel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleAccountLevel.EditValue = Nothing
        Else
            INDLciBook.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAccountLevel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleAccountLevel.EditValue = Nothing
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta al dar click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcel_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcel.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Accounting.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportSingleCircular(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportSingleCircular As DataTable = ds.Tables("ReportSingleCircular")

                        Await Task.Factory.StartNew(Sub()
                                                        Select Case INDGleArchive.EditValue
                                                            Case 1
                                                                chargueDatasourceFT001(dtReportSingleCircular)
                                                            Case 3
                                                                chargueDatasourceFT003(dtReportSingleCircular)
                                                            Case 4
                                                                chargueDatasourceFT004(dtReportSingleCircular)
                                                            Case 6
                                                                chargueDatasourceFT006(dtReportSingleCircular)
                                                            Case 7
                                                                chargueDatasourceFT007(dtReportSingleCircular)
                                                            Case 8
                                                                chargueDatasourceFT008(dtReportSingleCircular)
                                                            Case 9
                                                                chargueDatasourceFT009(dtReportSingleCircular)
                                                            Case 10
                                                                chargueDatasourceFT010(dtReportSingleCircular)
                                                            Case 25
                                                                chargueDatasourceFT025(dtReportSingleCircular)
                                                            Case Else
                                                                Exit Sub
                                                        End Select
                                                    End Sub)

                        If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                End Using
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBook()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True}
            bookXpcollection = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBookByStatusXpCollection, filter)
            INDsleBook.Properties.DataSource = bookXpcollection
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetOfficialBook()
        If bookXpcollection IsNot Nothing AndAlso bookXpcollection.Count > 0 Then
            Dim item = (From l In bookXpcollection Where l.OfficialBook = True Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDsleBook.EditValue = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("Archive", INDGleArchive.EditValue)
        criterias.Add("Year", INDCdnPeriod.GetYear)
        criterias.Add("Month", INDCdnPeriod.GetMonth)
        criterias.Add("LegalBookId", INDsleBook.EditValue)
        criterias.Add("AccountLevel", INDGleAccountLevel.EditValue)

        Return True
    End Function

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel del archvo FT001
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFT001(ByVal dtReportSingleCircular As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("codigoConcepto")
        dt.Columns.Add("claseConcepto")
        dt.Columns.Add("valor", GetType(Decimal))

        For Each item In dtReportSingleCircular.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("codigoConcepto") = item("codigoConcepto")
            row.Item("claseConcepto") = item("claseConcepto")
            row.Item("valor") = item("valor")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel del archivo FT003
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFT003(ByVal dtReportSingleCircular As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("LineaNegocio")
        dt.Columns.Add("tipoIdDeudor")
        dt.Columns.Add("IdDeudor")
        dt.Columns.Add("DvDeudor")
        dt.Columns.Add("NombreDeudor")
        dt.Columns.Add("CodigoMunicipio")
        dt.Columns.Add("ConceptoDeudores")
        dt.Columns.Add("TipoDeuda")
        dt.Columns.Add("MedicionPosterior")
        dt.Columns.Add("CxcPendientesRadicar", GetType(Decimal))
        dt.Columns.Add("CxcNoVencidas", GetType(Decimal))
        dt.Columns.Add("CxcMora30Dias", GetType(Decimal))
        dt.Columns.Add("CxcMora60Dias", GetType(Decimal))
        dt.Columns.Add("CxcMora90Dias", GetType(Decimal))
        dt.Columns.Add("CxcMora180Dias", GetType(Decimal))
        dt.Columns.Add("CxcMora360Dias", GetType(Decimal))
        dt.Columns.Add("CxcMoraMayor360Dias", GetType(Decimal))
        dt.Columns.Add("Deterioro30Dias", GetType(Decimal))
        dt.Columns.Add("Deterioro60Dias", GetType(Decimal))
        dt.Columns.Add("Deterioro90Dias", GetType(Decimal))
        dt.Columns.Add("Deterioro180Dias", GetType(Decimal))
        dt.Columns.Add("Deterioro360Dias", GetType(Decimal))
        dt.Columns.Add("DeterioroMayor360Dias", GetType(Decimal))
        dt.Columns.Add("Ajuste", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))

        For Each item In dtReportSingleCircular.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("LineaNegocio") = item("BusinessLine")
            row.Item("tipoIdDeudor") = item("tipoIdDeudor")
            row.Item("IdDeudor") = item("IdDeudor")
            row.Item("DvDeudor") = item("DvDeudor")
            row.Item("NombreDeudor") = item("NombreDeudor")
            row.Item("CodigoMunicipio") = item("CodigoMunicipio")
            row.Item("ConceptoDeudores") = item("ConceptoDeudores")
            row.Item("TipoDeuda") = item("TipoDeuda")
            row.Item("MedicionPosterior") = item("MedicionPosterior")
            row.Item("CxcPendientesRadicar") = item("CxcPendientesRadicar")
            row.Item("CxcNoVencidas") = item("CxcNoVencidas")
            row.Item("CxcMora30dias") = item("CxcMora30dias")
            row.Item("CxcMora60dias") = item("CxcMora60dias")
            row.Item("CxcMora90dias") = item("CxcMora90dias")
            row.Item("CxcMora180dias") = item("CxcMora180dias")
            row.Item("CxcMora360dias") = item("CxcMora360dias")
            row.Item("CxcMoraMayor360Dias") = item("CxcMoraMayor360Dias")
            row.Item("Deterioro30Dias") = item("Deterioro30Dias")
            row.Item("Deterioro60Dias") = item("Deterioro60Dias")
            row.Item("Deterioro90Dias") = item("Deterioro90Dias")
            row.Item("Deterioro180Dias") = item("Deterioro180Dias")
            row.Item("Deterioro360Dias") = item("Deterioro360Dias")
            row.Item("DeterioroMayor360Dias") = item("DeterioroMayor360Dias")
            row.Item("Ajuste") = item("Ajuste")
            row.Item("Saldo") = item("Saldo")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel del archivo FT004
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFT004(ByVal dtReportSingleCircular As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("LineaNegocio")
        dt.Columns.Add("TipoIdAcreedor")
        dt.Columns.Add("IdAcreedor")
        dt.Columns.Add("DvAcreedor")
        dt.Columns.Add("NombreAcreedor")
        dt.Columns.Add("ActividadAcreedor")
        dt.Columns.Add("ConceptoAcreencia")
        dt.Columns.Add("MedicionPosterior")
        dt.Columns.Add("CxPNoVencidas", GetType(Decimal))
        dt.Columns.Add("CxPMora30dias", GetType(Decimal))
        dt.Columns.Add("CxPMora60dias", GetType(Decimal))
        dt.Columns.Add("CxPMora90dias", GetType(Decimal))
        dt.Columns.Add("CxPMora180dias", GetType(Decimal))
        dt.Columns.Add("CxPMora360dias", GetType(Decimal))
        dt.Columns.Add("CxPMoraMayor360dias", GetType(Decimal))
        dt.Columns.Add("Ajuste", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))
        dt.Columns.Add("CxpRecursos")
        dt.Columns.Add("MetodoDePago")

        For Each item In dtReportSingleCircular.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("LineaNegocio") = item("BusinessLine")
            row.Item("TipoIdAcreedor") = item("TipoIdAcreedor")
            row.Item("IdAcreedor") = item("IdAcreedor")
            row.Item("DvAcreedor") = item("DvAcreedor")
            row.Item("NombreAcreedor") = item("NombreAcreedor")
            row.Item("ActividadAcreedor") = item("ActividadAcreedor")
            row.Item("ConceptoAcreencia") = item("ConceptoAcreencia")
            row.Item("MedicionPosterior") = item("MedicionPosterior")
            row.Item("CxPNoVencidas") = item("CxPNoVencidas")
            row.Item("CxPMora30dias") = item("CxPMora30dias")
            row.Item("CxPMora60dias") = item("CxPMora60dias")
            row.Item("CxPMora90dias") = item("CxPMora90dias")
            row.Item("CxPMora180dias") = item("CxPMora180dias")
            row.Item("CxPMora360dias") = item("CxPMora360dias")
            row.Item("CxPMoraMayor360dias") = item("CxPMoraMayor360dias")
            row.Item("Ajuste") = item("Ajuste")
            row.Item("Saldo") = item("Saldo")
            row.Item("CxpRecursos") = item("CxPRecursos")
            row.Item("MetodoDePago") = item("MetodoPago")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel del archivo FT006
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFT006(ByVal dtReportSingleCircular As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("LineaNegocio")
        dt.Columns.Add("Establecimiento")
        dt.Columns.Add("IdEstablecimiento")
        dt.Columns.Add("CodigoSIMEV")
        dt.Columns.Add("CalificacionRiesgo")
        dt.Columns.Add("EntidadCalificadora")
        dt.Columns.Add("OtraCalificadora")
        dt.Columns.Add("ClaseCuenta")
        dt.Columns.Add("TipoMoneda")
        dt.Columns.Add("IdCuenta")
        dt.Columns.Add("NombreCuenta")
        dt.Columns.Add("SaldoExtracto", GetType(Decimal))
        dt.Columns.Add("SaldoLibros", GetType(Decimal))
        dt.Columns.Add("Sobregiro", GetType(Decimal))
        dt.Columns.Add("Rendimientos", GetType(Decimal))
        dt.Columns.Add("Gravamen")
        dt.Columns.Add("Estado")
        dt.Columns.Add("FechaMedida")
        dt.Columns.Add("ValorMedida", GetType(Decimal))
        dt.Columns.Add("InversionReservas")
        For Each item In dtReportSingleCircular.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("LineaNegocio") = item("BusinessLine")
            row.Item("Establecimiento") = item("Establecimiento")
            row.Item("IdEstablecimiento") = item("IdEstablecimiento")
            row.Item("CodigoSIMEV") = item("CodigoSIMEV")
            row.Item("CalificacionRiesgo") = item("CalificacionRiesgo")
            row.Item("EntidadCalificadora") = item("EntidadCalificadora")
            row.Item("OtraCalificadora") = item("OtraCalificadora")
            row.Item("ClaseCuenta") = item("ClaseCuenta")
            row.Item("TipoMoneda") = item("TipoMoneda")
            row.Item("IdCuenta") = item("IdCuenta")
            row.Item("NombreCuenta") = item("NombreCuenta")
            row.Item("SaldoExtracto") = item("SaldoExtracto")
            row.Item("SaldoLibros") = item("SaldoLibros")
            row.Item("Sobregiro") = item("Sobregiro")
            row.Item("Rendimientos") = item("Rendimientos")
            row.Item("Gravamen") = item("Gravamen")
            row.Item("Estado") = item("Estado")
            row.Item("FechaMedida") = item("FechaMedida")
            row.Item("ValorMedida") = item("ValorMedida")
            row.Item("InversionReservas") = item("InversionReservas")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel del archivo FT007
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFT007(ByVal dtReportSingleCircular As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("LineaNegocio")
        dt.Columns.Add("Instrumento")
        dt.Columns.Add("TipoInversion")
        dt.Columns.Add("Nemotécnico")
        dt.Columns.Add("CodigoTitulo")
        dt.Columns.Add("IdEmisora")
        dt.Columns.Add("dvEmisora")
        dt.Columns.Add("CodigoSIMEV")
        dt.Columns.Add("EntidadCalificadora")
        dt.Columns.Add("OtraCalificadora")
        dt.Columns.Add("CalificacionRiesgo")
        dt.Columns.Add("FechaEmision")
        dt.Columns.Add("FechaVencimiento")
        dt.Columns.Add("FechaCompra")
        dt.Columns.Add("ValorCompra", GetType(Decimal))
        dt.Columns.Add("TasaCompra")
        dt.Columns.Add("ValorNominal", GetType(Decimal))
        dt.Columns.Add("TipoMoneda")
        dt.Columns.Add("TasaFacial")
        dt.Columns.Add("Modalidad")
        dt.Columns.Add("Periodicidad")
        dt.Columns.Add("TasaMercado")
        dt.Columns.Add("ValorMercado", GetType(Decimal))
        dt.Columns.Add("Duracion", GetType(Decimal))
        dt.Columns.Add("Gravamen")
        dt.Columns.Add("Estado")
        dt.Columns.Add("FechaMedida")
        dt.Columns.Add("ValorMedida", GetType(Decimal))
        dt.Columns.Add("Vinculado")
        dt.Columns.Add("Desmaterializado")
        dt.Columns.Add("InversionReservas")
        For Each item In dtReportSingleCircular.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("LineaNegocio") = item("BusinessLine")
            row.Item("Instrumento") = item("Instrumento")
            row.Item("TipoInversion") = item("TipoInversion")
            row.Item("Nemotécnico") = item("Nemotécnico")
            row.Item("CodigoTitulo") = item("CodigoTitulo")
            row.Item("IdEmisora") = item("IdEmisora")
            row.Item("dvEmisora") = item("dvEmisora")
            row.Item("CodigoSIMEV") = item("CodigoSIMEV")
            row.Item("EntidadCalificadora") = item("EntidadCalificadora")
            row.Item("OtraCalificadora") = item("OtraCalificadora")
            row.Item("CalificacionRiesgo") = item("CalificacionRiesgo")
            row.Item("FechaEmision") = item("FechaEmision")
            row.Item("FechaVencimiento") = item("FechaVencimiento")
            row.Item("FechaCompra") = item("FechaCompra")
            row.Item("ValorCompra") = item("ValorCompra")
            row.Item("TasaCompra") = item("TasaCompra")
            row.Item("ValorNominal") = item("ValorNominal")
            row.Item("TipoMoneda") = item("TipoMoneda")
            row.Item("TasaFacial") = item("TasaFacial")
            row.Item("Modalidad") = item("Modalidad")
            row.Item("Periodicidad") = item("Periodicidad")
            row.Item("TasaMercado") = item("TasaMercado")
            row.Item("ValorMercado") = item("ValorMercado")
            row.Item("Duracion") = item("Duracion")
            row.Item("Gravamen") = item("Gravamen")
            row.Item("Estado") = item("Estado")
            row.Item("FechaMedida") = item("FechaMedida")
            row.Item("ValorMedida") = item("ValorMedida")
            row.Item("Vinculado") = item("Vinculado")
            row.Item("Desmaterializado") = item("Desmaterializado")
            row.Item("InversionReservas") = item("InversionReservas")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel del archivo FT008
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFT008(ByVal dtReportSingleCircular As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("LineaNegocio")
        dt.Columns.Add("Pais")
        dt.Columns.Add("IdEmisora")
        dt.Columns.Add("DvEmisora")
        dt.Columns.Add("NombreEmisora")
        dt.Columns.Add("TipoInversion")
        dt.Columns.Add("OtraInversion")
        dt.Columns.Add("FechaEmision")
        dt.Columns.Add("FechaVencimiento")
        dt.Columns.Add("FechaCompra")
        dt.Columns.Add("ValorCompra")
        dt.Columns.Add("TasaCompra")
        dt.Columns.Add("ValorNominal")
        dt.Columns.Add("TipoMoneda")
        dt.Columns.Add("TasaFacial")
        dt.Columns.Add("Modalidad")
        dt.Columns.Add("Periodicidad")
        dt.Columns.Add("TasaMercado")
        dt.Columns.Add("ValorMercado")
        dt.Columns.Add("Duracion")
        dt.Columns.Add("Participacion")
        dt.Columns.Add("Rendimientos")
        dt.Columns.Add("Gravamen")
        dt.Columns.Add("Estado")
        dt.Columns.Add("FechaMedida")
        dt.Columns.Add("ValorMedida")
        dt.Columns.Add("Vinculado")
        For Each item In dtReportSingleCircular.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("LineaNegocio") = item("BusinessLine")
            row.Item("Pais") = item("Pais")
            row.Item("IdEmisora") = item("IdEmisora")
            row.Item("DvEmisora") = item("DvEmisora")
            row.Item("NombreEmisora") = item("NombreEmisora")
            row.Item("TipoInversion") = item("TipoInversion")
            row.Item("OtraInversion") = item("OtraInversion")
            row.Item("FechaEmision") = item("FechaEmision")
            row.Item("FechaVencimiento") = item("FechaVencimiento")
            row.Item("FechaCompra") = item("FechaCompra")
            row.Item("ValorCompra") = item("ValorCompra")
            row.Item("TasaCompra") = item("TasaCompra")
            row.Item("ValorNominal") = item("ValorNominal")
            row.Item("TipoMoneda") = item("TipoMoneda")
            row.Item("TasaFacial") = item("TasaFacial")
            row.Item("Modalidad") = item("Modalidad")
            row.Item("Periodicidad") = item("Periodicidad")
            row.Item("TasaMercado") = item("TasaMercado")
            row.Item("ValorMercado") = item("ValorMercado")
            row.Item("Duracion") = item("Duracion")
            row.Item("Participacion") = item("Participacion")
            row.Item("Rendimientos") = item("Rendimientos")
            row.Item("Gravamen") = item("Gravamen")
            row.Item("Estado") = item("Estado")
            row.Item("FechaMedida") = item("FechaMedida")
            row.Item("ValorMedida") = item("ValorMedida")
            row.Item("Vinculado") = item("Vinculado")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel del archivo FT010
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFT009(ByVal dtReportSingleCircular As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("lineaNegocio")
        dt.Columns.Add("tipoMoneda")
        dt.Columns.Add("activos", GetType(Decimal))
        dt.Columns.Add("pasivos", GetType(Decimal))

        For Each item In dtReportSingleCircular.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("lineaNegocio") = item("BusinessLine")
            row.Item("tipoMoneda") = item("tipoMoneda")
            row.Item("activos") = item("activos")
            row.Item("pasivos") = item("pasivos")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel del archivo FT010
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFT010(ByVal dtReportSingleCircular As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("lineaNegocio")
        dt.Columns.Add("concepto")
        dt.Columns.Add("descripcion")
        dt.Columns.Add("finalidad")
        dt.Columns.Add("medicion")
        dt.Columns.Add("reconocimiento", GetType(Decimal))
        dt.Columns.Add("depreciacion", GetType(Decimal))
        dt.Columns.Add("deterioro", GetType(Decimal))
        dt.Columns.Add("estado")
        dt.Columns.Add("fechaMedida")
        dt.Columns.Add("valorMedida", GetType(Decimal))

        For Each item In dtReportSingleCircular.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("lineaNegocio") = item("LineaNegocio")
            row.Item("concepto") = item("Concepto")
            row.Item("descripcion") = item("Descripcion")
            row.Item("finalidad") = item("Finalidad")
            row.Item("medicion") = item("Medicion")
            row.Item("reconocimiento") = item("Reconocimiento")
            row.Item("depreciacion") = item("Depreciacion")
            row.Item("deterioro") = item("Deterioro")
            row.Item("estado") = item("Estado")
            row.Item("fechaMedida") = item("FechaMedida")
            row.Item("valorMedida") = item("ValorMedida")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel del archivo FT010
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFT025(ByVal dtReportSingleCircular As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("NIT")
        dt.Columns.Add("razonSocial")
        dt.Columns.Add("tipoAseguramiento")
        dt.Columns.Add("ModalidadPago")
        dt.Columns.Add("OtraModalidad")
        dt.Columns.Add("ingresos", GetType(Decimal))
        dt.Columns.Add("ValorFacturación", GetType(Decimal))
        dt.Columns.Add("LineaNegocio")

        For Each item In dtReportSingleCircular.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("NIT") = item("ThirdPartyNit")
            row.Item("razonSocial") = item("ThirdPartyName")
            row.Item("tipoAseguramiento") = item("EntityType")
            row.Item("ModalidadPago") = item("LiquidationType")
            row.Item("OtraModalidad") = item("ContractType")
            row.Item("ingresos") = item("CollectionValue")
            row.Item("ValorFacturación") = item("InvoiceValue")
            row.Item("LineaNegocio") = item("BusinessLine")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

#End Region

#End Region

End Class