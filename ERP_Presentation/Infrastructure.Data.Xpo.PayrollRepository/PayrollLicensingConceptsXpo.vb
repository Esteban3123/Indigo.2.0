'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.Payroll
' Author           : Andrés Steven Rojas  
' Created          : 28-09-2023
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.LicensingConcepts")>
Partial Public Class PayrollLicensingConceptsXpo
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

    Dim fCode As String

    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String

    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fLicensingConceptsClass As Byte
    Public Property LicensingConceptsClass() As Byte
        Get
            Return fLicensingConceptsClass
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LicensingConceptsClass", fLicensingConceptsClass, value)
        End Set
    End Property

    <PersistentAlias("Iif(LicensingConceptsClass = 1,'Remunerada',Iif(LicensingConceptsClass = 2, 'No Remunerada', 'Con Cargo Vacaciones'))")>
    Public ReadOnly Property ClassName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ClassName"))
        End Get
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
End Class
