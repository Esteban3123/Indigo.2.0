Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFilingUnit
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
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyFilingUnit = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtlStruct = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDbtnAddStruct = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygFilingUnit = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAddStruct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemStruct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyFilingUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyFilingUnit.SuspendLayout()
        CType(Me.INDtlStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygFilingUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyFilingUnit)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(904, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(904, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(904, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyFilingUnit
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 574)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyFilingUnit
        '
        Me.INDlyFilingUnit.AllowCustomization = False
        Me.INDlyFilingUnit.Controls.Add(Me.INDtlStruct)
        Me.INDlyFilingUnit.Controls.Add(Me.INDbtnAddStruct)
        Me.INDlyFilingUnit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyFilingUnit, False)
        Me.INDlyFilingUnit.Location = New System.Drawing.Point(202, 7)
        Me.INDlyFilingUnit.Name = "INDlyFilingUnit"
        Me.INDlyFilingUnit.Root = Me.LayoutControlGroup1
        Me.INDlyFilingUnit.Size = New System.Drawing.Size(700, 574)
        Me.INDlyFilingUnit.TabIndex = 1
        Me.INDlyFilingUnit.Text = "LayoutControl1"
        '
        'INDtlStruct
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.White
        Me.INDtlStruct.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDtlStruct.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDtlStruct.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDtlStruct.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDtlStruct.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtlStruct.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.INDtlStruct.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDtlStruct.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDtlStruct.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.INDtlStruct.Appearance.Row.Options.UseFont = True
        Me.INDtlStruct.Appearance.Row.Options.UseForeColor = True
        Me.INDtlStruct.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn1, Me.TreeListColumn2})
        Me.IndigoTreeList1.SetExpandedAllNodes(Me.INDtlStruct, False)
        Me.INDtlStruct.KeyFieldName = "Id"
        Me.INDtlStruct.Location = New System.Drawing.Point(24, 95)
        Me.INDtlStruct.Name = "INDtlStruct"
        Me.INDtlStruct.OptionsBehavior.EnableFiltering = True
        Me.INDtlStruct.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
        Me.INDtlStruct.OptionsFind.AllowFindPanel = True
        Me.INDtlStruct.OptionsFind.AlwaysVisible = True
        Me.INDtlStruct.OptionsView.EnableAppearanceEvenRow = True
        Me.INDtlStruct.OptionsView.EnableAppearanceOddRow = True
        Me.INDtlStruct.ParentFieldName = "ParentId"
        Me.INDtlStruct.Size = New System.Drawing.Size(824, 438)
        Me.INDtlStruct.TabIndex = 5
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Código"
        Me.TreeListColumn1.FieldName = "Code"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.OptionsColumn.AllowEdit = False
        Me.TreeListColumn1.OptionsColumn.AllowFocus = False
        Me.TreeListColumn1.SortOrder = System.Windows.Forms.SortOrder.Ascending
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        Me.TreeListColumn1.Width = 212
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Nombre"
        Me.TreeListColumn2.FieldName = "Name"
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.OptionsColumn.AllowEdit = False
        Me.TreeListColumn2.OptionsColumn.AllowFocus = False
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 1
        Me.TreeListColumn2.Width = 594
        '
        'INDbtnAddStruct
        '
        Me.INDbtnAddStruct.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddStruct.Appearance.Options.UseFont = True
        Me.INDbtnAddStruct.Location = New System.Drawing.Point(24, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddStruct, True)
        Me.INDbtnAddStruct.Name = "INDbtnAddStruct"
        Me.INDbtnAddStruct.Size = New System.Drawing.Size(824, 32)
        Me.INDbtnAddStruct.StyleController = Me.INDlyFilingUnit
        Me.INDbtnAddStruct.TabIndex = 4
        Me.INDbtnAddStruct.Text = "Agregar Estructura"
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
        Me.LayoutControlGroup1.CustomizationFormText = "Unidad Radicación"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygFilingUnit})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(872, 557)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygFilingUnit
        '
        Me.INDlygFilingUnit.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygFilingUnit.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygFilingUnit.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygFilingUnit.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygFilingUnit.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygFilingUnit.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygFilingUnit.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygFilingUnit.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygFilingUnit.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygFilingUnit.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygFilingUnit.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygFilingUnit.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygFilingUnit.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygFilingUnit.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygFilingUnit, False)
        Me.INDlygFilingUnit.CustomizationFormText = "Estructura"
        Me.INDlygFilingUnit.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAddStruct, Me.INDlyItemStruct})
        Me.INDlygFilingUnit.Location = New System.Drawing.Point(0, 0)
        Me.INDlygFilingUnit.Name = "INDlygFilingUnit"
        Me.INDlygFilingUnit.Size = New System.Drawing.Size(852, 537)
        Me.INDlygFilingUnit.Text = "Estructura"
        '
        'INDlyItemAddStruct
        '
        Me.INDlyItemAddStruct.Control = Me.INDbtnAddStruct
        Me.INDlyItemAddStruct.CustomizationFormText = "Agregar Estructura"
        Me.INDlyItemAddStruct.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddStruct.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddStruct.MinSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddStruct.Name = "INDlyItemAddStruct"
        Me.INDlyItemAddStruct.Size = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddStruct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddStruct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAddStruct.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddStruct.TextToControlDistance = 0
        Me.INDlyItemAddStruct.TextVisible = False
        '
        'INDlyItemStruct
        '
        Me.INDlyItemStruct.Control = Me.INDtlStruct
        Me.INDlyItemStruct.CustomizationFormText = "Estructuras"
        Me.INDlyItemStruct.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemStruct.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemStruct.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemStruct.Name = "INDlyItemStruct"
        Me.INDlyItemStruct.Size = New System.Drawing.Size(828, 442)
        Me.INDlyItemStruct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemStruct.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemStruct.TextVisible = False
        '
        'FrmFilingUnit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(904, 701)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFilingUnit"
        Me.Opacity = 1.0R
        Me.Tag = "723"
        Me.Text = "Unidad Radicación"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyFilingUnit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyFilingUnit.ResumeLayout(False)
        CType(Me.INDtlStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygFilingUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyFilingUnit As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnAddStruct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAddStruct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygFilingUnit As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDtlStruct As DevExpress.XtraTreeList.TreeList
    Friend WithEvents INDlyItemStruct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
End Class
