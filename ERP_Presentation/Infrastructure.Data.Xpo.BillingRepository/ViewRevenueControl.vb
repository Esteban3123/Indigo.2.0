'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/12/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' asociacion entre procedureCups y MarketingUnitCups usado en los servicios Xpo
''' </summary>
<Persistent("Billing.ViewRevenueControl")>
Public Class ViewRevenueControl
    Inherits XPLiteObject

#Region "Members"
    <Key(), Persistent()>
    Public Property Key As String

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fIPNOMCOMP As String
    Public Property IPNOMCOMP() As String
        Get
            Return fIPNOMCOMP
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPNOMCOMP", fIPNOMCOMP, value)
        End Set
    End Property

    Dim fIESTADOIN As String
    Public Property IESTADOIN() As String
        Get
            Return fIESTADOIN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IESTADOIN", fIESTADOIN, value)
        End Set
    End Property

    <PersistentAlias("concat('No. INGRESO: ', Trim(AdmissionNumber), ' PACIENTE: ', Trim(PatientCode), ' - ', trim(IPNOMCOMP))")>
    Public ReadOnly Property FullNameAdmission() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("FullNameAdmission"))
        End Get
    End Property

    <PersistentAlias("IIF(  IESTADOIN = '','Abierto',
                            IESTADOIN = 'C','Cerrado',
                            IESTADOIN = 'B','Bloqueado','')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
