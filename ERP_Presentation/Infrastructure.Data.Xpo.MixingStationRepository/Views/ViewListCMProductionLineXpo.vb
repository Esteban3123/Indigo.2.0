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

<Persistent("MixingStation.ViewListCMProductionLine")>
Partial Public Class ViewListCMProductionLineXpo
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

    Dim fCMConfigurationId As String
    Public Property CMConfigurationId() As String
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CMConfigurationId", fCMConfigurationId, value)
        End Set
    End Property

    Dim fCMConfigurationCodeName As String
    Public Property CMConfigurationCodeName() As String
        Get
            Return fCMConfigurationCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CMConfigurationCodeName", fCMConfigurationCodeName, value)
        End Set
    End Property

    Dim fProductionLineId As String
    Public Property ProductionLineId() As String
        Get
            Return fProductionLineId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineId", fProductionLineId, value)
        End Set
    End Property

    Dim fProductionLineCodeName As String
    Public Property ProductionLineCodeName() As String
        Get
            Return fProductionLineCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineCodeName", fProductionLineCodeName, value)
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

    Dim fCodeName As String
    Public Property CodeName() As String
        Get
            Return fCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeName", fCodeName, value)
        End Set
    End Property

End Class