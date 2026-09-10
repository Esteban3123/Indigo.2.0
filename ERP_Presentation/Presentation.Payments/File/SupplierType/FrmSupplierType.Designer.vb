Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSupplierType
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
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlySupplierType = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtlStruct = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDbtnAddStruct = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygStruct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAddStruct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemStruct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlySupplierType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlySupplierType.SuspendLayout()
        CType(Me.INDtlStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlySupplierType)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(904, 579)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(904, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(904, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlySupplierType
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlySupplierType
        '
        Me.INDlySupplierType.AllowCustomization = False
        Me.INDlySupplierType.Controls.Add(Me.INDtlStruct)
        Me.INDlySupplierType.Controls.Add(Me.INDbtnAddStruct)
        Me.INDlySupplierType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlySupplierType, False)
        Me.INDlySupplierType.Location = New System.Drawing.Point(202, 7)
        Me.INDlySupplierType.Name = "INDlySupplierType"
        Me.INDlySupplierType.Root = Me.LayoutControlGroup1
        Me.INDlySupplierType.Size = New System.Drawing.Size(700, 570)
        Me.INDlySupplierType.TabIndex = 1
        Me.INDlySupplierType.Text = "LayoutControl1"
        '
        'INDtlStruct
        '
        Me.INDtlStruct.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDtlStruct.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDtlStruct.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDtlStruct.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDtlStruct.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtlStruct.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.INDtlStruct.Appearance.HeaderPanel.Options.UseFont = True
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
        Me.INDtlStruct.Size = New System.Drawing.Size(824, 434)
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
        Me.TreeListColumn1.Width = 230
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
        Me.TreeListColumn2.Width = 578
        '
        'INDbtnAddStruct
        '
        Me.INDbtnAddStruct.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddStruct.Appearance.Options.UseFont = True
        Me.INDbtnAddStruct.Location = New System.Drawing.Point(24, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddStruct, True)
        Me.INDbtnAddStruct.Name = "INDbtnAddStruct"
        Me.INDbtnAddStruct.Size = New System.Drawing.Size(824, 32)
        Me.INDbtnAddStruct.StyleController = Me.INDlySupplierType
        Me.INDbtnAddStruct.TabIndex = 4
        Me.INDbtnAddStruct.Text = "Agregar Estructura"
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "Tipo Proveedor"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygStruct})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(872, 553)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygStruct
        '
        Me.INDlygStruct.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygStruct.AppearanceGroup.Options.UseFont = True
        Me.INDlygStruct.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygStruct.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygStruct.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygStruct.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygStruct.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygStruct.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygStruct.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygStruct.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygStruct.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygStruct.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygStruct.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygStruct.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygStruct, False)
        Me.INDlygStruct.CustomizationFormText = "Estructura"
        Me.INDlygStruct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAddStruct, Me.INDlyItemStruct})
        Me.INDlygStruct.Location = New System.Drawing.Point(0, 0)
        Me.INDlygStruct.Name = "INDlygStruct"
        Me.INDlygStruct.Size = New System.Drawing.Size(852, 533)
        Me.INDlygStruct.Text = "Estructura"
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
        Me.INDlyItemStruct.Name = "INDlyItemStruct"
        Me.INDlyItemStruct.Size = New System.Drawing.Size(828, 438)
        Me.INDlyItemStruct.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemStruct.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmSupplierType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(904, 701)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSupplierType"
        Me.Opacity = 1.0R
        Me.Tag = "726"
        Me.Text = "Tipo Proveedor"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlySupplierType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlySupplierType.ResumeLayout(False)
        CType(Me.INDtlStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlySupplierType As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnAddStruct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAddStruct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygStruct As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDtlStruct As DevExpress.XtraTreeList.TreeList
    Friend WithEvents INDlyItemStruct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
End Class
