Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("Payroll.AgreementsC")> _
Public Class PayrollAgreementsCReportXpo
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
    Dim fGroupId As PayrollGroup
    <Association("Payroll_AgreementsCReferencesPayroll_Group")> _
    Public Property GroupId() As PayrollGroup
        Get
            Return fGroupId
        End Get
        Set(ByVal value As PayrollGroup)
            SetPropertyValue(Of PayrollGroup)("GroupId", fGroupId, value)
        End Set
    End Property
    Dim fEmployeeId As PayrollEmployee
    <Association("Payroll_AgreementsCReferencesPayroll_Employee")> _
    Public Property EmployeeId() As PayrollEmployee
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As PayrollEmployee)
            SetPropertyValue(Of PayrollEmployee)("EmployeeId", fEmployeeId, value)
        End Set
    End Property
    Dim fCompanyId As PayrollCompanyReportXpo
    <Association("Payroll_AgreementsCReferencesPayroll_Company")> _
    Public Property CompanyId() As PayrollCompanyReportXpo
        Get
            Return fCompanyId
        End Get
        Set(ByVal value As PayrollCompanyReportXpo)
            SetPropertyValue(Of PayrollCompanyReportXpo)("CompanyId", fCompanyId, value)
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
    Dim fTermType As Byte
    Public Property TermType() As Byte
        Get
            Return fTermType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TermType", fTermType, value)
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
    Dim fEndDateSuspend As DateTime
    Public Property EndDateSuspend() As DateTime
        Get
            Return fEndDateSuspend
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDateSuspend", fEndDateSuspend, value)
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
    <Association("Payroll_AgreementsDReferencesPayroll_AgreementsC", GetType(PayrollAgreementsDReportXpo))> _
    Public ReadOnly Property Payroll_AgreementsDs() As XPCollection(Of PayrollAgreementsDReportXpo)
        Get
            Return GetCollection(Of PayrollAgreementsDReportXpo)("Payroll_AgreementsDs")
        End Get
    End Property
    <Association("Payroll_LiquidationDetailReferencesPayrollAgreementsCReportXpo", GetType(PayrollLiquidationDetail))> _
    Public ReadOnly Property PayrollLiquidationDetail() As XPCollection(Of PayrollLiquidationDetail)
        Get
            Return GetCollection(Of PayrollLiquidationDetail)("PayrollLiquidationDetail")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
