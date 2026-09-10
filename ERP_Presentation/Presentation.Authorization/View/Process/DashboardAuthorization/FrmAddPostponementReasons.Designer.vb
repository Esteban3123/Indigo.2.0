Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAddPostponementReasons
    Inherits FormBase

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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDslePostponementReasons = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDdtePostponementDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDmemoPostponementObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemPostponementReasons = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPostponementObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPostponementDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAddCancellationReason = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDslePostponementReasons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDdtePostponementDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdtePostponementDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoPostponementObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPostponementReasons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPostponementObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPostponementDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(421, 373)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(421, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(421, 98)
        '
        'INDslePostponementReasons
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDslePostponementReasons, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDslePostponementReasons, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDslePostponementReasons, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDslePostponementReasons, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDslePostponementReasons, False)
        Me.INDslePostponementReasons.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDslePostponementReasons, False)
        Me.INDslePostponementReasons.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDslePostponementReasons, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDslePostponementReasons.Name = "INDslePostponementReasons"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDslePostponementReasons, False)
        Me.INDslePostponementReasons.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDslePostponementReasons.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDslePostponementReasons.Properties.Appearance.Options.UseBackColor = True
        Me.INDslePostponementReasons.Properties.Appearance.Options.UseFont = True
        Me.INDslePostponementReasons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDslePostponementReasons.Properties.DisplayMember = "CodeName"
        Me.INDslePostponementReasons.Properties.NullText = ""
        Me.INDslePostponementReasons.Properties.PopupSizeable = False
        Me.INDslePostponementReasons.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDslePostponementReasons.Properties.ShowFooter = False
        Me.INDslePostponementReasons.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDslePostponementReasons, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDslePostponementReasons, True)
        Me.INDslePostponementReasons.Size = New System.Drawing.Size(386, 28)
        Me.INDslePostponementReasons.StyleController = Me.INDlyRoot
        Me.INDslePostponementReasons.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDslePostponementReasons, "")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDslePostponementReasons, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDslePostponementReasons, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDslePostponementReasons, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDslePostponementReasons, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 232
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 1150
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDdtePostponementDate)
        Me.INDlyRoot.Controls.Add(Me.INDmemoPostponementObservations)
        Me.INDlyRoot.Controls.Add(Me.INDslePostponementReasons)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(417, 324)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDdtePostponementDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdtePostponementDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdtePostponementDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdtePostponementDate, False)
        Me.INDdtePostponementDate.EditValue = Nothing
        Me.INDdtePostponementDate.EnterMoveNextControl = True
        Me.INDdtePostponementDate.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDdtePostponementDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdtePostponementDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdtePostponementDate.Name = "INDdtePostponementDate"
        Me.INDdtePostponementDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdtePostponementDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdtePostponementDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdtePostponementDate.Properties.Appearance.Options.UseFont = True
        Me.INDdtePostponementDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdtePostponementDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdtePostponementDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdtePostponementDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdtePostponementDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdtePostponementDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdtePostponementDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdtePostponementDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdtePostponementDate.StyleController = Me.INDlyRoot
        Me.INDdtePostponementDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdtePostponementDate, 0)
        '
        'INDmemoPostponementObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoPostponementObservations, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoPostponementObservations, False)
        Me.INDmemoPostponementObservations.EnterMoveNextControl = True
        Me.INDmemoPostponementObservations.Location = New System.Drawing.Point(12, 158)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoPostponementObservations, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoPostponementObservations.Name = "INDmemoPostponementObservations"
        Me.INDmemoPostponementObservations.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmemoPostponementObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoPostponementObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoPostponementObservations.Properties.Appearance.Options.UseFont = True
        Me.INDmemoPostponementObservations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoPostponementObservations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoPostponementObservations.Properties.MaxLength = 500
        Me.INDmemoPostponementObservations.Size = New System.Drawing.Size(386, 120)
        Me.INDmemoPostponementObservations.StyleController = Me.INDlyRoot
        Me.INDmemoPostponementObservations.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoPostponementObservations, 0)
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemPostponementReasons, Me.INDlyItemPostponementObservations, Me.INDlyItemPostponementDate})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(417, 324)
        Me.Root.TextVisible = False
        '
        'INDlyItemPostponementReasons
        '
        Me.INDlyItemPostponementReasons.Control = Me.INDslePostponementReasons
        Me.INDlyItemPostponementReasons.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPostponementReasons.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPostponementReasons.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPostponementReasons.Name = "INDlyItemPostponementReasons"
        Me.INDlyItemPostponementReasons.Size = New System.Drawing.Size(397, 60)
        Me.INDlyItemPostponementReasons.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPostponementReasons.Text = "Motivo"
        Me.INDlyItemPostponementReasons.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPostponementReasons.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPostponementReasons.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPostponementReasons.TextToControlDistance = 5
        '
        'INDlyItemPostponementObservations
        '
        Me.INDlyItemPostponementObservations.Control = Me.INDmemoPostponementObservations
        Me.INDlyItemPostponementObservations.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemPostponementObservations.MaxSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemPostponementObservations.MinSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemPostponementObservations.Name = "INDlyItemPostponementObservations"
        Me.INDlyItemPostponementObservations.Size = New System.Drawing.Size(397, 184)
        Me.INDlyItemPostponementObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPostponementObservations.Text = "Observación"
        Me.INDlyItemPostponementObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPostponementObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPostponementObservations.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPostponementObservations.TextToControlDistance = 5
        '
        'INDlyItemPostponementDate
        '
        Me.INDlyItemPostponementDate.Control = Me.INDdtePostponementDate
        Me.INDlyItemPostponementDate.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemPostponementDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPostponementDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPostponementDate.Name = "INDlyItemPostponementDate"
        Me.INDlyItemPostponementDate.Size = New System.Drawing.Size(397, 60)
        Me.INDlyItemPostponementDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPostponementDate.Text = "Fecha Postergación"
        Me.INDlyItemPostponementDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPostponementDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPostponementDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPostponementDate.TextToControlDistance = 5
        '
        'INDbtnAddCancellationReason
        '
        Me.INDbtnAddCancellationReason.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddCancellationReason.Appearance.Options.UseFont = True
        Me.INDbtnAddCancellationReason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddCancellationReason.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddCancellationReason, True)
        Me.INDbtnAddCancellationReason.Name = "INDbtnAddCancellationReason"
        Me.INDbtnAddCancellationReason.Size = New System.Drawing.Size(413, 36)
        Me.INDbtnAddCancellationReason.TabIndex = 0
        Me.INDbtnAddCancellationReason.Text = "Aceptar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAddCancellationReason)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(2, 331)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(417, 40)
        Me.PanelControl1.TabIndex = 0
        '
        'FrmAddPostponementReasons
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 495)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAddPostponementReasons"
        Me.Opacity = 1.0R
        Me.Text = "Motivo de Postergación"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDslePostponementReasons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDdtePostponementDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdtePostponementDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoPostponementObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPostponementReasons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPostponementObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPostponementDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddCancellationReason As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDmemoPostponementObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDslePostponementReasons As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemPostponementReasons As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPostponementObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDdtePostponementDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemPostponementDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As IndigoDate
End Class
