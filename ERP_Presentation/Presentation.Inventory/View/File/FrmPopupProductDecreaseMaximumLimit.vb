'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Cristian Camilo Fierro R.
' Created          : 2023-06-08
'
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Drawing
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Inventory
Imports Presentation.Inventory.MVP
Public Class FrmPopupProductDecreaseMaximumLimit

#Region "Properties and variables"
    ''' <summary>
    ''' Ubicación del mouse al dar click
    ''' </summary>
    Private _mouseLocation As Drawing.Point
    ''' <summary>
    ''' Almacén de origen
    ''' </summary>
    ''' <returns></returns>
    Public Property _SourceProducts As ViewListConsignmentWarehouseProductsXpo
    ''' <summary>
    ''' Indica si se estan cargando los controles o no
    ''' </summary>
    Private _isLoading As Boolean = False
    ''' <summary>
    ''' detalle a editar
    ''' </summary>
    Private _decreaseMaximumLimit As ViewListConsignmentWarehouseProductsXpo = Nothing

    ''' <summary>
    ''' 
    ''' </summary>
    Private _SourceProduct As DocumentInvoiceProductSalesDetail = Nothing

    ''' <summary>
    ''' Evento ejecutado para guardar los cambios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="consignmentDetail"></param>
    Public Event OnAddConsignmentProduct(sender As Object, consignmentDetail As ViewListConsignmentWarehouseProductsXpo)
    ''' <summary>
    ''' Mensaje
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
    Public Property DecreaseQuantity As Integer
        Get
            Return INDSpnDecreaseQuantity.EditValue
        End Get
        Set(value As Integer)
            INDSpnDecreaseQuantity.EditValue = value
        End Set
    End Property

    Public Property Justification As String
        Get
            Return INDMeDescription.Text
        End Get
        Set(value As String)
            INDMeDescription.Text = value
        End Set
    End Property
#End Region
#Region "Functions and Methods"
    ''' <summary>
    ''' Carga los controles para hacer el decremento del limite maximo del producto
    ''' </summary>
    ''' <param name="_SourceProduct"></param>
    Public Async Sub EditDetail(_SourceProduct As ViewListConsignmentWarehouseProductsXpo)
        Try
            AsyncLoader(True)
            _isLoading = True
            INDPceProducts.Text = _SourceProducts.CodeName
        Finally
            _isLoading = False
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssigningValues()
        Dim QuantityMaxUpdate = _SourceProducts.QuantityMax - DecreaseQuantity
        If _decreaseMaximumLimit Is Nothing Then _decreaseMaximumLimit = New ViewListConsignmentWarehouseProductsXpo()
        With _decreaseMaximumLimit

            .DecreaseQuantity = DecreaseQuantity
            .Justificaton = Justification
            .ProductId = _SourceProducts.ProductId
            .WarehouseId = _SourceProducts.WarehouseId
            .QuantityMax = QuantityMaxUpdate
        End With
    End Sub

    ''' <summary>
    ''' Clean Controls
    ''' </summary>
    Public Sub CleanControls()
        _isLoading = False
        INDPceProducts.Properties.ReadOnly = False
        INDSpnDecreaseQuantity.EditValue = Nothing
        INDMeDescription.EditValue = Nothing
        INDSpnDecreaseQuantity.EditValue = 0
        INDPceProducts.Focus()
        _decreaseMaximumLimit = Nothing
        _SourceProduct = Nothing

    End Sub
#End Region

#Region "Handlers"
    ''' <summary>
    ''' Agrega los cambios a la entidad para guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        If Not ValidateControls() Then Return
        If DecreaseQuantity <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a ingresar debe ser mayor a cero"
            Exit Sub
        End If
        AssigningValues()
        RaiseEvent OnAddConsignmentProduct(Me, _decreaseMaximumLimit)
        CleanControls()
        Close()
    End Sub

#End Region

    ''' <summary>
    ''' Keydown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupProductConsignmentTransfer_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupProductConsignmentTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.StatusRecordVisible = False
        CleanControls()
    End Sub

    ''' <summary>
    ''' deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSpnDeliveryQuantity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSpnDecreaseQuantity.ButtonClick
        Dim control As Windows.Forms.Control = sender
    End Sub

    ''' <summary>
    ''' Mouse click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSpnDeliveryQuantity_MouseClick(sender As Object, e As Windows.Forms.MouseEventArgs) Handles INDSpnDecreaseQuantity.MouseClick
        _mouseLocation = e.Location
    End Sub

    Private Sub INDSpnDecreaseQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSpnDecreaseQuantity.EditValueChanged
        Dim value = _SourceProducts.QuantityMax - _SourceProducts.QuantityKardex
        If DecreaseQuantity < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se permite numeros menores de cero"
            DecreaseQuantity = 0
            Exit Sub
        ElseIf DecreaseQuantity > value Then
            Mensaje(EeventViewerImages.Advertencia) = "Límite máximo no puede ser menor al saldo en Kardex"
            DecreaseQuantity = 0
        End If
    End Sub
End Class