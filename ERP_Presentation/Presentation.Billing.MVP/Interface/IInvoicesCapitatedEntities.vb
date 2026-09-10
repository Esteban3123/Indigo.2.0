'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
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

Public Interface IInvoicesCapitatedEntities
    Inherits IcrudBase

    ''' <summary>
    ''' Código de la entidad
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' Grupo de atención
    ''' </summary>
    Property CareGroupId As Integer

    ''' <summary>
    ''' Periodo factura
    ''' </summary>
    ''' <returns></returns>
    Property InvoicePeriod As Integer?

    ''' <summary>
    ''' Id de la factura previa relacionada
    ''' </summary>
    ''' <returns></returns>
    Property PreviousRIPSInvoice As Integer?

    ''' <summary>
    ''' Id de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer

    ''' <summary>
    ''' Fecha inicial
    ''' </summary>
    Property InitialDate As DateTime?

    ''' <summary>
    ''' Fecha Final
    ''' </summary>
    Property EndDate As DateTime?

    ''' <summary>
    ''' Número de usuario
    ''' </summary>
    Property UserNumber As Integer

    ''' <summary>
    ''' Valor de usuario
    ''' </summary>
    Property UserValue As Decimal

    ''' <summary>
    ''' Valor total
    ''' </summary>
    Property TotalValue As Decimal

    Property SubTotalValue As Decimal

    ''' <summary>
    ''' Estado
    ''' </summary>
    Property Status As Byte

    ''' <summary>
    ''' datasource de grupo de atencion
    ''' </summary>
    Property CareGroupXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Property Sequense As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Obtiene o establece la categoria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CategoryId As Integer?

    ''' <summary>
    ''' Obtiene o establece el porcentaje de descuento
    ''' </summary>
    ''' <returns></returns>
    Property DiscountPercentage As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del descuento
    ''' </summary>
    ''' <returns></returns>
    Property DiscountValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del recaudo por copago
    ''' </summary>
    ''' <returns></returns>
    Property CopaymentAmount As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del recaudo por pago compartido
    ''' </summary>
    ''' <returns></returns>
    Property SharedPaymentAmouny As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del recaudo por cuota moderadora
    ''' </summary>
    ''' <returns></returns>
    Property ModeratingFeeAmount As Decimal

    ''' <summary>
    ''' Establece el datasource de las categorias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CategoryXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el DataSource de la factura previa RIPS
    ''' </summary>
    ''' <returns></returns>
    Property PreviousRIPSInvoiceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el DataSource del campo moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Observaciones de la factura capita
    ''' </summary>
    ''' <returns></returns>
    Property Observations As String
End Interface
