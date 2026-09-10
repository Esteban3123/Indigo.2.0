<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNationalSavingsFund
    Inherits Presentation.Controls.FormBase

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
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgleCompany = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColNitCompany = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCompanyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgleLastLiquidationDate = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.IndColDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrNationalSavingsFundFile = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemLastLiquidationDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCodeCompany = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgleCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleLastLiquidationDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrNationalSavingsFundFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLastLiquidationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCodeCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 98)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDgleLastLiquidationDate)
        Me.LayoutControl1.Controls.Add(Me.INDgleCompany)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(135, 387, 450, 521)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1004, 598)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDgleCompany
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleCompany, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleCompany, False)
        Me.INDgleCompany.Location = New System.Drawing.Point(201, 59)
        Me.INDgleCompany.Name = "INDgleCompany"
        Me.INDgleCompany.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgleCompany.Properties.Appearance.Options.UseFont = True
        Me.INDgleCompany.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleCompany.Properties.DisplayMember = "Name"
        Me.INDgleCompany.Properties.ImmediatePopup = True
        Me.INDgleCompany.Properties.NullText = ""
        Me.INDgleCompany.Properties.ValueMember = "Id"
        Me.INDgleCompany.Properties.View = Me.GridLookUpEdit1View
        Me.INDgleCompany.Size = New System.Drawing.Size(269, 28)
        Me.INDgleCompany.StyleController = Me.LayoutControl1
        Me.INDgleCompany.TabIndex = 4
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleCompany, Nothing)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColNitCompany, Me.INDColCompanyName})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'INDColNitCompany
        '
        Me.INDColNitCompany.Caption = "Nit"
        Me.INDColNitCompany.FieldName = "Nit"
        Me.INDColNitCompany.Name = "INDColNitCompany"
        Me.INDColNitCompany.Visible = True
        Me.INDColNitCompany.VisibleIndex = 0
        '
        'INDColCompanyName
        '
        Me.INDColCompanyName.Caption = "Nombre"
        Me.INDColCompanyName.FieldName = "Name"
        Me.INDColCompanyName.Name = "INDColCompanyName"
        Me.INDColCompanyName.Visible = True
        Me.INDColCompanyName.VisibleIndex = 1
        '
        'INDgleLastLiquidationDate
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleLastLiquidationDate, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleLastLiquidationDate, False)
        Me.INDgleLastLiquidationDate.Location = New System.Drawing.Point(201, 95)
        Me.INDgleLastLiquidationDate.Name = "INDgleLastLiquidationDate"
        Me.INDgleLastLiquidationDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgleLastLiquidationDate.Properties.Appearance.Options.UseFont = True
        Me.INDgleLastLiquidationDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleLastLiquidationDate.Properties.DisplayMember = "Date.Date"
        Me.INDgleLastLiquidationDate.Properties.ImmediatePopup = True
        Me.INDgleLastLiquidationDate.Properties.NullText = ""
        Me.INDgleLastLiquidationDate.Properties.ValueMember = "Date"
        Me.INDgleLastLiquidationDate.Properties.View = Me.GridLookUpEdit2View
        Me.INDgleLastLiquidationDate.Size = New System.Drawing.Size(269, 28)
        Me.INDgleLastLiquidationDate.StyleController = Me.LayoutControl1
        Me.INDgleLastLiquidationDate.TabIndex = 5
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleLastLiquidationDate, Nothing)
        '
        'GridLookUpEdit2View
        '
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.IndColDate})
        Me.GridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit2View.Name = "GridLookUpEdit2View"
        Me.GridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.GridLookUpEdit2View.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.IndColDate, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'IndColDate
        '
        Me.IndColDate.Caption = "Fechas"
        Me.IndColDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.IndColDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.IndColDate.FieldName = "Date"
        Me.IndColDate.Name = "IndColDate"
        Me.IndColDate.Visible = True
        Me.IndColDate.VisibleIndex = 0
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
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrNationalSavingsFundFile})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1004, 598)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrNationalSavingsFundFile
        '
        Me.INDlyGrNationalSavingsFundFile.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrNationalSavingsFundFile.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrNationalSavingsFundFile.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrNationalSavingsFundFile.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrNationalSavingsFundFile.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrNationalSavingsFundFile.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrNationalSavingsFundFile.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrNationalSavingsFundFile.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrNationalSavingsFundFile.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrNationalSavingsFundFile.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrNationalSavingsFundFile.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrNationalSavingsFundFile.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrNationalSavingsFundFile.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrNationalSavingsFundFile.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrNationalSavingsFundFile, False)
        Me.INDlyGrNationalSavingsFundFile.CustomizationFormText = "Generación Archivo Fondo Nacional del Ahorro"
        Me.INDlyGrNationalSavingsFundFile.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCodeCompany, Me.INDlyItemLastLiquidationDate})
        Me.INDlyGrNationalSavingsFundFile.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrNationalSavingsFundFile.Name = "INDlyGrNationalSavingsFundFile"
        Me.INDlyGrNationalSavingsFundFile.Size = New System.Drawing.Size(984, 578)
        Me.INDlyGrNationalSavingsFundFile.Text = "Archivo Fondo Nacional del Ahorro"
        '
        'INDlyItemLastLiquidationDate
        '
        Me.INDlyItemLastLiquidationDate.Control = Me.INDgleLastLiquidationDate
        Me.INDlyItemLastLiquidationDate.CustomizationFormText = "Fecha Liquidación"
        Me.INDlyItemLastLiquidationDate.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemLastLiquidationDate.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemLastLiquidationDate.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemLastLiquidationDate.Name = "INDlyItemLastLiquidationDate"
        Me.INDlyItemLastLiquidationDate.Size = New System.Drawing.Size(960, 483)
        Me.INDlyItemLastLiquidationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLastLiquidationDate.Text = "Fecha Liquidación"
        Me.INDlyItemLastLiquidationDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemLastLiquidationDate.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemLastLiquidationDate.TextToControlDistance = 12
        '
        'INDlyItemCodeCompany
        '
        Me.INDlyItemCodeCompany.Control = Me.INDgleCompany
        Me.INDlyItemCodeCompany.CustomizationFormText = "Empresa"
        Me.INDlyItemCodeCompany.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCodeCompany.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemCodeCompany.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemCodeCompany.Name = "INDlyItemCodeCompany"
        Me.INDlyItemCodeCompany.Size = New System.Drawing.Size(960, 36)
        Me.INDlyItemCodeCompany.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCodeCompany.Text = "Empresa"
        Me.INDlyItemCodeCompany.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCodeCompany.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemCodeCompany.TextToControlDistance = 12
        '
        'FrmNationalSavingsFund
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmNationalSavingsFund"
        Me.Opacity = 1.0R
        Me.Tag = "550"
        Me.Text = "Archivo Fondo Nacional del Ahorro"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgleCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleLastLiquidationDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrNationalSavingsFundFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLastLiquidationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCodeCompany, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgleLastLiquidationDate As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndColDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgleCompany As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColNitCompany As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCompanyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGrNationalSavingsFundFile As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCodeCompany As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemLastLiquidationDate As DevExpress.XtraLayout.LayoutControlItem
End Class
