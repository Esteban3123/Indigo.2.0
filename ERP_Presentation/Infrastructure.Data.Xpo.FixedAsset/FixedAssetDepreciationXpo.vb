Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetDepreciation")> _
Public Class FixedAssetDepreciationXpo
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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    Dim fClosingYear As Integer
    Public Property ClosingYear() As Integer
        Get
            Return fClosingYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ClosingYear", fClosingYear, value)
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

    Dim fObservation As String
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

    <Association("DepreciationDetailReferenceDepreciation", GetType(FixedAssetDepreciationDetailXpo))>
    Public ReadOnly Property FixedAssetDepreciationDetailXpo() As XPCollection(Of FixedAssetDepreciationDetailXpo)
        Get
            Return GetCollection(Of FixedAssetDepreciationDetailXpo)("FixedAssetDepreciationDetailXpo")
        End Get
    End Property

    <Association("FixedAssetAmortizationDetailReferenceDepreciation", GetType(FixedAssetAmortizationDetailXpo))>
    Public ReadOnly Property FixedAssetAmortizationDetailXpo() As XPCollection(Of FixedAssetAmortizationDetailXpo)
        Get
            Return GetCollection(Of FixedAssetAmortizationDetailXpo)("FixedAssetAmortizationDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

