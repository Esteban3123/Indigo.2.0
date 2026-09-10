Imports System.Runtime.Serialization

Partial Public Class DistributionFixedAssetDetail

    <DataMember()> _
    Property SetProportion As Decimal
        Get
            Return Me.Proportion
        End Get
        Set(value As Decimal)
            If Me.DistributionFixedAsset IsNot Nothing AndAlso value <> Me.Proportion Then
                Me.Proportion = value
                Me.DepreciationValue = GetDistributionValue(value)
                If DistributionFixedAsset.DistributionFixedAssetDetail.Sum(Function(x) x.Proportion) = 100 Then
                    Dim differencevalue As Decimal = Math.Round(Me.DistributionFixedAsset.DepreciationValue - Me.DistributionFixedAsset.DistributionFixedAssetDetail.Sum(Function(x) x.DepreciationValue), 0)
                    Me.DepreciationValue += differencevalue
                End If
            End If
        End Set
    End Property

    Public Function GetDistributionValue(percent As Decimal) As Decimal
        If percent = 0D Then
            Return 0D
        End If
        Return Math.Round((percent / 100) * Me.DistributionFixedAsset.DepreciationValue, 0)
    End Function

End Class
