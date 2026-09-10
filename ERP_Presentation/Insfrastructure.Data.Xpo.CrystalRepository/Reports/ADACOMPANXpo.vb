Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("dbo.ADACOMPAN")> _
Public Class ADACOMPANXpo
    Inherits XPLiteObject
    Dim fCONSEACOM As Integer
    <Key(True)> _
    Public Property CONSEACOM() As Integer
        Get
            Return fCONSEACOM
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CONSEACOM", fCONSEACOM, value)
        End Set
    End Property
    Dim fIPCODPACI As PatientXpo
    <Association("ADACOMPANXpoReferencesPatientXpo")> _
    Public Property IPCODPACI() As PatientXpo
        Get
            Return fIPCODPACI
        End Get
        Set(ByVal value As PatientXpo)
            SetPropertyValue(Of PatientXpo)("IPCODPACI", fIPCODPACI, value)
        End Set
    End Property
    Dim fPRINOMBRE As String
    <Size(60)> _
    Public Property PRINOMBRE() As String
        Get
            Return fPRINOMBRE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PRINOMBRE", fPRINOMBRE, value)
        End Set
    End Property
    Dim fSEGNOMBRE As String
    <Size(60)> _
    Public Property SEGNOMBRE() As String
        Get
            Return fSEGNOMBRE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SEGNOMBRE", fSEGNOMBRE, value)
        End Set
    End Property
    Dim fPRIAPELLI As String
    <Size(60)> _
    Public Property PRIAPELLI() As String
        Get
            Return fPRIAPELLI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PRIAPELLI", fPRIAPELLI, value)
        End Set
    End Property
    Dim fSEGAPELLI As String
    <Size(60)> _
    Public Property SEGAPELLI() As String
        Get
            Return fSEGAPELLI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SEGAPELLI", fSEGAPELLI, value)
        End Set
    End Property
    Dim fTELACOMPA As String
    <Size(15)> _
    Public Property TELACOMPA() As String
        Get
            Return fTELACOMPA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TELACOMPA", fTELACOMPA, value)
        End Set
    End Property
    Dim fPARACOMPA As String
    <Size(2)> _
    Public Property PARACOMPA() As String
        Get
            Return fPARACOMPA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PARACOMPA", fPARACOMPA, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
