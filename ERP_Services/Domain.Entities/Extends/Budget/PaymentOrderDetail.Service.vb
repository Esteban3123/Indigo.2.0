#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class PaymentOrderDetail

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
    ''' Obtiene o establece el saldo del documento a afectar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property BalanceAffects As Decimal

    ''' <summary>
    ''' Obtiene o establece el codigo de la obligación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CodeObligation As String

    ''' <summary>
    ''' Obtiene o establece el codigo del compromiso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CodeCommitment As String

    ''' <summary>
    ''' Obtiene o establece el id del rubro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CategoryId As Integer

    ''' <summary>
    ''' obtiene o establece la fecha de la obligación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property ObligationDate As Date

    ''' <summary>
    ''' Obtiene o establece el id del tipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RevenueTypeId As Integer

#End Region

End Class
