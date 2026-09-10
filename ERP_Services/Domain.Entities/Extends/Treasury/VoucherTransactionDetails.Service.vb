Imports System.Runtime.Serialization
Imports Domain.Base.Entities


Partial Public Class VoucherTransactionDetails
    Inherits Entity(Of VoucherTransactionDetails)

#Region "New"
    <DataMember()>
    Property FullNameMainAccount As String
    <DataMember()>
    Property FullNameThirdParty As String
    <DataMember()>
    Property FullNameCostCenter As String
    <DataMember()>
    Property ExpenseConceptCode As String
    <DataMember()>
    Property ExpenseConceptName As String
    <DataMember()>
    Property ExpenseConceptBehavior As Byte
    <DataMember()>
    Property NatureName As String

    'Para Traslados (se generaliza para guardar codigo y nombre de cajas o bancos)
    <DataMember()>
    Property EntityCode As String
    <DataMember()>
    Property EntityName As String


    'Valores de Anticipos de Pagos (cuando se crean las facturas)
    <DataMember()>
    Property AdvanceValue As Decimal
    <DataMember()>
    Property AdvanceDetail As String

    'Listado de facturas eliminadas
    <DataMember()>
    Property DischargeBillDelete As List(Of DischargeBill)

    ''' <summary>
    ''' bandera utilizada cuando se va a eliminar un detalle que tenga facturas relacionadas entonces para primero buscar las facturas y eliminarlas y luego eliminar este detalle
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if this instance is delete; otherwise, <c>false</c>.
    ''' </value>
    <DataMember()>
    Property IsDelete As Boolean

    <DataMember>
    Property CodeNameCashFlowConcept As String

    <DataMember()>
    Property BaseValueDiscount As Decimal

    ''' <summary>
    ''' Valor del anticipo en la moneda de la cabecera del comprobante
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property ValueAdvanceInCurrencyHeader As Decimal

    ''' <summary>
    ''' Entidad de la moneda del anticipo
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property CurrencyAdvance As Currency

    ''' <summary>
    ''' tasa de conversion del anticipo
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property TRMValueAdvance As Decimal

    ''' <summary>
    ''' Entidad moneda de la caja menor a la cual se hace el reembolso
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property CurrencyCashRegister As Currency

    <DataMember>
    Property VoucherTransactionDetailsAccountInfo As New List(Of VoucherTransactionDetailAccountInfo)()
#End Region

End Class

Public Class VoucherTransactionDetailAccountInfo
    <DataMember>
    Public Property MainAccountCodeName As String
    <DataMember>
    Public Property CostCenterCodeName As String
    <DataMember>
    Public Property NatureName As String
    <DataMember>
    Public Property ValueTotalConcept As Decimal
    <DataMember>
    Public Property Nature As Byte?
    <DataMember>
    Public Property ConceptName As String
    <DataMember>
    Public Property ThirdPartyNitName As String
    ''' <summary>
    ''' solo para reporte el valor del debito
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property DebitValue As Decimal

    ''' <summary>
    ''' solo para reporte el valor del credito
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property CreditValue As Decimal

    ''' <summary>
    ''' propiedad que almacena una  del detalle Observacion 
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property Observation As String
End Class