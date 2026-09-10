'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 06-08-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ProductionLine")>
Partial Public Class MixingStationProductionLineXpo
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

    Dim _Id As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", _Id, value)
        End Set
    End Property

    Dim _Name As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", _Name, value)
        End Set
    End Property

    Dim _Code As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", _Code, value)
        End Set
    End Property

    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    <PersistentAlias("Iif(State = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fFunctionalUnit As CMFunctionalUnitXpo
    <Association("MixingStation_ProductionLineReferencesFunctionalUnit")> _
    Public Property Id_FunctionalUnit() As CMFunctionalUnitXpo
        Get
            Return fFunctionalUnit
        End Get
        Set(ByVal value As CMFunctionalUnitXpo)
            SetPropertyValue(Of CMFunctionalUnitXpo)("Id_FunctionalUnit",  fFunctionalUnit, value)
        End Set
    End Property

    <Association("ProductionLineUnitDoseTypeReferencesProductionLine", GetType(ProductionLineUnitDoseTypeXpo))>
    Public ReadOnly Property ProductionLineUnitDoseTypeXpo() As XPCollection(Of ProductionLineUnitDoseTypeXpo)
        Get
            Return GetCollection(Of ProductionLineUnitDoseTypeXpo)("ProductionLineUnitDoseTypeXpo")
        End Get
    End Property

    <Association("CampaignDetailReferencesProductionLine", GetType(CampaignDetailXpo))>
    Public ReadOnly Property CampaignDetailXpo() As XPCollection(Of CampaignDetailXpo)
        Get
            Return GetCollection(Of CampaignDetailXpo)("CampaignDetailXpo")
        End Get
    End Property

    <Association("CMMixingProducitonLine_References_ProductionLine", GetType(CMMixingProducitonLineXpo))>
    Public ReadOnly Property CMMixingProducitonLineXpo() As XPCollection(Of CMMixingProducitonLineXpo)
        Get
            Return GetCollection(Of CMMixingProducitonLineXpo)("CMMixingProducitonLineXpo")
        End Get
    End Property

    <Association("CMMixingProducitonLine_References_CMExternalCareCenter", GetType(CMExternalCareCenterXpo))>
    Public ReadOnly Property CMExternalCareCenterXpo() As XPCollection(Of CMExternalCareCenterXpo)
        Get
            Return GetCollection(Of CMExternalCareCenterXpo)("CMExternalCareCenterXpo")
        End Get
    End Property

    <Association("CMCenterAttention_References_ProductionLine", GetType(CMCenterAttentionXpo))>
    Public ReadOnly Property CMCenterAttentions() As XPCollection(Of CMCenterAttentionXpo)
        Get
            Return GetCollection(Of CMCenterAttentionXpo)("CMCenterAttentions")
        End Get
    End Property

End Class
