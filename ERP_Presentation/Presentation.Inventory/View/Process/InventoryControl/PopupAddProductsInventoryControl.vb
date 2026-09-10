'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 27-03-2015
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
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
#End Region

Public Class PopupAddProductsInventoryControl

#Region "Builder"
    ''' <summary>
    ''' Inicializa una isntancia de la clase
    ''' </summary>
    Public Sub New(_Detaill As InventoryControlDetail, _listInventoryControlDetailValidation As List(Of InventoryControlDetail), _physicalInventory As Boolean, Optional _wharehouse As Integer? = Nothing)
        InitializeComponent()

        If _Detaill IsNot Nothing Then
            INDBtnAddProduct.Text = ResourceManager.GetString("Edit")
            product = _Detaill.InventoryProduct
            _editMode = True
            Detail = _Detaill
        End If
        PhysicalInventory = _physicalInventory
        warehouseId = _wharehouse
        ListDetailValidation = _listInventoryControlDetailValidation
    End Sub
#End Region

#Region "Globals"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Obtiene o establece el producto seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Private product As InventoryProduct

    ''' <summary>
    ''' listado de los lotes
    ''' </summary>
    ''' <remarks></remarks>
    Private listBatchSerial As List(Of BatchSerial)

    ''' <summary>
    ''' Objeto que establece el producto a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Private Detail As InventoryControlDetail

    ''' <summary>
    ''' Objeto que establece el almacen a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Private warehouseId As Integer

    ''' <summary>
    ''' Objeto que representa el listado de detalles utilziado para valdiar los existentes
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDetailValidation As List(Of InventoryControlDetail)

    ''' <summary>
    ''' Objeto que representa el listado que hay en el inventario fisico del producto seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Private physical As List(Of PhysicalInventory)

#End Region

#Region "Events"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddInventoryControlDetail(sender As Object, e As AddProductInventoryControlEventArg)
#End Region

#Region "Properties"
    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    Dim _editMode As Boolean = False
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el mensaje que se va a mostrar
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
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            INDTxtQuantity.Enabled = value
            INDPceBatchSerial.Enabled = value
            INDTxtUnid.Enabled = value
            INDTxtPhysicalInventoryQuantity.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    Dim inventoryControlDetail As InventoryControlDetail
    Public WriteOnly Property InventoryControlDetailEdit As InventoryControlDetail
        Set(value As InventoryControlDetail)
            inventoryControlDetail = value
        End Set
    End Property

    Private _physicalInventory As Boolean
    Public Property PhysicalInventory As Boolean
        Get
            Return _physicalInventory
        End Get
        Set(value As Boolean)
            _physicalInventory = value
        End Set
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Abre el formulario para adicion de productos
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Limpia los controles para agregar un producto nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        product = Nothing
        Detail = New InventoryControlDetail

        INDPceProduct.Text = String.Empty
        INDTxtUnid.Text = String.Empty
        INDTxtQuantity.EditValue = 0
        INDPceBatchSerial.Text = String.Empty
        INDTxtPhysicalInventoryQuantity.EditValue = 0

        CtrBatchSerial1.CleanControls()
        ActionsControls = False
        BarraBotones.FilterDataSource = Nothing
        _editMode = False

        INDPceProduct.Focus()

        INDLyTxtPhysicalInventoryQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    Private Sub LoadControls()
        INDBtnAddProduct.Text = ResourceManager.GetString("Edit")

        With Detail
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(.ProductId)
                INDPceProduct.Text = product.Code + " - " + product.Name
                INDPceProduct.Properties.ReadOnly = True
                INDTxtUnid.Text = product.PackingUnitDescription
            End Using
            Using model As New MSubGroup(Me.Tag)
                Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
                If subGroup.ObjectEmbbeded.HandlesBatch = True Then
                    INDTxtQuantity.Properties.ReadOnly = True
                    INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDTxtQuantity.Properties.ReadOnly = False
                    INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End Using

            INDTxtQuantity.EditValue = .Quantity
            If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If .InventoryControlDetailBatchSerial.Count = 0 Then
                    INDPceBatchSerial.EditValue = String.Empty
                Else
                    INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", NAME_MODULE), .InventoryControlDetailBatchSerial.Count.ToString())
                End If
                CtrBatchSerial1.Product = product
                CtrBatchSerial1.FormOwner = Me

                CtrBatchSerial1.SetListBatchSerial()
                CtrBatchSerial1.SetQuantityBatchSerial(.InventoryControlDetailBatchSerial.ToList())
            End If
            'Cargo la cantidad existente en el inventario fisico
            If PhysicalInventory Then
                INDLyTxtPhysicalInventoryQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Using model As New MInventoryControl("")
                    physical = model.GetPhysicalInventoryByProductAndWarehouse(product.Id, warehouseId)
                    If physical Is Nothing Then
                        INDTxtPhysicalInventoryQuantity.Text = 0
                    Else
                        INDTxtPhysicalInventoryQuantity.Text = physical.Sum(Function(x) x.Quantity)
                    End If
                End Using
            End If
        End With
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    Private Function ValidateControl() As String
        Dim errors As New StringBuilder

        If product Is Nothing Then
            Return INDLyPceProduct.Text + ResourceManager.GetString("Empty")
        End If
        If _editMode = False Then
            If ListDetailValidation IsNot Nothing AndAlso ListDetailValidation.Count > 0 Then
                Dim detailTmp = ListDetailValidation.Find(Function(x) x.ProductId = product.Id)
                If detailTmp IsNot Nothing Then
                    errors.AppendLine(String.Format("El producto {0} ya se encuetra agregado en el listado.", product.Code + " - " + product.Name))
                End If
            End If
        End If
        If INDTxtQuantity.EditValue = 0 Then
            errors.AppendLine(INDLyTxtQuantity.Text + ResourceManager.GetString("Empty"))
        End If
        If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If _editMode = False Then
                If listBatchSerial Is Nothing Then
                    errors.AppendLine(INDLyPceBatchSerial.Text + ResourceManager.GetString("Empty"))
                End If
                If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count = 0 Then
                    errors.AppendLine(INDLyPceBatchSerial.Text + ResourceManager.GetString("Empty"))
                End If
            Else
                If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count = 0 Then
                    errors.AppendLine(INDLyPceBatchSerial.Text + ResourceManager.GetString("Empty"))
                End If
            End If
        End If

        Return errors.ToString()
    End Function
#End Region

#Region "Handless"

#Region "Load"
    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupProductsEntranceVoucher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True

        If _editMode = True Then
            'si esta editando un registro
            Me.LoadControls()
        Else
            'si es un nuevo registro
            CleanControls()
        End If

        If PhysicalInventory = False Then
            CtrBatchSerial1.RemissionType = ERemissionType.Input
            INDLyTxtPhysicalInventoryQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            CtrBatchSerial1.RemissionType = ERemissionType.Output
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub PopUpProductsEntranceVoucher_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "Activated"
    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupAddProductsInventoryControl_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDPceProduct.Focus()
    End Sub
#End Region

#Region "SelectProduct"
    ''' <summary>
    ''' Evento que selecciona el producto en el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProduct.Text = e.CodeNameProduct
        INDPceProduct.Focus()
        INDPceProduct.ClosePopup()
        CtrBatchSerial1.CleanControls()
        INDTxtQuantity.EditValue = 0
        INDPceBatchSerial.EditValue = Nothing
        ActionsControls = True
        Using model As New MInventoryProduct(Me.Tag)
            product = model.GetInventoryProductByIdSimple(e.ProductId)
            INDTxtUnid.Text = product.PackingUnitDescription
        End Using
        Using model As New MSubGroup(Me.Tag)
            If product.ProductSubGroupId = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El product " + product.Code + " - " + product.Name + " no tiene un subgrupo asociado"
                CleanControls()
                Exit Sub
            End If
            Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
            If subGroup.ObjectEmbbeded.HandlesBatch = True Then
                INDTxtQuantity.Properties.ReadOnly = True
                INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDPceBatchSerial.Focus()
                If _editMode = True Then
                    'instancio el listado para poder hacer la validacion de los lotes cuando se esta editando y saber si esta sin registros
                    listBatchSerial = New List(Of BatchSerial)
                End If
            Else
                INDTxtQuantity.Properties.ReadOnly = False
                INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDTxtQuantity.Focus()
            End If
        End Using
        If PhysicalInventory Then
            INDLyTxtPhysicalInventoryQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Using model As New MInventoryControl("")
                physical = model.GetPhysicalInventoryByProductAndWarehouse(product.Id, warehouseId)
                If physical.Count = 0 Then
                    INDTxtPhysicalInventoryQuantity.Text = 0
                Else
                    INDTxtPhysicalInventoryQuantity.Text = physical.Sum(Function(x) x.Quantity)
                End If
            End Using
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Consulta y asigna los produuctos al listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProduct.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

    ''' <summary>
    ''' Consulta y asigna los lotes del producto seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceBatchSerial.QueryPopUp
        If product IsNot Nothing Then
            CtrBatchSerial1.Product = product
            CtrBatchSerial1.FormOwner = Me
            CtrBatchSerial1.WarehouseId = warehouseId
            CtrBatchSerial1.SetListBatchSerial()
            CtrBatchSerial1.IsInventoryControl = True
        End If
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Despliega el formulario de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenFormDialog(form)
            End Using
        End If
    End Sub
#End Region

#Region "Closed"
    ''' <summary>
    ''' Evento que se dispara cuando el control del batch se cierra y agrega los datos al control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerial.Closed
        listBatchSerial = CtrBatchSerial1.GetListBatchSerial()
        If listBatchSerial.Count = 0 Then
            INDPceBatchSerial.EditValue = String.Empty
        Else
            If PhysicalInventory = False Then
                For Each batch In listBatchSerial
                    If batch.ExpirationDate < GetDateServer() Then
                        Return
                    End If
                Next
            End If
            INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", NAME_MODULE), listBatchSerial.Count.ToString())
        End If
    End Sub
#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para cerrar el frontal con el boton escape
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopUpProductsEntranceVoucher_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento qeu coloca el foco sobre el boton de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtPhysicalInventoryQuantity_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtPhysicalInventoryQuantity.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter AndAlso INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            INDBtnAddProduct.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento qeu coloca el foco sobre el boton de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtUnid_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtUnid.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAddProduct.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Cierra el frontal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupAddProductsInventoryControl_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDPceBatchSerial_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceBatchSerial.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDPceBatchSerial.Text <> String.Empty Then
                INDBtnAddProduct.Focus()
            End If
        End If
    End Sub

#End Region

#Region "Click"
    Private Sub INDBtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDBtnAddProduct.Click
        Dim errors = ValidateControl()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        '********************* Defino el evento de retorno ****************'
        Dim args As New AddProductInventoryControlEventArg
        '********************* Verifico si el item es para actualziar o agregar ****************'
        Detail.InventoryProduct = product
        Detail.Quantity = INDTxtQuantity.EditValue
        Detail.ProductCodeName = INDPceProduct.Text
        If _physicalInventory Then
            Detail.QuantityPhysical = physical.Sum(Function(x) x.Quantity)
        End If

        If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If _editMode = True Then

                If listBatchSerial?.Any() AndAlso Detail?.InventoryControlDetailBatchSerial?.Any() Then
                    For Each item In Detail.InventoryControlDetailBatchSerial.ToList()
                        If item.Id > 0 Then
                            item.MarkAsDeleted()
                        Else
                            item.MarkAsUnchanged()
                            Detail.InventoryControlDetailBatchSerial.Remove(item)
                        End If
                    Next
                End If

            End If

            If listBatchSerial IsNot Nothing Then
                For Each item In listBatchSerial
                    Dim detailBatchSerial As New InventoryControlDetailBatchSerial
                    detailBatchSerial.BatchSerialId = item.Id
                    detailBatchSerial.Quantity = item.Quantity
                    detailBatchSerial.BatchSerialCode = item.BatchCode

                    If physical IsNot Nothing AndAlso physical.Count > 0 Then
                        For Each ph In physical
                            If ph.BatchSerialId = item.Id Then
                                detailBatchSerial.InventoryQuantity = ph.Quantity
                                If detailBatchSerial.Quantity <> detailBatchSerial.InventoryQuantity Then
                                    detailBatchSerial.Status = 1 'No Ajustado
                                Else
                                    detailBatchSerial.Status = 2 'Ajustado
                                End If
                                Exit For
                            End If
                        Next
                    End If
                    Detail.InventoryControlDetailBatchSerial.Add(detailBatchSerial)
                Next
            End If
        Else
            ' elimino todos los registros de RemissionEntranceDetailBatchSerial
            While Detail.InventoryControlDetailBatchSerial.Count > 0
                If Detail.InventoryControlDetailBatchSerial(0).Id > 0 Then
                    Detail.InventoryControlDetailBatchSerial(0).MarkAsDeleted()
                Else
                    Detail.InventoryControlDetailBatchSerial.Remove(Detail.InventoryControlDetailBatchSerial(0))
                End If
            End While
            Dim detailBatchSerial As New InventoryControlDetailBatchSerial
            detailBatchSerial.Quantity = INDTxtQuantity.EditValue
            detailBatchSerial.InventoryQuantity = Detail.QuantityPhysical
            If PhysicalInventory Then
                If detailBatchSerial.Quantity <> detailBatchSerial.InventoryQuantity Then
                    detailBatchSerial.Status = 1 'No Ajustado
                Else
                    detailBatchSerial.Status = 2 'Ajustado
                End If
            Else
                detailBatchSerial.Status = 0 'No Aplica
            End If
            
            Detail.InventoryControlDetailBatchSerial.Add(detailBatchSerial)
        End If

        If _editMode AndAlso Detail?.Id > 0 Then
            Detail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
        End If
        args.EditMode = _editMode
        args.inventoryControlDetail = Detail

        RaiseEvent AddInventoryControlDetail(Nothing, args)
        If _editMode Then
            product = Nothing
            Me.Close()
        Else
            CleanControls()
        End If
    End Sub
#End Region

#Region "ChangeQuantity"
    ''' <summary>
    ''' Modifica el valor de la cantidad cuando se asigna el lote
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrBatchSerial1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrBatchSerial1.ChangeQuantity
        INDTxtQuantity.EditValue = e.Quantity
    End Sub
#End Region

#Region "Popup"
    ''' <summary>
    ''' Evento que se utiliza para dirigir el foco en la regilla de los lotes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceBatchSerial_Popup(sender As Object, e As EventArgs) Handles INDPceBatchSerial.Popup
        CtrBatchSerial1.SetListBatchSerial()
    End Sub
#End Region

#End Region

#Region "BarraBotones"
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region

    
End Class