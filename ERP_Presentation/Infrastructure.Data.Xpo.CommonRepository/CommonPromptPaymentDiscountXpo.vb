'************************************************************
' Assembly         : Infraestructure.Data.Xpo.CommonRepository
' Author           : Giovanny plazas
' Created          : 29-06-2022
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Clase Rangos Descuentos Pronto pago para servicios Xpo.
''' </summary>
''' <remarks></remarks>
<Persistent("Common.PromptPaymentDiscount")>
Public Class PromptPaymentDiscountXpo
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

    Dim fRangeName As String
    Public Property RangeName() As String
        Get
            Return fRangeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RangeName", fRangeName, value)
        End Set
    End Property

    Dim fInitialRank As Integer
    Public Property InitialRank() As Integer
        Get
            Return fInitialRank
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InitialRank", fInitialRank, value)
        End Set
    End Property

    Dim fEndRank As Integer
    Public Property EndRank() As Integer
        Get
            Return fEndRank
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EndRank", fEndRank, value)
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

    <PersistentAlias("Supplier.Id")>
    Public ReadOnly Property SupplierId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("SupplierId"))
        End Get
    End Property

    Dim fSupplier As Maintenance_Supplier
    <Persistent("SupplierId")>
    <Association("MaintenanceSupplierReferencesPromptPaymentDiscountXpo")>
    Public Property Supplier() As Maintenance_Supplier
        Get
            Return fSupplier
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue("Maintenance_Supplier", fSupplier, value)
        End Set
    End Property

#Region "Constructores"

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

