#Region "Imports"

Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Infrastructure.Data.Xpo
Imports Presentation.Common.MVP
Imports Presentation.CloudAgent

#End Region

Public Class FrmReportPortfolio2193

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ProoftCloseXpoClients As XPInstantFeedbackSource
    Public Property ProoftCloseXpoSellers As XPInstantFeedbackSource

    Private _TypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property TypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _TypeReport Is Nothing Then
                _TypeReport = New List(Of Tuple(Of Integer, String))
                _TypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _TypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _TypeReport
        End Get
    End Property

    Private _PersonType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property PersonType As List(Of Tuple(Of Integer, String))
        Get
            If _PersonType Is Nothing Then
                _PersonType = New List(Of Tuple(Of Integer, String))
                _PersonType.Add(New Tuple(Of Integer, String)(1, "Natural"))
                _PersonType.Add(New Tuple(Of Integer, String)(2, "Jurídica"))
                _PersonType.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _PersonType
        End Get
    End Property

    Private criterias As Dictionary(Of String, String)
    Private filters As Dictionary(Of String, String)

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportPortfolio2193_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = TypeReport
        Me.INDGlePersonType.Properties.DataSource = PersonType

        AddHandler Me.EventLoadOperatingUnit, AddressOf LoadOperatingUnit

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGlePersonType.EditValue = 2
        Me.INDSleOperatingUnit.EditValue = indigo.IndigoOperatingUnitId

        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleCustomerStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCustomerByNit
        Me.INDSleCustomerEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCustomerByNit
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCustomerStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCustomerStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCustomerStart.QueryPopUp
        If INDSleCustomerStart.Datasource Is Nothing Then
            LoadXpoCustomerStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCustomerEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCustomerEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCustomerEnd.QueryPopUp
        If INDSleCustomerEnd.Datasource Is Nothing Then
            LoadXpoCustomerEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)

            Dim reporte As New rptReportPortfolio2193
            reporte.ParametrosReporte = New Object() {criterias, filters}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDClosingDate.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListReportPortfolio2193Async(criterias, filters, Me.IndigoSessionValues)
                If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dim dtReportPortfolio2193 As DataTable = ds.Tables("ReportPortfolio2193")

                    'cargamos el listado de las edades de cartera 
                    Dim filtroConsultaSettingsPayment As String = "SettingPortfolioId.OperatingUnitId = " & INDSleOperatingUnit.EditValue
                    Dim listSettingPortfolio As List(Of PortfolioAgesPortfolioXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioAgesPortfolioXpo)(Nothing, filtroConsultaSettingsPayment)

                    Await Task.Factory.StartNew(Sub()
                                                    If INDGleTypeReport.EditValue = 1 Then
                                                        chargueDatasource(listSettingPortfolio, dtReportPortfolio2193)
                                                    Else
                                                        chargueDatasourceResum(listSettingPortfolio, dtReportPortfolio2193)
                                                    End If
                                                End Sub)

                    If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                        generateExcel()
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoClients = Nothing
        ProoftCloseXpoSellers = Nothing
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        If INDDClosingDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateClosingDateReport", "Commons"))
            Me.INDDClosingDate.Focus()
            Validations = False
        End If
        'validaciones controles de cliente
        If INDSleCustomerStart.EditValue IsNot Nothing And INDSleCustomerEnd.EditValue Is Nothing Or INDSleCustomerStart.EditValue Is Nothing And INDSleCustomerEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCustomer.Text)
            Me.INDSleCustomerStart.Focus()
            Validations = False
        ElseIf INDSleCustomerStart.EditValue > INDSleCustomerEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCustomer.Text)
            Me.INDSleCustomerStart.Focus()
            Validations = False
        End If
        If String.IsNullOrEmpty(INDCcbeDocumentType.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un Tipo de Factura"
            Me.INDCcbeDocumentType.Focus()
            Validations = False
        End If

        If Validations Then
            criterias = New Dictionary(Of String, String)
            criterias.Add("ClosingDate", INDDClosingDate.EditValue)
            criterias.Add("OperatingUnit", INDSleOperatingUnit.EditValue)
            criterias.Add("TypeReport", INDGleTypeReport.EditValue)

            filters = New Dictionary(Of String, String)
            filters.Add("PersonType", INDGlePersonType.EditValue)
            filters.Add("CustomerStart", INDSleCustomerStart.EditValue)
            filters.Add("CustomerEnd", INDSleCustomerEnd.EditValue)
            filters.Add("DocumentType", INDCcbeDocumentType.EditValue)
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCustomerStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomerStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoClients = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCustomerReportPortfolio)
            INDSleCustomerStart.Datasource = ProoftCloseXpoClients
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCustomerEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomerEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoClients = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCustomerReportPortfolio)
            INDSleCustomerEnd.Datasource = ProoftCloseXpoClients
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleOperatingUnit
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadOperatingUnit(listOperatingUnit As List(Of OperatingUnit))
        INDSleOperatingUnit.Properties.DataSource = listOperatingUnit
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel resumido
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource(ByVal listSettingPortfolio As List(Of PortfolioAgesPortfolioXpo), ByVal dtReportPortfolio2193 As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Régimen")
        dt.Columns.Add("Numero Documento")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Numero Factura")
        dt.Columns.Add("Fecha Documento", GetType(DateTime))

        Dim NameMinimumAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMinimumAgeRange
        Dim NameMaximumAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMaximumAgeRange

        Dim RangeMin = listSettingPortfolio.Min(Function(x) x.InitialRange)
        Dim RangeMax = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.MaximunAgeRange

        For Each item In listSettingPortfolio
            dt.Columns.Add(item.Name, GetType(Decimal))
        Next

        dt.Columns.Add(NameMaximumAgeRange, GetType(Decimal))
        dt.Columns.Add("Total Cartera Radicada", GetType(Decimal))
        dt.Columns.Add(NameMinimumAgeRange, GetType(Decimal))

        dt.Columns.Add("Valor Glosado", GetType(Decimal))
        dt.Columns.Add("Valor Aceptado", GetType(Decimal))
        dt.Columns.Add("Deterioro Cartera", GetType(Decimal))

        For Each item In dtReportPortfolio2193.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Régimen") = item("RegimenCalculated")
            row.Item("Numero Documento") = item("ThirdPartyNit")
            row.Item("Nombre Tercero") = item("ThirdPartyName")
            row.Item("Numero Factura") = item("InvoiceNumber")
            row.Item("Fecha Documento") = CDate(item("AccountReceivableDate")).AsDate

            Dim value As Decimal = 0
            Dim totalRadicated As Decimal = 0
            For Each itemSettings In listSettingPortfolio
                value = If(item("Age") >= itemSettings.InitialRange AndAlso item("Age") <= itemSettings.EndRange, item("Balance"), 0)
                totalRadicated = totalRadicated + value
                row.Item(itemSettings.Name) = value
            Next
            value = If(item("Age") > RangeMax, item("Balance"), 0)
            totalRadicated = totalRadicated + value
            row.Item(NameMaximumAgeRange) = value
            row.Item("Total Cartera Radicada") = totalRadicated

            value = If(item("Age") < RangeMin, item("Balance"), 0)
            row.Item(NameMinimumAgeRange) = value

            row.Item("Valor Glosado") = item("ValueGlosado")
            row.Item("Valor Aceptado") = item("ValueAcceptedFirstInstance") + item("ValueAcceptedSecondInstance")
            row.Item("Deterioro Cartera") = item("DeteriorationBalance")

            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel resumido
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceResum(ByVal listSettingPortfolio As List(Of PortfolioAgesPortfolioXpo), ByVal dtReportPortfolio2193 As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Régimen")
        dt.Columns.Add("Numero Documento")
        dt.Columns.Add("Nombre Tercero")

        Dim groupedResult As IEnumerable(Of Object) = From t In dtReportPortfolio2193.AsEnumerable()
                                                      Group t By Key = New With {
                                                            Key .ThirdPartyNit = t.Field(Of String)("ThirdPartyNit"),
                                                            Key .ThirdPartyName = t.Field(Of String)("ThirdPartyName"),
                                                            Key .RegimenCalculated = t.Field(Of String)("RegimenCalculated")
                                                      }
                                                      Into Group Select New With {
                                                            .ThirdPartyNit = Key.ThirdPartyNit,
                                                            .ThirdPartyName = Key.ThirdPartyName,
                                                            .RegimenCalculated = Key.RegimenCalculated
                                                      }

        Dim NameMinimumAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMinimumAgeRange
        Dim NameMaximumAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMaximumAgeRange

        Dim RangeMin = listSettingPortfolio.Min(Function(x) x.InitialRange)
        Dim RangeMax = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.MaximunAgeRange

        For Each item In listSettingPortfolio
            dt.Columns.Add(item.Name, GetType(Decimal))
        Next

        dt.Columns.Add(NameMaximumAgeRange, GetType(Decimal))
        dt.Columns.Add("Total Cartera Radicada", GetType(Decimal))
        dt.Columns.Add(NameMinimumAgeRange, GetType(Decimal))

        dt.Columns.Add("Valor Glosado", GetType(Decimal))
        dt.Columns.Add("Valor Aceptado", GetType(Decimal))
        dt.Columns.Add("Deterioro Cartera", GetType(Decimal))

        For Each item In groupedResult
            Dim row As DataRow = dt.NewRow()
            Dim filterResult As List(Of DataRow) = dtReportPortfolio2193.AsEnumerable().Where(Function(t) t.Field(Of String)("ThirdPartyNit") = item.ThirdPartyNit AndAlso t.Field(Of String)("ThirdPartyName") = item.ThirdPartyName AndAlso t.Field(Of String)("RegimenCalculated") = item.RegimenCalculated).ToList()

            row.Item("Régimen") = item.RegimenCalculated
            row.Item("Numero Documento") = item.ThirdPartyNit
            row.Item("Nombre Tercero") = item.ThirdPartyName

            Dim value As Decimal = 0
            Dim totalRadicated As Decimal = 0
            For Each itemSettings In listSettingPortfolio
                value = filterResult.Where(Function(d) d.Field(Of Integer)("Age") >= itemSettings.InitialRange AndAlso d.Field(Of Integer)("Age") <= itemSettings.EndRange).Sum(Function(d) d.Field(Of Decimal)("Balance"))
                totalRadicated = totalRadicated + value
                row.Item(itemSettings.Name) = value
            Next
            value = filterResult.Where(Function(d) d.Field(Of Integer)("Age") > RangeMax).Sum(Function(d) d.Field(Of Decimal)("Balance"))
            totalRadicated = totalRadicated + value
            row.Item(NameMaximumAgeRange) = value
            row.Item("Total Cartera Radicada") = totalRadicated

            value = filterResult.Where(Function(d) d.Field(Of Integer)("Age") < RangeMin).Sum(Function(d) d.Field(Of Decimal)("Balance"))
            row.Item(NameMinimumAgeRange) = value

            row.Item("Valor Glosado") = filterResult.Sum(Function(d) d.Field(Of Decimal)("ValueGlosado"))
            row.Item("Valor Aceptado") = filterResult.Sum(Function(d) d.Field(Of Decimal)("ValueAcceptedFirstInstance") + d.Field(Of Decimal)("ValueAcceptedSecondInstance"))
            row.Item("Deterioro Cartera") = filterResult.Sum(Function(d) d.Field(Of Decimal)("DeteriorationBalance"))

            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleTypeReport.EditValue & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

#End Region

End Class