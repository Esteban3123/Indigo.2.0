Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("GeneralLedger.CompanySettings")> _
 Public Class GeneralLedgerCompanySettingsXpo
    Inherits XPLiteObject
#Region "Members"
    Dim fId As Byte
    <Key()>
    Public Property Id() As Byte
        Get
            Return fId
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Id", fId, value)
        End Set
    End Property

    Dim fLastClosingDate As DateTime
    Public Property LastClosingDate() As DateTime
        Get
            Return fLastClosingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastClosingDate", fLastClosingDate, value)
        End Set
    End Property

    Dim fConsolidate As Boolean
    Public Property Consolidate() As Boolean
        Get
            Return fConsolidate
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Consolidate", fConsolidate, value)
        End Set
    End Property

    Dim fConsolidateDate As DateTime
    Public Property ConsolidateDate() As DateTime
        Get
            Return fConsolidateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConsolidateDate", fConsolidateDate, value)
        End Set
    End Property

    Dim fSMLV As Decimal
    Public Property SMLV() As Decimal
        Get
            Return fSMLV
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SMLV", fSMLV, value)
        End Set
    End Property

    Dim fUVT As Decimal
    Public Property UVT() As Decimal
        Get
            Return fUVT
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UVT", fUVT, value)
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

    <PersistentAlias("OfficialCurrency.Id")>
    Public ReadOnly Property OfficialCurrencyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("OfficialCurrencyId"))
        End Get
    End Property

    Dim fSalePriceIncludeTax As Boolean
    Public Property SalePriceIncludeTax() As Boolean
        Get
            Return fSalePriceIncludeTax
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SalePriceIncludeTax", fSalePriceIncludeTax, value)
        End Set
    End Property
#End Region
#Region "Association"
    Dim fOfficialCurrency As CommonCurrencyXpo
    <Persistent("OfficialCurrencyId")>
    <Association("Currency_GeneralLedgerCompanySettingsXpo")>
    Public Property OfficialCurrency() As CommonCurrencyXpo
        Get
            Return fOfficialCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("OfficialCurrency", fOfficialCurrency, value)
        End Set
    End Property
#End Region
#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region
End Class
