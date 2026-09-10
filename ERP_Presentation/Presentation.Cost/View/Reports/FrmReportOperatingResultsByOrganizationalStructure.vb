#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CostRepository
#End Region

Public Class FrmReportOperatingResultsByOrganizationalStructure

#Region "Properties"

    ' ''' <summary>
    ' ''' variable que contiene el presentador
    ' ''' </summary>
    'Dim _presenter As PProductionCenter
    Public Property ProoftCloseXpoProductionCenter As XPInstantFeedbackSource

    Private _FillingReportType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Integer, String))
                _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Grafica Estadística"))
            End If
            Return _FillingReportType
        End Get
    End Property

    Dim reporte As Object

#End Region

#Region "Event"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ProoftCloseXpoProductionCenter = Nothing
        _FillingReportType = Nothing
        reporte = Nothing
    End Sub

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Me.Cursor = BaseClass.ChangeCursorIndigo

            Select Case INDGleTypeReport.EditValue
                Case 1
                    reporte = New rptCostOperatingResultsByOrganizationalStructure
                Case Else
                    reporte = New rptCostOperatingResultsByOrganizationalStatisticalGraphics
            End Select

            reporte.ParametrosReporte = New Object() {INDDnYearMonthStart.GetYear,
                                                      INDDnYearMonthStart.GetMonth,
                                                      INDDnYearMonthEnd.GetYear,
                                                      INDDnYearMonthEnd.GetMonth,
                                                      INDSleProductionCenterStart.TextEditValue,
                                                      INDSleProductionCenterEnd.TextEditValue,
                                                      INDsleOrganizationalStructure.EditValue}

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
    Private Sub INDSleProductionCenterStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductionCenterStart.QueryPopUp
        If INDSleProductionCenterStart.EditValue Is Nothing Then
            LoadXpoProductionCenterStart()
        End If
    End Sub
    Private Sub INDSleProductionCenterEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductionCenterEnd.QueryPopUp
        If INDSleProductionCenterEnd.EditValue Is Nothing Then
            LoadXpoProductionCenterEnd()
        End If
    End Sub
    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDNcpReport.Visible = True
        Me.INDsleOrganizationalStructure.Focus()
    End Sub
    Private Sub FrmReportOperatingResultsByOrganizationalStructure_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit        
        INDGleTypeReport.Properties.DataSource = FillingReportType

        'Dar un valor por defecto a los GridLookEdit        
        INDGleTypeReport.EditValue = 1
    End Sub
    Private Sub INDsleOrganizationalStructure_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleOrganizationalStructure.QueryPopUp
          If INDsleOrganizationalStructure.Properties.DataSource Is Nothing Then
            Dim _structure As XPCollection = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListOrganizationalStructureDataCost()
            If _structure IsNot Nothing Then
                OrganizationalStructureDatasourse = New List(Of Domain.Entities.CostOrganizationalStructureOfCosts)()
                For Each itemXpo As Infrastructure.Data.Xpo.CostRepository.CostOrganizationalStructureOfCostsXpo In _structure
                    Dim _item As New CostOrganizationalStructureOfCosts()
                    With _item
                        .Id = itemXpo.Id
                        .Code = itemXpo.Code
                        .Name = itemXpo.Name
                        If itemXpo.ParentId IsNot Nothing Then
                            .ParentId = itemXpo.ParentId.Id
                        End If
                        .Status = itemXpo.Status
                        .MarkAsModified()
                    End With
                    OrganizationalStructureDatasourse.Add(_item)
                Next
            End If
        End If
    End Sub
#End Region


#Region "Method"
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

        ElseIf (INDSleProductionCenterEnd.TextEditValue < INDSleProductionCenterStart.TextEditValue) Or (INDSleProductionCenterEnd.EditValue < INDSleProductionCenterStart.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblProductionCenter.Text)
            Me.INDSleProductionCenterStart.Focus()
            Validations = False
        End If

        If INDsleOrganizationalStructure.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDliOrganizationalStructure.Text)
            Me.INDsleOrganizationalStructure.Focus()
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

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductionCenter. Start y End
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoProductionCenterStart()
        ProoftCloseXpoProductionCenter = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListProductionCenterReport
        INDSleProductionCenterStart.Datasource = ProoftCloseXpoProductionCenter
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductionCenter. Start y End
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoProductionCenterEnd()
        ProoftCloseXpoProductionCenter = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListProductionCenterReport
        INDSleProductionCenterEnd.Datasource = ProoftCloseXpoProductionCenter
    End Sub


    ''' <summary>
    ''' Aqui se controla que solo se pueda seleccionar los nodos de ultimo nivel
    ''' </summary>
    Private Sub INDsleOrganizationalStructure_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleOrganizationalStructure.EditValueChanging
        'If Me.INDsleOrganizationalStructure.Properties.DataSource IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
        '    Dim res As Boolean = Me.OrganizationalStructureDatasourse.Any(Function(o) o.ParentId.HasValue AndAlso o.ParentId.Value = CInt(e.NewValue))
        '    If res Then
        '        e.Cancel = True
        '    End If
        'End If
    End Sub
#End Region


#Region "Datasource"

    ''' <summary>
    ''' Gets or sets the organizational structure datasourse.
    ''' </summary>
    Public Property OrganizationalStructureDatasourse As List(Of CostOrganizationalStructureOfCosts)
        Get
            Return CType(INDsleOrganizationalStructure.Properties.DataSource, List(Of CostOrganizationalStructureOfCosts))
        End Get
        Set(value As List(Of CostOrganizationalStructureOfCosts))
            INDsleOrganizationalStructure.Properties.DataSource = value
        End Set
    End Property
#End Region


#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleOrganizationalStructure_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleOrganizationalStructure.ButtonClick
        'If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
        '    OpenForm("1200", Nothing, True)
        '    _presenter.LoadStructure()
        'End If
    End Sub
#End Region

End Class