<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopupHomologation
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcHomologation = New DevExpress.XtraGrid.GridControl()
        Me.INDGvHomologation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGclState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcHomologation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvHomologation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AllowCustomization = False
        Me.LayoutControl1.Controls.Add(Me.INDBtnOk)
        Me.LayoutControl1.Controls.Add(Me.INDGcHomologation)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(370, 75, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(574, 303)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDBtnOk
        '
        Me.INDBtnOk.Location = New System.Drawing.Point(12, 259)
        Me.INDBtnOk.Name = "INDBtnOk"
        Me.INDBtnOk.Size = New System.Drawing.Size(550, 32)
        Me.INDBtnOk.StyleController = Me.LayoutControl1
        Me.INDBtnOk.TabIndex = 5
        Me.INDBtnOk.Text = "Aceptar"
        '
        'INDGcHomologation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcHomologation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcHomologation, Nothing)
        Me.INDGcHomologation.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcHomologation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcHomologation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcHomologation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcHomologation, False)
        Me.INDGcHomologation.Location = New System.Drawing.Point(12, 12)
        Me.INDGcHomologation.MainView = Me.INDGvHomologation
        Me.INDGcHomologation.Name = "INDGcHomologation"
        Me.INDGcHomologation.Size = New System.Drawing.Size(550, 243)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcHomologation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDGcHomologation, New System.Drawing.Size(554, 0))
        Me.INDGcHomologation.TabIndex = 4
        Me.INDGcHomologation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvHomologation})
        '
        'INDGvHomologation
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDGvHomologation.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvHomologation.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvHomologation.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvHomologation.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvHomologation.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvHomologation.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvHomologation.Appearance.Row.Options.UseFont = True
        Me.INDGvHomologation.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvHomologation.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvHomologation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGclState, Me.GridColumn2, Me.GridColumn1})
        Me.INDGvHomologation.GridControl = Me.INDGcHomologation
        Me.INDGvHomologation.Name = "INDGvHomologation"
        Me.INDGvHomologation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvHomologation.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvHomologation.OptionsView.ShowAutoFilterRow = True
        Me.INDGvHomologation.OptionsView.ShowDetailButtons = False
        Me.INDGvHomologation.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvHomologation, False)
        '
        'INDGclState
        '
        Me.INDGclState.Caption = " "
        Me.INDGclState.FieldName = "Activated"
        Me.INDGclState.Name = "INDGclState"
        Me.INDGclState.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.AllowMove = False
        Me.INDGclState.OptionsColumn.AllowShowHide = False
        Me.INDGclState.OptionsColumn.AllowSize = False
        Me.INDGclState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.FixedWidth = True
        Me.INDGclState.OptionsFilter.AllowAutoFilter = False
        Me.INDGclState.OptionsFilter.AllowFilter = False
        Me.INDGclState.Visible = True
        Me.INDGclState.VisibleIndex = 0
        Me.INDGclState.Width = 36
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "CUPS"
        Me.GridColumn2.FieldName = "CodeNameCupsEntity"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 677
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Servicio IPS"
        Me.GridColumn1.FieldName = "CodeNameIpsService"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 679
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
        Me.LayoutControlGroup1.CustomizationFormText = "Homologaciones"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(574, 303)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcHomologation
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(554, 247)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDBtnOk
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 247)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(83, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(554, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'PopupHomologation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 303)
        Me.ControlBox = False
        Me.Controls.Add(Me.LayoutControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PopupHomologation"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Homologación"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcHomologation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvHomologation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcHomologation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvHomologation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDGclState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDBtnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
