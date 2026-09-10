Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Inventory.InventoryMeasurementUnit")> _
Public Class InventoryInventoryMeasurementUnitReportXpo
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
    Dim fCode As String
    <Indexed(Name:="IX_InventoryMeasurementUnit", Unique:=True)> _
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
    Dim fAbbreviation As String
    <Size(10)> _
    Public Property Abbreviation() As String
        Get
            Return fAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Abbreviation", fAbbreviation, value)
        End Set
    End Property
    Dim fUnitType As Byte
    Public Property UnitType() As Byte
        Get
            Return fUnitType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UnitType", fUnitType, value)
        End Set
    End Property
    Dim fAllowEditCostValue As Boolean
    Public Property AllowEditCostValue() As Boolean
        Get
            Return fAllowEditCostValue
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowEditCostValue", fAllowEditCostValue, value)
        End Set
    End Property
    Dim fCostValue As Decimal
    Public Property CostValue() As Decimal
        Get
            Return fCostValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CostValue", fCostValue, value)
        End Set
    End Property
    Dim fCrystalMeasurementUnit As String
    <Size(3)> _
    Public Property CrystalMeasurementUnit() As String
        Get
            Return fCrystalMeasurementUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CrystalMeasurementUnit", fCrystalMeasurementUnit, value)
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
    <Association("InteropCost_DistributionDirectCostDetailReferencesInventory_InventoryMeasurementUnit", GetType(InteropCostDistributionDirectCostDetailReportXpo))> _
    Public ReadOnly Property InteropCost_DistributionDirectCostDetails() As XPCollection(Of InteropCostDistributionDirectCostDetailReportXpo)
        Get
            Return GetCollection(Of InteropCostDistributionDirectCostDetailReportXpo)("InteropCost_DistributionDirectCostDetails")
        End Get
    End Property

    <Association("DistributionBaseMeasurementReferencesInventoryMeasurement", GetType(DistributionBaseMeasurementUnitXpo))> _
    Public ReadOnly Property DistributionBaseMeasurementUnitXpo() As XPCollection(Of DistributionBaseMeasurementUnitXpo)
        Get
            Return GetCollection(Of DistributionBaseMeasurementUnitXpo)("DistributionBaseMeasurementUnitXpo")
        End Get
    End Property

    <Association("InteropCost_LogisticsProductionCenterRecordDetailReferencesInventoryMeasurement", GetType(LogisticsProductionCenterRecordDetailXpo))> _
    Public ReadOnly Property InteropCost_LogisticsProductionCenterRecordDetails() As XPCollection(Of LogisticsProductionCenterRecordDetailXpo)
        Get
            Return GetCollection(Of LogisticsProductionCenterRecordDetailXpo)("InteropCost_LogisticsProductionCenterRecordDetails")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
