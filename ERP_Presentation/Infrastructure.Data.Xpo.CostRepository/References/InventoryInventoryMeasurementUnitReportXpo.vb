Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Inventory.InventoryMeasurementUnit")> _
Public Class InventoryInventoryMeasurementUnitReportXpo
    Inherits XPLiteObject

#Region "Members"

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

#End Region

#Region "Custom Members"

    <PersistentAlias("concat(Code, ' - ', Name)")> _
    Public ReadOnly Property CodeName() As String
        Get
            Return Me.EvaluateAlias("CodeName")
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("DistributionBaseMeasurementReferencesInventoryMeasurement", GetType(CostDistributionBaseMeasurementUnitXpo))> _
    Public ReadOnly Property CostDistributionBaseMeasurementUnitXpo() As XPCollection(Of CostDistributionBaseMeasurementUnitXpo)
        Get
            Return GetCollection(Of CostDistributionBaseMeasurementUnitXpo)("CostDistributionBaseMeasurementUnitXpo")
        End Get
    End Property

    <Association("CostLogisticsProductionCenterRecordDetailReferencesInventoryMeasurement", GetType(CostLogisticsProductionCenterRecordDetailXpo))> _
    Public ReadOnly Property CostLogisticsProductionCenterRecordDetailXpo() As XPCollection(Of CostLogisticsProductionCenterRecordDetailXpo)
        Get
            Return GetCollection(Of CostLogisticsProductionCenterRecordDetailXpo)("CostLogisticsProductionCenterRecordDetailXpo")
        End Get
    End Property

    <Association("Cost_CostInventoryGroup_References_Inventory_InventoryMeasurementUnit", GetType(CostInventoryGroupXpo))> _
    Public ReadOnly Property CostInventoryGroups() As XPCollection(Of CostInventoryGroupXpo)
        Get
            Return GetCollection(Of CostInventoryGroupXpo)("CostInventoryGroups")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

#End Region
    
End Class
