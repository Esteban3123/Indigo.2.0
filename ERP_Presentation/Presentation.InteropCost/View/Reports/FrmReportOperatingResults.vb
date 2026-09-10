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

Imports Presentation.InteropCost.MVP
Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports System.Text
Imports Domain.Entities
Imports System.Drawing
Imports DevExpress.Utils.Menu
Imports System.ComponentModel
Imports DevExpress.Data.PLinq
Imports DevExpress.XtraEditors
Imports Infrastructure.Data.Xpo
Imports System.Windows
#End Region

Public Class FrmReportOperatingResults

#Region "Properties"

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PProductionCenter
    Public Property ProoftCloseXpoProductionCenter As XPInstantFeedbackSource

    Private reporte As Object

    Private _FillingSortBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingSortBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingSortBy Is Nothing Then
                _FillingSortBy = New List(Of Tuple(Of Integer, String))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(1, "Centro de Costo"))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(2, "Costo Total"))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(3, "Facturado"))
                '_FillingSortBy.Add(New Tuple(Of Integer, String)(4, "Diferencial"))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(5, "Porcentaje Margen"))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(6, "Utilidad"))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(7, "Margen de Contribución a la Utilidad"))
            End If
            Return _FillingSortBy
        End Get
    End Property

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Estructura Organizacional"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Centro de Producción"))
            End If
            Return _FillingTypeReport
        End Get
    End Property
#End Region

#Region "Event"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        ProoftCloseXpoProductionCenter = Nothing
        reporte = Nothing
        _FillingSortBy = Nothing
        _FillingTypeReport = Nothing
    End Sub

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then

            AsyncLoader(True)
            Me.Cursor = BaseClass.ChangeCursorIndigo

            If INDGleTypeReport.EditValue = 1 Then
                reporte = New rptOperatingResultsByCostCenterGrouped

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
            Else
                reporte = New rptOperatingResultsByProductionCenter

                reporte.ParametrosReporte = New Object() {INDDnYearMonthStart.GetYear,
                                                      INDDnYearMonthStart.GetMonth,
                                                      INDDnYearMonthEnd.GetYear,
                                                      INDDnYearMonthEnd.GetMonth,
                                                      INDSleProductionCenterStart.TextEditValue,
                                                      INDSleProductionCenterEnd.TextEditValue,
                                                      INDGleSortBy.EditValue}

                INDDvReport.DocumentSource = reporte
                Await CType(reporte, IReportAsync).CargarDataSourceAsync
                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                End If
                Me.Cursor = System.Windows.Forms.Cursors.Default
                AsyncLoader(False)
                If reporte.DataSource IsNot Nothing Then
                    Me.INDLcBase.Visible = False
                    Me.INDNcpReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    INDSleProductionCenterStart.Focus()
                End If
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
    Private Sub INDsleOrganizationalStructure_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleOrganizationalStructure.QueryPopUp
        If INDsleOrganizationalStructure.Properties.DataSource Is Nothing Then

            Using Model As New MBusqueda
                'Dim _structure As XPCollection = Model.ConsultarEntidades(eDataSource.ListOrganizationalStructureData)
                Dim _structure As XPCollection(Of InteropCostViewOrganizationalStructureOfCostsWithPCenterReportXpo) = Model.ConsultarEntidades(eDataSource.ListOrganizationalStructureWithProductionCenter)
                INDsleOrganizationalStructure.Properties.DataSource = _structure
            End Using

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

        'If INDsleOrganizationalStructure.EditValue Is Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDliOrganizationalStructure.Text)
        '    Me.INDsleOrganizationalStructure.Focus()
        '    Validations = False
        'End If

        If INDDnYearMonthStart.GetYear <> INDDnYearMonthEnd.GetYear Then
            Mensaje(EeventViewerImages.Advertencia) = "El año de la Fecha Inicial debe ser igual al año de la Fecha Final"
            Me.INDDnYearMonthStart.Focus()
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

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleOrganizationalStructure_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleOrganizationalStructure.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("1200", Nothing, True)
            _presenter.LoadStructure()
        End If
    End Sub
#End Region



    Private Sub FrmReportOperatingResults_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDGleSortBy.Properties.DataSource = FillingSortBy
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport

        'Dar un valor por defecto a los GridLookEdit     
        Me.INDGleSortBy.EditValue = 7
        Me.INDGleTypeReport.EditValue = 1
    End Sub

    ''' <summary>
    ''' Al seleccionar el tipo de Reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 1 Then
            INDliOrganizationalStructure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciGleSortBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleSortBy.EditValue = Nothing
        ElseIf INDGleTypeReport.EditValue = 2 Then
            INDliOrganizationalStructure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciGleSortBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDsleOrganizationalStructure.EditValue = Nothing
            INDGleSortBy.EditValue = 3
        End If
    End Sub
End Class