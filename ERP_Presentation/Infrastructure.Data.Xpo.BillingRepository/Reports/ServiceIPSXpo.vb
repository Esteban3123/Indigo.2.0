Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.IPSService")> _
Public Class ServiceIPSXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    '<Indexed(Name:="IX_IPSService", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fServiceManual As Byte
    Public Property ServiceManual() As Byte
        Get
            Return fServiceManual
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ServiceManual", fServiceManual, value)
        End Set
    End Property
    Dim fServiceClass As Byte
    Public Property ServiceClass() As Byte
        Get
            Return fServiceClass
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ServiceClass", fServiceClass, value)
        End Set
    End Property
    Dim fServiceType As Byte
    Public Property ServiceType() As Byte
        Get
            Return fServiceType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ServiceType", fServiceType, value)
        End Set
    End Property
    Dim fPresentation As Byte
    Public Property Presentation() As Byte
        Get
            Return fPresentation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Presentation", fPresentation, value)
        End Set
    End Property
    Dim fSurgicalGroupId As Integer
    Public Property SurgicalGroupId() As Integer
        Get
            Return fSurgicalGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SurgicalGroupId", fSurgicalGroupId, value)
        End Set
    End Property
    Dim fUVRNumber As Integer
    Public Property UVRNumber() As Integer
        Get
            Return fUVRNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UVRNumber", fUVRNumber, value)
        End Set
    End Property
    Dim fAuthorizationLevel As Byte
    Public Property AuthorizationLevel() As Byte
        Get
            Return fAuthorizationLevel
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AuthorizationLevel", fAuthorizationLevel, value)
        End Set
    End Property
    Dim fContributionsWeeks As Integer
    Public Property ContributionsWeeks() As Integer
        Get
            Return fContributionsWeeks
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContributionsWeeks", fContributionsWeeks, value)
        End Set
    End Property
    Dim fProcedure As Byte
    Public Property Procedure() As Byte
        Get
            Return fProcedure
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Procedure", fProcedure, value)
        End Set
    End Property
    Dim fSubattentionCode As Byte
    Public Property SubattentionCode() As Byte
        Get
            Return fSubattentionCode
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SubattentionCode", fSubattentionCode, value)
        End Set
    End Property
    Dim fMinimunAgeUnit As Byte
    Public Property MinimunAgeUnit() As Byte
        Get
            Return fMinimunAgeUnit
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MinimunAgeUnit", fMinimunAgeUnit, value)
        End Set
    End Property
    Dim fMinimunAge As Integer
    Public Property MinimunAge() As Integer
        Get
            Return fMinimunAge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MinimunAge", fMinimunAge, value)
        End Set
    End Property
    Dim fMaximumAgeUnit As Byte
    Public Property MaximumAgeUnit() As Byte
        Get
            Return fMaximumAgeUnit
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MaximumAgeUnit", fMaximumAgeUnit, value)
        End Set
    End Property
    Dim fMaximumAge As Integer
    Public Property MaximumAge() As Integer
        Get
            Return fMaximumAge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaximumAge", fMaximumAge, value)
        End Set
    End Property
    Dim fInMale As Boolean
    Public Property InMale() As Boolean
        Get
            Return fInMale
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("InMale", fInMale, value)
        End Set
    End Property
    Dim fInFemale As Boolean
    Public Property InFemale() As Boolean
        Get
            Return fInFemale
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("InFemale", fInFemale, value)
        End Set
    End Property
    Dim fChildbirthAbortion As Boolean
    Public Property ChildbirthAbortion() As Boolean
        Get
            Return fChildbirthAbortion
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ChildbirthAbortion", fChildbirthAbortion, value)
        End Set
    End Property
    Dim fPOS As Boolean
    Public Property POS() As Boolean
        Get
            Return fPOS
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("POS", fPOS, value)
        End Set
    End Property
    Dim fComplexityLevel As Byte
    Public Property ComplexityLevel() As Byte
        Get
            Return fComplexityLevel
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ComplexityLevel", fComplexityLevel, value)
        End Set
    End Property
    Dim fPromotionAndPrevention As Boolean
    Public Property PromotionAndPrevention() As Boolean
        Get
            Return fPromotionAndPrevention
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PromotionAndPrevention", fPromotionAndPrevention, value)
        End Set
    End Property
    Dim fPromotionAndPreventionActivities As String
    <Size(SizeAttribute.Unlimited)> _
    Public Property PromotionAndPreventionActivities() As String
        Get
            Return fPromotionAndPreventionActivities
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PromotionAndPreventionActivities", fPromotionAndPreventionActivities, value)
        End Set
    End Property
    Dim fSurgeryArtroscopica As Boolean
    Public Property SurgeryArtroscopica() As Boolean
        Get
            Return fSurgeryArtroscopica
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SurgeryArtroscopica", fSurgeryArtroscopica, value)
        End Set
    End Property
    Dim fPathologyService As Boolean
    Public Property PathologyService() As Boolean
        Get
            Return fPathologyService
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PathologyService", fPathologyService, value)
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
    Dim fCreationUser As String
    <Size(20)> _
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
    <Size(20)> _
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
    <Association("Billing_ServiceOrderDetailReferencesContract_IPSService", GetType(OrderServiceDetailXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetails() As XPCollection(Of OrderServiceDetailXpo)
        Get
            Return GetCollection(Of OrderServiceDetailXpo)("Billing_ServiceOrderDetails")
        End Get
    End Property
    <Association("Billing_ServiceOrderDetailSurgicalReferencesContract_IPSService", GetType(OrderServiceDetailSurgicalXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetailSurgicals() As XPCollection(Of OrderServiceDetailSurgicalXpo)
        Get
            Return GetCollection(Of OrderServiceDetailSurgicalXpo)("Billing_ServiceOrderDetailSurgicals")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
