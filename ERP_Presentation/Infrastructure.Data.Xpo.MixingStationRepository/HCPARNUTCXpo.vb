'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.MixingStationRepostory
' Author           : Andres Alarcon
' Created          : 18/01/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports DevExpress.Xpo

<Persistent("HCPARNUTC")>
Partial Public Class HCPARNUTCXpo
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

    Dim fID As Integer
    <Key(True)>
    Public Property ID() As Integer
        Get
            Return fID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ID", fID, value)
        End Set
    End Property

    Dim fNAME As String
    Public Property NAME() As String
        Get
            Return fNAME
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NAME", fNAME, value)
        End Set
    End Property

    Dim fCODE As String
    Public Property CODE() As String
        Get
            Return fCODE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODE", fCODE, value)
        End Set
    End Property

    Dim fSTATUS As Integer
    Public Property STATUS() As Integer
        Get
            Return fSTATUS
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("STATUS", fSTATUS, value)
        End Set
    End Property

    Dim fFinishedProductCode As String
    Public Property FinishedProductCode() As String
        Get
            Return fFinishedProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinishedProductCode", fFinishedProductCode, value)
        End Set
    End Property

    <PersistentAlias("concat(CODE,' - ', NAME)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("RequestUnitDoseExternalCareCenterPatientReferencesHCPARNUTC", GetType(RequestUnitDoseExternalCareCenterPatientXpo))>
    Public ReadOnly Property RequestUnitDoseExternalCareCenterPatientXpo() As XPCollection(Of RequestUnitDoseExternalCareCenterPatientXpo)
        Get
            Return GetCollection(Of RequestUnitDoseExternalCareCenterPatientXpo)("RequestUnitDoseExternalCareCenterPatientXpo")
        End Get
    End Property

    <Association("PackageReferencesHCPARNUTC", GetType(MixinStationPackageXpo))>
    Public ReadOnly Property MixinStationPackageXpo() As XPCollection(Of MixinStationPackageXpo)
        Get
            Return GetCollection(Of MixinStationPackageXpo)("MixinStationPackageXpo")
        End Get
    End Property
End Class
