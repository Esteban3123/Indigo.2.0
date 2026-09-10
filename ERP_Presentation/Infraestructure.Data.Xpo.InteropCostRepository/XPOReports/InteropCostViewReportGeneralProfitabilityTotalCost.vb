Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("InteropCost.ViewReportGeneralProfitabilityTotalCost")> _
Public Class InteropCostViewReportGeneralProfitabilityTotalCost
    Inherits XPLiteObject
    Dim fConsecutive As Integer
    <Key(True)> _
    Public Property Consecutive() As Integer
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Consecutive", fConsecutive, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fOrganizationalStructureOfCostId As Integer
    Public Property OrganizationalStructureOfCostId() As Integer
        Get
            Return fOrganizationalStructureOfCostId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OrganizationalStructureOfCostId", fOrganizationalStructureOfCostId, value)
        End Set
    End Property
    Dim fArea As Decimal
    Public Property Area() As Decimal
        Get
            Return fArea
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Area", fArea, value)
        End Set
    End Property
    Dim fCenterType As Byte
    Public Property CenterType() As Byte
        Get
            Return fCenterType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CenterType", fCenterType, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property
    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property
    Dim fMonth As Integer
    Public Property Month() As Integer
        Get
            Return fMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Month", fMonth, value)
        End Set
    End Property
    Dim fAutoCostDistribution As Decimal
    Public Property AutoCostDistribution() As Decimal
        Get
            Return fAutoCostDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AutoCostDistribution", fAutoCostDistribution, value)
        End Set
    End Property
    Dim fDirectCostDistribution As Decimal
    Public Property DirectCostDistribution() As Decimal
        Get
            Return fDirectCostDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DirectCostDistribution", fDirectCostDistribution, value)
        End Set
    End Property
    Dim fManPowerDistributionDirect As Decimal
    Public Property ManPowerDistributionDirect() As Decimal
        Get
            Return fManPowerDistributionDirect
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ManPowerDistributionDirect", fManPowerDistributionDirect, value)
        End Set
    End Property
    Dim fDispensingDistribution As Decimal
    Public Property DispensingDistribution() As Decimal
        Get
            Return fDispensingDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DispensingDistribution", fDispensingDistribution, value)
        End Set
    End Property
    Dim fManPowerDistributionInDirect As Decimal
    Public Property ManPowerDistributionInDirect() As Decimal
        Get
            Return fManPowerDistributionInDirect
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ManPowerDistributionInDirect", fManPowerDistributionInDirect, value)
        End Set
    End Property
    Dim fTransferDistribution As Decimal
    Public Property TransferDistribution() As Decimal
        Get
            Return fTransferDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TransferDistribution", fTransferDistribution, value)
        End Set
    End Property
    Dim fGeneralExpensesDirect As Decimal
    Public Property GeneralExpensesDirect() As Decimal
        Get
            Return fGeneralExpensesDirect
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("GeneralExpensesDirect", fGeneralExpensesDirect, value)
        End Set
    End Property
    Dim fFixedAssetDistribution As Decimal
    Public Property FixedAssetDistribution() As Decimal
        Get
            Return fFixedAssetDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FixedAssetDistribution", fFixedAssetDistribution, value)
        End Set
    End Property
    Dim fInitialDistribution As Decimal
    Public Property InitialDistribution() As Decimal
        Get
            Return fInitialDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialDistribution", fInitialDistribution, value)
        End Set
    End Property
    Dim fIntermediateDistribution As Decimal
    Public Property IntermediateDistribution() As Decimal
        Get
            Return fIntermediateDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IntermediateDistribution", fIntermediateDistribution, value)
        End Set
    End Property
    Dim fSecondaryDirectCostDistribution As Decimal
    Public Property SecondaryDirectCostDistribution() As Decimal
        Get
            Return fSecondaryDirectCostDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SecondaryDirectCostDistribution", fSecondaryDirectCostDistribution, value)
        End Set
    End Property
    Dim fSecondaryAutoCostDistribution As Decimal
    Public Property SecondaryAutoCostDistribution() As Decimal
        Get
            Return fSecondaryAutoCostDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SecondaryAutoCostDistribution", fSecondaryAutoCostDistribution, value)
        End Set
    End Property
    Dim fSecondaryManPowerDistributionDirect As Decimal
    Public Property SecondaryManPowerDistributionDirect() As Decimal
        Get
            Return fSecondaryManPowerDistributionDirect
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SecondaryManPowerDistributionDirect", fSecondaryManPowerDistributionDirect, value)
        End Set
    End Property
    Dim fSecondaryManPowerDistributionInDirect As Decimal
    Public Property SecondaryManPowerDistributionInDirect() As Decimal
        Get
            Return fSecondaryManPowerDistributionInDirect
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SecondaryManPowerDistributionInDirect", fSecondaryManPowerDistributionInDirect, value)
        End Set
    End Property
    Dim fSecondaryFixedAssetDistribution As Decimal
    Public Property SecondaryFixedAssetDistribution() As Decimal
        Get
            Return fSecondaryFixedAssetDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SecondaryFixedAssetDistribution", fSecondaryFixedAssetDistribution, value)
        End Set
    End Property
    Dim fSecondaryDispensingDistribution As Decimal
    Public Property SecondaryDispensingDistribution() As Decimal
        Get
            Return fSecondaryDispensingDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SecondaryDispensingDistribution", fSecondaryDispensingDistribution, value)
        End Set
    End Property
    Dim fSecondaryTransferDistribution As Decimal
    Public Property SecondaryTransferDistribution() As Decimal
        Get
            Return fSecondaryTransferDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SecondaryTransferDistribution", fSecondaryTransferDistribution, value)
        End Set
    End Property
    Dim fSecondaryDistribution As Decimal
    Public Property SecondaryDistribution() As Decimal
        Get
            Return fSecondaryDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SecondaryDistribution", fSecondaryDistribution, value)
        End Set
    End Property
    Dim fTotalGeneralExpenses As Decimal
    Public Property TotalGeneralExpenses() As Decimal
        Get
            Return fTotalGeneralExpenses
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalGeneralExpenses", fTotalGeneralExpenses, value)
        End Set
    End Property
    Dim fTotalPrimaryCost As Decimal
    Public Property TotalPrimaryCost() As Decimal
        Get
            Return fTotalPrimaryCost
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPrimaryCost", fTotalPrimaryCost, value)
        End Set
    End Property
    Dim fTotalConvergenceCost As Decimal
    Public Property TotalConvergenceCost() As Decimal
        Get
            Return fTotalConvergenceCost
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalConvergenceCost", fTotalConvergenceCost, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
