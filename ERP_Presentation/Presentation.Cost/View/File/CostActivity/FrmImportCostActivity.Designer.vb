<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmImportCostActivity
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmImportCostActivity))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcImportCostActivity = New DevExpress.XtraGrid.GridControl()
        Me.INDGvImportCostActivity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolImportCostActivityCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolImportCostActivityName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolImportCostActivityCUPSEntity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcImportCostActivity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvImportCostActivity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcImportCostActivity)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(784, 550)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcImportCostActivity
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcImportCostActivity, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcImportCostActivity, Nothing)
        Me.INDGcImportCostActivity.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcImportCostActivity, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcImportCostActivity, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcImportCostActivity, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcImportCostActivity, False)
        Me.INDGcImportCostActivity.Location = New System.Drawing.Point(14, 14)
        Me.INDGcImportCostActivity.MainView = Me.INDGvImportCostActivity
        Me.INDGcImportCostActivity.Name = "INDGcImportCostActivity"
        Me.INDGcImportCostActivity.Size = New System.Drawing.Size(756, 522)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcImportCostActivity, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcImportCostActivity.TabIndex = 5
        Me.INDGcImportCostActivity.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvImportCostActivity})
        '
        'INDGvImportCostActivity
        '
        Me.INDGvImportCostActivity.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvImportCostActivity.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvImportCostActivity.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvImportCostActivity.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvImportCostActivity.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvImportCostActivity.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvImportCostActivity.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvImportCostActivity.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvImportCostActivity.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvImportCostActivity.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvImportCostActivity.Appearance.Row.Options.UseFont = True
        Me.INDGvImportCostActivity.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvImportCostActivity.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvImportCostActivity.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolImportCostActivityCode, Me.INDcolImportCostActivityName, Me.INDcolImportCostActivityCUPSEntity})
        Me.INDGvImportCostActivity.GridControl = Me.INDGcImportCostActivity
        Me.INDGvImportCostActivity.Name = "INDGvImportCostActivity"
        Me.INDGvImportCostActivity.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvImportCostActivity.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvImportCostActivity.OptionsView.ShowAutoFilterRow = True
        Me.INDGvImportCostActivity.OptionsView.ShowDetailButtons = False
        Me.INDGvImportCostActivity.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvImportCostActivity, False)
        '
        'INDcolImportCostActivityCode
        '
        Me.INDcolImportCostActivityCode.Caption = "Código"
        Me.INDcolImportCostActivityCode.FieldName = "Code"
        Me.INDcolImportCostActivityCode.Name = "INDcolImportCostActivityCode"
        Me.INDcolImportCostActivityCode.Visible = True
        Me.INDcolImportCostActivityCode.VisibleIndex = 0
        Me.INDcolImportCostActivityCode.Width = 118
        '
        'INDcolImportCostActivityName
        '
        Me.INDcolImportCostActivityName.Caption = "Nombre"
        Me.INDcolImportCostActivityName.FieldName = "Name"
        Me.INDcolImportCostActivityName.Name = "INDcolImportCostActivityName"
        Me.INDcolImportCostActivityName.Visible = True
        Me.INDcolImportCostActivityName.VisibleIndex = 1
        Me.INDcolImportCostActivityName.Width = 310
        '
        'INDcolImportCostActivityCUPSEntity
        '
        Me.INDcolImportCostActivityCUPSEntity.Caption = "Servicio"
        Me.INDcolImportCostActivityCUPSEntity.FieldName = "CUPSEntityId.CodeDescription"
        Me.INDcolImportCostActivityCUPSEntity.Name = "INDcolImportCostActivityCUPSEntity"
        Me.INDcolImportCostActivityCUPSEntity.Visible = True
        Me.INDcolImportCostActivityCUPSEntity.VisibleIndex = 2
        Me.INDcolImportCostActivityCUPSEntity.Width = 310
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(784, 550)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(784, 550)
        Me.LayoutControlGroup2.Text = "Importar Información"
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDGcImportCostActivity
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(760, 526)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(780, 32)
        Me.INDBtnAdd.TabIndex = 6
        Me.INDBtnAdd.Text = "Aceptar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAdd)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 550)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(784, 36)
        Me.PanelControl1.TabIndex = 1
        '
        'FrmImportCostActivity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 586)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmImportCostActivity.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmImportCostActivity"
        Me.ShowInTaskbar = False
        Me.Text = "Importar Actividad"
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.INDGcImportCostActivity,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvImportCostActivity,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PanelControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelControl1.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDGcImportCostActivity As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvImportCostActivity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDcolImportCostActivityCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolImportCostActivityName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolImportCostActivityCUPSEntity As DevExpress.XtraGrid.Columns.GridColumn
End Class
