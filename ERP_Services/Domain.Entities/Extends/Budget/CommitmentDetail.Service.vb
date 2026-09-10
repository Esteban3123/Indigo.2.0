#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class CommitmentDetail

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre de la categoria
    ''' </summary>
    <DataMember()>
    Public Property CodeNameCategory As String

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
    ''' Obtiene o establece el saldo del presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property BalanceBudget As Decimal

    ''' <summary>
    ''' Obtiene o establece el saldo a afectar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property BalanceAffects As Decimal

    ''' <summary>
    ''' Obtiene o establece la fecha de la disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DateAvailability As DateTime

    ''' <summary>
    ''' Obtiene o establece la fecha de expiración de la disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DateExpirationAvailability As DateTime

    ''' <summary>
    ''' Obtiene o establece el codigo de la disponibilidad
    ''' </summary>
    <DataMember()>
    Public Property CodeAvailability As String

#End Region

End Class
