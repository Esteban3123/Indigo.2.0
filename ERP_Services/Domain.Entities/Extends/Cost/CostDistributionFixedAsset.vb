Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class CostDistributionFixedAsset

#Region "Properties"

    <DataMember()>
    Public Property OperatingUnitId As Integer

    <DataMember()>
    Property FixedAssetItemCodeDescription As String

    <DataMember()>
    Property FixedAssetLocationCodeName As String

    <DataMember()>
    Property ThirdPartyNitName As String

    <DataMember()>
    Property Checked As Boolean

#End Region

#Region "Methods"

    Public Sub CalculateDetails()
        Me.CalculateDetailPercentages()
        Me.CalculateDetailValues()
    End Sub

    Public Sub CalculateDetailPercentages()
        If Me.CostDistributionFixedAssetDetail IsNot Nothing AndAlso Me.CostDistributionFixedAssetDetail.Count > 0 Then
            Me.HoursWorked = Me.CostDistributionFixedAssetDetail.Sum(Function(d) d.HoursQuantity)
            Dim outstandingPercentage As Decimal = If(Me.HoursWorked = 0, 0, 1)

            For Each detail In Me.CostDistributionFixedAssetDetail
                detail.PercentageDistribution = If(Me.HoursWorked = 0, 0, Utils.RoundDown((detail.HoursQuantity / Me.HoursWorked), 6))
                detail.Proportion = detail.PercentageDistribution * 100

                outstandingPercentage = Utils.RoundDown((outstandingPercentage - detail.PercentageDistribution), 6)
            Next

            If outstandingPercentage <> 0 Then
                Dim adjustedPercentage As Decimal = 0
                For Each detail In Me.CostDistributionFixedAssetDetail
                    adjustedPercentage = Utils.CalculateAdjustedValue(outstandingPercentage, detail.PercentageDistribution, outstandingPercentage)

                    detail.PercentageDistribution = detail.PercentageDistribution + adjustedPercentage
                    detail.Proportion = detail.PercentageDistribution * 100

                    outstandingPercentage = outstandingPercentage - adjustedPercentage

                    If outstandingPercentage = 0 Then
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub CalculateDetailValues()
        If Me.CostDistributionFixedAssetDetail IsNot Nothing AndAlso Me.CostDistributionFixedAssetDetail.Count > 0 Then
            Dim outstandingDepreciationValue As Decimal = Me.DepreciationValue

            For Each detail In Me.CostDistributionFixedAssetDetail
                detail.DepreciationValue = Utils.RoundDown(detail.PercentageDistribution * Me.DepreciationValue, 4)
            Next

            outstandingDepreciationValue = Me.DepreciationValue - Me.CostDistributionFixedAssetDetail.Sum(Function(d) d.DepreciationValue)

            If outstandingDepreciationValue <> 0 Then
                Dim adjustedDepreciationValue As Decimal = 0

                For Each detail In Me.CostDistributionFixedAssetDetail
                    adjustedDepreciationValue = IIf(outstandingDepreciationValue > detail.DepreciationValue, detail.DepreciationValue, outstandingDepreciationValue)
                    detail.DepreciationValue = detail.DepreciationValue + adjustedDepreciationValue
                    outstandingDepreciationValue = outstandingDepreciationValue - adjustedDepreciationValue

                    If outstandingDepreciationValue = 0 Then
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

#End Region

End Class
