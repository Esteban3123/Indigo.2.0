Imports System.Runtime.Serialization

Partial Public Class ObligationModificationDetail

    ''' <summary>
    ''' Obtiene o establece el saldo de la obligacion
    ''' </summary>
    <DataMember>
    Property BalanceObligation As Decimal

    ''' <summary>
    ''' Obtiene o establece el id del rubro
    ''' </summary>
    <DataMember()>
    Property CategoryId As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre de la categoria
    ''' </summary>
    <DataMember>
    Property CodeNameCategory As String

    ''' <summary>
    ''' obtiene o establece el id del tipo
    ''' </summary>
    <DataMember()>
    Property RevenueTypeId As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del tipo de ingreso
    ''' </summary>
    <DataMember>
    Property CodeNameRevenueType As String

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre de la fuente de financiacion
    ''' </summary>
    <DataMember>
    Property CodeNameFinancialSource As String

    ''' <summary>
    ''' Obtiene o establece el id del compromiso
    ''' </summary>
    <DataMember>
    Property CommitmentDetailId As Integer?

    ''' <summary>
    ''' Obtiene o establece el saldo del compromiso
    ''' </summary>
    <DataMember>
    Property BalanceCommitment As Decimal

End Class
