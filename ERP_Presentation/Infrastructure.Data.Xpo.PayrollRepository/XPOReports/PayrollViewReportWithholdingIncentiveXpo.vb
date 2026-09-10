Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportWithholdingIncentive")> _
Public Class PayrollViewReportWithholdingIncentiveXpo
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
    Dim fNit As String
    <Size(20)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fThirdName As String
    <Size(300)> _
    Public Property ThirdName() As String
        Get
            Return fThirdName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdName", fThirdName, value)
        End Set
    End Property
    Dim fConceptType As Byte
    Public Property ConceptType() As Byte
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptType", fConceptType, value)
        End Set
    End Property
    Dim fPeriodInitialDate As DateTime
    Public Property PeriodInitialDate() As DateTime
        Get
            Return fPeriodInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PeriodInitialDate", fPeriodInitialDate, value)
        End Set
    End Property
    Dim fPeriodEndDate As DateTime
    Public Property PeriodEndDate() As DateTime
        Get
            Return fPeriodEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PeriodEndDate", fPeriodEndDate, value)
        End Set
    End Property
    Dim fEmployeeTypeCode As String
    <Size(20)> _
    Public Property EmployeeTypeCode() As String
        Get
            Return fEmployeeTypeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EmployeeTypeCode", fEmployeeTypeCode, value)
        End Set
    End Property
    Dim fPeriod As Char
    Public Property Period() As Char
        Get
            Return fPeriod
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Period", fPeriod, value)
        End Set
    End Property
    Dim fAccruedValue As Decimal
    Public Property AccruedValue() As Decimal
        Get
            Return fAccruedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AccruedValue", fAccruedValue, value)
        End Set
    End Property
    Dim fDeductedValue As Decimal
    Public Property DeductedValue() As Decimal
        Get
            Return fDeductedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeductedValue", fDeductedValue, value)
        End Set
    End Property
    Dim fEmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeId", fEmployeeId, value)
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
    Dim fGroupCode As String
    <Size(20)> _
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property
    Dim fGroupName As String
    <Size(150)> _
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
        End Set
    End Property
    Dim fProcedureTypeRTF As Byte
    Public Property ProcedureTypeRTF() As Byte
        Get
            Return fProcedureTypeRTF
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ProcedureTypeRTF", fProcedureTypeRTF, value)
        End Set
    End Property
    Dim fDeclarantType As Byte
    Public Property DeclarantType() As Byte
        Get
            Return fDeclarantType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DeclarantType", fDeclarantType, value)
        End Set
    End Property
    Dim fTypeArticleRTF As Byte
    Public Property TypeArticleRTF() As Byte
        Get
            Return fTypeArticleRTF
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeArticleRTF", fTypeArticleRTF, value)
        End Set
    End Property
    Dim fRetentionBase As Decimal
    Public Property RetentionBase() As Decimal
        Get
            Return fRetentionBase
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionBase", fRetentionBase, value)
        End Set
    End Property
    Dim fRetentionValue As Decimal
    Public Property RetentionValue() As Decimal
        Get
            Return fRetentionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionValue", fRetentionValue, value)
        End Set
    End Property

    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Long
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
