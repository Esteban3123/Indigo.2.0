'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/05/2020
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Authorization.AuthorizationPortfolioInventoryProduct")>
Public Class AuthorizationPortfolioInventoryProductXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fAuthorizationPortfolioId As AuthorizationPortfolioXpo
    <Association("ProductReferencesPortfolio")>
    Public Property AuthorizationPortfolioId() As AuthorizationPortfolioXpo
        Get
            Return fAuthorizationPortfolioId
        End Get
        Set(ByVal value As AuthorizationPortfolioXpo)
            SetPropertyValue(Of AuthorizationPortfolioXpo)("AuthorizationPortfolioId", fAuthorizationPortfolioId, value)
        End Set
    End Property

    Dim fAuthorizationGroupId As AuthorizationGroupXpo
    <Association("ProductReferencesGroup")>
    Public Property AuthorizationGroupId() As AuthorizationGroupXpo
        Get
            Return fAuthorizationGroupId
        End Get
        Set(ByVal value As AuthorizationGroupXpo)
            SetPropertyValue(Of AuthorizationGroupXpo)("AuthorizationGroupId", fAuthorizationGroupId, value)
        End Set
    End Property

    Dim fInventoryProductId As InventoryProductXpo
    <Association("ProductReferencesProduct")>
    Public Property InventoryProductId() As InventoryProductXpo
        Get
            Return fInventoryProductId
        End Get
        Set(ByVal value As InventoryProductXpo)
            SetPropertyValue(Of InventoryProductXpo)("InventoryProductId", fInventoryProductId, value)
        End Set
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
