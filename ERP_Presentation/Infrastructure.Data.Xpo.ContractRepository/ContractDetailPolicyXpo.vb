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
<Persistent("Contract.ContractDetailPolicy")>
Public Class ContractDetailPolicyXpo
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

    Dim fContractDetailId As ContractDetailXpo
    <Association("ContractDetailPolicyReferencesContractDetail")>
    Public Property ContractDetailId() As ContractDetailXpo
        Get
            Return fContractDetailId
        End Get
        Set(ByVal value As ContractDetailXpo)
            SetPropertyValue(Of ContractDetailXpo)("ContractDetailId", fContractDetailId, value)
        End Set
    End Property

    Dim fFixedAssetPolicyId As FixedAssetPolizaXpo
    <Association("ContractDetailPolicyReferencesFixedAssetPolicy")>
    Public Property FixedAssetPolicyId() As FixedAssetPolizaXpo
        Get
            Return fFixedAssetPolicyId
        End Get
        Set(ByVal value As FixedAssetPolizaXpo)
            SetPropertyValue(Of FixedAssetPolizaXpo)("FixedAssetPolicyId", fFixedAssetPolicyId, value)
        End Set
    End Property

    Dim fFixedAssetInsuranceId As FixedAssetInsuranceXpo
    <Association("ContractDetailPolicyReferencesFixedAssetInsurance")>
    Public Property FixedAssetInsuranceId() As FixedAssetInsuranceXpo
        Get
            Return fFixedAssetInsuranceId
        End Get
        Set(ByVal value As FixedAssetInsuranceXpo)
            SetPropertyValue(Of FixedAssetInsuranceXpo)("FixedAssetInsuranceId", fFixedAssetInsuranceId, value)
        End Set
    End Property

    Dim fPolicyNumber As String
    Public Property PolicyNumber() As String
        Get
            Return fPolicyNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PolicyNumber", fPolicyNumber, value)
        End Set
    End Property

    Dim fEmissionDate As DateTime
    Public Property EmissionDate() As DateTime
        Get
            Return fEmissionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EmissionDate", fEmissionDate, value)
        End Set
    End Property

    Dim fAmountInsured As Decimal
    Public Property AmountInsured() As Decimal
        Get
            Return fAmountInsured
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountInsured", fAmountInsured, value)
        End Set
    End Property

    Dim fCoveragePercentage As Decimal
    Public Property CoveragePercentage() As Decimal
        Get
            Return fCoveragePercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CoveragePercentage", fCoveragePercentage, value)
        End Set
    End Property

    Dim fObservation As String
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
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
