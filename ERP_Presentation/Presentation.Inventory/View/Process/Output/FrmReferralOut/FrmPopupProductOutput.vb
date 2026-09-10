'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Ernesto Córdoba
' Created          : 16-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports System.Drawing
Imports Presentation.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Inventory.MVP
Imports Infrastructure.CrossCutting.Resources
Imports System.Text

#End Region

Public Class FrmPopupProductOutput

#Region "EVENTS"
    ''' <summary>
    ''' evento para retornar la entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddRemissionOutputDetail(sender As Object, e As AddRemissionOutputDetailEventArgs)
#End Region

#Region "TUPLE"
    Dim _listVariationType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListVariationType As List(Of Tuple(Of Byte, String))
        Get
            If _listVariationType Is Nothing Then
                _listVariationType = New List(Of Tuple(Of Byte, String))
                _listVariationType.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("Increase")))
                _listVariationType.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("Discount")))
            End If
            Return _listVariationType
        End Get
    End Property
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"
    ''' <summary>
    ''' entidad de producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim product As InventoryProduct
    ''' <summary>
    ''' id del almacen 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _wareHouseId As Integer
    ''' <summary>
    ''' id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _operatingUnitId As Integer
    ''' <summary>
    ''' listado del inventario fisico
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPhysicalInventory As List(Of PhysicalInventory)
    ''' <summary>
    ''' entidad del detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim remissionOutputDetail As RemissionOutputDetail
    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean
    ''' <summary>
    ''' bandera para saber si se ejecuta el calculo del valor de la venta
    ''' </summary>
    ''' <remarks></remarks>
    Dim _runSetSaleValue As Boolean = True
#End Region

#Region "PROPERTIES"
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
    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property WareHouseId As Integer
        Set(value As Integer)
            _wareHouseId = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para establecer el id de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property operatingUnitId As Integer
        Set(value As Integer)
            _operatingUnitId = value
        End Set
    End Property

    WriteOnly Property ActionOnControls As Boolean
        Set(value As Boolean)
            INDMeDescription.Enabled = value
            INDPceQuantity.Enabled = value
            INDMeDetail.Enabled = value
            INDTxtOriginalValue.Enabled = value
            INDTxtEndValue.Enabled = value
            INDGleApply.Enabled = value
            INDSePercentage.Enabled = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property RemissionOutputDetailEdit As RemissionOutputDetail
        Set(value As RemissionOutputDetail)
            remissionOutputDetail = value
        End Set
    End Property
#End Region

#Region "METHODS"
    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 730)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    Private Sub CleanControls()
        CtrPhysicalInventory1.CleanControls()
        INDPceProduct.Text = String.Empty
        INDPceProduct.Properties.ReadOnly = False
        INDTxtUnitSale.Text = String.Empty
        INDMeDescription.Text = String.Empty
        INDPceQuantity.EditValue = Nothing
        INDMeDetail.Text = String.Empty
        INDTxtOriginalValue.EditValue = 0
        INDTxtEndValue.EditValue = 0
        INDGleApply.EditValue = 1
        INDSePercentage.EditValue = 0
        INDTxtSaleTotal.EditValue = 0
        product = Nothing
        listPhysicalInventory = Nothing
        _editMode = False
        _runSetSaleValue = True
        ActionOnControls = False
        INDBtnAdd.Text = ResourceManager.GetString("Add")
        INDPceProduct.Focus()
    End Sub

    Private Sub LoadControls()
        INDBtnAdd.Text = ResourceManager.GetString("Edit")
        With remissionOutputDetail
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(.ProductId)
                INDPceProduct.Text = product.Code + " - " + product.Name
            End Using
            INDPceProduct.Properties.ReadOnly = True
            INDTxtUnitSale.Text = product.PackingUnitDescription
            INDMeDescription.Text = .ProductDescription
            If .RemissionOutputDetailPhysical.Count = 0 Then
                INDPceQuantity.EditValue = String.Empty
            Else
                INDPceQuantity.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), .RemissionOutputDetailPhysical.Count.ToString(), .RemissionOutputDetailPhysical.Sum(Function(x) x.Quantity).ToString())
            End If
            CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
            CtrPhysicalInventory1.Product = product
            CtrPhysicalInventory1.WareHouseId = _wareHouseId
            CtrPhysicalInventory1.FormOwner = Me
            CtrPhysicalInventory1.SetListPhysicalInventory()
            CtrPhysicalInventory1.SetQuantityPhysicalInventory(.RemissionOutputDetailPhysical.ToList())
            INDMeDetail.Text = .DetailDescription
            INDTxtOriginalValue.EditValue = .SalesPriceOriginal
            _runSetSaleValue = False
            INDTxtEndValue.EditValue = .SalePrice
            INDGleApply.EditValue = .VariationType
            INDSePercentage.EditValue = .PercentageVariation
            INDTxtSaleTotal.EditValue = .SalePriceWithDiscount
            ActionOnControls = True
            _runSetSaleValue = True
            INDPceProduct.Focus()
        End With
    End Sub

    Private Sub SetSaleValue()
        If _runSetSaleValue = True Then
            Dim percentage = INDTxtEndValue.EditValue * INDSePercentage.EditValue / 100
            If INDGleApply.EditValue = 1 Then '1 aumento - 2 descuento
                INDTxtSaleTotal.EditValue = INDTxtEndValue.EditValue + percentage
            Else
                INDTxtSaleTotal.EditValue = INDTxtEndValue.EditValue - percentage
            End If
        End If
    End Sub

    Private Function ValidationControlsPopup() As String
        Dim errors As New StringBuilder
        If product Is Nothing Then
            errors.AppendLine(INDLciProduct.Text + ResourceManager.GetString("Empty"))
        End If
        If INDMeDescription.Text Is String.Empty Then
            errors.AppendLine(INDLciDescription.Text + ResourceManager.GetString("Empty"))
        End If
        If INDPceQuantity.EditValue Is Nothing Then
            errors.AppendLine(INDLciQuantity.Text + ResourceManager.GetString("Empty"))
        End If
        If INDTxtEndValue.EditValue = 0 Then
            errors.AppendLine(INDLciEndValue.Text + ResourceManager.GetString("Empty"))
        End If
        Return errors.ToString()
    End Function

    Private Function SetValues()
        If _editMode = False Then
            remissionOutputDetail = New RemissionOutputDetail
        End If
        With remissionOutputDetail
            .ProductId = product.Id
            .ProductDescription = INDMeDescription.Text
            .CodeNameProduct = product.Code + " - " + product.Name
            If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count > 0 Then
                .Quantity = listPhysicalInventory.Sum(Function(x) x.QuantityDeliver)
            End If
            .consumptionUnit = product.PackingUnitDescription
            .VariationType = INDGleApply.EditValue
            .PercentageVariation = INDSePercentage.EditValue
            .SalePrice = INDTxtEndValue.EditValue
            .SalesPriceOriginal = INDTxtOriginalValue.EditValue
            .DetailDescription = INDMeDetail.Text
            .SalePriceWithDiscount = INDTxtSaleTotal.EditValue
            .TotalPriceWithDiscount = .SalePriceWithDiscount * .Quantity
            If _editMode = True Then
                If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count > 0 Then
                    While .RemissionOutputDetailPhysical.Count > 0
                        If .RemissionOutputDetailPhysical(0).Id > 0 Then
                            .RemissionOutputDetailPhysical(0).MarkAsDeleted()
                        Else
                            .RemissionOutputDetailPhysical.Remove(.RemissionOutputDetailPhysical(0))
                        End If
                    End While
                End If
            End If
            If listPhysicalInventory IsNot Nothing Then
                For Each item In listPhysicalInventory
                    Dim remissionOutputDetailPhysical As New RemissionOutputDetailPhysical
                    remissionOutputDetailPhysical.PhysicalInventoryId = item.Id
                    remissionOutputDetailPhysical.Quantity = item.QuantityDeliver
                    remissionOutputDetailPhysical.OutstandingQuantity = item.QuantityDeliver
                    remissionOutputDetailPhysical.CodeBatchSerial = item.CodeNameBatchSerial
                    remissionOutputDetailPhysical.CodeNameWarehouse = item.CodeNameWarehouse
                    .RemissionOutputDetailPhysical.Add(remissionOutputDetailPhysical)
                Next
            End If
        End With
    End Function

    ''' <summary>
    ''' Lanza la consulta del producto para poderlo seleccionar
    ''' sin necesidad de desplegar el control de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AssignProductWithCode() As Task
        If INDPceProduct.Text.Trim <> String.Empty Then
            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct As String() = INDPceProduct.Text.Split(" - ")
            'Capturo el codigo del producto
            Dim codeProduct As String = arrayCodeProduct(0).Trim


            Using model As New MInventoryProduct(Me.Tag)
                'Consulto el producto para armar el objeto SelectProductEventArgs
                Dim resultProduct = Await model.GetInventoryProduct(codeProduct)
                If resultProduct.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = resultProduct.MessageResult(0)
                    CleanControls()
                    Exit Function
                End If
                'Valido que exista el producto
                If resultProduct.ObjectEmbbeded IsNot Nothing AndAlso resultProduct.ObjectEmbbeded.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto con código {0} no existe.", codeProduct)
                    CleanControls()
                    Exit Function
                End If
                'Valido que el producto no este inactivo
                If resultProduct.ObjectEmbbeded.Status = False Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name + " está inactivo")
                    CleanControls()
                    Exit Function
                End If
                'Instancio el objeto SelectProductEventArgs
                Dim obj As New SelectProductEventArgs
                obj.ProductId = resultProduct.ObjectEmbbeded.Id
                obj.CodeNameProduct = resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name
                'Invoco la funcion que realiza el resto de la logica
                CtrProducts1_SelectProduct(Nothing, obj)
            End Using
        ElseIf INDPceProduct.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDPceProduct.Text = product.Code + " - " + product.Name
            End If
        End If
    End Function
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        product = Nothing
        _wareHouseId = Nothing
        _operatingUnitId = Nothing
        listPhysicalInventory = Nothing
        remissionOutputDetail = Nothing
        _editMode = Nothing
        _runSetSaleValue = Nothing
    End Sub

    Private Sub FrmPopupProductOutput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGleApply.Properties.DataSource = ListVariationType
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = False
        If _editMode = True Then
            LoadControls()
        Else
            CleanControls()
        End If
    End Sub
#End Region

#Region "Activated"
    Private Sub FrmPopupProductOutput_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If product Is Nothing Then
            INDPceProduct.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmPopupProductOutput_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "SelectProduct"
    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProduct.Text = e.CodeNameProduct
        INDPceProduct.Focus()
        INDPceProduct.ClosePopup()
        CtrPhysicalInventory1.CleanControls()
        INDPceQuantity.EditValue = Nothing
        Using model As New MInventoryProduct(Me.Tag)
            product = model.GetInventoryProductByIdSimple(e.ProductId)
            INDTxtUnitSale.Text = product.PackingUnitDescription
            INDMeDescription.Text = product.Description
            INDTxtOriginalValue.EditValue = product.SellingPrice
            INDTxtEndValue.EditValue = product.SellingPrice
            INDPceQuantity.Focus()
        End Using
        ActionOnControls = True
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDPceProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenFormDialog(form)
            End Using
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDPceProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProduct.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

    Private Sub INDPceQuantity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceQuantity.QueryPopUp
        CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
        CtrPhysicalInventory1.Product = product
        CtrPhysicalInventory1.WareHouseId = _wareHouseId
        CtrPhysicalInventory1.FormOwner = Me
        CtrPhysicalInventory1.SetListPhysicalInventory()
    End Sub
#End Region

#Region "KeyDown"
    Private Sub INDSePercentage_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDSePercentage.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

    Private Sub INDMeDescription_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeDescription.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDPceQuantity.Focus()
        End If
    End Sub

    Private Sub INDMeDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDTxtEndValue.Focus()
        End If
    End Sub

    Private Sub FrmPopupProductOutput_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Async Sub INDPceProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProduct.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub
#End Region

#Region "Closed"
    Private Sub INDPceQuantity_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceQuantity.Closed
        listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
        If listPhysicalInventory.Count > 0 Then

            INDPceQuantity.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), listPhysicalInventory.Count.ToString(), listPhysicalInventory.Sum(Function(x) x.QuantityDeliver).ToString())
        Else
            INDPceQuantity.EditValue = Nothing
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDTxtEndValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtEndValue.EditValueChanged
        SetSaleValue()
    End Sub

    Private Sub INDGleApply_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleApply.EditValueChanged
        SetSaleValue()
    End Sub

    Private Sub INDSePercentage_EditValueChanged(sender As Object, e As EventArgs) Handles INDSePercentage.EditValueChanged
        SetSaleValue()
    End Sub
#End Region

#Region "Click"
    Private Async Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Try
            INDBtnAdd.Enabled = False
            Dim errors = ValidationControlsPopup()
            If errors.Length > 0 Then
                INDBtnAdd.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            SetValues()
            'validacion stock de producto
            Using modelSettings As New MSettingInventory(Me.Tag)
                Dim resulValidateStock = Await modelSettings.ValidateStock(remissionOutputDetail.ProductId, _operatingUnitId, _wareHouseId, remissionOutputDetail.Quantity, InventoryStaticServices.MovementType.OutPut)
                If resulValidateStock.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = resulValidateStock.Message
                    Exit Sub
                End If
            End Using
            Dim args As New AddRemissionOutputDetailEventArgs
            If _editMode = True Then
                args.EditMode = True
            End If
            args.RemissionOutputDetail = remissionOutputDetail
            RaiseEvent AddRemissionOutputDetail(Nothing, args)
            CleanControls()
            INDBtnAdd.Enabled = True
        Catch ex As Exception
            INDBtnAdd.Enabled = True
            Throw ex
        End Try
        
    End Sub
#End Region

#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region

End Class