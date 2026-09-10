Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportTotalRetroactiveConcept")> _
Public Class PayrollVTotalRetroactiveConceptReportXpo
    Inherits XPLiteObject
    Dim fIdRetoractive As Integer
    <Key(True)> _
    Public Property IdRetoractive() As Integer
        Get
            Return fIdRetoractive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetoractive", fIdRetoractive, value)
        End Set
    End Property
    Dim fInitialDateRetroactive As DateTime
    Public Property InitialDateRetroactive() As DateTime
        Get
            Return fInitialDateRetroactive
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDateRetroactive", fInitialDateRetroactive, value)
        End Set
    End Property
    Dim fId As Integer
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(4)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fIdEmployee As Integer
    Public Property IdEmployee() As Integer
        Get
            Return fIdEmployee
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEmployee", fIdEmployee, value)
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
    Dim fConceptType As Byte
    Public Property ConceptType() As Byte
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptType", fConceptType, value)
        End Set
    End Property
    Dim fConceptClass As String
    <Size(3)> _
    Public Property ConceptClass() As String
        Get
            Return fConceptClass
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptClass", fConceptClass, value)
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
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
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
