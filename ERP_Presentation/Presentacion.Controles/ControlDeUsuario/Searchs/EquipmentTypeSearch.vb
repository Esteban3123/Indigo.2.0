Imports System.ComponentModel

<ToolboxItem(True)>
<Docking(DockingBehavior.Ask)>
Public Class EquipmentTypeSearch
    Inherits DevExpress.XtraEditors.TreeListLookUpEdit

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
        Me.INDColCode = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDColName = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDColInventoryType = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList()
        Me.TreeListLookUpEdit1TreeList = New DevExpress.XtraTreeList.TreeList()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)

        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()

        Me.SuspendLayout()
        Me.IndigoTextEdit1.SetApplyStyle(Me, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me, False)
        Me.EnterMoveNextControl = True
        Me.Location = New System.Drawing.Point(630, 49)
        Me.IndigoTextEdit1.SetMascara(Me, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.MaximumSize = New System.Drawing.Size(244, 0)
        Me.Name = "INDglEquipmentType"
        Me.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Properties.Appearance.Options.UseBackColor = True
        Me.Properties.Appearance.Options.UseFont = True
        Me.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.Properties.AppearanceFocused.Options.UseFont = True
        Me.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Properties.DisplayMember = "CodeName"
        Me.Properties.NullText = ""
        Me.Properties.PopupFormMinSize = New System.Drawing.Size(500, 0)
        Me.Properties.TreeList = Me.TreeListLookUpEdit1TreeList
        Me.Properties.ValueMember = "Id"
        Me.Size = New System.Drawing.Size(386, 28)
        Me.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me, 0)
        Me.ToolTip = "Este Campo es Necesario"

        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.INDColCode, Me.INDColName, Me.INDColInventoryType})
        Me.IndigoTreeList1.SetExpandedAllNodes(Me.TreeListLookUpEdit1TreeList, False)
        Me.TreeListLookUpEdit1TreeList.KeyFieldName = "Id"
        Me.TreeListLookUpEdit1TreeList.Location = New System.Drawing.Point(0, 0)
        Me.TreeListLookUpEdit1TreeList.Name = "TreeListLookUpEdit1TreeList"
        Me.TreeListLookUpEdit1TreeList.OptionsBehavior.EnableFiltering = True
        Me.TreeListLookUpEdit1TreeList.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceOddRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = True
        Me.TreeListLookUpEdit1TreeList.ParentFieldName = "PadreId"
        Me.TreeListLookUpEdit1TreeList.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.TreeListLookUpEdit1TreeList.Size = New System.Drawing.Size(400, 200)
        Me.TreeListLookUpEdit1TreeList.TabIndex = 0

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

        Me.INDColInventoryType.Caption = "Tipo de Inventario"
        Me.INDColInventoryType.FieldName = "InventoryTypeId.Name"
        Me.INDColInventoryType.MinWidth = 30
        Me.INDColInventoryType.Name = "INDColInventoryType"
        Me.INDColInventoryType.OptionsColumn.AllowEdit = False
        Me.INDColInventoryType.Visible = True
        Me.INDColInventoryType.VisibleIndex = 2

        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDColInventoryType As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColCode As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColName As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents TreeListLookUpEdit1TreeList As DevExpress.XtraTreeList.TreeList

End Class
