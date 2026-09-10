'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 16-04-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.UnitDoseType")>
Partial Public Class MixinStationUnitDoseTypeXpo
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
    Dim fCode As String
    <Size(20)>
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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
    Dim fDescription As String
    <Size(100)>
    <Persistent("Description")>
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim _msClass As Integer
    <Persistent("MSClass")>
    Public Property MSClass() As Integer
        Get
            Return _msClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MSClass", _msClass, value)
        End Set
    End Property

    <PersistentAlias("concat(concat(Code,' - '),Description)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property

    Dim fCreationUser As String
    <Size(20)>
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)>
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    Dim fPrefix As String
    <Persistent("Prefix")>
    Public Property Prefix() As String
        Get
            Return fPrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Prefix", fPrefix, value)
        End Set
    End Property

    <PersistentAlias("iif(MSClass = 2, 'NPT', MSClass = 3, 'Antibioticoterapia', MSClass = 4, 'Citostático', MSClass = 5, 'Reempaque', MSClass = 7, 'Reenvase', MSClass = 9, 'Magistral', MSClass = 10, 'Otros estériles', '')")>
    Public ReadOnly Property ClassName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ClassName"))
        End Get
    End Property

    <PersistentAlias("iif(State = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property WordState() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("WordState"))
        End Get
    End Property

    <Association("StabilityTableDetailferencesUnitDosesType", GetType(StabilityTableDetailXpo))>
    Public ReadOnly Property StabilityTableDetailXpo() As XPCollection(Of StabilityTableDetailXpo)
        Get
            Return GetCollection(Of StabilityTableDetailXpo)("StabilityTableDetailXpo")
        End Get
    End Property

    <Association("MedicinesProductionReferencesUnitDoseType", GetType(MedicinesProductionXpo))>
    Public ReadOnly Property MedicinesProductionXpo() As XPCollection(Of MedicinesProductionXpo)
        Get
            Return GetCollection(Of MedicinesProductionXpo)("MedicinesProductionXpo")
        End Get
    End Property

    <Association("PackageReferencesUnitDoseType", GetType(MixinStationPackageXpo))>
    Public ReadOnly Property MixinStationPackageXpo() As XPCollection(Of MixinStationPackageXpo)
        Get
            Return GetCollection(Of MixinStationPackageXpo)("MixinStationPackageXpo")
        End Get
    End Property

    <Association("RequestUnitDoseInventoryDetailReferencesUnitDoseType", GetType(RequestUnitDoseInventoryDetailXpo))>
    Public ReadOnly Property RequestUnitDoseInventoryDetailXpo() As XPCollection(Of RequestUnitDoseInventoryDetailXpo)
        Get
            Return GetCollection(Of RequestUnitDoseInventoryDetailXpo)("RequestUnitDoseInventoryDetailXpo")
        End Get
    End Property

    <Association("MaquilaReferencesUnitDoseType", GetType(RequestUnitDoseExternalCareCenterMaquilaXpo))>
    Public ReadOnly Property RequestUnitDoseExternalCareCenterMaquilaXpo() As XPCollection(Of RequestUnitDoseExternalCareCenterMaquilaXpo)
        Get
            Return GetCollection(Of RequestUnitDoseExternalCareCenterMaquilaXpo)("RequestUnitDoseExternalCareCenterMaquilaXpo")
        End Get
    End Property

    <Association("ProductionLineUnitDoseTypeReferencesUnitDoseType", GetType(ProductionLineUnitDoseTypeXpo))>
    Public ReadOnly Property ProductionLineUnitDoseTypeXpo() As XPCollection(Of ProductionLineUnitDoseTypeXpo)
        Get
            Return GetCollection(Of ProductionLineUnitDoseTypeXpo)("ProductionLineUnitDoseTypeXpo")
        End Get
    End Property

    <Association("CampaignDetailReferencesUnitDoseType", GetType(CampaignDetailXpo))>
    Public ReadOnly Property CampaignDetailXpo() As XPCollection(Of CampaignDetailXpo)
        Get
            Return GetCollection(Of CampaignDetailXpo)("CampaignDetailXpo")
        End Get
    End Property

    <Association("RequestMixingStationDetail_References_UnitDoseType", GetType(RequestMixingStationDetailXpo))>
    Public ReadOnly Property RequestMixingStationDetailXpos() As XPCollection(Of RequestMixingStationDetailXpo)
        Get
            Return GetCollection(Of RequestMixingStationDetailXpo)("RequestMixingStationDetailXpos")
        End Get
    End Property

    <Association("RequestUnitDoseExternalCareCenterPatientReferencesUnitDoseType", GetType(RequestUnitDoseExternalCareCenterPatientXpo))>
    Public ReadOnly Property RequestUnitDoseExternalCareCenterPatientXpo() As XPCollection(Of RequestUnitDoseExternalCareCenterPatientXpo)
        Get
            Return GetCollection(Of RequestUnitDoseExternalCareCenterPatientXpo)("RequestUnitDoseExternalCareCenterPatientXpo")
        End Get
    End Property

End Class