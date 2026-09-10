<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrPharmacotherapeuticProfile
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
        Me.INDGcPharmacotherapeuticProfile = New DevExpress.XtraGrid.GridControl()
        Me.INDGvPharmacotherapeuticProfile = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDosage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDGcPharmacotherapeuticProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPharmacotherapeuticProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDGcPharmacotherapeuticProfile
        '
        Me.INDGcPharmacotherapeuticProfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDGcPharmacotherapeuticProfile.Location = New System.Drawing.Point(0, 0)
        Me.INDGcPharmacotherapeuticProfile.MainView = Me.INDGvPharmacotherapeuticProfile
        Me.INDGcPharmacotherapeuticProfile.Name = "INDGcPharmacotherapeuticProfile"
        Me.INDGcPharmacotherapeuticProfile.Size = New System.Drawing.Size(792, 350)
        Me.INDGcPharmacotherapeuticProfile.TabIndex = 0
        Me.INDGcPharmacotherapeuticProfile.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvPharmacotherapeuticProfile})
        '
        'INDGvPharmacotherapeuticProfile
        '
        Me.INDGvPharmacotherapeuticProfile.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProduct, Me.INDColDosage, Me.INDColQuantity})
        Me.INDGvPharmacotherapeuticProfile.GridControl = Me.INDGcPharmacotherapeuticProfile
        Me.INDGvPharmacotherapeuticProfile.Name = "INDGvPharmacotherapeuticProfile"
        Me.INDGvPharmacotherapeuticProfile.OptionsView.ShowDetailButtons = False
        Me.INDGvPharmacotherapeuticProfile.OptionsView.ShowGroupPanel = False
        '
        'INDColProduct
        '
        Me.INDColProduct.Caption = "Producto"
        Me.INDColProduct.Name = "INDColProduct"
        Me.INDColProduct.OptionsColumn.AllowEdit = False
        Me.INDColProduct.OptionsColumn.AllowFocus = False
        Me.INDColProduct.Visible = True
        Me.INDColProduct.VisibleIndex = 0
        Me.INDColProduct.Width = 691
        '
        'INDColDosage
        '
        Me.INDColDosage.Caption = "Dosis"
        Me.INDColDosage.Name = "INDColDosage"
        Me.INDColDosage.OptionsColumn.AllowEdit = False
        Me.INDColDosage.OptionsColumn.AllowFocus = False
        Me.INDColDosage.Visible = True
        Me.INDColDosage.VisibleIndex = 1
        Me.INDColDosage.Width = 286
        '
        'INDColQuantity
        '
        Me.INDColQuantity.Caption = "Cantidad"
        Me.INDColQuantity.Name = "INDColQuantity"
        Me.INDColQuantity.OptionsColumn.AllowEdit = False
        Me.INDColQuantity.OptionsColumn.AllowFocus = False
        Me.INDColQuantity.Visible = True
        Me.INDColQuantity.VisibleIndex = 2
        Me.INDColQuantity.Width = 287
        '
        'CtrPharmacotherapeuticProfile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDGcPharmacotherapeuticProfile)
        Me.Name = "CtrPharmacotherapeuticProfile"
        Me.Size = New System.Drawing.Size(792, 350)
        CType(Me.INDGcPharmacotherapeuticProfile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPharmacotherapeuticProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDGcPharmacotherapeuticProfile As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvPharmacotherapeuticProfile As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDosage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantity As DevExpress.XtraGrid.Columns.GridColumn
End Class
