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
Imports Presentation.Portfolio.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportTransferListPortfolio


#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "properties"
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoTrasnfers As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o asigna la fecha inicial del reporte
    ''' </summary>
    Public Property DateStart As Date
        Get
            Return INDDateStart.EditValue
        End Get
        Set(value As Date)
            INDDateStart.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la fecha final del reporte
    ''' </summary>
    Public Property DateEnd As Date
        Get
            Return INDDateEnd.EditValue
        End Get
        Set(value As Date)
            INDDateEnd.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el estado del reporte
    ''' </summary>
    Public Property StatusReport As Integer
        Get
            Return INDGleStatusReport.EditValue
        End Get
        Set(value As Integer)
            INDGleStatusReport.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tercero inicial
    ''' </summary>
    Public Property ThirdPartyStart As String
        Get
            Return INDSleThirdPartyStart.EditValue
        End Get
        Set(value As String)
            INDSleThirdPartyStart.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tercero final
    ''' </summary>
    Public Property ThirdPartyEnd As String
        Get
            Return INDSleThirdPartyEnd.EditValue
        End Get
        Set(value As String)
            INDSleThirdPartyEnd.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el Traslado Inicial
    ''' </summary>
    Public Property TransferStart As String
        Get
            Return INDSleTransferStart.EditValue
        End Get
        Set(value As String)
            INDSleTransferStart.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el traslado final
    ''' </summary>
    Public Property TransferEnd As String
        Get
            Return INDSleTransferEnd.EditValue
        End Get
        Set(value As String)
            INDSleTransferEnd.EditValue = value
        End Set
    End Property

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
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Listado Traslados"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Documento Traslados"))
            End If
            Return _FillingTypeReport
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
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReportPortfolio)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReportPortfolio)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTransferStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTransferStart()
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
            ProoftCloseXpoTrasnfers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioTransferReportFilter, criteria)
            INDSleTransferStart.Datasource = ProoftCloseXpoTrasnfers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTransferEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTransferEnd()
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
            ProoftCloseXpoTrasnfers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioTransferReportFilter, criteria)
            INDSleTransferEnd.Datasource = ProoftCloseXpoTrasnfers
        End Using
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDateEnd.Focus()
            Validations = False
        End If

        'Valida Terceros
        If INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Or INDSleThirdPartyEnd.EditValue Is Nothing And INDSleThirdPartyStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyEnd.EditValue < INDSleThirdPartyStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        End If

        'Valida Traslados
        If INDSleTransferStart.EditValue Is Nothing And INDSleTransferEnd.EditValue IsNot Nothing Or INDSleTransferEnd.EditValue Is Nothing And INDSleTransferStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblTranfer.Text)
            Me.INDSleTransferStart.Focus()
            Validations = False
        ElseIf INDSleTransferEnd.EditValue < INDSleTransferStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblTranfer.Text)
            Me.INDSleTransferStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerareReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        If Me.ValidateControlsReports Then
            AsyncLoader(True)
            If INDGleTypeReport.EditValue = 1 Then
                Dim reporte As New rptTransfersList

                reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDGleStatusReport.EditValue,
                                             INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             INDSleTransferStart.EditValue, INDSleTransferEnd.EditValue}

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
            Else
                AsyncLoader(True)
                Dim reporte As New rptSubTranferPorfolioAll

                reporte.ParametrosReporte = {DateStart, DateEnd, StatusReport,
                             ThirdPartyStart, ThirdPartyEnd,
                             TransferStart, TransferEnd}

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
    Private Sub FrmReportTransferListPortfolio_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar el GridLookUpEdit
        Me.INDGleStatusReport.Properties.DataSource = FillingStatus
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleStatusReport.EditValue = 4
        Me.INDGleTypeReport.EditValue = 1
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            LoadXpoThirdPartyStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleThirdPartyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            LoadXpoThirdPartyEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleTransferStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTransferStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleTransferStart.QueryPopUp
        If INDSleTransferStart.Datasource Is Nothing Then
            LoadXpoTransferStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleTransferEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTransferEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleTransferEnd.QueryPopUp
        If INDSleTransferEnd.Datasource Is Nothing Then
            LoadXpoTransferEnd()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoThirdParty = Nothing
        ProoftCloseXpoTrasnfers = Nothing
    End Sub

    Private Sub INDGleStatusReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleStatusReport.EditValueChanged
        LoadXpoTransferStart()
        LoadXpoTransferEnd()
    End Sub

    Private Sub INDSleThirdPartyStart_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyStart.EditValueChanged
        LoadXpoTransferStart()
        LoadXpoTransferEnd()
    End Sub

    Private Sub INDSleThirdPartyEnd_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyEnd.EditValueChanged
        LoadXpoTransferStart()
        LoadXpoTransferEnd()
    End Sub

    Private Sub FrmReportTransferListPortfolio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleTransferStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetPortfolioTransfersByCode
        Me.INDSleTransferEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetPortfolioTransfersByCode
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

End Class