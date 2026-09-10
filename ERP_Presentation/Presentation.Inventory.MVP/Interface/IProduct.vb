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

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IProduct
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene el layout del frontal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el codigo del producto
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre del producto
    ''' </summary>
    Property NameProduct As String

    ''' <summary>
    ''' Obtiene o establece el id del tipo de producto
    ''' </summary>
    Property ProductTypeId As Integer

    ''' <summary>
    ''' Obtiene o establece el id del sistema de clasificacion automatica
    ''' </summary>
    Property ATCId As Integer?

    ''' <summary>
    ''' Obtiene o establece el nivel de riesgo
    ''' </summary>
    Property InventoryRiskLevelId As Integer?

    ''' <summary>
    ''' Obtiene o establece el codigo CUM del producto
    ''' </summary>
    Property CodeCUM As String

    ''' <summary>
    ''' Obtiene o establece el codigo alternativo
    ''' </summary>
    Property CodeAlternative As String

    ''' <summary>
    ''' Obtiene o establece el codigo alternativo dos
    ''' </summary>
    Property CodeAlternativeTwo As String

    ''' <summary>
    ''' Obtiene o establece la descripcion larga del producto
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el id del grupo del producto
    ''' </summary>
    Property ProductGroupId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del subgrupo del producto
    ''' </summary>
    Property ProductSubGroupId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la unidad de empaques
    ''' </summary>
    Property PackagingUnitId As Integer

    ''' <summary>
    ''' Obtiene o establece el id del fabricante
    ''' </summary>
    Property ManufacturerId As Integer?

    ''' <summary>
    ''' Obtiene o establece la Osmolaridad
    ''' </summary>
    Property Osmolarity As Decimal

    ''' <summary>
    ''' Obtiene o establece el id del iva aplicado al producto
    ''' </summary>
    Property IVAId As Integer?

    ''' <summary>
    ''' Obtiene o establece la presentación del producto
    ''' </summary>
    Property ProductPresentation As String

    ''' <summary>
    ''' Obtiene o establece el codigo SICE del producto
    ''' </summary>
    Property CodeSISE As String

    ''' <summary>
    ''' Obtiene o establece si maneja serial
    ''' </summary>
    Property HandlesSerial As Boolean?

    ''' <summary>
    ''' Obtiene o establece si el producto maneja registro sanitario
    ''' </summary>
    Property HandlesHealthRegistration As Boolean?

    ''' <summary>
    ''' Obtiene o establece el registro sanitario del producto
    ''' </summary>
    Property HealthRegistration As String

    ''' <summary>
    ''' Obtiene o establece el Numero de Serial
    ''' </summary>
    Property SerialNumber As String


    ''' <summary>
    ''' Obtiene o establece la fecha de vencimiento R.S del producto
    ''' </summary>
    Property ExpirationDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece el id del grupo de facturación
    ''' </summary>
    Property BillingGroupId As Integer?

    ''' <summary>
    ''' Obtiene o establece si el producto es de control
    ''' </summary>
    Property ProductControl As Boolean?

    ''' <summary>
    ''' Obtiene o establece si el producto tiene control de precios
    ''' </summary>
    Property ProductWithPriceControl As Boolean?

    ''' <summary>
    ''' Obtiene o establece si el producto esta en el POS
    ''' </summary>
    Property POSProduct As Boolean?

    ''' <summary>
    ''' Obtiene o establece el numero de autorizaciones por pedido
    ''' </summary>
    Property AuthorizationByOrderNumber As Integer?

    ''' <summary>
    ''' Obtiene o establece los días de expiración del producto
    ''' </summary>
    Property ExpirationDay As Integer?

    ''' <summary>
    ''' Obtiene o establece si tiene control maximo por periodo
    ''' </summary>
    Property MaximumControlPeriod As Boolean?

    ''' <summary>
    ''' Obtiene o establece los dias de control que va a tener
    ''' </summary>
    Property ControlDays As Integer?

    ''' <summary>
    ''' Obtiene o establece si el producto tiene control de cantidad por orden
    ''' </summary>
    Property ControlOrderQuantity As Boolean?

    ''' <summary>
    ''' Obtiene o establece la cantidad de producto por orden
    ''' </summary>
    Property ProductOrderAmount As Integer?

    ''' <summary>
    ''' Obtiene o establece la ultima compra del producto
    ''' </summary>
    Property LastPurchase As DateTime?

    ''' <summary>
    ''' Obtiene o establece la ultima venta del producto
    ''' </summary>
    Property LastSale As DateTime?

    ''' <summary>
    ''' Obtiene o establece el origen del producto
    ''' </summary>
    Property ProductOrigin As Byte?

    ''' <summary>
    ''' Obtiene o establece el stock minimo del producto
    ''' </summary>
    Property MinimumStock As Integer?

    ''' <summary>
    ''' Obtiene o establece el stock maximo del producto
    ''' </summary>
    Property MaximumStock As Integer?

    ''' <summary>
    ''' Obtiene o establece el porcentaje de comision
    ''' </summary>
    Property CommissionPercentage As Decimal?

    ''' <summary>
    ''' Obtiene o establece el punto de reposicion
    ''' </summary>
    Property RepositionPoint As Integer?

    ''' <summary>
    ''' Obtiene o establece el tiempo de reposicion
    ''' </summary>
    Property ResetTime As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de moneda
    ''' </summary>
    Property CurrencyType As Byte?

    ''' <summary>
    ''' Obtiene o establece el porcentaje de variación del costo promedio
    ''' </summary>
    Property ControlCostPercentage As Decimal?

    ''' <summary>
    ''' Obtiene o establece el costo del producto
    ''' </summary>
    Property ProductCost As Decimal?

    ''' <summary>
    ''' Obtiene o establece el precio del ultimo costo del producto
    ''' </summary>
    Property FinalProductCost As Decimal?

    ''' <summary>
    ''' Obtiene o establece el precio de venta del producto
    ''' </summary>
    Property SellingPrice As Decimal?

    ''' <summary>
    ''' Obtiene o establece el estado del producto
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource de tipo de productos
    ''' </summary>
    Property ProductTypeDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de ATC
    ''' </summary>
    Property ATCDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de niveles de riesgo
    ''' </summary>
    Property RiskLevelDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de temperaturas de almacenamiento
    ''' </summary>
    Property StorageTemperatureDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de Unidad de empaque
    ''' </summary>
    Property PackingUnitDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de Grupo
    ''' </summary>
    Property GroupDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de subgrupo
    ''' </summary>
    Property SubGroupDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de fabricante
    ''' </summary>
    Property ManufacturerDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de iva
    ''' </summary>
    Property IVADatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the billing group datasource.
    ''' </summary>
    ''' <value>
    ''' The billing group datasource.
    ''' </value>
    Property BillingGroupDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de tipo de moneda
    ''' </summary>
    Property CurrencyTypeDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    Property Sequense As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece la unidad de manejo minimo que tendra el producto
    ''' </summary>
    Property DriveUnit As Integer?

    ''' <summary>
    ''' Obtiene o establece la temperatura minima en la que se almacenara el producto
    ''' </summary>
    Property MinimumTemperature As Integer?

    ''' <summary>
    ''' Obtiene o establece la temperatura maxima en la que se almacenara el producto
    ''' </summary>
    Property MaximumTemperature As Integer?

    ''' <summary>
    ''' Obtiene o establece el estado de registro sanitario
    ''' </summary>
    Property SanitaryRegistration As Integer?

    ''' <summary>
    ''' Obtiene o establece la abreviatura del producto
    ''' </summary>
    Property Abbreviation As String

    ''' <summary>
    ''' Obtiene o establece el id del insumo
    ''' </summary>
    Property SupplieId As Integer?

    ''' <summary>
    ''' Obtiene o establece el identificador unico de medicamento
    ''' </summary>
    Property IUM As String

    ''' <summary>
    ''' Obtiene o establece el datasource de insumos
    ''' </summary>
    Property SupplieDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el tipo de almacenamiento del medicamento
    ''' </summary>
    Property Storage As Integer?

    ''' <summary>
    ''' Obtiene o asigna los permisos que el usuario tiene asignados en éste formulario
    ''' </summary>
    ''' <value>Diccionario de permisos del usuario</value>
    ''' <returns>El diccionario de permisos del usuario</returns>
    Property PermissionsForm As Dictionary(Of Integer, String)

    ''' <summary>
    ''' Obtiene o establece el valor de producto gravado
    ''' </summary>
    Property TaxedProduct As Boolean

    ''' <summary>
    ''' propiedad que obitene o establece si un producto liquida iva en ventas Salud
    ''' </summary>
    ''' <returns></returns>
    Property LiquidateSalesTaxes As Boolean
	''' <summary>
	''' Propiedad donde se almacenara si tiene reporte sismed
	''' </summary>
	''' <returns></returns>
	Property SismedReport As Boolean
    ''' <summary>
    ''' Propiedad donde se almacenara si es un componente lácteo
    ''' </summary>
    ''' <returns></returns>
    Property DairyComponent As Boolean

    ''' <summary>
    ''' obtiene o establece el tipo de medicamento 
    ''' </summary>
    ''' <returns></returns>
    Property MedicationTypeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el peso de los insumos de tipo nutricion parenteral
    ''' </summary>
    Property WeightParenteralNutritionSupply As Decimal?

#Region "Datasource"

    ''' <summary>
    ''' obtiene o establece el tipo de medicamento
    ''' </summary>
    ''' <returns></returns>
    Property MedicationTypeDataSource As XPInstantFeedbackSource

#End Region


End Interface
