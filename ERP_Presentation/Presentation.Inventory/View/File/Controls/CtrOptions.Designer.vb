<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrOptions
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
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDrecNote = New DevExpress.XtraRichEdit.RichEditControl()
        Me.INDgcDiagnostisData = New DevExpress.XtraGrid.GridControl()
        Me.INDgvTechnicalSheet = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemComments = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.INDsbAddDiagnostic = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleDiagnostic = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvDiagnostic = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliDiagnostic = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAddDiagnostic = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDiagnosticList = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliNote = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        'Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgcDiagnostisData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvTechnicalSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleDiagnostic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDiagnostic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDiagnostic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAddDiagnostic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDiagnosticList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDrecNote)
        Me.LayoutControl1.Controls.Add(Me.INDgcDiagnostisData)
        Me.LayoutControl1.Controls.Add(Me.INDsbAddDiagnostic)
        Me.LayoutControl1.Controls.Add(Me.INDsleDiagnostic)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(776, 498)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDrecNote
        '
        Me.INDrecNote.EnableToolTips = True
        Me.INDrecNote.Location = New System.Drawing.Point(12, 198)
        Me.INDrecNote.Name = "INDrecNote"
        Me.INDrecNote.Options.Comments.ShowAllAuthors = True
        Me.INDrecNote.Options.Comments.Visibility = DevExpress.XtraRichEdit.RichEditCommentVisibility.Auto
        Me.INDrecNote.Options.CopyPaste.MaintainDocumentSectionSettings = False
        Me.INDrecNote.Options.Fields.UseCurrentCultureDateTimeFormat = False
        Me.INDrecNote.Options.MailMerge.KeepLastParagraph = False
        Me.INDrecNote.Size = New System.Drawing.Size(746, 288)
        Me.INDrecNote.TabIndex = 7
        '
        'INDgcDiagnostisData
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDiagnostisData, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDiagnostisData, Nothing)
        Me.INDgcDiagnostisData.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDiagnostisData, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDiagnostisData, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDiagnostisData, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDiagnostisData, False)
        Me.INDgcDiagnostisData.Location = New System.Drawing.Point(12, 48)
        Me.INDgcDiagnostisData.MainView = Me.INDgvTechnicalSheet
        Me.INDgcDiagnostisData.Name = "INDgcDiagnostisData"
        Me.INDgcDiagnostisData.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComments})
        Me.INDgcDiagnostisData.Size = New System.Drawing.Size(746, 146)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDiagnostisData, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcDiagnostisData.TabIndex = 6
        Me.INDgcDiagnostisData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvTechnicalSheet})
        '
        'INDgvTechnicalSheet
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDgvTechnicalSheet.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvTechnicalSheet.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvTechnicalSheet.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvTechnicalSheet.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvTechnicalSheet.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvTechnicalSheet.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvTechnicalSheet.Appearance.Row.Options.UseFont = True
        Me.INDgvTechnicalSheet.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvTechnicalSheet.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvTechnicalSheet.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.INDgvTechnicalSheet.GridControl = Me.INDgcDiagnostisData
        Me.INDgvTechnicalSheet.Name = "INDgvTechnicalSheet"
        Me.INDgvTechnicalSheet.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvTechnicalSheet.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvTechnicalSheet.OptionsView.ShowDetailButtons = False
        Me.INDgvTechnicalSheet.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvTechnicalSheet, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "DiagnosticCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.OptionsColumn.AllowMove = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 268
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "DiagnosticName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.OptionsColumn.AllowMove = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 473
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Comentarios"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemComments
        Me.GridColumn3.FieldName = "Comment"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowMove = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 348
        '
        'RepositoryItemComments
        '
        Me.RepositoryItemComments.AutoHeight = False
        Me.RepositoryItemComments.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComments.Name = "RepositoryItemComments"
        '
        'INDsbAddDiagnostic
        '
        Me.INDsbAddDiagnostic.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsbAddDiagnostic.Appearance.Options.UseFont = True
        Me.INDsbAddDiagnostic.Location = New System.Drawing.Point(412, 12)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAddDiagnostic, True)
        Me.INDsbAddDiagnostic.Name = "INDsbAddDiagnostic"
        Me.INDsbAddDiagnostic.Size = New System.Drawing.Size(96, 28)
        Me.INDsbAddDiagnostic.StyleController = Me.LayoutControl1
        Me.INDsbAddDiagnostic.TabIndex = 5
        Me.INDsbAddDiagnostic.Text = "Agregar"
        '
        'INDsleDiagnostic
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleDiagnostic, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleDiagnostic, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleDiagnostic, False)
        Me.INDsleDiagnostic.Location = New System.Drawing.Point(152, 12)
        Me.INDsleDiagnostic.Name = "INDsleDiagnostic"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleDiagnostic, False)
        Me.INDsleDiagnostic.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleDiagnostic.Properties.Appearance.Options.UseFont = True
        Me.INDsleDiagnostic.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleDiagnostic.Properties.DisplayMember = "CodeName"
        Me.INDsleDiagnostic.Properties.NullText = ""
        Me.INDsleDiagnostic.Properties.PopupSizeable = False
        Me.INDsleDiagnostic.Properties.ShowFooter = False
        Me.INDsleDiagnostic.Properties.ValueMember = "Id"
        Me.INDsleDiagnostic.Properties.View = Me.INDgvDiagnostic
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleDiagnostic, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleDiagnostic, True)
        Me.INDsleDiagnostic.Size = New System.Drawing.Size(256, 28)
        Me.INDsleDiagnostic.StyleController = Me.LayoutControl1
        Me.INDsleDiagnostic.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleDiagnostic, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleDiagnostic, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleDiagnostic, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleDiagnostic, False)
        '
        'INDgvDiagnostic
        '
        Me.INDgvDiagnostic.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvDiagnostic.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvDiagnostic.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvDiagnostic.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvDiagnostic.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDiagnostic.Appearance.Row.Options.UseFont = True
        Me.INDgvDiagnostic.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5})
        Me.INDgvDiagnostic.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvDiagnostic.Name = "INDgvDiagnostic"
        Me.INDgvDiagnostic.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvDiagnostic.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvDiagnostic.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvDiagnostic.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvDiagnostic, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Código"
        Me.GridColumn4.FieldName = "Code"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Nombre"
        Me.GridColumn5.FieldName = "Name"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliDiagnostic, Me.INDliAddDiagnostic, Me.INDliDiagnosticList, Me.INDliNote})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(776, 498)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDliDiagnostic
        '
        Me.INDliDiagnostic.Control = Me.INDsleDiagnostic
        Me.INDliDiagnostic.CustomizationFormText = "LayoutControlItem1"
        Me.INDliDiagnostic.Location = New System.Drawing.Point(0, 0)
        Me.INDliDiagnostic.MaxSize = New System.Drawing.Size(400, 36)
        Me.INDliDiagnostic.MinSize = New System.Drawing.Size(400, 36)
        Me.INDliDiagnostic.Name = "INDliDiagnostic"
        Me.INDliDiagnostic.Size = New System.Drawing.Size(400, 36)
        Me.INDliDiagnostic.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDiagnostic.Text = "Diagnóstico"
        Me.INDliDiagnostic.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDiagnostic.TextSize = New System.Drawing.Size(135, 13)
        Me.INDliDiagnostic.TextToControlDistance = 5
        '
        'INDliAddDiagnostic
        '
        Me.INDliAddDiagnostic.Control = Me.INDsbAddDiagnostic
        Me.INDliAddDiagnostic.CustomizationFormText = "LayoutControlItem2"
        Me.INDliAddDiagnostic.Location = New System.Drawing.Point(400, 0)
        Me.INDliAddDiagnostic.MaxSize = New System.Drawing.Size(100, 32)
        Me.INDliAddDiagnostic.MinSize = New System.Drawing.Size(100, 32)
        Me.INDliAddDiagnostic.Name = "INDliAddDiagnostic"
        Me.INDliAddDiagnostic.Size = New System.Drawing.Size(356, 36)
        Me.INDliAddDiagnostic.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAddDiagnostic.Text = "INDliAddDiagnostic"
        Me.INDliAddDiagnostic.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAddDiagnostic.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAddDiagnostic.TextToControlDistance = 0
        Me.INDliAddDiagnostic.TextVisible = False
        '
        'INDliDiagnosticList
        '
        Me.INDliDiagnosticList.Control = Me.INDgcDiagnostisData
        Me.INDliDiagnosticList.CustomizationFormText = "LayoutControlItem3"
        Me.INDliDiagnosticList.Location = New System.Drawing.Point(0, 36)
        Me.INDliDiagnosticList.MaxSize = New System.Drawing.Size(750, 150)
        Me.INDliDiagnosticList.MinSize = New System.Drawing.Size(750, 150)
        Me.INDliDiagnosticList.Name = "INDliDiagnosticList"
        Me.INDliDiagnosticList.Size = New System.Drawing.Size(756, 150)
        Me.INDliDiagnosticList.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDiagnosticList.Text = "INDliDiagnosticList"
        Me.INDliDiagnosticList.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDiagnosticList.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliDiagnosticList.TextToControlDistance = 0
        Me.INDliDiagnosticList.TextVisible = False
        '
        'INDliNote
        '
        Me.INDliNote.Control = Me.INDrecNote
        Me.INDliNote.CustomizationFormText = "LayoutControlItem4"
        Me.INDliNote.Location = New System.Drawing.Point(0, 186)
        Me.INDliNote.MaxSize = New System.Drawing.Size(750, 0)
        Me.INDliNote.MinSize = New System.Drawing.Size(750, 24)
        Me.INDliNote.Name = "INDliNote"
        Me.INDliNote.Size = New System.Drawing.Size(756, 292)
        Me.INDliNote.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliNote.Text = "INDliNote"
        Me.INDliNote.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliNote.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliNote.TextToControlDistance = 0
        Me.INDliNote.TextVisible = False
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20141202"

        '
        'CtrOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrOptions"
        Me.Size = New System.Drawing.Size(776, 498)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgcDiagnostisData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvTechnicalSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleDiagnostic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDiagnostic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDiagnostic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAddDiagnostic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDiagnosticList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsbAddDiagnostic As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDsleDiagnostic As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDgvDiagnostic As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDliDiagnostic As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliAddDiagnostic As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDrecNote As DevExpress.XtraRichEdit.RichEditControl
    Friend WithEvents INDgcDiagnostisData As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvTechnicalSheet As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliDiagnosticList As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliNote As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemComments As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit

End Class
