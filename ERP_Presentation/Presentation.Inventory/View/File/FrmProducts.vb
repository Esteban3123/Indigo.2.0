'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 19-11-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Utils
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports System.ComponentModel
Imports Presentation.Accounting
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Inventory.MVP
Imports Presentation.Controls.MVP
Imports Presentation.MixingStation.MVP
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
#End Region

Public Class FrmProducts
    Implements IProduct, ICustomizableForm

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmProducts"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrProduct()
        ctrTmp.Dock = DockStyle.Fill
        AddHandler ctrTmp.TakeInventoryProduct, AddressOf TakeProduct
        AdditionalControlPanel.Controls.Add(ctrTmp)
        ProductAttributes = New List(Of Tuple(Of Integer, Object))()
    End Sub

    ''' <summary>
    ''' Takes the product.
    ''' </summary>
    Private Sub TakeProduct()
        ctrTmp.InventoryProduct = _product
    End Sub
#End Region

#Region "Properties and Variables"

#Region "Variables"
    ''' <summary>
    ''' Nombre del módulo
    ''' </summary>
    Private Const MODULE_NAME As String = "Inventory"

    ''' <summary>
    ''' Ctr para el formulario
    ''' </summary>
    Private ctrTmp As CtrProduct

    ''' <summary>
    ''' indica si el popup de tipo de producto se abre por primara vez para cargar el datasource
    ''' </summary>
    Private _openPopUpProductType As Boolean

    ''' <summary>
    ''' indica si el popup de patologias POS se abre por primera vez para cargar el datasource
    ''' </summary>
    Private _openPopUpPOSPatologie As Boolean

    ''' <summary>
    ''' indica si el popup de SCA se abre por primara vez para cargar el datasource
    ''' </summary>
    Private _openPopUpATC As Boolean

    ''' <summary>
    ''' indica si el popup de RiskLevel se abre por primara vez para cargar el datasource
    ''' </summary>
    Private _openPopUpRiskLevel As Boolean

    ''' <summary>
    ''' indica si el popup de unidad de empaque se abre por primara vez para cargar el datasource
    ''' </summary>
    Private _openPopUpPackingUnit As Boolean

    ''' <summary>
    ''' indica si el popup de grupo se abre por primara vez para cargar el datasource
    ''' </summary>
    Private _openPopUpGroup As Boolean

    ''' <summary>
    ''' indica si el popup de subgrupo se abre por primara vez para cargar el datasource
    ''' </summary>
    Private _openPopUpSubGroup As Boolean

    ''' <summary>
    ''' indica si el popup de fabricante se abre por primara vez para cargar el datasource
    ''' </summary>
    Private _openPopUpManufacturer As Boolean

    ''' <summary>
    ''' indica si el popup de iva se abre por primara vez para cargar el datasource
    ''' </summary>
    Private _openPopUpIva As Boolean

    ''' <summary>
    ''' indica si el popup de grupo de facturacion se abre por primara vez para cargar el datasource
    ''' </summary>
    Private _openPopUpBillinfGroup As Boolean

    ''' <summary>
    ''' indica si el popup de grupo de facturacion se abre por primara vez para cargar el datasource
    ''' </summary>
    Private _openPopUpBillingGruposNoPos As Boolean

    ''' <summary>
    ''' Indica si el popup de tipos de medicamento ya fue inicializado
    ''' </summary>
    Private _openPopUpMedicationType As Boolean = False

    ''' <summary>
    ''' Variable que define si se va a cargar un registro o no
    ''' </summary>
    Private _isLoading As Boolean = False

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As InventorySequence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id que identifica si el tipo de producto maneja o no rango de temperatura
    ''' </summary>
    Private _handleTemperatureRange As Boolean

    ''' <summary>
    ''' Id que identifica si la empresa maneja o no central de mezclas
    ''' </summary>
    Private _handleMixingStation As Boolean

    ''' <summary>
    ''' Obtiene el layout del frontal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IProduct.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IProduct.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As InventorySequence Implements IProduct.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Private _product As InventoryProduct

    ''' <summary>
    ''' variable que se utiliza para saber si el frontal entra por modo busqueda o modo edicion
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PInventoryProduct

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInventory

    ''' <summary>
    ''' Listado de producto de origen
    ''' </summary>
    Private ListProductOrigin As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de tipos de moneda
    ''' </summary>
    Private ListCurrencyType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Datasource de estado de registro sanitario
    ''' </summary>
    Private ListSanitaryRegistration As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Datasource de consumo
    ''' </summary>
    Private ListYesNo As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Variable que contiene la configuración de central de mezclas
    ''' </summary>
    Private _configurationManageMS As CMConfiguration

    ''' <summary>
    ''' Entidad de medicamento cargada con xpo
    ''' </summary>
    Private MedicamentXpo As ATCXpo

	''' <summary>
	''' Datasource de alamcenamiento 
	''' </summary>
	Private ListStorageProduct As List(Of Tuple(Of Integer, String))

	''' <summary>
	''' Datasource tipo de componenete lácteo
	''' </summary>
	Private ListDairyComponentType As List(Of Tuple(Of Integer, String))

	Private _productTypeClass As eProductTypeClass
	Public Property ProductTypeClass() As eProductTypeClass
		Get
			Return _productTypeClass
		End Get
		Set(ByVal value As eProductTypeClass)
			_productTypeClass = value
		End Set
	End Property


#End Region

#Region "Product Properties"
	''' <summary>
	''' Obtiene o establece el codigo del producto
	''' </summary>
	Public Property Code As String Implements IProduct.Code
		Get
			If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
				Return String.Empty
			Else
				Return INDbteCode.Text
			End If
		End Get
		Set(value As String)
			INDbteCode.Text = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el nombre del producto
	''' </summary>
	Public Property NameProduct As String Implements IProduct.NameProduct
		Get
			Return INDtxtName.Text
		End Get
		Set(value As String)
			INDtxtName.Text = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el id del tipo de producto
	''' </summary>
	Public Property ProductTypeId As Integer Implements IProduct.ProductTypeId
		Get
			Return CType(INDsleProductType.EditValue, Integer)
		End Get
		Set(value As Integer)
			INDsleProductType.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el id del sistema de clasificacion automatica
	''' </summary>
	Public Property ATCId As Integer? Implements IProduct.ATCId
		Get
			Return CType(INDsleATC.EditValue, Integer?)
		End Get
		Set(value As Integer?)
			INDsleATC.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el nivel de riesgo
	''' </summary>
	Public Property InventoryRiskLevelId As Integer? Implements IProduct.InventoryRiskLevelId
		Get
			Return CType(INDsleRiskLevel.EditValue, Integer?)
		End Get
		Set(value As Integer?)
			INDsleRiskLevel.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el codigo CUM del producto
	''' </summary>
	Public Property CodeCUM As String Implements IProduct.CodeCUM
		Get
			Return INDtxtCUM.EditValue
		End Get
		Set(value As String)
			INDtxtCUM.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el codigo alternativo
	''' </summary>
	Public Property CodeAlternative As String Implements IProduct.CodeAlternative
		Get
			Return INDtxtCodeAlternative.EditValue
		End Get
		Set(value As String)
			INDtxtCodeAlternative.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el codigo alternativo dos
	''' </summary>
	Public Property CodeAlternativeTwo As String Implements IProduct.CodeAlternativeTwo
		Get
			Return INDtxtCodeAlternativeTwo.EditValue
		End Get
		Set(value As String)
			INDtxtCodeAlternativeTwo.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece la descripcion larga del producto
	''' </summary>
	Public Property Description As String Implements IProduct.Description
		Get
			Return INDmeDescription.EditValue
		End Get
		Set(value As String)
			INDmeDescription.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el id del grupo del producto
	''' </summary>
	Public Property ProductGroupId As Integer? Implements IProduct.ProductGroupId
		Get
			Return CType(INDsleGroup.EditValue, Integer?)
		End Get
		Set(value As Integer?)
			INDsleGroup.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el id del subgrupo del producto
	''' </summary>
	Public Property ProductSubGroupId As Integer? Implements IProduct.ProductSubGroupId
		Get
			Return CType(INDsleSubGroup.EditValue, Integer?)
		End Get
		Set(value As Integer?)
			INDsleSubGroup.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el id de la unidad de empaques
	''' </summary>
	Public Property PackagingUnitId As Integer Implements IProduct.PackagingUnitId
		Get
			Return CType(INDslePackingUnit.EditValue, Integer)
		End Get
		Set(value As Integer)
			INDslePackingUnit.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el id del fabricante
	''' </summary>
	Public Property ManufacturerId As Integer? Implements IProduct.ManufacturerId
		Get
			Return CType(INDsleManufacturer.EditValue, Integer?)
		End Get
		Set(value As Integer?)
			INDsleManufacturer.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece la Osmolaridad
	''' </summary>
	Public Property Osmolarity As Decimal Implements IProduct.Osmolarity
		Get
			Return CType(INDseOsmolarity.EditValue, Decimal)
		End Get
		Set(value As Decimal)
			INDseOsmolarity.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el id del iva aplicado al producto
	''' </summary>
	Public Property IVAId As Integer? Implements IProduct.IVAId
		Get
			Return CType(INDsleIva.EditValue, Integer?)
		End Get
		Set(value As Integer?)
			INDsleIva.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece la presentación del producto
	''' </summary>
	Public Property ProductPresentation As String Implements IProduct.ProductPresentation
		Get
			Return INDtxtPresentation.EditValue
		End Get
		Set(value As String)
			INDtxtPresentation.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el codigo SICE del producto
	''' </summary>
	Public Property CodeSISE As String Implements IProduct.CodeSISE
		Get
			Return INDtxtSICE.EditValue
		End Get
		Set(value As String)
			INDtxtSICE.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece si maneja serial
	''' </summary>
	Public Property HandlesSerial As Boolean? Implements IProduct.HandlesSerial
		Get
			Return CType(INDgleHandleSerial.EditValue, Boolean?)
		End Get
		Set(value As Boolean?)
			INDgleHandleSerial.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece si el producto maneja registro sanitario
	''' </summary>
	Public Property HandlesHealthRegistration As Boolean? Implements IProduct.HandlesHealthRegistration
		Get
			Return CType(INDgleHandleHealthRegistration.EditValue, Boolean?)
		End Get
		Set(value As Boolean?)
			INDgleHandleHealthRegistration.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el registro sanitario del producto
	''' </summary>
	Public Property HealthRegistration As String Implements IProduct.HealthRegistration
		Get
			Return INDtxtHealthRegistration.EditValue
		End Get
		Set(value As String)
			INDtxtHealthRegistration.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el Numero de Serial
	''' </summary>
	Public Property SerialNumber As String Implements IProduct.SerialNumber
		Get
			Return INDtxtSerialNumber.Text
		End Get
		Set(value As String)
			INDtxtSerialNumber.Text = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece la fecha de vencimiento R.S del producto
	''' </summary>
	Public Property ExpirationDate As DateTime? Implements IProduct.ExpirationDate
		Get
			Return CType(INDdeExpirationDate.EditValue, DateTime?)
		End Get
		Set(value As DateTime?)
			INDdeExpirationDate.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el id del grupo de facturación
	''' </summary>
	Public Property BillingGroupId As Integer? Implements IProduct.BillingGroupId
		Get
			Return CType(INDsleBillingGroup.EditValue, Integer?)
		End Get
		Set(value As Integer?)
			INDsleBillingGroup.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece si el producto es de control
	''' </summary>
	Public Property ProductControl As Boolean? Implements IProduct.ProductControl
		Get
			Return CType(INDgleProductControl.EditValue, Boolean?)
		End Get
		Set(value As Boolean?)
			INDgleProductControl.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece si el producto tiene control de precios
	''' </summary>
	Public Property ProductWithPriceControl As Boolean? Implements IProduct.ProductWithPriceControl
		Get
			Return CType(INDglePriceControl.EditValue, Boolean?)
		End Get
		Set(value As Boolean?)
			INDglePriceControl.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece si el producto esta en el POS
	''' </summary>
	Public Property POSProduct As Boolean? Implements IProduct.POSProduct
		Get
			Return CType(INDglePBSProduct.EditValue, Boolean?)
		End Get
		Set(value As Boolean?)
			INDglePBSProduct.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el numero de autorizaciones por pedido
	''' </summary>
	Public Property AuthorizationByOrderNumber As Integer? Implements IProduct.AuthorizationByOrderNumber
		Get
			Return IIf(INDtxtAuthorizationNumber.EditValue Is Nothing, Nothing, CInt(INDtxtAuthorizationNumber.EditValue))
		End Get
		Set(value As Integer?)
			INDtxtAuthorizationNumber.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece los días de expiración del producto
	''' </summary>
	Public Property ExpirationDay As Integer? Implements IProduct.ExpirationDay
		Get
			Return IIf(INDtxtExpirationDay.EditValue Is Nothing, Nothing, CInt(INDtxtExpirationDay.EditValue))
		End Get
		Set(value As Integer?)
			INDtxtExpirationDay.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece si tiene control maximo por periodo
	''' </summary>
	Public Property MaximumControlPeriod As Boolean? Implements IProduct.MaximumControlPeriod
		Get
			Return CType(INDgleMaximumControlPeriod.EditValue, Boolean?)
		End Get
		Set(value As Boolean?)
			INDgleMaximumControlPeriod.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece los dias de control que va a tener
	''' </summary>
	Public Property ControlDays As Integer? Implements IProduct.ControlDays
		Get
			Return IIf(INDspnControlDays.EditValue Is Nothing, Nothing, CInt(INDspnControlDays.EditValue))
		End Get
		Set(value As Integer?)
			INDspnControlDays.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece si el producto tiene control de cantidad por orden
	''' </summary>
	Public Property ControlOrderQuantity As Boolean? Implements IProduct.ControlOrderQuantity
		Get
			Return CType(INDgleOrderQuantityControl.EditValue, Boolean?)
		End Get
		Set(value As Boolean?)
			INDgleOrderQuantityControl.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece la cantidad de producto por orden
	''' </summary>
	Public Property ProductOrderAmount As Integer? Implements IProduct.ProductOrderAmount
		Get
			Return IIf(INDspnProductOrderAmount.EditValue Is Nothing, Nothing, CInt(INDspnProductOrderAmount.EditValue))
		End Get
		Set(value As Integer?)
			INDspnProductOrderAmount.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece la ultima compra del producto
	''' </summary>
	Public Property LastPurchase As DateTime? Implements IProduct.LastPurchase
		Get
			Return CType(INDdeLastPurchase.EditValue, DateTime?)
		End Get
		Set(value As DateTime?)
			INDdeLastPurchase.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece la ultima venta del producto
	''' </summary>
	Public Property LastSale As DateTime? Implements IProduct.LastSale
		Get
			Return CType(INDdeLastSale.EditValue, DateTime?)
		End Get
		Set(value As DateTime?)
			INDdeLastSale.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el origen del producto
	''' </summary>
	Public Property ProductOrigin As Byte? Implements IProduct.ProductOrigin
		Get
			Return IIf(INDgleOrigin.EditValue Is Nothing, Nothing, CByte(INDgleOrigin.EditValue))
		End Get
		Set(value As Byte?)
			INDgleOrigin.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el stock minimo del producto
	''' </summary>
	Public Property MinimumStock As Integer? Implements IProduct.MinimumStock
		Get
			Return IIf(INDspnMinimumStrock.EditValue Is Nothing, Nothing, CInt(INDspnMinimumStrock.EditValue))
		End Get
		Set(value As Integer?)
			INDspnMinimumStrock.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el stock maximo del producto
	''' </summary>
	Public Property MaximumStock As Integer? Implements IProduct.MaximumStock
		Get
			Return IIf(INDspnMaximumStock.EditValue Is Nothing, Nothing, CInt(INDspnMaximumStock.EditValue))
		End Get
		Set(value As Integer?)
			INDspnMaximumStock.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el porcentaje de comision
	''' </summary>
	Public Property CommissionPercentage As Decimal? Implements IProduct.CommissionPercentage
		Get
			Return CType(INDspnCommissionPercent.EditValue, Decimal?)
		End Get
		Set(value As Decimal?)
			INDspnCommissionPercent.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el punto de reposicion
	''' </summary>
	Public Property RepositionPoint As Integer? Implements IProduct.RepositionPoint
		Get
			Return IIf(INDspnRepositionPoint.EditValue Is Nothing, Nothing, CInt(INDspnRepositionPoint.EditValue))
		End Get
		Set(value As Integer?)
			INDspnRepositionPoint.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el tiempo de reposicion
	''' </summary>
	Public Property ResetTime As Integer? Implements IProduct.ResetTime
		Get
			Return IIf(INDspnResetTime.EditValue Is Nothing, Nothing, CInt(INDspnResetTime.EditValue))
		End Get
		Set(value As Integer?)
			INDspnResetTime.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el tipo de moneda
	''' </summary>
	Public Property CurrencyType As Byte? Implements IProduct.CurrencyType
		Get
			Return IIf(INDsleCurrencyType.EditValue Is Nothing, Nothing, CByte(INDsleCurrencyType.EditValue))
		End Get
		Set(value As Byte?)
			INDsleCurrencyType.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el porcentaje de variación del costo promedio
	''' </summary>
	Public Property ControlCostPercentage As Decimal? Implements IProduct.ControlCostPercentage
		Get
			Return CType(INDspnControlCostPercent.EditValue, Decimal?)
		End Get
		Set(value As Decimal?)
			INDspnControlCostPercent.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el costo del producto
	''' </summary>
	Public Property ProductCost As Decimal? Implements IProduct.ProductCost
		Get
			Return CType(INDspnProductCost.EditValue, Decimal?)
		End Get
		Set(value As Decimal?)
			INDspnProductCost.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el precio del ultimo costo del producto
	''' </summary>
	Public Property FinalProductCost As Decimal? Implements IProduct.FinalProductCost
		Get
			Return CType(INDspnFinalProductCost.EditValue, Decimal?)
		End Get
		Set(value As Decimal?)
			INDspnFinalProductCost.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el precio de venta del producto
	''' </summary>
	Public Property SellingPrice As Decimal? Implements IProduct.SellingPrice
		Get
			Return CType(INDspnSellingPrice.EditValue, Decimal?)
		End Get
		Set(value As Decimal?)
			INDspnSellingPrice.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el estado del producto
	''' </summary>
	Public Property Status As Boolean Implements IProduct.Status
		Get
			Return BarraBotones.StatusRecord
		End Get
		Set(value As Boolean)
			If value = True Then
				Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
			Else
				Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
			End If
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece la unidad de manejo minimo que tendra el producto
	''' </summary>
	Public Property DriveUnit As Integer? Implements IProduct.DriveUnit
		Get
			Return IIf(INDtxtDriveUnit.EditValue Is Nothing, Nothing, CInt(INDtxtDriveUnit.EditValue))
		End Get
		Set(value As Integer?)
			INDtxtDriveUnit.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece la temperatura minima en la que se almacenara el producto
	''' </summary>
	Public Property MinimumTemperature As Integer? Implements IProduct.MinimumTemperature
		Get
			Return IIf(INDtxtTempMin.EditValue Is Nothing, Nothing, CInt(INDtxtTempMin.EditValue))
		End Get
		Set(value As Integer?)
			INDtxtTempMin.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece la temperatura maxima en la que se almacenara el producto
	''' </summary>
	Public Property MaximumTemperature As Integer? Implements IProduct.MaximumTemperature
		Get
			Return IIf(INDtxtTempMax.EditValue Is Nothing, Nothing, CInt(INDtxtTempMax.EditValue))
		End Get
		Set(value As Integer?)
			INDtxtTempMax.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el estado de registro sanitario
	''' </summary>
	Public Property SanitaryRegistration As Integer? Implements IProduct.SanitaryRegistration
		Get
			Return INDsleSanitaryRegistration.EditValue
		End Get
		Set(value As Integer?)
			INDsleSanitaryRegistration.EditValue = value
		End Set
	End Property
	''' <summary>
	''' Obtiene o establece la abreviatura del producto
	''' </summary>
	Public Property Abbreviation As String Implements IProduct.Abbreviation
		Get
			Return txtNickName.EditValue
		End Get
		Set(value As String)
			txtNickName.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el identificador unico del medicamento
	''' </summary>
	Public Property IUM As String Implements IProduct.IUM
		Get
			Return INDTxeIUM.EditValue
		End Get
		Set(value As String)
			INDTxeIUM.EditValue = value
		End Set
	End Property

    ''' <summary>
    ''' Obtiene o establece el almacenamiento del medicamento
    ''' </summary>
    Public Property Storage As Integer? Implements IProduct.Storage
        Get
            Return INDgleStorage.EditValue
        End Get
        Set(value As Integer?)
            INDgleStorage.EditValue = value
        End Set
    End Property

    Public Property TaxedProduct As Boolean Implements IProduct.TaxedProduct
		Get
			Return INDgleTaxedProduct.EditValue
		End Get
		Set(value As Boolean)
			INDgleTaxedProduct.EditValue = value
		End Set
	End Property

	''' <summary>
	''' propiedad que obtiene o establece la bandera de liquida Iva en ventas salud
	''' </summary>
	''' <returns></returns>
	Public Property LiquidateSalesTaxes As Boolean Implements IProduct.LiquidateSalesTaxes
		Get
			Return INDYesnotLiquidateSalesTaxes.EditValue
		End Get
		Set(value As Boolean)
			INDYesnotLiquidateSalesTaxes.EditValue = value
		End Set
	End Property
	''' <summary>
	'''  Obtiene o establece si el producto tiene Reporte SISMED
	''' </summary>
	''' <returns></returns>
	Public Property SismedReport As Boolean Implements IProduct.SismedReport
		Get
			Return CType(INDgleSismedReport.EditValue, Boolean)
		End Get
		Set(value As Boolean)
			INDgleSismedReport.EditValue = value
		End Set
	End Property

    ''' <summary>
    ''' Obtiene o establece si el producto es componente lácteo
    ''' </summary>
    Public Property IsDairyComponente As Boolean Implements IProduct.DairyComponent
        Get
            Return CType(INDrgDairyComponent.EditValue, Boolean)
        End Get
        Set(value As Boolean)
            INDrgDairyComponent.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del tipo de medicamento
    ''' </summary>
    ''' <returns></returns>
    Public Property MedicationTypeId As Integer? Implements IProduct.MedicationTypeId
        Get
            Return INDsleMedicationType.EditValue
        End Get
        Set(value As Integer?)
            INDsleMedicationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el peso de los insumos de tipo nutricion parenteral
    ''' </summary>
    Public Property WeightParenteralNutritionSupply As Decimal? Implements IProduct.WeightParenteralNutritionSupply
        Get
            Return CType(INDSeWeightSupplyNutrition.EditValue, Decimal?)
        End Get
        Set(value As Decimal?)
            INDSeWeightSupplyNutrition.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasources"
    Property MeasureUnitXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleMeasurementUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleMeasurementUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the billing group datasource.
    ''' </summary>
    Public Property BillingGroupDatasource As XPInstantFeedbackSource Implements IProduct.BillingGroupDatasource
		Get
			Return CType(INDsleBillingGroup.Properties.DataSource, XPInstantFeedbackSource)
		End Get
		Set(value As XPInstantFeedbackSource)
			INDsleBillingGroup.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el datasource de tipo de moneda
	''' </summary>
	Public Property CurrencyTypeDatasource As XPInstantFeedbackSource Implements IProduct.CurrencyTypeDatasource
		Get
			Return CType(INDsleCurrencyType.Properties.DataSource, XPInstantFeedbackSource)
		End Get
		Set(value As XPInstantFeedbackSource)
			INDsleCurrencyType.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el datasource de Grupo
	''' </summary>
	Public Property GroupDatasource As XPInstantFeedbackSource Implements IProduct.GroupDatasource
		Get
			Return CType(INDsleGroup.Properties.DataSource, XPInstantFeedbackSource)
		End Get
		Set(value As XPInstantFeedbackSource)
			INDsleGroup.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el datasource de iva
	''' </summary>
	Public Property IVADatasource As XPInstantFeedbackSource Implements IProduct.IVADatasource
		Get
			Return CType(INDsleIva.Properties.DataSource, XPInstantFeedbackSource)
		End Get
		Set(value As XPInstantFeedbackSource)
			INDsleIva.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el datasource de fabricante
	''' </summary>
	Public Property ManufacturerDatasource As XPInstantFeedbackSource Implements IProduct.ManufacturerDatasource
		Get
			Return CType(INDsleManufacturer.Properties.DataSource, XPInstantFeedbackSource)
		End Get
		Set(value As XPInstantFeedbackSource)
			INDsleManufacturer.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el datasource de Unidad de empaque
	''' </summary>
	Public Property PackingUnitDatasource As XPInstantFeedbackSource Implements IProduct.PackingUnitDatasource
		Get
			Return CType(INDslePackingUnit.Properties.DataSource, XPInstantFeedbackSource)
		End Get
		Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
			INDslePackingUnit.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el datasource de tipo de productos
	''' </summary>
	Public Property ProductTypeDatasource As XPInstantFeedbackSource Implements IProduct.ProductTypeDatasource
		Get
			Return CType(INDsleProductType.Properties.DataSource, XPInstantFeedbackSource)
		End Get
		Set(value As XPInstantFeedbackSource)
			INDsleProductType.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el datasource de SCA
	''' </summary>
	Public Property ATCDatasource As XPInstantFeedbackSource Implements IProduct.ATCDatasource
		Get
			Return CType(INDsleATC.Properties.DataSource, XPInstantFeedbackSource)
		End Get
		Set(value As XPInstantFeedbackSource)
			INDsleATC.Properties.DataSource = value
		End Set
	End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de niveles de riesgo
    ''' </summary>
    Public Property RiskLevelDatasource As XPInstantFeedbackSource Implements IProduct.RiskLevelDatasource
        Get
            Return CType(INDsleRiskLevel.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRiskLevel.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de temperaturas de almacenamiento
    ''' </summary>
    Public Property StorageTemperatureDatasource As XPInstantFeedbackSource Implements IProduct.StorageTemperatureDatasource
        Get
            Return CType(INDgleStorage.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDgleStorage.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de subgrupo
    ''' </summary>
    Public Property SubGroupDatasource As XPInstantFeedbackSource Implements IProduct.SubGroupDatasource
		Get
			Return CType(INDsleSubGroup.Properties.DataSource, XPInstantFeedbackSource)
		End Get
		Set(value As XPInstantFeedbackSource)
			INDsleSubGroup.Properties.DataSource = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el id del insumo
	''' </summary>
	Public Property SupplieId As Integer? Implements IProduct.SupplieId
		Get
			Return INDSleSupplie.EditValue
		End Get
		Set(value As Integer?)
			INDSleSupplie.EditValue = value
		End Set
	End Property

	''' <summary>
	''' Obtiene o establece el datasource de insumos
	''' </summary>
	Public Property SupplieDatasource As XPInstantFeedbackSource Implements IProduct.SupplieDatasource
		Get
			Return CType(INDSleSupplie.Properties.DataSource, XPInstantFeedbackSource)
		End Get
		Set(value As XPInstantFeedbackSource)
			INDSleSupplie.Properties.DataSource = value
		End Set
	End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de tipos de medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Property MedicationTypeDataSource As XPInstantFeedbackSource Implements IProduct.MedicationTypeDataSource
        Get
            Return TryCast(INDsleMedicationType.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMedicationType.Properties.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
		ctrTmp = Nothing
		_openPopUpATC = Nothing
		_openPopUpBillinfGroup = Nothing
		_openPopUpBillingGruposNoPos = Nothing
		_openPopUpGroup = Nothing
		_openPopUpIva = Nothing
		_openPopUpManufacturer = Nothing
		_openPopUpPackingUnit = Nothing
		_openPopUpPOSPatologie = Nothing
		_openPopUpProductType = Nothing
		_openPopUpRiskLevel = Nothing
		_openPopUpSubGroup = Nothing
		_isLoading = Nothing
		_sequence = Nothing
		_idCurrentSequence = Nothing
		_idOperativeUnit = Nothing
		_product = Nothing
		_searchMode = Nothing
		_presenter = Nothing
		_record = Nothing
		ListProductOrigin = Nothing
		ListCurrencyType = Nothing
		_configurationManageMS = Nothing
	End Sub

	''' <summary>
	''' Handles the Load event of the FrmProducts control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
	Private Sub FrmProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
		Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
		'****Inicializar variables*****'
		_isLoading = True
		Me._doc = Nothing
		Me._funct = AddressOf GenerateDoc
		Me.indigo = SessionValues.Instance
		_presenter = New PInventoryProduct(Me)
		_presenter.GetSequense()
		_presenter.LoadDefinitionLayout()
		_presenter.LoadPermissionsForm(Me.Tag)
		_handleMixingStation = ValidationMixingStation()

		Using model As New MInventoryProduct(MyTag)
			MeasureUnitXPO = model.ListMeasureUnit()
		End Using

		If FormSearchObjects Is Nothing Then
			_searchMode = False
		End If

		InitializeTuple()
		CreateListProductOrigin()
		CreateListCurrencyType()
		LoadStatus()
        Deshacer()
        'Inicializa el DataSource de MedicationType para que el control pueda mostrar el valor correctamente
        _presenter.InitializeMedicationType()
        _isLoading = False
	End Sub


	''' <summary>
	''' Handles the FormClosing event of the FrmProducts control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
	Private Sub FrmProducts_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
		DeleteBlockedRecord()
	End Sub

#End Region

#Region "KeyDown"
	''' <summary>
	''' Handles the KeyDown event of the INDbteCode control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
	Private Async Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
		If e.KeyCode = System.Windows.Forms.Keys.Enter Then
			If _sequence Is Nothing OrElse _sequence.Id = 0 Then
				Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
				Exit Sub
			End If
			If Me._sequence.IsManual Then
				If Not String.IsNullOrEmpty(Code.Trim()) Then
					Await Me.LoadControls()
				Else
					Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
				End If
			Else
				If String.IsNullOrEmpty(Code) Then
					Await Me.NewProduct()
				Else
					Await Me.LoadControls()
				End If
			End If
		ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
			OpenSearch()
		End If
	End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' evento para consultar los tipos de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMedicationType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMedicationType.QueryPopUp
        If Not _openPopUpMedicationType Then
            _presenter.InitializeMedicationType()
            _openPopUpMedicationType = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de insumos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplie_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleSupplie.QueryPopUp
		If INDSleSupplie.Properties.DataSource Is Nothing Then
			_presenter.InitializeSupplie()
		End If
	End Sub

	''' <summary>
	''' Handles the QueryPopUp event of the INDsleProductType control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleProductType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductType.QueryPopUp
		If Not _openPopUpProductType Then
			_presenter.InitializeProdutType()
			_openPopUpProductType = True
		End If
	End Sub

	''' <summary>
	''' Handles the QueryPopUp event of the INDsleSCA control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleATC_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATC.QueryPopUp
		If Not _openPopUpATC Then
			_presenter.InitializeATC()
			_openPopUpATC = True
		End If
	End Sub

	''' <summary>
	''' Handles the QueryPopUp event of the INDsleRiskLevel control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleRiskLevel_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRiskLevel.QueryPopUp
		If Not _openPopUpRiskLevel Then
			_presenter.InitializeRiskLevel()
			_openPopUpRiskLevel = True
		End If
	End Sub

	''' <summary>
	''' Handles the QueryPopUp event of the INDslePackingUnit control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
	Private Sub INDslePackingUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePackingUnit.QueryPopUp
		If Not _openPopUpPackingUnit Then
			_presenter.InitializePackingUnit()
			_openPopUpPackingUnit = True
		End If
	End Sub

	''' <summary>
	''' Handles the QueryPopUp event of the INDsleGroup control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleGroup.QueryPopUp
		If Not _openPopUpGroup Then
			_presenter.InitializeGroups()
			_openPopUpGroup = True
		End If
	End Sub

	''' <summary>
	''' Handles the QueryPopUp event of the INDsleSubGroup control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleSubGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSubGroup.QueryPopUp
		If Not _openPopUpSubGroup Then
			_presenter.InitializeSubGroups()
			_openPopUpSubGroup = True
		End If
	End Sub

	''' <summary>
	''' Handles the QueryPopUp event of the INDsleManufacturer control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleManufacturer_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleManufacturer.QueryPopUp
		If Not _openPopUpManufacturer Then
			_presenter.InitializeManufacturer()
			_openPopUpManufacturer = True
		End If
	End Sub

	''' <summary>
	''' Handles the QueryPopUp event of the INDsleIva control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleIva_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIva.QueryPopUp
		If Not _openPopUpIva Then
			_presenter.InitializeIva()
			_openPopUpIva = True
		End If
	End Sub

	''' <summary>
	''' Handles the QueryPopUp event of the INDsleBillingGroup control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleBillingGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBillingGroup.QueryPopUp
		If Not _openPopUpBillinfGroup Then
			_presenter.InitializeBillingGroup()
			_openPopUpBillinfGroup = True
		End If
	End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMedicationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMedicationType.EditValueChanged
        If Me.MedicationTypeId Is Nothing OrElse Me._isLoading Then
            Exit Sub
        End If

        Dim product = New InventoryProduct With {.ATCId = Me.ATCId, .HandlesHealthRegistration = Me.HandlesHealthRegistration, .MedicationTypeId = Me.MedicationTypeId, .HealthRegistration = Me.HealthRegistration}

        Task.Run(Async Function()
                     Dim result = Await _presenter.ValidateMedicationTypeByProduct(product)

                     If result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.Message) Then

                         Me.SafeInvoke(Sub()
                                           Me.Mensaje(If(result.StateResult, EeventViewerImages.Informacion, EeventViewerImages.Advertencia)) = result?.Message
                                       End Sub)
                     End If
                 End Function)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de forma farmaceutica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleSupplie_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupplie.EditValueChanged
        If INDSleSupplie.EditValue IsNot Nothing Then
            Dim info = Await _presenter.GetSupplieById(INDSleSupplie.EditValue)
            If info IsNot Nothing Then
                InventoryRiskLevelId = info.RiskLevelId.Id

                Dim r As InventoryRiskLevelXpo = _presenter.GetRiskLevelById(info.RiskLevelId.Id)
                If r IsNot Nothing Then
                    INDsleRiskLevel.Properties.NullText = String.Format("{0} - {1}", r.Code, r.Name)
                End If

                If info.IsParenteralNutritionSupply Then
                    INDliWeightSupplyNutrition.ShowLayout()
                Else
                    INDliWeightSupplyNutrition.HideLayout()
                End If

                POSProduct = info.PBSProduct
                INDglePBSProduct.Properties.NullText = IIf(info.PBSProduct, "Si", "No")
                INDsleJustification.EditValue = info.JustificationOfInputs
                INDsleOsteosynthesis.EditValue = info.OsteosynthesisMaterial
                INDsleConsumption.EditValue = info.Consumption
            End If
            End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleATC_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleATC.EditValueChanged
        If ATCId IsNot Nothing AndAlso Not _isLoading Then
            MedicamentXpo = Await _presenter.GetMedicamentById(ATCId)
            Dim riskLevelXpo = _presenter.GetRiskLevelById(MedicamentXpo.InventoryRiskLevelId)
            INDsleRiskLevel.EditValue = MedicamentXpo.InventoryRiskLevelId
            INDsleRiskLevel.Properties.NullText = riskLevelXpo.CodeName
            INDglePBSProduct.EditValue = MedicamentXpo.POSProduct
            INDtxtCodeAlternative.EditValue = MedicamentXpo.ATCEntityId.Code
            INDtxtPresentation.EditValue = MedicamentXpo.Presentations
            INDsleConsumption.EditValue = MedicamentXpo.Consumption
            INDseOsmolarity.EditValue = MedicamentXpo.Osmolarity
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDgleMaximumControlPeriod control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgleMaximumControlPeriod_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleMaximumControlPeriod.EditValueChanged
        If MaximumControlPeriod IsNot Nothing Then
            If MaximumControlPeriod Then
                INDliControlDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDliControlDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                ControlDays = 0
                INDspnControlDays.EditValue = 0
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDgleOrderQuantityControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgleOrderQuantityControl_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleOrderQuantityControl.EditValueChanged
        If ControlOrderQuantity IsNot Nothing Then
            If ControlOrderQuantity Then
                INDliProductOrderAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDliProductOrderAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                ProductOrderAmount = 0
                INDspnProductOrderAmount.EditValue = 0
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleProductType control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleProductType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProductType.EditValueChanged
		If ProductTypeId <> 0 Then
			loadProductTypeAttributes()
			Dim _producttype As Object = Nothing
			If _openPopUpProductType Then
				_producttype = CType(CType(INDgvProductType.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.ProductTypeXpo)
			Else
				Using Model As New MProductType(Me.Tag)
					_producttype = (Await Model.GetProductTypeById(ProductTypeId)).ObjectEmbbeded
				End Using
			End If
			_handleTemperatureRange = If(_producttype.TemperatureRange Is Nothing, False, _producttype.TemperatureRange)
			SetListActions()
			Me.ProductTypeClass = _producttype.Class
			ShowLayouts(_producttype.Class)

			If _producttype.Class = eProductTypeClass.ItemInsumo Or _producttype.Class = eProductTypeClass.ItemOtro Or _producttype.Class = eProductTypeClass.ItemProduccion Then

				If _producttype.Class = eProductTypeClass.ItemInsumo Then
					INDLciSupplie.Visibility = LayoutVisibility.Always
					INDLciIUM.Visibility = LayoutVisibility.Always
					INDsleJustification.Properties.ReadOnly = True
					INDsleOsteosynthesis.Properties.ReadOnly = True
					INDsleConsumption.Properties.ReadOnly = True
					INDliSismedReport.Visibility = LayoutVisibility.Always
				Else
					INDLciSupplie.Visibility = LayoutVisibility.Never
                    SupplieId = Nothing
                    INDliWeightSupplyNutrition.HideLayout()
                    INDLciIUM.Visibility = LayoutVisibility.Never
					IUM = Nothing
					INDsleJustification.Properties.ReadOnly = False
					INDsleOsteosynthesis.Properties.ReadOnly = False
					INDsleConsumption.Properties.ReadOnly = False
					INDliSismedReport.Visibility = LayoutVisibility.Never
				End If

				If _producttype.Class = eProductTypeClass.ItemProduccion Then
					INDliMaximumControlPeriod.Visibility = LayoutVisibility.Never
					INDliOrderQuantityControl.Visibility = LayoutVisibility.Never
				Else
					INDliAuthorizationNumber.Visibility = LayoutVisibility.Always
					INDliMaximumControlPeriod.Visibility = LayoutVisibility.Always
					INDliOrderQuantityControl.Visibility = LayoutVisibility.Always
					INDliControlDays.Visibility = LayoutVisibility.Always
					INDliProductOrderAmount.Visibility = LayoutVisibility.Always
				End If

				INDliExpirationDay.Visibility = LayoutVisibility.Always
				INDliSICE.Visibility = LayoutVisibility.Never
				INDliCommissionPercent.Visibility = LayoutVisibility.Never
				INDliRepositionPoint.Visibility = LayoutVisibility.Never
				INDliResetTime.Visibility = LayoutVisibility.Never

				CodeSISE = Nothing
				CommissionPercentage = Nothing
				RepositionPoint = Nothing
				ResetTime = Nothing

				If Me.indigo.Culture.Name <> "es-CO" Then
					INDliCodeAlternative.Text = "CABYS"
					INDliCodeAlternativeTo.HideLayout()
				Else
					INDliCodeAlternative.Text = "Código Alterno 1"
					INDliCodeAlternativeTo.Text = "Código Alterno 2"
				End If
				INDLciMeasurementUnit.Visibility = LayoutVisibility.Always
				INDLciMeasurementUnit.AllowHide = False
				INDLciMeasurementUnit.ShowInCustomizationForm = False

				If (_handleMixingStation) Then
					INDliDriveUnit.Visibility = LayoutVisibility.Always
					INDliDriveUnit.AllowHide = False
					INDliDriveUnit.ShowInCustomizationForm = False
				Else
					INDtxtDriveUnit.EditValue = Nothing
					INDliDriveUnit.Visibility = LayoutVisibility.Never
					INDliDriveUnit.AllowHide = True
					INDliDriveUnit.ShowInCustomizationForm = True
				End If

			Else
				INDLciIUM.Visibility = LayoutVisibility.Always
				INDLciSupplie.Visibility = LayoutVisibility.Never
                SupplieId = Nothing
                INDliWeightSupplyNutrition.HideLayout()
                INDsleJustification.Properties.ReadOnly = False
				INDsleOsteosynthesis.Properties.ReadOnly = False
				INDsleConsumption.Properties.ReadOnly = False
				INDliAuthorizationNumber.Visibility = LayoutVisibility.Never
				INDliExpirationDay.Visibility = LayoutVisibility.Never
				INDliMaximumControlPeriod.Visibility = LayoutVisibility.Never
				INDliOrderQuantityControl.Visibility = LayoutVisibility.Never
				INDliCommissionPercent.Visibility = LayoutVisibility.Never
				INDliRepositionPoint.Visibility = LayoutVisibility.Never
				INDliResetTime.Visibility = LayoutVisibility.Never
				INDliControlDays.Visibility = LayoutVisibility.Never
				INDliProductOrderAmount.Visibility = LayoutVisibility.Never
				INDliSismedReport.Visibility = LayoutVisibility.Never
				AuthorizationByOrderNumber = Nothing
				ExpirationDay = Nothing
				MaximumControlPeriod = Nothing
				ControlOrderQuantity = Nothing
				CommissionPercentage = Nothing
				RepositionPoint = Nothing
				ResetTime = Nothing
				ControlDays = Nothing
				ProductOrderAmount = Nothing

				INDliCodeAlternative.Text = "A.T.C"
				If Me.indigo.Culture.Name <> "es-CO" Then
					INDliCodeAlternativeTo.Text = "CABYS"

					INDliCUM.Visibility = LayoutVisibility.Never
					CodeCUM = "100-10"
					INDliSICE.Visibility = LayoutVisibility.Never
					CodeSISE = "0"
				Else
					INDliCodeAlternativeTo.Text = "Código Alterno"
					INDliCUM.Visibility = LayoutVisibility.Always
					INDliSICE.Visibility = LayoutVisibility.Always
				End If
				INDSleMeasurementUnit.EditValue = Nothing
				INDLciMeasurementUnit.Visibility = LayoutVisibility.Never
				INDLciMeasurementUnit.AllowHide = True
				INDLciMeasurementUnit.ShowInCustomizationForm = True

				INDtxtDriveUnit.EditValue = Nothing
				INDliDriveUnit.Visibility = LayoutVisibility.Never
				INDliDriveUnit.AllowHide = True
				INDliDriveUnit.ShowInCustomizationForm = True

				INDPictETempMax.EditValue = Nothing
				INDliPictureTempMax.Visibility = LayoutVisibility.Never
				INDliPictureTempMax.AllowHide = True
				INDliPictureTempMax.ShowInCustomizationForm = True

				If Not _handleTemperatureRange Then INDtxtTempMax.EditValue = Nothing
				INDliTempMax.Visibility = LayoutVisibility.Never
				INDliTempMax.AllowHide = True
				INDliTempMax.ShowInCustomizationForm = True

				INDPictETempMin.EditValue = Nothing
				INDliPictureTempMin.Visibility = LayoutVisibility.Never
				INDliPictureTempMin.AllowHide = True
				INDliPictureTempMin.ShowInCustomizationForm = True

				If Not _handleTemperatureRange Then INDtxtTempMin.EditValue = Nothing
				INDliTempMin.Visibility = LayoutVisibility.Never
				INDliTempMin.AllowHide = True
				INDliTempMin.ShowInCustomizationForm = True

			End If

			If (_handleTemperatureRange) Then

				INDliPictureTempMax.Visibility = LayoutVisibility.Always
				INDliPictureTempMax.AllowHide = False
				INDliPictureTempMax.ShowInCustomizationForm = False

				INDliTempMax.Visibility = LayoutVisibility.Always
				INDliTempMax.AllowHide = False
				INDliTempMax.ShowInCustomizationForm = False

				INDliPictureTempMin.Visibility = LayoutVisibility.Always
				INDliPictureTempMin.AllowHide = False
				INDliPictureTempMin.ShowInCustomizationForm = False

				INDliTempMin.Visibility = LayoutVisibility.Always
				INDliTempMin.AllowHide = False
				INDliTempMin.ShowInCustomizationForm = False

				ValidationColors(False, Me._product)
			Else
				INDPictETempMax.EditValue = Nothing
				INDliPictureTempMax.Visibility = LayoutVisibility.Never
				INDliPictureTempMax.AllowHide = True
				INDliPictureTempMax.ShowInCustomizationForm = True

				INDtxtTempMax.EditValue = Nothing
				INDliTempMax.Visibility = LayoutVisibility.Never
				INDliTempMax.AllowHide = True
				INDliTempMax.ShowInCustomizationForm = True

				INDPictETempMin.EditValue = Nothing
				INDliPictureTempMin.Visibility = LayoutVisibility.Never
				INDliPictureTempMin.AllowHide = True
				INDliPictureTempMin.ShowInCustomizationForm = True

				INDtxtTempMin.EditValue = Nothing
				INDliTempMin.Visibility = LayoutVisibility.Never
				INDliTempMin.AllowHide = True
				INDliTempMin.ShowInCustomizationForm = True
			End If
			ShowPOSProduct()
			ShowCurrencyType()
		Else
			ClearAttributes()
			ProductAttributes.Clear()
		End If
	End Sub

	Private Sub ClearAttributes()
		If INDLcgCustomAttributes.Items.Count > 0 Then

			While INDLcgCustomAttributes.Items.Count > 0
				Dim lci As LayoutControlItem = CType(INDLcgCustomAttributes.Items(0), LayoutControlItem)
				lci.Parent.Remove(lci)
				lci.Control.Dispose()
			End While

		End If
		INDLcgCustomAttributes.Visibility = LayoutVisibility.Never
	End Sub

	Private Sub loadProductTypeAttributes()
		If _isLoading OrElse ProductTypeId = 0 Then
			Exit Sub
		End If

		If ProductAttributes Is Nothing Then
			ProductAttributes = New List(Of Tuple(Of Integer, Object))()
		End If

		ProductAttributes.Clear()
		Using model As New MAttributeProductType(Me.MyTag)
			Dim attributes As XPCollection(Of AttributeProductTypeXpo) = model.ListAttibutesInventoryProductByProductTypeId(ProductTypeId, True)
			SetListActions()
			If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
				INDLcgCustomAttributes.Visibility = LayoutVisibility.Always
				Me.SuspendLayout()
				Dim newControl As Control = Nothing
				'Mostramos el grupo de Atributos
				For Each att In attributes

					Select Case att.DataType
						Case 1
							newControl = AddDateControl(att.Name, att.Id)
						Case 2
							newControl = AddNumberControl(att.Name, att.Id)
						Case 3
							newControl = AddTextControl(att.Name, att.Id)
						Case 4
							newControl = AddSelectControl(att.Name, att.Id)
					End Select

					If newControl IsNot Nothing Then
						Dim LciTxtControl As DevExpress.XtraLayout.LayoutControlItem = INDLcgCustomAttributes.AddItem(att.Name, newControl)
						CType(LciTxtControl, System.ComponentModel.ISupportInitialize).BeginInit()
						LciTxtControl.Location = New Point(0, 36 * (INDLcgCustomAttributes.Items.Count - 1))
						LciTxtControl.MaxSize = New System.Drawing.Size(390, 36)
						LciTxtControl.MinSize = New System.Drawing.Size(390, 36)
						LciTxtControl.Name = $"INDLci_{att.Id}"
						LciTxtControl.Size = New System.Drawing.Size(390, 483)
						LciTxtControl.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
						LciTxtControl.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
						LciTxtControl.TextSize = New System.Drawing.Size(135, 21)
						LciTxtControl.TextToControlDistance = 5
						CType(LciTxtControl, System.ComponentModel.ISupportInitialize).EndInit()
					End If
				Next
				Me.ResumeLayout()
			Else
				ClearAttributes()
				ProductAttributes.Clear()
			End If
		End Using
	End Sub

	''' <summary>
	''' The product attributes AttributeProductTypeId, Control with contains Value
	''' </summary>
	Private ProductAttributes As List(Of Tuple(Of Integer, Object))

	Private Function AddTextControl(name As String, id As Integer) As DevExpress.XtraEditors.TextEdit
		Dim TxtControl As New DevExpress.XtraEditors.TextEdit()
		CType(TxtControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.INDlycRoot.Controls.Add(TxtControl)
		Me.IndigoTextEdit1.SetApplyStyle(TxtControl, True)
		Me.IndigoTextEdit1.SetCampoObligatorio(TxtControl, False)
		Me.IndigoTextEdit1.SetMascara(TxtControl, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
		TxtControl.Name = $"INDTxt_{id}"
		TxtControl.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
		TxtControl.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
		TxtControl.Properties.Appearance.Options.UseBackColor = True
		TxtControl.Properties.Appearance.Options.UseFont = True
		TxtControl.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
		TxtControl.Properties.AppearanceFocused.Options.UseFont = True
		TxtControl.EnterMoveNextControl = True
		TxtControl.StyleController = Me.INDlycRoot
		TxtControl.Size = New System.Drawing.Size(246, 28)
		Me.IndigoTextEdit1.SetTamañoMinimoString(TxtControl, 0)

		If _product IsNot Nothing AndAlso _product.InventoryProductAttribute.Any(Function(o) o.AttributeProductTypeId = id) Then
			TxtControl.EditValue = _product.InventoryProductAttribute.FirstOrDefault(Function(o) o.AttributeProductTypeId = id).Value
		End If

		CType(TxtControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		ProductAttributes.Add(New Tuple(Of Integer, Object)(id, TxtControl))
		Return TxtControl
	End Function

	Private Function AddNumberControl(name As String, id As Integer) As DevExpress.XtraEditors.SpinEdit
		Dim SpnControl As New DevExpress.XtraEditors.SpinEdit()
		CType(SpnControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.INDlycRoot.Controls.Add(SpnControl)
		Me.IndigoTextEdit1.SetApplyStyle(SpnControl, True)
		Me.IndigoTextEdit1.SetCampoObligatorio(SpnControl, False)
		SpnControl.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
		Me.IndigoTextEdit1.SetMascara(SpnControl, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
		SpnControl.Name = $"INDSpn_{id}"
		SpnControl.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
		SpnControl.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
		SpnControl.Properties.Appearance.Options.UseBackColor = True
		SpnControl.Properties.Appearance.Options.UseFont = True
		SpnControl.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
		SpnControl.Properties.AppearanceFocused.Options.UseFont = True
		SpnControl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		SpnControl.EnterMoveNextControl = True
		SpnControl.StyleController = Me.INDlycRoot
		SpnControl.Size = New System.Drawing.Size(246, 28)
		Me.IndigoTextEdit1.SetTamañoMinimoString(SpnControl, 0)

		If _product IsNot Nothing AndAlso _product.InventoryProductAttribute.Any(Function(o) o.AttributeProductTypeId = id) Then
			SpnControl.EditValue = _product.InventoryProductAttribute.FirstOrDefault(Function(o) o.AttributeProductTypeId = id).Value
		End If

		CType(SpnControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		ProductAttributes.Add(New Tuple(Of Integer, Object)(id, SpnControl))
		Return SpnControl
	End Function

	Private Function AddDateControl(name As String, id As Integer) As DevExpress.XtraEditors.DateEdit
		Dim DateControl As New DevExpress.XtraEditors.DateEdit()
		CType(DateControl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(DateControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()

		Me.INDlycRoot.Controls.Add(DateControl)
		Me.IndigoTextEdit1.SetApplyStyle(DateControl, True)
		Me.IndigoDate1.SetCampoObligatorio(DateControl, False)
		Me.IndigoTextEdit1.SetCampoObligatorio(DateControl, False)
		DateControl.EditValue = Nothing
		Me.IndigoTextEdit1.SetMascara(DateControl, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
		Me.IndigoDate1.SetMascaraDate(DateControl, IndigoDate.EMask.Fecha)
		DateControl.Name = $"INDDte_{id}"
		DateControl.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
		DateControl.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
		DateControl.Properties.Appearance.Options.UseBackColor = True
		DateControl.Properties.Appearance.Options.UseFont = True
		DateControl.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
		DateControl.Properties.AppearanceFocused.Options.UseFont = True
		DateControl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		DateControl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		DateControl.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
		DateControl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
		DateControl.Properties.Mask.UseMaskAsDisplayFormat = True
		DateControl.EnterMoveNextControl = True
		DateControl.StyleController = Me.INDlycRoot
		DateControl.Size = New System.Drawing.Size(246, 28)
		Me.IndigoTextEdit1.SetTamañoMinimoString(DateControl, 0)

		If _product IsNot Nothing AndAlso _product.InventoryProductAttribute.Any(Function(o) o.AttributeProductTypeId = id) Then
			DateControl.EditValue = CDate(_product.InventoryProductAttribute.FirstOrDefault(Function(o) o.AttributeProductTypeId = id).Value)
		End If

		CType(DateControl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(DateControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()

		ProductAttributes.Add(New Tuple(Of Integer, Object)(id, DateControl))
		Return DateControl
	End Function

	Private Function AddSelectControl(name As String, id As Integer) As DevExpress.XtraEditors.SearchLookUpEdit
		Using model As New MAttributeProductType(Me.MyTag)
			Dim attributes As XPCollection(Of AttributeProductTypeOptionListXpo) = model.ListAttibutesOptionListByAttributeProductTypeId(id)
			If attributes IsNot Nothing AndAlso attributes.Count > 0 Then

				Dim GlcControl As New DevExpress.XtraEditors.SearchLookUpEdit()
				Dim GlcControlView = New DevExpress.XtraGrid.Views.Grid.GridView()
				CType(GlcControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
				CType(GlcControlView, System.ComponentModel.ISupportInitialize).BeginInit()

				Me.INDlycRoot.Controls.Add(GlcControl)
				Me.IndigoTextEdit1.SetApplyStyle(GlcControl, True)
				Me.IndigoTextEdit1.SetCampoObligatorio(GlcControl, False)
				Me.IndigoTextEdit1.SetMascara(GlcControl, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
				GlcControl.Name = $"INDGle_{id}"
				GlcControl.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
				GlcControl.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
				GlcControl.Properties.Appearance.Options.UseBackColor = True
				GlcControl.Properties.Appearance.Options.UseFont = True
				GlcControl.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
				GlcControl.Properties.AppearanceFocused.Options.UseFont = True
				GlcControl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
				GlcControl.Properties.NullText = ""
				GlcControl.Properties.DisplayMember = "CodeName"
				GlcControl.Properties.ValueMember = "Id"
				GlcControl.Properties.View = GlcControlView
				GlcControl.EnterMoveNextControl = True
				GlcControl.StyleController = Me.INDlycRoot
				GlcControl.Size = New System.Drawing.Size(246, 28)
				Me.IndigoTextEdit1.SetTamañoMinimoString(GlcControl, 0)

				Dim GColCode As New DevExpress.XtraGrid.Columns.GridColumn()
				GColCode.Caption = "Código"
				GColCode.FieldName = "Code"
				GColCode.Name = $"INDColCode_{id}"
				GColCode.Visible = True

				Dim GColName As New DevExpress.XtraGrid.Columns.GridColumn()
				GColName.Caption = "Nombre"
				GColName.FieldName = "Name"
				GColName.Name = $"INDColName_{id}"
				GColName.Visible = True

				GlcControlView.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
				GlcControlView.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
				GlcControlView.Appearance.FocusedRow.Options.UseBorderColor = True
				GlcControlView.Appearance.FocusedRow.Options.UseFont = True
				GlcControlView.Appearance.FocusedRow.Options.UseForeColor = True
				GlcControlView.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
				GlcControlView.Appearance.GroupRow.Options.UseFont = True
				GlcControlView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
				GlcControlView.Appearance.HeaderPanel.Options.UseFont = True
				GlcControlView.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
				GlcControlView.Appearance.Row.Options.UseFont = True
				GlcControlView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
				GlcControlView.Name = $"INDGv_{id}"
				GlcControlView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {GColCode, GColName})
				GlcControlView.OptionsSelection.EnableAppearanceFocusedCell = False
				GlcControlView.OptionsView.EnableAppearanceEvenRow = True
				GlcControlView.OptionsView.EnableAppearanceOddRow = True
				GlcControlView.OptionsView.ShowAutoFilterRow = True
				GlcControlView.OptionsView.ShowGroupPanel = False
				Me.IndigoGridView1.SetTemaIndigoMetro(GlcControlView, False)

				GlcControl.Properties.DataSource = attributes

				If _product IsNot Nothing AndAlso _product.InventoryProductAttribute.Any(Function(o) o.AttributeProductTypeId = id) Then
					GlcControl.EditValue = _product.InventoryProductAttribute.FirstOrDefault(Function(o) o.AttributeProductTypeId = id).Value
				End If

				CType(GlcControlView, System.ComponentModel.ISupportInitialize).EndInit()
				CType(GlcControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()

				ProductAttributes.Add(New Tuple(Of Integer, Object)(id, GlcControl))

				Return GlcControl
			End If
		End Using
	End Function

	''' <summary>
	''' Handles the EditValueChanged event of the INDglePOSProduct control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
	Private Sub INDglePOSProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDglePBSProduct.EditValueChanged
		If INDglePBSProduct.EditValue IsNot Nothing Then
			If INDglePBSProduct.EditValue = True Then
				INDglePBSProduct.Properties.Buttons(1).Visible = True
			Else
				INDglePBSProduct.Properties.Buttons(1).Visible = False
				Me._product.BillingGroupNoPosId = Nothing
				Me._product.AllPOSPathologies = Nothing
			End If
		End If
	End Sub
	Private Sub INDglePOSProduct_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDglePBSProduct.EditValueChanging
		If Not _isLoading AndAlso e.NewValue = False Then
			If _product.POSPathologies.Count > 0 OrElse (_product.AllPOSPathologies.HasValue AndAlso _product.AllPOSPathologies.Value) Then
				e.Cancel = True
				Me.Mensaje(EeventViewerImages.Advertencia) = "Para cambiar el tipo de producto debe eliminar primero las aclaraciones asociadas"
			End If
		End If
	End Sub
	Private Sub INDglePOSProduct_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDglePBSProduct.ButtonClick
		If e.Button.Kind = ButtonPredefines.Glyph Then 'Click en el botón de aclaracion
			Dim frmPatologies As New FrmPopUpPosPatologies(Me._product, IIf(INDliATC.Visibility = LayoutVisibility.Always, MedicamentXpo, Nothing))
			Using Model As New MBusqueda
				frmPatologies.BillingGroupNoPOSDatasource = Model.ConsultarEntidades(eDataSource.ListBillingGroup)
				frmPatologies.Patologies = Model.ConsultarEntidades(eDataSource.ListDiagnostic)
			End Using
			Using tras As New FrmTransparent(frmPatologies, False)
				tras.ShowDialog(Me)
			End Using
		End If
	End Sub

	''' <summary>
	''' Handles the EditValueChanged event of the INDgleHandleHealthRegistration control.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Private Sub INDgleHandleHealthRegistration_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleHandleHealthRegistration.EditValueChanged
		If HandlesHealthRegistration IsNot Nothing Then
			If HandlesHealthRegistration Then
				INDliHealthRegistration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
				INDliExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
				INDlyItemSanitaryRegistration.Visibility = LayoutVisibility.Always
				INDliPictureSanitaryRegistration.Visibility = LayoutVisibility.Always
				INDliHealthRegistration.AllowHide = False
				INDliExpirationDate.AllowHide = False
				INDlyItemSanitaryRegistration.AllowHide = False
			Else
				INDliHealthRegistration.HideControl()
				INDliExpirationDate.HideControl()
				INDlyItemSanitaryRegistration.HideControl()
				INDliPictureSanitaryRegistration.HideControl()
				HealthRegistration = String.Empty
				INDtxtHealthRegistration.EditValue = String.Empty
				INDsleSanitaryRegistration.EditValue = Nothing
				ExpirationDate = Nothing
				INDdeExpirationDate.EditValue = Nothing
				INDPictESanitaryRegistration.EditValue = My.Resources.Resources.blanco
			End If
		Else
			INDliHealthRegistration.HideControl()
			INDliExpirationDate.HideControl()
			HealthRegistration = String.Empty
			INDtxtHealthRegistration.EditValue = String.Empty
			ExpirationDate = Nothing
			INDdeExpirationDate.EditValue = Nothing
			INDlyItemSanitaryRegistration.HideControl()
			INDsleSanitaryRegistration.EditValue = Nothing
			INDliPictureSanitaryRegistration.HideControl()
			INDPictESanitaryRegistration.EditValue = My.Resources.Resources.blanco
		End If
	End Sub

	''' <summary>
	''' Handles the EditValueChanged event of the INDgleHandleHealthRegistration control.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Private Sub INDgleHandleSerial_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleHandleSerial.EditValueChanged
		If HandlesSerial IsNot Nothing Then
			If HandlesSerial Then
				INDliSerialNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
				INDliSerialNumber.AllowHide = False
			Else
				INDliSerialNumber.HideControl()

				SerialNumber = String.Empty
				INDtxtSerialNumber.Text = String.Empty
			End If
		Else
			INDliSerialNumber.HideControl()

			SerialNumber = String.Empty
			INDtxtSerialNumber.Text = String.Empty
		End If
	End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDrgDairyComponent control.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrgDairyComponent_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgDairyComponent.SelectedIndexChanged
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDtxtTempMax control.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtTempMax_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtTempMax.EditValueChanged
		If Not String.IsNullOrEmpty(INDtxtTempMax.Text.Trim()) Then
			ValidationColors(False, sender)
		End If

	End Sub

	''' <summary>
	''' Handles the EditValueChanged event of the INDtxtTempMin control.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Private Sub INDtxtTempMin_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtTempMin.EditValueChanged
		If Not String.IsNullOrEmpty(INDtxtTempMin.Text.Trim()) Then
			ValidationColors(False, sender)
		End If
	End Sub
	''' <summary>
	''' Handles the EditValueChanged event of the INDsleSanitaryRegistration control.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Private Sub INDsleSanitaryRegistration_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSanitaryRegistration.EditValueChanged
		If INDsleSanitaryRegistration.Text.Trim().Equals("[Vacío]") Then
			INDPictESanitaryRegistration.EditValue = My.Resources.Resources.blanco
			Exit Sub
		End If
		If Not String.IsNullOrEmpty(INDsleSanitaryRegistration.Text.Trim()) Then
			ValidationColors(False, sender)
		End If
	End Sub


#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleProductType control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductType.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Using Formulario As New FrmProductType
				Formulario.ViewModeEditHold = True
				Formulario.MinimizeBox = False
				Formulario.MaximizeBox = False
				Formulario.Size = New Size(780, 700)
				Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
				Dim transparent As New FrmTransparent(Formulario, False)
				transparent.ShowDialog(Me)
				_presenter.InitializeProdutType()
			End Using
		End If
	End Sub

	''' <summary>
	''' Handles the ButtonClick event of the INDslePackingUnit control.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Private Sub INDslePackingUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePackingUnit.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Using Formulario As New FrmPackagingUnit
				Formulario.ViewModeEditHold = True
				Formulario.MinimizeBox = False
				Formulario.MaximizeBox = False
				Formulario.Size = New Size(780, 700)
				Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
				Dim transparent As New FrmTransparent(Formulario, False)
				transparent.ShowDialog(Me)
				_presenter.InitializePackingUnit()
			End Using
		End If
	End Sub

	''' <summary>
	''' Handles the ButtonClick event of the INDsleATC control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleATC_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleATC.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Using Formulario As New FrmATC
				Formulario.ViewModeEditHold = True
				Formulario.MinimizeBox = False
				Formulario.MaximizeBox = False
				Formulario.Size = New Size(780, 700)
				Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
				Dim transparent As New FrmTransparent(Formulario, False)
				transparent.ShowDialog(Me)
				_presenter.InitializeATC()
			End Using
		End If
	End Sub

	''' <summary>
	''' Handles the ButtonClick event of the INDsleRiskLevel control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleRiskLevel_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRiskLevel.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Using Formulario As New FrmInventoryRiskLevel
				Formulario.ViewModeEditHold = True
				Formulario.MinimizeBox = False
				Formulario.MaximizeBox = False
				Formulario.Size = New Size(780, 700)
				Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
				Dim transparent As New FrmTransparent(Formulario, False)
				transparent.ShowDialog(Me)
				_presenter.InitializeRiskLevel()
			End Using
		End If
	End Sub

	''' <summary>
	''' Handles the ButtonClick event of the INDsleGroup control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleGroup.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Using Formulario As New FrmGroup
				Formulario.ViewModeEditHold = True
				Formulario.MinimizeBox = False
				Formulario.MaximizeBox = False
				Formulario.Size = New Size(780, 700)
				Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
				Dim transparent As New FrmTransparent(Formulario, False)
				transparent.ShowDialog(Me)
				_presenter.InitializeGroups()
			End Using
		End If
	End Sub

	''' <summary>
	''' Handles the ButtonClick event of the INDsleSubGroup control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleSubGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSubGroup.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Using Formulario As New FrmSubGroup
				Formulario.ViewModeEditHold = True
				Formulario.MinimizeBox = False
				Formulario.MaximizeBox = False
				Formulario.Size = New Size(780, 700)
				Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
				Dim transparent As New FrmTransparent(Formulario, False)
				transparent.ShowDialog(Me)
				_presenter.InitializeSubGroups()
			End Using
		End If
	End Sub

	''' <summary>
	''' Handles the ButtonClick event of the INDsleManufacturer control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleManufacturer_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleManufacturer.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Using Formulario As New FrmManufacturers
				Formulario.ViewModeEditHold = True
				Formulario.MinimizeBox = False
				Formulario.MaximizeBox = False
				Formulario.Size = New Size(780, 700)
				Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
				Dim transparent As New FrmTransparent(Formulario, False)
				transparent.ShowDialog(Me)
				_presenter.InitializeManufacturer()
			End Using
		End If
	End Sub

	''' <summary>
	''' Handles the ButtonClick event of the INDsleIva control.
	''' </summary>
	''' <param name="sender">The source of the event.</param>
	''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
	Private Sub INDsleIva_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIva.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
				Using Formulario As New FrmGeneralLedgerIVA
					Formulario.ViewModeEditHold = True
					Formulario.MinimizeBox = False
					Formulario.MaximizeBox = False
					Formulario.Size = New Size(780, 700)
					Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
					Dim transparent As New FrmTransparent(Formulario, False)
					transparent.ShowDialog(Me)
					_presenter.InitializeIva()
				End Using
			End If
		End If
	End Sub

	''' <summary>
	''' Evento que se dispara al presionar click en el boton del control de insumos
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Private Sub INDslePharmaceuticalForm_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleSupplie.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Dim size As System.Drawing.Size
			size.Width = 780
			size.Height = 768
			Using pop As New FrmTransparent(New FrmInventorySupplie With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
				pop.Show()
			End Using
			_presenter.InitializeSupplie()
		End If
	End Sub
#End Region

#End Region

#Region "Methods and Functions"

	''' <summary>
	''' Crea el listado de producto de origen
	''' </summary>
	Private Sub CreateListProductOrigin()
		ListProductOrigin = New List(Of Tuple(Of Integer, String))
		ListProductOrigin.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("ProductOriginNational", MODULE_NAME)))
		ListProductOrigin.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("ProductOriginImport", MODULE_NAME)))
		INDgleOrigin.Properties.DataSource = ListProductOrigin
	End Sub

	''' <summary>
	''' Crea el listado de tipos de moneda
	''' </summary>
	Private Sub CreateListCurrencyType()
		ListCurrencyType = New List(Of Tuple(Of Integer, String))()
		ListCurrencyType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("CurrencyTypeNational", MODULE_NAME)))
		ListCurrencyType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("CurrencyTypeExt", MODULE_NAME)))
		INDsleCurrencyType.Properties.DataSource = ListCurrencyType
	End Sub

	''' <summary>
	''' Inicializa las tuplas que van quemadas
	''' </summary>
	Private Sub InitializeTuple()
		ListSanitaryRegistration = New List(Of Tuple(Of Integer, String))()
		ListSanitaryRegistration.Add(New Tuple(Of Integer, String)(1, "Vigente"))
		ListSanitaryRegistration.Add(New Tuple(Of Integer, String)(2, "En Tramite Renov"))
		ListSanitaryRegistration.Add(New Tuple(Of Integer, String)(3, "Vencido"))
		ListSanitaryRegistration.Add(New Tuple(Of Integer, String)(4, "Abandono"))
		ListSanitaryRegistration.Add(New Tuple(Of Integer, String)(5, "Cancelado"))
		ListSanitaryRegistration.Add(New Tuple(Of Integer, String)(6, "Negado"))
		ListSanitaryRegistration.Add(New Tuple(Of Integer, String)(7, "Perdida Fuerza Ejec"))
		ListSanitaryRegistration.Add(New Tuple(Of Integer, String)(8, "Revocado"))
		ListSanitaryRegistration.Add(New Tuple(Of Integer, String)(9, "Suspendido"))
		ListSanitaryRegistration.Add(New Tuple(Of Integer, String)(10, "Inactivo"))
		INDsleSanitaryRegistration.Properties.DataSource = ListSanitaryRegistration

		ListYesNo = New List(Of Tuple(Of Boolean, String))()
		ListYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
		ListYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
		INDsleConsumption.Properties.DataSource = ListYesNo
		INDsleJustification.Properties.DataSource = ListYesNo
		INDsleOsteosynthesis.Properties.DataSource = ListYesNo

        _presenter.InitializeStorageTemperature()
    End Sub

	''' <summary>
	''' Crea un nuevo producto
	''' </summary>
	Private Async Function NewProduct() As Task
        _product = New InventoryProduct() With {.Status = True}
        SetListActions()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

        INDgleHandleSerial.EditValue = False
        INDgleHandleHealthRegistration.EditValue = False
        INDgleProductControl.EditValue = False
        INDglePriceControl.EditValue = False
        INDglePBSProduct.EditValue = False
        INDgleMaximumControlPeriod.EditValue = False
        INDgleOrderQuantityControl.EditValue = False
    End Function

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15},
                              New ColumnInfo() With {.Caption = "Medicamento", .FieldName = "ATCId.Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15},
                              New ColumnInfo() With {.Caption = "Insumo", .FieldName = "SupplieId.Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "ProductTypeId.Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15},
                              New ColumnInfo() With {.Caption = "Clase", .FieldName = "ClassName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInventoryProduct
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()

        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IProduct.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleProductType.Enabled = value
            INDsleATC.Enabled = value
            INDsleRiskLevel.Enabled = value
            INDtxtCUM.Enabled = value
            INDtxtCodeAlternative.Enabled = value
            INDtxtCodeAlternativeTwo.Enabled = value
            INDslePackingUnit.Enabled = value
            INDsleConsumption.Enabled = value
            INDmeDescription.Enabled = value
            INDsleGroup.Enabled = value
            INDsleSubGroup.Enabled = value
            INDSleMeasurementUnit.Enabled = value
            INDsleManufacturer.Enabled = value
            INDsleIva.Enabled = value
            INDtxtPresentation.Enabled = value
            INDtxtSICE.Enabled = value
            INDgleHandleSerial.Enabled = value
            INDtxtSerialNumber.Enabled = value
            INDgleHandleHealthRegistration.Enabled = value
            INDtxtHealthRegistration.Enabled = value
            INDdeExpirationDate.Enabled = value
            INDsleBillingGroup.Enabled = value
            INDgleProductControl.Enabled = value
            INDglePriceControl.Enabled = value
            INDglePBSProduct.Enabled = value
            INDtxtAuthorizationNumber.Enabled = value
            INDtxtExpirationDay.Enabled = value
            INDgleMaximumControlPeriod.Enabled = value
            INDspnControlDays.Enabled = value
            INDgleOrderQuantityControl.Enabled = value
            INDspnProductOrderAmount.Enabled = value
            INDdeLastPurchase.Enabled = value
            INDdeLastSale.Enabled = value
            INDgleOrigin.Enabled = value
            INDspnMinimumStrock.Enabled = value
            INDspnMaximumStock.Enabled = value
            INDspnCommissionPercent.Enabled = value
            INDspnRepositionPoint.Enabled = value
            INDspnResetTime.Enabled = value
            INDsleCurrencyType.Enabled = value
            INDspnProductCost.Enabled = value
            INDspnFinalProductCost.Enabled = value
            INDspnSellingPrice.Enabled = value
            INDsleSanitaryRegistration.Enabled = value
            INDliNickName.Enabled = value
            INDSleSupplie.Enabled = value
            INDTxeIUM.Enabled = value
            INDYesnotLiquidateSalesTaxes.Enabled = value
            INDliSismedReport.Enabled = value
            Me.INDgleTaxedProduct.Enabled = value
            INDlycRoot.EndUpdate()

            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Code) Then
            Try
                Using Model As New MInventoryProduct(Me.Tag)
                    AsyncLoader(True)
                    Dim stateProduct As Boolean = Not Me._product.Status
                    Dim Result = Await Model.UpdateStateProduct(Me.Code, stateProduct)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me._product = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        If Result.Message IsNot Nothing Then
                            generateListError(Result.Message)
                        End If
                    End If
                End Using
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._product.Code, Me._product.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._product.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._product.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._product.Code, Me._product.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._product.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDlycRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        Code = String.Empty
        NameProduct = String.Empty
        ProductTypeId = Nothing
        ATCId = Nothing
        InventoryRiskLevelId = Nothing
        INDliRiskLevel.ShowInCustomizationForm = True
        CodeCUM = String.Empty
        CodeAlternative = String.Empty
        CodeAlternativeTwo = String.Empty
        INDsleConsumption.EditValue = Nothing
        Description = String.Empty
        ProductGroupId = Nothing
        ProductSubGroupId = Nothing
        INDSleMeasurementUnit.EditValue = Nothing
        INDLciMeasurementUnit.Visibility = LayoutVisibility.Never
        INDLciMeasurementUnit.AllowHide = True
        INDLciMeasurementUnit.ShowInCustomizationForm = True
        PackagingUnitId = Nothing
        ManufacturerId = Nothing
        Osmolarity = 0
        WeightParenteralNutritionSupply = Decimal.Zero
        IVAId = Nothing
        ProductPresentation = String.Empty
        CodeSISE = String.Empty
        HandlesSerial = Nothing
        HandlesHealthRegistration = Nothing
        HealthRegistration = String.Empty
        SerialNumber = String.Empty
        ExpirationDate = Nothing
        BillingGroupId = Nothing
        ProductControl = Nothing
        ProductWithPriceControl = Nothing
        POSProduct = Nothing
        AuthorizationByOrderNumber = Nothing
        ExpirationDay = Nothing
        MaximumControlPeriod = Nothing
        ControlDays = Nothing
        ControlOrderQuantity = Nothing
        ProductOrderAmount = Nothing
        LastPurchase = Nothing
        LastSale = Nothing
        ProductOrigin = Nothing
        MinimumStock = Nothing
        MaximumStock = Nothing
        CommissionPercentage = Nothing
        RepositionPoint = Nothing
        ResetTime = Nothing
        CurrencyType = Nothing
        ControlCostPercentage = Nothing
        ProductCost = 0
        FinalProductCost = Nothing
        SellingPrice = Nothing
        INDspnProductCost.ReadOnly = False
        SanitaryRegistration = Nothing
        Abbreviation = String.Empty
        IUM = Nothing
        TaxedProduct = False
        Me.LiquidateSalesTaxes = False
        SismedReport = False
        Me.MedicationTypeId = Nothing
        INDliSismedReport.Visibility = LayoutVisibility.Never
        ShowLayouts(0)

        INDsleProductType.Properties.NullText = String.Empty
        INDsleATC.Properties.NullText = String.Empty
        INDsleRiskLevel.Properties.NullText = String.Empty
        INDslePackingUnit.Properties.NullText = String.Empty
        INDsleGroup.Properties.NullText = String.Empty
        INDsleSubGroup.Properties.NullText = String.Empty
        INDsleManufacturer.Properties.NullText = String.Empty
        INDsleIva.Properties.NullText = String.Empty
        INDsleBillingGroup.Properties.NullText = String.Empty

        _openPopUpProductType = False
        _openPopUpATC = False
        _openPopUpRiskLevel = False
        _openPopUpPackingUnit = False
        _openPopUpGroup = False
        _openPopUpSubGroup = False
        _openPopUpManufacturer = False
        _openPopUpIva = False
        _openPopUpBillinfGroup = False
        _openPopUpPOSPatologie = False
        _openPopUpBillingGruposNoPos = False
        'Limpiar controles
        _product = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        IsDairyComponente = False

        INDsleRiskLevel.Properties.ReadOnly = False
        INDglePBSProduct.Properties.ReadOnly = False
        INDtxtCodeAlternative.Properties.ReadOnly = False
        INDtxtPresentation.Properties.ReadOnly = False
        MedicamentXpo = Nothing
        SupplieId = Nothing
        INDSleSupplie.Properties.NullText = String.Empty
        ClearAttributes()
        ProductAttributes.Clear()
        INDlycRoot.EndUpdate()
        DeleteBlockedRecord()
        ShowCurrencyType()
    End Sub

	''' <summary>
	''' Oculta los layout no obligatorios
	''' </summary>
	Private Sub ShowLayouts(classProduct As eProductTypeClass)
		Select Case classProduct
			Case eProductTypeClass.Grupo

				INDliATC.HideControl()
				INDsleATC.EditValue = Nothing
				INDliRiskLevel.HideControl()
				INDsleRiskLevel.EditValue = Nothing
				INDliCodeAlternative.HideControl()
				INDtxtCodeAlternative.EditValue = Nothing
				INDliCodeAlternativeTo.HideControl()
				INDtxtCodeAlternativeTwo.EditValue = Nothing
				INDliPresentation.HideControl()
				INDtxtPresentation.EditValue = Nothing
				INDliDescription.HideControl()
				INDliNickName.HideControl()
				txtNickName.EditValue = Nothing
				INDLyDairyComponent.HideControl()

				INDlyItemConsumption.HideControl()
				INDsleConsumption.Properties.ReadOnly = False
				INDsleRiskLevel.Properties.ReadOnly = False
				INDglePBSProduct.Properties.ReadOnly = False
				INDtxtCodeAlternative.Properties.ReadOnly = False
				INDtxtPresentation.Properties.ReadOnly = False
				txtNickName.Properties.ReadOnly = False
				INDlyItemJustification.HideControl()
				INDlyItemOsteosynthesis.HideControl()

				'Grupo General
				INDlcgGeneral.Visibility = LayoutVisibility.Never
				INDLcgTaxes.HideControl()
				INDliCUM.HideControl()
				INDtxtCUM.EditValue = Nothing
				INDliGroup.HideControl()
				INDsleGroup.EditValue = Nothing
				INDliSubGroup.HideControl()
				INDsleSubGroup.EditValue = Nothing
				INDliManufacturer.HideControl()
				INDsleManufacturer.EditValue = Nothing
				INDliIva.HideControl()
				INDLciLiquidateSalesTaxes.HideControl()
				INDsleIva.EditValue = Nothing
				INDliSICE.HideControl()
				INDtxtSICE.EditValue = Nothing
				INDliHandleSerial.HideControl()
				INDgleHandleSerial.EditValue = Nothing
				INDtxtSerialNumber.Text = Nothing
				INDliSerialNumber.HideControl()
				INDliHandleHealthRegistration.HideControl()
				INDlyItemSanitaryRegistration.HideControl()
				INDliPictureSanitaryRegistration.HideControl()
				INDgleHandleHealthRegistration.EditValue = Nothing
				INDliHealthRegistration.HideControl()
				INDtxtHealthRegistration.EditValue = Nothing
				INDliExpirationDate.HideControl()
				INDdeExpirationDate.EditValue = Nothing
				INDsleSanitaryRegistration.EditValue = Nothing
				INDPictESanitaryRegistration.EditValue = My.Resources.Resources.blanco

				'Autorizaciones
				INDlcgAuthorization.Visibility = LayoutVisibility.Never
				INDliBillingGroup.HideControl()
				INDsleBillingGroup.EditValue = Nothing
				INDliProductControl.HideControl()
				INDgleProductControl.EditValue = Nothing
				INDliPriceControl.HideControl()
				INDglePriceControl.EditValue = Nothing
				INDliPBSProducto.HideControl()
				INDglePBSProduct.EditValue = Nothing
				INDliAuthorizationNumber.HideControl()
				INDtxtAuthorizationNumber.EditValue = Nothing
				INDliExpirationDay.HideControl()
				INDtxtExpirationDay.EditValue = Nothing
				INDliMaximumControlPeriod.HideControl()
				INDgleMaximumControlPeriod.EditValue = Nothing
				INDliControlDays.HideControl()
				INDspnControlDays.EditValue = Nothing
				INDliOrderQuantityControl.HideControl()
				INDgleOrderQuantityControl.EditValue = Nothing
				INDliProductOrderAmount.HideControl()
				INDspnProductOrderAmount.EditValue = Nothing
				INDliLastPurchase.HideControl()
				INDdeLastPurchase.EditValue = Nothing
				INDliLastSale.HideControl()
				INDdeLastSale.EditValue = Nothing

				'Datos Adicionales
				INDlcgAditionalData.Visibility = LayoutVisibility.Never
				INDliOrigin.HideControl()
				INDgleOrigin.EditValue = Nothing
				INDliMinimumStrock.HideControl()
				INDspnMinimumStrock.EditValue = Nothing
				INDliMaximumStock.HideControl()
				INDspnMaximumStock.EditValue = Nothing
				INDliCommissionPercent.HideControl()
				INDspnCommissionPercent.EditValue = Nothing
				INDliRepositionPoint.HideControl()
				INDspnRepositionPoint.EditValue = Nothing
				INDliResetTime.HideControl()
				INDspnResetTime.EditValue = Nothing
				INDliCurrencyType.HideControl()
				INDsleCurrencyType.EditValue = Nothing
				INDliProductCost.HideControl()
				Me.ProductCost = 0
				INDliFinalProductCost.HideControl()
				INDspnFinalProductCost.EditValue = Nothing
				INDliSellingPrice.HideControl()
				INDspnSellingPrice.EditValue = Nothing
				INDLciIUM.HideControl
				IUM = Nothing
				INDliStorage.HideControl
                Storage = Nothing
                INDLciMedicationType.HideControl()
                Me.MedicationTypeId = Nothing

            Case eProductTypeClass.ItemInsumo

				INDliATC.HideControl()
				INDsleATC.EditValue = Nothing
				INDliRiskLevel.HideControl(False)
				INDliCodeAlternative.HideControl(False)
				INDliCodeAlternativeTo.HideControl(False)
				INDliPresentation.HideControl()
				INDtxtPresentation.EditValue = Nothing
				INDliDescription.HideControl(False)
				INDliNickName.HideControl()
				txtNickName.EditValue = Nothing
				INDLyDairyComponent.HideControl(False)

				'Grupo General
				INDlcgGeneral.Visibility = LayoutVisibility.Always
				INDLcgTaxes.HideControl(False)
				INDliCUM.HideControl()
				INDtxtCUM.EditValue = Nothing
				INDliGroup.HideControl(False)
				INDliSubGroup.HideControl(False)
				INDliManufacturer.HideControl(False)
				Me.DisplayIVA()
				INDliSICE.HideControl(False)
				INDliHandleSerial.HideControl(False)
				INDliHandleHealthRegistration.HideControl(False)
				INDPictESanitaryRegistration.EditValue = If(INDPictESanitaryRegistration.EditValue Is Nothing, My.Resources.Resources.blanco, INDPictESanitaryRegistration.EditValue)

				INDlyItemConsumption.HideControl(False)
				INDsleConsumption.Properties.ReadOnly = False
				INDsleRiskLevel.Properties.ReadOnly = True
				INDglePBSProduct.Properties.ReadOnly = True
				INDtxtCodeAlternative.Properties.ReadOnly = False
				INDtxtPresentation.Properties.ReadOnly = False
				txtNickName.Properties.ReadOnly = False
				INDlyItemJustification.HideControl(False)
				INDlyItemOsteosynthesis.HideControl(False)

				'Autorizaciones
				INDlcgAuthorization.Visibility = LayoutVisibility.Always
				INDliBillingGroup.HideControl(False)
				INDliProductControl.HideControl(False)
				INDliPriceControl.HideControl(False)
				INDliPBSProducto.HideControl(False)
				INDliAuthorizationNumber.HideControl(False)
				INDliExpirationDay.HideControl(False)
				INDliMaximumControlPeriod.HideControl(False)
				INDliOrderQuantityControl.HideControl(False)
				INDliLastPurchase.HideControl(False)
				INDliLastSale.HideControl(False)

				'Datos Adicionales
				INDlcgAditionalData.Visibility = LayoutVisibility.Always
				INDliOrigin.HideControl(False)
				INDliMinimumStrock.HideControl(False)
				INDliMaximumStock.HideControl(False)
				INDliCommissionPercent.HideControl(False)
				INDliRepositionPoint.HideControl(False)
				INDliResetTime.HideControl(False)
				INDliCurrencyType.HideControl(False)
				INDliProductCost.HideControl(False)
				INDliFinalProductCost.HideControl(False)
				INDliSellingPrice.HideControl(False)
				INDLciIUM.HideControl(False)
				INDLciIUM.AllowHide = True
				INDTxeIUM.Properties.MaxLength = 12
				INDliStorage.HideControl
                Storage = Nothing
                INDLciMedicationType.HideControl()
                Me.MedicationTypeId = Nothing

            Case eProductTypeClass.ItemMedicamento

				INDliATC.HideControl(False)
				INDliRiskLevel.HideControl(False)
				INDliCodeAlternative.HideControl(False)
				INDliCodeAlternativeTo.HideControl(False)
				INDliPresentation.HideControl(False)
				INDliDescription.HideControl(False)
				INDliNickName.HideControl(False)
				INDLyDairyComponent.HideControl(False)

				'Grupo General
				INDlcgGeneral.Visibility = LayoutVisibility.Always
				INDLcgTaxes.HideControl(False)
				INDliCUM.HideControl(False)
				INDliGroup.HideControl(False)
				INDliSubGroup.HideControl(False)
				INDliManufacturer.HideControl(False)
				Me.DisplayIVA()
				INDliSICE.HideControl(False)
				INDliHandleSerial.HideControl(False)
				INDliHandleHealthRegistration.HideControl(False)
				INDPictESanitaryRegistration.EditValue = If(INDPictESanitaryRegistration.EditValue Is Nothing, My.Resources.Resources.blanco, INDPictESanitaryRegistration.EditValue)

				'Autorizaciones
				INDlcgAuthorization.Visibility = LayoutVisibility.Always
				INDliBillingGroup.HideControl(False)
				INDliProductControl.HideControl(False)
				INDliPriceControl.HideControl(False)
				INDliPBSProducto.HideControl(False)
				INDliAuthorizationNumber.HideControl(False)
				INDliExpirationDay.HideControl(False)
				INDliMaximumControlPeriod.HideControl(False)
				INDliOrderQuantityControl.HideControl(False)
				INDliLastPurchase.HideControl(False)
				INDliLastSale.HideControl(False)

				INDlyItemConsumption.HideControl(False)
				INDsleConsumption.Properties.ReadOnly = True
				INDsleRiskLevel.Properties.ReadOnly = True
				INDglePBSProduct.Properties.ReadOnly = True
				INDtxtCodeAlternative.Properties.ReadOnly = True
				INDtxtPresentation.Properties.ReadOnly = True
				txtNickName.Properties.ReadOnly = False
				INDlyItemJustification.HideControl()
				INDlyItemOsteosynthesis.HideControl()

				'Datos Adicionales
				INDlcgAditionalData.Visibility = LayoutVisibility.Always
				INDliOrigin.HideControl(False)
				INDliMinimumStrock.HideControl(False)
				INDliMaximumStock.HideControl(False)
				INDliCommissionPercent.HideControl(False)
				INDliRepositionPoint.HideControl(False)
				INDliResetTime.HideControl(False)
				INDliCurrencyType.HideControl(False)
				INDliProductCost.HideControl(False)
				INDliFinalProductCost.HideControl(False)
				INDliSellingPrice.HideControl(False)
				INDLciIUM.Visibility = LayoutVisibility.Always
				INDTxeIUM.Properties.MaxLength = 15
                INDliStorage.HideControl(False)
                INDLciMedicationType.HideControl(False)

            Case eProductTypeClass.ItemOtro
				INDliATC.HideControl()
				INDsleATC.EditValue = Nothing
				INDliRiskLevel.HideControl()
				INDsleRiskLevel.EditValue = Nothing
				INDliCodeAlternative.HideControl(False)
				INDliCodeAlternativeTo.HideControl(False)
				INDliPresentation.HideControl()
				INDtxtPresentation.EditValue = Nothing
				INDliDescription.HideControl(False)
				INDliNickName.HideControl()
				txtNickName.EditValue = Nothing
				INDLyDairyComponent.HideControl(False)

				'Grupo General
				INDlcgGeneral.Visibility = LayoutVisibility.Always
				INDLcgTaxes.HideControl(False)
				INDliCUM.HideControl()
				INDliGroup.HideControl(False)
				INDliSubGroup.HideControl(False)
				INDliManufacturer.HideControl(False)
				INDliSICE.HideControl()
				Me.DisplayIVA()
				INDtxtSICE.EditValue = Nothing
				INDliHandleSerial.HideControl()
				INDgleHandleSerial.EditValue = Nothing
				INDliHandleHealthRegistration.HideControl()
				INDlyItemSanitaryRegistration.HideControl()
				INDliPictureSanitaryRegistration.HideControl()
				INDsleSanitaryRegistration.EditValue = Nothing
				INDgleHandleHealthRegistration.EditValue = Nothing
				INDPictESanitaryRegistration.EditValue = My.Resources.Resources.blanco

				'Autorizaciones
				INDlcgAuthorization.Visibility = LayoutVisibility.Always
				INDliBillingGroup.HideControl()
				INDliProductControl.HideControl()
				INDgleProductControl.EditValue = Nothing
				INDliPriceControl.HideControl()
				INDglePriceControl.EditValue = Nothing
				INDliPBSProducto.HideControl()
				INDglePBSProduct.EditValue = Nothing
				INDliAuthorizationNumber.HideControl()
				INDtxtAuthorizationNumber.EditValue = Nothing
				INDliExpirationDay.HideControl(False)
				INDliMaximumControlPeriod.HideControl(False)
				INDliOrderQuantityControl.HideControl(False)
				INDliLastPurchase.HideControl(False)
				INDliLastSale.HideControl(False)

				INDlyItemConsumption.HideControl()
				INDsleConsumption.Properties.ReadOnly = False
				INDsleRiskLevel.Properties.ReadOnly = False
				INDglePBSProduct.Properties.ReadOnly = False
				INDtxtCodeAlternative.Properties.ReadOnly = False
				INDtxtPresentation.Properties.ReadOnly = False
				txtNickName.Properties.ReadOnly = False
				INDlyItemJustification.HideControl()
				INDlyItemOsteosynthesis.HideControl()

				'Datos Adicionales
				INDlcgAditionalData.Visibility = LayoutVisibility.Always
				INDliOrigin.HideControl(False)
				INDliMinimumStrock.HideControl(False)
				INDliMaximumStock.HideControl(False)
				INDliCommissionPercent.HideControl(False)
				INDliRepositionPoint.HideControl(False)
				INDliResetTime.HideControl(False)
				INDliCurrencyType.HideControl(False)
				INDliProductCost.HideControl(False)
				INDliFinalProductCost.HideControl(False)
				INDliSellingPrice.HideControl(False)
				INDLciIUM.HideControl
				IUM = Nothing
				INDliStorage.HideControl
                Storage = Nothing
                INDLciMedicationType.HideControl()
                Me.MedicationTypeId = Nothing

            Case eProductTypeClass.ItemProduccion
				INDliATC.HideControl(False)
				INDliRiskLevel.HideControl()
				INDsleRiskLevel.EditValue = Nothing
				INDliCodeAlternative.HideControl()
				INDliCodeAlternativeTo.HideControl(False)
				INDliPresentation.HideControl()
				INDtxtPresentation.EditValue = Nothing
				INDliDescription.HideControl(False)
				INDliNickName.HideControl(False)
				INDLyDairyComponent.HideControl()

				'Grupo General
				INDlcgGeneral.Visibility = LayoutVisibility.Always
				INDLcgTaxes.HideControl(False)
				INDliCUM.HideControl()
				INDliGroup.HideControl(False)
				INDliSubGroup.HideControl(False)
				INDliManufacturer.HideControl(False)
				INDliSICE.HideControl()
				Me.DisplayIVA()
				INDtxtSICE.EditValue = Nothing
				INDliHandleSerial.HideControl()
				INDgleHandleSerial.EditValue = Nothing
				INDliHandleHealthRegistration.HideControl()
				INDlyItemSanitaryRegistration.HideControl()
				INDliPictureSanitaryRegistration.HideControl()
				INDsleSanitaryRegistration.EditValue = Nothing
				INDgleHandleHealthRegistration.EditValue = Nothing
				INDPictESanitaryRegistration.EditValue = My.Resources.Resources.blanco

				'Autorizaciones
				INDlcgAuthorization.Visibility = LayoutVisibility.Always
				INDliBillingGroup.HideControl(False)
				INDliProductControl.HideControl()
				INDgleProductControl.EditValue = Nothing
				INDliPriceControl.HideControl()
				INDglePriceControl.EditValue = Nothing
				INDliPBSProducto.HideControl()
				INDglePBSProduct.EditValue = Nothing
				INDliAuthorizationNumber.HideControl()
				INDtxtAuthorizationNumber.EditValue = Nothing
				INDliExpirationDay.HideControl(False)
				INDliMaximumControlPeriod.Visibility = LayoutVisibility.Never
				INDliOrderQuantityControl.Visibility = LayoutVisibility.Never
				INDliLastPurchase.HideControl(False)
				INDliLastSale.HideControl(False)

				INDlyItemConsumption.HideControl()
				INDsleConsumption.Properties.ReadOnly = False
				INDsleRiskLevel.Properties.ReadOnly = False
				INDglePBSProduct.Properties.ReadOnly = False
				INDtxtCodeAlternative.Properties.ReadOnly = False
				INDtxtPresentation.Properties.ReadOnly = False
				txtNickName.Properties.ReadOnly = False
				INDlyItemJustification.HideControl()
				INDlyItemOsteosynthesis.HideControl()

				'Datos Adicionales
				INDlcgAditionalData.Visibility = LayoutVisibility.Always
				INDliOrigin.HideControl(False)
				INDliMinimumStrock.HideControl(False)
				INDliMaximumStock.HideControl(False)
				INDliCommissionPercent.HideControl(False)
				INDliRepositionPoint.HideControl(False)
				INDliResetTime.HideControl(False)
				INDliCurrencyType.HideControl(False)
				INDliProductCost.HideControl(False)
				INDliFinalProductCost.HideControl(False)
				INDliSellingPrice.HideControl(False)
				INDLciIUM.HideControl
				IUM = Nothing
                INDliStorage.HideControl(False)
                INDLciMedicationType.HideControl()
                Me.MedicationTypeId = Nothing

            Case Else
				INDliATC.HideControl()
				INDsleATC.EditValue = Nothing
				INDliRiskLevel.HideControl()
				INDsleRiskLevel.EditValue = Nothing
				INDliCodeAlternative.HideControl()
				INDtxtCodeAlternative.EditValue = Nothing
				INDliCodeAlternativeTo.HideControl()
				INDtxtCodeAlternativeTwo.EditValue = Nothing
				INDliPresentation.HideControl()
				INDtxtPresentation.EditValue = Nothing
				INDliDescription.HideControl()
				INDmeDescription.EditValue = Nothing
				INDmeDescription.EditValue = Nothing
				INDliNickName.HideControl()
				txtNickName.EditValue = Nothing
				INDliPictureTempMax.HideControl()
				INDPictETempMax.EditValue = Nothing
				INDliTempMax.HideControl()
				INDtxtTempMax.EditValue = Nothing
				INDliPictureTempMin.HideControl()
				INDPictETempMin.EditValue = Nothing
				INDliTempMin.HideControl()
				INDtxtTempMin.EditValue = Nothing
				INDLyDairyComponent.HideControl()

				INDlyItemConsumption.HideControl()
				INDsleConsumption.Properties.ReadOnly = False
				INDsleRiskLevel.Properties.ReadOnly = False
				INDglePBSProduct.Properties.ReadOnly = False
				INDtxtCodeAlternative.Properties.ReadOnly = False
				INDtxtPresentation.Properties.ReadOnly = False
				txtNickName.Properties.ReadOnly = False
				INDlyItemJustification.HideControl()
				INDlyItemOsteosynthesis.HideControl()

				'Grupo General
				INDlcgGeneral.Visibility = LayoutVisibility.Never
				INDLcgTaxes.HideControl()
				INDliCUM.HideControl()
				INDtxtCUM.EditValue = Nothing
				INDliGroup.HideControl()
				INDsleGroup.EditValue = Nothing
				INDliSubGroup.HideControl()
				INDsleSubGroup.EditValue = Nothing
				INDliManufacturer.HideControl()
				INDsleManufacturer.EditValue = Nothing
				INDliIva.HideControl()
				INDLciLiquidateSalesTaxes.HideControl()
				INDsleIva.EditValue = Nothing
				INDliSICE.HideControl()
				INDtxtSICE.EditValue = Nothing
				INDliHandleSerial.HideControl()
				INDgleHandleSerial.EditValue = Nothing
				INDtxtSerialNumber.Text = Nothing
				INDliSerialNumber.HideControl()
				INDliHandleHealthRegistration.HideControl()
				INDlyItemSanitaryRegistration.HideControl()
				INDliPictureSanitaryRegistration.HideControl()
				INDgleHandleHealthRegistration.EditValue = Nothing
				INDliHealthRegistration.HideControl()
				INDtxtHealthRegistration.EditValue = Nothing
				INDliExpirationDate.HideControl()
				INDdeExpirationDate.EditValue = Nothing
				INDsleSanitaryRegistration.EditValue = Nothing
				INDPictESanitaryRegistration.EditValue = My.Resources.Resources.blanco

				'Autorizaciones
				INDlcgAuthorization.Visibility = LayoutVisibility.Never
				INDliBillingGroup.HideControl()
				INDsleBillingGroup.EditValue = Nothing
				INDliProductControl.HideControl()
				INDgleProductControl.EditValue = Nothing
				INDliPriceControl.HideControl()
				INDglePriceControl.EditValue = Nothing
				INDliPBSProducto.HideControl()
				INDglePBSProduct.EditValue = Nothing
				INDliAuthorizationNumber.HideControl()
				INDtxtAuthorizationNumber.EditValue = Nothing
				INDliExpirationDay.HideControl()
				INDtxtExpirationDay.EditValue = Nothing
				INDliMaximumControlPeriod.HideControl()
				INDgleMaximumControlPeriod.EditValue = Nothing
				INDliControlDays.HideControl()
				INDspnControlDays.EditValue = Nothing
				INDliOrderQuantityControl.HideControl()
				INDgleOrderQuantityControl.EditValue = Nothing
				INDliProductOrderAmount.HideControl()
				INDspnProductOrderAmount.EditValue = Nothing
				INDliLastPurchase.HideControl()
				INDdeLastPurchase.EditValue = Nothing
				INDliLastSale.HideControl()
				INDdeLastSale.EditValue = Nothing

				'Datos Adicionales
				INDlcgAditionalData.Visibility = LayoutVisibility.Never
				INDliOrigin.HideControl()
				INDgleOrigin.EditValue = Nothing
				INDliMinimumStrock.HideControl()
				INDspnMinimumStrock.EditValue = Nothing
				INDliMaximumStock.HideControl()
				INDspnMaximumStock.EditValue = Nothing
				INDliCommissionPercent.HideControl()
				INDspnCommissionPercent.EditValue = Nothing
				INDliRepositionPoint.HideControl()
				INDspnRepositionPoint.EditValue = Nothing
				INDliResetTime.HideControl()
				INDspnResetTime.EditValue = Nothing
				INDliCurrencyType.HideControl()
				INDsleCurrencyType.EditValue = Nothing
				INDliProductCost.HideControl()
				INDspnProductCost.EditValue = Nothing
				INDliFinalProductCost.HideControl()
				INDspnFinalProductCost.EditValue = Nothing
				INDliSellingPrice.HideControl()
				INDspnSellingPrice.EditValue = Nothing
				INDLciIUM.HideControl
				IUM = Nothing
				INDliStorage.HideControl
                Storage = Nothing
                INDLciMedicationType.HideControl()
                Me.MedicationTypeId = Nothing
        End Select
	End Sub

	''' <summary>
	''' Abre la jerarquia
	''' </summary>
	Private Sub OpenJerarquia()
        Using frmJerarquia As New FrmJerarquia()
            frmJerarquia.ViewModeEditHold = True
            frmJerarquia.MinimizeBox = False
            frmJerarquia.MaximizeBox = False
            If _product Is Nothing OrElse _product.Id = 0 Then
                With _product
                    .Name = NameProduct
                    .ProductTypeId = ProductTypeId
                    .PackagingUnitId = PackagingUnitId
                End With
            End If
            Dim _productClone = _product.Clone()
            frmJerarquia.InventoryProduct = _product
            frmJerarquia.Size = New Size(820, 600)
            frmJerarquia.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(frmJerarquia, False)
            If transparent.ShowDialog(Me) <> DialogResult.OK Then
                _product = _productClone
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _product
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameProduct
            .ProductTypeId = ProductTypeId

            If Me.ProductTypeClass = EProductTypeClass.ItemMedicamento Or Me.ProductTypeClass = EProductTypeClass.ItemProduccion Then
                .ATCId = ATCId
            Else
                .ATCId = Nothing
            End If

            .InventoryRiskLevelId = InventoryRiskLevelId
            .CodeCUM = CodeCUM
            .CodeAlternative = CodeAlternative
            .CodeAlternativeTwo = CodeAlternativeTwo
            .Description = Description
            .ProductGroupId = ProductGroupId
            .ProductSubGroupId = ProductSubGroupId
            .MeasurementUnitId = INDSleMeasurementUnit.EditValue
            .PackagingUnitId = PackagingUnitId
            .ManufacturerId = ManufacturerId
            .Osmolarity = Osmolarity
            .IVAId = IVAId
            .LiquidateSalesTaxes = Me.LiquidateSalesTaxes
            .Presentation = ProductPresentation
            .CodeSICE = CodeSISE
            .HandlesSerial = HandlesSerial
            .HandlesHealthRegistration = HandlesHealthRegistration
            .HealthRegistration = HealthRegistration
            .SerialNumber = SerialNumber
            .ExpirationDate = ExpirationDate
            .BillingGroupId = BillingGroupId
            .ProductControl = ProductControl
            .ProductWithPriceControl = ProductWithPriceControl
            .POSProduct = POSProduct
            .AuthorizationByOrderNumber = AuthorizationByOrderNumber
            .ExpirationDay = ExpirationDay
            .MaximumControlPeriod = MaximumControlPeriod
            .ControlDays = ControlDays
            .ControlOrderQuantity = ControlOrderQuantity
            .ProductOrderAmount = ProductOrderAmount
            .LastPurchase = LastPurchase
            .LastSale = LastSale
            .ProductOrigin = ProductOrigin
            .MinimumStock = MinimumStock
            .MaximumStock = MaximumStock
            .CommissionPercentage = CommissionPercentage
            .RepositionPoint = RepositionPoint
            .ResetTime = ResetTime
            .CurrencyType = CurrencyType
            .ControlCostPercentage = ControlCostPercentage
            .ProductCost = ProductCost
            .FinalProductCost = FinalProductCost
            .SellingPrice = SellingPrice
            .SanitaryRegistration = SanitaryRegistration
            .Abbreviation = Abbreviation
            .IUM = IUM
            .TaxedProduct = TaxedProduct
            .DairyComponent = IsDairyComponente
            .MedicationTypeId = Me.MedicationTypeId

            If Me.ProductTypeClass = EProductTypeClass.ItemInsumo Then
                .SupplieId = SupplieId
                .SismedReport = SismedReport

                If INDliWeightSupplyNutrition.Visibility = LayoutVisibility.Always Then
                    .WeightParenteralNutritionSupply = WeightParenteralNutritionSupply
                End If
            Else
                .SupplieId = Nothing
                .SismedReport = 0
            End If

            If INDlyItemConsumption.Visibility = LayoutVisibility.Always Then
                .Consumption = INDsleConsumption.EditValue
            Else
                .Consumption = Nothing
            End If

            If INDlyItemJustification.Visibility = LayoutVisibility.Always Then
                .JustificationSuppliesDispositives = INDsleJustification.EditValue
                .OsteosynthesisMaterial = INDsleOsteosynthesis.EditValue
            Else
                .JustificationSuppliesDispositives = Nothing
                .OsteosynthesisMaterial = Nothing
            End If

            If _handleMixingStation Then
                .DriveUnit = DriveUnit
            End If

            If _handleTemperatureRange Then
                .MaximumTemperature = MaximumTemperature
                .MinimumTemperature = MinimumTemperature
            End If
            If INDliStorage.Visibility = LayoutVisibility.Always Then
                .Storage = Storage
            End If
        End With

        If ProductAttributes IsNot Nothing AndAlso ProductAttributes.Any() Then

            ProductAttributes.ForEach(Sub(o)
                                          Dim productAttribute As InventoryProductAttribute = Nothing
                                          If _product.InventoryProductAttribute IsNot Nothing _
                                              AndAlso _product.InventoryProductAttribute.Any() _
                                              AndAlso _product.InventoryProductAttribute.Any(Function(m) m.AttributeProductTypeId = o.Item1) Then
                                              productAttribute = _product.InventoryProductAttribute.FirstOrDefault(Function(m) m.AttributeProductTypeId = o.Item1)
                                          Else
                                              productAttribute = New InventoryProductAttribute()
                                              productAttribute.AttributeProductTypeId = o.Item1
                                          End If

                                          If o.Item2.EditValue IsNot Nothing Then
                                              productAttribute.Value = o.Item2.EditValue.ToString()
                                              _product.InventoryProductAttribute.Add(productAttribute)
                                          End If
                                      End Sub)

        End If
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        _isLoading = True
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            If SearchBlankCode(Code) Then
                Mensaje(EeventViewerImages.Advertencia) = "El código del producto No puede contener espacios en blanco"
                Exit Function
            End If
            Try
                Using Model As New MInventoryProduct(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetInventoryProduct(Me.Code)
                    _product = resultOperation.ObjectEmbbeded
                    INDlycRoot.BeginUpdate()
                    If _product IsNot Nothing AndAlso _product.Id > 0 Then
                        SetListActions()
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_product.Id))
                            With _product
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                Code = .Code
                                NameProduct = .Name
                                ProductTypeId = .ProductTypeId
                                ATCId = .ATCId
                                InventoryRiskLevelId = .InventoryRiskLevelId
                                If InventoryRiskLevelId Is Nothing AndAlso ATCId IsNot Nothing Then
                                    MedicamentXpo = Await _presenter.GetMedicamentById(ATCId)
                                    If MedicamentXpo IsNot Nothing Then
                                        Dim riskLevelXpo = _presenter.GetRiskLevelById(MedicamentXpo.InventoryRiskLevelId)
                                        If riskLevelXpo IsNot Nothing Then
                                            InventoryRiskLevelId = MedicamentXpo.InventoryRiskLevelId
                                        End If
                                    End If
                                End If
                                CodeCUM = .CodeCUM
                                CodeAlternative = .CodeAlternative
                                CodeAlternativeTwo = .CodeAlternativeTwo
                                Description = .Description
                                ProductGroupId = .ProductGroupId
                                ProductSubGroupId = .ProductSubGroupId
                                INDSleMeasurementUnit.EditValue = .MeasurementUnitId
                                PackagingUnitId = .PackagingUnitId
                                ManufacturerId = .ManufacturerId
                                Osmolarity = .Osmolarity
                                ProductPresentation = .Presentation
                                CodeSISE = .CodeSICE
                                HandlesSerial = .HandlesSerial
                                INDsleConsumption.EditValue = If(.Consumption Is Nothing, False, .Consumption)
                                INDsleJustification.EditValue = .JustificationSuppliesDispositives
                                INDsleOsteosynthesis.EditValue = .OsteosynthesisMaterial
                                HandlesHealthRegistration = .HandlesHealthRegistration
                                HealthRegistration = .HealthRegistration
                                SerialNumber = .SerialNumber
                                ExpirationDate = .ExpirationDate
                                BillingGroupId = .BillingGroupId
                                ProductControl = .ProductControl
                                ProductWithPriceControl = .ProductWithPriceControl
                                POSProduct = .POSProduct
                                AuthorizationByOrderNumber = .AuthorizationByOrderNumber
                                ExpirationDay = .ExpirationDay
                                MaximumControlPeriod = .MaximumControlPeriod
                                ControlDays = .ControlDays
                                ControlOrderQuantity = .ControlOrderQuantity
                                ProductOrderAmount = .ProductOrderAmount
                                LastPurchase = .LastPurchase
                                LastSale = .LastSale
                                ProductOrigin = .ProductOrigin
                                MinimumStock = .MinimumStock
                                MaximumStock = .MaximumStock
                                CommissionPercentage = .CommissionPercentage
                                RepositionPoint = .RepositionPoint
                                ResetTime = .ResetTime
                                CurrencyType = .CurrencyType
                                ControlCostPercentage = .ControlCostPercentage
                                ProductCost = .ProductCost
                                FinalProductCost = .FinalProductCost
                                SellingPrice = .SellingPrice
                                Status = .Status
                                INDspnProductCost.ReadOnly = True
                                SanitaryRegistration = .SanitaryRegistration
								TaxedProduct = .TaxedProduct
                                IsDairyComponente = .DairyComponent
                                Me.MedicationTypeId = .MedicationTypeId
                                WeightParenteralNutritionSupply = .WeightParenteralNutritionSupply

                                If TaxedProduct Then
                                    IVAId = .IVAId
                                    Me.LiquidateSalesTaxes = .LiquidateSalesTaxes
                                End If

                                If _product.ProductType.Class = 3 Then
                                    INDliSismedReport.Visibility = LayoutVisibility.Always
                                    SismedReport = .SismedReport
                                Else
                                    INDliSismedReport.Visibility = LayoutVisibility.Never
                                End If

                                ValidationColors(False, Me._product)
                                If _handleMixingStation Then
                                    DriveUnit = .DriveUnit
                                End If

                                If (_handleTemperatureRange) Then
                                    MaximumTemperature = .MaximumTemperature
                                    MinimumTemperature = .MinimumTemperature
                                    ValidationColors(False, Me._product)
                                End If

                                Abbreviation = .Abbreviation
                                SupplieId = .SupplieId
                                INDSleSupplie.Properties.NullText = .SupplieDescription
                                IUM = .IUM
                                Storage = .Storage
                                ShowPOSProduct()
                                ShowCurrencyType()
                            End With
                            LoadNullText()
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._product.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _product.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_product.Id, Me.Tag.ToString(), Nothing, GetType(InventoryProduct).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            DisplayIVA()
                            'se bloquea los controles de medicamento y tipo de producto para que no se permita cambiar cuando ya esta creado
                            'Se valida que el producto tenga cantidades existentes disponibles en el inventario fisico para deshabilitar los campos para no permitir su modificacion
                            Dim warehouseWithQuantity = _product.PhysicalInventory.Any(Function(pi) pi.Quantity > 0 AndAlso pi.Warehouse.VirtualStore = 0)

                            If warehouseWithQuantity Then
                                INDlycRoot.BeginUpdate()
                                INDsleATC.Enabled = False
                                INDsleProductType.Enabled = False
                                INDlycRoot.EndUpdate()
                            End If
                        End Using
                    Else
                        AsyncLoader(False)
                        ' Validación solo para productos nuevos
                        If Me.Code.Length > 20 Then
                            Mensaje(EeventViewerImages.Advertencia) = "El código del producto solo permite un máximo de 20 dígitos"
                            Code = String.Empty
                            INDbteCode.Focus()
                        Else
                            If Me._sequence.IsManual Then
                                Await Me.NewProduct()
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                                Code = String.Empty
                                INDbteCode.Focus()
                            End If
                        End If
                    End If
                    INDlycRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                _isLoading = False
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
        _isLoading = False
        loadProductTypeAttributes()
    End Function

    ''' <summary>
    ''' Loads the null text.
    ''' </summary>
    Private Sub LoadNullText()
        With _product
            INDsleProductType.Properties.NullText = .ProductTypeDescription
            INDsleATC.Properties.NullText = .ATCDescription
            INDsleRiskLevel.Properties.NullText = .InventoryRiskLevelDescription
            INDslePackingUnit.Properties.NullText = .PackingUnitDescription
            INDsleGroup.Properties.NullText = .GroupDescription
            INDsleSubGroup.Properties.NullText = .SubGroupDescription
            INDsleManufacturer.Properties.NullText = .ManufacturerDescription
            INDsleIva.Properties.NullText = .IvaCodeDescription
            INDsleBillingGroup.Properties.NullText = .BillingGroupDescription
        End With
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._product IsNot Nothing AndAlso Me._product.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                INDbteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Función que identifica si la empresa tiene o no central de mezclas
    ''' </summary>
    Private Function ValidationMixingStation() As Boolean
        Using ModelCMConfig As New MCMConfig(CStr(Me.Tag))
            _configurationManageMS = ModelCMConfig.GetCMConfig()
        End Using

        If _configurationManageMS IsNot Nothing AndAlso (_configurationManageMS.ManageMS = 1) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Función que valida los rangos de temperatura
    ''' <param name="typeValidation"> Especifica el tipo de validación: False = No retorna mensaje - True='Retorna mensaje'</param>
    ''' <param name="objectValues">Contiene los valores a medir</param>
    ''' </summary>
    Private Function ValidationColors(typeValidation As Boolean, objectValues As Object) As String
        Dim message As String = Nothing
        If Not typeValidation Then 'Se valida si retorna o no mensaje
			'Se comparan los rangos de temperatura para asignar colores de acuerdo a los rangos establecidos (< 2 || > 25)
			If objectValues.ToString().Equals("Domain.Entities.InventoryProduct") Then
				If objectValues.MaximumTemperature <> Nothing Then
					If objectValues.MaximumTemperature < 2 Or objectValues.MaximumTemperature > 25 Then
						INDPictETempMax.EditValue = My.Resources.Resources.rojo
					Else
						INDPictETempMax.EditValue = My.Resources.Resources.verde
					End If
				Else
					INDPictETempMax.EditValue = My.Resources.Resources.blanco
				End If

				If objectValues.MinimumTemperature <> Nothing Then
					If objectValues.MinimumTemperature < 2 Or objectValues.MinimumTemperature > 25 Then

						INDPictETempMin.EditValue = My.Resources.Resources.rojo
					Else
						INDPictETempMin.EditValue = My.Resources.Resources.verde
					End If
				Else
					INDPictETempMin.EditValue = My.Resources.Resources.blanco
				End If

				If objectValues.SanitaryRegistration <> Nothing Then
					If objectValues.SanitaryRegistration = 1 Or objectValues.SanitaryRegistration = 2 Then
						INDPictESanitaryRegistration.EditValue = My.Resources.Resources.verde
					Else
						INDPictESanitaryRegistration.EditValue = My.Resources.Resources.rojo
					End If
				Else
					INDPictESanitaryRegistration.EditValue = My.Resources.Resources.blanco
				End If

			ElseIf objectValues.ToString().Equals("DevExpress.XtraEditors.SearchLookUpEdit") AndAlso objectValues.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(objectValues.EditValue.ToString()) Then
				If CInt(objectValues.EditValue) = 1 Or CInt(objectValues.EditValue) = 2 Then
					INDPictESanitaryRegistration.EditValue = My.Resources.Resources.verde
				Else
					INDPictESanitaryRegistration.EditValue = My.Resources.Resources.rojo
				End If

			Else
				Dim tempValue As Double = 0
				If objectValues.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(objectValues.EditValue.ToString().Trim()) Then
					Double.TryParse(objectValues.EditValue.ToString(), tempValue)
				End If

				If tempValue = 0 OrElse tempValue <> Nothing Then
					If tempValue < 2 Or tempValue > 25 Then
						If objectValues.Name.Equals("INDtxtTempMax") Then
							INDPictETempMax.EditValue = My.Resources.Resources.rojo
						Else
							INDPictETempMin.EditValue = My.Resources.Resources.rojo
						End If
					Else
						If objectValues.Name.Equals("INDtxtTempMax") Then
							INDPictETempMax.EditValue = My.Resources.Resources.verde
						Else
							INDPictETempMin.EditValue = My.Resources.Resources.verde
						End If
					End If
				Else
					If objectValues.Name.Equals("INDtxtTempMax") Then
						INDPictETempMax.EditValue = My.Resources.Resources.blanco
					Else
						INDPictETempMin.EditValue = My.Resources.Resources.blanco
					End If
				End If
			End If
			Return Nothing
        Else
            If (CInt(INDtxtTempMax.EditValue) < CInt(INDtxtTempMin.EditValue)) Then
                message = "La temperatura Máxima no debe ser menor a la temperatura Mínima"
                Return message
            ElseIf (CInt(INDtxtTempMax.EditValue) > 25 OrElse CInt(INDtxtTempMin.EditValue) < 2) Then
                message = "Rango de temperatura puede estar fuera de los limites permitido por el fabricante. ¿Desea continuar?"
                Return message
            Else
                Return Nothing
            End If
        End If
    End Function

    '''' <summary>
    '''' Metodo que muestra/oculta el control de Redondeo
    '''' </summary>
    Private Sub ShowPOSProduct()
        INDliPBSProducto.ShowLayout()
        If Me.indigo.Culture.Name <> "es-CO" Then
            INDliPBSProducto.HideLayout()           'Oculta el campo de Producto PBS / Producto POS
            POSProduct = True                       'Por defecto es True -> Si es producto POS
        Else
            INDliPBSProducto.ShowLayout()
        End If
    End Sub

    '''' <summary>
    '''' Metodo que muestra/oculta el control de Tipo de Moneda
    '''' </summary>
    Private Sub ShowCurrencyType()
        INDliCurrencyType.ShowLayout()
        If Me.indigo.Culture.Name <> "es-CO" Then
            INDliCurrencyType.HideLayout()           'Oculta el campo de Tipo Moneda
            CurrencyType = 2                         'Por defecto es 2 -> Moneda Extranjera
        Else
            INDliCurrencyType.ShowLayout()
        End If
    End Sub
#End Region

#Region "ICrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me._product IsNot Nothing AndAlso Me._product.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MInventoryProduct(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteInventoryProduct(Me._product)
                    AsyncLoader(False)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                    Else
                        If result.Message IsNot Nothing Then
                            generateListError(result.Message)
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        _isLoading = True
        If ValidateControls() Then
            If POSProduct AndAlso Not Me._product.AllPOSPathologies AndAlso INDliATC.Visibility = LayoutVisibility.Never Then
                If Me._product.POSPathologies Is Nothing OrElse Me._product.POSPathologies.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una aclaración"
                    Exit Sub
                End If
            End If

            If FinalProductCost IsNot Nothing Then
                If FinalProductCost.Value = 0 OrElse FinalProductCost Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "El valor del campo último costo debe ser mayor a cero"
                    INDspnFinalProductCost.Focus()
                    Exit Sub
                End If
            End If

            If SismedReport AndAlso String.IsNullOrEmpty(IUM) AndAlso ProductTypeClass = EProductTypeClass.ItemInsumo Then
                Mensaje(EeventViewerImages.Advertencia) = "El insumo debe contar con un identificador unico si se va a mostrar en el reporte SISMED "
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        If ProductTypeClass = eProductTypeClass.ItemMedicamento AndAlso Not String.IsNullOrEmpty(CodeCUM) Then
            If INDtxtCUM.Text <> CodeCUM Then
                CodeCUM = INDtxtCUM.Text
            End If
            If CodeCUM.Contains("-") Then
                Dim _cadenas As String()
                _cadenas = CodeCUM.Split("-")
                If _cadenas.Count = 2 Then
                    If String.IsNullOrEmpty(_cadenas(0)) Then
                        Mensaje(EeventViewerImages.Advertencia) = "Ingrese un valor en la parte izquierda del código CUM"
                        Exit Sub
                    End If
                    If _cadenas(0) = "0" Then
                        Mensaje(EeventViewerImages.Advertencia) = "Ingrese un valor distinto de 0 en la parte izquierda del código CUM"
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(_cadenas(1)) Then
                        Mensaje(EeventViewerImages.Advertencia) = "Ingrese un valor en la parte derecha del código CUM"
                        Exit Sub
                    End If
                End If
            End If
        End If

        If _handleTemperatureRange Then
            Dim message As String = ValidationColors(True, Me._product)
            If (message IsNot Nothing AndAlso message.Contains("Rango")) Then
                If MessageIndigo.Show(message, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AssigningValues()
                Else
                    Exit Sub
                End If
            ElseIf (message IsNot Nothing AndAlso message.Contains("La temperatura")) Then
                Mensaje(EeventViewerImages.Advertencia) = message
                Exit Sub
            Else
                AssigningValues()
            End If
        Else
            AssigningValues()
        End If
        Try
            Using Model As New MInventoryProduct(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveInventoryProduct(Me._product, Me._idCurrentSequence)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If _product.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._product = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.Deshacer()
                Else
                    If Result.Message IsNot Nothing Then
                        generateListError(Result.Message)
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        Finally
            _isLoading = False
        End Try
    End Sub

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = errors
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewProduct()
        End If
    End Sub
#End Region

#Region "Bar Button Events"
    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _isLoading = True
        _searchMode = False
        Deshacer()
        _isLoading = False
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    Private Sub BarraBotones_Click_Jerarquia() Handles BarraBotones.Click_Jerarquia
        OpenJerarquia()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub
#End Region

    ''' <summary>
    ''' Diccionario donde se guardan todos los permisos del form
    ''' </summary>
    Private _permissionsForm As Dictionary(Of Integer, String)
    Public Property PermissionsForm As Dictionary(Of Integer, String) Implements IProduct.PermissionsForm
        Get
            Return _permissionsForm
        End Get
        Set(value As Dictionary(Of Integer, String))
            _permissionsForm = value
        End Set
    End Property

    ''' <summary>
    ''' Verifica permisos de usuario y asigna el menu
    ''' </summary>
    Private Sub SetListActions()
        If _permissionsForm IsNot Nothing AndAlso _permissionsForm.Count > 0 Then
            If _permissionsForm.ContainsKey(142) Then 'Si tiene permiso de jerarquia
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Jerarquia) = False
                Me.BarraBotones.BarBtnJerarquia.Enabled = True
            End If
        End If
    End Sub

    Private Sub INDSleMeasurementUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleMeasurementUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("300", Nothing, True)
        End If
    End Sub

    Private Sub INDsleSubGroup_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleSubGroup.EditValueChanging
        If _isLoading Then
            Exit Sub
        End If
        If _product IsNot Nothing AndAlso _product.Id > 0 AndAlso e.NewValue IsNot Nothing Then
            Using model As New MInventoryProduct(MyTag)
                Dim count = model.ValidateChangeSubgroupPhysicalInventory(_product.Id)
                If count > 0 Then
                    e.Cancel = True
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede cambiar el subgrupo porque el producto tiene cantidades en el Inventario Físico"
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Funcion para saber si contiene espacios en blanco
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Private Function SearchBlankCode(Code As String) As Boolean
        Dim Espacios = 0
        Dim Posicion = 1
        Dim Total = Len(Code)
        Dim Letra = LCase(Mid(Code, Posicion, 1))
        For i = 1 To Total
            If Letra = " " Then
                Espacios = Espacios + 1
            End If
            Posicion = Posicion + 1
            Letra = LCase(Mid(Code, Posicion, 1))
        Next i
        Return If(Espacios > 0, True, False)
    End Function

    Private Sub INDgleTaxedProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleTaxedProduct.EditValueChanged
        DisplayIVA()
    End Sub

    Private Sub DisplayIVA()
        Me.INDLciLiquidateSalesTaxes.HideControl(Not TaxedProduct)
        If TaxedProduct Then
            INDliIva.Visibility = LayoutVisibility.Always
        Else
            INDliIva.Visibility = LayoutVisibility.Never
            IVAId = Nothing
            Me.LiquidateSalesTaxes = False
        End If
    End Sub

End Class