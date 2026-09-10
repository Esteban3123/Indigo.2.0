Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportLaborCertification")> _
Public Class PayrollViewReportLaborCertificationReportXpo
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
    Dim fCompanyName As String
    <Size(50)> _
    Public Property CompanyName() As String
        Get
            Return fCompanyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CompanyName", fCompanyName, value)
        End Set
    End Property
    Dim fFirstLastName As String
    <Size(50)> _
    Public Property FirstLastName() As String
        Get
            Return fFirstLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FirstLastName", fFirstLastName, value)
        End Set
    End Property
    Dim fSecondLastName As String
    <Size(50)> _
    Public Property SecondLastName() As String
        Get
            Return fSecondLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SecondLastName", fSecondLastName, value)
        End Set
    End Property
    Dim fFirstName As String
    <Size(50)> _
    Public Property FirstName() As String
        Get
            Return fFirstName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FirstName", fFirstName, value)
        End Set
    End Property
    Dim fSecondName As String
    <Size(50)> _
    Public Property SecondName() As String
        Get
            Return fSecondName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SecondName", fSecondName, value)
        End Set
    End Property
    Dim fIdentificationNumber As String
    <Size(15)> _
    Public Property IdentificationNumber() As String
        Get
            Return fIdentificationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdentificationNumber", fIdentificationNumber, value)
        End Set
    End Property
    Dim fCityName As String
    Public Property CityName() As String
        Get
            Return fCityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CityName", fCityName, value)
        End Set
    End Property
    Dim fJobBondingDate As DateTime
    Public Property JobBondingDate() As DateTime
        Get
            Return fJobBondingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("JobBondingDate", fJobBondingDate, value)
        End Set
    End Property
    Dim fRol As String
    <Size(80)> _
    Public Property Rol() As String
        Get
            Return fRol
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Rol", fRol, value)
        End Set
    End Property
    Dim fBasicSalary As Decimal
    Public Property BasicSalary() As Decimal
        Get
            Return fBasicSalary
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BasicSalary", fBasicSalary, value)
        End Set
    End Property
    Dim fName As String
    <Size(50)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fHumanResource As String
    <Size(300)> _
    Public Property HumanResource() As String
        Get
            Return fHumanResource
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HumanResource", fHumanResource, value)
        End Set
    End Property
    Dim fProfessionHumanResource As String
    <Size(80)> _
    Public Property ProfessionHumanResource() As String
        Get
            Return fProfessionHumanResource
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProfessionHumanResource", fProfessionHumanResource, value)
        End Set
    End Property
    Dim fJobBondingType As String
    <Size(80)>
    Public Property JobBondingType() As String
        Get
            Return fJobBondingType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JobBondingType", fJobBondingType, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
