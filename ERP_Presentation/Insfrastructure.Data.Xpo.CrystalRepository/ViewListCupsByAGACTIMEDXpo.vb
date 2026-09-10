Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.ViewListCupsByAGACTIMED")>
Partial Public Class ViewListCupsByAGACTIMEDXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCODSERIPS As String
    Public Property CODSERIPS() As String
        Get
            Return fCODSERIPS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODSERIPS", fCODSERIPS, value)
        End Set
    End Property

    Dim fDESSERIPS As String
    Public Property DESSERIPS() As String
        Get
            Return fDESSERIPS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DESSERIPS", fDESSERIPS, Trim(value))
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

    Dim fCODACTMED As String
    Public Property CODACTMED() As String
        Get
            Return fCODACTMED
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODACTMED", fCODACTMED, Trim(value))
        End Set
    End Property

    Dim fTIPSERIPS As Integer
    Public Property TIPSERIPS() As Integer
        Get
            Return fTIPSERIPS
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TIPSERIPS", fTIPSERIPS, value)
        End Set
    End Property

    Dim fAPLICARIAS As Boolean
    Public Property APLICARIAS() As Boolean
        Get
            Return fAPLICARIAS
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("APLICARIAS", fAPLICARIAS, value)
        End Set
    End Property

    Dim fHaveDescription As Boolean
    Public Property HaveDescription() As Boolean
        Get
            Return fHaveDescription
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HaveDescription", fHaveDescription, value)
        End Set
    End Property

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
