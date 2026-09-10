<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRejectionPopup
    Inherits DevExpress.XtraEditors.XtraForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.INDLcRejection = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbAcceptRejection = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcgRejection = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDTeRejectionObservation = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleRejectionReason = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCCodeRejection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCNameRejection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLciRejection = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservation = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDLcRejection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRejection.SuspendLayout()
        CType(Me.INDLcgRejection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeRejectionObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleRejectionReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRejection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDLcRejection
        '
        Me.INDLcRejection.AutoScroll = False
        Me.INDLcRejection.AutoSize = True
        Me.INDLcRejection.Controls.Add(Me.INDSbAcceptRejection)
        Me.INDLcRejection.Controls.Add(Me.INDTeRejectionObservation)
        Me.INDLcRejection.Controls.Add(Me.INDSleRejectionReason)
        Me.INDLcRejection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRejection.Location = New System.Drawing.Point(0, 0)
        Me.INDLcRejection.Name = "INDLcRejection"
        Me.INDLcRejection.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(941, 155, 574, 569)
        Me.INDLcRejection.Root = Me.INDLcgRejection
        Me.INDLcRejection.Size = New System.Drawing.Size(343, 244)
        Me.INDLcRejection.TabIndex = 0
        Me.INDLcRejection.Text = "LayoutControl1"
        '
        'INDSbAcceptRejection
        '
        Me.INDSbAcceptRejection.Location = New System.Drawing.Point(9, 214)
        Me.INDSbAcceptRejection.Name = "INDSbAcceptRejection"
        Me.INDSbAcceptRejection.Size = New System.Drawing.Size(326, 22)
        Me.INDSbAcceptRejection.StyleController = Me.INDLcRejection
        Me.INDSbAcceptRejection.TabIndex = 6
        Me.INDSbAcceptRejection.Text = "Aceptar"
        '
        'INDLcgRejection
        '
        Me.INDLcgRejection.AllowHide = False
        Me.INDLcgRejection.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDLcgRejection.AppearanceGroup.Options.UseFont = True
        Me.INDLcgRejection.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgRejection.GroupBordersVisible = False
        Me.INDLcgRejection.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciRejection, Me.INDLciObservation, Me.LayoutControlItem1})
        Me.INDLcgRejection.Name = "Root"
        Me.INDLcgRejection.Size = New System.Drawing.Size(344, 244)
        Me.INDLcgRejection.Text = "Observaciones"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.ContentVertAlignment = DevExpress.Utils.VertAlignment.Bottom
        Me.LayoutControlItem1.Control = Me.INDSbAcceptRejection
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 163)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(326, 65)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDTeRejectionObservation
        '
        Me.INDTeRejectionObservation.Location = New System.Drawing.Point(11, 71)
        Me.INDTeRejectionObservation.MaximumSize = New System.Drawing.Size(322, 98)
        Me.INDTeRejectionObservation.MinimumSize = New System.Drawing.Size(322, 98)
        Me.INDTeRejectionObservation.Name = "INDTeRejectionObservation"
        Me.INDTeRejectionObservation.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTeRejectionObservation.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDTeRejectionObservation.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.INDTeRejectionObservation.Properties.AutoHeight = False
        Me.INDTeRejectionObservation.Properties.MaxLength = 500
        Me.INDTeRejectionObservation.Size = New System.Drawing.Size(322, 98)
        Me.INDTeRejectionObservation.StyleController = Me.INDLcRejection
        Me.INDTeRejectionObservation.TabIndex = 5
        '
        'INDSleRejectionReason
        '
        Me.INDSleRejectionReason.Location = New System.Drawing.Point(11, 28)
        Me.INDSleRejectionReason.Name = "INDSleRejectionReason"
        Me.INDSleRejectionReason.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleRejectionReason.Properties.DisplayMember = "Name"
        Me.INDSleRejectionReason.Properties.NullText = ""
        Me.INDSleRejectionReason.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleRejectionReason.Properties.ValueMember = "Id"
        Me.INDSleRejectionReason.Size = New System.Drawing.Size(322, 20)
        Me.INDSleRejectionReason.StyleController = Me.INDLcRejection
        Me.INDSleRejectionReason.TabIndex = 4
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCCodeRejection, Me.GCNameRejection})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GCCodeRejection
        '
        Me.GCCodeRejection.Caption = "Código"
        Me.GCCodeRejection.FieldName = "Code"
        Me.GCCodeRejection.Name = "GCCodeRejection"
        Me.GCCodeRejection.Visible = True
        Me.GCCodeRejection.VisibleIndex = 0
        '
        'GCNameRejection
        '
        Me.GCNameRejection.Caption = "Nombre"
        Me.GCNameRejection.FieldName = "Name"
        Me.GCNameRejection.Name = "GCNameRejection"
        Me.GCNameRejection.Visible = True
        Me.GCNameRejection.VisibleIndex = 1
        '
        'INDLciRejection
        '
        Me.INDLciRejection.Control = Me.INDSleRejectionReason
        Me.INDLciRejection.Location = New System.Drawing.Point(0, 0)
        Me.INDLciRejection.Name = "INDLciRejection"
        Me.INDLciRejection.Size = New System.Drawing.Size(326, 42)
        Me.INDLciRejection.Text = "Rechazo"
        Me.INDLciRejection.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDLciRejection.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRejection.TextSize = New System.Drawing.Size(41, 13)
        Me.INDLciRejection.TextToControlDistance = 5
        '
        'INDLciObservation
        '
        Me.INDLciObservation.AllowHide = False
        Me.INDLciObservation.Control = Me.INDTeRejectionObservation
        Me.INDLciObservation.Location = New System.Drawing.Point(0, 42)
        Me.INDLciObservation.Name = "INDLciObservation"
        Me.INDLciObservation.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 3, 2)
        Me.INDLciObservation.Size = New System.Drawing.Size(326, 121)
        Me.INDLciObservation.Text = "Observación Rechazo"
        Me.INDLciObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDLciObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservation.TextSize = New System.Drawing.Size(104, 13)
        Me.INDLciObservation.TextToControlDistance = 5
        '
        'FrmRejectionPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 244)
        Me.Controls.Add(Me.INDLcRejection)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(600, 500)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(345, 276)
        Me.Name = "FrmRejectionPopup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Observaciones"
        CType(Me.INDLcRejection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRejection.ResumeLayout(False)
        CType(Me.INDLcgRejection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeRejectionObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleRejectionReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRejection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDLcRejection As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSbAcceptRejection As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDTeRejectionObservation As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSleRejectionReason As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgRejection As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciRejection As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GCCodeRejection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCNameRejection As DevExpress.XtraGrid.Columns.GridColumn
End Class

