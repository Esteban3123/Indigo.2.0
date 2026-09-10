<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrSearchLookUpEditWithPopUp
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


    Private SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject
    Private SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrSearchLookUpEditWithPopUp))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.repIceAdmissionType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.repIceLiquidationType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.PcePopUpEdit = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.SleSearchLookUpEdit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SlevSearchLookUpEdit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColAdmissionCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColPatientCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColAdmissionType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColBedStay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColLiquidationType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColResponsibleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        CType(Me.repIceAdmissionType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repIceLiquidationType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PcePopUpEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SleSearchLookUpEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SlevSearchLookUpEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'repIceAdmissionType
        '
        Me.repIceAdmissionType.AutoHeight = False
        Me.repIceAdmissionType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ambulatorio", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Hospitalario", 2, -1)})
        Me.repIceAdmissionType.Name = "repIceAdmissionType"
        Me.repIceAdmissionType.ReadOnly = True
        '
        'repIceLiquidationType
        '
        Me.repIceLiquidationType.AutoHeight = False
        Me.repIceLiquidationType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Copago", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cuota Moderadora", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No Aplica", 3, -1)})
        Me.repIceLiquidationType.Name = "repIceLiquidationType"
        Me.repIceLiquidationType.ReadOnly = True
        '
        'PcePopUpEdit
        '
        Me.PcePopUpEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PcePopUpEdit.Location = New System.Drawing.Point(0, 0)
        Me.PcePopUpEdit.Name = "PcePopUpEdit"
        Me.PcePopUpEdit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.PcePopUpEdit.Properties.Appearance.Options.UseFont = True
        Me.PcePopUpEdit.Properties.PopupSizeable = False
        Me.PcePopUpEdit.Properties.ShowPopupCloseButton = False
        Me.PcePopUpEdit.Properties.ShowPopupShadow = False
        Me.PcePopUpEdit.Size = New System.Drawing.Size(70, 28)
        Me.PcePopUpEdit.TabIndex = 1
        '
        'SleSearchLookUpEdit
        '
        Me.SleSearchLookUpEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SleSearchLookUpEdit.Location = New System.Drawing.Point(0, 0)
        Me.SleSearchLookUpEdit.Margin = New System.Windows.Forms.Padding(0)
        Me.SleSearchLookUpEdit.Name = "SleSearchLookUpEdit"
        Me.SleSearchLookUpEdit.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.SleSearchLookUpEdit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SleSearchLookUpEdit.Properties.Appearance.Options.UseBackColor = True
        Me.SleSearchLookUpEdit.Properties.Appearance.Options.UseFont = True
        Me.SleSearchLookUpEdit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SleSearchLookUpEdit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SleSearchLookUpEdit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SleSearchLookUpEdit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.SleSearchLookUpEdit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.SleSearchLookUpEdit.Properties.AppearanceFocused.Options.UseFont = True
        Me.SleSearchLookUpEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("SleSearchLookUpEdit.Properties.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "ADD", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("SleSearchLookUpEdit.Properties.Buttons1"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "MORE_INFO", Nothing, True)})
        Me.SleSearchLookUpEdit.Properties.NullText = ""
        Me.SleSearchLookUpEdit.Properties.PopupFormMinSize = New System.Drawing.Size(1000, 300)
        Me.SleSearchLookUpEdit.Properties.PopupFormSize = New System.Drawing.Size(1000, 300)
        Me.SleSearchLookUpEdit.Properties.PopupSizeable = False
        Me.SleSearchLookUpEdit.Properties.ShowClearButton = False
        Me.SleSearchLookUpEdit.Properties.ShowFooter = False
        Me.SleSearchLookUpEdit.Properties.ShowPopupShadow = False
        Me.SleSearchLookUpEdit.Properties.View = Me.SlevSearchLookUpEdit
        Me.SleSearchLookUpEdit.Size = New System.Drawing.Size(70, 28)
        Me.SleSearchLookUpEdit.TabIndex = 0
        '
        'SlevSearchLookUpEdit
        '
        Me.SlevSearchLookUpEdit.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SlevSearchLookUpEdit.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SlevSearchLookUpEdit.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SlevSearchLookUpEdit.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SlevSearchLookUpEdit.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SlevSearchLookUpEdit.Appearance.Row.Options.UseFont = True
        Me.SlevSearchLookUpEdit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColAdmissionCode, Me.ColPatientCode, Me.ColPatientName, Me.ColAdmissionType, Me.ColBedStay, Me.ColLiquidationType, Me.ColResponsibleName, Me.ColStatus})
        Me.SlevSearchLookUpEdit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SlevSearchLookUpEdit.GroupCount = 1
        Me.SlevSearchLookUpEdit.Name = "SlevSearchLookUpEdit"
        Me.SlevSearchLookUpEdit.OptionsBehavior.AutoExpandAllGroups = True
        Me.SlevSearchLookUpEdit.OptionsFind.FindDelay = 1500
        Me.SlevSearchLookUpEdit.OptionsFind.FindFilterColumns = "AdmissionCode"
        Me.SlevSearchLookUpEdit.OptionsMenu.EnableColumnMenu = False
        Me.SlevSearchLookUpEdit.OptionsMenu.EnableFooterMenu = False
        Me.SlevSearchLookUpEdit.OptionsMenu.EnableGroupPanelMenu = False
        Me.SlevSearchLookUpEdit.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[False]
        Me.SlevSearchLookUpEdit.OptionsMenu.ShowAutoFilterRowItem = False
        Me.SlevSearchLookUpEdit.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.SlevSearchLookUpEdit.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.SlevSearchLookUpEdit.OptionsMenu.ShowSplitItem = False
        Me.SlevSearchLookUpEdit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SlevSearchLookUpEdit.OptionsView.EnableAppearanceEvenRow = True
        Me.SlevSearchLookUpEdit.OptionsView.EnableAppearanceOddRow = True
        Me.SlevSearchLookUpEdit.OptionsView.ShowAutoFilterRow = True
        Me.SlevSearchLookUpEdit.OptionsView.ShowGroupPanel = False
        Me.SlevSearchLookUpEdit.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.ColStatus, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SlevSearchLookUpEdit, False)
        '
        'ColAdmissionCode
        '
        Me.ColAdmissionCode.AppearanceHeader.Options.UseTextOptions = True
        Me.ColAdmissionCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColAdmissionCode.Caption = "No. Ingreso"
        Me.ColAdmissionCode.FieldName = "AdmissionCode"
        Me.ColAdmissionCode.MinWidth = 100
        Me.ColAdmissionCode.Name = "ColAdmissionCode"
        Me.ColAdmissionCode.OptionsColumn.AllowEdit = False
        Me.ColAdmissionCode.Visible = True
        Me.ColAdmissionCode.VisibleIndex = 0
        Me.ColAdmissionCode.Width = 121
        '
        'ColPatientCode
        '
        Me.ColPatientCode.AppearanceHeader.Options.UseTextOptions = True
        Me.ColPatientCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColPatientCode.Caption = "Identificación"
        Me.ColPatientCode.FieldName = "PatientCode"
        Me.ColPatientCode.MinWidth = 120
        Me.ColPatientCode.Name = "ColPatientCode"
        Me.ColPatientCode.OptionsColumn.AllowEdit = False
        Me.ColPatientCode.OptionsFilter.AllowFilter = False
        Me.ColPatientCode.Visible = True
        Me.ColPatientCode.VisibleIndex = 1
        Me.ColPatientCode.Width = 120
        '
        'ColPatientName
        '
        Me.ColPatientName.AppearanceHeader.Options.UseTextOptions = True
        Me.ColPatientName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColPatientName.Caption = "Paciente"
        Me.ColPatientName.FieldName = "PatientName"
        Me.ColPatientName.MinWidth = 100
        Me.ColPatientName.Name = "ColPatientName"
        Me.ColPatientName.OptionsColumn.AllowEdit = False
        Me.ColPatientName.OptionsFilter.AllowFilter = False
        Me.ColPatientName.Visible = True
        Me.ColPatientName.VisibleIndex = 2
        Me.ColPatientName.Width = 100
        '
        'ColAdmissionType
        '
        Me.ColAdmissionType.AppearanceHeader.Options.UseTextOptions = True
        Me.ColAdmissionType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColAdmissionType.Caption = "T. Ingreso"
        Me.ColAdmissionType.ColumnEdit = Me.repIceAdmissionType
        Me.ColAdmissionType.FieldName = "AdmissionType"
        Me.ColAdmissionType.MinWidth = 100
        Me.ColAdmissionType.Name = "ColAdmissionType"
        Me.ColAdmissionType.OptionsColumn.AllowEdit = False
        Me.ColAdmissionType.OptionsFilter.AllowAutoFilter = False
        Me.ColAdmissionType.OptionsFilter.AllowFilter = False
        Me.ColAdmissionType.Visible = True
        Me.ColAdmissionType.VisibleIndex = 3
        Me.ColAdmissionType.Width = 100
        '
        'ColBedStay
        '
        Me.ColBedStay.AppearanceHeader.Options.UseTextOptions = True
        Me.ColBedStay.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColBedStay.Caption = "Estancia (Cama)"
        Me.ColBedStay.FieldName = "BedStay"
        Me.ColBedStay.MinWidth = 120
        Me.ColBedStay.Name = "ColBedStay"
        Me.ColBedStay.OptionsColumn.AllowEdit = False
        Me.ColBedStay.OptionsFilter.AllowFilter = False
        Me.ColBedStay.Visible = True
        Me.ColBedStay.VisibleIndex = 4
        Me.ColBedStay.Width = 120
        '
        'ColLiquidationType
        '
        Me.ColLiquidationType.AppearanceHeader.Options.UseTextOptions = True
        Me.ColLiquidationType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLiquidationType.Caption = "T. Liquidación"
        Me.ColLiquidationType.ColumnEdit = Me.repIceLiquidationType
        Me.ColLiquidationType.FieldName = "LiquidationType"
        Me.ColLiquidationType.MinWidth = 120
        Me.ColLiquidationType.Name = "ColLiquidationType"
        Me.ColLiquidationType.OptionsColumn.AllowEdit = False
        Me.ColLiquidationType.OptionsFilter.AllowAutoFilter = False
        Me.ColLiquidationType.OptionsFilter.AllowFilter = False
        Me.ColLiquidationType.Visible = True
        Me.ColLiquidationType.VisibleIndex = 5
        Me.ColLiquidationType.Width = 120
        '
        'ColResponsibleName
        '
        Me.ColResponsibleName.AppearanceHeader.Options.UseTextOptions = True
        Me.ColResponsibleName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColResponsibleName.Caption = "Acudiente"
        Me.ColResponsibleName.FieldName = "ResponsibleName"
        Me.ColResponsibleName.MinWidth = 100
        Me.ColResponsibleName.Name = "ColResponsibleName"
        Me.ColResponsibleName.OptionsColumn.AllowEdit = False
        Me.ColResponsibleName.OptionsFilter.AllowAutoFilter = False
        Me.ColResponsibleName.OptionsFilter.AllowFilter = False
        Me.ColResponsibleName.Visible = True
        Me.ColResponsibleName.VisibleIndex = 6
        Me.ColResponsibleName.Width = 100
        '
        'ColStatus
        '
        Me.ColStatus.AppearanceHeader.Options.UseTextOptions = True
        Me.ColStatus.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColStatus.Caption = "Estado"
        Me.ColStatus.FieldName = "StatusName"
        Me.ColStatus.MinWidth = 100
        Me.ColStatus.Name = "ColStatus"
        Me.ColStatus.OptionsColumn.AllowEdit = False
        Me.ColStatus.OptionsFilter.AllowAutoFilter = False
        Me.ColStatus.OptionsFilter.AllowFilter = False
        Me.ColStatus.Visible = True
        Me.ColStatus.VisibleIndex = 7
        Me.ColStatus.Width = 100
        '
        'CtrSearchLookUpEditWithPopUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SleSearchLookUpEdit)
        Me.Controls.Add(Me.PcePopUpEdit)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(0, 28)
        Me.MinimumSize = New System.Drawing.Size(70, 28)
        Me.Name = "CtrSearchLookUpEditWithPopUp"
        Me.Size = New System.Drawing.Size(70, 28)
        CType(Me.repIceAdmissionType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repIceLiquidationType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PcePopUpEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SleSearchLookUpEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SlevSearchLookUpEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PcePopUpEdit As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents SleSearchLookUpEdit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SlevSearchLookUpEdit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColAdmissionCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColPatientCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColPatientName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColAdmissionType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColBedStay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColLiquidationType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColResponsibleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents repIceAdmissionType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents repIceLiquidationType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox

End Class
