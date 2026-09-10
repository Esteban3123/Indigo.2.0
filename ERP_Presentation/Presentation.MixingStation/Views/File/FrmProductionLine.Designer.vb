#Region "Imports"
Imports Presentation.Controls
#End Region

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmProductionLine
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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDcncUnitDoseType = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlycBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleddlDosis = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSmbScheduleException = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcScheduleException = New DevExpress.XtraGrid.GridControl()
        Me.INDGvScheduleException = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMmeReasonForStop = New DevExpress.XtraEditors.MemoEdit()
        Me.INDDteStopDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDGcSchedules = New DevExpress.XtraGrid.GridControl()
        Me.INDGvSchedules = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSmbAddSchedule = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTmeEndTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDChkFestivo = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkDomingo = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkSabado = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkViernes = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkJueves = New DevExpress.XtraEditors.CheckEdit()
        Me.INDTmeStartTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDChkMartes = New DevExpress.XtraEditors.CheckEdit()
        Me.INDChkLunes = New DevExpress.XtraEditors.CheckEdit()
        Me.INDGleTime24 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleddlUnidad = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDgcUnitDoseType = New DevExpress.XtraGrid.GridControl()
        Me.INDviewUnitDosesType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.INDbtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDChkMiercoles = New DevExpress.XtraEditors.CheckEdit()
        Me.INDlycBaseUnitDoseType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrUnitDoseType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciUnitDoseType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgSchedule = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciTime24 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciLunes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMartes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMiercoles = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciJueves = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciViernes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSabado = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDomingo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFestivo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStartTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEndTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAddSchedule = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSchedules = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgScheduleException = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciScheduleException = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciReasonForStop = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStopDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAddScheduleException = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridView3 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDcncUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycBase.SuspendLayout()
        CType(Me.INDsleddlDosis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcScheduleException, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvScheduleException, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMmeReasonForStop.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteStopDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteStopDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcSchedules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSchedules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTmeEndTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkFestivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkDomingo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkSabado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkViernes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkJueves.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTmeStartTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkMartes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkLunes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleTime24.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleddlUnidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewUnitDosesType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkMiercoles.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBaseUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTime24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciLunes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMartes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMiercoles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciJueves, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciViernes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSabado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDomingo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFestivo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStartTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAddSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSchedules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgScheduleException, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciScheduleException, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReasonForStop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStopDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAddScheduleException, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDcncUnitDoseType)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1471, 601)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1471, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1471, 130)
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'INDcncUnitDoseType
        '
        Me.INDcncUnitDoseType.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDcncUnitDoseType.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDcncUnitDoseType.LayoutControl = Me.INDlycBase
        Me.INDcncUnitDoseType.Location = New System.Drawing.Point(2, 7)
        Me.INDcncUnitDoseType.Margin = New System.Windows.Forms.Padding(0)
        Me.INDcncUnitDoseType.Name = "INDcncUnitDoseType"
        Me.INDcncUnitDoseType.Size = New System.Drawing.Size(200, 592)
        Me.INDcncUnitDoseType.TabIndex = 2
        Me.INDcncUnitDoseType.UseDisabledStatePainter = False
        '
        'INDlycBase
        '
        Me.INDlycBase.Controls.Add(Me.INDsleddlDosis)
        Me.INDlycBase.Controls.Add(Me.INDSmbScheduleException)
        Me.INDlycBase.Controls.Add(Me.INDGcScheduleException)
        Me.INDlycBase.Controls.Add(Me.INDMmeReasonForStop)
        Me.INDlycBase.Controls.Add(Me.INDDteStopDate)
        Me.INDlycBase.Controls.Add(Me.INDGcSchedules)
        Me.INDlycBase.Controls.Add(Me.INDSmbAddSchedule)
        Me.INDlycBase.Controls.Add(Me.INDTmeEndTime)
        Me.INDlycBase.Controls.Add(Me.INDChkFestivo)
        Me.INDlycBase.Controls.Add(Me.INDChkDomingo)
        Me.INDlycBase.Controls.Add(Me.INDChkSabado)
        Me.INDlycBase.Controls.Add(Me.INDChkViernes)
        Me.INDlycBase.Controls.Add(Me.INDChkJueves)
        Me.INDlycBase.Controls.Add(Me.INDTmeStartTime)
        Me.INDlycBase.Controls.Add(Me.INDChkMartes)
        Me.INDlycBase.Controls.Add(Me.INDChkLunes)
        Me.INDlycBase.Controls.Add(Me.INDGleTime24)
        Me.INDlycBase.Controls.Add(Me.INDsleddlUnidad)
        Me.INDlycBase.Controls.Add(Me.INDtxtNombre)
        Me.INDlycBase.Controls.Add(Me.INDbtnCode)
        Me.INDlycBase.Controls.Add(Me.INDgcUnitDoseType)
        Me.INDlycBase.Controls.Add(Me.INDbtnAdd)
        Me.INDlycBase.Controls.Add(Me.INDChkMiercoles)
        Me.INDlycBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycBase.Location = New System.Drawing.Point(202, 7)
        Me.INDlycBase.Name = "INDlycBase"
        Me.INDlycBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(434, 310, 1201, 569)
        Me.INDlycBase.Root = Me.INDlycBaseUnitDoseType
        Me.INDlycBase.Size = New System.Drawing.Size(1267, 592)
        Me.INDlycBase.TabIndex = 3
        Me.INDlycBase.Text = "LayoutControl1"
        '
        'INDsleddlDosis
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleddlDosis, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleddlDosis, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleddlDosis, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleddlDosis, False)
        Me.INDsleddlDosis.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleddlDosis, False)
        Me.INDsleddlDosis.Location = New System.Drawing.Point(543, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleddlDosis, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleddlDosis.Name = "INDsleddlDosis"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleddlDosis, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleddlDosis, False)
        Me.INDsleddlDosis.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleddlDosis.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleddlDosis.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleddlDosis.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleddlDosis.Properties.Appearance.Options.UseFont = True
        Me.INDsleddlDosis.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleddlDosis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleddlDosis.Properties.DisplayMember = "CodeDescription"
        Me.INDsleddlDosis.Properties.NullText = ""
        Me.INDsleddlDosis.Properties.PopupSizeable = False
        Me.INDsleddlDosis.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleddlDosis.Properties.ShowFooter = False
        Me.INDsleddlDosis.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleddlDosis, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleddlDosis, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleddlDosis, True)
        Me.INDsleddlDosis.Size = New System.Drawing.Size(261, 28)
        Me.INDsleddlDosis.StyleController = Me.INDlycBase
        Me.INDsleddlDosis.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleddlDosis, "2067")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleddlDosis, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleddlDosis, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleddlDosis, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleddlDosis, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn12})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 355
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Descripción"
        Me.GridColumn12.FieldName = "Description"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 1
        Me.GridColumn12.Width = 1027
        '
        'INDSmbScheduleException
        '
        Me.INDSmbScheduleException.Location = New System.Drawing.Point(1311, 213)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSmbScheduleException, False)
        Me.INDSmbScheduleException.Name = "INDSmbScheduleException"
        Me.INDSmbScheduleException.Size = New System.Drawing.Size(386, 28)
        Me.INDSmbScheduleException.StyleController = Me.INDlycBase
        Me.INDSmbScheduleException.TabIndex = 21
        Me.INDSmbScheduleException.Text = "Agregar"
        '
        'INDGcScheduleException
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcScheduleException, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcScheduleException, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcScheduleException, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcScheduleException, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcScheduleException, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcScheduleException, False)
        Me.INDGcScheduleException.Location = New System.Drawing.Point(1311, 245)
        Me.INDGcScheduleException.MainView = Me.INDGvScheduleException
        Me.INDGcScheduleException.Name = "INDGcScheduleException"
        Me.INDGcScheduleException.Size = New System.Drawing.Size(386, 388)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcScheduleException, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcScheduleException.TabIndex = 22
        Me.INDGcScheduleException.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvScheduleException})
        '
        'INDGvScheduleException
        '
        Me.INDGvScheduleException.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvScheduleException.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvScheduleException.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvScheduleException.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvScheduleException.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvScheduleException.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvScheduleException.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvScheduleException.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvScheduleException.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvScheduleException.Appearance.Row.Options.UseFont = True
        Me.INDGvScheduleException.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvScheduleException.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvScheduleException.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10, Me.GridColumn11})
        Me.INDGvScheduleException.GridControl = Me.INDGcScheduleException
        Me.INDGvScheduleException.Name = "INDGvScheduleException"
        Me.INDGvScheduleException.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvScheduleException.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvScheduleException.OptionsView.ShowAutoFilterRow = True
        Me.INDGvScheduleException.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvScheduleException, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvScheduleException, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvScheduleException, False)
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Fecha"
        Me.GridColumn10.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn10.FieldName = "StopDate"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.OptionsColumn.AllowMove = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 0
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Motivo"
        Me.GridColumn11.FieldName = "ReasonForStop"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.OptionsColumn.AllowMove = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        '
        'INDMmeReasonForStop
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmeReasonForStop, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmeReasonForStop, False)
        Me.INDMmeReasonForStop.EnterMoveNextControl = True
        Me.INDMmeReasonForStop.Location = New System.Drawing.Point(1311, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmeReasonForStop, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMmeReasonForStop.Name = "INDMmeReasonForStop"
        Me.INDMmeReasonForStop.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMmeReasonForStop.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmeReasonForStop.Properties.Appearance.Options.UseBackColor = True
        Me.INDMmeReasonForStop.Properties.Appearance.Options.UseFont = True
        Me.INDMmeReasonForStop.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmeReasonForStop.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMmeReasonForStop.Properties.LinesCount = 4
        Me.INDMmeReasonForStop.Properties.MaxLength = 200
        Me.INDMmeReasonForStop.Size = New System.Drawing.Size(386, 66)
        Me.INDMmeReasonForStop.StyleController = Me.INDlycBase
        Me.INDMmeReasonForStop.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMmeReasonForStop, 10)
        '
        'INDDteStopDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteStopDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteStopDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteStopDate, False)
        Me.INDDteStopDate.EditValue = Nothing
        Me.INDDteStopDate.EnterMoveNextControl = True
        Me.INDDteStopDate.Location = New System.Drawing.Point(1311, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteStopDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteStopDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteStopDate.Name = "INDDteStopDate"
        Me.INDDteStopDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDteStopDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteStopDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteStopDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteStopDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteStopDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteStopDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteStopDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteStopDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDteStopDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteStopDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteStopDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDteStopDate.StyleController = Me.INDlycBase
        Me.INDDteStopDate.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteStopDate, 0)
        '
        'INDGcSchedules
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcSchedules, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcSchedules, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcSchedules, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcSchedules, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcSchedules, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcSchedules, False)
        Me.INDGcSchedules.Location = New System.Drawing.Point(907, 159)
        Me.INDGcSchedules.MainView = Me.INDGvSchedules
        Me.INDGcSchedules.Name = "INDGcSchedules"
        Me.INDGcSchedules.Size = New System.Drawing.Size(376, 474)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcSchedules, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcSchedules.TabIndex = 18
        Me.INDGcSchedules.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvSchedules})
        '
        'INDGvSchedules
        '
        Me.INDGvSchedules.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvSchedules.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvSchedules.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvSchedules.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvSchedules.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSchedules.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvSchedules.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSchedules.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSchedules.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvSchedules.Appearance.Row.Options.UseFont = True
        Me.INDGvSchedules.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvSchedules.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvSchedules.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.INDGvSchedules.GridControl = Me.INDGcSchedules
        Me.INDGvSchedules.Name = "INDGvSchedules"
        Me.INDGvSchedules.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSchedules.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSchedules.OptionsView.ShowAutoFilterRow = True
        Me.INDGvSchedules.OptionsView.ShowGroupPanel = False
        Me.INDGvSchedules.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn9, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn7, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvSchedules, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvSchedules, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvSchedules, False)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Día"
        Me.GridColumn6.FieldName = "DayName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.OptionsColumn.AllowMove = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Hora Inicial"
        Me.GridColumn7.DisplayFormat.FormatString = "HH:mm:ss"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn7.FieldName = "StartTime"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.OptionsColumn.AllowMove = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Hora Final"
        Me.GridColumn8.DisplayFormat.FormatString = "HH:mm:ss"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn8.FieldName = "EndTime"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.OptionsColumn.AllowMove = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 2
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Id Día "
        Me.GridColumn9.FieldName = "DayId"
        Me.GridColumn9.Name = "GridColumn9"
        '
        'INDSmbAddSchedule
        '
        Me.INDSmbAddSchedule.Location = New System.Drawing.Point(1198, 122)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSmbAddSchedule, False)
        Me.INDSmbAddSchedule.Name = "INDSmbAddSchedule"
        Me.INDSmbAddSchedule.Size = New System.Drawing.Size(81, 28)
        Me.INDSmbAddSchedule.StyleController = Me.INDlycBase
        Me.INDSmbAddSchedule.TabIndex = 9
        Me.INDSmbAddSchedule.Text = "Agregar"
        '
        'INDTmeEndTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTmeEndTime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTmeEndTime, False)
        Me.INDTmeEndTime.EditValue = New Date(2020, 1, 27, 0, 0, 0, 0)
        Me.INDTmeEndTime.EnterMoveNextControl = True
        Me.INDTmeEndTime.Location = New System.Drawing.Point(1197, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDTmeEndTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTmeEndTime.Name = "INDTmeEndTime"
        Me.INDTmeEndTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTmeEndTime.Properties.Appearance.Options.UseFont = True
        Me.INDTmeEndTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTmeEndTime.Properties.Mask.EditMask = "HH:mm:ss"
        Me.INDTmeEndTime.Properties.MaxLength = 8
        Me.INDTmeEndTime.Size = New System.Drawing.Size(86, 28)
        Me.INDTmeEndTime.StyleController = Me.INDlycBase
        Me.INDTmeEndTime.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTmeEndTime, 0)
        '
        'INDChkFestivo
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkFestivo, False)
        Me.INDChkFestivo.EnterMoveNextControl = True
        Me.INDChkFestivo.Location = New System.Drawing.Point(1155, 137)
        Me.INDChkFestivo.Name = "INDChkFestivo"
        Me.INDChkFestivo.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkFestivo.Properties.Appearance.Options.UseFont = True
        Me.INDChkFestivo.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkFestivo.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkFestivo.Properties.Caption = ""
        Me.INDChkFestivo.Size = New System.Drawing.Size(41, 20)
        Me.INDChkFestivo.StyleController = Me.INDlycBase
        Me.INDChkFestivo.TabIndex = 17
        '
        'INDChkDomingo
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkDomingo, False)
        Me.INDChkDomingo.EnterMoveNextControl = True
        Me.INDChkDomingo.Location = New System.Drawing.Point(1117, 137)
        Me.INDChkDomingo.Name = "INDChkDomingo"
        Me.INDChkDomingo.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkDomingo.Properties.Appearance.Options.UseFont = True
        Me.INDChkDomingo.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkDomingo.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkDomingo.Properties.Caption = ""
        Me.INDChkDomingo.Size = New System.Drawing.Size(34, 20)
        Me.INDChkDomingo.StyleController = Me.INDlycBase
        Me.INDChkDomingo.TabIndex = 16
        '
        'INDChkSabado
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkSabado, False)
        Me.INDChkSabado.EnterMoveNextControl = True
        Me.INDChkSabado.Location = New System.Drawing.Point(1082, 137)
        Me.INDChkSabado.Name = "INDChkSabado"
        Me.INDChkSabado.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkSabado.Properties.Appearance.Options.UseFont = True
        Me.INDChkSabado.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkSabado.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkSabado.Properties.Caption = ""
        Me.INDChkSabado.Size = New System.Drawing.Size(31, 20)
        Me.INDChkSabado.StyleController = Me.INDlycBase
        Me.INDChkSabado.TabIndex = 15
        '
        'INDChkViernes
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkViernes, False)
        Me.INDChkViernes.EnterMoveNextControl = True
        Me.INDChkViernes.Location = New System.Drawing.Point(1047, 137)
        Me.INDChkViernes.Name = "INDChkViernes"
        Me.INDChkViernes.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkViernes.Properties.Appearance.Options.UseFont = True
        Me.INDChkViernes.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkViernes.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkViernes.Properties.Caption = ""
        Me.INDChkViernes.Size = New System.Drawing.Size(31, 20)
        Me.INDChkViernes.StyleController = Me.INDlycBase
        Me.INDChkViernes.TabIndex = 14
        '
        'INDChkJueves
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkJueves, False)
        Me.INDChkJueves.EnterMoveNextControl = True
        Me.INDChkJueves.Location = New System.Drawing.Point(1012, 137)
        Me.INDChkJueves.Name = "INDChkJueves"
        Me.INDChkJueves.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkJueves.Properties.Appearance.Options.UseFont = True
        Me.INDChkJueves.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkJueves.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkJueves.Properties.Caption = ""
        Me.INDChkJueves.Size = New System.Drawing.Size(31, 20)
        Me.INDChkJueves.StyleController = Me.INDlycBase
        Me.INDChkJueves.TabIndex = 13
        '
        'INDTmeStartTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTmeStartTime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTmeStartTime, False)
        Me.INDTmeStartTime.EditValue = New Date(2020, 1, 24, 0, 0, 0, 0)
        Me.INDTmeStartTime.EnterMoveNextControl = True
        Me.INDTmeStartTime.Location = New System.Drawing.Point(994, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDTmeStartTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTmeStartTime.Name = "INDTmeStartTime"
        Me.INDTmeStartTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTmeStartTime.Properties.Appearance.Options.UseFont = True
        Me.INDTmeStartTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTmeStartTime.Properties.Mask.EditMask = "HH:mm:ss"
        Me.INDTmeStartTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTmeStartTime.Properties.MaxLength = 8
        Me.INDTmeStartTime.Size = New System.Drawing.Size(89, 28)
        Me.INDTmeStartTime.StyleController = Me.INDlycBase
        Me.INDTmeStartTime.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTmeStartTime, 0)
        '
        'INDChkMartes
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkMartes, False)
        Me.INDChkMartes.EnterMoveNextControl = True
        Me.INDChkMartes.Location = New System.Drawing.Point(942, 137)
        Me.INDChkMartes.Name = "INDChkMartes"
        Me.INDChkMartes.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkMartes.Properties.Appearance.Options.UseFont = True
        Me.INDChkMartes.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkMartes.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkMartes.Properties.Caption = ""
        Me.INDChkMartes.Size = New System.Drawing.Size(31, 20)
        Me.INDChkMartes.StyleController = Me.INDlycBase
        Me.INDChkMartes.TabIndex = 11
        '
        'INDChkLunes
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkLunes, False)
        Me.INDChkLunes.EnterMoveNextControl = True
        Me.INDChkLunes.Location = New System.Drawing.Point(907, 137)
        Me.INDChkLunes.Name = "INDChkLunes"
        Me.INDChkLunes.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkLunes.Properties.Appearance.Options.UseFont = True
        Me.INDChkLunes.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkLunes.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkLunes.Properties.Caption = ""
        Me.INDChkLunes.Size = New System.Drawing.Size(31, 20)
        Me.INDChkLunes.StyleController = Me.INDlycBase
        Me.INDChkLunes.TabIndex = 10
        '
        'INDGleTime24
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleTime24, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleTime24, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleTime24, True)
        Me.INDGleTime24.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleTime24, True)
        Me.INDGleTime24.Location = New System.Drawing.Point(1089, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleTime24, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleTime24.Name = "INDGleTime24"
        Me.INDGleTime24.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleTime24.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleTime24.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDGleTime24.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleTime24.Properties.Appearance.Options.UseFont = True
        Me.INDGleTime24.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleTime24.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTime24.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTime24.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleTime24.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleTime24.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleTime24.Properties.DisplayMember = "Item2"
        Me.INDGleTime24.Properties.ImmediatePopup = True
        Me.INDGleTime24.Properties.NullText = ""
        Me.INDGleTime24.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleTime24.Properties.ValueMember = "Item1"
        Me.INDGleTime24.Size = New System.Drawing.Size(194, 28)
        Me.INDGleTime24.StyleController = Me.INDlycBase
        Me.INDGleTime24.TabIndex = 6
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleTime24, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleTime24, 0)
        Me.INDGleTime24.ToolTip = "Este Campo es Necesario"
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "24 Horas"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'INDsleddlUnidad
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleddlUnidad, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleddlUnidad, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleddlUnidad, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleddlUnidad, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleddlUnidad, False)
        Me.INDsleddlUnidad.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleddlUnidad, False)
        Me.INDsleddlUnidad.Location = New System.Drawing.Point(24, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleddlUnidad, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleddlUnidad.MaximumSize = New System.Drawing.Size(386, 0)
        Me.INDsleddlUnidad.Name = "INDsleddlUnidad"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleddlUnidad, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleddlUnidad, False)
        Me.INDsleddlUnidad.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleddlUnidad.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleddlUnidad.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleddlUnidad.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleddlUnidad.Properties.Appearance.Options.UseFont = True
        Me.INDsleddlUnidad.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleddlUnidad.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleddlUnidad.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleddlUnidad.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleddlUnidad.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleddlUnidad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleddlUnidad.Properties.DisplayMember = "Descripcion"
        Me.INDsleddlUnidad.Properties.NullText = ""
        Me.INDsleddlUnidad.Properties.PopupSizeable = False
        Me.INDsleddlUnidad.Properties.PopupView = Me.GridView3
        Me.INDsleddlUnidad.Properties.ShowFooter = False
        Me.INDsleddlUnidad.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleddlUnidad, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleddlUnidad, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleddlUnidad, True)
        Me.INDsleddlUnidad.Size = New System.Drawing.Size(376, 28)
        Me.INDsleddlUnidad.StyleController = Me.INDlycBase
        Me.INDsleddlUnidad.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleddlUnidad, "523")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleddlUnidad, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleddlUnidad, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleddlUnidad, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleddlUnidad, False)
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView3, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.GridView3, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Unidad Funcional"
        Me.GridColumn4.FieldName = "Descripcion"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'INDtxtNombre
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtNombre, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtNombre, False)
        Me.INDtxtNombre.EnterMoveNextControl = True
        Me.INDtxtNombre.Location = New System.Drawing.Point(24, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtNombre, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtNombre.Name = "INDtxtNombre"
        Me.INDtxtNombre.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtNombre.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtNombre.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtNombre.Properties.Appearance.Options.UseFont = True
        Me.INDtxtNombre.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtNombre.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtNombre.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtNombre.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtNombre.Properties.MaxLength = 40
        Me.INDtxtNombre.Size = New System.Drawing.Size(376, 28)
        Me.INDtxtNombre.StyleController = Me.INDlycBase
        Me.INDtxtNombre.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtNombre, 0)
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.MixingStation.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.Mask.EditMask = "[0-9]+"
        Me.INDbtnCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDbtnCode.Size = New System.Drawing.Size(376, 28)
        Me.INDbtnCode.StyleController = Me.INDlycBase
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDgcUnitDoseType
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcUnitDoseType, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcUnitDoseType, Nothing)
        Me.INDgcUnitDoseType.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcUnitDoseType, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcUnitDoseType, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcUnitDoseType, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcUnitDoseType, False)
        Me.INDgcUnitDoseType.Location = New System.Drawing.Point(428, 89)
        Me.INDgcUnitDoseType.MainView = Me.INDviewUnitDosesType
        Me.INDgcUnitDoseType.Name = "INDgcUnitDoseType"
        Me.INDgcUnitDoseType.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonEdit1})
        Me.INDgcUnitDoseType.Size = New System.Drawing.Size(451, 544)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcUnitDoseType, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcUnitDoseType.TabIndex = 5
        Me.INDgcUnitDoseType.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewUnitDosesType})
        '
        'INDviewUnitDosesType
        '
        Me.INDviewUnitDosesType.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewUnitDosesType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewUnitDosesType.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewUnitDosesType.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDviewUnitDosesType.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewUnitDosesType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewUnitDosesType.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewUnitDosesType.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewUnitDosesType.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewUnitDosesType.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewUnitDosesType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewUnitDosesType.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewUnitDosesType.Appearance.Row.Options.UseFont = True
        Me.INDviewUnitDosesType.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewUnitDosesType.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewUnitDosesType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn2})
        Me.INDviewUnitDosesType.GridControl = Me.INDgcUnitDoseType
        Me.INDviewUnitDosesType.Name = "INDviewUnitDosesType"
        Me.INDviewUnitDosesType.OptionsCustomization.AllowGroup = False
        Me.INDviewUnitDosesType.OptionsCustomization.AllowSort = False
        Me.INDviewUnitDosesType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewUnitDosesType.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewUnitDosesType.OptionsView.ShowAutoFilterRow = True
        Me.INDviewUnitDosesType.OptionsView.ShowGroupPanel = False
        Me.INDviewUnitDosesType.Tag = 137
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewUnitDosesType, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDviewUnitDosesType, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewUnitDosesType, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "UnitDoseTypeCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 116
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Descripción"
        Me.GridColumn2.FieldName = "UnitDoseTypeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 317
        '
        'RepositoryItemButtonEdit1
        '
        Me.RepositoryItemButtonEdit1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Underline)
        Me.RepositoryItemButtonEdit1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.RepositoryItemButtonEdit1.Appearance.Options.UseFont = True
        Me.RepositoryItemButtonEdit1.Appearance.Options.UseForeColor = True
        Me.RepositoryItemButtonEdit1.Appearance.Options.UseTextOptions = True
        Me.RepositoryItemButtonEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemButtonEdit1.AutoHeight = False
        Me.RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEdit1"
        Me.RepositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'INDbtnAdd
        '
        Me.INDbtnAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAdd.Appearance.Options.UseFont = True
        Me.INDbtnAdd.Location = New System.Drawing.Point(808, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAdd, True)
        Me.INDbtnAdd.Name = "INDbtnAdd"
        Me.INDbtnAdd.Size = New System.Drawing.Size(71, 28)
        Me.INDbtnAdd.StyleController = Me.INDlycBase
        Me.INDbtnAdd.TabIndex = 4
        Me.INDbtnAdd.Text = "Agregar"
        '
        'INDChkMiercoles
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDChkMiercoles, False)
        Me.INDChkMiercoles.EnterMoveNextControl = True
        Me.INDChkMiercoles.Location = New System.Drawing.Point(977, 137)
        Me.INDChkMiercoles.Name = "INDChkMiercoles"
        Me.INDChkMiercoles.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkMiercoles.Properties.Appearance.Options.UseFont = True
        Me.INDChkMiercoles.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDChkMiercoles.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDChkMiercoles.Properties.Caption = ""
        Me.INDChkMiercoles.Size = New System.Drawing.Size(31, 20)
        Me.INDChkMiercoles.StyleController = Me.INDlycBase
        Me.INDChkMiercoles.TabIndex = 12
        '
        'INDlycBaseUnitDoseType
        '
        Me.INDlycBaseUnitDoseType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycBaseUnitDoseType.AppearanceGroup.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycBaseUnitDoseType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycBaseUnitDoseType, False)
        Me.INDlycBaseUnitDoseType.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycBaseUnitDoseType.GroupBordersVisible = False
        Me.INDlycBaseUnitDoseType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrUnitDoseType, Me.LayoutControlGroup2, Me.INDLcgSchedule, Me.INDLcgScheduleException})
        Me.INDlycBaseUnitDoseType.Name = "Root"
        Me.INDlycBaseUnitDoseType.Size = New System.Drawing.Size(1721, 657)
        Me.INDlycBaseUnitDoseType.TextVisible = False
        '
        'INDlyGrUnitDoseType
        '
        Me.INDlyGrUnitDoseType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrUnitDoseType.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrUnitDoseType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrUnitDoseType, False)
        Me.INDlyGrUnitDoseType.CustomizationFormText = "Datos Principales"
        Me.INDlyGrUnitDoseType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDLciFunctionalUnit, Me.INDlyItemDescription})
        Me.INDlyGrUnitDoseType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrUnitDoseType.Name = "INDlyGrUnitDoseType"
        Me.INDlyGrUnitDoseType.Size = New System.Drawing.Size(404, 637)
        Me.INDlyGrUnitDoseType.Text = "Datos Principales"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(380, 64)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(208, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDLciFunctionalUnit
        '
        Me.INDLciFunctionalUnit.Control = Me.INDsleddlUnidad
        Me.INDLciFunctionalUnit.Location = New System.Drawing.Point(0, 128)
        Me.INDLciFunctionalUnit.MaxSize = New System.Drawing.Size(380, 64)
        Me.INDLciFunctionalUnit.MinSize = New System.Drawing.Size(380, 64)
        Me.INDLciFunctionalUnit.Name = "INDLciFunctionalUnit"
        Me.INDLciFunctionalUnit.Size = New System.Drawing.Size(380, 456)
        Me.INDLciFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFunctionalUnit.Text = "Unidad Funcional"
        Me.INDLciFunctionalUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFunctionalUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFunctionalUnit.TextSize = New System.Drawing.Size(208, 21)
        Me.INDLciFunctionalUnit.TextToControlDistance = 5
        Me.INDLciFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.Control = Me.INDtxtNombre
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemDescription.MaxSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemDescription.MinSize = New System.Drawing.Size(380, 64)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.Size = New System.Drawing.Size(380, 64)
        Me.INDlyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDescription.Text = "Nombre de Línea de Producción"
        Me.INDlyItemDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(208, 21)
        Me.INDlyItemDescription.TextToControlDistance = 5
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "Tipos de Dosis unitarias"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciUnitDoseType, Me.LayoutControlItem5, Me.LayoutControlItem2})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(404, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(479, 637)
        Me.LayoutControlGroup2.Text = "Tipos de Dosis unitarias"
        '
        'INDLciUnitDoseType
        '
        Me.INDLciUnitDoseType.Control = Me.INDgcUnitDoseType
        Me.INDLciUnitDoseType.CustomizationFormText = "Dosis unitarias"
        Me.INDLciUnitDoseType.Location = New System.Drawing.Point(0, 36)
        Me.INDLciUnitDoseType.MaxSize = New System.Drawing.Size(455, 0)
        Me.INDLciUnitDoseType.MinSize = New System.Drawing.Size(455, 461)
        Me.INDLciUnitDoseType.Name = "INDLciUnitDoseType"
        Me.INDLciUnitDoseType.Size = New System.Drawing.Size(455, 548)
        Me.INDLciUnitDoseType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciUnitDoseType.Text = "Dosis unitarias"
        Me.INDLciUnitDoseType.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciUnitDoseType.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDbtnAdd
        Me.LayoutControlItem5.CustomizationFormText = "Agregar Dosis unitarias"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(380, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(75, 32)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(75, 32)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(75, 36)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Agregar Dosis unitarias"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDsleddlDosis
        Me.LayoutControlItem2.CustomizationFormText = "Tipos Dosis Unitarias"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(380, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(380, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(380, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Dosis Unitarias"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem2.TextToControlDistance = 0
        '
        'INDLcgSchedule
        '
        Me.INDLcgSchedule.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgSchedule.AppearanceGroup.Options.UseFont = True
        Me.INDLcgSchedule.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgSchedule.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgSchedule.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgSchedule.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgSchedule.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgSchedule.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgSchedule.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgSchedule.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgSchedule.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgSchedule.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgSchedule.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgSchedule.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgSchedule, False)
        Me.INDLcgSchedule.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciTime24, Me.INDLciLunes, Me.INDLciMartes, Me.INDLciMiercoles, Me.INDLciJueves, Me.INDLciViernes, Me.INDLciSabado, Me.INDLciDomingo, Me.INDLciFestivo, Me.INDLciStartTime, Me.INDLciEndTime, Me.INDLciAddSchedule, Me.INDLciSchedules})
        Me.INDLcgSchedule.Location = New System.Drawing.Point(883, 0)
        Me.INDLcgSchedule.Name = "INDLcgSchedule"
        Me.INDLcgSchedule.Size = New System.Drawing.Size(404, 637)
        Me.INDLcgSchedule.Text = "Horarios"
        '
        'INDLciTime24
        '
        Me.INDLciTime24.AllowHide = False
        Me.INDLciTime24.Control = Me.INDGleTime24
        Me.INDLciTime24.Location = New System.Drawing.Point(0, 0)
        Me.INDLciTime24.MaxSize = New System.Drawing.Size(380, 32)
        Me.INDLciTime24.MinSize = New System.Drawing.Size(380, 32)
        Me.INDLciTime24.Name = "INDLciTime24"
        Me.INDLciTime24.Size = New System.Drawing.Size(380, 32)
        Me.INDLciTime24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTime24.Text = "Labora 24 Horas"
        Me.INDLciTime24.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTime24.TextSize = New System.Drawing.Size(172, 17)
        Me.INDLciTime24.TextToControlDistance = 10
        '
        'INDLciLunes
        '
        Me.INDLciLunes.Control = Me.INDChkLunes
        Me.INDLciLunes.Location = New System.Drawing.Point(0, 64)
        Me.INDLciLunes.MaxSize = New System.Drawing.Size(35, 42)
        Me.INDLciLunes.MinSize = New System.Drawing.Size(35, 42)
        Me.INDLciLunes.Name = "INDLciLunes"
        Me.INDLciLunes.Size = New System.Drawing.Size(35, 42)
        Me.INDLciLunes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciLunes.Text = "LUN"
        Me.INDLciLunes.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciLunes.TextSize = New System.Drawing.Size(33, 17)
        '
        'INDLciMartes
        '
        Me.INDLciMartes.Control = Me.INDChkMartes
        Me.INDLciMartes.Location = New System.Drawing.Point(35, 64)
        Me.INDLciMartes.MaxSize = New System.Drawing.Size(35, 42)
        Me.INDLciMartes.MinSize = New System.Drawing.Size(35, 42)
        Me.INDLciMartes.Name = "INDLciMartes"
        Me.INDLciMartes.Size = New System.Drawing.Size(35, 42)
        Me.INDLciMartes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMartes.Text = "MAR"
        Me.INDLciMartes.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciMartes.TextSize = New System.Drawing.Size(33, 17)
        '
        'INDLciMiercoles
        '
        Me.INDLciMiercoles.Control = Me.INDChkMiercoles
        Me.INDLciMiercoles.CustomizationFormText = "MIE"
        Me.INDLciMiercoles.Location = New System.Drawing.Point(70, 64)
        Me.INDLciMiercoles.MaxSize = New System.Drawing.Size(35, 42)
        Me.INDLciMiercoles.MinSize = New System.Drawing.Size(35, 42)
        Me.INDLciMiercoles.Name = "INDLciMiercoles"
        Me.INDLciMiercoles.Size = New System.Drawing.Size(35, 42)
        Me.INDLciMiercoles.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMiercoles.Text = "MIE"
        Me.INDLciMiercoles.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciMiercoles.TextSize = New System.Drawing.Size(33, 17)
        '
        'INDLciJueves
        '
        Me.INDLciJueves.Control = Me.INDChkJueves
        Me.INDLciJueves.Location = New System.Drawing.Point(105, 64)
        Me.INDLciJueves.MaxSize = New System.Drawing.Size(35, 42)
        Me.INDLciJueves.MinSize = New System.Drawing.Size(35, 42)
        Me.INDLciJueves.Name = "INDLciJueves"
        Me.INDLciJueves.Size = New System.Drawing.Size(35, 42)
        Me.INDLciJueves.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciJueves.Text = "JUE"
        Me.INDLciJueves.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciJueves.TextSize = New System.Drawing.Size(33, 17)
        '
        'INDLciViernes
        '
        Me.INDLciViernes.Control = Me.INDChkViernes
        Me.INDLciViernes.Location = New System.Drawing.Point(140, 64)
        Me.INDLciViernes.MaxSize = New System.Drawing.Size(35, 42)
        Me.INDLciViernes.MinSize = New System.Drawing.Size(35, 42)
        Me.INDLciViernes.Name = "INDLciViernes"
        Me.INDLciViernes.Size = New System.Drawing.Size(35, 42)
        Me.INDLciViernes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciViernes.Text = "VIE"
        Me.INDLciViernes.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciViernes.TextSize = New System.Drawing.Size(33, 17)
        '
        'INDLciSabado
        '
        Me.INDLciSabado.Control = Me.INDChkSabado
        Me.INDLciSabado.Location = New System.Drawing.Point(175, 64)
        Me.INDLciSabado.MaxSize = New System.Drawing.Size(35, 42)
        Me.INDLciSabado.MinSize = New System.Drawing.Size(35, 42)
        Me.INDLciSabado.Name = "INDLciSabado"
        Me.INDLciSabado.Size = New System.Drawing.Size(35, 42)
        Me.INDLciSabado.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSabado.Text = "SAB"
        Me.INDLciSabado.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciSabado.TextSize = New System.Drawing.Size(33, 17)
        '
        'INDLciDomingo
        '
        Me.INDLciDomingo.Control = Me.INDChkDomingo
        Me.INDLciDomingo.Location = New System.Drawing.Point(210, 64)
        Me.INDLciDomingo.MaxSize = New System.Drawing.Size(38, 42)
        Me.INDLciDomingo.MinSize = New System.Drawing.Size(38, 42)
        Me.INDLciDomingo.Name = "INDLciDomingo"
        Me.INDLciDomingo.Size = New System.Drawing.Size(38, 42)
        Me.INDLciDomingo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDomingo.Text = "DOM"
        Me.INDLciDomingo.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDomingo.TextSize = New System.Drawing.Size(33, 17)
        '
        'INDLciFestivo
        '
        Me.INDLciFestivo.Control = Me.INDChkFestivo
        Me.INDLciFestivo.Location = New System.Drawing.Point(248, 64)
        Me.INDLciFestivo.MaxSize = New System.Drawing.Size(45, 42)
        Me.INDLciFestivo.MinSize = New System.Drawing.Size(45, 42)
        Me.INDLciFestivo.Name = "INDLciFestivo"
        Me.INDLciFestivo.Size = New System.Drawing.Size(45, 42)
        Me.INDLciFestivo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFestivo.Text = "FES"
        Me.INDLciFestivo.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFestivo.TextSize = New System.Drawing.Size(33, 17)
        '
        'INDLciStartTime
        '
        Me.INDLciStartTime.Control = Me.INDTmeStartTime
        Me.INDLciStartTime.Location = New System.Drawing.Point(0, 32)
        Me.INDLciStartTime.MaxSize = New System.Drawing.Size(180, 32)
        Me.INDLciStartTime.MinSize = New System.Drawing.Size(180, 32)
        Me.INDLciStartTime.Name = "INDLciStartTime"
        Me.INDLciStartTime.Size = New System.Drawing.Size(210, 32)
        Me.INDLciStartTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStartTime.Text = "Hora Inicial"
        Me.INDLciStartTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciStartTime.TextSize = New System.Drawing.Size(82, 17)
        Me.INDLciStartTime.TextToControlDistance = 5
        Me.INDLciStartTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciEndTime
        '
        Me.INDLciEndTime.Control = Me.INDTmeEndTime
        Me.INDLciEndTime.Location = New System.Drawing.Point(210, 32)
        Me.INDLciEndTime.MaxSize = New System.Drawing.Size(170, 32)
        Me.INDLciEndTime.MinSize = New System.Drawing.Size(170, 32)
        Me.INDLciEndTime.Name = "INDLciEndTime"
        Me.INDLciEndTime.Size = New System.Drawing.Size(170, 32)
        Me.INDLciEndTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEndTime.Text = "Hora Final"
        Me.INDLciEndTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEndTime.TextSize = New System.Drawing.Size(75, 17)
        Me.INDLciEndTime.TextToControlDistance = 5
        Me.INDLciEndTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciAddSchedule
        '
        Me.INDLciAddSchedule.Control = Me.INDSmbAddSchedule
        Me.INDLciAddSchedule.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDLciAddSchedule.CustomizationFormText = "Agregar Horario"
        Me.INDLciAddSchedule.Location = New System.Drawing.Point(293, 64)
        Me.INDLciAddSchedule.MaxSize = New System.Drawing.Size(85, 42)
        Me.INDLciAddSchedule.MinSize = New System.Drawing.Size(85, 42)
        Me.INDLciAddSchedule.Name = "INDLciAddSchedule"
        Me.INDLciAddSchedule.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 4, 7, 7)
        Me.INDLciAddSchedule.Size = New System.Drawing.Size(87, 42)
        Me.INDLciAddSchedule.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAddSchedule.Text = "Agregar Horario"
        Me.INDLciAddSchedule.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAddSchedule.TextVisible = False
        '
        'INDLciSchedules
        '
        Me.INDLciSchedules.Control = Me.INDGcSchedules
        Me.INDLciSchedules.CustomizationFormText = "Horarios"
        Me.INDLciSchedules.Location = New System.Drawing.Point(0, 106)
        Me.INDLciSchedules.MaxSize = New System.Drawing.Size(380, 0)
        Me.INDLciSchedules.MinSize = New System.Drawing.Size(380, 387)
        Me.INDLciSchedules.Name = "INDLciSchedules"
        Me.INDLciSchedules.Size = New System.Drawing.Size(380, 478)
        Me.INDLciSchedules.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSchedules.Text = "Horarios"
        Me.INDLciSchedules.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciSchedules.TextVisible = False
        Me.INDLciSchedules.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLcgScheduleException
        '
        Me.INDLcgScheduleException.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgScheduleException.AppearanceGroup.Options.UseFont = True
        Me.INDLcgScheduleException.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgScheduleException.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgScheduleException.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgScheduleException.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgScheduleException.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgScheduleException.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgScheduleException.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgScheduleException.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgScheduleException.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgScheduleException.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgScheduleException.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgScheduleException.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgScheduleException, False)
        Me.INDLcgScheduleException.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciScheduleException, Me.INDLciReasonForStop, Me.INDLciStopDate, Me.INDLciAddScheduleException})
        Me.INDLcgScheduleException.Location = New System.Drawing.Point(1287, 0)
        Me.INDLcgScheduleException.Name = "INDLcgScheduleException"
        Me.INDLcgScheduleException.Size = New System.Drawing.Size(414, 637)
        Me.INDLcgScheduleException.Text = "Excepciones"
        '
        'INDLciScheduleException
        '
        Me.INDLciScheduleException.Control = Me.INDGcScheduleException
        Me.INDLciScheduleException.CustomizationFormText = "Calendario de Excepciones"
        Me.INDLciScheduleException.Location = New System.Drawing.Point(0, 192)
        Me.INDLciScheduleException.MaxSize = New System.Drawing.Size(390, 0)
        Me.INDLciScheduleException.MinSize = New System.Drawing.Size(390, 392)
        Me.INDLciScheduleException.Name = "INDLciScheduleException"
        Me.INDLciScheduleException.Size = New System.Drawing.Size(390, 392)
        Me.INDLciScheduleException.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciScheduleException.Text = "Calendario de Excepciones"
        Me.INDLciScheduleException.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciScheduleException.TextVisible = False
        '
        'INDLciReasonForStop
        '
        Me.INDLciReasonForStop.Control = Me.INDMmeReasonForStop
        Me.INDLciReasonForStop.Location = New System.Drawing.Point(0, 64)
        Me.INDLciReasonForStop.MaxSize = New System.Drawing.Size(390, 96)
        Me.INDLciReasonForStop.MinSize = New System.Drawing.Size(390, 96)
        Me.INDLciReasonForStop.Name = "INDLciReasonForStop"
        Me.INDLciReasonForStop.Size = New System.Drawing.Size(390, 96)
        Me.INDLciReasonForStop.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReasonForStop.Text = "Motivo"
        Me.INDLciReasonForStop.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciReasonForStop.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciReasonForStop.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciReasonForStop.TextToControlDistance = 5
        '
        'INDLciStopDate
        '
        Me.INDLciStopDate.Control = Me.INDDteStopDate
        Me.INDLciStopDate.Location = New System.Drawing.Point(0, 0)
        Me.INDLciStopDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciStopDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciStopDate.Name = "INDLciStopDate"
        Me.INDLciStopDate.Size = New System.Drawing.Size(390, 64)
        Me.INDLciStopDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStopDate.Text = "Fecha"
        Me.INDLciStopDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciStopDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciStopDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciStopDate.TextToControlDistance = 5
        '
        'INDLciAddScheduleException
        '
        Me.INDLciAddScheduleException.Control = Me.INDSmbScheduleException
        Me.INDLciAddScheduleException.CustomizationFormText = "Agregar Excepciones"
        Me.INDLciAddScheduleException.Location = New System.Drawing.Point(0, 160)
        Me.INDLciAddScheduleException.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciAddScheduleException.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciAddScheduleException.Name = "INDLciAddScheduleException"
        Me.INDLciAddScheduleException.Size = New System.Drawing.Size(390, 32)
        Me.INDLciAddScheduleException.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAddScheduleException.Text = "Agregar Excepciones"
        Me.INDLciAddScheduleException.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAddScheduleException.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
        '
        'IndigoGridView2
        '
        Me.IndigoGridView2.RaiseMenuPopUp = True
        Me.IndigoGridView2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView3
        '
        Me.IndigoGridView3.RaiseMenuPopUp = True
        Me.IndigoGridView3.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'FrmProductionLine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1471, 736)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmProductionLine"
        Me.Opacity = 1.0R
        Me.Tag = "2073"
        Me.Text = "Configuración Líneas de producción"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDcncUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycBase.ResumeLayout(False)
        CType(Me.INDsleddlDosis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcScheduleException, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvScheduleException, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMmeReasonForStop.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteStopDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteStopDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcSchedules, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSchedules, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTmeEndTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkFestivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkDomingo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkSabado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkViernes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkJueves.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTmeStartTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkMartes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkLunes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleTime24.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleddlUnidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewUnitDosesType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkMiercoles.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBaseUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTime24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciLunes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMartes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMiercoles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciJueves, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciViernes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSabado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDomingo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFestivo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStartTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAddSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSchedules, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgScheduleException, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciScheduleException, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReasonForStop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStopDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAddScheduleException, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDcncUnitDoseType As CtrNavigationControlPanel
    Friend WithEvents INDlycBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycBaseUnitDoseType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyGrUnitDoseType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As IndigoGridLookUpControl
    Friend WithEvents IndigoRadioGroup1 As IndigoRadioGroup
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridView2 As IndigoGridView
    Friend WithEvents INDgcUnitDoseType As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDviewUnitDosesType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemButtonEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents INDbtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciUnitDoseType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleddlUnidad As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTmeStartTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDChkMartes As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkLunes As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDGleTime24 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgSchedule As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciTime24 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciLunes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciMartes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciStartTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDChkFestivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkDomingo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkSabado As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkViernes As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkJueves As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDChkMiercoles As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDLciMiercoles As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciJueves As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciViernes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciSabado As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDomingo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFestivo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoCheckEdit1 As IndigoCheckEdit
    Friend WithEvents INDGcSchedules As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvSchedules As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSmbAddSchedule As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDTmeEndTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciEndTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAddSchedule As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciSchedules As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDteStopDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciStopDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcScheduleException As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvScheduleException As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDMmeReasonForStop As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLcgScheduleException As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciScheduleException As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciReasonForStop As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSmbScheduleException As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciAddScheduleException As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleddlDosis As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView3 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
