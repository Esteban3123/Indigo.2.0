'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/06/2020
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
<Persistent("Authorization.ViewListEvents")>
Public Class ViewListEventsXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fTraceabilityPaperworkEventsId As Integer
    <Key(True)>
    Public Property TraceabilityPaperworkEventsId() As Integer
        Get
            Return fTraceabilityPaperworkEventsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkEventsId", fTraceabilityPaperworkEventsId, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkId As Integer
    Public Property TraceabilityPaperworkId() As Integer
        Get
            Return fTraceabilityPaperworkId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkId", fTraceabilityPaperworkId, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkAnnexesId As Integer
    Public Property TraceabilityPaperworkAnnexesId() As Integer
        Get
            Return fTraceabilityPaperworkAnnexesId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkAnnexesId", fTraceabilityPaperworkAnnexesId, value)
        End Set
    End Property

    Dim fHealthAdministratorId As Integer
    Public Property HealthAdministratorId() As Integer
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property

    Dim fHealthAdministratorCode As String
    Public Property HealthAdministratorCode() As String
        Get
            Return fHealthAdministratorCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorCode", fHealthAdministratorCode, value)
        End Set
    End Property

    Dim fHealthAdministratorName As String
    Public Property HealthAdministratorName() As String
        Get
            Return fHealthAdministratorName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorName", fHealthAdministratorName, value)
        End Set
    End Property

    Dim fHealthAdministratorCodeName As String
    Public Property HealthAdministratorCodeName() As String
        Get
            Return fHealthAdministratorCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorCodeName", fHealthAdministratorCodeName, value)
        End Set
    End Property

    Dim fCreationUser As String
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

    Dim fReportType As Integer
    Public Property ReportType() As Integer
        Get
            Return fReportType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ReportType", fReportType, value)
        End Set
    End Property

    Dim fReportTypeName As String
    Public Property ReportTypeName() As String
        Get
            Return fReportTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReportTypeName", fReportTypeName, value)
        End Set
    End Property

    Dim fAnnexConsecutive As Decimal
    Public Property AnnexConsecutive() As Decimal
        Get
            Return fAnnexConsecutive
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AnnexConsecutive", fAnnexConsecutive, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkStatus As Integer
    Public Property TraceabilityPaperworkStatus() As Integer
        Get
            Return fTraceabilityPaperworkStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkStatus", fTraceabilityPaperworkStatus, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkEventsStatus As Integer
    Public Property TraceabilityPaperworkEventsStatus() As Integer
        Get
            Return fTraceabilityPaperworkEventsStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkEventsStatus", fTraceabilityPaperworkEventsStatus, value)
        End Set
    End Property

    Dim fTraceabilityPaperworkEventsStatusName As String
    Public Property TraceabilityPaperworkEventsStatusName() As String
        Get
            Return fTraceabilityPaperworkEventsStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TraceabilityPaperworkEventsStatusName", fTraceabilityPaperworkEventsStatusName, value)
        End Set
    End Property

#End Region

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
