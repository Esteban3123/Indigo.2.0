#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class RecognitionDetail

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el codigo de la categoria
    ''' </summary>
    <DataMember()>
    Public Property CodeCategory As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la categoria
    ''' </summary>
    <DataMember()>
    Public Property NameCategory As String

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del tipo de ingreso
    ''' </summary>
    <DataMember()>
    Public Property CodeNameRevenueType As String

    ''' <summary>
    ''' Obtiene o establece el id de la fuente de financiacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property FinancialSourceId As Integer

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
    Public Property ValueBalance As Decimal

    ''' <summary>
    ''' obtiene o establece el id de la cabecera del presupuesto inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property BudgetHeaderId As Integer

#End Region

End Class
