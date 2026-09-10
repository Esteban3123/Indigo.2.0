'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldan
' Created          : 2022-02-24
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.PackagePersonalized")>
Partial Public Class MixinStationPackagePersonalizedXpo
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

    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    <PersistentAlias("iif(State=True, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StateName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
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

    <PersistentAlias("concat(Code,' - ', Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fATCId As Integer?
    Public Property ATCId() As Integer?
        Get
            Return fATCId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ATCId", fATCId, value)
        End Set
    End Property

    Dim fConcentration As Decimal
    Public Property Concentration() As Decimal
        Get
            Return fConcentration
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Concentration", fConcentration, value)
        End Set
    End Property

    Dim fRefrigeratedTerm As Integer
    Public Property RefrigeratedTerm() As Integer
        Get
            Return fRefrigeratedTerm
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RefrigeratedTerm", fRefrigeratedTerm, value)
        End Set
    End Property

    Dim fEnvironmentalTemperatureTerm As Integer
    Public Property EnvironmentalTemperatureTerm() As Integer
        Get
            Return fEnvironmentalTemperatureTerm
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EnvironmentalTemperatureTerm", fEnvironmentalTemperatureTerm, value)
        End Set
    End Property
End Class