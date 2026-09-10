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
<Persistent("Authorization.ViewListAnnexes")>
Public Class ViewListAnnexesXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fTraceabilityPaperworkAnnexesId As Integer
    <Key(True)>
    Public Property TraceabilityPaperworkAnnexesId() As Integer
        Get
            Return fTraceabilityPaperworkAnnexesId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TraceabilityPaperworkAnnexesId", fTraceabilityPaperworkAnnexesId, value)
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

    Dim fConsecutive As Decimal
    Public Property Consecutive() As Decimal
        Get
            Return fConsecutive
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Consecutive", fConsecutive, value)
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

    Dim fCreationUser As String
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fDocumentTypeName As String
    Public Property DocumentTypeName() As String
        Get
            Return fDocumentTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentTypeName", fDocumentTypeName, value)
        End Set
    End Property

    Dim fServiceCode As String
    Public Property ServiceCode() As String
        Get
            Return fServiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceCode", fServiceCode, value)
        End Set
    End Property

    Dim fServiceDescription As String
    Public Property ServiceDescription() As String
        Get
            Return fServiceDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceDescription", fServiceDescription, value)
        End Set
    End Property

    Dim fRequestQuantity As Integer
    Public Property RequestQuantity() As Integer
        Get
            Return fRequestQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestQuantity", fRequestQuantity, value)
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

    Dim fTraceabilityPaperworkStatusName As String
    Public Property TraceabilityPaperworkStatusName() As String
        Get
            Return fTraceabilityPaperworkStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TraceabilityPaperworkStatusName", fTraceabilityPaperworkStatusName, value)
        End Set
    End Property

    Dim fFolio As String
    Public Property Folio() As String
        Get
            Return fFolio
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Folio", fFolio, value)
        End Set
    End Property

    Dim fTypeRequestServices As Integer
    Public Property TypeRequestServices() As Integer
        Get
            Return fTypeRequestServices
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TypeRequestServices", fTypeRequestServices, value)
        End Set
    End Property

    Dim fPriorityAttention As Integer
    Public Property PriorityAttention() As Integer
        Get
            Return fPriorityAttention
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PriorityAttention", fPriorityAttention, value)
        End Set
    End Property

    Dim fJustification As String
    Public Property Justification() As String
        Get
            Return fJustification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Justification", fJustification, value)
        End Set
    End Property

    Dim fDiagnosticCode As String
    Public Property DiagnosticCode() As String
        Get
            Return fDiagnosticCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DiagnosticCode", fDiagnosticCode, value)
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
