Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettingsAuthorization
    Inherits FormBase

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
        Me.INDCnNavigation = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGleAutomaticAllocation = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGvAutomaticAllocation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvAutomaticAllocation_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSeDaysToAssign = New DevExpress.XtraEditors.SpinEdit()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAutomaticAllocation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDaysToAssign = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn264 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn265 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn262 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn263 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn258 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn259 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn256 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn257 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn254 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn255 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn252 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn253 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn248 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn249 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn246 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn247 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn244 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn245 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn234 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn235 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn224 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn225 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn221 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn222 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn218 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn219 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn216 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn217 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn214 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn215 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn210 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn211 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn208 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn209 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn206 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn207 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn198 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn199 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn196 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn197 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn202 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn203 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn200 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn201 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn194 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn195 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn192 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn193 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn190 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn191 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn187 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn188 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn184 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn185 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn182 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn183 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn176 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn177 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn174 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn175 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn172 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn173 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn165 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn166 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn163 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn164 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn160 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn161 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn158 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn159 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn156 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn157 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn153 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn154 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn151 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn152 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn128 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn129 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn122 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn123 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn120 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn121 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn118 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn119 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn116 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn117 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn114 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn115 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn108 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn109 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn106 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn107 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn104 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn105 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn102 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn103 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn100 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn101 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn96 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn97 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn94 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn95 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn92 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn93 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn90 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn91 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn84 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn85 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn82 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn83 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn80 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn81 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn73 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn79 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn71 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn72 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.GridColumn69 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn70 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn67 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn68 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn63 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn64 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn61 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn62 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn57 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn58 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.GridColumn141 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCnNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDGleAutomaticAllocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAutomaticAllocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeDaysToAssign.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAutomaticAllocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDaysToAssign, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCnNavigation)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1014, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1014, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1014, 98)
        '
        'INDCnNavigation
        '
        Me.INDCnNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCnNavigation.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnNavigation.LayoutControl = Me.INDLcBase
        Me.INDCnNavigation.Location = New System.Drawing.Point(2, 7)
        Me.INDCnNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCnNavigation.Name = "INDCnNavigation"
        Me.INDCnNavigation.Size = New System.Drawing.Size(200, 598)
        Me.INDCnNavigation.TabIndex = 0
        Me.INDCnNavigation.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDGleAutomaticAllocation)
        Me.INDLcBase.Controls.Add(Me.INDSeDaysToAssign)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(810, 598)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDGleAutomaticAllocation
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleAutomaticAllocation, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleAutomaticAllocation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleAutomaticAllocation, True)
        Me.INDGleAutomaticAllocation.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleAutomaticAllocation, True)
        Me.INDGleAutomaticAllocation.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleAutomaticAllocation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleAutomaticAllocation.Name = "INDGleAutomaticAllocation"
        Me.INDGleAutomaticAllocation.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleAutomaticAllocation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleAutomaticAllocation.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleAutomaticAllocation.Properties.Appearance.Options.UseFont = True
        Me.INDGleAutomaticAllocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleAutomaticAllocation.Properties.DisplayMember = "Item2"
        Me.INDGleAutomaticAllocation.Properties.ImmediatePopup = True
        Me.INDGleAutomaticAllocation.Properties.NullText = ""
        Me.INDGleAutomaticAllocation.Properties.PopupView = Me.INDGvAutomaticAllocation
        Me.INDGleAutomaticAllocation.Properties.ValueMember = "Item1"
        Me.INDGleAutomaticAllocation.Size = New System.Drawing.Size(386, 28)
        Me.INDGleAutomaticAllocation.StyleController = Me.INDLcBase
        Me.INDGleAutomaticAllocation.TabIndex = 46
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleAutomaticAllocation, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleAutomaticAllocation, 0)
        Me.INDGleAutomaticAllocation.ToolTip = "Este Campo es Necesario"
        '
        'INDGvAutomaticAllocation
        '
        Me.INDGvAutomaticAllocation.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvAutomaticAllocation.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvAutomaticAllocation.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvAutomaticAllocation.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvAutomaticAllocation.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAutomaticAllocation.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvAutomaticAllocation.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAutomaticAllocation.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvAutomaticAllocation.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAutomaticAllocation.Appearance.Row.Options.UseFont = True
        Me.INDGvAutomaticAllocation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvAutomaticAllocation_Description})
        Me.INDGvAutomaticAllocation.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvAutomaticAllocation.Name = "INDGvAutomaticAllocation"
        Me.INDGvAutomaticAllocation.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvAutomaticAllocation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvAutomaticAllocation.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvAutomaticAllocation.OptionsView.ShowAutoFilterRow = True
        Me.INDGvAutomaticAllocation.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvAutomaticAllocation, False)
        '
        'INDGvAutomaticAllocation_Description
        '
        Me.INDGvAutomaticAllocation_Description.Caption = "Descripción"
        Me.INDGvAutomaticAllocation_Description.FieldName = "Item2"
        Me.INDGvAutomaticAllocation_Description.Name = "INDGvAutomaticAllocation_Description"
        Me.INDGvAutomaticAllocation_Description.Visible = True
        Me.INDGvAutomaticAllocation_Description.VisibleIndex = 0
        '
        'INDSeDaysToAssign
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeDaysToAssign, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeDaysToAssign, True)
        Me.INDSeDaysToAssign.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDSeDaysToAssign.Location = New System.Drawing.Point(24, 135)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeDaysToAssign, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeDaysToAssign.Name = "INDSeDaysToAssign"
        Me.INDSeDaysToAssign.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeDaysToAssign.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeDaysToAssign.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeDaysToAssign.Properties.Appearance.Options.UseFont = True
        Me.INDSeDaysToAssign.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSeDaysToAssign.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDSeDaysToAssign.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeDaysToAssign.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSeDaysToAssign.Properties.Mask.EditMask = "n0"
        Me.INDSeDaysToAssign.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeDaysToAssign.Properties.MaxValue = New Decimal(New Integer() {365, 0, 0, 0})
        Me.INDSeDaysToAssign.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDSeDaysToAssign.Size = New System.Drawing.Size(386, 28)
        Me.INDSeDaysToAssign.StyleController = Me.INDLcBase
        Me.INDSeDaysToAssign.TabIndex = 47
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeDaysToAssign, 0)
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.CustomizationFormText = "INDLcgBase"
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralInformation})
        Me.INDLcgBase.Name = "Root"
        Me.INDLcgBase.Size = New System.Drawing.Size(810, 598)
        Me.INDLcgBase.TextVisible = False
        '
        'INDlygGeneralInformation
        '
        Me.INDlygGeneralInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneralInformation, False)
        Me.INDlygGeneralInformation.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAutomaticAllocation, Me.INDLciDaysToAssign})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(790, 578)
        Me.INDlygGeneralInformation.Text = "Información General"
        '
        'INDLciAutomaticAllocation
        '
        Me.INDLciAutomaticAllocation.Control = Me.INDGleAutomaticAllocation
        Me.INDLciAutomaticAllocation.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAutomaticAllocation.MaxSize = New System.Drawing.Size(390, 56)
        Me.INDLciAutomaticAllocation.MinSize = New System.Drawing.Size(390, 56)
        Me.INDLciAutomaticAllocation.Name = "INDLciAutomaticAllocation"
        Me.INDLciAutomaticAllocation.Size = New System.Drawing.Size(766, 56)
        Me.INDLciAutomaticAllocation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAutomaticAllocation.Text = "Asignación Automática"
        Me.INDLciAutomaticAllocation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAutomaticAllocation.TextSize = New System.Drawing.Size(146, 17)
        '
        'INDLciDaysToAssign
        '
        Me.INDLciDaysToAssign.Control = Me.INDSeDaysToAssign
        Me.INDLciDaysToAssign.Location = New System.Drawing.Point(0, 56)
        Me.INDLciDaysToAssign.MaxSize = New System.Drawing.Size(390, 56)
        Me.INDLciDaysToAssign.MinSize = New System.Drawing.Size(390, 56)
        Me.INDLciDaysToAssign.Name = "INDLciDaysToAssign"
        Me.INDLciDaysToAssign.Size = New System.Drawing.Size(766, 463)
        Me.INDLciDaysToAssign.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDaysToAssign.Text = "Dias a Asignar"
        Me.INDLciDaysToAssign.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDaysToAssign.TextSize = New System.Drawing.Size(146, 17)
        '
        'GridColumn264
        '
        Me.GridColumn264.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn264.Caption = "Selección"
        Me.GridColumn264.FieldName = "Item2"
        Me.GridColumn264.Name = "GridColumn264"
        '
        'GridColumn265
        '
        Me.GridColumn265.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn265.Caption = "Selección"
        Me.GridColumn265.FieldName = "Item2"
        Me.GridColumn265.Name = "GridColumn265"
        '
        'GridColumn262
        '
        Me.GridColumn262.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn262.Caption = "Selección"
        Me.GridColumn262.FieldName = "Item2"
        Me.GridColumn262.Name = "GridColumn262"
        '
        'GridColumn263
        '
        Me.GridColumn263.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn263.Caption = "Selección"
        Me.GridColumn263.FieldName = "Item2"
        Me.GridColumn263.Name = "GridColumn263"
        '
        'GridColumn258
        '
        Me.GridColumn258.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn258.Caption = "Selección"
        Me.GridColumn258.FieldName = "Item2"
        Me.GridColumn258.Name = "GridColumn258"
        '
        'GridColumn259
        '
        Me.GridColumn259.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn259.Caption = "Selección"
        Me.GridColumn259.FieldName = "Item2"
        Me.GridColumn259.Name = "GridColumn259"
        '
        'GridColumn256
        '
        Me.GridColumn256.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn256.Caption = "Selección"
        Me.GridColumn256.FieldName = "Item2"
        Me.GridColumn256.Name = "GridColumn256"
        '
        'GridColumn257
        '
        Me.GridColumn257.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn257.Caption = "Selección"
        Me.GridColumn257.FieldName = "Item2"
        Me.GridColumn257.Name = "GridColumn257"
        '
        'GridColumn254
        '
        Me.GridColumn254.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn254.Caption = "Selección"
        Me.GridColumn254.FieldName = "Item2"
        Me.GridColumn254.Name = "GridColumn254"
        '
        'GridColumn255
        '
        Me.GridColumn255.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn255.Caption = "Selección"
        Me.GridColumn255.FieldName = "Item2"
        Me.GridColumn255.Name = "GridColumn255"
        '
        'GridColumn252
        '
        Me.GridColumn252.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn252.Caption = "Selección"
        Me.GridColumn252.FieldName = "Item2"
        Me.GridColumn252.Name = "GridColumn252"
        '
        'GridColumn253
        '
        Me.GridColumn253.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn253.Caption = "Selección"
        Me.GridColumn253.FieldName = "Item2"
        Me.GridColumn253.Name = "GridColumn253"
        '
        'GridColumn248
        '
        Me.GridColumn248.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn248.Caption = "Selección"
        Me.GridColumn248.FieldName = "Item2"
        Me.GridColumn248.Name = "GridColumn248"
        '
        'GridColumn249
        '
        Me.GridColumn249.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn249.Caption = "Selección"
        Me.GridColumn249.FieldName = "Item2"
        Me.GridColumn249.Name = "GridColumn249"
        '
        'GridColumn246
        '
        Me.GridColumn246.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn246.Caption = "Selección"
        Me.GridColumn246.FieldName = "Item2"
        Me.GridColumn246.Name = "GridColumn246"
        '
        'GridColumn247
        '
        Me.GridColumn247.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn247.Caption = "Selección"
        Me.GridColumn247.FieldName = "Item2"
        Me.GridColumn247.Name = "GridColumn247"
        '
        'GridColumn244
        '
        Me.GridColumn244.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn244.Caption = "Selección"
        Me.GridColumn244.FieldName = "Item2"
        Me.GridColumn244.Name = "GridColumn244"
        '
        'GridColumn245
        '
        Me.GridColumn245.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn245.Caption = "Selección"
        Me.GridColumn245.FieldName = "Item2"
        Me.GridColumn245.Name = "GridColumn245"
        '
        'GridColumn234
        '
        Me.GridColumn234.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn234.Caption = "Selección"
        Me.GridColumn234.FieldName = "Item2"
        Me.GridColumn234.Name = "GridColumn234"
        '
        'GridColumn235
        '
        Me.GridColumn235.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn235.Caption = "Selección"
        Me.GridColumn235.FieldName = "Item2"
        Me.GridColumn235.Name = "GridColumn235"
        '
        'GridColumn224
        '
        Me.GridColumn224.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn224.Caption = "Selección"
        Me.GridColumn224.FieldName = "Item2"
        Me.GridColumn224.Name = "GridColumn224"
        '
        'GridColumn225
        '
        Me.GridColumn225.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn225.Caption = "Selección"
        Me.GridColumn225.FieldName = "Item2"
        Me.GridColumn225.Name = "GridColumn225"
        '
        'GridColumn221
        '
        Me.GridColumn221.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn221.Caption = "Selección"
        Me.GridColumn221.FieldName = "Item2"
        Me.GridColumn221.Name = "GridColumn221"
        '
        'GridColumn222
        '
        Me.GridColumn222.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn222.Caption = "Selección"
        Me.GridColumn222.FieldName = "Item2"
        Me.GridColumn222.Name = "GridColumn222"
        '
        'GridColumn218
        '
        Me.GridColumn218.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn218.Caption = "Selección"
        Me.GridColumn218.FieldName = "Item2"
        Me.GridColumn218.Name = "GridColumn218"
        '
        'GridColumn219
        '
        Me.GridColumn219.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn219.Caption = "Selección"
        Me.GridColumn219.FieldName = "Item2"
        Me.GridColumn219.Name = "GridColumn219"
        '
        'GridColumn216
        '
        Me.GridColumn216.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn216.Caption = "Selección"
        Me.GridColumn216.FieldName = "Item2"
        Me.GridColumn216.Name = "GridColumn216"
        '
        'GridColumn217
        '
        Me.GridColumn217.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn217.Caption = "Selección"
        Me.GridColumn217.FieldName = "Item2"
        Me.GridColumn217.Name = "GridColumn217"
        '
        'GridColumn214
        '
        Me.GridColumn214.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn214.Caption = "Selección"
        Me.GridColumn214.FieldName = "Item2"
        Me.GridColumn214.Name = "GridColumn214"
        '
        'GridColumn215
        '
        Me.GridColumn215.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn215.Caption = "Selección"
        Me.GridColumn215.FieldName = "Item2"
        Me.GridColumn215.Name = "GridColumn215"
        '
        'GridColumn210
        '
        Me.GridColumn210.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn210.Caption = "Selección"
        Me.GridColumn210.FieldName = "Item2"
        Me.GridColumn210.Name = "GridColumn210"
        '
        'GridColumn211
        '
        Me.GridColumn211.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn211.Caption = "Selección"
        Me.GridColumn211.FieldName = "Item2"
        Me.GridColumn211.Name = "GridColumn211"
        '
        'GridColumn208
        '
        Me.GridColumn208.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn208.Caption = "Selección"
        Me.GridColumn208.FieldName = "Item2"
        Me.GridColumn208.Name = "GridColumn208"
        '
        'GridColumn209
        '
        Me.GridColumn209.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn209.Caption = "Selección"
        Me.GridColumn209.FieldName = "Item2"
        Me.GridColumn209.Name = "GridColumn209"
        '
        'GridColumn206
        '
        Me.GridColumn206.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn206.Caption = "Selección"
        Me.GridColumn206.FieldName = "Item2"
        Me.GridColumn206.Name = "GridColumn206"
        '
        'GridColumn207
        '
        Me.GridColumn207.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn207.Caption = "Selección"
        Me.GridColumn207.FieldName = "Item2"
        Me.GridColumn207.Name = "GridColumn207"
        '
        'GridColumn198
        '
        Me.GridColumn198.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn198.Caption = "Selección"
        Me.GridColumn198.FieldName = "Item2"
        Me.GridColumn198.Name = "GridColumn198"
        '
        'GridColumn199
        '
        Me.GridColumn199.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn199.Caption = "Selección"
        Me.GridColumn199.FieldName = "Item2"
        Me.GridColumn199.Name = "GridColumn199"
        '
        'GridColumn196
        '
        Me.GridColumn196.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn196.Caption = "Selección"
        Me.GridColumn196.FieldName = "Item2"
        Me.GridColumn196.Name = "GridColumn196"
        '
        'GridColumn197
        '
        Me.GridColumn197.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn197.Caption = "Selección"
        Me.GridColumn197.FieldName = "Item2"
        Me.GridColumn197.Name = "GridColumn197"
        '
        'GridColumn202
        '
        Me.GridColumn202.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn202.Caption = "Selección"
        Me.GridColumn202.FieldName = "Item2"
        Me.GridColumn202.Name = "GridColumn202"
        '
        'GridColumn203
        '
        Me.GridColumn203.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn203.Caption = "Selección"
        Me.GridColumn203.FieldName = "Item2"
        Me.GridColumn203.Name = "GridColumn203"
        '
        'GridColumn200
        '
        Me.GridColumn200.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn200.Caption = "Selección"
        Me.GridColumn200.FieldName = "Item2"
        Me.GridColumn200.Name = "GridColumn200"
        '
        'GridColumn201
        '
        Me.GridColumn201.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn201.Caption = "Selección"
        Me.GridColumn201.FieldName = "Item2"
        Me.GridColumn201.Name = "GridColumn201"
        '
        'GridColumn194
        '
        Me.GridColumn194.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn194.Caption = "Selección"
        Me.GridColumn194.FieldName = "Item2"
        Me.GridColumn194.Name = "GridColumn194"
        '
        'GridColumn195
        '
        Me.GridColumn195.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn195.Caption = "Selección"
        Me.GridColumn195.FieldName = "Item2"
        Me.GridColumn195.Name = "GridColumn195"
        '
        'GridColumn192
        '
        Me.GridColumn192.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn192.Caption = "Selección"
        Me.GridColumn192.FieldName = "Item2"
        Me.GridColumn192.Name = "GridColumn192"
        '
        'GridColumn193
        '
        Me.GridColumn193.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn193.Caption = "Selección"
        Me.GridColumn193.FieldName = "Item2"
        Me.GridColumn193.Name = "GridColumn193"
        '
        'GridColumn190
        '
        Me.GridColumn190.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn190.Caption = "Selección"
        Me.GridColumn190.FieldName = "Item2"
        Me.GridColumn190.Name = "GridColumn190"
        '
        'GridColumn191
        '
        Me.GridColumn191.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn191.Caption = "Selección"
        Me.GridColumn191.FieldName = "Item2"
        Me.GridColumn191.Name = "GridColumn191"
        '
        'GridColumn187
        '
        Me.GridColumn187.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn187.Caption = "Selección"
        Me.GridColumn187.FieldName = "Item2"
        Me.GridColumn187.Name = "GridColumn187"
        '
        'GridColumn188
        '
        Me.GridColumn188.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn188.Caption = "Selección"
        Me.GridColumn188.FieldName = "Item2"
        Me.GridColumn188.Name = "GridColumn188"
        '
        'GridColumn184
        '
        Me.GridColumn184.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn184.Caption = "Selección"
        Me.GridColumn184.FieldName = "Item2"
        Me.GridColumn184.Name = "GridColumn184"
        '
        'GridColumn185
        '
        Me.GridColumn185.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn185.Caption = "Selección"
        Me.GridColumn185.FieldName = "Item2"
        Me.GridColumn185.Name = "GridColumn185"
        '
        'GridColumn182
        '
        Me.GridColumn182.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn182.Caption = "Selección"
        Me.GridColumn182.FieldName = "Item2"
        Me.GridColumn182.Name = "GridColumn182"
        '
        'GridColumn183
        '
        Me.GridColumn183.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn183.Caption = "Selección"
        Me.GridColumn183.FieldName = "Item2"
        Me.GridColumn183.Name = "GridColumn183"
        '
        'GridColumn176
        '
        Me.GridColumn176.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn176.Caption = "Selección"
        Me.GridColumn176.FieldName = "Item2"
        Me.GridColumn176.Name = "GridColumn176"
        '
        'GridColumn177
        '
        Me.GridColumn177.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn177.Caption = "Selección"
        Me.GridColumn177.FieldName = "Item2"
        Me.GridColumn177.Name = "GridColumn177"
        '
        'GridColumn174
        '
        Me.GridColumn174.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn174.Caption = "Selección"
        Me.GridColumn174.FieldName = "Item2"
        Me.GridColumn174.Name = "GridColumn174"
        '
        'GridColumn175
        '
        Me.GridColumn175.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn175.Caption = "Selección"
        Me.GridColumn175.FieldName = "Item2"
        Me.GridColumn175.Name = "GridColumn175"
        '
        'GridColumn172
        '
        Me.GridColumn172.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn172.Caption = "Selección"
        Me.GridColumn172.FieldName = "Item2"
        Me.GridColumn172.Name = "GridColumn172"
        '
        'GridColumn173
        '
        Me.GridColumn173.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn173.Caption = "Selección"
        Me.GridColumn173.FieldName = "Item2"
        Me.GridColumn173.Name = "GridColumn173"
        '
        'GridColumn165
        '
        Me.GridColumn165.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn165.Caption = "Selección"
        Me.GridColumn165.FieldName = "Item2"
        Me.GridColumn165.Name = "GridColumn165"
        '
        'GridColumn166
        '
        Me.GridColumn166.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn166.Caption = "Selección"
        Me.GridColumn166.FieldName = "Item2"
        Me.GridColumn166.Name = "GridColumn166"
        '
        'GridColumn163
        '
        Me.GridColumn163.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn163.Caption = "Selección"
        Me.GridColumn163.FieldName = "Item2"
        Me.GridColumn163.Name = "GridColumn163"
        '
        'GridColumn164
        '
        Me.GridColumn164.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn164.Caption = "Selección"
        Me.GridColumn164.FieldName = "Item2"
        Me.GridColumn164.Name = "GridColumn164"
        '
        'GridColumn160
        '
        Me.GridColumn160.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn160.Caption = "Selección"
        Me.GridColumn160.FieldName = "Item2"
        Me.GridColumn160.Name = "GridColumn160"
        '
        'GridColumn161
        '
        Me.GridColumn161.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn161.Caption = "Selección"
        Me.GridColumn161.FieldName = "Item2"
        Me.GridColumn161.Name = "GridColumn161"
        '
        'GridColumn158
        '
        Me.GridColumn158.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn158.Caption = "Selección"
        Me.GridColumn158.FieldName = "Item2"
        Me.GridColumn158.Name = "GridColumn158"
        '
        'GridColumn159
        '
        Me.GridColumn159.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn159.Caption = "Selección"
        Me.GridColumn159.FieldName = "Item2"
        Me.GridColumn159.Name = "GridColumn159"
        '
        'GridColumn156
        '
        Me.GridColumn156.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn156.Caption = "Selección"
        Me.GridColumn156.FieldName = "Item2"
        Me.GridColumn156.Name = "GridColumn156"
        '
        'GridColumn157
        '
        Me.GridColumn157.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn157.Caption = "Selección"
        Me.GridColumn157.FieldName = "Item2"
        Me.GridColumn157.Name = "GridColumn157"
        '
        'GridColumn153
        '
        Me.GridColumn153.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn153.Caption = "Selección"
        Me.GridColumn153.FieldName = "Item2"
        Me.GridColumn153.Name = "GridColumn153"
        '
        'GridColumn154
        '
        Me.GridColumn154.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn154.Caption = "Selección"
        Me.GridColumn154.FieldName = "Item2"
        Me.GridColumn154.Name = "GridColumn154"
        '
        'GridColumn151
        '
        Me.GridColumn151.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn151.Caption = "Selección"
        Me.GridColumn151.FieldName = "Item2"
        Me.GridColumn151.Name = "GridColumn151"
        '
        'GridColumn152
        '
        Me.GridColumn152.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn152.Caption = "Selección"
        Me.GridColumn152.FieldName = "Item2"
        Me.GridColumn152.Name = "GridColumn152"
        '
        'GridColumn128
        '
        Me.GridColumn128.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn128.Caption = "Selección"
        Me.GridColumn128.FieldName = "Item2"
        Me.GridColumn128.Name = "GridColumn128"
        '
        'GridColumn129
        '
        Me.GridColumn129.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn129.Caption = "Selección"
        Me.GridColumn129.FieldName = "Item2"
        Me.GridColumn129.Name = "GridColumn129"
        '
        'GridColumn122
        '
        Me.GridColumn122.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn122.Caption = "Selección"
        Me.GridColumn122.FieldName = "Item2"
        Me.GridColumn122.Name = "GridColumn122"
        '
        'GridColumn123
        '
        Me.GridColumn123.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn123.Caption = "Selección"
        Me.GridColumn123.FieldName = "Item2"
        Me.GridColumn123.Name = "GridColumn123"
        '
        'GridColumn120
        '
        Me.GridColumn120.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn120.Caption = "Selección"
        Me.GridColumn120.FieldName = "Item2"
        Me.GridColumn120.Name = "GridColumn120"
        '
        'GridColumn121
        '
        Me.GridColumn121.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn121.Caption = "Selección"
        Me.GridColumn121.FieldName = "Item2"
        Me.GridColumn121.Name = "GridColumn121"
        '
        'GridColumn118
        '
        Me.GridColumn118.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn118.Caption = "Selección"
        Me.GridColumn118.FieldName = "Item2"
        Me.GridColumn118.Name = "GridColumn118"
        '
        'GridColumn119
        '
        Me.GridColumn119.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn119.Caption = "Selección"
        Me.GridColumn119.FieldName = "Item2"
        Me.GridColumn119.Name = "GridColumn119"
        '
        'GridColumn116
        '
        Me.GridColumn116.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn116.Caption = "Selección"
        Me.GridColumn116.FieldName = "Item2"
        Me.GridColumn116.Name = "GridColumn116"
        '
        'GridColumn117
        '
        Me.GridColumn117.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn117.Caption = "Selección"
        Me.GridColumn117.FieldName = "Item2"
        Me.GridColumn117.Name = "GridColumn117"
        '
        'GridColumn114
        '
        Me.GridColumn114.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn114.Caption = "Selección"
        Me.GridColumn114.FieldName = "Item2"
        Me.GridColumn114.Name = "GridColumn114"
        '
        'GridColumn115
        '
        Me.GridColumn115.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn115.Caption = "Selección"
        Me.GridColumn115.FieldName = "Item2"
        Me.GridColumn115.Name = "GridColumn115"
        '
        'GridColumn112
        '
        Me.GridColumn112.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn112.Caption = "Selección"
        Me.GridColumn112.FieldName = "Item2"
        Me.GridColumn112.Name = "GridColumn112"
        '
        'GridColumn113
        '
        Me.GridColumn113.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn113.Caption = "Selección"
        Me.GridColumn113.FieldName = "Item2"
        Me.GridColumn113.Name = "GridColumn113"
        '
        'GridColumn108
        '
        Me.GridColumn108.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn108.Caption = "Selección"
        Me.GridColumn108.FieldName = "Item2"
        Me.GridColumn108.Name = "GridColumn108"
        '
        'GridColumn109
        '
        Me.GridColumn109.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn109.Caption = "Selección"
        Me.GridColumn109.FieldName = "Item2"
        Me.GridColumn109.Name = "GridColumn109"
        '
        'GridColumn106
        '
        Me.GridColumn106.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn106.Caption = "Selección"
        Me.GridColumn106.FieldName = "Item2"
        Me.GridColumn106.Name = "GridColumn106"
        '
        'GridColumn107
        '
        Me.GridColumn107.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn107.Caption = "Selección"
        Me.GridColumn107.FieldName = "Item2"
        Me.GridColumn107.Name = "GridColumn107"
        '
        'GridColumn104
        '
        Me.GridColumn104.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn104.Caption = "Selección"
        Me.GridColumn104.FieldName = "Item2"
        Me.GridColumn104.Name = "GridColumn104"
        '
        'GridColumn105
        '
        Me.GridColumn105.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn105.Caption = "Selección"
        Me.GridColumn105.FieldName = "Item2"
        Me.GridColumn105.Name = "GridColumn105"
        '
        'GridColumn102
        '
        Me.GridColumn102.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn102.Caption = "Selección"
        Me.GridColumn102.FieldName = "Item2"
        Me.GridColumn102.Name = "GridColumn102"
        '
        'GridColumn103
        '
        Me.GridColumn103.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn103.Caption = "Selección"
        Me.GridColumn103.FieldName = "Item2"
        Me.GridColumn103.Name = "GridColumn103"
        '
        'GridColumn100
        '
        Me.GridColumn100.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn100.Caption = "Selección"
        Me.GridColumn100.FieldName = "Item2"
        Me.GridColumn100.Name = "GridColumn100"
        '
        'GridColumn101
        '
        Me.GridColumn101.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn101.Caption = "Selección"
        Me.GridColumn101.FieldName = "Item2"
        Me.GridColumn101.Name = "GridColumn101"
        '
        'GridColumn96
        '
        Me.GridColumn96.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn96.Caption = "Selección"
        Me.GridColumn96.FieldName = "Item2"
        Me.GridColumn96.Name = "GridColumn96"
        '
        'GridColumn97
        '
        Me.GridColumn97.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn97.Caption = "Selección"
        Me.GridColumn97.FieldName = "Item2"
        Me.GridColumn97.Name = "GridColumn97"
        '
        'GridColumn94
        '
        Me.GridColumn94.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn94.Caption = "Selección"
        Me.GridColumn94.FieldName = "Item2"
        Me.GridColumn94.Name = "GridColumn94"
        '
        'GridColumn95
        '
        Me.GridColumn95.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn95.Caption = "Selección"
        Me.GridColumn95.FieldName = "Item2"
        Me.GridColumn95.Name = "GridColumn95"
        '
        'GridColumn92
        '
        Me.GridColumn92.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn92.Caption = "Selección"
        Me.GridColumn92.FieldName = "Item2"
        Me.GridColumn92.Name = "GridColumn92"
        '
        'GridColumn93
        '
        Me.GridColumn93.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn93.Caption = "Selección"
        Me.GridColumn93.FieldName = "Item2"
        Me.GridColumn93.Name = "GridColumn93"
        '
        'GridColumn90
        '
        Me.GridColumn90.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn90.Caption = "Selección"
        Me.GridColumn90.FieldName = "Item2"
        Me.GridColumn90.Name = "GridColumn90"
        '
        'GridColumn91
        '
        Me.GridColumn91.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn91.Caption = "Selección"
        Me.GridColumn91.FieldName = "Item2"
        Me.GridColumn91.Name = "GridColumn91"
        '
        'GridColumn84
        '
        Me.GridColumn84.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn84.Caption = "Selección"
        Me.GridColumn84.FieldName = "Item2"
        Me.GridColumn84.Name = "GridColumn84"
        '
        'GridColumn85
        '
        Me.GridColumn85.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn85.Caption = "Selección"
        Me.GridColumn85.FieldName = "Item2"
        Me.GridColumn85.Name = "GridColumn85"
        '
        'GridColumn82
        '
        Me.GridColumn82.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn82.Caption = "Selección"
        Me.GridColumn82.FieldName = "Item2"
        Me.GridColumn82.Name = "GridColumn82"
        '
        'GridColumn83
        '
        Me.GridColumn83.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn83.Caption = "Selección"
        Me.GridColumn83.FieldName = "Item2"
        Me.GridColumn83.Name = "GridColumn83"
        '
        'GridColumn80
        '
        Me.GridColumn80.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn80.Caption = "Selección"
        Me.GridColumn80.FieldName = "Item2"
        Me.GridColumn80.Name = "GridColumn80"
        '
        'GridColumn81
        '
        Me.GridColumn81.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn81.Caption = "Selección"
        Me.GridColumn81.FieldName = "Item2"
        Me.GridColumn81.Name = "GridColumn81"
        '
        'GridColumn73
        '
        Me.GridColumn73.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn73.Caption = "Selección"
        Me.GridColumn73.FieldName = "Item2"
        Me.GridColumn73.Name = "GridColumn73"
        '
        'GridColumn79
        '
        Me.GridColumn79.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn79.Caption = "Selección"
        Me.GridColumn79.FieldName = "Item2"
        Me.GridColumn79.Name = "GridColumn79"
        '
        'GridColumn71
        '
        Me.GridColumn71.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn71.Caption = "Selección"
        Me.GridColumn71.FieldName = "Item2"
        Me.GridColumn71.Name = "GridColumn71"
        '
        'GridColumn72
        '
        Me.GridColumn72.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn72.Caption = "Selección"
        Me.GridColumn72.FieldName = "Item2"
        Me.GridColumn72.Name = "GridColumn72"
        '
        'GridColumn69
        '
        Me.GridColumn69.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn69.Caption = "Selección"
        Me.GridColumn69.FieldName = "Item2"
        Me.GridColumn69.Name = "GridColumn69"
        '
        'GridColumn70
        '
        Me.GridColumn70.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn70.Caption = "Selección"
        Me.GridColumn70.FieldName = "Item2"
        Me.GridColumn70.Name = "GridColumn70"
        '
        'GridColumn67
        '
        Me.GridColumn67.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn67.Caption = "Selección"
        Me.GridColumn67.FieldName = "Item2"
        Me.GridColumn67.Name = "GridColumn67"
        '
        'GridColumn68
        '
        Me.GridColumn68.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn68.Caption = "Selección"
        Me.GridColumn68.FieldName = "Item2"
        Me.GridColumn68.Name = "GridColumn68"
        '
        'GridColumn63
        '
        Me.GridColumn63.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn63.Caption = "Selección"
        Me.GridColumn63.FieldName = "Item2"
        Me.GridColumn63.Name = "GridColumn63"
        '
        'GridColumn64
        '
        Me.GridColumn64.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn64.Caption = "Selección"
        Me.GridColumn64.FieldName = "Item2"
        Me.GridColumn64.Name = "GridColumn64"
        '
        'GridColumn61
        '
        Me.GridColumn61.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn61.Caption = "Selección"
        Me.GridColumn61.FieldName = "Item2"
        Me.GridColumn61.Name = "GridColumn61"
        '
        'GridColumn62
        '
        Me.GridColumn62.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn62.Caption = "Selección"
        Me.GridColumn62.FieldName = "Item2"
        Me.GridColumn62.Name = "GridColumn62"
        '
        'GridColumn57
        '
        Me.GridColumn57.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn57.Caption = "Selección"
        Me.GridColumn57.FieldName = "Item2"
        Me.GridColumn57.Name = "GridColumn57"
        '
        'GridColumn58
        '
        Me.GridColumn58.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn58.Caption = "Selección"
        Me.GridColumn58.FieldName = "Item2"
        Me.GridColumn58.Name = "GridColumn58"
        '
        'GridColumn54
        '
        Me.GridColumn54.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn54.Caption = "Selección"
        Me.GridColumn54.FieldName = "Item2"
        Me.GridColumn54.Name = "GridColumn54"
        '
        'GridColumn55
        '
        Me.GridColumn55.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn55.Caption = "Selección"
        Me.GridColumn55.FieldName = "Item2"
        Me.GridColumn55.Name = "GridColumn55"
        '
        'GridColumn52
        '
        Me.GridColumn52.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn52.Caption = "Selección"
        Me.GridColumn52.FieldName = "Item2"
        Me.GridColumn52.Name = "GridColumn52"
        '
        'GridColumn53
        '
        Me.GridColumn53.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn53.Caption = "Selección"
        Me.GridColumn53.FieldName = "Item2"
        Me.GridColumn53.Name = "GridColumn53"
        '
        'GridColumn48
        '
        Me.GridColumn48.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn48.Caption = "Selección"
        Me.GridColumn48.FieldName = "Item2"
        Me.GridColumn48.Name = "GridColumn48"
        '
        'GridColumn49
        '
        Me.GridColumn49.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn49.Caption = "Selección"
        Me.GridColumn49.FieldName = "Item2"
        Me.GridColumn49.Name = "GridColumn49"
        '
        'GridColumn46
        '
        Me.GridColumn46.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn46.Caption = "Selección"
        Me.GridColumn46.FieldName = "Item2"
        Me.GridColumn46.Name = "GridColumn46"
        '
        'GridColumn47
        '
        Me.GridColumn47.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn47.Caption = "Selección"
        Me.GridColumn47.FieldName = "Item2"
        Me.GridColumn47.Name = "GridColumn47"
        '
        'GridColumn44
        '
        Me.GridColumn44.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn44.Caption = "Selección"
        Me.GridColumn44.FieldName = "Item2"
        Me.GridColumn44.Name = "GridColumn44"
        '
        'GridColumn45
        '
        Me.GridColumn45.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn45.Caption = "Selección"
        Me.GridColumn45.FieldName = "Item2"
        Me.GridColumn45.Name = "GridColumn45"
        '
        'GridColumn42
        '
        Me.GridColumn42.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn42.Caption = "Selección"
        Me.GridColumn42.FieldName = "Item2"
        Me.GridColumn42.Name = "GridColumn42"
        '
        'GridColumn43
        '
        Me.GridColumn43.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn43.Caption = "Selección"
        Me.GridColumn43.FieldName = "Item2"
        Me.GridColumn43.Name = "GridColumn43"
        '
        'GridColumn38
        '
        Me.GridColumn38.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn38.Caption = "Selección"
        Me.GridColumn38.FieldName = "Item2"
        Me.GridColumn38.Name = "GridColumn38"
        '
        'GridColumn39
        '
        Me.GridColumn39.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn39.Caption = "Selección"
        Me.GridColumn39.FieldName = "Item2"
        Me.GridColumn39.Name = "GridColumn39"
        '
        'GridColumn36
        '
        Me.GridColumn36.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn36.Caption = "Selección"
        Me.GridColumn36.FieldName = "Item2"
        Me.GridColumn36.Name = "GridColumn36"
        '
        'GridColumn37
        '
        Me.GridColumn37.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn37.Caption = "Selección"
        Me.GridColumn37.FieldName = "Item2"
        Me.GridColumn37.Name = "GridColumn37"
        '
        'GridColumn34
        '
        Me.GridColumn34.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn34.Caption = "Selección"
        Me.GridColumn34.FieldName = "Item2"
        Me.GridColumn34.Name = "GridColumn34"
        '
        'GridColumn35
        '
        Me.GridColumn35.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn35.Caption = "Selección"
        Me.GridColumn35.FieldName = "Item2"
        Me.GridColumn35.Name = "GridColumn35"
        '
        'GridColumn32
        '
        Me.GridColumn32.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn32.Caption = "Selección"
        Me.GridColumn32.FieldName = "Item2"
        Me.GridColumn32.Name = "GridColumn32"
        '
        'GridColumn33
        '
        Me.GridColumn33.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn33.Caption = "Selección"
        Me.GridColumn33.FieldName = "Item2"
        Me.GridColumn33.Name = "GridColumn33"
        '
        'GridColumn30
        '
        Me.GridColumn30.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn30.Caption = "Selección"
        Me.GridColumn30.FieldName = "Item2"
        Me.GridColumn30.Name = "GridColumn30"
        '
        'GridColumn31
        '
        Me.GridColumn31.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn31.Caption = "Selección"
        Me.GridColumn31.FieldName = "Item2"
        Me.GridColumn31.Name = "GridColumn31"
        '
        'GridColumn28
        '
        Me.GridColumn28.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn28.Caption = "Selección"
        Me.GridColumn28.FieldName = "Item2"
        Me.GridColumn28.Name = "GridColumn28"
        '
        'GridColumn29
        '
        Me.GridColumn29.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn29.Caption = "Selección"
        Me.GridColumn29.FieldName = "Item2"
        Me.GridColumn29.Name = "GridColumn29"
        '
        'GridColumn26
        '
        Me.GridColumn26.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn26.Caption = "Selección"
        Me.GridColumn26.FieldName = "Item2"
        Me.GridColumn26.Name = "GridColumn26"
        '
        'GridColumn27
        '
        Me.GridColumn27.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn27.Caption = "Selección"
        Me.GridColumn27.FieldName = "Item2"
        Me.GridColumn27.Name = "GridColumn27"
        '
        'GridColumn24
        '
        Me.GridColumn24.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn24.Caption = "Selección"
        Me.GridColumn24.FieldName = "Item2"
        Me.GridColumn24.Name = "GridColumn24"
        '
        'GridColumn25
        '
        Me.GridColumn25.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn25.Caption = "Selección"
        Me.GridColumn25.FieldName = "Item2"
        Me.GridColumn25.Name = "GridColumn25"
        '
        'GridColumn23
        '
        Me.GridColumn23.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn23.Caption = "Selección"
        Me.GridColumn23.FieldName = "Item2"
        Me.GridColumn23.Name = "GridColumn23"
        '
        'GridColumn21
        '
        Me.GridColumn21.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn21.Caption = "Selección"
        Me.GridColumn21.FieldName = "Item2"
        Me.GridColumn21.Name = "GridColumn21"
        '
        'GridColumn19
        '
        Me.GridColumn19.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn19.Caption = "Selección"
        Me.GridColumn19.FieldName = "Item2"
        Me.GridColumn19.Name = "GridColumn19"
        '
        'GridColumn141
        '
        Me.GridColumn141.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn141.Caption = "Selección"
        Me.GridColumn141.FieldName = "Item2"
        Me.GridColumn141.Name = "GridColumn141"
        '
        'GridColumn22
        '
        Me.GridColumn22.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn22.Caption = "Selección"
        Me.GridColumn22.FieldName = "Item2"
        Me.GridColumn22.Name = "GridColumn22"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmSettingsAuthorization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 729)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSettingsAuthorization"
        Me.Opacity = 1.0R
        Me.Tag = "2181"
        Me.Text = "Parametros de Autorización"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCnNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDGleAutomaticAllocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAutomaticAllocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeDaysToAssign.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAutomaticAllocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDaysToAssign, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDCnNavigation As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn141 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents GridColumn54 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn57 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn58 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn61 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn62 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn63 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn64 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn67 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn68 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn69 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn70 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn71 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn72 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn73 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn79 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn80 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn81 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn82 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn83 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn84 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn85 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn90 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn91 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn92 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn93 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn94 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn95 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn96 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn97 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn100 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn101 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn102 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn103 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn104 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn105 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn106 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn107 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn108 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn109 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn114 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn115 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn116 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn117 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn118 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn119 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn120 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn121 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn122 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn123 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn128 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn129 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn151 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn152 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn153 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn154 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn156 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn157 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn158 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn159 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn160 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn161 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn163 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn164 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn165 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn166 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn172 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn173 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn174 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn175 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn176 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn177 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn182 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn183 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn184 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn185 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn187 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn188 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn190 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn191 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn192 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn193 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn194 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn195 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn200 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn201 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn202 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn203 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn196 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn197 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn198 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn199 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn206 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn207 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn208 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn209 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn210 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn211 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn214 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn215 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn216 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn217 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn218 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn219 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn221 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn222 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn224 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn225 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn234 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn235 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn244 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn245 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn246 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn247 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn248 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn249 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn252 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn253 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn254 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn255 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn256 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn257 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn258 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn259 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn262 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn263 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGleAutomaticAllocation As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDGvAutomaticAllocation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvAutomaticAllocation_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn264 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn265 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciAutomaticAllocation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDaysToAssign As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeDaysToAssign As DevExpress.XtraEditors.SpinEdit
End Class
