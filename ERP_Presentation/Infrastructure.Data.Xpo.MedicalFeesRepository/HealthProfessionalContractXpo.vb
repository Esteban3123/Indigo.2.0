'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MedicalFeesRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/12/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("MedicalFees.HealthProfessionalContract")> _
Public Class HealthProfessionalContractXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fHealthProfessionalCode As String
    <Persistent("HealthProfessionalCode")>
    Public Property HealthProfessionalCode() As String
        Get
            Return fHealthProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthProfessionalCode", fHealthProfessionalCode, value)
        End Set
    End Property

    Dim fMedicalFeesContractId As MedicalFeesContractXpo
    <Association("HealthProfessionalContractReferencesMedicalFeesContract")> _
    Public Property MedicalFeesContractId() As MedicalFeesContractXpo
        Get
            Return fMedicalFeesContractId
        End Get
        Set(ByVal value As MedicalFeesContractXpo)
            SetPropertyValue(Of MedicalFeesContractXpo)("MedicalFeesContractId", fMedicalFeesContractId, value)
        End Set
    End Property

    Dim fLiquidateDefault As Boolean
    <Persistent("LiquidateDefault")> _
    Public Property LiquidateDefault() As Boolean
        Get
            Return fLiquidateDefault
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("LiquidateDefault", fLiquidateDefault, value)
        End Set
    End Property

    Dim fSelectOption As Boolean
    <NonPersistent()> _
    Public Property SelectOption As Boolean
        Get
            Return fSelectOption
        End Get
        Set(value As Boolean)
            fSelectOption = value
        End Set
    End Property

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
