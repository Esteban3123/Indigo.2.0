Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmGrouperCareGroup
    Inherits FormBase

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbAddGroupers = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcGroupers = New DevExpress.XtraGrid.GridControl()
        Me.INDGvGroupers = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColGrouper = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleGrouper = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDsgvGroupers = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGrouper = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyRoot.SuspendLayout()
        CType(Me.INDGcGroupers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvGroupers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleGrouper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsgvGroupers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGrouper, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyRoot)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(549, 327)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(549, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(549, 98)
        '
        'INDLyRoot
        '
        Me.INDLyRoot.Controls.Add(Me.INDSbAddGroupers)
        Me.INDLyRoot.Controls.Add(Me.INDSbAdd)
        Me.INDLyRoot.Controls.Add(Me.INDGcGroupers)
        Me.INDLyRoot.Controls.Add(Me.INDSleGrouper)
        Me.INDLyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDLyRoot.Name = "INDLyRoot"
        Me.INDLyRoot.Root = Me.INDLcgRoot
        Me.INDLyRoot.Size = New System.Drawing.Size(545, 318)
        Me.INDLyRoot.TabIndex = 0
        Me.INDLyRoot.Text = "LayoutControl1"
        '
        'INDSbAddGroupers
        '
        Me.INDSbAddGroupers.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSbAddGroupers.Appearance.Options.UseFont = True
        Me.INDSbAddGroupers.Location = New System.Drawing.Point(12, 278)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbAddGroupers, False)
        Me.INDSbAddGroupers.Name = "INDSbAddGroupers"
        Me.INDSbAddGroupers.Size = New System.Drawing.Size(521, 28)
        Me.INDSbAddGroupers.StyleController = Me.INDLyRoot
        Me.INDSbAddGroupers.TabIndex = 7
        Me.INDSbAddGroupers.Text = "Agregar Agrupadores"
        '
        'INDSbAdd
        '
        Me.INDSbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSbAdd.Appearance.Options.UseFont = True
        Me.INDSbAdd.Location = New System.Drawing.Point(444, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbAdd, False)
        Me.INDSbAdd.Name = "INDSbAdd"
        Me.INDSbAdd.Size = New System.Drawing.Size(76, 28)
        Me.INDSbAdd.StyleController = Me.INDLyRoot
        Me.INDSbAdd.TabIndex = 1
        Me.INDSbAdd.Text = "Agregar"
        '
        'INDGcGroupers
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcGroupers, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcGroupers, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcGroupers, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcGroupers, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcGroupers, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcGroupers, False)
        Me.INDGcGroupers.Location = New System.Drawing.Point(24, 91)
        Me.INDGcGroupers.MainView = Me.INDGvGroupers
        Me.INDGcGroupers.Name = "INDGcGroupers"
        Me.INDGcGroupers.Size = New System.Drawing.Size(497, 171)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcGroupers, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcGroupers.TabIndex = 2
        Me.INDGcGroupers.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvGroupers})
        '
        'INDGvGroupers
        '
        Me.INDGvGroupers.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvGroupers.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvGroupers.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvGroupers.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvGroupers.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvGroupers.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvGroupers.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvGroupers.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvGroupers.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvGroupers.Appearance.Row.Options.UseFont = True
        Me.INDGvGroupers.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvGroupers.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvGroupers.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColGrouper})
        Me.INDGvGroupers.GridControl = Me.INDGcGroupers
        Me.INDGvGroupers.Name = "INDGvGroupers"
        Me.INDGvGroupers.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvGroupers.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvGroupers.OptionsView.ShowAutoFilterRow = True
        Me.INDGvGroupers.OptionsView.ShowDetailButtons = False
        Me.INDGvGroupers.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvGroupers, False)
        '
        'INDColGrouper
        '
        Me.INDColGrouper.Caption = "Agrupador"
        Me.INDColGrouper.FieldName = "GrouperName"
        Me.INDColGrouper.Name = "INDColGrouper"
        Me.INDColGrouper.OptionsColumn.AllowEdit = False
        Me.INDColGrouper.OptionsColumn.AllowFocus = False
        Me.INDColGrouper.Visible = True
        Me.INDColGrouper.VisibleIndex = 0
        '
        'INDSleGrouper
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleGrouper, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleGrouper, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleGrouper, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleGrouper, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleGrouper, False)
        Me.INDSleGrouper.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleGrouper, False)
        Me.INDSleGrouper.Location = New System.Drawing.Point(144, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleGrouper, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleGrouper.Name = "INDSleGrouper"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleGrouper, False)
        Me.INDSleGrouper.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleGrouper.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleGrouper.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleGrouper.Properties.Appearance.Options.UseFont = True
        Me.INDSleGrouper.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDSleGrouper.Properties.DisplayMember = "CodeName"
        Me.INDSleGrouper.Properties.NullText = ""
        Me.INDSleGrouper.Properties.PopupSizeable = False
        Me.INDSleGrouper.Properties.ShowFooter = False
        Me.INDSleGrouper.Properties.ValueMember = "Id"
        Me.INDSleGrouper.Properties.View = Me.INDsgvGroupers
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleGrouper, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleGrouper, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleGrouper, True)
        Me.INDSleGrouper.Size = New System.Drawing.Size(296, 28)
        Me.INDSleGrouper.StyleController = Me.INDLyRoot
        Me.INDSleGrouper.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleGrouper, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleGrouper, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleGrouper, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleGrouper, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleGrouper, False)
        '
        'INDsgvGroupers
        '
        Me.INDsgvGroupers.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDsgvGroupers.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDsgvGroupers.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDsgvGroupers.Appearance.FocusedRow.Options.UseFont = True
        Me.INDsgvGroupers.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsgvGroupers.Appearance.GroupRow.Options.UseFont = True
        Me.INDsgvGroupers.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsgvGroupers.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDsgvGroupers.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDsgvGroupers.Appearance.Row.Options.UseFont = True
        Me.INDsgvGroupers.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.INDsgvGroupers.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDsgvGroupers.Name = "INDsgvGroupers"
        Me.INDsgvGroupers.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDsgvGroupers.OptionsView.EnableAppearanceEvenRow = True
        Me.INDsgvGroupers.OptionsView.EnableAppearanceOddRow = True
        Me.INDsgvGroupers.OptionsView.ShowAutoFilterRow = True
        Me.INDsgvGroupers.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDsgvGroupers, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 154
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Description"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 542
        '
        'INDLcgRoot
        '
        Me.INDLcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDLcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgRoot, False)
        Me.INDLcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgRoot.GroupBordersVisible = False
        Me.INDLcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LayoutControlItem1})
        Me.INDLcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgRoot.Name = "INDLcgRoot"
        Me.INDLcgRoot.Size = New System.Drawing.Size(545, 318)
        Me.INDLcgRoot.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.INDLciGrouper, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(525, 266)
        Me.LayoutControlGroup1.Text = "Agregar Agrupadores"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDGcGroupers
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(501, 175)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDLciGrouper
        '
        Me.INDLciGrouper.Control = Me.INDSleGrouper
        Me.INDLciGrouper.Location = New System.Drawing.Point(0, 0)
        Me.INDLciGrouper.MaxSize = New System.Drawing.Size(420, 30)
        Me.INDLciGrouper.MinSize = New System.Drawing.Size(420, 30)
        Me.INDLciGrouper.Name = "INDLciGrouper"
        Me.INDLciGrouper.Size = New System.Drawing.Size(420, 32)
        Me.INDLciGrouper.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGrouper.Text = "Agrupador"
        Me.INDLciGrouper.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciGrouper.TextSize = New System.Drawing.Size(115, 13)
        Me.INDLciGrouper.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDSbAdd
        Me.LayoutControlItem3.CustomizationFormText = "Agregar"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(420, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(80, 32)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(80, 32)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(81, 32)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSbAddGroupers
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 266)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(0, 32)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(131, 32)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(525, 32)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmGrouperCareGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(549, 449)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmGrouperCareGroup"
        Me.Opacity = 1.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Tag = "1530"
        Me.Text = "Agrupadores"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyRoot.ResumeLayout(False)
        CType(Me.INDGcGroupers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvGroupers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleGrouper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsgvGroupers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGrouper, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleGrouper As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDsgvGroupers As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciGrouper As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDGcGroupers As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvGroupers As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDColGrouper As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSbAddGroupers As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
