'************************************************************
' Assembly         : Infraestructure.Data.Xpo.CommonRepository
' Author           : Juan Diego Diaz
' Created          : 16-04-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

#End Region

''' <summary>
''' Clase Persona para servicios Xpo.
''' </summary>
<Persistent("INPERSONA")> _
Public Class CommonINPERSONAXpo
    Inherits XPLiteObject
    Dim fAUTO1 As Integer
    <Key(True)> _
    <Persistent("AUTO")> _
    Public Property AUTO1() As Integer
        Get
            Return fAUTO1
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AUTO1", fAUTO1, value)
        End Set
    End Property
    Dim fIDENTIFICACION As String
    <Indexed(Name:="IX_INPERSONA", Unique:=True)> _
    <Size(15)> _
    Public Property IDENTIFICACION() As String
        Get
            Return fIDENTIFICACION
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IDENTIFICACION", fIDENTIFICACION, value)
        End Set
    End Property
    Dim fIPTIPODOC As Short
    Public Property IPTIPODOC() As Short
        Get
            Return fIPTIPODOC
        End Get
        Set(ByVal value As Short)
            SetPropertyValue(Of Short)("IPTIPODOC", fIPTIPODOC, value)
        End Set
    End Property
    Dim fIPPRINOMB As String
    <Size(50)> _
    Public Property IPPRINOMB() As String
        Get
            Return fIPPRINOMB
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPPRINOMB", fIPPRINOMB, value)
        End Set
    End Property
    Dim fIPSEGNOMB As String
    <Size(50)> _
    Public Property IPSEGNOMB() As String
        Get
            Return fIPSEGNOMB
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSEGNOMB", fIPSEGNOMB, value)
        End Set
    End Property
    Dim fIPPRIAPEL As String
    <Size(50)> _
    Public Property IPPRIAPEL() As String
        Get
            Return fIPPRIAPEL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPPRIAPEL", fIPPRIAPEL, value)
        End Set
    End Property
    Dim fIPSEGAPEL As String
    <Size(50)> _
    Public Property IPSEGAPEL() As String
        Get
            Return fIPSEGAPEL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSEGAPEL", fIPSEGAPEL, value)
        End Set
    End Property
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
    Dim fIPFECNACI As DateTime
    Public Property IPFECNACI() As DateTime
        Get
            Return fIPFECNACI
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IPFECNACI", fIPFECNACI, value)
        End Set
    End Property
    Dim fTEMPLATE() As Byte
    <Size(SizeAttribute.Unlimited)> _
    Public Property TEMPLATE() As Byte()
        Get
            Return fTEMPLATE
        End Get
        Set(ByVal value As Byte())
            SetPropertyValue(Of Byte())("TEMPLATE", fTEMPLATE, value)
        End Set
    End Property
    Dim fIPSEXOPER As Short
    Public Property IPSEXOPER() As Short
        Get
            Return fIPSEXOPER
        End Get
        Set(ByVal value As Short)
            SetPropertyValue(Of Short)("IPSEXOPER", fIPSEXOPER, value)
        End Set
    End Property
    Dim fELIMINADO As Boolean
    <Indexed(Name:="IX_INPERSONA_ELIMINADO_ESTADO")> _
    Public Property ELIMINADO() As Boolean
        Get
            Return fELIMINADO
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ELIMINADO", fELIMINADO, value)
        End Set
    End Property
    Dim fSINCRONI As Char
    Public Property SINCRONI() As Char
        Get
            Return fSINCRONI
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("SINCRONI", fSINCRONI, value)
        End Set
    End Property
    <Association("SEGUSUARUReferencesINPERSONA", GetType(CommonSEGUSUARUXpo))> _
    Public ReadOnly Property SEGUSUARUs() As XPCollection(Of CommonSEGUSUARUXpo)
        Get
            Return GetCollection(Of CommonSEGUSUARUXpo)("SEGUSUARUs")
        End Get
    End Property


#Region "Constructores"

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
