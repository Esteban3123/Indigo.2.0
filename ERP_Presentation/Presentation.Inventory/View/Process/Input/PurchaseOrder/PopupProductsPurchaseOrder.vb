'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 31-12-2014
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
Imports DevExpress.Xpo
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Presentation.Accounting.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class PopupProductsPurchaseOrder

#Region "Builder"

    ''' <summary>
    ''' Inicializa una isntancia de la clase
    ''' </summary>
    ''' <param name="_Detaill"></param>
    ''' <param name="_listProducts"></param>
    ''' <remarks></remarks>
    Public Sub New(_ReadOnly As Boolean, _Detaill As PurchaseOrderDetail,
                   _listProducts As TrackableCollection(Of PurchaseOrderDetail), Optional _currency As Currency = Nothing, Optional _tRMValue As Decimal = 1, Optional _roundType As Decimal = 1)
        InitializeComponent()
        ctrTmp = New CtrContractTotalInfo()
        ctrTmp.SetInfoFunction(AddressOf getInfoServiceOrder)
        ctrTmp.PrintInfo()
        ctrTmp.TextIvaValue = "IVA: "
        ctrTmp.TextDiscountValue = "DTO: "
        ctrTmp.INDPceNetValue.Visible = False
        ctrTmp.INDPceTotalValue.Size = New Size(292, 52)
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)

        Me._tRMValue = _tRMValue
        Me._indigo = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)
        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)

        If _Detaill IsNot Nothing Then
            INDBtnAddProduct.Text = ResourceManager.GetString("Edit")
            'GetProduct(_Detaill.ProductId)
            product = _Detaill.InventoryProduct
            editModeForm = True
            Detail = _Detaill
        End If
        If _listProducts IsNot Nothing Then
            For Each item In _listProducts
                ListProduct.Add(item)
            Next
        End If
        OnlyRead = _ReadOnly

        ''se actualiza el formato de los campos numericos en el formuario
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(Me._headCurrency.Abbreviation.GetCultureId()).NumberFormat
        _culture.NumberFormat.CurrencyDecimalDigits = Utils.MaskByCurrencyRounding(_roundType, _culture.NumberFormat)
        roundingDecimals = _roundType
        Me.changeNumericFormatByCurrency(_culture.NumberFormat)
        ''solo el campo de precio unitario queda con la decimales 
        INDTxtLastPurcharse.Properties.Mask.EditMask = "c2"
        ctrTmp.CurrencyNumbertFormat = _culture.NumberFormat
    End Sub

#End Region

#Region "Globals"

    ''' <summary>
    ''' Permite saber cual es la cantidad confirmada de un detalles para no dejar modificar el control
    ''' </summary>
    Private _quantityConfirmed As Integer

    ''' <summary>
    ''' Permite saber cual es la cantidad confirmada de un detalles para no dejar modificar el control
    ''' </summary>
    Public Property QuantityConfirmed As Integer
        Get
            Return _quantityConfirmed
        End Get
        Set(value As Integer)
            _quantityConfirmed = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el producto seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim product As InventoryProduct

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrContractTotalInfo

    ''' <summary>
    ''' Obtiene o establece el valor IVA del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IvaValue As Decimal = 0

    ''' <summary>
    ''' Obtiene o establece el valor descuento del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DiscountValue As Decimal = 0

    ''' <summary>
    ''' Obtiene o establece el valor Total del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalValue As Decimal = 0

    ''' <summary>
    ''' Obtiene o establece el listado de los detalles agregados
    ''' </summary>
    ''' <remarks></remarks>
    Private ListProduct As TrackableCollection(Of PurchaseOrderDetail) = New TrackableCollection(Of PurchaseOrderDetail)

    ''' <summary>
    ''' Objeto que establece el producto a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim Detail As PurchaseOrderDetail

    ''' <summary>
    ''' Define si el formulario esta editando o no
    ''' </summary>
    ''' <remarks></remarks>
    Dim editModeForm As Boolean = False

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Define si el form es solo para visualizar
    ''' </summary>
    ''' <remarks></remarks>
    Dim OnlyRead As Boolean

    Dim ProductId As Integer = 0

    ''' <summary>
    ''' Establece si por parametros se tiene en cuenta el iva
    ''' </summary>
    ''' <remarks></remarks>
    Dim settingsIvaCost As Boolean = False
    ''' <summary>
    ''' Define si debo consultar el parametro cuando cambio de producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim ConsSettingsIvaCost As Boolean = True

    ''' <summary>
    ''' Iva del producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ivaProduct As GeneralLedgerIVA

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _headCurrency As Currency

    ''' <summary>
    ''' variable de la tasa de cambio, por defecto es 1 cuando la moneda es igual a la oficial
    ''' </summary>
    Private _tRMValue As Decimal = 1

    ''' <summary>
    ''' variable de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' variable para determinar el tipo de redondeo 
    ''' </summary>
    Private roundingDecimals As Decimal

    ''' <summary>
    ''' Bandera para determinar si el campo subtotal se está cambiando manualmente o por modificacion de otros campos
    ''' </summary>
    Private subtotalChangeFlag As Boolean
    ''' <summary>
    ''' variable privada que almacena el subtotal bruto
    ''' </summary>
    Private _subTotal As Decimal

#End Region

#Region "Properties"

    ''' <summary>
    ''' obtiene O establece el subtotal  bruto
    ''' </summary>
    ''' <returns></returns>
    Property SubTotal As Decimal
        Get
            Return _subTotal
        End Get
        Set(value As Decimal)
            _subTotal = value
            INDTxtSubTotal.EditValue = value - Me.DiscountValue
        End Set
    End Property

    ''' <summary>
    ''' obtiene O establece el valor del descuento
    ''' </summary>
    ''' <returns></returns>
    Property DiscountPercent As Decimal
        Get
            Return INDTxtDiscountPercent.EditValue
        End Get
        Set(value As Decimal)
            INDTxtDiscountPercent.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el valor de % del IVA
    ''' </summary>
    ''' <returns></returns>
    Property IvaPercent As Decimal
        Get
            Return INDTxtIvaPercent.EditValue
        End Get
        Set(value As Decimal)
            INDTxtIvaPercent.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el valor de la ultima compra
    ''' </summary>
    ''' <returns></returns>
    Property LastPurcharse As Decimal
        Get
            Return INDTxtLastPurcharse.EditValue
        End Get
        Set(value As Decimal)
            INDTxtLastPurcharse.EditValue = value
        End Set
    End Property

    ''' <summary>id de la jerarquia
    ''' </summary>
    ''' <returns></returns>
    Property ProductHierarchy As Nullable(Of Integer)
        Get
            If String.IsNullOrEmpty(INDsleProductHierarchy.EditValue) Then
                INDsleProductHierarchy.EditValue = Nothing
            End If
            Return INDsleProductHierarchy.EditValue
        End Get
        Set(value As Nullable(Of Integer))
            INDsleProductHierarchy.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el valor de la cantidad segun la jerarquia
    ''' </summary>
    ''' <returns></returns>
    Property ProductHierarchyQuantity As Integer
        Get
            Return INDTxtProductHierarchyQuantity.EditValue
        End Get
        Set(value As Integer)
            INDTxtProductHierarchyQuantity.EditValue = value
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

        INDPceProducts.Text = String.Empty
        INDTxtProductName.Text = String.Empty
        INDTxtUnitPurcharse.EditValue = 0
        INDTxtMeasureUnit.Text = String.Empty
        Me.LastPurcharse = 0
        INDTxtQuantity.EditValue = 0
        Me.SubTotal = 0
        Me.IvaPercent = 0
        Me.DiscountPercent = 0
        INDTxtIvaValue.EditValue = 0
        settingsIvaCost = False
        ConsSettingsIvaCost = True
        _ivaProduct = Nothing
        ProductHierarchyQuantity = 0
        ProductHierarchy = Nothing

        IvaValue = 0
        DiscountValue = 0
        TotalValue = 0
        'ReadOnlyControls(False)
        INDPceProducts.Focus()
    End Sub

    ''' <summary>
    ''' Lanza la consulta del producto para poderlo seleccionar
    ''' sin necesidad de desplegar el control de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AssignProductWithCode() As Task
        If INDPceProducts.Text.Trim <> String.Empty Then
            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct As String() = INDPceProducts.Text.Split(" - ")
            'Capturo el codigo del producto
            Dim codeProduct As String = arrayCodeProduct(0).Trim

            If codeProduct Is String.Empty Then
                If product IsNot Nothing Then
                    INDPceProducts.Text = product.Code + " - " + product.Name
                End If
                Exit Function
            End If

            Using model As New MInventoryProduct(Me.Tag)
                'Consulto el producto para armar el objeto SelectProductEventArgs
                Dim resultProduct = Await model.GetInventoryProduct(INDPceProducts.Text.Trim)
                If resultProduct.StateResult = False Then 'Si devuelve un error
                    Mensaje(EeventViewerImages.Advertencia) = resultProduct.MessageResult(0)
                    CleanControls()
                    Exit Function
                End If
                'Valido que exista el producto
                If resultProduct.ObjectEmbbeded IsNot Nothing AndAlso resultProduct.ObjectEmbbeded.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto no existe")
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
        ElseIf INDPceProducts.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDPceProducts.Text = product.Code + " - " + product.Description
            End If
        End If
    End Function

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(_currencyAbbreviation.GetCultureId()).NumberFormat

        Me.INDTxtLastPurcharse.Properties.Mask.Culture = _culture
        Me.INDTxtSubTotal.Properties.Mask.Culture = _culture
        Me.INDTxtIvaValue.Properties.Mask.Culture = _culture
        Me.INDTxtSubTotal.Properties.Mask.Culture = _culture
        ctrTmp.CodeISO4217 = _currencyAbbreviation
    End Sub

#End Region

#Region "Functions"

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of String, String, String, String)
        Return New Tuple(Of String, String, String, String)(IvaValue.ToString("c2"), DiscountValue.ToString("c2"), TotalValue.ToString("c2"), "")
    End Function

#End Region

#Region "Events"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub PopupProductsPurchaseOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True
        CleanControls()
        If Me._headCurrency?.Id <> Me._indigo?.OfficialCurrencyId AndAlso Me._tRMValue = 1 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("TRMEmpty")
            Me.Close()
        End If
        If Detail IsNot Nothing Then
            If Detail.InventoryProduct Is Nothing Then
                ProductId = Detail.ProductId
            Else
                ProductId = Detail.InventoryProduct.Id
            End If
            'Detail.InventoryProduct = Nothing
            Await Me.LoadControls()
            INDTxtQuantity.Properties.MinValue = QuantityConfirmed
            'If OnlyRead Then
            '    Me.ReadOnlyControls(True)
            'End If
        End If
    End Sub

    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupProductsContract_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If product Is Nothing Then
            INDPceProducts.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Agrega el producto al listado del contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDBtnAddProduct.Click
        AddInventoryProduct()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDPceProducts_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProducts.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

#End Region

    Private Async Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        Try
            AsyncLoader(True)
            INDPceProducts.Text = e.CodeNameProduct
            INDPceProducts.Focus()
            INDPceProducts.ClosePopup()
            Await GetProduct(e.ProductId)
            AsyncLoader(False)
            INDTxtLastPurcharse.Focus()
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try

    End Sub

    Private Sub INDPceProducts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProducts.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

    Private Sub INDPceProducts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceProducts.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    Private Sub sum_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtQuantity.EditValueChanged, INDTxtLastPurcharse.EditValueChanged, INDTxtIvaPercent.EditValueChanged, INDTxtDiscountPercent.EditValueChanged
        If INDTxtSubTotal.EditValue Is Nothing OrElse INDTxtQuantity.EditValue Is Nothing OrElse INDTxtLastPurcharse.EditValue Is Nothing OrElse
           INDTxtIvaPercent.EditValue Is Nothing OrElse INDTxtDiscountPercent.EditValue Is Nothing Then
            Exit Sub
        End If
        If Me.DiscountPercent > 100 Then
            Me.DiscountPercent = 100
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValuePorcentage", "Inventory"), INDLyTxtDiscountPercent.Text)
        End If

        Me.subtotalChangeFlag = True
        'valor subtotal
        Dim ValorSubtotal = INDTxtQuantity.EditValue * Me.LastPurcharse
        'valor descuento
        Me.DiscountValue = Utils.RoundValue(CDec(ValorSubtotal * (Me.DiscountPercent / 100)), roundingDecimals)
        Me.SubTotal = ValorSubtotal
        'valor iva
        IvaValue = Utils.RoundValue(CDec((Me.SubTotal - DiscountValue) * (Me.IvaPercent / 100)), roundingDecimals)
        'valor total
        TotalValue = (Me.SubTotal - DiscountValue) + IvaValue

        subtotalChangeFlag = False

        'lógica de valor del iva del producto
        If product?.IVAId IsNot Nothing Then
            INDTxtIvaValue.EditValue = Utils.RoundValue(CDec(((Me.SubTotal - DiscountValue) / If(INDTxtQuantity.EditValue = 0, 1, INDTxtQuantity.EditValue)) * _ivaProduct.Percentage / 100), roundingDecimals)
        End If

        ctrTmp.PrintInfo()
    End Sub

    Private Sub subtotalChange(sender As Object, e As EventArgs) Handles INDTxtSubTotal.EditValueChanged
        If Not subtotalChangeFlag And ProductHierarchy > 0 And INDTxtQuantity.EditValue > 0 Then
            Me.LastPurcharse = Me.SubTotal / INDTxtQuantity.EditValue
            Dim ValorSubtotal = INDTxtQuantity.EditValue * Me.LastPurcharse
            'valor descuento
            DiscountValue = Utils.RoundValue(CDec(ValorSubtotal * (Me.DiscountPercent / 100)), roundingDecimals)
            'valor subtotal
            Me.SubTotal = ValorSubtotal - DiscountValue
            'valor iva
            IvaValue = Utils.RoundValue(CDec((Me.SubTotal - DiscountValue) * (Me.IvaPercent / 100)), roundingDecimals)
            'valor total
            TotalValue = (Me.SubTotal - DiscountValue) + IvaValue
            INDLyTxtSubTotal.Control.BackColor = Color.White
            INDLyTxtSubTotal.Control.ForeColor = Color.White
        End If
    End Sub



    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

    Private Sub AddInventoryProduct()
        Try
            INDBtnAddProduct.Enabled = False
            '********************* Valida los Campos esten diligenciados ****************'
            If ValidateControls() = False OrElse product Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                INDBtnAddProduct.Enabled = True
                INDPceProducts.Focus()
                Exit Sub
            End If
            If INDTxtQuantity.EditValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad debe ser mayor a 0"
                INDBtnAddProduct.Enabled = True
                Exit Sub
            End If
            '********************* Defino el evento de retorno ****************'
            Dim args As AddProductPurchaseOrder
            '********************* Verifico si el item es para actualziar o agregar ****************'
            If editModeForm Then
                If Detail.Id > 0 Then
                    Detail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                    product.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                End If
                Detail.InventoryProduct = product
                Detail.ProductCode = product.Code
                Detail.ProductName = product.Name
                Detail.ManufacturerName = product.ManufacturerDescription
                Detail.HealthRegistration = product.HealthRegistration
                Detail.Presentation = product.Presentation
                Detail.Quantity = INDTxtQuantity.EditValue
                Detail.OutstandingQuantity = IIf(_quantityConfirmed > 0, INDTxtQuantity.EditValue - _quantityConfirmed, INDTxtQuantity.EditValue)
                Detail.CancelledQuantity = If(Detail.CancelledQuantity = 0, 0, Detail.CancelledQuantity)
                Detail.Value = Me.LastPurcharse
                Detail.SubTotalValue = Utils.RoundValue(Me.SubTotal, roundingDecimals)
                Detail.IvaPercentage = Me.IvaPercent
                Detail.IvaValue = Utils.RoundValue(IvaValue, roundingDecimals)
                Detail.DiscountPercentage = INDTxtDiscountPercent.EditValue
                Detail.DiscountValue = DiscountValue
                Detail.TotalValue = Utils.RoundValue(TotalValue, roundingDecimals)
                Detail.HierarchyQuantity = ProductHierarchyQuantity
                Detail.ProductHierarchyId = ProductHierarchy
                Detail.TotalIva = Utils.RoundValue((Me.SubTotal * (IvaPercent / 100)), roundingDecimals)
                args = New AddProductPurchaseOrder
                args.ItemPurchaseOrderDetail = Detail
                args.ListPurchaseOrderDetail = Nothing
            Else
                If ListProduct Is Nothing Then
                    ListProduct = New TrackableCollection(Of PurchaseOrderDetail)
                End If
                If ListProduct.Count <> 0 Then
                    For Each contractProduct In ListProduct
                        If contractProduct.ProductCode = product.Code Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("DistributionLineDetailExist")
                            INDBtnAddProduct.Enabled = True
                            CleanControls()
                            Exit Sub
                        End If
                    Next
                End If

                Detail = New PurchaseOrderDetail()

                Detail.InventoryProduct = product
                Detail.ProductCode = product.Code
                Detail.ProductName = product.Name
                Detail.ManufacturerName = product.ManufacturerDescription
                Detail.HealthRegistration = product.HealthRegistration
                Detail.Presentation = product.Presentation
                Detail.Quantity = INDTxtQuantity.EditValue
                Detail.OutstandingQuantity = INDTxtQuantity.EditValue
                Detail.CancelledQuantity = 0
                Detail.Value = Me.LastPurcharse
                Detail.SubTotalValue = Utils.RoundValue(Me.SubTotal, roundingDecimals)
                Detail.IvaPercentage = Me.IvaPercent
                Detail.IvaValue = Utils.RoundValue(IvaValue, roundingDecimals)
                Detail.DiscountPercentage = INDTxtDiscountPercent.EditValue
                Detail.DiscountValue = DiscountValue
                Detail.TotalValue = Utils.RoundValue(TotalValue, roundingDecimals)
                Detail.HierarchyQuantity = ProductHierarchyQuantity
                Detail.ProductHierarchyId = ProductHierarchy
                Detail.TotalIva = Utils.RoundValue((Me.SubTotal * (IvaPercent / 100)), roundingDecimals)
                ListProduct.Add(Detail)

                args = New AddProductPurchaseOrder
                args.ListPurchaseOrderDetail = ListProduct
                args.ItemPurchaseOrderDetail = Nothing
            End If

            Detail = Nothing
            CleanControls()
            RaiseEvent AddProductContract(Nothing, args)
            INDBtnAddProduct.Enabled = True
            INDPceProducts.Focus()
            If editModeForm Then
                Me.Close()
            End If
        Catch ex As Exception
            INDBtnAddProduct.Enabled = True
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' evento publico para agregar un concepto de recibo de caja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddProductContract(sender As Object, e As AddProductPurchaseOrder)

    Private Async Function LoadControls() As Task
        Await GetProduct(ProductId)
        If product IsNot Nothing Then
            INDPceProducts.Text = product.Code + " - " + product.Name
        End If

        INDTxtQuantity.EditValue = Detail.Quantity
        Me.SubTotal = Detail.SubTotalValue
        Me.IvaPercent = Detail.IvaPercentage
        Me.ProductHierarchyQuantity = If(Detail.HierarchyQuantity Is Nothing, 0, Detail.HierarchyQuantity)
        Me.ProductHierarchy = Detail.ProductHierarchyId
        INDTxtDiscountPercent.EditValue = Detail.DiscountPercentage
        IvaValue = Detail.IvaValue
        DiscountValue = Detail.DiscountValue
        TotalValue = Detail.TotalValue

        ctrTmp.PrintInfo()
    End Function

    Private Async Function GetProduct(ProductId As Integer) As Task
        Using model As New MInventoryProduct(Me.Tag)
            product = Await model.GetInventoryProductById(ProductId)

            INDTxtProductName.Text = product.Name
            INDTxtUnitPurcharse.Text = product.PackingUnitDescription
            INDTxtMeasureUnit.Text = product.MeasureUnitDescription
            'si es editado coloco el valor anterior, sino el valor del producto
            Dim _finalProductCost As Decimal = 0

            If Detail Is Nothing Then
                _finalProductCost = If(product.FinalProductCost Is Nothing, 0, product.FinalProductCost)
                _finalProductCost = Math.Round(_finalProductCost / Me._tRMValue, 2, MidpointRounding.AwayFromZero)
            Else
                _finalProductCost = Detail.Value
            End If

            Using modelIva As New MGeneralLedgerIVA(Me.Tag)
                If ConsSettingsIvaCost Then
                    If product?.IVAId IsNot Nothing Then
                        _ivaProduct = modelIva.GetGeneralLedgerIVAById(product.IVAId).ObjectEmbbeded
                    End If
                    Using msearch As New MBusqueda
                        Dim filter() As Object = {BarraBotones.OperatingUnitValue}
                        Dim settings As XPCollection(Of SettingInventoryXpo) = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetSettingInventoryByOperatingUnit, filter)
                        If settings Is Nothing OrElse settings.Count = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = "No existe parametros de inventario para la unidad operativa seleccionada. Contacte al administrador."
                        Else
                            Select Case settings(0).TaxRegistration
                                Case 1, 3
                                    settingsIvaCost = True
                                Case 2
                                    settingsIvaCost = False
                                Case Else
                                    Mensaje(EeventViewerImages.Advertencia) = "Parámetro de contabilizacion del IVA no es válido. Contacte al administrador."
                                    Return
                            End Select

                        End If
                    End Using
                    ConsSettingsIvaCost = False
                End If

                Me.LastPurcharse = Math.Round(_finalProductCost, 2, MidpointRounding.AwayFromZero)

                If settingsIvaCost AndAlso Detail Is Nothing Then
                    Me.LastPurcharse = Math.Round(Me.LastPurcharse / (1 + (IIf(_ivaProduct?.Percentage IsNot Nothing, _ivaProduct?.Percentage, 0) / 100)), 2, MidpointRounding.AwayFromZero)
                End If


            End Using

            If Not String.IsNullOrEmpty(product.PercentageIVA) Then
                Me.IvaPercent = product.PercentageIVA
            End If
            ''Se consulta la jerarquia del producto para asignar el datasource del campo jerarquia

            Dim HierarchyProduct = model.GetProductHierarchyXpo(ProductId)
            If HierarchyProduct.Any() Then
                INDlyItemProductHierarchy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleProductHierarchy.Properties.DataSource = HierarchyProduct
                INDLyTxtUnitPurcharse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDlyItemProductHierarchy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyTxtUnitPurcharse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyTxtProductHierarchyQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyTxtUnitPurcharse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDTxtSubTotal.ReadOnly = True
                INDLyTxtSubTotal.Enabled = False
                INDTxtQuantity.ReadOnly = False
                ProductHierarchy = Nothing
                ProductHierarchyQuantity = 0
            End If
        End Using
    End Function

    Private Sub PopupProductsPurchaseOrder_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub PopupProductsPurchaseOrder_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub INDTxtDiscountPercent_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtDiscountPercent.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAddProduct.Focus()
        End If
    End Sub

    Private Sub INDsleProductHierarchy_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProductHierarchy.EditValueChanged
        If ProductHierarchy > 0 Then
            SubTotal = LastPurcharse * INDTxtQuantity.EditValue
            INDLyTxtProductHierarchyQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLyTxtUnitPurcharse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDTxtSubTotal.ReadOnly = False
            INDLyTxtSubTotal.Enabled = True
            INDLyTxtSubTotal.Control.BackColor = Color.White
            INDTxtQuantity.ReadOnly = True
        Else

            INDLyTxtProductHierarchyQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLyTxtUnitPurcharse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDTxtSubTotal.ReadOnly = True
            INDLyTxtSubTotal.Enabled = False
            INDTxtQuantity.ReadOnly = False
        End If
    End Sub

    Private Sub INDTxtProductHierarchyQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtProductHierarchyQuantity.EditValueChanged
        If (ProductHierarchy > 0 And ProductHierarchyQuantity > 0) Then
            Using model As New MInventoryProduct(Me.Tag)
                Dim row = model.GetProductHierarchyXpoById(ProductHierarchy)
                If row IsNot Nothing Then
                    INDTxtQuantity.EditValue = row.Quantity * ProductHierarchyQuantity
                End If
            End Using
        End If
    End Sub
End Class