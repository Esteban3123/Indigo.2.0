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
<Persistent("Authorization.AuthorizationPortfolioCUPSEntity")>
Public Class AuthorizationPortfolioCUPSEntityXpo
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
    <Association("CUPSReferencesPortfolio")>
    Public Property AuthorizationPortfolioId() As AuthorizationPortfolioXpo
        Get
            Return fAuthorizationPortfolioId
        End Get
        Set(ByVal value As AuthorizationPortfolioXpo)
            SetPropertyValue(Of AuthorizationPortfolioXpo)("AuthorizationPortfolioId", fAuthorizationPortfolioId, value)
        End Set
    End Property

    Dim fAuthorizationGroupId As AuthorizationGroupXpo
    <Association("CUPSReferencesGroup")>
    Public Property AuthorizationGroupId() As AuthorizationGroupXpo
        Get
            Return fAuthorizationGroupId
        End Get
        Set(ByVal value As AuthorizationGroupXpo)
            SetPropertyValue(Of AuthorizationGroupXpo)("AuthorizationGroupId", fAuthorizationGroupId, value)
        End Set
    End Property

    Dim fCUPSEntityId As CupsEntityXpo
    <Association("CUPSReferencesCUPS")>
    Public Property CUPSEntityId() As CupsEntityXpo
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As CupsEntityXpo)
            SetPropertyValue(Of CupsEntityXpo)("CUPSEntityId", fCUPSEntityId, value)
        End Set
    End Property

    Dim fContractDescriptionId As ContractDescriptionsXpo
    <Association("CUPSReferencesDescriptions")>
    Public Property ContractDescriptionId() As ContractDescriptionsXpo
        Get
            Return fContractDescriptionId
        End Get
        Set(ByVal value As ContractDescriptionsXpo)
            SetPropertyValue(Of ContractDescriptionsXpo)("ContractDescriptionId", fContractDescriptionId, value)
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
