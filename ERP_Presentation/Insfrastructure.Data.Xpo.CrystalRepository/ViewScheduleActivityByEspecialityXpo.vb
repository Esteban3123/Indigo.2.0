
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ViewScheduleActivityByEspeciality")>
Partial Public Class ViewScheduleActivityByEspecialityXpo
    Inherits XPLiteObject

    Dim fActivityCode As String
    <Key()>
    Public Property ActivityCode() As String
        Get
            Return fActivityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ActivityCode", fActivityCode, value)
        End Set
    End Property

    Dim fActivityDescription As String
    Public Property ActivityDescription() As String
        Get
            Return fActivityDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ActivityDescription", fActivityDescription, value)
        End Set
    End Property

    Dim fActivities As String
    Public Property Activities() As String
        Get
            Return fActivities
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Activities", fActivities, value)
        End Set
    End Property

    Dim fActivityType As String
    Public Property ActivityType() As String
        Get
            Return fActivityType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ActivityType", fActivityType, value)
        End Set
    End Property

    Dim fEspecialityCode As String
    Public Property EspecialityCode() As String
        Get
            Return fEspecialityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EspecialityCode", fEspecialityCode, value)
        End Set
    End Property

#Region "Association"
#End Region

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
