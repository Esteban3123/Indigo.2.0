#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportThirdBalance

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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
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

        _criterias = New Dictionary(Of String, String)
        _criterias.Add("Year", INDCdnPeriod.GetYear)
        _criterias.Add("Month", INDCdnPeriod.GetMonth)
        _criterias.Add("LegalBookId", INDsleBook.EditValue)
        _criterias.Add("AccountsZero", INDsleAccountsZero.EditValue)
        _criterias.Add("ThirdPartyStart", INDSleThirdPartyStart.EditValue)
        _criterias.Add("ThirdPartyEnd", INDSleThirdPartyEnd.EditValue)
        _criterias.Add("AccountStart", INDSleAccountStart.EditValue)
        _criterias.Add("AccountEnd", INDSleAccountEnd.EditValue)

        Return Validations
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
    ''' Esta función carga datos desde un DataTable de informe de terceros en un nuevo DataTable, luego establece el DataSource de un control llamado INDGcGenerateExcel con el nuevo DataTable para su posterior procesamiento o visualización.
    ''' </summary>
    ''' <param name="dtReportThirdPartyBalance"></param>
    Private Sub chargueDatasource(dtReportThirdPartyBalance As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("NIT Tercero")
        dt.Columns.Add("Tercero")
        dt.Columns.Add("Libro")
        dt.Columns.Add("Código Cuenta Contable")
        dt.Columns.Add("Cuenta Contable")
        dt.Columns.Add("Saldo Anterior", GetType(Decimal))
        dt.Columns.Add("Valor Débito", GetType(Decimal))
        dt.Columns.Add("Valor Crédito", GetType(Decimal))
        dt.Columns.Add("Nuevo Saldo", GetType(Decimal))

        For Each item As DataRow In dtReportThirdPartyBalance.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("NIT Tercero") = item("ThirdPartyNit")
            row.Item("Tercero") = item("ThirdPartyName")
            row.Item("Libro") = item("LegalBookName")
            row.Item("Código Cuenta Contable") = item("MainAccountCode")
            row.Item("Cuenta Contable") = item("MainAccountName")
            row.Item("Saldo Anterior") = item("PreviousBalance")
            row.Item("Valor Débito") = item("ValueDebitMovement")
            row.Item("Valor Crédito") = item("ValueCreditMovement")
            row.Item("Nuevo Saldo") = item("NewBalance")
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
    Private Sub FrmReportThirdBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode

        'Cargar los libros contables
        LoadXpoBook()

        'Dar un valor por defecto
        Me.INDsleAccountsZero.EditValue = 0
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
        bookXpcollection = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportThirdBalance_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'asignar la fecha del ultimo cierre a los controles date
        Using msearch As New MBusqueda
            If msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate) = Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateExistencePeriodClosingReport", "Commons"))
                INDLastClosingDate = Date.Now
            Else
                INDLastClosingDate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate)
            End If
        End Using

        INDCdnPeriod.SetMonth = Month(INDLastClosingDate)
        INDCdnPeriod.SetYear = Year(INDLastClosingDate)
    End Sub

#End Region

#Region "QueryPopUp"

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
    ''' se ejecuta en el evento querypopup del control INDSleAccountStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountStart.QueryPopUp
        If INDsleBook.EditValue IsNot Nothing AndAlso INDSleAccountStart.Datasource Is Nothing Then
            Using msearch As New MBusqueda
                Dim criteria = String.Format("LegalBookId = {0} AND AllowsMovement = 1 AND HandlesThirdParty = 1", INDsleBook.EditValue)
                INDSleAccountStart.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsReportByFilter, criteria)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypoup del control INDSleAccountEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountEnd.QueryPopUp
        If INDsleBook.EditValue IsNot Nothing AndAlso INDSleAccountEnd.Datasource Is Nothing Then
            Using msearch As New MBusqueda
                Dim criteria = String.Format("LegalBookId = {0} AND AllowsMovement = 1 AND HandlesThirdParty = 1", INDsleBook.EditValue)
                INDSleAccountEnd.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsReportByFilter, criteria)
            End Using
        End If
    End Sub

#End Region

#Region "OnChangeDate"

    ''' <summary>
    ''' se ejecuta cuando cambie la fecha en el control INDCdnPeriod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCdnPeriod_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnPeriod.OnChangeDate
        'cuando el usuario cambie la fecha inicial si la fecha inicial es menor que la fecha del ultimo cierre hacer la fecha inicial igual que la fecha del ultimo cierre
        If INDCdnPeriod.GetYear <= Year(INDLastClosingDate) Then
            INDCdnPeriod.SetYear = Year(INDLastClosingDate)
            If INDCdnPeriod.GetMonth < Month(INDLastClosingDate) Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidatePeriodClosingReport", "Commons"), INDCdnPeriod.GetMonth, INDCdnPeriod.GetYear)
                INDCdnPeriod.SetMonth = Month(INDLastClosingDate)
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' metodo que carga los datasource de la cuenta inicial y final al momento de cambiar el libro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBook.EditValueChanged
        INDSleAccountStart.EditValue = Nothing
        INDSleAccountEnd.EditValue = Nothing

        INDSleAccountStart.Datasource = Nothing
        INDSleAccountEnd.Datasource = Nothing
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)

                Dim reporte As New rptThirdBalance()
                reporte.ParametrosReporte = New Object() {_criterias}
                Await reporte.CargarDataSourceAsync()
                INDDvDocumentViewer.DocumentSource = reporte
                AsyncLoader(False)

                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBaseHome.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnPeriod.Focus()
                End If
            Catch ex As Exception
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBaseHome.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
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
                Dim ds = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetReportThirdPartyBalanceAsync(_criterias, Me.indigo)
                If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dim dtReportThirdPartyBalance As DataTable = ds.Tables("ReportThirdPartyBalance")
                    chargueDatasource(dtReportThirdPartyBalance)
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