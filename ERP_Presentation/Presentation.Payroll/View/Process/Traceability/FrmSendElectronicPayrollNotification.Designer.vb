<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSendElectronicPayrollNotification
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.components = New System.ComponentModel.Container()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbtnSendNotification = New DevExpress.XtraEditors.SimpleButton()
        Me.INDpcCorreos = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDgcEmail = New DevExpress.XtraGrid.GridControl()
        Me.INDgcEmailView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnEliminarEmail = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDrepSleEmailType = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlbEmail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDpcCorreos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcEmailView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnEliminarEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleEmailType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlbEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDSbtnSendNotification)
        Me.LayoutControl1.Controls.Add(Me.INDpcCorreos)
        Me.LayoutControl1.Controls.Add(Me.INDgcEmail)
        Me.LayoutControl1.Location = New System.Drawing.Point(12, 3)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2827, 58, 650, 400)
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(642, 400)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDSbtnSendNotification
        '
        Me.INDSbtnSendNotification.Location = New System.Drawing.Point(12, 366)
        Me.INDSbtnSendNotification.Name = "INDSbtnSendNotification"
        Me.INDSbtnSendNotification.Size = New System.Drawing.Size(618, 22)
        Me.INDSbtnSendNotification.StyleController = Me.LayoutControl1
        Me.INDSbtnSendNotification.TabIndex = 4
        Me.INDSbtnSendNotification.Text = "Enviar Notificación"
        '
        'INDpcCorreos
        '
        Me.INDpcCorreos.Location = New System.Drawing.Point(39, 12)
        Me.INDpcCorreos.Name = "INDpcCorreos"
        Me.INDpcCorreos.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpcCorreos.Properties.Appearance.Options.UseFont = True
        Me.INDpcCorreos.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDpcCorreos.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpcCorreos.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpcCorreos.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDpcCorreos.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDpcCorreos.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDpcCorreos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)})
        Me.INDpcCorreos.Properties.Mask.EditMask = "[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})"
        Me.INDpcCorreos.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDpcCorreos.Properties.Mask.ShowPlaceHolders = False
        Me.INDpcCorreos.Properties.MaxLength = 60
        Me.INDpcCorreos.Properties.PopupSizeable = False
        Me.INDpcCorreos.Properties.ShowPopupCloseButton = False
        Me.INDpcCorreos.Properties.ShowPopupShadow = False
        Me.INDpcCorreos.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDpcCorreos.Size = New System.Drawing.Size(578, 28)
        Me.INDpcCorreos.StyleController = Me.LayoutControl1
        Me.INDpcCorreos.TabIndex = 0
        '
        'INDgcEmail
        '
        Me.INDgcEmail.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.INDgcEmail.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.INDgcEmail.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.INDgcEmail.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.INDgcEmail.EmbeddedNavigator.Buttons.First.Visible = False
        Me.INDgcEmail.EmbeddedNavigator.Buttons.Last.Visible = False
        Me.INDgcEmail.EmbeddedNavigator.Buttons.Next.Visible = False
        Me.INDgcEmail.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.INDgcEmail.EmbeddedNavigator.Buttons.Prev.Visible = False
        Me.INDgcEmail.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.INDgcEmail.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.INDgcEmail.Location = New System.Drawing.Point(12, 44)
        Me.INDgcEmail.MainView = Me.INDgcEmailView
        Me.INDgcEmail.Name = "INDgcEmail"
        Me.INDgcEmail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDbtnEliminarEmail, Me.INDrepSleEmailType})
        Me.INDgcEmail.Size = New System.Drawing.Size(605, 318)
        Me.INDgcEmail.TabIndex = 1
        Me.INDgcEmail.UseEmbeddedNavigator = True
        Me.INDgcEmail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgcEmailView})
        '
        'INDgcEmailView
        '
        Me.INDgcEmailView.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgcEmailView.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgcEmailView.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgcEmailView.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgcEmailView.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgcEmailView.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgcEmailView.Appearance.GroupRow.Options.UseFont = True
        Me.INDgcEmailView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgcEmailView.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgcEmailView.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgcEmailView.Appearance.Row.Options.UseFont = True
        Me.INDgcEmailView.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgcEmailView.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgcEmailView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn5})
        Me.INDgcEmailView.GridControl = Me.INDgcEmail
        Me.INDgcEmailView.Name = "INDgcEmailView"
        Me.INDgcEmailView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgcEmailView.OptionsView.EnableAppearanceOddRow = True
        Me.INDgcEmailView.OptionsView.ShowAutoFilterRow = True
        Me.INDgcEmailView.OptionsView.ShowGroupPanel = False
        Me.INDgcEmailView.Tag = 235
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgcEmailView, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Email"
        Me.GridColumn2.FieldName = "Email1"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 336
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Acciones"
        Me.GridColumn5.ColumnEdit = Me.INDbtnEliminarEmail
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 76
        '
        'INDbtnEliminarEmail
        '
        Me.INDbtnEliminarEmail.AutoHeight = False
        Me.INDbtnEliminarEmail.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Eliminar", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnEliminarEmail.Name = "INDbtnEliminarEmail"
        Me.INDbtnEliminarEmail.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDrepSleEmailType
        '
        Me.INDrepSleEmailType.AutoHeight = False
        Me.INDrepSleEmailType.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleEmailType.DisplayMember = "Item2"
        Me.INDrepSleEmailType.Name = "INDrepSleEmailType"
        Me.INDrepSleEmailType.NullText = ""
        Me.INDrepSleEmailType.PopupView = Me.RepositoryItemSearchLookUpEdit1View
        Me.INDrepSleEmailType.ValueMember = "Item1"
        '
        'RepositoryItemSearchLookUpEdit1View
        '
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11})
        Me.RepositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1View.Name = "RepositoryItemSearchLookUpEdit1View"
        Me.RepositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Descripción"
        Me.GridColumn11.FieldName = "Item2"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlbEmail, Me.LayoutControlItem3, Me.LayoutControlItem1})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(642, 400)
        Me.Root.TextVisible = False
        '
        'INDlbEmail
        '
        Me.INDlbEmail.Control = Me.INDpcCorreos
        Me.INDlbEmail.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlbEmail.CustomizationFormText = "INDlbEmail"
        Me.INDlbEmail.Location = New System.Drawing.Point(0, 0)
        Me.INDlbEmail.Name = "INDlbEmail"
        Me.INDlbEmail.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 15, 2, 2)
        Me.INDlbEmail.Size = New System.Drawing.Size(622, 32)
        Me.INDlbEmail.Text = "Email"
        Me.INDlbEmail.TextSize = New System.Drawing.Size(24, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDgcEmail
        Me.LayoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 15, 2, 2)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(622, 322)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSbtnSendNotification
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 354)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(622, 26)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmSendElectronicPayrollNotification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 416)
        Me.Controls.Add(Me.LayoutControl1)
        Me.IconOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.login
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmSendElectronicPayrollNotification"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Enviar Notificación"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDpcCorreos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcEmailView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnEliminarEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleEmailType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlbEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents INDpcCorreos As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDgcEmail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgcEmailView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepSleEmailType As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbtnEliminarEmail As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDlbEmail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbtnSendNotification As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
