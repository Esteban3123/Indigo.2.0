#Region "Imports"

Imports System.Text
Imports System.IO
Imports System.Xml
Imports System.Threading.Tasks
Imports Infrastructure.CrossCutting.MessageStoreView.CrossThreadExtentions
Imports Infrastructure.CrossCutting.MessageStore
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class FrmMain

#Region "Fields"

    ''' <summary>
    ''' Ruta al almacén de mensajes
    ''' </summary>
    Private _pathStore As String

    ''' <summary>
    ''' Objeto ordenador
    ''' </summary>
    Private _lvwColumnSorter As ListViewColumnSorter

    ''' <summary>
    ''' Lista de mensajes de indexación
    ''' </summary>
    Private _indexingMessages As List(Of IMessage)

    ''' <summary>
    ''' Lista de mensajes de auditoria
    ''' </summary>
    Private _auditingMessages As List(Of IMessage)

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer Or ControlStyles.CacheText, True)
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se controla la salida de la aplicación
    ''' </summary>
    Private Sub MnuExit_Click(sender As Object, e As EventArgs) Handles MnuExit.Click
        End
    End Sub

    ''' <summary>
    ''' Aqui se abre un almacén de mensajes
    ''' </summary>
    Private Async Sub MnuOpeStore_Click(sender As Object, e As EventArgs) Handles MnuOpeStore.Click
        If FolderBrowserDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            Me.IsLoading(True, "Abriendo almacén...")
            Await Me.OpenStoreAsync(FolderBrowserDialog.SelectedPath)
            Me.IsLoading()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se realiza las tareas necesaria cuando la aplicación esta cargando
    ''' </summary>
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0} - Versión: {1}", My.Application.Info.Title, My.Application.Info.Version.ToString())
        Me._lvwColumnSorter = New ListViewColumnSorter()
        Me.LvwMessages.ListViewItemSorter = Me._lvwColumnSorter
        Me.SpcRight.Panel2Collapsed = True
        Me.PnlMessageView.Visible = False
    End Sub

    ''' <summary>
    ''' Aqui se muestra los mensajes en el almacén seleccionado
    ''' </summary>
    Private Async Sub TvwStores_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TvwStores.NodeMouseClick
        Me.IsLoading(True, "Cargando datos...")
        Await Me.LoadNodeToListViewAsync(e.Node)
        Me.IsLoading()
        sender.Focus()
    End Sub

    ''' <summary>
    ''' Aqui se muestra el detalle de la selección
    ''' </summary>
    Private Async Sub LvwMessages_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles LvwMessages.ItemSelectionChanged
        If TypeOf e.Item.Tag Is IMessage Then 'Mensaje
            Await Me.LoadMessageAsync(DirectCast(e.Item.Tag, IMessage))
        Else 'Almacenes
            Await Me.LoadMessagesFromStoreSelectedAsync(e.Item.Tag)
        End If
    End Sub

    ''' <summary>
    ''' Aqui cerramos el almacén de mensajes seleccionado
    ''' </summary>
    Private Async Sub MnuCloseStore_Click(sender As Object, e As EventArgs) Handles MnuCloseStore.Click
        Await Me.CloseStoreAsync()
    End Sub

    ''' <summary>
    ''' Aqui se refresca la lista de almacenes
    ''' </summary>
    Private Async Sub TvwStores_KeyDown(sender As Object, e As KeyEventArgs) Handles TvwStores.KeyDown
        If e.KeyCode = Keys.F5 AndAlso Me._pathStore IsNot Nothing Then
            Me.IsLoading(True, "Cargando datos...")
            If Me.TvwStores.Nodes(0).IsSelected Then
                Await Me.OpenStoreAsync(Me._pathStore)
            Else
                For Each n As TreeNode In Me.TvwStores.Nodes(0).Nodes
                    If n.IsSelected Then
                        Await Me.LoadNodeToListViewAsync(n, True)
                        Exit For
                    End If
                Next
            End If
            Me.IsLoading()
            sender.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Ordena la columna
    ''' </summary>
    Private Sub LvwMessages_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles LvwMessages.ColumnClick
        If e.Column = Me._lvwColumnSorter.SortColumn Then
            If Me._lvwColumnSorter.Order = SortOrder.Ascending Then
                Me._lvwColumnSorter.Order = SortOrder.Descending
            Else
                Me._lvwColumnSorter.Order = SortOrder.Ascending
            End If
        Else
            Me._lvwColumnSorter.SortColumn = e.Column
            Me._lvwColumnSorter.Order = SortOrder.Ascending
        End If

        Me.LvwMessages.Sort()
    End Sub

    ''' <summary>
    ''' Aqui se muestra el error del mensaje
    ''' </summary>
    Private Sub BtnMessageError_Click(sender As Object, e As EventArgs) Handles BtnMessageError.Click
        If Me.BtnMessageError.Tag IsNot Nothing Then
            'Dim pathFile As String = Path.Combine(Path.GetTempPath(), "MessageError.txt")
            Dim pathFile As String = Path.Combine(Utils.TemporalFolder(), "MessageError.txt")
            File.WriteAllText(pathFile, Me.BtnMessageError.Tag.ToString())
            Dim psi As New ProcessStartInfo(pathFile)
            psi.UseShellExecute = True
            Process.Start(psi)
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Convierte el tamaño del archivo a una unidad mayor
    ''' </summary>
    ''' <param name="size">Tamaño del archivo</param>
    ''' <returns>Tamaño en unidades mayores</returns>
    Private Function ConverSizeToString(ByVal size As UInteger) As String
        Dim unit As String = " Bytes"
        If size <= 1024 Then
            Return (size & " Bytes")
        End If
        Dim res = (size / 1024) 'Convierte en KB
        unit = " KB"
        If res > 1024 Then
            res = (res / 1024) 'Convierte en MB
            unit = " MB"
        End If
        Return (Math.Round(res, 1) & unit)
    End Function

    ''' <summary>
    ''' Carga los mensajes del almacén seleccionado de forma asíncrona
    ''' </summary>
    ''' <param name="typeStore">Tipo de configuración del almacén seleccionado</param>
    Private Function LoadMessagesFromStoreSelectedAsync(ByVal typeStore As ConfigStoreType) As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.LoadMessagesFromStoreSelected(typeStore)
                                     End Sub)
    End Function

    ''' <summary>
    ''' Carga los mensajes del almacén seleccionado
    ''' </summary>
    ''' <param name="typeStore">Tipo de configuración del almacén seleccionado</param>
    Private Sub LoadMessagesFromStoreSelected(ByVal typeStore As ConfigStoreType)
        Me.SpcRight.SafeInvoke(Sub(p) p.Panel2Collapsed = True)
        Me.PnlMessageView.SafeInvoke(Sub(p) p.Visible = False)
        Me.LvwMessages.SafeInvoke(Sub(p) p.Items.Clear())
        Me.LvwMessages.SafeInvoke(Sub(p) p.View = View.Details)
        Me.EnsureListInternalLoaded(typeStore)
        For Each m As IMessage In If(typeStore = ConfigStoreType.IndexingStore, Me._indexingMessages, Me._auditingMessages)
            Dim item As ListViewItem = Me.LvwMessages.SafeInvoke(Of ListViewItem)(Function(p) p.Items.Add(New ListViewItem(New String() {m.Title.Substring(0, m.Title.LastIndexOf(".")).Trim(), New DateTime(m.TimeStamp.Ticks).ToLocalTime(), Me.ConverSizeToString(m.Size)})))
            Me.LvwMessages.SafeInvoke(Sub(l) l.Items(Me.LvwMessages.Items.Count - 1).ImageIndex = 2)
            Me.LvwMessages.SafeInvoke(Sub(l) l.Items(Me.LvwMessages.Items.Count - 1).Tag = m)
        Next
    End Sub

    ''' <summary>
    ''' Asegura que las listas internas de mensajes estén cargadas
    ''' </summary>
    ''' <param name="typeStore">Tipo de configuración</param>
    Private Sub EnsureListInternalLoaded(ByVal typeStore As ConfigStoreType)
        If typeStore = ConfigStoreType.IndexingStore AndAlso Me._indexingMessages Is Nothing Then
            Me.LoadMessagesToInternalList(typeStore)
        ElseIf typeStore = ConfigStoreType.AuditingStore AndAlso Me._auditingMessages Is Nothing Then 'Auditoria
            Me.LoadMessagesToInternalList(typeStore)
        End If
    End Sub

    ''' <summary>
    ''' Carga los mensajes correspondientes al tipo de configuración
    ''' en la lista interna
    ''' </summary>
    ''' <param name="configType">Tipo de configuración</param>
    Private Sub LoadMessagesToInternalList(ByVal configType As ConfigStoreType, Optional ByVal isReload As Boolean = False)
        If configType = ConfigStoreType.IndexingStore Then
            If Me._indexingMessages Is Nothing OrElse isReload Then
                Me._indexingMessages = New List(Of IMessage)()
            End If
        Else 'Auditoria
            If Me._auditingMessages Is Nothing OrElse isReload Then
                Me._auditingMessages = New List(Of IMessage)()
            End If
        End If
        Using store As New MessageStore.MessageStore(Me.CreateConfigStore(configType))
            For Each m As IMessage In store.ListMessages()
                If configType = ConfigStoreType.IndexingStore Then
                    Me._indexingMessages.Add(m)
                Else 'Auditoria
                    Me._auditingMessages.Add(m)
                End If
            Next
        End Using
    End Sub

    ''' <summary>
    ''' Carga un mensaje en el visor de forma asíncrona
    ''' </summary>
    ''' <param name="mess">Mensaje a cargar</param>
    Private Function LoadMessageAsync(ByVal mess As IMessage) As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.LoadMessage(mess)
                                     End Sub)
    End Function

    ''' <summary>
    ''' Carga un mensaje en el visor
    ''' </summary>
    ''' <param name="mess">Mensaje a cargar</param>
    Private Sub LoadMessage(ByVal mess As IMessage)
        Me.SpcRight.SafeInvoke(Sub(p) p.Panel2Collapsed = False)
        Me.PnlMessageView.SafeInvoke(Sub(p) p.Visible = True)
        Me.TxtIdFile.SafeInvoke(Sub(t) t.Text = mess.IdFile.ToString())
        Me.TxtTitle.SafeInvoke(Sub(t) t.Text = mess.Title.Substring(0, mess.Title.LastIndexOf(".")).Trim())
        Me.TxtTimeStamp.SafeInvoke(Sub(t) t.Text = New DateTime(mess.TimeStamp.Ticks).ToLocalTime())
        Me.TxtSize.SafeInvoke(Sub(t) t.Text = Me.ConverSizeToString(mess.Size))
        Me.TxtBody.SafeInvoke(Sub(t) t.Text = mess.Body.GetXml())
        If mess.HasError Then
            Me.BtnMessageError.SafeInvoke(Sub(t) t.Tag = mess.MessageError)
            Me.BtnMessageError.SafeInvoke(Sub(t) t.Visible = True)
            Me.BtnDeleteMarkError.SafeInvoke(Sub(t) t.Visible = True)
        Else
            Me.BtnMessageError.SafeInvoke(Sub(t) t.Tag = Nothing)
            Me.BtnMessageError.SafeInvoke(Sub(t) t.Visible = False)
            Me.BtnDeleteMarkError.SafeInvoke(Sub(t) t.Visible = False)
        End If
    End Sub

    ''' <summary>
    ''' Indica si la aplicación se encuentra desempeñando alguna acción
    ''' </summary>
    ''' <param name="loading">Valor que indica si se inicia la carga</param>
    ''' <param name="action">Nombre de la acción que se esta desempeñando</param>
    Private Sub IsLoading(Optional ByVal loading As Boolean = False, Optional ByVal action As String = "")
        If loading Then
            Me.SafeInvoke(Sub(f) f.Cursor = Cursors.WaitCursor)

            Me.TvwStores.SafeInvoke(Sub(t) t.Enabled = False)
            Me.LvwMessages.SafeInvoke(Sub(t) t.Enabled = False)
            Me.MnuMain.SafeInvoke(Sub(t) t.Enabled = False)

            Me.StatusBarMain.SafeInvoke(Sub(s) DirectCast(s.Items(Me.PgbLoading.Name), ToolStripProgressBar).MarqueeAnimationSpeed = 100)
            Me.StatusBarMain.SafeInvoke(Sub(s) DirectCast(s.Items(Me.PgbLoading.Name), ToolStripProgressBar).Style = ProgressBarStyle.Marquee)
            Me.StatusBarMain.SafeInvoke(Sub(s) DirectCast(s.Items(Me.PgbLoading.Name), ToolStripProgressBar).Visible = True)
            Me.StatusBarMain.SafeInvoke(Sub(s) DirectCast(s.Items(Me.LblAction.Name), ToolStripLabel).Text = action)
            Me.StatusBarMain.SafeInvoke(Sub(s) DirectCast(s.Items(Me.LblAction.Name), ToolStripLabel).Visible = True)
        Else
            Me.SafeInvoke(Sub(f) f.Cursor = Cursors.Default)

            Me.TvwStores.SafeInvoke(Sub(t) t.Enabled = True)
            Me.LvwMessages.SafeInvoke(Sub(t) t.Enabled = True)
            Me.MnuMain.SafeInvoke(Sub(t) t.Enabled = True)

            Me.StatusBarMain.SafeInvoke(Sub(s) DirectCast(s.Items(Me.PgbLoading.Name), ToolStripProgressBar).MarqueeAnimationSpeed = 0)
            Me.StatusBarMain.SafeInvoke(Sub(s) DirectCast(s.Items(Me.PgbLoading.Name), ToolStripProgressBar).Style = ProgressBarStyle.Continuous)
            Me.StatusBarMain.SafeInvoke(Sub(s) DirectCast(s.Items(Me.PgbLoading.Name), ToolStripProgressBar).Visible = False)
            Me.StatusBarMain.SafeInvoke(Sub(s) DirectCast(s.Items(Me.LblAction.Name), ToolStripLabel).Text = "--")
            Me.StatusBarMain.SafeInvoke(Sub(s) DirectCast(s.Items(Me.LblAction.Name), ToolStripLabel).Visible = False)
        End If
    End Sub

    ''' <summary>
    ''' Carga los nodos de un nodo en la lista de forma asíncrona
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="isReload">Valor opcional que indica si se recarga los datos del almacén</param>
    Private Function LoadNodeToListViewAsync(ByVal node As TreeNode, Optional ByVal isReload As Boolean = False) As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.LoadNodeToListView(node, isReload)
                                     End Sub)
    End Function

    ''' <summary>
    ''' Carga los nodos de un nodo en la lista
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="isReload">Valor opcional que indica si se recarga los datos del almacén</param>
    Private Sub LoadNodeToListView(ByVal node As TreeNode, Optional ByVal isReload As Boolean = False)
        If Not node.Name.Equals("_Root_") Then
            Dim typeStore As ConfigStoreType = DirectCast(node.Tag, ConfigStoreType)
            Me.LvwMessages.SafeInvoke(Sub(v) v.Items.Clear())
            Me.LvwMessages.SafeInvoke(Sub(v) v.View = View.Details)
            If isReload Then
                Me.LoadMessagesToInternalList(typeStore, isReload)
            Else
                Me.EnsureListInternalLoaded(typeStore)
            End If
            For Each m As IMessage In If(typeStore = ConfigStoreType.IndexingStore, Me._indexingMessages, Me._auditingMessages)
                Dim item As ListViewItem = Me.LvwMessages.SafeInvoke(Of ListViewItem)(Function(v) v.Items.Add(New ListViewItem(New String() {m.Title.Substring(0, m.Title.LastIndexOf(".")).Trim(), New DateTime(m.TimeStamp.Ticks).ToLocalTime(), Me.ConverSizeToString(m.Size)})))
                Me.LvwMessages.SafeInvoke(Sub(l) l.Items(Me.LvwMessages.Items.Count - 1).ImageIndex = 2)
                Me.LvwMessages.SafeInvoke(Sub(l) l.Items(Me.LvwMessages.Items.Count - 1).Tag = m)
            Next
            Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(node.Name).Text = "Mensajes de " & If(typeStore = ConfigStoreType.IndexingStore, "Indexación (" & Me._indexingMessages.Count & ")", "Auditoria (" & Me._auditingMessages.Count & ")"))
        Else
            Me.LvwMessages.SafeInvoke(Sub(v) v.Items.Clear())
            Me.LvwMessages.SafeInvoke(Sub(v) v.View = View.Tile)
            For Each n As TreeNode In node.Nodes
                Dim item As ListViewItem = Me.LvwMessages.SafeInvoke(Of ListViewItem)(Function(v) v.Items.Add(n.Text))
                Me.LvwMessages.SafeInvoke(Sub(l) l.Items(Me.LvwMessages.Items.Count - 1).ImageIndex = 1)
                Me.LvwMessages.SafeInvoke(Sub(l) l.Items(Me.LvwMessages.Items.Count - 1).Name = n.Name)
                Me.LvwMessages.SafeInvoke(Sub(l) l.Items(Me.LvwMessages.Items.Count - 1).Tag = n.Tag)
            Next
        End If
        Me.SpcRight.SafeInvoke(Sub(p) p.Panel2Collapsed = True)
        Me.PnlMessageView.SafeInvoke(Sub(p) p.Visible = False)
    End Sub

    ''' <summary>
    ''' Crea la configuración para un almacén de mensajes
    ''' </summary>
    ''' <param name="type">Tipo de almacén</param>
    ''' <returns>Configuración de almacén</returns>
    Private Function CreateConfigStore(ByVal type As ConfigStoreType) As IConfigStore
        Select Case type
            Case ConfigStoreType.AuditingStore
                Return New AuditingConfigStore(Me._pathStore)
            Case ConfigStoreType.IndexingStore
                Return New IndexingConfigStore(Me._pathStore)
            Case Else
                Return Nothing
        End Select
    End Function

    ''' <summary>
    ''' Busca y abre los almacenes de mensajes que existan en la ruta de forma asincrona
    ''' </summary>
    ''' <param name="pathStore">Ruta de los almacenes</param>
    Private Function OpenStoreAsync(ByVal pathStore As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.OpenStore(pathStore)
                                     End Sub)
    End Function

    ''' <summary>
    ''' Busca y abre los almacenes de mensajes que existan en la ruta
    ''' </summary>
    ''' <param name="pathStore">Ruta de los almacenes</param>
    Private Sub OpenStore(ByVal pathStore As String)
        If Directory.Exists(pathStore) Then
            Dim exist As Boolean = False 'Bandera que me indica si se encontro algun almacén
            Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes.Clear())
            Me._pathStore = pathStore
            If File.Exists(Path.Combine(pathStore, IndexingConfigStore.FILENAME & ".mfs")) Then
                Using store As New MessageStore.MessageStore(Me.CreateConfigStore(ConfigStoreType.IndexingStore))
                    Dim node As TreeNode = Me.TvwStores.SafeInvoke(Of TreeNode)(Function(t) t.Nodes(0).Nodes.Add(IndexingConfigStore.FILENAME, "Mensajes de Indexación (" & store.GetCountMessage() & ")"))
                    Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(t.Nodes(0).Nodes.Count - 1).SelectedImageIndex = 1)
                    Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(t.Nodes(0).Nodes.Count - 1).ImageIndex = 1)
                    Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(t.Nodes(0).Nodes.Count - 1).Name = Path.Combine(pathStore, IndexingConfigStore.FILENAME & ".mfs"))
                    Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(t.Nodes(0).Nodes.Count - 1).Tag = ConfigStoreType.IndexingStore)
                    Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(t.Nodes(0).Nodes.Count - 1).ToolTipText = "F5 - Actualiza la vista del almacén")
                    exist = True
                End Using
            End If
            If File.Exists(Path.Combine(pathStore, AuditingConfigStore.FILENAME & ".mfs")) Then
                Using store As New MessageStore.MessageStore(Me.CreateConfigStore(ConfigStoreType.AuditingStore))
                    Dim node As TreeNode = Me.TvwStores.SafeInvoke(Of TreeNode)(Function(t) t.Nodes(0).Nodes.Add(AuditingConfigStore.FILENAME, "Mensajes de Auditoria (" & store.GetCountMessage() & ")"))
                    Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(t.Nodes(0).Nodes.Count - 1).SelectedImageIndex = 1)
                    Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(t.Nodes(0).Nodes.Count - 1).ImageIndex = 1)
                    Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(t.Nodes(0).Nodes.Count - 1).Name = Path.Combine(pathStore, AuditingConfigStore.FILENAME & ".mfs"))
                    Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(t.Nodes(0).Nodes.Count - 1).Tag = ConfigStoreType.AuditingStore)
                    Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes(t.Nodes(0).Nodes.Count - 1).ToolTipText = "F5 - Actualiza la vista del almacén")
                    exist = True
                End Using
            End If
            If exist Then
                Me.StatusBarMain.SafeInvoke(Sub(s) s.Items(Me.LblPathCurrentMessageStore.Name).Text = Me._pathStore)
                Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Text = "ALMACÉN DE MENSAJES")
                Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).ImageIndex = 0)
                Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).SelectedImageIndex = 0)
                Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).ExpandAll())
                Me.LoadNodeToListView(Me.TvwStores.SafeInvoke(Of TreeNode)(Function(t) t.Nodes(0)))
                Me.SpcRight.SafeInvoke(Sub(p) p.Panel2Collapsed = True)
                Me.PnlMessageView.SafeInvoke(Sub(p) p.Visible = False)
                Me.MnuMain.SafeInvoke(Sub(m) DirectCast(m.Items(0), ToolStripMenuItem).DropDownItems(Me.MnuOpeStore.Name).Enabled = False)
                Me.MnuMain.SafeInvoke(Sub(m) DirectCast(m.Items(0), ToolStripMenuItem).DropDownItems(Me.MnuCloseStore.Name).Enabled = True)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cierra el almacén alctual de forma asíncrona
    ''' </summary>
    Private Function CloseStoreAsync() As Task
        Return Task.Factory.StartNew(AddressOf CloseStore)
    End Function

    ''' <summary>
    ''' Cierra el almacén alctual
    ''' </summary>
    Private Sub CloseStore()
        Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Nodes.Clear())
        Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).Text = "SIM ALMACÉN DE MENSAJES")
        Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).ImageIndex = 3)
        Me.TvwStores.SafeInvoke(Sub(t) t.Nodes(0).SelectedImageIndex = 3)
        Me.LvwMessages.SafeInvoke(Sub(t) t.Items.Clear())
        Me.LvwMessages.SafeInvoke(Sub(t) t.View = View.Tile)
        Me.SpcRight.SafeInvoke(Sub(t) t.Panel2Collapsed = True)
        Me.PnlMessageView.SafeInvoke(Sub(t) t.Visible = False)
        Me.TxtIdFile.SafeInvoke(Sub(t) t.Text = String.Empty)
        Me.TxtTitle.SafeInvoke(Sub(t) t.Text = String.Empty)
        Me.TxtTimeStamp.SafeInvoke(Sub(t) t.Text = String.Empty)
        Me.TxtBody.SafeInvoke(Sub(t) t.Text = String.Empty)
        Me.MnuMain.SafeInvoke(Sub(t) DirectCast(t.Items(0), ToolStripMenuItem).DropDownItems(Me.MnuOpeStore.Name).Enabled = True)
        Me.MnuMain.SafeInvoke(Sub(t) DirectCast(t.Items(0), ToolStripMenuItem).DropDownItems(Me.MnuCloseStore.Name).Enabled = False)
        Me._pathStore = Nothing
        Me._auditingMessages = Nothing
        Me._indexingMessages = Nothing
    End Sub

#End Region

End Class

''' <summary>
''' This class is an implementation of the 'IComparer' interface.
''' </summary>
Public Class ListViewColumnSorter
    Implements IComparer
    ''' <summary>
    ''' Specifies the column to be sorted
    ''' </summary>
    Private ColumnToSort As Integer
    ''' <summary>
    ''' Specifies the order in which to sort (i.e. 'Ascending').
    ''' </summary>
    Private OrderOfSort As SortOrder
    ''' <summary>
    ''' Case insensitive comparer object
    ''' </summary>
    Private ObjectCompare As CaseInsensitiveComparer

    ''' <summary>
    ''' Class constructor.  Initializes various elements
    ''' </summary>
    Public Sub New()
        ' Initialize the column to '0'
        ColumnToSort = 0

        ' Initialize the sort order to 'none'
        OrderOfSort = SortOrder.None

        ' Initialize the CaseInsensitiveComparer object
        ObjectCompare = New CaseInsensitiveComparer()
    End Sub

    ''' <summary>
    ''' This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
    ''' </summary>
    ''' <param name="x">First object to be compared</param>
    ''' <param name="y">Second object to be compared</param>
    ''' <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Dim compareResult As Integer
        Dim listviewX As ListViewItem, listviewY As ListViewItem

        ' Cast the objects to be compared to ListViewItem objects
        listviewX = DirectCast(x, ListViewItem)
        listviewY = DirectCast(y, ListViewItem)

        ' Compare the two items
        compareResult = ObjectCompare.Compare(listviewX.SubItems(ColumnToSort).Text, listviewY.SubItems(ColumnToSort).Text)

        ' Calculate correct return value based on object comparison
        If OrderOfSort = SortOrder.Ascending Then
            ' Ascending sort is selected, return normal result of compare operation
            Return compareResult
        ElseIf OrderOfSort = SortOrder.Descending Then
            ' Descending sort is selected, return negative result of compare operation
            Return (-compareResult)
        Else
            ' Return '0' to indicate they are equal
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
    ''' </summary>
    Public Property SortColumn() As Integer
        Get
            Return ColumnToSort
        End Get
        Set(value As Integer)
            ColumnToSort = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
    ''' </summary>
    Public Property Order() As SortOrder
        Get
            Return OrderOfSort
        End Get
        Set(value As SortOrder)
            OrderOfSort = value
        End Set
    End Property

End Class

''' <summary>
''' Tipo de configuración de almacén
''' </summary>
Public Enum ConfigStoreType

    ''' <summary>
    ''' Almacén de auditoria
    ''' </summary>
    AuditingStore = 1
    ''' <summary>
    ''' Almacén de indexación
    ''' </summary>
    IndexingStore = 2

End Enum