'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-11-26
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewProductionScheduleToReport")>
Partial Public Class ViewProductionScheduleToReportXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

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

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fAtcId As Integer?
    Public Property AtcId() As Integer?
        Get
            Return fAtcId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AtcId", fAtcId, value)
        End Set
    End Property

    Dim fSupplyId As Integer?
    Public Property SupplyId() As Integer?
        Get
            Return fSupplyId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("SupplyId", fSupplyId, value)
        End Set
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

    Dim fItemType As Byte
    Public Property ItemType() As Byte
        Get
            Return fItemType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ItemType", fItemType, value)
        End Set
    End Property

    Dim fConcentration As String
    Public Property Concentration() As String
        Get
            Return fConcentration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Concentration", fConcentration, value)
        End Set
    End Property

    Dim fItemCodeName As String
    Public Property ItemCodeName() As String
        Get
            Return fItemCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeName", fItemCodeName, value)
        End Set
    End Property

    Dim fBatchCodes As String
    Public Property BatchCodes() As String
        Get
            Return fBatchCodes
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCodes", fBatchCodes, value)
        End Set
    End Property

    Dim fRequestQuantities As Integer
    Public Property RequestQuantities() As Integer
        Get
            Return fRequestQuantities
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestQuantities", fRequestQuantities, value)
        End Set
    End Property

    Dim fDispensingQuantities As Integer
    Public Property DispensingQuantities() As Integer
        Get
            Return fDispensingQuantities
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DispensingQuantities", fDispensingQuantities, value)
        End Set
    End Property

    Dim fIsCold As Boolean
    Public Property IsCold() As Boolean
        Get
            Return fIsCold
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsCold", fIsCold, value)
        End Set
    End Property

    Dim fIsEnvironment As Boolean
    Public Property IsEnvironment() As Boolean
        Get
            Return fIsEnvironment
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsEnvironment", fIsEnvironment, value)
        End Set
    End Property

    Dim fAditionalQuantities As Integer
    Public Property AditionalQuantities() As Integer
        Get
            Return fAditionalQuantities
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AditionalQuantities", fAditionalQuantities, value)
        End Set
    End Property

    Dim fDevolutionQuantities As Integer
    Public Property DevolutionQuantities() As Integer
        Get
            Return fDevolutionQuantities
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DevolutionQuantities", fDevolutionQuantities, value)
        End Set
    End Property

    Dim fProductionDate As Date
    Public Property ProductionDate() As Date
        Get
            Return fProductionDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ProductionDate", fProductionDate, value)
        End Set
    End Property

    Dim fProductionScheduleCode As String
    Public Property ProductionScheduleCode() As String
        Get
            Return fProductionScheduleCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionScheduleCode", fProductionScheduleCode, value)
        End Set
    End Property

    Dim fUnitDoseTypeName As String
    Public Property UnitDoseTypeName() As String
        Get
            Return fUnitDoseTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseTypeName", fUnitDoseTypeName, value)
        End Set
    End Property

    Dim fUserCode As String
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

    Dim fCantidadLotes As Integer
    Public Property CantidadLotes() As Integer
        Get
            Return fCantidadLotes
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadLotes", fCantidadLotes, value)
        End Set
    End Property

End Class