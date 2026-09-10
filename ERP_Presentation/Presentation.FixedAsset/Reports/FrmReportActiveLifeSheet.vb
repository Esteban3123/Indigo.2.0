#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
#End Region

Public Class FrmReportActiveLifeSheet
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
#End Region

#Region "Properties"
    Public Property ProoftCloseXpoResponsible As XPInstantFeedbackSource
    Public Property ProoftCloseXpoClassification As XPInstantFeedbackSource
    Public Property ProoftCloseXpoStatusAsset As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEquipmentType As XPInstantFeedbackSource
    Public Property ProoftCloseXpoItem As XPInstantFeedbackSource
    Public Property ProoftCloseXpoPlate As XPInstantFeedbackSource
    Public Property ProoftCloseXpoLocation As XPCollection

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private List As List(Of FixedAssetPhysicalAssetReportXpo)

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'Valida Tipo de equipo
        If INDSleEquipmentTypeStart.EditValue Is Nothing And INDSleEquipmentTypeEnd.EditValue IsNot Nothing Or INDSleEquipmentTypeEnd.EditValue Is Nothing And INDSleEquipmentTypeStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblEquipmentType.Text)
            Me.INDSleEquipmentTypeStart.Focus()
            Validations = False
        ElseIf INDSleEquipmentTypeEnd.EditValue < INDSleEquipmentTypeStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblEquipmentType.Text)
            Me.INDSleEquipmentTypeStart.Focus()
            Validations = False
        End If

        'Valida Plate
        If INDSlePlateStart.EditValue Is Nothing And INDSlePlateEnd.EditValue IsNot Nothing Or INDSlePlateEnd.EditValue Is Nothing And INDSlePlateStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblPlate.Text)
            Me.INDSlePlateStart.Focus()
            Validations = False
        ElseIf INDSlePlateEnd.EditValue < INDSlePlateStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblPlate.Text)
            Me.INDSlePlateStart.Focus()
            Validations = False
        End If

        'Valida Location
        If INDSleLocationStart.EditValue Is Nothing And INDSleLocationEnd.EditValue IsNot Nothing Or INDSleLocationEnd.EditValue Is Nothing And INDSleLocationStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblLocation.Text)
            Me.INDSleLocationStart.Focus()
            Validations = False
        ElseIf INDSleLocationEnd.EditValue < INDSleLocationStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblLocation.Text)
            Me.INDSleLocationStart.Focus()
            Validations = False
        End If

        'Valida Artículo
        If INDSleItemStart.EditValue Is Nothing And INDSleItemEnd.EditValue IsNot Nothing Or INDSleItemEnd.EditValue Is Nothing And INDSleItemStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblItem.Text)
            Me.INDSleItemStart.Focus()
            Validations = False
        ElseIf INDSleItemEnd.EditValue < INDSleItemStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblItem.Text)
            Me.INDSleItemStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

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

#Region "Eventos"
    ''' <summary>
    ''' cargamos el datasource de Responsable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsible.QueryPopUp
        If INDSleResponsible.Datasource Is Nothing Then
            LoadXpoResponsible()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Clasificación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleClassification_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleClassification.QueryPopUp
        If INDSleClassification.Datasource Is Nothing Then
            LoadXpoClassification()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de Esatdo Activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleStatusAsset_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleStatusAsset.QueryPopUp
        If INDSleStatusAsset.Datasource Is Nothing Then
            LoadXpoStatusAsset()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource del Artículo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItemStart.QueryPopUp
        If INDSleItemStart.Datasource Is Nothing Then
            LoadXpoItemStart()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource del Artículo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItemEnd.QueryPopUp
        If INDSleItemEnd.Datasource Is Nothing Then
            LoadXpoItemEnd()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource del Tipo de Equipo Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEquipmentTypeStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEquipmentTypeStart.QueryPopUp
        If INDSleEquipmentTypeStart.Datasource Is Nothing Then
            LoadXpoEquipmentTypeStart()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource del Tipo de Equipo Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks> 
    Private Sub INDSleEquipmentTypeEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEquipmentTypeEnd.QueryPopUp
        If INDSleEquipmentTypeEnd.Datasource Is Nothing Then
            LoadXpoEquipmentTypeEnd()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Placa Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePlateStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePlateStart.QueryPopUp
        If INDSlePlateStart.Datasource Is Nothing Then
            LoadXpoPlateStart()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Placa Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePlateEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePlateEnd.QueryPopUp
        If INDSlePlateEnd.Datasource Is Nothing Then
            LoadXpoPlateEnd()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Ubicación Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleLocationStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleLocationStart.QueryPopUp
        If INDSleLocationStart.Datasource Is Nothing Then
            LoadXpoLocationStart()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de Ubicación Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleLocationEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleLocationEnd.QueryPopUp
        If INDSleLocationEnd.Datasource Is Nothing Then
            LoadXpoLocationEnd()
        End If
    End Sub


    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.FixedAsset.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportActiveLifeSheet(INDDnMonthYear.GetYear,
                                                      INDDnMonthYear.GetMonth,
                                                      INDSlePlateStart.EditValue,
                                                      INDSlePlateEnd.EditValue,
                                                      INDSleItemStart.EditValue,
                                                      INDSleItemEnd.EditValue,
                                                      INDSleEquipmentTypeStart.EditValue,
                                                      INDSleEquipmentTypeEnd.EditValue,
                                                      INDSleLocationStart.EditValue,
                                                      INDSleLocationEnd.EditValue,
                                                      INDSleResponsible.EditValue,
                                                      INDSleClassification.EditValue,
                                                      INDSleStatusAsset.EditValue)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportActiveLifeSheet As DataTable = ds.Tables("FixedAssetPhysicalAsset")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDatasource(dtReportActiveLifeSheet)
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

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource(dtReportActiveLifeSheet As DataTable) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Placa")
        dt.Columns.Add("Serie")
        dt.Columns.Add("Fecha Adquisición", GetType(DateTime))
        dt.Columns.Add("Responsable")
        dt.Columns.Add("Artículo")
        dt.Columns.Add("Catálogo")
        dt.Columns.Add("Catálogo Bienes y Servicios")
        dt.Columns.Add("Ubicación")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Ingreso")
        dt.Columns.Add("Fecha Ingreso", GetType(DateTime))
        dt.Columns.Add("Fecha Salida", GetType(DateTime))

        dt.Columns.Add("Libro")
        dt.Columns.Add("Año")
        dt.Columns.Add("Mes")
        dt.Columns.Add("Depreciación Saldo inicial", GetType(Decimal))
        dt.Columns.Add("PAAG", GetType(Decimal))
        dt.Columns.Add("Adiciones", GetType(Decimal))
        dt.Columns.Add("Adiciones Mes", GetType(Decimal))
        dt.Columns.Add("Valor Acumulado Activo (Vr Adq + Ad)", GetType(Decimal))
        dt.Columns.Add("Ajuste por Inflación Act Acumulado", GetType(Decimal))
        dt.Columns.Add("Valor Total del Activo", GetType(Decimal))
        dt.Columns.Add("Depreciación Histórica", GetType(Decimal))
        dt.Columns.Add("Depreciación Mes", GetType(Decimal))
        dt.Columns.Add("Acumulado Depreciación Mes a Mes", GetType(Decimal))
        dt.Columns.Add("At Acumulada Depreciación", GetType(Decimal))
        dt.Columns.Add("Saldo Libros", GetType(Decimal))

        For Each item In dtReportActiveLifeSheet.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Placa") = item("Plate")
            row.Item("Serie") = item("Serie")
            row.Item("Fecha Adquisición") = CDate(item("AdquisitionDate")).AsDate
            row.Item("Responsable") = item("ThirdPartyNitName")
            row.Item("Artículo") = item("ItemCodeName")
            row.Item("Catálogo") = item("ItemCatalogCodeName")
            row.Item("Catálogo Bienes y Servicios") = item("CatalogOfPropertyandServicesCodeDescription")
            row.Item("Ubicación") = item("LocationCodeName")
            row.Item("Estado") = item("StatusName")
            row.Item("Ingreso") = item("EntryNumber")
            row.Item("Fecha Ingreso") = If(IsDBNull(item("PurchaseDate")), DBNull.Value, CDate(item("PurchaseDate")).AsDate)
            row.Item("Fecha Salida") = If(IsDBNull(item("OutputDate")), DBNull.Value, CDate(item("OutputDate")).AsDate)

            row.Item("Libro") = item("Name")
            row.Item("Año") = item("year")
            row.Item("Mes") = item("month")
            row.Item("Depreciación Saldo inicial") = item("InitialBalanceDepreciation")
            row.Item("PAAG") = item("PAAG")
            row.Item("Adiciones") = item("additions")
            row.Item("Adiciones Mes") = item("additionsMonth")
            row.Item("Valor Acumulado Activo (Vr Adq + Ad)") = item("accumulatedValueAsset")
            row.Item("Ajuste por Inflación Act Acumulado") = item("InflationAdjustmentAccumulatedAct")
            row.Item("Valor Total del Activo") = item("TotalValueAsset")
            row.Item("Depreciación Histórica") = item("historicalDepreciated")
            row.Item("Depreciación Mes") = item("depreciationMonth")
            row.Item("Acumulado Depreciación Mes a Mes") = item("accumulatedDepreciationMonthToMonth")
            row.Item("At Acumulada Depreciación") = item("ATAccumulatedDepreciation")
            row.Item("Saldo Libros") = item("balance books")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Function

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


    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
    End Sub

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptActiveLifeSheet
            reporte.ParametrosReporte = New Object() {INDDnMonthYear.GetYear,
                                                      INDDnMonthYear.GetMonth,
                                                      INDSlePlateStart.EditValue,
                                                      INDSlePlateEnd.EditValue,
                                                      INDSleItemStart.EditValue,
                                                      INDSleItemEnd.EditValue,
                                                      INDSleEquipmentTypeStart.EditValue,
                                                      INDSleEquipmentTypeEnd.EditValue,
                                                      INDSleLocationStart.EditValue,
                                                      INDSleLocationEnd.EditValue,
                                                      INDSleResponsible.EditValue,
                                                      INDSleClassification.EditValue,
                                                      INDSleStatusAsset.EditValue}

            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSource1()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDnMonthYear.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
        Me.INDDnMonthYear.Focus()
    End Sub

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportListFixedAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        'Me.INDSleSubAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleSubAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleGroupStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        'Me.INDSleGroupEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode

        INDSleEquipmentTypeStart.View.OptionsView.ShowGroupPanel = False
        INDSleEquipmentTypeEnd.View.OptionsView.ShowGroupPanel = False
        INDSlePlateStart.View.OptionsView.ShowGroupPanel = False
        INDSlePlateEnd.View.OptionsView.ShowGroupPanel = False
        INDSleLocationStart.View.OptionsView.ShowGroupPanel = False
        INDSleLocationEnd.View.OptionsView.ShowGroupPanel = False
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleResponsible
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoResponsible()
        Using msearch As New MBusqueda
            ProoftCloseXpoResponsible = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetResponsible)
            INDSleResponsible.Datasource = ProoftCloseXpoResponsible
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleClassification
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoClassification()
        Using msearch As New MBusqueda
            ProoftCloseXpoClassification = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentCatalog)
            INDSleClassification.Datasource = ProoftCloseXpoClassification
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleStatusAsset
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoStatusAsset()
        Using msearch As New MBusqueda
            ProoftCloseXpoStatusAsset = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetStatusAsset)
            INDSleStatusAsset.Datasource = ProoftCloseXpoStatusAsset
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleItemStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoItemStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoItem = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            INDSleItemStart.Datasource = ProoftCloseXpoItem
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleItemEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoItemEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoItem = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            INDSleItemEnd.Datasource = ProoftCloseXpoItem
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEquipmentTypeStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEquipmentTypeStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoEquipmentType = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentType)
            INDSleEquipmentTypeStart.Datasource = ProoftCloseXpoEquipmentType
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEquipmentTypeEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEquipmentTypeEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoEquipmentType = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentType)
            INDSleEquipmentTypeEnd.Datasource = ProoftCloseXpoEquipmentType
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePlateStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoPlateStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoPlate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            INDSlePlateStart.Datasource = ProoftCloseXpoPlate
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePlateEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoPlateEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoPlate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            INDSlePlateEnd.Datasource = ProoftCloseXpoPlate
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleLocationStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoLocationStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoLocation = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetLocation)
            INDSleLocationStart.Datasource = ProoftCloseXpoLocation
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleLocationEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoLocationEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoLocation = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetLocation)
            INDSleLocationEnd.Datasource = ProoftCloseXpoLocation
        End Using
    End Sub
#End Region
End Class