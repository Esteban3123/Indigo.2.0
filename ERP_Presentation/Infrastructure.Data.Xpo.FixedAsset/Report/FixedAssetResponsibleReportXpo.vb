Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetResponsible")> _
Public Class FixedAssetResponsibleReportXpo
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
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("FixedAsset_FixedAssetResponsibleReferencesFixedAsset_ThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fVinculationTypeId As Integer
    Public Property VinculationTypeId() As Integer
        Get
            Return fVinculationTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VinculationTypeId", fVinculationTypeId, value)
        End Set
    End Property
    Dim fReponsibleTypeId As FixedAssetResponsibleTypeReportXpo
    <Association("FixedAsset_FixedAssetResponsibleReferencesFixedAsset_ResponsibleType")> _
    Public Property ReponsibleTypeId() As FixedAssetResponsibleTypeReportXpo
        Get
            Return fReponsibleTypeId
        End Get
        Set(ByVal value As FixedAssetResponsibleTypeReportXpo)
            SetPropertyValue(Of FixedAssetResponsibleTypeReportXpo)("ReponsibleTypeId", fReponsibleTypeId, value)
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
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetResponsible", GetType(FixedAssetPhysicalAssetReportXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetReportXpo() As XPCollection(Of FixedAssetPhysicalAssetReportXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetReportXpo)("FixedAssetPhysicalAssetReportXpo")
        End Get
    End Property
    <Association("FixedAssetFixedAssetRemissionEntranceItemDetailReportXpoReferencesFixedAssetResponsibleReportXpo", GetType(FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo() As XPCollection(Of FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo)("FixedAssetFixedAssetRemissionEntranceItemDetailReportXpo")
        End Get
    End Property
    <Association("FixedAssetFixedAssetRemissionEntranceReportXpoReferencesFixedAssetResponsibleReportXpo", GetType(FixedAssetFixedAssetRemissionEntranceReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetRemissionEntranceReportXpo() As XPCollection(Of FixedAssetFixedAssetRemissionEntranceReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetRemissionEntranceReportXpo)("FixedAssetFixedAssetRemissionEntranceReportXpo")
        End Get
    End Property
    <Association("FixedAssetFixedAssetEntryItemDetailReportXpoReferencesFixedAssetResponsibleReportXpo", GetType(FixedAssetFixedAssetEntryItemDetailReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetEntryItemDetailReportXpo() As XPCollection(Of FixedAssetFixedAssetEntryItemDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryItemDetailReportXpo)("FixedAssetFixedAssetEntryItemDetailReportXpo")
        End Get
    End Property
    <Association("FixedAssetFixedAssetEntryReportXpoReferencesFixedAssetResponsibleReportXpo", GetType(FixedAssetFixedAssetEntryReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetEntryReportXpo() As XPCollection(Of FixedAssetFixedAssetEntryReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryReportXpo)("FixedAssetFixedAssetEntryReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetTransferReferencesFixedAsset_FixedAssetResponsible", GetType(FixedAssetTransferReportXpo))> _
    Public ReadOnly Property FixedAsset_FixedAssetTransfers() As XPCollection(Of FixedAssetTransferReportXpo)
        Get
            Return GetCollection(Of FixedAssetTransferReportXpo)("FixedAsset_FixedAssetTransfers")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetTransferReferencesFixedAsset_FixedAssetResponsible1", GetType(FixedAssetTransferReportXpo))> _
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
