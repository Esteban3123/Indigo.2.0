Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Presentation.FixedAsset.MVP
Imports Domain.Entities
Imports System.Drawing
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities

Public Class FrmPopupPurchaseOrderEquipment

#Region "Builder"

    ''' <summary>
    ''' Inicializa una isntancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(Optional _currency As Currency = Nothing, Optional _tRMValue As Decimal = 1, Optional _roundType As Decimal = 1)
        InitializeComponent()

        Me._tRMValue = _tRMValue
        Me._indigo = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)
        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)

        ''se actualiza el formato de los campos numericos en el formuario
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(Me._headCurrency.Abbreviation.GetCultureId()).NumberFormat
        _culture.NumberFormat.CurrencyDecimalDigits = Utils.MaskByCurrencyRounding(_roundType, _culture.NumberFormat)
        roundingDecimals = _roundType
        Me.changeNumericFormatByCurrency(_culture.NumberFormat)
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddEquipmentRemissionEventArgs(sender As Object, e As AddEquipmentPurchaseOrderEventArgs)

#End Region

#Region "Globals"

    Dim _MyTag As String

    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public FixedAssetPurchaseOrderEquipmentEdit As FixedAssetPurchaseOrderItem

    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listFixedAssetPurchaseOrderEquipmentValidation As List(Of FixedAssetPurchaseOrderItem)

    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer

    ''' <summary>
    ''' Listado para comparar si el articulo agregado esta ya incluido en el listado del form principal
    ''' </summary>
    ''' <remarks></remarks>
    Public ListCompare As List(Of FixedAssetPurchaseOrderItem)

    ''' <summary>
    ''' se recibe la moneda desde el form
    ''' </summary>
    Public Currency As Currency

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PFixedAssetPurchaseOrder

    ''' <summary>
    ''' variable de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' variable de la tasa de cambio, por defecto es 1 cuando la moneda es igual a la oficial
    ''' </summary>
    Private _tRMValue As Decimal = 1

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _headCurrency As Currency

    ''' <summary>
    ''' variable para determinar el tipo de redondeo 
    ''' </summary>
    Private roundingDecimals As Decimal


#End Region

#Region "Properties"

    Property EquipmentXPO As XPInstantFeedbackSource
        Get
            Return CType(INDsleItem.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleItem.Properties.DataSource = value
        End Set
    End Property

    Property TrademarkXPO As XPInstantFeedbackSource
        Get
            Return CType(INDsleTrademark.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTrademark.Properties.DataSource = value
        End Set
    End Property

    Property BranchOfficeXPO As XPInstantFeedbackSource
        Get
            Return CType(INDsleBranchOffice.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBranchOffice.Properties.DataSource = value
        End Set
    End Property

    Property FunctionalUnitXPO As XPInstantFeedbackSource
        Get
            Return CType(INDsleFunctionalUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property


    Property IVAXPO As XPInstantFeedbackSource
        Get
            Return CType(INDsleIVA.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIVA.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MyTag As String
        Set(value As String)
            _MyTag = value
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
    ''' obtiene o establece el valor unitario
    ''' </summary>
    ''' <returns></returns>
    Property UnitValue As Decimal
        Get
            Return INDtxtUnitValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtUnitValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para para pasar el listado del detalle de la remision cuando se importa imformacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ListFixedAssetPurchaseOrderEquipment As List(Of FixedAssetPurchaseOrderItem)
    Public WriteOnly Property ListFixedAssetPurchaseOrderEquipment As List(Of FixedAssetPurchaseOrderItem)
        Set(value As List(Of FixedAssetPurchaseOrderItem))
            _ListFixedAssetPurchaseOrderEquipment = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece el subtotal
    ''' </summary>
    ''' <returns></returns>
    Property SubTotalValue As Decimal
        Get
            Return INDtxtSubTotalValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSubTotalValue.EditValue = value
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
    ''' obtiene o establece el valor Descuento
    ''' </summary>
    ''' <returns></returns>
    Property DiscountValue As Decimal
        Get
            Return INDtxtDiscountValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtDiscountValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Dim _fixedAssetPurchaseOrderEquipment As FixedAssetPurchaseOrderItem
    Public Property FixedAssetPurchaseOrderEquipment As FixedAssetPurchaseOrderItem
        Get
            Return _fixedAssetPurchaseOrderEquipment
        End Get
        Set(value As FixedAssetPurchaseOrderItem)
            _fixedAssetPurchaseOrderEquipment = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el valor del iva
    ''' </summary>
    ''' <returns></returns>
    Property IVAValue As Decimal
        Get
            Return INDtxtIVAValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtIVAValue.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece la cantidad
    ''' </summary>
    ''' <returns></returns>
    Property Quantity As Integer
        Get
            Return INDseQuantity.EditValue
        End Get
        Set(value As Integer)
            INDseQuantity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el valor Total
    ''' </summary>
    ''' <returns></returns>
    Property TotalValue As Decimal
        Get
            Return INDTxtTotalValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtTotalValue.EditValue = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que calcula el valor total
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateTotalValue()
        TotalValue = (SubTotalValue - DiscountValue) + IVAValue
        If FixedAssetPurchaseOrderEquipment IsNot Nothing AndAlso _importDataMode = True Then
            FixedAssetPurchaseOrderEquipment.Quantity = Quantity
            FixedAssetPurchaseOrderEquipment.OutstandingQuantity = Quantity
            FixedAssetPurchaseOrderEquipment.CancelledQuantity = 0
            FixedAssetPurchaseOrderEquipment.UnitValue = UnitValue
            FixedAssetPurchaseOrderEquipment.SubTotalValue = SubTotalValue
            FixedAssetPurchaseOrderEquipment.IvaValue = IVAValue
            FixedAssetPurchaseOrderEquipment.DiscountPercentage = INDseDiscountPercentage.EditValue
            FixedAssetPurchaseOrderEquipment.DiscountValue = DiscountValue
            FixedAssetPurchaseOrderEquipment.TotalValue = TotalValue
        End If
    End Sub

    ''' <summary>
    ''' Metodo que calcula los valores para los controles del form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateValue()
        'Se calcula el SubTotal
        If INDseQuantity.EditValue IsNot Nothing AndAlso Quantity > 0 AndAlso INDtxtUnitValue.EditValue IsNot Nothing AndAlso UnitValue > 0 Then
            Dim valueCalculate = Quantity * UnitValue
            SubTotalValue = valueCalculate
        Else
            SubTotalValue = 0
        End If
    End Sub

    ''' <summary>
    ''' Metodo que calcula el valor del IVA
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateIVAValue()
        'Se calcula el valor del IVA
        If INDtxtSubTotalValue.EditValue IsNot Nothing AndAlso SubTotalValue > 0 AndAlso INDseIVAPercentage.EditValue IsNot Nothing AndAlso INDseIVAPercentage.EditValue > 0 Then

            IVAValue = Utils.RoundValue((SubTotalValue - DiscountValue) * (INDseIVAPercentage.EditValue / 100), roundingDecimals)

        Else
            IVAValue = 0
        End If
    End Sub

    ''' <summary>
    ''' establece los valores a la entidad cada que se navega en el PopUp
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValuesPurchaseOrder(position As Integer)
        Dim fixedAssetPurchaseOrderItem = _ListFixedAssetPurchaseOrderEquipment(position)

        With fixedAssetPurchaseOrderItem
            .ItemId = INDsleItem.EditValue
            .NameEquipment = INDsleItem.Text

            .TrademarkId = INDsleTrademark.EditValue
            .NameTrademark = INDsleTrademark.Text

            .BranchOfficeId = INDsleBranchOffice.EditValue
            .NameBranchOffice = INDsleBranchOffice.Text

            .FunctionalUnitId = INDsleFunctionalUnit.EditValue
            .NameFunctionalUnit = INDsleFunctionalUnit.Text

            .Model = INDtxtModel.EditValue

            .IvaPercentage = INDseIVAPercentage.EditValue
            .IVAId = INDsleIVA.EditValue
            .NameIva = INDsleIVA.Text
        End With
    End Sub

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        If _editMode = False Then
            FixedAssetPurchaseOrderEquipment = New FixedAssetPurchaseOrderItem
        Else
            If FixedAssetPurchaseOrderEquipment.Id > 0 Then
                FixedAssetPurchaseOrderEquipment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
        End If

        With FixedAssetPurchaseOrderEquipment

            .ItemId = INDsleItem.EditValue
            .NameEquipment = INDsleItem.Text

            .TrademarkId = INDsleTrademark.EditValue
            .NameTrademark = INDsleTrademark.Text

            .BranchOfficeId = INDsleBranchOffice.EditValue
            .NameBranchOffice = INDsleBranchOffice.Text

            .FunctionalUnitId = INDsleFunctionalUnit.EditValue
            .NameFunctionalUnit = INDsleFunctionalUnit.Text

            .Model = INDtxtModel.EditValue

            .IVAId = INDsleIVA.EditValue
            .NameIva = INDsleIVA.Text

            .Quantity = Quantity
            .OutstandingQuantity = If(.CancelledQuantity > 0, Quantity - .CancelledQuantity, Quantity)
            .CancelledQuantity = If(.CancelledQuantity = 0, 0, .CancelledQuantity)
            .UnitValue = UnitValue
            .SubTotalValue = SubTotalValue
            .IvaPercentage = INDseIVAPercentage.EditValue
            .IvaValue = IVAValue
            .DiscountPercentage = INDseDiscountPercentage.EditValue
            .DiscountValue = DiscountValue
            .TotalValue = TotalValue
        End With

    End Sub

    '''<summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls(Optional FixedAssetPurchaseOrderItemTmp As FixedAssetPurchaseOrderItem = Nothing)
        If _editMode = True Then
            INDBtnAdd.Text = ResourceManager.GetString("Edit")
        End If
        If _importDataMode Then
            FixedAssetPurchaseOrderEquipment = FixedAssetPurchaseOrderItemTmp
        End If
        With FixedAssetPurchaseOrderEquipment

            INDsleItem.EditValue = .ItemId
            INDsleItem.Properties.NullText = .NameEquipment

            INDsleTrademark.EditValue = .TrademarkId
            INDsleTrademark.Properties.NullText = .NameTrademark

            INDsleBranchOffice.EditValue = .BranchOfficeId
            INDsleBranchOffice.Properties.NullText = .NameBranchOffice

            If .BranchOfficeId IsNot Nothing AndAlso String.IsNullOrEmpty(.NameBranchOffice) = True Then
                INDsleBranchOffice.Properties.NullText = Presenter.GetBranchOfficeByIdXpo(INDsleBranchOffice.EditValue).Descripcion
            End If

            INDsleFunctionalUnit.EditValue = .FunctionalUnitId
            INDsleFunctionalUnit.Properties.NullText = .NameFunctionalUnit


            INDtxtModel.EditValue = .Model
            INDSlIVA_QueryPopUp(Nothing, Nothing)
            INDsleIVA.EditValue = .IVAId
            INDsleIVA.Properties.NullText = .NameIva


            Quantity = .Quantity
            INDseOutstandingQuantity.EditValue = .OutstandingQuantity
            INDseCancelledQuantity.EditValue = .CancelledQuantity
            INDseIVAPercentage.EditValue = .IvaPercentage
            UnitValue = .UnitValue
            SubTotalValue = .SubTotalValue
            IVAValue = .IvaValue
            INDseDiscountPercentage.EditValue = .DiscountPercentage
            DiscountValue = .DiscountValue
            TotalValue = .TotalValue
        End With
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDsleItem.EditValue = Nothing
        INDsleItem.Properties.NullText = String.Empty
        INDsleTrademark.EditValue = Nothing
        INDsleTrademark.Properties.NullText = String.Empty
        INDsleBranchOffice.EditValue = Nothing
        INDsleBranchOffice.Properties.NullText = String.Empty
        INDsleFunctionalUnit.EditValue = Nothing
        INDsleFunctionalUnit.Properties.NullText = String.Empty
        INDtxtModel.EditValue = Nothing
        INDsleIVA.EditValue = Nothing
        INDsleIVA.Properties.NullText = String.Empty
        Quantity = Nothing
        INDseOutstandingQuantity.EditValue = Nothing
        INDseCancelledQuantity.EditValue = Nothing
        UnitValue = Nothing
        SubTotalValue = Nothing
        INDseIVAPercentage.EditValue = Nothing
        IVAValue = Nothing
        INDseDiscountPercentage.EditValue = Nothing
        DiscountValue = Nothing
        _importDataMode = False
        TotalValue = Nothing
        _ListFixedAssetPurchaseOrderEquipment = Nothing
        FixedAssetPurchaseOrderEquipment = Nothing
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        Dim itemValidation As Integer = -1
        For Each item In _ListFixedAssetPurchaseOrderEquipment
            itemValidation += 1
            If item.ItemId = 0 Then
                errors.AppendLine(String.Format("El Identificador del articulo {0} ." + ResourceManager.GetString("Empty"), (_ListFixedAssetPurchaseOrderEquipment.IndexOf(item) + 1).ToString()))
            End If

            If item.BranchOfficeId = 0 Then
                errors.AppendLine(String.Format("La Sucursal del articulo {0} ." + ResourceManager.GetString("Empty"), (_ListFixedAssetPurchaseOrderEquipment.IndexOf(item) + 1).ToString()))
            End If

            If item.FunctionalUnitId = 0 Then
                errors.AppendLine(String.Format("La Unidad funcional del articulo {0} ." + ResourceManager.GetString("Empty"), (_ListFixedAssetPurchaseOrderEquipment.IndexOf(item) + 1).ToString()))
            End If

            If item.Quantity = 0 Then
                errors.AppendLine(String.Format("La Cantidad del articulo {0} ." + ResourceManager.GetString("Empty"), (_ListFixedAssetPurchaseOrderEquipment.IndexOf(item) + 1).ToString()))
            End If

            If item.TrademarkId = 0 Then
                errors.AppendLine(String.Format("La Marca del articulo {0} ." + ResourceManager.GetString("Empty"), (_ListFixedAssetPurchaseOrderEquipment.IndexOf(item) + 1).ToString()))
            End If

            If item.Model = String.Empty Then
                errors.AppendLine(String.Format("El Modelo del articulo {0} ." + ResourceManager.GetString("Empty"), (_ListFixedAssetPurchaseOrderEquipment.IndexOf(item) + 1).ToString()))
            End If

            If item.UnitValue = 0 Then
                errors.AppendLine(String.Format("El Valor del articulo {0} ." + ResourceManager.GetString("Empty"), (_ListFixedAssetPurchaseOrderEquipment.IndexOf(item) + 1).ToString()))
            End If

            If item.TotalValue = 0 Then
                errors.AppendLine(String.Format("El Valor total del articulo {0} ." + ResourceManager.GetString("Empty"), (_ListFixedAssetPurchaseOrderEquipment.IndexOf(item) + 1).ToString()))
            End If

            If item.IVAId = 0 Then
                errors.AppendLine(String.Format("El Valor IVA del articulo {0} ." + ResourceManager.GetString("Empty"), (_ListFixedAssetPurchaseOrderEquipment.IndexOf(item) + 1).ToString()))
            End If

        Next
        Return errors.ToString()
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


        INDtxtUnitValue.Properties.Mask.Culture = _culture
        INDtxtSubTotalValue.Properties.Mask.Culture = _culture
        INDtxtIVAValue.Properties.Mask.Culture = _culture
        INDtxtDiscountValue.Properties.Mask.Culture = _culture
        INDTxtTotalValue.Properties.Mask.Culture = _culture
    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _MyTag = Nothing
        FixedAssetPurchaseOrderEquipment = Nothing
        FixedAssetPurchaseOrderEquipmentEdit = Nothing
        _listFixedAssetPurchaseOrderEquipmentValidation = Nothing
        _editMode = Nothing
        indexEditRecord = Nothing
        ListCompare = Nothing
        Presenter = Nothing
    End Sub

    Private Sub FrmPopUpEquipment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ModoNavegacion) = False
        BarraBotones.StatusRecordVisible = True
        If Me.Currency?.Id <> Me._indigo?.OfficialCurrencyId AndAlso Me._tRMValue = 1 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("TRMEmpty")
            Me.Close()
        End If
        Presenter = New PFixedAssetPurchaseOrder()
        If _editMode = True Then
            LoadControls()
            INDsleItem.Properties.ReadOnly = True
        ElseIf _importDataMode Then
            If _ListFixedAssetPurchaseOrderEquipment.Count() > 1 Then
                BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Articulo", .FieldName = "NameEquipment"},
                                           New ColumnInfo With {.Caption = "Cantidad", .FieldName = "Quantity"}}.ToList()
                BarraBotones.FilterDataSource = _ListFixedAssetPurchaseOrderEquipment
            Else
                LoadControls(_ListFixedAssetPurchaseOrderEquipment(0))
            End If
        Else
            INDsleItem.Properties.ReadOnly = False
            CleanControls()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleEquipmentType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs)

    End Sub

    Private Sub INDSleTrademark_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleTrademark.QueryPopUp
        If INDsleTrademark.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If TrademarkXPO Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                TrademarkXPO = model.ListTrademark()
            End Using
        End If
    End Sub

    Private Sub INDSlIVA_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleIVA.QueryPopUp
        If INDsleIVA.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If IVAXPO Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                IVAXPO = model.ListIVA()
            End Using
        End If
    End Sub

    Private Sub INDsleItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleItem.QueryPopUp
        If EquipmentXPO Is Nothing Then
            Using model As New MFixedAssetPurchaseOrder(_MyTag)
                EquipmentXPO = model.ListFixedAssetItem()
            End Using
        End If
    End Sub

    Private Sub INDsleBranchOffice_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleBranchOffice.QueryPopUp
        If INDsleBranchOffice.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If BranchOfficeXPO Is Nothing Then
            Using model As New MEquipmentEntry(_MyTag)
                BranchOfficeXPO = model.listBranchOffice()
            End Using
        End If

    End Sub

    Private Sub INDsleFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleFunctionalUnit.QueryPopUp
        If INDsleBranchOffice.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la sucursal."
            e.Cancel = True
            Exit Sub
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSleEquipment_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleItem.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(572, Nothing, True)
            Using model As New MFixedAssetPurchaseOrder(_MyTag)
                EquipmentXPO = model.ListFixedAssetItem()
            End Using
        End If
    End Sub

    Private Sub INDSleTrademark_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTrademark.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1700, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)
                TrademarkXPO = model.ListTrademark()
            End Using
        End If
    End Sub

    Private Sub INDSleBranchOffice_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBranchOffice.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(563, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)
                BranchOfficeXPO = model.listBranchOffice()
            End Using
        End If
    End Sub

    Private Sub INDSleFunctionalUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFunctionalUnit.ButtonClick
        If INDsleBranchOffice.EditValue Is Nothing Then
            Exit Sub
        End If
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(523, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)
                FunctionalUnitXPO = model.ListFunctionalUnitByBranchOffice(INDsleBranchOffice.EditValue)
            End Using
        End If
    End Sub



    Private Sub INDSlIVA_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIVA.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1509, Nothing, True)
            Using model As New MEquipmentEntry(_MyTag)
                IVAXPO = model.ListIVA()
            End Using
        End If
    End Sub

#End Region

#Region "Click"

    'Metodo que se dispara cada que se agrega un nuevo articulo a la regilla
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Try
            Dim errors As String = String.Empty
            AsyncLoader(True)
            INDBtnAdd.Enabled = False
            If _importDataMode Then
                errors = ValidateControlsPopup()
            End If
            'Valida si los controles han sido diligenciados en su totalidad
            If ValidateControls() = False OrElse Not String.IsNullOrEmpty(errors) Then
                AsyncLoader(False)
                INDBtnAdd.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If

            'Se valida que el articulo que se va a ingresar no este en el listado del form principal
            If _editMode = False Then 'Si se esta agregando
                If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then
                    If (From l In ListCompare Where l.ItemId = INDsleItem.EditValue Select l).Count > 0 Then
                        AsyncLoader(False)
                        INDBtnAdd.Enabled = True
                        Mensaje(EeventViewerImages.Advertencia) = "El articulo " + INDsleItem.Text + " ya se encuentra en el listado."
                        Exit Sub
                    End If
                End If
            End If

            'Se registran las propiedades en la entidad
            SetValues()

            If _listFixedAssetPurchaseOrderEquipmentValidation Is Nothing Then
                _listFixedAssetPurchaseOrderEquipmentValidation = New List(Of FixedAssetPurchaseOrderItem)
            End If
            Dim args As New AddEquipmentPurchaseOrderEventArgs
            If _editMode = True Then
                args.FixedAssetPurchaseOrderEquipment = FixedAssetPurchaseOrderEquipment
                args.EditMode = True
            Else
                args.FixedAssetPurchaseOrderEquipment = FixedAssetPurchaseOrderEquipment
                _listFixedAssetPurchaseOrderEquipmentValidation.Add(FixedAssetPurchaseOrderEquipment)
            End If
            If _importDataMode = True Then
                args.ImportDataMode = True
                args.ListFixedAssetPurchaseOrderEquipment = _ListFixedAssetPurchaseOrderEquipment
                _listFixedAssetPurchaseOrderEquipmentValidation.AddRange(_ListFixedAssetPurchaseOrderEquipment)
            End If
            RaiseEvent AddEquipmentRemissionEventArgs(Nothing, args)
            AsyncLoader(False)
            If _importDataMode = True OrElse _editMode = True Then
                FixedAssetPurchaseOrderEquipment = Nothing
                Me.Close()
            Else
                CleanControls()
            End If
            INDBtnAdd.Enabled = True
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub

    Private Sub INDBtnAddDetailEquipment_Click(sender As Object, e As EventArgs)

    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmPopUpEquipment_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If FixedAssetPurchaseOrderEquipment IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDtxtIVAValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtIVAValue.EditValueChanged
        CalculateTotalValue()
    End Sub

    Private Sub INDseDiscountPercentage_EditValueChanged(sender As Object, e As EventArgs) Handles INDseDiscountPercentage.EditValueChanged
        If INDseDiscountPercentage.EditValue IsNot Nothing AndAlso INDseDiscountPercentage.EditValue > 0 AndAlso SubTotalValue AndAlso SubTotalValue > 0 Then
            DiscountValue = Utils.RoundValue(SubTotalValue * (INDseDiscountPercentage.EditValue / 100), roundingDecimals)
        Else
            DiscountValue = 0
        End If
    End Sub

    Private Sub INDtxtDiscountValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtDiscountValue.EditValueChanged
        CalculateIVAValue()
        CalculateTotalValue()
    End Sub

    Private Sub INDtxtSubTotalValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtSubTotalValue.EditValueChanged, INDseIVAPercentage.EditValueChanged
        CalculateIVAValue()
        CalculateTotalValue()
    End Sub

    Private Sub INDseQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDseQuantity.EditValueChanged, INDtxtUnitValue.EditValueChanged
        CalculateValue()
    End Sub

    Private Sub INDSlIVA_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIVA.EditValueChanged
        If INDsleIVA.EditValue IsNot Nothing AndAlso INDsleIVA.Properties.View.FocusedRowHandle >= 0 Then
            INDseIVAPercentage.EditValue = INDsleIVA.Properties.View.GetFocusedRowCellValue("Percentage")
        End If
    End Sub

    Private Sub INDSleEquipment_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleItem.EditValueChanged
        If INDsleItem.EditValue IsNot Nothing Then
            Dim xpo = Presenter.GetItemById(INDsleItem.EditValue)
            UnitValue = Math.Round(xpo.LastCostItem / _tRMValue, 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Sub INDsleBranchOffice_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBranchOffice.EditValueChanged
        If INDsleBranchOffice.EditValue Is Nothing Then
            Exit Sub
        End If
        If INDsleFunctionalUnit.Properties.ReadOnly = True Then
            Exit Sub
        End If
        Using model As New MEquipmentEntry(_MyTag)
            FunctionalUnitXPO = model.ListFunctionalUnitByBranchOffice(INDsleBranchOffice.EditValue)
        End Using

    End Sub

#End Region

#Region "RecordNavigationChangeEvent"
    ''' <summary>
    ''' Se dispara cuando navega entre items importados
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        If FixedAssetPurchaseOrderEquipment IsNot Nothing Then
            SetValuesPurchaseOrder(BarraBotones.LastRecordPosition)
        End If

        LoadControls(Record)
    End Sub
#End Region
#Region "KeyDown"

    Private Sub FrmPopupPurchaseOrderEquipment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub


#End Region

#End Region

End Class