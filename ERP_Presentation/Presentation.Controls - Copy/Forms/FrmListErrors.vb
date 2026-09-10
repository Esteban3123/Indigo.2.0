Public Class FrmListErrors 

#Region "Constructor"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmListErrors"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmListErrors"/> class.
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Public Sub New(ByVal errors As List(Of String))
        InitializeComponent()
        ErrorList = errors
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmListErrors"/> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    Public Sub New(ByVal message As List(Of Tuple(Of String, Integer)))
        InitializeComponent()
        ListMessage = message
    End Sub

#End Region

#Region "Properties and Variables"

    ''' <summary>
    ''' Gets or sets the title.
    ''' </summary>
    ''' <value>
    ''' The title.
    ''' </value>
    Public Property Title As String
        Get
            Return Me.Text
        End Get
        Set(value As String)
            Me.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de errores
    ''' </summary>
    ''' <value>
    ''' The error list.
    ''' </value>
    Public Property ErrorList As List(Of String)
        Get
            Return CType(INDgcErrorList.DataSource, List(Of String))
        End Get
        Set(value As List(Of String))
            INDgcErrorList.DataSource = value.Select(Function(x) New ItemMessage With {.Message = x, .Icon = 0}).ToList()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de errores si envio mensajes de error y confirmacion
    ''' </summary>
    ''' <value>
    ''' The list message.
    ''' </value>
    Public Property ListMessage As List(Of Tuple(Of String, Integer))
        Get
            Return CType(INDgcErrorList.DataSource, List(Of Tuple(Of String, Integer)))
        End Get
        Set(value As List(Of Tuple(Of String, Integer)))
            Dim datasource As List(Of ItemMessage) = New List(Of ItemMessage)
            For Each item As Tuple(Of String, Integer) In value
                If item.Item2 = eMessageType.Confirm Then
                    datasource.Add(New ItemMessage With {.Message = item.Item1, .Icon = 1})
                ElseIf item.Item2 = eMessageType.Errors Then
                    datasource.Add(New ItemMessage With {.Message = item.Item1, .Icon = 0})
                ElseIf item.Item2 = eMessageType.Warning Then
                    datasource.Add(New ItemMessage With {.Message = item.Item1, .Icon = 2})
                End If
            Next
            datasource.OrderByDescending(Function(x) x.Icon)
            INDgcErrorList.DataSource = datasource
        End Set
    End Property

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Configuraciones iniciales
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmListErrors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridControl1.RefreshGrid(INDgcErrorList)
    End Sub
#End Region

#Region "KeyDown"
    Private Sub FrmListErrors_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#End Region
    
End Class

Public Class ItemMessage
    Property Message As String
    Property Icon As Integer
End Class

Public Enum eMessageType
    Confirm = 1
    Errors = 2
    Warning = 3
End Enum