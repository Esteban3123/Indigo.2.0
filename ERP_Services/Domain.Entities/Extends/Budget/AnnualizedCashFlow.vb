Imports System.Runtime.Serialization

Partial Public Class AnnualizedCashFlow
    <DataMember>
    Property CodeNameCategory As String

    <DataMember>
    Property CategoryResource As String

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

End Class
