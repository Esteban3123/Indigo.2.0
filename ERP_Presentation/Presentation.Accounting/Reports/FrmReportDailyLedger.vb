#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports Presentation.Reporter
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports System.Globalization

#End Region

Public Class FrmReportDailyLedger

#Region "Properties"

    ''' <summary>
    ''' Propiedad que almacena la última fecha de cierre
    ''' </summary>
    ''' <returns></returns>
    Public Property INDLastClosingDate As Date

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para libros contables
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    ''' Lsita de tuplas que contiene tipos de reporte
    ''' </summary>
    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Permite acceder a una lista de tipos de reportes predefinidos (Comprobante, Cuenta, SubCuenta, Auxiliar) 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Comprobante"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Cuenta"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "SubCuenta"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(4, "Auxiliar"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    ''' <summary>
    ''' Lista de tuplas que contiene distintos tipos de reporte
    ''' </summary>
    Private _FillingTypeRepor2 As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Permite acceder a una lista de tipos de reportes (Informe Definitivo, Numeración) 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingTypeRepor2 As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeRepor2 Is Nothing Then
                _FillingTypeRepor2 = New List(Of Tuple(Of Integer, String))
                _FillingTypeRepor2.Add(New Tuple(Of Integer, String)(1, "Informe Definitivo"))
                _FillingTypeRepor2.Add(New Tuple(Of Integer, String)(2, "Numeración"))
            End If
            Return _FillingTypeRepor2
        End Get
    End Property

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

#Region "Methods"

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
    ''' Método para realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateStart.Focus()
            Validations = False
        End If
        Return Validations
    End Function

    ''' <summary>
    ''' se ejecuta al cambiar la fecha del control INDcdnPeriod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDcdnPeriod_OnChangeDate(sender As Object, e As EventArgs) Handles INDcdnPeriod.OnChangeDate
        'cuando el usuario cambie la fecha inicial si la fecha inicial es menor que la fecha del ultimo cierre hacer la fecha inicial igual que la fecha del ultimo cierre
        If INDcdnPeriod.GetYear <= Year(INDLastClosingDate) Then
            INDcdnPeriod.SetYear = Year(INDLastClosingDate)
            If INDcdnPeriod.GetMonth < Month(INDLastClosingDate) Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidatePeriodClosingReport", "Commons"), INDcdnPeriod.GetMonth, INDcdnPeriod.GetYear)
                INDcdnPeriod.SetMonth = Month(INDLastClosingDate)
            End If
        End If
        Dim DateStart As Date = "01-" & INDcdnPeriod.GetMonth & "-" & INDcdnPeriod.GetYear
        Dim DateEnd As Date = DateStart.AddMonths(1).AddDays(-1)

        INDDateStart.EditValue = DateStart
        INDDateStart.Properties.MinValue = DateStart
        INDDateStart.Properties.MaxValue = DateEnd

        INDDateEnd.EditValue = DateEnd
        INDDateEnd.Properties.MinValue = DateStart
        INDDateEnd.Properties.MaxValue = DateEnd
    End Sub

    ''' <summary>
    ''' carga el datasource de libros oficiales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleBook.QueryPopUp
        If INDsleBook.Properties.DataSource Is Nothing Then
            LoadXpoBook()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click al control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBaseHome.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptDailyLedger()
            reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                      INDDateEnd.EditValue,
                                                      INDGleTypeReport.EditValue,
                                                      INDGleTypeReport2.EditValue,
                                                      INDsleBook.EditValue,
                                                      INDTxtPrefix.EditValue,
                                                      INDTxtInitialConsecutive.EditValue}

            Dim DataSourceEmpty As Boolean = True
            If INDGleTypeReport.EditValue = 1 Then
                'se agrupa por tipos de comprobantes
                reporte.INDSrGroupJournalAccount.ReportSource.DataSource = reporte.FillListDailyLedger()
                If DirectCast(reporte.INDSrGroupJournalAccount.ReportSource.DataSource, ICollection).Count <= 0 Then
                    DataSourceEmpty = False
                Else
                    Dim currencyAbbreviation As String = reporte.INDSrGroupJournalAccount.ReportSource.DataSource(0)?.IdAccounting?.LegalBookId?.CommonCurrency?.Abbreviation
                    Dim currencyName As String = reporte.INDSrGroupJournalAccount.ReportSource.DataSource(0)?.IdAccounting?.LegalBookId?.CurrencyNameISO
                    Dim _culture As CultureInfo
                    If String.IsNullOrEmpty(currencyAbbreviation) Then
                        currencyAbbreviation = SessionValues.Instance.CurrencyISO4217
                        currencyName = currencyAbbreviation
                    End If
                    _culture = CultureInfo.CurrentCulture.Clone()
                    _culture.NumberFormat = New CultureInfo(currencyAbbreviation.GetCultureId).NumberFormat
                    reporte.ApplyLocalization(_culture)

                End If
            Else
                'se agrupa por cuentas
                reporte.INDSrGroupAccountJournal.ReportSource.DataSource = reporte.FillListDailyLedger()

                If DirectCast(reporte.INDSrGroupAccountJournal.ReportSource.DataSource, ICollection).Count <= 0 Then
                    DataSourceEmpty = False
                Else
                    Dim currencyAbbreviation As String = reporte.INDSrGroupAccountJournal.ReportSource.DataSource(0)?.IdAccounting?.LegalBookId?.CommonCurrency?.Abbreviation
                    Dim currencyName As String = reporte.INDSrGroupAccountJournal.ReportSource.DataSource(0)?.IdAccounting?.LegalBookId?.CurrencyNameISO
                    Dim _culture As CultureInfo
                    If String.IsNullOrEmpty(currencyAbbreviation) Then
                        currencyAbbreviation = SessionValues.Instance.CurrencyISO4217
                        currencyName = currencyAbbreviation
                    End If
                    _culture = CultureInfo.CurrentCulture.Clone()
                    _culture.NumberFormat = New CultureInfo(currencyAbbreviation.GetCultureId).NumberFormat
                    reporte.ApplyLocalization(_culture)
                End If
            End If

            INDDvDocumentViewer.DocumentSource = reporte
            reporte.CargarDataSource()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If DataSourceEmpty = True Then
                Me.INDLcBaseHome.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDcdnPeriod.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportDailyLedger_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleTypeReport2.Properties.DataSource = FillingTypeRepor2

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleTypeReport2.EditValue = 1

        Using msearch As New MBusqueda
            If msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate) = Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateExistencePeriodClosingReport", "Commons"))
                INDLastClosingDate = Date.Now
            Else
                INDLastClosingDate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate)
            End If
        End Using

        'cargar los campos date
        INDcdnPeriod.SetMonth = Month(INDLastClosingDate)
        INDcdnPeriod.SetYear = Year(INDLastClosingDate)

        Dim DateStart As Date = "01-" & INDcdnPeriod.GetMonth & "-" & INDcdnPeriod.GetYear
        Dim DateEnd As Date = DateStart.AddMonths(1).AddDays(-1)

        INDDateStart.EditValue = DateStart
        INDDateStart.Properties.MinValue = DateStart
        INDDateStart.Properties.MaxValue = DateEnd

        INDDateEnd.EditValue = DateEnd
        INDDateEnd.Properties.MinValue = DateStart
        INDDateEnd.Properties.MaxValue = DateEnd

        INDsleBook.Properties.Buttons(1).Visible = False
        LoadXpoBook()
        SetOfficialBook()

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
    ''' Actualiza los permisos de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' al cambiar el tipo de reporte se muestran los filtros de la paginacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeReport2_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport2.EditValueChanged
        If INDGleTypeReport2.EditValue = 1 Then
            INDLciTxtPrefix.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciTxtInitialConsecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDTxtPrefix.EditValue = Nothing
            INDTxtInitialConsecutive.EditValue = Nothing
        Else
            INDLciTxtPrefix.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciTxtInitialConsecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
#End Region
End Class