Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.InvoiceEntityCapitatedGroupers")> _
Public Class InvoiceEntityCapitatedGroupersReportXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fInvoiceEntityCapitatedId As InvoiceEntityCapitatedReportXpo
    <Association("Billing_InvoiceEntityCapitatedGroupers_References_InvoiceEntityCapitated")> _
    Public Property InvoiceEntityCapitatedId() As InvoiceEntityCapitatedReportXpo
        Get
            Return fInvoiceEntityCapitatedId
        End Get
        Set(ByVal value As InvoiceEntityCapitatedReportXpo)
            SetPropertyValue(Of InvoiceEntityCapitatedReportXpo)("InvoiceEntityCapitatedId", fInvoiceEntityCapitatedId, value)
        End Set
    End Property

    Dim fGroupersId As Integer
    Public Property GroupersId() As Integer
        Get
            Return fGroupersId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupersId", fGroupersId, value)
        End Set
    End Property

    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fDescription As String
    <Size(100)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fUserMin As Integer
    Public Property UserMin() As Integer
        Get
            Return fUserMin
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserMin", fUserMin, value)
        End Set
    End Property

    Dim fUserMax As Integer
    Public Property UserMax() As Integer
        Get
            Return fUserMax
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserMax", fUserMax, value)
        End Set
    End Property

    Dim fProjectCME As Decimal
    Public Property ProjectCME() As Decimal
        Get
            Return fProjectCME
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProjectCME", fProjectCME, value)
        End Set
    End Property

    Dim fTotalContract As Decimal
    Public Property TotalContract() As Decimal
        Get
            Return fTotalContract
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalContract", fTotalContract, value)
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

#End Region

End Class

