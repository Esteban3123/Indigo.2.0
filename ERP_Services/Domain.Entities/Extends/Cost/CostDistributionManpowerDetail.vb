Imports System.Runtime.Serialization

Partial Public Class CostDistributionManpowerDetail

#Region "Properties"

    <DataMember()>
    Property ProductionCenterCodeName As String

    <DataMember()>
    Public Property TotalDistribuited As Decimal

    <DataMember()>
    Property PercentageDistribution As Decimal

    <DataMember()>
    Property TotalDistribuitedRevalued As Decimal

    <DataMember()>
    Property EntityCurrencyId As Integer

    <DataMember()>
    Property TRMValue As String = "Sin Conversión"

#End Region

#Region "Methods"

    Public Sub CalculateTotalDistribuited(CostEstimateLabor As Byte)
        If CostEstimateLabor = 1 Then
            Me.TotalDistribuited = Me.TotalAccrued
        ElseIf CostEstimateLabor = 2 Then
            Me.TotalDistribuited = Me.TotalAccrued + Me.TotalEmployerContribution
        ElseIf CostEstimateLabor = 3 Then
            Me.TotalDistribuited = Me.TotalAccrued + Me.TotalEmployerContribution + Me.TotalParafiscal
        ElseIf CostEstimateLabor = 4 Then
            Me.TotalDistribuited = Me.TotalAccrued + Me.TotalEmployerContribution + Me.TotalParafiscal + Me.TotalProvision
        End If
    End Sub

    Public Sub CalculateTotalDistribuitedRevalued(CostEstimateLabor As Byte)
        If CostEstimateLabor = 1 Then
            Me.TotalDistribuitedRevalued = Me.TotalAccruedRevalued
        ElseIf CostEstimateLabor = 2 Then
            Me.TotalDistribuitedRevalued = Me.TotalAccruedRevalued + Me.TotalEmployerContributionRevalued
        ElseIf CostEstimateLabor = 3 Then
            Me.TotalDistribuitedRevalued = Me.TotalAccruedRevalued + Me.TotalEmployerContributionRevalued + Me.TotalParafiscalRevalued
        ElseIf CostEstimateLabor = 4 Then
            Me.TotalDistribuitedRevalued = Me.TotalAccruedRevalued + Me.TotalEmployerContributionRevalued + Me.TotalParafiscalRevalued + Me.TotalProvisionRevalued
        End If
    End Sub

#End Region

End Class
