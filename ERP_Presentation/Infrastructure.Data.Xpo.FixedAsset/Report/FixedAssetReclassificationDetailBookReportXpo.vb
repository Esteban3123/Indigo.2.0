Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetReclassificationDetailBook")>
Public Class FixedAssetReclassificationDetailBookReportXpo
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

    Dim fFixedAssetReclassificationDetailId As FixedAssetReclassificationDetailReportXpo
    <Association("FK_FixedAssetReclassificationDetailBook_FixedAssetReclassificationDetail")>
    Public Property FixedAssetReclassificationDetailId() As FixedAssetReclassificationDetailReportXpo
        Get
            Return fFixedAssetReclassificationDetailId
        End Get
        Set(ByVal value As FixedAssetReclassificationDetailReportXpo)
            SetPropertyValue(Of FixedAssetReclassificationDetailReportXpo)("FixedAssetReclassificationDetailId", fFixedAssetReclassificationDetailId, value)
        End Set
    End Property

    Dim fLegalBookId As GeneralLedgerLegalBookReportXpo
    <Association("FK_FixedAssetReclassificationDetailBook_LegalBook")>
    Public Property LegalBookId() As GeneralLedgerLegalBookReportXpo
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As GeneralLedgerLegalBookReportXpo)
            SetPropertyValue(Of GeneralLedgerLegalBookReportXpo)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Dim fDaysPendingDepreciate As Integer
    Public Property DaysPendingDepreciate() As Integer
        Get
            Return fDaysPendingDepreciate
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DaysPendingDepreciate", fDaysPendingDepreciate, value)
        End Set
    End Property

    Dim fDepreciatedDays As Integer
    Public Property DepreciatedDays() As Integer
        Get
            Return fDepreciatedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DepreciatedDays", fDepreciatedDays, value)
        End Set
    End Property

    Dim fValorization As Decimal
    Public Property Valorization() As Decimal
        Get
            Return fValorization
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Valorization", fValorization, value)
        End Set
    End Property

    Dim fDevaluation As Decimal
    Public Property Devaluation() As Decimal
        Get
            Return fDevaluation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Devaluation", fDevaluation, value)
        End Set
    End Property

    Dim fAdjustedValue As Decimal
    Public Property AdjustedValue() As Decimal
        Get
            Return fAdjustedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdjustedValue", fAdjustedValue, value)
        End Set
    End Property

    Dim fTransactionValue As Decimal
    Public Property TransactionValue() As Decimal
        Get
            Return fTransactionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TransactionValue", fTransactionValue, value)
        End Set
    End Property

    Dim fDepreciatedValue As Decimal
    Public Property DepreciatedValue() As Decimal
        Get
            Return fDepreciatedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DepreciatedValue", fDepreciatedValue, value)
        End Set
    End Property

    Dim fInflationAdjustmentValue As Decimal
    Public Property InflationAdjustmentValue() As Decimal
        Get
            Return fInflationAdjustmentValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InflationAdjustmentValue", fInflationAdjustmentValue, value)
        End Set
    End Property

    Dim fResidualValue As Decimal
    Public Property ResidualValue() As Decimal
        Get
            Return fResidualValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ResidualValue", fResidualValue, value)
        End Set
    End Property

    Dim fHistoricalValue As Decimal
    Public Property HistoricalValue() As Decimal
        Get
            Return fHistoricalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HistoricalValue", fHistoricalValue, value)
        End Set
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
