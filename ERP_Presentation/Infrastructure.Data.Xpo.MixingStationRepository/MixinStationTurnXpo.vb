'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 16-04-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.Turn")>
Partial Public Class MixinStationTurnXpo
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
    <Size(20)>
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    <Size(100)>
    <Persistent("Name")>
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
    Dim fWeekFraction As Integer
    <Persistent("WeekFraction")>
    Public Property WeekFraction() As Integer
        Get
            Return fWeekFraction
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("WeekFraction", fWeekFraction, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(100)>
    <Persistent("Description")>
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fInitialTimeWork As DateTime
    <Persistent("InitialTimeWork")>
    Public Property InitialTimeWork() As DateTime
        Get
            Return fInitialTimeWork
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialTimeWork", fInitialTimeWork, value)
        End Set
    End Property
    Dim fEndingTimeWork As DateTime
    <Persistent("EndingTimeWork")>
    Public Property EndingTimeWork() As DateTime
        Get
            Return fEndingTimeWork
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndingTimeWork", fEndingTimeWork, value)
        End Set
    End Property
    Dim fInitialTimeDelivery As DateTime
    <Persistent("InitialTimeDelivery")>
    Public Property InitialTimeDelivery() As DateTime
        Get
            Return fInitialTimeDelivery
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialTimeDelivery", fInitialTimeDelivery, value)
        End Set
    End Property
    Dim fEndingTimeDelivery As DateTime
    <Persistent("EndingTimeDelivery")>
    Public Property EndingTimeDelivery() As DateTime
        Get
            Return fEndingTimeDelivery
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndingTimeDelivery", fEndingTimeDelivery, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)>
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)>
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

#Region "Property Hour and Minutes"
    ''' <summary>
    ''' Extrae horas y minutos de InitialTimeWork
    ''' </summary>
    Public ReadOnly Property InitialTimeWorkHM() As String
        Get
            Return InitialTimeWork.ToShortTimeString()
        End Get
    End Property

    ''' <summary>
    ''' Extrae horas y minutos de EndingTimeWork
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EndingTimeWorkHM() As String
        Get
            Return EndingTimeWork.ToShortTimeString()
        End Get
    End Property

    ''' <summary>
    ''' Extrae horas y minutos de InitialTimeDelivery
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InitialTimeDeliveryHM() As String
        Get
            Return InitialTimeDelivery.ToShortTimeString()
        End Get
    End Property

    ''' <summary>
    ''' Extrae horas y minutos de EndingTimeDelivery
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EndingTimeDeliveryHM() As String
        Get
            Return EndingTimeDelivery.ToShortTimeString()
        End Get
    End Property
#End Region
End Class