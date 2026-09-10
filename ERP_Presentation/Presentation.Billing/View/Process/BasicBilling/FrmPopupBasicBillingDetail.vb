'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-11-14
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP
Imports Presentation.Accounting.MVP

#End Region

Public Class FrmPopupBasicBillingDetail
    Implements IBasicBillingDetail

#Region "Builder"
    Public Sub New(Optional _currency As Currency = Nothing, Optional _tRMValue As Decimal = 1)
        ' This call is required by the designer.
        InitializeComponent()
        Me._tRMValue = _tRMValue
        Me._indigoSession = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigoSession.OfficialCurrencyId,
                            .Abbreviation = Me._indigoSession.CurrencyISO4217}, _currency)
        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)
        ' Add any initialization after the InitializeComponent() call.
    End Sub
#End Region

#Region "Events"

    Public Event AddBasicBillingDetail(sender As Object, e As AddBasicBillingDetailEventArgs)

#End Region

#Region "Globals"

    Private Const MODULE_NAME = "Inventory"

    Private _presenter As PBasicBillingDetail

    Private _addressId As Integer

    Private _wareHouseId As Integer?

    Private _editMode As Boolean

    Private _retentionPercentageIVA As Decimal

    Private _basicBillingDetail As BasicBillingDetail

    Private _product As InventoryProduct

    Private _listPhysicalInventory As List(Of PhysicalInventory)

    Private _concept As BillingConcept

    Private _physicalAsset As FixedAssetPhysicalAsset

    Private _physicalAssetPart As FixedAssetPhysicalAssetParts

    Private _SettingsBilling As SettingsBilling

    Private _indigoSession As SessionValues

    Private listWarehouseIds As List(Of Integer)

    Private productId As Integer?

    Private warehouse As Warehouse

    ''' <summary>
    ''' Configuración del Tenant del campo "Asociar Actividad Económica en Transacciones"
    ''' </summary>
    Private _isEconomicActivity As Boolean

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _headCurrency As Currency

    ''' <summary>
    ''' variable de la tasa de cambio, por defecto es 1 cuando la moneda es igual a la oficial
    ''' </summary>
    Private _tRMValue As Decimal = 1

    ''' <summary>
    ''' Tarifa de productos y servicios
    ''' </summary>
    Private _productAndServicesFee As ProductAndServiceFee

    ''' <summary>
    ''' Establece el Id del servicio Principal si el parametro de maneja diferentes tarifas está activo
    ''' </summary>
    Private _ServiceSecondaryId As Integer

    ''' <summary>
    ''' Establece el tipo de contribuyente del tercero relacionado al cliente seleccionado en la cabecera
    ''' </summary>
    Private _ContributionType As Byte

    ''' <summary>
    ''' Establece las configuraciones del modulo de activos fijos
    ''' </summary>
    Private _SettingsFixedAsset As SettingFixedAsset

    ''' <summary>
    ''' propiedad que obtiene o establece el trm
    ''' </summary>
    Private Property TRM As TRM

#End Region

#Region "Fields"

    WriteOnly Property ActionOnControls As Boolean
        Set(value As Boolean)
            INDSeQuantity.Enabled = value
            INDTxtPrice.Enabled = value
            INDSePercentageDiscount.Enabled = value
            INDSePercentageIVA.Enabled = value
            INDTxtSubTotal.Enabled = value
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

#End Region

#Region "Properties"

    Public Property customerThirdParty As CommonThirdPartyXpo

    Public Property ListBasicBillingDetail As List(Of BasicBillingDetail)

    Public Property IdOperativeUnit As Integer

    Public ReadOnly Property MyTag As Object Implements IBasicBillingDetail.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public WriteOnly Property AddressId As Integer Implements IBasicBillingDetail.AddressId
        Set(value As Integer)
            Me._addressId = value
        End Set
    End Property

    Public WriteOnly Property ContributionType As Byte Implements IBasicBillingDetail.ContributionType
        Set(value As Byte)
            Me._ContributionType = value
        End Set
    End Property

    Public Property FeeId As Integer? Implements IBasicBillingDetail.FeeId
        Get
            Return INDSleRate.EditValue
        End Get
        Set(value As Integer?)
            INDSleRate.EditValue = value
        End Set
    End Property

    Public Property WareHouseId As Integer Implements IBasicBillingDetail.WareHouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer)
            INDSleWarehouse.EditValue = value
        End Set
    End Property

    Public Property ServicesProvided As Integer? Implements IBasicBillingDetail.ServicesProvided
        Get
            Return INDgleServicesProvided.EditValue
        End Get
        Set(value As Integer?)
            INDgleServicesProvided.EditValue = value
        End Set
    End Property

    Public Property Supplier As Integer? Implements IBasicBillingDetail.Supplier
        Get
            Return INDgleSupplier.EditValue
        End Get
        Set(value As Integer?)
            INDgleSupplier.EditValue = value
        End Set
    End Property

    Public Property SalesExecutive As Integer? Implements IBasicBillingDetail.SalesExecutive
        Get
            Return INDgleSalesExecutive.EditValue
        End Get
        Set(value As Integer?)
            INDgleSalesExecutive.EditValue = value
        End Set
    End Property

    Public WriteOnly Property BasicBillingDetail As BasicBillingDetail Implements IBasicBillingDetail.BasicBillingDetail
        Set(value As BasicBillingDetail)
            Me._basicBillingDetail = value
        End Set
    End Property

    Public WriteOnly Property EditMode As Boolean Implements IBasicBillingDetail.EditMode
        Set(value As Boolean)
            Me._editMode = value
        End Set
    End Property

    Public Property RoundLevel As Integer Implements IBasicBillingDetail.RoundLevel

    Public Property BillingConceptId As Integer? Implements IBasicBillingDetail.BillingConceptId
        Get
            Return INDSleBillingConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleBillingConcept.EditValue = value
        End Set
    End Property

    Private Property DetailType As Integer Implements IBasicBillingDetail.DetailType
        Get
            Return INDGleDetailType.EditValue
        End Get
        Set(value As Integer)
            INDGleDetailType.EditValue = value
        End Set
    End Property

    Public Property PhysicalAssetId As Integer? Implements IBasicBillingDetail.PhysicalAssetId
        Get
            Return INDSlePhysicalAsset.EditValue
        End Get
        Set(value As Integer?)
            INDSlePhysicalAsset.EditValue = value
        End Set
    End Property

    Public Property PhysicalAssetPartId As Integer? Implements IBasicBillingDetail.PhysicalAssetPartId
        Get
            Return INDSlePhysicalAssetPart.EditValue
        End Get
        Set(value As Integer?)
            INDSlePhysicalAssetPart.EditValue = value
        End Set
    End Property

    Public Property FunctionalUnitId As Integer Implements IBasicBillingDetail.FunctionalUnitId
        Get
            Return INDSleFunctionalUnit.EditValue
        End Get
        Set(value As Integer)
            INDSleFunctionalUnit.EditValue = value
        End Set
    End Property

    Public Property EconomicActivityId As Integer? Implements IBasicBillingDetail.EconomicActivityId
        Get
            Return INDsleEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDsleEconomicActivity.EditValue = value
        End Set
    End Property

    Private ReadOnly Property Value As Decimal Implements IBasicBillingDetail.Value
        Get
            Return Utils.RoundValue((INDSeQuantity.EditValue * INDTxtPrice.EditValue), Me.RoundLevel)
        End Get
    End Property

    Private ReadOnly Property ValueDiscount As Decimal Implements IBasicBillingDetail.ValueDiscount
        Get
            Return Utils.RoundValue((Me.Value * INDSePercentageDiscount.EditValue / 100), Me.RoundLevel)
        End Get
    End Property

    Private ReadOnly Property ValueIVA As Decimal Implements IBasicBillingDetail.ValueIVA
        Get
            Return Utils.RoundValue(((Me.Value - Me.ValueDiscount) * INDSePercentageIVA.EditValue / 100), Me.RoundLevel)
        End Get
    End Property

    Private ReadOnly Property SubTotalValue As Decimal Implements IBasicBillingDetail.SubTotalValue
        Get
            Return Utils.RoundValue((Me.Value - Me.ValueDiscount + Me.ValueIVA), Me.RoundLevel)
        End Get
    End Property


    Private Property RetentionIdTax As Integer? Implements IBasicBillingDetail.RetentionIdTax

    Private Property RetentionPercentageTax As Decimal Implements IBasicBillingDetail.RetentionPercentageTax

    Private Property RetentionBaseTax As Decimal Implements IBasicBillingDetail.RetentionBaseTax

    Private Property RetentionIdICA As Integer? Implements IBasicBillingDetail.RetentionIdICA

    Private Property RetentionPercentageICA As Decimal Implements IBasicBillingDetail.RetentionPercentageICA

    Private Property RetentionBaseICA As Decimal Implements IBasicBillingDetail.RetentionBaseICA

    Private Property CostCenterId As Integer? Implements IBasicBillingDetail.CostCenterId

    ''' <summary>
    ''' propiedad que establece la mascara de decimales
    ''' </summary>
    Public WriteOnly Property SetFieldMask As String
        Set(value As String)
            INDTxtPrice.Properties.Mask.EditMask = value
            INDTxtSubTotal.Properties.Mask.EditMask = value
        End Set
    End Property

    ''' <summary>
    ''' variable que guarda el número de decimales que se van a usar
    ''' </summary>
    Public Property RoundDecimal As Integer = 0

#End Region

#Region "XPO"

    Public Property ProductAndServiceFeeXPO As XPInstantFeedbackSource Implements IBasicBillingDetail.ProductAndServiceFeeXPO
        Get
            Return CType(INDSleRate.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleRate.Properties.DataSource = value
        End Set
    End Property

    Public Property BillingConceptXPO As XPInstantFeedbackSource Implements IBasicBillingDetail.BillingConceptXPO
        Get
            Return CType(INDSleBillingConcept.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBillingConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property PhysicalAssetXPO As XPInstantFeedbackSource Implements IBasicBillingDetail.PhysicalAssetXPO
        Get
            Return CType(INDSlePhysicalAsset.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlePhysicalAsset.Properties.DataSource = value
        End Set
    End Property

    Public Property PhysicalAssetPartXPO As XPInstantFeedbackSource Implements IBasicBillingDetail.PhysicalAssetPartXPO
        Get
            Return CType(INDSlePhysicalAssetPart.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlePhysicalAssetPart.Properties.DataSource = value
        End Set
    End Property


    Public Property WarehouseXpo As XPInstantFeedbackSource Implements IBasicBillingDetail.WarehouseXpo
        Get
            Return CType(INDSleWarehouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

    Public Property SupplierXPO As XPInstantFeedbackSource Implements IBasicBillingDetail.SupplierXPO
        Get
            Return CType(INDgleSupplier.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDgleSupplier.Properties.DataSource = value
        End Set
    End Property

    Public Property ServicesProvidedXPO As XPInstantFeedbackSource Implements IBasicBillingDetail.ServicesProvidedXPO
        Get
            Return CType(INDgleServicesProvided.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDgleServicesProvided.Properties.DataSource = value
        End Set
    End Property

    Public Property SalesExecutiveXPO As XPInstantFeedbackSource Implements IBasicBillingDetail.SalesExecutiveXPO
        Get
            Return CType(INDgleSalesExecutive.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDgleSalesExecutive.Properties.DataSource = value
        End Set
    End Property

    Public Property FunctionalUnitXpo As XPInstantFeedbackSource Implements IBasicBillingDetail.FunctionalUnitXpo
        Get
            Return CType(INDSleFunctionalUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    Public Property EconomicActivityDatasource As XPInstantFeedbackSource Implements IBasicBillingDetail.EconomicActivityDatasource
        Get
            Return CType(INDsleEconomicActivity.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEconomicActivity.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Bar Buttons"

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.CleanControls()
    End Sub

#End Region

#Region "Handles"

#Region "Load And Disposed"

    Private Async Sub FrmPopupBasicBillingDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = False
        Me._indigoSession = SessionValues.Instance

        If Me._headCurrency?.Id <> Me._indigoSession?.OfficialCurrencyId AndAlso Me._tRMValue = 1 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("TRMEmpty")
            Me.Close()
            Exit Sub
        End If

        If _SettingsBilling Is Nothing Then
            Await GetCompanySettings()
        End If

        Me._presenter = New PBasicBillingDetail(Me)
        Me.InitTuples()

        'Se trae la configuración del Tenant para las Actividades Económicas
        Using modelCompany As New MCompanySettings(Tag)
            Dim config = Await modelCompany.GetCompanySettings()
            If config IsNot Nothing Then
                _isEconomicActivity = config.TransactionEconomicActivity
            End If
        End Using

        If Me._editMode Then
            Await LoadControls()
        Else
            Me.CleanControls()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Me._presenter = Nothing
        Me._indigoSession = Nothing
        Me._wareHouseId = Nothing
        Me._editMode = Nothing
        Me._retentionPercentageIVA = Nothing
        Me._basicBillingDetail = Nothing
        Me._product = Nothing
        Me._listPhysicalInventory = Nothing
        Me._concept = Nothing
        Me._physicalAsset = Nothing
        Me._physicalAssetPart = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmPopupBasicBillingDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDGleDetailType.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDPceProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProduct.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Me.AssignProductWithCode()
        End If
    End Sub

    Private Sub FrmPopupBasicBillingDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDTxtTotal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtSubTotal.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnit.QueryPopUp
        If FunctionalUnitXpo Is Nothing Then
            Me._presenter.InitializeFunctionalUnitXPO()
        End If
    End Sub

    Private Sub INDSleRate_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRate.QueryPopUp
        If ProductAndServiceFeeXPO Is Nothing Then
            _presenter.InitializeProductAndServiceFee()
        End If
    End Sub

    Private Sub INDPceProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProduct.QueryPopUp
        If INDSleRate.EditValue Is Nothing Then
            CtrProducts1.SetDataSourceProduct()
        Else
            CtrProducts1.SetDataSourceProduct(ProductAndServiceFeeId:=INDSleRate.EditValue)
        End If
    End Sub

    Private Sub INDPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceBatchSerial.QueryPopUp
        If WareHouseId <> 0 Then
            CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
            CtrPhysicalInventory1.Product = Me._product
            CtrPhysicalInventory1.WareHouseId = Me.WareHouseId
            CtrPhysicalInventory1.FormOwner = Me
            CtrPhysicalInventory1.SetListPhysicalInventory()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un almacen"
        End If
    End Sub

    Private Sub INDSleBillingConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBillingConcept.QueryPopUp
        If INDSleRate.EditValue Is Nothing Then
            Me._presenter.InitializeBillingConceptXPO()
        Else
            INDSleBillingConcept.Properties.ValueMember = "ServiceId.Id"
            Me._presenter.InitializeServiceFeeXPO(FeeId)
        End If
    End Sub

    Private Sub INDSlePhysicalAsset_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePhysicalAsset.QueryPopUp
        If PhysicalAssetXPO Is Nothing Then
            Me._presenter.InitializePhysicalAssetXPO()
        End If
    End Sub

    Private Sub INDgleServicesProvided_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDgleServicesProvided.QueryPopUp
        If ServicesProvidedXPO Is Nothing AndAlso BillingConceptId IsNot Nothing Then
            _presenter.InitializeServicesProvidedXPO(BillingConceptId)
        End If
    End Sub

    Private Sub INDgleSupplier_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDgleSupplier.QueryPopUp
        If SupplierXPO Is Nothing Then
            _presenter.InitializeSupplierXPO()
        End If
    End Sub

    Private Sub INDgleSalesExecutive_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDgleSalesExecutive.QueryPopUp
        If SalesExecutiveXPO Is Nothing Then
            _presenter.InitializeSalesExecutiveXPO()
        End If
    End Sub

    Private Sub INDSlePhysicalAssetPart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePhysicalAssetPart.QueryPopUp
        If PhysicalAssetPartXPO Is Nothing Then
            Me._presenter.InitializePhysicalAssetPartXPO()
        End If
    End Sub

    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If WarehouseXpo Is Nothing Then
            Me._presenter.InitializeWarehouseXPO(listWarehouseIds)
        End If
    End Sub

    ''' <summary>
    ''' Cargamos el Datasource de las actividades económicas para los detalles de tipo "Activos Fijos"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEconomicActivity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleEconomicActivity.QueryPopUp
        If EconomicActivityDatasource Is Nothing Then _presenter.InitializeEconomicActivityDatasource()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleFunctionalUnitFirst_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleFunctionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("523", Nothing, True)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleFunctionalUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleFunctionalUnit.EditValueChanged
        Me.CostCenterId = Nothing
        If FunctionalUnitId > 0 Then
            Dim functionalUnit = DirectCast(INDSleFunctionalUnit.GetSelectedObject(), Infrastructure.Data.Xpo.PayrollRepository.PayrollFunctionalUnit)
            If functionalUnit Is Nothing Then
                functionalUnit = Me._presenter.GetFunctionalUnitById(FunctionalUnitId)
            End If
            Me.CostCenterId = functionalUnit.CostCenterId.Id
        End If
    End Sub

    Private Async Sub INDGleDetailType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleDetailType.EditValueChanged
        INDPceProduct.EditValue = Nothing
        INDPceProduct.Text = String.Empty
        INDLciProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDSleRate.EditValue = Nothing

        INDPceBatchSerial.EditValue = Nothing
        INDPceBatchSerial.Text = String.Empty
        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDSleBillingConcept.EditValue = Nothing
        INDSleBillingConcept.Properties.NullText = String.Empty
        INDLciBillingConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDSlePhysicalAsset.EditValue = Nothing
        INDSlePhysicalAsset.Properties.NullText = String.Empty
        INDLciPhysicalAsset.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDSlePhysicalAssetPart.EditValue = Nothing
        INDSlePhysicalAssetPart.Properties.NullText = String.Empty
        INDLciPhysicalAssetPart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        EconomicActivityId = Nothing
        INDsleEconomicActivity.Properties.NullText = String.Empty
        INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        Select Case DetailType
            Case 1
                INDLciProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDliServicesProvided.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDliSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDliSalesExecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Case 2
                INDSeQuantity.Properties.ReadOnly = False
                INDLciBillingConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDliServicesProvided.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                If _SettingsBilling.AllowsSalesExecutiveAndSupplier Then
                    INDliSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDliSalesExecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDliSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliSalesExecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            Case 3
                INDSeQuantity.EditValue = 1
                INDSleRate.EditValue = Nothing
                INDSeQuantity.Properties.ReadOnly = True
                INDLciPhysicalAsset.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDliServicesProvided.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDliSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDliSalesExecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                'Mostramos el segmento de actividad económica según la configuración del Tenant
                If _isEconomicActivity Then INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Await LoadCurrencyFixedAssetModule()
            Case 4
                INDSeQuantity.EditValue = 1
                INDSeQuantity.Properties.ReadOnly = True
                INDLciPhysicalAssetPart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDliServicesProvided.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDliSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDliSalesExecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End Select
    End Sub

    ''' <summary>
    ''' Consulta los parametros de activos fijos para identificar la moneda del modulo
    ''' </summary>
    Private Async Function LoadCurrencyFixedAssetModule() As Task
        If _SettingsFixedAsset Is Nothing Then
            Using model As New MSettingFixedAsset(MyTag)
                _SettingsFixedAsset = Await model.GetSettingFixedAssetByOperatingUnitId(_IdOperativeUnit)
            End Using
        End If
    End Function

    Private Async Sub INDsleRate_editvaluechanged(sender As Object, e As EventArgs) Handles INDSleRate.EditValueChanged
        If FeeId > 0 Then
            Using model As New MProductAndServiceFee(Me.Tag)
                _productAndServicesFee = Await model.GetProductAndServiceFeeById(INDSleRate.EditValue)
            End Using
        End If
    End Sub

    Private Async Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        If Not _editMode Then
            INDPceProduct.Text = e.CodeNameProduct
            INDPceProduct.Focus()
            INDPceProduct.ClosePopup()
            INDPceBatchSerial.EditValue = String.Empty
            INDSleWarehouse.EditValue = Nothing
            INDSeQuantity.EditValue = 0
            CtrPhysicalInventory1.CleanControls()

            Me.AsyncLoader(True)
            Using model As New MBasicBillingDetail(Me.Tag)

                Me._product = Await model.GetInventoryProductByIdWithoutAggregates(e.ProductId)

                If Me._product.HandlesBatch Then
                    INDSeQuantity.Properties.ReadOnly = True
                    INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDPceBatchSerial.Focus()
                Else
                    INDSeQuantity.Properties.ReadOnly = False
                    INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSeQuantity.Focus()
                End If

                If Not _ContributionType = 5 Then 'Exento
                    INDSePercentageIVA.EditValue = Me._product.PercentageIVA
                End If

                Me.RetentionIdTax = Me._product.RetentionIdTax
                Me.RetentionPercentageTax = Me._product.RetentionPercentageTax

                Me.RetentionBaseTax = Utils.RoundValue(Me._product.RetentionBaseTax / _tRMValue, Me.RoundLevel)

                Me.RetentionIdICA = Me._product.RetentionIdICA

                Me.RetentionBaseICA = Utils.RoundValue(Me._product.RetentionBaseICA / _tRMValue, Me.RoundLevel)

                If INDSleRate.EditValue Is Nothing Then

                    INDTxtPrice.EditValue = Utils.RoundValue(Me._product.SellingPrice.Value / _tRMValue, Me.RoundLevel)

                Else
                    Dim productFeeDetail = _productAndServicesFee.ProductFeeDetail.FirstOrDefault(Function(x) x.ProductId = e.ProductId)
                    If productFeeDetail.FinalDate < GetDateServer() Or productFeeDetail.InitialDate > GetDateServer() Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontró tarifa vigente para el producto"
                        AsyncLoader(False)
                        Exit Sub
                    End If

                    If Not productFeeDetail.RateType Then
                        INDTxtPrice.EditValue = productFeeDetail.SalePrice / _tRMValue
                    Else
                        If productFeeDetail.PercentageType Then
                            INDTxtPrice.EditValue = (_product.ProductCost - (_product.ProductCost * (productFeeDetail.Percentage / 100))) / _tRMValue
                        Else
                            INDTxtPrice.EditValue = (_product.FinalProductCost - (_product.FinalProductCost * (productFeeDetail.Percentage / 100))) / _tRMValue
                        End If
                    End If
                End If

                'Se añade la actividad económica asociada al grupo del producto según la configuración del Tenant
                If _isEconomicActivity Then EconomicActivityId = _product.ProductGroup.EconomicActivityId

                ''Consulta para sacar los ids de los almacenes que tienen el producto
                Dim physicalInventory = model.GetPhysicalInventoryByProductId(e.ProductId)
                listWarehouseIds = (From item In physicalInventory
                                    Group By item.WarehouseId.Id Into Group
                                    From i In Group
                                    Select i.WarehouseId.Id).ToList()

                Me.AsyncLoader(False)
                Me.ActionOnControls = True

            End Using
        End If
    End Sub

    Private Async Sub INDSleBillingConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBillingConcept.EditValueChanged
        If Me.BillingConceptId > 0 Then
            Me.AsyncLoader(True)

            If Not _editMode Then
                Using model As New MBasicBillingDetail(Me.Tag)
                    Me._concept = Await model.GetBillingConceptById(BillingConceptId)

                    If FeeId Is Nothing Then
                        INDTxtPrice.EditValue = Math.Round(Me._concept.Price / Me._tRMValue, RoundDecimal, MidpointRounding.AwayFromZero)
                    Else

                        Dim ServiceFeeDetail = _productAndServicesFee.ServiceFeeDetail.FirstOrDefault(Function(x) x.ServiceId = BillingConceptId)
                        If ServiceFeeDetail IsNot Nothing Then
                            INDTxtPrice.EditValue = Math.Round(ServiceFeeDetail.SalePrice / Me._tRMValue, RoundDecimal, MidpointRounding.AwayFromZero)
                        End If

                        If _concept.AssociatedMainServiceId IsNot Nothing Then
                            _ServiceSecondaryId = _concept.AssociatedMainServiceId
                        End If
                    End If

                    If Not _ContributionType = 5 Then 'Exento
                        INDSePercentageIVA.EditValue = Me._concept.PercentageIVA
                    End If

                    Me.RetentionIdTax = Me._concept.RetentionIdTax
                    Me.RetentionPercentageTax = Me._concept.RetentionPercentageTax

                    Me.RetentionBaseTax = Utils.RoundValue(Me._concept.RetentionBaseTax / Me._tRMValue, Me.RoundLevel)

                    Me.RetentionIdICA = Me._concept.RetentionIdICA
                    Me.RetentionBaseICA = Utils.RoundValue(Me._concept.RetentionBaseICA / Me._tRMValue, Me.RoundLevel)

                    If _concept.CountSecondaryService > 0 Then
                        INDliServicesProvided.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        INDliServicesProvided.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If

                    'Se añade la actividad económica asociada al concepto de facturación del servicio según la configuración del Tenant
                    If _isEconomicActivity Then EconomicActivityId = _concept.EconomicActivityId
                End Using
            End If

            Me.AsyncLoader(False)
            Me.ActionOnControls = True
        End If
    End Sub

    Private Async Sub INDSlePhysicalAsset_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePhysicalAsset.EditValueChanged
        If Me.PhysicalAssetId > 0 Then
            If Not _editMode Then
                Me.AsyncLoader(True)

                Using model As New MBasicBillingDetail(Me.Tag)
                    Me._physicalAsset = model.GetPhysicalAssetById(PhysicalAssetId)
                    Await SetValorPhysicalAsset(_physicalAsset)
                End Using
            End If
            Me.AsyncLoader(False)
            Me.ActionOnControls = True
        End If
    End Sub

    ''' <summary>
    ''' Funcion que establece el valor de la TRM para los activos fijos
    ''' </summary>
    Private Async Function ValidateCurrency() As Task
        If _SettingsFixedAsset Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No se han cargado los parámetros de activos fijos"
            Return
        End If

        If _headCurrency.Id = _SettingsFixedAsset.CurrencyId Then
            Me.TRM = New TRM With {.CurrencyId = _headCurrency.Id, .OfficialCurrencyId = Me._SettingsFixedAsset.CurrencyId, .Value = 1}
            Return
        End If

        Await GetTRM(_SettingsFixedAsset.CurrencyId, _headCurrency.Id)
    End Function

    ''' <summary>
    ''' Establece el valor de los diferentes campos del PopUp dependiendo de la moneda del modulo de "Activos fijos" 
    ''' y de la moneda definida en la cabecera del documento
    ''' </summary>
    Private Async Function SetValorPhysicalAsset(ByVal _PhysicalAssetTmp As FixedAssetPhysicalAsset) As Task

        If _PhysicalAssetTmp Is Nothing Then Return

        Await ValidateCurrency()

        If TRM IsNot Nothing Then
            INDTxtPrice.EditValue = Math.Round(Me._physicalAsset.FairValue / TRM.Value, RoundDecimal, MidpointRounding.AwayFromZero)

            If Not _ContributionType = 5 Then 'Exento
                INDSePercentageIVA.EditValue = Me._physicalAsset.PercentageIVA
            End If

            Me.RetentionIdTax = Me._physicalAsset.RetentionIdTax
            Me.RetentionPercentageTax = Me._physicalAsset.RetentionPercentageTax

            Me.RetentionBaseTax = Utils.RoundValue(Me._physicalAsset.RetentionBaseTax / Me.TRM.Value, Me.RoundLevel)

            'Si el cliente maneja ICA
            If customerThirdParty?.Ica Then
                Me.RetentionIdICA = Me._physicalAsset.RetentionIdICA

                Me.RetentionBaseICA = Utils.RoundValue(Me._physicalAsset.RetentionBaseICA / TRM.Value, Me.RoundLevel)

            End If
        End If
    End Function

    ''' <summary>
    ''' Funcion que se encarga de consultar el TRM
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetTRM(_currencyId As Integer, ToCurrencyId As Integer) As Task(Of Boolean)
        Using Model As New MBasicBilling(Me.Tag)
            Dim Result = Await Model.GetTRMbyCurrencyIdAsync(ToCurrencyId, _currencyId)

            If Not Result?.StateResult Then
                Me.Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                Return False
            End If

            Me.TRM = Result.ObjectEmbbeded
            Me.Mensaje(EeventViewerImages.Informacion) = Result?.Message
            Return True

        End Using
    End Function

    Private Sub INDSlePhysicalAssetPart_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePhysicalAssetPart.EditValueChanged
        If Me.PhysicalAssetPartId > 0 Then
            Me.AsyncLoader(True)

            Me.AsyncLoader(False)
            Me.ActionOnControls = True
        End If
    End Sub

    Private Sub INDSeQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeQuantity.EditValueChanged
        INDTxtSubTotal.EditValue = Me.SubTotalValue
    End Sub

    Private Sub INDTxtSalePrice_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtPrice.EditValueChanged
        INDTxtSubTotal.EditValue = Me.SubTotalValue
    End Sub

    Private Sub INDSeDiscountPercent_EditValueChanged(sender As Object, e As EventArgs) Handles INDSePercentageDiscount.EditValueChanged
        INDTxtSubTotal.EditValue = Me.SubTotalValue
    End Sub

    Private Sub INDSePercentageIVA_EditValueChanged(sender As Object, e As EventArgs) Handles INDSePercentageIVA.EditValueChanged
        INDTxtSubTotal.EditValue = Me.SubTotalValue
    End Sub

    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        INDSeQuantity.EditValue = 0
        INDPceBatchSerial.EditValue = String.Empty
        CtrPhysicalInventory1.CleanControls()
    End Sub

#End Region

#Region "EditValueChanging"
    Private Sub INDSleProduct_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDPceProduct.EditValueChanging
        If INDSleWarehouse.EditValue IsNot Nothing Then
            If e.NewValue <> e.OldValue Then
                INDSleWarehouse.Properties.DataSource = Nothing
            End If
        End If
    End Sub
#End Region

#Region "Closed"

    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerial.Closed
        Dim quantity As Integer = 0
        Me._listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
        If Me._listPhysicalInventory.Count > 0 Then
            quantity = Me._listPhysicalInventory.Sum(Function(x) x.QuantityDeliver)
            INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), Me._listPhysicalInventory.Count.ToString(), quantity.ToString())
        Else
            INDPceBatchSerial.EditValue = Nothing
        End If
        INDSeQuantity.EditValue = quantity
    End Sub

#End Region

#Region "Click"

    Private Async Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        AsyncLoader(True)

        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            AsyncLoader(False)
            Exit Sub
        End If

        If _editMode Then
            If ListBasicBillingDetail.Where(Function(x) x.Id <> _basicBillingDetail.Id).Any(
                Function(x) x.DetailType = _basicBillingDetail.DetailType AndAlso
                            (
                                (x.DetailType = 1 AndAlso x.ProductId = _basicBillingDetail.ProductId) OrElse
                                (x.DetailType = 2 AndAlso IIf(_basicBillingDetail.ServicesProvidedId IsNot Nothing,
                                                              x.ServicesProvidedId = _basicBillingDetail.ServicesProvidedId,
                                                              x.BillingConceptId = _basicBillingDetail.BillingConceptId)) OrElse
                                (x.DetailType = 3 AndAlso x.PhysicalAssetId = _basicBillingDetail.PhysicalAssetId) OrElse
                                (x.DetailType = 4 AndAlso x.PhysicalAssetPartId = _basicBillingDetail.PhysicalAssetPartId)
                            )
            ) Then
                Exit Sub
            End If
        End If

        Dim resultAssignValues = Await AssignValues()

        If resultAssignValues.StateResult = False Then
            Mensaje(EeventViewerImages.Advertencia) = resultAssignValues.Message
            AsyncLoader(False)
            Exit Sub
        End If

        RaiseEvent AddBasicBillingDetail(Nothing, New AddBasicBillingDetailEventArgs With
            {
                .EditMode = Me._editMode,
                .BasicBillingDetail = Me._basicBillingDetail
            }
        )

        AsyncLoader(False)
        If _editMode = True Then
            Me.Close()
        End If
        Me.CleanControls()
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Sub InitTuples()
        Dim tipos As New List(Of Tuple(Of Byte, String))()
        tipos.Add(New Tuple(Of Byte, String)(1, "Producto"))
        tipos.Add(New Tuple(Of Byte, String)(2, "Servicio"))
        tipos.Add(New Tuple(Of Byte, String)(3, "Activo Fijo"))
        'Pendiente analisis contable
        'tipos.Add(New Tuple(Of Byte, String)(4, "Parte de Activo Fijo"))
        INDGleDetailType.Properties.DataSource = tipos
    End Sub


    Private Async Function GetCompanySettings() As Task
        Using model As New MBillingSetting(MyTag)
            Me._SettingsBilling = Await model.GetSettingsBillingByIdUnitOperative(IdOperativeUnit, False)

            If _SettingsBilling IsNot Nothing Then
                If _SettingsBilling.HandlesDifferentRates Then
                    INDLciRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            End If
        End Using
    End Function

    Private Sub CleanControls()
        INDLciProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciBillingConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciPhysicalAsset.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciPhysicalAssetPart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDGleDetailType.EditValue = Nothing
        INDGleDetailType.Properties.ReadOnly = False

        INDPceProduct.EditValue = Nothing
        INDPceProduct.Text = String.Empty
        INDPceProduct.Properties.ReadOnly = False

        INDPceBatchSerial.EditValue = Nothing
        INDPceBatchSerial.Text = String.Empty

        INDSleRate.EditValue = Nothing
        INDSleRate.Properties.NullText = String.Empty

        INDSleBillingConcept.EditValue = Nothing
        INDSleBillingConcept.Properties.NullText = String.Empty
        INDSleBillingConcept.Properties.ReadOnly = False

        ServicesProvided = Nothing
        INDgleServicesProvided.Properties.NullText = String.Empty
        Supplier = Nothing
        INDgleSupplier.Properties.NullText = String.Empty
        SalesExecutive = Nothing
        INDgleSalesExecutive.Properties.NullText = String.Empty

        INDSlePhysicalAsset.EditValue = Nothing
        INDSlePhysicalAsset.Properties.NullText = String.Empty
        INDSlePhysicalAsset.Properties.ReadOnly = False

        INDSlePhysicalAssetPart.EditValue = Nothing
        INDSlePhysicalAssetPart.Properties.NullText = String.Empty
        INDSlePhysicalAssetPart.Properties.ReadOnly = False

        EconomicActivityId = Nothing

        INDSeQuantity.EditValue = 0
        INDTxtPrice.EditValue = 0
        INDSePercentageDiscount.EditValue = 0
        INDSePercentageIVA.EditValue = 0
        INDTxtSubTotal.EditValue = 0


        Me._editMode = False
        Me._basicBillingDetail = New BasicBillingDetail
        Me._product = Nothing
        CtrPhysicalInventory1.CleanControls()
        Me._listPhysicalInventory = Nothing
        Me._concept = Nothing
        Me._physicalAsset = Nothing
        Me._physicalAssetPart = Nothing
        _ServiceSecondaryId = Nothing

        Me.RetentionIdTax = Nothing
        Me.RetentionPercentageTax = 0
        Me.RetentionBaseTax = 0

        Me.RetentionIdICA = Nothing
        Me.RetentionPercentageICA = 0
        Me.RetentionBaseICA = 0
        Me.WareHouseId = Nothing
        INDLciWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        ActionOnControls = False
        INDBtnAdd.Enabled = True
        INDBtnAdd.Text = ResourceManager.GetString("Add")

        INDPceProduct.Focus()
    End Sub

    Private Async Function LoadControls() As Task
        Try
            AsyncLoader(True)

            ActionOnControls = True
            INDBtnAdd.Text = ResourceManager.GetString("Edit")

            With Me._basicBillingDetail
                DetailType = .DetailType
                INDGleDetailType.Properties.ReadOnly = True

                If .WarehouseId IsNot Nothing Then
                    INDSleWarehouse.EditValue = .WarehouseId
                    INDSleWarehouse.Properties.NullText = .WarehouseName
                End If

                INDSeQuantity.EditValue = .Quantity

                If .FunctionalUnitId IsNot Nothing Then
                    FunctionalUnitId = .FunctionalUnitId
                    INDSleFunctionalUnit.Properties.NullText = .FunctionalUnitIdName
                End If


                If DetailType = 1 Or DetailType = 2 Then
                    If .FeeId IsNot Nothing Then
                        INDLciRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDSleRate.EditValue = .FeeId
                        INDSleRate.Properties.NullText = .FeeName
                        INDSleRate.Properties.ReadOnly = True
                    End If
                End If

                If DetailType = 1 Then
                    INDPceProduct.Properties.ReadOnly = True
                    INDPceProduct.Text = .CodeName

                    Using model As New MBasicBillingDetail(Me.Tag)
                        Me._product = Await model.GetInventoryProductByIdWithoutAggregates(.ProductId)

                        If .BasicBillingDetailItem IsNot Nothing AndAlso .BasicBillingDetailItem.Count > 0 Then
                            INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), .BasicBillingDetailItem.Count.ToString(), .BasicBillingDetailItem.Sum(Function(x) x.Quantity).ToString())
                            CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
                            CtrPhysicalInventory1.Product = Me._product
                            CtrPhysicalInventory1.WareHouseId = Me.WareHouseId
                            CtrPhysicalInventory1.FormOwner = Me
                            CtrPhysicalInventory1.SetListPhysicalInventory()
                            CtrPhysicalInventory1.SetQuantityPhysicalInventory(.BasicBillingDetailItem.ToList())
                            Me._listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
                            INDSeQuantity.Properties.ReadOnly = True
                        Else
                            INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            INDSeQuantity.Properties.ReadOnly = False
                        End If
                    End Using
                ElseIf DetailType = 2 Then

                    If .ServicesProvidedId IsNot Nothing Then
                        INDgleServicesProvided.Properties.ReadOnly = True
                        INDSleBillingConcept.Properties.NullText = .ServicesProvidedName
                        INDgleServicesProvided.Properties.NullText = .CodeName
                        INDliServicesProvided.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        INDSleBillingConcept.Properties.NullText = .CodeName
                        INDliServicesProvided.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If

                    INDSleBillingConcept.Properties.ReadOnly = True
                    INDSleBillingConcept.EditValue = .BillingConceptId
                    ServicesProvided = .ServicesProvidedId
                    Supplier = .SupplierId
                    INDgleSupplier.Properties.NullText = .SupplierName
                    SalesExecutive = .SalesExecutiveId
                    INDgleSalesExecutive.Properties.NullText = .SalesExecutiveName

                ElseIf DetailType = 3 Then
                    INDSlePhysicalAsset.Properties.ReadOnly = True
                    INDSlePhysicalAsset.EditValue = .PhysicalAssetId
                    INDSlePhysicalAsset.Properties.NullText = .CodeName
                    'Cargamos el Datasource para mostrar según la configuración del Tenant
                    If _isEconomicActivity Then _presenter.InitializeEconomicActivityDatasource()
                ElseIf DetailType = 4 Then
                    INDSlePhysicalAssetPart.Properties.ReadOnly = True
                    INDSlePhysicalAssetPart.EditValue = .PhysicalAssetPartId
                    INDSlePhysicalAssetPart.Properties.NullText = .CodeName
                End If

                INDTxtPrice.EditValue = .Price
                INDSePercentageDiscount.EditValue = .PercentageDiscount
                INDSePercentageIVA.EditValue = .PercentageIVA
                INDTxtSubTotal.EditValue = Me.SubTotalValue

                If .ProductId IsNot Nothing Then
                    Me.productId = .ProductId
                    If Me.productId > 0 Then
                        Using model As New MBasicBillingDetail(Me.Tag)
                            Dim physicalInventory = model.GetPhysicalInventoryByProductId(Me.productId)
                            listWarehouseIds = (From item In physicalInventory
                                                Group By item.WarehouseId.Id Into Group
                                                From i In Group
                                                Select i.WarehouseId.Id).ToList()
                        End Using
                    End If
                End If

                'Se trae la actividad económica asignada al momento de agregar el detalle
                EconomicActivityId = .EconomicActivityId

                Me.RetentionIdTax = .RetentionIdTax
                Me.RetentionPercentageTax = .RetentionPercentageTax
                Me.RetentionBaseTax = .RetentionBaseTax
                Me.RetentionIdICA = .RetentionIdICA
                Me.RetentionPercentageICA = .RetentionPercentageICA
                Me.RetentionBaseICA = .RetentionBaseICA
            End With
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAdd.Enabled = False
            Throw ex
        End Try
    End Function

    Private Sub AssignProductWithCode()
        If INDPceProduct.Text.Trim <> String.Empty Then
            Me.AsyncLoader(True)

            INDPceProduct.Focus()
            INDPceProduct.ClosePopup()
            CtrPhysicalInventory1.CleanControls()

            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct As String() = INDPceProduct.Text.Split(" - ")
            Dim codeProduct As String = arrayCodeProduct(0).Trim

            Using model As New MBasicBillingDetail(Me.Tag)
                Me._product = model.GetInventoryProductByCodeWithoutAggregates(codeProduct)
                If Me._product IsNot Nothing AndAlso Me._product.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto con código {0} no existe.", codeProduct)
                    Me.CleanControls()
                    AsyncLoader(False)
                    Exit Sub
                End If

                INDPceProduct.Text = Me._product.Code + " - " + Me._product.Name
                INDTxtPrice.EditValue = Me._product.SellingPrice / Me._tRMValue
                INDSePercentageIVA.EditValue = Me._product.PercentageIVA
                Me.RetentionIdTax = Me._product.RetentionIdTax
                Me.RetentionPercentageTax = Me._product.RetentionPercentageTax

                Me.RetentionBaseTax = Utils.RoundValue(Me._product.RetentionBaseTax / Me._tRMValue, Me.RoundLevel)

                Me.RetentionIdICA = Me._product.RetentionIdICA

                Me.RetentionBaseICA = Utils.RoundValue(Me._product.RetentionBaseICA / Me._tRMValue, Me.RoundLevel)

                If Me._product.HandlesBatch = True Then
                    INDSeQuantity.Properties.ReadOnly = True
                    INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDPceBatchSerial.Focus()
                Else
                    INDSeQuantity.Properties.ReadOnly = False
                    INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSeQuantity.Focus()
                End If

                'Se añade la actividad económica asociada al grupo del producto según la configuración del Tenant
                If _isEconomicActivity Then EconomicActivityId = _product.ProductGroup.EconomicActivityId
            End Using

            Me.AsyncLoader(False)
            Me.ActionOnControls = True
        ElseIf INDPceProduct.Text.Trim = String.Empty Then
            If Me._product IsNot Nothing Then
                INDPceProduct.Text = Me._product.Code + " - " + Me._product.Name
            End If
        End If
    End Sub

    ''' <summary>
    ''' Genera un mensaje según el tipo de detalle seleccionado y la configuración que se tiene en el Tenant
    ''' </summary>
    ''' <param name="detailType"></param>
    ''' <returns></returns>
    Private Function ValidateEconomicActivity(detailType As Integer) As String
        If Not _isEconomicActivity Then Return String.Empty

        Select Case detailType
            Case 1 ' Producto
                If EconomicActivityId Is Nothing Then
                    Return String.Format("No se tiene definida una Actividad Económica Generadora de Ingreso en el Grupo {0} asociado al Producto {1} ",
                                      _product.ProductGroup.Name, _product.Name)
                End If
            Case 2 ' Servicio / Concepto de facturación
                If EconomicActivityId Is Nothing Then
                    If _concept.ServiceOrderDetail.Count = 0 Then
                        Return String.Format("El concepto de facturación {0} no tiene un servicio asociado", _concept.Name)
                    Else
                        Return String.Format("No se tiene definida una Actividad Económica Generadora de Ingreso en el Concepto de Facturación {0} asociado al servicio {1}",
                                      _concept.Name, _concept.ServiceOrderDetail(0).ServiceOrder.Code)
                    End If
                End If
            Case 3 ' Activo Fijo
                If EconomicActivityId Is Nothing Then
                    Return "Se debe diligenciar el campo Actividad Económica Generadora de Ingreso"
                End If
        End Select

        Return String.Empty
    End Function

    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder

        'Se valida si existe una actividad económica que se va a asociar al detalle según la configuración del Tenant
        Dim economicActivityError = ValidateEconomicActivity(DetailType)

        If DetailType = 0 Then
            errors.AppendLine("Debe seleccionar el Tipo de Detalle")
        ElseIf DetailType = 1 Then
            If Me._product Is Nothing Then
                errors.AppendLine("Debe seleccionar un Producto")
            Else
                If Me._product.HandlesBatch Then
                    If Me._listPhysicalInventory Is Nothing OrElse Me._listPhysicalInventory.Count = 0 Then
                        errors.AppendLine("Debe seleccionar al menos un Lote")
                    End If
                End If

                'Únicamente aplica para productos que tengan control de precios
                If Me._product.ProductWithPriceControl = True Then
                    If INDTxtPrice.EditValue > Me._product.SellingPrice Then
                        errors.AppendLine(String.Format("El valor del Precio Unitario no puede ser mayor al valor de Venta del Producto({0})", Me._product.SellingPrice))
                    End If
                End If

                'Para la actividad económica asociada al grupo del producto
                If Not String.IsNullOrEmpty(economicActivityError) Then
                    errors.AppendLine(economicActivityError)
                End If

            End If
        ElseIf DetailType = 2 Then
            If Me.BillingConceptId Is Nothing Then
                errors.AppendLine("Debe seleccionar un Concepto de Facturacion")
            End If

            If _SettingsBilling.AllowsSalesExecutiveAndSupplier Then
                If Supplier Is Nothing Then
                    errors.AppendLine("Debe seleccionar un proovedor")
                End If

                If SalesExecutive Is Nothing Then
                    errors.AppendLine("Debe seleccionar un ejecutivo de ventas")
                End If
            End If

            'Para la actividad económica asociada al concepto de facturación del servicio
            If Not String.IsNullOrEmpty(economicActivityError) Then
                errors.AppendLine(economicActivityError)
            End If

        ElseIf DetailType = 3 Then
            If Me.PhysicalAssetId Is Nothing Then
                errors.AppendLine("Debe seleccionar un Activo Fijo")
            End If
            'Para la actividad económica seleccionada para "Activos Fijos"
            If Not String.IsNullOrEmpty(economicActivityError) Then
                errors.AppendLine(economicActivityError)
            End If
        ElseIf DetailType = 4 Then
            If Me.PhysicalAssetPartId Is Nothing Then
                errors.AppendLine("Debe seleccionar una Parte de un Activo Fijo")
            End If
        End If

        If INDSleFunctionalUnit.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una unidad funcional")
        End If

        If INDLciRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSleRate.EditValue = Nothing Then
                errors.AppendLine("Debe selecionar una tarifa")
            End If
        End If

        If errors.Length = 0 Then
            If INDSeQuantity.EditValue = 0 Then
                errors.AppendLine("Cantidad en cero")
            End If

            If INDTxtPrice.EditValue = 0 Then
                errors.AppendLine("Valor unitario de venta en cero")
            End If
        End If

        Return errors.ToString()
    End Function

    Private Async Function AssignValues() As Task(Of ActionResult)
        With Me._basicBillingDetail

            .DetailType = DetailType
            .FeeId = FeeId
            .FeeName = INDSleRate.Text
            .FunctionalUnitId = Me.FunctionalUnitId
            .FunctionalUnitIdName = INDSleFunctionalUnit.Text
            .CostCenterId = CostCenterId

            If Me.DetailType = 1 Then
                .ProductId = Me._product.Id
                .CodeName = INDPceProduct.Text
                .AlternativeCode = _product.CodeAlternative
                'Si tiene items asociados
                If .BasicBillingDetailItem IsNot Nothing Then
                    'elimino todos los items asociados al detalle
                    While .BasicBillingDetailItem.Count > 0
                        If .BasicBillingDetailItem(0).Id > 0 Then
                            .BasicBillingDetailItem(0).MarkAsDeleted()
                        Else
                            .BasicBillingDetailItem.RemoveAt(0)
                        End If
                    End While
                End If

                'Si el producto maneja lote
                If Me._product.HandlesBatch = True Then
                    If Me._listPhysicalInventory IsNot Nothing Then
                        'Agrego items por cada lote agregado
                        For Each item In Me._listPhysicalInventory
                            Dim basicBillingDetailItem As New BasicBillingDetailItem
                            With basicBillingDetailItem
                                .PhysicalInventoryId = item.Id
                                .Quantity = item.QuantityDeliver
                                .BatchCode = item.CodeNameBatchSerial
                            End With
                            .BasicBillingDetailItem.Add(basicBillingDetailItem)
                        Next
                    End If
                End If

            ElseIf Me.DetailType = 2 Then
                If _ServiceSecondaryId = 0 Then
                    If ServicesProvided IsNot Nothing Then
                        .CodeName = INDgleServicesProvided.Text
                        .ServicesProvidedName = INDSleBillingConcept.Text
                    Else
                        .CodeName = INDSleBillingConcept.Text
                    End If
                    .BillingConceptId = Me.BillingConceptId
                    .ServicesProvidedId = ServicesProvided
                Else
                    Using model As New MBasicBillingDetail(Me.Tag)
                        Me._concept = Await model.GetBillingConceptById(_ServiceSecondaryId)
                        .BillingConceptId = _concept.Id
                        .ServicesProvidedId = Me.BillingConceptId
                        .CodeName = INDSleBillingConcept.Text
                        .ServicesProvidedName = String.Concat(_concept.Code, " - ", _concept.Name)
                    End Using
                    .AlternativeCode = _concept?.AlternativeCode?.ToString()
                End If

                .SupplierId = Supplier
                .SalesExecutiveId = SalesExecutive
                .SupplierName = INDgleSupplier.Text
                .SalesExecutiveName = INDgleSalesExecutive.Text

            ElseIf Me.DetailType = 3 Then
                .PhysicalAssetId = Me.PhysicalAssetId
                .CodeName = INDSlePhysicalAsset.Text
            ElseIf Me.DetailType = 4 Then
                .PhysicalAssetPartId = Me.PhysicalAssetPartId
                .CodeName = INDSlePhysicalAssetPart.Text
            End If

            .Quantity = INDSeQuantity.EditValue
            .Price = INDTxtPrice.EditValue
            .Value = Me.Value
            .PercentageDiscount = INDSePercentageDiscount.EditValue
            .PercentageIVA = INDSePercentageIVA.EditValue
            .RetentionIdTax = Me.RetentionIdTax
            .RetentionPercentageTax = Me.RetentionPercentageTax
            .RetentionBaseTax = Me.RetentionBaseTax
            .RetentionIdICA = Me.RetentionIdICA
            .WarehouseId = Me.WareHouseId
            .WarehouseName = INDSleWarehouse.Text

            If .RetentionIdICA IsNot Nothing Then
                Using model As New MBasicBillingDetail(Me.Tag)
                    Dim retentionConceptByCity = model.GetRateRetentionByIdAndAddressId(.RetentionIdICA, Me._addressId)
                    If retentionConceptByCity IsNot Nothing Then
                        .RetentionPercentageICA = retentionConceptByCity.Rate
                    End If
                End Using
            End If
            .RetentionBaseICA = Me.RetentionBaseICA
            .EconomicActivityId = EconomicActivityId
        End With

        Return New ActionResult With {.StateResult = True}
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

        Me.changeNumericFormatByCurrency(_currencyAbbreviation.GetNumberFormat)
    End Sub
#End Region

End Class