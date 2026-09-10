#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
#End Region

Public Class FrmReportDeteriorationIndication

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Public Property bookXpcollection As XPCollection
    Public Property ProoftCloseXpoPlate As XPInstantFeedbackSource
    Public Property ProoftCloseXpoMainAccount As XPInstantFeedbackSource

    Private LegalBookId As Integer

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'Valida Tipo de Reporte
        If INDGleTypeReport.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(INDLciGleTypeReport.Text, ResourceManager.GetString("Empty"))
            Me.INDGleTypeReport.Focus()
            Validations = False
        End If

        'Valida Placas
        If INDSlePlateStart.EditValue Is Nothing And INDSlePlateEnd.EditValue IsNot Nothing Or INDSlePlateEnd.EditValue Is Nothing And INDSlePlateStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblPlate.Text)
            Me.INDSlePlateStart.Focus()
            Validations = False
        ElseIf INDSlePlateEnd.EditValue < INDSlePlateStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblPlate.Text)
            Me.INDSlePlateStart.Focus()
            Validations = False
        End If

        'Valida Cuentas
        If INDSleInitialMainAccount.EditValue Is Nothing And INDSleFinalMainAccount.EditValue IsNot Nothing Or INDSleFinalMainAccount.EditValue Is Nothing And INDSleInitialMainAccount.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblMainAccount.Text)
            Me.INDSleInitialMainAccount.Focus()
            Validations = False
        ElseIf INDSleFinalMainAccount.EditValue < INDSleInitialMainAccount.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblMainAccount.Text)
            Me.INDSleInitialMainAccount.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

#Region "Eventos"

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportDeteriorationIndication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport

        'Dar un valor por defecto a los GridLookEdit
        INDGleTypeReport.EditValue = 1

        INDSlePlateStart.View.OptionsView.ShowGroupPanel = False
        INDSlePlateEnd.View.OptionsView.ShowGroupPanel = False
        INDSleInitialMainAccount.View.OptionsView.ShowGroupPanel = False
        INDSleFinalMainAccount.View.OptionsView.ShowGroupPanel = False

        LoadXpoBook()
        SetOfficialBook()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        bookXpcollection = Nothing
        ProoftCloseXpoPlate = Nothing
        ProoftCloseXpoMainAccount = Nothing
    End Sub

#Region "QueryPopUp"

    ''' <summary>
    ''' cargamos el datasource de Placa Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePlateStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePlateStart.QueryPopUp
        If INDSlePlateStart.Datasource Is Nothing Then
            LoadXpoPlateStart()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de Placa Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePlateEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePlateEnd.QueryPopUp
        If INDSlePlateEnd.Datasource Is Nothing Then
            LoadXpoPlateEnd()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de cuenta Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleInitialMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialMainAccount.QueryPopUp
        If INDSleInitialMainAccount.Datasource Is Nothing Then
            LoadXpoInitialMainAccount()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de cuenta Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleFinalMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalMainAccount.QueryPopUp
        If INDSleFinalMainAccount.Datasource Is Nothing Then
            LoadXpoFinalMainAccount()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)

            Dim reporte As Object
            If INDGleTypeReport.EditValue = 1 Then
                reporte = New rptDeteriorationIndicationsByPhysicalAsset
            Else
                reporte = New rptDeteriorationIndicationsByPhysicalAssetSummarized
            End If

            reporte.ParametrosReporte = New Object() {INDDnMonthYear.GetYear,
                                                      INDDnMonthYear.GetMonth,
                                                      INDGleTypeReport.EditValue,
                                                      INDSlePlateStart.EditValue,
                                                      INDSlePlateEnd.EditValue,
                                                      LegalBookId,
                                                      INDSleInitialMainAccount.EditValue,
                                                      INDSleFinalMainAccount.EditValue}

            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDnMonthYear.Focus()
            End If
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports = True Then
            Await chargueDatasource()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
        Me.INDDnMonthYear.Focus()
    End Sub

#End Region

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
                LegalBookId = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePlateStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoPlateStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoPlate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            INDSlePlateStart.Datasource = ProoftCloseXpoPlate
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePlateEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoPlateEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoPlate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            INDSlePlateEnd.Datasource = ProoftCloseXpoPlate
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleInitialMainAccount
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialMainAccount()
        Using msearch As New MBusqueda
            If LegalBookId > 0 Then
                Dim filter() As Object = {1, LegalBookId}
                ProoftCloseXpoMainAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetMainAccountsByTypeAndByBook, filter)
                INDSleInitialMainAccount.Datasource = ProoftCloseXpoMainAccount
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFinalMainAccount
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalMainAccount()
        Using msearch As New MBusqueda
            If LegalBookId > 0 Then
                Dim filter() As Object = {1, LegalBookId}
                ProoftCloseXpoMainAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetMainAccountsByTypeAndByBook, filter)
                INDSleFinalMainAccount.Datasource = ProoftCloseXpoMainAccount
            End If
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function chargueDatasource() As Task
        AsyncLoader(True)
        Dim dtReport As DataTable
        Try
            Dim IndList = Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetListReportDeteriorationIndicationsByPhysicalAssetAsync(INDDnMonthYear.GetYear,
                                                      INDDnMonthYear.GetMonth,
                                                      INDGleTypeReport.EditValue,
                                                      INDSlePlateStart.EditValue,
                                                      INDSlePlateEnd.EditValue,
                                                      LegalBookId,
                                                      INDSleInitialMainAccount.EditValue,
                                                      INDSleFinalMainAccount.EditValue,
                                                      Me.IndigoSessionValues)
            If IndList.Tables(0).Rows.Count > 0 Then
                dtReport = IndList.Tables("DeteriorationIndicationsByPhysicalAsset")
                INDGcExportExcel.DataSource = dtReport
                If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                    generateExcel()
                End If
            Else
                INDGcExportExcel.DataSource = Nothing
            End If
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

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

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region
End Class