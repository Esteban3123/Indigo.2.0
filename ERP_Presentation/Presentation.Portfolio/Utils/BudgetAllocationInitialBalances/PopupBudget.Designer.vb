<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopupBudget
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleBudget = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4179 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4180 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4181 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4182 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4183 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDSleBudget.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnOk)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 67)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(411, 36)
        Me.PanelControl1.TabIndex = 0
        '
        'INDBtnOk
        '
        Me.INDBtnOk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnOk.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnOk.Name = "INDBtnOk"
        Me.INDBtnOk.Size = New System.Drawing.Size(407, 32)
        Me.INDBtnOk.TabIndex = 0
        Me.INDBtnOk.Text = "Aceptar"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDSleBudget)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(411, 67)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDSleBudget
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleBudget, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleBudget, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleBudget, False)
        Me.INDSleBudget.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleBudget, False)
        Me.INDSleBudget.Location = New System.Drawing.Point(12, 20)
        Me.INDSleBudget.Name = "INDSleBudget"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleBudget, False)
        Me.INDSleBudget.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSleBudget.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleBudget.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleBudget.Properties.Appearance.Options.UseFont = True
        Me.INDSleBudget.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleBudget.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleBudget.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleBudget.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleBudget.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleBudget.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleBudget.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDSleBudget.Properties.DisplayMember = "CategoryId.NameCode"
        Me.INDSleBudget.Properties.NullText = ""
        Me.INDSleBudget.Properties.PopupFormMinSize = New System.Drawing.Size(700, 0)
        Me.INDSleBudget.Properties.PopupSizeable = False
        Me.INDSleBudget.Properties.ShowFooter = False
        Me.INDSleBudget.Properties.ValueMember = "Id"
        Me.INDSleBudget.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleBudget, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleBudget, True)
        Me.INDSleBudget.Size = New System.Drawing.Size(386, 28)
        Me.INDSleBudget.StyleController = Me.LayoutControl1
        Me.INDSleBudget.TabIndex = 35
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleBudget, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleBudget, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleBudget, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleBudget, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4179, Me.GridColumn4180, Me.GridColumn4181, Me.GridColumn4182, Me.GridColumn4183})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn4179
        '
        Me.GridColumn4179.Caption = "Rubro"
        Me.GridColumn4179.FieldName = "CategoryId.NameCode"
        Me.GridColumn4179.Name = "GridColumn4179"
        Me.GridColumn4179.Visible = True
        Me.GridColumn4179.VisibleIndex = 0
        '
        'GridColumn4180
        '
        Me.GridColumn4180.Caption = "Tipo de Ingreso"
        Me.GridColumn4180.FieldName = "RevenueTypeId.NameCode"
        Me.GridColumn4180.Name = "GridColumn4180"
        Me.GridColumn4180.Visible = True
        Me.GridColumn4180.VisibleIndex = 1
        '
        'GridColumn4181
        '
        Me.GridColumn4181.Caption = "Vigencia"
        Me.GridColumn4181.FieldName = "CategoryId.BudgetaryValidityId.Year"
        Me.GridColumn4181.Name = "GridColumn4181"
        Me.GridColumn4181.Visible = True
        Me.GridColumn4181.VisibleIndex = 2
        '
        'GridColumn4182
        '
        Me.GridColumn4182.Caption = "Entidad Presupuestal"
        Me.GridColumn4182.FieldName = "CategoryId.BudgetaryValidityId.BudgetaryEntityId.NameCode"
        Me.GridColumn4182.Name = "GridColumn4182"
        Me.GridColumn4182.Visible = True
        Me.GridColumn4182.VisibleIndex = 3
        '
        'GridColumn4183
        '
        Me.GridColumn4183.Caption = "Saldo"
        Me.GridColumn4183.DisplayFormat.FormatString = "c0"
        Me.GridColumn4183.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4183.FieldName = "Balance"
        Me.GridColumn4183.Name = "GridColumn4183"
        Me.GridColumn4183.Visible = True
        Me.GridColumn4183.VisibleIndex = 4
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(411, 67)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSleBudget
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 10, 2)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(391, 47)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'PopupBudget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 103)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PopupBudget"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Seleccione Presupuesto"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDSleBudget.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleBudget As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4179 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4180 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4181 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4182 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4183 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
End Class
