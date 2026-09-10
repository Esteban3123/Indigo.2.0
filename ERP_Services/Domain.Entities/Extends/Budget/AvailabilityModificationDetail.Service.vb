#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class AvailabilityModificationDetail

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el saldo de la disponibilidad
    ''' </summary>
    <DataMember()>
    Public Property BalanceAvailability As Decimal

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
    ''' Obtiene o establece el id del tipo
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
    ''' Obtiene o establece el saldo del presupuesto
    ''' </summary>
    <DataMember()>
    Public Property BalanceBudget As Decimal

    ''' <summary>
    ''' Obtiene o establece el codigo CPC
    ''' </summary>
    <DataMember()>
    Public Property CPCCodeId As Integer?
    ''' <summary>
    ''' Obtiene o establece el codigo CPC
    ''' </summary>
    <DataMember()>
    Public Property CPCCode As String
    ''' <summary>
    ''' Obtiene el LinkAccount de ccpet
    ''' </summary>
    <DataMember()>
    Public Property CCPETLinkAccount As Boolean
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CategoryCPCCodeId As Integer?

#End Region

End Class
