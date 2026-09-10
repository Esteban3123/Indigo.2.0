<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupConceptLiquidationAdjusments
    Inherits DevExpress.XtraEditors.XtraForm

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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.INDSbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcDeliveryTime = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSeValue = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSleNature = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColNatureDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMeObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDLcgPccDeliveryTime = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciNature = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDSeIncome = New DevExpress.XtraEditors.SpinEdit()
        Me.INDLciIncome = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDLcDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcDeliveryTime.SuspendLayout()
        CType(Me.INDSeValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleNature.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPccDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeIncome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIncome, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDSbAdd
        '
        Me.INDSbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAdd.Appearance.Options.UseFont = True
        Me.INDSbAdd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDSbAdd.Location = New System.Drawing.Point(0, 286)
        Me.INDSbAdd.Name = "INDSbAdd"
        Me.INDSbAdd.Size = New System.Drawing.Size(484, 36)
        Me.INDSbAdd.TabIndex = 1
        Me.INDSbAdd.Text = "Agregar"
        '
        'INDLcDeliveryTime
        '
        Me.INDLcDeliveryTime.Controls.Add(Me.INDSeValue)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDSleNature)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDMeObservations)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDSeIncome)
        Me.INDLcDeliveryTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcDeliveryTime.Location = New System.Drawing.Point(0, 0)
        Me.INDLcDeliveryTime.Name = "INDLcDeliveryTime"
        Me.INDLcDeliveryTime.Root = Me.INDLcgPccDeliveryTime
        Me.INDLcDeliveryTime.Size = New System.Drawing.Size(484, 286)
        Me.INDLcDeliveryTime.TabIndex = 3
        Me.INDLcDeliveryTime.Text = "LayoutControl1"
        '
        'INDSeValue
        '
        Me.INDSeValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeValue.EnterMoveNextControl = True
        Me.INDSeValue.Location = New System.Drawing.Point(12, 154)
        Me.INDSeValue.Name = "INDSeValue"
        Me.INDSeValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeValue.Properties.Appearance.Options.UseFont = True
        Me.INDSeValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeValue.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeValue.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeValue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeValue.Properties.Mask.EditMask = "c0"
        Me.INDSeValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeValue.Properties.MaxLength = 18
        Me.INDSeValue.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDSeValue.Size = New System.Drawing.Size(460, 28)
        Me.INDSeValue.StyleController = Me.INDLcDeliveryTime
        Me.INDSeValue.TabIndex = 23
        '
        'INDSleNature
        '
        Me.INDSleNature.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSleNature.Location = New System.Drawing.Point(12, 96)
        Me.INDSleNature.Name = "INDSleNature"
        Me.INDSleNature.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleNature.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleNature.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleNature.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleNature.Properties.Appearance.Options.UseFont = True
        Me.INDSleNature.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleNature.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleNature.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleNature.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleNature.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleNature.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleNature.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleNature.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleNature.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleNature.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleNature.Properties.DisplayMember = "Item2"
        Me.INDSleNature.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.INDSleNature.Properties.MaxLength = 9
        Me.INDSleNature.Properties.NullText = ""
        Me.INDSleNature.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleNature.Properties.ValueMember = "Item1"
        Me.INDSleNature.Size = New System.Drawing.Size(460, 28)
        Me.INDSleNature.StyleController = Me.INDLcDeliveryTime
        Me.INDSleNature.TabIndex = 23
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColNatureDescription})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'INDColNatureDescription
        '
        Me.INDColNatureDescription.Caption = "Naturaleza"
        Me.INDColNatureDescription.FieldName = "Item2"
        Me.INDColNatureDescription.Name = "INDColNatureDescription"
        Me.INDColNatureDescription.OptionsColumn.AllowEdit = False
        Me.INDColNatureDescription.Visible = True
        Me.INDColNatureDescription.VisibleIndex = 0
        '
        'INDMeObservations
        '
        Me.INDMeObservations.Location = New System.Drawing.Point(12, 206)
        Me.INDMeObservations.Name = "INDMeObservations"
        Me.INDMeObservations.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDMeObservations.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDMeObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeObservations.Properties.Appearance.Options.UseFont = True
        Me.INDMeObservations.Properties.Appearance.Options.UseForeColor = True
        Me.INDMeObservations.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeObservations.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDMeObservations.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeObservations.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDMeObservations.Size = New System.Drawing.Size(460, 68)
        Me.INDMeObservations.StyleController = Me.INDLcDeliveryTime
        Me.INDMeObservations.TabIndex = 3
        '
        'INDLcgPccDeliveryTime
        '
        Me.INDLcgPccDeliveryTime.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPccDeliveryTime.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPccDeliveryTime.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgPccDeliveryTime.GroupBordersVisible = False
        Me.INDLcgPccDeliveryTime.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciNature, Me.INDLciValue, Me.INDLciObservations, Me.INDLciIncome})
        Me.INDLcgPccDeliveryTime.Name = "Root"
        Me.INDLcgPccDeliveryTime.Size = New System.Drawing.Size(484, 286)
        Me.INDLcgPccDeliveryTime.TextVisible = False
        '
        'INDLciNature
        '
        Me.INDLciNature.Control = Me.INDSleNature
        Me.INDLciNature.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciNature.CustomizationFormText = "Naturaleza"
        Me.INDLciNature.Location = New System.Drawing.Point(0, 58)
        Me.INDLciNature.Name = "INDLciNature"
        Me.INDLciNature.Size = New System.Drawing.Size(464, 58)
        Me.INDLciNature.Text = "Naturaleza"
        Me.INDLciNature.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNature.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNature.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciNature.TextToControlDistance = 5
        '
        'INDLciValue
        '
        Me.INDLciValue.Control = Me.INDSeValue
        Me.INDLciValue.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciValue.CustomizationFormText = "Valor"
        Me.INDLciValue.Location = New System.Drawing.Point(0, 116)
        Me.INDLciValue.Name = "INDLciValue"
        Me.INDLciValue.Size = New System.Drawing.Size(464, 58)
        Me.INDLciValue.Text = "Valor"
        Me.INDLciValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciValue.TextToControlDistance = 5
        '
        'INDLciObservations
        '
        Me.INDLciObservations.Control = Me.INDMeObservations
        Me.INDLciObservations.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciObservations.CustomizationFormText = "Observaciones"
        Me.INDLciObservations.Location = New System.Drawing.Point(0, 174)
        Me.INDLciObservations.Name = "INDLciObservations"
        Me.INDLciObservations.Size = New System.Drawing.Size(464, 92)
        Me.INDLciObservations.Text = "Observaciones"
        Me.INDLciObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservations.TextSize = New System.Drawing.Size(93, 17)
        '
        'INDSeIncome
        '
        Me.INDSeIncome.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeIncome.EnterMoveNextControl = True
        Me.INDSeIncome.Location = New System.Drawing.Point(12, 38)
        Me.INDSeIncome.Name = "INDSeIncome"
        Me.INDSeIncome.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeIncome.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeIncome.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeIncome.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeIncome.Properties.Appearance.Options.UseFont = True
        Me.INDSeIncome.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeIncome.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeIncome.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeIncome.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeIncome.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeIncome.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeIncome.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeIncome.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeIncome.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeIncome.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeIncome.Properties.Mask.EditMask = "c0"
        Me.INDSeIncome.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeIncome.Properties.MaxLength = 18
        Me.INDSeIncome.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDSeIncome.Size = New System.Drawing.Size(460, 28)
        Me.INDSeIncome.StyleController = Me.INDLcDeliveryTime
        Me.INDSeIncome.TabIndex = 23
        '
        'INDLciIncome
        '
        Me.INDLciIncome.Control = Me.INDSeIncome
        Me.INDLciIncome.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciIncome.CustomizationFormText = "Ingresos"
        Me.INDLciIncome.Location = New System.Drawing.Point(0, 0)
        Me.INDLciIncome.Name = "INDLciIncome"
        Me.INDLciIncome.Size = New System.Drawing.Size(464, 58)
        Me.INDLciIncome.Text = "Ingresos"
        Me.INDLciIncome.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciIncome.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciIncome.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciIncome.TextToControlDistance = 5
        '
        'FrmPopupConceptLiquidationAdjusments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 322)
        Me.Controls.Add(Me.INDLcDeliveryTime)
        Me.Controls.Add(Me.INDSbAdd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupConceptLiquidationAdjusments"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Ajustes de liquidación de renta para trabajadores"
        CType(Me.INDLcDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcDeliveryTime.ResumeLayout(False)
        CType(Me.INDSeValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleNature.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPccDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeIncome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIncome, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDSbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcDeliveryTime As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSeValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLcgPccDeliveryTime As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciNature As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleNature As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDMeObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColNatureDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSeIncome As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciIncome As DevExpress.XtraLayout.LayoutControlItem
End Class
