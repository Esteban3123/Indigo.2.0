<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFolioTransferPopup
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
        Me.INDLcMain_Popup = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbTransfer = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleManagementArea_Popup = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GcManagementAreaPopup_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcManagementAreaPopup_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleTransferUser_Popup = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCManAreUsers_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCManAreUsers_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgMain_Popup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPopupArea = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPopupUser = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDLcMain_Popup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcMain_Popup.SuspendLayout()
        CType(Me.INDSleManagementArea_Popup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleTransferUser_Popup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMain_Popup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPopupArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPopupUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDLcMain_Popup
        '
        Me.INDLcMain_Popup.Controls.Add(Me.INDSbTransfer)
        Me.INDLcMain_Popup.Controls.Add(Me.INDSleManagementArea_Popup)
        Me.INDLcMain_Popup.Controls.Add(Me.INDSleTransferUser_Popup)
        Me.INDLcMain_Popup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcMain_Popup.Location = New System.Drawing.Point(0, 0)
        Me.INDLcMain_Popup.Name = "INDLcMain_Popup"
        Me.INDLcMain_Popup.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(767, 140, 764, 569)
        Me.INDLcMain_Popup.Root = Me.INDLcgMain_Popup
        Me.INDLcMain_Popup.Size = New System.Drawing.Size(388, 164)
        Me.INDLcMain_Popup.TabIndex = 0
        Me.INDLcMain_Popup.Text = "LayoutControl2"
        '
        'INDSbTransfer
        '
        Me.INDSbTransfer.Location = New System.Drawing.Point(10, 124)
        Me.INDSbTransfer.Margin = New System.Windows.Forms.Padding(0)
        Me.INDSbTransfer.MaximumSize = New System.Drawing.Size(367, 30)
        Me.INDSbTransfer.MinimumSize = New System.Drawing.Size(367, 30)
        Me.INDSbTransfer.Name = "INDSbTransfer"
        Me.INDSbTransfer.Size = New System.Drawing.Size(367, 30)
        Me.INDSbTransfer.StyleController = Me.INDLcMain_Popup
        Me.INDSbTransfer.TabIndex = 4
        Me.INDSbTransfer.Text = "Trasladar"
        '
        'INDSleManagementArea_Popup
        '
        Me.INDSleManagementArea_Popup.Location = New System.Drawing.Point(12, 32)
        Me.INDSleManagementArea_Popup.Name = "INDSleManagementArea_Popup"
        Me.INDSleManagementArea_Popup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleManagementArea_Popup.Properties.DisplayMember = "CodeName"
        Me.INDSleManagementArea_Popup.Properties.NullText = ""
        Me.INDSleManagementArea_Popup.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleManagementArea_Popup.Properties.ValueMember = "Id"
        Me.INDSleManagementArea_Popup.Size = New System.Drawing.Size(364, 20)
        Me.INDSleManagementArea_Popup.StyleController = Me.INDLcMain_Popup
        Me.INDSleManagementArea_Popup.TabIndex = 5
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GcManagementAreaPopup_Code, Me.GcManagementAreaPopup_Name})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GcManagementAreaPopup_Code
        '
        Me.GcManagementAreaPopup_Code.Caption = "Código"
        Me.GcManagementAreaPopup_Code.FieldName = "Code"
        Me.GcManagementAreaPopup_Code.Name = "GcManagementAreaPopup_Code"
        Me.GcManagementAreaPopup_Code.OptionsColumn.AllowEdit = False
        Me.GcManagementAreaPopup_Code.Visible = True
        Me.GcManagementAreaPopup_Code.VisibleIndex = 0
        '
        'GcManagementAreaPopup_Name
        '
        Me.GcManagementAreaPopup_Name.Caption = "Nombre"
        Me.GcManagementAreaPopup_Name.FieldName = "Name"
        Me.GcManagementAreaPopup_Name.Name = "GcManagementAreaPopup_Name"
        Me.GcManagementAreaPopup_Name.OptionsColumn.AllowEdit = False
        Me.GcManagementAreaPopup_Name.Visible = True
        Me.GcManagementAreaPopup_Name.VisibleIndex = 1
        '
        'INDSleTransferUser_Popup
        '
        Me.INDSleTransferUser_Popup.EditValue = ""
        Me.INDSleTransferUser_Popup.Location = New System.Drawing.Point(12, 76)
        Me.INDSleTransferUser_Popup.Name = "INDSleTransferUser_Popup"
        Me.INDSleTransferUser_Popup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleTransferUser_Popup.Properties.DisplayMember = "FullNameUser"
        Me.INDSleTransferUser_Popup.Properties.NullText = ""
        Me.INDSleTransferUser_Popup.Properties.PopupView = Me.GridView1
        Me.INDSleTransferUser_Popup.Properties.ValueMember = "Usercode"
        Me.INDSleTransferUser_Popup.Size = New System.Drawing.Size(364, 20)
        Me.INDSleTransferUser_Popup.StyleController = Me.INDLcMain_Popup
        Me.INDSleTransferUser_Popup.TabIndex = 6
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCManAreUsers_Code, Me.GCManAreUsers_Name})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GCManAreUsers_Code
        '
        Me.GCManAreUsers_Code.Caption = "Código"
        Me.GCManAreUsers_Code.FieldName = "Usercode"
        Me.GCManAreUsers_Code.Name = "GCManAreUsers_Code"
        Me.GCManAreUsers_Code.Visible = True
        Me.GCManAreUsers_Code.VisibleIndex = 0
        '
        'GCManAreUsers_Name
        '
        Me.GCManAreUsers_Name.Caption = "Nombre"
        Me.GCManAreUsers_Name.FieldName = "FullNameUser"
        Me.GCManAreUsers_Name.Name = "GCManAreUsers_Name"
        Me.GCManAreUsers_Name.Visible = True
        Me.GCManAreUsers_Name.VisibleIndex = 1
        '
        'INDLcgMain_Popup
        '
        Me.INDLcgMain_Popup.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain_Popup.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMain_Popup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain_Popup.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMain_Popup.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain_Popup.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMain_Popup.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMain_Popup.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMain_Popup.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain_Popup.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMain_Popup.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain_Popup.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMain_Popup.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain_Popup.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgMain_Popup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgMain_Popup.GroupBordersVisible = False
        Me.INDLcgMain_Popup.GroupStyle = DevExpress.Utils.GroupStyle.Title
        Me.INDLcgMain_Popup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.INDLciPopupArea, Me.INDLciPopupUser})
        Me.INDLcgMain_Popup.Name = "INDLcgMain_Popup"
        Me.INDLcgMain_Popup.Size = New System.Drawing.Size(388, 164)
        Me.INDLcgMain_Popup.Text = "Traslado"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.ContentVertAlignment = DevExpress.Utils.VertAlignment.Bottom
        Me.LayoutControlItem4.Control = Me.INDSbTransfer
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 88)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem4.Size = New System.Drawing.Size(368, 56)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'INDLciPopupArea
        '
        Me.INDLciPopupArea.AllowHide = False
        Me.INDLciPopupArea.Control = Me.INDSleManagementArea_Popup
        Me.INDLciPopupArea.Location = New System.Drawing.Point(0, 0)
        Me.INDLciPopupArea.Name = "INDLciPopupArea"
        Me.INDLciPopupArea.Size = New System.Drawing.Size(368, 44)
        Me.INDLciPopupArea.Text = "Area de gestión"
        Me.INDLciPopupArea.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPopupArea.TextSize = New System.Drawing.Size(102, 17)
        '
        'INDLciPopupUser
        '
        Me.INDLciPopupUser.AllowHide = False
        Me.INDLciPopupUser.Control = Me.INDSleTransferUser_Popup
        Me.INDLciPopupUser.Location = New System.Drawing.Point(0, 44)
        Me.INDLciPopupUser.Name = "INDLciPopupUser"
        Me.INDLciPopupUser.Size = New System.Drawing.Size(368, 44)
        Me.INDLciPopupUser.Text = "Usuario"
        Me.INDLciPopupUser.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPopupUser.TextSize = New System.Drawing.Size(102, 17)
        '
        'FrmFolioTransferPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(388, 164)
        Me.Controls.Add(Me.INDLcMain_Popup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(500, 250)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(385, 175)
        Me.Name = "FrmFolioTransferPopup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Traslados"
        CType(Me.INDLcMain_Popup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcMain_Popup.ResumeLayout(False)
        CType(Me.INDSleManagementArea_Popup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleTransferUser_Popup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMain_Popup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPopupArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPopupUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcMain_Popup As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSbTransfer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSleManagementArea_Popup As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GcManagementAreaPopup_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcManagementAreaPopup_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleTransferUser_Popup As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCManAreUsers_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCManAreUsers_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcgMain_Popup As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciPopupArea As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciPopupUser As DevExpress.XtraLayout.LayoutControlItem
End Class

