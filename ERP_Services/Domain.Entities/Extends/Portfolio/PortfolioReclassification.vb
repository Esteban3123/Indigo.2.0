Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class PortfolioReclassification
    Inherits Entity(Of PortfolioReclassification)


    <DataMember>
    Property AccountReceivableDescription As String

    <DataMember>
    Property SourceAccountDescription As String

    <DataMember>
    Property TargetAccountDescription As String

    ''' <summary>
    ''' id de la moneda del las facturas asociadas al documento
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CurrencyId As Integer?

    ''' <summary>
    ''' abbreviacion segun ISO4217 de la moneda de las facturas
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CurrencyAbbreviation As String
End Class
