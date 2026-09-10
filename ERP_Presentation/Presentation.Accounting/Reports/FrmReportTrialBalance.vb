#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraPrinting
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportTrialBalance

#Region "Variables"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    ''' <summary>
    ''' Diccionario que almacena diferentes criterios 
    ''' </summary>
    Private _criterias As Dictionary(Of String, String)

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para libros contables
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    ''' Permite acceder a una lista de niveles de cuenta predefinidos (Clase, Grupo, Cuenta, Subcuenta, Auxiliar) 
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
            End If
            Return _FillingNivel
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla de datos de Si y No
    ''' </summary>
    Private _FillingYesOrNot As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property FillingYesOrNot As List(Of Tuple(Of Boolean, String))
        Get
            If _FillingYesOrNot Is Nothing Then
                _FillingYesOrNot = New List(Of Tuple(Of Boolean, String))
                _FillingYesOrNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
                _FillingYesOrNot.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return _FillingYesOrNot
        End Get
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
        Dim errors As New StringBuilder

        Dim DateStart As Date = "01/" & INDCdnDateStart.GetMonth & "/" & INDCdnDateStart.GetYear
        Dim DateEnd As Date = "01/" & INDCdnDateEnd.GetMonth & "/" & INDCdnDateEnd.GetYear
        If DateStart > DateEnd Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDCdnDateStart.Focus()
        End If

        'validaciones controles de cuenta
        If INDSleAccountStart.EditValue IsNot Nothing And INDSleAccountEnd.EditValue Is Nothing Or INDSleAccountStart.EditValue Is Nothing And INDSleAccountEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccount.Text)
            Me.INDSleAccountStart.Focus()
        ElseIf INDSleAccountStart.EditValue > INDSleAccountEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccount.Text)
            Me.INDSleAccountStart.Focus()
        End If

        'validaciones controles de terceros
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue Is Nothing Or INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
        ElseIf INDSleThirdPartyStart.EditValue > INDSleThirdPartyEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
        End If

        'validaciones controles de centro de costo
        If INDSleCostCenterStart.EditValue IsNot Nothing And INDSleCostCenterEnd.EditValue Is Nothing Or INDSleCostCenterStart.EditValue Is Nothing And INDSleCostCenterEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCostCenter.Text)
            Me.INDSleCostCenterStart.Focus()
        ElseIf INDSleCostCenterStart.EditValue > INDSleCostCenterEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCostCenter.Text)
            Me.INDSleCostCenterStart.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        _criterias = New Dictionary(Of String, String)
        _criterias.Add("Year", INDCdnDateStart.GetYear)
        _criterias.Add("MonthStart", INDCdnDateStart.GetMonth)
        _criterias.Add("MonthEnd", INDCdnDateEnd.GetMonth)

        _criterias.Add("LevelAccount", INDGleLevelAccount.EditValue)
        _criterias.Add("DetailingThird", INDsleDetailingThird.EditValue)
        _criterias.Add("DetailingCostCenter", INDSleDetailingCostCenter.EditValue)
        _criterias.Add("AccountsZero", INDSleAccountsZero.EditValue)
        _criterias.Add("AccountsAffectedTotal", INDSleAccountsAffectedTotal.EditValue)
        _criterias.Add("IncludeClosed", INDGleIncludeClosed.EditValue)

        _criterias.Add("LegalBookId", INDsleBook.EditValue)
        _criterias.Add("LegalBookName", INDsleBook.Text)
        _criterias.Add("AccountStart", INDSleAccountStart.EditValue)
        _criterias.Add("AccountEnd", INDSleAccountEnd.EditValue)
        _criterias.Add("ThirdPartyStart", INDSleThirdPartyStart.EditValue)
        _criterias.Add("ThirdPartyEnd", INDSleThirdPartyEnd.EditValue)
        _criterias.Add("CostCenterStart", INDSleCostCenterStart.EditValue)
        _criterias.Add("CostCenterEnd", INDSleCostCenterEnd.EditValue)

        Return True
    End Function

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

#Region "ToExcel"

    ''' <summary>
    ''' Carga los datos desde el Datatable para mostrarlos en cuadrícula 
    ''' </summary>
    ''' <param name="dtReportAuxiliar"></param>
    Private Sub chargueDatasource(dtReportAuxiliar As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Libro")
        dt.Columns.Add("Clase")
        dt.Columns.Add("Nivel")
        dt.Columns.Add("Código Cuenta Contable")
        dt.Columns.Add("Cuenta Contable")
        If INDsleDetailingThird.EditValue Then
            dt.Columns.Add("NIT Tercero")
            dt.Columns.Add("Tercero")
        End If
        If INDSleDetailingCostCenter.EditValue Then
            dt.Columns.Add("Código Centro de Costo")
            dt.Columns.Add("Centro de Costo")
        End If
        dt.Columns.Add("Moneda")
        dt.Columns.Add("Naturaleza Saldo Anterior")
        dt.Columns.Add("Saldo Anterior", GetType(Decimal))
        dt.Columns.Add("Valor Débito", GetType(Decimal))
        dt.Columns.Add("Valor Crédito", GetType(Decimal))
        dt.Columns.Add("Naturaleza Nuevo Saldo")
        dt.Columns.Add("Nuevo Saldo", GetType(Decimal))

        For Each item As DataRow In dtReportAuxiliar.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Libro") = INDsleBook.Text
            row.Item("Clase") = item("mainAccountClassName")
            row.Item("Nivel") = item("mainAccountLevel")
            row.Item("Código Cuenta Contable") = item("mainAccountCode")
            row.Item("Cuenta Contable") = item("mainAccountName")
            If INDsleDetailingThird.EditValue Then
                row.Item("NIT Tercero") = item("thirdPartyNit")
                row.Item("Tercero") = item("thirdPartyName")
            End If
            If INDSleDetailingCostCenter.EditValue Then
                row.Item("Código Centro de Costo") = item("costCenterCode")
                row.Item("Centro de Costo") = item("costCenterName")
            End If
            row.Item("Moneda") = item("LegalBookCurrency") + "(" + item("LegalBookCurrencyAbbreviation") + ")"
            row.Item("Naturaleza Saldo Anterior") = item("naturePreviousBalance")
            row.Item("Saldo Anterior") = item("previousBalance")
            row.Item("Valor Débito") = item("valueDebitMovement")
            row.Item("Valor Crédito") = item("valueCreditMovement")
            row.Item("Naturaleza Nuevo Saldo") = item("natureNewBalance")
            row.Item("Nuevo Saldo") = item("newBalance")
            dt.Rows.Add(row)
        Next

        INDGcGenerateExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = INDGcGenerateExcel
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcGenerateExcel.DataSource = Nothing
        Me.INDGcGenerateExcel.RefreshDataSource()
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' se inicializan los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportTrialBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleCostCenterStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCostCenterAsync
        Me.INDSleCostCenterEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCostCenterAsync

        'Cargar los libros contables
        LoadXpoBook()

        'obtener fecha actual
        Dim dateNow = Me.GetDateServer()

        'Cargar GridLookUpEdit
        Me.INDGleLevelAccount.Properties.DataSource = Me.FillingNivel
        Me.INDGleIncludeClosed.Properties.DataSource = Me.FillingYesOrNot

        'Dar un valor por defecto
        Me.INDGleLevelAccount.EditValue = 5
        Me.INDsleDetailingThird.EditValue = False
        Me.INDSleDetailingCostCenter.EditValue = False
        Me.INDSleAccountsZero.EditValue = False
        Me.INDSleAccountsAffectedTotal.EditValue = True
        Me.INDGleIncludeClosed.EditValue = True
        INDCdnDateStart.SetMonth = Month(dateNow)
        INDCdnDateStart.SetYear = Year(dateNow)
        INDCdnDateEnd.SetMonth = Month(dateNow)
        INDCdnDateEnd.SetYear = Year(dateNow)
        SetOfficialBook()
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando el formulario es eliminado y sus recursos deben ser liberados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing

        _criterias = Nothing
        _FillingNivel = Nothing
        _FillingYesOrNot = Nothing
        bookXpcollection = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleAccountStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountStart.QueryPopUp
        If INDSleAccountStart.Datasource Is Nothing Then
            Using msearch As New MBusqueda
                Dim filter() As Object = {True, INDsleBook.EditValue}
                INDSleAccountStart.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookId, filter)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleAccountEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountEnd.QueryPopUp
        If INDSleAccountEnd.Datasource Is Nothing Then
            Using msearch As New MBusqueda
                Dim filter() As Object = {True, INDsleBook.EditValue}
                INDSleAccountEnd.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookId, filter)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            Using msearch As New MBusqueda
                INDSleThirdPartyStart.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            Using msearch As New MBusqueda
                INDSleThirdPartyEnd.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCostCenterStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCostCenterStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenterStart.QueryPopUp
        If INDSleCostCenterStart.Datasource Is Nothing Then
            Using msearch As New MBusqueda
                INDSleCostCenterStart.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostcenterReport)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCostCenterEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCostCenterEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenterEnd.QueryPopUp
        If INDSleCostCenterEnd.Datasource Is Nothing Then
            Using msearch As New MBusqueda
                INDSleCostCenterEnd.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostcenterReport)
            End Using
        End If
    End Sub

#End Region

#Region "OnChangeDate"

    ''' <summary>
    ''' se ejecuta en el evento OnChangeDate del control INDCdnDateStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCdnDateStart_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateStart.OnChangeDate
        'cuando el usuario cambie la fecha inicial si la fecha final es menor que la fecha inicial hacer la fecha final igual que la fecha inicial
        If INDCdnDateEnd.GetYear < INDCdnDateStart.GetYear Then
            INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            If INDCdnDateEnd.GetMonth < INDCdnDateStart.GetMonth Then
                INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
            End If
        End If

        'el reporte solo se puede sacar de 1 año fiscal
        If INDCdnDateStart.GetYear <> INDCdnDateEnd.GetYear Then
            INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            INDCdnDateEnd.SetMonth = 12
        End If

        INDLciIncludeClosed.HideControl(Not (INDCdnDateEnd.GetMonth = 12))
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento OnChangeDate en el control INDCdnDateEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCdnDateEnd_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateEnd.OnChangeDate
        'cuando el usuario cambie la fecha final si la fecha final es menor que la fecha inicial hacer la fecha final igual que la fecha inicial
        If INDCdnDateEnd.GetYear < INDCdnDateStart.GetYear Then
            INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            If INDCdnDateEnd.GetMonth < INDCdnDateStart.GetMonth Then
                INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
            End If
        End If

        'el reporte solo se puede sacar de 1 año fiscal
        If INDCdnDateEnd.GetYear <> INDCdnDateStart.GetYear Then
            INDCdnDateStart.SetYear = INDCdnDateEnd.GetYear
            INDCdnDateStart.SetMonth = INDCdnDateEnd.GetMonth
        End If

        INDLciIncludeClosed.HideControl(Not (INDCdnDateEnd.GetMonth = 12))
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)

                Dim reporte As New rptTrialBalance()
                reporte.ParametrosReporte = New Object() {_criterias}
                Await reporte.CargarDataSourceAsync()
                INDDvViewReport.DocumentSource = reporte
                AsyncLoader(False)

                If reporte.DataSource IsNot Nothing Then
                    ' Specify export options.
                    Dim xlsxExportOptions As XlsxExportOptions = reporte.ExportOptions.Xlsx()
                    xlsxExportOptions.TextExportMode = TextExportMode.Text
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnDateStart.Focus()
                End If
            Catch ex As Exception
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
    End Sub

    ''' <summary>
    ''' Exporta datos a Excel basados en los filtros y criterios seleccionados en el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerateExcel_Click(sender As Object, e As EventArgs) Handles INDSbExportExcel.Click
        If ValidateControlsReports() Then
            Try
                AsyncLoader(True)
                Dim ds = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetReportBalancesAsync(_criterias, Me.indigo)
                If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dim dtReportBalances As DataTable = ds.Tables("ReportBalances")
                    chargueDatasource(dtReportBalances)
                    If Me.INDGcGenerateExcel.DataSource IsNot Nothing Then
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

#End Region

#End Region

End Class