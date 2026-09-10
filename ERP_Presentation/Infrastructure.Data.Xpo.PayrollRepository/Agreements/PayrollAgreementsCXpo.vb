Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering


<Persistent("Payroll.AgreementsC")> _
Public Class PayrollAgreementsCXpo
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
    Dim fConsecutive As Integer
    Public Property Consecutive() As Integer
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Consecutive", fConsecutive, value)
        End Set
    End Property
    Dim fGroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return fGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupId", fGroupId, value)
        End Set
    End Property
    Dim fEmployeeId As PayrollEmployeeXpo
    <Association("PayrollAgreementsCReferencesPayrollEmployee")> _
    Public Property EmployeeId() As PayrollEmployeeXpo
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As PayrollEmployeeXpo)
            SetPropertyValue(Of PayrollEmployeeXpo)("EmployeeId", fEmployeeId, value)
        End Set
    End Property
    Dim fCompanyId As PayrollCompanyXpo
    <Association("PayrollAgreementsCReferencesPayrollCompany")> _
    Public Property CompanyId() As PayrollCompanyXpo
        Get
            Return fCompanyId
        End Get
        Set(ByVal value As PayrollCompanyXpo)
            SetPropertyValue(Of PayrollCompanyXpo)("CompanyId", fCompanyId, value)
        End Set
    End Property
    Dim fConceptId As Integer
    Public Property ConceptId() As Integer
        Get
            Return fConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConceptId", fConceptId, value)
        End Set
    End Property
    Dim fKindsAgreementsId As Integer
    Public Property KindsAgreementsId() As Integer
        Get
            Return fKindsAgreementsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("KindsAgreementsId", fKindsAgreementsId, value)
        End Set
    End Property
    Dim fComments As String
    <Size(250)> _
    Public Property Comments() As String
        Get
            Return fComments
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comments", fComments, value)
        End Set
    End Property
    Dim fLiquidationType As Byte
    Public Property LiquidationType() As Byte
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LiquidationType", fLiquidationType, value)
        End Set
    End Property
    Dim fTermtype As Byte
    Public Property Termtype() As Byte
        Get
            Return fTermtype
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Termtype", fTermtype, value)
        End Set
    End Property
    Dim fAgreementValue As Decimal
    Public Property AgreementValue() As Decimal
        Get
            Return fAgreementValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AgreementValue", fAgreementValue, value)
        End Set
    End Property
    Dim fNumberShares As Integer
    Public Property NumberShares() As Integer
        Get
            Return fNumberShares
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NumberShares", fNumberShares, value)
        End Set
    End Property
    Dim fState As String
    <Size(1)> _
    Public Property State() As String
        Get
            Return fState
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("State", fState, value)
        End Set
    End Property
    Dim fStartingDate As DateTime
    Public Property StartingDate() As DateTime
        Get
            Return fStartingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("StartingDate", fStartingDate, value)
        End Set
    End Property
    Dim fCurrentBalance As Decimal
    Public Property CurrentBalance() As Decimal
        Get
            Return fCurrentBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CurrentBalance", fCurrentBalance, value)
        End Set
    End Property
    Dim fCommentChangeState As String
    <Size(250)> _
    Public Property CommentChangeState() As String
        Get
            Return fCommentChangeState
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CommentChangeState", fCommentChangeState, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class

