Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmElectronicPayrollConcepts
    Inherits FormBase

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
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleConceptType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTeName = New DevExpress.XtraEditors.TextEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlygMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemConceptType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit11 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridView11 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLycRoot.SuspendLayout()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleConceptType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemConceptType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLycRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1451, 559)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1451, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLycRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 550)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDLycRoot
        '
        Me.INDLycRoot.Controls.Add(Me.INDBtnCode)
        Me.INDLycRoot.Controls.Add(Me.INDsleConceptType)
        Me.INDLycRoot.Controls.Add(Me.INDTeName)
        Me.INDLycRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLycRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDLycRoot.Name = "INDLycRoot"
        Me.INDLycRoot.Root = Me.Root
        Me.INDLycRoot.Size = New System.Drawing.Size(1247, 550)
        Me.INDLycRoot.TabIndex = 1
        Me.INDLycRoot.Text = "LayoutControl1"
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDBtnCode, True)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDBtnCode, False)
        Me.INDBtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit11.SetMascara(Me.INDBtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBtnCode.Name = "INDBtnCode"
        Me.INDBtnCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDBtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDBtnCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseForeColor = True
        EditorButtonImageOptions2.Image = Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro
        Me.INDBtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBtnCode.Properties.MaxLength = 20
        Me.INDBtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBtnCode.StyleController = Me.INDLycRoot
        Me.INDBtnCode.TabIndex = 0
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDBtnCode, 0)
        '
        'INDsleConceptType
        '
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDsleConceptType, True)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDsleConceptType, True)
        Me.INDsleConceptType.Location = New System.Drawing.Point(24, 203)
        Me.IndigoTextEdit11.SetMascara(Me.INDsleConceptType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleConceptType.Name = "INDsleConceptType"
        Me.INDsleConceptType.Properties.AcceptEditorTextAsNewValue = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDsleConceptType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleConceptType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleConceptType.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDsleConceptType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleConceptType.Properties.Appearance.Options.UseFont = True
        Me.INDsleConceptType.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleConceptType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleConceptType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleConceptType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleConceptType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleConceptType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleConceptType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleConceptType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleConceptType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleConceptType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleConceptType.Properties.DisplayMember = "Item2"
        Me.INDsleConceptType.Properties.ImmediatePopup = True
        Me.INDsleConceptType.Properties.NullText = ""
        Me.INDsleConceptType.Properties.PopupSizeable = False
        Me.INDsleConceptType.Properties.PopupView = Me.GridLookUpEdit1View1
        Me.INDsleConceptType.Properties.ShowFooter = False
        Me.INDsleConceptType.Properties.ValueMember = "Item1"
        Me.INDsleConceptType.Size = New System.Drawing.Size(386, 28)
        Me.INDsleConceptType.StyleController = Me.INDLycRoot
        Me.INDsleConceptType.TabIndex = 2
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDsleConceptType, 0)
        Me.INDsleConceptType.ToolTip = "Este Campo es Necesario"
        '
        'GridLookUpEdit1View1
        '
        Me.GridLookUpEdit1View1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit1View1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View1.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View1.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn41})
        Me.GridLookUpEdit1View1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View1.Name = "GridLookUpEdit1View1"
        Me.GridLookUpEdit1View1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View1.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View1.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.GridLookUpEdit1View1, False)
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "Descripción"
        Me.GridColumn41.FieldName = "Item2"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.Visible = True
        Me.GridColumn41.VisibleIndex = 0
        '
        'INDTeName
        '
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDTeName, True)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDTeName, True)
        Me.INDTeName.Enabled = False
        Me.INDTeName.EnterMoveNextControl = True
        Me.INDTeName.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit11.SetMascara(Me.INDTeName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTeName.MaximumSize = New System.Drawing.Size(386, 28)
        Me.INDTeName.MinimumSize = New System.Drawing.Size(386, 28)
        Me.INDTeName.Name = "INDTeName"
        Me.INDTeName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTeName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTeName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeName.Properties.Appearance.Options.UseFont = True
        Me.INDTeName.Properties.Appearance.Options.UseForeColor = True
        Me.INDTeName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTeName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTeName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTeName.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTeName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTeName.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTeName.Properties.MaxLength = 100
        Me.INDTeName.Size = New System.Drawing.Size(386, 28)
        Me.INDTeName.StyleController = Me.INDLycRoot
        Me.INDTeName.TabIndex = 1
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDTeName, 0)
        Me.INDTeName.ToolTip = "Este Campo es Necesario"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.INDlygMain})
        Me.Root.Name = "Root"
        Me.Root.OptionsItemText.TextToControlDistance = 5
        Me.Root.Size = New System.Drawing.Size(1247, 550)
        Me.Root.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 237)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(1227, 293)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlygMain
        '
        Me.INDlygMain.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygMain.AppearanceGroup.Options.UseFont = True
        Me.INDlygMain.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygMain.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygMain.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMain.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygMain.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygMain.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygMain.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMain.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygMain.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMain.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygMain.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygMain.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygMain, False)
        Me.INDlygMain.CustomizationFormText = "Información General"
        Me.INDlygMain.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemConceptType, Me.INDlyItemName})
        Me.INDlygMain.Location = New System.Drawing.Point(0, 0)
        Me.INDlygMain.Name = "INDlygMain"
        Me.INDlygMain.OptionsPrint.AppearanceGroupCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlygMain.OptionsPrint.AppearanceGroupCaption.Options.UseFont = True
        Me.INDlygMain.OptionsPrint.AppearanceItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlygMain.OptionsPrint.AppearanceItem.Options.UseFont = True
        Me.INDlygMain.OptionsPrint.AppearanceItemControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlygMain.OptionsPrint.AppearanceItemControl.Options.UseFont = True
        Me.INDlygMain.OptionsPrint.AppearanceItemText.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlygMain.OptionsPrint.AppearanceItemText.Options.UseFont = True
        Me.INDlygMain.Size = New System.Drawing.Size(1227, 237)
        Me.INDlygMain.Text = "Información Principal"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDBtnCode
        Me.INDlyItemCode.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.OptionsPrint.AppearanceItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlyItemCode.OptionsPrint.AppearanceItem.Options.UseFont = True
        Me.INDlyItemCode.OptionsPrint.AppearanceItemControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlyItemCode.OptionsPrint.AppearanceItemControl.Options.UseFont = True
        Me.INDlyItemCode.OptionsPrint.AppearanceItemText.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlyItemCode.OptionsPrint.AppearanceItemText.Options.UseFont = True
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(1203, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemConceptType
        '
        Me.INDlyItemConceptType.Control = Me.INDsleConceptType
        Me.INDlyItemConceptType.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemConceptType.CustomizationFormText = "Nombre"
        Me.INDlyItemConceptType.Location = New System.Drawing.Point(0, 124)
        Me.INDlyItemConceptType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemConceptType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemConceptType.Name = "INDlyItemConceptType"
        Me.INDlyItemConceptType.OptionsPrint.AppearanceItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlyItemConceptType.OptionsPrint.AppearanceItem.Options.UseFont = True
        Me.INDlyItemConceptType.OptionsPrint.AppearanceItemControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlyItemConceptType.OptionsPrint.AppearanceItemControl.Options.UseFont = True
        Me.INDlyItemConceptType.OptionsPrint.AppearanceItemText.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlyItemConceptType.OptionsPrint.AppearanceItemText.Options.UseFont = True
        Me.INDlyItemConceptType.ShowInCustomizationForm = False
        Me.INDlyItemConceptType.Size = New System.Drawing.Size(1203, 60)
        Me.INDlyItemConceptType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemConceptType.Text = "Tipo de Concepto"
        Me.INDlyItemConceptType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemConceptType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemConceptType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemConceptType.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDTeName
        Me.INDlyItemName.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemName.CustomizationFormText = "Nombre"
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.OptionsPrint.AppearanceItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlyItemName.OptionsPrint.AppearanceItem.Options.UseFont = True
        Me.INDlyItemName.OptionsPrint.AppearanceItemControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlyItemName.OptionsPrint.AppearanceItemControl.Options.UseFont = True
        Me.INDlyItemName.OptionsPrint.AppearanceItemText.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDlyItemName.OptionsPrint.AppearanceItemText.Options.UseFont = True
        Me.INDlyItemName.Size = New System.Drawing.Size(1203, 64)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemName.TextSize = New System.Drawing.Size(208, 21)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'IndigoGridView11
        '
        Me.IndigoGridView11.RaiseMenuPopUp = True
        Me.IndigoGridView11.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit11
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'FrmElectronicPayrollConcepts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1451, 694)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmElectronicPayrollConcepts"
        Me.Opacity = 1.0R
        Me.Text = "FrmElectronicPayrollConcepts"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLycRoot.ResumeLayout(False)
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleConceptType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemConceptType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDLycRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents IndigoTextEdit11 As IndigoTextEdit
    Friend WithEvents INDsleConceptType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView11 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDTeName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlygMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemConceptType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
End Class
