<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PopUpActionQualityControl
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbAceptar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsbCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGleProductClassification = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDMeObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDGleCauseRejection = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit6View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcProductClassification = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCauseRejection = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGleProductClassification.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleCauseRejection.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit6View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcProductClassification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCauseRejection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDsbAceptar)
        Me.LayoutControl1.Controls.Add(Me.INDsbCancel)
        Me.LayoutControl1.Controls.Add(Me.INDGleProductClassification)
        Me.LayoutControl1.Controls.Add(Me.INDMeObservations)
        Me.LayoutControl1.Controls.Add(Me.INDGleCauseRejection)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(413, 303)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDsbAceptar
        '
        Me.INDsbAceptar.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAceptar.Appearance.Options.UseFont = True
        Me.INDsbAceptar.Location = New System.Drawing.Point(12, 263)
        Me.INDsbAceptar.Name = "INDsbAceptar"
        Me.INDsbAceptar.Size = New System.Drawing.Size(192, 28)
        Me.INDsbAceptar.StyleController = Me.LayoutControl1
        Me.INDsbAceptar.TabIndex = 12
        Me.INDsbAceptar.Text = "Aceptar"
        '
        'INDsbCancel
        '
        Me.INDsbCancel.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbCancel.Appearance.Options.UseFont = True
        Me.INDsbCancel.Location = New System.Drawing.Point(208, 263)
        Me.INDsbCancel.Name = "INDsbCancel"
        Me.INDsbCancel.Size = New System.Drawing.Size(193, 28)
        Me.INDsbCancel.StyleController = Me.LayoutControl1
        Me.INDsbCancel.TabIndex = 11
        Me.INDsbCancel.Text = "Cancelar"
        '
        'INDGleProductClassification
        '
        Me.INDGleProductClassification.Location = New System.Drawing.Point(12, 34)
        Me.INDGleProductClassification.Name = "INDGleProductClassification"
        Me.INDGleProductClassification.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleProductClassification.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleProductClassification.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleProductClassification.Properties.Appearance.Options.UseFont = True
        Me.INDGleProductClassification.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleProductClassification.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleProductClassification.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleProductClassification.Properties.NullText = ""
        Me.INDGleProductClassification.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleProductClassification.Size = New System.Drawing.Size(386, 28)
        Me.INDGleProductClassification.StyleController = Me.LayoutControl1
        Me.INDGleProductClassification.TabIndex = 5
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'INDMeObservations
        '
        Me.INDMeObservations.Location = New System.Drawing.Point(12, 152)
        Me.INDMeObservations.Name = "INDMeObservations"
        Me.INDMeObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Semilight", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDMeObservations.Properties.Appearance.Options.UseFont = True
        Me.INDMeObservations.Properties.MaxLength = 2000
        Me.INDMeObservations.Size = New System.Drawing.Size(389, 107)
        Me.INDMeObservations.StyleController = Me.LayoutControl1
        Me.INDMeObservations.TabIndex = 10
        '
        'INDGleCauseRejection
        '
        Me.INDGleCauseRejection.EnterMoveNextControl = True
        Me.INDGleCauseRejection.Location = New System.Drawing.Point(12, 98)
        Me.INDGleCauseRejection.Name = "INDGleCauseRejection"
        Me.INDGleCauseRejection.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleCauseRejection.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleCauseRejection.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDGleCauseRejection.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleCauseRejection.Properties.Appearance.Options.UseFont = True
        Me.INDGleCauseRejection.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleCauseRejection.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleCauseRejection.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleCauseRejection.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleCauseRejection.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleCauseRejection.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleCauseRejection.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleCauseRejection.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDGleCauseRejection.Properties.DisplayMember = "CodeName"
        Me.INDGleCauseRejection.Properties.NullText = ""
        Me.INDGleCauseRejection.Properties.PopupSizeable = False
        Me.INDGleCauseRejection.Properties.PopupView = Me.SearchLookUpEdit6View
        Me.INDGleCauseRejection.Properties.ShowFooter = False
        Me.INDGleCauseRejection.Properties.ValueMember = "Id"
        Me.INDGleCauseRejection.Size = New System.Drawing.Size(386, 28)
        Me.INDGleCauseRejection.StyleController = Me.LayoutControl1
        Me.INDGleCauseRejection.TabIndex = 5
        '
        'SearchLookUpEdit6View
        '
        Me.SearchLookUpEdit6View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit6View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit6View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit6View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit6View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit6View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit6View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit6View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit6View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit6View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit6View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEdit6View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit6View.Name = "SearchLookUpEdit6View"
        Me.SearchLookUpEdit6View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit6View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit6View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit6View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit6View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit6View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Codigo"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.INDlcProductClassification, Me.INDLcObservations, Me.INDlciCauseRejection})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(413, 303)
        Me.Root.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDsbCancel
        Me.LayoutControlItem1.Location = New System.Drawing.Point(196, 251)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(197, 32)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(197, 32)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(197, 32)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDsbAceptar
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 251)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(196, 32)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(196, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(196, 32)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDlcProductClassification
        '
        Me.INDlcProductClassification.Control = Me.INDGleProductClassification
        Me.INDlcProductClassification.Location = New System.Drawing.Point(0, 0)
        Me.INDlcProductClassification.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlcProductClassification.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlcProductClassification.Name = "INDlcProductClassification"
        Me.INDlcProductClassification.Size = New System.Drawing.Size(393, 60)
        Me.INDlcProductClassification.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlcProductClassification.Text = "Clasificación de Defectos"
        Me.INDlcProductClassification.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlcProductClassification.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlcProductClassification.TextSize = New System.Drawing.Size(163, 17)
        Me.INDlcProductClassification.TextToControlDistance = 5
        Me.INDlcProductClassification.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLcObservations
        '
        Me.INDLcObservations.Control = Me.INDMeObservations
        Me.INDLcObservations.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLcObservations.CustomizationFormText = "Observaciones"
        Me.INDLcObservations.Location = New System.Drawing.Point(0, 120)
        Me.INDLcObservations.Name = "INDLcObservations"
        Me.INDLcObservations.Size = New System.Drawing.Size(393, 131)
        Me.INDLcObservations.Text = "Observación"
        Me.INDLcObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLcObservations.TextSize = New System.Drawing.Size(79, 17)
        '
        'INDlciCauseRejection
        '
        Me.INDlciCauseRejection.Control = Me.INDGleCauseRejection
        Me.INDlciCauseRejection.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlciCauseRejection.CustomizationFormText = "Causa de Rechazo"
        Me.INDlciCauseRejection.Location = New System.Drawing.Point(0, 60)
        Me.INDlciCauseRejection.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciCauseRejection.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciCauseRejection.Name = "INDlciCauseRejection"
        Me.INDlciCauseRejection.ShowInCustomizationForm = False
        Me.INDlciCauseRejection.Size = New System.Drawing.Size(393, 60)
        Me.INDlciCauseRejection.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCauseRejection.Text = "Causa de Rechazo"
        Me.INDlciCauseRejection.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCauseRejection.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCauseRejection.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciCauseRejection.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'PopUpActionQualityControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(413, 303)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PopUpActionQualityControl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Rechazar Producto"
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGleProductClassification.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleCauseRejection.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit6View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcProductClassification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCauseRejection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGleProductClassification As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Public WithEvents INDMeObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDsbAceptar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDsbCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcProductClassification As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcObservations As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents INDGleCauseRejection As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit6View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCauseRejection As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
