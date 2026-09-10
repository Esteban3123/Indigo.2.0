Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostOrganizationalStructure
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCostOrganizationalStructure))
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbAddOrgStruct = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtlStruct = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgStruct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliStruct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDtlStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(901, 415)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(901, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(901, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.Appearance.BackColor = System.Drawing.Color.White
        Me.CtrNavigationControl1.Appearance.Image = CType(resources.GetObject("CtrNavigationControl1.Appearance.Image"), System.Drawing.Image)
        Me.CtrNavigationControl1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControl1.Appearance.Options.UseImage = True
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(4, 9)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 401)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDsbAddOrgStruct)
        Me.INDlcRoot.Controls.Add(Me.INDtlStruct)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(204, 9)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2052, 381, 250, 350)
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(693, 401)
        Me.INDlcRoot.TabIndex = 1
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDsbAddOrgStruct
        '
        Me.INDsbAddOrgStruct.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsbAddOrgStruct.Appearance.Options.UseFont = True
        Me.INDsbAddOrgStruct.Location = New System.Drawing.Point(9, 43)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAddOrgStruct, True)
        Me.INDsbAddOrgStruct.Name = "INDsbAddOrgStruct"
        Me.INDsbAddOrgStruct.Size = New System.Drawing.Size(814, 26)
        Me.INDsbAddOrgStruct.StyleController = Me.INDlcRoot
        Me.INDsbAddOrgStruct.TabIndex = 7
        Me.INDsbAddOrgStruct.Text = "Agregar Estructura"
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
        Me.INDtlStruct.Location = New System.Drawing.Point(9, 79)
        Me.INDtlStruct.Name = "INDtlStruct"
        Me.INDtlStruct.OptionsBehavior.EnableFiltering = True
        Me.INDtlStruct.OptionsBehavior.PopulateServiceColumns = True
        Me.INDtlStruct.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
        Me.INDtlStruct.OptionsFind.AllowFindPanel = True
        Me.INDtlStruct.OptionsFind.AlwaysVisible = True
        Me.INDtlStruct.OptionsView.EnableAppearanceEvenRow = True
        Me.INDtlStruct.OptionsView.EnableAppearanceOddRow = True
        Me.INDtlStruct.ParentFieldName = "ParentId"
        Me.INDtlStruct.Size = New System.Drawing.Size(814, 295)
        Me.INDtlStruct.TabIndex = 5
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Código"
        Me.TreeListColumn1.FieldName = "Code"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.OptionsColumn.AllowEdit = False
        Me.TreeListColumn1.OptionsColumn.AllowFocus = False
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        Me.TreeListColumn1.Width = 209
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
        Me.TreeListColumn2.Width = 593
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, False)
        Me.INDlcgRoot.CustomizationFormText = "Root"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgStruct})
        Me.INDlcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(832, 384)
        Me.INDlcgRoot.TextVisible = False
        '
        'INDlcgStruct
        '
        Me.INDlcgStruct.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgStruct.AppearanceGroup.Options.UseFont = True
        Me.INDlcgStruct.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgStruct.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgStruct.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgStruct.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgStruct.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgStruct.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgStruct.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgStruct.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgStruct.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgStruct.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgStruct.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgStruct.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgStruct, False)
        Me.INDlcgStruct.CustomizationFormText = "Datos Principales"
        Me.INDlcgStruct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliStruct, Me.LayoutControlItem1})
        Me.INDlcgStruct.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgStruct.Name = "INDlcgStruct"
        Me.INDlcgStruct.Size = New System.Drawing.Size(832, 384)
        Me.INDlcgStruct.Text = "Estructura"
        '
        'INDliStruct
        '
        Me.INDliStruct.Control = Me.INDtlStruct
        Me.INDliStruct.CustomizationFormText = "Estructura Organizacional"
        Me.INDliStruct.Location = New System.Drawing.Point(0, 36)
        Me.INDliStruct.MaxSize = New System.Drawing.Size(824, 0)
        Me.INDliStruct.MinSize = New System.Drawing.Size(824, 24)
        Me.INDliStruct.Name = "INDliStruct"
        Me.INDliStruct.Size = New System.Drawing.Size(824, 305)
        Me.INDliStruct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliStruct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliStruct.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliStruct.TextToControlDistance = 0
        Me.INDliStruct.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDsbAddOrgStruct
        Me.LayoutControlItem1.CustomizationFormText = "Agregar Estructura"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(824, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(824, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(824, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmCostOrganizationalStructure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(901, 537)
        Me.Name = "FrmCostOrganizationalStructure"
        Me.Opacity = 1.0R
        Me.Tag = "1728"
        Me.Text = "Estructura Organizacional"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDtlStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents INDlcgStruct As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtlStruct As DevExpress.XtraTreeList.TreeList
    Friend WithEvents INDliStruct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDsbAddOrgStruct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
