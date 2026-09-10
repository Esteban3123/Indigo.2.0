'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 05-11-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Resources

#End Region

<Persistent("Treasury.CheckCashing")> _
Public Class CheckCashingXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fVoucherTransactionId As Integer
    <Persistent("VoucherTransactionId")> _
    Public Property VoucherTransactionId() As Integer
        Get
            Return fVoucherTransactionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VoucherTransactionId", fVoucherTransactionId, value)
        End Set
    End Property

    Dim fCurrentCheckNumber As Long
    <Persistent("CurrentCheckNumber")> _
    Public Property CurrentCheckNumber() As Long
        Get
            Return fCurrentCheckNumber
        End Get
        Set(value As Long)
            SetPropertyValue(Of Long)("CurrentCheckNumber", fCurrentCheckNumber, value)
        End Set
    End Property

    Dim fNextCheckNumber As Long
    <Persistent("NextCheckNumber")> _
    Public Property NextCheckNumber() As Long
        Get
            Return fNextCheckNumber
        End Get
        Set(value As Long)
            SetPropertyValue(Of Long)("NextCheckNumber", fNextCheckNumber, value)
        End Set
    End Property

    Dim fCancellationDate As DateTime
    <Persistent("CancellationDate")> _
    Public Property CancellationDate() As DateTime
        Get
            Return fCancellationDate
        End Get
        Set(value As DateTime)
            SetPropertyValue(Of DateTime)("CancellationDate", fCancellationDate, value)
        End Set
    End Property

    Dim fCancellationCheckId As Integer
    <Persistent("CancellationCheckId")> _
    Public Property CancellationCheckId() As Integer
        Get
            Return fCancellationCheckId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CancellationCheckId", fCancellationCheckId, value)
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
