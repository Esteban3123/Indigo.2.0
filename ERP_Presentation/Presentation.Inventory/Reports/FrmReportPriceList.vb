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

#End Region

Public Class FrmReportPriceList

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoProducts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoSubGroup As XPInstantFeedbackSource

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Precio Venta"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Tarifa"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

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

        'validaciones controles de producto
        If INDSleProductsStart.EditValue IsNot Nothing And INDSleProductsEnd.EditValue Is Nothing Or INDSleProductsStart.EditValue Is Nothing And INDSleProductsEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblProducts.Text)
            Me.INDSleProductsStart.Focus()
            Validations = False
        ElseIf INDSleProductsStart.EditValue > INDSleProductsEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblProducts.Text)
            Me.INDSleProductsStart.Focus()
            Validations = False
        End If

        'validaciones controles de grupos
        If INDSleGroupsStart.EditValue IsNot Nothing And INDSleGroupsEnd.EditValue Is Nothing Or INDSleGroupsStart.EditValue Is Nothing And INDSleGroupsEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroups.Text)
            Me.INDSleGroupsStart.Focus()
            Validations = False
        ElseIf INDSleGroupsStart.EditValue > INDSleGroupsEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroups.Text)
            Me.INDSleGroupsStart.Focus()
            Validations = False
        End If

        'validaciones controles de subgrupos
        If INDSleSubGroupsStart.EditValue IsNot Nothing And INDSleSubGroupsEnd.EditValue Is Nothing Or INDSleSubGroupsStart.EditValue Is Nothing And INDSleSubGroupsEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblSubGroups.Text)
            Me.INDSleSubGroupsStart.Focus()
            Validations = False
        ElseIf INDSleSubGroupsStart.EditValue > INDSleSubGroupsEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblSubGroups.Text)
            Me.INDSleSubGroupsStart.Focus()
            Validations = False
        End If

        'validaciones controles de filtros - tipo de tarifa
        If INDSleRateTypeStart.EditValue IsNot Nothing And INDSleRateTypeFinish.EditValue Is Nothing Or INDSleRateTypeStart.EditValue Is Nothing And INDSleRateTypeFinish.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblRateType.Text)
            Me.INDSleRateTypeStart.Focus()
            Validations = False
        ElseIf INDSleRateTypeStart.EditValue > INDSleRateTypeFinish.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblRateType.Text)
            Me.INDSleRateTypeFinish.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProductsStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoProducts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductsReport)
            INDSleProductsStart.Datasource = ProoftCloseXpoProducts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProductsEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoProducts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductsReport)
            INDSleProductsEnd.Datasource = ProoftCloseXpoProducts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoGroupStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListGroupReport)
            INDSleGroupsStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoGroupEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListGroupReport)
            INDSleGroupsEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSubGroupStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSubGroupStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSubGroupReport)
            INDSleSubGroupsStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSubGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSubGroupEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSubGroupReport)
            INDSleSubGroupsEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleRateTypeStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoRateTypeStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductRateByIdName)
            INDSleRateTypeStart.Datasource = ProoftCloseXpoGroup

        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleRateTypeFinish
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoRateTypeFinsih()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductRateByIdName)
            INDSleRateTypeFinish.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)

            
            If INDGlePriceType.EditValue = 1 Then

                Dim reporte As New rptPriceList

                reporte.ParametrosReporte = New Object() {INDSleGroupsStart.EditValue,
                                                          INDSleGroupsEnd.EditValue,
                                                          INDSleSubGroupsStart.EditValue,
                                                          INDSleSubGroupsEnd.EditValue,
                                                          INDSleProductsStart.EditValue,
                                                          INDSleProductsEnd.EditValue,
                                                          INDGlePriceType.EditValue}
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
                    Me.INDSleGroupsStart.Focus()
                End If

            ElseIf INDGlePriceType.EditValue = 2 Then

                Dim reporte As New rptPriceListRate

                reporte.ParametrosReporte = New Object() {INDSleGroupsStart.EditValue,
                                                          INDSleGroupsEnd.EditValue,
                                                          INDSleSubGroupsStart.EditValue,
                                                          INDSleSubGroupsEnd.EditValue,
                                                          INDSleProductsStart.EditValue,
                                                          INDSleProductsEnd.EditValue,
                                                          INDGlePriceType.EditValue,
                                                          INDSleRateTypeStart.EditValue,
                                                          INDSleRateTypeFinish.EditValue}
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
                    Me.INDSleGroupsStart.Focus()
                End If

            End If

            
            
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleGroupStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupsStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupsStart.QueryPopUp
        If INDSleGroupsStart.Datasource Is Nothing Then
            LoadXpoGroupStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleGroupEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupsEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupsEnd.QueryPopUp
        If INDSleGroupsEnd.Datasource Is Nothing Then
            LoadXpoGroupEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleSubGroupStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSubGroupsStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSubGroupsStart.QueryPopUp
        If INDSleSubGroupsStart.Datasource Is Nothing Then
            LoadXpoSubGroupStart()
        End If
    End Sub

   

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub INDSleProductsStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductsStart.QueryPopUp
        If INDSleProductsStart.Datasource Is Nothing Then
            LoadXpoProductsStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductsEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductsEnd.QueryPopUp
        If INDSleProductsEnd.Datasource Is Nothing Then
            LoadXpoProductsEnd()
        End If
    End Sub



    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoProducts = Nothing
        ProoftCloseXpoSubGroup = Nothing
        _FillingTypeReport = Nothing
    End Sub




    Private Sub FrmReportPriceList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        INDSleGroupsStart.Focus()

        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleGroupsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetProductGroup
        Me.INDSleGroupsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetProductGroup
        Me.INDSleSubGroupsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetProductSubGroup
        Me.INDSleSubGroupsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetProductSubGroup
        Me.INDSleProductsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetInventoryProduct
        Me.INDSleProductsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetInventoryProduct

        INDSleGroupsStart.View.OptionsView.ShowGroupPanel = False
        INDSleGroupsEnd.View.OptionsView.ShowGroupPanel = False
        INDSleSubGroupsStart.View.OptionsView.ShowGroupPanel = False
        INDSleSubGroupsEnd.View.OptionsView.ShowGroupPanel = False
        INDSleProductsStart.View.OptionsView.ShowGroupPanel = False
        INDSleProductsEnd.View.OptionsView.ShowGroupPanel = False

    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
    End Sub


    ''' <summary>
    ''' se ejecuta al cargar el formulario FrmReportPriceList
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub FrmReportPriceList_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        'Cargar GridLookUpEdit
        INDGlePriceType.Properties.DataSource = FillingTypeReport
        'Dar un valor por defecto a los GridLookEdit
        INDGlePriceType.EditValue = 1


        

    End Sub

    

    
    ''' <summary>
    ''' se ejecuta al cambiar el valor del Grid LookUp Edit del tipo de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDGlePriceType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlePriceType.EditValueChanged


        'Oculta los filtros del tipo de tarifa
        If INDGlePriceType.EditValue = 1 Then
            INDLciLabelRateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRateTypeStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRateTypeFinish.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgFilters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        End If

        'muestra los filtros del tipo de tarifa
        If INDGlePriceType.EditValue = 2 Then
            INDSleRateTypeStart.EditValue = Nothing
            INDSleRateTypeFinish.EditValue = Nothing
            INDLciLabelRateType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciRateTypeStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciRateTypeFinish.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgFilters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If



    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleRateTypeStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    
    Private Sub INDSleRateTypeStart_QueryPopUp1(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRateTypeStart.QueryPopUp
        If INDSleRateTypeStart.Datasource Is Nothing Then
            LoadXpoRateTypeStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleRateTypeFinish
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    
    Private Sub INDSleRateTypeFinish_QueryPopUp1(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRateTypeFinish.QueryPopUp
        If INDSleRateTypeFinish.Datasource Is Nothing Then
            LoadXpoRateTypeFinsih()
        End If
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleSubGroupEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    
    Private Sub INDSleSubGroupsEnd_QueryPopUp1(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSubGroupsEnd.QueryPopUp
        If INDSleSubGroupsEnd.Datasource Is Nothing Then
            LoadXpoSubGroupEnd()
        End If
    End Sub


End Class