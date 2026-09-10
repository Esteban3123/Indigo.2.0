'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AccountingRepository
' Author           : 
' Created          : 2022-04-22
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Tipo de documento usado en los servicios Xpo
''' </summary>
<Persistent("Common.PromptPaymentDiscount")>
Public Class PromptPaymentDiscountXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fRangeName As String
    <Persistent("RangeName")>
    Public Property RangeName() As String
        Get
            Return fRangeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RangeName", fRangeName, value)
        End Set
    End Property

    Dim fInitialRank As Integer
    <Persistent("InitialRank")>
    Public Property InitialRank() As Integer
        Get
            Return fInitialRank
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InitialRank", fInitialRank, value)
        End Set
    End Property


    Dim fEndRank As Integer
    <Persistent("EndRank")>
    Public Property EndRank() As Integer
        Get
            Return fEndRank
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EndRank", fEndRank, value)
        End Set
    End Property

    Dim fDiscountRate As Decimal
    <Persistent("DiscountRate")>
    Public Property DiscountRate() As Decimal
        Get
            Return fDiscountRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountRate", fDiscountRate, value)
        End Set
    End Property

    Dim fSupplier As Maintenance_Supplier
    <Persistent("SupplierId")>
    <Association("PromptPaymentDiscountXpoReferencesMaintenance_Supplier")>
    Public Property Supplier() As Maintenance_Supplier
        Get
            Return fSupplier
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("Supplier", fSupplier, value)
        End Set
    End Property

    <PersistentAlias("Supplier.Id")>
    Public ReadOnly Property SupplierId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("SupplierId"))
        End Get
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
