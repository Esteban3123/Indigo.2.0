
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewCampaignMainInfo")>
Public Class ViewCampaignMainInfoXpo
    Inherits XPLiteObject

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

    Dim fCampaignId As Integer
    <Persistent("CampaignId")>
    Public Property CampaignId() As Integer
        Get
            Return fCampaignId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignId", fCampaignId, value)
        End Set
    End Property

    Dim fCampaignNumber As Integer
    <Persistent("CampaignNumber")>
    Public Property CampaignNumber() As Integer
        Get
            Return fCampaignNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignNumber", fCampaignNumber, value)
        End Set
    End Property

    Dim fMSClass As Integer
    <Persistent("MSClass")>
    Public Property MSClass() As Integer
        Get
            Return fMSClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MSClass", fMSClass, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As Integer
    <Persistent("UnitDoseTypeId")>
    Public Property UnitDoseTypeId() As Integer
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fUnitDoseTypeDescription As String
    <Persistent("UnitDoseTypeDescription")>
    Public Property UnitDoseTypeDescription() As String
        Get
            Return fUnitDoseTypeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseTypeDescription", fUnitDoseTypeDescription, value)
        End Set
    End Property

    Dim fCampaignStatus As Integer
    <Persistent("CampaignStatus")>
    Public Property CampaignStatus() As Integer
        Get
            Return fCampaignStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignStatus", fCampaignStatus, value)
        End Set
    End Property

    Dim fDescriptionStatus As String
    <Persistent("DescriptionStatus")>
    Public Property DescriptionStatus() As String
        Get
            Return fDescriptionStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DescriptionStatus", fDescriptionStatus, value)
        End Set
    End Property

    Dim fQFQualityCodeName As String
    <Persistent("QFQualityCodeName")>
    Public Property QFQualityCodeName() As String
        Get
            Return fQFQualityCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QFQualityCodeName", fQFQualityCodeName, value)
        End Set
    End Property

    Dim fQFProductionCodeName As String
    <Persistent("QFProductionCodeName")>
    Public Property QFProductionCodeName() As String
        Get
            Return fQFProductionCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QFProductionCodeName", fQFProductionCodeName, value)
        End Set
    End Property

    Dim fModificationStatusDate As DateTime
    <Persistent("ModificationStatusDate")>
    Public Property ModificationStatusDate() As DateTime
        Get
            Return fModificationStatusDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationStatusDate", fModificationStatusDate, value)
        End Set
    End Property

    Dim fCreationDate As Date
    <Persistent("CreationDate")>
    Public Property CreationDate() As Date
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fProcessingDate As Date
    <Persistent("ProcessingDate")>
    Public Property ProcessingDate() As Date
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ProcessingDate", fProcessingDate, value)
        End Set
    End Property



#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
#End Region

End Class
