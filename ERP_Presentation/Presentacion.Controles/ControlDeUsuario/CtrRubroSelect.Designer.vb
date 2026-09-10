<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrRubroSelect
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcRubro = New DevExpress.XtraGrid.GridControl()
        Me.INDGvRubro = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleVigence = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliVigence = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcRubro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvRubro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleVigence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliVigence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcRubro)
        Me.LayoutControl1.Controls.Add(Me.INDsleVigence)
        Me.LayoutControl1.Controls.Add(Me.INDsleEntity)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(620, 349)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcRubro
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcRubro, Nothing)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcRubro, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcRubro, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcRubro, False)
        Me.INDGcRubro.Location = New System.Drawing.Point(12, 48)
        Me.INDGcRubro.MainView = Me.INDGvRubro
        Me.INDGcRubro.Name = "INDGcRubro"
        Me.INDGcRubro.Size = New System.Drawing.Size(596, 289)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcRubro, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcRubro.TabIndex = 6
        Me.INDGcRubro.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvRubro})
        '
        'INDGvRubro
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDGvRubro.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvRubro.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvRubro.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvRubro.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvRubro.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvRubro.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvRubro.Appearance.Row.Options.UseFont = True
        Me.INDGvRubro.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvRubro.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvRubro.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.INDGvRubro.GridControl = Me.INDGcRubro
        Me.INDGvRubro.Name = "INDGvRubro"
        Me.INDGvRubro.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvRubro.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvRubro.OptionsView.ShowDetailButtons = False
        Me.INDGvRubro.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvRubro, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.OptionsColumn.AllowMove = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 301
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.OptionsColumn.AllowMove = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 630
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Recurso"
        Me.GridColumn3.FieldName = "FinancialSource.Name"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.OptionsColumn.AllowMove = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 268
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Tipo"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.OptionsColumn.AllowMove = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 212
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Saldo"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.OptionsColumn.AllowMove = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 221
        '
        'INDsleVigence
        '
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleVigence, False)
        Me.INDsleVigence.Location = New System.Drawing.Point(477, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleVigence, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleVigence.Name = "INDsleVigence"
        Me.INDsleVigence.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleVigence.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleVigence.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleVigence.Properties.Appearance.Options.UseFont = True
        Me.INDsleVigence.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleVigence.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleVigence.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleVigence.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleVigence.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleVigence.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleVigence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", 1, Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "Seleccionar", Nothing, Nothing, True)})
        Me.INDsleVigence.Properties.DisplayMember = "Year"
        Me.INDsleVigence.Properties.NullText = ""
        Me.INDsleVigence.Properties.ValueMember = "Id"
        Me.INDsleVigence.Properties.View = Me.SearchLookUpEdit2View
        Me.INDsleVigence.Size = New System.Drawing.Size(121, 28)
        Me.INDsleVigence.StyleController = Me.LayoutControl1
        Me.INDsleVigence.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleVigence, 0)
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Vigencia"
        Me.GridColumn8.FieldName = "Year"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'INDsleEntity
        '
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntity, False)
        Me.INDsleEntity.Location = New System.Drawing.Point(169, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntity.Name = "INDsleEntity"
        Me.INDsleEntity.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntity.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleEntity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleEntity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEntity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleEntity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleEntity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleEntity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleEntity.Properties.DisplayMember = "NameCode"
        Me.INDsleEntity.Properties.NullText = ""
        Me.INDsleEntity.Properties.ValueMember = "Id"
        Me.INDsleEntity.Properties.View = Me.SearchLookUpEdit1View
        Me.INDsleEntity.Size = New System.Drawing.Size(229, 28)
        Me.INDsleEntity.StyleController = Me.LayoutControl1
        Me.INDsleEntity.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntity, 0)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6, Me.GridColumn7})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Código"
        Me.GridColumn6.FieldName = "Code"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        Me.GridColumn6.Width = 309
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Nombre"
        Me.GridColumn7.FieldName = "Name"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        Me.GridColumn7.Width = 861
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliEntity, Me.INDliVigence, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(620, 349)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDliEntity
        '
        Me.INDliEntity.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDliEntity.AppearanceItemCaption.Options.UseFont = True
        Me.INDliEntity.Control = Me.INDsleEntity
        Me.INDliEntity.CustomizationFormText = "LayoutControlItem1"
        Me.INDliEntity.Location = New System.Drawing.Point(0, 0)
        Me.INDliEntity.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliEntity.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliEntity.Name = "INDliEntity"
        Me.INDliEntity.Size = New System.Drawing.Size(390, 36)
        Me.INDliEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliEntity.Text = "Entidad Presupuestal"
        Me.INDliEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliEntity.TextSize = New System.Drawing.Size(145, 13)
        Me.INDliEntity.TextToControlDistance = 12
        '
        'INDliVigence
        '
        Me.INDliVigence.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDliVigence.AppearanceItemCaption.Options.UseFont = True
        Me.INDliVigence.Control = Me.INDsleVigence
        Me.INDliVigence.CustomizationFormText = "LayoutControlItem2"
        Me.INDliVigence.Location = New System.Drawing.Point(390, 0)
        Me.INDliVigence.MaxSize = New System.Drawing.Size(200, 36)
        Me.INDliVigence.MinSize = New System.Drawing.Size(200, 36)
        Me.INDliVigence.Name = "INDliVigence"
        Me.INDliVigence.Size = New System.Drawing.Size(210, 36)
        Me.INDliVigence.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliVigence.Text = "Vigencia"
        Me.INDliVigence.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliVigence.TextSize = New System.Drawing.Size(70, 21)
        Me.INDliVigence.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDGcRubro
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(600, 293)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'CtrRubroSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrRubroSelect"
        Me.Size = New System.Drawing.Size(620, 349)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcRubro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvRubro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleVigence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliVigence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDsleVigence As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliVigence As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcRubro As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDGvRubro As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn

End Class
