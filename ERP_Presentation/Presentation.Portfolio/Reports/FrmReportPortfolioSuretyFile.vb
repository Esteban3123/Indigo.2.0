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
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportPortfolioSuretyFile

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoBankAccount As XPInstantFeedbackSource

    Dim _collectionType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property CollectionType As List(Of Tuple(Of Integer, String))
        Get
            If _collectionType Is Nothing Then
                _collectionType = New List(Of Tuple(Of Integer, String))
                _collectionType.Add(New Tuple(Of Integer, String)(0, "Todas"))
                _collectionType.Add(New Tuple(Of Integer, String)(1, "Cajas"))
                _collectionType.Add(New Tuple(Of Integer, String)(2, "Bancos"))
                _collectionType.Add(New Tuple(Of Integer, String)(3, "Saldo Inicial"))
                _collectionType.Add(New Tuple(Of Integer, String)(4, "Distribución Anticipo"))
            End If
            Return _collectionType
        End Get
    End Property

    Dim _incluideZero As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property IncluideZero As List(Of Tuple(Of Boolean, String))
        Get
            If IncluideZero Is Nothing Then
                IncluideZero = New List(Of Tuple(Of Boolean, String))
                IncluideZero.Add(New Tuple(Of Boolean, String)(False, "No"))
                IncluideZero.Add(New Tuple(Of Boolean, String)(True, "Si"))
            End If
            Return IncluideZero
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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True
        'If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
        '    Me.INDDateStart.Focus()
        '    Validations = False
        'ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
        '    Me.INDDateStart.Focus()
        '    Validations = False
        'End If

        'Valida si la fecha inicial es mayor a la inicial
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            If Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
                Me.INDDateEnd.Focus()
                Validations = False
            End If
            'valida si alguna de las fechas tiene informacion  y el radiacado tiene datos ..para informar a obligar a completar las dos fechas
        ElseIf (INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        End If

        'validaciones controles de tercero
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue Is Nothing Or INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyStart.EditValue > INDSleThirdPartyEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        End If
        'validaciones controles de Cuenta Bancaria
        If INDSleAccountBankStart.EditValue IsNot Nothing And INDSleAccountBankEnd.EditValue Is Nothing Or INDSleAccountBankStart.EditValue Is Nothing And INDSleAccountBankEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccountBank.Text)
            Me.INDSleAccountBankStart.Focus()
            Validations = False
        ElseIf INDSleAccountBankStart.EditValue > INDSleAccountBankEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccountBank.Text)
            Me.INDSleAccountBankStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

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
    ''' metodo para Cargar el data source Del Control INDSleAccountBankStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountBankStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoBankAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountBankReportPortfolio)
            INDSleAccountBankStart.Datasource = ProoftCloseXpoBankAccount
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountBankEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountBankEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoBankAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountBankReportPortfolio)
            INDSleAccountBankEnd.Datasource = ProoftCloseXpoBankAccount
        End Using
    End Sub

    ''' <summary>
    ''' metodo para ejecutar el QueryPopUp source Del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            LoadXpoThirdPartyStart()
        End If
    End Sub

    ''' <summary>
    ''' metodo para ejecutar el QueryPopUp source Del Control INDSleThirdPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            LoadXpoThirdPartyEnd()
        End If
    End Sub

    ''' <summary>
    ''' metodo para ejecutar el QueryPopUp source Del Control INDSleAccountBankStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDSleAccountBankStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountBankStart.QueryPopUp
        If INDSleAccountBankStart.Datasource Is Nothing Then
            LoadXpoAccountBankStart()
        End If
    End Sub

    ''' <summary>
    ''' metodo para ejecutar el QueryPopUp source Del Control INDSleAccountBankEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDSleAccountBankEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountBankEnd.QueryPopUp
        If INDSleAccountBankEnd.Datasource Is Nothing Then
            LoadXpoAccountBankEnd()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoBankAccount = Nothing
        ProoftCloseXpoThirdParty = Nothing
    End Sub

    ''' <summary>
    ''' metodo para ejecutar el evento Shown del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FrmReportPortfolioSuretyFile_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleAccountBankStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEntityBankAccount
        Me.INDSleAccountBankEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEntityBankAccount
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() Then
            AsyncLoader(True)

            Dim filtro As String = Nothing

            If INDDateStart.EditValue IsNot Nothing AndAlso INDDateEnd.EditValue IsNot Nothing Then
                filtro &= "GetDate(DocumentDate) >= #" & Format(Me.INDDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(Me.INDDateEnd.EditValue, "yyyy-MM-dd") & "#"
            End If

            'Se filtra por Tercero
            If Me.INDSleThirdPartyStart.EditValue IsNot Nothing And Me.INDSleThirdPartyEnd.EditValue IsNot Nothing Then
                If filtro Is Nothing Then
                    filtro &= "Nit >= '" & Me.INDSleThirdPartyStart.EditValue.ToString & "' AND Nit <= '" & Me.INDSleThirdPartyEnd.EditValue.ToString & "'"
                Else
                    filtro &= " AND Nit >= '" & Me.INDSleThirdPartyStart.EditValue.ToString & "' AND Nit <= '" & Me.INDSleThirdPartyEnd.EditValue.ToString & "'"
                End If
            End If

            'Se filtra por Cuentas Bancaria
            If Me.INDSleAccountBankStart.EditValue IsNot Nothing And Me.INDSleAccountBankEnd.EditValue IsNot Nothing Then
                If filtro Is Nothing Then
                    filtro &= "BankAccount >= '" & Me.INDSleAccountBankStart.EditValue.ToString & "' AND BankAccount <= '" & Me.INDSleAccountBankEnd.EditValue.ToString & "'"
                Else
                    filtro &= " AND BankAccount >= '" & Me.INDSleAccountBankStart.EditValue.ToString & "' AND BankAccount <= '" & Me.INDSleAccountBankEnd.EditValue.ToString & "'"
                End If
            End If

            If Me.INDGleTipoRecaudo.EditValue <> 0 Then
                If filtro Is Nothing Then
                    filtro &= "CollectType = " & Me.INDGleTipoRecaudo.EditValue.ToString()
                Else
                    filtro &= " AND CollectType = " & Me.INDGleTipoRecaudo.EditValue.ToString()
                End If
            End If

            If Not Me.INDGleIncludeZero.EditValue Then
                If filtro IsNot Nothing Then
                    filtro &= " AND Balance <> 0"
                Else
                    filtro &= "  Balance <> 0"
                End If
            End If

            Dim listReport As XPCollection(Of VReportSuretyFileXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PortfolioService.GetAllVReportSuretyFileXpo(filtro)
            Me.INDGcExportExcel.DataSource = listReport
            AsyncLoader(False)
            Me.INDGcExportExcel.Visible = True
            If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                generateExcel()
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
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
    ''' se ejecuta para generar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports Then
            AsyncLoader(True)

            Dim reporte As New rptSuretyFile

            reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue,
                                         INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                         INDSleAccountBankStart.EditValue, INDSleAccountBankEnd.EditValue,
                                         INDGleTipoRecaudo.EditValue, INDGleIncludeZero.EditValue}

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
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del Control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCncNavigatin_ClickBack() Handles INDCncNavigatin.ClickBack
        INDLcBase.Visible = True
        INDCncNavigation.Visible = True
        INDPcViewReport.Visible = False
    End Sub

    Private Sub FrmReportPortfolioSuretyFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGleTipoRecaudo.Properties.DataSource = CollectionType
        INDGleIncludeZero.Properties.DataSource = IncluideZero

        INDGleTipoRecaudo.EditValue = 0
        INDGleIncludeZero.EditValue = False
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

End Class