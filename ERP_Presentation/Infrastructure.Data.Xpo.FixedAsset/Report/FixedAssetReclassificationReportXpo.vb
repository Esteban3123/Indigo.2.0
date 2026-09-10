Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetReclassification")>
Public Class FixedAssetReclassificationReportXpo
    Inherits XPLiteObject

#Region "Propierties"
    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fReclassificationType As Byte
    Public Property ReclassificationType() As Byte
        Get
            Return fReclassificationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ReclassificationType", fReclassificationType, value)
        End Set
    End Property
    Dim fDetail As String
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property
    Dim fItemIdPrevious As FixedAssetItemReportXpo
    <Association("FK_FixedAssetReclassification_FixedAssetItemPrevious")>
    Public Property ItemIdPrevious() As FixedAssetItemReportXpo
        Get
            Return fItemIdPrevious
        End Get
        Set(ByVal value As FixedAssetItemReportXpo)
            SetPropertyValue(Of FixedAssetItemReportXpo)("ItemIdPrevious", fItemIdPrevious, value)
        End Set
    End Property
    Dim fItemCatalogIdPrevious As FixedAssetItemCatalogReportXpo
    <Association("FK_FixedAssetReclassification_FixedAssetItemCatalogPrevious")>
    Public Property ItemCatalogIdPrevious() As FixedAssetItemCatalogReportXpo
        Get
            Return fItemCatalogIdPrevious
        End Get
        Set(ByVal value As FixedAssetItemCatalogReportXpo)
            SetPropertyValue(Of FixedAssetItemCatalogReportXpo)("ItemCatalogIdPrevious", fItemCatalogIdPrevious, value)
        End Set
    End Property
    Dim fItemId As FixedAssetItemReportXpo
    <Association("FK_FixedAssetReclassification_FixedAssetItem")>
    Public Property ItemId() As FixedAssetItemReportXpo
        Get
            Return fItemId
        End Get
        Set(ByVal value As FixedAssetItemReportXpo)
            SetPropertyValue(Of FixedAssetItemReportXpo)("ItemId", fItemId, value)
        End Set
    End Property
    Dim fItemCatalogId As FixedAssetItemCatalogReportXpo
    <Association("FK_FixedAssetReclassification_FixedAssetItemCatalog")>
    Public Property ItemCatalogId() As FixedAssetItemCatalogReportXpo
        Get
            Return fItemCatalogId
        End Get
        Set(ByVal value As FixedAssetItemCatalogReportXpo)
            SetPropertyValue(Of FixedAssetItemCatalogReportXpo)("ItemCatalogId", fItemCatalogId, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)>
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
    <Size(20)>
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
    Dim fConfirmationUser As String
    <Size(20)>
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)>
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property
#End Region

#Region "Relationships"
    <Association("FK_FixedAssetReclassificationDetail_FixedAssetReclassification", GetType(FixedAssetReclassificationDetailReportXpo))>
    Public ReadOnly Property FixedAssetReclassificationDetailReportXpo() As XPCollection(Of FixedAssetReclassificationDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetReclassificationDetailReportXpo)("FixedAssetReclassificationDetailReportXpo")
        End Get
    End Property
#End Region

#Region "Buldier"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

End Class
