Imports System.Windows.Forms

Public Class FrmTransparentPopUp

#Region "Fields"

    ''' <summary>
    ''' Encapsula el formulario a mostrar
    ''' </summary>
    Private _insideForm As System.Windows.Forms.Form
    ''' <summary>
    ''' Variable para controlar tamaño del modal
    ''' </summary>
    Private _isInfoMessage As Boolean

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el formulario a mostrar
    ''' </summary>
    ''' <value>Formulario a mostrar</value>
    ''' <returns>El formulario a mostrar</returns>
    Public Property InsideForm As System.Windows.Forms.Form
        Get
            Return Me._insideForm
        End Get
        Set(value As System.Windows.Forms.Form)
            If value IsNot Nothing Then
                Me._insideForm = value
                value.Parent = Me
            End If
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._insideForm = Nothing
    End Sub

    Public Sub New(ByVal insideForm As System.Windows.Forms.Form, Optional ByVal isInfoMessage As Boolean = True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._insideForm = insideForm
        Me._isInfoMessage = isInfoMessage
    End Sub

#End Region

#Region "Methods"

    'Public Overloads Function ShowDialog(owner As IWin32Window) As DialogResult
    '    Return DialogResult.Abort
    'End Function

    '<Obsolete("No se debe llamar sin parámetro")>
    'Public Overloads Function ShowDialog() As DialogResult
    '    Return DialogResult.Abort
    'End Function
#End Region

#Region "Handlers"

    Private Sub FrmTransparent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me._insideForm IsNot Nothing Then
            Me.Bounds = Screen.FromControl(Me).WorkingArea
            Me.WindowState = FormWindowState.Normal
            Me.MaximumSize = Screen.FromControl(Me).WorkingArea.Size
            Me.MinimumSize = Screen.FromControl(Me).WorkingArea.Size
            Me.Location = Screen.FromControl(Me).WorkingArea.Location
            If Me._insideForm.Owner Is Nothing Then
                Me._insideForm.Owner = Me.Owner
            End If
            If Me._isInfoMessage Then
                Me._insideForm.Size = New Drawing.Size(Me.Size.Width, Me._insideForm.Size.Height)
            End If
            Me.DialogResult = Me._insideForm.ShowDialog()
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.Abort
            Me.Close()
        End If
    End Sub

#End Region

End Class