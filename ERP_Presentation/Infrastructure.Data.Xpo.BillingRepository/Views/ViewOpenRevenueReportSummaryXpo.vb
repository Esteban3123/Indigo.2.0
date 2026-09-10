Imports DevExpress.Xpo

<Persistent("Billing.ViewOpenRevenueReportSummary")>
Public Class ViewOpenRevenueReportSummaryXpo
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

    Dim fCareGroupId As Integer
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fCareGroup As String
    Public Property CareGroup() As String
        Get
            Return fCareGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroup", fCareGroup, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntity As String
    Public Property Entity() As String
        Get
            Return fEntity
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Entity", fEntity, value)
        End Set
    End Property

    Dim fCodCareCenter As String
    Public Property CodCareCenter() As String
        Get
            Return fCodCareCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodCareCenter", fCodCareCenter, value)
        End Set
    End Property

    Dim fCareCenter As String
    Public Property CareCenter() As String
        Get
            Return fCareCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenter", fCareCenter, value)
        End Set
    End Property

    Dim fCodCreationUser As String
    Public Property CodCreationUser() As String
        Get
            Return fCodCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodCreationUser", fCodCreationUser, value)
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

    Dim fConceptStatusFolio As String
    Public Property ConceptStatusFolio() As String
        Get
            Return fConceptStatusFolio
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptStatusFolio", fConceptStatusFolio, value)
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

    Dim fDateIncome As DateTime
    Public Property DateIncome() As DateTime
        Get
            Return fDateIncome
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateIncome", fDateIncome, value)
        End Set
    End Property

    Dim fQuantityFolios As String
    Public Property QuantityFolios() As String
        Get
            Return fQuantityFolios
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QuantityFolios", fQuantityFolios, value)
        End Set
    End Property

    Dim fIncome As String
    Public Property Income() As String
        Get
            Return fIncome
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Income", fIncome, value)
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