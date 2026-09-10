Imports DevExpress.Xpo

<Persistent("Payroll.ViewAgreementsC")>
Public Class PayrollViewAgreementsCReportXpo
    Inherits XPLiteObject

#Region "Properties"

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

    Dim fConsecutive As Integer
    Public Property Consecutive() As Integer
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Consecutive", fConsecutive, value)
        End Set
    End Property

    Dim fGroupId As Integer?
    Public Property GroupId() As Integer?
        Get
            Return fGroupId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("GroupId", fGroupId, value)
        End Set
    End Property

    Dim fGroupCode As String
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property

    Dim fGroupName As String
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
        End Set
    End Property

    Dim fEmployeeId As PayrollEmployee
    Public Property EmployeeId() As PayrollEmployee
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As PayrollEmployee)
            SetPropertyValue(Of PayrollEmployee)("EmployeeId", fEmployeeId, value)
        End Set
    End Property

    Dim fCompanyId As Integer
    Public Property CompanyId() As Integer
        Get
            Return fCompanyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CompanyId", fCompanyId, value)
        End Set
    End Property

    Dim fCompanyNit As String
    Public Property CompanyNit() As String
        Get
            Return fCompanyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CompanyNit", fCompanyNit, value)
        End Set
    End Property

    Dim fCompanyName As String
    Public Property CompanyName() As String
        Get
            Return fCompanyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CompanyName", fCompanyName, value)
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

    Dim fConceptCode As String
    Public Property ConceptCode() As String
        Get
            Return fConceptCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptCode", fConceptCode, value)
        End Set
    End Property

    Dim fConceptName As String
    Public Property ConceptName() As String
        Get
            Return fConceptName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptName", fConceptName, value)
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
    <Size(250)>
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
    <Size(1)>
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
    <Size(250)>
    Public Property CommentChangeState() As String
        Get
            Return fCommentChangeState
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CommentChangeState", fCommentChangeState, value)
        End Set
    End Property

    Dim fBranchOfficeID As Integer
    Public Property BranchOfficeID() As Integer
        Get
            Return fBranchOfficeID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BranchOfficeID", fBranchOfficeID, value)
        End Set
    End Property

    Dim fBranchOfficeCode As String
    Public Property BranchOfficeCode() As String
        Get
            Return fBranchOfficeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BranchOfficeCode", fBranchOfficeCode, value)
        End Set
    End Property

    Dim fBranchOffice As String
    Public Property BranchOffice() As String
        Get
            Return fBranchOffice
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BranchOffice", fBranchOffice, value)
        End Set
    End Property

#End Region

#Region "Custom Properties"

    <PersistentAlias("Concat(GroupCode, ' - ', GroupName)")>
    Public ReadOnly Property GroupCodeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("GroupCodeName"))
        End Get
    End Property

    <PersistentAlias("Concat(CompanyNit, ' - ', CompanyName)")>
    Public ReadOnly Property CompanyNitName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CompanyNitName"))
        End Get
    End Property

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
