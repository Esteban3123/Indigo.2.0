Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.CrossingAccountDetailCxP")> _
Public Class TreasuryCrossingAccountDetailCxPXpo
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
    Dim fCrossingAccountId As TreasuryCrossingAccountXpo
    <Association("TreasuryCrossingAccountDetailCxPXpoReferencesTreasuryCrossingAccountXpo")> _
    Public Property CrossingAccountId() As TreasuryCrossingAccountXpo
        Get
            Return fCrossingAccountId
        End Get
        Set(ByVal value As TreasuryCrossingAccountXpo)
            SetPropertyValue(Of TreasuryCrossingAccountXpo)("CrossingAccountId", fCrossingAccountId, value)
        End Set
    End Property
    Dim fAccountPayableId As PaymentsAccountPayableXpo
    <Association("TreasuryCrossingAccountDetailCxPXpoReferencesPaymentsAccountPayableXpo")> _
    Public Property AccountPayableId() As PaymentsAccountPayableXpo
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayableXpo)
            SetPropertyValue(Of PaymentsAccountPayableXpo)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("TreasuryCrossingAccountDetailCxPXpoReferencesGeneralLedgerMainAccountsXpo")> _
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
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
