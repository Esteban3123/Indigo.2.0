<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDispensingConfirmation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDispensingConfirmation))
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnAcept = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcDispensing = New DevExpress.XtraGrid.GridControl()
        Me.GvDispensing = New DevExpress.XtraGrid.Views.Card.CardView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptMeDescription = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcDispensing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvDispensing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptMeDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.BtnAcept)
        Me.PanelControl1.Controls.Add(Me.BtnCancel)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 288)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(791, 37)
        Me.PanelControl1.TabIndex = 0
        '
        'BtnAcept
        '
        Me.BtnAcept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.5!)
        Me.BtnAcept.Appearance.Options.UseFont = True
        Me.BtnAcept.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnAcept.Location = New System.Drawing.Point(0, 0)
        Me.BtnAcept.Name = "BtnAcept"
        Me.BtnAcept.Size = New System.Drawing.Size(394, 37)
        Me.BtnAcept.TabIndex = 1
        Me.BtnAcept.Text = "Si"
        '
        'BtnCancel
        '
        Me.BtnCancel.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.5!)
        Me.BtnCancel.Appearance.Options.UseFont = True
        Me.BtnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnCancel.Location = New System.Drawing.Point(394, 0)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(397, 37)
        Me.BtnCancel.TabIndex = 0
        Me.BtnCancel.Text = "No"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 14.0!)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(213, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(365, 25)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "¿Desea Continuar con la liquidación del Folio?"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcDispensing)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(791, 252)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcDispensing
        '
        Me.INDGcDispensing.Location = New System.Drawing.Point(12, 12)
        Me.INDGcDispensing.MainView = Me.GvDispensing
        Me.INDGcDispensing.Name = "INDGcDispensing"
        Me.INDGcDispensing.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptMeDescription})
        Me.INDGcDispensing.Size = New System.Drawing.Size(767, 228)
        Me.INDGcDispensing.TabIndex = 0
        Me.INDGcDispensing.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GvDispensing})
        '
        'GvDispensing
        '
        Me.GvDispensing.Appearance.CardCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.GvDispensing.Appearance.CardCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GvDispensing.Appearance.CardCaption.Options.UseFont = True
        Me.GvDispensing.Appearance.CardCaption.Options.UseForeColor = True
        Me.GvDispensing.Appearance.FieldCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GvDispensing.Appearance.FieldCaption.Options.UseFont = True
        Me.GvDispensing.Appearance.FieldValue.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.GvDispensing.Appearance.FieldValue.Options.UseFont = True
        Me.GvDispensing.Appearance.FocusedCardCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.GvDispensing.Appearance.FocusedCardCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GvDispensing.Appearance.FocusedCardCaption.Options.UseFont = True
        Me.GvDispensing.Appearance.FocusedCardCaption.Options.UseForeColor = True
        Me.GvDispensing.Appearance.HideSelectionCardCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.GvDispensing.Appearance.HideSelectionCardCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GvDispensing.Appearance.HideSelectionCardCaption.Options.UseFont = True
        Me.GvDispensing.Appearance.HideSelectionCardCaption.Options.UseForeColor = True
        Me.GvDispensing.Appearance.SelectedCardCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.GvDispensing.Appearance.SelectedCardCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GvDispensing.Appearance.SelectedCardCaption.Options.UseFont = True
        Me.GvDispensing.Appearance.SelectedCardCaption.Options.UseForeColor = True
        Me.GvDispensing.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 14.25!)
        Me.GvDispensing.Appearance.ViewCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GvDispensing.Appearance.ViewCaption.Options.UseFont = True
        Me.GvDispensing.Appearance.ViewCaption.Options.UseForeColor = True
        Me.GvDispensing.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.GvDispensing.CardWidth = 368
        Me.GvDispensing.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.GvDispensing.GridControl = Me.INDGcDispensing
        Me.GvDispensing.MaximumCardColumns = 2
        Me.GvDispensing.MaximumCardRows = 1
        Me.GvDispensing.Name = "GvDispensing"
        Me.GvDispensing.OptionsBehavior.Editable = False
        Me.GvDispensing.OptionsBehavior.FieldAutoHeight = True
        Me.GvDispensing.OptionsView.ShowCardExpandButton = False
        Me.GvDispensing.OptionsView.ShowHorzScrollBar = False
        Me.GvDispensing.OptionsView.ShowQuickCustomizeButton = False
        Me.GvDispensing.OptionsView.ShowViewCaption = True
        Me.GvDispensing.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.[Auto]
        Me.GvDispensing.ViewCaption = "Documentos sin Confirmar"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Códigos"
        Me.GridColumn1.ColumnEdit = Me.INDRptMeDescription
        Me.GridColumn1.FieldName = "DocumentCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDRptMeDescription
        '
        Me.INDRptMeDescription.Name = "INDRptMeDescription"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(791, 252)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcDispensing
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.ShowInCustomizationForm = False
        Me.LayoutControlItem1.Size = New System.Drawing.Size(771, 232)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.LabelControl1)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 252)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(791, 36)
        Me.PanelControl2.TabIndex = 2
        '
        'FrmDispensingConfirmation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(791, 325)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmDispensingConfirmation.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDispensingConfirmation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Confirmación"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcDispensing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvDispensing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptMeDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnAcept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDGcDispensing As DevExpress.XtraGrid.GridControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GvDispensing As DevExpress.XtraGrid.Views.Card.CardView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptMeDescription As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
End Class
