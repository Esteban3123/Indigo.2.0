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

#End Region

Public Class PopupProductsContract

#Region "Builder"

    ''' <summary>
    ''' Inicializa una isntancia de la clase
    ''' </summary>
    ''' <param name="_Detaill"></param>
    ''' <param name="_listProducts"></param>
    ''' <remarks></remarks>
    Public Sub New(_ReadOnly As Boolean, _Detaill As InventoryContractDetail,
                   _listProducts As TrackableCollection(Of InventoryContractDetail), Optional _currency As Currency = Nothing, Optional _tRMValue As Decimal = 1)
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
    End Sub

#End Region

#Region "Globals"

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
    Private ListProduct As TrackableCollection(Of InventoryContractDetail) = New TrackableCollection(Of InventoryContractDetail)

    ''' <summary>
    ''' Objeto que establece el producto a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim Detail As InventoryContractDetail

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

    ''' <summary>
    ''' variable de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _headCurrency As Currency

    ''' <summary>
    ''' variable de la tasa de cambio, por defecto es 1 cuando la moneda es igual a la oficial
    ''' </summary>
    Private _tRMValue As Decimal = 1
#End Region

#Region "Properties"
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

    ''' <summary>
    ''' porcentaje del impuesto IVA
    ''' </summary>
    ''' <returns></returns>
    Private Property IVAPercent As Decimal
        Get
            Return INDTxtIvaPercent.EditValue
        End Get
        Set(value As Decimal)
            INDTxtIvaPercent.EditValue = value
        End Set
    End Property
#End Region

#Region "Methods"

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
                IVAPercent = product.PercentageIVA
            End Using
        ElseIf INDPceProducts.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDPceProducts.Text = product.Code + " - " + product.Description
            End If
        End If
    End Function

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
        INDTxtUnitPurcharse.EditValue = 0F
        INDTxtLastPurcharse.EditValue = 0F
        INDTxtQuantity.EditValue = 0F
        INDTxtSubTotal.EditValue = 0F
        IVAPercent = 0F
        INDTxtDiscountPercent.EditValue = 0F

        IvaValue = 0F
        DiscountValue = 0F
        TotalValue = 0F

        ReadOnlyControls(False)
        INDTxtIvaPercent.Properties.ReadOnly = True
        INDPceProducts.Focus()
    End Sub

    ''' <summary>
    ''' Carga los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        GetProduct(Detail.ProductId)
        INDPceProducts.Text = product.Code + " - " + product.Name
        INDTxtQuantity.EditValue = Detail.Quantity
        INDTxtSubTotal.EditValue = Detail.SubTotalValue
        IVAPercent = Detail.IvaPercentage
        INDTxtDiscountPercent.EditValue = Detail.DiscountPercentage
        IvaValue = Detail.IvaValue
        DiscountValue = Detail.DiscountValue
        TotalValue = Detail.TotalValue

        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Consulta el producto
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <remarks></remarks>
    Private Sub GetProduct(ProductId As Integer)
        Using model As New MInventoryProduct(Me.Tag)
            product = model.GetInventoryProductByIdSimple(ProductId)
            INDTxtProductName.Text = product.Name
            INDTxtUnitPurcharse.Text = product.PackingUnitDescription
            Dim _finalProductCost As Decimal = If(product.FinalProductCost Is Nothing, 0, product.FinalProductCost)
            INDTxtLastPurcharse.EditValue = Math.Round(_finalProductCost / _tRMValue, 2)
        End Using
    End Sub

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
        Return New Tuple(Of String, String, String, String)(String.Format(IvaValue, "c2"), String.Format(DiscountValue, "c2"), String.Format(TotalValue, "c2"), "")
    End Function

#End Region

#Region "Events"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupProductsContract_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Me.LoadControls()
            If OnlyRead Then
                Me.ReadOnlyControls(True)
            End If
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
    ''' cierra el form al presionar enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupProductsContract_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando seleccionas el producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProducts.Text = e.CodeNameProduct
        INDPceProducts.Focus()
        INDPceProducts.ClosePopup()
        GetProduct(e.ProductId)
        INDTxtQuantity.Focus()
        If product IsNot Nothing Then
            IVAPercent = product.PercentageIVA
        End If
    End Sub

    ''' <summary>
    ''' Despliega la busqueda de los productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProducts.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones para deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Despliega el formuarlio de busqueda para productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceProducts.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Realiza el calculo del producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sum_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtSubTotal.EditValueChanged, INDTxtQuantity.EditValueChanged, INDTxtLastPurcharse.EditValueChanged, INDTxtIvaPercent.EditValueChanged, INDTxtDiscountPercent.EditValueChanged
        If INDTxtSubTotal.EditValue Is Nothing OrElse INDTxtQuantity.EditValue Is Nothing OrElse INDTxtLastPurcharse.EditValue Is Nothing OrElse
           INDTxtIvaPercent.EditValue Is Nothing OrElse INDTxtDiscountPercent.EditValue Is Nothing Then
            Exit Sub
        End If

        INDTxtSubTotal.EditValue = INDTxtQuantity.EditValue * INDTxtLastPurcharse.EditValue

        DiscountValue = INDTxtSubTotal.EditValue * (INDTxtDiscountPercent.EditValue / 100)
        IvaValue = (INDTxtSubTotal.EditValue - DiscountValue) * (IVAPercent / 100)

        TotalValue = INDTxtSubTotal.EditValue + IvaValue - DiscountValue

        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Asigna el valor del iva que maneja el producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_EditValueChanged(sender As Object, e As EventArgs) Handles INDPceProducts.EditValueChanged
        'If product IsNot Nothing Then
        '    INDTxtIvaPercent.Text = product.IvaPercent
        'End If
    End Sub

#Region "FormClosing"
    Private Sub PopupProductsContract_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "KeyDown"

    Private Async Sub INDPceProducts_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProducts.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

#End Region

#End Region

#Region "Handless"
    ''' <summary>
    ''' Agrega los productos a la cabecera
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddInventoryProduct()
        '********************* Valida los Campos esten diligenciados ****************'
        If product Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        If ValidateControls() = False Then
            Exit Sub
        End If
        '********************* Defino el evento de retorno ****************'
        Dim args As AddProductContractEventArgs
        '********************* Verifico si el item es para actualziar o agregar ****************'
        If editModeForm Then
            Detail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified

            Detail.InventoryProduct = product
            Detail.Quantity = INDTxtQuantity.EditValue
            Detail.OutstandingQuantity = INDTxtQuantity.EditValue
            Detail.CancelledQuantity = 0
            Detail.Value = INDTxtLastPurcharse.EditValue
            Detail.SubTotalValue = INDTxtSubTotal.EditValue
            Detail.IvaPercentage = IVAPercent
            Detail.IvaValue = IvaValue
            Detail.DiscountPercentage = INDTxtDiscountPercent.EditValue
            Detail.DiscountValue = DiscountValue
            Detail.TotalValue = TotalValue

            args = New AddProductContractEventArgs
            args.ItemInventorycontractDetail = Detail
            args.ListInventoryContractDetail = Nothing
        Else
            If ListProduct Is Nothing Then
                ListProduct = New TrackableCollection(Of InventoryContractDetail)
            End If
            If ListProduct.Count <> 0 Then
                For Each contractProduct In ListProduct
                    If contractProduct.InventoryProduct.Code = product.Code Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("DistributionLineDetailExist")
                        CleanControls()
                        Exit Sub
                    End If
                Next
            End If

            Detail = New InventoryContractDetail()

            Detail.InventoryProduct = product
            Detail.Quantity = INDTxtQuantity.EditValue
            Detail.OutstandingQuantity = INDTxtQuantity.EditValue
            Detail.CancelledQuantity = 0
            Detail.Value = INDTxtLastPurcharse.EditValue
            Detail.SubTotalValue = INDTxtSubTotal.EditValue
            Detail.IvaPercentage = IVAPercent
            Detail.IvaValue = IvaValue
            Detail.DiscountPercentage = INDTxtDiscountPercent.EditValue
            Detail.DiscountValue = DiscountValue
            Detail.TotalValue = TotalValue

            ListProduct.Add(Detail)

            args = New AddProductContractEventArgs
            args.ListInventoryContractDetail = ListProduct
            args.ItemInventorycontractDetail = Nothing
        End If

        CleanControls()
        RaiseEvent AddProductContract(Nothing, args)
        INDPceProducts.Focus()
        If editModeForm Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' evento publico para agregar un concepto de recibo de caja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddProductContract(sender As Object, e As AddProductContractEventArgs)

#End Region

End Class