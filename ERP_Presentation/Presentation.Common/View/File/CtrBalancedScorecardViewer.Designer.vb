<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrBalancedScorecardViewer
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleBalancedScorecard = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvBalancedscorecard = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.DashboardViewer1 = New DevExpress.DashboardWin.DashboardViewer(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDsleBalancedScorecard.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvBalancedscorecard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DashboardViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.LayoutControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1143, 34)
        Me.PanelControl1.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDsleBalancedScorecard)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1143, 34)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDsleBalancedScorecard
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleBalancedScorecard, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleBalancedScorecard, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleBalancedScorecard, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.INDsleBalancedScorecard.Location = New System.Drawing.Point(225, 5)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleBalancedScorecard, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleBalancedScorecard.Name = "INDsleBalancedScorecard"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.INDsleBalancedScorecard.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleBalancedScorecard.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleBalancedScorecard.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.INDsleBalancedScorecard.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleBalancedScorecard.Properties.Appearance.Options.UseFont = True
        Me.INDsleBalancedScorecard.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleBalancedScorecard.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDsleBalancedScorecard.Properties.DisplayMember = "Name"
        Me.INDsleBalancedScorecard.Properties.NullText = ""
        Me.INDsleBalancedScorecard.Properties.PopupSizeable = False
        Me.INDsleBalancedScorecard.Properties.PopupView = Me.INDgvBalancedscorecard
        Me.INDsleBalancedScorecard.Properties.ShowFooter = False
        Me.INDsleBalancedScorecard.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleBalancedScorecard, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleBalancedScorecard, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleBalancedScorecard, True)
        Me.INDsleBalancedScorecard.Size = New System.Drawing.Size(325, 28)
        Me.INDsleBalancedScorecard.StyleController = Me.LayoutControl1
        Me.INDsleBalancedScorecard.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleBalancedScorecard, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleBalancedScorecard, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleBalancedScorecard, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleBalancedScorecard, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleBalancedScorecard, False)
        '
        'INDgvBalancedscorecard
        '
        Me.INDgvBalancedscorecard.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvBalancedscorecard.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvBalancedscorecard.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvBalancedscorecard.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvBalancedscorecard.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvBalancedscorecard.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvBalancedscorecard.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvBalancedscorecard.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvBalancedscorecard.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvBalancedscorecard.Appearance.Row.Options.UseFont = True
        Me.INDgvBalancedscorecard.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.INDgvBalancedscorecard.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvBalancedscorecard.Name = "INDgvBalancedscorecard"
        Me.INDgvBalancedscorecard.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvBalancedscorecard.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvBalancedscorecard.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvBalancedscorecard.OptionsView.ShowAutoFilterRow = True
        Me.INDgvBalancedscorecard.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvBalancedscorecard, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nombre"
        Me.GridColumn1.FieldName = "Name"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1143, 34)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDsleBalancedScorecard
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(550, 32)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(550, 32)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(20, 0, 5, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1143, 34)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Balanced Scorecard Item"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(200, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'DashboardViewer1
        '
        Me.DashboardViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DashboardViewer1.Location = New System.Drawing.Point(0, 34)
        Me.DashboardViewer1.Name = "DashboardViewer1"
        Me.DashboardViewer1.Size = New System.Drawing.Size(1143, 605)
        Me.DashboardViewer1.TabIndex = 1
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'CtrBalancedScorecardViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DashboardViewer1)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "CtrBalancedScorecardViewer"
        Me.Size = New System.Drawing.Size(1143, 639)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDsleBalancedScorecard.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvBalancedscorecard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DashboardViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents DashboardViewer1 As DevExpress.DashboardWin.DashboardViewer
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleBalancedScorecard As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvBalancedscorecard As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
