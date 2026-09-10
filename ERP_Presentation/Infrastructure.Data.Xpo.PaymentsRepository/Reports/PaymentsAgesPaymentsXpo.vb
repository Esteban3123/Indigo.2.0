Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.AgesPayments")> _
Public Class PaymentsAgesPaymentsXpo
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
    Dim fSettingPaymentId As PaymentsSettingPaymentsXpo
    <Association("PaymentsAgesPaymentsXpoReferencesPaymentsSettingPaymentsXpo")> _
    Public Property SettingPaymentId() As PaymentsSettingPaymentsXpo
        Get
            Return fSettingPaymentId
        End Get
        Set(ByVal value As PaymentsSettingPaymentsXpo)
            SetPropertyValue(Of PaymentsSettingPaymentsXpo)("SettingPaymentId", fSettingPaymentId, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fInitialRange As Integer
    Public Property InitialRange() As Integer
        Get
            Return fInitialRange
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InitialRange", fInitialRange, value)
        End Set
    End Property
    Dim fEndRange As Integer
    Public Property EndRange() As Integer
        Get
            Return fEndRange
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EndRange", fEndRange, value)
        End Set
    End Property
    Dim fColor As Integer
    Public Property Color() As Integer
        Get
            Return fColor
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Color", fColor, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
