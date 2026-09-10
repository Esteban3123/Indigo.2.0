'************************************************************
' Assembly         : Infraestructure.FisedAssets.ClasificationServiceXpo
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 6-20-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
#End Region
<Persistent("FixedAsset.FixedAssetClassification")> _
Public Class ClassificationXpo
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
    <Persistent("Name")> _
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Name", _Name, value)
        End Set
    End Property

    Dim _State As Boolean
    <Persistent("State")> _
    Public Property State() As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("State", _State, value)
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