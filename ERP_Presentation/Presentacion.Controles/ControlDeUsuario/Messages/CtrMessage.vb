'***********************************************************************
' Assembly         : Presentacion.IndigoMessenger.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 06-12-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Controls.MVP
Imports DevExpress.XtraEditors
Imports Presentation.Base
Imports System.Text.RegularExpressions
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraEditors.Drawing

#End Region

''' <summary>
''' Clase con la funcionalidad del control de mensajeria
''' </summary>
''' <remarks></remarks>
Public Class CtrMessage

#Region "Eventos"
    ''' <summary>
    ''' Evento que indica que se esta escribiendo
    ''' </summary>
    ''' <param name="Writing"></param>
    ''' <remarks></remarks>
    Public Event Writing(ByVal Writing As Boolean)
    ''' <summary>
    ''' Evento que indica que se ha leido los mensajes
    ''' </summary>
    ''' <param name="Read"></param>
    ''' <remarks></remarks>
    Public Event MessageRead(ByVal Read As Boolean)
    ''' <summary>
    ''' Evento que indica que se envio un mensaje nuevo
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <param name="MessageDate"></param>
    ''' <param name="MessageUser"></param>
    ''' <remarks></remarks>
    Public Event SendMessage(ByVal Message As String, ByVal MessageDate As DateTime, ByVal MessageUser As String)

#End Region

#Region "Variables Globales"
    ''' <summary>
    ''' Variable para saber si el documento esta vacio
    ''' </summary>
    ''' <remarks></remarks>
    Private IsDocumentEmpty As Boolean
    ''' <summary>
    ''' Variable que contiene el diccionario de emoticons
    ''' </summary>
    ''' <remarks></remarks>
    Private Dictionary As New Dictionary(Of String, String)
    ''' <summary>
    ''' variable que contiene las expresiones regulares
    ''' </summary>
    ''' <remarks></remarks>
    Private regular As Regex
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As Infrastructure.CrossCutting.Base.SessionValues
#End Region

#Region "Metodos"

    Public Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Metodo load del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub CtrMessage_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadDictionary()
        indigo = Await LoadSessionValuesAsync()
        If UserCode Is Nothing OrElse UserCode = String.Empty Then
            EnabledConversation(False)
        End If
        LoadMessages()
    End Sub


    ''' <summary>
    ''' Metodo keydown donde identificamos si la tecla presionada es enter para enviar el mensaje
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDtxtMessage_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtMessage.KeyDown
        If INDtxtMessage.Document.IsEmpty = False Then
            If e.Control = False AndAlso e.KeyCode = Keys.Enter Then
                If INDtxtMessage.ReadOnly = False Then
                    Using Model As New MformBase
                        Dim Message = INDtxtMessage.HtmlText
                        INDtxtMessage.Text = String.Empty
                        Dim DateServer As DateTime = Await Model.GetDateServerAsync
                        NewMessage(Message, DateServer, indigo.UserIndigoName)
                    End Using
                Else
                    MessageIndigo.Show("No se puede enviar el mensaje porque el usuario esta desconectado", Infrastructure.CrossCutting.Base.MessageType.Warning, obtenerRecurso(Eresources.MessagesTitle, Eform.Messages))
                End If
            End If
        Else
                 RaiseEvent MessageRead(True)
            End If
    End Sub

    ''' <summary>
    ''' KeyUp donde indentificamo si la tecla presionada es enter entonces borramos el texto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtMessage_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtMessage.KeyUp
        If e.Control = False AndAlso e.KeyCode = Keys.Enter Then
            INDtxtMessage.Text = String.Empty
            INDtxtMessage.HtmlText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Evento textChange donde comparamos con el diccionario de datos para remplazar los caracteres por emoticons
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtMessage_TextChanged(sender As Object, e As EventArgs) Handles INDtxtMessage.TextChanged
        If UserCode Is Nothing OrElse UserCode = String.Empty Then
            Exit Sub
        End If
        If IsDocumentEmpty = True AndAlso INDtxtMessage.Document.IsEmpty = False Then
            RaiseEvent Writing(True)
            IsDocumentEmpty = INDtxtMessage.Document.IsEmpty
        End If
        If IsDocumentEmpty = False AndAlso INDtxtMessage.Document.IsEmpty = True Then
            RaiseEvent MessageRead(True)
            IsDocumentEmpty = INDtxtMessage.Document.IsEmpty
            Exit Sub
        End If

        Dim match = regular.Matches(INDtxtMessage.Text)
        Dim findWord As DocumentRange() = INDtxtMessage.Document.FindAll(regular)
        If match.Count > 0 Then
            Dim data = (From a In match
                       Select a).ToList
            Dim newText As String = INDtxtMessage.HtmlText
            For Each item As System.Text.RegularExpressions.Match In data
                Dim dataReplace = item.Value
                If item.ToString.Contains("¬¬") Then
                    dataReplace = item.ToString.Replace("¬¬", "&not;&not;")
                End If
                If item.ToString.Contains("(!)") Then
                    dataReplace = item.ToString.Replace("(¡)", "(&iexcl;)")
                End If
                If item.ToString.Contains("<)") Then
                    dataReplace = item.ToString.Replace("<)", "&lt;)")
                End If
                If item.ToString.Contains("<D") Then
                    dataReplace = item.ToString.Replace("<D", "&lt;D")
                End If
                If item.ToString.Contains("<]") Then
                    dataReplace = item.ToString.Replace("<]", "&lt;]")
                End If
                If item.ToString.Contains(">.<") Then
                    dataReplace = item.ToString.Replace(">.<", "&gt;.&lt;")
                End If
                If item.ToString.Contains(":'(") Then
                    dataReplace = item.ToString.Replace(":'(", ":&#39;(")
                End If
                If item.ToString.Contains(":&") Then
                    dataReplace = item.ToString.Replace(":&", ":&amp;")
                End If
                If item.ToString.Contains("'|") Then
                    dataReplace = item.ToString.Replace("'|", "&#39;|")
                End If
                If item.ToString.Contains("'/") Then
                    dataReplace = item.ToString.Replace("'/", "&#39;/")
                End If

                newText = newText.Replace(dataReplace, "")
                INDtxtMessage.HtmlText = newText
                Dim myStart As DocumentPosition
                Try
                    myStart = INDtxtMessage.Document.CreatePosition(findWord.LastOrDefault.End.ToInt - 2)

                    Select Case Dictionary(item.Value)
                        Case "1"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._1)
                        Case "2"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._2)
                        Case "3"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._3)
                        Case "4"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._4)
                        Case "5"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._5)
                        Case "6"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._6)
                        Case "7"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._7)
                        Case "8"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._8)
                        Case "9"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._9)
                        Case "10"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._10)
                        Case "11"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._11)
                        Case "12"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._12)
                        Case "13"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._13)
                        Case "14"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._14)
                        Case "15"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._15)
                        Case "16"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._16)
                        Case "17"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._17)
                        Case "18"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._18)
                        Case "19"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._19)
                        Case "20"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._20)
                        Case "21"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._21)
                        Case "22"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._22)
                        Case "23"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._23)
                        Case "24"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._24)
                        Case "25"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._25)
                        Case "26"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._26)
                        Case "27"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._27)
                        Case "28"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._28)
                        Case "29"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._29)
                        Case "30"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._30)
                        Case "31"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._31)
                        Case "32"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._32)
                        Case "33"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._33)
                        Case "33"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._34)
                        Case "35"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._35)
                        Case "36"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._36)
                        Case "37"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._37)
                        Case "38"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._38)
                        Case "39"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._39)
                        Case "40"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._40)
                        Case "41"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._41)
                        Case "42"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._42)
                        Case "43"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._43)
                        Case "44"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._44)
                        Case "45"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._45)
                        Case "46"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._46)
                        Case "47"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._47)
                        Case "49"
                            INDtxtMessage.Document.Images.Insert(myStart, My.Resources.EmoticonsResources._49)
                    End Select
                    INDtxtMessage.Document.CaretPosition = myStart
                Catch
                End Try
            Next
        End If
    End Sub

    ''' <summary>
    ''' Metodo para crear un nuevo mensaje
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <param name="MessageDate"></param>
    ''' <param name="MessageUser"></param>
    ''' <remarks></remarks>
    Private Sub NewMessage(ByVal Message As String, ByVal MessageDate As DateTime, ByVal MessageUser As String)
        If Messages Is Nothing Then Messages = New List(Of INDmessage)
        Messages.Add(New INDmessage With {.Message = Message, .MessageDate = MessageDate, .MessageUser = MessageUser, .MessageUserCode = UserCode})
        Dim ConversationItem As New CtrConversationItem
        ConversationItem.INDDateMessage = MessageDate
        ConversationItem.INDMessage = Message
        ConversationItem.INDUserName = MessageUser
        ConversationItem.Dock = DockStyle.Top
        INDxcConversation.Controls.Add(ConversationItem)
        ConversationItem.BringToFront()
        RaiseEvent SendMessage(Message, MessageDate, UserCode)
        INDxcConversation.AutoScrollPosition = New Point(INDxcConversation.AutoScrollPosition.X, INDxcConversation.VerticalScroll.Maximum)
    End Sub

    ''' <summary>
    ''' metodo para agregar un nuevo mensaje 
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <param name="MessageDate"></param>
    ''' <param name="MessageUser"></param>
    ''' <remarks></remarks>
    Public Sub AddMessage(ByVal Message As String, ByVal MessageDate As DateTime, ByVal MessageUser As String)
        Dim ConversationItem As New CtrConversationItem
        ConversationItem.INDDateMessage = MessageDate
        ConversationItem.INDMessage = Message
        ConversationItem.INDUserName = MessageUser
        ConversationItem.Dock = DockStyle.Top
        INDxcConversation.Controls.Add(ConversationItem)
        ConversationItem.BringToFront()
        INDxcConversation.AutoScrollPosition = New Point(INDxcConversation.AutoScrollPosition.X, INDxcConversation.VerticalScroll.Maximum)
    End Sub

    ''' <summary>
    ''' Metodo para cargar el diccionario con los caracteres y la llave para posteriormente reemplazar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDictionary()
        Dictionary.Add(":)", "1")
        Dictionary.Add("<)", "2")
        Dictionary.Add(":D", "3")
        Dictionary.Add("<D", "4")
        Dictionary.Add("<]", "5")
        Dictionary.Add(";)", "6")
        Dictionary.Add("*.*", "7")
        Dictionary.Add(":*", "8")
        Dictionary.Add(":|", "9")
        Dictionary.Add(">.<", "10")
        Dictionary.Add(":'(", "11")
        Dictionary.Add("x.x", "12")
        Dictionary.Add(":#", "13")
        Dictionary.Add(":$", "14")
        Dictionary.Add(":x", "15")
        Dictionary.Add(":p", "16")
        Dictionary.Add("xp", "17")
        Dictionary.Add("D)", "18")
        Dictionary.Add("¬¬", "19")
        Dictionary.Add(":@", "20")
        Dictionary.Add(":s", "21")
        Dictionary.Add(":&", "22")
        Dictionary.Add("3:)", "23")
        Dictionary.Add("o:)", "24")
        Dictionary.Add("¬¬)", "25")
        Dictionary.Add("'|", "26")
        Dictionary.Add(":/", "27")
        Dictionary.Add(":c", "28")
        Dictionary.Add(":z", "29")
        Dictionary.Add("'/", "30")
        Dictionary.Add("(blue)", "31")
        Dictionary.Add("(purple)", "32")
        Dictionary.Add("(pink)", "33")
        Dictionary.Add("(green)", "34")
        Dictionary.Add("(l)", "35")
        Dictionary.Add("(u)", "36")
        Dictionary.Add("(l*)", "37")
        Dictionary.Add("(**)", "38")
        Dictionary.Add("(*)", "39")
        Dictionary.Add("(x)", "40")
        Dictionary.Add("(!)", "41")
        Dictionary.Add("(?)", "42")
        Dictionary.Add("(zzz)", "43")
        Dictionary.Add("(m)", "44")
        Dictionary.Add("(8)", "45")
        Dictionary.Add("*shit*", "46")
        Dictionary.Add("(y)", "47")
        Dictionary.Add("(▲)", "48")
        regular = New Regex(Dictionary.CreateExpression)
    End Sub

    ''' <summary>
    ''' Metodo para habilitar o deshabilitar la escritura en el control
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Public Sub EnabledWriting(ByVal Value As Boolean)
        INDtxtMessage.ReadOnly = Value
    End Sub

    ''' <summary>
    ''' Metodo para establecer el focus en el control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetFocus()
        INDtxtMessage.Focus()
    End Sub

    ''' <summary>
    ''' Metodo para cargar los mensajes en el control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadMessages()
        ClearMessages()
        INDxcConversation.Visible = False
        If Messages IsNot Nothing Then
            For Each Message As INDmessage In Messages
                Dim ConversationItem As New CtrConversationItem
                ConversationItem.INDDateMessage = Message.MessageDate
                ConversationItem.INDMessage = Message.Message
                ConversationItem.INDUserName = Message.MessageUser
                ConversationItem.Dock = DockStyle.Top
                INDxcConversation.Controls.Add(ConversationItem)
                ConversationItem.BringToFront()
            Next
        End If
        INDxcConversation.Visible = True
        INDxcConversation.AutoScrollPosition = New Point(INDxcConversation.AutoScrollPosition.X, INDxcConversation.VerticalScroll.Maximum)
    End Sub

    ''' <summary>
    ''' Metodo para limpiar el control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearMessages()
        INDxcConversation.Controls.Clear()
    End Sub

    ''' <summary>
    ''' Metodo para habilitar o deshabilitar el control de conversacion
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Public Sub EnabledConversation(ByVal Value As Boolean)
        If Value = False Then ClearMessages()
        Me.Enabled = Value
    End Sub

    ''' <summary>
    ''' Metodo clic de los controles para disparar el evento de mensaje leido
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDxcConversation_Click(sender As Object, e As EventArgs) Handles INDxcConversation.Click, INDtxtMessage.Click
        RaiseEvent MessageRead(True)
    End Sub

    Private Sub INDtxtMessage_GotFocus(sender As Object, e As EventArgs) Handles INDtxtMessage.GotFocus
        If INDtxtMessage.Document.IsEmpty = True Then
            RaiseEvent MessageRead(True)
        Else
            RaiseEvent Writing(True)
        End If
    End Sub

    ''' <summary>
    ''' Metodo lostfocus del control de escritura para dispara el evento mensaje leido
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtMessage_LostFocus(sender As Object, e As EventArgs) Handles INDtxtMessage.LostFocus
        RaiseEvent MessageRead(True)
    End Sub

    ''' <summary>
    ''' Metodo para insertar los emoticons correspondientes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub InsertEmoticon(sender As Object, e As EventArgs) Handles PictureEdit9.Click, PictureEdit8.Click, PictureEdit7.Click, PictureEdit6.Click, PictureEdit54.Click, PictureEdit53.Click, PictureEdit5.Click, PictureEdit45.Click, PictureEdit44.Click, PictureEdit43.Click, PictureEdit42.Click, PictureEdit41.Click, PictureEdit40.Click, PictureEdit4.Click, PictureEdit39.Click, PictureEdit38.Click, PictureEdit37.Click, PictureEdit36.Click, PictureEdit35.Click, PictureEdit34.Click, PictureEdit33.Click, PictureEdit32.Click, PictureEdit31.Click, PictureEdit30.Click, PictureEdit3.Click, PictureEdit29.Click, PictureEdit28.Click, PictureEdit27.Click, PictureEdit26.Click, PictureEdit25.Click, PictureEdit24.Click, PictureEdit23.Click, PictureEdit22.Click, PictureEdit21.Click, PictureEdit20.Click, PictureEdit2.Click, PictureEdit19.Click, PictureEdit18.Click, PictureEdit17.Click, PictureEdit16.Click, PictureEdit15.Click, PictureEdit14.Click, PictureEdit13.Click, PictureEdit12.Click, PictureEdit11.Click, PictureEdit10.Click, PictureEdit1.Click
        Select Case CType(sender, DevExpress.XtraEditors.PictureEdit).Tag
            Case "1"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._1)
            Case "2"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._2)
            Case "3"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._3)
            Case "4"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._4)
            Case "5"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._5)
            Case "6"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._6)
            Case "7"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._7)
            Case "8"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._8)
            Case "9"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._9)
            Case "10"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._10)
            Case "11"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._11)
            Case "12"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._12)
            Case "13"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._13)
            Case "14"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._14)
            Case "15"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._15)
            Case "16"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._16)
            Case "17"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._17)
            Case "18"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._18)
            Case "19"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._19)
            Case "20"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._20)
            Case "21"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._21)
            Case "22"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._22)
            Case "23"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._23)
            Case "24"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._24)
            Case "25"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._25)
            Case "26"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._26)
            Case "27"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._27)
            Case "28"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._28)
            Case "29"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._29)
            Case "30"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._30)
            Case "31"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._31)
            Case "32"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._32)
            Case "33"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._33)
            Case "33"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._34)
            Case "35"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._35)
            Case "36"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._36)
            Case "37"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._37)
            Case "38"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._38)
            Case "39"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._39)
            Case "40"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._40)
            Case "41"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._41)
            Case "42"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._42)
            Case "43"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._43)
            Case "44"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._44)
            Case "45"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._45)
            Case "46"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._46)
            Case "47"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._47)
            Case "49"
                INDtxtMessage.Document.Images.Insert(INDtxtMessage.Document.CaretPosition, My.Resources.EmoticonsResources._49)
        End Select
    End Sub

#End Region

#Region "Propiedades"
    ''' <summary>
    ''' obtiene o establece el listado de mensajes
    ''' </summary>
    ''' <remarks></remarks>
    Private _Messages As List(Of INDmessage)
    Public Property Messages As List(Of INDmessage)
        Get
            Return _Messages
        End Get
        Set(value As List(Of INDmessage))
            _Messages = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del usuario con el que se tiene la conversacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _UserCode As String
    Public Property UserCode As String
        Get
            Return _UserCode
        End Get
        Set(value As String)
            _UserCode = value
        End Set
    End Property
#End Region


End Class