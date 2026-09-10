#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP

Imports Infrastructure.Data.Xpo

Imports DevExpress.Xpo.DB
Imports DevExpress.XtraGrid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.UserDesigner
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraReports.Parameters

#End Region

Public Class FrmReportAuditor

#Region "Properties"
    Private Property ProoftCloseXpoWarehouse As XPInstantFeedbackSource
    Private Property ProoftCloseXpoProducts As XPInstantFeedbackSource
    Private reporte As Object
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _FillingReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReport Is Nothing Then
                _FillingReport = New List(Of Tuple(Of Integer, String))
                _FillingReport.Add(New Tuple(Of Integer, String)(1, "Kardex vs Físico"))
                _FillingReport.Add(New Tuple(Of Integer, String)(2, "Inventario Contabilidad Resumido"))
                _FillingReport.Add(New Tuple(Of Integer, String)(3, "Documento Descuadrado Inventario vs Contabilidad"))
            End If
            Return _FillingReport
        End Get
    End Property

#End Region


#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ProoftCloseXpoProducts = Nothing
        ProoftCloseXpoWarehouse = Nothing
        _FillingReport = Nothing
    End Sub

    Private Sub FrmReportAuditor_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar GridLookUpEdit
        INDGleReport.Properties.DataSource = FillingReport
        'Dar un valor por defecto a los GridLookEdit
        INDGleReport.EditValue = 1
    End Sub
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReport() = True Then
            AsyncLoader(True)
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            Select Case INDGleReport.EditValue
                Case 1
                    If INDSleProductStart.TextEditValue <> "" Then
                        reporte = New rptKardexVsPhysicalByGroup
                    Else
                        reporte = New rptKardexVsPhysical
                    End If

                Case 2
                    reporte = New rptSummarizedInventoryAccounting
                Case Else
                    reporte = New rptDocumentsDisorganizedInventoryVsAccounting
            End Select

            reporte.ParametrosReporte = New Object() {INDDateEStart.EditValue,
                                                      INDDateEEnd.EditValue,
                                                      INDSleProductStart.TextEditValue,
                                                      INDSleProductEnd.TextEditValue,
                                                      LoadItemsSelect()}

            INDDvReport.DocumentSource = reporte
            'Await CType(reporte, IReportAsync).CargarDataSourceAsync()
            Await reporte.CargarDataSourceAsync

            'Valida si el datasource del reporte se retorna vacio y muestra el mensaje de error
            If INDGleReport.EditValue = 3 Then
                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    OcultarFormulario()
                Else
                    MensajeNohayDatos()
                End If
            Else
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                    OcultarFormulario()
                Else
                    MensajeNohayDatos()
                End If
            End If
            Me.Cursor = Cursors.Default
            AsyncLoader(False)
        End If
    End Sub

    Private Sub OcultarFormulario()
        Me.INDLcBase.Visible = False
        Me.INDCncReport.Visible = False
        Me.INDPcReport.Visible = True
        INDDvReport.Show()
    End Sub

    Private Sub MensajeNohayDatos()
        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
        If INDGleReport.EditValue = 1 Then
            INDCceWarehouse.Focus()
        Else
            Me.INDDateEStart.Focus()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSlueWarehouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCceWarehouse_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDCceWarehouse.QueryPopUp
        If Me.INDCceWarehouse.Properties.Items.Count < 1 Then
            LoadXpoWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductStart_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProductStart.QueryPopUp
        If INDSleProductStart.Datasource Is Nothing Then
            LoadXpoProductStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductEnd_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProductEnd.QueryPopUp
        If INDSleProductEnd.Datasource Is Nothing Then
            LoadXpoProductEnd()
        End If
    End Sub
    Private Sub INDGleReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReport.EditValueChanged
        If INDGleReport.EditValue = 1 Then 'Kardex VS Físico

            INDLciWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'INDLciProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciProductStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciProductEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            'INDLciDateE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDDateEStart.EditValue = Nothing
            INDDateEEnd.EditValue = Nothing

        ElseIf INDGleReport.EditValue = 2 Or 3 Then  'Inventario Contabilidad Resumido  O  Documento Descuadrado Inventario vs Contabilidad

            INDLciWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'INDLciProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciProductStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciProductEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleProductStart.EditValue = Nothing
            INDSleProductEnd.EditValue = Nothing
            INDCceWarehouse.EditValue = Nothing

            'INDLciDateE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        INDCceWarehouse.CheckAll()
    End Sub
    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
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
    Private Function ValidateControlsReport() As Boolean
        Dim Validations As Boolean = True

        If INDGleReport.EditValue = 1 Then
            'validaciones controles de Productos
            If INDSleProductStart.EditValue IsNot Nothing And INDSleProductEnd.EditValue Is Nothing Or INDSleProductStart.EditValue Is Nothing And INDSleProductEnd.EditValue IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), "Productos")
                Me.INDSleProductStart.Focus()
                Validations = False
            ElseIf INDSleProductStart.EditValue > INDSleProductEnd.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), "Productos")
                Me.INDSleProductStart.Focus()
                Validations = False
            End If
        ElseIf INDGleReport.EditValue = 2 OrElse INDGleReport.EditValue = 3 Then
            'validaciones controles de Fechas
            If INDDateEStart.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe indicar una fecha inicial"
                Me.INDDateEStart.Focus()
                Validations = False
            End If

            If INDDateEEnd.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe indicar una fecha final"
                Me.INDDateEEnd.Focus()
                Validations = False
            End If

            If Me.INDDateEStart.EditValue > INDDateEEnd.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
                Me.INDDateEStart.Focus()
                Validations = False
            End If

            If INDDateEStart.EditValue IsNot Nothing And INDDateEEnd.EditValue Is Nothing Or INDDateEStart.EditValue Is Nothing And INDDateEStart.EditValue IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
                Me.INDDateEStart.Focus()
                Validations = False
            End If
        End If

        Return Validations
    End Function

    '' <summary>
    '' metodo para Cargar el data source Del Control INDSlueWarehouseStart
    '' </summary>
    '' <remarks></remarks>
    Private Sub LoadXpoWarehouse()
        ''' <summary>
        ''' Variable para inicializar los valores de sesion
        ''' </summary>
        Dim IndigoSessionValues As SessionValues = SessionValues.Instance
        Dim listReport As XPCollection(Of InventoryWarehouseReportXpo) = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListWarehouseReportC()
        Dim j As Integer = 0
        For Each item In listReport
            Me.INDCceWarehouse.Properties.Items.Add(item.Code.ToString & " - " & item.Name.ToString, CheckState.Unchecked, True)            
            Me.INDCceWarehouse.Properties.Items.Item(j).Tag = Convert.ToInt32(item.Id.ToString)
            j = j + 1
        Next
    End Sub

    '' <summary>
    '' metodo para Cargar el data source Del Control INDSleProductStart
    '' </summary>
    '' <remarks></remarks>
    Private Sub LoadXpoProductStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoProducts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductsReport)
            INDSleProductStart.Datasource = ProoftCloseXpoProducts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProductEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoProducts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductsReport)
            INDSleProductEnd.Datasource = ProoftCloseXpoProducts
        End Using
    End Sub
    Private Function LoadItemsSelect() As List(Of Integer)
        Dim listWarehouse As New List(Of Integer)
        For i As Integer = 0 To Me.INDCceWarehouse.Properties.Items.Count - 1
            If Me.INDCceWarehouse.Properties.Items.Item(i).CheckState = CheckState.Checked Then
                listWarehouse.Add(Convert.ToInt32(Me.INDCceWarehouse.Properties.Items.Item(i).Tag.ToString))
            End If
        Next
        Return listWarehouse
    End Function
#End Region
    Private Sub INDSleProductStart_Load(sender As Object, e As EventArgs) Handles INDSleProductStart.Load
        If INDCceWarehouse.Properties.DataSource Is Nothing Then
            LoadXpoWarehouse()
        End If

        INDCceWarehouse.CheckAll()
    End Sub
End Class