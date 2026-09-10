'***********************************************************************
' Assembly         : Infraestructure.Data.Xpo.CrystalRepository
' Author           : Johan Sebastian Carranza Ramos
' Created          : 20-05-2019
'
' Last Modified By : Johan Sebastian Carranza Ramos
' Last Modified On : 20-05-2019
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports DevExpress.Xpo

<Persistent("dbo.ViewAuditoryDetail")> _
Partial Public Class HCAUDITORIAXpo
    Inherits XPLiteObject
    
    'IDENTIFICADOR
    Dim fID As Integer
    <Key()> _
    Public Property ID() As Integer 
        Get
            Return fID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ID", fID, value)
        End Set
    End Property


    'Codigo de Usuario 
    Dim fCODUSUCONS As String
    <Size(15)> _
    Public Property CODUSUCONS() As String
        Get
            Return fCODUSUCONS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODUSUCONS", fCODUSUCONS, value)
        End Set
    End Property

    'Codigo del paciente 
    Dim fCODPACQCON As String
    <Size(15)> _
    Public Property CODPACQCON() As String
        Get
            Return fCODPACQCON
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODPACQCON", fCODPACQCON, value)
        End Set
    End Property

    'Fecha - Hora del servidor 
    Dim fFECHCONSU As DateTime
    Public Property FECHCONSU() As DateTime
        Get
            Return fFECHCONSU
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECHCONSU", fFECHCONSU, value)
        End Set
    End Property

    'Nombre de la maquina 
    Dim fNOMMAQCONS As String
    <Size(50)> _
    Public Property NOMMAQCONS() As String
        Get
            Return fNOMMAQCONS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMMAQCONS", fNOMMAQCONS, value)
        End Set
    End Property

    'IP de la consulta
    Dim fIPMAQCONS As String
    <Size(50)> _
    Public Property IPMAQCONS() As String
        Get
            Return fIPMAQCONS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPMAQCONS", fIPMAQCONS, value)
        End Set
    End Property

    'Ingreso del paciente
    Dim fINGRESO As String
    <Size(15)> _
    Public Property INGRESO() As String
        Get
            Return fINGRESO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("INGRESO", fINGRESO, value)
        End Set
    End Property

    'Folio del paciente
    Dim fFOLIOIN As String
    <Size(10)> _
    Public Property FOLIOIN() As String
        Get
            Return fFOLIOIN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FOLIOIN", fFOLIOIN, value)
        End Set
    End Property

    'Anonimato 
    Dim fANONI As Boolean
    Public Property ANONIMATO() As Boolean
        Get
            Return fANONI
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ANONIMATO", fANONI, value)
        End Set
    End Property

    'Unidad funcional donde se encuentra el paciente.
    Dim fUFUCODIGO As String
    <Size(10)> _
    Public Property UFUCODIGO() As String
        Get
            Return fUFUCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUCODIGO", fUFUCODIGO, value)
        End Set
    End Property

    'Nombre completo del paciente 
    Dim fIPNOMCOMP As String
    <Size(250)> _
    Public Property IPNOMCOMP() As String
        Get
            Return fIPNOMCOMP
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPNOMCOMP", fIPNOMCOMP, value)
        End Set
    End Property

    'Nombre del usuario
    Dim fNOMUSUARI As String
    <Size(60)> _
    Public Property NOMUSUARI() As String
        Get
            Return fNOMUSUARI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMUSUARI", fNOMUSUARI, value)
        End Set
    End Property

    'Descripcion del cargo del usuario 
    Dim fDESCARUSU As String
    <Size(30)> _
    Public Property DESCARUSU() As String
        Get
            Return fDESCARUSU
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DESCARUSU", fDESCARUSU, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

End Class
