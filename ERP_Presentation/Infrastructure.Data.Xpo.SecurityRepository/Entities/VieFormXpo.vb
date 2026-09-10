
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Base

<Persistent("Security.Form")>
Public Class VieFormXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fName As String
    <Size(60)>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fClassName As String
    <Size(60)>
    Public Property ClassName() As String
        Get
            Return fClassName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ClassName", fClassName, value)
        End Set
    End Property

    Dim fAssemblyName As String
    <Size(60)>
    Public Property AssemblyName() As String
        Get
            Return fAssemblyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssemblyName", fAssemblyName, value)
        End Set
    End Property


    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
        End Set
    End Property

    <PersistentAlias("Iif(State = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StateName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
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
