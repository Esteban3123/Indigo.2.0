'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.Payroll
' Author           : Juan Pablo Daza Medina 
' Created          : 08-11-2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
<Persistent("Payroll.MinimumSalary")>
Public Class PayrollMinimumSalaryXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fYear As String
    '<Size(4)>
    '<Persistent("Year")>
    Public Property Year() As Integer
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Year", fYear, value)
        End Set
    End Property
    Dim fLegalMinimumSalary As Integer
    '<Size(50)>
    '<Persistent("MinimumSalary")>
    Public Property LegalMinimumSalary() As Integer
        Get
            Return fLegalMinimumSalary
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LegalMinimumSalary", fLegalMinimumSalary, value)
        End Set
    End Property
    Dim fTransportHelpValue As Integer
    Public Property TransportHelpValue() As Integer
        Get
            Return fTransportHelpValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TransportHelpValue", fTransportHelpValue, value)
        End Set
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    <PersistentAlias("concat(Year, ' - ', LegalMinimumSalary)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property
End Class
