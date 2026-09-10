Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports System.Drawing
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Contract
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Billing.MVP
Imports Presentation.Common.MVP
Imports System.Text
Imports Presentation.Payroll
Imports Domain.Base.Entities
Imports Presentation.Common
Imports Presentation.Contract.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Inventory

Public Class FrmPharmaceuticalDispensingDetail
    Implements IPharmaceuticalDispensingDetail

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmPharmaceuticalDispensingDetail"/> class.
    ''' </summary>
    Public Sub New()
        ctrSubTotal = New CtrCostProduct()
        ctrSubTotal.SetFunctionDelegate(AddressOf GetSubTotalValue)
        ctrSubTotal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdditionalControlPanel.Controls.Add(ctrSubTotal)
        InitializeComponent()
    End Sub
#End Region

#Region "Properties and Variables"

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de productos
    ''' </summary>
    Dim _requestQuoteProducts As Boolean

    ''' <summary>
    ''' modo editar
    ''' </summary>
    Dim _editMode As Boolean

    ''' <summary>
    '''  control que se muestra en la barra botones
    ''' </summary>
    Dim ctrSubTotal As CtrCostProduct

    ''' <summary>
    ''' Permite saber si se calcula el valor, se realiza esto para que al momento de cargar los controles no me vuelva a calcular sino que obtenga de lo ya guardado
    ''' </summary>
    Private calculateSalePrice As Boolean = True

    ''' <summary>
    ''' Permite saber si hay error de producto no encontrado en el almacen
    ''' </summary>
    Private messageErrorQuotation As String

    ''' <summary>
    ''' Gets or sets the list pharmaceutical dispensing batch serial.
    ''' </summary>
    Private Property ListPharmaceuticalDispensingBatchSerial As List(Of PharmaceuticalDispensingDetailBatchSerial)

    ''' <summary>
    ''' Tercero del paciente en la admisión
    ''' </summary>
    Property ThirdPartyPatientId As Integer

    ''' <summary>
    ''' genero del paciente en la admision
    ''' </summary>
    ''' <returns></returns>
    Property GenderThirdParty As Byte

    ''' <summary>
    ''' Almacena el centro de atencion de la unidad funcional
    ''' </summary>
    Private _functionalUnitCenterAttentionCode As String

    ''' <summary>
    ''' fecha de nacimiento del paciente de la admision
    ''' </summary>
    ''' <returns></returns>
    Property PatientDate As DateTime
    ''' <summary>
    ''' Gets or sets the full name third party patient.
    ''' </summary>
    ''' <value>
    ''' The full name third party patient.
    ''' </value>
    Property FullNameThirdPartyPatient As String

    ''' <summary>
    ''' Lotes agregados
    ''' </summary>
    Private listPhysicalInventory As List(Of PhysicalInventory)

    ''' <summary>
    ''' Lotes agregados
    ''' </summary>
    Private listPhysicalInventoryCustody As List(Of PhysicalInventoryCustody)

    ''' <summary>
    ''' listado de tipos de liquidacion
    ''' </summary>
    Private ListLiquidationType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Inventory"

    ''' <summary>
    ''' Gets or sets the admission number header.
    ''' </summary>
    Public Property AdmissionNumberHeader As String

    ''' <summary>
    ''' 1 - Afecta inventario; 2 - no afecta inventario
    ''' </summary>
    Public Property AffectedInventory As Boolean

    ''' <summary>
    ''' Gets or sets the pharmaceutical dispensing detail.
    ''' </summary>
    Public Property PharmaceuticalDispensingDetail As PharmaceuticalDispensingDetail

    ''' <summary>
    ''' Propiedad que guarda o almacena el ProductServiceDetail 
    ''' </summary>
    Private ListProductServiceDetail As List(Of ProductServiceDetail)

    ''' <summary>
    ''' Precio de venta
    ''' </summary>
    ''' <returns></returns>
    Private Property _salePrice As Decimal

    ''' <summary>
    ''' Id del lote/serial
    ''' </summary>
    Public Property BatchSerialId As Integer Implements IPharmaceuticalDispensingDetail.BatchSerialId

    ''' <summary>
    ''' Id del tercero relacionado al médico
    ''' </summary>
    Public Property OrderedHealthProfessionalThirdPartyId As Integer


    ''' <summary>
    ''' Producto
    ''' </summary>
    Public Property ProductId As Integer Implements IPharmaceuticalDispensingDetail.ProductId

    ''' <summary>
    ''' Gets or sets the quantity batch serial.
    ''' </summary>
    Public Property QuantityBatchSerial As Decimal Implements IPharmaceuticalDispensingDetail.QuantityBatchSerial

    ''' <summary>
    ''' fecha del ingreso
    ''' </summary>
    Private _admissionDate As DateTime

    ''' <summary>
    ''' The _sub total sales
    ''' </summary>
    Private _grandTotalSalesPrice As Decimal

    ''' <summary>
    ''' Especifica el subtotal del valor de venta, el cual es el valor de venta unitario por la cantidad a dispensar
    ''' </summary>
    Public Property TotalSalesPrice As Decimal Implements IPharmaceuticalDispensingDetail.SubTotalSales

    ''' <summary>
    ''' Detalle de la dispensacion farmaceutica, en esta tabla de especifica las cantidades que salieron de cada lote
    ''' </summary>
    Public Property ListPharmaceuticalDispensingDetailBatchSerial As List(Of Domain.Entities.PharmaceuticalDispensingDetailBatchSerial) Implements IPharmaceuticalDispensingDetail.ListPharmaceuticalDispensingDetailBatchSerial

    ''' <summary>
    ''' bandera de estado del popup de caregroup
    ''' </summary>
    Private _flagPopUpCareGroup As Boolean

    ''' <summary>
    ''' bandera de estado del popup de productos
    ''' </summary>
    Private _flagPopUpProduct As Boolean

    ''' <summary>
    ''' Permite saber si el almacen es virtual o no
    ''' </summary>
    Private VirtualStore As Boolean = False

    ''' <summary>
    ''' bandera de estado del popup de almacenes
    ''' </summary>
    Private _flagPopUpWareHouse As Boolean

    ''' <summary>
    ''' bandera de estado del popup de Medicos
    ''' </summary>
    Private _flagPopUpHealtProfessional As Boolean

    ''' <summary>
    ''' The _flag pop up functional unit
    ''' </summary>
    Private _flagPopUpFunctionalUnit As Boolean

    ''' <summary>
    ''' bandera de estado del popup de Especialidades
    ''' </summary>
    Private _flagPopUpHealthProfessionalSpecialty As Boolean

    ''' <summary>
    ''' bandera de estado del popup de Cups Entity
    ''' </summary>
    Private _flagPopUpCupsEntity As Boolean

    ''' <summary>
    ''' presentador
    ''' </summary>
    Private _presenter As PPharmaceuticalDispensingDetail

    ''' <summary>
    ''' Permite saber si el almacen es virtual o no
    ''' </summary>
    Private Custody As Boolean = False

    ''' <summary>
    ''' numero de indentificacion del paciente
    ''' </summary>
    Private _patientCode As String

    ''' <summary>
    ''' valor del impuesto del producto
    ''' </summary>
    Private _taxValue As Decimal

    ''' <summary>
    ''' valor bruto valor antes del precio de venta- sin incluir impuestos
    ''' </summary>
    Private _grossValue As Decimal

    ''' <summary>
    ''' Bandera que establece si el valor del impuesto es incluido o No
    ''' </summary>
    Private _flagTaxInclude As Boolean

    ''' <summary>
    ''' valor porcentaje del impuesto a aplicar
    ''' </summary>
    Private _taxPercent As Decimal


    ''' <summary>
    ''' Tipo de redondeo de la moneda parametrizada
    ''' </summary>
    Public _roundingType As Integer = 0

    ''' <summary>
    ''' Variable que me indica que el registro que se esta editando viene desde cotización
    ''' </summary>
    Private QuotationPharmaceuticalDispensingDetailId As Integer? = Nothing
    Public WriteOnly Property RequestQuoteProducts As Boolean
        Set(value As Boolean)
            _requestQuoteProducts = value
        End Set
    End Property

    ''' <summary>
    ''' establece si es un modo de edicion del detalle
    ''' </summary>
    WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece el numero de autorizacion
    ''' </summary>
    Public WriteOnly Property AutorizationNumber As String
        Set(value As String)
            INDtxtAuthorizationNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' estado para saber si se afecta inventario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _statusInventory As Boolean
    Public WriteOnly Property StatusInventory As Boolean
        Set(value As Boolean)
            _statusInventory = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el nullText del grupo de atención
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CareGroupCodeName As String
        Set(value As String)
            INDsleCareGroup.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the third party identifier.
    ''' </summary>
    ''' <value>
    ''' The third party identifier.
    ''' </value>
    Property ThirdPartyId As Integer
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the health administrator identifier.
    ''' </summary>
    ''' <value>
    ''' The health administrator identifier.
    ''' </value>
    Property HealthAdministratorId As Integer?
        Get
            Return INDsleHealthAdministrator.EditValue
        End Get
        Set(value As Integer?)
            INDsleHealthAdministrator.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Sets the health administrator datasource.
    ''' </summary>
    ''' <value>
    ''' The health administrator datasource.
    ''' </value>
    WriteOnly Property HealthAdministratorDatasource As XPInstantFeedbackSource
        Set(value As XPInstantFeedbackSource)
            INDsleHealthAdministrator.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Número de autorizacion
    ''' </summary>
    Public Property AuthorizationNumber As String Implements IPharmaceuticalDispensingDetail.AuthorizationNumber
        Get
            Return INDtxtAuthorizationNumber.EditValue
        End Get
        Set(value As String)
            INDtxtAuthorizationNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el Costo promedio del producto, Valor Unitario
    ''' </summary>
    Public Property AverageCost As Decimal Implements IPharmaceuticalDispensingDetail.AverageCost
        Get
            Return CType(INDtxtAverageCost.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDtxtAverageCost.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el Costo promedio del producto, Valor Unitario
    ''' </summary>
    Public Property FinalProductCost As Decimal Implements IPharmaceuticalDispensingDetail.FinalProductCost
        Get
            Return CType(INDtxtFinalProductCost.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDtxtFinalProductCost.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Grupo de Atención
    ''' </summary>
    Public Property CareGroupId As Integer Implements IPharmaceuticalDispensingDetail.CareGroupId
        Get
            Return CType(INDsleCareGroup.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleCareGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el id del cups que se debe encontrar en la orden de servicio, Este campo se solicita solo si el tipo de liquidacion es 2, es decir que esta incluido al 100% dentro de un servicio
    ''' </summary>
    Public Property CupsEntityId As Integer? Implements IPharmaceuticalDispensingDetail.CupsEntityId
        Get
            Return INDsleCupsEntity.EditValue
        End Get
        Set(value As Integer?)
            INDsleCupsEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Porcentaje de descuento que se le va aplicar a al subtotal
    ''' </summary>
    Public Property DiscountPercentage As Decimal Implements IPharmaceuticalDispensingDetail.DiscountPercentage
        Get
            Return CType(INDspnDiscountPercentage.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDspnDiscountPercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor del descuento, se obtiene multiplicando el SubTotalSales por el porcentaje del descuento
    ''' </summary>
    Public Property DiscountValue As Decimal Implements IPharmaceuticalDispensingDetail.DiscountValue
        Get
            Return CType(INDtxtDiscountValue.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDtxtDiscountValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad funcional
    ''' </summary>
    Public Property FunctionalUnitId As Integer Implements IPharmaceuticalDispensingDetail.FunctionalUnitId
        Get
            Return CType(INDsleFunctionalUnit.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleFunctionalUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el tipo de liquidaicon: 1 - Manual Tarifario; 2 - Incluido al 100% dentro de un servicio IPS
    ''' </summary>
    Public Property LiquidationType As Byte Implements IPharmaceuticalDispensingDetail.LiquidationType
        Get
            Return CType(INDgleLiquidationType.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDgleLiquidationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el Codigo del Profesional de la Salud que ordeno el Producto. Estos datos se sacan de la tabla de Crystal INPROFSAL
    ''' </summary>
    Public Property OrderedHealthProfessionalCode As String Implements IPharmaceuticalDispensingDetail.OrderedHealthProfessionalCode
        Get
            Return INDsleHealthProfessionalCode.EditValue
        End Get
        Set(value As String)
            INDsleHealthProfessionalCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica la especialidad el profesional que ordeno
    ''' </summary>
    Public Property OrderedProfessionalSpecialty As String Implements IPharmaceuticalDispensingDetail.OrderedProfessionalSpecialty
        Get
            Return INDsleHealthProfessionalSpecialty.EditValue
        End Get
        Set(value As String)
            INDsleHealthProfessionalSpecialty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' cantidad a dispensar
    ''' </summary>
    Public Property Quantity As Integer Implements IPharmaceuticalDispensingDetail.Quantity
        Get
            Return CType(INDspnQuantity.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDspnQuantity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el precio de venta del producto, Valor Unitario
    ''' </summary>
    Public Property SalePrice As Decimal Implements IPharmaceuticalDispensingDetail.SalePrice
        Get
            Return _salePrice
        End Get
        Set(value As Decimal)
            _salePrice = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha en que se dispensó el medicamento
    ''' </summary>
    Public Property ServiceDate As DateTime Implements IPharmaceuticalDispensingDetail.ServiceDate
        Get
            Return CType(INDdeServiceDate.EditValue, DateTime)
        End Get
        Set(value As DateTime)
            INDdeServiceDate.EditValue = value
            INDdeServiceDate.Properties.MinValue = _admissionDate
            INDdeServiceDate.Properties.MaxValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha en que se dispensó el medicamento
    ''' </summary>
    Public WriteOnly Property AdmissionDate As DateTime
        Set(value As DateTime)
            _admissionDate = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el subtotal del valor de venta, el cual es el valor de venta unitario por la cantidad a dispensar
    ''' </summary>
    Public Property GrandTotalSalesPrice As Decimal
        Get
            Return _grandTotalSalesPrice
        End Get
        Set(value As Decimal)
            _grandTotalSalesPrice = value
            ctrSubTotal.PrintValue()
        End Set
    End Property

    ''' <summary>
    ''' Especifica si se le aplico recargo al calculo del precio de venta
    ''' </summary>
    Public Property SurchargeApply As Boolean Implements IPharmaceuticalDispensingDetail.SurchargeApply
        Get
            Return CType(INDgleSurchargeApply.EditValue, Boolean)
        End Get
        Set(value As Boolean)
            INDgleSurchargeApply.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Almacén
    ''' </summary>
    Public Property WarehouseId As Integer Implements IPharmaceuticalDispensingDetail.WarehouseId
        Get
            Return CType(INDsleWareHouse.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleWareHouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de Grupos de Atensión
    ''' </summary>
    Public Property CareGroupDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IPharmaceuticalDispensingDetail.CareGroupDatasource
        Get
            Return CType(INDsleCareGroup.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCareGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de Almacenes
    ''' </summary>
    Public Property WareHouseDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IPharmaceuticalDispensingDetail.WareHouseDatasource
        Get
            Return CType(INDsleWareHouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleWareHouse.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de Productos
    ''' </summary>
    Public Property ProductDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IPharmaceuticalDispensingDetail.ProductDatasource
        Get
            'Return CType(INDsleProduct.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            'INDsleProduct.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de Medicos
    ''' </summary>
    Public Property HealthProfessionalDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IPharmaceuticalDispensingDetail.HealthProfessionalDatasource
        Get
            Return CType(INDsleHealthProfessionalCode.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleHealthProfessionalCode.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de unidades funcionales
    ''' </summary>
    Public Property FunctionalUnitDatasource As XPInstantFeedbackSource Implements IPharmaceuticalDispensingDetail.FunctionalUnitDatasource
        Get
            Return CType(INDsleFunctionalUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de Cups
    ''' </summary>
    Public Property CupsDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IPharmaceuticalDispensingDetail.CupsDatasource
        Get
            Return CType(INDsleCupsEntity.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCupsEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' establece el # de identificacion del paciente
    ''' </summary>
    Public WriteOnly Property PatientCode() As String Implements IPharmaceuticalDispensingDetail.PatientCode
        Set(ByVal value As String)
            _patientCode = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establce el valor del impuesto del producto
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxValue As Decimal Implements IPharmaceuticalDispensingDetail.TaxValue
        Get
            Return Me._taxValue
        End Get
        Set(value As Decimal)
            Me._taxValue = value
            Me.INDtxtTaxValue.EditValue = value
            Me.INDtxtTaxValue1.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establce el subtotal del valor del producto (antes de impuestos)
    ''' </summary>
    ''' <returns></returns>
    Public Property GrossValue As Decimal Implements IPharmaceuticalDispensingDetail.GrossValue
        Get
            Return Me._grossValue
        End Get
        Set(value As Decimal)
            Me._grossValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece si el impuesto esta incluido en el precio de venta o no
    ''' </summary>
    Public WriteOnly Property FlagTaxInclude As Boolean
        Set(value As Boolean)
            Me._flagTaxInclude = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece el valor de los controles
    ''' </summary>
    Private WriteOnly Property SetValuesTaxControls As Boolean
        Set(value As Boolean)
            If value Then
                Me.INDtxtTotalValue.EditValue = 0
                Me.INDtxtSubTotal.EditValue = Me.GrossValue
                Me.INDtxtSalePrice.EditValue = Me.SalePrice
            Else
                Me.INDtxtSubTotal.EditValue = 0
                Me.INDtxtSalePrice.EditValue = Me.GrossValue
                Me.INDtxtTotalValue.EditValue = Me.SalePrice
            End If
        End Set
    End Property


#End Region

#Region "Events"

    ''' <summary>
    ''' Occurs when [add pharmaceutical dispensing detail].
    ''' </summary>
    Public Event AddPharmaceuticalDispensingDetail(sender As Object, e As AddPharmaceuticalDispensingDetailEventArgs)


#Region "EditValueChanged"

    Private Sub INDtxtSubTotal_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtSubTotal.EditValueChanged
        INDtxtSubTotal.EditValue = RoundValue(INDtxtSubTotal.EditValue, _roundingType)
    End Sub

    Private Sub INDtxtTaxValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtTaxValue.EditValueChanged
        INDtxtTaxValue.EditValue = RoundValue(INDtxtTaxValue.EditValue, _roundingType)
    End Sub

    Private Sub INDtxtFinalProductCost_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtFinalProductCost.EditValueChanged
        INDtxtFinalProductCost.EditValue = RoundValue(INDtxtFinalProductCost.EditValue, _roundingType)
    End Sub

    Private Sub INDtxtAverageCost_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtAverageCost.EditValueChanged
        INDtxtAverageCost.EditValue = RoundValue(INDtxtAverageCost.EditValue, _roundingType)
    End Sub

    Private Sub INDtxtTotalValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtTotalValue.EditValueChanged
        INDtxtTotalValue.EditValue = RoundValue(INDtxtTotalValue.EditValue, _roundingType)
    End Sub

    Private Sub INDtxtTaxValue1_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtTaxValue1.EditValueChanged
        INDtxtTaxValue1.EditValue = RoundValue(INDtxtTaxValue1.EditValue, _roundingType)
    End Sub

#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _editMode = Nothing
        StatusInventory = Nothing
        ctrSubTotal = Nothing
        ListPharmaceuticalDispensingBatchSerial = Nothing
        ThirdPartyPatientId = Nothing
        GenderThirdParty = Nothing
        _functionalUnitCenterAttentionCode = Nothing
        PatientDate = Nothing
        FullNameThirdPartyPatient = Nothing
        listPhysicalInventory = Nothing
        ListLiquidationType = Nothing
        AdmissionNumberHeader = Nothing
        AffectedInventory = Nothing
        PharmaceuticalDispensingDetail = Nothing
        ProductId = Nothing
        QuantityBatchSerial = Nothing
        _admissionDate = Nothing
        _grandTotalSalesPrice = Nothing
        ListPharmaceuticalDispensingDetailBatchSerial = Nothing
        _flagPopUpCareGroup = Nothing
        _flagPopUpProduct = Nothing
        VirtualStore = Nothing
        _flagPopUpCupsEntity = Nothing
        _flagPopUpFunctionalUnit = Nothing
        _flagPopUpWareHouse = Nothing
        _flagPopUpHealthProfessionalSpecialty = Nothing
        _flagPopUpHealtProfessional = Nothing
        _presenter = Nothing
        listPhysicalInventoryCustody = Nothing
        Custody = Nothing
        ListProductServiceDetail = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmAddProduct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmAddProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        _presenter = New PPharmaceuticalDispensingDetail(Me)
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        Me.BarraBotones.StatusRecordVisible = True
        CreateLiquidationType()
        LoadLastWareHouse()
        CleanFlags()
        LoadStatus()
        If Not Me._editMode Then
            SetTaxIncludeOrExcludeControls(Me._flagTaxInclude)
        End If
        BarraBotones.StatusRecord = _statusInventory
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(indigo.CurrencyISO4217.GetCultureId()).NumberFormat
        SetFormatCurrency(_culture.NumberFormat)
    End Sub



    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls(False)
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmPharmaceuticalDispensingDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPharmaceuticalDispensingDetail_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If product IsNot Nothing AndAlso Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            e.Cancel = True
        End If

    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDpceProduct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDpceProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenForms(form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCareGroup control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCareGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("985", Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCupsEntity control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCupsEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCupsEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Form As New FrmCupsEntity()
                OpenForms(Form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleWareHouse control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleWareHouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleWareHouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Form As New FrmStores
                OpenForms(Form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleFunctionalUnit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFunctionalUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFunctionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Form As New FrmFunctionalUnit()
                OpenForms(Form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleHealthAdministrator control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleHealthAdministrator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleHealthAdministrator.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Form As New FrmHealthAdministrator()
                OpenForms(Form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleThirdParty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Form As New FrmThirdParty()
                OpenForms(Form)
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDgleLiquidationType control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgleLiquidationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleLiquidationType.EditValueChanged
        If LiquidationType = 2 Then
            INDliCupsEntity.HideControl(False)
        Else
            INDliCupsEntity.HideControl(True)
            CupsEntityId = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleCareGroup control.
    ''' </summary>
    Private Async Sub INDsleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareGroup.EditValueChanged, INDdeServiceDate.EditValueChanged, INDgleSurchargeApply.EditValueChanged, INDsleHealthProfessionalCode.EditValueChanged
        Await LoadSalePrice()
    End Sub

    Private Async Sub INDsleFunctionalUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFunctionalUnit.EditValueChanged
        If INDsleFunctionalUnit.EditValue IsNot Nothing Then
            Using modelServiceOrder As New MServiceOrder(Me.Tag)
                Dim functionalUnitTemp = modelServiceOrder.GetFunctionalUnitById(INDsleFunctionalUnit.EditValue)
                _functionalUnitCenterAttentionCode = functionalUnitTemp.BranchOfficeId.Code
            End Using
        End If
        Await LoadSalePrice()
    End Sub

    ''' <summary>
    ''' Evento que selecciona el producto en el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProduct.EditValueChanged
        'Separo el string escrito en el control de producto
        If (Not Me.LoadDoc) AndAlso INDSleProduct.EditValue IsNot Nothing AndAlso Me.Custody Then
            Dim arrayCodeProduct As String() = INDSleProduct.Text.Split(" - ")
            'Capturo el codigo del producto
            Dim codeProduct As String = arrayCodeProduct(0).Trim

            Using model As New MInventoryProduct(Me.Tag)
                Dim resultProduct = Await model.GetInventoryProduct(codeProduct)

                If resultProduct Is Nothing OrElse Not resultProduct?.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = If(resultProduct?.MessageResult?.Any(), resultProduct?.MessageResult?.FirstOrDefault, "La consulta no trajo ningún resultado")
                    CleanControls(False)
                    Exit Sub
                End If
                'Valido que exista el producto
                If resultProduct.ObjectEmbbeded IsNot Nothing AndAlso resultProduct.ObjectEmbbeded.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto con código {0} no existe.", codeProduct)
                    CleanControls(False)
                    Exit Sub
                End If
                'Valido que el producto no este inactivo
                If resultProduct.ObjectEmbbeded.Status = False Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name + " está inactivo")
                    CleanControls(False)
                    Exit Sub
                End If
                'Instancio el objeto SelectProductEventArgs
                Dim obj As New SelectProductEventArgs
                obj.ProductId = resultProduct.ObjectEmbbeded.Id
                obj.CodeNameProduct = resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name
                'Invoco la funcion que realiza el resto de la logica
                CtrProducts1_SelectProduct(Nothing, obj)
            End Using
        End If
    End Sub


    Dim _healthAdministratorCrystal As Integer
    WriteOnly Property HealthAdministratorCrystal As Integer
        Set(value As Integer)
            _healthAdministratorCrystal = value
        End Set
    End Property
    ''' <summary>
    ''' Handles the EditValueChanging event of the INDsleCareGroup control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCareGroup_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleCareGroup.EditValueChanging
        If e.NewValue <> 0 Then
            Using model As New MCareGroup(Me.Tag)
                Dim res = model.GetCareGroupByIdSimple(e.NewValue)
                If res.StateResult Then
                    INDliHealthAdministrator.HideControl()
                    INDliThirdParty.HideControl()
                    Me.HealthAdministratorId = Nothing
                    Me.ThirdPartyId = Nothing
                    INDsleThirdParty.Properties.NullText = String.Empty
                    Select Case res.ObjectEmbbeded.CareGroupType
                        Case 1 'EAPB con contrato
                            Using modelc As New MContract(Me.Tag)
                                Dim contract As ActionResult(Of Domain.Entities.Contract) = modelc.GetContractByIdWithAggregates(res.ObjectEmbbeded.ContractId)
                                If contract.StateResult AndAlso contract.ObjectEmbbeded IsNot Nothing AndAlso contract.ObjectEmbbeded.Id > 0 Then
                                    Me.HealthAdministratorId = contract.ObjectEmbbeded.HealthAdministratorId
                                    Me.ThirdPartyId = contract.ObjectEmbbeded.HealthAdministrator.ThirdPartyId
                                Else
                                    e.Cancel = True
                                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró un contrato asociado al grupo de atención seleccionado"
                                End If
                            End Using
                        Case 2, 4 'EAPB sin contrato
                            INDliHealthAdministrator.HideControl(False)
                            Using modelHealthAdministrator As New MHealthAdministrator(Me.Tag)
                                Dim healthAdministratorTmp As HealthAdministrator = Nothing
                                If _healthAdministratorCrystal > 0 Then
                                    healthAdministratorTmp = modelHealthAdministrator.GetHealthAdministratorByIdSimple(_healthAdministratorCrystal).ObjectEmbbeded
                                End If
                                If healthAdministratorTmp IsNot Nothing AndAlso healthAdministratorTmp.Id > 0 Then
                                    INDsleHealthAdministrator.Properties.ReadOnly = False
                                    INDsleHealthAdministrator.EditValue = healthAdministratorTmp.Id
                                    INDsleHealthAdministrator.Properties.NullText = healthAdministratorTmp.Code + " - " + healthAdministratorTmp.Name
                                Else
                                    INDsleHealthAdministrator.Properties.ReadOnly = False
                                    INDsleHealthAdministrator.EditValue = Nothing
                                    INDsleHealthAdministrator.Properties.NullText = Nothing
                                End If
                            End Using

                            Using modelCtr As New MCtrFolio()
                                HealthAdministratorDatasource = modelCtr.ListHealthAdministrator(res.ObjectEmbbeded.EntityType)
                            End Using
                        Case 3 'Particulares
                            INDliThirdParty.HideControl(False)
                            INDsleThirdParty.Properties.NullText = FullNameThirdPartyPatient
                            ThirdPartyId = ThirdPartyPatientId

                    End Select
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleHealthAdministrator control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleHealthAdministrator_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHealthAdministrator.EditValueChanged
        If INDsleHealthAdministrator.EditValue IsNot Nothing Then
            Using model As New MHealthAdministrator(Me.Tag)
                Dim healthAdministrator As ActionResult(Of HealthAdministrator) = Await model.GetHealthAdministratorById(CType(INDsleHealthAdministrator.EditValue, Integer))
                If healthAdministrator.StateResult AndAlso healthAdministrator.ObjectEmbbeded IsNot Nothing AndAlso healthAdministrator.ObjectEmbbeded.Id > 0 Then
                    Me.ThirdPartyId = healthAdministrator.ObjectEmbbeded.ThirdPartyId
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró la entidad administradora"
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDspnQuantity control.
    ''' </summary>
    Private Sub INDspnQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDspnQuantity.EditValueChanged, INDtxtSalePrice.EditValueChanged
        INDtxtSalePrice.EditValue = RoundValue(INDtxtSalePrice.EditValue, _roundingType)
        LoadGrandTotalValue()
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDspnDiscountPercentage control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDspnDiscountPercentage_EditValueChanged(sender As Object, e As EventArgs) Handles INDspnDiscountPercentage.EditValueChanged
        DiscountValue = RoundValue(InventoryStaticServices.CalculateDiscountValue(SalePrice, DiscountPercentage), _roundingType)
        LoadGrandTotalValue()
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDtxtDiscountValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtDiscountValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtDiscountValue.EditValueChanged
        DiscountPercentage = InventoryStaticServices.CalculateDiscountPercentage(SalePrice, DiscountValue)
        LoadGrandTotalValue()
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleWareHouse control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleWareHouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleWareHouse.EditValueChanged
        Dim processQuotation As Boolean = False

        'Se obtiene el almacen por id para poder asignar si el almacen es virtual o no
        If INDsleWareHouse.EditValue IsNot Nothing Then
            'Se consulta el almacen por id
            Dim info = _presenter.GetWareHouseById(INDsleWareHouse.EditValue)
            If info IsNot Nothing Then
                'Se asigna el valor a la bandera para saber si es virtual o no
                VirtualStore = info.VirtualStore
                Me.Custody = info.CustodyStore
            End If

            'Si el registro se cargo por medio de una cotización, al cambiar el almacén se valida que el producto exista dentro del almacen seleccionado
            If QuotationPharmaceuticalDispensingDetailId IsNot Nothing AndAlso QuotationPharmaceuticalDispensingDetailId > 0 Then
                Dim inventoryProductXpo As InventoryRepository.InventoryProductXpo = Nothing

                If AffectedInventory Then
                    inventoryProductXpo = _presenter.GetInventoryProductXpoAffectInventory(INDsleWareHouse.EditValue, product.Code, VirtualStore)
                Else
                    inventoryProductXpo = _presenter.GetInventoryProductXpoNotAffectInventory(INDsleWareHouse.EditValue, product.Code, VirtualStore)
                End If

                'Si esta entidad xpo es nula se emite un mensaje de validación
                If inventoryProductXpo Is Nothing Then
                    messageErrorQuotation = "El producto " + INDpceProduct.Text + " no existe en el almacén seleccionado"
                    Mensaje(EeventViewerImages.Advertencia) = messageErrorQuotation
                    Exit Sub
                End If

                processQuotation = True
                messageErrorQuotation = String.Empty

                If Not VirtualStore AndAlso inventoryProductXpo.ProductSubGroupId.HandlesBatch = True Then
                    INDspnQuantity.Properties.ReadOnly = True
                    INDliBatchSerial.HideControl(False)
                    INDPceQuantity.Focus()
                    INDPceQuantity.ShowPopup()
                Else
                    INDliBatchSerial.HideControl(True)
                End If
            End If

        End If

        HideProduct()

        If processQuotation = False Then
            CtrPhysicalInventory1.CleanControls()
            INDpceProduct.EditValue = Nothing
            INDpceProduct.Properties.NullText = String.Empty
            INDspnQuantity.EditValue = 1
            INDPceQuantity.EditValue = Nothing
            INDSleProduct.EditValue = Nothing
            LoadSalePrice()
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub HideProduct()
        If Me.Custody Then
            INDliProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDliProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCareGroup control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCareGroup.QueryPopUp
        If Not _flagPopUpCareGroup Then
            _presenter.LoadCareGroup()
            _flagPopUpCareGroup = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleFunctionalUnit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleFunctionalUnit.QueryPopUp
        If Not _flagPopUpFunctionalUnit Then
            _presenter.LoadFunctionalUnit()
            _flagPopUpFunctionalUnit = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleWareHouse control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleWareHouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleWareHouse.QueryPopUp
        If Not _flagPopUpWareHouse Then
            _presenter.LoadWareHouse()
            _flagPopUpWareHouse = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleHealthProfessionalCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleHealthProfessionalCode_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleHealthProfessionalCode.QueryPopUp
        If Not _flagPopUpHealtProfessional Then
            _presenter.LoadHealthProfessional()
            _flagPopUpHealtProfessional = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleHealthProfessionalSpecialty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleHealthProfessionalSpecialty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleHealthProfessionalSpecialty.QueryPopUp
        If Not _flagPopUpHealthProfessionalSpecialty Then

            _flagPopUpHealthProfessionalSpecialty = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCupsEntity control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCupsEntity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCupsEntity.QueryPopUp
        If Not _flagPopUpCupsEntity Then
            _presenter.LoadCupsEntity(AdmissionNumberHeader, 1)
            _flagPopUpCupsEntity = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDPceBatchSerial control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceQuantity.QueryPopUp
        CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
        CtrPhysicalInventory1.Product = product
        CtrPhysicalInventory1.WareHouseId = WarehouseId
        CtrPhysicalInventory1.FormOwner = Me
        CtrPhysicalInventory1.PatientCode = Me._patientCode
        CtrPhysicalInventory1.AdmissionNumber = Me.AdmissionNumberHeader
        CtrPhysicalInventory1.Custody = Me.Custody
        CtrPhysicalInventory1.SetListPhysicalInventory()
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDpceProduct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceProduct.QueryPopUp
        If INDsleWareHouse.EditValue IsNot Nothing Then
            CtrProducts1.SetDataSourceProductNoAffectedInventory(INDsleWareHouse.EditValue, VirtualStore)
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Debe selecionar un almacen"
            INDsleWareHouse.Focus()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProduct_QueryPopUP(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProduct.QueryPopUp
        If INDSleProduct.Properties.DataSource Is Nothing Then
            Using model As New MDashBoardPharmacy(Me.Tag)
                INDSleProduct.Properties.DataSource = model.GetPhysicalInventoryCustodyByAdmissionWareHouse(Me._patientCode, Me.AdmissionNumberHeader, Me.WarehouseId)
            End Using
        End If
    End Sub
#End Region

#Region "GotFocus"
    ''' <summary>
    ''' Handles the GotFocus event of the INDPceBatchSerial control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDPceBatchSerial_GotFocus(sender As Object, e As EventArgs) Handles INDPceQuantity.GotFocus
    End Sub
#End Region

#Region "Actived"
    ''' <summary>
    ''' Handles the Activated event of the FrmPharmaceuticalDispensingDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmPharmaceuticalDispensingDetail_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDsleCareGroup.Focus()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the FrmPharmaceuticalDispensingDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPharmaceuticalDispensingDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDsleHealthProfessionalCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleHealthProfessionalCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDsleHealthProfessionalCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDsleHealthProfessionalSpecialty.Properties.DataSource IsNot Nothing AndAlso CType(INDsleHealthProfessionalSpecialty.Properties.DataSource, IEnumerable(Of Object)).Count > 1 Then
                INDsleHealthProfessionalSpecialty.Focus()
            Else
                INDtxtAuthorizationNumber.Focus()
            End If
        End If
    End Sub

    Private Async Sub INDpceProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceProduct.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        'Si el registro fue cargado desde una cotización y al cambiar el almacen genera error no deja continuar
        If QuotationPharmaceuticalDispensingDetailId IsNot Nothing AndAlso QuotationPharmaceuticalDispensingDetailId > 0 AndAlso Not String.IsNullOrEmpty(messageErrorQuotation) Then
            Mensaje(EeventViewerImages.Advertencia) = messageErrorQuotation
            Exit Sub
        End If

        If ValidateControls() = False Then
            Exit Sub
        End If

        'Variable que permite saber si se va a importar un detalle de cotización
        Dim processImportQuotation As Boolean = False

        'Se valida si los servicios necesitan cotización, siempre y cuando el registro no venga imoprtada desde una cotización
        If QuotationPharmaceuticalDispensingDetailId Is Nothing Then
            Dim listTuple As New List(Of Tuple(Of Integer, Integer, Date))
            listTuple.Add(New Tuple(Of Integer, Integer, Date)(ProductId, CareGroupId, ServiceDate))
            Dim messageQuoted = _presenter.GetProductsWithQuoted(listTuple)
            If Not String.IsNullOrEmpty(messageQuoted) Then
                If MessageIndigo.Show(messageQuoted, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    If Me._requestQuoteProducts Then
                        Exit Sub
                    End If
                Else
                    processImportQuotation = True
                End If
            End If
        End If

        If processImportQuotation Then 'Si se ejecuta el proceso de importación de cotización
            QuotationProcess()
        Else 'Si se ejecuta el proceso normal de agregar o editar el detalle de la dispensación
            NormalProcess()
        End If
    End Sub

    Private Sub QuotationProcess()
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmImportQuotationPharmaceutical()
            AddHandler Formulario.ImportEvent, AddressOf ImportInfo
            Formulario.PatientCode = _patientCode
            Formulario.ListProductId = {ProductId}.ToList()
            Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 1054
            Formulario.Height = 500
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ImportInfo(e As AddImportQuotationPharmaceuticalDispensingDetail)
        If e IsNot Nothing AndAlso e.ListQuotationPharmaceuticalDispensingDetailXpo IsNot Nothing AndAlso e.ListQuotationPharmaceuticalDispensingDetailXpo.Count > 0 Then
            If PharmaceuticalDispensingDetail Is Nothing Then
                PharmaceuticalDispensingDetail = New PharmaceuticalDispensingDetail
            End If
            Dim entityXpo = e.ListQuotationPharmaceuticalDispensingDetailXpo(0)
            With PharmaceuticalDispensingDetail
                .QuotationPharmaceuticalDispensingDetailId = entityXpo.Id
                .CareGroupId = entityXpo.CareGroupId.Id
                .CodeNameCareGroup = entityXpo.CareGroupId.CodeName
                .ProductId = entityXpo.ProductId.Id
                .CodeProduct = entityXpo.ProductId.Code
                .NameProduct = entityXpo.ProductId.CodeName
                .WarehouseId = entityXpo.WarehouseId.Id
                .CodeNameWareHouse = entityXpo.WarehouseId.CodeName
                If entityXpo.HealthAdministratorId IsNot Nothing Then
                    .HealthAdministratorId = entityXpo.HealthAdministratorId.Id
                End If
                If entityXpo.ThirdPartyId IsNot Nothing Then
                    .ThirdPartyId = entityXpo.ThirdPartyId.Id
                End If
                .Quantity = entityXpo.Quantity
                .ReturnedQuantity = 0
                .ServiceDate = entityXpo.ServiceDate
                .FunctionalUnitId = entityXpo.FunctionalUnitId.Id
                .FullNameFunctionalUnit = entityXpo.FunctionalUnitId.Code + " - " + entityXpo.FunctionalUnitId.Name
                .OrderedHealthProfessionalCode = entityXpo.OrderedHealthProfessionalCode
                .OrderedProfessionalSpecialty = entityXpo.OrderedProfessionalSpecialty
                If entityXpo.OrderedHealthProfessionalThirdPartyId IsNot Nothing Then
                    .OrderedHealthProfessionalThirdPartyId = entityXpo.OrderedHealthProfessionalThirdPartyId.Id
                    .CodeNameHealthProfessional = entityXpo.OrderedHealthProfessionalCode + " - " + entityXpo.OrderedHealthProfessionalThirdPartyId.Name
                End If
                .AuthorizationNumber = entityXpo.AuthorizationNumber
                .LiquidationType = entityXpo.LiquidationType
                If entityXpo.CUPSEntityId IsNot Nothing Then
                    .CupsEntityId = entityXpo.CUPSEntityId.Id
                    .CodeNameCups = entityXpo.CUPSEntityId.CodeDescription
                End If
                .SurchargeApply = entityXpo.SurchargeApply
                .SalePrice = RoundValue(entityXpo.SalePrice, _roundingType)
                .AverageCost = RoundValue(entityXpo.AverageCost, _roundingType)
                .FinalProductCost = RoundValue(entityXpo.AverageCost, _roundingType)
                .TotalSalesPrice = RoundValue(entityXpo.TotalSalesPrice, _roundingType)
                .GrandTotalSalesPrice = RoundValue(entityXpo.GrandTotalSalesPrice, _roundingType)
                .DiscountPercentage = entityXpo.DiscountPercentage
                .DiscountValue = RoundValue(entityXpo.DiscountValue, _roundingType)
                .Custody = Me.Custody
                .QuotationCode = entityXpo.QuotationId.Code

                If entityXpo.QuotationPharmaceuticalDispensingDetailBatchSerialXpo IsNot Nothing AndAlso entityXpo.QuotationPharmaceuticalDispensingDetailBatchSerialXpo.Count > 0 Then
                    For Each item In entityXpo.QuotationPharmaceuticalDispensingDetailBatchSerialXpo
                        Dim batchSerial As New PharmaceuticalDispensingDetailBatchSerial
                        batchSerial.PhysicalInventoryId = item.PhysicalInventoryId
                        batchSerial.Quantity = item.Quantity
                        batchSerial.OutstandingQuantity = item.OutstandingQuantity
                        batchSerial.PhysicalInventoryCustodyId = item.PhysicalInventoryCustodyId
                        .PharmaceuticalDispensingDetailBatchSerial.Add(batchSerial)
                    Next
                End If
            End With

            LoadControls()
        End If
    End Sub

    Private Async Sub NormalProcess()
        'Se valida si el almacen es virtual para mostrar el mensaje al usuario
        If VirtualStore AndAlso _editMode = False AndAlso AffectedInventory Then
            If MessageIndigo.Show("El almacén seleccionado es Virtual, por lo tanto se cambiará el parámetro de Afecta Inventario en NO, Desea Continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        'Se valida si el almacen es custodia para mostrar el mensaje al usuario
        If Me.Custody AndAlso _editMode = False AndAlso Not AffectedInventory Then
            If MessageIndigo.Show("El almacén seleccionado es de custodia, por lo tanto se cambiará el parámetro de Afecta Inventario de custodia en Si, Desea Continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        If Me.Custody Then
            'Se valida si el producto existe en el inventario fisico siempre y cuando afecte inventario y el almacen es custodia
            If (listPhysicalInventoryCustody Is Nothing OrElse listPhysicalInventoryCustody.Count = 0) Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + product.Name + " no se encontro dentro del inventario fisico de custodia")
                Exit Sub
            End If

            'Se valida las cantidades del inventario fisico por almacen, siempre y cuando afecte inventario y el almacen es custodia
            If listPhysicalInventoryCustody IsNot Nothing AndAlso listPhysicalInventoryCustody.Count > 0 Then
                If Quantity > (From x In listPhysicalInventoryCustody Where x.ProductId = ProductId AndAlso x.WarehouseId = WarehouseId Select x.Quantity).FirstOrDefault() Then
                    Mensaje(EeventViewerImages.Advertencia) = "El producto no se puede agregar ya que la cantidad ingresada supera a la cantidad del inventario fisico de custodia"
                    Exit Sub
                End If
            End If
        Else
            'Se valida si el producto existe en el inventario fisico siempre y cuando afecte inventario y el almacen no sea virtual
            If (listPhysicalInventory Is Nothing OrElse listPhysicalInventory.Count = 0) AndAlso VirtualStore = False AndAlso AffectedInventory = True Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + product.Name + " no se encontro dentro del inventario fisico")
                Exit Sub
            End If

            'Se valida las cantidades del inventario fisico por almacen, siempre y cuando afecte inventario y el almacen no sea virtual
            If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count > 0 AndAlso VirtualStore = False AndAlso AffectedInventory = True Then
                If INDliBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    For Each item As PhysicalInventory In listPhysicalInventory
                        If item.QuantityDeliver > item.Quantity Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto con el lote {0} no se puede agregar ya que la cantidad ingresada supera a la cantidad del inventario fisico", item.CodeNameBatchSerial)
                            Exit Sub
                        End If
                    Next
                Else
                    If Quantity > (From x In listPhysicalInventory Where x.ProductId = ProductId AndAlso x.WarehouseId = WarehouseId Select x.Quantity).FirstOrDefault() Then
                        Mensaje(EeventViewerImages.Advertencia) = "El producto no se puede agregar ya que la cantidad ingresada supera a la cantidad del inventario fisico"
                        Exit Sub
                    End If
                End If
            End If

            If SalePrice = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProductNoTarifa", MODULE_NAME), INDsleCareGroup.Text, ServiceDate)
                Exit Sub
            End If
        End If
        AssigningValues()
        If ListProductServiceDetail Is Nothing OrElse ListProductServiceDetail.Count = ListProductServiceDetail.Where(Function(x) x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted).Count _
            OrElse ListProductServiceDetail.Count = 0 Then
            Await LoadSalePrice()
        End If
        Dim eventAdd As New AddPharmaceuticalDispensingDetailEventArgs()

        If ListProductServiceDetail IsNot Nothing Then
            ListProductServiceDetail.ForEach(Sub(x)
                                                 x.DiscountPercentage = DiscountPercentage
                                                 PharmaceuticalDispensingDetail.ProductServiceDetail.Add(x)
                                             End Sub)
        End If

        eventAdd.PharmaceuticalDispensingDetail = PharmaceuticalDispensingDetail


        eventAdd.VirtualStore = VirtualStore
        eventAdd.Custody = Me.Custody
        RaiseEvent AddPharmaceuticalDispensingDetail(Me, eventAdd)
        CleanControls(False)
        If _editMode Then
            Me.Close()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub

#End Region

#Region "Closed"
    ''' <summary>
    ''' Handles the Closed event of the INDPceBatchSerial control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ClosedEventArgs"/> instance containing the event data.</param>
    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceQuantity.Closed
        If Me.Custody Then
            listPhysicalInventoryCustody = CtrPhysicalInventory1.GetListPhysicalInventoryCustody()
            If listPhysicalInventoryCustody.Count > 0 Then
                INDPceQuantity.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), listPhysicalInventoryCustody.Count.ToString(), listPhysicalInventoryCustody.Sum(Function(x) x.QuantityDeliver).ToString())
                Quantity = listPhysicalInventoryCustody.Sum(Function(x) x.QuantityDeliver)
            Else
                Quantity = 0
                INDPceQuantity.EditValue = Nothing
            End If
        Else
            listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
            If listPhysicalInventory.Count > 0 Then
                INDPceQuantity.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), listPhysicalInventory.Count.ToString(), listPhysicalInventory.Sum(Function(x) x.QuantityDeliver).ToString())
                Quantity = listPhysicalInventory.Sum(Function(x) x.QuantityDeliver)
                'INDspnQuantity.Properties.ReadOnly = True
            Else
                Quantity = 0
                'INDspnQuantity.Properties.ReadOnly = False
                INDPceQuantity.EditValue = Nothing
            End If
        End If

    End Sub

    ''' <summary>
    ''' Handles the Closed event of the INDsleHealthProfessionalCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ClosedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleHealthProfessionalCode_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDsleHealthProfessionalCode.Closed
        If INDsleHealthProfessionalCode.EditValue IsNot Nothing Then
            If INDsleHealthProfessionalSpecialty.Properties.DataSource IsNot Nothing AndAlso CType(INDsleHealthProfessionalSpecialty.Properties.DataSource, IEnumerable(Of Object)).Count > 1 Then
                INDsleHealthProfessionalSpecialty.Focus()
            Else
                INDtxtAuthorizationNumber.Focus()
            End If
        End If
    End Sub
#End Region

#Region "ChangeQuantity"
    ' ''' <summary>
    ' ''' Handles the ChangeQuantity event of the CtrBatchSerial1 control.
    ' ''' </summary>
    ' ''' <param name="sender">The source of the event.</param>
    ' ''' <param name="e">The <see cref="ChangeQuantityEventArgs"/> instance containing the event data.</param>
    'Private Sub CtrBatchSerial1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrPhysicalInventory1.ChangeQuantity
    '    INDspnQuantity.EditValue = e.Quantity
    'End Sub
#End Region

#Region "SelectProduct"
    Private product As InventoryProduct
    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDpceProduct.Text = e.CodeNameProduct
        INDpceProduct.Focus()
        INDpceProduct.ClosePopup()
        CtrPhysicalInventory1.CleanControls()
        INDspnQuantity.EditValue = 1
        INDPceQuantity.EditValue = Nothing

        Using model As New MInventoryProduct(Me.Tag)
            product = model.GetInventoryProductByIdSimple(e.ProductId)
            ProductId = product?.Id
            INDSleProduct.EditValue = product?.Id
            AverageCost = product?.ProductCost
            FinalProductCost = product?.FinalProductCost
        End Using

        Me.SetTaxPercentbyProduct(Me._flagTaxInclude, product)

        If product IsNot Nothing AndAlso product?.Id > 0 Then
            INDliBatchSerial.HideControl(False)
        End If
        If product.ProductSubGroup IsNot Nothing Then
            If Not VirtualStore AndAlso product.ProductSubGroup.HandlesBatch = True Then
                INDspnQuantity.Properties.ReadOnly = True
                INDliBatchSerial.HideControl(False)
                INDPceQuantity.Focus()
                INDPceQuantity.ShowPopup()
            Else
                If Me.Custody Then
                    Using modelPhysical As New MCtrPhysicalInventory(Me.Tag)
                        listPhysicalInventoryCustody = modelPhysical.GetListPhysicalInventoryCustody(_patientCode, AdmissionNumberHeader, product.Id, WarehouseId)
                    End Using
                Else
                    Using modelPhysical As New MCtrPhysicalInventory(Me.Tag)
                        listPhysicalInventory = modelPhysical.GetListPhysicalInventory(product.Id, WarehouseId)
                    End Using
                End If

                INDspnQuantity.Properties.ReadOnly = False
                INDliBatchSerial.HideControl(True)
                INDspnQuantity.Focus()
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "El producto " + String.Concat(product.Code, " - ", product.Name) + " no tiene un subgrupo asociado"
            CleanControls(False)
        End If

        ActionsControls = True
        LoadSalePrice()
    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = "Afecta Inventario", .StatusColor = Color.Green})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = "No Afecta Inventario", .StatusColor = Color.Red})

        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Gets the sub total value.
    ''' </summary>
    ''' <returns></returns>
    Private Function GetSubTotalValue() As Tuple(Of Decimal, Decimal)
        Return New Tuple(Of Decimal, Decimal)(GrandTotalSalesPrice, Me._taxValue * Me.Quantity)
    End Function

    ''' <summary>
    ''' Loads the sub total value.
    ''' </summary>
    Private Async Function LoadSalePrice() As Task
        If calculateSalePrice Then
            If (Not Me.Custody) AndAlso CareGroupId <> 0 AndAlso ProductId <> 0 AndAlso INDdeServiceDate.EditValue IsNot Nothing AndAlso INDsleFunctionalUnit.EditValue IsNot Nothing _
                AndAlso INDsleHealthProfessionalCode.EditValue IsNot Nothing Then

                Using Model As New MProductRate(Me.Tag)
                    Dim _productRateDetail As ProductRateDetail = (Await Model.GetProductRateDetailWithValue(CareGroupId, ProductId, ServiceDate.Date)).ObjectEmbbeded
                    If _productRateDetail IsNot Nothing AndAlso _productRateDetail.Id > 0 Then
                        Dim SalePriceRateType As Decimal = 0
                        ValidatioSurcharge(_productRateDetail.RateType, SurchargeApply)

                        If ListProductServiceDetail IsNot Nothing AndAlso ListProductServiceDetail.Count > 0 Then
                            ListProductServiceDetail.RemoveAll(Function(x) x.ChangeTracker.State = ObjectState.Added)
                        End If

                        If SurchargeApply Then
                            SalePriceRateType = _productRateDetail.SalesValueWithSurcharge
                        Else
                            SalePriceRateType = _productRateDetail.SalesValue
                        End If

                        SalePriceRateType = RoundValue(SalePriceRateType, _roundingType)

                        If Me._flagTaxInclude Then
                            Me.SalePrice = SalePriceRateType
                            Me.GrossValue = RoundValue((Me.SalePrice / ((Me._taxPercent / 100.0) + 1)), _roundingType)
                            Me.TaxValue = RoundValue(If(Me._taxPercent = 0, 0, (Me.SalePrice - Me.GrossValue)), _roundingType)
                        Else
                            Me.GrossValue = SalePriceRateType
                            Me.TaxValue = RoundValue((Me.GrossValue * (Me._taxPercent / 100.0)), _roundingType)
                            Me.SalePrice = RoundValue(Me.GrossValue + Me.TaxValue, _roundingType)
                        End If
                    Else
                        SalePrice = 0
                        Me.TaxValue = 0
                        Me.GrossValue = 0
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProductNoTarifa", MODULE_NAME), INDsleCareGroup.Text, ServiceDate)
                    End If
                End Using
            Else
                SalePrice = 0
                Me.TaxValue = 0
                Me.GrossValue = 0
            End If
            Me.SetValuesTaxControls = Me._flagTaxInclude
            LoadGrandTotalValue()
        End If
    End Function

    ''' <summary>
    ''' valida si no es tarifa fija para que no pueda cambiar el control de recargo
    ''' </summary>
    ''' <param name="RateType"></param>
    ''' <param name="SurchargeApply"></param>
    Sub ValidatioSurcharge(RateType As Byte, SurchargeApply As Boolean)
        If RateType <> 1 And SurchargeApply Then
            Me.SurchargeApply = False
            Mensaje(EeventViewerImages.Advertencia) = "La tarifa del producto No es fija, por ende no maneja valor de recargo"
        End If
    End Sub

    ''' <summary>
    ''' funcion para obtener el valor de un CUPS
    ''' </summary>
    ''' <param name="Admission"></param>
    ''' <param name="FunctionalUnitCenterAttentionCode"></param>
    ''' <param name="listHomologation"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="FuncionalUnitId"></param>
    ''' <param name="HealthProfessionalSpecialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <param name="GenderThirdParty"></param>
    ''' <param name="_patientDate"></param>
    ''' <param name="ProfessionalCode"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="ContractDescriptionId"></param>
    ''' <returns></returns>
    Private Function GetServiceValue(Admission As String, FunctionalUnitCenterAttentionCode As String, listHomologation As List(Of CupsHomologation), CareGroupId As Integer, FuncionalUnitId As Integer, HealthProfessionalSpecialty As String,
                                ServiceDate As DateTime, GenderThirdParty As Byte, _patientDate As DateTime, ProfessionalCode As String, ThirdPartyId As Integer, ContractDescriptionId As Integer?) As List(Of ServiceOrderDetail)
        Using model As New MServiceOrder(Me.Tag)
            Dim result = model.GetServiceValue(Admission, FunctionalUnitCenterAttentionCode, listHomologation, CareGroupId, FuncionalUnitId, HealthProfessionalSpecialty, ServiceDate, GenderThirdParty, _patientDate, 1, ProfessionalCode, ThirdPartyId, 0, ContractDescriptionId)
            If result Is Nothing OrElse result.StateResult = False Then
                If result.Message.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                Return New List(Of ServiceOrderDetail)
            End If
            Return result.ObjectEmbbeded
        End Using
    End Function

    ''' <summary>
    ''' Loads the grand total value.
    ''' </summary>
    Private Sub LoadGrandTotalValue()
        If Quantity <> 0 AndAlso SalePrice <> 0 Then
            TotalSalesPrice = RoundValue(InventoryStaticServices.CalculateTotalValue(SalePrice, DiscountValue), _roundingType)
            GrandTotalSalesPrice = RoundValue(InventoryStaticServices.CalculateGrandTotalValue(SalePrice, Quantity, DiscountValue), _roundingType)
        Else
            TotalSalesPrice = 0
            GrandTotalSalesPrice = 0
        End If
    End Sub

    ''' <summary>
    ''' Crea el detalle para guardar en la tabla
    ''' </summary>
    ''' <param name="CUPSId"></param>
    ''' <param name="ContractDescriptionsId"></param>
    ''' <param name="ProductId"></param>
    ''' <param name="Price"></param>
    Private Sub GenerateProductServiceDetail(CUPSId As Integer?, ContractDescriptionsId As Integer?, ProductId As Integer?, Price As Decimal _
                                             , LiquidationType As Byte, RateType As Byte)
        If ListProductServiceDetail Is Nothing Then
            ListProductServiceDetail = New List(Of ProductServiceDetail)
        End If

        Dim ProductServiceDetail = New ProductServiceDetail

        With ProductServiceDetail
            .CUPSEntityId = CUPSId
            .ContractDescriptionsId = ContractDescriptionsId
            .ProductId = ProductId
            .Price = Price
            .LiquidationType = LiquidationType
            .RateType = RateType
        End With

        ListProductServiceDetail.Add(ProductServiceDetail)
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls(ByVal flagClean As Boolean)
        CtrPhysicalInventory1.CleanControls()
        CleanFlags()
        If flagClean Then
            INDsleWareHouse.EditValue = Nothing
            INDsleWareHouse.Properties.NullText = String.Empty
            INDsleCareGroup.EditValue = Nothing
            INDsleCareGroup.Properties.NullText = String.Empty
            INDsleFunctionalUnit.EditValue = Nothing
            INDsleFunctionalUnit.Properties.NullText = String.Empty
            INDsleHealthProfessionalCode.Properties.NullText = String.Empty
            INDsleHealthProfessionalSpecialty.Properties.NullText = String.Empty
            OrderedHealthProfessionalCode = Nothing
            OrderedProfessionalSpecialty = Nothing
            INDgleLiquidationType.EditValue = Nothing
            INDgleSurchargeApply.EditValue = Nothing
            AuthorizationNumber = Nothing
        End If

        ProductId = Nothing
        INDspnQuantity.EditValue = 1
        ServiceDate = ServiceDate

        QuotationPharmaceuticalDispensingDetailId = Nothing
        messageErrorQuotation = String.Empty

        CupsEntityId = Nothing
        Me.GrossValue = 0
        Me.TaxValue = 0
        Me._taxPercent = 0
        Me.SetValuesTaxControls = Me._flagTaxInclude
        SalePrice = 0
        AverageCost = 0
        FinalProductCost = 0
        TotalSalesPrice = 0
        GrandTotalSalesPrice = 0
        DiscountPercentage = 0
        DiscountValue = 0
        INDpceProduct.EditValue = Nothing
        INDsleCupsEntity.Properties.NullText = String.Empty
        listPhysicalInventory = Nothing
        PharmaceuticalDispensingDetail = New PharmaceuticalDispensingDetail()
        INDliCupsEntity.HideControl(True)
        INDliBatchSerial.HideControl(True)
        INDPceQuantity.EditValue = Nothing
        ListProductServiceDetail = Nothing

        INDliHealthAdministrator.HideControl()
        INDliThirdParty.HideControl()
        'Me.HealthAdministratorId = Nothing
        'No es necesario limpiar el tercero, si se habilitan estas lineas genera error cuando inserto dos items seguidos ya que envia el tercero como 0 :P
        'Me.ThirdPartyId = Nothing
        'INDsleThirdParty.Properties.NullText = String.Empty
        product = Nothing

        INDSleProduct.EditValue = Nothing
        If INDsleWareHouse.EditValue Is Nothing Then
            Me.Custody = False
            HideProduct()
        End If
        SetReadOnlyControlsForQuotation = False

        INDsleCareGroup.Focus()
    End Sub

    ''' <summary>
    ''' Cleans the flags.
    ''' </summary>
    Public Sub CleanFlags()
        _flagPopUpCareGroup = False
        _flagPopUpProduct = False
        _flagPopUpWareHouse = False
        _flagPopUpHealtProfessional = False
        _flagPopUpHealthProfessionalSpecialty = False
        _flagPopUpCupsEntity = False
        _flagPopUpFunctionalUnit = False
    End Sub

    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            INDspnQuantity.Enabled = value
            INDPceQuantity.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With PharmaceuticalDispensingDetail
            .CareGroupId = CareGroupId
            .ProductId = ProductId
            .WarehouseId = WarehouseId
            .HealthAdministratorId = HealthAdministratorId
            .ThirdPartyId = ThirdPartyId
            .Quantity = Quantity
            .ReturnedQuantity = 0
            .ServiceDate = ServiceDate
            .FunctionalUnitId = FunctionalUnitId
            .OrderedHealthProfessionalCode = OrderedHealthProfessionalCode
            .OrderedProfessionalSpecialty = OrderedProfessionalSpecialty
            .OrderedHealthProfessionalThirdPartyId = OrderedHealthProfessionalThirdPartyId
            .AuthorizationNumber = AuthorizationNumber
            .LiquidationType = LiquidationType
            .CupsEntityId = CupsEntityId
            .SurchargeApply = SurchargeApply
            .TaxValue = RoundValue(Me.TaxValue, _roundingType)
            .GrossValue = RoundValue(Me.GrossValue, _roundingType)
            .SalePrice = RoundValue(SalePrice, _roundingType)
            .AverageCost = RoundValue(AverageCost, _roundingType)
            .FinalProductCost = RoundValue(FinalProductCost, _roundingType)
            .TotalSalesPrice = TotalSalesPrice
            .GrandTotalSalesPrice = GrandTotalSalesPrice
            .DiscountPercentage = DiscountPercentage
            .DiscountValue = DiscountValue
            .CodeProduct = INDpceProduct.Text.Split(" - ")(0)
            .NameProduct = INDpceProduct.Text
            .CodeNameCareGroup = INDsleCareGroup.Text
            .CodeNameWareHouse = INDsleWareHouse.Text
            .CodeNameHealthProfessional = INDsleHealthProfessionalCode.Text
            .CodeNameHealthProfessionalSpeciality = INDsleHealthProfessionalSpecialty.Text
            .CodeNameCups = INDsleCupsEntity.Text
            .FullNameFunctionalUnit = INDsleFunctionalUnit.Text
            .Custody = Me.Custody
        End With

        Dim itemBatchSerial As PharmaceuticalDispensingDetailBatchSerial
        If INDliBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If Me.Custody Then
                For Each item As PhysicalInventoryCustody In listPhysicalInventoryCustody
                    If PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Where(Function(x) x.PhysicalInventoryCustodyId = item.Id).ToList().Count > 0 Then
                        itemBatchSerial = PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Where(Function(x) x.PhysicalInventoryCustodyId = item.Id).FirstOrDefault()
                    Else
                        itemBatchSerial = New PharmaceuticalDispensingDetailBatchSerial()
                    End If
                    itemBatchSerial.PhysicalInventoryCustodyId = item.Id
                    itemBatchSerial.Quantity = item.QuantityDeliver
                    itemBatchSerial.OutstandingQuantity = item.QuantityDeliver
                    PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Add(itemBatchSerial)
                Next
            Else
                'If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count > 0 Then
                For Each item As PhysicalInventory In listPhysicalInventory
                    If PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Where(Function(x) x.PhysicalInventoryId = item.Id).ToList().Count > 0 Then
                        itemBatchSerial = PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Where(Function(x) x.PhysicalInventoryId = item.Id).FirstOrDefault()
                    Else
                        itemBatchSerial = New PharmaceuticalDispensingDetailBatchSerial()
                    End If
                    itemBatchSerial.PhysicalInventoryId = item.Id
                    itemBatchSerial.Quantity = item.QuantityDeliver
                    itemBatchSerial.OutstandingQuantity = item.QuantityDeliver
                    PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Add(itemBatchSerial)
                Next
            End If
        Else
            If Me.Custody Then
                If PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Count > 0 Then
                    PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial(0).PhysicalInventoryCustodyId = listPhysicalInventoryCustody(0).Id
                    PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial(0).Quantity = Quantity
                    PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial(0).OutstandingQuantity = Quantity
                Else
                    Dim detail As New PharmaceuticalDispensingDetailBatchSerial
                    If listPhysicalInventoryCustody.Count > 0 Then
                        With detail
                            .PhysicalInventoryCustodyId = listPhysicalInventoryCustody(0).Id
                            .Quantity = Quantity
                            .OutstandingQuantity = Quantity
                        End With
                        PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Add(detail)
                    End If
                End If
            Else
                If PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Count > 0 Then
                    If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count > 0 Then
                        PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial(0).PhysicalInventoryId = listPhysicalInventory(0).Id
                    End If
                    PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial(0).Quantity = Quantity
                    PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial(0).OutstandingQuantity = Quantity
                Else
                    Dim detail As New PharmaceuticalDispensingDetailBatchSerial
                    If listPhysicalInventory.Count > 0 Then
                        With detail
                            .PhysicalInventoryId = listPhysicalInventory(0).Id
                            .Quantity = Quantity
                            .OutstandingQuantity = Quantity
                        End With
                        PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Add(detail)
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Creates the type of the liquidation.
    ''' </summary>
    Private Sub CreateLiquidationType()
        ListLiquidationType = New List(Of Tuple(Of Integer, String))()
        ListLiquidationType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("LiquidationTypeRateManual", MODULE_NAME)))
        ListLiquidationType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("LiquidationTypeIPS", MODULE_NAME)))
        INDgleLiquidationType.Properties.DataSource = ListLiquidationType
        LiquidationType = 1
        SurchargeApply = False
    End Sub

    ''' <summary>
    ''' Opens the form.
    ''' </summary>
    ''' <param name="form">The form.</param>
    Private Sub OpenForms(ByVal form As FormBase)
        form.ViewModeEditHold = True
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.MinimizeBox = False
        form.MaximizeBox = False
        form.Size = New Size(800, 700)
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Dim transparent As New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Property LoadDoc As Boolean


    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Sub LoadControls() Implements IPharmaceuticalDispensingDetail.LoadControls
        If _presenter Is Nothing Then
            _presenter = New PPharmaceuticalDispensingDetail(Me)
        End If
        calculateSalePrice = False
        With PharmaceuticalDispensingDetail
            CareGroupId = .CareGroupId
            ProductId = .ProductId
            WarehouseId = .WarehouseId
            Quantity = .Quantity
            ServiceDate = .ServiceDate
            FunctionalUnitId = .FunctionalUnitId
            OrderedHealthProfessionalCode = .OrderedHealthProfessionalCode
            OrderedProfessionalSpecialty = .OrderedProfessionalSpecialty
            AuthorizationNumber = .AuthorizationNumber
            LiquidationType = .LiquidationType
            HealthAdministratorId = .HealthAdministratorId
            If .ThirdPartyId IsNot Nothing Then
                ThirdPartyId = .ThirdPartyId
            End If
            CupsEntityId = .CupsEntityId
            SurchargeApply = .SurchargeApply
            SalePrice = .SalePrice
            Me.GrossValue = .GrossValue
            Me.TaxValue = .TaxValue
            AverageCost = .AverageCost
            FinalProductCost = .FinalProductCost
            TotalSalesPrice = .TotalSalesPrice
            GrandTotalSalesPrice = .GrandTotalSalesPrice
            DiscountPercentage = .DiscountPercentage
            DiscountValue = .DiscountValue
            INDpceProduct.Text = .NameProduct
            INDsleCareGroup.Properties.NullText = .CodeNameCareGroup
            INDsleWareHouse.Properties.NullText = .CodeNameWareHouse
            If _editMode Then
                INDsleWareHouse.Properties.ReadOnly = True
                INDpceProduct.Properties.ReadOnly = True
                INDsbAdd.Text = "Editar"
            End If
            INDsleHealthProfessionalCode.Properties.NullText = .CodeNameHealthProfessional
            INDsleHealthProfessionalSpecialty.Properties.NullText = .OrderedProfessionalSpecialty
            INDsleCupsEntity.Properties.NullText = .CodeNameCups
            INDsleFunctionalUnit.Properties.NullText = .FullNameFunctionalUnit
            Custody = .Custody
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(ProductId)
                If Custody Then
                    INDSleProduct_QueryPopUP(Nothing, Nothing)
                    INDSleProduct.Properties.ReadOnly = True
                End If
                Me.LoadDoc = True
                INDSleProduct.EditValue = product.Id
                product.TaxedProduct = If(Not product.TaxedProduct AndAlso Me.TaxValue > 0, True, (product.TaxedProduct AndAlso product.LiquidateSalesTaxes))
                Me.LoadDoc = True
            End Using
            If product.ProductSubGroup IsNot Nothing Then
                If product.ProductSubGroup.HandlesBatch = True AndAlso VirtualStore = False Then
                    INDspnQuantity.Properties.ReadOnly = True
                    INDliBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDPceQuantity.Focus()
                    'INDPceQuantity.ShowPopup()
                Else
                    Using modelPhysical As New MCtrPhysicalInventory(Me.Tag)
                        If Me.Custody Then
                            listPhysicalInventoryCustody = modelPhysical.GetListPhysicalInventoryCustody(_patientCode, AdmissionNumberHeader, product.Id, WarehouseId)
                        Else
                            listPhysicalInventory = modelPhysical.GetListPhysicalInventory(product.Id, WarehouseId)
                        End If

                    End Using
                    INDspnQuantity.Properties.ReadOnly = False
                    INDliBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDspnQuantity.Focus()
                    'listPhysicalInventory = Nothing
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "El producto " + String.Concat(product.Code, " - ", product.Name) + " no tiene un subgrupo asociado"
                CleanControls(False)
            End If
            If INDliBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If .PharmaceuticalDispensingDetailBatchSerial.Count = 0 Then
                    INDPceQuantity.EditValue = String.Empty
                Else
                    INDPceQuantity.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), .PharmaceuticalDispensingDetailBatchSerial.Count.ToString(), .PharmaceuticalDispensingDetailBatchSerial.Sum(Function(x) x.Quantity).ToString())
                End If
                CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
                CtrPhysicalInventory1.Product = product
                CtrPhysicalInventory1.WareHouseId = WarehouseId
                CtrPhysicalInventory1.PatientCode = Me._patientCode
                CtrPhysicalInventory1.AdmissionNumber = Me.AdmissionNumberHeader
                CtrPhysicalInventory1.Custody = Me.Custody
                CtrPhysicalInventory1.FormOwner = Me
                CtrPhysicalInventory1.SetListPhysicalInventory()
                CtrPhysicalInventory1.SetQuantityPhysicalInventory(.PharmaceuticalDispensingDetailBatchSerial.ToList())

                If listPhysicalInventory Is Nothing OrElse listPhysicalInventory.Count > 0 Then
                    listPhysicalInventory = CtrPhysicalInventory1.listPhysicalInventory
                End If
            End If

            QuotationPharmaceuticalDispensingDetailId = .QuotationPharmaceuticalDispensingDetailId
            If QuotationPharmaceuticalDispensingDetailId IsNot Nothing AndAlso QuotationPharmaceuticalDispensingDetailId > 0 Then
                SetReadOnlyControlsForQuotation = True
            End If

            Me.SetTaxPercentbyProduct(Me._flagTaxInclude, product)

            Dim ToRemove = New List(Of ProductServiceDetail)
            ListProductServiceDetail = .ProductServiceDetail.ToList()

            ListProductServiceDetail.ForEach(Sub(x)
                                                 If x.Id > 0 Then
                                                     x.MarkAsDeleted()
                                                 Else
                                                     ToRemove.Add(x)
                                                 End If
                                             End Sub)
            If ToRemove.Count > 0 Then
                ToRemove.ForEach(Sub(h)
                                     .ProductServiceDetail.Remove(h)
                                     ListProductServiceDetail.Remove(h)
                                 End Sub)
            End If

            Me.SetValuesTaxControls = Me._flagTaxInclude
        End With
        calculateSalePrice = True
    End Sub

    Private WriteOnly Property SetReadOnlyControlsForQuotation
        Set(value)
            INDsleCareGroup.ReadOnly = value
            INDpceProduct.Properties.ReadOnly = value
            INDspnQuantity.Properties.ReadOnly = value
            INDdeServiceDate.Properties.ReadOnly = value
        End Set
    End Property

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleHealthProfessionalCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleHealthProfessionalCode_EditValueChanged(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleHealthProfessionalCode.EditValueChanging
        If e.NewValue IsNot Nothing Then
            INDsleHealthProfessionalSpecialty.Enabled = True
            Dim healthProfessional As HealthCareProfessionalXpo
            Using model As New MServiceOrder(Me.Tag)
                healthProfessional = model.GetCareProfessionalByCode(e.NewValue.ToString())(0)
            End Using
            Using model As New MThirdParty(Me.Tag)
                Dim ThirdParty = model.GetThirdParty(healthProfessional.CODIGONIT.TrimStart("0"))
                If ThirdParty.Id = 0 Then
                    'si el medico no esta creado como tercero en la BD no continua el proceso
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", "Billing"), healthProfessional.CodeName)
                    e.Cancel = True
                    Exit Sub
                End If
                OrderedHealthProfessionalThirdPartyId = ThirdParty.Id
            End Using
            'If _editMode = True AndAlso ServiceOrderDetail IsNot Nothing Then
            '    ServiceOrderDetail.PerformsHealthProfessionalCode = e.NewValue
            '    ServiceOrderDetail.PerformsHealthProfessionalThirdPartyId = ThirdParty.Id
            'End If

            'If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            '    Dim rowIndex = INDGvSurgery.FocusedRowHandle
            '    'obtengo el registro de la clase cirujano y le pongo el mismo medico que se selecciona para mostrar en la rejilla
            '    Dim surgicalProcedure = listSurgicalProcedureService.Find(Function(x) x.ClassService = ResourceManager.GetString("Surgeon", "Contract") And x.DefaultService = True)
            '    surgicalProcedure.PerformsHealthProfessionalCode = e.NewValue
            '    Dim surgicalDetailTmp = ServiceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedure.IPSServiceId).FirstOrDefault()
            '    'cambio el medico tambn a la entidad ServiceOrderDetailSurgical
            '    surgicalDetailTmp.PerformsHealthProfessionalCode = e.NewValue
            '    surgicalDetailTmp.PerformsHealthProfessionalThirdPartyId = ThirdParty.Id
            '    INDGcSurgery.DataSource = Nothing
            '    INDGcSurgery.DataSource = listSurgicalProcedureService
            '    INDGvSurgery.ExpandAllGroups()
            '    INDGvSurgery.FocusedRowHandle = rowIndex
            'End If
            SetSpecialties(healthProfessional, INDsleHealthProfessionalSpecialty)
        Else
            INDsleHealthProfessionalSpecialty.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Sets the specialties.
    ''' </summary>
    ''' <param name="healthProfessional">The health professional.</param>
    ''' <param name="searchLookUpEdit">The search look up edit.</param>
    Private Sub SetSpecialties(healthProfessional As HealthCareProfessionalXpo, searchLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit)
        Dim listSpecialty As New List(Of Tuple(Of String, String))
        If healthProfessional.CODESPEC1 IsNot Nothing Then
            listSpecialty.Add(New Tuple(Of String, String)(healthProfessional.CODESPEC1.CODESPECI, healthProfessional.CODESPEC1.CodeName))
        End If
        If healthProfessional.CODESPEC2 IsNot Nothing Then
            listSpecialty.Add(New Tuple(Of String, String)(healthProfessional.CODESPEC2.CODESPECI, healthProfessional.CODESPEC2.CodeName))
        End If
        If healthProfessional.CODESPEC3 IsNot Nothing Then
            listSpecialty.Add(New Tuple(Of String, String)(healthProfessional.CODESPEC3.CODESPECI, healthProfessional.CODESPEC3.CodeName))
        End If
        searchLookUpEdit.Properties.DataSource = listSpecialty
        If listSpecialty.Count = 1 Then
            searchLookUpEdit.EditValue = listSpecialty(0).Item1
        Else
            searchLookUpEdit.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Loads the last ware house.
    ''' </summary>
    Private Sub LoadLastWareHouse()

    End Sub

    ''' <summary>
    ''' Lanza la consulta del producto para poderlo seleccionar
    ''' sin necesidad de desplegar el control de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AssignProductWithCode() As Task
        If INDpceProduct.Text.Trim <> String.Empty Then
            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct As String() = INDpceProduct.Text.Split(" - ")
            'Capturo el codigo del producto
            Dim codeProduct As String = arrayCodeProduct(0).Trim

            If codeProduct Is String.Empty Then
                If product IsNot Nothing Then
                    INDpceProduct.Text = product.Code + " - " + product.Name
                End If
                Exit Function
            End If

            Using model As New MInventoryProduct(Me.Tag)
                'Consulto el producto para armar el objeto SelectProductEventArgs
                Dim resultProduct = Await model.GetInventoryProduct(INDpceProduct.Text.Trim)
                If resultProduct.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = resultProduct.MessageResult(0)
                    CleanControls(False)
                    Exit Function
                End If
                'Valido que exista el producto
                If resultProduct.ObjectEmbbeded IsNot Nothing AndAlso resultProduct.ObjectEmbbeded.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto no existe")
                    CleanControls(False)
                    Exit Function
                End If
                'Valido que el producto no este inactivo
                If resultProduct.ObjectEmbbeded.Status = False Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name + " está inactivo")
                    CleanControls(False)
                    Exit Function
                End If

                'Se valida que el producto sea medicamento o insumo
                Dim xpo = model.GetProductTypeXpo(resultProduct.ObjectEmbbeded.ProductTypeId)
                If xpo IsNot Nothing Then
                    If xpo.Class <> 2 AndAlso xpo.Class <> 3 Then 'Medicamento o insumo
                        Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name + " no es de tipo medicamento o insumo")
                        CleanControls(False)
                        Exit Function
                    End If
                End If

                'Instancio el objeto SelectProductEventArgs
                Dim obj As New SelectProductEventArgs
                obj.ProductId = resultProduct.ObjectEmbbeded.Id
                obj.CodeNameProduct = resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name
                'Invoco la funcion que realiza el resto de la logica
                CtrProducts1_SelectProduct(Nothing, obj)
            End Using
        ElseIf INDpceProduct.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDpceProduct.Text = product.Code + " - " + product.Name
            End If
        End If
    End Function

    ''' <summary>
    ''' metodo que establece que controles muestra (si los de Impuestos incluidos o excluido) 
    ''' </summary>
    ''' <param name="value">valor que indica si se muestra incluido o excluido, si es nothing se ocultan todos</param>
    Private Sub SetTaxIncludeOrExcludeControls(value As Boolean?)
        If value Is Nothing Then
            HideTaxControls()
            Exit Sub
        End If
        INDliSubTotal.Visibility = If(value, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDliTaxValue.Visibility = If(value, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDliTaxValue1.Visibility = If(Not value, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDliTotalValue.Visibility = If(Not value, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

    ''' <summary>
    ''' Oculta los controles de gravado con impuestos
    ''' </summary>
    Private Sub HideTaxControls()
        INDliSubTotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliTaxValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliTaxValue1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub


    ''' <summary>
    ''' metodo que establece el valor del impuesto y si es un producto gravable dependiendo de la parametrizacion del producto
    ''' </summary>
    ''' <param name="TaxInclude"></param>
    ''' <param name="product"></param>
    Private Sub SetTaxPercentbyProduct(TaxInclude As Boolean, product As InventoryProduct)
        'valida si el producto es grabado, si lo esta mostramos los controles de impuesto, si no  se ocultan todos
        If product IsNot Nothing AndAlso product?.TaxedProduct AndAlso product?.LiquidateSalesTaxes Then
            Me.SetTaxIncludeOrExcludeControls(TaxInclude)
            Me._taxPercent = product?.PercentageIVA
        Else
            Me._taxPercent = 0
            Me.HideTaxControls()
        End If
    End Sub



    ''' <summary>
    '''Configura las propiedades de un campo numérico dependiendo de la moneda  de la compania segun el tipo de redondeo
    ''' </summary>
    Public Sub SetFormatCurrency(currencyNumberFormat As Globalization.NumberFormatInfo)
        Dim objMaskDisplay As Object = Nothing
        If _roundingType > 0 Then
            currencyNumberFormat.CurrencyDecimalDigits = Utils.MaskByCurrencyRounding(_roundingType)
            If _roundingType <> ECurrencyRoundingType.OneDecimal AndAlso _roundingType <> ECurrencyRoundingType.TwoDecimals Then
                objMaskDisplay = New With {.useMaskAsDisplayFormat = False, .decimals = 2}
            End If
            changeNumericFormatByCurrency(currencyNumberFormat, Nothing, objMaskDisplay)
        End If
    End Sub


    ''' <summary>
    ''' Redondea el valor por el tipo de redondeo
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="RoundingType"></param>
    ''' <returns></returns>
    Function RoundValue(value As Decimal, RoundingType As Integer) As Decimal
        Return Utils.RoundValueByTypeCurrency(value, RoundingType)
    End Function

#End Region

End Class