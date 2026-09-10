Imports System.ComponentModel

<ToolboxItem(True)>
<Docking(DockingBehavior.Ask)>
Public Class SupplierSearch
    Inherits DevExpress.XtraEditors.SearchLookUpEdit

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

    Public Sub New()
        InitializeComponent()
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)

        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSupplier, System.ComponentModel.ISupportInitialize).BeginInit()

        Me.SuspendLayout()

        Me.Name = "INDSleSupplier"
        Me.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.Properties.Appearance.Options.UseFont = True
        Me.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Properties.DisplayMember = "CodeName"
        Me.Properties.NullText = ""
        Me.Properties.ValueMember = "Id"
        Me.Properties.View = Me.INDGvSupplier
        Me.Size = New System.Drawing.Size(576, 24)
        Me.TabIndex = 25

        Me.INDGvSupplier.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDGvSupplier.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSupplier.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSupplier.Appearance.Row.Options.UseFont = True
        Me.INDGvSupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCode, Me.INDColName})
        Me.INDGvSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvSupplier.Name = "INDGvSupplier"
        Me.INDGvSupplier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvSupplier.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSupplier.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSupplier.OptionsView.ShowGroupPanel = False

        Me.INDColCode.Caption = "Codigo"
        Me.INDColCode.FieldName = "Code"
        Me.INDColCode.MinWidth = 30
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 0

        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "Name"
        Me.INDColName.MinWidth = 30
        Me.INDColName.Name = "INDColName"
        Me.INDColName.OptionsColumn.AllowEdit = False
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 1

        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvSupplier As DevExpress.XtraGrid.Views.Grid.GridView

End Class
