Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetTransfer")> _
Public Class FixedAssetTransferReportXpo
    Inherits XPLiteObject
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

    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
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
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fTransferType As Byte
    Public Property TransferType() As Byte
        Get
            Return fTransferType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TransferType", fTransferType, value)
        End Set
    End Property
    Dim fSourceLocationId As FixedAssetLocationReportXpo
    <Association("FixedAsset_FixedAssetTransferReferencesFixedAsset_FixedAssetLocation")> _
    Public Property SourceLocationId() As FixedAssetLocationReportXpo
        Get
            Return fSourceLocationId
        End Get
        Set(ByVal value As FixedAssetLocationReportXpo)
            SetPropertyValue(Of FixedAssetLocationReportXpo)("SourceLocationId", fSourceLocationId, value)
        End Set
    End Property
    Dim fSourceResponsibleId As FixedAssetResponsibleReportXpo
    <Association("FixedAsset_FixedAssetTransferReferencesFixedAsset_FixedAssetResponsible")> _
    Public Property SourceResponsibleId() As FixedAssetResponsibleReportXpo
        Get
            Return fSourceResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleReportXpo)
            SetPropertyValue(Of FixedAssetResponsibleReportXpo)("SourceResponsibleId", fSourceResponsibleId, value)
        End Set
    End Property
    Dim fTargetLocationId As FixedAssetLocationReportXpo
    <Association("FixedAsset_FixedAssetTransferReferencesFixedAsset_FixedAssetLocation1")> _
    Public Property TargetLocationId() As FixedAssetLocationReportXpo
        Get
            Return fTargetLocationId
        End Get
        Set(ByVal value As FixedAssetLocationReportXpo)
            SetPropertyValue(Of FixedAssetLocationReportXpo)("TargetLocationId", fTargetLocationId, value)
        End Set
    End Property
    Dim fTargetResponsibleId As FixedAssetResponsibleReportXpo
    <Association("FixedAsset_FixedAssetTransferReferencesFixedAsset_FixedAssetResponsible1")> _
    Public Property TargetResponsibleId() As FixedAssetResponsibleReportXpo
        Get
            Return fTargetResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleReportXpo)
            SetPropertyValue(Of FixedAssetResponsibleReportXpo)("TargetResponsibleId", fTargetResponsibleId, value)
        End Set
    End Property
    Dim fObservation As String
    <Size(500)> _
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
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
    Dim fConfirmationUser As String
    <Size(20)> _
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
    <Size(20)> _
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
    <Association("FixedAsset_FixedAssetTransferDetailReferencesFixedAsset_FixedAssetTransfer", GetType(FixedAssetTransferDetailReportXpo))> _
    Public ReadOnly Property FixedAssetTransferDetailReportXpo() As XPCollection(Of FixedAssetTransferDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetTransferDetailReportXpo)("FixedAssetTransferDetailReportXpo")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
