#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportGeneralBalance

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    ''' <summary>
    ''' Variable que almacena la fecha actualizada
    ''' </summary>
    Dim dateNow As Date
#End Region

#Region "Properties"


    ''' <summary>
    ''' Propiedad para acceder a la última fecha de cierre
    ''' </summary>
    ''' <returns></returns>
    Public Property INDLastClosingDate As Date

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para libros contables
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    ''' Almacena el origen de datos para centros de costo
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoCostCenter As XPInstantFeedbackSource

    ''' <summary>
    ''' Lista de tuplas tipo de reporte
    ''' </summary>
    Private _FillingReportType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    '''  Permite acceder a una lista de tipos de reportes predefinidos (Bancaria Mensual, Mensual, Comparativo) 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Integer, String))
                _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Bancaria Mensual"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Mensual"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(3, "Comparativo"))
            End If
            Return _FillingReportType
        End Get
    End Property

    ''' <summary>
    ''' Lista de tuplas de niveles de cuenta
    ''' </summary>
    Private _FillingNivel As List(Of Tuple(Of Integer, String))

    ''' <summary>
    '''  Permite acceder a una lista de niveles de cuenta predefinidos (Grupo, Cuenta, Subcuenta, Auxiliar) 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingNivel As List(Of Tuple(Of Integer, String))
        Get
            If _FillingNivel Is Nothing Then
                _FillingNivel = New List(Of Tuple(Of Integer, String))
                _FillingNivel.Add(New Tuple(Of Integer, String)(2, "Grupo"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(3, "Cuenta"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(4, "Subcuenta"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(5, "Auxiliar"))
            End If
            Return _FillingNivel
        End Get
    End Property

    ''' <summary>
    ''' Almacena pares de clave-valor, donde cada uno será de tipo String respectivamente
    ''' </summary>
    Private criterias As Dictionary(Of String, String)

#End Region

#Region "BarraBotones"

    ''' <summary>
    ''' Se cargan los permisos que tiene el frontal en la barra
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
    '''  Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportGeneralBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit
        Me.INDGleReportType.Properties.DataSource = Me.FillingReportType
        Me.INDGleLevelAccount.Properties.DataSource = Me.FillingNivel

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleReportType.EditValue = 2
        Me.INDGleLevelAccount.EditValue = 3
        Me.INDsleDetailingThird.EditValue = False
        Me.INDSleMemorandumAccounts.EditValue = False
        Me.INDSleNumberAccounts.EditValue = True
        Me.INDSleAccountsZero.EditValue = False

        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleCostCenterStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCostCenterAsync
        Me.INDSleCostCenterEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCostCenterAsync

        'Se establece las propiedades de los searchloockupedit
        INDSleCostCenterStart.View.OptionsView.ShowGroupPanel = False
        INDSleCostCenterEnd.View.OptionsView.ShowGroupPanel = False

        'Se obtiene el libro contable por defecto
        LoadXpoBook()
        SetOfficialBook()
        dateNow = Me.GetDateServer()
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando el formulario es eliminado y sus recursos deben ser liberados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
        dateNow = Nothing
        INDLastClosingDate = Nothing
        bookXpcollection = Nothing
        ProoftCloseXpoCostCenter = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportGeneralBalance_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using msearch As New MBusqueda
            If msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate) = Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateExistencePeriodClosingReport", "Commons"))
                INDLastClosingDate = Date.Now
            Else
                INDLastClosingDate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate)
                If Month(INDLastClosingDate) = 12 Then
                    INDLastClosingDate = DateAdd(DateInterval.Month, 1, INDLastClosingDate)
                End If
            End If
        End Using
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCostCenterStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCostCenterStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenterStart.QueryPopUp
        If INDSleCostCenterStart.Datasource Is Nothing Then
            LoadXpoCostCenterStart()
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
            LoadXpoCostCenterEnd()
        End If
    End Sub

#End Region

#Region "ValueChanged"

    ''' <summary>
    ''' se ejecuta en el evento onchangedate del control INDCdnDateStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCdnDateStart_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateStart.OnChangeDate
        'cuando el usuario cambie la fecha final si la fecha final es menor que la fecha inicial hacer la fecha final igual que la fecha inicial
        If INDCdnDateStart.GetYear <= Year(INDLastClosingDate) Then
            INDCdnDateStart.SetYear = Year(INDLastClosingDate)
            If INDCdnDateStart.GetMonth < Month(INDLastClosingDate) Then
                INDCdnDateStart.SetMonth = Month(INDLastClosingDate)
            End If
        Else
            'Permitir seleccionar cualquier fecha hasta la fecha actual
            If INDCdnDateStart.GetYear > dateNow.Year Then
                INDCdnDateStart.SetYear = dateNow.Year
            End If
            If INDCdnDateStart.GetYear = dateNow.Year AndAlso INDCdnDateStart.GetMonth > dateNow.Month Then
                INDCdnDateStart.SetMonth = dateNow.Month
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento onchangedate del control INDCdnDateEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCdnDateEnd_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateEnd.OnChangeDate
        'cuando el usuario cambie la fecha final si la fecha final es menor que la fecha inicial hacer la fecha final igual que la fecha inicial
        If INDCdnDateEnd.GetYear <= Year(INDLastClosingDate) Then
            INDCdnDateEnd.SetYear = Year(INDLastClosingDate)
            If INDCdnDateEnd.GetMonth < Month(INDLastClosingDate) Then
                INDCdnDateEnd.SetMonth = Month(INDLastClosingDate)
            End If
        Else
            'Permitir seleccionar cualquier fecha hasta la fecha actual
            If INDCdnDateEnd.GetYear > dateNow.Year Then
                INDCdnDateEnd.SetYear = dateNow.Year
            End If
            If INDCdnDateEnd.GetYear = dateNow.Year AndAlso INDCdnDateEnd.GetMonth > dateNow.Month Then
                INDCdnDateEnd.SetMonth = dateNow.Month
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento EditValueChanged del control INDslePeriod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReportType.EditValueChanged
        INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDCdnDateStart.SetMonth = Month(INDLastClosingDate)
        INDCdnDateStart.SetYear = Year(INDLastClosingDate)
        INDCdnDateStart.Enabled = False

        INDSleCostCenterStart.EditValue = Nothing
        INDSleCostCenterStart.DisplayNullText = String.Empty
        INDSleCostCenterEnd.EditValue = Nothing
        INDSleCostCenterEnd.DisplayNullText = String.Empty

        If INDGleReportType.EditValue = 1 Then
            INDsleDetailingThird.EditValue = False
            INDLciDetailingThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleMemorandumAccounts.EditValue = False
            INDLciMemorandumAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleNumberAccounts.EditValue = True
            INDLciNumberAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleAccountsZero.EditValue = False
            INDLciAccountsZero.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciLabelCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCostCenterStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCostCenterEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDGleReportType.EditValue = 2 Then
            INDLciDetailingThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciMemorandumAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciNumberAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAccountsZero.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciLabelCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciCostCenterStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciCostCenterEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf INDGleReportType.EditValue = 3 Then
            INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDCdnDateStart.Enabled = True

            INDsleDetailingThird.EditValue = False
            INDLciDetailingThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleMemorandumAccounts.EditValue = False
            INDLciMemorandumAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleNumberAccounts.EditValue = True
            INDLciNumberAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSleAccountsZero.EditValue = False
            INDLciAccountsZero.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciLabelCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCostCenterStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCostCenterEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
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
                Dim reporte As Object = Nothing
                Dim hasData As Boolean = False

                AsyncLoader(True)
                If INDGleReportType.EditValue = 1 Then
                    reporte = New rptGeneralBalance() With {.ParametrosReporte = New Object() {criterias}}
                    'se envia la naturaleza de la clase de la cuenta  1 = Debito
                    reporte.XrSubreport1.ReportSource.DataSource = Await reporte.FillListGeneralLedgerBalance(1)
                    reporte.XrSubreport1.ReportSource.DataMember = "ReportGeneralBalance"
                    'se envia la naturaleza de la clase de la cuenta  2 = Credito
                    reporte.XrSubreport2.ReportSource.DataSource = Await reporte.FillListGeneralLedgerBalance(2)
                    reporte.XrSubreport2.ReportSource.DataMember = "ReportGeneralBalance"
                    hasData = reporte.XrSubreport1.ReportSource.DataSource IsNot Nothing OrElse reporte.XrSubreport2.ReportSource.DataSource IsNot Nothing
                ElseIf INDGleReportType.EditValue = 2 Then
                    reporte = New rptGeneralBalanceStandard() With {.ParametrosReporte = New Object() {criterias}}
                    Await reporte.CargarDataSourceAsync()
                    hasData = reporte.DataSource IsNot Nothing
                ElseIf INDGleReportType.EditValue = 3 Then
                    reporte = New rptGeneralBalanceComparative() With {.ParametrosReporte = New Object() {criterias}}
                    Await reporte.CargarDataSourceAsync()
                    hasData = reporte.DataSource IsNot Nothing
                End If
                AsyncLoader(False)

                If hasData Then
                    INDDvViewReport.DocumentSource = reporte
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDGleReportType.Focus()
                End If
            Catch ex As Exception
                AsyncLoader(True)
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento clickback del control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
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
    ''' metodo para Cargar el data source Del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBook()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True}
            bookXpcollection = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllBookByStatusXpCollection, filter)
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
    ''' metodo para Cargar el data source Del Control INDSleCostCenterStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCostCenterStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCostCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostcenterReport)
            INDSleCostCenterStart.Datasource = ProoftCloseXpoCostCenter
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCostCenterEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCostCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostcenterReport)
            INDSleCostCenterEnd.Datasource = ProoftCloseXpoCostCenter
        End Using
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        Dim DateStart As Date = "01/" & INDCdnDateStart.GetMonth & "/" & INDCdnDateStart.GetYear
        Dim DateEnd As Date = "01/" & INDCdnDateEnd.GetMonth & "/" & INDCdnDateEnd.GetYear
        If DateStart > DateEnd Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDCdnDateStart.Focus()
            Validations = False
        End If
        'validaciones controles de centro de costo
        If INDSleCostCenterStart.EditValue IsNot Nothing And INDSleCostCenterEnd.EditValue Is Nothing Or INDSleCostCenterStart.EditValue Is Nothing And INDSleCostCenterEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCostCenter.Text)
            Me.INDSleCostCenterStart.Focus()
            Validations = False
        ElseIf INDSleCostCenterStart.EditValue > INDSleCostCenterEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCostCenter.Text)
            Me.INDSleCostCenterStart.Focus()
            Validations = False
        End If

        If Validations Then
            criterias = New Dictionary(Of String, String)
            criterias.Add("ReportType", INDGleReportType.EditValue)
            criterias.Add("YearInitial", INDCdnDateStart.GetYear)
            criterias.Add("MonthInitial", INDCdnDateStart.GetMonth)
            criterias.Add("YearFinal", INDCdnDateEnd.GetYear)
            criterias.Add("MonthFinal", INDCdnDateEnd.GetMonth)
            criterias.Add("LegalBookId", INDsleBook.EditValue)
            criterias.Add("LegalBookName", INDsleBook.Text)
            criterias.Add("AccountLevel", INDGleLevelAccount.EditValue)
            criterias.Add("DetailingThird", INDsleDetailingThird.EditValue)
            criterias.Add("MemorandumAccounts", INDSleMemorandumAccounts.EditValue)
            criterias.Add("NumberAccounts", INDSleNumberAccounts.EditValue)
            criterias.Add("AccountsZero", INDSleAccountsZero.EditValue)
            criterias.Add("CostCenterInitial", INDSleCostCenterStart.EditValue)
            criterias.Add("CostCenterFinal", INDSleCostCenterEnd.EditValue)
            criterias.Add("Title", INDTxtTitle.EditValue)
        End If

        Return Validations
    End Function

#End Region

End Class