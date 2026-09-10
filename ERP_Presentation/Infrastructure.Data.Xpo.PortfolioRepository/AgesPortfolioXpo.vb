Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

<Persistent("Portfolio.AgesPortfolio")> _
Public Class AgesPortfolioXpo
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

    <Association("PortfolioProvisionDetailReferencesAgesPortfolio", GetType(PortfolioProvisionDetailXpo))> _
    Public ReadOnly Property PortfolioProvisionDetailXpo() As XPCollection(Of PortfolioProvisionDetailXpo)
        Get
            Return GetCollection(Of PortfolioProvisionDetailXpo)("PortfolioProvisionDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
