'************************************************************
' Assembly         : Infraestructure.FisedAssets.AddressServiceXpo
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 1-04-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
#End Region
<Persistent("GeneralLedger.GeneralLedgerIVA")>
Public Class IvaXpo
    Inherits XPLiteObject

    Dim _id As Integer
    <Key(True)>
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("Id", _id, value)
        End Set
    End Property

    Dim _Code As String
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Code", _Code, value)
        End Set
    End Property

    Dim _Name As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Name", _Name, value)
        End Set
    End Property

    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim _Percent As String
    <Persistent("Percent")> _
    Public Property Percent() As String
        Get
            Return _Percent
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Percent", _Percent, value)
        End Set
    End Property

    Dim _Status As Boolean
    <Persistent("Status")>
    Public Property Status() As Boolean

        Get
            Return _Status
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("Status", _Status, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
