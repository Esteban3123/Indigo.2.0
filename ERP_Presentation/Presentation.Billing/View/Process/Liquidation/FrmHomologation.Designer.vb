<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHomologation
    Inherits Presentation.Controls.FormBase

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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.TxtSpeciality = New DevExpress.XtraEditors.TextEdit()
        Me.TxtFunctionalUnit = New DevExpress.XtraEditors.TextEdit()
        Me.TxtProfessional = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnHomologate = New DevExpress.XtraEditors.SimpleButton()
        Me.GdcCounterpart = New DevExpress.XtraGrid.GridControl()
        Me.GdvCounterpart = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCupsCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColIPSService = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCheck = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RptChkSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GdcWithoutCounterpart = New DevExpress.XtraGrid.GridControl()
        Me.GdvWithoutCounterpart = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColMessage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TxtServiceDate = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LycgErrors = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LycgHomologations = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyciButtons = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.TxtSpeciality.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtProfessional.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GdcCounterpart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GdvCounterpart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RptChkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GdcWithoutCounterpart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GdvWithoutCounterpart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtServiceDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtServiceDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LycgErrors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LycgHomologations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyciButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(722, 611)
        Me.INDPanelControlBase.Visible = True
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(722, 94)
        Me.ToolBars.Visible = False
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(722, 94)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AllowCustomization = False
        Me.LayoutControl1.Controls.Add(Me.TxtSpeciality)
        Me.LayoutControl1.Controls.Add(Me.TxtFunctionalUnit)
        Me.LayoutControl1.Controls.Add(Me.TxtProfessional)
        Me.LayoutControl1.Controls.Add(Me.PanelControl1)
        Me.LayoutControl1.Controls.Add(Me.GdcCounterpart)
        Me.LayoutControl1.Controls.Add(Me.GdcWithoutCounterpart)
        Me.LayoutControl1.Controls.Add(Me.TxtServiceDate)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, False)
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(718, 602)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'TxtSpeciality
        '
        Me.TxtSpeciality.Location = New System.Drawing.Point(311, 291)
        Me.TxtSpeciality.Name = "TxtSpeciality"
        Me.TxtSpeciality.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtSpeciality.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TxtSpeciality.Properties.Appearance.Options.UseBackColor = True
        Me.TxtSpeciality.Properties.Appearance.Options.UseFont = True
        Me.TxtSpeciality.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtSpeciality.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtSpeciality.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TxtSpeciality.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtSpeciality.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtSpeciality.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtSpeciality.Properties.ReadOnly = True
        Me.TxtSpeciality.Size = New System.Drawing.Size(293, 28)
        Me.TxtSpeciality.StyleController = Me.LayoutControl1
        Me.TxtSpeciality.TabIndex = 4
        '
        'TxtFunctionalUnit
        '
        Me.TxtFunctionalUnit.Location = New System.Drawing.Point(311, 231)
        Me.TxtFunctionalUnit.Name = "TxtFunctionalUnit"
        Me.TxtFunctionalUnit.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtFunctionalUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TxtFunctionalUnit.Properties.Appearance.Options.UseBackColor = True
        Me.TxtFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.TxtFunctionalUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtFunctionalUnit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtFunctionalUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TxtFunctionalUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtFunctionalUnit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtFunctionalUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtFunctionalUnit.Properties.ReadOnly = True
        Me.TxtFunctionalUnit.Size = New System.Drawing.Size(293, 28)
        Me.TxtFunctionalUnit.StyleController = Me.LayoutControl1
        Me.TxtFunctionalUnit.TabIndex = 2
        '
        'TxtProfessional
        '
        Me.TxtProfessional.Location = New System.Drawing.Point(14, 291)
        Me.TxtProfessional.Name = "TxtProfessional"
        Me.TxtProfessional.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtProfessional.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TxtProfessional.Properties.Appearance.Options.UseBackColor = True
        Me.TxtProfessional.Properties.Appearance.Options.UseFont = True
        Me.TxtProfessional.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtProfessional.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtProfessional.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TxtProfessional.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtProfessional.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtProfessional.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtProfessional.Properties.ReadOnly = True
        Me.TxtProfessional.Size = New System.Drawing.Size(293, 28)
        Me.TxtProfessional.StyleController = Me.LayoutControl1
        Me.TxtProfessional.TabIndex = 3
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtnCancel)
        Me.PanelControl1.Controls.Add(Me.BtnHomologate)
        Me.PanelControl1.Location = New System.Drawing.Point(2, 554)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(714, 46)
        Me.PanelControl1.TabIndex = 6
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(110, 6)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(98, 35)
        Me.BtnCancel.TabIndex = 1
        Me.BtnCancel.Text = "Cancelar"
        '
        'BtnHomologate
        '
        Me.BtnHomologate.Location = New System.Drawing.Point(6, 6)
        Me.BtnHomologate.Name = "BtnHomologate"
        Me.BtnHomologate.Size = New System.Drawing.Size(98, 35)
        Me.BtnHomologate.TabIndex = 0
        Me.BtnHomologate.Text = "Homologar"
        '
        'GdcCounterpart
        '
        Me.IndigoGridControl1.SetAddActions(Me.GdcCounterpart, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.GdcCounterpart, Nothing)
        Me.GdcCounterpart.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.GdcCounterpart, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.GdcCounterpart, True)
        Me.IndigoGridControl1.SetHoldSize(Me.GdcCounterpart, False)
        Me.IndigoGridControl1.SetHotTrack(Me.GdcCounterpart, False)
        Me.GdcCounterpart.Location = New System.Drawing.Point(14, 351)
        Me.GdcCounterpart.MainView = Me.GdvCounterpart
        Me.GdcCounterpart.Name = "GdcCounterpart"
        Me.GdcCounterpart.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RptChkSelect})
        Me.GdcCounterpart.Size = New System.Drawing.Size(690, 187)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.GdcCounterpart, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.GdcCounterpart, New System.Drawing.Size(694, 0))
        Me.GdcCounterpart.TabIndex = 5
        Me.GdcCounterpart.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GdvCounterpart})
        '
        'GdvCounterpart
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.White
        Me.GdvCounterpart.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GdvCounterpart.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GdvCounterpart.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GdvCounterpart.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GdvCounterpart.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GdvCounterpart.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GdvCounterpart.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GdvCounterpart.Appearance.Row.Options.UseFont = True
        Me.GdvCounterpart.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GdvCounterpart.Appearance.ViewCaption.Options.UseFont = True
        Me.GdvCounterpart.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCupsCode, Me.ColIPSService, Me.ColCheck})
        Me.GdvCounterpart.GridControl = Me.GdcCounterpart
        Me.GdvCounterpart.Name = "GdvCounterpart"
        Me.GdvCounterpart.OptionsFilter.AllowFilterEditor = False
        Me.GdvCounterpart.OptionsFind.AllowFindPanel = False
        Me.GdvCounterpart.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvCounterpart.OptionsView.EnableAppearanceOddRow = True
        Me.GdvCounterpart.OptionsView.ShowAutoFilterRow = True
        Me.GdvCounterpart.OptionsView.ShowGroupPanel = False
        Me.GdvCounterpart.OptionsView.ShowIndicator = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GdvCounterpart, False)
        '
        'ColCupsCode
        '
        Me.ColCupsCode.AppearanceCell.Options.UseTextOptions = True
        Me.ColCupsCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColCupsCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColCupsCode.AppearanceHeader.Options.UseTextOptions = True
        Me.ColCupsCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColCupsCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColCupsCode.Caption = "CUPS"
        Me.ColCupsCode.FieldName = "CodeNameCupsEntity"
        Me.ColCupsCode.Name = "ColCupsCode"
        Me.ColCupsCode.OptionsColumn.AllowEdit = False
        Me.ColCupsCode.OptionsColumn.AllowFocus = False
        Me.ColCupsCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColCupsCode.OptionsColumn.AllowMove = False
        Me.ColCupsCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColCupsCode.OptionsColumn.ReadOnly = True
        Me.ColCupsCode.OptionsFilter.AllowAutoFilter = False
        Me.ColCupsCode.OptionsFilter.AllowFilter = False
        Me.ColCupsCode.Visible = True
        Me.ColCupsCode.VisibleIndex = 0
        '
        'ColIPSService
        '
        Me.ColIPSService.AppearanceCell.Options.UseTextOptions = True
        Me.ColIPSService.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColIPSService.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColIPSService.AppearanceHeader.Options.UseTextOptions = True
        Me.ColIPSService.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColIPSService.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColIPSService.Caption = "Servicio IPS"
        Me.ColIPSService.FieldName = "CodeNameIpsService"
        Me.ColIPSService.Name = "ColIPSService"
        Me.ColIPSService.OptionsColumn.AllowEdit = False
        Me.ColIPSService.OptionsColumn.AllowFocus = False
        Me.ColIPSService.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColIPSService.OptionsColumn.AllowMove = False
        Me.ColIPSService.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColIPSService.OptionsColumn.ReadOnly = True
        Me.ColIPSService.OptionsFilter.AllowAutoFilter = False
        Me.ColIPSService.OptionsFilter.AllowFilter = False
        Me.ColIPSService.Visible = True
        Me.ColIPSService.VisibleIndex = 1
        '
        'ColCheck
        '
        Me.ColCheck.AppearanceCell.Options.UseTextOptions = True
        Me.ColCheck.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColCheck.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColCheck.AppearanceHeader.Options.UseTextOptions = True
        Me.ColCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColCheck.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColCheck.Caption = "Homologar"
        Me.ColCheck.ColumnEdit = Me.RptChkSelect
        Me.ColCheck.FieldName = "Activated"
        Me.ColCheck.Name = "ColCheck"
        Me.ColCheck.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColCheck.OptionsColumn.AllowMove = False
        Me.ColCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColCheck.OptionsFilter.AllowAutoFilter = False
        Me.ColCheck.OptionsFilter.AllowFilter = False
        Me.ColCheck.Visible = True
        Me.ColCheck.VisibleIndex = 2
        '
        'RptChkSelect
        '
        Me.RptChkSelect.AutoHeight = False
        Me.RptChkSelect.Name = "RptChkSelect"
        '
        'GdcWithoutCounterpart
        '
        Me.IndigoGridControl1.SetAddActions(Me.GdcWithoutCounterpart, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.GdcWithoutCounterpart, Nothing)
        Me.GdcWithoutCounterpart.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.GdcWithoutCounterpart, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.GdcWithoutCounterpart, True)
        Me.IndigoGridControl1.SetHoldSize(Me.GdcWithoutCounterpart, False)
        Me.IndigoGridControl1.SetHotTrack(Me.GdcWithoutCounterpart, False)
        Me.GdcWithoutCounterpart.Location = New System.Drawing.Point(14, 60)
        Me.GdcWithoutCounterpart.MainView = Me.GdvWithoutCounterpart
        Me.GdcWithoutCounterpart.Name = "GdcWithoutCounterpart"
        Me.GdcWithoutCounterpart.Size = New System.Drawing.Size(690, 84)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.GdcWithoutCounterpart, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.GdcWithoutCounterpart, New System.Drawing.Size(694, 0))
        Me.GdcWithoutCounterpart.TabIndex = 0
        Me.GdcWithoutCounterpart.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GdvWithoutCounterpart})
        '
        'GdvWithoutCounterpart
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.White
        Me.GdvWithoutCounterpart.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GdvWithoutCounterpart.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GdvWithoutCounterpart.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GdvWithoutCounterpart.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GdvWithoutCounterpart.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GdvWithoutCounterpart.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GdvWithoutCounterpart.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GdvWithoutCounterpart.Appearance.Row.Options.UseFont = True
        Me.GdvWithoutCounterpart.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GdvWithoutCounterpart.Appearance.ViewCaption.Options.UseFont = True
        Me.GdvWithoutCounterpart.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColMessage})
        Me.GdvWithoutCounterpart.GridControl = Me.GdcWithoutCounterpart
        Me.GdvWithoutCounterpart.Name = "GdvWithoutCounterpart"
        Me.GdvWithoutCounterpart.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvWithoutCounterpart.OptionsView.EnableAppearanceOddRow = True
        Me.GdvWithoutCounterpart.OptionsView.ShowAutoFilterRow = True
        Me.GdvWithoutCounterpart.OptionsView.ShowGroupPanel = False
        Me.GdvWithoutCounterpart.OptionsView.ShowIndicator = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GdvWithoutCounterpart, False)
        '
        'ColMessage
        '
        Me.ColMessage.AppearanceCell.Options.UseTextOptions = True
        Me.ColMessage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColMessage.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColMessage.AppearanceHeader.Options.UseTextOptions = True
        Me.ColMessage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColMessage.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColMessage.Caption = "Listado de Advertencias"
        Me.ColMessage.FieldName = "CupsCode"
        Me.ColMessage.Name = "ColMessage"
        Me.ColMessage.OptionsColumn.AllowEdit = False
        Me.ColMessage.OptionsColumn.AllowFocus = False
        Me.ColMessage.OptionsColumn.AllowMove = False
        Me.ColMessage.OptionsColumn.AllowSize = False
        Me.ColMessage.OptionsColumn.ReadOnly = True
        Me.ColMessage.Visible = True
        Me.ColMessage.VisibleIndex = 0
        '
        'TxtServiceDate
        '
        Me.TxtServiceDate.EditValue = Nothing
        Me.TxtServiceDate.Location = New System.Drawing.Point(14, 231)
        Me.TxtServiceDate.Name = "TxtServiceDate"
        Me.TxtServiceDate.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtServiceDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TxtServiceDate.Properties.Appearance.Options.UseBackColor = True
        Me.TxtServiceDate.Properties.Appearance.Options.UseFont = True
        Me.TxtServiceDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtServiceDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtServiceDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TxtServiceDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtServiceDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtServiceDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtServiceDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TxtServiceDate.Properties.Mask.EditMask = ""
        Me.TxtServiceDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.TxtServiceDate.Properties.ReadOnly = True
        Me.TxtServiceDate.Size = New System.Drawing.Size(293, 28)
        Me.TxtServiceDate.StyleController = Me.LayoutControl1
        Me.TxtServiceDate.TabIndex = 1
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LycgErrors, Me.LycgHomologations, Me.LyciButtons})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(718, 602)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LycgErrors
        '
        Me.LycgErrors.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgErrors.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgErrors.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgErrors.AppearanceItemCaption.Options.UseFont = True
        Me.LycgErrors.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgErrors.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgErrors.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgErrors.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgErrors.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgErrors.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgErrors.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgErrors.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgErrors.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgErrors.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LycgErrors, False)
        Me.LycgErrors.CustomizationFormText = "LayoutControlGroup2"
        Me.LycgErrors.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LycgErrors.Location = New System.Drawing.Point(0, 0)
        Me.LycgErrors.Name = "LycgErrors"
        Me.LycgErrors.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 9, 0, 9)
        Me.LycgErrors.Size = New System.Drawing.Size(718, 158)
        Me.LycgErrors.Text = "Lista de Servicios Sin Homólogo"
        Me.LycgErrors.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.GdcWithoutCounterpart
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(694, 108)
        Me.LayoutControlItem1.Text = "Por favor agregue las homologaciones necesarias y vuelva a realizar la retarifica" & _
    "ción"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem1.TextToControlDistance = 0
        '
        'LycgHomologations
        '
        Me.LycgHomologations.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgHomologations.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgHomologations.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgHomologations.AppearanceItemCaption.Options.UseFont = True
        Me.LycgHomologations.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgHomologations.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgHomologations.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgHomologations.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgHomologations.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgHomologations.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgHomologations.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgHomologations.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgHomologations.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgHomologations.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LycgHomologations, False)
        Me.LycgHomologations.CustomizationFormText = "LycgHomologations"
        Me.LycgHomologations.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7})
        Me.LycgHomologations.Location = New System.Drawing.Point(0, 158)
        Me.LycgHomologations.Name = "LycgHomologations"
        Me.LycgHomologations.Size = New System.Drawing.Size(718, 394)
        Me.LycgHomologations.Text = "Servicios Homólogos"
        Me.LycgHomologations.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.GdcCounterpart
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(694, 215)
        Me.LayoutControlItem2.Text = "Seleccione los servicios a homologar"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(240, 21)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.TxtServiceDate
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(297, 60)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(297, 60)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(297, 60)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Fecha del Servicio"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(240, 21)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.TxtProfessional
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(297, 60)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(297, 60)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(297, 60)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Profesional"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(240, 21)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.TxtFunctionalUnit
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(297, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(297, 60)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(297, 60)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(397, 60)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "Unidad Funcional"
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(240, 21)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.TxtSpeciality
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(297, 60)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(297, 60)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(297, 60)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(397, 60)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "Especialidad"
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(240, 21)
        '
        'LyciButtons
        '
        Me.LyciButtons.Control = Me.PanelControl1
        Me.LyciButtons.CustomizationFormText = "LayoutControlItem3"
        Me.LyciButtons.Location = New System.Drawing.Point(0, 552)
        Me.LyciButtons.MaxSize = New System.Drawing.Size(0, 50)
        Me.LyciButtons.MinSize = New System.Drawing.Size(104, 50)
        Me.LyciButtons.Name = "LyciButtons"
        Me.LyciButtons.Size = New System.Drawing.Size(718, 50)
        Me.LyciButtons.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LyciButtons.TextSize = New System.Drawing.Size(0, 0)
        Me.LyciButtons.TextVisible = False
        Me.LyciButtons.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'FrmHomologation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(722, 729)
        Me.ControlBox = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmHomologation"
        Me.Opacity = 1.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.TxtSpeciality.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtProfessional.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.GdcCounterpart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GdvCounterpart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RptChkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GdcWithoutCounterpart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GdvWithoutCounterpart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtServiceDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtServiceDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LycgErrors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LycgHomologations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyciButtons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GdcWithoutCounterpart As DevExpress.XtraGrid.GridControl
    Friend WithEvents GdvWithoutCounterpart As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LycgErrors As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents ColMessage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GdcCounterpart As DevExpress.XtraGrid.GridControl
    Friend WithEvents GdvCounterpart As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LycgHomologations As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LyciButtons As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnHomologate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtProfessional As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TxtSpeciality As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtFunctionalUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TxtServiceDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents ColCupsCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColIPSService As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCheck As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RptChkSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
