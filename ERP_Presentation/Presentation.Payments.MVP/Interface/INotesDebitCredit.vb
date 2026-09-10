'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface INotesDebitCredit
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el parametro de de pagos
    ''' </summary>
    ''' <value>
    ''' The payment concept datasource.
    ''' </value>
    Property PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Obtiene o establece el codigo del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CodeAdvancePayments As String

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCostCenter As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de proveedores con las lineas de distribucion
    ''' </summary>
    Property SuppliersDistributionLinesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de centros de costo
    ''' </summary>
    Property CostCenterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas por pagar
    ''' </summary>
    Property AccountPayableXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de anticipos a proveedores
    ''' </summary>
    Property AdvancePaymentsXpo As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el codigo de notas debito/credito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

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
    Property Sequense As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property SupplierXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdSupplier As Integer?

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DateDocument As DateTime?

    ''' <summary>
    ''' Obtiene o establece el id de la naturaleza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Nature As Integer?

    ''' <summary>
    ''' Obtiene o establece las observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Comments As String

    ''' <summary>
    ''' Obtiene o establece para que tipo aplica(factura, anticipo)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Apply As Byte?

    ''' <summary>
    ''' Obtiene o establece el listado de facturas por id proveedor y estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayableDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la entidad de cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property accountPayable As Domain.Entities.AccountPayable

    ''' <summary>
    ''' Obtiene o establece la entidad de anticipos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property advancePayments As Domain.Entities.AdvancePayments

    ''' <summary>
    ''' Obtiene o establece la fecha de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillDate As DateTime

    ''' <summary>
    ''' Obtiene o establece la fecha de vencimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExpiredDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el valor de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Value As Decimal

    ''' <summary>
    ''' Obtiene o establece el saldo de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Balance As Decimal

    ''' <summary>
    ''' Obtiene o establece el ajuste
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Adjustment As Decimal

    ''' <summary>
    ''' Id del concepto electrónico cuando se aplica nota
    ''' </summary>
    ''' <returns></returns>
    Property ConceptAdjusment As Integer?

    ''' <summary>
    ''' Obtiene o establece el porcentaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Percentage As Decimal

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Id As Integer

    ''' <summary>
    ''' Obtiene o establece el numero de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillNumber As String

    ''' <summary>
    ''' Obtiene o establece el listado de cuotas de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListAccountPayableShares As List(Of Domain.Entities.AccountPayableShares)

    ''' <summary>
    ''' Establece el id del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAdvancePayments As Integer

    ''' <summary>
    ''' Establece la fecha del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDateAdvance As DateTime

    ''' <summary>
    ''' Establece el valor del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueAdvance As Decimal

    ''' <summary>
    ''' Establece el saldo del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BalanceAdvance As Decimal

    ''' <summary>
    ''' Establece el ajuste del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdjustmentAdvance As Decimal

    ''' <summary>
    ''' Establece el porcentaje del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PercentageAdvance As Decimal

    ''' <summary>
    ''' Permite postular facturas para descuento pronto pago
    ''' </summary>
    ''' <returns></returns>
    Property AllowDiscountPromptPayment As Boolean

    ''' <summary>
    ''' Afecta base para descuento pronto pago
    ''' </summary>
    ''' <returns></returns>
    Property AffectBaseToDiscount As Boolean

    ''' <summary>
    ''' Id de la moneda del documento, recibe como parametro opcional la abreviacion para postular cuando se asigna el valor por codigo
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId(Optional CurrencyAbbreviation As String = Nothing) As Integer?

    ''' <summary>
    ''' DataSource de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property DataSourceCurrency As XPInstantFeedbackSource
#End Region

End Interface
