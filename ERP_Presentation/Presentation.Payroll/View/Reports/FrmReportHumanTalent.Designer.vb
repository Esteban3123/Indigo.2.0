Imports System.Windows.Forms
Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmReportHumanTalent
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.root = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbGenerateReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.INDDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleemployee = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgCriteria = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDateStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDateEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEmployee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGenerateReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPanelControlReport = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcHumanTalent = New DevExpress.XtraGrid.GridControl()
        Me.INDgvHumanTalent = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.root, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.root.SuspendLayout()
        CType(Me.INDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleemployee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPanelControlReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlReport.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgcHumanTalent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvHumanTalent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.root)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1219, 345)
        Me.INDPanelControlBase.Dock = DockStyle.None
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1219, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1219, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.root
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 336)
        Me.CtrNavigationControlPanel1.TabIndex = 4
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'root
        '
        Me.root.Controls.Add(Me.INDSbGenerateReport)
        Me.root.Controls.Add(Me.INDDateEnd)
        Me.root.Controls.Add(Me.INDDateStart)
        Me.root.Controls.Add(Me.INDSleemployee)
        Me.root.Dock = System.Windows.Forms.DockStyle.Fill
        Me.root.Location = New System.Drawing.Point(202, 7)
        Me.root.Name = "root"
        Me.root.Root = Me.LayoutControlGroup2
        Me.root.Size = New System.Drawing.Size(1015, 336)
        Me.root.TabIndex = 5
        Me.root.Text = "LayoutControl2"
        '
        'INDSbGenerateReport
        '
        Me.INDSbGenerateReport.Location = New System.Drawing.Point(24, 233)
        Me.INDSbGenerateReport.MaximumSize = New System.Drawing.Size(0, 30)
        Me.INDSbGenerateReport.MinimumSize = New System.Drawing.Size(0, 30)
        Me.INDSbGenerateReport.Name = "INDSbGenerateReport"
        Me.INDSbGenerateReport.Size = New System.Drawing.Size(386, 30)
        Me.INDSbGenerateReport.StyleController = Me.root
        Me.INDSbGenerateReport.TabIndex = 6
        Me.INDSbGenerateReport.Text = "Generar reporte"
        '
        'INDDateEnd
        '
        Me.INDDateEnd.EditValue = Nothing
        Me.INDDateEnd.EnterMoveNextControl = True
        Me.INDDateEnd.Location = New System.Drawing.Point(24, 133)
        Me.INDDateEnd.Name = "INDDateEnd"
        Me.INDDateEnd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDateEnd.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEnd.Properties.Appearance.Options.UseBackColor = True
        Me.INDDateEnd.Properties.Appearance.Options.UseFont = True
        Me.INDDateEnd.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDateEnd.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDateEnd.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateEnd.Size = New System.Drawing.Size(386, 28)
        Me.INDDateEnd.StyleController = Me.root
        Me.INDDateEnd.TabIndex = 2
        '
        'INDDateStart
        '
        Me.INDDateStart.EditValue = Nothing
        Me.INDDateStart.EnterMoveNextControl = True
        Me.INDDateStart.Location = New System.Drawing.Point(24, 73)
        Me.INDDateStart.Name = "INDDateStart"
        Me.INDDateStart.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDateStart.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateStart.Properties.Appearance.Options.UseBackColor = True
        Me.INDDateStart.Properties.Appearance.Options.UseFont = True
        Me.INDDateStart.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDateStart.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDateStart.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateStart.Size = New System.Drawing.Size(386, 28)
        Me.INDDateStart.StyleController = Me.root
        Me.INDDateStart.TabIndex = 1
        '
        'INDSleemployee
        '
        Me.INDSleemployee.EditValue = ""
        Me.INDSleemployee.Location = New System.Drawing.Point(24, 193)
        Me.INDSleemployee.MaximumSize = New System.Drawing.Size(385, 0)
        Me.INDSleemployee.Name = "INDSleemployee"
        Me.INDSleemployee.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleemployee.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleemployee.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleemployee.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleemployee.Properties.Appearance.Options.UseFont = True
        Me.INDSleemployee.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleemployee.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleemployee.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleemployee.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleemployee.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleemployee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleemployee.Properties.DisplayMember = "ThirdPartyNitName"
        Me.INDSleemployee.Properties.NullText = ""
        Me.INDSleemployee.Properties.PopupSizeable = False
        Me.INDSleemployee.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleemployee.Properties.ShowFooter = False
        Me.INDSleemployee.Properties.ValueMember = "Id"
        Me.INDSleemployee.Size = New System.Drawing.Size(385, 28)
        Me.INDSleemployee.StyleController = Me.root
        Me.INDSleemployee.TabIndex = 5
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColNit, Me.INDColName})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'INDColNit
        '
        Me.INDColNit.Caption = "Nit"
        Me.INDColNit.FieldName = "Nit"
        Me.INDColNit.Name = "INDColNit"
        Me.INDColNit.Visible = True
        Me.INDColNit.VisibleIndex = 0
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "Name"
        Me.INDColName.Name = "INDColName"
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 1
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgCriteria})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1015, 336)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDLcgCriteria
        '
        Me.INDLcgCriteria.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgCriteria.CustomizationFormText = "Criterios"
        Me.INDLcgCriteria.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDateStart, Me.INDLciDateEnd, Me.INDLciEmployee, Me.INDLciGenerateReport})
        Me.INDLcgCriteria.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgCriteria.Name = "INDLcgCriteria"
        Me.INDLcgCriteria.Size = New System.Drawing.Size(995, 316)
        Me.INDLcgCriteria.Text = "Criterios"
        '
        'INDLciDateStart
        '
        Me.INDLciDateStart.Control = Me.INDDateStart
        Me.INDLciDateStart.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciDateStart.CustomizationFormText = "Fecha Inicial:"
        Me.INDLciDateStart.Location = New System.Drawing.Point(0, 0)
        Me.INDLciDateStart.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDateStart.MinSize = New System.Drawing.Size(390, 50)
        Me.INDLciDateStart.Name = "INDLciDateStart"
        Me.INDLciDateStart.Size = New System.Drawing.Size(971, 60)
        Me.INDLciDateStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateStart.Text = "Fecha Inicial:"
        Me.INDLciDateStart.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateStart.TextSize = New System.Drawing.Size(84, 17)
        '
        'INDLciDateEnd
        '
        Me.INDLciDateEnd.Control = Me.INDDateEnd
        Me.INDLciDateEnd.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciDateEnd.CustomizationFormText = "Fecha Final:"
        Me.INDLciDateEnd.Location = New System.Drawing.Point(0, 60)
        Me.INDLciDateEnd.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDateEnd.MinSize = New System.Drawing.Size(390, 50)
        Me.INDLciDateEnd.Name = "INDLciDateEnd"
        Me.INDLciDateEnd.Size = New System.Drawing.Size(971, 60)
        Me.INDLciDateEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateEnd.Text = "Fecha Final:"
        Me.INDLciDateEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateEnd.TextSize = New System.Drawing.Size(84, 17)
        '
        'INDLciEmployee
        '
        Me.INDLciEmployee.Control = Me.INDSleemployee
        Me.INDLciEmployee.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciEmployee.CustomizationFormText = "Empleado:"
        Me.INDLciEmployee.Location = New System.Drawing.Point(0, 120)
        Me.INDLciEmployee.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciEmployee.MinSize = New System.Drawing.Size(390, 50)
        Me.INDLciEmployee.Name = "INDLciEmployee"
        Me.INDLciEmployee.Size = New System.Drawing.Size(971, 60)
        Me.INDLciEmployee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEmployee.Text = "Empleado:"
        Me.INDLciEmployee.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEmployee.TextSize = New System.Drawing.Size(84, 17)
        '
        'INDLciGenerateReport
        '
        Me.INDLciGenerateReport.Control = Me.INDSbGenerateReport
        Me.INDLciGenerateReport.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciGenerateReport.CustomizationFormText = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Location = New System.Drawing.Point(0, 180)
        Me.INDLciGenerateReport.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciGenerateReport.MinSize = New System.Drawing.Size(150, 26)
        Me.INDLciGenerateReport.Name = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Size = New System.Drawing.Size(971, 83)
        Me.INDLciGenerateReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGenerateReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenerateReport.TextVisible = False
        '
        'INDPanelControlReport
        '
        Me.INDPanelControlReport.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlReport.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDPanelControlReport.Location = New System.Drawing.Point(0, 480)
        Me.INDPanelControlReport.Name = "INDPanelControlReport"
        Me.INDPanelControlReport.Size = New System.Drawing.Size(1219, 278)
        Me.INDPanelControlReport.TabIndex = 1
        Me.INDPanelControlReport.Visible = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDgcHumanTalent)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1215, 274)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDgcHumanTalent
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcHumanTalent, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcHumanTalent, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcHumanTalent, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcHumanTalent, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcHumanTalent, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcHumanTalent, False)
        Me.INDgcHumanTalent.Location = New System.Drawing.Point(12, 12)
        Me.INDgcHumanTalent.MainView = Me.INDgvHumanTalent
        Me.INDgcHumanTalent.Name = "INDgcHumanTalent"
        Me.INDgcHumanTalent.Size = New System.Drawing.Size(1191, 250)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcHumanTalent, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcHumanTalent.TabIndex = 6
        Me.INDgcHumanTalent.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvHumanTalent})
        '
        'INDgvHumanTalent
        '
        Me.INDgvHumanTalent.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvHumanTalent.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvHumanTalent.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvHumanTalent.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvHumanTalent.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvHumanTalent.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvHumanTalent.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvHumanTalent.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvHumanTalent.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvHumanTalent.Appearance.Row.Options.UseFont = True
        Me.INDgvHumanTalent.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvHumanTalent.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvHumanTalent.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18, Me.GridColumn19, Me.GridColumn20})
        Me.INDgvHumanTalent.GridControl = Me.INDgcHumanTalent
        Me.INDgvHumanTalent.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "DocumentType", Nothing, ""), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BasicSalary", Nothing, "(Salario Basico: SUM={0:0.##})")})
        Me.INDgvHumanTalent.Name = "INDgvHumanTalent"
        Me.INDgvHumanTalent.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvHumanTalent.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvHumanTalent.OptionsView.ShowAutoFilterRow = True
        Me.INDgvHumanTalent.OptionsView.ShowFooter = True
        Me.INDgvHumanTalent.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo Documento"
        Me.GridColumn1.FieldName = "DocumentType"
        Me.GridColumn1.MaxWidth = 350
        Me.GridColumn1.MinWidth = 80
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 80
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Cedula Fisica"
        Me.GridColumn2.FieldName = "Nit"
        Me.GridColumn2.MaxWidth = 350
        Me.GridColumn2.MinWidth = 80
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 80
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Empleado"
        Me.GridColumn3.FieldName = "EmployeeName"
        Me.GridColumn3.MaxWidth = 350
        Me.GridColumn3.MinWidth = 80
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 80
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Estado"
        Me.GridColumn4.FieldName = "ContractStatus"
        Me.GridColumn4.MaxWidth = 350
        Me.GridColumn4.MinWidth = 80
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 80
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Codigo Unidad Funcional"
        Me.GridColumn5.FieldName = "FunctionalUnitCode"
        Me.GridColumn5.MaxWidth = 350
        Me.GridColumn5.MinWidth = 80
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 80
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Unidad Funcional"
        Me.GridColumn6.FieldName = "FunctionalUnitName"
        Me.GridColumn6.MaxWidth = 350
        Me.GridColumn6.MinWidth = 80
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 80
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Codigo Cargo"
        Me.GridColumn7.FieldName = "PositionCode"
        Me.GridColumn7.MaxWidth = 350
        Me.GridColumn7.MinWidth = 80
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        Me.GridColumn7.Width = 80
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Cargo"
        Me.GridColumn8.FieldName = "PositionName"
        Me.GridColumn8.MaxWidth = 350
        Me.GridColumn8.MinWidth = 80
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 7
        Me.GridColumn8.Width = 80
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Salario Basico"
        Me.GridColumn9.DisplayFormat.FormatString = "C2"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.GridColumn9.FieldName = "BasicSalary"
        Me.GridColumn9.MaxWidth = 350
        Me.GridColumn9.MinWidth = 80
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 8
        Me.GridColumn9.Width = 80
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Fecha Ingreso"
        Me.GridColumn10.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.GridColumn10.FieldName = "ContractStartDate"
        Me.GridColumn10.MaxWidth = 350
        Me.GridColumn10.MinWidth = 80
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 9
        Me.GridColumn10.Width = 80
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Fecha Salida"
        Me.GridColumn11.FieldName = "ContractEndDate"
        Me.GridColumn11.MaxWidth = 350
        Me.GridColumn11.MinWidth = 80
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 10
        Me.GridColumn11.Width = 80
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Fecha Nacimiento"
        Me.GridColumn12.FieldName = "BirthDate"
        Me.GridColumn12.MaxWidth = 350
        Me.GridColumn12.MinWidth = 80
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 11
        Me.GridColumn12.Width = 80
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Edad"
        Me.GridColumn13.FieldName = "Age"
        Me.GridColumn13.MaxWidth = 350
        Me.GridColumn13.MinWidth = 80
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 12
        Me.GridColumn13.Width = 80
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Genero"
        Me.GridColumn14.FieldName = "Gender"
        Me.GridColumn14.MaxWidth = 350
        Me.GridColumn14.MinWidth = 80
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 13
        Me.GridColumn14.Width = 80
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Nacionalidad"
        Me.GridColumn15.FieldName = "NationalityName"
        Me.GridColumn15.MaxWidth = 350
        Me.GridColumn15.MinWidth = 80
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 14
        Me.GridColumn15.Width = 80
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Estado Civil"
        Me.GridColumn16.FieldName = "MaritalStatus"
        Me.GridColumn16.MaxWidth = 350
        Me.GridColumn16.MinWidth = 80
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 15
        Me.GridColumn16.Width = 80
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Forma de Pago"
        Me.GridColumn17.FieldName = "PaymentMethod"
        Me.GridColumn17.MaxWidth = 350
        Me.GridColumn17.MinWidth = 80
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 16
        Me.GridColumn17.Width = 80
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Banco"
        Me.GridColumn18.FieldName = "BankName"
        Me.GridColumn18.MaxWidth = 350
        Me.GridColumn18.MinWidth = 80
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 17
        Me.GridColumn18.Width = 80
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Numero de Cuenta"
        Me.GridColumn19.FieldName = "BankAccountNumber"
        Me.GridColumn19.MaxWidth = 350
        Me.GridColumn19.MinWidth = 80
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 18
        Me.GridColumn19.Width = 80
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Correo"
        Me.GridColumn20.FieldName = "EmailAddress"
        Me.GridColumn20.MaxWidth = 350
        Me.GridColumn20.MinWidth = 80
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 19
        Me.GridColumn20.Width = 80
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1215, 274)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcHumanTalent
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1195, 254)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FrmReportHumanTalent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1219, 758)
        Me.Controls.Add(Me.INDPanelControlReport)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmReportHumanTalent"
        Me.Opacity = 1.0R
        Me.Tag = "89037"
        Me.Text = "Reporte Talento Humano"
        Me.Controls.SetChildIndex(Me.INDPanelControlReport, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.root, System.ComponentModel.ISupportInitialize).EndInit()
        Me.root.ResumeLayout(False)
        CType(Me.INDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleemployee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPanelControlReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlReport.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgcHumanTalent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvHumanTalent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents root As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSbGenerateReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDSleemployee As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgCriteria As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDateStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDateEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciEmployee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciGenerateReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPanelControlReport As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcHumanTalent As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvHumanTalent As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbExportExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
End Class
