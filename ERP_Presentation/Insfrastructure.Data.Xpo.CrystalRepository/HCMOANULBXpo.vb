
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("dbo.HCMOANULB")>
Partial Public Class HCMOANULBXpo
    Inherits XPLiteObject

    Dim fCODMOTANU As String
    <Key()>
    Public Property CODMOTANU() As String
        Get
            Return fCODMOTANU
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODMOTANU", fCODMOTANU, value)
        End Set
    End Property

    Dim fDESMOTANU As String
    Public Property DESMOTANU() As String
        Get
            Return fDESMOTANU
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DESMOTANU", fDESMOTANU, value)
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

    Dim fESTADO As Integer
    Public Property ESTADO() As Integer
        Get
            Return fESTADO
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ESTADO", fESTADO, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
