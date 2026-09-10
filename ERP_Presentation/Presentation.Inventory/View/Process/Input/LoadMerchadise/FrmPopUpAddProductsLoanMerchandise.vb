'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 23/12/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Presentation.Inventory.MVP
Imports Presentation.Base
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Drawing
Imports System.Text
Imports Domain.Base.Entities


Public Class FrmPopUpAddProductsLoanMerchandise

#Region "Events"

    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddProductLoanMerchandiseDetail(sender As Object, e As AddProductLoanMerchandiseDetailEventArgs)

#End Region

#Region "Constantes"

    Const MODULE_NAME As String = "Inventory"

#End Region

#Region "variables"
    ''' <summary>
    ''' variable para saber si el prestamo  es una entrada o salida
    ''' </summary>
    ''' <remarks></remarks>
    Dim _loanType As ELeanType

    ''' <summary>
    ''' id del almacen 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _wareHouseId As Integer

    ''' <summary>
    ''' Objeto detalle de un prestamo de mercancia
    ''' </summary>
    ''' <remarks></remarks>
    Dim LoanMerchandiseDetail As LoanMerchandiseDetail

    ''' <summary>
    ''' Obtiene o establece el producto seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim product As InventoryProduct
    ''' <summary>
    ''' listado de los lotes
    ''' </summary>
    ''' <remarks></remarks>
    Dim listBatchSerial As List(Of BatchSerial)

    ''' <summary>
    ''' listado del inventario fisico
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPhysicalInventory As List(Of PhysicalInventory)

    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean

    ''' <summary>
    ''' listado del detalla de solictud de prestamo para validar que los productos no se repitan
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listLoanMerchandiseDetailValidation As List(Of LoanMerchandiseDetail)

#End Region

#Region "propiedades"

    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            INDSeQuantity.Enabled = value
            INDPceBatchSerialInput.Enabled = value
            INDPceBatchSerialOutput.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' propiedad para asignar el tipo de solicitud de prestamo, entrada o salida
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property LoanType As ELeanType
        Set(value As ELeanType)
            _loanType = value
            If value = ELeanType.Input Then
                CtrBatchSerial1.RemissionType = ELeanType.Input
                CtrBatchSerial1.WarehouseId = _wareHouseId
            Else
                CtrPhysicalInventory1.MovementType = ELeanType.Output
                CtrPhysicalInventory1.WareHouseId = _wareHouseId
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
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property LoanMerchandiseDetailEdit As LoanMerchandiseDetail
        Set(value As LoanMerchandiseDetail)
            LoanMerchandiseDetail = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar la presentación del producto
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PresentationProduct As String
        Set(value As String)
            INDtxtPresentation.Text = value
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
    ''' propiedad para para pasar el listado del detalla de la remision
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListLoanMerchandiseDetailValidation As List(Of LoanMerchandiseDetail)
        Set(value As List(Of LoanMerchandiseDetail))
            If value IsNot Nothing Then
                _listLoanMerchandiseDetailValidation = New List(Of LoanMerchandiseDetail)(value.ToArray())
            End If
        End Set
    End Property

#End Region

#Region "Methods"

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

    ''' <summary>
    ''' Limpiar Controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        ActionsControls = False

        INDPceProduct.Text = String.Empty
        INDPceProduct.Properties.ReadOnly = False
        INDtxtPresentation.Text = String.Empty
        INDSeQuantity.EditValue = 1
        INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDPceBatchSerialInput.EditValue = Nothing
        INDPceBatchSerialOutput.EditValue = Nothing
        CtrBatchSerial1.CleanControls()
        CtrPhysicalInventory1.CleanControls()

        _editMode = False
        product = Nothing
        listBatchSerial = Nothing
        listPhysicalInventory = Nothing

        INDPceProduct.Focus()
        BarraBotones.FilterDataSource = Nothing
    End Sub

    ''' <summary>
    ''' Carga de datos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        If _editMode = True Then
            INDBtnAdd.Text = ResourceManager.GetString("Edit")
        End If
        With LoanMerchandiseDetail
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(.ProductId)
                INDPceProduct.Text = product.Code + " - " + product.Name
                INDPceProduct.Properties.ReadOnly = True
                INDtxtPresentation.Text = product.Presentation
                INDSeQuantity.EditValue = .Quantity
            End Using
            Using model As New MSubGroup(Me.Tag)
                Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
                If subGroup.ObjectEmbbeded.HandlesBatch = True Then
                    INDSeQuantity.Properties.ReadOnly = True
                    If _loanType = ELeanType.Input Then
                        INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        If .LoanMerchandiseDetailBatchSerial.Count = 0 Then
                            INDPceBatchSerialInput.EditValue = String.Empty
                        Else
                            INDPceBatchSerialInput.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", MODULE_NAME), .LoanMerchandiseDetailBatchSerial.Count.ToString())
                        End If
                        CtrBatchSerial1.Product = product
                        CtrBatchSerial1.FormOwner = Me
                        CtrBatchSerial1.SetListBatchSerial()
                        CtrBatchSerial1.SetQuantityBatchSerial(.LoanMerchandiseDetailBatchSerial.ToList())
                        listBatchSerial = CtrBatchSerial1.GetListBatchSerial()
                    ElseIf INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        If .LoanMerchandiseDetailBatchSerial.Count = 0 Then
                            INDPceBatchSerialOutput.EditValue = String.Empty
                        Else
                            INDPceBatchSerialOutput.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", MODULE_NAME), .LoanMerchandiseDetailBatchSerial.Count.ToString())
                        End If
                        CtrPhysicalInventory1.Product = product
                        CtrPhysicalInventory1.FormOwner = Me
                        CtrPhysicalInventory1.SetListPhysicalInventory()
                        CtrPhysicalInventory1.SetQuantityPhysicalInventory(.LoanMerchandiseDetailBatchSerial.ToList())
                        listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
                    End If
                Else
                    INDSeQuantity.Properties.ReadOnly = False
                    INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End Using
        End With
    End Sub

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If product Is Nothing Then
            errors.AppendLine(INDLciProduct.Text + ResourceManager.GetString("Empty"))
        End If
        If _editMode = False Then
            'valido que no se agregue el mismo producto 
            If product IsNot Nothing Then
                If _listLoanMerchandiseDetailValidation IsNot Nothing AndAlso _listLoanMerchandiseDetailValidation.Count > 0 Then
                    Dim remissionEntranceDetailTmp = _listLoanMerchandiseDetailValidation.Find(Function(x) x.ProductId = product.Id)
                    If remissionEntranceDetailTmp IsNot Nothing Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("SourceProductNone", MODULE_NAME), remissionEntranceDetailTmp.CodeNameProduct))
                    End If
                End If
            End If
        End If
        If INDSeQuantity.EditValue Is Nothing OrElse INDSeQuantity.EditValue = 0 Then
            errors.AppendLine(INDLciQuantity.Text + ResourceManager.GetString("Empty"))
        End If
        If INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If listPhysicalInventory Is Nothing OrElse listPhysicalInventory.Count = 0 Then
                errors.AppendLine(INDLciBatchSerialOutput.Text + ResourceManager.GetString("Empty"))
            End If
        End If
        If INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If listBatchSerial Is Nothing OrElse listBatchSerial.Count = 0 Then
                errors.AppendLine(INDLciBatchSerialInput.Text + ResourceManager.GetString("Empty"))
            End If
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        If _editMode = False Then
            LoanMerchandiseDetail = New LoanMerchandiseDetail
        End If

        With LoanMerchandiseDetail
            .ProductId = product.Id
            .CodeNameProduct = product.Code + " - " + product.Name
            .Code = product.Code
            .Quantity = INDSeQuantity.EditValue
            .OutstandingQuantity = INDSeQuantity.EditValue
            .UnitValue = product.FinalProductCost

            'Eliminamos los detalles previos
            While .LoanMerchandiseDetailBatchSerial.Count > 0
                If .LoanMerchandiseDetailBatchSerial(0).Id > 0 Then
                    .LoanMerchandiseDetailBatchSerial(0).MarkAsDeleted()
                Else
                    .LoanMerchandiseDetailBatchSerial.Remove(.LoanMerchandiseDetailBatchSerial(0))
                End If
            End While

            'Agregamos el lote
            If INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If listPhysicalInventory IsNot Nothing Then
                    For Each item In listPhysicalInventory
                        Dim LoanMerchandiseDetailBatchSerial As New LoanMerchandiseDetailBatchSerial
                        LoanMerchandiseDetailBatchSerial.PhysicalInventoryId = item.Id
                        LoanMerchandiseDetailBatchSerial.BatchSerialId = item.BatchSerialId
                        LoanMerchandiseDetailBatchSerial.CodeBatchSerial = item.CodeNameBatchSerial
                        LoanMerchandiseDetailBatchSerial.Quantity = item.QuantityDeliver
                        .LoanMerchandiseDetailBatchSerial.Add(LoanMerchandiseDetailBatchSerial)
                    Next
                End If
            ElseIf INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If listBatchSerial IsNot Nothing Then
                    For Each item In listBatchSerial
                        Dim LoanMerchandiseDetailBatchSerial As New LoanMerchandiseDetailBatchSerial
                        LoanMerchandiseDetailBatchSerial.PhysicalInventoryId = Nothing
                        LoanMerchandiseDetailBatchSerial.BatchSerialId = item.Id
                        LoanMerchandiseDetailBatchSerial.CodeBatchSerial = item.BatchCode
                        LoanMerchandiseDetailBatchSerial.Quantity = item.Quantity
                        .LoanMerchandiseDetailBatchSerial.Add(LoanMerchandiseDetailBatchSerial)
                    Next
                End If
            Else
                Dim LoanMerchandiseDetailBatchSerial As New LoanMerchandiseDetailBatchSerial
                LoanMerchandiseDetailBatchSerial.PhysicalInventoryId = Nothing
                LoanMerchandiseDetailBatchSerial.BatchSerialId = Nothing
                LoanMerchandiseDetailBatchSerial.Quantity = INDSeQuantity.EditValue
                .LoanMerchandiseDetailBatchSerial.Add(LoanMerchandiseDetailBatchSerial)
            End If
        End With
    End Sub

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
            If codeProduct Is String.Empty Then
                If product IsNot Nothing Then
                    INDPceProduct.Text = product.Code + " - " + product.Name
                End If
                Exit Function
            End If

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

#Region "Events"

#Region "Load"

    Private Sub FrmPopupProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True

        If _editMode = True Then
            LoadControls()
        Else
            CleanControls()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _loanType = Nothing
        _wareHouseId = Nothing
        LoanMerchandiseDetail = Nothing
        product = Nothing
        listBatchSerial = Nothing
        listPhysicalInventory = Nothing
        _editMode = Nothing
        _listLoanMerchandiseDetailValidation = Nothing
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmPopupProduct_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Activated"
    Private Sub FrmPopupProduct_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If product Is Nothing Then
            INDPceProduct.Focus()
        End If
        If _editMode = True Then
            INDPceProduct.Focus()
        End If
    End Sub
#End Region

#Region "KeyDown"

    Private Sub FrmPopUpAddProductsLoanMerchandise_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDSeQuantity_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDSeQuantity.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

    Private Sub INDPceQuantity_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceBatchSerialOutput.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

    Private Sub INDPceBatchSerial_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceBatchSerialInput.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

    Private Async Sub INDPceProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProduct.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
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

#Region "QueryPopup"

    ''' <summary>
    ''' Consulta y asigna los produuctos al listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProduct.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

#End Region

#Region "Popup"

    Private Sub INDPceBatchSerialInput_Popup(sender As Object, e As EventArgs) Handles INDPceBatchSerialInput.Popup
        CtrBatchSerial1.SetFocusGrid()
    End Sub

    Private Sub INDPceBatchSerialOutput_Popup(sender As Object, e As EventArgs) Handles INDPceBatchSerialOutput.Popup
        CtrPhysicalInventory1.SetFocusGrid()
    End Sub

#End Region

#Region "SelectProduct"

    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProduct.Text = e.CodeNameProduct
        INDPceProduct.Focus()
        INDPceProduct.ClosePopup()
        Using model As New MInventoryProduct(Me.Tag)
            product = model.GetInventoryProductByIdSimple(e.ProductId)
            INDtxtPresentation.Text = product.Presentation
        End Using

        ActionsControls = True
        INDSeQuantity.EditValue = 0
        INDPceBatchSerialInput.EditValue = Nothing
        INDPceBatchSerialOutput.EditValue = Nothing
        CtrBatchSerial1.CleanControls()
        CtrPhysicalInventory1.CleanControls()
        listBatchSerial = Nothing
        listPhysicalInventory = Nothing

        Using model As New MSubGroup(Me.Tag)
            Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
            If subGroup.ObjectEmbbeded.HandlesBatch = True Then
                INDSeQuantity.Properties.ReadOnly = True
                If _loanType = ELeanType.Input Then
                    INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    CtrBatchSerial1.Product = product
                    CtrBatchSerial1.FormOwner = Me
                    CtrBatchSerial1.SetListBatchSerial()

                    INDPceBatchSerialInput.Focus()
                    INDPceBatchSerialInput.ShowPopup()
                Else
                    INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    CtrPhysicalInventory1.Product = product
                    CtrPhysicalInventory1.FormOwner = Me
                    CtrPhysicalInventory1.SetListPhysicalInventory()

                    INDPceBatchSerialOutput.Focus()
                    INDPceBatchSerialOutput.ShowPopup()
                End If
            Else
                INDSeQuantity.Properties.ReadOnly = False
                INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDSeQuantity.Focus()
            End If
        End Using
    End Sub

#End Region

#Region "ChangeQuantity"

    Private Sub CtrBatchSerial1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrBatchSerial1.ChangeQuantity
        INDSeQuantity.EditValue = e.Quantity
    End Sub

    Private Sub CtrPhysicalInventory1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrPhysicalInventory1.ChangeQuantity
        INDSeQuantity.EditValue = e.Quantity
    End Sub

#End Region

#Region "Closed"

    ''' <summary>
    ''' Obtiene la lista de lotes y cantidades  - cuando es una entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerialInput.Closed
        listBatchSerial = CtrBatchSerial1.GetListBatchSerial()
        If listBatchSerial.Count = 0 Then
            INDPceBatchSerialInput.EditValue = String.Empty
        Else
            INDPceBatchSerialInput.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", MODULE_NAME), listBatchSerial.Count.ToString())
        End If
    End Sub
    ''' <summary>
    ''' Obtiene la lista del control del inventario físico  - cuando es una salida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceQuantity_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerialOutput.Closed
        listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
        If listPhysicalInventory.Count > 0 Then

            INDPceBatchSerialOutput.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), listPhysicalInventory.Count.ToString(), listPhysicalInventory.Sum(Function(x) x.QuantityDeliver).ToString())
        Else
            INDPceBatchSerialOutput.EditValue = Nothing
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Try
            If product.FinalProductCost = 0 OrElse product.FinalProductCost Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "El producto con código " + product.Code + ", no se puede agregar ya que no tiene último costo."
                Exit Sub
            End If
            INDBtnAdd.Enabled = False
            Dim errors = ValidateControlsPopup()
            If errors.Length > 0 Then
                INDBtnAdd.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            SetValues()
            If _listLoanMerchandiseDetailValidation Is Nothing Then
                _listLoanMerchandiseDetailValidation = New List(Of LoanMerchandiseDetail)
            End If
            Dim args As New AddProductLoanMerchandiseDetailEventArgs
            If _editMode = True Then
                args.LoanMerchandiseDetail = LoanMerchandiseDetail
                args.EditMode = True
            Else
                args.LoanMerchandiseDetail = LoanMerchandiseDetail
                _listLoanMerchandiseDetailValidation.Add(LoanMerchandiseDetail)
            End If
            RaiseEvent AddProductLoanMerchandiseDetail(Nothing, args)
            CleanControls()
            INDBtnAdd.Enabled = True
        Catch ex As Exception
            INDBtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub
#End Region

#End Region

#Region "Eventos Barra botones"

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

#End Region

#Region "Enumeraciones"
    Public Enum ELeanType As Integer
        Input = 1
        Output = 2
    End Enum
#End Region

End Class