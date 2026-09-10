#Region "Imports"

Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Reporter
Imports System.Text.RegularExpressions
#End Region

Public Class FrmReportListInvoicePay

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Datasource"

    Public Property ProoftCloseXpoInitialInvoice As XPInstantFeedbackSource
    Public Property ProoftCloseXpoFinalInvoice As XPInstantFeedbackSource
    Public Property ProoftCloseXpoHealthAdministrator As XPInstantFeedbackSource
    Public Property ProoftCloseXpoPatient As XPInstantFeedbackSource
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoInvoiceCategories As XPInstantFeedbackSource
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoAdmissionNumber As XPInstantFeedbackSource
    Public Property ProoftCloseXpoRadicated As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCreationUsers As XPInstantFeedbackSource

    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _LoadTypeReport
        End Get
    End Property

    Private _LoadTypeInvoice As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeInvoice As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeInvoice Is Nothing Then
                _LoadTypeInvoice = New List(Of Tuple(Of Integer, String))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(1, "Factura EAPB con Contrato"))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(2, "Factura EAPB sin Contrato"))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(3, "Factura Particular"))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(4, "Factura Capitada"))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(5, "Control De Capitación"))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(6, "Factura Básica"))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(7, "Factura de Venta de Productos"))
                _LoadTypeInvoice.Add(New Tuple(Of Integer, String)(0, "Todos"))
            End If
            Return _LoadTypeInvoice
        End Get
    End Property

    Private _LoadStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadStatus As List(Of Tuple(Of Integer, String))
        Get
            If _LoadStatus Is Nothing Then
                _LoadStatus = New List(Of Tuple(Of Integer, String))
                _LoadStatus.Add(New Tuple(Of Integer, String)(1, "Facturado"))
                _LoadStatus.Add(New Tuple(Of Integer, String)(2, "Anulado"))
                _LoadStatus.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _LoadStatus
        End Get
    End Property

    Private _LoadOrder As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadOrder As List(Of Tuple(Of Integer, String))
        Get
            If _LoadOrder Is Nothing Then
                _LoadOrder = New List(Of Tuple(Of Integer, String))
                _LoadOrder.Add(New Tuple(Of Integer, String)(1, "Número Factura"))
                _LoadOrder.Add(New Tuple(Of Integer, String)(2, "Fecha"))
                _LoadOrder.Add(New Tuple(Of Integer, String)(3, "Cliente"))
            End If
            Return _LoadOrder
        End Get
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True



        'Valida si la fecha inicial es mayor a la inicial
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            If Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
                Me.INDDateEnd.Focus()
                Validations = False
            End If
            'valida si alguna de las fechas tiene informacion  y el radiacado tiene datos ..para informar a obligar a completar las dos fechas
        ElseIf ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing)) Or ((INDDateStart.EditValue IsNot Nothing Or INDDateEnd.EditValue IsNot Nothing)) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function


#Region "Load Datasources"


    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePatient
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBillingPatient()
        Using msearch As New MBusqueda
            ProoftCloseXpoPatient = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllPatients)
            INDSlePatient.Datasource = ProoftCloseXpoPatient
        End Using
    End Sub



#End Region



#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmReportListInvoicePay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)

        Me.INDSlePatient.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetPattientByCode

    End Sub

    Private Sub FrmReportListInvoicePay_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        INDSlePatient.View.OptionsView.ShowGroupPanel = False

    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing

        ProoftCloseXpoPatient = Nothing


        _LoadTypeReport = Nothing
        _LoadTypeInvoice = Nothing
        _LoadStatus = Nothing
        _LoadOrder = Nothing
    End Sub

#End Region

#Region "QueryPopup"


    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSlePatient
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePatient_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePatient.QueryPopUp
        If INDSlePatient.Datasource Is Nothing Then
            LoadXpoBillingPatient()
        End If
    End Sub

#End Region

#Region "EditValueChanged"




#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbReportGenerate
    ''' </summary>
    ''' ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then

            AsyncLoader(True)
            Dim reporte As New rptListInvoicePay
            reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                        INDDateEnd.EditValue,
                                                        INDSlePatient.EditValue}
            INDDvReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
            End If
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBaseHome.Visible = False
                Me.INDCncReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If


        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBaseHome.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
    End Sub



#End Region

#End Region

End Class