Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTaxeStatles
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
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDclkind = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.INDseDaysDeadlines = New DevExpress.XtraEditors.SpinEdit()
        Me.INDgleStateType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SpinEdit1 = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtCode = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcStateType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcDaysDeadlines = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlckind = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDclkind, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseDaysDeadlines.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleStateType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpinEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcStateType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcDaysDeadlines, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlckind, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 574)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDclkind)
        Me.LayoutControl1.Controls.Add(Me.INDseDaysDeadlines)
        Me.LayoutControl1.Controls.Add(Me.INDgleStateType)
        Me.LayoutControl1.Controls.Add(Me.SpinEdit1)
        Me.LayoutControl1.Controls.Add(Me.INDtxtCode)
        Me.LayoutControl1.Controls.Add(Me.INDtxtName)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1261, 574)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDclkind
        '
        Me.INDclkind.Items.AddRange(New DevExpress.XtraEditors.Controls.CheckedListBoxItem() {New DevExpress.XtraEditors.Controls.CheckedListBoxItem(Nothing, "Control de Plazos"), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(Nothing, "Control de Documentos"), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(Nothing, "Control de Acuerdos de Pago")})
        Me.INDclkind.Location = New System.Drawing.Point(24, 383)
        Me.INDclkind.Name = "INDclkind"
        Me.INDclkind.Size = New System.Drawing.Size(386, 57)
        Me.INDclkind.StyleController = Me.LayoutControl1
        Me.INDclkind.TabIndex = 13
        '
        'INDseDaysDeadlines
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseDaysDeadlines, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseDaysDeadlines, False)
        Me.INDseDaysDeadlines.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseDaysDeadlines.EnterMoveNextControl = True
        Me.INDseDaysDeadlines.Location = New System.Drawing.Point(24, 325)
        Me.IndigoTextEdit1.SetMascara(Me.INDseDaysDeadlines, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseDaysDeadlines.Name = "INDseDaysDeadlines"
        Me.INDseDaysDeadlines.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseDaysDeadlines.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseDaysDeadlines.Properties.Appearance.Options.UseBackColor = True
        Me.INDseDaysDeadlines.Properties.Appearance.Options.UseFont = True
        Me.INDseDaysDeadlines.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseDaysDeadlines.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseDaysDeadlines.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseDaysDeadlines.Size = New System.Drawing.Size(386, 28)
        Me.INDseDaysDeadlines.StyleController = Me.LayoutControl1
        Me.INDseDaysDeadlines.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseDaysDeadlines, 0)
        '
        'INDgleStateType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleStateType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleStateType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleStateType, True)
        Me.INDgleStateType.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleStateType, True)
        Me.INDgleStateType.Location = New System.Drawing.Point(24, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleStateType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleStateType.Name = "INDgleStateType"
        Me.INDgleStateType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDgleStateType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgleStateType.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleStateType.Properties.Appearance.Options.UseFont = True
        Me.INDgleStateType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleStateType.Properties.ImmediatePopup = True
        Me.INDgleStateType.Properties.NullText = ""
        Me.INDgleStateType.Properties.View = Me.GridLookUpEdit1View
        Me.INDgleStateType.Size = New System.Drawing.Size(386, 28)
        Me.INDgleStateType.StyleController = Me.LayoutControl1
        Me.INDgleStateType.TabIndex = 11
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleStateType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleStateType, 0)
        Me.INDgleStateType.ToolTip = "Este Campo es Necesario"
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 292
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Descripción"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 836
        '
        'SpinEdit1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.SpinEdit1, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.SpinEdit1, True)
        Me.SpinEdit1.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.SpinEdit1.EnterMoveNextControl = True
        Me.SpinEdit1.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.SpinEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.SpinEdit1.Name = "SpinEdit1"
        Me.SpinEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.SpinEdit1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SpinEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.SpinEdit1.Properties.Appearance.Options.UseFont = True
        Me.SpinEdit1.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SpinEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.SpinEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SpinEdit1.Size = New System.Drawing.Size(386, 28)
        Me.SpinEdit1.StyleController = Me.LayoutControl1
        Me.SpinEdit1.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.SpinEdit1, 0)
        '
        'INDtxtCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtCode, True)
        Me.INDtxtCode.EnterMoveNextControl = True
        Me.INDtxtCode.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtCode.Name = "INDtxtCode"
        Me.INDtxtCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtCode.Properties.Appearance.Options.UseFont = True
        Me.INDtxtCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtCode.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtCode.StyleController = Me.LayoutControl1
        Me.INDtxtCode.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtCode, 0)
        Me.INDtxtCode.ToolTip = "Este Campo es Necesario"
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtName.StyleController = Me.LayoutControl1
        Me.INDtxtName.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1261, 574)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcName, Me.INDlcCode, Me.LayoutControlItem1, Me.INDlcStateType, Me.INDlcDaysDeadlines, Me.INDlckind})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1241, 554)
        Me.LayoutControlGroup2.Text = "Datos Generales"
        '
        'INDlcName
        '
        Me.INDlcName.Control = Me.INDtxtName
        Me.INDlcName.Location = New System.Drawing.Point(0, 120)
        Me.INDlcName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlcName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlcName.Name = "INDlcName"
        Me.INDlcName.Size = New System.Drawing.Size(1217, 60)
        Me.INDlcName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlcName.Text = "Nombre:"
        Me.INDlcName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlcName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlcName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlcName.TextToControlDistance = 5
        '
        'INDlcCode
        '
        Me.INDlcCode.Control = Me.INDtxtCode
        Me.INDlcCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlcCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlcCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlcCode.Name = "INDlcCode"
        Me.INDlcCode.Size = New System.Drawing.Size(1217, 60)
        Me.INDlcCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlcCode.Text = "Código:"
        Me.INDlcCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlcCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlcCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlcCode.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.SpinEdit1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1217, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Indice:"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'INDlcStateType
        '
        Me.INDlcStateType.Control = Me.INDgleStateType
        Me.INDlcStateType.Location = New System.Drawing.Point(0, 180)
        Me.INDlcStateType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlcStateType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlcStateType.Name = "INDlcStateType"
        Me.INDlcStateType.Size = New System.Drawing.Size(1217, 60)
        Me.INDlcStateType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlcStateType.Text = "Tipo Estado:"
        Me.INDlcStateType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlcStateType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlcStateType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlcStateType.TextToControlDistance = 5
        '
        'INDlcDaysDeadlines
        '
        Me.INDlcDaysDeadlines.Control = Me.INDseDaysDeadlines
        Me.INDlcDaysDeadlines.Location = New System.Drawing.Point(0, 240)
        Me.INDlcDaysDeadlines.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlcDaysDeadlines.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlcDaysDeadlines.Name = "INDlcDaysDeadlines"
        Me.INDlcDaysDeadlines.Size = New System.Drawing.Size(1217, 60)
        Me.INDlcDaysDeadlines.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlcDaysDeadlines.Text = "Días de Plazo:"
        Me.INDlcDaysDeadlines.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlcDaysDeadlines.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlcDaysDeadlines.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlcDaysDeadlines.TextToControlDistance = 5
        '
        'INDlckind
        '
        Me.INDlckind.Control = Me.INDclkind
        Me.INDlckind.Location = New System.Drawing.Point(0, 300)
        Me.INDlckind.MaxSize = New System.Drawing.Size(390, 85)
        Me.INDlckind.MinSize = New System.Drawing.Size(390, 85)
        Me.INDlckind.Name = "INDlckind"
        Me.INDlckind.Size = New System.Drawing.Size(1217, 195)
        Me.INDlckind.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlckind.Text = "Tipo:"
        Me.INDlckind.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlckind.TextSize = New System.Drawing.Size(33, 21)
        '
        'FrmTaxeStatles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmTaxeStatles"
        Me.Opacity = 1.0R
        Me.Tag = "1849"
        Me.Text = "Ruta de Estados"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDclkind, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseDaysDeadlines.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleStateType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpinEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcStateType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcDaysDeadlines, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlckind, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlcName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDtxtCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlcCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseDaysDeadlines As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDgleStateType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SpinEdit1 As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcStateType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcDaysDeadlines As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDclkind As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents INDlckind As DevExpress.XtraLayout.LayoutControlItem
End Class
