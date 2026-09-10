#Region "Imports"

Imports System.Globalization
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.LookAndFeel
Imports DevExpress.Skins
Imports Presentation.Resources
Imports Infrastructure.CrossCutting.Base.Window
Imports Utils = Infrastructure.CrossCutting.Base.Window.Utils

#End Region

Public Class FrmMessageException
    Implements ISujeto

#Region "ISujeto"
    Dim indigo As MDISingleInstance = MDISingleInstance.Instance

    Public Sub Desregistrar(objObservador As IObservador) Implements ISujeto.Desregistrar
        indigo.ObserversList.Remove(objObservador)
    End Sub

    Public Sub Notificar(ByVal Mensaje As String, ByVal Tipo As MessageType, Optional form As Form = Nothing) Implements ISujeto.Notificar

    End Sub

    Public Sub NotificationCenter(Title As String, Message As String, TypeMessage As MessageType, MessageDate As Date, ByVal AditionalMessage As String) Implements ISujeto.NotificationCenter
        If indigo.ObserversList.Count = 0 Then Exit Sub
        For Each objObservador As IObservador In indigo.ObserversList
            objObservador.NotificationCenter(Title, Message, TypeMessage, MessageDate, AditionalMessage)
        Next
    End Sub

    Public Sub Registrar(objObservador As IObservador) Implements ISujeto.Registrar
        indigo.ObserversList.Add(objObservador)
    End Sub
#End Region

#Region "Consts"

    ''' <summary>
    ''' Maximo de caracteres aceptados por el label
    ''' </summary>
    Private Const MAX_CANT_CHARACTERS As Integer = 345

#End Region

#Region "Fields"

    ''' <summary>
    ''' Encapsula la excepcion a mostrar
    ''' </summary>
    Private _exception As Exception
    ''' <summary>
    ''' Encapsula la cultura usada por la aplicacion
    ''' </summary>
    Private _culture As CultureInfo

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna la excepcion a mostrar
    ''' </summary>
    ''' <value>Excepcion a mostrar</value>
    ''' <returns>Excepcion a mostrar</returns>
    Public Property Ex As Exception
        Get
            Return Me._exception
        End Get
        Set(value As Exception)
            Me._exception = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la cultura usada por la aplicacion
    ''' </summary>
    ''' <value>Cultura</value>
    ''' <returns>La cultura</returns>
    Public Property Culture As CultureInfo
        Get
            Return Me._culture
        End Get
        Set(value As CultureInfo)
            Me._culture = value
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me._culture = Nothing
        Me._exception = Nothing
        Me.INDlblMessage.Text = Me.GetLanguageResource(Me.INDlblMessage.Name)
    End Sub

#End Region

#Region "Handlers"

    Private Sub INDbtnCancel_Click(sender As Object, e As EventArgs) Handles INDbtnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub INDbtnDetails_Click(sender As Object, e As EventArgs) Handles INDbtnDetails.Click
        If Me.INDbtnDetails.Tag.ToString().Equals("0") Then
            If Me._exception IsNot Nothing Then
                Me.INDbtnDetails.Tag = "1"
                If Me._exception.Message IsNot Nothing Then
                    If Me._exception.Message.Trim().Length > MAX_CANT_CHARACTERS Then
                        Me.INDlblMessage.Text = Me._exception.Message.Trim().Substring(0, Me._exception.Message.Trim().Length - 3) & "..."
                    Else
                        Me.INDlblMessage.Text = Me._exception.Message.Trim()
                    End If
                    Me.INDbtnDetails.Text = Me.GetLanguageResource(Me.INDbtnDetails.Name & "2")
                End If
            End If
        Else
            Me.INDbtnDetails.Tag = "0"
            Me.INDlblMessage.Text = Me.GetLanguageResource(Me.INDlblMessage.Name)
            Me.INDbtnDetails.Text = Me.GetLanguageResource(Me.INDbtnDetails.Name & "1")
        End If
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        setStyleTheme()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Le da estilo al formulario seg�n el tema
    ''' </summary>
    Private Sub setStyleTheme()
        If UserLookAndFeel.Default.ActiveSkinName.Equals(Infrastructure.CrossCutting.Base.Utils.DEFAULT_SKIN_NAME) Then
            Me.Appearance.BackColor = Drawing.Color.FromArgb(0, 148, 223)
            Me.Appearance.BackColor2 = Drawing.Color.FromArgb(0, 148, 223)
            Me.LookAndFeel.Style = LookAndFeelStyle.Office2003

            Me.INDbtnDetails.Appearance.BackColor = Drawing.Color.FromArgb(0, 148, 223)
            Me.INDbtnDetails.Appearance.BackColor2 = Drawing.Color.FromArgb(0, 148, 223)
            Me.INDbtnDetails.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
            Me.INDbtnDetails.Appearance.BorderColor = Drawing.Color.White
            Me.INDbtnDetails.ForeColor = Drawing.Color.White
            Me.INDbtnDetails.LookAndFeel.Style = ActiveLookAndFeelStyle.UltraFlat

            Me.INDbtnCancel.Appearance.BackColor = Drawing.Color.FromArgb(0, 148, 223)
            Me.INDbtnCancel.Appearance.BackColor2 = Drawing.Color.FromArgb(0, 148, 223)
            Me.INDbtnCancel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
            Me.INDbtnCancel.Appearance.BorderColor = Drawing.Color.White
            Me.INDbtnCancel.ForeColor = Drawing.Color.White
            Me.INDbtnCancel.LookAndFeel.Style = ActiveLookAndFeelStyle.UltraFlat

            Me.INDlblMessage.ForeColor = Drawing.Color.White
        Else
            Me.INDpceLogo.Image = ThemeResourceManager.GetIconMessageErrorIndigo(UserLookAndFeel.Default.ActiveSkinName)
            Dim skin = CommonSkins.GetSkin(UserLookAndFeel.Default.ActiveLookAndFeel)
            Dim colr As Drawing.Color = skin(CommonSkins.SkinButton).Color.BackColor
            If colr <> Drawing.Color.Transparent Then
                Me.Appearance.BackColor = colr
                Me.Appearance.BackColor2 = colr
            End If
        End If
    End Sub

    ''' <summary>
    ''' Captura la tecla ESCAPE en cualquier parte del formulario y ejecuta el cerrado
    ''' </summary>
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            Me.INDbtnCancel_Click(Me.INDbtnCancel, New EventArgs())
        ElseIf keyData = System.Windows.Forms.Keys.Enter Then
            Me.INDbtnDetails_Click(Me.INDbtnDetails, New EventArgs())
        ElseIf My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Keyboard.AltKeyDown Then
                If My.Computer.Keyboard.ShiftKeyDown Then
                    If keyData = 458787 Then
                        If Me._exception IsNot Nothing AndAlso Me._exception.StackTrace IsNot Nothing Then
                            Dim strBuilder As StringBuilder = New StringBuilder()
                            ReadException(Me._exception, strBuilder)
                            'Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                            Dim path As String = Window.Utils.LocalFolder()
                            Using writer As StreamWriter = New StreamWriter(path + "\ExceptionCrystal.txt")
                                writer.Write(strBuilder.ToString())
                            End Using
                            Dim psi = New ProcessStartInfo()
                            psi.UseShellExecute = True
                            psi.FileName = path + "\ExceptionCrystal.txt"
                            Process.Start(psi)
                            ''DevExpress.XtraEditors.XtraMessageBox.Show(Me._exception.StackTrace, "Exception...!!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
                        End If
                    End If
                End If
            End If
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    ''' <summary>
    ''' Funcion que lee una excepcion y la devuelve en un string
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <param name="srtException"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub ReadException(ex As Exception, ByRef strException As StringBuilder)
        strException.Append("Exception")
        strException.Append("Message " + ex.Message)
        strException.Append("StackTrace " + ex.StackTrace)
        strException.Append(vbCrLf)
        If ex.InnerException IsNot Nothing Then
            ReadException(ex.InnerException, strException)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene un recurso de idioma segun la cultura configurada
    ''' </summary>
    ''' <param name="resource">Recurso a consultar</param>
    ''' <returns>Texto del recurso</returns>
    Private Function GetLanguageResource(ByVal resource As String) As String
        Dim languaje As String = "es-CO"
        If Me._culture IsNot Nothing Then
            languaje = Me._culture.Name
        End If
        Select Case languaje
            Case "en-US"
                Return My.Resources.en_US.ResourceManager.GetString(resource)
            Case Else
                Return My.Resources.es_CO.ResourceManager.GetString(resource)
        End Select
    End Function

    ''' <summary>
    ''' Eviar notificacion al MDI
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmMessageException_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim languaje As String = "es-CO"
        If Me._culture IsNot Nothing Then
            languaje = Me._culture.Name
        End If
        Select Case languaje
            Case "en-US"
                NotificationCenter(My.Resources.en_US.ResourceManager.GetString("Exception"), Me._exception.Message, MessageType.Errores, Date.Now, "")
            Case Else
                NotificationCenter(My.Resources.es_CO.ResourceManager.GetString("Exception"), Me._exception.Message, MessageType.Errores, Date.Now, "")
        End Select
    End Sub

#End Region

End Class