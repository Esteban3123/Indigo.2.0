Imports System.Runtime.Serialization

Partial Public Class PortfolioProvisionDetail

#Region "Properties"

    <DataMember>
    Property AgesDescription As String

    <DataMember>
    Property SelectOption As Boolean

    <DataMember>
    Property Deterioration As Decimal

    <DataMember>
    Property RegimenName As String

    <DataMember>
    Property ThirdPartyNitName As String

    <DataMember()>
    Property BalanceInvoice As Decimal

    <DataMember()>
    Property BalanceWithGlosa As Decimal

    <DataMember()>
    Property DeteriorationCalculated As Decimal

    <DataMember()>
    Property TypeBook As Integer?

    <DataMember()>
    Property PortfolioClassification As String

#End Region

#Region "Methods"

    ''' <summary>
    ''' Se calcula el deterioro, es necesario asignar
    ''' El saldo de la factura
    ''' El porcentaje
    ''' La expectativa
    ''' El valor acumulado de deterioro
    ''' Formula del VPN = Me.BalanceAccountReceivable / Math.Pow((1 + Me.Percentage / 100), Me.Expectative)
    ''' </summary>
    Public Sub CalculateDeterioration()
        Me.NetPresentValue = Me.BalanceAccountReceivable / Math.Pow((1 + Me.Percentage / 100), Me.Expectative)
        Me.Value = Me.BalanceAccountReceivable - Me.NetPresentValue
        Me.DeteriorationCalculated = Me.Value - Me.AccumulatedDeterioration
    End Sub

#End Region

End Class
