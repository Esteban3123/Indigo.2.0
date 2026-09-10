Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAssignResponsible
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAssignResponsible))
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDsleMaintenanceResponsible = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewMaintenanceResponsible = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDviewMaintenanceResponsible_Nit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewMaintenanceResponsible_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemMaintenanceResponsible = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleMaintenanceResponsible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewMaintenanceResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemMaintenanceResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(419, 163)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(419, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(419, 98)
        '
        'INDsleMaintenanceResponsible
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleMaintenanceResponsible, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleMaintenanceResponsible, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.INDsleMaintenanceResponsible.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleMaintenanceResponsible, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleMaintenanceResponsible.Name = "INDsleMaintenanceResponsible"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleMaintenanceResponsible, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.INDsleMaintenanceResponsible.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleMaintenanceResponsible.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleMaintenanceResponsible.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleMaintenanceResponsible.Properties.Appearance.Options.UseFont = True
        Me.INDsleMaintenanceResponsible.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleMaintenanceResponsible.Properties.DisplayMember = "ThirdPartyNitName"
        Me.INDsleMaintenanceResponsible.Properties.NullText = ""
        Me.INDsleMaintenanceResponsible.Properties.PopupSizeable = False
        Me.INDsleMaintenanceResponsible.Properties.PopupView = Me.INDviewMaintenanceResponsible
        Me.INDsleMaintenanceResponsible.Properties.ShowFooter = False
        Me.INDsleMaintenanceResponsible.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleMaintenanceResponsible, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleMaintenanceResponsible, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleMaintenanceResponsible, True)
        Me.INDsleMaintenanceResponsible.Size = New System.Drawing.Size(386, 28)
        Me.INDsleMaintenanceResponsible.StyleController = Me.INDlyRoot
        Me.INDsleMaintenanceResponsible.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleMaintenanceResponsible, "2210")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleMaintenanceResponsible, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleMaintenanceResponsible, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleMaintenanceResponsible, False)
        '
        'INDviewMaintenanceResponsible
        '
        Me.INDviewMaintenanceResponsible.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewMaintenanceResponsible.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewMaintenanceResponsible.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewMaintenanceResponsible.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewMaintenanceResponsible.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewMaintenanceResponsible.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewMaintenanceResponsible.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewMaintenanceResponsible.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewMaintenanceResponsible.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewMaintenanceResponsible.Appearance.Row.Options.UseFont = True
        Me.INDviewMaintenanceResponsible.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDviewMaintenanceResponsible_Nit, Me.INDviewMaintenanceResponsible_Name})
        Me.INDviewMaintenanceResponsible.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewMaintenanceResponsible.Name = "INDviewMaintenanceResponsible"
        Me.INDviewMaintenanceResponsible.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewMaintenanceResponsible.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewMaintenanceResponsible.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewMaintenanceResponsible.OptionsView.ShowAutoFilterRow = True
        Me.INDviewMaintenanceResponsible.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewMaintenanceResponsible, False)
        '
        'INDviewMaintenanceResponsible_Nit
        '
        Me.INDviewMaintenanceResponsible_Nit.Caption = "Nit"
        Me.INDviewMaintenanceResponsible_Nit.FieldName = "ThirdPartyNit"
        Me.INDviewMaintenanceResponsible_Nit.Name = "INDviewMaintenanceResponsible_Nit"
        Me.INDviewMaintenanceResponsible_Nit.OptionsColumn.AllowEdit = False
        Me.INDviewMaintenanceResponsible_Nit.OptionsColumn.AllowFocus = False
        Me.INDviewMaintenanceResponsible_Nit.Visible = True
        Me.INDviewMaintenanceResponsible_Nit.VisibleIndex = 0
        Me.INDviewMaintenanceResponsible_Nit.Width = 342
        '
        'INDviewMaintenanceResponsible_Name
        '
        Me.INDviewMaintenanceResponsible_Name.Caption = "Nombre"
        Me.INDviewMaintenanceResponsible_Name.FieldName = "ThirdPartyName"
        Me.INDviewMaintenanceResponsible_Name.Name = "INDviewMaintenanceResponsible_Name"
        Me.INDviewMaintenanceResponsible_Name.OptionsColumn.AllowEdit = False
        Me.INDviewMaintenanceResponsible_Name.OptionsColumn.AllowFocus = False
        Me.INDviewMaintenanceResponsible_Name.Visible = True
        Me.INDviewMaintenanceResponsible_Name.VisibleIndex = 1
        Me.INDviewMaintenanceResponsible_Name.Width = 1040
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDsleMaintenanceResponsible)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(415, 114)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemMaintenanceResponsible})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(415, 114)
        Me.Root.TextVisible = False
        '
        'INDlyItemMaintenanceResponsible
        '
        Me.INDlyItemMaintenanceResponsible.Control = Me.INDsleMaintenanceResponsible
        Me.INDlyItemMaintenanceResponsible.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemMaintenanceResponsible.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemMaintenanceResponsible.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemMaintenanceResponsible.Name = "INDlyItemMaintenanceResponsible"
        Me.INDlyItemMaintenanceResponsible.Size = New System.Drawing.Size(395, 94)
        Me.INDlyItemMaintenanceResponsible.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemMaintenanceResponsible.Text = "Responsables"
        Me.INDlyItemMaintenanceResponsible.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemMaintenanceResponsible.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemMaintenanceResponsible.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemMaintenanceResponsible.TextToControlDistance = 5
        '
        'INDbtnAccept
        '
        Me.INDbtnAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAccept.Appearance.Options.UseFont = True
        Me.INDbtnAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAccept.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAccept, True)
        Me.INDbtnAccept.Name = "INDbtnAccept"
        Me.INDbtnAccept.Size = New System.Drawing.Size(411, 36)
        Me.INDbtnAccept.TabIndex = 0
        Me.INDbtnAccept.Text = "Aceptar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAccept)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(2, 121)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(415, 40)
        Me.PanelControl1.TabIndex = 0
        '
        'FrmAssignResponsible
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(419, 285)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmAssignResponsible.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAssignResponsible"
        Me.Opacity = 1.0R
        Me.Text = "Asignación de Responsable"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleMaintenanceResponsible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewMaintenanceResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemMaintenanceResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleMaintenanceResponsible As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewMaintenanceResponsible As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemMaintenanceResponsible As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDviewMaintenanceResponsible_Nit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewMaintenanceResponsible_Name As DevExpress.XtraGrid.Columns.GridColumn
End Class
