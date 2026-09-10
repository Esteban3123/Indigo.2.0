'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.MixingStationRepostory
' Author           : Andres Alarcon
' Created          : 18/02/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewListValidationWeightNPT")>
Partial Public Class ViewListValidationWeightNPTXpo
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

    Dim fRequestPackageDetailStatusId As Integer
    <Key(True)>
    Public Property RequestPackageDetailStatusId() As Integer
        Get
            Return fRequestPackageDetailStatusId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("RequestPackageDetailStatusId", fRequestPackageDetailStatusId, value)
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

    Dim fTheoreticalWeight As Decimal
    Public Property TheoreticalWeight() As Decimal
        Get
            Return fTheoreticalWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("TheoreticalWeight", fTheoreticalWeight, value)
        End Set
    End Property

    Dim fInputsWeight As Decimal
    Public Property InputsWeight() As Decimal
        Get
            Return fInputsWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("InputsWeight", fInputsWeight, value)
        End Set
    End Property

    Dim fTheoreticalAndInputsWeight As Decimal
    Public Property TheoreticalAndInputsWeight() As Decimal
        Get
            Return fTheoreticalAndInputsWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("TheoreticalAndInputsWeight", fTheoreticalAndInputsWeight, value)
        End Set
    End Property

    Dim fMinimunWeight As Decimal
    Public Property MinimunWeight() As Decimal
        Get
            Return fMinimunWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("MinimunWeight", fMinimunWeight, value)
        End Set
    End Property

    Dim fMaximunWeight As Decimal
    Public Property MaximunWeight() As Decimal
        Get
            Return fMaximunWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("MaximunWeight", fMaximunWeight, value)
        End Set
    End Property

End Class
