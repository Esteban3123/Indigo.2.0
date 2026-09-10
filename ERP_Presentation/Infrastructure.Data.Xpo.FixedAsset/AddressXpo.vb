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
<Persistent("FixedAsset.Address")> _
Public Class AddressXpo
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
