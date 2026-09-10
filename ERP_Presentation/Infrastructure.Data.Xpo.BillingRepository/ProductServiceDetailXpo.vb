Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ProductServiceDetail")>
Public Class ProductServiceDetailXpo
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

    Dim fPharmaceuticalDispensingDetailId As Integer
    Public Property PharmaceuticalDispensingDetailId() As Integer
        Get
            Return fPharmaceuticalDispensingDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PharmaceuticalDispensingDetailId", fPharmaceuticalDispensingDetailId, value)
        End Set
    End Property

    Dim fServiceOrderDetailId As Integer
    Public Property ServiceOrderDetailId() As Integer
        Get
            Return fServiceOrderDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceOrderDetailId", fServiceOrderDetailId, value)
        End Set
    End Property

    <PersistentAlias("CUPSEntity.Id")>
    Public ReadOnly Property CUPSEntityId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CUPSEntityId"))
        End Get
    End Property

    <PersistentAlias("ContractDescriptions.Id")>
    Public ReadOnly Property ContractDescriptionsId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("ContractDescriptionsId"))
        End Get
    End Property

    Dim fProductId As Integer?
    Public Property ProductId() As Integer?
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fPrice As Decimal
    Public Property Price() As Decimal
        Get
            Return fPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Price", fPrice, value)
        End Set
    End Property

    Dim fLiquidationType As Byte
    Public Property LiquidationType() As Byte
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LiquidationType", fLiquidationType, value)
        End Set
    End Property


    Dim fRateType As Byte
    Public Property RateType() As Byte
        Get
            Return fRateType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RateType", fRateType, value)
        End Set
    End Property

    Dim fCUPSEntity As CUPSEntityXpo
    <Persistent("CUPSEntityId")>
    <Association("ProductServiceDetailReferencesContract_CUPSEntity")>
    Public Property CUPSEntity() As CUPSEntityXpo
        Get
            Return fCUPSEntity
        End Get
        Set(ByVal value As CUPSEntityXpo)
            SetPropertyValue(Of CUPSEntityXpo)("CUPSEntity", fCUPSEntity, value)
        End Set
    End Property

    Dim fContractDescriptions As ContractDescriptionsXpo
    <Persistent("ContractDescriptionsId")>
    <Association("ProductServiceDetailReferencesDescriptions")>
    Public Property ContractDescriptions() As ContractDescriptionsXpo
        Get
            Return fContractDescriptions
        End Get
        Set(ByVal value As ContractDescriptionsXpo)
            SetPropertyValue(Of ContractDescriptionsXpo)("ContractDescriptions", fContractDescriptions, value)
        End Set
    End Property


    <PersistentAlias("Iif(LiquidationType = 1, 'Tarifa',LiquidationType=2,'Servicio',LiquidationType=3,'Tarifa-Servicio','N/A')")>
    Public ReadOnly Property LiquidationTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("LiquidationTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(RateType = 1, 'Tarifa Fija',RateType=2,'Basado en porcentaje','N/A')")>
    Public ReadOnly Property RateTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("RateTypeName"))
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class