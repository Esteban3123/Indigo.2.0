Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ManualConcepts")> _
Public Class PayrollManualConceptsReportXpo
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
    <Association("Payroll_ManualConceptsReferencesPayroll_Group")> _
    Public Property GroupId() As PayrollGroup
        Get
            Return fGroupId
        End Get
        Set(ByVal value As PayrollGroup)
            SetPropertyValue(Of PayrollGroup)("GroupId", fGroupId, value)
        End Set
    End Property
    Dim fEmployeeId As PayrollEmployee
    <Association("Payroll_ManualConceptsReferencesPayroll_Employee")> _
    Public Property EmployeeId() As PayrollEmployee
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As PayrollEmployee)
            SetPropertyValue(Of PayrollEmployee)("EmployeeId", fEmployeeId, value)
        End Set
    End Property
    Dim fContractNumber As Integer
    Public Property ContractNumber() As Integer
        Get
            Return fContractNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractNumber", fContractNumber, value)
        End Set
    End Property
    Dim fContractId As Integer
    Public Property ContractId() As Integer
        Get
            Return fContractId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractId", fContractId, value)
        End Set
    End Property
    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Integer
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property
    Dim fFunctionalUnitId As Integer
    Public Property FunctionalUnitId() As Integer
        Get
            Return fFunctionalUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FunctionalUnitId", fFunctionalUnitId, value)
        End Set
    End Property
    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property
    Dim fPayrollInitialDate As DateTime
    Public Property PayrollInitialDate() As DateTime
        Get
            Return fPayrollInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PayrollInitialDate", fPayrollInitialDate, value)
        End Set
    End Property
    Dim fPayrollEndingDate As DateTime
    Public Property PayrollEndingDate() As DateTime
        Get
            Return fPayrollEndingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PayrollEndingDate", fPayrollEndingDate, value)
        End Set
    End Property
    Dim fConceptId As PayrollConcept
    <Association("Payroll_ManualConceptsReferencesPayroll_Concept")> _
    Public Property ConceptId() As PayrollConcept
        Get
            Return fConceptId
        End Get
        Set(ByVal value As PayrollConcept)
            SetPropertyValue(Of PayrollConcept)("ConceptId", fConceptId, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(200)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fPaidEndContract As Boolean
    Public Property PaidEndContract() As Boolean
        Get
            Return fPaidEndContract
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PaidEndContract", fPaidEndContract, value)
        End Set
    End Property
    Dim fPaidFormat As Byte
    Public Property PaidFormat() As Byte
        Get
            Return fPaidFormat
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PaidFormat", fPaidFormat, value)
        End Set
    End Property
    Dim fQuoteNumber As Byte
    Public Property QuoteNumber() As Byte
        Get
            Return fQuoteNumber
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("QuoteNumber", fQuoteNumber, value)
        End Set
    End Property
    Dim fQuoteValue As Decimal
    Public Property QuoteValue() As Decimal
        Get
            Return fQuoteValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("QuoteValue", fQuoteValue, value)
        End Set
    End Property
    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
        End Set
    End Property
    <Association("Payroll_ManualConceptsDetailReferencesPayroll_ManualConcepts", GetType(PayrollManualConceptsDetailReportXpo))> _
    Public ReadOnly Property Payroll_ManualConceptsDetails() As XPCollection(Of PayrollManualConceptsDetailReportXpo)
        Get
            Return GetCollection(Of PayrollManualConceptsDetailReportXpo)("Payroll_ManualConceptsDetails")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
