Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.Vacation")> _
Public Class PayrollVacation
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
    Dim fVacationPeriodId As PayrollVacationPeriod
    <Association("Payroll_VacationReferencesPayroll_VacationPeriod")> _
    Public Property VacationPeriodId() As PayrollVacationPeriod
        Get
            Return fVacationPeriodId
        End Get
        Set(ByVal value As PayrollVacationPeriod)
            SetPropertyValue(Of PayrollVacationPeriod)("VacationPeriodId", fVacationPeriodId, value)
        End Set
    End Property
    Dim fVacationStartDate As DateTime
    Public Property VacationStartDate() As DateTime
        Get
            Return fVacationStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VacationStartDate", fVacationStartDate, value)
        End Set
    End Property
    Dim fVacationEndDate As DateTime
    Public Property VacationEndDate() As DateTime
        Get
            Return fVacationEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VacationEndDate", fVacationEndDate, value)
        End Set
    End Property
    Dim fVacationStartDateReal As DateTime
    Public Property VacationStartDateReal() As DateTime
        Get
            Return fVacationStartDateReal
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VacationStartDateReal", fVacationStartDateReal, value)
        End Set
    End Property
    Dim fVacationEndDateReal As DateTime
    Public Property VacationEndDateReal() As DateTime
        Get
            Return fVacationEndDateReal
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VacationEndDateReal", fVacationEndDateReal, value)
        End Set
    End Property
    Dim fIncorporationDateVacationReal As DateTime
    Public Property IncorporationDateVacationReal() As DateTime
        Get
            Return fIncorporationDateVacationReal
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IncorporationDateVacationReal", fIncorporationDateVacationReal, value)
        End Set
    End Property
    Dim fTypeLiquidation As Byte
    Public Property TypeLiquidation() As Byte
        Get
            Return fTypeLiquidation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeLiquidation", fTypeLiquidation, value)
        End Set
    End Property
    Dim fTypeVacation As Byte
    Public Property TypeVacation() As Byte
        Get
            Return fTypeVacation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeVacation", fTypeVacation, value)
        End Set
    End Property
    Dim fTypePayment As Byte
    Public Property TypePayment() As Byte
        Get
            Return fTypePayment
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypePayment", fTypePayment, value)
        End Set
    End Property
    Dim fLiquidationDate As DateTime
    Public Property LiquidationDate() As DateTime
        Get
            Return fLiquidationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LiquidationDate", fLiquidationDate, value)
        End Set
    End Property
    Dim fTakenDays As Byte
    Public Property TakenDays() As Byte
        Get
            Return fTakenDays
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TakenDays", fTakenDays, value)
        End Set
    End Property
    Dim fEnjoyDays As Byte
    Public Property EnjoyDays() As Byte
        Get
            Return fEnjoyDays
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("EnjoyDays", fEnjoyDays, value)
        End Set
    End Property
    Dim fIncorporationDate As DateTime
    Public Property IncorporationDate() As DateTime
        Get
            Return fIncorporationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IncorporationDate", fIncorporationDate, value)
        End Set
    End Property
    Dim fWorkedDays As Integer
    Public Property WorkedDays() As Integer
        Get
            Return fWorkedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("WorkedDays", fWorkedDays, value)
        End Set
    End Property
    Dim fNotWorkedDays As Integer
    Public Property NotWorkedDays() As Integer
        Get
            Return fNotWorkedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NotWorkedDays", fNotWorkedDays, value)
        End Set
    End Property
    Dim fBaseLiquidation As Decimal
    Public Property BaseLiquidation() As Decimal
        Get
            Return fBaseLiquidation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseLiquidation", fBaseLiquidation, value)
        End Set
    End Property
    Dim fVacationValue As Decimal
    Public Property VacationValue() As Decimal
        Get
            Return fVacationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VacationValue", fVacationValue, value)
        End Set
    End Property
    Dim fHealthContribution As Decimal
    Public Property HealthContribution() As Decimal
        Get
            Return fHealthContribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HealthContribution", fHealthContribution, value)
        End Set
    End Property
    Dim fPensionContribution As Decimal
    Public Property PensionContribution() As Decimal
        Get
            Return fPensionContribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PensionContribution", fPensionContribution, value)
        End Set
    End Property
    Dim fSolidarityFundValue As Decimal
    Public Property SolidarityFundValue() As Decimal
        Get
            Return fSolidarityFundValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SolidarityFundValue", fSolidarityFundValue, value)
        End Set
    End Property
    Dim fVacationValueNet As Decimal
    Public Property VacationValueNet() As Decimal
        Get
            Return fVacationValueNet
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VacationValueNet", fVacationValueNet, value)
        End Set
    End Property
    Dim fVacationBonificationValue As Decimal
    Public Property VacationBonificationValue() As Decimal
        Get
            Return fVacationBonificationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VacationBonificationValue", fVacationBonificationValue, value)
        End Set
    End Property
    Dim fIncentivePaymentVacationValue As Decimal
    Public Property IncentivePaymentVacationValue() As Decimal
        Get
            Return fIncentivePaymentVacationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IncentivePaymentVacationValue", fIncentivePaymentVacationValue, value)
        End Set
    End Property
    Dim fVacationalIncreaseValue As Decimal
    Public Property VacationalIncreaseValue() As Decimal
        Get
            Return fVacationalIncreaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VacationalIncreaseValue", fVacationalIncreaseValue, value)
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

    Public ReadOnly Property StateName() As String
        Get
            Select Case State
                Case 1
                    Return "Esperando Pago"
                Case 2
                    Return "Pagadas"
                Case 3
                    Return "Aplazadas"
                Case Else
                    Return ""
            End Select
        End Get
    End Property
    Dim fTakenDaysReal As Integer
    Public Property TakenDaysReal() As Integer
        Get
            Return fTakenDaysReal
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TakenDaysReal", fTakenDaysReal, value)
        End Set
    End Property
    Dim fIncorporationDateReal As DateTime
    Public Property IncorporationDateReal() As DateTime
        Get
            Return fIncorporationDateReal
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IncorporationDateReal", fIncorporationDateReal, value)
        End Set
    End Property
    Dim fResolutionNumber As String
    <Size(50)> _
    Public Property ResolutionNumber() As String
        Get
            Return fResolutionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResolutionNumber", fResolutionNumber, value)
        End Set
    End Property
    Dim fResolutionDate As DateTime
    Public Property ResolutionDate() As DateTime
        Get
            Return fResolutionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ResolutionDate", fResolutionDate, value)
        End Set
    End Property
    Dim fForceEntryResolutionNumber As String
    <Size(50)> _
    Public Property ForceEntryResolutionNumber() As String
        Get
            Return fForceEntryResolutionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ForceEntryResolutionNumber", fForceEntryResolutionNumber, value)
        End Set
    End Property
    Dim fForceEntryResolutionDate As DateTime
    Public Property ForceEntryResolutionDate() As DateTime
        Get
            Return fForceEntryResolutionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ForceEntryResolutionDate", fForceEntryResolutionDate, value)
        End Set
    End Property
    Dim fStateIncorporation As Byte
    Public Property StateIncorporation() As Byte
        Get
            Return fStateIncorporation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StateIncorporation", fStateIncorporation, value)
        End Set
    End Property
    Dim fDaysDeferredPending As Integer
    Public Property DaysDeferredPending() As Integer
        Get
            Return fDaysDeferredPending
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DaysDeferredPending", fDaysDeferredPending, value)
        End Set
    End Property

    <Association("Payroll_VacationReferencesPayroll_Vacation", GetType(PayrollVacationDetail))>
    Public ReadOnly Property Payroll_VacationsDetail() As XPCollection(Of PayrollVacationDetail)
        Get
            Return GetCollection(Of PayrollVacationDetail)("Payroll_VacationsDetail")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
