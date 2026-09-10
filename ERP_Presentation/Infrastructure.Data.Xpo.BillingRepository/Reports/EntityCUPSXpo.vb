Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.CUPSEntity")> _
Public Class EntityCUPSXpo
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
    Dim fCUPSSubGroupId As Integer
    Public Property CUPSSubGroupId() As Integer
        Get
            Return fCUPSSubGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CUPSSubGroupId", fCUPSSubGroupId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(300)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fRIPSCode As String
    <Size(20)> _
    Public Property RIPSCode() As String
        Get
            Return fRIPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RIPSCode", fRIPSCode, value)
        End Set
    End Property
    Dim fRIPSDescription As String
    <Size(300)> _
    Public Property RIPSDescription() As String
        Get
            Return fRIPSDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RIPSDescription", fRIPSDescription, value)
        End Set
    End Property
    'Dim fIPSServiceGroupId As ConceptReportXpo
    '<Association("Contract_CUPSEntityReferencesBilling_BillingConcept")> _
    'Public Property IPSServiceGroupId() As ConceptReportXpo
    '    Get
    '        Return fIPSServiceGroupId
    '    End Get
    '    Set(ByVal value As ConceptReportXpo)
    '        SetPropertyValue(Of ConceptReportXpo)("IPSServiceGroupId", fIPSServiceGroupId, value)
    '    End Set
    'End Property
    Dim fBillingGroupId As GroupBillingXpo
    <Association("Contract_CUPSEntityReferencesBilling_BillingGroup", GetType(GroupBillingXpo))> _
    Public Property BillingGroupId() As GroupBillingXpo
        Get
            Return fBillingGroupId
        End Get
        Set(ByVal value As GroupBillingXpo)
            SetPropertyValue(Of GroupBillingXpo)("BillingGroupId", fBillingGroupId, value)
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
    <Association("Billing_ServiceOrderDetailReferencesContract_CUPSEntity", GetType(OrderServiceDetailXpo))> _
    Public ReadOnly Property Billing_ServiceOrderDetails() As XPCollection(Of OrderServiceDetailXpo)
        Get
            Return GetCollection(Of OrderServiceDetailXpo)("Billing_ServiceOrderDetails")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
