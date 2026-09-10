'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Giovanny plazas
' Created          : 05-07-2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Tarjetas usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.View_SchedulePayment")>
Public Class View_SchedulePaymentXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fSupplierId As Integer
    Public Property SupplierId() As Integer
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Code", fSupplierId, value)
        End Set
    End Property

    Dim fThirdId As Integer
    Public Property ThirdId() As Integer
        Get
            Return fThirdId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdId", fThirdId, value)
        End Set
    End Property

    Dim fInvoice As String
    Public Property Invoice() As String
        Get
            Return fInvoice
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Invoice", fInvoice, value)
        End Set
    End Property

    Dim fBalanceShare As Decimal
    Public Property BalanceShare() As Decimal
        Get
            Return fBalanceShare
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceShare", fBalanceShare, value)
        End Set
    End Property

    <PersistentAlias("AccountPayable.Id")>
    Public ReadOnly Property AccountPayableId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("AccountPayableId"))
        End Get
    End Property

    Dim fAccountPayable As AccountPayableXpo
    <Persistent("AccountPayableId")>
    <Association("View_SchedulePaymentXpoReferencesAccountPayable")>
    Public Property AccountPayable() As AccountPayableXpo
        Get
            Return fAccountPayable
        End Get
        Set(ByVal value As AccountPayableXpo)
            SetPropertyValue("AccountPayable", fAccountPayable, value)
        End Set
    End Property


    Dim fCXPValue As Decimal
    Public Property CXPValue() As Decimal
        Get
            Return fCXPValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CXPValue", fCXPValue, value)
        End Set
    End Property

    Dim fRangeNameDiscount As String
    Public Property RangeNameDiscount() As String
        Get
            Return fRangeNameDiscount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RangeNameDiscount", fRangeNameDiscount, value)
        End Set
    End Property

    Dim fDiscountRate As Decimal
    Public Property DiscountRate() As Decimal
        Get
            Return fDiscountRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountRate", fDiscountRate, value)
        End Set
    End Property

    Dim fDiscountValue As Decimal
    Public Property DiscountValue() As Decimal
        Get
            Return fDiscountValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountValue", fDiscountValue, value)
        End Set
    End Property

    Dim fCxPRadicateDate As DateTime
    Public Property CxPRadicateDate() As DateTime
        Get
            Return fCxPRadicateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CxPRadicateDate", fCxPRadicateDate, value)
        End Set
    End Property

    Dim fBaseValue As Decimal
    Public Property BaseValue() As Decimal
        Get
            Return fBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValue", fBaseValue, value)
        End Set
    End Property

    Dim fValueTax As Decimal?
    Public Property ValueTax() As Decimal?
        Get
            Return fValueTax
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("ValueTax", fValueTax, value)
        End Set
    End Property

    Dim fRateValue As Decimal?
    Public Property RateValue() As Decimal?
        Get
            Return fRateValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("RateValue", fRateValue, value)
        End Set
    End Property

    Dim fValueNote As Decimal
    Public Property ValueNote() As Decimal
        Get
            Return fValueNote
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueNote", fValueNote, value)
        End Set
    End Property

    Dim fAdjusmentValueToAffectBase As Decimal
    Public Property AdjusmentValueToAffectBase() As Decimal
        Get
            Return fAdjusmentValueToAffectBase
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdjusmentValueToAffectBase", fAdjusmentValueToAffectBase, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class