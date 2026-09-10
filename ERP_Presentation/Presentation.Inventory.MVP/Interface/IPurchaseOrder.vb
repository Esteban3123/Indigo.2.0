'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IPurchaseOrder
    Inherits IcrudBase

#Region "Variables"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.InventorySequence

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el consecutivo de la Orden de Compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o Establece el Tipo de Orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OrderType As Integer

    ''' <summary>
    ''' Obtiene o Establece el Id de Contrato de Inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IsBasedContract As Boolean

    ''' <summary>
    ''' Obtiene o Establece el Id de Contrato de Inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractId As Integer

    ''' <summary>
    ''' Obtiene o Establece la Fecha del Documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime?

    ''' <summary>
    ''' Obtiene o Establece la Fecha de Entrega
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeliveredDate As DateTime?

    ''' <summary>
    ''' Obtiene o Establece el Id del Proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierId As Integer

    ''' <summary>
    ''' Obtiene o Establece el Id de la Línea de Distribución del Proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierDistributionLineId As Integer?

    ''' <summary>
    ''' Obtiene o Establece el Id del Almacén
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarehouseId As Integer?

    ''' <summary>
    ''' Obtiene o Establece la Descripción de la Orden de la Compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Obtiene o Establece la Forma de Entrega
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeliveryMethod As String

    ''' <summary>
    ''' Obtiene o Establece el lugar de Entrega
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeliveryPlace As String

    ''' <summary>
    ''' Obtiene o Establece el Valor de la Orden de Compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Value As Decimal

    ''' <summary>
    ''' Obtiene o Establece el Valor de Descuento de la Orden de Compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DiscountValue As Decimal

    ''' <summary>
    ''' Obtiene o Establece el Valor de IVA de la Orden de Compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IvaValue As Decimal

    ''' <summary>
    ''' Obtiene o Establece el Valor Total de la Orden de Compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TotalValue As Decimal

    ''' <summary>
    ''' bandera que Especifica que fue creado desde el formulario de material osteosintesis de cristal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OsteosynthesisEquipment As Boolean

    ''' <summary>
    ''' Obtiene o establece el id de la cabecera de la orden de procedimientos QX(HCORDPROQ)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OsteosynthesisEquipmentId As Integer?

    ''' <summary>
    ''' Obtiene o Establece el Estado de la Orden de Compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Status As String

    ''' <summary>
    ''' obtiene o establece el Id de la moneda del documento
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId(Optional CurrencyAbbreviation As String = Nothing) As Integer

    ''' <summary>
    ''' Obtiene o establece el metodo de pago 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TypePaymentMethod As Integer?

    ''' <summary>
    ''' Obtiene o Establece la unidad funcional requerida 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitRequestId As Integer?

    ''' <summary>
    ''' Obtiene o Establece los dias de plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DaysTerm As Integer?




#End Region

#Region "XPO"

    ''' <summary>
    ''' Obtiene la Lista de Lineas de Distribución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SuppliersDistributionLinesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene la Lista de Proveedores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListSupplier As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene la Lista de Almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListWarehouse As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene la Lista de Contratos de Inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListContractInventory As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource del combo de moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyDatasource As XPInstantFeedbackSource

#Region "Budget Interface"

    ''' <summary>
    ''' Datasource de entidades de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityXpo As XPCollection

    ''' <summary>
    ''' Datasource de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As XPCollection

    ''' <summary>
    ''' Establece el datasource de las disponibilidades
    ''' </summary>
    ''' <returns></returns>
    Property AvailabilityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Disponibilidades asociadas al contrato 
    ''' </summary>
    ''' <returns></returns>
    Property ListPurchaseOrderAvailability As List(Of PurchaseOrderAvailability)

#End Region

#End Region

End Interface
