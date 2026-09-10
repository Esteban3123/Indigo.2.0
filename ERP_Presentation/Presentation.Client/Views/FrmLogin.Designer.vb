<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLogin
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        If disposing Then
            taskGetCompanies.Dispose()
            'taskLoadSettingsHIS.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLogin))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.PnlBackgroundLoginBox = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnMinimizar = New DevExpress.XtraEditors.PictureEdit()
        Me.INDbtnCerrar = New DevExpress.XtraEditors.PictureEdit()
        Me.PnlLoginBox = New DevExpress.XtraEditors.PanelControl()
        Me.PnlLoginData = New DevExpress.XtraEditors.PanelControl()
        Me.PnlPasswordBoxFocus = New DevExpress.XtraEditors.PanelControl()
        Me.PnlPasswordBoxLineFocus = New DevExpress.XtraEditors.PanelControl()
        Me.TxtPasswordBox = New DevExpress.XtraEditors.TextEdit()
        Me.PictureEdit2 = New DevExpress.XtraEditors.PictureEdit()
        Me.PnlLoginBoxFocus = New DevExpress.XtraEditors.PanelControl()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.PnlLoginBoxLineFocus = New DevExpress.XtraEditors.PanelControl()
        Me.TxtLoginBox = New DevExpress.XtraEditors.TextEdit()
        Me.GleOptions = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GdvCompanies = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColVersion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColIsProduction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepIsProduction = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.PbxLogo = New DevExpress.XtraEditors.PictureEdit()
        Me.PnlLoginButtons = New DevExpress.XtraEditors.PanelControl()
        Me.BtnLogin = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.PnlProgreso = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.MarqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.PnlTime = New DevExpress.XtraEditors.PanelControl()
        Me.LblTime = New DevExpress.XtraEditors.LabelControl()
        Me.LblDate = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.PbxBackgroundLogin = New DevExpress.XtraEditors.PictureEdit()
        Me.PnlMessageDay = New DevExpress.XtraEditors.PanelControl()
        Me.LblMessageDayText = New DevExpress.XtraEditors.LabelControl()
        Me.LblMessageDayTitle = New DevExpress.XtraEditors.LabelControl()
        Me.TmrDateTime = New System.Windows.Forms.Timer(Me.components)
        Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
        Me.lb2 = New DevExpress.XtraEditors.LabelControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.PnlBackgroundLoginBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlBackgroundLoginBox.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.INDbtnMinimizar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCerrar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlLoginBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlLoginBox.SuspendLayout()
        CType(Me.PnlLoginData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlLoginData.SuspendLayout()
        CType(Me.PnlPasswordBoxFocus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlPasswordBoxFocus.SuspendLayout()
        CType(Me.PnlPasswordBoxLineFocus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPasswordBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlLoginBoxFocus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlLoginBoxFocus.SuspendLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlLoginBoxLineFocus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLoginBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GleOptions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GdvCompanies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepIsProduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PbxLogo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlLoginButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlLoginButtons.SuspendLayout()
        CType(Me.PnlProgreso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlProgreso.SuspendLayout()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlTime.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PbxBackgroundLogin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlMessageDay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlMessageDay.SuspendLayout()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PnlBackgroundLoginBox
        '
        Me.PnlBackgroundLoginBox.Appearance.BackColor = System.Drawing.Color.White
        Me.PnlBackgroundLoginBox.Appearance.Options.UseBackColor = True
        Me.PnlBackgroundLoginBox.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlBackgroundLoginBox.Controls.Add(Me.PanelControl3)
        Me.PnlBackgroundLoginBox.Controls.Add(Me.PnlLoginBox)
        Me.PnlBackgroundLoginBox.Controls.Add(Me.PnlTime)
        Me.PnlBackgroundLoginBox.Controls.Add(Me.PanelControl1)
        Me.PnlBackgroundLoginBox.Dock = System.Windows.Forms.DockStyle.Right
        Me.PnlBackgroundLoginBox.Location = New System.Drawing.Point(673, 0)
        Me.PnlBackgroundLoginBox.Name = "PnlBackgroundLoginBox"
        Me.PnlBackgroundLoginBox.Size = New System.Drawing.Size(390, 790)
        Me.PnlBackgroundLoginBox.TabIndex = 0
        '
        'PanelControl3
        '
        Me.PanelControl3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl3.Appearance.BackColor = System.Drawing.Color.White
        Me.PanelControl3.Appearance.Options.UseBackColor = True
        Me.PanelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl3.Controls.Add(Me.INDbtnMinimizar)
        Me.PanelControl3.Controls.Add(Me.INDbtnCerrar)
        Me.PanelControl3.Location = New System.Drawing.Point(332, 0)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(58, 44)
        Me.PanelControl3.TabIndex = 7
        '
        'INDbtnMinimizar
        '
        Me.INDbtnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnMinimizar.EditValue = Global.Presentation.Client.My.Resources.Resources.minimizarClaro
        Me.INDbtnMinimizar.Location = New System.Drawing.Point(6, 5)
        Me.INDbtnMinimizar.Name = "INDbtnMinimizar"
        Me.INDbtnMinimizar.Properties.AllowFocused = False
        Me.INDbtnMinimizar.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDbtnMinimizar.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDbtnMinimizar.Size = New System.Drawing.Size(20, 20)
        Me.INDbtnMinimizar.TabIndex = 5
        Me.INDbtnMinimizar.ToolTip = "Minimizar"
        '
        'INDbtnCerrar
        '
        Me.INDbtnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnCerrar.EditValue = Global.Presentation.Client.My.Resources.Resources.CerrarClaro
        Me.INDbtnCerrar.Location = New System.Drawing.Point(31, 4)
        Me.INDbtnCerrar.Name = "INDbtnCerrar"
        Me.INDbtnCerrar.Properties.AllowFocused = False
        Me.INDbtnCerrar.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDbtnCerrar.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDbtnCerrar.Size = New System.Drawing.Size(20, 20)
        Me.INDbtnCerrar.TabIndex = 4
        Me.INDbtnCerrar.ToolTip = "Cerrar"
        '
        'PnlLoginBox
        '
        Me.PnlLoginBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlLoginBox.Appearance.BackColor = System.Drawing.Color.White
        Me.PnlLoginBox.Appearance.Options.UseBackColor = True
        Me.PnlLoginBox.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlLoginBox.Controls.Add(Me.PnlLoginData)
        Me.PnlLoginBox.Controls.Add(Me.GleOptions)
        Me.PnlLoginBox.Controls.Add(Me.PbxLogo)
        Me.PnlLoginBox.Controls.Add(Me.PnlLoginButtons)
        Me.PnlLoginBox.Controls.Add(Me.PnlProgreso)
        Me.PnlLoginBox.Location = New System.Drawing.Point(40, 68)
        Me.PnlLoginBox.MaximumSize = New System.Drawing.Size(310, 400)
        Me.PnlLoginBox.MinimumSize = New System.Drawing.Size(310, 400)
        Me.PnlLoginBox.Name = "PnlLoginBox"
        Me.PnlLoginBox.Size = New System.Drawing.Size(310, 400)
        Me.PnlLoginBox.TabIndex = 0
        '
        'PnlLoginData
        '
        Me.PnlLoginData.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlLoginData.Controls.Add(Me.PnlPasswordBoxFocus)
        Me.PnlLoginData.Controls.Add(Me.PnlLoginBoxFocus)
        Me.PnlLoginData.Location = New System.Drawing.Point(1, 119)
        Me.PnlLoginData.Margin = New System.Windows.Forms.Padding(0)
        Me.PnlLoginData.MaximumSize = New System.Drawing.Size(308, 190)
        Me.PnlLoginData.MinimumSize = New System.Drawing.Size(308, 190)
        Me.PnlLoginData.Name = "PnlLoginData"
        Me.PnlLoginData.Size = New System.Drawing.Size(308, 190)
        Me.PnlLoginData.TabIndex = 0
        '
        'PnlPasswordBoxFocus
        '
        Me.PnlPasswordBoxFocus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlPasswordBoxFocus.Appearance.BackColor = System.Drawing.Color.White
        Me.PnlPasswordBoxFocus.Appearance.Options.UseBackColor = True
        Me.PnlPasswordBoxFocus.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlPasswordBoxFocus.Controls.Add(Me.PnlPasswordBoxLineFocus)
        Me.PnlPasswordBoxFocus.Controls.Add(Me.TxtPasswordBox)
        Me.PnlPasswordBoxFocus.Controls.Add(Me.PictureEdit2)
        Me.PnlPasswordBoxFocus.Location = New System.Drawing.Point(1, 94)
        Me.PnlPasswordBoxFocus.MaximumSize = New System.Drawing.Size(306, 90)
        Me.PnlPasswordBoxFocus.MinimumSize = New System.Drawing.Size(306, 90)
        Me.PnlPasswordBoxFocus.Name = "PnlPasswordBoxFocus"
        Me.PnlPasswordBoxFocus.Size = New System.Drawing.Size(306, 90)
        Me.PnlPasswordBoxFocus.TabIndex = 1
        '
        'PnlPasswordBoxLineFocus
        '
        Me.PnlPasswordBoxLineFocus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlPasswordBoxLineFocus.Appearance.BackColor = System.Drawing.Color.White
        Me.PnlPasswordBoxLineFocus.Appearance.Options.UseBackColor = True
        Me.PnlPasswordBoxLineFocus.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlPasswordBoxLineFocus.Location = New System.Drawing.Point(0, 0)
        Me.PnlPasswordBoxLineFocus.MaximumSize = New System.Drawing.Size(5, 90)
        Me.PnlPasswordBoxLineFocus.MinimumSize = New System.Drawing.Size(5, 90)
        Me.PnlPasswordBoxLineFocus.Name = "PnlPasswordBoxLineFocus"
        Me.PnlPasswordBoxLineFocus.Size = New System.Drawing.Size(5, 90)
        Me.PnlPasswordBoxLineFocus.TabIndex = 0
        '
        'TxtPasswordBox
        '
        Me.TxtPasswordBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtPasswordBox.EnterMoveNextControl = True
        Me.TxtPasswordBox.Location = New System.Drawing.Point(66, 31)
        Me.TxtPasswordBox.Margin = New System.Windows.Forms.Padding(0)
        Me.TxtPasswordBox.Name = "TxtPasswordBox"
        Me.TxtPasswordBox.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Symbol", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPasswordBox.Properties.Appearance.Options.UseFont = True
        Me.TxtPasswordBox.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.TxtPasswordBox.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.TxtPasswordBox.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPasswordBox.Properties.UseSystemPasswordChar = True
        Me.TxtPasswordBox.Size = New System.Drawing.Size(178, 28)
        Me.TxtPasswordBox.TabIndex = 0
        '
        'PictureEdit2
        '
        Me.PictureEdit2.EditValue = CType(resources.GetObject("PictureEdit2.EditValue"), Object)
        Me.PictureEdit2.Location = New System.Drawing.Point(36, 31)
        Me.PictureEdit2.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureEdit2.Name = "PictureEdit2"
        Me.PictureEdit2.Properties.AllowFocused = False
        Me.PictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit2.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit2.Properties.ShowMenu = False
        Me.PictureEdit2.Size = New System.Drawing.Size(30, 27)
        Me.PictureEdit2.TabIndex = 1
        '
        'PnlLoginBoxFocus
        '
        Me.PnlLoginBoxFocus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlLoginBoxFocus.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.PnlLoginBoxFocus.Appearance.Options.UseBackColor = True
        Me.PnlLoginBoxFocus.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlLoginBoxFocus.Controls.Add(Me.PictureEdit1)
        Me.PnlLoginBoxFocus.Controls.Add(Me.PnlLoginBoxLineFocus)
        Me.PnlLoginBoxFocus.Controls.Add(Me.TxtLoginBox)
        Me.PnlLoginBoxFocus.Location = New System.Drawing.Point(1, 5)
        Me.PnlLoginBoxFocus.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PnlLoginBoxFocus.MaximumSize = New System.Drawing.Size(306, 90)
        Me.PnlLoginBoxFocus.MinimumSize = New System.Drawing.Size(306, 90)
        Me.PnlLoginBoxFocus.Name = "PnlLoginBoxFocus"
        Me.PnlLoginBoxFocus.Size = New System.Drawing.Size(306, 90)
        Me.PnlLoginBoxFocus.TabIndex = 0
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(36, 31)
        Me.PictureEdit1.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.AllowFocused = False
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.ShowMenu = False
        Me.PictureEdit1.Size = New System.Drawing.Size(30, 27)
        Me.PictureEdit1.TabIndex = 1
        '
        'PnlLoginBoxLineFocus
        '
        Me.PnlLoginBoxLineFocus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PnlLoginBoxLineFocus.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.PnlLoginBoxLineFocus.Appearance.Options.UseBackColor = True
        Me.PnlLoginBoxLineFocus.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlLoginBoxLineFocus.Location = New System.Drawing.Point(0, 0)
        Me.PnlLoginBoxLineFocus.MaximumSize = New System.Drawing.Size(5, 90)
        Me.PnlLoginBoxLineFocus.MinimumSize = New System.Drawing.Size(5, 90)
        Me.PnlLoginBoxLineFocus.Name = "PnlLoginBoxLineFocus"
        Me.PnlLoginBoxLineFocus.Size = New System.Drawing.Size(5, 90)
        Me.PnlLoginBoxLineFocus.TabIndex = 0
        '
        'TxtLoginBox
        '
        Me.TxtLoginBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtLoginBox.EditValue = ""
        Me.TxtLoginBox.EnterMoveNextControl = True
        Me.TxtLoginBox.Location = New System.Drawing.Point(66, 31)
        Me.TxtLoginBox.Margin = New System.Windows.Forms.Padding(0)
        Me.TxtLoginBox.Name = "TxtLoginBox"
        Me.TxtLoginBox.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Symbol", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.TxtLoginBox.Properties.Appearance.Options.UseFont = True
        Me.TxtLoginBox.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.TxtLoginBox.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.TxtLoginBox.Size = New System.Drawing.Size(178, 28)
        Me.TxtLoginBox.TabIndex = 0
        '
        'GleOptions
        '
        Me.GleOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GleOptions.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GleOptions.Location = New System.Drawing.Point(221, 24)
        Me.GleOptions.Name = "GleOptions"
        Me.GleOptions.Properties.AutoHeight = False
        Me.GleOptions.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        EditorButtonImageOptions2.Image = CType(resources.GetObject("EditorButtonImageOptions2.Image"), System.Drawing.Image)
        Me.GleOptions.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", 15, True, True, True, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "Companies", Nothing, DevExpress.Utils.ToolTipAnchor.[Default]), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", 15, True, True, True, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", "Home", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.GleOptions.Properties.PopupView = Me.GdvCompanies
        Me.GleOptions.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepIsProduction})
        Me.GleOptions.Properties.SearchMode = DevExpress.XtraEditors.Repository.GridLookUpSearchMode.None
        Me.GleOptions.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.GleOptions.Size = New System.Drawing.Size(68, 22)
        Me.GleOptions.TabIndex = 4
        '
        'GdvCompanies
        '
        Me.GdvCompanies.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GdvCompanies.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GdvCompanies.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GdvCompanies.Appearance.FocusedRow.Options.UseFont = True
        Me.GdvCompanies.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GdvCompanies.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GdvCompanies.Appearance.GroupRow.Options.UseFont = True
        Me.GdvCompanies.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GdvCompanies.Appearance.HeaderPanel.Options.UseFont = True
        Me.GdvCompanies.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GdvCompanies.Appearance.Row.Options.UseFont = True
        Me.GdvCompanies.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCode, Me.ColName, Me.ColVersion, Me.ColIsProduction})
        Me.GdvCompanies.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GdvCompanies.Name = "GdvCompanies"
        Me.GdvCompanies.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GdvCompanies.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvCompanies.OptionsView.EnableAppearanceOddRow = True
        Me.GdvCompanies.OptionsView.ShowAutoFilterRow = True
        Me.GdvCompanies.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GdvCompanies, False)
        '
        'ColCode
        '
        Me.ColCode.Caption = "Código"
        Me.ColCode.FieldName = "Code"
        Me.ColCode.Name = "ColCode"
        Me.ColCode.OptionsColumn.AllowEdit = False
        Me.ColCode.OptionsColumn.AllowFocus = False
        Me.ColCode.OptionsColumn.FixedWidth = True
        Me.ColCode.Visible = True
        Me.ColCode.VisibleIndex = 0
        '
        'ColName
        '
        Me.ColName.Caption = "Nombre"
        Me.ColName.FieldName = "Name"
        Me.ColName.Name = "ColName"
        Me.ColName.OptionsColumn.AllowEdit = False
        Me.ColName.OptionsColumn.AllowFocus = False
        Me.ColName.Visible = True
        Me.ColName.VisibleIndex = 1
        '
        'ColVersion
        '
        Me.ColVersion.Caption = "Versión"
        Me.ColVersion.FieldName = "Version"
        Me.ColVersion.Name = "ColVersion"
        Me.ColVersion.OptionsColumn.AllowEdit = False
        Me.ColVersion.OptionsColumn.AllowFocus = False
        Me.ColVersion.Visible = True
        Me.ColVersion.VisibleIndex = 2
        '
        'ColIsProduction
        '
        Me.ColIsProduction.Caption = "Producción"
        Me.ColIsProduction.ColumnEdit = Me.RepIsProduction
        Me.ColIsProduction.FieldName = "ProductionCompany"
        Me.ColIsProduction.Name = "ColIsProduction"
        Me.ColIsProduction.OptionsColumn.AllowEdit = False
        Me.ColIsProduction.OptionsColumn.AllowFocus = False
        Me.ColIsProduction.Visible = True
        Me.ColIsProduction.VisibleIndex = 3
        '
        'RepIsProduction
        '
        Me.RepIsProduction.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepIsProduction.Appearance.Options.UseFont = True
        Me.RepIsProduction.AutoHeight = False
        Me.RepIsProduction.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Sí", True, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No", False, -1)})
        Me.RepIsProduction.Name = "RepIsProduction"
        '
        'PbxLogo
        '
        Me.PbxLogo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PbxLogo.EditValue = Global.Presentation.Client.My.Resources.Resources.login
        Me.PbxLogo.Location = New System.Drawing.Point(57, 56)
        Me.PbxLogo.Name = "PbxLogo"
        Me.PbxLogo.Properties.AllowFocused = False
        Me.PbxLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PbxLogo.Properties.Appearance.Options.UseBackColor = True
        Me.PbxLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PbxLogo.Properties.ShowMenu = False
        Me.PbxLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.PbxLogo.Size = New System.Drawing.Size(201, 60)
        Me.PbxLogo.TabIndex = 1
        '
        'PnlLoginButtons
        '
        Me.PnlLoginButtons.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlLoginButtons.Appearance.BackColor = System.Drawing.Color.White
        Me.PnlLoginButtons.Appearance.Options.UseBackColor = True
        Me.PnlLoginButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlLoginButtons.Controls.Add(Me.BtnLogin)
        Me.PnlLoginButtons.Controls.Add(Me.BtnClose)
        Me.PnlLoginButtons.Location = New System.Drawing.Point(0, 320)
        Me.PnlLoginButtons.Margin = New System.Windows.Forms.Padding(0)
        Me.PnlLoginButtons.MaximumSize = New System.Drawing.Size(310, 80)
        Me.PnlLoginButtons.MinimumSize = New System.Drawing.Size(310, 80)
        Me.PnlLoginButtons.Name = "PnlLoginButtons"
        Me.PnlLoginButtons.Size = New System.Drawing.Size(310, 80)
        Me.PnlLoginButtons.TabIndex = 1
        '
        'BtnLogin
        '
        Me.BtnLogin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnLogin.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLogin.Appearance.Options.UseFont = True
        Me.BtnLogin.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnLogin.Location = New System.Drawing.Point(147, 7)
        Me.BtnLogin.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BtnLogin.MinimumSize = New System.Drawing.Size(80, 30)
        Me.BtnLogin.Name = "BtnLogin"
        Me.BtnLogin.Size = New System.Drawing.Size(80, 30)
        Me.BtnLogin.TabIndex = 0
        Me.BtnLogin.Text = "Login"
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Appearance.Font = New System.Drawing.Font("Segoe UI Symbol", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Appearance.Options.UseFont = True
        Me.BtnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnClose.ImageOptions.Image = CType(resources.GetObject("BtnClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnClose.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtnClose.Location = New System.Drawing.Point(233, 7)
        Me.BtnClose.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BtnClose.MaximumSize = New System.Drawing.Size(30, 30)
        Me.BtnClose.MinimumSize = New System.Drawing.Size(30, 30)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(30, 30)
        Me.BtnClose.TabIndex = 1
        '
        'PnlProgreso
        '
        Me.PnlProgreso.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlProgreso.Controls.Add(Me.LabelControl1)
        Me.PnlProgreso.Controls.Add(Me.MarqueeProgressBarControl1)
        Me.PnlProgreso.Location = New System.Drawing.Point(1, 119)
        Me.PnlProgreso.Margin = New System.Windows.Forms.Padding(0)
        Me.PnlProgreso.MaximumSize = New System.Drawing.Size(308, 190)
        Me.PnlProgreso.MinimumSize = New System.Drawing.Size(308, 190)
        Me.PnlProgreso.Name = "PnlProgreso"
        Me.PnlProgreso.Size = New System.Drawing.Size(308, 190)
        Me.PnlProgreso.TabIndex = 5
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Location = New System.Drawing.Point(102, 74)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(110, 25)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Validando ..."
        '
        'MarqueeProgressBarControl1
        '
        Me.MarqueeProgressBarControl1.EditValue = 0
        Me.MarqueeProgressBarControl1.Location = New System.Drawing.Point(70, 112)
        Me.MarqueeProgressBarControl1.Name = "MarqueeProgressBarControl1"
        Me.MarqueeProgressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Properties.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Properties.AppearanceDisabled.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Properties.AppearanceFocused.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Properties.AppearanceFocused.FontStyleDelta = System.Drawing.FontStyle.Italic
        Me.MarqueeProgressBarControl1.Properties.EndColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Properties.StartColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MarqueeProgressBarControl1.Size = New System.Drawing.Size(166, 10)
        Me.MarqueeProgressBarControl1.TabIndex = 1
        '
        'PnlTime
        '
        Me.PnlTime.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.PnlTime.Appearance.Options.UseBackColor = True
        Me.PnlTime.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlTime.Controls.Add(Me.LblTime)
        Me.PnlTime.Controls.Add(Me.LblDate)
        Me.PnlTime.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PnlTime.Location = New System.Drawing.Point(0, 550)
        Me.PnlTime.Name = "PnlTime"
        Me.PnlTime.Size = New System.Drawing.Size(390, 240)
        Me.PnlTime.TabIndex = 0
        '
        'LblTime
        '
        Me.LblTime.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 120.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.LblTime.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblTime.Appearance.Options.UseFont = True
        Me.LblTime.Appearance.Options.UseForeColor = True
        Me.LblTime.Appearance.Options.UseTextOptions = True
        Me.LblTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LblTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTime.Location = New System.Drawing.Point(0, 0)
        Me.LblTime.Name = "LblTime"
        Me.LblTime.Size = New System.Drawing.Size(390, 171)
        Me.LblTime.TabIndex = 0
        Me.LblTime.Text = "00:00"
        '
        'LblDate
        '
        Me.LblDate.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LblDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.LblDate.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblDate.Appearance.Options.UseBackColor = True
        Me.LblDate.Appearance.Options.UseFont = True
        Me.LblDate.Appearance.Options.UseForeColor = True
        Me.LblDate.Appearance.Options.UseTextOptions = True
        Me.LblDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblDate.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LblDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblDate.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LblDate.Location = New System.Drawing.Point(0, 171)
        Me.LblDate.Name = "LblDate"
        Me.LblDate.Size = New System.Drawing.Size(390, 69)
        Me.LblDate.TabIndex = 1
        Me.LblDate.Text = "miércoles, 29 julio 2015"
        '
        'PanelControl1
        '
        Me.PanelControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.Silver
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Location = New System.Drawing.Point(38, 67)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(314, 404)
        Me.PanelControl1.TabIndex = 1
        '
        'PbxBackgroundLogin
        '
        Me.PbxBackgroundLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PbxBackgroundLogin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PbxBackgroundLogin.Location = New System.Drawing.Point(0, 0)
        Me.PbxBackgroundLogin.Margin = New System.Windows.Forms.Padding(0)
        Me.PbxBackgroundLogin.Name = "PbxBackgroundLogin"
        Me.PbxBackgroundLogin.Properties.AllowFocused = False
        Me.PbxBackgroundLogin.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.PbxBackgroundLogin.Properties.Appearance.Options.UseBackColor = True
        Me.PbxBackgroundLogin.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PbxBackgroundLogin.Properties.Caption.Visible = False
        Me.PbxBackgroundLogin.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PbxBackgroundLogin.Properties.NullText = " "
        Me.PbxBackgroundLogin.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.PbxBackgroundLogin.Properties.ShowMenu = False
        Me.PbxBackgroundLogin.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.PbxBackgroundLogin.Size = New System.Drawing.Size(1063, 790)
        Me.PbxBackgroundLogin.TabIndex = 10
        '
        'PnlMessageDay
        '
        Me.PnlMessageDay.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.PnlMessageDay.Appearance.Options.UseBackColor = True
        Me.PnlMessageDay.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlMessageDay.Controls.Add(Me.LblMessageDayText)
        Me.PnlMessageDay.Controls.Add(Me.LblMessageDayTitle)
        Me.PnlMessageDay.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PnlMessageDay.Location = New System.Drawing.Point(0, 550)
        Me.PnlMessageDay.Name = "PnlMessageDay"
        Me.PnlMessageDay.Size = New System.Drawing.Size(673, 240)
        Me.PnlMessageDay.TabIndex = 2
        '
        'LblMessageDayText
        '
        Me.LblMessageDayText.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.LblMessageDayText.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblMessageDayText.Appearance.Options.UseFont = True
        Me.LblMessageDayText.Appearance.Options.UseForeColor = True
        Me.LblMessageDayText.Appearance.Options.UseTextOptions = True
        Me.LblMessageDayText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.LblMessageDayText.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LblMessageDayText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LblMessageDayText.AutoEllipsis = True
        Me.LblMessageDayText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblMessageDayText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMessageDayText.Location = New System.Drawing.Point(0, 77)
        Me.LblMessageDayText.Name = "LblMessageDayText"
        Me.LblMessageDayText.Padding = New System.Windows.Forms.Padding(15, 0, 15, 0)
        Me.LblMessageDayText.Size = New System.Drawing.Size(673, 163)
        Me.LblMessageDayText.TabIndex = 12
        Me.LblMessageDayText.Text = "Versión RDP"
        '
        'LblMessageDayTitle
        '
        Me.LblMessageDayTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 50.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.LblMessageDayTitle.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblMessageDayTitle.Appearance.Options.UseFont = True
        Me.LblMessageDayTitle.Appearance.Options.UseForeColor = True
        Me.LblMessageDayTitle.Appearance.Options.UseTextOptions = True
        Me.LblMessageDayTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.LblMessageDayTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LblMessageDayTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LblMessageDayTitle.AutoEllipsis = True
        Me.LblMessageDayTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblMessageDayTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.LblMessageDayTitle.Location = New System.Drawing.Point(0, 0)
        Me.LblMessageDayTitle.Name = "LblMessageDayTitle"
        Me.LblMessageDayTitle.Padding = New System.Windows.Forms.Padding(15, 0, 15, 0)
        Me.LblMessageDayTitle.Size = New System.Drawing.Size(673, 77)
        Me.LblMessageDayTitle.TabIndex = 11
        Me.LblMessageDayTitle.Text = "Vie Cloud Native Client"
        '
        'TmrDateTime
        '
        Me.TmrDateTime.Enabled = True
        Me.TmrDateTime.Interval = 20000
        '
        'ElementHost1
        '
        Me.ElementHost1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ElementHost1.Location = New System.Drawing.Point(0, 0)
        Me.ElementHost1.Name = "ElementHost1"
        Me.ElementHost1.Size = New System.Drawing.Size(1063, 790)
        Me.ElementHost1.TabIndex = 11
        Me.ElementHost1.Text = "ElementHost1"
        Me.ElementHost1.Child = Nothing
        '
        'lb2
        '
        Me.lb2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.lb2.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 50.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.lb2.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lb2.Appearance.Options.UseBackColor = True
        Me.lb2.Appearance.Options.UseFont = True
        Me.lb2.Appearance.Options.UseForeColor = True
        Me.lb2.Appearance.Options.UseTextOptions = True
        Me.lb2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.lb2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lb2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lb2.AutoEllipsis = True
        Me.lb2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lb2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.lb2.Dock = System.Windows.Forms.DockStyle.Top
        Me.lb2.Location = New System.Drawing.Point(0, 0)
        Me.lb2.Name = "lb2"
        Me.lb2.Padding = New System.Windows.Forms.Padding(15, 0, 15, 0)
        Me.lb2.Size = New System.Drawing.Size(673, 74)
        Me.lb2.TabIndex = 12
        Me.lb2.Text = "Vie Cloud Native Client"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmLogin
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1063, 790)
        Me.Controls.Add(Me.lb2)
        Me.Controls.Add(Me.PnlMessageDay)
        Me.Controls.Add(Me.PnlBackgroundLoginBox)
        Me.Controls.Add(Me.PbxBackgroundLogin)
        Me.Controls.Add(Me.ElementHost1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmLogin.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login Vie Cloud Native Client"
        CType(Me.PnlBackgroundLoginBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlBackgroundLoginBox.ResumeLayout(False)
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.INDbtnMinimizar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCerrar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlLoginBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlLoginBox.ResumeLayout(False)
        CType(Me.PnlLoginData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlLoginData.ResumeLayout(False)
        CType(Me.PnlPasswordBoxFocus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlPasswordBoxFocus.ResumeLayout(False)
        CType(Me.PnlPasswordBoxLineFocus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPasswordBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlLoginBoxFocus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlLoginBoxFocus.ResumeLayout(False)
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlLoginBoxLineFocus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLoginBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GleOptions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GdvCompanies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepIsProduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PbxLogo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlLoginButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlLoginButtons.ResumeLayout(False)
        CType(Me.PnlProgreso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlProgreso.ResumeLayout(False)
        Me.PnlProgreso.PerformLayout()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlTime.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PbxBackgroundLogin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlMessageDay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlMessageDay.ResumeLayout(False)
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PnlBackgroundLoginBox As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PbxBackgroundLogin As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents PnlMessageDay As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PnlTime As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PnlLoginBox As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LblTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblMessageDayText As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblMessageDayTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PbxLogo As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents GleOptions As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GdvCompanies As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TxtPasswordBox As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtLoginBox As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PnlLoginButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PictureEdit2 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents BtnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnLogin As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PnlPasswordBoxFocus As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PnlLoginBoxFocus As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PnlPasswordBoxLineFocus As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PnlLoginBoxLineFocus As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TmrDateTime As System.Windows.Forms.Timer
    Friend WithEvents ColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnMinimizar As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDbtnCerrar As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents ElementHost1 As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents ColVersion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColIsProduction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepIsProduction As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents PnlProgreso As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PnlLoginData As DevExpress.XtraEditors.PanelControl
    Friend WithEvents MarqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lb2 As DevExpress.XtraEditors.LabelControl
End Class
