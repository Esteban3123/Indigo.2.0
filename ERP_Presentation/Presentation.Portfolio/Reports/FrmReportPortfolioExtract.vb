#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP

#End Region

Public Class FrmReportPortfolioExtract

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoSellers As XPInstantFeedbackSource
    Public Property ProoftCloseXpoAccountsReceivables As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCareGroups As XPInstantFeedbackSource
    Public Property ProoftCloseXpoMainAccounts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoAdvances As XPInstantFeedbackSource

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Anulado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido Cliente"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido Cuenta"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Resumido Factura/Anticipo"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(4, "Detallado"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingViewReport As List(Of Tuple(Of String, String))
    Private ReadOnly Property FillingViewReport As List(Of Tuple(Of String, String))
        Get
            If _FillingViewReport Is Nothing Then
                _FillingViewReport = New List(Of Tuple(Of String, String))
                _FillingViewReport.Add(New Tuple(Of String, String)("Anticipo", "Anticipos"))
                _FillingViewReport.Add(New Tuple(Of String, String)("Factura", "Factura"))
                _FillingViewReport.Add(New Tuple(Of String, String)("Todos", "Todos"))
            End If
            Return _FillingViewReport
        End Get
    End Property

    Private criteria As String = Nothing

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
    ''' metodo para Cargar el data source Del Control INDSleCustomersStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomersStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReportPortfolio)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCustomersEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomersEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReportPortfolio)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountsReceivableStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsReceivableStart()
        criteria = Nothing

        If (INDGleStatusReport.EditValue IsNot Nothing And INDGleStatusReport.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatusReport.EditValue
        End If

        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            Else
                criteria &= " AND ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoAccountsReceivables = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInvoiceReportPortfolioFilter, criteria)
            INDSleAccountsReceivableStart.Datasource = ProoftCloseXpoAccountsReceivables
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountsReceivableEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsReceivableEnd()
        criteria = Nothing

        If (INDGleStatusReport.EditValue IsNot Nothing And INDGleStatusReport.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatusReport.EditValue
        End If

        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            Else
                criteria &= " AND ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoAccountsReceivables = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInvoiceReportPortfolioFilter, criteria)
            INDSleAccountsReceivableEnd.Datasource = ProoftCloseXpoAccountsReceivables
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCareGroupStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCareGroupStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCareGroups = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCareGroupReportPortfolio)
            INDSleCareGroupsStart.Datasource = ProoftCloseXpoCareGroups
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCareGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCareGroupEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCareGroups = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCareGroupReportPortfolio)
            INDSleCareGroupsEnd.Datasource = ProoftCloseXpoCareGroups
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleMainAccountsStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoMainAccountsStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoMainAccounts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsReportPortfolio)
            INDSleMainAccountsStart.Datasource = ProoftCloseXpoMainAccounts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleMainAccountsStartEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoMainAccountsEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoMainAccounts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsReportPortfolio)
            INDSleMainAccountsEnd.Datasource = ProoftCloseXpoMainAccounts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAdvanceStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAdvancesStart()
        criteria = Nothing

        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            criteria = "ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "' AND Status = 2"
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoAdvances = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdvancesReportPortfolioFilter, criteria)
            INDSleAdvanceStart.Datasource = ProoftCloseXpoAdvances
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAdvanceStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAdvancesEnd()
        criteria = Nothing

        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            criteria = "ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "' AND Status = 2"
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoAdvances = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdvancesReportPortfolioFilter, criteria)
            INDSleAdvanceEnd.Datasource = ProoftCloseXpoAdvances
        End Using
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True

        'Valida Cuentas por Cobrar
        If INDSleAccountsReceivableStart.EditValue Is Nothing And INDSleAccountsReceivableEnd.EditValue IsNot Nothing Or INDSleAccountsReceivableEnd.EditValue Is Nothing And INDSleAccountsReceivableStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccountsReceivable.Text)
            Me.INDSleAccountsReceivableStart.Focus()
            Validations = False
        ElseIf INDSleAccountsReceivableEnd.EditValue < INDSleAccountsReceivableStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccountsReceivable.Text)
            Me.INDSleAccountsReceivableStart.Focus()
            Validations = False
        End If

        'Valida Anticipos
        If INDSleAdvanceStart.EditValue Is Nothing And INDSleAdvanceEnd.EditValue IsNot Nothing Or INDSleAdvanceEnd.EditValue Is Nothing And INDSleAdvanceStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAdvance.Text)
            Me.INDSleAdvanceStart.Focus()
            Validations = False
        ElseIf INDSleAdvanceEnd.EditValue < INDSleAdvanceStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAdvance.Text)
            Me.INDSleAdvanceStart.Focus()
            Validations = False
        End If

        'Valida si no hay datos en los filtros anticipos o cuentas por cobrar y realiza la validación de fecha
        If INDSleAccountsReceivableStart.EditValue Is Nothing And INDSleAccountsReceivableEnd.EditValue Is Nothing And INDSleAdvanceStart.EditValue Is Nothing And INDSleAdvanceEnd.EditValue Is Nothing Then

            'Valida las fechas de inicio y fin de los criterios
            If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
                Me.INDDateStart.Focus()
                Validations = False
            ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
                Me.INDDateEnd.Focus()
                Validations = False
            End If
        End If

        'Valida Clientes
        If INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Or INDSleThirdPartyEnd.EditValue Is Nothing And INDSleThirdPartyStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCustomers.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyEnd.EditValue < INDSleThirdPartyStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCustomers.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        End If



            'Valida Grupos de Atención
            If INDSleCareGroupsStart.EditValue Is Nothing And INDSleCareGroupsEnd.EditValue IsNot Nothing Or INDSleCareGroupsEnd.EditValue Is Nothing And INDSleCareGroupsStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCareGroups.Text)
            Me.INDSleCareGroupsStart.Focus()
            Validations = False
        ElseIf INDSleCareGroupsEnd.EditValue < INDSleCareGroupsStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCareGroups.Text)
            Me.INDSleCareGroupsStart.Focus()
            Validations = False
        End If

        'Valida Cuenta Contable
        If INDSleMainAccountsStart.EditValue Is Nothing And INDSleMainAccountsEnd.EditValue IsNot Nothing Or INDSleMainAccountsEnd.EditValue Is Nothing And INDSleMainAccountsStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblMainAccounts.Text)
            Me.INDSleMainAccountsStart.Focus()
            Validations = False
        ElseIf INDSleMainAccountsEnd.EditValue < INDSleMainAccountsStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblMainAccounts.Text)
            Me.INDSleMainAccountsStart.Focus()
            Validations = False
        End If



            Return Validations

    End Function

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoAccountsReceivables = Nothing
        ProoftCloseXpoAdvances = Nothing
        ProoftCloseXpoCareGroups = Nothing
        ProoftCloseXpoMainAccounts = Nothing
        ProoftCloseXpoSellers = Nothing
        ProoftCloseXpoThirdParty = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerareReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        If Me.ValidateControlsReports Then
            If INDGleTypeReport.EditValue = 1 Then

                AsyncLoader(True)

                Dim reporte As New rptExtractResumeThirdParty

                reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDGleStatusReport.EditValue, INDGleTypeReport.EditValue,
                                             INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             INDSleAccountsReceivableStart.EditValue, INDSleAccountsReceivableEnd.EditValue,
                                             INDSleCareGroupsStart.EditValue, INDSleCareGroupsEnd.EditValue,
                                             INDSleMainAccountsStart.EditValue, INDSleMainAccountsEnd.EditValue,
                                             INDSleAdvanceStart.EditValue, INDSleAdvanceEnd.EditValue, INDCcbAccountReceivableType.EditValue, INDGleViewReport.EditValue}

                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If

            ElseIf INDGleTypeReport.EditValue = 2 Then

                AsyncLoader(True)

                Dim reporte As New rptExtractResumeAccount

                reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDGleStatusReport.EditValue, INDGleTypeReport.EditValue,
                                             INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             INDSleAccountsReceivableStart.EditValue, INDSleAccountsReceivableEnd.EditValue,
                                             INDSleCareGroupsStart.EditValue, INDSleCareGroupsEnd.EditValue,
                                             INDSleMainAccountsStart.EditValue, INDSleMainAccountsEnd.EditValue,
                                             INDSleAdvanceStart.EditValue, INDSleAdvanceEnd.EditValue, INDCcbAccountReceivableType.EditValue, INDGleViewReport.EditValue}

                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If

            ElseIf INDGleTypeReport.EditValue = 3 Then

                AsyncLoader(True)

                Dim reporte As New rptExtract

                reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDGleStatusReport.EditValue, INDGleTypeReport.EditValue,
                                             INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             INDSleAccountsReceivableStart.EditValue, INDSleAccountsReceivableEnd.EditValue,
                                             INDSleCareGroupsStart.EditValue, INDSleCareGroupsEnd.EditValue,
                                             INDSleMainAccountsStart.EditValue, INDSleMainAccountsEnd.EditValue,
                                             INDSleAdvanceStart.EditValue, INDSleAdvanceEnd.EditValue, INDCcbAccountReceivableType.EditValue, INDGleViewReport.EditValue}

                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If

            ElseIf INDGleTypeReport.EditValue = 4 Then

                AsyncLoader(True)

                Dim reporte As New rptExtract

                reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDGleStatusReport.EditValue, INDGleTypeReport.EditValue,
                                             INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             INDSleAccountsReceivableStart.EditValue, INDSleAccountsReceivableEnd.EditValue,
                                             INDSleCareGroupsStart.EditValue, INDSleCareGroupsEnd.EditValue,
                                             INDSleMainAccountsStart.EditValue, INDSleMainAccountsEnd.EditValue,
                                             INDSleAdvanceStart.EditValue, INDSleAdvanceEnd.EditValue, INDCcbAccountReceivableType.EditValue, INDGleViewReport.EditValue}

                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del Control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        INDLcBase.Visible = True
        INDCncNavigation.Visible = True
        INDPcViewReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento Shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportPortfolioExtract_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar el GridLookUpEdit
        Me.INDGleStatusReport.Properties.DataSource = FillingStatus
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleViewReport.Properties.DataSource = FillingViewReport

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleStatusReport.EditValue = 4
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleViewReport.EditValue = "Todos"

    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCustomerStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            LoadXpoCustomersStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCustomerEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            LoadXpoCustomersEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleAccountsReceivableStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsReceivableStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountsReceivableStart.QueryPopUp
        If INDSleAccountsReceivableStart.Datasource Is Nothing Then
            LoadXpoAccountsReceivableStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleAccountsReceivableEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsReceivableEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountsReceivableEnd.QueryPopUp
        If INDSleAccountsReceivableEnd.Datasource Is Nothing Then
            LoadXpoAccountsReceivableEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCareGroupStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCareGroupsStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCareGroupsStart.QueryPopUp
        If INDSleCareGroupsStart.Datasource Is Nothing Then
            LoadXpoCareGroupStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCareGroupEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCareGroupsEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCareGroupsEnd.QueryPopUp
        If INDSleCareGroupsEnd.Datasource Is Nothing Then
            LoadXpoCareGroupEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleMaintAccountsStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleMainAccountsStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleMainAccountsStart.QueryPopUp
        If INDSleMainAccountsStart.Datasource Is Nothing Then
            LoadXpoMainAccountsStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleMaintAccountsEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleMainAccountsEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleMainAccountsEnd.QueryPopUp
        If INDSleMainAccountsEnd.Datasource Is Nothing Then
            LoadXpoMainAccountsEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleAdvanceStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAdvanceStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAdvanceStart.QueryPopUp
        If INDSleAdvanceStart.Datasource Is Nothing Then
            LoadXpoAdvancesStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleAdvanceEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAdvanceEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAdvanceEnd.QueryPopUp
        If INDSleAdvanceEnd.Datasource Is Nothing Then
            LoadXpoAdvancesEnd()
        End If
    End Sub

    Private Sub INDGleStatusReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleStatusReport.EditValueChanged
        LoadXpoAccountsReceivableStart()
        LoadXpoAccountsReceivableEnd()
    End Sub

    Private Sub INDSleThirdPartyStart_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyStart.EditValueChanged
        LoadXpoAccountsReceivableStart()
        LoadXpoAccountsReceivableEnd()
        LoadXpoAdvancesStart()
        LoadXpoAdvancesEnd()
    End Sub

    Private Sub INDSleThirdPartyEnd_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyEnd.EditValueChanged
        LoadXpoAccountsReceivableStart()
        LoadXpoAccountsReceivableEnd()
        LoadXpoAdvancesStart()
        LoadXpoAdvancesEnd()
    End Sub

    Private Sub FrmReportPortfolioExtract_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleAccountsReceivableStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountReceivableByInvoiceNumber
        Me.INDSleAccountsReceivableEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountReceivableByInvoiceNumber
        Me.INDSleCareGroupsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCareGroup
        Me.INDSleCareGroupsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCareGroup
        Me.INDSleMainAccountsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleMainAccountsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleAdvanceStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetPortfolioAdvance
        Me.INDSleAdvanceEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetPortfolioAdvance

        INDSleThirdPartyStart.View.OptionsView.ShowGroupPanel = False
        INDSleThirdPartyEnd.View.OptionsView.ShowGroupPanel = False
        INDSleAccountsReceivableStart.View.OptionsView.ShowGroupPanel = False
        INDSleAccountsReceivableEnd.View.OptionsView.ShowGroupPanel = False
        INDSleCareGroupsStart.View.OptionsView.ShowGroupPanel = False
        INDSleCareGroupsEnd.View.OptionsView.ShowGroupPanel = False
        INDSleMainAccountsStart.View.OptionsView.ShowGroupPanel = False
        INDSleMainAccountsEnd.View.OptionsView.ShowGroupPanel = False
        INDSleAdvanceStart.View.OptionsView.ShowGroupPanel = False
        INDSleAdvanceEnd.View.OptionsView.ShowGroupPanel = False
    End Sub

    Private Sub INDGleViewReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleViewReport.EditValueChanged
        If Me.INDGleViewReport.EditValue = "Anticipo" Then
            Me.INDSleAccountsReceivableStart.EditValue = Nothing
            Me.INDSleAccountsReceivableEnd.EditValue = Nothing
            Me.INDCcbAccountReceivableType.EditValue = Nothing
            Me.INDLciAccountsReceivable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciAccountsReceivableStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciAccountsReceivableEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciCareGroups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciCareGroupsStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciCareGroupsEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAdvanceStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAdvanceEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAccountReceivableType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        ElseIf Me.INDGleViewReport.EditValue = "Factura" Then
            Me.INDSleAdvanceStart.EditValue = Nothing
            Me.INDSleAdvanceEnd.EditValue = Nothing
            Me.INDLciAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciAdvanceStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciAdvanceEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciAccountsReceivable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAccountsReceivableStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAccountsReceivableEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciCareGroups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciCareGroupsStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciCareGroupsEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAccountReceivableType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.INDLciCareGroups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciCareGroupsStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciCareGroupsEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDLciAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAdvanceStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAdvanceEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAccountsReceivable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAccountsReceivableStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAccountsReceivableEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciAccountReceivableType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDCcbAccountReceivableType.EditValue = Nothing
        End If
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

End Class