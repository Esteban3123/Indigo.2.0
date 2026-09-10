#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportHistoricalDepreciation

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property LegalBookXpo As XPCollection
    Public Property InitialMainAccountXpo As XPInstantFeedbackSource
    Public Property FinalMainAccountXpo As XPInstantFeedbackSource
    Public Property InitialDepreciationAccountXpo As XPInstantFeedbackSource
    Public Property FinalDepreciationAccountXpo As XPInstantFeedbackSource
    Public Property InitialCatalogXpo As XPInstantFeedbackSource
    Public Property FinalCatalogXpo As XPInstantFeedbackSource
    Public Property InitialItemXpo As XPInstantFeedbackSource
    Public Property FinalItemXpo As XPInstantFeedbackSource
    Public Property InitialTypeXpo As XPInstantFeedbackSource
    Public Property FinalTypeXpo As XPInstantFeedbackSource

    Private _IncludeDepreciated As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property IncludeDepreciated As List(Of Tuple(Of Boolean, String))
        Get
            If _IncludeDepreciated Is Nothing Then
                _IncludeDepreciated = New List(Of Tuple(Of Boolean, String))
                _IncludeDepreciated.Add(New Tuple(Of Boolean, String)(True, "Si"))
                _IncludeDepreciated.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return _IncludeDepreciated
        End Get
    End Property

    Private _TypeReport As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property TypeReport As List(Of Tuple(Of Boolean, String))
        Get
            If _TypeReport Is Nothing Then
                _TypeReport = New List(Of Tuple(Of Boolean, String))
                _TypeReport.Add(New Tuple(Of Boolean, String)(True, "Mensual"))
                _TypeReport.Add(New Tuple(Of Boolean, String)(False, "Historico"))
            End If
            Return _TypeReport
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

#Region "Eventos"

#Region "Load"

    Private Sub FrmReportHistoricalDepreciation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar SearchLookupEdit
        Me.INDSleIncludeDepreciated.Properties.DataSource = IncludeDepreciated

        'Dar un valor por defecto a los FrmReportHistoricalDepreciation_Load
        Me.INDSleIncludeDepreciated.EditValue = True

        Me.INDSleTypeReport.Properties.DataSource = TypeReport
        Me.INDSleTypeReport.EditValue = True
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        LegalBookXpo = Nothing
        InitialMainAccountXpo = Nothing
        FinalMainAccountXpo = Nothing
        InitialDepreciationAccountXpo = Nothing
        FinalDepreciationAccountXpo = Nothing
        InitialCatalogXpo = Nothing
        FinalCatalogXpo = Nothing
        InitialItemXpo = Nothing
        FinalItemXpo = Nothing
        InitialTypeXpo = Nothing
        FinalTypeXpo = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleLegalBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            LoadXpoLegalBook()
        End If
    End Sub

    Private Sub INDSleInitialMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialMainAccount.QueryPopUp
        LoadXpoInitialMainAccount()
    End Sub

    Private Sub INDSleFinalMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalMainAccount.QueryPopUp
        LoadXpoFinalMainAccount()
    End Sub

    Private Sub INDSleInitialDepreciationAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialDepreciationAccount.QueryPopUp
        LoadXpoInitialDepreciationAccount()
    End Sub

    Private Sub INDSleFinalDepreciationAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalDepreciationAccount.QueryPopUp
        LoadXpoFinalDepreciationAccount()
    End Sub

    Private Sub INDSleInitialCatalog_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialCatalog.QueryPopUp
        LoadXpoInitialCatalog()
    End Sub

    Private Sub INDSleFinalCatalog_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalCatalog.QueryPopUp
        LoadXpoFinalCatalog()
    End Sub

    Private Sub INDSleInitialItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialItem.QueryPopUp
        LoadXpoInitialItem()
    End Sub

    Private Sub INDSleFinalItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalItem.QueryPopUp
        LoadXpoFinalItem()
    End Sub

    Private Sub INDSleInitialType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialType.QueryPopUp
        LoadXpoInitialType()
    End Sub

    Private Sub INDSleFinalType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalType.QueryPopUp
        LoadXpoFinalType()
    End Sub


#End Region

#Region "EditValueChanged"

    Private Sub INDSleLegalBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleLegalBook.EditValueChanged
        INDSleInitialMainAccount.EditValue = Nothing
        INDSleInitialMainAccount.DisplayNullText = String.Empty
        InitialMainAccountXpo = Nothing

        INDSleFinalMainAccount.EditValue = Nothing
        INDSleFinalMainAccount.DisplayNullText = String.Empty
        FinalMainAccountXpo = Nothing

        INDSleInitialDepreciationAccount.EditValue = Nothing
        INDSleInitialDepreciationAccount.DisplayNullText = String.Empty
        InitialDepreciationAccountXpo = Nothing

        INDSleFinalDepreciationAccount.EditValue = Nothing
        INDSleFinalDepreciationAccount.DisplayNullText = String.Empty
        FinalDepreciationAccountXpo = Nothing
    End Sub

#End Region

#Region "Click"

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            If INDSleTypeReport.EditValue Then
                AsyncLoader(True)
                Dim reporte As New rptFixedAssetDepreciationMonth
                reporte.ParametrosReporte = New Object() {criterias, filters}
                INDDvReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()

                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnDate.Focus()
                End If
                AsyncLoader(False)
            Else
                AsyncLoader(True)
                Dim reporte As New rptFixedAssetHistoricalDepreciation
                reporte.ParametrosReporte = New Object() {criterias, filters}
                INDDvReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()

                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnDate.Focus()
                End If
                AsyncLoader(False)
            End If
        End If
    End Sub

    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDCdnDate.Focus()
    End Sub

    Private Async Sub INDSbExportReport_Click(sender As Object, e As EventArgs) Handles INDSbExportReport.Click
        If ValidateControlsReports() = True Then
            Try
                If INDSleTypeReport.EditValue Then
                    AsyncLoader(True)
                    Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetListReportDepreciationMonthAsync(criterias, filters, Me.IndigoSessionValues)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportDepreciationMonth As DataTable = ds.Tables("ReportDepreciationMonth")
                        INDGcExportExcel.DataSource = chargueDatasource(dtReportDepreciationMonth, True)
                        If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                Else
                    AsyncLoader(True)
                    Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetListReportHistoricalDepreciationAsync(criterias, filters, Me.IndigoSessionValues)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportHistoricalDepreciation As DataTable = ds.Tables("ReportHistoricalDepreciation")
                        INDGcExportExcel.DataSource = chargueDatasource(dtReportHistoricalDepreciation, False)
                        If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                End If

            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try

            AsyncLoader(True)

            If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

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

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'Valida libro contable
        If String.IsNullOrEmpty(INDSleLegalBook.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un libro contable"
            Me.INDSleLegalBook.Focus()
            Validations = False
        End If

        'Valida cuentas del activo
        If INDSleInitialMainAccount.EditValue Is Nothing And INDSleFinalMainAccount.EditValue IsNot Nothing Or INDSleFinalMainAccount.EditValue Is Nothing And INDSleInitialMainAccount.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblMainAccounts.Text)
            Me.INDSleInitialMainAccount.Focus()
            Validations = False
        ElseIf INDSleFinalMainAccount.EditValue < INDSleInitialMainAccount.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblMainAccounts.Text)
            Me.INDSleInitialMainAccount.Focus()
            Validations = False
        End If

        'Valida cuentas de depreciación
        If INDSleInitialDepreciationAccount.EditValue Is Nothing And INDSleFinalDepreciationAccount.EditValue IsNot Nothing Or INDSleFinalDepreciationAccount.EditValue Is Nothing And INDSleInitialDepreciationAccount.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblDepreciationAccounts.Text)
            Me.INDSleInitialDepreciationAccount.Focus()
            Validations = False
        ElseIf INDSleFinalDepreciationAccount.EditValue < INDSleInitialDepreciationAccount.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblDepreciationAccounts.Text)
            Me.INDSleInitialDepreciationAccount.Focus()
            Validations = False
        End If

        'Valida catalogos
        If INDSleInitialCatalog.EditValue Is Nothing And INDSleFinalCatalog.EditValue IsNot Nothing Or INDSleFinalCatalog.EditValue Is Nothing And INDSleInitialCatalog.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCatalogs.Text)
            Me.INDSleInitialCatalog.Focus()
            Validations = False
        ElseIf INDSleFinalCatalog.EditValue < INDSleInitialCatalog.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCatalogs.Text)
            Me.INDSleInitialCatalog.Focus()
            Validations = False
        End If

        'Valida artículos
        If INDSleInitialItem.EditValue Is Nothing And INDSleFinalItem.EditValue IsNot Nothing Or INDSleFinalItem.EditValue Is Nothing And INDSleInitialItem.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblItems.Text)
            Me.INDSleInitialItem.Focus()
            Validations = False
        ElseIf INDSleFinalItem.EditValue < INDSleInitialItem.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblItems.Text)
            Me.INDSleInitialItem.Focus()
            Validations = False
        End If

        'Valida tipos de equipos
        If INDSleInitialType.EditValue Is Nothing And INDSleFinalType.EditValue IsNot Nothing Or INDSleFinalType.EditValue Is Nothing And INDSleInitialType.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblTypes.Text)
            Me.INDSleInitialType.Focus()
            Validations = False
        ElseIf INDSleFinalType.EditValue < INDSleInitialType.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblTypes.Text)
            Me.INDSleInitialType.Focus()
            Validations = False
        End If

        'Valida tipos de adquisición
        If String.IsNullOrEmpty(INDCcbeAdquisitionType.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un Tipo de Adquisición"
            Me.INDCcbeAdquisitionType.Focus()
            Validations = False
        End If

        If Validations Then
            criterias = New Dictionary(Of String, String)
            criterias.Add("OperatingUnitId", Me.BarraBotones.OperatingUnit.Id)
            criterias.Add("Year", INDCdnDate.GetYear)
            criterias.Add("Month", INDCdnDate.GetMonth)
            criterias.Add("LegalBookId", INDSleLegalBook.EditValue)
            criterias.Add("LegalBookName", INDSleLegalBook.TextEditValue)
            criterias.Add("IncludeDepreciated", INDSleIncludeDepreciated.EditValue)
            criterias.Add("AdquisitionType", INDCcbeAdquisitionType.EditValue)

            filters = New Dictionary(Of String, String)
            filters.Add("InitialMainAccount", INDSleInitialMainAccount.EditValue)
            filters.Add("FinalMainAccount", INDSleFinalMainAccount.EditValue)
            filters.Add("InitialDepreciationAccount", INDSleInitialDepreciationAccount.EditValue)
            filters.Add("FinalDepreciationAccount", INDSleFinalDepreciationAccount.EditValue)
            filters.Add("InitialCatalog", INDSleInitialCatalog.EditValue)
            filters.Add("FinalCatalog", INDSleFinalCatalog.EditValue)
            filters.Add("InitialItem", INDSleInitialItem.EditValue)
            filters.Add("FinalItem", INDSleFinalItem.EditValue)
            filters.Add("InitialType", INDSleInitialType.EditValue)
            filters.Add("FinalType", INDSleFinalType.EditValue)
        End If

        Return Validations
    End Function

#Region "LoadXpo"

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleLegalBook
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoLegalBook()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True}
            LegalBookXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBookByStatusXpCollection, filter)
            INDSleLegalBook.Datasource = LegalBookXpo
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialMainAccount
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialMainAccount()
        If InitialMainAccountXpo Is Nothing Then
            Using msearch As New MBusqueda
                If Not String.IsNullOrEmpty(INDSleLegalBook.EditValue) Then
                    Dim filter() As Object = {1, INDSleLegalBook.EditValue}
                    InitialMainAccountXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetMainAccountsByTypeAndByBook, filter)
                End If
            End Using
        End If

        INDSleInitialMainAccount.Datasource = InitialMainAccountXpo
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalMainAccount
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalMainAccount()
        If FinalMainAccountXpo Is Nothing Then
            Using msearch As New MBusqueda
                If Not String.IsNullOrEmpty(INDSleLegalBook.EditValue) Then
                    Dim filter() As Object = {1, INDSleLegalBook.EditValue}
                    FinalMainAccountXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetMainAccountsByTypeAndByBook, filter)
                End If
            End Using
        End If

        INDSleFinalMainAccount.Datasource = FinalMainAccountXpo
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialDepreciationAccount
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialDepreciationAccount()
        If InitialDepreciationAccountXpo Is Nothing Then
            Using msearch As New MBusqueda
                If Not String.IsNullOrEmpty(INDSleLegalBook.EditValue) Then
                    Dim filter() As Object = {1, INDSleLegalBook.EditValue}
                    InitialDepreciationAccountXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetMainAccountsByTypeAndByBook, filter)
                End If
            End Using
        End If

        INDSleInitialDepreciationAccount.Datasource = InitialDepreciationAccountXpo
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalDepreciationAccount
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalDepreciationAccount()
        If FinalDepreciationAccountXpo Is Nothing Then
            Using msearch As New MBusqueda
                If Not String.IsNullOrEmpty(INDSleLegalBook.EditValue) Then
                    Dim filter() As Object = {1, INDSleLegalBook.EditValue}
                    FinalDepreciationAccountXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetMainAccountsByTypeAndByBook, filter)
                End If
            End Using
        End If

        INDSleFinalDepreciationAccount.Datasource = FinalDepreciationAccountXpo
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialCatalog
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialCatalog()
        If InitialCatalogXpo Is Nothing Then
            Using msearch As New MBusqueda
                InitialCatalogXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentCatalog)
            End Using
        End If

        INDSleInitialCatalog.Datasource = InitialCatalogXpo
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalCatalog
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalCatalog()
        If FinalCatalogXpo Is Nothing Then
            Using msearch As New MBusqueda
                FinalCatalogXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentCatalog)
            End Using
        End If

        INDSleFinalCatalog.Datasource = FinalCatalogXpo
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialItem
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialItem()
        If InitialItemXpo Is Nothing Then
            Using msearch As New MBusqueda
                InitialItemXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            End Using
        End If

        INDSleInitialItem.Datasource = InitialItemXpo
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalItem
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalItem()
        If FinalItemXpo Is Nothing Then
            Using msearch As New MBusqueda
                FinalItemXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            End Using
        End If

        INDSleFinalItem.Datasource = FinalItemXpo
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialType
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialType()
        If InitialTypeXpo Is Nothing Then
            Using msearch As New MBusqueda
                InitialTypeXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentType)
            End Using
        End If

        INDSleInitialType.Datasource = InitialTypeXpo
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalType
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalType()
        If FinalTypeXpo Is Nothing Then
            Using msearch As New MBusqueda
                FinalTypeXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentType)
            End Using
        End If

        INDSleFinalType.Datasource = FinalTypeXpo
    End Sub

#End Region

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource(dtReportHistoricalDepreciation As DataTable, flagType As Boolean) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Libro")
        dt.Columns.Add("Año")
        dt.Columns.Add("Mes")

        dt.Columns.Add("Placa")
        dt.Columns.Add("Serie")
        dt.Columns.Add("Modelo")
        dt.Columns.Add("Fecha Adquisición", GetType(DateTime))
        dt.Columns.Add("Tipo de Adquisición")
        dt.Columns.Add("Estado")

        dt.Columns.Add("Catálogo")
        dt.Columns.Add("Artículo")
        dt.Columns.Add("Tipo de Equipo")
        dt.Columns.Add("Responsable")
        dt.Columns.Add("Localización")

        dt.Columns.Add("Cuenta del Activo")
        dt.Columns.Add("Cuenta de la Depreciación")

        dt.Columns.Add("Valor Histórico", GetType(Decimal))
        If flagType = False Then
            dt.Columns.Add("Valor Valorización", GetType(Decimal))
            dt.Columns.Add("Valor Devaluación", GetType(Decimal))
            dt.Columns.Add("Depreciación Acumulada", GetType(Decimal))
        End If
        dt.Columns.Add("Valor Depreciación Mes", GetType(Decimal))
        If flagType = False Then
            dt.Columns.Add("Vida Util en Días", GetType(Integer))
            dt.Columns.Add("Días Ajustados", GetType(Integer))
            dt.Columns.Add("Días Depreciados", GetType(Integer))
        End If
        dt.Columns.Add("Días Depreaciado Mes", GetType(Integer))
        dt.Columns.Add("Valor Residual", GetType(Decimal))
        dt.Columns.Add("Observación")

        For Each item In dtReportHistoricalDepreciation.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Libro") = INDSleLegalBook.TextEditValue
            row.Item("Año") = INDCdnDate.GetYear
            row.Item("Mes") = INDCdnDate.GetMonth

            row.Item("Placa") = item("Plate")
            row.Item("Serie") = item("Serie")
            row.Item("Modelo") = item("Model")
            row.Item("Fecha Adquisición") = CDate(item("AdquisitionDate")).AsDate
            row.Item("Tipo de Adquisición") = item("AdquisitionTypeName")
            row.Item("Estado") = item("StatusName")

            row.Item("Catálogo") = String.Format("{0} - {1}", item("ItemCatalogCode"), item("ItemCatalogDescription"))
            row.Item("Artículo") = String.Format("{0} - {1}", item("ItemCode"), item("ItemDescription"))
            row.Item("Tipo de Equipo") = String.Format("{0} - {1}", item("ItemTypeCode"), item("ItemTypeDescription"))
            row.Item("Responsable") = String.Format("{0} - {1}", item("ResponsibleCode"), item("ResponsibleName"))
            row.Item("Localización") = String.Format("{0} - {1}", item("LocationCode"), item("LocationName"))

            row.Item("Cuenta del Activo") = String.Format("{0} - {1}", item("MainAccounNumber"), item("MainAccountName"))
            row.Item("Cuenta de la Depreciación") = String.Format("{0} - {1}", item("DepreciationAccounNumber"), item("DepreciationAccountName"))

            row.Item("Valor Histórico") = item("HistoricalValue")
            If flagType = False Then
                row.Item("Valor Valorización") = item("ValorizationValue")
                row.Item("Valor Devaluación") = item("DevaluationValue")
                row.Item("Depreciación Acumulada") = item("AccumulatedDepreciation")
            End If

            row.Item("Valor Depreciación Mes") = item("DepreciateValue")
            If flagType = False Then
                row.Item("Vida Util en Días") = item("LifeTimeInDays")
                row.Item("Días Ajustados") = item("AdjustedDays")
                row.Item("Días Depreciados") = item("DepreciatedDays")
            End If

            row.Item("Días Depreaciado Mes") = item("DepreciateDays")
            row.Item("Valor Residual") = item("ResidualValue")
            row.Item("Observación") = item("Observation")
            dt.Rows.Add(row)
        Next

        If dt IsNot Nothing Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
        End If
    End Function

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

End Class