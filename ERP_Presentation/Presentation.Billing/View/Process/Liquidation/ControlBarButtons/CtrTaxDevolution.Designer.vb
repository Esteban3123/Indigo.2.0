<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrTaxDevolution
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcTaxDevolution = New DevExpress.XtraGrid.GridControl()
        Me.INDGvTaxDevolution = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColPaymentMethods = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgcTaxDevolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvTaxDevolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.BackColor = System.Drawing.Color.Transparent
        Me.LayoutControl1.Controls.Add(Me.INDgcTaxDevolution)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(-1220, 173, 650, 400)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(333, 110)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDgcTaxDevolution
        '
        Me.INDgcTaxDevolution.Location = New System.Drawing.Point(2, 2)
        Me.INDgcTaxDevolution.MainView = Me.INDGvTaxDevolution
        Me.INDgcTaxDevolution.Name = "INDgcTaxDevolution"
        Me.INDgcTaxDevolution.Size = New System.Drawing.Size(329, 106)
        Me.INDgcTaxDevolution.TabIndex = 5
        Me.INDgcTaxDevolution.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvTaxDevolution})
        '
        'INDGvTaxDevolution
        '
        Me.INDGvTaxDevolution.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColPaymentMethods, Me.INDColValue})
        Me.INDGvTaxDevolution.GridControl = Me.INDgcTaxDevolution
        Me.INDGvTaxDevolution.Name = "INDGvTaxDevolution"
        Me.INDGvTaxDevolution.OptionsSelection.UseIndicatorForSelection = False
        Me.INDGvTaxDevolution.OptionsView.AllowCellMerge = True
        Me.INDGvTaxDevolution.OptionsView.ShowGroupPanel = False
        '
        'INDColPaymentMethods
        '
        Me.INDColPaymentMethods.Caption = "Tipo de pago"
        Me.INDColPaymentMethods.FieldName = "PaymentMethodTypeName"
        Me.INDColPaymentMethods.Name = "INDColPaymentMethods"
        Me.INDColPaymentMethods.OptionsColumn.AllowEdit = False
        Me.INDColPaymentMethods.OptionsColumn.AllowFocus = False
        Me.INDColPaymentMethods.Visible = True
        Me.INDColPaymentMethods.VisibleIndex = 0
        '
        'INDColValue
        '
        Me.INDColValue.Caption = "Valor  de Recaudo"
        Me.INDColValue.DisplayFormat.FormatString = "c2"
        Me.INDColValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColValue.FieldName = "ValueWithTaxDevolution"
        Me.INDColValue.Name = "INDColValue"
        Me.INDColValue.OptionsColumn.AllowEdit = False
        Me.INDColValue.OptionsColumn.AllowFocus = False
        Me.INDColValue.Visible = True
        Me.INDColValue.VisibleIndex = 1
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(333, 110)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcTaxDevolution
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(333, 110)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'CtrTaxDevolution
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrTaxDevolution"
        Me.Size = New System.Drawing.Size(333, 110)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgcTaxDevolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvTaxDevolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcTaxDevolution As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvTaxDevolution As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColPaymentMethods As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColValue As DevExpress.XtraGrid.Columns.GridColumn
End Class
