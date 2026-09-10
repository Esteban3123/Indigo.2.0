Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.ViewDiscountReteIcaAndPrivateDeclaration")> _
Public Class ViewDiscountReteIcaAndPrivateDeclarationXpo
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

    Dim fNit As String
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property

    Dim fReasonSocial As String
    Public Property ReasonSocial() As String
        Get
            Return fReasonSocial
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReasonSocial", fReasonSocial, value)
        End Set
    End Property

    Dim fAddresss As String
    Public Property Addresss() As String
        Get
            Return fAddresss
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Addresss", fAddresss, value)
        End Set
    End Property

    Dim fPhone As String
    Public Property Phone() As String
        Get
            Return fPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Phone", fPhone, value)
        End Set
    End Property

    Dim fValueOne As Decimal
    Public Property ValueOne() As Decimal
        Get
            Return fValueOne
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueOne", fValueOne, value)
        End Set
    End Property

    Dim fValueTwo As Decimal
    Public Property ValueTwo() As Decimal
        Get
            Return fValueTwo
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueTwo", fValueTwo, value)
        End Set
    End Property

    Dim fDifferenceValue As Decimal
    Public Property DifferenceValue() As Decimal
        Get
            Return fDifferenceValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DifferenceValue", fDifferenceValue, value)
        End Set
    End Property

    Dim fYear As Integer
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
