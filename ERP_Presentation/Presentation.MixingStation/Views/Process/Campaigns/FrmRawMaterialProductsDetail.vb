'***********************************************************************
' Assembly         : Presentation.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 25/08/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmRawMaterialProductsDetail

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddProductsCampaignDetailArgs(sender As Object, e As AddProductsCampaignDetail)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PCampaigns

    ''' <summary>
    ''' Permite saber si se esta editando el detalle
    ''' </summary>
    Public EditModeDetail As Boolean

    ''' <summary>
    ''' Tabla de detalle 
    ''' </summary>
    Public ProductsCampaignDetail As CampaignDetailItems

#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property


    Private _campaignId As Integer
    ''' <summary>
    ''' Id de la cabecera de la campaña, viene desde el form principal
    ''' </summary>
    Public Property CampaignId As Integer
        Get
            Return _campaignId
        End Get
        Set(value As Integer)
            _campaignId = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa los combos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim ListComponentType = New List(Of Tuple(Of Byte, String))()
        ListComponentType.Add(New Tuple(Of Byte, String)(1, "Medicamento"))
        ListComponentType.Add(New Tuple(Of Byte, String)(2, "Insumo"))
        ListComponentType.Add(New Tuple(Of Byte, String)(3, "Producto"))
        INDsleType.Properties.DataSource = ListComponentType
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupPackageDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Visible = False
        Presenter = New PCampaigns()
        InitializeTuples()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmProductionBaskets_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleType.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega el producto al listado del paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        If Not ValidateControls() Then
            Exit Sub
        End If

        ProductsCampaignDetail = New CampaignDetailItems

        With ProductsCampaignDetail
            .CampaignDetailId = _campaignId

            .ProductId = Nothing
            If INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ProductId = INDsleProduct.EditValue
            End If

            .AtcId = Nothing
            If INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AtcId = INDsleATC.EditValue
            End If

            .SupplyId = Nothing
            If INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SupplyId = INDsleSupplie.EditValue
            End If

            .RequestQuantity = INDseQuantity.EditValue
            .QuantityStock = 0
            .ItemType = 4

            .CauseReprocessingRejectionId = CInt(INDSleReasonAdd.EditValue)
            .CauseReprocessingRejectionJustification = CStr(INDMeJustification.EditValue)
        End With

        Dim args As New AddProductsCampaignDetail
        args.ProductsCampaignDetail = ProductsCampaignDetail
        args.EditMode = EditModeDetail
        RaiseEvent AddProductsCampaignDetailArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup del listado de productos
    ''' </summary>
    Private Sub FrmProductionBaskets_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMedicine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATC.QueryPopUp
        If INDsleATC.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDsleATC.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListATC)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de insumos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSupplie_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplie.QueryPopUp
        If INDsleSupplie.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDsleSupplie.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListInventorySupplie)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProduct.QueryPopUp
        If INDsleProduct.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDsleProduct.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListInventoryProductByProductType, "4")
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de componente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If INDsleType.EditValue IsNot Nothing Then
            If INDsleType.EditValue = 1 Then 'Medicamento
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemATC.AllowHide = False
                INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSupplie.AllowHide = True
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemProduct.AllowHide = True
            ElseIf INDsleType.EditValue = 2 Then 'Insumo
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemATC.AllowHide = True
                INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemSupplie.AllowHide = False
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemProduct.AllowHide = True
            ElseIf INDsleType.EditValue = 3 Then 'Producto
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemATC.AllowHide = True
                INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSupplie.AllowHide = True
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemProduct.AllowHide = False
            End If
        End If
    End Sub

    Private Sub INDSleReasonAdd_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleReasonAdd.QueryPopUp
        If INDSleReasonAdd.Properties.DataSource Is Nothing Then
            INDSleReasonAdd.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.ListXPInstantFeedbackSource(Of CauseReprocessingRejectionXpo)("Status=True And Class = 3")
        End If
    End Sub
#End Region

#End Region

End Class

Public Class AddProductsCampaignDetail
    Inherits EventArgs

    Property ProductsCampaignDetail As CampaignDetailItems

    Property EditMode As Boolean

End Class