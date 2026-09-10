Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupTechnicalConceptReadjusments
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcDefectClassification = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDefectClassification = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColDefectGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCritical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLess = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProduction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuality = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmeTechnicalConcept = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdeExpiratedDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDCBReadJusment = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.INDTeTypeDose = New DevExpress.XtraEditors.TextEdit()
        Me.INDDeDateTechnicalConcept = New DevExpress.XtraEditors.DateEdit()
        Me.INDteTemperature = New DevExpress.XtraEditors.TextEdit()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDateTechnicalConcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLclTypeDose = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLCIReadjusment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLCIExpiredDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLclTemperature = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcITechnicalConcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoComboBoxEdit1 = New Presentation.Controls.IndigoComboBoxEdit(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoTextEdit11 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoTextEdit12 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoTextEdit111 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDGcDefectClassification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDefectClassification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeTechnicalConcept.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeExpiratedDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeExpiratedDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCBReadJusment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeTypeDose.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDateTechnicalConcept.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDateTechnicalConcept.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteTemperature.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateTechnicalConcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLclTypeDose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLCIReadjusment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLCIExpiredDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLclTemperature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcITechnicalConcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit111, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(993, 563)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(993, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(993, 130)
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDGcDefectClassification)
        Me.INDLcBase.Controls.Add(Me.INDmeTechnicalConcept)
        Me.INDLcBase.Controls.Add(Me.INDdeExpiratedDate)
        Me.INDLcBase.Controls.Add(Me.INDCBReadJusment)
        Me.INDLcBase.Controls.Add(Me.INDTeTypeDose)
        Me.INDLcBase.Controls.Add(Me.INDDeDateTechnicalConcept)
        Me.INDLcBase.Controls.Add(Me.INDteTemperature)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(2, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(989, 554)
        Me.INDLcBase.TabIndex = 0
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDGcDefectClassification
        '
        Me.INDGcDefectClassification.Location = New System.Drawing.Point(12, 241)
        Me.INDGcDefectClassification.MainView = Me.INDGvDefectClassification
        Me.INDGcDefectClassification.Name = "INDGcDefectClassification"
        Me.INDGcDefectClassification.Size = New System.Drawing.Size(965, 301)
        Me.INDGcDefectClassification.TabIndex = 9
        Me.INDGcDefectClassification.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDefectClassification})
        '
        'INDGvDefectClassification
        '
        Me.INDGvDefectClassification.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDefectClassification.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDefectClassification.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDefectClassification.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDefectClassification.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDefectClassification.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDefectClassification.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDefectClassification.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDefectClassification.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDefectClassification.Appearance.Row.Options.UseFont = True
        Me.INDGvDefectClassification.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvDefectClassification.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDefectClassification.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColDefectGroup, Me.INDColItem, Me.INDColCritical, Me.INDColLess, Me.INDColProduction, Me.INDColQuality})
        Me.INDGvDefectClassification.GridControl = Me.INDGcDefectClassification
        Me.INDGvDefectClassification.GroupCount = 1
        Me.INDGvDefectClassification.IndicatorWidth = 30
        Me.INDGvDefectClassification.Name = "INDGvDefectClassification"
        Me.INDGvDefectClassification.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvDefectClassification.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDefectClassification.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDefectClassification.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDefectClassification.OptionsView.ShowGroupPanel = False
        Me.INDGvDefectClassification.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColDefectGroup, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'INDColDefectGroup
        '
        Me.INDColDefectGroup.Caption = "Grupo"
        Me.INDColDefectGroup.FieldName = "DefectClassificationGroupDescription"
        Me.INDColDefectGroup.FieldNameSortGroup = "DefectClassificationGroupWeight"
        Me.INDColDefectGroup.Name = "INDColDefectGroup"
        Me.INDColDefectGroup.OptionsColumn.AllowEdit = False
        Me.INDColDefectGroup.OptionsColumn.AllowFocus = False
        Me.INDColDefectGroup.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColDefectGroup.OptionsColumn.AllowMove = False
        Me.INDColDefectGroup.OptionsColumn.AllowShowHide = False
        Me.INDColDefectGroup.OptionsColumn.AllowSize = False
        Me.INDColDefectGroup.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColDefectGroup.Visible = True
        Me.INDColDefectGroup.VisibleIndex = 0
        '
        'INDColItem
        '
        Me.INDColItem.Caption = "DEFECTO"
        Me.INDColItem.FieldName = "DefectClassificationItemDescription"
        Me.INDColItem.Name = "INDColItem"
        Me.INDColItem.OptionsColumn.AllowEdit = False
        Me.INDColItem.OptionsColumn.AllowFocus = False
        Me.INDColItem.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColItem.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColItem.OptionsColumn.AllowMove = False
        Me.INDColItem.OptionsColumn.AllowShowHide = False
        Me.INDColItem.OptionsColumn.AllowSize = False
        Me.INDColItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColItem.Visible = True
        Me.INDColItem.VisibleIndex = 0
        Me.INDColItem.Width = 414
        '
        'INDColCritical
        '
        Me.INDColCritical.Caption = "CRÍTICO"
        Me.INDColCritical.FieldName = "Critical"
        Me.INDColCritical.Name = "INDColCritical"
        Me.INDColCritical.OptionsColumn.AllowEdit = False
        Me.INDColCritical.OptionsColumn.AllowFocus = False
        Me.INDColCritical.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColCritical.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColCritical.OptionsColumn.AllowMove = False
        Me.INDColCritical.OptionsColumn.AllowShowHide = False
        Me.INDColCritical.OptionsColumn.AllowSize = False
        Me.INDColCritical.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColCritical.Visible = True
        Me.INDColCritical.VisibleIndex = 1
        Me.INDColCritical.Width = 56
        '
        'INDColLess
        '
        Me.INDColLess.Caption = "MENOR"
        Me.INDColLess.FieldName = "Less"
        Me.INDColLess.Name = "INDColLess"
        Me.INDColLess.OptionsColumn.AllowEdit = False
        Me.INDColLess.OptionsColumn.AllowFocus = False
        Me.INDColLess.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColLess.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColLess.OptionsColumn.AllowMove = False
        Me.INDColLess.OptionsColumn.AllowShowHide = False
        Me.INDColLess.OptionsColumn.AllowSize = False
        Me.INDColLess.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColLess.Visible = True
        Me.INDColLess.VisibleIndex = 2
        Me.INDColLess.Width = 56
        '
        'INDColProduction
        '
        Me.INDColProduction.Caption = "PRODUCCIÓN"
        Me.INDColProduction.FieldName = "Production"
        Me.INDColProduction.Name = "INDColProduction"
        Me.INDColProduction.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColProduction.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColProduction.OptionsColumn.AllowMove = False
        Me.INDColProduction.OptionsColumn.AllowShowHide = False
        Me.INDColProduction.OptionsColumn.AllowSize = False
        Me.INDColProduction.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColProduction.Width = 80
        '
        'INDColQuality
        '
        Me.INDColQuality.Caption = "CALIDAD"
        Me.INDColQuality.FieldName = "Quality"
        Me.INDColQuality.Name = "INDColQuality"
        Me.INDColQuality.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColQuality.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColQuality.OptionsColumn.AllowMove = False
        Me.INDColQuality.OptionsColumn.AllowShowHide = False
        Me.INDColQuality.OptionsColumn.AllowSize = False
        Me.INDColQuality.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColQuality.Visible = True
        Me.INDColQuality.VisibleIndex = 3
        Me.INDColQuality.Width = 79
        '
        'INDmeTechnicalConcept
        '
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDmeTechnicalConcept, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDmeTechnicalConcept, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeTechnicalConcept, False)
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDmeTechnicalConcept, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDmeTechnicalConcept, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDmeTechnicalConcept, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDmeTechnicalConcept, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeTechnicalConcept, False)
        Me.INDmeTechnicalConcept.Location = New System.Drawing.Point(279, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeTechnicalConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDmeTechnicalConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit12.SetMascara(Me.INDmeTechnicalConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDmeTechnicalConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeTechnicalConcept.Name = "INDmeTechnicalConcept"
        Me.INDmeTechnicalConcept.Size = New System.Drawing.Size(686, 76)
        Me.INDmeTechnicalConcept.StyleController = Me.INDLcBase
        Me.INDmeTechnicalConcept.TabIndex = 8
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDmeTechnicalConcept, 500)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDmeTechnicalConcept, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDmeTechnicalConcept, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeTechnicalConcept, 0)
        '
        'INDdeExpiratedDate
        '
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDdeExpiratedDate, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeExpiratedDate, False)
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDdeExpiratedDate, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDdeExpiratedDate, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDdeExpiratedDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeExpiratedDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeExpiratedDate, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDdeExpiratedDate, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDdeExpiratedDate, False)
        Me.INDdeExpiratedDate.EditValue = Nothing
        Me.INDdeExpiratedDate.Location = New System.Drawing.Point(709, 53)
        Me.IndigoTextEdit11.SetMascara(Me.INDdeExpiratedDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeExpiratedDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit12.SetMascara(Me.INDdeExpiratedDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDdeExpiratedDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeExpiratedDate, Presentation.Controls.IndigoDate.EMask.FechaHora)
        Me.INDdeExpiratedDate.Name = "INDdeExpiratedDate"
        Me.INDdeExpiratedDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeExpiratedDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeExpiratedDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeExpiratedDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeExpiratedDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeExpiratedDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeExpiratedDate.Properties.Mask.EditMask = "dd/MM/yyyy hh:mm tt"
        Me.INDdeExpiratedDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeExpiratedDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeExpiratedDate.Size = New System.Drawing.Size(256, 28)
        Me.INDdeExpiratedDate.StyleController = Me.INDLcBase
        Me.INDdeExpiratedDate.TabIndex = 7
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDdeExpiratedDate, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDdeExpiratedDate, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeExpiratedDate, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDdeExpiratedDate, 0)
        '
        'INDCBReadJusment
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDCBReadJusment, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDCBReadJusment, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDCBReadJusment, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDCBReadJusment, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDCBReadJusment, False)
        Me.IndigoComboBoxEdit1.SetCampoObligatorio(Me.INDCBReadJusment, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDCBReadJusment, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDCBReadJusment, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDCBReadJusment, False)
        Me.INDCBReadJusment.Enabled = False
        Me.INDCBReadJusment.Location = New System.Drawing.Point(279, 117)
        Me.IndigoTextEdit111.SetMascara(Me.INDCBReadJusment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDCBReadJusment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDCBReadJusment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit12.SetMascara(Me.INDCBReadJusment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDCBReadJusment.Name = "INDCBReadJusment"
        Me.INDCBReadJusment.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCBReadJusment.Properties.Appearance.Options.UseFont = True
        Me.INDCBReadJusment.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCBReadJusment.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDCBReadJusment.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCBReadJusment.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDCBReadJusment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCBReadJusment.Properties.Items.AddRange(New Object() {"SI", "NO"})
        Me.INDCBReadJusment.Size = New System.Drawing.Size(241, 28)
        Me.INDCBReadJusment.StyleController = Me.INDLcBase
        Me.INDCBReadJusment.TabIndex = 6
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDCBReadJusment, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDCBReadJusment, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDCBReadJusment, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDCBReadJusment, 0)
        '
        'INDTeTypeDose
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDTeTypeDose, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDTeTypeDose, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDTeTypeDose, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeTypeDose, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDTeTypeDose, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDTeTypeDose, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDTeTypeDose, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeTypeDose, False)
        Me.INDTeTypeDose.Enabled = False
        Me.INDTeTypeDose.Location = New System.Drawing.Point(279, 85)
        Me.IndigoTextEdit11.SetMascara(Me.INDTeTypeDose, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDTeTypeDose, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeTypeDose, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit12.SetMascara(Me.INDTeTypeDose, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeTypeDose.Name = "INDTeTypeDose"
        Me.INDTeTypeDose.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDTeTypeDose.Properties.Appearance.Options.UseFont = True
        Me.INDTeTypeDose.Size = New System.Drawing.Size(241, 28)
        Me.INDTeTypeDose.StyleController = Me.INDLcBase
        Me.INDTeTypeDose.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeTypeDose, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDTeTypeDose, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDTeTypeDose, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDTeTypeDose, 0)
        '
        'INDDeDateTechnicalConcept
        '
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDDeDateTechnicalConcept, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeDateTechnicalConcept, False)
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDDeDateTechnicalConcept, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDDeDateTechnicalConcept, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDDeDateTechnicalConcept, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeDateTechnicalConcept, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeDateTechnicalConcept, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDDeDateTechnicalConcept, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDDeDateTechnicalConcept, False)
        Me.INDDeDateTechnicalConcept.CausesValidation = False
        Me.INDDeDateTechnicalConcept.EditValue = Nothing
        Me.INDDeDateTechnicalConcept.Enabled = False
        Me.INDDeDateTechnicalConcept.Location = New System.Drawing.Point(279, 53)
        Me.IndigoTextEdit11.SetMascara(Me.INDDeDateTechnicalConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeDateTechnicalConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit12.SetMascara(Me.INDDeDateTechnicalConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDDeDateTechnicalConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeDateTechnicalConcept, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeDateTechnicalConcept.Name = "INDDeDateTechnicalConcept"
        Me.INDDeDateTechnicalConcept.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDateTechnicalConcept.Properties.Appearance.Options.UseFont = True
        Me.INDDeDateTechnicalConcept.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDateTechnicalConcept.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeDateTechnicalConcept.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDateTechnicalConcept.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDateTechnicalConcept.Properties.DisplayFormat.FormatString = ""
        Me.INDDeDateTechnicalConcept.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDDeDateTechnicalConcept.Properties.EditFormat.FormatString = ""
        Me.INDDeDateTechnicalConcept.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDDeDateTechnicalConcept.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDeDateTechnicalConcept.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeDateTechnicalConcept.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeDateTechnicalConcept.Size = New System.Drawing.Size(241, 28)
        Me.INDDeDateTechnicalConcept.StyleController = Me.INDLcBase
        Me.INDDeDateTechnicalConcept.TabIndex = 4
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDDeDateTechnicalConcept, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDDeDateTechnicalConcept, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeDateTechnicalConcept, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDDeDateTechnicalConcept, 0)
        '
        'INDteTemperature
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteTemperature, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteTemperature, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteTemperature, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteTemperature, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteTemperature, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteTemperature, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteTemperature, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteTemperature, False)
        Me.INDteTemperature.Location = New System.Drawing.Point(709, 85)
        Me.IndigoTextEdit11.SetMascara(Me.INDteTemperature, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteTemperature, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteTemperature, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit12.SetMascara(Me.INDteTemperature, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteTemperature.Name = "INDteTemperature"
        Me.INDteTemperature.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDteTemperature.Properties.Appearance.Options.UseFont = True
        Me.INDteTemperature.Size = New System.Drawing.Size(256, 28)
        Me.INDteTemperature.StyleController = Me.INDLcBase
        Me.INDteTemperature.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteTemperature, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteTemperature, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteTemperature, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteTemperature, 0)
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LayoutControlItem1})
        Me.INDLcgBase.Name = "Root"
        Me.INDLcgBase.Size = New System.Drawing.Size(989, 554)
        Me.INDLcgBase.TextVisible = False
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDateTechnicalConcept, Me.INDLclTypeDose, Me.INDLCIReadjusment, Me.INDLCIExpiredDate, Me.INDLclTemperature, Me.INDLcITechnicalConcept})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(969, 229)
        Me.LayoutControlGroup1.Text = "Información Principal"
        '
        'INDLciDateTechnicalConcept
        '
        Me.INDLciDateTechnicalConcept.Control = Me.INDDeDateTechnicalConcept
        Me.INDLciDateTechnicalConcept.Location = New System.Drawing.Point(0, 0)
        Me.INDLciDateTechnicalConcept.MaxSize = New System.Drawing.Size(500, 32)
        Me.INDLciDateTechnicalConcept.MinSize = New System.Drawing.Size(500, 32)
        Me.INDLciDateTechnicalConcept.Name = "INDLciDateTechnicalConcept"
        Me.INDLciDateTechnicalConcept.Size = New System.Drawing.Size(500, 32)
        Me.INDLciDateTechnicalConcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateTechnicalConcept.Text = "Fecha concepto técnico"
        Me.INDLciDateTechnicalConcept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDateTechnicalConcept.TextSize = New System.Drawing.Size(250, 13)
        Me.INDLciDateTechnicalConcept.TextToControlDistance = 5
        '
        'INDLclTypeDose
        '
        Me.INDLclTypeDose.Control = Me.INDTeTypeDose
        Me.INDLclTypeDose.Location = New System.Drawing.Point(0, 32)
        Me.INDLclTypeDose.MaxSize = New System.Drawing.Size(500, 32)
        Me.INDLclTypeDose.MinSize = New System.Drawing.Size(500, 32)
        Me.INDLclTypeDose.Name = "INDLclTypeDose"
        Me.INDLclTypeDose.Size = New System.Drawing.Size(500, 32)
        Me.INDLclTypeDose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLclTypeDose.Text = "Tipo de dosis unitaria"
        Me.INDLclTypeDose.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLclTypeDose.TextSize = New System.Drawing.Size(250, 13)
        Me.INDLclTypeDose.TextToControlDistance = 5
        '
        'INDLCIReadjusment
        '
        Me.INDLCIReadjusment.Control = Me.INDCBReadJusment
        Me.INDLCIReadjusment.CustomizationFormText = "Ha sido readecuada anteriormente"
        Me.INDLCIReadjusment.Location = New System.Drawing.Point(0, 64)
        Me.INDLCIReadjusment.MaxSize = New System.Drawing.Size(500, 32)
        Me.INDLCIReadjusment.MinSize = New System.Drawing.Size(500, 32)
        Me.INDLCIReadjusment.Name = "INDLCIReadjusment"
        Me.INDLCIReadjusment.Size = New System.Drawing.Size(945, 32)
        Me.INDLCIReadjusment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLCIReadjusment.Text = "Ha sido readecuada anteriormente"
        Me.INDLCIReadjusment.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLCIReadjusment.TextSize = New System.Drawing.Size(250, 13)
        Me.INDLCIReadjusment.TextToControlDistance = 5
        '
        'INDLCIExpiredDate
        '
        Me.INDLCIExpiredDate.Control = Me.INDdeExpiratedDate
        Me.INDLCIExpiredDate.Location = New System.Drawing.Point(500, 0)
        Me.INDLCIExpiredDate.Name = "INDLCIExpiredDate"
        Me.INDLCIExpiredDate.ShowInCustomizationForm = False
        Me.INDLCIExpiredDate.Size = New System.Drawing.Size(445, 32)
        Me.INDLCIExpiredDate.Text = "Fecha de vencimiento"
        Me.INDLCIExpiredDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLCIExpiredDate.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDLCIExpiredDate.TextSize = New System.Drawing.Size(180, 13)
        Me.INDLCIExpiredDate.TextToControlDistance = 5
        '
        'INDLclTemperature
        '
        Me.INDLclTemperature.Control = Me.INDteTemperature
        Me.INDLclTemperature.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLclTemperature.CustomizationFormText = "Tipo de dosis unitaria"
        Me.INDLclTemperature.Location = New System.Drawing.Point(500, 32)
        Me.INDLclTemperature.MinSize = New System.Drawing.Size(239, 32)
        Me.INDLclTemperature.Name = "INDLclTemperature"
        Me.INDLclTemperature.ShowInCustomizationForm = False
        Me.INDLclTemperature.Size = New System.Drawing.Size(445, 32)
        Me.INDLclTemperature.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLclTemperature.Text = "Temperatura aceptación"
        Me.INDLclTemperature.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLclTemperature.TextSize = New System.Drawing.Size(180, 13)
        Me.INDLclTemperature.TextToControlDistance = 5
        '
        'INDLcITechnicalConcept
        '
        Me.INDLcITechnicalConcept.Control = Me.INDmeTechnicalConcept
        Me.INDLcITechnicalConcept.Location = New System.Drawing.Point(0, 96)
        Me.INDLcITechnicalConcept.MinSize = New System.Drawing.Size(130, 21)
        Me.INDLcITechnicalConcept.Name = "INDLcITechnicalConcept"
        Me.INDLcITechnicalConcept.Size = New System.Drawing.Size(945, 80)
        Me.INDLcITechnicalConcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcITechnicalConcept.Text = "Concepto técnico"
        Me.INDLcITechnicalConcept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcITechnicalConcept.TextSize = New System.Drawing.Size(250, 20)
        Me.INDLcITechnicalConcept.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcDefectClassification
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 229)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(969, 305)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FrmPopupTechnicalConceptReadjusments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(993, 698)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupTechnicalConceptReadjusments"
        Me.Opacity = 1.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Concepto técnico de Readecuación"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDGcDefectClassification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDefectClassification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeTechnicalConcept.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeExpiratedDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeExpiratedDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCBReadJusment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeTypeDose.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDateTechnicalConcept.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDateTechnicalConcept.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteTemperature.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateTechnicalConcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLclTypeDose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLCIReadjusment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLCIExpiredDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLclTemperature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcITechnicalConcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit111, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDateTechnicalConcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDeDateTechnicalConcept As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTeTypeDose As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLclTypeDose As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCBReadJusment As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents INDLCIReadjusment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeExpiratedDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLCIExpiredDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents IndigoComboBoxEdit1 As IndigoComboBoxEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoTextEdit11 As IndigoTextEdit
    Friend WithEvents INDteTemperature As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLclTemperature As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeTechnicalConcept As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLcITechnicalConcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit12 As IndigoTextEdit
    Friend WithEvents IndigoTextEdit111 As IndigoTextEdit
    Friend WithEvents INDGcDefectClassification As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDefectClassification As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColDefectGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCritical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLess As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProduction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuality As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
