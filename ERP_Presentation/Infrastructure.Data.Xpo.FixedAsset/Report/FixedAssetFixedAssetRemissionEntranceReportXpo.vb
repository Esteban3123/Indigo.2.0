Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetRemissionEntrance")> _
Public Class FixedAssetFixedAssetRemissionEntranceReportXpo
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
    Dim fRemisionDate As DateTime
    Public Property RemisionDate() As DateTime
        Get
            Return fRemisionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RemisionDate", fRemisionDate, value)
        End Set
    End Property
    Dim fAdquisitionType As Byte
    Public Property AdquisitionType() As Byte
        Get
            Return fAdquisitionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AdquisitionType", fAdquisitionType, value)
        End Set
    End Property
    Dim fSupplierDistributionLineId As CommonSupplierDistributionLinesReporXpo
    <Association("FixedAssetFixedAssetRemissionEntranceReportXpoReferencesCommonSupplierDistributionLinesReporXpo")> _
    Public Property SupplierDistributionLineId() As CommonSupplierDistributionLinesReporXpo
        Get
            Return fSupplierDistributionLineId
        End Get
        Set(ByVal value As CommonSupplierDistributionLinesReporXpo)
            SetPropertyValue(Of CommonSupplierDistributionLinesReporXpo)("SupplierDistributionLineId", fSupplierDistributionLineId, value)
        End Set
    End Property
    Dim fRemisionNumber As String
    <Size(20)> _
    Public Property RemisionNumber() As String
        Get
            Return fRemisionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RemisionNumber", fRemisionNumber, value)
        End Set
    End Property
    Dim fGetLocationResponsible As Byte
    Public Property GetLocationResponsible() As Byte
        Get
            Return fGetLocationResponsible
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("GetLocationResponsible", fGetLocationResponsible, value)
        End Set
    End Property
    Dim fLocationId As FixedAssetLocationReportXpo
    <Association("FixedAssetFixedAssetRemissionEntranceReportXpoReferencesFixedAssetLocationReportXpo")> _
    Public Property LocationId() As FixedAssetLocationReportXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetLocationReportXpo)
            SetPropertyValue(Of FixedAssetLocationReportXpo)("LocationId", fLocationId, value)
        End Set
    End Property
    Dim fResponsibleId As FixedAssetResponsibleReportXpo
    <Association("FixedAssetFixedAssetRemissionEntranceReportXpoReferencesFixedAssetResponsibleReportXpo")> _
    Public Property ResponsibleId() As FixedAssetResponsibleReportXpo
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleReportXpo)
            SetPropertyValue(Of FixedAssetResponsibleReportXpo)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property
    Dim fDetail As String
    <Size(300)> _
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
    <Association("FixedAssetFixedAssetRemissionEntranceItemReportXpoReferencesFixedAssetFixedAssetRemissionEntranceReportXpo", GetType(FixedAssetFixedAssetRemissionEntranceItemReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetRemissionEntranceItemReportXpo() As XPCollection(Of FixedAssetFixedAssetRemissionEntranceItemReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetRemissionEntranceItemReportXpo)("FixedAssetFixedAssetRemissionEntranceItemReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
