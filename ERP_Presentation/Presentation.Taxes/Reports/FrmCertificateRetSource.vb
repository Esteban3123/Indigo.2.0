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

Public Class FrmCertificateRetSource

#Region "Variables"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

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
    Private Sub FrmCertificateRetSource_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)

        'Asignar funcion a evento keyenter a los searchlookupedit
        Me.INDSleAccountsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleAccountsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDTxtYear.EditValue = DateAndTime.Now.ToString("yyyy")

        LoadXpoBook()
        SetOfficialBook()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoAccounts = Nothing
        ProoftCloseXpoThirdParty = Nothing
        bookXpcollection = Nothing
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
    ''' se ejecuta en el evento querypopup del control INDSleAccountsStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountsStart.QueryPopUp
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
    Private Sub INDSleAccountsEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountsEnd.QueryPopUp
        If INDSleAccountsEnd.Datasource Is Nothing Then
            LoadXpoAccountsEnd()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBook.EditValueChanged
        If INDsleBook.EditValue IsNot Nothing Then
            LoadXpoAccountsStart()
            LoadXpoAccountsEnd()
        End If
    End Sub

#End Region

#Region "KeyPress"

    ''' <summary>
    ''' Se ejecuta para validar la entrada de sólo números en el control de Año Gravable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtYear_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles INDTxtYear.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
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
        If Me.ValidateControlsReports = True Then


            AsyncLoader(True)
            Dim reporte As New rptCertificateRetSource
            reporte.ParametrosReporte = {INDTxtYear.EditValue,
                                         INDSleAccountsStart.EditValue,
                                         INDSleAccountsEnd.EditValue,
                                         INDSleThirdPartyStart.EditValue,
                                         INDSleThirdPartyEnd.EditValue,
                                         INDsleBook.EditValue,
                                         InitialDate(INDCdnDate.GetMonth, INDCdnDate.GetYear),
                                         FinalDate(INDCdnDate1.GetMonth, INDCdnDate1.GetYear)}

            INDDVReport.DocumentSource = reporte
            Await reporte.CargarDataSource1()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcReportViewer.Visible = True
                INDDVReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDTxtYear.Focus()
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
                    Dim ds As DataSet = Await model.GetListReportCertificateRetSource(INDsleBook.EditValue, INDTxtYear.EditValue, INDSleAccountsStart.EditValue, INDSleAccountsEnd.EditValue, INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue, InitialDate(INDCdnDate.GetMonth, INDCdnDate.GetYear), FinalDate(INDCdnDate1.GetMonth, INDCdnDate1.GetYear))
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReport As DataTable = ds.Tables("ReportCertificateRetSource")
                        INDGcGenerateExcel.DataSource = dtReport
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
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, INDsleBook.EditValue}
            ProoftCloseXpoAccounts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookId, filter)
            INDSleAccountsStart.Datasource = ProoftCloseXpoAccounts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsEnd()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, INDsleBook.EditValue}
            ProoftCloseXpoAccounts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookId, filter)
            INDSleAccountsEnd.Datasource = ProoftCloseXpoAccounts
        End Using
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

        'Valida que el año sea de 4 dígitos
        If INDTxtYear.Text.Length < 4 Or INDTxtYear.Text Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateYear", "Commons"))
            Me.INDSleAccountsStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    Private Sub generateExcel()
        Dim _gridView = INDGvGenerateExcel
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

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Private Sub INDCdnDate_Load(sender As Object, e As EventArgs) Handles INDCdnDate.Load

    End Sub

    Private Sub LabelControl1_Click(sender As Object, e As EventArgs) Handles LabelControl1.Click

    End Sub
    ''' <summary>
    ''' Funcion para colocar la fecha inicial del filtro
    ''' </summary>
    ''' <param name="Month1"></param>
    ''' <param name="Year1"></param>
    ''' <returns></returns>
    Private Function InitialDate(Month1 As Integer, Year1 As Integer) As DateTime
        Dim InitialDate1 = New Date(Year1, Month1, 1)

        Return InitialDate1

    End Function
    ''' <summary>
    ''' Funcion para General la fecha final del filtro
    ''' </summary>
    ''' <param name="Month2"></param>
    ''' <param name="Year2"></param>
    ''' <returns></returns>
    Private Function FinalDate(Month2 As Integer, Year2 As Integer) As DateTime

        Dim Day As Integer = 30

        Select Case Month2
            Case 1, 3, 5, 7, 8, 10, 12
                Day = 31
            Case 2 'Verificamos si el año es bifiesto par asignar el dia
                If (Year2 Mod 4 = 0 And Year2 Mod 100 <> 0) Or (Year2 Mod 400 = 0) Then
                    Day = 29
                Else
                    Day = 28
                End If
        End Select

        Dim FinalDate1 As DateTime = New Date(Year2, Month2, Day)

        Return FinalDate1

    End Function
#End Region

End Class