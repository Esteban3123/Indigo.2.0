Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.LookAndFeel
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Window
Imports Presentation.Resources
Imports Utils = Infrastructure.CrossCutting.Base.Window.Utils

Public Class FrmFlyoutIndigo

    Private _dialogResultESCAPE As System.Windows.Forms.DialogResult

    Private _dialogResultENTER As System.Windows.Forms.DialogResult

    ''' <summary>
    ''' Encapsula la excepcion a mostrar
    ''' </summary>
    Private _exception As Exception

    Public Sub New(ByVal Mensaje As String, ByVal Botones As Botones, ByVal Icono As MessageType, ByVal _image As Image, Optional ex As Exception = Nothing)
        InitializeComponent()
        Me.BringToFront()
        FlyoutPanel1.OwnerControl = Me
        _exception = ex

        If ex IsNot Nothing Then
            INDSmbYes.Tag = "0"
            INDSmbYes.Text = My.Resources.Base_es_CO.ResourceManager.GetString("INDbtnDetails1")
            INDSmbNo.Text = "Cerrar"
            INDSmbClose.Visible = False
            INDLciClose.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me._dialogResultENTER = System.Windows.Forms.DialogResult.Yes
            Me._dialogResultESCAPE = System.Windows.Forms.DialogResult.Cancel
            Mensaje = My.Resources.Base_es_CO.ResourceManager.GetString("INDlblMessage")
            labelControl1.Text = Mensaje
        Else
            Select Case Botones
                Case Botones.SiNo
                    INDSmbClose.Visible = False
                    INDLciClose.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me._dialogResultENTER = System.Windows.Forms.DialogResult.Yes
                    Me._dialogResultESCAPE = System.Windows.Forms.DialogResult.No
                Case Botones.AceptarCancelar
                    INDSmbYes.Text = "Aceptar"
                    INDSmbNo.Text = "Cancelar"
                    INDSmbClose.Visible = False
                    INDLciClose.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me._dialogResultENTER = System.Windows.Forms.DialogResult.Yes
                    Me._dialogResultESCAPE = System.Windows.Forms.DialogResult.Cancel
                Case Botones.Cancelar
                    INDSmbYes.Visible = False
                    INDSmbNo.Visible = False
                    INDLciYes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciNot.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me._dialogResultENTER = System.Windows.Forms.DialogResult.Cancel
                    Me._dialogResultESCAPE = System.Windows.Forms.DialogResult.Cancel
                Case Botones.Continuar
                    INDSmbYes.Text = "Continuar"
                    INDSmbClose.Visible = False
                    INDSmbNo.Visible = False
                    INDLciNot.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciClose.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me._dialogResultENTER = System.Windows.Forms.DialogResult.Yes
                    Me._dialogResultESCAPE = System.Windows.Forms.DialogResult.Cancel
                Case Else

            End Select
            labelControl1.Text = Mensaje
        End If

        pictureEdit1.Image = My.Resources.ResourceManager.GetObject("BannerMensajeIzq")
        'If _image IsNot Nothing Then
        '    pictureEdit2.Image = _image
        'Else
        '    pictureEdit2.Image = ThemeResourceManager.GetIconMessageIndigo(UserLookAndFeel.Default.ActiveSkinName, Icono, True)
        'End If
        ChangeControlAppearance(Mensaje)
        PictureEdit3.Image = My.Resources.ResourceManager.GetObject("Banner_angulo")
    End Sub

    Public Sub ChangeControlAppearance(ByVal mensaje As String)
        If mensaje.Length > 100 Then
            labelControl1.Appearance.Font = New Font("Segoe UI Light", 18)
            labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Else
            labelControl1.Appearance.Font = New Font("Segoe UI Light", 26)
            labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        End If
    End Sub

    Public Sub Frm_Shown(Sender As Object, e As EventArgs) Handles Me.Shown
        FlyoutPanel1.ShowPopup()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            Me.DialogResult = Me._dialogResultESCAPE
        ElseIf keyData = System.Windows.Forms.Keys.Enter Then
            If INDSmbYes.Tag = "0" OrElse INDSmbYes.Tag = "1" Then
                INDSmbYes_Click(INDSmbYes, New EventArgs())
            Else
                Me.DialogResult = Me._dialogResultENTER
            End If
        ElseIf My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Keyboard.AltKeyDown Then
                If My.Computer.Keyboard.ShiftKeyDown Then
                    If keyData = 458787 Then
                        If Me._exception IsNot Nothing AndAlso Me._exception.StackTrace IsNot Nothing Then
                            Dim strBuilder As StringBuilder = New StringBuilder()
                            ReadException(Me._exception, strBuilder)
                            Dim path As String = Window.Utils.LocalFolder()
                            Using writer As StreamWriter = New StreamWriter(path + "\ExceptionVieCloudPlatform.txt")
                                writer.Write(strBuilder.ToString())
                            End Using
                            Dim psi = New ProcessStartInfo()
                            psi.UseShellExecute = True
                            psi.FileName = path + "\ExceptionVieCloudPlatform.txt"
                            Process.Start(psi)
                            ''DevExpress.XtraEditors.XtraMessageBox.Show(Me._exception.StackTrace, "Exception...!!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
                        End If
                    End If
                End If
            ElseIf keyData = (Keys.Control Or Keys.C) Then
                If Me._exception IsNot Nothing AndAlso Me._exception.StackTrace IsNot Nothing Then
                    Dim strBuilder As StringBuilder = New StringBuilder()
                    ReadException(Me._exception, strBuilder)
                    Clipboard.SetText(strBuilder.ToString())
                Else
                    Clipboard.SetText(labelControl1.Text)
                End If
            End If
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub INDSmbClose_Click(sender As Object, e As EventArgs) Handles INDSmbClose.Click
        Me.DialogResult = Me._dialogResultENTER
        FlyoutPanel1.HidePopup()
        Me.Close()
    End Sub

    Private Sub INDSmbYes_Click(sender As Object, e As EventArgs) Handles INDSmbYes.Click
        Dim _message As String = String.Empty
        If INDSmbYes.Tag = "0" Then
            If Me._exception IsNot Nothing Then
                INDSmbYes.Tag = "1"
                _message = Me._exception.Message.Trim()
                If Me._exception.Message IsNot Nothing Then
                    ChangeControlAppearance(_message)
                    Me.labelControl1.Text = _message
                    INDSmbYes.Text = My.Resources.Base_es_CO.ResourceManager.GetString("INDbtnDetails2")
                End If
            End If
        ElseIf INDSmbYes.Tag = "1" Then
            INDSmbYes.Tag = "0"
            _message = My.Resources.Base_es_CO.ResourceManager.GetString("INDlblMessage")
            ChangeControlAppearance(_message)
            Me.labelControl1.Text = _message
            INDSmbYes.Text = My.Resources.Base_es_CO.ResourceManager.GetString("INDbtnDetails1")
        Else
            Me.DialogResult = Me._dialogResultENTER
            FlyoutPanel1.HidePopup()
            Me.Close()
        End If
    End Sub

    Private Sub INDSmbNo_Click(sender As Object, e As EventArgs) Handles INDSmbNo.Click
        Me.DialogResult = Me._dialogResultESCAPE
        FlyoutPanel1.HidePopup()
        Me.Close()
    End Sub

    ''' <summary>
    ''' Funcion que lee una excepcion y la devuelve en un string
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <param name="strException"></param>
    Public Sub ReadException(ex As Exception, ByRef strException As StringBuilder)
        strException.Append("Exception")
        strException.Append("Message " + ex.Message)
        strException.Append("StackTrace " + ex.StackTrace)
        strException.Append(vbCrLf)
        If ex.InnerException IsNot Nothing Then
            ReadException(ex.InnerException, strException)
        End If
    End Sub
End Class