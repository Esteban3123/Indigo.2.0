#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP

#End Region

Public Class FrmReportDeferredCausation

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoBill As XPInstantFeedbackSource
    Dim dateStart As Date
    Dim dateEnd As Date
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
        If dateStart = Nothing Or dateEnd = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDCdnStart.Focus()
            Validations = False
        ElseIf dateStart > dateEnd Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDCdnStart.Focus()
            Validations = False
        End If

        'Valida Cuentas Por Pagar (Facturas)
        If INDSleDocumentStart.EditValue Is Nothing And INDSleDocumentEnd.EditValue IsNot Nothing Or INDSleDocumentEnd.EditValue Is Nothing And INDSleDocumentStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblDocuments.Text)
            Me.INDSleDocumentStart.Focus()
            Validations = False
        ElseIf INDSleDocumentEnd.EditValue < INDSleDocumentStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblDocuments.Text)
            Me.INDSleDocumentStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsPayableStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoBill = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPaymentsAccountPayableReport)
            INDSleDocumentStart.Datasource = ProoftCloseXpoBill
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsPayableEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoBill = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPaymentsAccountPayableReport)
            INDSleDocumentEnd.Datasource = ProoftCloseXpoBill
        End Using
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleDocumentStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocumentStart.QueryPopUp
        If INDSleDocumentStart.Datasource Is Nothing Then
            LoadXpoAccountsPayableStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleDocumentEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocumentEnd.QueryPopUp
        If INDSleDocumentEnd.Datasource Is Nothing Then
            LoadXpoAccountsPayableEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        INDLcBase.Visible = True
        INDCncNavigation.Visible = True
        INDPcDocumentViewer.Visible = False
    End Sub


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
    End Sub
    ''' <summary>
    ''' se ejecuta al dar click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        dateStart = "01/" & INDCdnStart.GetMonth & "/" & INDCdnStart.GetYear
        dateEnd = "01/" & INDCdnEnd.GetMonth & "/" & INDCdnEnd.GetYear
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptReportDeferredCausation
            reporte.ParametrosReporte = New Object() {dateStart,
                                                      dateEnd,
                                                      INDSleDocumentStart.EditValue,
                                                      INDSleDocumentEnd.EditValue}
            INDDvDocumentViewer.DocumentSource = reporte
            reporte.CargarDataSource()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDCdnStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportDeferredCausation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleDocumentStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountsPayableByBillNumber
        Me.INDSleDocumentEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountsPayableByBillNumber
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

End Class