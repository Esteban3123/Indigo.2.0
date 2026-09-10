Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Cost.CostEstimationNative")> _
Public Class CostCostEstimationNativeReportXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
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
    Dim fProductionCenterId As Integer
    Public Property ProductionCenterId() As Integer
        Get
            Return fProductionCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductionCenterId", fProductionCenterId, value)
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
    Dim fAutoCostDistribution As Decimal
    Public Property AutoCostDistribution() As Decimal
        Get
            Return fAutoCostDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AutoCostDistribution", fAutoCostDistribution, value)
        End Set
    End Property
    Dim fManPowerDistribution As Decimal
    Public Property ManPowerDistribution() As Decimal
        Get
            Return fManPowerDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ManPowerDistribution", fManPowerDistribution, value)
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
    Dim fDispensingDistribution As Decimal
    Public Property DispensingDistribution() As Decimal
        Get
            Return fDispensingDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DispensingDistribution", fDispensingDistribution, value)
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
    Dim fSecondaryDistribution As Decimal
    Public Property SecondaryDistribution() As Decimal
        Get
            Return fSecondaryDistribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SecondaryDistribution", fSecondaryDistribution, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property



    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
