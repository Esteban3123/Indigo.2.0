Partial Class ToolsCustomizationForm
    Inherits DevExpress.XtraLayout.Customization.UserCustomizationForm

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ToolsCustomizationForm))
        Me.INDHiddenItemsList = New DevExpress.XtraLayout.Customization.Controls.HiddenItemsList()
        Me.INDlytvItems = New DevExpress.XtraLayout.Customization.Controls.LayoutTreeView()
        Me.INDlstControls = New DevExpress.XtraEditors.ImageListBoxControl()
        Me.INDimcControls = New DevExpress.Utils.ImageCollection()
        Me.INDlycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.CustomizationPropertyGrid1 = New DevExpress.XtraLayout.Customization.Controls.CustomizationPropertyGrid()
        Me.INDlycgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.TabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDHiddenItemsList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlstControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDimcControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycRoot.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDHiddenItemsList
        '
        Me.INDHiddenItemsList.Location = New System.Drawing.Point(24, 54)
        Me.INDHiddenItemsList.Name = "INDHiddenItemsList"
        Me.INDHiddenItemsList.Size = New System.Drawing.Size(510, 452)
        '
        'INDlytvItems
        '
        Me.INDlytvItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlytvItems.Location = New System.Drawing.Point(0, 0)
        Me.INDlytvItems.Name = "INDlytvItems"
        Me.INDlytvItems.Role = DevExpress.XtraLayout.Customization.Controls.TreeViewRoles.LayoutTreeView
        Me.INDlytvItems.ShowHiddenItemsInTreeView = True
        Me.INDlytvItems.Size = New System.Drawing.Size(252, 452)
        '
        'INDlstControls
        '
        Me.INDlstControls.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlstControls.Appearance.Options.UseFont = True
        Me.INDlstControls.ImageList = Me.INDimcControls
        Me.INDlstControls.Location = New System.Drawing.Point(24, 54)
        Me.INDlstControls.Name = "INDlstControls"
        Me.INDlstControls.Size = New System.Drawing.Size(510, 452)
        Me.INDlstControls.StyleController = Me.INDlycRoot
        Me.INDlstControls.TabIndex = 3
        '
        'INDimcControls
        '
        Me.INDimcControls.ImageStream = CType(resources.GetObject("INDimcControls.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDimcControls.Images.SetKeyName(0, "ButtonEdit")
        Me.INDimcControls.Images.SetKeyName(1, "CheckEdit")
        Me.INDimcControls.Images.SetKeyName(2, "DateEdit")
        Me.INDimcControls.Images.SetKeyName(3, "MemoEdit")
        Me.INDimcControls.Images.SetKeyName(4, "MemoExEdit")
        Me.INDimcControls.Images.SetKeyName(5, "RadioGroup")
        Me.INDimcControls.Images.SetKeyName(6, "SpinEdit")
        Me.INDimcControls.Images.SetKeyName(7, "TextEdit")
        Me.INDimcControls.Images.SetKeyName(8, "TimeEdit")
        Me.INDimcControls.InsertGalleryImage("Nothing", "images/programming/ide_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/programming/ide_16x16.png"), 9)
        Me.INDimcControls.Images.SetKeyName(9, "Nothing")
        '
        'INDlycRoot
        '
        Me.INDlycRoot.AllowCustomizationMenu = False
        Me.INDlycRoot.Controls.Add(Me.INDHiddenItemsList)
        Me.INDlycRoot.Controls.Add(Me.SplitContainerControl1)
        Me.INDlycRoot.Controls.Add(Me.INDlstControls)
        Me.INDlycRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlycRoot.Name = "INDlycRoot"
        Me.INDlycRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(600, 39, 439, 482)
        Me.INDlycRoot.Root = Me.INDlycgRoot
        Me.INDlycRoot.Size = New System.Drawing.Size(558, 530)
        Me.INDlycRoot.TabIndex = 2
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Location = New System.Drawing.Point(24, 54)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.INDlytvItems)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.CustomizationPropertyGrid1)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(510, 452)
        Me.SplitContainerControl1.SplitterPosition = 252
        Me.SplitContainerControl1.TabIndex = 4
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'CustomizationPropertyGrid1
        '
        Me.CustomizationPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CustomizationPropertyGrid1.Location = New System.Drawing.Point(0, 0)
        Me.CustomizationPropertyGrid1.Name = "CustomizationPropertyGrid1"
        Me.CustomizationPropertyGrid1.Size = New System.Drawing.Size(253, 452)
        '
        'INDlycgRoot
        '
        Me.INDlycgRoot.AllowHide = False
        Me.INDlycgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlycgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycgRoot.CustomizationFormText = "INDlycgRoot"
        Me.INDlycgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycgRoot.GroupBordersVisible = False
        Me.INDlycgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.TabbedControlGroup1})
        Me.INDlycgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgRoot.Name = "INDlycgRoot"
        Me.INDlycgRoot.ShowInCustomizationForm = False
        Me.INDlycgRoot.Size = New System.Drawing.Size(558, 530)
        Me.INDlycgRoot.Text = "INDlycgRoot"
        Me.INDlycgRoot.TextVisible = False
        '
        'TabbedControlGroup1
        '
        Me.TabbedControlGroup1.AllowHide = False
        Me.TabbedControlGroup1.CustomizationFormText = "TabbedControlGroup1"
        Me.TabbedControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.TabbedControlGroup1.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup1.SelectedTabPage = Me.LayoutControlGroup1
        Me.TabbedControlGroup1.SelectedTabPageIndex = 0
        Me.TabbedControlGroup1.Size = New System.Drawing.Size(538, 510)
        Me.TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.TabbedControlGroup1.Text = "TabbedControlGroup1"
        '
        'LayoutControlGroup1
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(223, Byte), Integer))
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.CustomizationFormText = "Objetos Ocultos"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(514, 456)
        Me.LayoutControlGroup1.Text = "Objetos Ocultos"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDHiddenItemsList
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(514, 456)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(223, Byte), Integer))
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.CustomizationFormText = "Lista de Objetos"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(514, 456)
        Me.LayoutControlGroup2.Text = "Lista de Objetos"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.SplitContainerControl1
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(514, 456)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlGroup3
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(223, Byte), Integer))
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.CustomizationFormText = "Lista de Controles"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(514, 456)
        Me.LayoutControlGroup3.Text = "Lista de Controles"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDlstControls
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(514, 456)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'ToolsCustomizationForm
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 530)
        Me.Controls.Add(Me.INDlycRoot)
        Me.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MinimumSize = New System.Drawing.Size(206, 107)
        Me.Name = "ToolsCustomizationForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Herramientas de Personalización"
        CType(Me.INDHiddenItemsList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlstControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDimcControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycRoot.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents INDHiddenItemsList As DevExpress.XtraLayout.Customization.Controls.HiddenItemsList
    Private WithEvents INDlytvItems As DevExpress.XtraLayout.Customization.Controls.LayoutTreeView
    Friend WithEvents INDlstControls As DevExpress.XtraEditors.ImageListBoxControl
    Friend WithEvents INDimcControls As DevExpress.Utils.ImageCollection
    Private WithEvents CustomizationPropertyGrid1 As DevExpress.XtraLayout.Customization.Controls.CustomizationPropertyGrid
    Friend WithEvents INDlycRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TabbedControlGroup1 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
