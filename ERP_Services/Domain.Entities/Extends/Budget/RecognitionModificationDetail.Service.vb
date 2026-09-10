#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class RecognitionModificationDetail

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

#End Region

End Class
