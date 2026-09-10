'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
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
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls
#End Region

Public Interface IBasicBillingDetail

#Region "Properties"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Id de la direccion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property AddressId As Integer

    ''' <summary>
    ''' tipo de contribuyente del tercero
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ContributionType As Byte

    ''' <summary>
    ''' Id del Almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property WareHouseId As Integer

    ''' <summary>
    ''' Id de la tarifa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property FeeId As Integer?

    ''' <summary>
    ''' Id del Almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property BasicBillingDetail As Domain.Entities.BasicBillingDetail

    ''' <summary>
    ''' Indica si se esta editando el registro
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property EditMode As Boolean

    ''' <summary>
    ''' Nivel de redondeo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RoundLevel As Integer

    ''' <summary>
    ''' Tipo de Detalle 
    '''   1. Productos
    '''   2. Servicios
    '''   3. Activos Fijos
    '''   4. Partes de Activos Fijos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DetailType As Integer

    ''' <summary>
    ''' Id Concepto de Facturacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillingConceptId As Integer?

    ''' <summary>
    ''' Id Activo Fijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PhysicalAssetId As Integer?

    ''' <summary>
    ''' Id de la Parte del Activo Fijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PhysicalAssetPartId As Integer?

    ''' <summary>
    ''' Valor del detalle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Value As Decimal

    ''' <summary>
    ''' Valor del descuento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ValueDiscount As Decimal

    ''' <summary>
    ''' Valor del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ValueIVA As Decimal

    ''' <summary>
    ''' Valor del Detalle - Descuento + IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property SubTotalValue As Decimal

    ''' <summary>
    ''' Id Concepto Retencion en la Fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RetentionIdTax As Integer?

    ''' <summary>
    ''' Porcentaje de la Retencion en la Fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RetentionPercentageTax As Decimal

    ''' <summary>
    ''' Base Retencion en la Fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RetentionBaseTax As Decimal

    ''' <summary>
    ''' Id Concepto Retencion de ICA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RetentionIdICA As Integer?

    ''' <summary>
    ''' Porcentaje de la Retencion de ICA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RetentionPercentageICA As Decimal

    ''' <summary>
    ''' Base Retencion de ICA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RetentionBaseICA As Decimal

    ''' <summary>
    ''' Establece el id Servicios prestados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServicesProvided As Integer?

    ''' <summary>
    ''' Establece el id Proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Supplier As Integer?

    ''' <summary>
    ''' Establece el id Ejecutivo de ventas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesExecutive As Integer?

    ''' <summary>
    ''' Id de la Unidad Funcional
    ''' </summary>
    ''' <returns></returns>
    Property FunctionalUnitId As Integer

    ''' <summary>
    ''' Id del Centro de costo asociado a la unidad funcional
    ''' </summary>
    ''' <returns></returns>
    Property CostCenterId As Integer?

    ''' <summary>
    ''' Id de la actividad económica asociada a los detalles de la factura
    ''' </summary>
    ''' <returns></returns>
    Property EconomicActivityId As Integer?

#End Region

#Region "XPO"

    ''' <summary>
    ''' Datasource de los Conceptos de Facturación
    ''' </summary>
    ''' <returns></returns>
    Property ProductAndServiceFeeXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los Conceptos de Facturación
    ''' </summary>
    ''' <returns></returns>
    Property BillingConceptXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los Activos Fijos
    ''' </summary>
    ''' <returns></returns>
    Property PhysicalAssetXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de las Partes de los Activos Fijos
    ''' </summary>
    ''' <returns></returns>
    Property PhysicalAssetPartXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los almacenes
    ''' </summary>
    ''' <returns></returns>
    Property WarehouseXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de Servicios prestados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServicesProvidedXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de Proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de Ejecutivo de ventas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesExecutiveXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de la Unidad Funcional (Si maneja productos)
    ''' </summary>
    ''' <returns></returns>
    Property FunctionalUnitXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de las Actividades Económicas
    ''' </summary>
    ''' <returns></returns>
    Property EconomicActivityDatasource As XPInstantFeedbackSource

#End Region

End Interface