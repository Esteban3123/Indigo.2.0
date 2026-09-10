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
<Persistent("Authorization.ViewListPortfolioServices")>
Public Class ViewListPortfolioServicesXpo
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

    Dim fCUPSEntityId As Integer
    Public Property CUPSEntityId() As Integer
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CUPSEntityId", fCUPSEntityId, value)
        End Set
    End Property

    Dim fCUPSEntityCode As String
    Public Property CUPSEntityCode() As String
        Get
            Return fCUPSEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSEntityCode", fCUPSEntityCode, value)
        End Set
    End Property

    Dim fCUPSEntityName As String
    Public Property CUPSEntityName() As String
        Get
            Return fCUPSEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSEntityName", fCUPSEntityName, value)
        End Set
    End Property

    Dim fCUPSEntityCodeName As String
    Public Property CUPSEntityCodeName() As String
        Get
            Return fCUPSEntityCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSEntityCodeName", fCUPSEntityCodeName, value)
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

    Dim fContractDescriptionId As Integer
    Public Property ContractDescriptionId() As Integer
        Get
            Return fContractDescriptionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractDescriptionId", fContractDescriptionId, value)
        End Set
    End Property

    Dim fContractDescriptionCodeName As String
    Public Property ContractDescriptionCodeName() As String
        Get
            Return fContractDescriptionCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractDescriptionCodeName", fContractDescriptionCodeName, value)
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
