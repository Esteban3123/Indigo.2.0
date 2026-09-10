Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAssignImportunityCause
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAssignImportunityCause))
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDSleImportunityCauseId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvImportunityCauseId = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvImportunityCauseId_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvImportunityCauseId_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciImportunityCauseId = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.INDSleImportunityCauseId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvImportunityCauseId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciImportunityCauseId, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.INDPanelControlBase.Size = New System.Drawing.Size(426, 161)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(426, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(426, 98)
        '
        'INDSleImportunityCauseId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleImportunityCauseId, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleImportunityCauseId, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleImportunityCauseId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.INDSleImportunityCauseId.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleImportunityCauseId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleImportunityCauseId.Name = "INDSleImportunityCauseId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleImportunityCauseId, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.INDSleImportunityCauseId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleImportunityCauseId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleImportunityCauseId.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleImportunityCauseId.Properties.Appearance.Options.UseFont = True
        Me.INDSleImportunityCauseId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleImportunityCauseId.Properties.DisplayMember = "CodeName"
        Me.INDSleImportunityCauseId.Properties.NullText = ""
        Me.INDSleImportunityCauseId.Properties.PopupSizeable = False
        Me.INDSleImportunityCauseId.Properties.PopupView = Me.INDGvImportunityCauseId
        Me.INDSleImportunityCauseId.Properties.ShowFooter = False
        Me.INDSleImportunityCauseId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleImportunityCauseId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleImportunityCauseId, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleImportunityCauseId, True)
        Me.INDSleImportunityCauseId.Size = New System.Drawing.Size(386, 28)
        Me.INDSleImportunityCauseId.StyleController = Me.INDlyRoot
        Me.INDSleImportunityCauseId.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleImportunityCauseId, "2135")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleImportunityCauseId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleImportunityCauseId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleImportunityCauseId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleImportunityCauseId, False)
        '
        'INDGvImportunityCauseId
        '
        Me.INDGvImportunityCauseId.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvImportunityCauseId.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvImportunityCauseId.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvImportunityCauseId.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvImportunityCauseId.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvImportunityCauseId.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvImportunityCauseId.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvImportunityCauseId.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvImportunityCauseId.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvImportunityCauseId.Appearance.Row.Options.UseFont = True
        Me.INDGvImportunityCauseId.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvImportunityCauseId_Code, Me.INDGvImportunityCauseId_Name})
        Me.INDGvImportunityCauseId.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvImportunityCauseId.Name = "INDGvImportunityCauseId"
        Me.INDGvImportunityCauseId.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvImportunityCauseId.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvImportunityCauseId.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvImportunityCauseId.OptionsView.ShowAutoFilterRow = True
        Me.INDGvImportunityCauseId.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvImportunityCauseId, False)
        '
        'INDGvImportunityCauseId_Code
        '
        Me.INDGvImportunityCauseId_Code.Caption = "Código"
        Me.INDGvImportunityCauseId_Code.FieldName = "Code"
        Me.INDGvImportunityCauseId_Code.Name = "INDGvImportunityCauseId_Code"
        Me.INDGvImportunityCauseId_Code.Visible = True
        Me.INDGvImportunityCauseId_Code.VisibleIndex = 0
        Me.INDGvImportunityCauseId_Code.Width = 342
        '
        'INDGvImportunityCauseId_Name
        '
        Me.INDGvImportunityCauseId_Name.Caption = "Nombre"
        Me.INDGvImportunityCauseId_Name.FieldName = "Name"
        Me.INDGvImportunityCauseId_Name.Name = "INDGvImportunityCauseId_Name"
        Me.INDGvImportunityCauseId_Name.Visible = True
        Me.INDGvImportunityCauseId_Name.VisibleIndex = 1
        Me.INDGvImportunityCauseId_Name.Width = 1040
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDSleImportunityCauseId)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(422, 112)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciImportunityCauseId})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(422, 112)
        Me.Root.TextVisible = False
        '
        'INDLciImportunityCauseId
        '
        Me.INDLciImportunityCauseId.Control = Me.INDSleImportunityCauseId
        Me.INDLciImportunityCauseId.Location = New System.Drawing.Point(0, 0)
        Me.INDLciImportunityCauseId.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciImportunityCauseId.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciImportunityCauseId.Name = "INDLciImportunityCauseId"
        Me.INDLciImportunityCauseId.Size = New System.Drawing.Size(402, 92)
        Me.INDLciImportunityCauseId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciImportunityCauseId.Text = "Causas de Inoportunidad"
        Me.INDLciImportunityCauseId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciImportunityCauseId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciImportunityCauseId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciImportunityCauseId.TextToControlDistance = 5
        '
        'INDbtnAccept
        '
        Me.INDbtnAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAccept.Appearance.Options.UseFont = True
        Me.INDbtnAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAccept.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAccept, True)
        Me.INDbtnAccept.Name = "INDbtnAccept"
        Me.INDbtnAccept.Size = New System.Drawing.Size(418, 36)
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
        Me.PanelControl1.Location = New System.Drawing.Point(2, 119)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(422, 40)
        Me.PanelControl1.TabIndex = 0
        '
        'FrmAssignImportunityCause
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 283)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmAssignImportunityCause.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAssignImportunityCause"
        Me.Opacity = 1.0R
        Me.Text = "Asignación de Causa de Inoportunidad"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleImportunityCauseId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvImportunityCauseId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciImportunityCauseId, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents INDSleImportunityCauseId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvImportunityCauseId As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciImportunityCauseId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvImportunityCauseId_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvImportunityCauseId_Name As DevExpress.XtraGrid.Columns.GridColumn
End Class
