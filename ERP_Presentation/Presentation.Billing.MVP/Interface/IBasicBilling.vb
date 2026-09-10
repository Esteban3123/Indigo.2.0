'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-11-13
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

Public Interface IBasicBilling
    Inherits IcrudBase

#Region "Fields"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequence As Domain.Entities.BillingSequence

#End Region

#Region "Properties"

    ReadOnly Property ApplyRetention As Boolean

    ReadOnly Property RetentionIdIVA As Integer?

    ReadOnly Property RetentionPercentageIVA As Decimal

    ''' <summary>
    ''' Codigo de la Factura Basica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Fecha del Documneto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' Descripcion o Observacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Modalidad de Pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SaleModality As Byte

    ''' <summary>
    ''' Unidad Operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OperatingUnitId As Integer

    ''' <summary>
    ''' Autorizacion de Facturacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillingAuthorizationId As Integer

    ''' <summary>
    ''' Id del Cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyCustomerId As String

    ''' <summary>
    ''' Id de la cuenta contable asociada al cliente
    ''' </summary>
    ''' <returns></returns>
    Property MainAccountId As Integer?

    ''' <summary>
    ''' Id de la Direccion
    ''' </summary>
    ''' <returns></returns>
    Property AddressId As Integer

    ''' <summary>
    ''' Id del Almacen
    ''' </summary>
    ''' <returns></returns>
    Property WarehouseId As Integer?

    ''' <summary>
    ''' Id de la condicion de venta
    ''' </summary>
    ''' <returns></returns>
    Property ConditionSalesId As Integer?

    ''' <summary>
    ''' Valor de la Factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Value As Decimal

    ''' <summary>
    ''' Valor del descuento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueDiscount As Decimal

    ''' <summary>
    ''' Valor del IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueIVA As Decimal

    ''' <summary>
    ''' Valor de la Retencion en la Fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingTax As Decimal

    ''' <summary>
    ''' Valor de la Retencion de IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingIVA As Decimal

    ''' <summary>
    ''' Valor de la Retencion de ICA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingICA As Decimal

    ''' <summary>
    ''' Valor Total del Documento 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TotalValue As Decimal

    ''' <summary>
    ''' propiedad que obtiene o establece el id de la moneda, ademas establece la abreviacion
    ''' </summary>
    ''' <param name="currencyAbbreviation"></param>
    ''' <returns></returns>
    Property CurrencyId(Optional currencyAbbreviation As String = Nothing) As Integer

#End Region

#Region "XPO"

    ''' <summary>
    ''' Datasource de la Autorizacion de Facturacion
    ''' </summary>
    ''' <returns></returns>
    Property BillingAuthorizationXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource del Cliente
    ''' </summary>
    ''' <returns></returns>
    Property CustomerXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de las direcciones asociadas al Tercero del Cliente
    ''' </summary>
    ''' <returns></returns>
    Property AddressXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource del Almacen (Si maneja productos)
    ''' </summary>
    ''' <returns></returns>
    Property WarehouseXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' datasource de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyXpo As XPInstantFeedbackSource
#End Region

#Region "Budget Interface"

    ''' <summary>
    ''' Id de la entidad de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityId As Integer?

    ''' <summary>
    ''' Datasource de entidades de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityXpo As XPCollection

    ''' <summary>
    ''' Id de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityId As Integer?

    ''' <summary>
    ''' Datasource de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As XPCollection

    ''' <summary>
    ''' Id de la dependencia presupuestal Factura Básica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetId As Integer?

    ''' <summary>
    ''' Datasource de las dependencias presupuestales de Factura Básica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetXpo As XPCollection

#End Region

End Interface