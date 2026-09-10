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

<Persistent("MixingStation.CampaignDetail")>
Partial Public Class CampaignDetailXpo
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

    Dim fCampaignId As CampaignXpo
    <Association("CampaignDetailReferencesCampaign")>
    Public Property CampaignId() As CampaignXpo
        Get
            Return fCampaignId
        End Get
        Set(ByVal value As CampaignXpo)
            SetPropertyValue(Of CampaignXpo)("CampaignId", fCampaignId, value)
        End Set
    End Property

    Dim fProductionLineId As MixingStationProductionLineXpo
    <Association("CampaignDetailReferencesProductionLine")>
    Public Property ProductionLineId() As MixingStationProductionLineXpo
        Get
            Return fProductionLineId
        End Get
        Set(ByVal value As MixingStationProductionLineXpo)
            SetPropertyValue(Of MixingStationProductionLineXpo)("ProductionLineId", fProductionLineId, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As MixinStationUnitDoseTypeXpo
    <Association("CampaignDetailReferencesUnitDoseType")>
    Public Property UnitDoseTypeId() As MixinStationUnitDoseTypeXpo
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As MixinStationUnitDoseTypeXpo)
            SetPropertyValue(Of MixinStationUnitDoseTypeXpo)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fCampaignNumber As Integer
    Public Property CampaignNumber() As Integer
        Get
            Return fCampaignNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignNumber", fCampaignNumber, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
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

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fLabelConfirmationDate As DateTime
    Public Property LabelConfirmationDate() As DateTime
        Get
            Return fLabelConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LabelConfirmationDate", fLabelConfirmationDate, value)
        End Set
    End Property

    Dim fFinishDate As DateTime
    Public Property FinishDate() As DateTime
        Get
            Return fFinishDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FinishDate", fFinishDate, value)
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

    Dim fProductionBasketId As Integer?
    Public Property ProductionBasketId() As Integer?
        Get
            Return fProductionBasketId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ProductionBasketId", fProductionBasketId, value)
        End Set
    End Property

    Dim fProcessingDate As DateTime
    Public Property ProcessingDate() As DateTime
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ProcessingDate", fProcessingDate, value)
        End Set
    End Property

    Dim fWorkingAreaId As WorkingAreaXpo
    <Association("CampaignDetailReferencesWorkingArea")>
    Public Property WorkingAreaId() As WorkingAreaXpo
        Get
            Return fWorkingAreaId
        End Get
        Set(ByVal value As WorkingAreaXpo)
            SetPropertyValue(Of WorkingAreaXpo)("WorkingAreaId", fWorkingAreaId, value)
        End Set
    End Property

    Dim fPreparationTime As TimeSpan
    Public Property PreparationTime() As TimeSpan
        Get
            Return fPreparationTime
        End Get
        Set(ByVal value As TimeSpan)
            SetPropertyValue(Of TimeSpan)("PreparationTime", fPreparationTime, value)
        End Set
    End Property


#Region "PersistentAlias"
    <PersistentAlias("Concat('Campaña # ', ToStr(CampaignNumber))")>
    Public ReadOnly Property Title() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("Title"))
        End Get
    End Property

    <PersistentAlias("concat('CM: ', CampaignId.CMConfiguration.Code, ', ', 'Campaña # ', ToStr(CampaignNumber))")>
    Public ReadOnly Property FullTitle() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("FullTitle"))
        End Get
    End Property

    <PersistentAlias("IIF(CampaignStatus = 1,'Abierta',CampaignStatus = 2,'Cerrada',CampaignStatus = 3,'Bloqueada',CampaignStatus = 4,'Anulada',CampaignStatus = 5, 'Procesada', 'Terminada')")>
    Public ReadOnly Property CampaignStatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CampaignStatusName"))
        End Get
    End Property

    <PersistentAlias("CampaignDetailUsersXpo[UserRole = 2].single(UserCodeRoleName)")>
    Public ReadOnly Property QFUser() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("QFUser"))
        End Get
    End Property

    <PersistentAlias("CampaignId.CMConfiguration.CodeName")>
    Public ReadOnly Property CMConfigurationCodeName() As String
        Get
            Return Convert.ToString(EvaluateAlias("CMConfigurationCodeName"))
        End Get
    End Property

#End Region
#Region "Association"
    <Association("CampaignDetailUsersReferencesCampaignDetail", GetType(CampaignDetailUsersXpo))>
    Public ReadOnly Property CampaignDetailUsersXpo() As XPCollection(Of CampaignDetailUsersXpo)
        Get
            Return GetCollection(Of CampaignDetailUsersXpo)("CampaignDetailUsersXpo")
        End Get
    End Property

    <Association("RawMaterialDevolution_References_CampaignDetail", GetType(RawMaterialDevolutionXpo))>
    Public ReadOnly Property RawMaterialDevolutions() As XPCollection(Of RawMaterialDevolutionXpo)
        Get
            Return GetCollection(Of RawMaterialDevolutionXpo)("RawMaterialDevolutions")
        End Get
    End Property

    <Association("ReleaseLine_Reference_CampaigDetail", GetType(ReleaseLineXpo))>
    Public ReadOnly Property ReleaseLines() As XPCollection(Of ReleaseLineXpo)
        Get
            Return GetCollection(Of ReleaseLineXpo)("ReleaseLines")
        End Get
    End Property
#End Region


End Class