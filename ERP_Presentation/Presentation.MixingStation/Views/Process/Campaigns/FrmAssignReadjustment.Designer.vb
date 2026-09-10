Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAssignReadjustment
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDGcAssignReadjustment = New DevExpress.XtraGrid.GridControl()
        Me.INDGvAssingReadjustment = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColMedicineName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBatchCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColExpireDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUnitDoseType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAssignReadjustment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcAssignReadjustment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAssingReadjustment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAssignReadjustment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1044, 476)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1044, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1044, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDGcAssignReadjustment
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcAssignReadjustment, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcAssignReadjustment, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcAssignReadjustment, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcAssignReadjustment, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcAssignReadjustment, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcAssignReadjustment, False)
        Me.INDGcAssignReadjustment.Location = New System.Drawing.Point(12, 12)
        Me.INDGcAssignReadjustment.MainView = Me.INDGvAssingReadjustment
        Me.INDGcAssignReadjustment.Name = "INDGcAssignReadjustment"
        Me.INDGcAssignReadjustment.Size = New System.Drawing.Size(1016, 443)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcAssignReadjustment, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcAssignReadjustment.TabIndex = 4
        Me.INDGcAssignReadjustment.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvAssingReadjustment})
        '
        'INDGvAssingReadjustment
        '
        Me.INDGvAssingReadjustment.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvAssingReadjustment.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvAssingReadjustment.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvAssingReadjustment.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvAssingReadjustment.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAssingReadjustment.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvAssingReadjustment.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAssingReadjustment.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvAssingReadjustment.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvAssingReadjustment.Appearance.Row.Options.UseFont = True
        Me.INDGvAssingReadjustment.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvAssingReadjustment.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvAssingReadjustment.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColMedicineName, Me.INDColBatchCode, Me.INDColExpireDate, Me.INDColUnitDoseType})
        Me.INDGvAssingReadjustment.GridControl = Me.INDGcAssignReadjustment
        Me.INDGvAssingReadjustment.Name = "INDGvAssingReadjustment"
        Me.INDGvAssingReadjustment.OptionsSelection.MultiSelect = True
        Me.INDGvAssingReadjustment.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDGvAssingReadjustment.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvAssingReadjustment.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvAssingReadjustment.OptionsView.ShowAutoFilterRow = True
        Me.INDGvAssingReadjustment.OptionsView.ShowDetailButtons = False
        Me.INDGvAssingReadjustment.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvAssingReadjustment, False)
        '
        'INDColMedicineName
        '
        Me.INDColMedicineName.AppearanceCell.Options.UseTextOptions = True
        Me.INDColMedicineName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColMedicineName.Caption = "Nombre medicamento"
        Me.INDColMedicineName.FieldName = "ProductCodeName"
        Me.INDColMedicineName.Name = "INDColMedicineName"
        Me.INDColMedicineName.OptionsColumn.AllowEdit = False
        Me.INDColMedicineName.OptionsColumn.AllowFocus = False
        Me.INDColMedicineName.Visible = True
        Me.INDColMedicineName.VisibleIndex = 1
        Me.INDColMedicineName.Width = 300
        '
        'INDColBatchCode
        '
        Me.INDColBatchCode.Caption = "Lote"
        Me.INDColBatchCode.FieldName = "BatchCode"
        Me.INDColBatchCode.Name = "INDColBatchCode"
        Me.INDColBatchCode.OptionsColumn.AllowEdit = False
        Me.INDColBatchCode.OptionsColumn.AllowFocus = False
        Me.INDColBatchCode.Visible = True
        Me.INDColBatchCode.VisibleIndex = 2
        Me.INDColBatchCode.Width = 150
        '
        'INDColExpireDate
        '
        Me.INDColExpireDate.Caption = "Fecha vencimiento"
        Me.INDColExpireDate.DisplayFormat.FormatString = "d"
        Me.INDColExpireDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColExpireDate.FieldName = "ExpirationDate"
        Me.INDColExpireDate.Name = "INDColExpireDate"
        Me.INDColExpireDate.OptionsColumn.AllowEdit = False
        Me.INDColExpireDate.OptionsColumn.AllowFocus = False
        Me.INDColExpireDate.Visible = True
        Me.INDColExpireDate.VisibleIndex = 3
        Me.INDColExpireDate.Width = 200
        '
        'INDColUnitDoseType
        '
        Me.INDColUnitDoseType.Caption = "Tipo de dosis Unitaria"
        Me.INDColUnitDoseType.FieldName = "DosageDescription"
        Me.INDColUnitDoseType.Name = "INDColUnitDoseType"
        Me.INDColUnitDoseType.OptionsColumn.AllowEdit = False
        Me.INDColUnitDoseType.OptionsColumn.AllowFocus = False
        Me.INDColUnitDoseType.Visible = True
        Me.INDColUnitDoseType.VisibleIndex = 4
        Me.INDColUnitDoseType.Width = 200
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAssignReadjustment})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1040, 467)
        Me.Root.TextVisible = False
        '
        'INDLciAssignReadjustment
        '
        Me.INDLciAssignReadjustment.Control = Me.INDGcAssignReadjustment
        Me.INDLciAssignReadjustment.CustomizationFormText = "Rejilla Asignar readecuacion"
        Me.INDLciAssignReadjustment.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAssignReadjustment.Name = "INDLciAssignReadjustment"
        Me.INDLciAssignReadjustment.Size = New System.Drawing.Size(1020, 447)
        Me.INDLciAssignReadjustment.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAssignReadjustment.TextVisible = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDGcAssignReadjustment)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1040, 467)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'FrmAssignReadjustment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 611)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAssignReadjustment"
        Me.Opacity = 1.0R
        Me.Text = "Asignar readecuación"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcAssignReadjustment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAssingReadjustment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAssignReadjustment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcAssignReadjustment As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvAssingReadjustment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColMedicineName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColExpireDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciAssignReadjustment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColBatchCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColUnitDoseType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
