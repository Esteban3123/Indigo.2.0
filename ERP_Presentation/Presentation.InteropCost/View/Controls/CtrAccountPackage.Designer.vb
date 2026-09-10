<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrAccountPackage
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
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDsleMainAccountDestination = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvMainAccountDestination = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleMainAccountOrigin = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvMainAccountOrigin = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliMainAccountOrigin = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliMainAccountDestination = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleMainAccountDestination.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvMainAccountDestination, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleMainAccountOrigin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvMainAccountOrigin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliMainAccountOrigin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliMainAccountDestination, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDsbAdd)
        Me.LayoutControl1.Controls.Add(Me.INDmeDescription)
        Me.LayoutControl1.Controls.Add(Me.INDsleMainAccountDestination)
        Me.LayoutControl1.Controls.Add(Me.INDsleMainAccountOrigin)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(447, 283)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDsbAdd
        '
        Me.INDsbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAdd.Appearance.Options.UseFont = True
        Me.INDsbAdd.Location = New System.Drawing.Point(12, 236)
        Me.INDsbAdd.Name = "INDsbAdd"
        Me.INDsbAdd.Size = New System.Drawing.Size(386, 32)
        Me.INDsbAdd.StyleController = Me.LayoutControl1
        Me.INDsbAdd.TabIndex = 7
        Me.INDsbAdd.Text = "Agregar"
        '
        'INDmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDescription, False)
        Me.INDmeDescription.EnterMoveNextControl = True
        Me.INDmeDescription.Location = New System.Drawing.Point(12, 162)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDescription.Name = "INDmeDescription"
        Me.INDmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmeDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeDescription.Size = New System.Drawing.Size(386, 70)
        Me.INDmeDescription.StyleController = Me.LayoutControl1
        Me.INDmeDescription.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDescription, 0)
        '
        'INDsleMainAccountDestination
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleMainAccountDestination, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleMainAccountDestination, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleMainAccountDestination, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.INDsleMainAccountDestination.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.INDsleMainAccountDestination.Location = New System.Drawing.Point(12, 100)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleMainAccountDestination, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleMainAccountDestination.Name = "INDsleMainAccountDestination"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.INDsleMainAccountDestination.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleMainAccountDestination.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleMainAccountDestination.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleMainAccountDestination.Properties.Appearance.Options.UseFont = True
        Me.INDsleMainAccountDestination.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleMainAccountDestination.Properties.DisplayMember = "CodeName"
        Me.INDsleMainAccountDestination.Properties.NullText = ""
        Me.INDsleMainAccountDestination.Properties.PopupSizeable = False
        Me.INDsleMainAccountDestination.Properties.ShowFooter = False
        Me.INDsleMainAccountDestination.Properties.ValueMember = "OID"
        Me.INDsleMainAccountDestination.Properties.View = Me.INDgvMainAccountDestination
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleMainAccountDestination, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleMainAccountDestination, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleMainAccountDestination, True)
        Me.INDsleMainAccountDestination.Size = New System.Drawing.Size(386, 28)
        Me.INDsleMainAccountDestination.StyleController = Me.LayoutControl1
        Me.INDsleMainAccountDestination.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleMainAccountDestination, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleMainAccountDestination, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleMainAccountDestination, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleMainAccountDestination, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleMainAccountDestination, False)
        '
        'INDgvMainAccountDestination
        '
        Me.INDgvMainAccountDestination.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvMainAccountDestination.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvMainAccountDestination.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvMainAccountDestination.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvMainAccountDestination.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvMainAccountDestination.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvMainAccountDestination.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvMainAccountDestination.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvMainAccountDestination.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvMainAccountDestination.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvMainAccountDestination.Appearance.Row.Options.UseFont = True
        Me.INDgvMainAccountDestination.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.INDgvMainAccountDestination.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvMainAccountDestination.Name = "INDgvMainAccountDestination"
        Me.INDgvMainAccountDestination.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvMainAccountDestination.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvMainAccountDestination.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvMainAccountDestination.OptionsView.ShowAutoFilterRow = True
        Me.INDgvMainAccountDestination.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvMainAccountDestination, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "CUECODIGO"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 383
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre"
        Me.GridColumn4.FieldName = "CUENOMBRE"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 826
        '
        'INDsleMainAccountOrigin
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleMainAccountOrigin, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleMainAccountOrigin, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleMainAccountOrigin, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.INDsleMainAccountOrigin.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.INDsleMainAccountOrigin.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleMainAccountOrigin, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleMainAccountOrigin.Name = "INDsleMainAccountOrigin"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.INDsleMainAccountOrigin.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleMainAccountOrigin.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleMainAccountOrigin.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleMainAccountOrigin.Properties.Appearance.Options.UseFont = True
        Me.INDsleMainAccountOrigin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleMainAccountOrigin.Properties.DisplayMember = "CodeName"
        Me.INDsleMainAccountOrigin.Properties.NullText = ""
        Me.INDsleMainAccountOrigin.Properties.PopupSizeable = False
        Me.INDsleMainAccountOrigin.Properties.ShowFooter = False
        Me.INDsleMainAccountOrigin.Properties.ValueMember = "OID"
        Me.INDsleMainAccountOrigin.Properties.View = Me.INDgvMainAccountOrigin
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleMainAccountOrigin, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleMainAccountOrigin, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleMainAccountOrigin, True)
        Me.INDsleMainAccountOrigin.Size = New System.Drawing.Size(386, 28)
        Me.INDsleMainAccountOrigin.StyleController = Me.LayoutControl1
        Me.INDsleMainAccountOrigin.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleMainAccountOrigin, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleMainAccountOrigin, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleMainAccountOrigin, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleMainAccountOrigin, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleMainAccountOrigin, False)
        '
        'INDgvMainAccountOrigin
        '
        Me.INDgvMainAccountOrigin.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvMainAccountOrigin.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvMainAccountOrigin.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvMainAccountOrigin.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvMainAccountOrigin.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvMainAccountOrigin.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvMainAccountOrigin.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvMainAccountOrigin.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvMainAccountOrigin.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvMainAccountOrigin.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvMainAccountOrigin.Appearance.Row.Options.UseFont = True
        Me.INDgvMainAccountOrigin.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.INDgvMainAccountOrigin.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvMainAccountOrigin.Name = "INDgvMainAccountOrigin"
        Me.INDgvMainAccountOrigin.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvMainAccountOrigin.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvMainAccountOrigin.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvMainAccountOrigin.OptionsView.ShowAutoFilterRow = True
        Me.INDgvMainAccountOrigin.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvMainAccountOrigin, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "CUECODIGO"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 218
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "CUENOMBRE"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 338
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliMainAccountOrigin, Me.INDliMainAccountDestination, Me.INDliDescription, Me.INDliAdd})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(447, 283)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDliMainAccountOrigin
        '
        Me.INDliMainAccountOrigin.Control = Me.INDsleMainAccountOrigin
        Me.INDliMainAccountOrigin.CustomizationFormText = "LayoutControlItem1"
        Me.INDliMainAccountOrigin.Location = New System.Drawing.Point(0, 0)
        Me.INDliMainAccountOrigin.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDliMainAccountOrigin.MinSize = New System.Drawing.Size(390, 62)
        Me.INDliMainAccountOrigin.Name = "INDliMainAccountOrigin"
        Me.INDliMainAccountOrigin.Size = New System.Drawing.Size(427, 62)
        Me.INDliMainAccountOrigin.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliMainAccountOrigin.Text = "Cuenta Origen"
        Me.INDliMainAccountOrigin.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliMainAccountOrigin.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliMainAccountOrigin.TextSize = New System.Drawing.Size(96, 21)
        Me.INDliMainAccountOrigin.TextToControlDistance = 5
        '
        'INDliMainAccountDestination
        '
        Me.INDliMainAccountDestination.Control = Me.INDsleMainAccountDestination
        Me.INDliMainAccountDestination.CustomizationFormText = "LayoutControlItem2"
        Me.INDliMainAccountDestination.Location = New System.Drawing.Point(0, 62)
        Me.INDliMainAccountDestination.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDliMainAccountDestination.MinSize = New System.Drawing.Size(390, 62)
        Me.INDliMainAccountDestination.Name = "INDliMainAccountDestination"
        Me.INDliMainAccountDestination.Size = New System.Drawing.Size(427, 62)
        Me.INDliMainAccountDestination.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliMainAccountDestination.Text = "Cuenta Destino"
        Me.INDliMainAccountDestination.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliMainAccountDestination.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliMainAccountDestination.TextSize = New System.Drawing.Size(96, 21)
        Me.INDliMainAccountDestination.TextToControlDistance = 5
        '
        'INDliDescription
        '
        Me.INDliDescription.Control = Me.INDmeDescription
        Me.INDliDescription.CustomizationFormText = "LayoutControlItem3"
        Me.INDliDescription.Location = New System.Drawing.Point(0, 124)
        Me.INDliDescription.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDliDescription.MinSize = New System.Drawing.Size(390, 100)
        Me.INDliDescription.Name = "INDliDescription"
        Me.INDliDescription.Size = New System.Drawing.Size(427, 100)
        Me.INDliDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDescription.Text = "Descripción"
        Me.INDliDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliDescription.TextSize = New System.Drawing.Size(96, 21)
        Me.INDliDescription.TextToControlDistance = 5
        '
        'INDliAdd
        '
        Me.INDliAdd.Control = Me.INDsbAdd
        Me.INDliAdd.CustomizationFormText = "LayoutControlItem1"
        Me.INDliAdd.Location = New System.Drawing.Point(0, 224)
        Me.INDliAdd.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliAdd.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliAdd.Name = "INDliAdd"
        Me.INDliAdd.Size = New System.Drawing.Size(427, 39)
        Me.INDliAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAdd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAdd.TextToControlDistance = 0
        Me.INDliAdd.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'CtrAccountPackage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrAccountPackage"
        Me.Size = New System.Drawing.Size(447, 283)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleMainAccountDestination.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvMainAccountDestination, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleMainAccountOrigin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvMainAccountOrigin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliMainAccountOrigin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliMainAccountDestination, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDsleMainAccountDestination As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvMainAccountDestination As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDsleMainAccountOrigin As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvMainAccountOrigin As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliMainAccountOrigin As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDliAdd As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents INDliMainAccountDestination As DevExpress.XtraLayout.LayoutControlItem

End Class
