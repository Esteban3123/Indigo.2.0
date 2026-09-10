Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetEntryDevolution")> _
Public Class FixedAssetFixedAssetEntryDevolutionReportXpo
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
    Dim fOperatingUnitId As CommonOperartionUnitReportXpo
    <Association("FixedAsset_FixedAssetEntryDevolutionReferencesCommon_OperatingUnit")> _
    Public Property OperatingUnitId() As CommonOperartionUnitReportXpo
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As CommonOperartionUnitReportXpo)
            SetPropertyValue(Of CommonOperartionUnitReportXpo)("OperatingUnitId", fOperatingUnitId, value)
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
    Dim fFixedAssetEntryId As FixedAssetFixedAssetEntryReportXpo
    <Association("FixedAsset_FixedAssetEntryDevolutionReferencesFixedAsset_FixedAssetEntry")> _
    Public Property FixedAssetEntryId() As FixedAssetFixedAssetEntryReportXpo
        Get
            Return fFixedAssetEntryId
        End Get
        Set(ByVal value As FixedAssetFixedAssetEntryReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetEntryReportXpo)("FixedAssetEntryId", fFixedAssetEntryId, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fFreightValue As Decimal
    Public Property FreightValue() As Decimal
        Get
            Return fFreightValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightValue", fFreightValue, value)
        End Set
    End Property
    Dim fFreightIVAPercentage As Decimal
    Public Property FreightIVAPercentage() As Decimal
        Get
            Return fFreightIVAPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightIVAPercentage", fFreightIVAPercentage, value)
        End Set
    End Property
    Dim fFreightIVAValue As Decimal
    Public Property FreightIVAValue() As Decimal
        Get
            Return fFreightIVAValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightIVAValue", fFreightIVAValue, value)
        End Set
    End Property
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fValueDiscount As Decimal
    Public Property ValueDiscount() As Decimal
        Get
            Return fValueDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueDiscount", fValueDiscount, value)
        End Set
    End Property
    Dim fValueTax As Decimal
    Public Property ValueTax() As Decimal
        Get
            Return fValueTax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueTax", fValueTax, value)
        End Set
    End Property
    Dim fWithholdingTax As Decimal
    Public Property WithholdingTax() As Decimal
        Get
            Return fWithholdingTax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WithholdingTax", fWithholdingTax, value)
        End Set
    End Property
    Dim fWithholdingICA As Decimal
    Public Property WithholdingICA() As Decimal
        Get
            Return fWithholdingICA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WithholdingICA", fWithholdingICA, value)
        End Set
    End Property
    Dim fRetentionSource As Decimal
    Public Property RetentionSource() As Decimal
        Get
            Return fRetentionSource
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionSource", fRetentionSource, value)
        End Set
    End Property
    Dim fRetentionOther As Decimal
    Public Property RetentionOther() As Decimal
        Get
            Return fRetentionOther
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionOther", fRetentionOther, value)
        End Set
    End Property
    Dim fDeductionOther As Decimal
    Public Property DeductionOther() As Decimal
        Get
            Return fDeductionOther
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeductionOther", fDeductionOther, value)
        End Set
    End Property
    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
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
    <Association("FixedAsset_FixedAssetEntryDevolutionDetailReferencesFixedAsset_FixedAssetEntryDevolution", GetType(FixedAssetFixedAssetEntryDevolutionDetailReportXpo))> _
    Public ReadOnly Property FixedAsset_FixedAssetEntryDevolutionDetails() As XPCollection(Of FixedAssetFixedAssetEntryDevolutionDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryDevolutionDetailReportXpo)("FixedAsset_FixedAssetEntryDevolutionDetails")
        End Get
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
