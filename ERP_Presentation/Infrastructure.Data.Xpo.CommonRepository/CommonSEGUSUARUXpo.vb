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
''' Clase Usuario para servicios Xpo.
''' </summary>
<Persistent("SEGUSUARU")> _
Public Class CommonSEGUSUARUXpo
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

    Dim fIDENTIFICACION As CommonINPERSONAXpo
    <Indexed(Name:="IX_SEGUSUARU", Unique:=True)> _
    <Association("SEGUSUARUReferencesINPERSONA")> _
    Public Property IDENTIFICACION() As CommonINPERSONAXpo
        Get
            Return fIDENTIFICACION
        End Get
        Set(ByVal value As CommonINPERSONAXpo)
            SetPropertyValue(Of CommonINPERSONAXpo)("IDENTIFICACION", fIDENTIFICACION, value)
        End Set
    End Property

    Dim fCODUSUARI As String
    <Size(20)> _
    <Persistent("CODUSUARI")> _
    Public Property Codigo() As String
        Get
            Return fCODUSUARI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODUSUARI", fCODUSUARI, value)
        End Set
    End Property
    Dim fUSUACTIVO As Boolean
    Public Property USUACTIVO() As Boolean
        Get
            Return fUSUACTIVO
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("USUACTIVO", fUSUACTIVO, value)
        End Set
    End Property
    Dim fCODIGOROL As Integer
    Public Property CODIGOROL() As Integer
        Get
            Return fCODIGOROL
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CODIGOROL", fCODIGOROL, value)
        End Set
    End Property
    Dim fCODGRUPOU As Integer
    Public Property CODGRUPOU() As Integer
        Get
            Return fCODGRUPOU
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CODGRUPOU", fCODGRUPOU, value)
        End Set
    End Property
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
