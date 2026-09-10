Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetReclassificationDetail")>
Public Class FixedAssetReclassificationDetailReportXpo
    Inherits XPLiteObject

#Region "Properties"
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

    Dim fFixedAssetReclassificationId As FixedAssetReclassificationReportXpo
    <Association("FK_FixedAssetReclassificationDetail_FixedAssetReclassification")>
    Public Property FixedAssetReclassificationId() As FixedAssetReclassificationReportXpo
        Get
            Return fFixedAssetReclassificationId
        End Get
        Set(ByVal value As FixedAssetReclassificationReportXpo)
            SetPropertyValue(Of FixedAssetReclassificationReportXpo)("FixedAssetReclassificationId", fFixedAssetReclassificationId, value)
        End Set
    End Property

    Dim fPhysicalAssetId As FixedAssetPhysicalAssetReportXpo
    <Association("FK_FixedAssetReclassificationDetail_FixedAssetPhysicalAsset")>
    Public Property PhysicalAssetId() As FixedAssetPhysicalAssetReportXpo
        Get
            Return fPhysicalAssetId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetReportXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetReportXpo)("PhysicalAssetId", fPhysicalAssetId, value)
        End Set
    End Property
    Dim fHasOutput As Boolean
    Public Property HasOutput() As Boolean
        Get
            Return fHasOutput
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasOutput", fHasOutput, value)
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
    Dim fOutputRefund As Boolean
    Public Property OutputRefund() As Boolean
        Get
            Return fOutputRefund
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OutputRefund", fOutputRefund, value)
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
#End Region

#Region "Relationships"
    <Association("FK_FixedAssetReclassificationDetailBook_FixedAssetReclassificationDetail", GetType(FixedAssetReclassificationDetailBookReportXpo))>
    Public ReadOnly Property FixedAssetReclassificationDetailBookReportXpo() As XPCollection(Of FixedAssetReclassificationDetailBookReportXpo)
        Get
            Return GetCollection(Of FixedAssetReclassificationDetailBookReportXpo)("FixedAssetReclassificationDetailBookReportXpo")
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
