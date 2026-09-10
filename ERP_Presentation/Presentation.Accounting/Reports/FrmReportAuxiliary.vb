#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports Infrastructure.Data.Xpo
Imports Presentation.Common.MVP
Imports Presentation.CloudAgent

#End Region
Public Class FrmReportAuxiliary

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

    ''' <summary>
    ''' Instancia de la clase XPInstantFeedbackSource
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoAccount As XPInstantFeedbackSource

    ''' <summary>
    ''' Instancia de la clase XPInstantFeedbackSource
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource

    ''' <summary>
    ''' Instancia de la clase XPInstantFeedbackSource
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoCostCenter As XPInstantFeedbackSource

    ''' <summary>
    ''' Instancia de la clase XPCollection
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    ''' Lista de tuplas
    ''' </summary>
    Private _FillingCriteria As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Devuelve una lista de tuplas, donde cada tupla contiene un valor numérico y una cadena de texto asociada
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingCriteria As List(Of Tuple(Of Integer, String))
        Get
            If _FillingCriteria Is Nothing Then
                _FillingCriteria = New List(Of Tuple(Of Integer, String))
                _FillingCriteria.Add(New Tuple(Of Integer, String)(1, "Cuenta"))
                _FillingCriteria.Add(New Tuple(Of Integer, String)(2, "Tercero"))
                _FillingCriteria.Add(New Tuple(Of Integer, String)(3, "Centro"))
            End If
            Return _FillingCriteria
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una lista de tuplas, donde cada tupla contiene un valor numérico y una cadena de texto asociada
    ''' </summary>
    Private _FillingSort As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingSort As List(Of Tuple(Of Integer, String))
        Get
            If _FillingSort Is Nothing Then
                _FillingSort = New List(Of Tuple(Of Integer, String))
                _FillingSort.Add(New Tuple(Of Integer, String)(1, "Consecutivo"))
                _FillingSort.Add(New Tuple(Of Integer, String)(2, "Fecha"))
            End If
            Return _FillingSort
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una lista de tuplas, donde cada tupla contiene un valor numérico y una cadena de texto asociada
    ''' </summary>
    Private _FillingTypeVoucher As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeVoucher As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeVoucher Is Nothing Then
                _FillingTypeVoucher = New List(Of Tuple(Of Integer, String))
                _FillingTypeVoucher.Add(New Tuple(Of Integer, String)(1, "Registrados"))
                _FillingTypeVoucher.Add(New Tuple(Of Integer, String)(2, "Confirmados"))
                _FillingTypeVoucher.Add(New Tuple(Of Integer, String)(3, "Anulados"))
                _FillingTypeVoucher.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingTypeVoucher
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una lista de tuplas, donde cada tupla contiene un valor numérico y una cadena de texto asociada
    ''' </summary>
    Private _FillingAccumulatedBalance As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingAccumulatedBalance As List(Of Tuple(Of Integer, String))
        Get
            If _FillingAccumulatedBalance Is Nothing Then
                _FillingAccumulatedBalance = New List(Of Tuple(Of Integer, String))
                _FillingAccumulatedBalance.Add(New Tuple(Of Integer, String)(False, "No"))
                _FillingAccumulatedBalance.Add(New Tuple(Of Integer, String)(True, "Si"))
            End If
            Return _FillingAccumulatedBalance
        End Get
    End Property

    ''' <summary>
    '''  Determina si se debe realizar un "detalle de cuenta" en función de la selección del usuario en los controles INDsleGeneralReport, INDGleCriteria1 y INDGleCriteria2
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property DetallingAccount As Boolean
        Get
            If Not INDsleGeneralReport.EditValue OrElse INDGleCriteria1.EditValue = 1 OrElse INDGleCriteria2.EditValue = 1 Then
                Return True
            End If

            Return False
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad indica si se debe realizar un "detalle de tercero" en función a condiciones dadas por los controles INDsleGeneralReport, INDGleCriteria1, INDGleCriteria2.
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property DetallingThirdParty As Boolean
        Get
            If Not INDsleGeneralReport.EditValue OrElse INDGleCriteria1.EditValue = 2 OrElse INDGleCriteria2.EditValue = 2 Then
                Return True
            End If

            Return False
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad indica si se debe realizar un "detalle de centro de costos" en función a condiciones dadas por los controles.
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property DetallingCostCenter As Boolean
        Get
            If Not INDsleGeneralReport.EditValue OrElse INDGleCriteria1.EditValue = 3 OrElse INDGleCriteria2.EditValue = 3 Then
                Return True
            End If

            Return False
        End Get
    End Property

#End Region

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
    ''' Método para cargar el DataSource Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountStart()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, INDsleBook.EditValue}
            ProoftCloseXpoAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookId, filter)
            'ProoftCloseXpoAccount.Sorting.Add(New SortProperty("Number", DB.SortingDirection.Ascending))
            INDSleAccountStart.Datasource = ProoftCloseXpoAccount
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountEnd()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, INDsleBook.EditValue}
            ProoftCloseXpoAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookId, filter)
            'ProoftCloseXpoAccount.Sorting.Add(New SortProperty("Number", DB.SortingDirection.Ascending))
            INDSleAccountEnd.Datasource = ProoftCloseXpoAccount
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        'validaciones controles de fecha
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateStart.Focus()
            Validations = False
        End If
        'validaciones controles de cuenta
        If INDSleAccountStart.EditValue IsNot Nothing And INDSleAccountEnd.EditValue Is Nothing Or INDSleAccountStart.EditValue Is Nothing And INDSleAccountEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccount.Text)
            Me.INDSleAccountStart.Focus()
            Validations = False
        ElseIf INDSleAccountStart.EditValue > INDSleAccountEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccount.Text)
            Me.INDSleAccountStart.Focus()
            Validations = False
        End If

        'validaciones controles de terceros
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue Is Nothing Or INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyStart.EditValue > INDSleThirdPartyEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
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

        Return Validations
    End Function

    ''' <summary>
    ''' propiedad para Cargar los valores del segundo criteria
    ''' </summary>
    ''' ''' <param name="ValueCriteria1"></param>
    ''' <remarks></remarks>
    Public Function FillTuple(ByVal ValueCriteria1 As Integer)
        Dim ListTuple As New List(Of Tuple(Of Integer, String))
        If ValueCriteria1 = 0 Then
            ListTuple.Add(New Tuple(Of Integer, String)(1, "Cuenta"))
            ListTuple.Add(New Tuple(Of Integer, String)(2, "Tercero"))
            ListTuple.Add(New Tuple(Of Integer, String)(3, "Centro"))
            ListTuple.Add(New Tuple(Of Integer, String)(4, "Fecha"))
        ElseIf ValueCriteria1 = 1 Then
            ListTuple.Add(New Tuple(Of Integer, String)(2, "Tercero"))
            ListTuple.Add(New Tuple(Of Integer, String)(3, "Centro"))
            ListTuple.Add(New Tuple(Of Integer, String)(4, "Fecha"))
        ElseIf ValueCriteria1 = 2 Then
            ListTuple.Add(New Tuple(Of Integer, String)(1, "Cuenta"))
            ListTuple.Add(New Tuple(Of Integer, String)(3, "Centro"))
            ListTuple.Add(New Tuple(Of Integer, String)(4, "Fecha"))
        ElseIf ValueCriteria1 = 3 Then
            ListTuple.Add(New Tuple(Of Integer, String)(1, "Cuenta"))
            ListTuple.Add(New Tuple(Of Integer, String)(2, "Tercero"))
            ListTuple.Add(New Tuple(Of Integer, String)(4, "Fecha"))
        End If
        Return ListTuple
    End Function

    ''' <summary>
    ''' se ejecuta al dar click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
		If Me.ValidateControlsReports = True Then
			Dim StartDate As DateTime? = DateConvert(INDDateStart.Text)
			If Not StartDate.HasValue Then
				StartDate = INDDateStart.EditValue
			End If
			Dim EndDate As DateTime? = DateConvert(INDDateEnd.Text)
			If Not EndDate.HasValue Then
				EndDate = INDDateEnd.EditValue
			End If

			If Me.INDGleCriteria1.EditValue = 1 And Me.INDGleCriteria2.EditValue = 2 Then
				AsyncLoader(True)
				Dim reporte As New rptAccountThirdParty2()
				reporte.ParametrosReporte = New Object() {StartDate,
														  EndDate,
														  INDsleGeneralReport.EditValue,
														  INDGleCriteria1.EditValue,
														  INDGleCriteria2.EditValue,
														  INDsleBook.EditValue,
														  INDGleTypeVoucher.EditValue,
														  INDGleSort.EditValue,
														  INDSleAccountStart.TextEditValue,
														  INDSleAccountEnd.TextEditValue,
														  INDSleThirdPartyStart.TextEditValue,
														  INDSleThirdPartyEnd.TextEditValue,
														  INDSleCostCenterStart.TextEditValue,
														  INDSleCostCenterEnd.TextEditValue,
														  INDGleAccumulatedBalance.EditValue,
														  INDsleBook.Text}
				INDDvReportPrint.DocumentSource = reporte
				Await reporte.CargarDataSource1()
				AsyncLoader(False)
				If reporte.DataSource IsNot Nothing Then
					reporte.CreateDocument(True)
					Me.INDLcBaseHome.Visible = False
					Me.INDCtcNavigation.Visible = False
					Me.INDPcReport.Visible = True
					INDDvReportPrint.Show()
				Else
					Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
					Me.INDDateStart.Focus()
				End If
			ElseIf Me.INDGleCriteria1.EditValue = 2 And Me.INDGleCriteria2.EditValue = 1 Then
				AsyncLoader(True)
				Dim reporte As New rptThirdPartyAccount2()
				reporte.ParametrosReporte = New Object() {StartDate,
														 EndDate,
														 INDsleGeneralReport.EditValue,
														 INDGleCriteria1.EditValue,
														 INDGleCriteria2.EditValue,
														 INDsleBook.EditValue,
														 INDGleTypeVoucher.EditValue,
														 INDGleSort.EditValue,
														 INDSleAccountStart.TextEditValue,
														 INDSleAccountEnd.TextEditValue,
														 INDSleThirdPartyStart.TextEditValue,
														 INDSleThirdPartyEnd.TextEditValue,
														 INDSleCostCenterStart.TextEditValue,
														 INDSleCostCenterEnd.TextEditValue,
														 INDGleAccumulatedBalance.EditValue,
														 INDsleBook.Text}
				INDDvReportPrint.DocumentSource = reporte
				Await reporte.CargarDataSource1()
				AsyncLoader(False)
				If reporte.DataSource IsNot Nothing Then
					reporte.CreateDocument(True)
					Me.INDLcBaseHome.Visible = False
					Me.INDCtcNavigation.Visible = False
					Me.INDPcReport.Visible = True
					INDDvReportPrint.Show()
				Else
					Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
					Me.INDDateStart.Focus()
				End If
			ElseIf Me.INDGleCriteria1.EditValue = 1 And Me.INDGleCriteria2.EditValue = 3 Then
				AsyncLoader(True)
				Dim reporte As New rptAccountCostCenter2()
				reporte.ParametrosReporte = New Object() {StartDate,
														  EndDate,
														  INDsleGeneralReport.EditValue,
														  INDGleCriteria1.EditValue,
														  INDGleCriteria2.EditValue,
														  INDsleBook.EditValue,
														  INDGleTypeVoucher.EditValue,
														  INDGleSort.EditValue,
														  INDSleAccountStart.TextEditValue,
														  INDSleAccountEnd.TextEditValue,
														  INDSleThirdPartyStart.TextEditValue,
														  INDSleThirdPartyEnd.TextEditValue,
														  INDSleCostCenterStart.TextEditValue,
														  INDSleCostCenterEnd.TextEditValue,
														  INDGleAccumulatedBalance.EditValue,
														  INDsleBook.Text}
				INDDvReportPrint.DocumentSource = reporte
				Await reporte.CargarDataSource1()
				AsyncLoader(False)
				If reporte.DataSource IsNot Nothing Then
					reporte.CreateDocument(True)
					Me.INDLcBaseHome.Visible = False
					Me.INDCtcNavigation.Visible = False
					Me.INDPcReport.Visible = True
					INDDvReportPrint.Show()
				Else
					Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
					Me.INDDateStart.Focus()
				End If
			ElseIf Me.INDGleCriteria1.EditValue = 3 And Me.INDGleCriteria2.EditValue = 1 Then
				AsyncLoader(True)
				Dim reporte As New rptCostCenterAccount2()
				reporte.ParametrosReporte = New Object() {StartDate,
														  EndDate,
														  INDsleGeneralReport.EditValue,
														  INDGleCriteria1.EditValue,
														  INDGleCriteria2.EditValue,
														  INDsleBook.EditValue,
														  INDGleTypeVoucher.EditValue,
														  INDGleSort.EditValue,
														  INDSleAccountStart.TextEditValue,
														  INDSleAccountEnd.TextEditValue,
														  INDSleThirdPartyStart.TextEditValue,
														  INDSleThirdPartyEnd.TextEditValue,
														  INDSleCostCenterStart.TextEditValue,
														  INDSleCostCenterEnd.TextEditValue,
														  INDGleAccumulatedBalance.EditValue,
														  INDsleBook.Text}
				INDDvReportPrint.DocumentSource = reporte
				Await reporte.CargarDataSource1()
				AsyncLoader(False)
				If reporte.DataSource IsNot Nothing Then
					reporte.CreateDocument(True)
					Me.INDLcBaseHome.Visible = False
					Me.INDCtcNavigation.Visible = False
					Me.INDPcReport.Visible = True
					INDDvReportPrint.Show()
				Else
					Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
					Me.INDDateStart.Focus()
				End If
			ElseIf Me.INDGleCriteria1.EditValue = 2 And Me.INDGleCriteria2.EditValue = 3 Then
				AsyncLoader(True)
				Dim reporte As New rptThirdPartyCostCenter2()
				reporte.ParametrosReporte = New Object() {StartDate,
														  EndDate,
														  INDsleGeneralReport.EditValue,
														  INDGleCriteria1.EditValue,
														  INDGleCriteria2.EditValue,
														  INDsleBook.EditValue,
														  INDGleTypeVoucher.EditValue,
														  INDGleSort.EditValue,
														  INDSleAccountStart.TextEditValue,
														  INDSleAccountEnd.TextEditValue,
														  INDSleThirdPartyStart.TextEditValue,
														  INDSleThirdPartyEnd.TextEditValue,
														  INDSleCostCenterStart.TextEditValue,
														  INDSleCostCenterEnd.TextEditValue,
														  INDGleAccumulatedBalance.EditValue,
														  INDsleBook.Text}
				INDDvReportPrint.DocumentSource = reporte
				Await reporte.CargarDataSource1()
				AsyncLoader(False)
				If reporte.DataSource IsNot Nothing Then
					reporte.CreateDocument(True)
					Me.INDLcBaseHome.Visible = False
					Me.INDCtcNavigation.Visible = False
					Me.INDPcReport.Visible = True
					INDDvReportPrint.Show()
				Else
					Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
					Me.INDDateStart.Focus()
				End If
			ElseIf Me.INDGleCriteria1.EditValue = 3 And Me.INDGleCriteria2.EditValue = 2 Then
				AsyncLoader(True)
				Dim reporte As New rptCostCenterThirdParty2()
				reporte.ParametrosReporte = New Object() {StartDate,
														  EndDate,
														  INDsleGeneralReport.EditValue,
														  INDGleCriteria1.EditValue,
														  INDGleCriteria2.EditValue,
														  INDsleBook.EditValue,
														  INDGleTypeVoucher.EditValue,
														  INDGleSort.EditValue,
														  INDSleAccountStart.TextEditValue,
														  INDSleAccountEnd.TextEditValue,
														  INDSleThirdPartyStart.TextEditValue,
														  INDSleThirdPartyEnd.TextEditValue,
														  INDSleCostCenterStart.TextEditValue,
														  INDSleCostCenterEnd.TextEditValue,
														  INDGleAccumulatedBalance.EditValue,
														  INDsleBook.Text}
				INDDvReportPrint.DocumentSource = reporte
				Await reporte.CargarDataSource1()
				AsyncLoader(False)
				If reporte.DataSource IsNot Nothing Then
					reporte.CreateDocument(True)
					Me.INDLcBaseHome.Visible = False
					Me.INDCtcNavigation.Visible = False
					Me.INDPcReport.Visible = True
					INDDvReportPrint.Show()
				Else
					Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
					Me.INDDateStart.Focus()
				End If
			ElseIf Me.INDGleCriteria1.EditValue = 1 And Me.INDGleCriteria2.EditValue = 4 Then
				AsyncLoader(True)
				Dim reporte As New rptAccountDate2()
				reporte.ParametrosReporte = New Object() {StartDate,
														  EndDate,
														  INDsleGeneralReport.EditValue,
														  INDGleCriteria1.EditValue,
														  INDGleCriteria2.EditValue,
														  INDsleBook.EditValue,
														  INDGleTypeVoucher.EditValue,
														  INDGleSort.EditValue,
														  INDSleAccountStart.TextEditValue,
														  INDSleAccountEnd.TextEditValue,
														  INDSleThirdPartyStart.TextEditValue,
														  INDSleThirdPartyEnd.TextEditValue,
														  INDSleCostCenterStart.TextEditValue,
														  INDSleCostCenterEnd.TextEditValue,
														  INDGleAccumulatedBalance.EditValue,
														  INDsleBook.Text}
				INDDvReportPrint.DocumentSource = reporte
				Await reporte.CargarDataSource1()
				AsyncLoader(False)
				If reporte.DataSource IsNot Nothing Then
					reporte.CreateDocument(True)
					Me.INDLcBaseHome.Visible = False
					Me.INDCtcNavigation.Visible = False
					Me.INDPcReport.Visible = True
					INDDvReportPrint.Show()
				Else
					Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
					Me.INDDateStart.Focus()
				End If
			ElseIf Me.INDGleCriteria1.EditValue = 2 And Me.INDGleCriteria2.EditValue = 4 Then
				AsyncLoader(True)
				Dim reporte As New rptThirdPartyDate2()
				reporte.ParametrosReporte = New Object() {StartDate,
														  EndDate,
														  INDsleGeneralReport.EditValue,
														  INDGleCriteria1.EditValue,
														  INDGleCriteria2.EditValue,
														  INDsleBook.EditValue,
														  INDGleTypeVoucher.EditValue,
														  INDGleSort.EditValue,
														  INDSleAccountStart.TextEditValue,
														  INDSleAccountEnd.TextEditValue,
														  INDSleThirdPartyStart.TextEditValue,
														  INDSleThirdPartyEnd.TextEditValue,
														  INDSleCostCenterStart.TextEditValue,
														  INDSleCostCenterEnd.TextEditValue,
														  INDGleAccumulatedBalance.EditValue,
														  INDsleBook.Text}
				INDDvReportPrint.DocumentSource = reporte
				Await reporte.CargarDataSource1()
				AsyncLoader(False)
				If reporte.DataSource IsNot Nothing Then
					reporte.CreateDocument(True)
					Me.INDLcBaseHome.Visible = False
					Me.INDCtcNavigation.Visible = False
					Me.INDPcReport.Visible = True
					INDDvReportPrint.Show()
				Else
					Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
					Me.INDDateStart.Focus()
				End If
			ElseIf Me.INDGleCriteria1.EditValue = 3 And Me.INDGleCriteria2.EditValue = 4 Then
				AsyncLoader(True)
				Dim reporte As New rptCostCenterDate2()
				reporte.ParametrosReporte = New Object() {StartDate,
														  EndDate,
														  INDsleGeneralReport.EditValue,
														  INDGleCriteria1.EditValue,
														  INDGleCriteria2.EditValue,
														  INDsleBook.EditValue,
														  INDGleTypeVoucher.EditValue,
														  INDGleSort.EditValue,
														  INDSleAccountStart.TextEditValue,
														  INDSleAccountEnd.TextEditValue,
														  INDSleThirdPartyStart.TextEditValue,
														  INDSleThirdPartyEnd.TextEditValue,
														  INDSleCostCenterStart.TextEditValue,
														  INDSleCostCenterEnd.TextEditValue,
														  INDGleAccumulatedBalance.EditValue,
														  INDsleBook.Text}
				INDDvReportPrint.DocumentSource = reporte
				Await reporte.CargarDataSource1()
				AsyncLoader(False)
				If reporte.DataSource IsNot Nothing Then
					reporte.CreateDocument(True)
					Me.INDLcBaseHome.Visible = False
					Me.INDCtcNavigation.Visible = False
					Me.INDPcReport.Visible = True
					INDDvReportPrint.Show()
				Else
					Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
					Me.INDDateStart.Focus()
				End If
			End If
		End If

	End Sub

    ''' <summary>
    ''' se ejecuta al cambiar el valor del control INDGleCriteria1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleCriteria1_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleCriteria1.EditValueChanged
        If Me.INDGleCriteria1.EditValue = 1 Then
            Me.INDGleCriteria2.Properties.DataSource = FillTuple(1)
        ElseIf Me.INDGleCriteria1.EditValue = 2 Then
            Me.INDGleCriteria2.Properties.DataSource = FillTuple(2)
        ElseIf Me.INDGleCriteria1.EditValue = 3 Then
            Me.INDGleCriteria2.Properties.DataSource = FillTuple(3)
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBaseHome.Visible = True
        INDCtcNavigation.Visible = True
        INDPcReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountStart.QueryPopUp
        If INDSleAccountStart.Datasource Is Nothing Then
            LoadXpoAccountStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountEnd.QueryPopUp
        If INDSleAccountEnd.Datasource Is Nothing Then
            LoadXpoAccountEnd()
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
            LoadXpoThirdPartyStart()
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
            LoadXpoThirdPartyEnd()
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

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    Private Sub FrmReportAuxiliary_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleCostCenterStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCostCenterAsync
        Me.INDSleCostCenterEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCostCenterAsync

        INDsleBook.Properties.Buttons(1).Visible = False
        INDSleAccountStart.View.OptionsView.ShowGroupPanel = False
        INDSleAccountEnd.View.OptionsView.ShowGroupPanel = False
        INDSleThirdPartyStart.View.OptionsView.ShowGroupPanel = False
        INDSleThirdPartyEnd.View.OptionsView.ShowGroupPanel = False
        INDSleCostCenterStart.View.OptionsView.ShowGroupPanel = False
        INDSleCostCenterEnd.View.OptionsView.ShowGroupPanel = False
        LoadXpoBook()
        SetOfficialBook()
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportAuxiliary_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleCriteria1.Properties.DataSource = FillingCriteria
        Me.INDGleCriteria2.Properties.DataSource = FillTuple(0)
        Me.INDGleTypeVoucher.Properties.DataSource = FillingTypeVoucher
        Me.INDGleSort.Properties.DataSource = FillingSort
        Me.INDGleAccumulatedBalance.Properties.DataSource = FillingAccumulatedBalance

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleCriteria1.EditValue = 1
        Me.INDGleCriteria2.EditValue = 2
        Me.INDGleTypeVoucher.EditValue = 2
        Me.INDsleGeneralReport.EditValue = False
        Me.INDGleSort.EditValue = 2
        Me.INDGleAccumulatedBalance.EditValue = False
    End Sub

	''' <summary>
	''' se ejecuta para exportar a excel el reporte
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
		If ValidateControlsReports() Then

			Try
				AsyncLoader(True)
				Dim StartDate As DateTime? = DateConvert(INDDateStart.Text)
				If Not StartDate.HasValue Then
					StartDate = INDDateStart.EditValue
				End If
				Dim EndDate As DateTime? = DateConvert(INDDateEnd.Text)
				If Not EndDate.HasValue Then
					EndDate = INDDateEnd.EditValue
				End If
				Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportAuxiliarAsync(
					StartDate, EndDate,
					INDsleGeneralReport.EditValue,
					INDGleCriteria1.EditValue, INDGleCriteria2.EditValue,
					INDsleBook.EditValue,
					INDGleTypeVoucher.EditValue, INDGleSort.EditValue,
					INDSleAccountStart.TextEditValue, INDSleAccountEnd.TextEditValue,
					INDSleThirdPartyStart.TextEditValue, INDSleThirdPartyEnd.TextEditValue,
					INDSleCostCenterStart.TextEditValue, INDSleCostCenterEnd.TextEditValue,
					INDGleAccumulatedBalance.EditValue,
					Me.IndigoSessionValues)
				If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
					Dim dtReportAuxiliar As DataTable = ds.Tables("ReportAuxiliar")

					Await Task.Factory.StartNew(Sub()
													chargueDatasource(dtReportAuxiliar)
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
	''' Convierte el string de una fecha en datetime
	''' </summary>
	''' <param name="dateString"></param>
	Private Function DateConvert(dateString As String) As DateTime?
		Dim cultureInfo As Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentUICulture
		Dim shortPattern As String = Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern
		Dim dateTimeConvert As DateTime

		If DateTime.TryParseExact(dateString, shortPattern, cultureInfo, Globalization.DateTimeStyles.None, dateTimeConvert) Then
			Return dateTimeConvert
		Else
			Return Nothing
		End If
	End Function

	''' <summary>
	''' Carga el DataSource del informe auxiliar
	''' </summary>
	''' <param name="dtReportAuxiliar"></param>
	Private Sub chargueDatasource(dtReportAuxiliar As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Libro")
        If DetallingAccount Then
            dt.Columns.Add("Código Cuenta")
            dt.Columns.Add("Nombre Cuenta")
            dt.Columns.Add("Naturaleza")
        End If
        If DetallingThirdParty Then
            dt.Columns.Add("Nit Tercero")
            dt.Columns.Add("Nombre Tercero")
        End If
        If DetallingCostCenter Then
            dt.Columns.Add("Codigo C. Costo")
            dt.Columns.Add("Nombre C. Costo")
        End If
        If INDsleGeneralReport.EditValue Then
            dt.Columns.Add("Saldo Anterior", GetType(Decimal))
        Else
            dt.Columns.Add("Fecha Documento", GetType(DateTime))
            dt.Columns.Add("Codigo Comprobante")
            dt.Columns.Add("Tipo de Comprobante")
            dt.Columns.Add("Código Documento Fuente")
            dt.Columns.Add("Nombre Documento Fuente")
            dt.Columns.Add("Estado")
            dt.Columns.Add("Detalle")
        End If
        dt.Columns.Add("Moneda")
        dt.Columns.Add("Valor Débito", GetType(Decimal))
        dt.Columns.Add("Valor Crédito", GetType(Decimal))
        dt.Columns.Add("Saldo Acumulado", GetType(Decimal))

        For Each item As DataRow In dtReportAuxiliar.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Libro") = INDsleBook.Text
            If DetallingAccount Then
                row.Item("Código Cuenta") = item("Number")
                row.Item("Nombre Cuenta") = item("NameAccount")
                row.Item("Naturaleza") = item("NatureName")
            End If
            If DetallingThirdParty Then
                row.Item("Nit Tercero") = item("Nit")
                row.Item("Nombre Tercero") = item("NameThirdParty")
            End If
            If DetallingCostCenter Then
                row.Item("Codigo C. Costo") = item("CodeCostCenter")
                row.Item("Nombre C. Costo") = item("NameCodeCenter")
            End If
            If INDsleGeneralReport.EditValue Then
                row.Item("Saldo Anterior") = item("PreviousBalance")
            Else
                row.Item("Fecha Documento") = If(IsDBNull(item("DocumentDate")), DBNull.Value, CDate(item("DocumentDate")).AsDate)
                row.Item("Codigo Comprobante") = item("Consecutive")
                row.Item("Tipo de Comprobante") = item("JournalVoucherType")
                row.Item("Código Documento Fuente") = item("EntityCode")
                row.Item("Nombre Documento Fuente") = item("EntityName")
                row.Item("Estado") = item("StatusName")
                row.Item("Detalle") = item("Detail")
            End If
            row.Item("Moneda") = item("LegalBookCurrency") + "(" + item("LegalBookCurrencyAbbreviation") + ")"
            row.Item("Valor Débito") = item("DebitValue")
            row.Item("Valor Crédito") = item("CreditValue")
            row.Item("Saldo Acumulado") = item("Balance")
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
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se activa la barra de botones 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
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
    ''' Este evento se activa cuando el formulario es eliminado y sus recursos deben ser liberados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
    End Sub



    ''' <summary>
    ''' metodo que carga los datasource de la cuenta inicial y final al momento de cambiar el libro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBook.EditValueChanged
        If INDsleBook.EditValue IsNot Nothing Then
            LoadXpoAccountStart()
            LoadXpoAccountEnd()
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando cambia el valor seleccionado en el elemento de edición "INDsleGeneralReport"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleGeneralReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleGeneralReport.EditValueChanged
        INDLciSort.Visibility = IIf(INDsleGeneralReport.EditValue, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        INDLciAccumulatedBalance.Visibility = IIf(INDsleGeneralReport.EditValue, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
    End Sub

End Class