Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetLocation")> _
Public Class FixedAssetLocationReportXpo
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
    Dim fParentId As FixedAssetLocationReportXpo
    <Association("FixedAsset_FixedAssetLocationReferencesFixedAsset_FixedAssetLocation")> _
    Public Property ParentId() As FixedAssetLocationReportXpo
        Get
            Return fParentId
        End Get
        Set(ByVal value As FixedAssetLocationReportXpo)
            SetPropertyValue(Of FixedAssetLocationReportXpo)("ParentId", fParentId, value)
        End Set
    End Property
    Dim fCityId As Integer
    Public Property CityId() As Integer
        Get
            Return fCityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CityId", fCityId, value)
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
    <Size(50)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fFunctionalUnitId As PayrollFunctionalUnitReportXpo
    <Association("FixedAsset_FixedAssetLocationReferencesPayroll_FunctionalUnit")> _
    Public Property FunctionalUnitId() As PayrollFunctionalUnitReportXpo
        Get
            Return fFunctionalUnitId
        End Get
        Set(ByVal value As PayrollFunctionalUnitReportXpo)
            SetPropertyValue(Of PayrollFunctionalUnitReportXpo)("FunctionalUnitId", fFunctionalUnitId, value)
        End Set
    End Property
    Dim fClass1 As Byte
    <Persistent("Class")> _
    Public Property Class1() As Byte
        Get
            Return fClass1
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Class1", fClass1, value)
        End Set
    End Property
    Dim fLocationTypeId As FixedAssetLocationTypeReportXpo
    <Association("FixedAsset_FixedAssetLocationReferencesFixedAsset_FixedAssetLocationType")> _
    Public Property LocationTypeId() As FixedAssetLocationTypeReportXpo
        Get
            Return fLocationTypeId
        End Get
        Set(ByVal value As FixedAssetLocationTypeReportXpo)
            SetPropertyValue(Of FixedAssetLocationTypeReportXpo)("LocationTypeId", fLocationTypeId, value)
        End Set
    End Property
    Dim fRiskLevel As Char
    Public Property RiskLevel() As Char
        Get
            Return fRiskLevel
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("RiskLevel", fRiskLevel, value)
        End Set
    End Property
    <Association("FixedAsset_FixedAssetLocationReferencesFixedAsset_FixedAssetLocation", GetType(FixedAssetLocationReportXpo))> _
    Public ReadOnly Property FixedAssetLocationReportXpo() As XPCollection(Of FixedAssetLocationReportXpo)
        Get
            Return GetCollection(Of FixedAssetLocationReportXpo)("FixedAssetLocationReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetLocation", GetType(FixedAssetPhysicalAssetReportXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetReportXpo() As XPCollection(Of FixedAssetPhysicalAssetReportXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetReportXpo)("FixedAssetPhysicalAssetReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetEntryReferencesFixedAsset_FixedAssetLocation", GetType(FixedAssetFixedAssetEntryReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetEntryReportXpo() As XPCollection(Of FixedAssetFixedAssetEntryReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryReportXpo)("FixedAssetFixedAssetEntryReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetEntryItemDetailReferencesFixedAsset_FixedAssetLocation", GetType(FixedAssetFixedAssetEntryItemDetailReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetEntryItemDetailReportXpo() As XPCollection(Of FixedAssetFixedAssetEntryItemDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryItemDetailReportXpo)("FixedAssetFixedAssetEntryItemDetailReportXpo")
        End Get
    End Property
    <Association("FixedAssetFixedAssetRemissionEntranceReportXpoReferencesFixedAssetLocationReportXpo", GetType(FixedAssetFixedAssetRemissionEntranceReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetRemissionEntranceReportXpo() As XPCollection(Of FixedAssetFixedAssetRemissionEntranceReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetRemissionEntranceReportXpo)("FixedAssetFixedAssetRemissionEntranceReportXpo")
        End Get
    End Property
    <Association("FixedAssetFixedAssetRemissionEntranceItemDetailReportXpoReferencesFixedAssetLocationReportXpo", GetType(FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo() As XPCollection(Of FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo)("FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetTransferReferencesFixedAsset_FixedAssetLocation", GetType(FixedAssetTransferReportXpo))> _
    Public ReadOnly Property FixedAsset_FixedAssetTransfers() As XPCollection(Of FixedAssetTransferReportXpo)
        Get
            Return GetCollection(Of FixedAssetTransferReportXpo)("FixedAsset_FixedAssetTransfers")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetTransferReferencesFixedAsset_FixedAssetLocation1", GetType(FixedAssetTransferReportXpo))> _
    Public ReadOnly Property FixedAsset_FixedAssetTransfers1() As XPCollection(Of FixedAssetTransferReportXpo)
        Get
            Return GetCollection(Of FixedAssetTransferReportXpo)("FixedAsset_FixedAssetTransfers1")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
