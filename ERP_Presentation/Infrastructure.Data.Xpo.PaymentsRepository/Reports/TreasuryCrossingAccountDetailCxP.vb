Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.CrossingAccountDetailCxP")> _
Public Class TreasuryCrossingAccountDetailCxP
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
    <Association("TreasuryCrossingAccountDetailCxPReferencesTreasuryCrossingAccount")> _
    Public Property CrossingAccountId() As TreasuryCrossingAccount
        Get
            Return fCrossingAccountId
        End Get
        Set(ByVal value As TreasuryCrossingAccount)
            SetPropertyValue(Of TreasuryCrossingAccount)("CrossingAccountId", fCrossingAccountId, value)
        End Set
    End Property
    Dim fAccountPayableId As PaymentsAccountPayable
    <Association("TreasuryCrossingAccountDetailCxPReferencesPaymentsAccountPayable")> _
    Public Property AccountPayableId() As PaymentsAccountPayable
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("TreasuryCrossingAccountDetailCxPReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCrossingValue As Integer
    Public Property CrossingValue() As Integer
        Get
            Return fCrossingValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CrossingValue", fCrossingValue, value)
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
