<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SIN ALMACÉN DE MENSAJES", 3, 3)
        Me.MnuMain = New System.Windows.Forms.MenuStrip()
        Me.MnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuOpeStore = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCloseStore = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.SpcMain = New System.Windows.Forms.SplitContainer()
        Me.TvwStores = New System.Windows.Forms.TreeView()
        Me.ImageListMain = New System.Windows.Forms.ImageList(Me.components)
        Me.SpcRight = New System.Windows.Forms.SplitContainer()
        Me.LvwMessages = New System.Windows.Forms.ListView()
        Me.ChTitle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ChTimeStamp = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ChSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PnlMessageView = New System.Windows.Forms.Panel()
        Me.BtnMessageError = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtBody = New System.Windows.Forms.RichTextBox()
        Me.TxtSize = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtIdFile = New System.Windows.Forms.TextBox()
        Me.TxtTitle = New System.Windows.Forms.TextBox()
        Me.TxtTimeStamp = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.StatusBarMain = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LblPathCurrentMessageStore = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PgbLoading = New System.Windows.Forms.ToolStripProgressBar()
        Me.LblAction = New System.Windows.Forms.ToolStripStatusLabel()
        Me.BtnDeleteMarkError = New System.Windows.Forms.Button()
        Me.ToolTipMessage = New System.Windows.Forms.ToolTip(Me.components)
        Me.MnuMain.SuspendLayout()
        CType(Me.SpcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SpcMain.Panel1.SuspendLayout()
        Me.SpcMain.Panel2.SuspendLayout()
        Me.SpcMain.SuspendLayout()
        CType(Me.SpcRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SpcRight.Panel1.SuspendLayout()
        Me.SpcRight.Panel2.SuspendLayout()
        Me.SpcRight.SuspendLayout()
        Me.PnlMessageView.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.StatusBarMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuFile})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(1008, 24)
        Me.MnuMain.TabIndex = 0
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuFile
        '
        Me.MnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuOpeStore, Me.MnuCloseStore, Me.SalirToolStripMenuItem, Me.MnuExit})
        Me.MnuFile.Name = "MnuFile"
        Me.MnuFile.Size = New System.Drawing.Size(71, 20)
        Me.MnuFile.Text = "&ARCHIVO"
        '
        'MnuOpeStore
        '
        Me.MnuOpeStore.Image = CType(resources.GetObject("MnuOpeStore.Image"), System.Drawing.Image)
        Me.MnuOpeStore.Name = "MnuOpeStore"
        Me.MnuOpeStore.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.MnuOpeStore.Size = New System.Drawing.Size(194, 22)
        Me.MnuOpeStore.Text = "Abrir Almacén"
        Me.MnuOpeStore.ToolTipText = "Abrir un nuevo almacén"
        '
        'MnuCloseStore
        '
        Me.MnuCloseStore.Enabled = False
        Me.MnuCloseStore.Image = CType(resources.GetObject("MnuCloseStore.Image"), System.Drawing.Image)
        Me.MnuCloseStore.Name = "MnuCloseStore"
        Me.MnuCloseStore.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F2), System.Windows.Forms.Keys)
        Me.MnuCloseStore.Size = New System.Drawing.Size(194, 22)
        Me.MnuCloseStore.Text = "Cerra Almacén"
        Me.MnuCloseStore.ToolTipText = "Cerrar el almacén actual"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(191, 6)
        '
        'MnuExit
        '
        Me.MnuExit.Name = "MnuExit"
        Me.MnuExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.MnuExit.Size = New System.Drawing.Size(194, 22)
        Me.MnuExit.Text = "Salir"
        Me.MnuExit.ToolTipText = "Salir de la aplicación"
        '
        'SpcMain
        '
        Me.SpcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SpcMain.Location = New System.Drawing.Point(0, 24)
        Me.SpcMain.Name = "SpcMain"
        '
        'SpcMain.Panel1
        '
        Me.SpcMain.Panel1.Controls.Add(Me.TvwStores)
        '
        'SpcMain.Panel2
        '
        Me.SpcMain.Panel2.Controls.Add(Me.SpcRight)
        Me.SpcMain.Size = New System.Drawing.Size(1008, 683)
        Me.SpcMain.SplitterDistance = 336
        Me.SpcMain.TabIndex = 1
        '
        'TvwStores
        '
        Me.TvwStores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TvwStores.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TvwStores.ImageIndex = 3
        Me.TvwStores.ImageList = Me.ImageListMain
        Me.TvwStores.Location = New System.Drawing.Point(0, 0)
        Me.TvwStores.Name = "TvwStores"
        TreeNode1.ImageIndex = 3
        TreeNode1.Name = "_Root_"
        TreeNode1.SelectedImageIndex = 3
        TreeNode1.Text = "SIN ALMACÉN DE MENSAJES"
        TreeNode1.ToolTipText = "F5 - Actualiza la lista de almacenes"
        Me.TvwStores.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.TvwStores.SelectedImageIndex = 3
        Me.TvwStores.ShowLines = False
        Me.TvwStores.ShowNodeToolTips = True
        Me.TvwStores.ShowPlusMinus = False
        Me.TvwStores.ShowRootLines = False
        Me.TvwStores.Size = New System.Drawing.Size(336, 683)
        Me.TvwStores.TabIndex = 0
        '
        'ImageListMain
        '
        Me.ImageListMain.ImageStream = CType(resources.GetObject("ImageListMain.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListMain.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListMain.Images.SetKeyName(0, "packagekit.ico")
        Me.ImageListMain.Images.SetKeyName(1, "code-block.ico")
        Me.ImageListMain.Images.SetKeyName(2, "code-context.ico")
        Me.ImageListMain.Images.SetKeyName(3, "delete.ico")
        '
        'SpcRight
        '
        Me.SpcRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SpcRight.Location = New System.Drawing.Point(0, 0)
        Me.SpcRight.Name = "SpcRight"
        Me.SpcRight.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SpcRight.Panel1
        '
        Me.SpcRight.Panel1.Controls.Add(Me.LvwMessages)
        '
        'SpcRight.Panel2
        '
        Me.SpcRight.Panel2.BackColor = System.Drawing.SystemColors.Window
        Me.SpcRight.Panel2.Controls.Add(Me.PnlMessageView)
        Me.SpcRight.Size = New System.Drawing.Size(668, 683)
        Me.SpcRight.SplitterDistance = 311
        Me.SpcRight.TabIndex = 0
        '
        'LvwMessages
        '
        Me.LvwMessages.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.LvwMessages.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ChTitle, Me.ChTimeStamp, Me.ChSize})
        Me.LvwMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LvwMessages.LabelWrap = False
        Me.LvwMessages.LargeImageList = Me.ImageListMain
        Me.LvwMessages.Location = New System.Drawing.Point(0, 0)
        Me.LvwMessages.MultiSelect = False
        Me.LvwMessages.Name = "LvwMessages"
        Me.LvwMessages.Size = New System.Drawing.Size(668, 311)
        Me.LvwMessages.SmallImageList = Me.ImageListMain
        Me.LvwMessages.StateImageList = Me.ImageListMain
        Me.LvwMessages.TabIndex = 0
        Me.LvwMessages.UseCompatibleStateImageBehavior = False
        Me.LvwMessages.View = System.Windows.Forms.View.Tile
        '
        'ChTitle
        '
        Me.ChTitle.Text = "Title"
        Me.ChTitle.Width = 250
        '
        'ChTimeStamp
        '
        Me.ChTimeStamp.Text = "TimeStamp"
        Me.ChTimeStamp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ChTimeStamp.Width = 150
        '
        'ChSize
        '
        Me.ChSize.Text = "Size"
        Me.ChSize.Width = 100
        '
        'PnlMessageView
        '
        Me.PnlMessageView.BackColor = System.Drawing.SystemColors.Window
        Me.PnlMessageView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlMessageView.Controls.Add(Me.BtnDeleteMarkError)
        Me.PnlMessageView.Controls.Add(Me.BtnMessageError)
        Me.PnlMessageView.Controls.Add(Me.Panel1)
        Me.PnlMessageView.Controls.Add(Me.TxtSize)
        Me.PnlMessageView.Controls.Add(Me.Label5)
        Me.PnlMessageView.Controls.Add(Me.TxtIdFile)
        Me.PnlMessageView.Controls.Add(Me.TxtTitle)
        Me.PnlMessageView.Controls.Add(Me.TxtTimeStamp)
        Me.PnlMessageView.Controls.Add(Me.Label4)
        Me.PnlMessageView.Controls.Add(Me.Label3)
        Me.PnlMessageView.Controls.Add(Me.Label2)
        Me.PnlMessageView.Controls.Add(Me.Label1)
        Me.PnlMessageView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMessageView.Location = New System.Drawing.Point(0, 0)
        Me.PnlMessageView.Name = "PnlMessageView"
        Me.PnlMessageView.Size = New System.Drawing.Size(668, 368)
        Me.PnlMessageView.TabIndex = 0
        Me.PnlMessageView.Visible = False
        '
        'BtnMessageError
        '
        Me.BtnMessageError.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMessageError.Location = New System.Drawing.Point(544, 111)
        Me.BtnMessageError.Name = "BtnMessageError"
        Me.BtnMessageError.Size = New System.Drawing.Size(111, 29)
        Me.BtnMessageError.TabIndex = 11
        Me.BtnMessageError.Text = "Ver Error"
        Me.ToolTipMessage.SetToolTip(Me.BtnMessageError, "Ver el mensaje de error")
        Me.BtnMessageError.UseVisualStyleBackColor = True
        Me.BtnMessageError.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TxtBody)
        Me.Panel1.Location = New System.Drawing.Point(24, 146)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(632, 209)
        Me.Panel1.TabIndex = 10
        '
        'TxtBody
        '
        Me.TxtBody.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtBody.Location = New System.Drawing.Point(0, 0)
        Me.TxtBody.Name = "TxtBody"
        Me.TxtBody.ReadOnly = True
        Me.TxtBody.Size = New System.Drawing.Size(630, 207)
        Me.TxtBody.TabIndex = 10
        Me.TxtBody.Text = ""
        '
        'TxtSize
        '
        Me.TxtSize.Location = New System.Drawing.Point(460, 80)
        Me.TxtSize.Name = "TxtSize"
        Me.TxtSize.ReadOnly = True
        Me.TxtSize.Size = New System.Drawing.Size(196, 23)
        Me.TxtSize.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(411, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 21)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Size:"
        '
        'TxtIdFile
        '
        Me.TxtIdFile.Location = New System.Drawing.Point(124, 20)
        Me.TxtIdFile.Name = "TxtIdFile"
        Me.TxtIdFile.ReadOnly = True
        Me.TxtIdFile.Size = New System.Drawing.Size(532, 23)
        Me.TxtIdFile.TabIndex = 7
        '
        'TxtTitle
        '
        Me.TxtTitle.Location = New System.Drawing.Point(124, 51)
        Me.TxtTitle.Name = "TxtTitle"
        Me.TxtTitle.ReadOnly = True
        Me.TxtTitle.Size = New System.Drawing.Size(532, 23)
        Me.TxtTitle.TabIndex = 6
        '
        'TxtTimeStamp
        '
        Me.TxtTimeStamp.Location = New System.Drawing.Point(124, 80)
        Me.TxtTimeStamp.Name = "TxtTimeStamp"
        Me.TxtTimeStamp.ReadOnly = True
        Me.TxtTimeStamp.Size = New System.Drawing.Size(281, 23)
        Me.TxtTimeStamp.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 21)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Body:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 21)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Title:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "TimeStamp:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "IdFile:"
        '
        'FolderBrowserDialog
        '
        Me.FolderBrowserDialog.Description = "Seleccione la carpeta que contiene los almacenes de mensajes"
        '
        'StatusBarMain
        '
        Me.StatusBarMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.LblPathCurrentMessageStore, Me.PgbLoading, Me.LblAction})
        Me.StatusBarMain.Location = New System.Drawing.Point(0, 707)
        Me.StatusBarMain.Name = "StatusBarMain"
        Me.StatusBarMain.Size = New System.Drawing.Size(1008, 22)
        Me.StatusBarMain.TabIndex = 2
        Me.StatusBarMain.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(65, 17)
        Me.ToolStripStatusLabel1.Text = "ALMACÉN:"
        '
        'LblPathCurrentMessageStore
        '
        Me.LblPathCurrentMessageStore.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPathCurrentMessageStore.Name = "LblPathCurrentMessageStore"
        Me.LblPathCurrentMessageStore.Size = New System.Drawing.Size(17, 17)
        Me.LblPathCurrentMessageStore.Text = "--"
        '
        'PgbLoading
        '
        Me.PgbLoading.Enabled = False
        Me.PgbLoading.MarqueeAnimationSpeed = 0
        Me.PgbLoading.Name = "PgbLoading"
        Me.PgbLoading.Size = New System.Drawing.Size(100, 16)
        Me.PgbLoading.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.PgbLoading.Visible = False
        '
        'LblAction
        '
        Me.LblAction.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAction.ForeColor = System.Drawing.Color.Green
        Me.LblAction.Name = "LblAction"
        Me.LblAction.Size = New System.Drawing.Size(17, 17)
        Me.LblAction.Text = "--"
        Me.LblAction.Visible = False
        '
        'BtnDeleteMarkError
        '
        Me.BtnDeleteMarkError.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteMarkError.Location = New System.Drawing.Point(427, 111)
        Me.BtnDeleteMarkError.Name = "BtnDeleteMarkError"
        Me.BtnDeleteMarkError.Size = New System.Drawing.Size(111, 29)
        Me.BtnDeleteMarkError.TabIndex = 12
        Me.BtnDeleteMarkError.Text = "Supr Error"
        Me.ToolTipMessage.SetToolTip(Me.BtnDeleteMarkError, "Suprimir la marca de error")
        Me.BtnDeleteMarkError.UseVisualStyleBackColor = True
        Me.BtnDeleteMarkError.Visible = False
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.SpcMain)
        Me.Controls.Add(Me.StatusBarMain)
        Me.Controls.Add(Me.MnuMain)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MnuMain
        Me.MinimumSize = New System.Drawing.Size(1024, 768)
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.SpcMain.Panel1.ResumeLayout(False)
        Me.SpcMain.Panel2.ResumeLayout(False)
        CType(Me.SpcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SpcMain.ResumeLayout(False)
        Me.SpcRight.Panel1.ResumeLayout(False)
        Me.SpcRight.Panel2.ResumeLayout(False)
        CType(Me.SpcRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SpcRight.ResumeLayout(False)
        Me.PnlMessageView.ResumeLayout(False)
        Me.PnlMessageView.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.StatusBarMain.ResumeLayout(False)
        Me.StatusBarMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuOpeStore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCloseStore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SpcMain As System.Windows.Forms.SplitContainer
    Friend WithEvents TvwStores As System.Windows.Forms.TreeView
    Friend WithEvents LvwMessages As System.Windows.Forms.ListView
    Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ImageListMain As System.Windows.Forms.ImageList
    Friend WithEvents SpcRight As System.Windows.Forms.SplitContainer
    Friend WithEvents PnlMessageView As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtIdFile As System.Windows.Forms.TextBox
    Friend WithEvents TxtTitle As System.Windows.Forms.TextBox
    Friend WithEvents TxtTimeStamp As System.Windows.Forms.TextBox
    Friend WithEvents ChTitle As System.Windows.Forms.ColumnHeader
    Friend WithEvents ChTimeStamp As System.Windows.Forms.ColumnHeader
    Friend WithEvents StatusBarMain As System.Windows.Forms.StatusStrip
    Friend WithEvents PgbLoading As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents LblPathCurrentMessageStore As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents LblAction As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ChSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents TxtSize As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtBody As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BtnMessageError As System.Windows.Forms.Button
    Friend WithEvents BtnDeleteMarkError As System.Windows.Forms.Button
    Friend WithEvents ToolTipMessage As System.Windows.Forms.ToolTip

End Class
