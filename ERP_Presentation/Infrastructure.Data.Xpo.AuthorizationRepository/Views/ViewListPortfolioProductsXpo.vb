'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/09/2014
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
<Persistent("Authorization.ViewListPortfolioProducts")>
Public Class ViewListPortfolioProductsXpo
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

    Dim fAuthorizationPortfolioId As Integer
    Public Property AuthorizationPortfolioId() As Integer
        Get
            Return fAuthorizationPortfolioId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AuthorizationPortfolioId", fAuthorizationPortfolioId, value)
        End Set
    End Property

    Dim fAuthorizationCode As String
    Public Property AuthorizationCode() As String
        Get
            Return fAuthorizationCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationCode", fAuthorizationCode, value)
        End Set
    End Property

    Dim fAuthorizationName As String
    Public Property AuthorizationName() As String
        Get
            Return fAuthorizationName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationName", fAuthorizationName, value)
        End Set
    End Property

    Dim fAuthorizationCodeName As String
    Public Property AuthorizationCodeName() As String
        Get
            Return fAuthorizationCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationCodeName", fAuthorizationCodeName, value)
        End Set
    End Property

    Dim fAuthorizationGroupId As Integer
    Public Property AuthorizationGroupId() As Integer
        Get
            Return fAuthorizationGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AuthorizationGroupId", fAuthorizationGroupId, value)
        End Set
    End Property

    Dim fAuthorizationGroupCode As String
    Public Property AuthorizationGroupCode() As String
        Get
            Return fAuthorizationGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationGroupCode", fAuthorizationGroupCode, value)
        End Set
    End Property

    Dim fAuthorizationGroupName As String
    Public Property AuthorizationGroupName() As String
        Get
            Return fAuthorizationGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationGroupName", fAuthorizationGroupName, value)
        End Set
    End Property

    Dim fAuthorizationGroupCodeName As String
    Public Property AuthorizationGroupCodeName() As String
        Get
            Return fAuthorizationGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationGroupCodeName", fAuthorizationGroupCodeName, value)
        End Set
    End Property

    Dim fInventoryProductId As Integer
    Public Property InventoryProductId() As Integer
        Get
            Return fInventoryProductId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InventoryProductId", fInventoryProductId, value)
        End Set
    End Property

    Dim fInventoryProductCode As String
    Public Property InventoryProductCode() As String
        Get
            Return fInventoryProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InventoryProductCode", fInventoryProductCode, value)
        End Set
    End Property

    Dim fInventoryProductName As String
    Public Property InventoryProductName() As String
        Get
            Return fInventoryProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InventoryProductName", fInventoryProductName, value)
        End Set
    End Property

    Dim fInventoryProductCodeName As String
    Public Property InventoryProductCodeName() As String
        Get
            Return fInventoryProductCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InventoryProductCodeName", fInventoryProductCodeName, value)
        End Set
    End Property

    Dim fSelectOption As Boolean
    Public Property SelectOption() As Boolean
        Get
            Return fSelectOption
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SelectOption", fSelectOption, value)
        End Set
    End Property

    Dim fConfigurationServicesAmbulatoryId As Integer
    Public Property ConfigurationServicesAmbulatoryId() As Integer
        Get
            Return fConfigurationServicesAmbulatoryId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConfigurationServicesAmbulatoryId", fConfigurationServicesAmbulatoryId, value)
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
