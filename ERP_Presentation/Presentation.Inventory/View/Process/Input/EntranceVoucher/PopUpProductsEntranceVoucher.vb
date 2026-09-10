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
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Presentation.Accounting.MVP
Imports Presentation.Maintenance.MVP
Imports Presentation.Payments.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports System.Globalization
Imports DevExpress.XtraEditors

#End Region

Public Class PopUpProductsEntranceVoucher

#Region "Builder"

    ''' <summary>
    ''' Inicializa una isntancia de la clase
    ''' </summary>
    ''' <param name="_Rounding"></param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal _Rounding As Integer = 1, Optional _currency As Currency = Nothing, Optional _tRMValue As Decimal = 1)
        InitializeComponent()
        ctrTmp = New CtrEntranceVoucherSource()
        ctrTmp.SetInfoFunction(AddressOf getInfo)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        Me._tRMValue = _tRMValue
        Me._indigo = SessionValues.Instance
        Me.Rounding = _Rounding
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)

        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)
    End Sub

#End Region

#Region "Globals"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrEntranceVoucherSource

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
    ''' Obtiene o establece el valor descuento del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NetoValue As Decimal = 0

    ''' <summary>
    ''' Obtiene o establece el valor Total del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalValue As Decimal
        Get
            Return INDTxtTotal.EditValue
        End Get
        Set(value As Decimal)
            INDTxtTotal.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Define el tipo de ingreso que se le da al producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim Source As String = String.Empty

    ''' <summary>
    ''' Define el codigo del ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Dim SourceCode As String = String.Empty

    ''' <summary>
    ''' Controla el calculo general dependiendo si esta importando
    ''' </summary>
    ''' <remarks></remarks>
    Dim flagImport As Boolean = False

    ''' <summary>
    ''' Listado de cantidades para validar que las cantidades sean menores de las solicitadas
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListValitadeImport As New List(Of Decimal)

    ''' <summary>
    ''' Variableu que contiene el valor del redondeo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Rounding As Integer

    ''' <summary>
    ''' id del almacen 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _wareHouseId As Integer

    ''' <summary>
    ''' propiedad para establecer el tipo de iva cuando en parametros de invetario esta como mixto
    ''' </summary>
    ''' <remarks></remarks>
    Dim _TaxRegistration As Byte?

    ''' <summary>
    ''' id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _operatingUnitId As Integer

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
#End Region

#Region "Events"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddEntranceVoucherDetail(sender As Object, e As AddProductEntranceVoucherDetailEventArgs)
#End Region

#Region "Properties"
    ''' <summary>
    ''' propiedad para para pasar el listado del detalla de la remision
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private Property _listEntranceVoucherDetailValidation As List(Of EntranceVoucherDetail)
    Public WriteOnly Property ListEntranceVoucherDetailValidation As List(Of EntranceVoucherDetail)
        Set(value As List(Of EntranceVoucherDetail))
            If value IsNot Nothing Then
                _listEntranceVoucherDetailValidation = New List(Of EntranceVoucherDetail)(value.ToArray())
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad para para pasar el listado del detalle de la remision cuando se importa imformacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listEntranceVoucherDetailImportInfo As List(Of EntranceVoucherDetail)
    Public WriteOnly Property ListEntranceVoucherDetailImportInfo As List(Of EntranceVoucherDetail)
        Set(value As List(Of EntranceVoucherDetail))
            _listEntranceVoucherDetailImportInfo = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Dim _editMode As Boolean = False
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
    Dim _importDataMode As Boolean = False
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
    Dim entranceVoucherDetail As EntranceVoucherDetail
    Public WriteOnly Property EntranceVoucherDetailEdit As EntranceVoucherDetail
        Set(value As EntranceVoucherDetail)
            entranceVoucherDetail = value
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

    Dim _supplierId As Integer
    Public WriteOnly Property SupplierId As Integer
        Set(value As Integer)
            _supplierId = value
        End Set
    End Property

    Dim _declarant As Boolean
    Public WriteOnly Property Declarant As Boolean
        Set(value As Boolean)
            _declarant = value
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
    ''' propiedad para establecer el tipo de iva cuando en parametros de invetario esta como mixto
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property TaxRegistration As Byte?
        Set(value As Byte?)
            _TaxRegistration = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el porcentaje del iva
    ''' </summary>
    ''' <returns></returns>
    Private Property IvaPercent As Decimal
        Get
            Return CDec(INDTxtIvaPercent.EditValue)
        End Get
        Set(value As Decimal)
            INDTxtIvaPercent.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad del descuento
    ''' </summary>
    ''' <returns></returns>
    Private Property DiscountPercent As Decimal
        Get
            Return CDec(INDTxtDiscountPercent.EditValue)
        End Get
        Set(value As Decimal)
            INDTxtDiscountPercent.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' digitos de la mascara de moneda
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property MaskDigitRounding As Integer
        Get
            Return If(Me.Rounding = 0, 2, 0)
        End Get
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
        INDTxtUnitPurcharse.EditValue = 0
        INDTxtMeasureUnit.Text = String.Empty
        INDTxtQuantity.EditValue = 0
        INDPceBatchSerial.Text = String.Empty
        INDTxtLastValue.EditValue = 0
        DiscountPercent = 0
        IvaPercent = 0
        INDTxtUnitValue.EditValue = 0

        settingsIvaCost = False
        ConsSettingsIvaCost = True
        _ivaProduct = Nothing

        IvaValue = 0
        DiscountValue = 0
        TotalValue = 0
        NetoValue = 0

        ListValitadeImport.Clear()
        CtrBatchSerial1.CleanControls()
        ActionsControls = False
        _listEntranceVoucherDetailImportInfo = Nothing
        BarraBotones.FilterDataSource = Nothing
        _importDataMode = False
        _editMode = False
        ctrTmp.PrintInfo()

        INDPceProducts.Focus()
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateFields() As String
        Dim errors As New StringBuilder
        If _importDataMode = False Then
            If product Is Nothing Then
                errors.AppendLine(INDLyPceProducts.Text + ResourceManager.GetString("Empty"))
            Else
                If product.FinalProductCost Is Nothing OrElse (product.FinalProductCost / _tRMValue) = 0 Then
                    errors.AppendLine(String.Format("El precio del ultimo costo del producto es 0.", product.Code + " - " + product.Name))
                End If
            End If
            If _editMode = False Then
                'valido que no se agregue el mismo producto con tipo fuente ninguno
                If _listEntranceVoucherDetailValidation IsNot Nothing AndAlso _listEntranceVoucherDetailValidation.Count > 0 AndAlso product IsNot Nothing Then
                    Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidation.Find(Function(x) x.ProductId = product.Id And x.EntranceSource = 1)
                    If entranceVoucherDetailTmp IsNot Nothing Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("SourceProductNone", NAME_MODULE), entranceVoucherDetailTmp.ProductCodeName))
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
            If INDTxtUnitValue.EditValue = 0 Then
                errors.AppendLine(INDTxtUnitValue.Text + ResourceManager.GetString("Empty"))
            End If

            If entranceVoucherDetail IsNot Nothing AndAlso entranceVoucherDetail.EntranceSource = 5 Then
                If INDTxtQuantity.EditValue > entranceVoucherDetail.EntranceVoucherDetailBatchSerial.Sum(Function(x) x.OutstandingQuantity) Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("QuantityOverFlow", NAME_MODULE), entranceVoucherDetail.ProductCodeName, "Remision de Inventario en consignación"))
                End If
            End If
        Else
            Dim itemValidation As Integer = -1
            'recorro todos los items que se importaron para hacer las validaciones
            If _listEntranceVoucherDetailValidation IsNot Nothing AndAlso _listEntranceVoucherDetailValidation.Count > 0 AndAlso product IsNot Nothing Then
                Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidation.Find(Function(x) x.ProductId = product.Id And x.EntranceSource = 1)
                If entranceVoucherDetailTmp IsNot Nothing Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("SourceProductNone", NAME_MODULE), entranceVoucherDetailTmp.ProductCodeName))
                End If
            End If
            For Each item In _listEntranceVoucherDetailImportInfo
                itemValidation = itemValidation + 1
                Dim validateItem = ListValitadeImport(itemValidation)
                If item.EntranceSource <> 4 Then
                    Dim productForValidation As InventoryProduct = Nothing
                    Using model As New MInventoryProduct(Me.Tag)
                        productForValidation = model.GetInventoryProductByIdSimpleToGroup(item.ProductId)
                    End Using
                    If productForValidation.FinalProductCost Is Nothing OrElse productForValidation.FinalProductCost = 0 Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("FinalProductCostZero", NAME_MODULE), productForValidation.Code + " - " + productForValidation.Name))
                    End If
                    Using model As New MSubGroup(Me.Tag)
                        If productForValidation.ProductSubGroupId = 0 Then
                            errors.AppendLine(String.Format("El producto del item {0} no tiene un SubGrupo asociado.", (_listEntranceVoucherDetailImportInfo.IndexOf(item) + 1).ToString()))
                        Else
                            Dim subGroup = model.GetProductSubGroupByIdSimple(productForValidation.ProductSubGroupId)
                            If subGroup.ObjectEmbbeded.HandlesBatch = True And item.EntranceVoucherDetailBatchSerial.Count = 0 Then
                                'valido que si el producto maneja lote se encuentre agregado minimo un lote en RemissionEntranceDetailBatchSerial 
                                errors.AppendLine(String.Format(ResourceManager.GetString("HandlesBatch", NAME_MODULE), (_listEntranceVoucherDetailImportInfo.IndexOf(item) + 1).ToString()))
                            Else
                                If item.Quantity = 0 Then
                                    errors.AppendLine(String.Format(ResourceManager.GetString("QuantityZero", NAME_MODULE), (_listEntranceVoucherDetailImportInfo.IndexOf(item) + 1).ToString()))
                                ElseIf validateItem < item.Quantity Then
                                    'Se valida si viene desde una orden de compra
                                    If item.EntranceSource = 2 Then
                                        errors.AppendLine(String.Format(ResourceManager.GetString("QuantityOverFlow", NAME_MODULE), (_listEntranceVoucherDetailImportInfo.IndexOf(item) + 1).ToString(), "cantidad de la orden de compra"))
                                    Else
                                        errors.AppendLine(String.Format(ResourceManager.GetString("QuantityOverFlow", NAME_MODULE), (_listEntranceVoucherDetailImportInfo.IndexOf(item) + 1).ToString(), "cantidad del documento de origen"))
                                    End If
                                End If
                            End If
                        End If
                    End Using
                Else
                    If item.Quantity = 0 Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("QuantityZero", NAME_MODULE), (_listEntranceVoucherDetailImportInfo.IndexOf(item) + 1).ToString()))
                    ElseIf validateItem < item.Quantity Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("QuantityOverFlow", NAME_MODULE), (_listEntranceVoucherDetailImportInfo.IndexOf(item) + 1).ToString(), "Remision de Entrada"))
                    End If
                End If
                If INDTxtUnitValue.EditValue = 0 Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("ValueZero", NAME_MODULE), (_listEntranceVoucherDetailImportInfo.IndexOf(item)).ToString()))
                End If
            Next
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetValues() As ActionResult
        If product.ProductGroupId = 0 Then
            Return New ActionResult With {.StateResult = False, .Message = String.Format("El producto {0} no tiene asociado un grupo.", product.Code + " - " + product.Name)}
        End If

        If _editMode = False Then
            entranceVoucherDetail = New EntranceVoucherDetail
        End If
        Dim apc As PaymentsAccountPayableConceptsXpoP
        Dim rc As GeneralLedgerRetentionConceptsReportXpo = Nothing
        Dim presenter = New PEntranceVoucher()
        apc = presenter.GetAccountPayableConceptById(If(_declarant, product.ProductGroup.DeclarantRetentionAccountPayableConceptId, product.ProductGroup.NotDeclarantRetentionAccountPayableConceptId))
        If apc IsNot Nothing Then
            rc = presenter.GetRetentionConceptById(apc.RetentionConceptId)
        End If
        presenter = Nothing
        With entranceVoucherDetail
            If _editMode = False Then
                .EntranceSource = 1
            End If
            .ProductId = product.Id
            .ProductCode = product.Code
            .ProductName = product.Name
            .ProductCodeName = product.Code + " - " + product.Name
            .GroupCodeName = If(product.ProductGroup IsNot Nothing, product.ProductGroup.Code + " - " + product.ProductGroup.Name, Nothing)
            .ManufacturerName = product.ManufacturerDescription
            .HealthRegistration = product.HealthRegistration
            .Presentation = product.Presentation
            .Quantity = INDTxtQuantity.EditValue
            .UnitValue = INDTxtUnitValue.EditValue
            .LastValue = INDTxtLastValue.EditValue
            .DiscountPercentage = DiscountPercent
            .IvaPercentage = product.PercentageIVA

            .SubTotalValue = Utils.RoundValue(.UnitValue * .Quantity, Rounding)
            .DiscountValue = Utils.RoundValue(.SubTotalValue * .DiscountPercentage / 100, Rounding)
            .NetoValue = .SubTotalValue - .DiscountValue
            .IvaValue = Utils.RoundValue(CDec(.NetoValue * (product.PercentageIVA / 100)), Rounding)
            .TotalValue = .NetoValue + .IvaValue

            .CostWithDescount = .UnitValue - (.UnitValue * (.DiscountPercentage / 100))
            If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never And INDLyTxtBatchSerialRemision.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                .DescriptionBatch = "No maneja Lote"
            ElseIf INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .DescriptionBatch = INDPceBatchSerial.Text
            Else
                .DescriptionBatch = INDTxtBatchSerialRemision.Text
            End If
            .HandlesBatch = INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If rc IsNot Nothing Then
                .MinBase = rc.MinBase
                .Rate = rc.Rate
                .TypeRounding = rc.TypeRounding
            End If
            If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                'si se esta editando el registro elimino los detalles de entranceVoucherDetailBatchSerial 
                If _editMode = True Then
                    If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count > 0 Then
                        While .EntranceVoucherDetailBatchSerial.Count > 0
                            If .EntranceVoucherDetailBatchSerial(0).Id > 0 Then
                                .EntranceVoucherDetailBatchSerial(0).MarkAsDeleted() 'ChangeTracker.State = ObjectState.Deleted
                            Else
                                entranceVoucherDetail.StopTracking
                                entranceVoucherDetail.EntranceVoucherDetailBatchSerial.RemoveAt(0)
                            End If
                        End While
                    End If
                End If

                If listBatchSerial IsNot Nothing Then
                    For Each item In listBatchSerial
                        Dim entranceVoucherDetailBatchSerial As New EntranceVoucherDetailBatchSerial
                        entranceVoucherDetailBatchSerial.BatchSerialId = item.Id
                        entranceVoucherDetailBatchSerial.Quantity = item.Quantity
                        entranceVoucherDetailBatchSerial.OutstandingQuantity = item.Quantity
                        entranceVoucherDetailBatchSerial.CodeBatchSerial = item.BatchCode
                        .EntranceVoucherDetailBatchSerial.Add(entranceVoucherDetailBatchSerial)
                    Next
                End If
            Else
                If _editMode = True Then
                    'si se edita el registro y ya no maneja lote elimino todos los registros de RemissionEntranceDetailBatchSerial
                    While .EntranceVoucherDetailBatchSerial.Count > 0
                        If .EntranceVoucherDetailBatchSerial(0).Id > 0 Then
                            .EntranceVoucherDetailBatchSerial(0).MarkAsDeleted()
                        Else
                            entranceVoucherDetail.StopTracking
                            entranceVoucherDetail.EntranceVoucherDetailBatchSerial.RemoveAt(0)
                        End If
                    End While
                End If

                'Agregamos el lote así no tenga, para la modificacion de las cantidades en la devolucion
                Dim entranceVoucherDetailBatchSerial As New EntranceVoucherDetailBatchSerial
                entranceVoucherDetailBatchSerial.Quantity = .Quantity
                entranceVoucherDetailBatchSerial.OutstandingQuantity = .Quantity
                .EntranceVoucherDetailBatchSerial.Add(entranceVoucherDetailBatchSerial)
            End If
        End With

        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValuesBatchSerial(position As Integer)
        Dim iEntranceVoucherDetail = _listEntranceVoucherDetailImportInfo(position)
        Dim apc As PaymentsAccountPayableConceptsXpoP
        Dim presenter = New PEntranceVoucher()
        Dim rc As GeneralLedgerRetentionConceptsReportXpo = Nothing

        apc = presenter.GetAccountPayableConceptById(If(_declarant, product.ProductGroup.DeclarantRetentionAccountPayableConceptId, product.ProductGroup.NotDeclarantRetentionAccountPayableConceptId))

        If apc IsNot Nothing Then
            rc = presenter.GetRetentionConceptById(apc.RetentionConceptId)
        End If

        presenter = Nothing

        With iEntranceVoucherDetail
            .ProductId = product.Id
            .ProductCode = product.Code
            .ProductName = product.Name
            .ProductCodeName = product.Code + " - " + product.Name
            .Quantity = INDTxtQuantity.EditValue
            .UnitValue = INDTxtUnitValue.EditValue
            .LastValue = INDTxtLastValue.EditValue
            .DiscountPercentage = DiscountPercent
            .IvaPercentage = product.PercentageIVA
            'valor subtotal
            .SubTotalValue = Utils.RoundValue(.UnitValue * .Quantity, Rounding)
            'valor descuento
            .DiscountValue = Utils.RoundValue(.SubTotalValue * .DiscountPercentage / 100, Rounding)
            'valor neto
            .NetoValue = .SubTotalValue - .DiscountValue
            'valor iva
            .IvaValue = Utils.RoundValue(.NetoValue * (.IvaPercentage / 100), Rounding)
            'valor total
            .TotalValue = .NetoValue + .IvaValue
            .CostWithDescount = .UnitValue - (.UnitValue * (.DiscountPercentage / 100))
            If rc IsNot Nothing Then
                .MinBase = rc.MinBase
                .Rate = rc.Rate
                .TypeRounding = rc.TypeRounding
            Else
                .TypeRounding = 1
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <param name="entranceVoucherDetailTmp"></param>
    ''' <remarks></remarks>
    Private Sub LoadControls(Optional entranceVoucherDetailTmp As EntranceVoucherDetail = Nothing)
        If _editMode = True Then
            INDBtnAddProduct.Text = ResourceManager.GetString("Edit")
        End If
        If _importDataMode = True Then
            entranceVoucherDetail = entranceVoucherDetailTmp
        End If
        With entranceVoucherDetail
            Dim _finalProductCost As Decimal = 0
            flagImport = True
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(.ProductId)
                INDPceProducts.Text = product.Code + " - " + product.Name
                If _importDataMode = True Then
                    entranceVoucherDetail.ProductCode = product.Code
                    entranceVoucherDetail.ProductName = product.Name
                    entranceVoucherDetail.ProductCodeName = INDPceProducts.Text
                End If

                _finalProductCost = If(product?.FinalProductCost Is Nothing, 0, Math.Round((product.FinalProductCost.Value / _tRMValue), 2))
                IvaPercent = product.PercentageIVA
                INDPceProducts.Properties.ReadOnly = True
                INDTxtUnitPurcharse.Text = product.PackingUnitDescription
                INDTxtMeasureUnit.Text = product.MeasureUnitDescription
                INDTxtLastValue.EditValue = _finalProductCost
                If entranceVoucherDetail.UnitValue = 0 Then
                    INDTxtUnitValue.EditValue = _finalProductCost
                Else
                    INDTxtUnitValue.EditValue = entranceVoucherDetail.UnitValue
                End If
                INDTxtDiscountPercent.Text = entranceVoucherDetail.DiscountPercentage
                flagImport = False
            End Using
            If product.ProductSubGroupId > 0 Then

                If product.ProductSubGroup.HandlesBatch = True Then
                    INDTxtQuantity.Properties.ReadOnly = True
                    INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLyTxtBatchSerialRemision.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    INDTxtQuantity.Properties.ReadOnly = False
                    INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLyTxtBatchSerialRemision.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End If
            INDTxtQuantity.EditValue = .Quantity
            If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If .EntranceVoucherDetailBatchSerial.Count = 0 Then
                    INDPceBatchSerial.EditValue = String.Empty
                Else
                    INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", NAME_MODULE), .EntranceVoucherDetailBatchSerial.Count.ToString())
                End If
                CtrBatchSerial1.Product = product
                CtrBatchSerial1.FormOwner = Me
                If _importDataMode = True Then
                    CtrBatchSerial1.CleanControls()
                End If
                CtrBatchSerial1.SetListBatchSerial()
                CtrBatchSerial1.SetQuantityBatchSerial(.EntranceVoucherDetailBatchSerial.ToList())
            End If
            'si se esta editando coloco el valor que asignaron sino el ultimo valor del producto
            If _editMode = True Or _importDataMode = True Then
                If .UnitValue = 0 Then
                    INDTxtUnitValue.EditValue = _finalProductCost
                Else
                    INDTxtUnitValue.EditValue = .UnitValue
                End If
            Else
                INDTxtUnitValue.EditValue = _finalProductCost
            End If
        End With
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' metodo para obtener la informacion y colocarla en el control de usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        If entranceVoucherDetail IsNot Nothing Then
            Return New Tuple(Of String, String)(entranceVoucherDetail.EntranceSource.ToString(), entranceVoucherDetail.SourceCode)
        Else
            Return New Tuple(Of String, String)(1, String.Empty)
        End If
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
        CtrBatchSerial1.RemissionType = ERemissionType.Input

        If Me._headCurrency?.Id <> Me._indigo?.OfficialCurrencyId AndAlso Me._tRMValue = 1 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("TRMEmpty")
            Me.Close()
            Exit Sub
        End If

        If _editMode = True Then
            'si esta editando un registro
            Me.LoadControls()
        ElseIf _importDataMode = True Then
            ListValitadeImport.Clear()
            'si se esta importando informacion
            If _listEntranceVoucherDetailImportInfo.Count > 1 Then
                For i As Integer = 0 To _listEntranceVoucherDetailImportInfo.Count - 1
                    ListValitadeImport.Add(_listEntranceVoucherDetailImportInfo(i).Quantity)
                Next
                BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Producto", .FieldName = "ProductCodeName"},
                                           New ColumnInfo With {.Caption = "Cantidad", .FieldName = "Quantity"}}.ToList()
                BarraBotones.FilterDataSource = _listEntranceVoucherDetailImportInfo
            Else
                LoadControls(_listEntranceVoucherDetailImportInfo(0))
                ListValitadeImport.Add(_listEntranceVoucherDetailImportInfo(0).Quantity)
            End If
        Else
            'si es un nuevo registro
            CleanControls()
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
    Private Sub PopupProductsContract_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If product Is Nothing Then
            INDPceProducts.Focus()
        End If
        If _importDataMode = True Then
            If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDPceBatchSerial.EditValue Is String.Empty Then
                    INDPceBatchSerial.Focus()
                End If
            Else
                If INDTxtQuantity.EditValue = 0 Then
                    INDTxtQuantity.Focus()
                End If
            End If
        End If
        If _editMode = True Then
            INDPceProducts.Focus()
        End If
    End Sub
#End Region

#Region "SelectProduct"
    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProducts.Text = e.CodeNameProduct
        INDPceProducts.Focus()
        INDPceProducts.ClosePopup()
        CtrBatchSerial1.CleanControls()
        INDTxtQuantity.EditValue = 0
        INDPceBatchSerial.EditValue = Nothing
        _ivaProduct = Nothing
        Me.AsyncLoader(True)
        Task.Factory.StartNew(Sub()
                                  Using model As New MInventoryProduct(Me.Tag)
                                      product = model.GetInventoryProductByIdSimple(e.ProductId)
                                      Dim _finalCostProducto As Decimal = 0
                                      'lógica para establecer valor del producto con el iva
                                      Using modelIva As New MGeneralLedgerIVA(Me.Tag)
                                          If ConsSettingsIvaCost Then
                                              If product?.IVAId IsNot Nothing Then
                                                  _ivaProduct = modelIva.GetGeneralLedgerIVAById(product.IVAId).ObjectEmbbeded
                                              End If
                                              Using msearch As New MBusqueda
                                                  Dim filter() As Object = {BarraBotones.OperatingUnitValue}
                                                  Dim settings As XPCollection(Of SettingInventoryXpo) = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetSettingInventoryByOperatingUnit, filter)
                                                  If settings Is Nothing OrElse settings.Count = 0 Then
                                                      Me.SafeInvoke(Sub()
                                                                        Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de inventarios para la unidad operativa seleccionada"
                                                                        Me.AsyncLoader(False)
                                                                    End Sub)
                                                      Return
                                                  Else
                                                      Select Case settings(0).TaxRegistration
                                                          Case 1
                                                              settingsIvaCost = True
                                                          Case 2
                                                              settingsIvaCost = False
                                                          Case Else
                                                              settingsIvaCost = _TaxRegistration
                                                      End Select
                                                  End If
                                              End Using
                                              ConsSettingsIvaCost = False
                                          End If

                                          If product?.FinalProductCost IsNot Nothing AndAlso settingsIvaCost AndAlso _ivaProduct IsNot Nothing Then
                                              _finalCostProducto = Utils.RoundValue(CDec((product.FinalProductCost / _tRMValue) / (1 + (_ivaProduct?.Percentage / 100))), Rounding)
                                          ElseIf product?.FinalProductCost IsNot Nothing Then
                                              _finalCostProducto = Utils.RoundValue(CDec(product.FinalProductCost / _tRMValue), Rounding)
                                          End If

                                      End Using
                                      Me.SafeInvoke(Sub()
                                                        IvaPercent = product.PercentageIVA
                                                        INDTxtUnitPurcharse.Text = product.PackingUnitDescription
                                                        INDTxtMeasureUnit.Text = product.MeasureUnitDescription
                                                        INDTxtLastValue.EditValue = _finalCostProducto
                                                        INDTxtUnitValue.EditValue = _finalCostProducto
                                                    End Sub)
                                  End Using
                                  If product.ProductSubGroupId > 0 Then
                                      If product.ProductSubGroup.HandlesBatch = True Then
                                          If Me.InvokeRequired Then
                                              Me.BeginInvoke(Sub()
                                                                 INDTxtQuantity.Properties.ReadOnly = True
                                                                 INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                                                 INDPceBatchSerial.Focus()
                                                             End Sub)
                                          Else
                                              INDTxtQuantity.Properties.ReadOnly = True
                                              INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                              INDPceBatchSerial.Focus()
                                          End If
                                          If _editMode = True Then
                                              'instancio el listado para poder hacer la validacion de los lotes cuando se esta editando y saber si esta sin registros
                                              listBatchSerial = New List(Of BatchSerial)
                                          End If
                                      Else
                                          If Me.InvokeRequired Then
                                              Me.BeginInvoke(Sub()
                                                                 INDTxtQuantity.Properties.ReadOnly = False
                                                                 INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                                                 INDTxtQuantity.Focus()
                                                             End Sub)
                                          Else
                                              INDTxtQuantity.Properties.ReadOnly = False
                                              INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                              INDTxtQuantity.Focus()
                                          End If
                                      End If
                                  Else
                                      If Me.InvokeRequired Then
                                          Me.BeginInvoke(Sub()
                                                             Mensaje(EeventViewerImages.Advertencia) = "El producto " + String.Concat(product.Code, " - ", product.Name) + " no tiene un subgrupo asociado"
                                                             CleanControls()
                                                         End Sub)
                                      Else
                                          Mensaje(EeventViewerImages.Advertencia) = "El producto " + String.Concat(product.Code, " - ", product.Name) + " no tiene un subgrupo asociado"
                                          CleanControls()
                                      End If
                                  End If
                                  If Me.InvokeRequired Then
                                      Me.BeginInvoke(Sub()
                                                         AsyncLoader(False)
                                                         ActionsControls = True
                                                     End Sub)
                                  Else
                                      AsyncLoader(False)
                                      ActionsControls = True
                                  End If
                              End Sub)
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
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat
        _culture.NumberFormat.CurrencyDecimalDigits = Me.MaskDigitRounding

        For Each control In ListControls
            control.Properties.mask.Culture = _culture
            control.Properties.Mask.EditMask = $"C{ _culture.NumberFormat.CurrencyDecimalDigits}"
        Next
    End Sub

    Private _listControls As List(Of TextEdit)
    ''' <summary>
    ''' Lista de TextEdit para culturizar segun moneda y digitos
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ListControls As List(Of TextEdit)
        Get
            If _listControls Is Nothing Then
                _listControls = New List(Of TextEdit)
                _listControls.Add(Me.INDTxtLastValue)
                _listControls.Add(Me.INDTxtUnitValue)
                _listControls.Add(Me.INDtxtSubtotal)
                _listControls.Add(Me.INDtxtSubtotalWithDiscount)
                _listControls.Add(Me.INDTxtTotal)

            End If
            Return _listControls
        End Get
    End Property
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Consulta y asigna los produuctos al listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProducts.QueryPopUp
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
            CtrBatchSerial1.WarehouseId = _wareHouseId
            CtrBatchSerial1.FormOwner = Me
            CtrBatchSerial1.SetListBatchSerial()
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
    Private Sub INDPceProducts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceProducts.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenFormDialog(form)
            End Using
        End If
    End Sub
#End Region

#Region "ChangeQuantity"
    Private Sub CtrBatchSerial1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrBatchSerial1.ChangeQuantity
        INDTxtQuantity.EditValue = e.Quantity
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
            INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", NAME_MODULE), listBatchSerial.Count.ToString())
        End If
        If entranceVoucherDetail IsNot Nothing AndAlso _importDataMode = True Then
            If listBatchSerial IsNot Nothing Then
                entranceVoucherDetail.EntranceVoucherDetailBatchSerial.Clear()
                For Each item In listBatchSerial
                    Dim entranceVoucherDetailBatchSerial As New EntranceVoucherDetailBatchSerial
                    entranceVoucherDetailBatchSerial.BatchSerialId = item.Id
                    entranceVoucherDetailBatchSerial.Quantity = item.Quantity
                    entranceVoucherDetailBatchSerial.OutstandingQuantity = item.Quantity
                    entranceVoucherDetailBatchSerial.CodeBatchSerial = item.BatchCode
                    If _importDataMode Then
                        entranceVoucherDetail.DescriptionBatch = item.BatchCode
                    End If
                    entranceVoucherDetail.EntranceVoucherDetailBatchSerial.Add(entranceVoucherDetailBatchSerial)
                Next
            End If
        End If
    End Sub
#End Region

#Region "Click"
    Private Async Sub INDBtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDBtnAddProduct.Click
        Try
            AsyncLoader(True)
            INDBtnAddProduct.Enabled = False
            Dim errors = ValidateFields()
            If errors.Length > 0 Then
                AsyncLoader(False)
                INDBtnAddProduct.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            'si no se esta importando informacion asigno valores
            If _importDataMode = False Then
                Dim result = SetValues()
                If result.StateResult = False Then
                    AsyncLoader(False)
                    INDBtnAddProduct.Enabled = True
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Exit Sub
                End If
                Using modelSettings As New MSettingInventory(Me.Tag)
                    Dim resulValidateStock = Await modelSettings.ValidateStock(entranceVoucherDetail.ProductId, _operatingUnitId, _wareHouseId, entranceVoucherDetail.Quantity, InventoryStaticServices.MovementType.Input)
                    If resulValidateStock.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = resulValidateStock.Message
                    End If
                End Using
            Else
                SetValuesBatchSerial(BarraBotones.CurrentRecordPosition)
            End If

            If _listEntranceVoucherDetailValidation Is Nothing Then
                _listEntranceVoucherDetailValidation = New List(Of EntranceVoucherDetail)
            End If
            Dim args As New AddProductEntranceVoucherDetailEventArgs
            If _editMode = True Then
                args.ItemEntranceVoucherDetail = entranceVoucherDetail
                args.EditMode = True
            Else
                args.ItemEntranceVoucherDetail = entranceVoucherDetail
                _listEntranceVoucherDetailValidation.Add(entranceVoucherDetail)
            End If
            If _importDataMode = True Then
                args.ImportDataMode = True
                args.ListEntranceVoucherDetail = _listEntranceVoucherDetailImportInfo
                _listEntranceVoucherDetailValidation.AddRange(_listEntranceVoucherDetailImportInfo)
            End If
            RaiseEvent AddEntranceVoucherDetail(Nothing, args)
            Dim flagClose As Boolean = False
            AsyncLoader(False)
            If _importDataMode = True OrElse _editMode = True Then
                product = Nothing
                Me.Close()
            Else
                CleanControls()
            End If
            INDBtnAddProduct.Enabled = True
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAddProduct.Enabled = True
            Throw ex
        End Try
    End Sub
#End Region

#Region "KeyDown"
    Private Sub PopUpProductsEntranceVoucher_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDTxtTotal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtTotal.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAddProduct.Focus()
        End If
    End Sub

#End Region

#Region "RecordNavigationChangeEvent"
    ''' <summary>
    ''' Se dispara cuando navega entre items importados
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        If entranceVoucherDetail IsNot Nothing Then
            SetValuesBatchSerial(BarraBotones.LastRecordPosition)
        End If

        LoadControls(Record)
        INDTxtQuantity.Focus()
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDTxtQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtQuantity.EditValueChanged
        If entranceVoucherDetail IsNot Nothing AndAlso _importDataMode = True Then
            entranceVoucherDetail.Quantity = INDTxtQuantity.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Evento que realiza la totalizacion del comprobante de entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sum_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtLastValue.EditValueChanged, INDTxtQuantity.EditValueChanged, INDTxtUnitValue.EditValueChanged, INDTxtIvaPercent.EditValueChanged, INDTxtDiscountPercent.EditValueChanged
        If product IsNot Nothing Then
            If INDTxtLastValue.EditValue Is Nothing OrElse INDTxtQuantity.EditValue Is Nothing OrElse INDTxtUnitValue.EditValue Is Nothing OrElse
                INDTxtIvaPercent.EditValue Is Nothing OrElse INDTxtDiscountPercent.EditValue Is Nothing Then
                Exit Sub
            End If
            'valor subtotal
            Dim Subtotal = Utils.RoundValue(INDTxtQuantity.EditValue * INDTxtUnitValue.EditValue, Rounding)
            'valor descuento
            DiscountValue = Utils.RoundValue(Subtotal * (DiscountPercent / 100), Rounding)
            'valor neto
            NetoValue = Subtotal - DiscountValue
            'valor iva
            IvaValue = Utils.RoundValue(NetoValue * (IvaPercent / 100), Rounding)
            'valor total
            TotalValue = NetoValue + IvaValue

            INDtxtSubtotal.EditValue = Subtotal
            INDtxtSubtotalWithDiscount.EditValue = NetoValue

            If entranceVoucherDetail IsNot Nothing AndAlso _importDataMode = True AndAlso flagImport = False Then
                entranceVoucherDetail.Quantity = INDTxtQuantity.EditValue
                entranceVoucherDetail.UnitValue = INDTxtUnitValue.EditValue
                entranceVoucherDetail.LastValue = INDTxtLastValue.EditValue
                entranceVoucherDetail.IvaPercentage = IvaPercent
                entranceVoucherDetail.DiscountPercentage = DiscountPercent
                entranceVoucherDetail.DiscountValue = DiscountValue
                entranceVoucherDetail.IvaValue = IvaValue
                entranceVoucherDetail.SubTotalValue = Subtotal
                entranceVoucherDetail.TotalValue = TotalValue
            End If
        End If
    End Sub

#End Region

#End Region

#Region "BarraBotones"
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region

    Private Async Sub INDPceProducts_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProducts.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

    ''' <summary>
    ''' Lanza la consulta del producto para poderlo seleccionar
    ''' sin necesidad de desplegar el control de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AssignProductWithCode() As Task
        If INDPceProducts.Text.Trim <> String.Empty AndAlso _importDataMode = False Then
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
        ElseIf INDPceProducts.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDPceProducts.Text = product.Code + " - " + product.Description
            End If
        End If
    End Function
End Class