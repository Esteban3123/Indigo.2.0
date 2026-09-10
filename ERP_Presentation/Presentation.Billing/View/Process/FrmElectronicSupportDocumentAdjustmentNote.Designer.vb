Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout
Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmElectronicSupportDocumentAdjustmentNote
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGleNature = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSpnTotal = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpnIVA = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpnSubtotal = New DevExpress.XtraEditors.SpinEdit()
        Me.INDMeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDteSupportDocumentValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeSupportDocumentDate = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleSupportDocument = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDeDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBeCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSupportDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSupportDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSupportDocumentValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNoteType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNature = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgLiquidation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciSubtotal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciIva = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTotal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDGleNature.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnIVA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnSubtotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteSupportDocumentValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeSupportDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleSupportDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBeCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSupportDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSupportDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSupportDocumentValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNoteType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSubtotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIva, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1088, 545)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1088, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1088, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 536)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDGleNature)
        Me.INDLcRoot.Controls.Add(Me.INDGleType)
        Me.INDLcRoot.Controls.Add(Me.INDSpnTotal)
        Me.INDLcRoot.Controls.Add(Me.INDSpnIVA)
        Me.INDLcRoot.Controls.Add(Me.INDSpnSubtotal)
        Me.INDLcRoot.Controls.Add(Me.INDMeDescription)
        Me.INDLcRoot.Controls.Add(Me.INDteSupportDocumentValue)
        Me.INDLcRoot.Controls.Add(Me.INDTeSupportDocumentDate)
        Me.INDLcRoot.Controls.Add(Me.INDSleSupportDocument)
        Me.INDLcRoot.Controls.Add(Me.INDDeDate)
        Me.INDLcRoot.Controls.Add(Me.INDBeCode)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.Root
        Me.INDLcRoot.Size = New System.Drawing.Size(884, 536)
        Me.INDLcRoot.TabIndex = 1
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDGleNature
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleNature, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleNature, False)
        Me.INDGleNature.EnterMoveNextControl = True
        Me.INDGleNature.Location = New System.Drawing.Point(24, 258)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleNature, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleNature.Name = "INDGleNature"
        Me.INDGleNature.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleNature.Properties.Appearance.Options.UseFont = True
        Me.INDGleNature.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleNature.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleNature.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleNature.Properties.DisplayMember = "Item2"
        Me.INDGleNature.Properties.NullText = ""
        Me.INDGleNature.Properties.PopupView = Me.GridLookUpEdit2View
        Me.INDGleNature.Properties.ValueMember = "Item1"
        Me.INDGleNature.Size = New System.Drawing.Size(386, 28)
        Me.INDGleNature.StyleController = Me.INDLcRoot
        Me.INDGleNature.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleNature, 0)
        '
        'GridLookUpEdit2View
        '
        Me.GridLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.GridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit2View.Name = "GridLookUpEdit2View"
        Me.GridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit2View, False)
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Naturaleza"
        Me.GridColumn8.FieldName = "Item2"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'INDGleType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleType, False)
        Me.INDGleType.EnterMoveNextControl = True
        Me.INDGleType.Location = New System.Drawing.Point(24, 198)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleType.Name = "INDGleType"
        Me.INDGleType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleType.Properties.Appearance.Options.UseFont = True
        Me.INDGleType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleType.Properties.DisplayMember = "Item2"
        Me.INDGleType.Properties.NullText = ""
        Me.INDGleType.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleType.Properties.ValueMember = "Item1"
        Me.INDGleType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleType.StyleController = Me.INDLcRoot
        Me.INDGleType.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleType, 0)
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
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tipo"
        Me.GridColumn7.FieldName = "Item2"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        '
        'INDSpnTotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnTotal, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnTotal, True)
        Me.INDSpnTotal.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnTotal.Location = New System.Drawing.Point(438, 195)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnTotal, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDSpnTotal.Name = "INDSpnTotal"
        Me.INDSpnTotal.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDSpnTotal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnTotal.Properties.Appearance.Options.UseFont = True
        Me.INDSpnTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSpnTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDSpnTotal.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnTotal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnTotal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnTotal.Properties.Mask.EditMask = "c0"
        Me.INDSpnTotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpnTotal.Properties.ReadOnly = True
        Me.INDSpnTotal.Size = New System.Drawing.Size(386, 28)
        Me.INDSpnTotal.StyleController = Me.INDLcRoot
        Me.INDSpnTotal.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnTotal, 0)
        '
        'INDSpnIVA
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnIVA, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnIVA, True)
        Me.INDSpnIVA.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnIVA.EnterMoveNextControl = True
        Me.INDSpnIVA.Location = New System.Drawing.Point(438, 135)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnIVA, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDSpnIVA.Name = "INDSpnIVA"
        Me.INDSpnIVA.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDSpnIVA.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnIVA.Properties.Appearance.Options.UseFont = True
        Me.INDSpnIVA.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSpnIVA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDSpnIVA.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnIVA.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnIVA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnIVA.Properties.Mask.EditMask = "c0"
        Me.INDSpnIVA.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpnIVA.Properties.ReadOnly = True
        Me.INDSpnIVA.Size = New System.Drawing.Size(386, 28)
        Me.INDSpnIVA.StyleController = Me.INDLcRoot
        Me.INDSpnIVA.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnIVA, 0)
        '
        'INDSpnSubtotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnSubtotal, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnSubtotal, True)
        Me.INDSpnSubtotal.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnSubtotal.EnterMoveNextControl = True
        Me.INDSpnSubtotal.Location = New System.Drawing.Point(438, 75)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnSubtotal, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDSpnSubtotal.Name = "INDSpnSubtotal"
        Me.INDSpnSubtotal.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDSpnSubtotal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnSubtotal.Properties.Appearance.Options.UseFont = True
        Me.INDSpnSubtotal.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSpnSubtotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDSpnSubtotal.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnSubtotal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnSubtotal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnSubtotal.Properties.Mask.EditMask = "c0"
        Me.INDSpnSubtotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpnSubtotal.Size = New System.Drawing.Size(386, 28)
        Me.INDSpnSubtotal.StyleController = Me.INDLcRoot
        Me.INDSpnSubtotal.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnSubtotal, 0)
        '
        'INDMeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDescription, True)
        Me.INDMeDescription.EnterMoveNextControl = True
        Me.INDMeDescription.Location = New System.Drawing.Point(24, 495)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeDescription.Name = "INDMeDescription"
        Me.INDMeDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDMeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDMeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDMeDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDMeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeDescription.Properties.MaxLength = 500
        Me.INDMeDescription.Size = New System.Drawing.Size(386, 94)
        Me.INDMeDescription.StyleController = Me.INDLcRoot
        Me.INDMeDescription.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDescription, 0)
        Me.INDMeDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDteSupportDocumentValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteSupportDocumentValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteSupportDocumentValue, False)
        Me.INDteSupportDocumentValue.EnterMoveNextControl = True
        Me.INDteSupportDocumentValue.Location = New System.Drawing.Point(24, 435)
        Me.IndigoTextEdit1.SetMascara(Me.INDteSupportDocumentValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteSupportDocumentValue.Name = "INDteSupportDocumentValue"
        Me.INDteSupportDocumentValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteSupportDocumentValue.Properties.Appearance.Options.UseFont = True
        Me.INDteSupportDocumentValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteSupportDocumentValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteSupportDocumentValue.Properties.Mask.EditMask = "c0"
        Me.INDteSupportDocumentValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteSupportDocumentValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteSupportDocumentValue.Properties.ReadOnly = True
        Me.INDteSupportDocumentValue.Size = New System.Drawing.Size(386, 28)
        Me.INDteSupportDocumentValue.StyleController = Me.INDLcRoot
        Me.INDteSupportDocumentValue.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteSupportDocumentValue, 0)
        '
        'INDTeSupportDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeSupportDocumentDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeSupportDocumentDate, False)
        Me.INDTeSupportDocumentDate.EnterMoveNextControl = True
        Me.INDTeSupportDocumentDate.Location = New System.Drawing.Point(24, 375)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeSupportDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeSupportDocumentDate.Name = "INDTeSupportDocumentDate"
        Me.INDTeSupportDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeSupportDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDTeSupportDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeSupportDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeSupportDocumentDate.Properties.ReadOnly = True
        Me.INDTeSupportDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDTeSupportDocumentDate.StyleController = Me.INDLcRoot
        Me.INDTeSupportDocumentDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeSupportDocumentDate, 0)
        '
        'INDSleSupportDocument
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleSupportDocument, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleSupportDocument, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleSupportDocument, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleSupportDocument, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleSupportDocument, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleSupportDocument, False)
        Me.INDSleSupportDocument.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleSupportDocument, False)
        Me.INDSleSupportDocument.Location = New System.Drawing.Point(24, 315)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleSupportDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleSupportDocument.Name = "INDSleSupportDocument"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleSupportDocument, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleSupportDocument, False)
        Me.INDSleSupportDocument.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleSupportDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleSupportDocument.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleSupportDocument.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleSupportDocument.Properties.Appearance.Options.UseFont = True
        Me.INDSleSupportDocument.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleSupportDocument.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleSupportDocument.Properties.DisplayMember = "FullName"
        Me.INDSleSupportDocument.Properties.NullText = ""
        Me.INDSleSupportDocument.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDSleSupportDocument.Properties.PopupSizeable = False
        Me.INDSleSupportDocument.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleSupportDocument.Properties.ShowFooter = False
        Me.INDSleSupportDocument.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleSupportDocument, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleSupportDocument, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleSupportDocument, True)
        Me.INDSleSupportDocument.Size = New System.Drawing.Size(386, 28)
        Me.INDSleSupportDocument.StyleController = Me.INDLcRoot
        Me.INDSleSupportDocument.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleSupportDocument, "754")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleSupportDocument, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleSupportDocument, "{0} - {1}")
        Me.INDSleSupportDocument.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleSupportDocument, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleSupportDocument, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
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
        Me.GridColumn1.Width = 165
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Fecha"
        Me.GridColumn2.FieldName = "DocumentDate"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 220
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Proveedor"
        Me.GridColumn3.FieldName = "SupplierThirdParty.NitName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 327
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "SubTotal"
        Me.GridColumn4.DisplayFormat.FormatString = "c0"
        Me.GridColumn4.FieldName = "SubTotalValue"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 124
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "IVA"
        Me.GridColumn5.DisplayFormat.FormatString = "c0"
        Me.GridColumn5.FieldName = "TaxValue"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 124
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Total"
        Me.GridColumn6.DisplayFormat.FormatString = "c0"
        Me.GridColumn6.FieldName = "TotalValue"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 138
        '
        'INDDeDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeDate, False)
        Me.INDDeDate.EditValue = Nothing
        Me.INDDeDate.EnterMoveNextControl = True
        Me.INDDeDate.Location = New System.Drawing.Point(24, 135)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeDate.Name = "INDDeDate"
        Me.INDDeDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDeDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDeDate.StyleController = Me.INDLcRoot
        Me.INDDeDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeDate, 0)
        '
        'INDBeCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBeCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBeCode, False)
        Me.INDBeCode.Location = New System.Drawing.Point(24, 75)
        Me.IndigoTextEdit1.SetMascara(Me.INDBeCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBeCode.Name = "INDBeCode"
        Me.INDBeCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBeCode.Properties.Appearance.Options.UseFont = True
        Me.INDBeCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBeCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Billing.My.Resources.Resources.BuscarMetro
        Me.INDBeCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBeCode.Properties.MaxLength = 20
        Me.INDBeCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBeCode.StyleController = Me.INDLcRoot
        Me.INDBeCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBeCode, 0)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgRoot, Me.INDLcgLiquidation})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(867, 613)
        Me.Root.TextVisible = False
        '
        'INDLcgRoot
        '
        Me.INDLcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDLcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgRoot, False)
        Me.INDLcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciDate, Me.INDLciSupportDocument, Me.INDLciSupportDocumentDate, Me.INDLciSupportDocumentValue, Me.INDLciDescription, Me.INDLciNoteType, Me.INDLciNature})
        Me.INDLcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgRoot.Name = "INDLcgRoot"
        Me.INDLcgRoot.Size = New System.Drawing.Size(414, 593)
        Me.INDLcgRoot.Text = "Datos principales"
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDBeCode
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.Size = New System.Drawing.Size(390, 60)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(167, 17)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciDate
        '
        Me.INDLciDate.Control = Me.INDDeDate
        Me.INDLciDate.Location = New System.Drawing.Point(0, 60)
        Me.INDLciDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciDate.Name = "INDLciDate"
        Me.INDLciDate.Size = New System.Drawing.Size(390, 60)
        Me.INDLciDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDate.Text = "Fecha"
        Me.INDLciDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDate.TextSize = New System.Drawing.Size(167, 17)
        Me.INDLciDate.TextToControlDistance = 5
        '
        'INDLciSupportDocument
        '
        Me.INDLciSupportDocument.Control = Me.INDSleSupportDocument
        Me.INDLciSupportDocument.Location = New System.Drawing.Point(0, 240)
        Me.INDLciSupportDocument.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciSupportDocument.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciSupportDocument.Name = "INDLciSupportDocument"
        Me.INDLciSupportDocument.Size = New System.Drawing.Size(390, 60)
        Me.INDLciSupportDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSupportDocument.Text = "Documento soporte"
        Me.INDLciSupportDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSupportDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciSupportDocument.TextSize = New System.Drawing.Size(167, 17)
        Me.INDLciSupportDocument.TextToControlDistance = 5
        '
        'INDLciSupportDocumentDate
        '
        Me.INDLciSupportDocumentDate.Control = Me.INDTeSupportDocumentDate
        Me.INDLciSupportDocumentDate.Location = New System.Drawing.Point(0, 300)
        Me.INDLciSupportDocumentDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciSupportDocumentDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciSupportDocumentDate.Name = "INDLciSupportDocumentDate"
        Me.INDLciSupportDocumentDate.Size = New System.Drawing.Size(390, 60)
        Me.INDLciSupportDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSupportDocumentDate.Text = "Fecha documento soporte"
        Me.INDLciSupportDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSupportDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciSupportDocumentDate.TextSize = New System.Drawing.Size(167, 17)
        Me.INDLciSupportDocumentDate.TextToControlDistance = 5
        Me.INDLciSupportDocumentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciSupportDocumentValue
        '
        Me.INDLciSupportDocumentValue.Control = Me.INDteSupportDocumentValue
        Me.INDLciSupportDocumentValue.Location = New System.Drawing.Point(0, 360)
        Me.INDLciSupportDocumentValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciSupportDocumentValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciSupportDocumentValue.Name = "INDLciSupportDocumentValue"
        Me.INDLciSupportDocumentValue.Size = New System.Drawing.Size(390, 60)
        Me.INDLciSupportDocumentValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSupportDocumentValue.Text = "Valor documento soporte"
        Me.INDLciSupportDocumentValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSupportDocumentValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciSupportDocumentValue.TextSize = New System.Drawing.Size(167, 17)
        Me.INDLciSupportDocumentValue.TextToControlDistance = 5
        Me.INDLciSupportDocumentValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciDescription
        '
        Me.INDLciDescription.Control = Me.INDMeDescription
        Me.INDLciDescription.Location = New System.Drawing.Point(0, 420)
        Me.INDLciDescription.MaxSize = New System.Drawing.Size(390, 120)
        Me.INDLciDescription.MinSize = New System.Drawing.Size(390, 120)
        Me.INDLciDescription.Name = "INDLciDescription"
        Me.INDLciDescription.Size = New System.Drawing.Size(390, 120)
        Me.INDLciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDescription.Text = "Descripción"
        Me.INDLciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDescription.TextSize = New System.Drawing.Size(167, 17)
        Me.INDLciDescription.TextToControlDistance = 5
        '
        'INDLciNoteType
        '
        Me.INDLciNoteType.Control = Me.INDGleType
        Me.INDLciNoteType.Location = New System.Drawing.Point(0, 120)
        Me.INDLciNoteType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciNoteType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciNoteType.Name = "INDLciNoteType"
        Me.INDLciNoteType.Size = New System.Drawing.Size(390, 60)
        Me.INDLciNoteType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNoteType.Text = "Tipo"
        Me.INDLciNoteType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNoteType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNoteType.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciNoteType.TextToControlDistance = 5
        '
        'INDLciNature
        '
        Me.INDLciNature.Control = Me.INDGleNature
        Me.INDLciNature.Location = New System.Drawing.Point(0, 180)
        Me.INDLciNature.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciNature.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciNature.Name = "INDLciNature"
        Me.INDLciNature.Size = New System.Drawing.Size(390, 60)
        Me.INDLciNature.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNature.Text = "Naturaleza"
        Me.INDLciNature.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNature.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNature.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciNature.TextToControlDistance = 5
        '
        'INDLcgLiquidation
        '
        Me.INDLcgLiquidation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgLiquidation.AppearanceGroup.Options.UseFont = True
        Me.INDLcgLiquidation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgLiquidation.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgLiquidation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgLiquidation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgLiquidation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgLiquidation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgLiquidation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgLiquidation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgLiquidation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgLiquidation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgLiquidation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgLiquidation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgLiquidation, False)
        Me.INDLcgLiquidation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciSubtotal, Me.INDLciIva, Me.INDLciTotal})
        Me.INDLcgLiquidation.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgLiquidation.Name = "INDLcgLiquidation"
        Me.INDLcgLiquidation.Size = New System.Drawing.Size(433, 593)
        Me.INDLcgLiquidation.Text = "Liquidación"
        '
        'INDLciSubtotal
        '
        Me.INDLciSubtotal.Control = Me.INDSpnSubtotal
        Me.INDLciSubtotal.Location = New System.Drawing.Point(0, 0)
        Me.INDLciSubtotal.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciSubtotal.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciSubtotal.Name = "INDLciSubtotal"
        Me.INDLciSubtotal.Size = New System.Drawing.Size(409, 60)
        Me.INDLciSubtotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSubtotal.Text = "SubTotal"
        Me.INDLciSubtotal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSubtotal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciSubtotal.TextSize = New System.Drawing.Size(126, 17)
        Me.INDLciSubtotal.TextToControlDistance = 5
        '
        'INDLciIva
        '
        Me.INDLciIva.Control = Me.INDSpnIVA
        Me.INDLciIva.Location = New System.Drawing.Point(0, 60)
        Me.INDLciIva.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciIva.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciIva.Name = "INDLciIva"
        Me.INDLciIva.Size = New System.Drawing.Size(409, 60)
        Me.INDLciIva.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciIva.Text = "IVA"
        Me.INDLciIva.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciIva.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciIva.TextSize = New System.Drawing.Size(126, 17)
        Me.INDLciIva.TextToControlDistance = 5
        '
        'INDLciTotal
        '
        Me.INDLciTotal.Control = Me.INDSpnTotal
        Me.INDLciTotal.Location = New System.Drawing.Point(0, 120)
        Me.INDLciTotal.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciTotal.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciTotal.Name = "INDLciTotal"
        Me.INDLciTotal.Size = New System.Drawing.Size(409, 420)
        Me.INDLciTotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTotal.Text = "Total"
        Me.INDLciTotal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTotal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTotal.TextSize = New System.Drawing.Size(126, 17)
        Me.INDLciTotal.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmElectronicSupportDocumentAdjustmentNote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1088, 680)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmElectronicSupportDocumentAdjustmentNote"
        Me.Opacity = 1.0R
        Me.Tag = "2819"
        Me.Text = "Notas de ajuste del documento soporte"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDGleNature.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnIVA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnSubtotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteSupportDocumentValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeSupportDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleSupportDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBeCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSupportDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSupportDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSupportDocumentValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNoteType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSubtotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIva, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBeCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDeDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents INDMeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDteSupportDocumentValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeSupportDocumentDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSleSupportDocument As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciSupportDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciSupportDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciSupportDocumentValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSpnTotal As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSpnIVA As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSpnSubtotal As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLcgLiquidation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciSubtotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciIva As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGleNature As GridLookUpEdit
    Friend WithEvents GridLookUpEdit2View As GridView
    Friend WithEvents INDGleType As GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As GridView
    Friend WithEvents INDLciNoteType As LayoutControlItem
    Friend WithEvents INDLciNature As LayoutControlItem
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
End Class
