'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Ernesto Córdoba
' Created          : 17-09-2015
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
Imports Presentation.Controls.MVP

#End Region

Public Class FrmPopupProductInvoice

#Region "EVENTS"
    ''' <summary>
    ''' evento para retornar la entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddDocumentInvoiceProductSalesDetail(sender As Object, e As AddDocumentInvoiceProductSalesDetailEventArgs)
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
    ''' entidad del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim documentInvoiceProductSalesDetail As DocumentInvoiceProductSalesDetail
    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean
    ''' <summary>
    ''' bandera para saber si se ejecuta el metod para establecer el valor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _flagLoadControls As Boolean
    ''' <summary>
    ''' valor del subtotal
    ''' </summary>
    ''' <remarks></remarks>
    Dim _subTotal As Decimal
    ''' <summary>
    ''' valor del descuento
    ''' </summary>
    ''' <remarks></remarks>
    Dim _discountValue As Decimal
    ''' <summary>
    ''' valor del iva
    ''' </summary>
    ''' <remarks></remarks>
    Dim _IVAValue As Decimal

    ''' <summary>
    ''' Modo de contrato de centro de atencion externo
    ''' </summary>
    Private _externalCenterMode As Boolean = False

    ''' <summary>
    ''' Id del contrato de centro de atencion externo
    ''' </summary>
    Private _contractExternalClientId As Integer?

    ''' <summary>
    ''' Id de la unidad funcional
    ''' </summary>
    Private _functionalUnitId As Integer

    ''' <summary>
    ''' Fecha del documento para usar en la nueva tarificacion
    ''' </summary>
    Private _rateProductDate As DateTime
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
            INDSeQuantity.Enabled = value
            INDPceBatchSerial.Enabled = value
            INDTxtSalePrice.Enabled = IIf(_externalCenterMode, False, value)
            INDSeDiscountPercent.Enabled = value
            INDSeIVAPercent.Enabled = value
            INDTxtTotal.Enabled = value
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
    ''' propiedad para saber si el precio del producto se obtiene de la nueva tarificacion, asociadoa un contrato de centro de atencion externo
    ''' </summary>
    Public WriteOnly Property ExternalCenterMode As Boolean
        Set(value As Boolean)
            _externalCenterMode = value
        End Set
    End Property

    ''' <summary>
    ''' Id del contrato del centro de atencion externo
    ''' </summary>
    Public WriteOnly Property ContractExternalClientId As Integer?
        Set(value As Integer?)
            _contractExternalClientId = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    Public WriteOnly Property RateProductDate As DateTime
        Set(value As DateTime)
            _rateProductDate = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la Unidad funcional
    ''' </summary>
    Public WriteOnly Property FunctionalUnitId As Integer
        Set(value As Integer)
            _functionalUnitId = value
        End Set
    End Property

    Public WriteOnly Property DocumentInvoiceProductSalesDetailEdit As DocumentInvoiceProductSalesDetail
        Set(value As DocumentInvoiceProductSalesDetail)
            documentInvoiceProductSalesDetail = value
        End Set
    End Property
#End Region

#Region "METHODS"
    Private Sub CleanControls()
        CtrPhysicalInventory1.CleanControls()
        INDPceProduct.Text = String.Empty
        INDPceProduct.Properties.ReadOnly = False
        INDSeQuantity.EditValue = 0
        INDLciQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDPceBatchSerial.EditValue = Nothing
        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDTxtSalePrice.EditValue = 0
        INDSeDiscountPercent.EditValue = 0
        INDSeIVAPercent.EditValue = 0
        INDTxtTotal.EditValue = 0
        product = Nothing
        listPhysicalInventory = Nothing
        _editMode = False
        ActionOnControls = False
        INDBtnAdd.Enabled = True
        INDBtnAdd.Text = ResourceManager.GetString("Add")
        INDPceProduct.Focus()
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

    Private Sub LoadControls()
        INDBtnAdd.Text = ResourceManager.GetString("Edit")
        With documentInvoiceProductSalesDetail
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(.ProductId)
                INDPceProduct.Text = product.Code + " - " + product.Name
            End Using
            INDPceProduct.Properties.ReadOnly = True
            If .HandlesBatch = True Then
                INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), .DocumentInvoiceProductSalesDetailBatchSerial.Count.ToString(), .DocumentInvoiceProductSalesDetailBatchSerial.Sum(Function(x) x.Quantity).ToString())
                CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
                CtrPhysicalInventory1.Product = product
                CtrPhysicalInventory1.WareHouseId = _wareHouseId
                CtrPhysicalInventory1.FormOwner = Me
                CtrPhysicalInventory1.SetListPhysicalInventory()
                CtrPhysicalInventory1.SetQuantityPhysicalInventory(.DocumentInvoiceProductSalesDetailBatchSerial.ToList())
                listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
            Else
                INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSeQuantity.EditValue = .Quantity
            End If
            _flagLoadControls = True
            _subTotal = .SubTotalValue
            _discountValue = .DiscountValue
            _IVAValue = .IvaValue
            INDTxtSalePrice.EditValue = .SalePrice
            INDSeDiscountPercent.EditValue = .DiscountPercentage
            INDSeIVAPercent.EditValue = .IvaPercentage
            INDTxtTotal.EditValue = .TotalValue
            ActionOnControls = True
            _flagLoadControls = False
            INDPceProduct.Focus()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para establecer los valores a los campos 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        If _flagLoadControls Then
            Exit Sub
        End If
        If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If listPhysicalInventory IsNot Nothing Then
                _subTotal = listPhysicalInventory.Sum(Function(x) x.QuantityDeliver) * INDTxtSalePrice.EditValue
            End If
        Else
            _subTotal = INDSeQuantity.EditValue * INDTxtSalePrice.EditValue
        End If
        _discountValue = CDec(Utils.RoundValue((_subTotal * INDSeDiscountPercent.EditValue) / 100, 6))
        _IVAValue = CDec(Utils.RoundValue(((_subTotal - _discountValue) * INDSeIVAPercent.EditValue) / 100, 6))
        INDTxtTotal.EditValue = _subTotal - _discountValue + _IVAValue
    End Sub

    Private Function ValidateProductPrice() As Boolean
        If product.ProductWithPriceControl = True Then
            'Únicamente aplica para productos que tengan control de precios
            If INDTxtSalePrice.EditValue IsNot Nothing AndAlso INDTxtSalePrice.EditValue > 0 Then
                If INDTxtSalePrice.EditValue > product.SellingPrice Then
                    Mensaje(EeventViewerImages.Advertencia) = "El valor del Precio Unitario no puede ser mayor al valor de Venta del Producto"
                    INDTxtSalePrice.EditValue = product.SellingPrice
                    Return False
                End If
            End If
        End If
        Return True
    End Function

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
#End Region

#Region "HANDLES"
#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        product = Nothing
        _wareHouseId = Nothing
        _operatingUnitId = Nothing
        listPhysicalInventory = Nothing
        documentInvoiceProductSalesDetail = Nothing
        _editMode = Nothing
        _flagLoadControls = Nothing
        _subTotal = Nothing
        _discountValue = Nothing
        _IVAValue = Nothing
    End Sub

    Private Sub FrmPopupProductInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

#Region "Shown"
    Private Sub FrmPopupProductInvoice_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDPceProduct.Focus()
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmPopupProductInvoice_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
        Me.AsyncLoader(True)

        Task.Factory.StartNew(Sub()
                                  Using model As New MInventoryProduct(Me.Tag)
                                      product = model.GetInventoryProductByIdSimple(e.ProductId)
                                  End Using
                                  Using model As New MSubGroup(Me.Tag)
                                      If product.ProductSubGroupId = 0 Then
                                          SafeInvoke(Sub()
                                                         Me.AsyncLoader(False)
                                                         Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ProductWithoutSubgroup", MODULE_NAME)
                                                         Exit Sub
                                                     End Sub)
                                      End If
                                      If indigo.IndigoCompanyType = 1 Then
                                          SafeInvoke(Sub()
                                                         INDSeIVAPercent.EditValue = product.IvaPercent
                                                     End Sub)
                                      Else
                                          _IVAValue = 0
                                          SafeInvoke(Sub()
                                                         INDSeIVAPercent.EditValue = 0
                                                     End Sub)
                                      End If
                                      Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
                                      If subGroup IsNot Nothing AndAlso subGroup.ObjectEmbbeded IsNot Nothing AndAlso subGroup.ObjectEmbbeded.HandlesBatch = True Then

                                          SafeInvoke(Sub()
                                                         INDSeQuantity.Enabled = False
                                                         INDLciQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                                         INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                                         CtrPhysicalInventory1.CleanControls()
                                                     End Sub)
                                      Else
                                          SafeInvoke(Sub()
                                                         INDSeQuantity.Enabled = True
                                                         INDLciQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                                         INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                                     End Sub)
                                      End If
                                  End Using
                                  If _externalCenterMode Then
                                      Using Model As New MProductInvoice(Me.Tag)
                                          Dim ListDocumentInvoiceProductSalesDetail = New List(Of DocumentInvoiceProductSalesDetail)
                                          Dim DocumentInvoiceProductSalesDetail = New DocumentInvoiceProductSalesDetail
                                          With DocumentInvoiceProductSalesDetail
                                              .ProductId = e.ProductId
                                              .Quantity = 1
                                              .IvaPercentage = product.IvaPercent
                                              .CodeNameProduct = $"{product.Code} - {product.Name}"
                                              .RemissionDate = _rateProductDate
                                          End With
                                          ListDocumentInvoiceProductSalesDetail.Add(DocumentInvoiceProductSalesDetail)
                                          SafeInvoke(Sub()
                                                         If _contractExternalClientId Is Nothing Then
                                                             Mensaje(EeventViewerImages.Advertencia) = "No se encuentra Seleccionado un contrato de centro de atención externo."
                                                             Me.AsyncLoader(False)
                                                             Me.Close()
                                                             Me.AsyncLoader(False)
                                                             Exit Sub
                                                         End If
                                                         Dim ResultValue = Model.CalculateNewRate(ListDocumentInvoiceProductSalesDetail, _contractExternalClientId, _functionalUnitId)
                                                         Me.AsyncLoader(False)
                                                         If ResultValue Is Nothing OrElse ResultValue.StateResult = False Then
                                                             If ResultValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultValue.Message) Then
                                                                 Mensaje(EeventViewerImages.Advertencia) = ResultValue.Message
                                                             Else
                                                                 Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un error al calcular la tarificación del producto"
                                                             End If
                                                             Me.Close()
                                                             Exit Sub
                                                         End If
                                                         If ResultValue.StateResult = True Then
                                                             INDTxtSalePrice.EditValue = ResultValue.ObjectEmbbeded.FirstOrDefault.SalePrice
                                                             ActionOnControls = True
                                                             If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                                                                 INDPceBatchSerial.Focus()
                                                             Else
                                                                 INDSeQuantity.Focus()
                                                             End If
                                                         End If
                                                     End Sub)
                                      End Using

                                  Else

                                      SafeInvoke(Sub()
                                                     Me.AsyncLoader(False)
                                                     INDTxtSalePrice.EditValue = product.SellingPrice
                                                     ActionOnControls = True
                                                     If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                                                         INDPceBatchSerial.Focus()
                                                     Else
                                                         INDSeQuantity.Focus()
                                                     End If
                                                 End Sub)
                                  End If

                              End Sub)
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDPceProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProduct.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

    Private Sub INDPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceBatchSerial.QueryPopUp
        CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
        CtrPhysicalInventory1.Product = product
        CtrPhysicalInventory1.WareHouseId = _wareHouseId
        CtrPhysicalInventory1.FormOwner = Me
        CtrPhysicalInventory1.SetListPhysicalInventory()
    End Sub
#End Region

#Region "KeyDown"
    Private Async Sub INDPceProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProduct.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

    Private Sub FrmPopupProductInvoice_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "Closed"
    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerial.Closed
        listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
        If listPhysicalInventory.Count > 0 Then

            INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), listPhysicalInventory.Count.ToString(), listPhysicalInventory.Sum(Function(x) x.QuantityDeliver).ToString())
        Else
            INDPceBatchSerial.EditValue = Nothing
        End If
        SetValues()
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDPceProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
                Using form As New FrmProducts
                    OpenFormDialog(form)
                End Using
            End If
        End If
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


    Private Sub INDSeQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeQuantity.EditValueChanged
        SetValues()
    End Sub

    Private Sub INDTxtSalePrice_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtSalePrice.EditValueChanged
        SetValues()
    End Sub

    Private Sub INDSeDiscountPercent_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeDiscountPercent.EditValueChanged
        SetValues()
    End Sub

    Private Async Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        AsyncLoader(True)
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            AsyncLoader(False)
            Exit Sub
        End If

        If ValidateProductPrice() = False Then
            AsyncLoader(False)
            Exit Sub
        End If

        Dim resultAssignValues = AssignValues()

        If resultAssignValues.StateResult = False Then
            Mensaje(EeventViewerImages.Advertencia) = resultAssignValues.Message
            AsyncLoader(False)
            Exit Sub
        End If
        'validacion stock de producto
        Using modelSettings As New MSettingInventory(Me.Tag)
            Dim resulValidateStock = Await modelSettings.ValidateStock(documentInvoiceProductSalesDetail.ProductId, _operatingUnitId, _wareHouseId, documentInvoiceProductSalesDetail.Quantity, InventoryStaticServices.MovementType.OutPut)
            If resulValidateStock.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = resulValidateStock.Message
            End If
        End Using

        Dim args As New AddDocumentInvoiceProductSalesDetailEventArgs
        If _editMode = True Then
            args.EditMode = True
        End If
        args.DocumentInvoiceProductSalesDetail = documentInvoiceProductSalesDetail
        RaiseEvent AddDocumentInvoiceProductSalesDetail(Nothing, args)
        AsyncLoader(False)
        CleanControls()

    End Sub

    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDPceBatchSerial.EditValue Is Nothing Then
                errors.AppendLine("Cantidad en cero")
            End If
        Else
            If INDSeQuantity.EditValue = 0 Then
                errors.AppendLine("Cantidad en cero")
            End If
        End If
        If INDTxtSalePrice.EditValue = 0 Then
            errors.AppendLine("Valor unitario de venta en cero")
        End If
        Return errors.ToString()
    End Function

    Private Function AssignValues() As ActionResult
        If _editMode = False Then
            documentInvoiceProductSalesDetail = New DocumentInvoiceProductSalesDetail
        End If
        With documentInvoiceProductSalesDetail
            .ProductId = product.Id
            .CodeNameProduct = product.Code + " - " + product.Name
            If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count > 0 Then
                .Quantity = listPhysicalInventory.Sum(Function(x) x.QuantityDeliver)
            Else
                .Quantity = INDSeQuantity.EditValue
            End If
            .SalePrice = INDTxtSalePrice.EditValue
            .SubTotalValue = _subTotal
            .DiscountPercentage = INDSeDiscountPercent.EditValue
            .DiscountValue = _discountValue
            .IvaPercentage = INDSeIVAPercent.EditValue
            .IvaValue = _IVAValue
            .TotalValue = INDTxtTotal.EditValue
            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .HandlesBatch = True

                'si se esta editando el registro elimino los detalles de ConsignmentInventoryRemissionDetailBatchSerial 
                If _editMode = True Then
                    If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count > 0 Then
                        While .DocumentInvoiceProductSalesDetailBatchSerial.Count > 0
                            If .DocumentInvoiceProductSalesDetailBatchSerial(0).Id > 0 Then
                                .DocumentInvoiceProductSalesDetailBatchSerial(0).MarkAsDeleted()
                            Else
                                .DocumentInvoiceProductSalesDetailBatchSerial.Remove(.DocumentInvoiceProductSalesDetailBatchSerial(0))
                            End If
                        End While
                    End If
                End If
            Else
                .HandlesBatch = False

                ' elimino todos los registros de ConsignmentInventoryRemissionDetailBatchSerial
                While .DocumentInvoiceProductSalesDetailBatchSerial.Count > 0
                    If .DocumentInvoiceProductSalesDetailBatchSerial(0).Id > 0 Then
                        .DocumentInvoiceProductSalesDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        .DocumentInvoiceProductSalesDetailBatchSerial.RemoveAt(0)
                    End If
                End While
            End If

            'si el producto maneja lote
            If listPhysicalInventory IsNot Nothing Then
                For Each item In listPhysicalInventory
                    Dim documentInvoiceProductSalesDetailBatchSerial As New DocumentInvoiceProductSalesDetailBatchSerial
                    With documentInvoiceProductSalesDetailBatchSerial
                        .PhysicalInventoryId = item.Id
                        .Quantity = item.QuantityDeliver
                        .BatchCode = item.CodeNameBatchSerial
                    End With
                    .DocumentInvoiceProductSalesDetailBatchSerial.Add(documentInvoiceProductSalesDetailBatchSerial)
                Next
            Else
                Using model As New MCtrPhysicalInventory(Me.Tag)
                    Dim physicalInventoryTmp = model.GetListPhysicalInventory(product.Id, _wareHouseId)
                    If physicalInventoryTmp.Count = 0 Then
                        Return New ActionResult With {.StateResult = False, .Message = "El producto " + product.Code + " - " + product.Name + " no esta en el inventario fisico"}
                    End If
                    If physicalInventoryTmp.ElementAt(0).Quantity < INDSeQuantity.EditValue Then
                        Return New ActionResult With {.StateResult = False, .Message = "El producto " + product.Code + " - " + product.Name + " no cuenta con la cantidad suficiente en el almacen seleccionado"}
                    End If
                    Dim documentInvoiceProductSalesDetailBatchSerial As New DocumentInvoiceProductSalesDetailBatchSerial
                    With documentInvoiceProductSalesDetailBatchSerial
                        .PhysicalInventoryId = physicalInventoryTmp.ElementAt(0).Id
                        .Quantity = INDSeQuantity.EditValue
                    End With
                    .DocumentInvoiceProductSalesDetailBatchSerial.Add(documentInvoiceProductSalesDetailBatchSerial)
                End Using
            End If
        End With
        Return New ActionResult With {.StateResult = True}
    End Function

    Private Sub INDTxtTotal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtTotal.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub
End Class