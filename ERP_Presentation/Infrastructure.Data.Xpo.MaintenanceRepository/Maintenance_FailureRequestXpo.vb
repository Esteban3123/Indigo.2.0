#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

#End Region

<Persistent("Maintenance.MaintenanceFailureRequest")>
Public Class MaintenanceFailureRequestXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fDateFailure As DateTime
    Public Property DateFailure() As DateTime
        Get
            Return fDateFailure
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateFailure", fDateFailure, value)
        End Set
    End Property

    Dim fTypeRequest As Integer
    Public Property TypeRequest() As Integer
        Get
            Return fTypeRequest
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TypeRequest", fTypeRequest, value)
        End Set
    End Property

    Dim fReport As Integer
    Public Property Report() As Integer
        Get
            Return fReport
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Report", fReport, value)
        End Set
    End Property

    Dim fObservation As String
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
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

#End Region

#Region "CustomMembers"



    <PersistentAlias("Iif(TypeRequest = 1, 'Falla General',Iif(TypeRequest = 2,'Revisión',Iif(TypeRequest = 3,'Otro','')))")>
    Public ReadOnly Property TypeRequestName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeRequestName"))
        End Get
    End Property


    <PersistentAlias("Iif(Report = 1, 'Usuario del Sistema',Iif(Report = 2,'Otro',''))")>
    Public ReadOnly Property ReportName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ReportName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Registrado',Iif(Status = 2,'Confirmado',Iif(Status = 3,'Anulado','')))")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

#End Region

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

End Class
