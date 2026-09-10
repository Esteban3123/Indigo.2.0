#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.InteropCostRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP

#End Region

Public Class FrmReportGeneralProfitability

#Region "Properties"
    Public Property ProoftCloseXpoProductionCenter As XPInstantFeedbackSource
    Public Property ProoftCloseXpoProductionCenterByType As XPInstantFeedbackSource

    Private reporte As Object
    Private OperatAdmin As Integer

    Private _FillingTypeProductionCenter As List(Of Tuple(Of Integer, String))

    Private ReadOnly Property FillingTypeProductionCenter As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeProductionCenter Is Nothing Then
                _FillingTypeProductionCenter = New List(Of Tuple(Of Integer, String))
                _FillingTypeProductionCenter.Add(New Tuple(Of Integer, String)(1, "Operativo"))
                _FillingTypeProductionCenter.Add(New Tuple(Of Integer, String)(2, "Administrativo"))
                _FillingTypeProductionCenter.Add(New Tuple(Of Integer, String)(3, "Ambos"))
            End If
            Return _FillingTypeProductionCenter
        End Get
    End Property

    Private _FillingReportType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Integer, String))
                _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Estimación Primaria General"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Estimación Primaria Detallado"))
                '_FillingReportType.Add(New Tuple(Of Integer, String)(3, "Estimación Primaria Costo Primo"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(4, "Estimación Secundaria General"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(5, "Estimación Secundaria Detallado"))
            End If
            Return _FillingReportType
        End Get
    End Property

    ''' <summary>
    ''' Tipo de Centro
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingCenterType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingCenterType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingCenterType Is Nothing Then
                _FillingCenterType = New List(Of Tuple(Of Integer, String))
                _FillingCenterType.Add(New Tuple(Of Integer, String)(0, "Todos"))
                _FillingCenterType.Add(New Tuple(Of Integer, String)(1, "Operativo"))
                _FillingCenterType.Add(New Tuple(Of Integer, String)(2, "Administrativo"))
                _FillingCenterType.Add(New Tuple(Of Integer, String)(3, "Logístico"))
            End If
            Return _FillingCenterType
        End Get
    End Property


    Private _Fillingvisualization As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property Fillingvisualization As List(Of Tuple(Of Integer, String))
        Get
            If _Fillingvisualization Is Nothing Then
                _Fillingvisualization = New List(Of Tuple(Of Integer, String))
                _Fillingvisualization.Add(New Tuple(Of Integer, String)(1, "General"))
                _Fillingvisualization.Add(New Tuple(Of Integer, String)(2, "Grafica"))
            End If
            Return _Fillingvisualization
        End Get
    End Property
#End Region


#Region "Event"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ProoftCloseXpoProductionCenter = Nothing
        ProoftCloseXpoProductionCenterByType = Nothing
        reporte = Nothing
        OperatAdmin = Nothing
        _FillingCenterType = Nothing
        _FillingReportType = Nothing
        _FillingTypeProductionCenter = Nothing
        _Fillingvisualization = Nothing
    End Sub

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Me.Cursor = BaseClass.ChangeCursorIndigo
            If INDGleVisualization.EditValue = 1 Then
                Select Case INDGleTypeReport.EditValue
                    Case 1
                        reporte = New rptEstimatingPrimaryGeneralB
                    Case 2
                        reporte = New rptPrimaryEstimationDetailing
                        'Case 3
                        '    reporte = New rptPrimaryCostestimatePrimo
                    Case 4
                        reporte = New rptFinalEstimateGeneral
                    Case Else
                        reporte = New rptDetailedEstimateFinal
                End Select
            Else
                reporte = New rptGeneralProfitabilityGraph
            End If

            reporte.ParametrosReporte = New Object() {INDDnYearMonthStart.GetYear,
                                                      INDDnYearMonthStart.GetMonth,
                                                      INDDnYearMonthEnd.GetYear,
                                                      INDDnYearMonthEnd.GetMonth,
                                                      INDSleProductionCenterStart.TextEditValue,
                                                      INDSleProductionCenterEnd.TextEditValue,
                                                      INDGleCenterType.EditValue}

            INDDvReport.DocumentSource = reporte
            Await CType(reporte, IReportAsync).CargarDataSourceAsync
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDNcpReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                INDGleTypeReport.Focus()
            End If
        End If
    End Sub

    Private Sub FrmReportGeneralProfitability_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar GridLookUpEdit        
        INDGleTypeReport.Properties.DataSource = FillingReportType
        INDGleVisualization.Properties.DataSource = Fillingvisualization
        INDGleCenterType.Properties.DataSource = FillingCenterType

        'Dar un valor por defecto a los GridLookEdit        
        INDGleTypeReport.EditValue = 1
        INDGleVisualization.EditValue = 1
        INDGleCenterType.EditValue = 0
    End Sub
    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDNcpReport.Visible = True
        Me.INDGleTypeReport.Focus()
    End Sub

    ' <summary>
    ' se ejecuta en el evento querypopup del control INDSleProductionCenterStart
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    ' <remarks></remarks>
    Private Sub INDSleProductionCenterStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductionCenterStart.QueryPopUp
        If INDGleTypeReport.EditValue = 1 Or INDGleTypeReport.EditValue = 2 Or INDGleTypeReport.EditValue = 3 Then
            LoadXpoProductionCenterStart()
        Else
            LoadXpoProductionCenterByStatusAndCenterTypeStart()
        End If
    End Sub

    ' <summary>
    ' se ejecuta en el evento querypopup del control INDSleProductionCenterEnd
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    ' <remarks></remarks>
    Private Sub INDSleProductionCenterEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductionCenterEnd.QueryPopUp
        If INDGleTypeReport.EditValue = 1 Or INDGleTypeReport.EditValue = 2 Or INDGleTypeReport.EditValue = 3 Then
            LoadXpoProductionCenterEnd()
        Else
            LoadXpoProductionCenterByStatusAndCenterTypeEnd()
        End If
    End Sub
#End Region


#Region "Method"
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
    ''' metodo para Cargar el data source Del Control INDSleProductionCenter. Start y End
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoProductionCenterStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoProductionCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductionCenter)
            INDSleProductionCenterStart.Datasource = ProoftCloseXpoProductionCenter
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductionCenter. Start y End
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoProductionCenterEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoProductionCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductionCenter)
            INDSleProductionCenterEnd.Datasource = ProoftCloseXpoProductionCenter
        End Using
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductionCenter, según el Status y el CenterType.  Start y End
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoProductionCenterByStatusAndCenterTypeStart()
        ProoftCloseXpoProductionCenterByType = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatusAndCenterType(1, 1)
        INDSleProductionCenterStart.Datasource = ProoftCloseXpoProductionCenterByType
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductionCenter, según el Status y el CenterType.  Start y End
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoProductionCenterByStatusAndCenterTypeEnd()
        ProoftCloseXpoProductionCenterByType = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatusAndCenterType(1, 1)
        INDSleProductionCenterEnd.Datasource = ProoftCloseXpoProductionCenterByType
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        'Valida centro de producción 
        If (INDSleProductionCenterStart.EditValue IsNot Nothing And INDSleProductionCenterEnd.EditValue Is Nothing Or INDSleProductionCenterEnd.EditValue IsNot Nothing And INDSleProductionCenterStart.EditValue Is Nothing) Or (INDSleProductionCenterStart.TextEditValue <> String.Empty And INDSleProductionCenterEnd.TextEditValue = String.Empty Or INDSleProductionCenterEnd.TextEditValue <> String.Empty And INDSleProductionCenterStart.TextEditValue = String.Empty) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblProductionCenter.Text)
            Me.INDSleProductionCenterStart.Focus()
            Validations = False

        ElseIf INDSleProductionCenterEnd.TextEditValue < INDSleProductionCenterStart.TextEditValue Or INDSleProductionCenterEnd.EditValue < INDSleProductionCenterStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblProductionCenter.Text)
            Me.INDSleProductionCenterStart.Focus()
            Validations = False
        End If
        Return Validations
    End Function
#End Region

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 1 Or INDGleTypeReport.EditValue = 2 Or INDGleTypeReport.EditValue = 3 Then
            If OperatAdmin <> 1 Then
                INDSleProductionCenterStart.EditValue = Nothing
                INDSleProductionCenterEnd.EditValue = Nothing
                OperatAdmin = 1
            End If            
        Else
            If OperatAdmin <> 2 Then
                INDSleProductionCenterStart.EditValue = Nothing
                INDSleProductionCenterEnd.EditValue = Nothing
                OperatAdmin = 2
            End If            
        End If
    End Sub
End Class