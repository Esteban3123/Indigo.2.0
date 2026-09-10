'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-11-08
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewReleaseLineToReport")>
Partial Public Class ViewReleaseLineToReportXpo
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

    Dim fWorkingAreaId As Integer
    Public Property WorkingAreaId() As Integer
        Get
            Return fWorkingAreaId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("WorkingAreaId", fWorkingAreaId, value)
        End Set
    End Property

    Dim fAirIgnitionTime As Date
    Public Property AirIgnitionTime() As Date
        Get
            Return fAirIgnitionTime
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("AirIgnitionTime", fAirIgnitionTime, value)
        End Set
    End Property

    Dim fCPIIgnitionTime As Date
    Public Property CPIIgnitionTime() As Date
        Get
            Return fCPIIgnitionTime
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("CPIIgnitionTime", fCPIIgnitionTime, value)
        End Set
    End Property

    Dim fEntryMixingStationTime As Date
    Public Property EntryMixingStationTime() As Date
        Get
            Return fEntryMixingStationTime
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("EntryMixingStationTime", fEntryMixingStationTime, value)
        End Set
    End Property

    Dim fPreparationStartTime As Date
    Public Property PreparationStartTime() As Date
        Get
            Return fPreparationStartTime
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("PreparationStartTime", fPreparationStartTime, value)
        End Set
    End Property

    Dim fIsSterile As Boolean
    Public Property IsSterile() As Boolean
        Get
            Return fIsSterile
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsSterile", fIsSterile, value)
        End Set
    End Property

    Dim fAdequacyItem1 As Boolean
    Public Property AdequacyItem1() As Boolean
        Get
            Return fAdequacyItem1
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem1", fAdequacyItem1, value)
        End Set
    End Property

    Dim fAdequacyItem2 As Boolean
    Public Property AdequacyItem2() As Boolean
        Get
            Return fAdequacyItem2
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem2", fAdequacyItem2, value)
        End Set
    End Property

    Dim fAdequacyItem3 As Boolean
    Public Property AdequacyItem3() As Boolean
        Get
            Return fAdequacyItem3
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem3", fAdequacyItem3, value)
        End Set
    End Property

    Dim fAdequacyItem4 As Boolean
    Public Property AdequacyItem4() As Boolean
        Get
            Return fAdequacyItem4
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem4", fAdequacyItem4, value)
        End Set
    End Property

    Dim fAdequacyItem5 As Boolean
    Public Property AdequacyItem5() As Boolean
        Get
            Return fAdequacyItem5
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem5", fAdequacyItem5, value)
        End Set
    End Property

    Dim fAdequacyItem6 As Boolean
    Public Property AdequacyItem6() As Boolean
        Get
            Return fAdequacyItem6
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem6", fAdequacyItem6, value)
        End Set
    End Property

    Dim fAdequacyItem7 As Boolean
    Public Property AdequacyItem7() As Boolean
        Get
            Return fAdequacyItem7
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem7", fAdequacyItem7, value)
        End Set
    End Property

    Dim fAdequacyItem8 As Boolean
    Public Property AdequacyItem8() As Boolean
        Get
            Return fAdequacyItem8
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem8", fAdequacyItem8, value)
        End Set
    End Property

    Dim fConditioningItem1 As Boolean
    Public Property ConditioningItem1() As Boolean
        Get
            Return fConditioningItem1
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem1", fConditioningItem1, value)
        End Set
    End Property

    Dim fConditioningItem2 As Boolean
    Public Property ConditioningItem2() As Boolean
        Get
            Return fConditioningItem2
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem2", fConditioningItem2, value)
        End Set
    End Property

    Dim fConditioningItem3 As Boolean
    Public Property ConditioningItem3() As Boolean
        Get
            Return fConditioningItem3
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem3", fConditioningItem3, value)
        End Set
    End Property

    Dim fConditioningItem4 As Boolean
    Public Property ConditioningItem4() As Boolean
        Get
            Return fConditioningItem4
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem4", fConditioningItem4, value)
        End Set
    End Property

    Dim fConditioningItem5 As Boolean
    Public Property ConditioningItem5() As Boolean
        Get
            Return fConditioningItem5
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem5", fConditioningItem5, value)
        End Set
    End Property

    Dim fConditioningItem6 As Boolean
    Public Property ConditioningItem6() As Boolean
        Get
            Return fConditioningItem6
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem6", fConditioningItem6, value)
        End Set
    End Property

    Dim fConditioningItem7 As Boolean
    Public Property ConditioningItem7() As Boolean
        Get
            Return fConditioningItem7
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem7", fConditioningItem7, value)
        End Set
    End Property

    Dim fConditioningItem8 As Boolean
    Public Property ConditioningItem8() As Boolean
        Get
            Return fConditioningItem8
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem8", fConditioningItem8, value)
        End Set
    End Property

    Dim fApplyItem9 As Boolean
    Public Property ApplyItem9() As Boolean
        Get
            Return fApplyItem9
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplyItem9", fApplyItem9, value)
        End Set
    End Property

    Dim fApplyItem11 As Boolean
    Public Property ApplyItem11() As Boolean
        Get
            Return fApplyItem11
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplyItem11", fApplyItem11, value)
        End Set
    End Property

    Dim fValueItem9 As Decimal
    Public Property ValueItem9() As Decimal
        Get
            Return fValueItem9
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueItem9", fValueItem9, value)
        End Set
    End Property

    Dim fValueItem11 As Decimal
    Public Property ValueItem11() As Decimal
        Get
            Return fValueItem11
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueItem11", fValueItem11, value)
        End Set
    End Property

    Dim fBatchesProcess As Integer
    Public Property BatchesProcess() As Integer
        Get
            Return fBatchesProcess
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BatchesProcess", fBatchesProcess, value)
        End Set
    End Property

    Dim fProductionLineId As Integer
    Public Property ProductionLineId() As Integer
        Get
            Return fProductionLineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductionLineId", fProductionLineId, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As Integer
    Public Property UnitDoseTypeId() As Integer
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fCampaignStatus As Byte
    Public Property CampaignStatus() As Byte
        Get
            Return fCampaignStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CampaignStatus", fCampaignStatus, value)
        End Set
    End Property

    Dim fProductionLineCode As String
    Public Property ProductionLineCode() As String
        Get
            Return fProductionLineCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineCode", fProductionLineCode, value)
        End Set
    End Property

    Dim fProductionLineName As String
    Public Property ProductionLineName() As String
        Get
            Return fProductionLineName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineName", fProductionLineName, value)
        End Set
    End Property

    Dim fUnitDoseCode As String
    Public Property UnitDoseCode() As String
        Get
            Return fUnitDoseCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseCode", fUnitDoseCode, value)
        End Set
    End Property

    Dim fUnitDoseName As String
    Public Property UnitDoseName() As String
        Get
            Return fUnitDoseName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseName", fUnitDoseName, value)
        End Set
    End Property

    Dim fWorkingAreaCode As String
    Public Property WorkingAreaCode() As String
        Get
            Return fWorkingAreaCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WorkingAreaCode", fWorkingAreaCode, value)
        End Set
    End Property

    Dim fWorkingAreaName As String
    Public Property WorkingAreaName() As String
        Get
            Return fWorkingAreaName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WorkingAreaName", fWorkingAreaName, value)
        End Set
    End Property

    Dim fLastUnitDoseCodeName As String
    Public Property LastUnitDoseCodeName() As String
        Get
            Return fLastUnitDoseCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastUnitDoseCodeName", fLastUnitDoseCodeName, value)
        End Set
    End Property

    Dim fLastCampaignDetailBatchesProcessed As Integer?
    Public Property LastCampaignDetailBatchesProcessed() As Integer?
        Get
            Return fLastCampaignDetailBatchesProcessed
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("LastCampaignDetailBatchesProcessed", fLastCampaignDetailBatchesProcessed, value)
        End Set
    End Property

    Dim fCreationUser As String
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCreationDate As Date
    Public Property CreationDate() As Date
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("CreationDate", fCreationDate, value)
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

    Dim fUserSupervisor As String
    Public Property UserSupervisor() As String
        Get
            Return fUserSupervisor
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserSupervisor", fUserSupervisor, value)
        End Set
    End Property

End Class