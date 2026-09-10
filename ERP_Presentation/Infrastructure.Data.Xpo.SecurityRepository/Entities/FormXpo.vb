Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Base

<Persistent("Security.Form")>
Public Class FormXpo
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
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fPrintEvents As String
    Public Property PrintEvents() As String
        Get
            Return fPrintEvents
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PrintEvents", fPrintEvents, value)
        End Set
    End Property

    Dim fHasSequence As Boolean
    Public Property HasSequence() As Boolean
        Get
            Return fHasSequence
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasSequence", fHasSequence, value)
        End Set
    End Property

    Dim fIsNativeForm As Boolean
    Public Property IsNativeForm() As Boolean
        Get
            Return fIsNativeForm
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsNativeForm", fIsNativeForm, value)
        End Set
    End Property

    Dim fHasForm As Boolean
    Public Property HasForm() As Boolean
        Get
            Return fHasForm
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasForm", fHasForm, value)
        End Set
    End Property

    Dim fClassName As String
    Public Property ClassName() As String
        Get
            Return fClassName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ClassName", fClassName, value)
        End Set
    End Property

    Dim fAssemblyName As String
    Public Property AssemblyName() As String
        Get
            Return fAssemblyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssemblyName", fAssemblyName, value)
        End Set
    End Property

    Dim fHandlesMassiveConfirm As Boolean
    Public Property HandlesMassiveConfirm() As Boolean
        Get
            Return fHandlesMassiveConfirm
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesMassiveConfirm", fHandlesMassiveConfirm, value)
        End Set
    End Property

    Dim fSequenceModule As String
    Public Property SequenceModule() As String
        Get
            Return fSequenceModule
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SequenceModule", fSequenceModule, value)
        End Set
    End Property

    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
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
