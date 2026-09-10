Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.AccountPayableInterestShares")> _
Public Class PaymentsAccountPayableInterestSharesXpoP
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
    Dim fIdAccountPayableShares As Integer
    Public Property IdAccountPayableShares() As Integer
        Get
            Return fIdAccountPayableShares
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdAccountPayableShares", fIdAccountPayableShares, value)
        End Set
    End Property
    Dim fDateInterestShares As DateTime
    Public Property DateInterestShares() As DateTime
        Get
            Return fDateInterestShares
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateInterestShares", fDateInterestShares, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
