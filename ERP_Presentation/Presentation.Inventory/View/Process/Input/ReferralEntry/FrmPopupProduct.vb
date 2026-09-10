'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Ernesto Córdoba
' Created          : 16-12-2014
'
' Last Modified By : Carlos Mario Arias Rubiano
' Last Modified On : 03/07/2015
' Description      : Se habilito la opción de poder digitar el código
'                    del producto para poder consultarlo y evitar
'                    que abran el popup para seleccionarlo.
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
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Presentation.Accounting.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class FrmPopupProduct
#Region "BUILDER"
    Sub New(Optional _currency As Currency = Nothing, Optional _tRMValue As Decimal = 1)
        InitializeComponent()
        ctrTmp = New CtrRemissionSource()
        ctrTmp.SetInfoFunction(AddressOf getInfo)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        Me._tRMValue = _tRMValue
        Me._indigo = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)
        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)
    End Sub
#End Region

#Region "EVENTS"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddRemissionEntranceDetail(sender As Object, e As AddRemissionEntranceDetailEventArgs)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"
    ''' <summary>
    ''' control que se muestra en la barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Dim ctrTmp As CtrRemissionSource
    ''' <summary>
    ''' entidad de producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim product As InventoryProduct
    ''' <summary>
    ''' listado de los lotes
    ''' </summary>
    ''' <remarks></remarks>
    Dim listBatchSerial As List(Of BatchSerial)
    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim remissionEntranceDetail As RemissionEntranceDetail
    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean
    ''' <summary>
    ''' bandera para saber si se esta importando informacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _importDataMode As Boolean
    ''' <summary>
    ''' listado del detalle de la remision cuando se importa imformacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listRemissionEntranceDetailImportInfo As List(Of RemissionEntranceDetail)
    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listRemissionEntranceDetailValidation As List(Of RemissionEntranceDetail)
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

#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' propiedad para para pasar el listado del detalla de la remision
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListRemissionEntranceDetailValidation As List(Of RemissionEntranceDetail)
        Set(value As List(Of RemissionEntranceDetail))
            If value IsNot Nothing Then
                _listRemissionEntranceDetailValidation = New List(Of RemissionEntranceDetail)(value.ToArray())
            End If
        End Set
    End Property
    ''' <summary>
    ''' propiedad para para pasar el listado del detalle de la remision cuando se importa imformacion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListRemissionEntranceDetailImportInfo As List(Of RemissionEntranceDetail)
        Set(value As List(Of RemissionEntranceDetail))
            _listRemissionEntranceDetailImportInfo = value
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
    ''' propiedad para establecer si se esta importando informacion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ImportDataMode As Boolean
        Set(value As Boolean)
            _importDataMode = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property RemissionEntranceDetailEdit As RemissionEntranceDetail
        Set(value As RemissionEntranceDetail)
            remissionEntranceDetail = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            INDSeQuantity.Enabled = value
            INDPceBatchSerial.Enabled = value
            INDTxtUnitValue.Enabled = value
        End Set
    End Property

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

    ''' <summary>
    '''  propiedad que obtiene o establece el valor de la  ultima compra
    ''' </summary>
    ''' <returns></returns>
    Private Property LastPurcharse As Decimal
        Get
            Return INDTxtLastPurcharse.EditValue
        End Get
        Set(value As Decimal)
            INDTxtLastPurcharse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el valor unitario
    ''' </summary>
    ''' <returns></returns>
    Private Property UnitValue As Decimal


    ''' <summary>
    ''' propiedad que obtiene o establece el valor unitario
    ''' </summary>
    ''' <returns></returns>
    Private Property GrossUnitValue As Decimal
        Get
            Return INDTxtUnitValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtUnitValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el valor del IVA
    ''' </summary>
    ''' <returns></returns>
    Private Property IvaValue As Decimal
        Get
            Return INDTxtIvaValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtIvaValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''  propiedad que obtiene o establece el valor del subtotal
    ''' </summary>
    ''' <returns></returns>
    Private Property Subtotal As Decimal
        Get
            Return INDTxtSubtotal.EditValue
        End Get
        Set(value As Decimal)
            INDTxtSubtotal.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''  propiedad que obtiene o establece el valor del subtotal
    ''' </summary>
    ''' <returns></returns>
    Private Property DiscountValue As Decimal

    ''' <summary>
    '''  propiedad que obtiene o establece el valor del subtotal con descuento(neto)
    ''' </summary>
    ''' <returns></returns>
    Private Property NetSubtotal As Decimal
        Get
            Return INDTxtNetSubtotal.EditValue
        End Get
        Set(value As Decimal)
            INDTxtNetSubtotal.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''  propiedad que obtiene o establece el valor del porcentaje de descuento
    ''' </summary>
    ''' <returns></returns>
    Private Property IvaPercentage As Decimal
        Get
            Return INDSeIVAPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDSeIVAPercentage.EditValue = value
        End Set
    End Property


    ''' <summary>
    '''  propiedad que obtiene o establece el valor del porcentaje de descuento
    ''' </summary>
    ''' <returns></returns>
    Private Property DiscountPercentage As Decimal
        Get
            Return INDSeDiscountPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDSeDiscountPercentage.EditValue = value
        End Set
    End Property


    ''' <summary>
    '''  propiedad que obtiene o establece el valor del porcentaje de descuento
    ''' </summary>
    ''' <returns></returns>
    Private Property Total As Decimal
        Get
            Return INDTxtTotal.EditValue
        End Get
        Set(value As Decimal)
            INDTxtTotal.EditValue = value
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

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        CtrBatchSerial1.CleanControls()
        remissionEntranceDetail = Nothing
        ActionsControls = False
        INDPceProduct.Text = String.Empty
        INDPceProduct.Properties.ReadOnly = False
        INDPceBatchSerial.EditValue = Nothing
        product = Nothing
        INDTxtUnitPurcharse.Text = String.Empty
        Me.LastPurcharse = 0F
        INDSeQuantity.EditValue = 0
        Me.GrossUnitValue = 0F
        Me.IvaValue = 0F
        INDPceProduct.Focus()
        listBatchSerial = Nothing
        _listRemissionEntranceDetailImportInfo = Nothing
        BarraBotones.FilterDataSource = Nothing
        _importDataMode = False
        _editMode = False
        INDSeDiscountPercentage.ReadOnly = _importDataMode
        ctrTmp.PrintInfo()
        DiscountPercentage = 0
        NetSubtotal = 0F
        GrossUnitValue = 0F
        IvaPercentage = 0
        Total = 0F
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If _importDataMode = False Then
            If product Is Nothing Then
                errors.AppendLine(INDLciProduct.Text + ResourceManager.GetString("Empty"))
            End If
            If _editMode = False AndAlso product IsNot Nothing Then
                'valido que no se agregue el mismo producto con tipo fuente ninguno

                If _listRemissionEntranceDetailValidation IsNot Nothing AndAlso _listRemissionEntranceDetailValidation.Count > 0 Then
                    Dim remissionEntranceDetailTmp = _listRemissionEntranceDetailValidation.Find(Function(x) x.ProductId = product.Id And x.RemissionSource = 1)
                    If remissionEntranceDetailTmp IsNot Nothing Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("SourceProductNone", MODULE_NAME), remissionEntranceDetailTmp.CodeNameProduct))
                    End If
                End If
            End If
            If INDSeQuantity.EditValue = 0 Then
                errors.AppendLine(INDLciQuantity.Text + ResourceManager.GetString("Empty"))
            End If
            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If _editMode = False Then
                    If listBatchSerial Is Nothing Then
                        errors.AppendLine(INDLciBatchSerial.Text + ResourceManager.GetString("Empty"))
                    End If
                    If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count = 0 Then
                        errors.AppendLine(INDLciBatchSerial.Text + ResourceManager.GetString("Empty"))
                    End If
                Else
                    If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count = 0 Then
                        errors.AppendLine(INDLciBatchSerial.Text + ResourceManager.GetString("Empty"))
                    End If
                End If
            End If
            If GrossUnitValue = 0 Then
                errors.AppendLine(INDLciUnitValue.Text + ResourceManager.GetString("Empty"))
            End If
            If DiscountPercentage > 100 Then
                errors.AppendLine(String.Format("El porcentaje de descuento no puede ser mayor al 100%"))
            End If
        Else
            'recorro todos los items que se importaron para hacer las validaciones
            For Each item In _listRemissionEntranceDetailImportInfo
                Using model As New MInventoryProduct(Me.Tag)
                    product = model.GetInventoryProductByIdSimple(item.ProductId)
                End Using
                Using model As New MSubGroup(Me.Tag)
                    If product.ProductSubGroupId = 0 Then
                        errors.AppendLine(String.Format("El producto del item {0} no tiene un SubGrupo asociado.", (_listRemissionEntranceDetailImportInfo.IndexOf(item) + 1).ToString()))
                    Else
                        Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
                        If subGroup.ObjectEmbbeded.HandlesBatch = True And item.RemissionEntranceDetailBatchSerial.Count = 0 Then
                            'valido que si el producto maneja lote se encuentre agregado minimo un lote en RemissionEntranceDetailBatchSerial 
                            errors.AppendLine(String.Format(ResourceManager.GetString("HandlesBatch", MODULE_NAME), (_listRemissionEntranceDetailImportInfo.IndexOf(item) + 1).ToString()))
                        Else
                            If item.Quantity = 0 Then
                                errors.AppendLine(String.Format(ResourceManager.GetString("QuantityZero", MODULE_NAME), (_listRemissionEntranceDetailImportInfo.IndexOf(item) + 1).ToString()))
                            End If
                        End If
                    End If
                End Using
                If item.Quantity > item.QuantityImport Then
                    If item.RemissionSource = 2 Then
                        errors.AppendLine("La cantidad pendiente del producto " + item.CodeNameProduct + " en la orden de compra del item " + (_listRemissionEntranceDetailImportInfo.IndexOf(item) + 1).ToString() + " es menor a la cantidad que se va agregar")
                    Else
                        errors.AppendLine("La cantidad pendiente del producto " + item.CodeNameProduct + " en el contrato del item " + (_listRemissionEntranceDetailImportInfo.IndexOf(item) + 1).ToString() + " es menor a la cantidad que se va agregar")
                    End If

                End If
                If item.GrossUnitValue = 0 Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("ValueZero", MODULE_NAME), (_listRemissionEntranceDetailImportInfo.IndexOf(item) + 1).ToString()))
                End If
                If item.DiscountPercentage > 100 Then
                    errors.AppendLine(String.Format("El porcentaje de descuento no puede ser mayor al 100%"))
                End If
            Next
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        If _editMode = False Then
            remissionEntranceDetail = New RemissionEntranceDetail
        End If
        With remissionEntranceDetail
            If _editMode = False Then
                .RemissionSource = 1
            End If

            .ProductId = product.Id
            .CodeNameProduct = product.Code + " - " + product.Name
            .ManufacturerName = product.ManufacturerDescription
            .HealthRegistration = product.HealthRegistration
            .Presentation = product.Presentation
            .Quantity = INDSeQuantity.EditValue
            .UnitValue = Math.Round((GrossUnitValue - (GrossUnitValue * (DiscountPercentage / 100))), 2, MidpointRounding.AwayFromZero)
            .GrossUnitValue = GrossUnitValue
            .LastValue = LastPurcharse
            'valor subtotal
            .SubTotalValue = .GrossUnitValue * .Quantity
            .DiscountPercentage = DiscountPercentage
            'valor descuento
            .DiscountValue = Math.Round(.SubTotalValue * .DiscountPercentage / 100, 2, MidpointRounding.AwayFromZero)
            'valor neto
            .NetDiscount = .SubTotalValue - .DiscountValue

            If product.IVAId IsNot Nothing Then
                Using model As New MGeneralLedgerIVA(Me.Tag)
                    Dim iva = model.GetGeneralLedgerIVAById(product.IVAId)
                    .IvaPercentage = IvaPercentage
                    IvaPercentage = .IvaPercentage
                    'valor iva
                    .IvaValue = Math.Round((.SubTotalValue - .DiscountValue) * (IvaPercentage / 100), 2, MidpointRounding.AwayFromZero)
                End Using
            End If
            'valor total
            .TotalValue = .NetDiscount + .IvaValue

            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                'si se esta editando el registro elimino los detalles de RemissionEntranceDetailBatchSerial 
                If _editMode = True Then
                    If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count > 0 Then
                        While .RemissionEntranceDetailBatchSerial.Count > 0
                            If .RemissionEntranceDetailBatchSerial(0).Id > 0 Then
                                .RemissionEntranceDetailBatchSerial(0).MarkAsDeleted()
                            Else
                                .RemissionEntranceDetailBatchSerial.Remove(.RemissionEntranceDetailBatchSerial(0))
                            End If
                        End While
                    End If
                End If

                If listBatchSerial IsNot Nothing Then
                    For Each item In listBatchSerial
                        Dim remissionEntranceDetailBatchSerial As New RemissionEntranceDetailBatchSerial
                        remissionEntranceDetailBatchSerial.BatchSerialId = item.Id
                        remissionEntranceDetailBatchSerial.Quantity = item.Quantity
                        remissionEntranceDetailBatchSerial.OutstandingQuantity = item.Quantity
                        remissionEntranceDetailBatchSerial.CodeBatchSerial = item.BatchCode
                        .RemissionEntranceDetailBatchSerial.Add(remissionEntranceDetailBatchSerial)
                    Next
                End If
            Else
                ' elimino todos los registros de RemissionEntranceDetailBatchSerial
                While .RemissionEntranceDetailBatchSerial.Count > 0
                    If .RemissionEntranceDetailBatchSerial(0).Id > 0 Then
                        .RemissionEntranceDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        .RemissionEntranceDetailBatchSerial.Remove(.RemissionEntranceDetailBatchSerial(0))
                    End If
                End While
                Dim remissionEntranceDetailBatchSerial As New RemissionEntranceDetailBatchSerial
                remissionEntranceDetailBatchSerial.Quantity = INDSeQuantity.EditValue
                remissionEntranceDetailBatchSerial.OutstandingQuantity = INDSeQuantity.EditValue
                .RemissionEntranceDetailBatchSerial.Add(remissionEntranceDetailBatchSerial)
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <param name="remissionEntranceDetailTmp"></param>
    ''' <remarks></remarks>
    Private Sub LoadControls(Optional remissionEntranceDetailTmp As RemissionEntranceDetail = Nothing)
        If _editMode = True Then
            INDBtnAdd.Text = ResourceManager.GetString("Edit")
        End If

        If _importDataMode Then
            remissionEntranceDetail = remissionEntranceDetailTmp
        End If

        With remissionEntranceDetail
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(.ProductId)
                INDPceProduct.Text = product.Code + " - " + product.Name
                If _importDataMode = True Then
                    remissionEntranceDetail.CodeNameProduct = INDPceProduct.Text
                End If
                INDPceProduct.Properties.ReadOnly = True
                INDTxtUnitPurcharse.Text = product.PackingUnitDescription
                LastPurcharse = Math.Round((product.FinalProductCost.Value / Me._tRMValue), 2, MidpointRounding.AwayFromZero)
            End Using
            Using model As New MSubGroup(Me.Tag)
                If product.ProductSubGroupId > 0 Then
                    Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
                    If subGroup.ObjectEmbbeded.HandlesBatch = True Then
                        INDSeQuantity.Properties.ReadOnly = True
                        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        INDSeQuantity.Properties.ReadOnly = False
                        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If
                End If
            End Using
            INDSeQuantity.EditValue = .Quantity
            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If .RemissionEntranceDetailBatchSerial.Count = 0 Then
                    INDPceBatchSerial.EditValue = String.Empty
                Else
                    INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", MODULE_NAME), .RemissionEntranceDetailBatchSerial.Count.ToString())
                End If
                CtrBatchSerial1.Product = product
                CtrBatchSerial1.FormOwner = Me
                If _importDataMode = True Then
                    CtrBatchSerial1.CleanControls()
                End If
                CtrBatchSerial1.SetListBatchSerial()
                CtrBatchSerial1.SetQuantityBatchSerial(.RemissionEntranceDetailBatchSerial.ToList())
            End If

            IvaPercentage = .IvaPercentage
            IvaValue = .IvaValue
            DiscountPercentage = .DiscountPercentage

            'si se esta editando coloco el valor que asignaron sino el ultimo valor del producto
            If (_editMode OrElse _importDataMode) AndAlso .GrossUnitValue <> 0 Then
                GrossUnitValue = .GrossUnitValue
            Else
                GrossUnitValue = Math.Round((product.FinalProductCost.Value / Me._tRMValue), 2, MidpointRounding.AwayFromZero)
            End If
        End With
        updateTotal()
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' metodo para obtener la informacion y colocarla en el control de usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        If remissionEntranceDetail IsNot Nothing Then
            Return New Tuple(Of String, String)(remissionEntranceDetail.RemissionSource.ToString(), remissionEntranceDetail.SourceCode)
        Else
            Return New Tuple(Of String, String)(1, String.Empty)
        End If
    End Function

    ''' <summary>
    ''' Lanza la consulta del producto para poderlo seleccionar
    ''' sin necesidad de desplegar el control de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AssignProductWithCode() As Task
        If INDPceProduct.Text.Trim <> String.Empty AndAlso _importDataMode = False Then
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
                Dim resultProduct = Await model.GetInventoryProduct(INDPceProduct.Text.Trim)
                If resultProduct.StateResult = False Then
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
        ElseIf INDPceProduct.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDPceProduct.Text = product.Code + " - " + product.Name
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

        Me.changeNumericFormatByCurrency(New Globalization.CultureInfo(_currencyAbbreviation.GetCultureId()).NumberFormat)
    End Sub
#End Region

#Region "HANDLES"
#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        product = Nothing
        listBatchSerial = Nothing
        remissionEntranceDetail = Nothing
        _editMode = Nothing
        _importDataMode = Nothing
        _listRemissionEntranceDetailImportInfo = Nothing
        _listRemissionEntranceDetailValidation = Nothing
        _wareHouseId = Nothing
        _operatingUnitId = Nothing
    End Sub

    Private Sub FrmPopupProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True
        CtrBatchSerial1.RemissionType = ERemissionType.Input

        If Me._headCurrency?.Id <> Me._indigo?.OfficialCurrencyId AndAlso Me._tRMValue = 1 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("TRMEmpty")
            Me.Close()
        End If

        INDSeDiscountPercentage.ReadOnly = _importDataMode

        If _editMode = True Then
            'si esta editando un registro
            LoadControls()
        ElseIf _importDataMode = True Then
            'si se esta importando informacion
            If _listRemissionEntranceDetailImportInfo.Count > 1 Then
                Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Producto", .FieldName = "CodeNameProduct"}}.ToList()
                BarraBotones.FilterDataSource = _listRemissionEntranceDetailImportInfo
            Else
                LoadControls(_listRemissionEntranceDetailImportInfo(0))
            End If
        Else
            'si es un nuevo registro
            CleanControls()
        End If
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
        If _importDataMode = True Then
            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDPceBatchSerial.EditValue Is String.Empty Then
                    INDPceBatchSerial.Focus()
                End If
            Else
                If INDSeQuantity.EditValue = 0 Then
                    INDSeQuantity.Focus()
                End If
            End If
        End If
        If _editMode = True Then
            INDPceProduct.Focus()
        End If
    End Sub
#End Region

#Region "SelectProduct"
    ''' <summary>
    ''' Handles the SelectProduct event of the CtrProducts1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="SelectProductEventArgs"/> instance containing the event data.</param>
    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProduct.Text = e.CodeNameProduct
        INDPceProduct.Focus()
        INDPceProduct.ClosePopup()
        CtrBatchSerial1.CleanControls()
        INDSeQuantity.EditValue = 0
        INDPceBatchSerial.EditValue = Nothing
        ActionsControls = True
        Using model As New MInventoryProduct(Me.Tag)
            product = model.GetInventoryProductByIdSimple(e.ProductId)
            If product Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("Empty")
                CleanControls()
                Exit Sub
            End If
            INDTxtUnitPurcharse.Text = product.PackingUnitDescription
            Using modelIva As New MGeneralLedgerIVA(Me.Tag)
                If product?.IVAId IsNot Nothing Then
                    Dim iva = modelIva.GetGeneralLedgerIVAById(product.IVAId)
                    IvaPercentage = iva.ObjectEmbbeded.Percentage
                End If
                Using msearch As New MBusqueda
                    Dim filter() As Object = {BarraBotones.OperatingUnitValue}
                    Dim settings As XPCollection(Of SettingInventoryXpo) = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetSettingInventoryByOperatingUnit, filter)
                    product.FinalProductCost = If(product?.FinalProductCost Is Nothing, 0, product?.FinalProductCost)

                    Select Case settings?.FirstOrDefault?.TaxRegistration
                        Case 1, 3
                            LastPurcharse = Math.Round(product.FinalProductCost.Value / Me._tRMValue, 2, MidpointRounding.AwayFromZero)
                            GrossUnitValue = Math.Round((product.FinalProductCost.Value / (1 + (IvaPercentage / 100))) / Me._tRMValue, 2, MidpointRounding.AwayFromZero)
                        Case 2
                            LastPurcharse = Math.Round((product.FinalProductCost.Value / Me._tRMValue), 2, MidpointRounding.AwayFromZero)
                            GrossUnitValue = Math.Round((product.FinalProductCost.Value / Me._tRMValue), 2, MidpointRounding.AwayFromZero)
                        Case Else
                            Mensaje(EeventViewerImages.Advertencia) = "Debe parámetrizar un IVA"
                            Me.CleanControls()
                            Exit Sub
                    End Select
                End Using
            End Using
        End Using
        Using model As New MSubGroup(Me.Tag)
            If product.ProductSubGroup IsNot Nothing Then
                If product.ProductSubGroup.HandlesBatch = True Then
                    INDSeQuantity.Properties.ReadOnly = True
                    INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDPceBatchSerial.Focus()
                    If _editMode = True Then
                        'instancio el listado para poder hacer la validacion de los lotes cuando se esta editando y saber si esta sin registros
                        listBatchSerial = New List(Of BatchSerial)
                    End If
                    INDPceBatchSerial.Focus()
                    INDPceBatchSerial.ShowPopup()
                Else
                    INDSeQuantity.Properties.ReadOnly = False
                    INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSeQuantity.Focus()
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "El producto " + String.Concat(product.Code, " - ", product.Name) + " no tiene un subgrupo asociado"
                CleanControls()
            End If
        End Using
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDPceProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProduct.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

    Private Sub INDPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceBatchSerial.QueryPopUp
        CtrBatchSerial1.Product = product
        CtrBatchSerial1.FormOwner = Me
        CtrBatchSerial1.SetListBatchSerial()
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

#Region "ChangeQuantity"
    Private Sub CtrBatchSerial1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrBatchSerial1.ChangeQuantity
        INDSeQuantity.EditValue = e.Quantity
    End Sub
#End Region

#Region "Closed"
    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerial.Closed
        listBatchSerial = CtrBatchSerial1.GetListBatchSerial()
        If listBatchSerial.Count = 0 Then
            INDPceBatchSerial.EditValue = String.Empty
        Else
            INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", MODULE_NAME), listBatchSerial.Count.ToString())
        End If
        If remissionEntranceDetail IsNot Nothing AndAlso _importDataMode = True Then
            If listBatchSerial IsNot Nothing Then
                remissionEntranceDetail.RemissionEntranceDetailBatchSerial.Clear()
                For Each item In listBatchSerial
                    Dim remissionEntranceDetailBatchSerial As New RemissionEntranceDetailBatchSerial
                    remissionEntranceDetailBatchSerial.BatchSerialId = item.Id
                    remissionEntranceDetailBatchSerial.Quantity = item.Quantity
                    remissionEntranceDetailBatchSerial.OutstandingQuantity = item.Quantity
                    remissionEntranceDetailBatchSerial.CodeBatchSerial = item.BatchCode
                    remissionEntranceDetail.RemissionEntranceDetailBatchSerial.Add(remissionEntranceDetailBatchSerial)
                Next
            Else
                Dim remissionEntranceDetailBatchSerial As New RemissionEntranceDetailBatchSerial
                remissionEntranceDetailBatchSerial.Quantity = INDSeQuantity.EditValue
                remissionEntranceDetailBatchSerial.OutstandingQuantity = INDSeQuantity.EditValue
                remissionEntranceDetail.RemissionEntranceDetailBatchSerial.Add(remissionEntranceDetailBatchSerial)
            End If
        End If
    End Sub
#End Region

#Region "Click"
    Private Async Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Try
            AsyncLoader(True)
            INDBtnAdd.Enabled = False
            Dim errors = ValidateControlsPopup()
            If errors.Length > 0 Then
                AsyncLoader(False)
                INDBtnAdd.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            'si no se esta importando informacion asigno valores
            If _importDataMode = False Then
                SetValues()
                Using modelSettings As New MSettingInventory(Me.Tag)
                    Dim resulValidateStock = Await modelSettings.ValidateStock(remissionEntranceDetail.ProductId, _operatingUnitId, _wareHouseId, remissionEntranceDetail.Quantity, InventoryStaticServices.MovementType.Input)
                    If resulValidateStock.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = resulValidateStock.Message
                    End If
                End Using
            End If
            If _listRemissionEntranceDetailValidation Is Nothing Then
                _listRemissionEntranceDetailValidation = New List(Of RemissionEntranceDetail)
            End If
            Dim args As New AddRemissionEntranceDetailEventArgs
            If _editMode = True Then
                args.RemissionEntranceDetail = remissionEntranceDetail
                args.EditMode = True
            Else
                args.RemissionEntranceDetail = remissionEntranceDetail
                _listRemissionEntranceDetailValidation.Add(remissionEntranceDetail)
            End If
            If _importDataMode = True Then
                args.ImportDataMode = True
                'asignamos las propiedades a la lista de detalles importada
                For Each item In _listRemissionEntranceDetailImportInfo
                    Dim productItem As New InventoryProduct
                    Using model As New MInventoryProduct(Me.Tag)
                        productItem = model.GetInventoryProductByIdSimple(item.ProductId)
                    End Using
                    item.CodeNameProduct = productItem.Code + " - " + productItem.Name
                    item.ManufacturerName = productItem.ManufacturerDescription
                    item.HealthRegistration = productItem.HealthRegistration
                    item.Presentation = productItem.Presentation
                    If item?.GrossUnitValue Is Nothing OrElse item?.GrossUnitValue = 0 Then
                        item.GrossUnitValue = Math.Round((productItem.FinalProductCost.Value / Me._tRMValue), 2, MidpointRounding.AwayFromZero)
                    End If
                    item.LastValue = Math.Round((productItem.FinalProductCost.Value / Me._tRMValue), 2, MidpointRounding.AwayFromZero)

                    If productItem.IVAId IsNot Nothing Then
                        If Not item.IvaPercentage > 0 Then
                            Using model As New MGeneralLedgerIVA(Me.Tag)
                                Dim iva = model.GetGeneralLedgerIVAById(productItem.IVAId)
                                item.IvaPercentage = iva.ObjectEmbbeded.Percentage
                                'item.IvaValue = item.Quantity * (item.GrossUnitValue * item.IvaPercentage / 100)
                            End Using
                            'Else
                            'item.IvaValue = item.Quantity * (item.GrossUnitValue * item.IvaPercentage / 100)
                        End If
                    End If

                    'valor unitario neto
                    item.UnitValue = Math.Round(((item.GrossUnitValue - ((item.DiscountPercentage / 100) * item.GrossUnitValue))), 2, MidpointRounding.AwayFromZero)
                    'valor subtotal
                    item.SubTotalValue = item.GrossUnitValue * item.Quantity
                    'valor descuento
                    item.DiscountValue = Math.Round(item.SubTotalValue * (item.DiscountPercentage / 100), 2, MidpointRounding.AwayFromZero)
                    'valor subtotal NETO
                    item.NetDiscount = item.SubTotalValue - item.DiscountValue
                    'valor del IVA
                    item.IvaValue = Math.Round((item.SubTotalValue - item.DiscountValue) * (item.IvaPercentage / 100), 2, MidpointRounding.AwayFromZero)
                    'valor del total
                    item.TotalValue = item.NetDiscount + item.IvaValue

                Next
                args.ListRemissionEntranceDetail = _listRemissionEntranceDetailImportInfo
                _listRemissionEntranceDetailValidation.AddRange(_listRemissionEntranceDetailImportInfo)
            End If
            RaiseEvent AddRemissionEntranceDetail(Nothing, args)
            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            CleanControls()
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub


#End Region


#Region "KeyDown"
    Private Sub FrmPopupProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDTxtUnitValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtUnitValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDPceProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProduct.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

#End Region

#Region "RecordNavigationChangeEvent"
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        LoadControls(Record)
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDSeQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeQuantity.EditValueChanged
        If remissionEntranceDetail IsNot Nothing AndAlso _importDataMode = True Then
            remissionEntranceDetail.Quantity = INDSeQuantity.EditValue
        End If
        If INDSeQuantity.EditValue > 0 Then
            updateTotal()
        End If
    End Sub

    Private Sub INDTxtUnitValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtUnitValue.EditValueChanged
        If remissionEntranceDetail IsNot Nothing AndAlso _importDataMode = True Then
            remissionEntranceDetail.GrossUnitValue = GrossUnitValue
            remissionEntranceDetail.LastValue = LastPurcharse
            If product.IVAId IsNot Nothing Then
                If Not IvaPercentage > 0 Then
                    Using model As New MGeneralLedgerIVA(Me.Tag)
                        Dim iva = model.GetGeneralLedgerIVAById(product.IVAId)
                        remissionEntranceDetail.IvaPercentage = iva.ObjectEmbbeded.Percentage
                        'remissionEntranceDetail.IvaValue = Math.Round(remissionEntranceDetail.GrossUnitValue * iva.ObjectEmbbeded.Percentage / 100, 2, MidpointRounding.AwayFromZero)
                        'IvaValue = remissionEntranceDetail.IvaValue
                    End Using
                Else
                    remissionEntranceDetail.IvaPercentage = IvaPercentage
                    'remissionEntranceDetail.IvaValue = Math.Round(remissionEntranceDetail.GrossUnitValue * IvaPercentage / 100, 2, MidpointRounding.AwayFromZero)
                    'IvaValue = remissionEntranceDetail.IvaValue
                End If
            End If
            'valor subtotal
            remissionEntranceDetail.SubTotalValue = remissionEntranceDetail.GrossUnitValue * remissionEntranceDetail.Quantity
            'valor descuento
            remissionEntranceDetail.DiscountValue = Math.Round(remissionEntranceDetail.SubTotalValue * (remissionEntranceDetail.DiscountPercentage / 100), 2, MidpointRounding.AwayFromZero)
            'valor neto
            remissionEntranceDetail.NetDiscount = remissionEntranceDetail.SubTotalValue - remissionEntranceDetail.DiscountValue
            'valor IVA
            IvaValue = Math.Round((remissionEntranceDetail.SubTotalValue - remissionEntranceDetail.DiscountValue) * (remissionEntranceDetail.IvaPercentage / 100), 2, MidpointRounding.AwayFromZero)
            remissionEntranceDetail.IvaValue = IvaValue
            'valor total
            remissionEntranceDetail.TotalValue = remissionEntranceDetail.NetDiscount + remissionEntranceDetail.IvaValue
        Else
            If product Is Nothing Then
                Exit Sub
            End If

            If product.IVAId IsNot Nothing AndAlso Not IvaPercentage > 0 Then
                Using model As New MGeneralLedgerIVA(Me.Tag)
                    Dim iva = model.GetGeneralLedgerIVAById(product.IVAId)
                    IvaPercentage = iva.ObjectEmbbeded.Percentage
                End Using
            End If

            updateTotal()
        End If
    End Sub
#End Region

#Region "Popup"
    Private Sub INDPceBatchSerial_Popup(sender As Object, e As EventArgs) Handles INDPceBatchSerial.Popup
        CtrBatchSerial1.SetFocusGrid()
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

    Private Sub INDSeDiscountPercentage_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeDiscountPercentage.EditValueChanged
        If DiscountPercentage > 0 And INDSeQuantity.EditValue > 0 Then
            updateTotal()
        End If
    End Sub

    Public Sub updateTotal()
        Subtotal = GrossUnitValue * INDSeQuantity.EditValue
        DiscountValue = Math.Round(Subtotal * (DiscountPercentage / 100), 2, MidpointRounding.AwayFromZero)
        NetSubtotal = Subtotal - DiscountValue
        IvaValue = Math.Round(((Subtotal - DiscountValue) * (IvaPercentage / 100)), 2, MidpointRounding.AwayFromZero)
        Total = (NetSubtotal + IvaValue)
    End Sub

#End Region

End Class