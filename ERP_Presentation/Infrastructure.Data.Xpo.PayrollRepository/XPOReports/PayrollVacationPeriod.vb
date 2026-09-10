Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.VacationPeriod")> _
Public Class PayrollVacationPeriod
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
    Dim fEmployeeId As PayrollEmployee
    <Association("Payroll_VacationPeriodReferencesPayroll_Employee")> _
    Public Property EmployeeId() As PayrollEmployee
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As PayrollEmployee)
            SetPropertyValue(Of PayrollEmployee)("EmployeeId", fEmployeeId, value)
        End Set
    End Property
    Dim fContractId As PayrollContract
    <Association("Payroll_VacationPeriodReferencesPayroll_Contract")> _
    Public Property ContractId() As PayrollContract
        Get
            Return fContractId
        End Get
        Set(ByVal value As PayrollContract)
            SetPropertyValue(Of PayrollContract)("ContractId", fContractId, value)
        End Set
    End Property
    Dim fInitialDatePeriod As DateTime
    Public Property InitialDatePeriod() As DateTime
        Get
            Return fInitialDatePeriod
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDatePeriod", fInitialDatePeriod, value)
        End Set
    End Property
    Dim fEndDatePeriod As DateTime
    Public Property EndDatePeriod() As DateTime
        Get
            Return fEndDatePeriod
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDatePeriod", fEndDatePeriod, value)
        End Set
    End Property
    Dim fVacationDays As Byte
    Public Property VacationDays() As Byte
        Get
            Return fVacationDays
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("VacationDays", fVacationDays, value)
        End Set
    End Property
    Dim fPendingDays As Byte
    Public Property PendingDays() As Byte
        Get
            Return fPendingDays
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PendingDays", fPendingDays, value)
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
    <Association("Payroll_VacationReferencesPayroll_VacationPeriod", GetType(PayrollVacation))> _
    Public ReadOnly Property Payroll_Vacations() As XPCollection(Of PayrollVacation)
        Get
            Return GetCollection(Of PayrollVacation)("Payroll_Vacations")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
