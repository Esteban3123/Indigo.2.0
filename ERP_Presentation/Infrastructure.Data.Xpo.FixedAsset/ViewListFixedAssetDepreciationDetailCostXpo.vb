Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.ViewListFixedAssetDepreciationDetailCost")> _
Public Class ViewListFixedAssetDepreciationDetailCostXpo
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

    Dim fCodeDescriptionItem As String
    Public Property CodeDescriptionItem() As String
        Get
            Return fCodeDescriptionItem
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeDescriptionItem", fCodeDescriptionItem, value)
        End Set
    End Property

    Dim fCodeItem As String
    Public Property CodeItem() As String
        Get
            Return fCodeItem
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeItem", fCodeItem, value)
        End Set
    End Property

    Dim fDescriptionItem As String
    Public Property DescriptionItem() As String
        Get
            Return fDescriptionItem
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DescriptionItem", fDescriptionItem, value)
        End Set
    End Property

    Dim fClosingDate As Date
    Public Property ClosingDate() As Date
        Get
            Return fClosingDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ClosingDate", fClosingDate, value)
        End Set
    End Property

    Dim fFixedAssetPhysicalAssetId As Integer
    Public Property FixedAssetPhysicalAssetId() As Integer
        Get
            Return fFixedAssetPhysicalAssetId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FixedAssetPhysicalAssetId", fFixedAssetPhysicalAssetId, value)
        End Set
    End Property

    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fDepreciationValue As Decimal
    Public Property DepreciationValue() As Decimal
        Get
            Return fDepreciationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DepreciationValue", fDepreciationValue, value)
        End Set
    End Property

    Dim fClosingYear As Integer
    Public Property ClosingYear() As Integer
        Get
            Return fClosingYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ClosingYear", fClosingYear, value)
        End Set
    End Property

    Dim fClosingMonth As Integer
    Public Property ClosingMonth() As Integer
        Get
            Return fClosingMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ClosingMonth", fClosingMonth, value)
        End Set
    End Property

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    Dim fLegalBookId As Integer
    Public Property LegalBookId() As Integer
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class


