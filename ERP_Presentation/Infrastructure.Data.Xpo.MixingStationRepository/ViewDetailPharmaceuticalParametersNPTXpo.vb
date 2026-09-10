'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.MixingStationRepostory
' Author           : Andres Alarcon
' Created          : 07/11/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewDetailPharmaceuticalParametersNPT")>
Partial Public Class ViewDetailPharmaceuticalParametersNPTXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("Id", fId, value)
        End Set
    End Property

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fBed As String
    Public Property Bed() As String
        Get
            Return fBed
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Bed", fBed, value)
        End Set
    End Property

    Dim fFunctionalUnitCodeName As String
    Public Property FunctionalUnitCodeName() As String
        Get
            Return fFunctionalUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("FunctionalUnitCodeName", fFunctionalUnitCodeName, value)
        End Set
    End Property

    Dim fPatientWeight As Integer
    Public Property PatientWeight() As Integer
        Get
            Return fPatientWeight
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("PatientWeight", fPatientWeight, value)
        End Set
    End Property

    Dim fAdministrationRoute As String
    Public Property AdministrationRoute() As String
        Get
            Return fAdministrationRoute
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AdministrationRoute", fAdministrationRoute, value)
        End Set
    End Property

    Dim fAdministrationRouteDescription As String
    Public Property AdministrationRouteDescription() As String
        Get
            Return fAdministrationRouteDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AdministrationRouteDescription", fAdministrationRouteDescription, value)
        End Set
    End Property

    Dim fInfusionTime As Integer
    Public Property InfusionTime() As Integer
        Get
            Return fInfusionTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("InfusionTime", fInfusionTime, value)
        End Set
    End Property

    Dim fTotalVolume As Decimal
    Public Property TotalVolume() As Decimal
        Get
            Return fTotalVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("TotalVolume", fTotalVolume, value)
        End Set
    End Property

    Dim fInfusionVelocity As Decimal
    Public Property InfusionVelocity() As Decimal
        Get
            Return fInfusionVelocity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("InfusionVelocity", fInfusionVelocity, value)
        End Set
    End Property

    Dim fParameterName As String
    Public Property ParameterName() As String
        Get
            Return fParameterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ParameterName", fParameterName, value)
        End Set
    End Property

    Dim fResult As Decimal
    Public Property Result() As Decimal
        Get
            Return fResult
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("Result", fResult, value)
        End Set
    End Property

    Dim fColor As String
    Public Property Color() As String
        Get
            Return fColor
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Color", fColor, value)
        End Set
    End Property

    Dim fGroupingCodeDose As String
    Public Property GroupingCodeDose() As String
        Get
            Return fGroupingCodeDose
        End Get
        Set(ByVal value As String)
            SetPropertyValue("GroupingCodeDose", fGroupingCodeDose, value)
        End Set
    End Property

End Class
