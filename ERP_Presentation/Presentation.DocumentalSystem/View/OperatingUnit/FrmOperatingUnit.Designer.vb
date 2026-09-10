Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOperatingUnit
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
        Me.INDtlStruct = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDbtnAddStruct = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLciBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygStruct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAddStruct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCnNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDtlStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCnNavigation)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1074, 611)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1074, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1074, 94)
        '
        'INDCnNavigation
        '
        Me.INDCnNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCnNavigation.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnNavigation.LayoutControl = Me.INDLcBase
        Me.INDCnNavigation.Location = New System.Drawing.Point(2, 7)
        Me.INDCnNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCnNavigation.Name = "INDCnNavigation"
        Me.INDCnNavigation.Size = New System.Drawing.Size(200, 602)
        Me.INDCnNavigation.TabIndex = 0
        Me.INDCnNavigation.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.AllowCustomization = False
        Me.INDLcBase.Controls.Add(Me.INDtlStruct)
        Me.INDLcBase.Controls.Add(Me.INDbtnAddStruct)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLcBase, False)
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLciBase
        Me.INDLcBase.Size = New System.Drawing.Size(870, 602)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
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
        Me.INDtlStruct.OptionsFind.FindFilterColumns = "UnitCode"
        Me.INDtlStruct.OptionsView.EnableAppearanceEvenRow = True
        Me.INDtlStruct.OptionsView.EnableAppearanceOddRow = True
        Me.INDtlStruct.ParentFieldName = "IdUnit"
        Me.INDtlStruct.Size = New System.Drawing.Size(824, 466)
        Me.INDtlStruct.TabIndex = 5
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Código"
        Me.TreeListColumn1.FieldName = "UnitCode"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.OptionsColumn.AllowEdit = False
        Me.TreeListColumn1.OptionsColumn.AllowFocus = False
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        Me.TreeListColumn1.Width = 218
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Nombre"
        Me.TreeListColumn2.FieldName = "UnitName"
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.OptionsColumn.AllowEdit = False
        Me.TreeListColumn2.OptionsColumn.AllowFocus = False
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 1
        Me.TreeListColumn2.Width = 588
        '
        'INDbtnAddStruct
        '
        Me.INDbtnAddStruct.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddStruct.Appearance.Options.UseFont = True
        Me.INDbtnAddStruct.Location = New System.Drawing.Point(24, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddStruct, True)
        Me.INDbtnAddStruct.Name = "INDbtnAddStruct"
        Me.INDbtnAddStruct.Size = New System.Drawing.Size(824, 32)
        Me.INDbtnAddStruct.StyleController = Me.INDLcBase
        Me.INDbtnAddStruct.TabIndex = 4
        Me.INDbtnAddStruct.Text = "Agregar Estructura"
        '
        'INDLciBase
        '
        Me.INDLciBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLciBase.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLciBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLciBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLciBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLciBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLciBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLciBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLciBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLciBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLciBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLciBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLciBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLciBase, False)
        Me.INDLciBase.CustomizationFormText = "Unidad Operativa"
        Me.INDLciBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLciBase.GroupBordersVisible = False
        Me.INDLciBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygStruct})
        Me.INDLciBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLciBase.Name = "INDLciBase"
        Me.INDLciBase.Size = New System.Drawing.Size(872, 585)
        '
        'INDlygStruct
        '
        Me.INDlygStruct.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygStruct.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygStruct.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygStruct.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygStruct.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygStruct.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygStruct.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygStruct.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygStruct.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygStruct.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygStruct.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygStruct.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygStruct.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygStruct.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygStruct, False)
        Me.INDlygStruct.CustomizationFormText = "Estructura"
        Me.INDlygStruct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAddStruct, Me.LayoutControlItem1})
        Me.INDlygStruct.Location = New System.Drawing.Point(0, 0)
        Me.INDlygStruct.Name = "INDlygStruct"
        Me.INDlygStruct.Size = New System.Drawing.Size(852, 565)
        Me.INDlygStruct.Text = "Estructura"
        '
        'INDlyItemAddStruct
        '
        Me.INDlyItemAddStruct.Control = Me.INDbtnAddStruct
        Me.INDlyItemAddStruct.CustomizationFormText = "Agregar"
        Me.INDlyItemAddStruct.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddStruct.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddStruct.MinSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddStruct.Name = "INDlyItemAddStruct"
        Me.INDlyItemAddStruct.Size = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddStruct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddStruct.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddStruct.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDtlStruct
        Me.LayoutControlItem1.CustomizationFormText = "Estructuras"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 470)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FrmOperatingUnit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1074, 729)
        Me.Name = "FrmOperatingUnit"
        Me.Opacity = 1.0R
        Me.Tag = "1511"
        Me.Text = "Unidad Operativa"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCnNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDtlStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDCnNavigation As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLciBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDbtnAddStruct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAddStruct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygStruct As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDtlStruct As DevExpress.XtraTreeList.TreeList
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
End Class
