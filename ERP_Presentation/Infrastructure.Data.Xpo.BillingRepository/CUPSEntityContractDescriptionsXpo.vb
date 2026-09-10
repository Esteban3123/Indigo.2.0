'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2019
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
<Persistent("Contract.CUPSEntityContractDescriptions")>
Public Class CUPSEntityContractDescriptionsXpo
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

    Dim fContractDescriptionId As ContractDescriptionsXpo
    <Association("CupsEntityDescriptionReferencesDescriptions")>
    Public Property ContractDescriptionId() As ContractDescriptionsXpo
        Get
            Return fContractDescriptionId
        End Get
        Set(ByVal value As ContractDescriptionsXpo)
            SetPropertyValue(Of ContractDescriptionsXpo)("ContractDescriptionId", fContractDescriptionId, value)
        End Set
    End Property

    <Association("ServiceOrderDetailReferencesDescriptions", GetType(ServiceOrderDetailXpo))>
    Public ReadOnly Property ServiceOrderDetailXpo() As XPCollection(Of ServiceOrderDetailXpo)
        Get
            Return GetCollection(Of ServiceOrderDetailXpo)("ServiceOrderDetailXpo")
        End Get
    End Property

    <Association("QuotationServiceOrderDetailReferencesDescriptions", GetType(QuotationServiceOrderDetailXpo))>
    Public ReadOnly Property QuotationServiceOrderDetailXpo() As XPCollection(Of QuotationServiceOrderDetailXpo)
        Get
            Return GetCollection(Of QuotationServiceOrderDetailXpo)("QuotationServiceOrderDetailXpo")
        End Get
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
