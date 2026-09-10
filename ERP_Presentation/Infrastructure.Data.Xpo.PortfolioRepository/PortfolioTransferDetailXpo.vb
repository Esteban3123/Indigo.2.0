Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

<Persistent("Portfolio.PortfolioTransferDetail")> _
Public Class PortfolioTransferDetailXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

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

    Dim fPortfolioTrasferId As PortfolioTransferXpo
    <Association("PortfolioTransferDetailReferencesPortfolioTransfer")> _
    Public Property PortfolioTrasferId() As PortfolioTransferXpo
        Get
            Return fPortfolioTrasferId
        End Get
        Set(ByVal value As PortfolioTransferXpo)
            SetPropertyValue(Of PortfolioTransferXpo)("PortfolioTrasferId", fPortfolioTrasferId, value)
        End Set
    End Property

    Dim fAccountReceivableId As PortfolioAccountReceivableXpo
    <Association("PortfolioTransferDetailReferencesAccountReceivable")> _
    Public Property AccountReceivableId() As PortfolioAccountReceivableXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableXpo)
            SetPropertyValue(Of PortfolioAccountReceivableXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property

    Dim fMainAccountId As MainAccountsXpo
    <Association("PortfolioTransferDetailReferencesMainAccount")> _
    Public Property MainAccountId() As MainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As MainAccountsXpo)
            SetPropertyValue(Of MainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
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

End Class
