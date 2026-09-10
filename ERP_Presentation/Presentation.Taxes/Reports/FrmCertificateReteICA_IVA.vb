#Region "Imports"

Imports System.Data
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmCertificateReteICA_IVA

#Region "Variables"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    Private _FillingType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingType Is Nothing Then
                _FillingType = New List(Of Tuple(Of Integer, String))
                _FillingType.Add(New Tuple(Of Integer, String)(1, "ICA"))
                _FillingType.Add(New Tuple(Of Integer, String)(2, "IVA"))
            End If
            Return _FillingType
        End Get
    End Property

    Private criteria As String = Nothing

    Private INDDateStart As Date = Nothing

    Private INDDateEnd As Date = Nothing

#End Region

#Region "XPO"

    Public Property bookXpcollection As XPCollection

    Public Property ProoftCloseXpoAccounts As XPInstantFeedbackSource

    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCertificateReteICA_IVA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)

        'Asignar funcion a evento keyenter a los searchlookupedit
        Me.INDSleAccountsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleAccountsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync

        LoadXpoBook()
        SetOfficialBook()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoAccounts = Nothing
        ProoftCloseXpoThirdParty = Nothing
    End Sub

    ''' <summary>
    ''' Se ejecuta al cargar por primer vez en formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCertificateReteICA_IVA_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar el GridLookUpEdit
        Me.INDGleType.Properties.DataSource = FillingType

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleType.EditValue = 1
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountsStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs)
        If INDSleAccountsStart.Datasource Is Nothing Then
            LoadXpoAccountsStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountsEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs)
        If INDSleAccountsEnd.Datasource Is Nothing Then
            LoadXpoAccountsEnd()
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

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se ejecuta en el evento Changed del control INDGleType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleType.EditValueChanged
        LoadXpoAccountsStart()
        LoadXpoAccountsEnd()
    End Sub

    Private Sub INDsleBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBook.EditValueChanged
        If INDsleBook.EditValue IsNot Nothing Then
            LoadXpoAccountsStart()
            LoadXpoAccountsEnd()
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' Se ejecuta al darle clic al Botón INDSbGenerateReport para generar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            If (INDGleType.EditValue = 1) Then
                AsyncLoader(True)
                Dim reporte As New rptCertificateRetICA

                reporte.ParametrosReporte = {INDDateStart, INDDateEnd,
                                             INDSleAccountsStart.EditValue, INDSleAccountsEnd.EditValue,
                                             INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             INDCdNavigatorStart.GetYear, INDCdNavigatorStart.GetMonth,
                                             INDCdNavigatorEnd.GetYear, INDCdNavigatorEnd.GetMonth, INDsleBook.EditValue}

                INDDVReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If reporte.DataSource IsNot Nothing Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcReportViewer.Visible = True
                    INDDVReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDGleType.Focus()
                End If
            Else
                AsyncLoader(True)
                Dim reporte As New rptCertificateRetIVA

                'Dim INDDateStart As Date = New Date(INDCdNavigatorStart.GetYear, INDCdNavigatorStart.GetMonth, 1, 0, 0, 1)
                Dim INDDateStart As Date = New Date(INDCdNavigatorStart.GetYear, INDCdNavigatorStart.GetMonth, 1)
                Dim INDDateEnd As Date = New Date(INDCdNavigatorEnd.GetYear, INDCdNavigatorEnd.GetMonth, 1)

                INDDateEnd = (INDDateEnd.AddDays(-INDDateEnd.Day + 1).AddMonths(1).AddDays(-1))
                'INDDateEnd = New Date(INDDateEnd.Year, INDDateEnd.Month, INDDateEnd.Day, 23, 59, 59)

                reporte.ParametrosReporte = {INDDateStart, INDDateEnd,
                                             INDSleAccountsStart.EditValue, INDSleAccountsEnd.EditValue,
                                             INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             INDCdNavigatorStart.GetYear, INDCdNavigatorStart.GetMonth,
                                             INDCdNavigatorEnd.GetYear, INDCdNavigatorEnd.GetMonth, INDsleBook.EditValue}

                INDDVReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If reporte.DataSource IsNot Nothing Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcReportViewer.Visible = True
                    INDDVReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDGleType.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCtnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCtnReturn_ClickBack() Handles INDCtnReturn.ClickBack
        Me.INDPcReportViewer.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
    End Sub

    Private Async Sub INDSbExportExcel_Click(sender As Object, e As EventArgs) Handles INDSbExportExcel.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Taxes.MVP.MReports(Me.Tag)
                    Dim dtReport As DataTable = Nothing
                    Dim ds As DataSet = Nothing

                    If INDGleType.EditValue = 1 Then
                        ds = Await model.GetListReportCertificateRetICA(INDDateStart, INDDateEnd,
                                             INDSleAccountsStart.EditValue, INDSleAccountsEnd.EditValue,
                                             INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             3, INDsleBook.EditValue)
                        If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                            dtReport = ds.Tables("ReportCertificateRetICA")
                            Await Task.Factory.StartNew(Sub()
                                                            chargueDatasourceCertificateRetICA(dtReport)
                                                        End Sub)
                        End If
                    ElseIf INDGleType.EditValue = 2 Then
                        ds = Await model.GetListReportCertificateRetIVA(INDDateStart, INDDateEnd,
                                             INDSleAccountsStart.EditValue, INDSleAccountsEnd.EditValue,
                                             INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             2, INDsleBook.EditValue)
                        If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                            dtReport = ds.Tables("ReportCertificateRetIVA")
                            Await Task.Factory.StartNew(Sub()
                                                            chargueDatasourceCertificateRetIVA(dtReport)
                                                        End Sub)
                        End If
                    End If

                    If dtReport IsNot Nothing AndAlso Me.INDGcGenerateExcel.DataSource IsNot Nothing Then
                        If Me.INDGcGenerateExcel.DataSource IsNot Nothing Then
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
            bookXpcollection = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBookByStatusXpCollection, filter)
            INDsleBook.Properties.DataSource = bookXpcollection
        End Using
    End Sub

    ''' <summary>
    ''' Método para cargar el DataSource Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsStart()
        criteria = Nothing        
        If (INDGleType.EditValue = 1) Then
            criteria = "RetencionType = 3 AND Status = " & 1 & " AND LegalBookId.Id = " & INDsleBook.EditValue & ""
        Else
            criteria = "RetencionType = 2 AND Status = " & 1 & " AND LegalBookId.Id = " & INDsleBook.EditValue & ""
        End If
        ProoftCloseXpoAccounts = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).TaxesService.ListMainAccountsReportByFilter(criteria)
        INDSleAccountsStart.Datasource = ProoftCloseXpoAccounts
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsEnd()
        criteria = Nothing
        If (INDGleType.EditValue = 1) Then
            criteria = "RetencionType = 3 AND Status = " & True & " AND LegalBookId.Id = " & INDsleBook.EditValue
        Else
            criteria = "RetencionType = 2 AND Status = " & True & " AND LegalBookId.Id = " & INDsleBook.EditValue
        End If
        ProoftCloseXpoAccounts = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).TaxesService.ListMainAccountsReportByFilter(criteria)
            INDSleAccountsEnd.Datasource = ProoftCloseXpoAccounts
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
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
        Dim Validations As Boolean = True

        'Valida Año y Mes
        If INDCdNavigatorStart.GetYear > INDCdNavigatorEnd.GetYear Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblPeriod.Text)
            Me.INDCdNavigatorStart.Focus()
            Validations = False
        ElseIf INDCdNavigatorStart.GetYear = INDCdNavigatorEnd.GetYear Then
            If INDCdNavigatorStart.GetMonth > INDCdNavigatorEnd.GetMonth Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblPeriod.Text)
                Me.INDCdNavigatorStart.Focus()
                Validations = False
            End If
        End If

        'Valida Cuentas
        If INDSleAccountsStart.EditValue Is Nothing And INDSleAccountsEnd.EditValue IsNot Nothing Or INDSleAccountsEnd.EditValue Is Nothing And INDSleAccountsStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccounts.Text)
            Me.INDSleAccountsStart.Focus()
            Validations = False
        ElseIf INDSleAccountsEnd.EditValue < INDSleAccountsStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccounts.Text)
            Me.INDSleAccountsStart.Focus()
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

        If Validations Then
            INDDateStart = INDCdNavigatorStart.GetYear & "-" & INDCdNavigatorStart.GetMonth & "-01"
            INDDateEnd = INDCdNavigatorEnd.GetYear & "-" & INDCdNavigatorEnd.GetMonth & "-01"
            INDDateEnd = (INDDateEnd.AddDays(-INDDateEnd.Day + 1).AddMonths(1).AddDays(-1))
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel de reconocimientos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceCertificateRetICA(ByVal dtReport As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Nit")
        dt.Columns.Add("Tercero")
        dt.Columns.Add("Concepto de Retención")
        dt.Columns.Add("Ingresos Totales", GetType(Decimal))
        dt.Columns.Add("Base de Retención", GetType(Decimal))
        dt.Columns.Add("Valor Retenido", GetType(Decimal))

        For Each item In dtReport.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Nit") = item("Nit")
            row.Item("Tercero") = item("Tercero")
            row.Item("Concepto de Retención") = item("Concept")
            row.Item("Ingresos Totales") = item("ValorFacturado")
            row.Item("Base de Retención") = item("BaseRetención")
            row.Item("Valor Retenido") = item("ValorRetenido")
            dt.Rows.Add(row)
        Next

        INDGcGenerateExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de reconocimientos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceCertificateRetIVA(ByVal dtReportListDocumentIncome As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Nit")
        dt.Columns.Add("Tercero")
        dt.Columns.Add("Concepto de Retención")
        dt.Columns.Add("Ingresos Totales", GetType(Decimal))
        dt.Columns.Add("Base de Retención", GetType(Decimal))
        dt.Columns.Add("Valor Retenido", GetType(Decimal))

        For Each item In dtReportListDocumentIncome.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Nit") = item("Nit")
            row.Item("Tercero") = item("Tercero")
            row.Item("Concepto de Retención") = item("Concept")
            row.Item("Ingresos Totales") = item("ValorFacturado")
            row.Item("Base de Retención") = item("BaseRetención")
            row.Item("Valor Retenido") = item("ValorRetenido")
            dt.Rows.Add(row)
        Next

        INDGcGenerateExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcGenerateExcel
        If _gridView IsNot Nothing Then
            _gridView.MainView.PopulateColumns()
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleType.EditValue & ".xlsx"
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

End Class