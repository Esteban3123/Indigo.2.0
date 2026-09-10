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

    Dim fCUPSEntityId As CupsEntityXpo
    <Association("CupsEntityDescriptionReferencesCups")>
    Public Property CUPSEntityId() As CupsEntityXpo
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As CupsEntityXpo)
            SetPropertyValue(Of CupsEntityXpo)("CUPSEntityId", fCUPSEntityId, value)
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

    Dim fCupsSubgroupId As CupsSubGroupXpo
    <Association("CupsEntityDescriptionReferencesSubgroup")>
    Public Property CupsSubgroupId() As CupsSubGroupXpo
        Get
            Return fCupsSubgroupId
        End Get
        Set(ByVal value As CupsSubGroupXpo)
            SetPropertyValue(Of CupsSubGroupXpo)("CupsSubgroupId", fCupsSubgroupId, value)
        End Set
    End Property

    Dim fBillingGroupId As BillingGroupXpo
    <Association("CupsEntityDescriptionReferencesBillingGroup")>
    Public Property BillingGroupId() As BillingGroupXpo
        Get
            Return fBillingGroupId
        End Get
        Set(ByVal value As BillingGroupXpo)
            SetPropertyValue(Of BillingGroupXpo)("BillingGroupId", fBillingGroupId, value)
        End Set
    End Property

    Dim fBillingConceptId As BillingConcept
    <Association("CupsEntityDescriptionReferencesBillingConcept")>
    Public Property BillingConceptId() As BillingConcept
        Get
            Return fBillingConceptId
        End Get
        Set(ByVal value As BillingConcept)
            SetPropertyValue(Of BillingConcept)("BillingConceptId", fBillingConceptId, value)
        End Set
    End Property

    Dim fIsDelete As Boolean
    Public Property IsDelete() As Boolean
        Get
            Return fIsDelete
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsDelete", fIsDelete, value)
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
