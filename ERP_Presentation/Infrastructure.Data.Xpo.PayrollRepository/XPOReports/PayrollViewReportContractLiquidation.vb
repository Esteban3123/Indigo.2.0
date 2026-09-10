Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel


<Persistent("Payroll.ViewReportContractLiquidation")> _
Public Class PayrollViewReportContractLiquidation
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
    Dim fcontractLiquidationId As Integer
    Public Property contractLiquidationId() As Integer
        Get
            Return fcontractLiquidationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("contractLiquidationId", fcontractLiquidationId, value)
        End Set
    End Property
    Dim femployeeCostCenterName As String
    <Size(200)>
    Public Property employeeCostCenterName() As String
        Get
            Return femployeeCostCenterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("employeeCostCenterName", femployeeCostCenterName, value)
        End Set
    End Property
    Dim femployeeName As String
    <Size(203)>
    Public Property employeeName() As String
        Get
            Return femployeeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("employeeName", femployeeName, value)
        End Set
    End Property
    Dim femployeeIdentificationNumber As String
    <Size(20)>
    Public Property employeeIdentificationNumber() As String
        Get
            Return femployeeIdentificationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("employeeIdentificationNumber", femployeeIdentificationNumber, value)
        End Set
    End Property
    Dim femployeePosition As String
    <Size(80)>
    Public Property employeePosition() As String
        Get
            Return femployeePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("employeePosition", femployeePosition, value)
        End Set
    End Property
    Dim femployeeRetirementReason As String
    <Size(100)>
    Public Property employeeRetirementReason() As String
        Get
            Return femployeeRetirementReason
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("employeeRetirementReason", femployeeRetirementReason, value)
        End Set
    End Property
    Dim fcontractInitialDate As Date
    Public Property contractInitialDate() As Date
        Get
            Return fcontractInitialDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("contractInitialDate", fcontractInitialDate, value)
        End Set
    End Property
    Dim fcontractEndingDate As Date
    Public Property contractEndingDate() As Date
        Get
            Return fcontractEndingDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("contractEndingDate", fcontractEndingDate, value)
        End Set
    End Property
    Dim fworkedDays As Integer
    Public Property workedDays() As Integer
        Get
            Return fworkedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("workedDays", fworkedDays, value)
        End Set
    End Property
    Dim finitialBenefitsLiquidation As Date
    Public Property initialBenefitsLiquidation() As Date
        Get
            Return finitialBenefitsLiquidation
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("initialBenefitsLiquidation", finitialBenefitsLiquidation, value)
        End Set
    End Property
    Dim fbasicSalary As Decimal
    Public Property basicSalary() As Decimal
        Get
            Return fbasicSalary
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("basicSalary", fbasicSalary, value)
        End Set
    End Property
    Dim fincentiveLiquidationDate As Date
    Public Property incentiveLiquidationDate() As Date
        Get
            Return fincentiveLiquidationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("incentiveLiquidationDate", fincentiveLiquidationDate, value)
        End Set
    End Property
    Dim fincentiveCourtDate As Date
    Public Property incentiveCourtDate() As Date
        Get
            Return fincentiveCourtDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("incentiveCourtDate", fincentiveCourtDate, value)
        End Set
    End Property
    Dim fincentiveDays As Integer
    Public Property incentiveDays() As Integer
        Get
            Return fincentiveDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("incentiveDays", fincentiveDays, value)
        End Set
    End Property
    Dim fchristmasLiquidationDate As Date
    Public Property christmasLiquidationDate() As Date
        Get
            Return fchristmasLiquidationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("christmasLiquidationDate", fchristmasLiquidationDate, value)
        End Set
    End Property
    Dim fchristmasCourtDate As Date
    Public Property christmasCourtDate() As Date
        Get
            Return fchristmasCourtDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("christmasCourtDate", fchristmasCourtDate, value)
        End Set
    End Property
    Dim fchristmasDays As Integer
    Public Property christmasDays() As Integer
        Get
            Return fchristmasDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("christmasDays", fchristmasDays, value)
        End Set
    End Property
    Dim fvacationTakenDays As Byte
    Public Property vacationTakenDays() As Byte
        Get
            Return fvacationTakenDays
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("vacationTakenDays", fvacationTakenDays, value)
        End Set
    End Property
    Dim fvacationPendingDays As Integer
    Public Property vacationPendingDays() As Integer
        Get
            Return fvacationPendingDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("vacationPendingDays", fvacationPendingDays, value)
        End Set
    End Property
    Dim funemployedInitialDate As Date
    Public Property unemployedInitialDate() As Date
        Get
            Return funemployedInitialDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("unemployedInitialDate", funemployedInitialDate, value)
        End Set
    End Property
    Dim funemployedEndingDate As Date
    Public Property unemployedEndingDate() As Date
        Get
            Return funemployedEndingDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("unemployedEndingDate", funemployedEndingDate, value)
        End Set
    End Property
    Dim funemployedDays As Integer
    Public Property unemployedDays() As Integer
        Get
            Return funemployedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("unemployedDays", funemployedDays, value)
        End Set
    End Property
    Dim funemployedInterestInitialDate As Date
    Public Property unemployedInterestInitialDate() As Date
        Get
            Return funemployedInterestInitialDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("unemployedInterestInitialDate", funemployedInterestInitialDate, value)
        End Set
    End Property
    Dim funemployedInterestEndingDate As Date
    Public Property unemployedInterestEndingDate() As Date
        Get
            Return funemployedInterestEndingDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("unemployedInterestEndingDate", funemployedInterestEndingDate, value)
        End Set
    End Property
    Dim funemployedInterestDays As Integer
    Public Property unemployedInterestDays() As Integer
        Get
            Return funemployedInterestDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("unemployedInterestDays", funemployedInterestDays, value)
        End Set
    End Property
    Dim fsumaryVacationAccured As Byte
    Public Property sumaryVacationAccured() As Byte
        Get
            Return fsumaryVacationAccured
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("sumaryVacationAccured", fsumaryVacationAccured, value)
        End Set
    End Property
    Dim fsumaryAccured As Decimal
    Public Property sumaryAccured() As Decimal
        Get
            Return fsumaryAccured
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("sumaryAccured", fsumaryAccured, value)
        End Set
    End Property
    Dim fsumaryDeducted As Decimal
    Public Property sumaryDeducted() As Decimal
        Get
            Return fsumaryDeducted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("sumaryDeducted", fsumaryDeducted, value)
        End Set
    End Property
    Dim fsumaryDescription As String
    <Size(500)>
    Public Property sumaryDescription() As String
        Get
            Return fsumaryDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("sumaryDescription", fsumaryDescription, value)
        End Set
    End Property
    Dim ftotalPaidLiquidation As Decimal
    Public Property totalPaidLiquidation() As Decimal
        Get
            Return ftotalPaidLiquidation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("totalPaidLiquidation", ftotalPaidLiquidation, value)
        End Set
    End Property
    Dim ftotalAccured As Decimal
    Public Property totalAccured() As Decimal
        Get
            Return ftotalAccured
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("totalAccured", ftotalAccured, value)
        End Set
    End Property
    Dim ftotalDeducted As Decimal
    Public Property totalDeducted() As Decimal
        Get
            Return ftotalDeducted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("totalDeducted", ftotalDeducted, value)
        End Set
    End Property
    Dim ftransportHelpValue As Decimal
    Public Property transportHelpValue() As Decimal
        Get
            Return ftransportHelpValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("transportHelpValue", ftransportHelpValue, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
