'***********************************************************************
' Assembly         : Presentacion.Base
' Author           : Jorge Leonardo Vernaza
' Created          : 29-10-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports System.Drawing
#End Region
Public NotInheritable Class FrmMessageConversation

    Dim Timer As New Timers.Timer
    Public Event Answer(ByVal Code As String)
    Public Event Declined()


    Private _UserCode As String
    Public Property UserCode As String
        Get
            Return _UserCode
        End Get
        Set(value As String)
            _UserCode = value
        End Set
    End Property

    Private _UserPhoto As Bitmap
    Public Property UserPhoto As Bitmap
        Get
            Return _UserPhoto
        End Get
        Set(value As Bitmap)
            _UserPhoto = value
        End Set
    End Property


    Private _UserName As String
    Public Property UserName As String
        Get
            Return _UserName
        End Get
        Set(value As String)
            _UserName = value
        End Set
    End Property

    Private _UserMessage As String
    Public Property UserMessage As String
        Get
            Return _UserMessage
        End Get
        Set(value As String)
            _UserMessage = value
        End Set
    End Property


    ''' <summary>
    ''' Instancia del popup de mensajeria <see cref="FrmMessageConversation"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Timer1.Start()
    End Sub

    Private Sub AnswerWpf()
        RaiseEvent Answer(UserCode)
        Me.Close()
    End Sub

    Private Sub DeclinedWPF()
        RaiseEvent Declined()
        Me.Close()
    End Sub

    Private Sub FrmMessageConversation_Load(sender As Object, e As EventArgs) Handles Me.Load
        WpfMessageConversation1.UserName = UserName
        WpfMessageConversation1.UserMessage = UserMessage
        WpfMessageConversation1.UserPhoto = UserPhoto
        AddHandler WpfMessageConversation1.Answer, AddressOf AnswerWpf
        AddHandler WpfMessageConversation1.Declined, AddressOf DeclinedWPF
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        RaiseEvent Declined()
        Me.Close()
    End Sub
End Class