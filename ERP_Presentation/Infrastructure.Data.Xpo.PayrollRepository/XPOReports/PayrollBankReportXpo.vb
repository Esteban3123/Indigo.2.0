Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.Bank")> _
 Public Class PayrollBankReportXpo
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
    Dim fCode As String
    <Indexed(Name:="IX_Corporation", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fName As String
    <Size(320)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fAchCode As String
    <Size(10)> _
    Public Property AchCode() As String
        Get
            Return fAchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AchCode", fAchCode, value)
        End Set
    End Property
    Dim fCenitCode As String
    <Size(4)> _
    Public Property CenitCode() As String
        Get
            Return fCenitCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenitCode", fCenitCode, value)
        End Set
    End Property
    Dim fCenitVerification As String
    <Size(1)> _
    Public Property CenitVerification() As String
        Get
            Return fCenitVerification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenitVerification", fCenitVerification, value)
        End Set
    End Property
    Dim fBankFileCode As String
    <Size(3)> _
    Public Property BankFileCode() As String
        Get
            Return fBankFileCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankFileCode", fBankFileCode, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
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
    <Association("Payroll_LiquidationReferencesPayroll_Bank", GetType(PayrollLiquidation))> _
    Public ReadOnly Property PayrollLiquidation() As XPCollection(Of PayrollLiquidation)
        Get
            Return GetCollection(Of PayrollLiquidation)("PayrollLiquidation")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
