#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class CommitmentModificationDetail

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la fecha de vencimiento
    ''' </summary>
    <DataMember()>
    Public Property DateExpired As Date

    ''' <summary>
    ''' Obtiene o establece el saldo del compromiso
    ''' </summary>
    <DataMember()>
    Public Property BalanceCommitment As Decimal

    ''' <summary>
    ''' Obtiene o establece el id del rubro
    ''' </summary>
    <DataMember()>
    Public Property CategoryId As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre de la categoria
    ''' </summary>
    <DataMember()>
    Public Property CodeNameCategory As String

    ''' <summary>
    ''' obtiene o establece el id del tipo
    ''' </summary>
    <DataMember()>
    Public Property RevenueTypeId As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del tipo de ingreso
    ''' </summary>
    <DataMember()>
    Public Property CodeNameRevenueType As String

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre de la fuente de financiacion
    ''' </summary>
    <DataMember()>
    Public Property CodeNameFinancialSource As String

    ''' <summary>
    ''' Obtiene o establece el id de la disponibilidad
    ''' </summary>
    <DataMember>
    Property AvailabilityDetailId As Integer?

    ''' <summary>
    ''' Obtiene o establece el codigo de la disponibilidad
    ''' </summary>
    <DataMember()>
    Public Property CodeAvailability As String

    ''' <summary>
    ''' Obtiene o establece el saldo de la disponibilidad o el presupuesto
    ''' </summary>
    <DataMember()>
    Public Property BalanceAffects As Decimal

#End Region

End Class
