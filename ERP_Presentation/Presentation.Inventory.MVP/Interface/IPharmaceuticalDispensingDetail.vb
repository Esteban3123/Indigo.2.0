'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán
' Created          : 26-01-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Domain.Entities
Imports DevExpress.Data.Linq

#End Region

Public Interface IPharmaceuticalDispensingDetail

#Region "Properties"

    ''' <summary>
    ''' Grupo de Atención
    ''' </summary>
    Property CareGroupId As Integer

    ''' <summary>
    ''' Producto
    ''' </summary>
    Property ProductId As Integer

    ''' <summary>
    ''' Almacén
    ''' </summary>
    Property WarehouseId As Integer

    ''' <summary>
    ''' cantidad a dispensar
    ''' </summary>
    Property Quantity As Integer

    ''' <summary>
    ''' Fecha en que se dispensó el medicamento
    ''' </summary>
    Property ServiceDate As DateTime

    ''' <summary>
    ''' Unidad funcional
    ''' </summary>
    Property FunctionalUnitId As Integer

    ''' <summary>
    ''' Especifica el Codigo del Profesional de la Salud que ordeno el Producto. Estos datos se sacan de la tabla de Crystal INPROFSAL
    ''' </summary>
    Property OrderedHealthProfessionalCode As String

    ''' <summary>
    ''' Especifica la especialidad el profesional que ordeno
    ''' </summary>
    Property OrderedProfessionalSpecialty As String

    ''' <summary>
    ''' Número de autorizacion
    ''' </summary>
    Property AuthorizationNumber As String

    ''' <summary>
    ''' Especifica el tipo de liquidaicon: 1 - Manual Tarifario; 2 - Incluido al 100% dentro de un servicio IPS
    ''' </summary>
    Property LiquidationType As Byte

    ''' <summary>
    ''' Especifica el id del cups que se debe encontrar en la orden de servicio, Este campo se solicita solo si el tipo de liquidacion es 2, es decir que esta incluido al 100% dentro de un servicio
    ''' </summary>
    Property CupsEntityId As Integer?

    ''' <summary>
    ''' Especifica si se le aplico recargo al calculo del precio de venta
    ''' </summary>
    Property SurchargeApply As Boolean

    ''' <summary>
    ''' Especifica el precio de venta del producto, Valor Unitario
    ''' </summary>
    Property SalePrice As Decimal

    ''' <summary>
    ''' Especifica el Costo promedio del producto, Valor Unitario
    ''' </summary>
    Property AverageCost As Decimal

    ''' <summary>
    ''' Especifica el ultimo Costo producto, Valor Unitario
    ''' </summary>
    Property FinalProductCost As Decimal

    ''' <summary>
    ''' Especifica el subtotal del valor de venta, el cual es el valor de venta unitario por la cantidad a dispensar
    ''' </summary>
    Property SubTotalSales As Decimal

    ''' <summary>
    ''' Porcentaje de descuento que se le va aplicar a al subtotal
    ''' </summary>
    Property DiscountPercentage As Decimal

    ''' <summary>
    ''' Valor del descuento, se obtiene multiplicando el SubTotalSales por el porcentaje del descuento
    ''' </summary>
    Property DiscountValue As Decimal

    ''' <summary>
    ''' Id del lote/serial
    ''' </summary>
    Property BatchSerialId As Integer

    ''' <summary>
    ''' Gets or sets the quantity batch serial.
    ''' </summary>
    Property QuantityBatchSerial As Decimal
    ''' <summary>
    ''' 
    ''' </summary>
    WriteOnly Property PatientCode As String

    ''' <summary>
    ''' Detalle de la dispensacion farmaceutica, en esta tabla de especifica las cantidades que salieron de cada lote
    ''' </summary>
    Property ListPharmaceuticalDispensingDetailBatchSerial As List(Of Domain.Entities.PharmaceuticalDispensingDetailBatchSerial)

    ''' <summary>
    ''' Valor del subtotal
    ''' </summary>
    ''' <returns></returns>
    Property GrossValue As Decimal

    ''' <summary>
    ''' valor del impuesto del producto
    ''' </summary>
    ''' <returns></returns>
    Property TaxValue As Decimal
#Region "DataSource"
    ''' <summary>
    ''' Datasource de Grupos de Atensión
    ''' </summary>
    Property CareGroupDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de Productos
    ''' </summary>
    Property ProductDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de Almacenes
    ''' </summary>
    Property WareHouseDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de Cups
    ''' </summary>
    Property CupsDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de Medicos
    ''' </summary>
    Property HealthProfessionalDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de unidades funcionales
    ''' </summary>
    Property FunctionalUnitDatasource As XPInstantFeedbackSource
#End Region

#End Region
#Region "Methods"
    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Sub LoadControls()
#End Region
End Interface
