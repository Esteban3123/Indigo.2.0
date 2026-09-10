'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/11/2020
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListCM")>
Partial Public Class ViewListCMXpo
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

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fCMCode As String
    Public Property CMCode() As String
        Get
            Return fCMCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CMCode", fCMCode, value)
        End Set
    End Property

    Dim fCMName As String
    Public Property CMName() As String
        Get
            Return fCMName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CMName", fCMName, value)
        End Set
    End Property

    Dim fCMCodeName As String
    Public Property CMCodeName() As String
        Get
            Return fCMCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CMCodeName", fCMCodeName, value)
        End Set
    End Property

    Dim fCareCenterCode As String
    Public Property CareCenterCode() As String
        Get
            Return fCareCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCode", fCareCenterCode, value)
        End Set
    End Property

    Dim fCareCenterName As String
    Public Property CareCenterName() As String
        Get
            Return fCareCenterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterName", fCareCenterName, value)
        End Set
    End Property

    Dim fCareCenterCodeName As String
    Public Property CareCenterCodeName() As String
        Get
            Return fCareCenterCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCodeName", fCareCenterCodeName, value)
        End Set
    End Property

    Dim fUserCode As String
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fDeviatePharmacotherapeuticProfile As Boolean
    Public Property DeviatePharmacotherapeuticProfile() As Boolean
        Get
            Return fDeviatePharmacotherapeuticProfile
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("DeviatePharmacotherapeuticProfile", fDeviatePharmacotherapeuticProfile, value)
        End Set
    End Property

    Dim fCMConfigurationId As Integer
    Public Property CMConfigurationId() As Integer
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CMConfigurationId", fCMConfigurationId, value)
        End Set
    End Property

End Class