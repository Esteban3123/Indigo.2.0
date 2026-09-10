Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBacterialResistanceMedication
    Inherits FormBase

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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSmbHistoricalLoad = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcATCHistorical = New DevExpress.XtraGrid.GridControl()
        Me.INDGvATCHistorical = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRiMmeObservationHistorical = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.INDGcATCValid = New DevExpress.XtraGrid.GridControl()
        Me.INDGvATCValid = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRiMmeObservation = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.INDSmbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDMmeObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDDteEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDDteStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleAtc = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvAtc = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSel = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRICESel = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgDatosGenerales = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAtc = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStartDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgATCValid = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciATCValid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgATCHistorical = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciATCHistorical = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciHistoricalLoad = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl2 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.GridViewColumnHeaderExtender1 = New Presentation.Controls.GridViewColumnHeaderExtender()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDGcATCHistorical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvATCHistorical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRiMmeObservationHistorical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcATCValid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvATCValid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRiMmeObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMmeObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleAtc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAtc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRICESel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgDatosGenerales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAtc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgATCValid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciATCValid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgATCHistorical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciATCHistorical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciHistoricalLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1014, 614)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1014, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1014, 98)
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDSmbHistoricalLoad)
        Me.INDLcRoot.Controls.Add(Me.INDGcATCHistorical)
        Me.INDLcRoot.Controls.Add(Me.INDGcATCValid)
        Me.INDLcRoot.Controls.Add(Me.INDSmbAdd)
        Me.INDLcRoot.Controls.Add(Me.INDMmeObservation)
        Me.INDLcRoot.Controls.Add(Me.INDDteEndDate)
        Me.INDLcRoot.Controls.Add(Me.INDDteStartDate)
        Me.INDLcRoot.Controls.Add(Me.INDSleAtc)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.INDLcgRoot
        Me.INDLcRoot.Size = New System.Drawing.Size(810, 605)
        Me.INDLcRoot.TabIndex = 1
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDSmbHistoricalLoad
        '
        Me.INDSmbHistoricalLoad.Location = New System.Drawing.Point(1152, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSmbHistoricalLoad, False)
        Me.INDSmbHistoricalLoad.Name = "INDSmbHistoricalLoad"
        Me.INDSmbHistoricalLoad.Size = New System.Drawing.Size(496, 28)
        Me.INDSmbHistoricalLoad.StyleController = Me.INDLcRoot
        Me.INDSmbHistoricalLoad.TabIndex = 6
        Me.INDSmbHistoricalLoad.Text = "Cargar"
        '
        'INDGcATCHistorical
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcATCHistorical, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcATCHistorical, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcATCHistorical, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcATCHistorical, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcATCHistorical, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcATCHistorical, False)
        Me.INDGcATCHistorical.Location = New System.Drawing.Point(1152, 91)
        Me.INDGcATCHistorical.MainView = Me.INDGvATCHistorical
        Me.INDGcATCHistorical.Name = "INDGcATCHistorical"
        Me.INDGcATCHistorical.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRiMmeObservationHistorical})
        Me.INDGcATCHistorical.Size = New System.Drawing.Size(496, 473)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcATCHistorical, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcATCHistorical.TabIndex = 7
        Me.INDGcATCHistorical.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvATCHistorical})
        '
        'INDGvATCHistorical
        '
        Me.INDGvATCHistorical.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvATCHistorical.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvATCHistorical.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvATCHistorical.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvATCHistorical.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvATCHistorical.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvATCHistorical.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvATCHistorical.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvATCHistorical.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvATCHistorical.Appearance.Row.Options.UseFont = True
        Me.INDGvATCHistorical.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvATCHistorical.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvATCHistorical.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10})
        Me.INDGvATCHistorical.GridControl = Me.INDGcATCHistorical
        Me.INDGvATCHistorical.Name = "INDGvATCHistorical"
        Me.INDGvATCHistorical.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvATCHistorical.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvATCHistorical.OptionsView.ShowAutoFilterRow = True
        Me.INDGvATCHistorical.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvATCHistorical, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "ATC"
        Me.GridColumn7.FieldName = "CodeNameATC"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 212
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Fecha Inicial"
        Me.GridColumn8.DisplayFormat.FormatString = "d"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn8.FieldName = "StartDate"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 97
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Fecha Final"
        Me.GridColumn9.DisplayFormat.FormatString = "d"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn9.FieldName = "EndDate"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 2
        Me.GridColumn9.Width = 81
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Observación"
        Me.GridColumn10.ColumnEdit = Me.INDRiMmeObservationHistorical
        Me.GridColumn10.FieldName = "Observation"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 3
        Me.GridColumn10.Width = 88
        '
        'INDRiMmeObservationHistorical
        '
        Me.INDRiMmeObservationHistorical.AutoHeight = False
        Me.INDRiMmeObservationHistorical.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRiMmeObservationHistorical.Name = "INDRiMmeObservationHistorical"
        Me.INDRiMmeObservationHistorical.ReadOnly = True
        Me.INDRiMmeObservationHistorical.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGcATCValid
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcATCValid, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcATCValid, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcATCValid, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcATCValid, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcATCValid, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcATCValid, False)
        Me.INDGcATCValid.Location = New System.Drawing.Point(428, 59)
        Me.INDGcATCValid.MainView = Me.INDGvATCValid
        Me.INDGcATCValid.Name = "INDGcATCValid"
        Me.INDGcATCValid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRiMmeObservation})
        Me.INDGcATCValid.Size = New System.Drawing.Size(696, 505)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcATCValid, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcATCValid.TabIndex = 5
        Me.INDGcATCValid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvATCValid})
        '
        'INDGvATCValid
        '
        Me.INDGvATCValid.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvATCValid.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvATCValid.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvATCValid.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvATCValid.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvATCValid.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvATCValid.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvATCValid.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvATCValid.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvATCValid.Appearance.Row.Options.UseFont = True
        Me.INDGvATCValid.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvATCValid.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvATCValid.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDGvATCValid.GridControl = Me.INDGcATCValid
        Me.INDGvATCValid.Name = "INDGvATCValid"
        Me.INDGvATCValid.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvATCValid.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvATCValid.OptionsView.ShowAutoFilterRow = True
        Me.INDGvATCValid.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvATCValid, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "ATC"
        Me.GridColumn3.FieldName = "CodeNameATC"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 384
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Fecha Inicial"
        Me.GridColumn4.DisplayFormat.FormatString = "d"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn4.FieldName = "StartDate"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 105
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Fecha Final"
        Me.GridColumn5.DisplayFormat.FormatString = "d"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn5.FieldName = "EndDate"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        Me.GridColumn5.Width = 96
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Observación"
        Me.GridColumn6.ColumnEdit = Me.INDRiMmeObservation
        Me.GridColumn6.FieldName = "Observation"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        Me.GridColumn6.Width = 93
        '
        'INDRiMmeObservation
        '
        Me.INDRiMmeObservation.AutoHeight = False
        Me.INDRiMmeObservation.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRiMmeObservation.Name = "INDRiMmeObservation"
        Me.INDRiMmeObservation.ReadOnly = True
        Me.INDRiMmeObservation.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDSmbAdd
        '
        Me.INDSmbAdd.Location = New System.Drawing.Point(24, 295)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSmbAdd, False)
        Me.INDSmbAdd.Name = "INDSmbAdd"
        Me.INDSmbAdd.Size = New System.Drawing.Size(376, 28)
        Me.INDSmbAdd.StyleController = Me.INDLcRoot
        Me.INDSmbAdd.TabIndex = 4
        Me.INDSmbAdd.Text = "Agregar"
        '
        'INDMmeObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmeObservation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmeObservation, False)
        Me.INDMmeObservation.Location = New System.Drawing.Point(24, 175)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmeObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMmeObservation.Name = "INDMmeObservation"
        Me.INDMmeObservation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMmeObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDMmeObservation.Properties.MaxLength = 500
        Me.INDMmeObservation.Size = New System.Drawing.Size(376, 116)
        Me.INDMmeObservation.StyleController = Me.INDLcRoot
        Me.INDMmeObservation.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMmeObservation, 0)
        '
        'INDDteEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteEndDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteEndDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteEndDate, True)
        Me.INDDteEndDate.EditValue = Nothing
        Me.INDDteEndDate.EnterMoveNextControl = True
        Me.INDDteEndDate.Location = New System.Drawing.Point(119, 123)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteEndDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteEndDate.Name = "INDDteEndDate"
        Me.INDDteEndDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDteEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteEndDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDteEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteEndDate.Size = New System.Drawing.Size(281, 28)
        Me.INDDteEndDate.StyleController = Me.INDLcRoot
        Me.INDDteEndDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteEndDate, 0)
        Me.INDDteEndDate.ToolTip = "Este Campo es Necesario"
        '
        'INDDteStartDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteStartDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteStartDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteStartDate, True)
        Me.INDDteStartDate.EditValue = Nothing
        Me.INDDteStartDate.EnterMoveNextControl = True
        Me.INDDteStartDate.Location = New System.Drawing.Point(119, 91)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteStartDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteStartDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteStartDate.Name = "INDDteStartDate"
        Me.INDDteStartDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDteStartDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteStartDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteStartDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteStartDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteStartDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteStartDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDteStartDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteStartDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteStartDate.Size = New System.Drawing.Size(281, 28)
        Me.INDDteStartDate.StyleController = Me.INDLcRoot
        Me.INDDteStartDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteStartDate, 0)
        Me.INDDteStartDate.ToolTip = "Este Campo es Necesario"
        '
        'INDSleAtc
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleAtc, AppearanceObject1)
        Me.IndigoSearchLookUpControl2.SetAppearanceEmbeddedNavigator(Me.INDSleAtc, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleAtc, AppearanceObject3)
        Me.IndigoSearchLookUpControl2.SetAppearanceTextFindControl(Me.INDSleAtc, AppearanceObject4)
        Me.IndigoSearchLookUpControl2.SetAppendButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleAtc, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleAtc, True)
        Me.IndigoSearchLookUpControl2.SetCancelEditButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetEditButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetEndEditButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleAtc, False)
        Me.INDSleAtc.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl2.SetExportButton(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetFirstButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetLastButtonNavigator(Me.INDSleAtc, False)
        Me.INDSleAtc.Location = New System.Drawing.Point(119, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleAtc, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleAtc.Name = "INDSleAtc"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetNextButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetNextPageButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetOpenForm(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetPrevButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetPrevPageButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleAtc, False)
        Me.INDSleAtc.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleAtc.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleAtc.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleAtc.Properties.Appearance.Options.UseFont = True
        Me.INDSleAtc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleAtc.Properties.DisplayMember = "CodeName"
        Me.INDSleAtc.Properties.NullText = ""
        Me.INDSleAtc.Properties.PopupSizeable = False
        Me.INDSleAtc.Properties.PopupView = Me.INDGvAtc
        Me.INDSleAtc.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRICESel})
        Me.INDSleAtc.Properties.ShowFooter = False
        Me.INDSleAtc.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl2.SetRemoveButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleAtc, True)
        Me.IndigoSearchLookUpControl2.SetSaveXmlGrid(Me.INDSleAtc, True)
        Me.IndigoSearchLookUpControl2.SetShowDeleteButton(Me.INDSleAtc, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleAtc, True)
        Me.IndigoSearchLookUpControl2.SetShowFindButton(Me.INDSleAtc, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleAtc, True)
        Me.INDSleAtc.Size = New System.Drawing.Size(281, 28)
        Me.INDSleAtc.StyleController = Me.INDLcRoot
        Me.INDSleAtc.TabIndex = 0
        Me.IndigoSearchLookUpControl2.SetTagForm(Me.INDSleAtc, Nothing)
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleAtc, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleAtc, 0)
        Me.IndigoSearchLookUpControl2.SetTextStringFormat(Me.INDSleAtc, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleAtc, "{0} - {1}")
        Me.INDSleAtc.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetTxtFindEnterEnabled(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl2.SetUseEmbeddedNavigator(Me.INDSleAtc, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleAtc, False)
        '
        'INDGvAtc
        '
        Me.INDGvAtc.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvAtc.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvAtc.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvAtc.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvAtc.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAtc.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvAtc.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAtc.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvAtc.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvAtc.Appearance.Row.Options.UseFont = True
        Me.INDGvAtc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSel, Me.GridColumn1, Me.GridColumn2})
        Me.INDGvAtc.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvAtc.Name = "INDGvAtc"
        Me.INDGvAtc.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvAtc.OptionsSelection.MultiSelect = True
        Me.INDGvAtc.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvAtc.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvAtc.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvAtc.OptionsView.ShowAutoFilterRow = True
        Me.INDGvAtc.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvAtc, False)
        '
        'INDColSel
        '
        Me.INDColSel.ColumnEdit = Me.INDRICESel
        Me.INDColSel.FieldName = "CheckValue"
        Me.INDColSel.Name = "INDColSel"
        Me.INDColSel.OptionsColumn.ShowCaption = False
        Me.INDColSel.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Me.INDColSel.Tag = "Presentation.Controls.ColumnStateRepository"
        Me.INDColSel.Visible = True
        Me.INDColSel.VisibleIndex = 0
        '
        'INDRICESel
        '
        Me.INDRICESel.AutoHeight = False
        Me.INDRICESel.Name = "INDRICESel"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
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
        Me.INDLcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgDatosGenerales, Me.INDLcgATCValid, Me.INDLcgATCHistorical})
        Me.INDLcgRoot.Name = "INDLcgRoot"
        Me.INDLcgRoot.Size = New System.Drawing.Size(1672, 588)
        Me.INDLcgRoot.TextVisible = False
        '
        'INDLcgDatosGenerales
        '
        Me.INDLcgDatosGenerales.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgDatosGenerales.AppearanceGroup.Options.UseFont = True
        Me.INDLcgDatosGenerales.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgDatosGenerales.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgDatosGenerales.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDatosGenerales.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgDatosGenerales.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgDatosGenerales.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgDatosGenerales.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDatosGenerales.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgDatosGenerales.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDatosGenerales.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgDatosGenerales.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDatosGenerales.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgDatosGenerales, False)
        Me.INDLcgDatosGenerales.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAtc, Me.INDLciStartDate, Me.INDLciEndDate, Me.INDLciObservation, Me.INDLciAdd})
        Me.INDLcgDatosGenerales.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgDatosGenerales.Name = "INDLcgDatosGenerales"
        Me.INDLcgDatosGenerales.Size = New System.Drawing.Size(404, 568)
        Me.INDLcgDatosGenerales.Text = "Datos Generales"
        '
        'INDLciAtc
        '
        Me.INDLciAtc.Control = Me.INDSleAtc
        Me.INDLciAtc.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAtc.MaxSize = New System.Drawing.Size(380, 32)
        Me.INDLciAtc.MinSize = New System.Drawing.Size(380, 32)
        Me.INDLciAtc.Name = "INDLciAtc"
        Me.INDLciAtc.Size = New System.Drawing.Size(380, 32)
        Me.INDLciAtc.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAtc.Text = "ATC"
        Me.INDLciAtc.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAtc.TextSize = New System.Drawing.Size(90, 20)
        Me.INDLciAtc.TextToControlDistance = 5
        '
        'INDLciStartDate
        '
        Me.INDLciStartDate.Control = Me.INDDteStartDate
        Me.INDLciStartDate.Location = New System.Drawing.Point(0, 32)
        Me.INDLciStartDate.MaxSize = New System.Drawing.Size(380, 32)
        Me.INDLciStartDate.MinSize = New System.Drawing.Size(380, 32)
        Me.INDLciStartDate.Name = "INDLciStartDate"
        Me.INDLciStartDate.Size = New System.Drawing.Size(380, 32)
        Me.INDLciStartDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStartDate.Text = "Fecha Inicial"
        Me.INDLciStartDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciStartDate.TextSize = New System.Drawing.Size(90, 20)
        Me.INDLciStartDate.TextToControlDistance = 5
        '
        'INDLciEndDate
        '
        Me.INDLciEndDate.Control = Me.INDDteEndDate
        Me.INDLciEndDate.Location = New System.Drawing.Point(0, 64)
        Me.INDLciEndDate.MaxSize = New System.Drawing.Size(380, 32)
        Me.INDLciEndDate.MinSize = New System.Drawing.Size(380, 32)
        Me.INDLciEndDate.Name = "INDLciEndDate"
        Me.INDLciEndDate.Size = New System.Drawing.Size(380, 32)
        Me.INDLciEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEndDate.Text = "Fecha Final"
        Me.INDLciEndDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEndDate.TextSize = New System.Drawing.Size(90, 20)
        Me.INDLciEndDate.TextToControlDistance = 5
        '
        'INDLciObservation
        '
        Me.INDLciObservation.Control = Me.INDMmeObservation
        Me.INDLciObservation.Location = New System.Drawing.Point(0, 96)
        Me.INDLciObservation.MaxSize = New System.Drawing.Size(380, 140)
        Me.INDLciObservation.MinSize = New System.Drawing.Size(380, 140)
        Me.INDLciObservation.Name = "INDLciObservation"
        Me.INDLciObservation.Size = New System.Drawing.Size(380, 140)
        Me.INDLciObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciObservation.Text = "Observación:"
        Me.INDLciObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservation.TextSize = New System.Drawing.Size(83, 17)
        '
        'INDLciAdd
        '
        Me.INDLciAdd.Control = Me.INDSmbAdd
        Me.INDLciAdd.Location = New System.Drawing.Point(0, 236)
        Me.INDLciAdd.MaxSize = New System.Drawing.Size(380, 32)
        Me.INDLciAdd.MinSize = New System.Drawing.Size(380, 32)
        Me.INDLciAdd.Name = "INDLciAdd"
        Me.INDLciAdd.Size = New System.Drawing.Size(380, 273)
        Me.INDLciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAdd.TextVisible = False
        '
        'INDLcgATCValid
        '
        Me.INDLcgATCValid.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgATCValid.AppearanceGroup.Options.UseFont = True
        Me.INDLcgATCValid.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgATCValid.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgATCValid.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgATCValid.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgATCValid.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgATCValid.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgATCValid.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgATCValid.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgATCValid.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgATCValid.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgATCValid.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgATCValid.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgATCValid, False)
        Me.INDLcgATCValid.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciATCValid})
        Me.INDLcgATCValid.Location = New System.Drawing.Point(404, 0)
        Me.INDLcgATCValid.Name = "INDLcgATCValid"
        Me.INDLcgATCValid.Size = New System.Drawing.Size(724, 568)
        Me.INDLcgATCValid.Text = "Vigentes"
        '
        'INDLciATCValid
        '
        Me.INDLciATCValid.Control = Me.INDGcATCValid
        Me.INDLciATCValid.Location = New System.Drawing.Point(0, 0)
        Me.INDLciATCValid.MaxSize = New System.Drawing.Size(700, 0)
        Me.INDLciATCValid.MinSize = New System.Drawing.Size(700, 120)
        Me.INDLciATCValid.Name = "INDLciATCValid"
        Me.INDLciATCValid.Size = New System.Drawing.Size(700, 509)
        Me.INDLciATCValid.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciATCValid.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciATCValid.TextVisible = False
        '
        'INDLcgATCHistorical
        '
        Me.INDLcgATCHistorical.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgATCHistorical.AppearanceGroup.Options.UseFont = True
        Me.INDLcgATCHistorical.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgATCHistorical.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgATCHistorical.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgATCHistorical.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgATCHistorical.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgATCHistorical.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgATCHistorical.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgATCHistorical.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgATCHistorical.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgATCHistorical.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgATCHistorical.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgATCHistorical.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgATCHistorical, False)
        Me.INDLcgATCHistorical.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciATCHistorical, Me.INDLciHistoricalLoad})
        Me.INDLcgATCHistorical.Location = New System.Drawing.Point(1128, 0)
        Me.INDLcgATCHistorical.Name = "INDLcgATCHistorical"
        Me.INDLcgATCHistorical.Size = New System.Drawing.Size(524, 568)
        Me.INDLcgATCHistorical.Text = "Histórico"
        '
        'INDLciATCHistorical
        '
        Me.INDLciATCHistorical.Control = Me.INDGcATCHistorical
        Me.INDLciATCHistorical.Location = New System.Drawing.Point(0, 32)
        Me.INDLciATCHistorical.MinSize = New System.Drawing.Size(500, 120)
        Me.INDLciATCHistorical.Name = "INDLciATCHistorical"
        Me.INDLciATCHistorical.Size = New System.Drawing.Size(500, 477)
        Me.INDLciATCHistorical.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciATCHistorical.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciATCHistorical.TextVisible = False
        '
        'INDLciHistoricalLoad
        '
        Me.INDLciHistoricalLoad.Control = Me.INDSmbHistoricalLoad
        Me.INDLciHistoricalLoad.Location = New System.Drawing.Point(0, 0)
        Me.INDLciHistoricalLoad.MaxSize = New System.Drawing.Size(0, 32)
        Me.INDLciHistoricalLoad.MinSize = New System.Drawing.Size(500, 32)
        Me.INDLciHistoricalLoad.Name = "INDLciHistoricalLoad"
        Me.INDLciHistoricalLoad.Size = New System.Drawing.Size(500, 32)
        Me.INDLciHistoricalLoad.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciHistoricalLoad.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciHistoricalLoad.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 605)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'GridViewColumnHeaderExtender1
        '
        Me.GridViewColumnHeaderExtender1.DrawCheckBoxByDefault = True
        Me.GridViewColumnHeaderExtender1.View = Me.INDGvAtc
        '
        'FrmBacterialResistanceMedication
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 736)
        Me.Name = "FrmBacterialResistanceMedication"
        Me.Opacity = 1.0R
        Me.Tag = "2144"
        Me.Text = "Medicamentos con resistencia bacteriana"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDGcATCHistorical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvATCHistorical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRiMmeObservationHistorical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcATCValid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvATCValid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRiMmeObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMmeObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleAtc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAtc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRICESel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgDatosGenerales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAtc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgATCValid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciATCValid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgATCHistorical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciATCHistorical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciHistoricalLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDSmbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDMmeObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDDteEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDDteStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDSleAtc As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl2 As IndigoSearchLookUpControl
    Friend WithEvents INDGvAtc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciAtc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciStartDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgDatosGenerales As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcATCValid As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvATCValid As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRiMmeObservation As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents INDLciATCValid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgATCValid As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcATCHistorical As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvATCHistorical As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRiMmeObservationHistorical As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents INDLciATCHistorical As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgATCHistorical As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDSmbHistoricalLoad As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciHistoricalLoad As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridViewColumnHeaderExtender1 As GridViewColumnHeaderExtender
    Friend WithEvents INDColSel As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRICESel As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
