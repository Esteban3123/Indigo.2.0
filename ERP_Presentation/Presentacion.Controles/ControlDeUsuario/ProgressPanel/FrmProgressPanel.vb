Public Class FrmProgressPanel

#Region "Properties & Variables"
    Private _totalItems As Integer
    ''' <summary>
    ''' Propiedad obtiene el total de items a procesar
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TotalItems As Integer
        Get
            Return _totalItems
        End Get
    End Property

    Private _currentItem As Integer
    ''' <summary>
    ''' Propiedad que obtiene el item Actual que se esta procesando
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CurrentItem As Integer
        Get
            Return _currentItem
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece la descripcion de la barra de progreso
    ''' </summary>
    ''' <returns></returns>
    Private Property LabelProgressBar As String
        Get
            Return Me.INDLbcDescription.Text
        End Get
        Set(value As String)
            Me.INDLbcDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el nombre del layout de la barra de progreso
    ''' </summary>
    ''' <returns></returns>
    Public Property LayoutControlName As String
        Get
            Return Me.INDLciProgress.Text
        End Get
        Set(value As String)
            Me.INDLciProgress.Text = value
        End Set
    End Property
#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="totalItems"></param>
    Public Sub New(totalItems As Integer)
        Me._totalItems = totalItems
        Me._currentItem = 0
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo que se encarga de actualizar el progreso de la barra y la descripcion
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="text"></param>
    Public Sub UpdateProgressBar(value As Integer, Optional text As String = Nothing)
        If value > TotalItems Then
            Exit Sub
        End If
        Dim label = $"Procesando {value} de {_totalItems} Items..."
        If Not String.IsNullOrEmpty(text) Then
            label &= $" ({text})"
        End If
        Me.LabelProgressBar = label
        Me.INDPbcProgress.Position = value
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' evento Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmProgressPanel_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Location = New Point((Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2,
                            (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) \ 2)
        Me.INDPbcProgress.Properties.Maximum = Me._totalItems
        Me.LabelProgressBar = $"Procesando {_currentItem} de {_totalItems} Items..."
    End Sub

    ''' <summary>
    ''' evento closed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmProgressPanel_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
#End Region

End Class