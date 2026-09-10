Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class CostDistributionManpower

#Region "Properties"

    <DataMember()>
    Public Property OperatingUnitId As Integer

    <DataMember()>
    Property GroupCodeName As String

    <DataMember()>
    Property ThirdPartyNitName As String

    <DataMember()>
    Property PositionCodeName As String

    <DataMember()>
    Public Property TotalDistribuited As Decimal

    <DataMember()>
    Property Checked As Boolean

    Public ReadOnly Property ManpowerTypeName As String
        Get
            Return IIf(Me.ManpowerType = 1, "Empleado", "Contratista")
        End Get
    End Property

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

        If Me.CostDistributionManpowerDetail IsNot Nothing AndAlso Me.CostDistributionManpowerDetail.Count > 0 Then
            For Each detail In Me.CostDistributionManpowerDetail
                detail.CalculateTotalDistribuited(CostEstimateLabor)
                detail.CalculateTotalDistribuitedRevalued(CostEstimateLabor)
            Next
        End If
    End Sub

    Public Sub CalculateDetails()
        Me.CalculateDetailPercentages()
        Me.CalculateDetailValues()
    End Sub

    Public Sub CalculateDetailPercentages()
        If Me.CostDistributionManpowerDetail IsNot Nothing AndAlso Me.CostDistributionManpowerDetail.Count > 0 Then
            Me.HoursWorked = Me.CostDistributionManpowerDetail.Sum(Function(d) d.HoursQuantity)
            Dim outstandingPercentage As Decimal = If(Me.HoursWorked = 0, 0, 1)

            For Each detail In Me.CostDistributionManpowerDetail
                detail.PercentageDistribution = If(Me.HoursWorked = 0, 0, Utils.RoundDown((detail.HoursQuantity / Me.HoursWorked), 6))

                outstandingPercentage = Utils.RoundDown((outstandingPercentage - detail.PercentageDistribution), 6)
            Next

            If outstandingPercentage <> 0 Then
                Dim adjustedPercentage As Decimal = 0
                For Each detail In Me.CostDistributionManpowerDetail
                    adjustedPercentage = Utils.CalculateAdjustedValue(outstandingPercentage, detail.PercentageDistribution, outstandingPercentage)

                    detail.PercentageDistribution = detail.PercentageDistribution + adjustedPercentage

                    outstandingPercentage = outstandingPercentage - adjustedPercentage

                    If outstandingPercentage = 0 Then
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub CalculateDetailValues()
        If Me.CostDistributionManpowerDetail IsNot Nothing AndAlso Me.CostDistributionManpowerDetail.Count > 0 Then
            Dim outstandingAccrued As Decimal = Me.TotalAccrued
            Dim outstandingProvision As Decimal = Me.TotalProvision
            Dim outstandingEmployerContribution As Decimal = Me.TotalEmployerContribution
            Dim outstandingParafiscal As Decimal = Me.TotalParafiscal

            For Each detail In Me.CostDistributionManpowerDetail
                detail.TotalAccrued = Utils.RoundDown(detail.PercentageDistribution * Me.TotalAccrued, 4)
                detail.TotalProvision = Utils.RoundDown(detail.PercentageDistribution * Me.TotalProvision, 4)
                detail.TotalEmployerContribution = Utils.RoundDown(detail.PercentageDistribution * Me.TotalEmployerContribution, 4)
                detail.TotalParafiscal = Utils.RoundDown(detail.PercentageDistribution * Me.TotalParafiscal, 4)
            Next

            outstandingAccrued = Me.TotalAccrued - Me.CostDistributionManpowerDetail.Sum(Function(d) d.TotalAccrued)
            outstandingProvision = Me.TotalProvision - Me.CostDistributionManpowerDetail.Sum(Function(d) d.TotalProvision)
            outstandingEmployerContribution = Me.TotalEmployerContribution - Me.CostDistributionManpowerDetail.Sum(Function(d) d.TotalEmployerContribution)
            outstandingParafiscal = Me.TotalParafiscal - Me.CostDistributionManpowerDetail.Sum(Function(d) d.TotalParafiscal)

            If outstandingAccrued <> 0 OrElse outstandingProvision <> 0 OrElse outstandingEmployerContribution <> 0 OrElse outstandingParafiscal <> 0 Then
                Dim adjustedAccrued As Decimal = 0
                Dim adjustedProvision As Decimal = 0
                Dim adjustedEmployerContribution As Decimal = 0
                Dim adjustedParafiscal As Decimal = 0

                For Each detail In Me.CostDistributionManpowerDetail
                    adjustedAccrued = IIf(outstandingAccrued > detail.TotalAccrued, detail.TotalAccrued, outstandingAccrued)
                    detail.TotalAccrued = detail.TotalAccrued + adjustedAccrued
                    outstandingAccrued = outstandingAccrued - adjustedAccrued

                    adjustedProvision = IIf(outstandingProvision > detail.TotalProvision, detail.TotalProvision, outstandingProvision)
                    detail.TotalProvision = detail.TotalProvision + adjustedProvision
                    outstandingProvision = outstandingProvision - adjustedProvision

                    adjustedEmployerContribution = IIf(outstandingEmployerContribution > detail.TotalEmployerContribution, detail.TotalEmployerContribution, outstandingEmployerContribution)
                    detail.TotalEmployerContribution = detail.TotalEmployerContribution + adjustedEmployerContribution
                    outstandingEmployerContribution = outstandingEmployerContribution - adjustedEmployerContribution

                    adjustedParafiscal = IIf(outstandingParafiscal > detail.TotalParafiscal, detail.TotalParafiscal, outstandingParafiscal)
                    detail.TotalParafiscal = detail.TotalParafiscal + adjustedParafiscal
                    outstandingParafiscal = outstandingParafiscal - adjustedParafiscal

                    If outstandingAccrued = 0 OrElse outstandingProvision = 0 OrElse outstandingEmployerContribution = 0 OrElse outstandingParafiscal = 0 Then
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

#End Region

End Class
