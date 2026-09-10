Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.CrossingAccountDetailCxC")> _
Public Class TreasuryCrossingAccountDetailCxC
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
    Dim fCrossingAccountId As TreasuryCrossingAccount
    <Association("TreasuryCrossingAccountDetailCxCReferencesTreasuryCrossingAccount")> _
    Public Property CrossingAccountId() As TreasuryCrossingAccount
        Get
            Return fCrossingAccountId
        End Get
        Set(ByVal value As TreasuryCrossingAccount)
            SetPropertyValue(Of TreasuryCrossingAccount)("CrossingAccountId", fCrossingAccountId, value)
        End Set
    End Property
    Dim fAccountReceivableId As Integer
    Public Property AccountReceivableId() As Integer
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Dim fAccountReceivableAccountingId As Integer
    Public Property AccountReceivableAccountingId() As Integer
        Get
            Return fAccountReceivableAccountingId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableAccountingId", fAccountReceivableAccountingId, value)
        End Set
    End Property
    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCrossingValue As Decimal
    Public Property CrossingValue() As Decimal
        Get
            Return fCrossingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CrossingValue", fCrossingValue, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
