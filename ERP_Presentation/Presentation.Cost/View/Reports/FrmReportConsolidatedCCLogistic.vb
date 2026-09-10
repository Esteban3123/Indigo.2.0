#Region "Imports"
Imports Presentation.Reporter
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.BaseClass
Imports System.ComponentModel


Imports Presentation.Controls.MVP

Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.InteropCostRepository

Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns

Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform

Imports Domain.Base.Entities
Imports System.Text
Imports Domain.Entities
Imports System.Drawing
Imports DevExpress.Utils.Menu

Imports DevExpress.Data.PLinq
Imports DevExpress.XtraEditors
Imports Infrastructure.Data.Xpo
#End Region


Public Class FrmReportConsolidatedCCLogistic
#Region "Properties"
    Public Property ProoftCloseXpoProductionCenter As XPInstantFeedbackSource
    Public Property ProoftCloseXpoUnitOfMeasurement As XPInstantFeedbackSource

    Private reporte As Object


    Private _FillingReportType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Integer, String))
                '_FillingReportType.Add(New Tuple(Of Integer, String)(1, "Informe Por Días"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Informe Por Unidad de Medida"))
            End If
            Return _FillingReportType
        End Get
    End Property


    Private _FillingByOrganizationalStructure As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingByOrganizationalStructure As List(Of Tuple(Of Integer, String))
        Get
            If _FillingByOrganizationalStructure Is Nothing Then
                _FillingByOrganizationalStructure = New List(Of Tuple(Of Integer, String))
                _FillingByOrganizationalStructure.Add(New Tuple(Of Integer, String)(1, "Si"))
                _FillingByOrganizationalStructure.Add(New Tuple(Of Integer, String)(2, "No"))
            End If
            Return _FillingByOrganizationalStructure
        End Get
    End Property


    Private _FillingLevel As List(Of Tuple(Of Integer, Integer))
    Private ReadOnly Property FillingLevel As List(Of Tuple(Of Integer, Integer))
        Get
            If _FillingLevel Is Nothing Then
                _FillingLevel = New List(Of Tuple(Of Integer, Integer))
                _FillingLevel.Add(New Tuple(Of Integer, Integer)(0, 1))
                _FillingLevel.Add(New Tuple(Of Integer, Integer)(1, 2))
                _FillingLevel.Add(New Tuple(Of Integer, Integer)(2, 3))
            End If
            Return _FillingLevel
        End Get
    End Property

#End Region


#Region "Method"
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Me.Cursor = BaseClass.ChangeCursorIndigo

            'If INDGleTypeReport.EditValue = 1 Then

            '    reporte = New rptCostReportConsolidatedCCLogisticByDays
            'Else
            reporte = New rptCostReportConsolidatedCCLogisticBDynamicMeasurementUnit
            'End If

            reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                      INDDateEditDateEnd.EditValue,
                                                      INDSleLogisticProductionCenter.TextEditValue,
                                                      INDSleProductionCenterStart.TextEditValue,
                                                      INDSleProductionCenterEnd.TextEditValue,
                                                      INDSleUnitOfMeasurementStart.TextEditValue,
                                                      INDSleUnitOfMeasurementEnd.TextEditValue,
                                                      INDGleLevel.EditValue}

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
                INDSleProductionCenterStart.Focus()
            End If

        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductionCenterStart
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoProductionCenterStart()
        ProoftCloseXpoProductionCenter = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListProductionCenterByStatusAndCenterType(1, 1)
        INDSleProductionCenterStart.Datasource = ProoftCloseXpoProductionCenter
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductionCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoProductionCenterEnd()
        ProoftCloseXpoProductionCenter = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListProductionCenterByStatusAndCenterType(1, 1)
        INDSleProductionCenterEnd.Datasource = ProoftCloseXpoProductionCenter
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductionCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoProductionCenterLogistic()
        ProoftCloseXpoProductionCenter = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListProductionCenterByStatusAndCenterType(1, 3)
        INDSleLogisticProductionCenter.Datasource = ProoftCloseXpoProductionCenter
    End Sub


    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleUnitOfMeasurementStart
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoUnitOfMeasurementStart()
        ProoftCloseXpoUnitOfMeasurement = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.GetInventoryMeasurementUnitCCLogistic
        INDSleUnitOfMeasurementStart.Datasource = ProoftCloseXpoUnitOfMeasurement
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleUnitOfMeasurementEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoUnitOfMeasurementEnd()
        ProoftCloseXpoUnitOfMeasurement = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.GetInventoryMeasurementUnitCCLogistic
        INDSleUnitOfMeasurementEnd.Datasource = ProoftCloseXpoUnitOfMeasurement
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        'Valida Fechas
        If INDDateEditDateStart.EditValue Is Nothing Or INDDateEditDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateEditDateStart.EditValue > INDDateEditDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        End If

        'Valida centro de producción 
        If (INDSleProductionCenterStart.EditValue IsNot Nothing And INDSleProductionCenterEnd.EditValue Is Nothing Or INDSleProductionCenterEnd.EditValue IsNot Nothing And INDSleProductionCenterStart.EditValue Is Nothing) Or (INDSleProductionCenterStart.TextEditValue <> String.Empty And INDSleProductionCenterEnd.TextEditValue = String.Empty Or INDSleProductionCenterEnd.TextEditValue <> String.Empty And INDSleProductionCenterStart.TextEditValue = String.Empty) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblProductionCenter.Text)
            Me.INDSleProductionCenterStart.Focus()
            Validations = False

        ElseIf (INDSleProductionCenterEnd.TextEditValue < INDSleProductionCenterStart.TextEditValue) Or (INDSleProductionCenterEnd.EditValue < INDSleProductionCenterStart.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblProductionCenter.Text)
            Me.INDSleProductionCenterStart.Focus()
            Validations = False
        End If

        'Valida Unidad de Medida
        If (INDSleUnitOfMeasurementStart.EditValue IsNot Nothing And INDSleUnitOfMeasurementEnd.EditValue Is Nothing Or INDSleUnitOfMeasurementEnd.EditValue IsNot Nothing And INDSleUnitOfMeasurementStart.EditValue Is Nothing) Or (INDSleUnitOfMeasurementStart.TextEditValue <> String.Empty And INDSleUnitOfMeasurementEnd.TextEditValue = String.Empty Or INDSleUnitOfMeasurementEnd.TextEditValue <> String.Empty And INDSleUnitOfMeasurementStart.TextEditValue = String.Empty) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblProductionCenter.Text)
            Me.INDSleUnitOfMeasurementStart.Focus()
            Validations = False

        ElseIf (INDSleUnitOfMeasurementEnd.TextEditValue < INDSleUnitOfMeasurementStart.TextEditValue) Or (INDSleUnitOfMeasurementEnd.EditValue < INDSleUnitOfMeasurementStart.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblProductionCenter.Text)
            Me.INDSleUnitOfMeasurementStart.Focus()
            Validations = False
        End If

        'Valida centro de producción Logístico
        If (INDSleLogisticProductionCenter.EditValue Is Nothing) And (INDSleLogisticProductionCenter.TextEditValue = String.Empty) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDLciLogisticProductionCenter.Text)
            Me.INDSleLogisticProductionCenter.Focus()
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


#Region "Events"
    Private Sub INDSleProductionCenterStart_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProductionCenterStart.QueryPopUp
        If INDSleProductionCenterStart.EditValue Is Nothing Then
            LoadXpoProductionCenterStart()
        End If
    End Sub

    Private Sub INDSleProductionCenterEnd_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProductionCenterEnd.QueryPopUp
        If INDSleProductionCenterEnd.EditValue Is Nothing Then
            LoadXpoProductionCenterEnd()
        End If
    End Sub

    Private Sub INDSleLogisticProductionCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleLogisticProductionCenter.QueryPopUp
        If INDSleLogisticProductionCenter.EditValue Is Nothing Then
            LoadXpoProductionCenterLogistic()
        End If
    End Sub

    Private Sub INDSleUnitOfMeasurementStart_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleUnitOfMeasurementStart.QueryPopUp
        If INDSleUnitOfMeasurementStart.EditValue Is Nothing Then
            LoadXpoUnitOfMeasurementStart()
        End If
    End Sub

    Private Sub INDSleUnitOfMeasurementEnd_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleUnitOfMeasurementEnd.QueryPopUp
        If INDSleUnitOfMeasurementEnd.EditValue Is Nothing Then
            LoadXpoUnitOfMeasurementEnd()
        End If
    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDNcpReport.Visible = True
        Me.INDDateEditDateStart.Focus()
    End Sub

    Private Sub FrmReportConsolidatedCCLogistic_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit        
        INDGleTypeReport.Properties.DataSource = FillingReportType
        INDGleByOrganizationalStructure.Properties.DataSource = FillingByOrganizationalStructure
        INDGleLevel.Properties.DataSource = FillingLevel

        'Dar un valor por defecto a los GridLookEdit        
        INDGleTypeReport.EditValue = 2
        INDGleByOrganizationalStructure.EditValue = 2
        INDGleLevel.EditValue = 1
    End Sub

#End Region

    Private Sub INDGleByOrganizationalStructure_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleByOrganizationalStructure.EditValueChanged
        If INDGleByOrganizationalStructure.EditValue = 1 Then
            INDLciLevel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciLevel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

    End Sub

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        'If INDGleTypeReport.EditValue = 1 Then
        '    INDLciByOrganizationalStructure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'Else
        '    INDLciByOrganizationalStructure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '    INDLciLevel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'End If
    End Sub
End Class