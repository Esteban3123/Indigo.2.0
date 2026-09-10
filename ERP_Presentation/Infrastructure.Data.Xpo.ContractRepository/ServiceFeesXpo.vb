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
<Persistent("Contract.ServiceFees")> _
Public Class ServiceFeesXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    <Size(100)> _
    <Persistent("InitialDate")> _
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fEndDate As DateTime
    <Size(100)> _
    <Persistent("EndDate")> _
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("EndDate", fEndDate, value)
        End Set
    End Property

    Dim fSalesValue As Decimal
    <Size(100)> _
    <Persistent("SalesValue")> _
    Public Property SalesValue() As Decimal
        Get
            Return fSalesValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of String)("SalesValue", fSalesValue, value)
        End Set
    End Property

    Dim fSalesValueWithSurcharge As Decimal
    <Size(100)> _
    <Persistent("SalesValueWithSurcharge")> _
    Public Property SalesValueWithSurcharge() As Decimal
        Get
            Return fSalesValueWithSurcharge
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of String)("SalesValueWithSurcharge", fSalesValueWithSurcharge, value)
        End Set
    End Property

    Dim fRateManualDetailId As RateManualDetailXpo
    <Association("ServiceFeesReferencesRateManualDetail")> _
    Public Property RateManualDetailId() As RateManualDetailXpo
        Get
            Return fRateManualDetailId
        End Get
        Set(ByVal value As RateManualDetailXpo)
            SetPropertyValue(Of RateManualDetailXpo)("RateManualDetailId", fRateManualDetailId, value)
        End Set
    End Property

    Dim fContractMinimumWageId As ContractMinimumWageXpo
    <Association("ServiceFeesReferencesContractMinimumWage")> _
    Public Property ContractMinimumWageId() As ContractMinimumWageXpo
        Get
            Return fContractMinimumWageId
        End Get
        Set(ByVal value As ContractMinimumWageXpo)
            SetPropertyValue(Of ContractMinimumWageXpo)("ContractMinimumWageId", fContractMinimumWageId, value)
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
