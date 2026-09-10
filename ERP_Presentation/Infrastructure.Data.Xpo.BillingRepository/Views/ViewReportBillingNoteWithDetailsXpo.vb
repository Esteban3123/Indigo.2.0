Imports DevExpress.Xpo

<Persistent("Billing.ViewReportBillingNoteWithDetails")>
Public Class ViewReportBillingNoteWithDetailsXpo
    Inherits XPLiteObject

#Region "Properties"

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fBillingNoteId As ViewBillingNoteXpo
    <Association("ViewBillingNote_References_ViewReportBillingNoteWithDetails")>
    Public Property BillingNoteId() As ViewBillingNoteXpo
        Get
            Return fBillingNoteId
        End Get
        Set(ByVal value As ViewBillingNoteXpo)
            SetPropertyValue(Of ViewBillingNoteXpo)("BillingNoteId", fBillingNoteId, value)
        End Set
    End Property

    Dim fCode As String
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

    Dim fCodeAlternative As String
    Public Property CodeAlternative() As String
        Get
            Return fCodeAlternative
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAlternative", fCodeAlternative, value)
        End Set
    End Property

    Dim fCodeAlternativeTwo As String
    Public Property CodeAlternativeTwo() As String
        Get
            Return fCodeAlternativeTwo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAlternativeTwo", fCodeAlternativeTwo, value)
        End Set
    End Property

    Dim fNameAlternative As String
    Public Property NameAlternative() As String
        Get
            Return fNameAlternative
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameAlternative", fNameAlternative, value)
        End Set
    End Property

    Dim fNameAlternativeTwo As String
    Public Property NameAlternativeTwo() As String
        Get
            Return fNameAlternativeTwo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameAlternativeTwo", fNameAlternativeTwo, value)
        End Set
    End Property

    Dim fAccountNumberName As String
    Public Property AccountNumberName() As String
        Get
            Return fAccountNumberName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AccountNumberName", fAccountNumberName, value)
        End Set
    End Property

    Dim fCostCenterCode As String
    Public Property CostCenterCode() As String
        Get
            Return fCostCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterCode", fCostCenterCode, value)
        End Set
    End Property

    Dim fCreditValue As Decimal
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValue", fCreditValue, value)
        End Set
    End Property

    Dim fDebitValue As Decimal
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValue", fDebitValue, value)
        End Set
    End Property

    Dim fGroupDetails As String
    Public Property GroupDetails() As String
        Get
            Return fGroupDetails
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupDetails", fGroupDetails, value)
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
