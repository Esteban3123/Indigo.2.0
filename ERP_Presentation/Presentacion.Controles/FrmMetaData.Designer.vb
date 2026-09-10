<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMetaData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMetaData))
        Me.INDLycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDFilePathLbl = New DevExpress.XtraEditors.LabelControl()
        Me.INDUseInformationRdg = New DevExpress.XtraEditors.RadioGroup()
        Me.INDFileContainersGle = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLycRootGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDLoadingpe = New DevExpress.XtraEditors.PictureEdit()
        Me.INDLoadingPcc = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLoadingPce = New DevExpress.XtraEditors.PictureEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLycRoot.SuspendLayout()
        CType(Me.INDUseInformationRdg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDFileContainersGle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycRootGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLoadingpe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLoadingPcc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLoadingPcc.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDLoadingPce.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLycRoot)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(455, 172)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 24)
        Me.ToolBars.Size = New System.Drawing.Size(455, 98)
        Me.ToolBars.Visible = False
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(455, 98)
        '
        'INDLycRoot
        '
        Me.INDLycRoot.Controls.Add(Me.INDFilePathLbl)
        Me.INDLycRoot.Controls.Add(Me.INDUseInformationRdg)
        Me.INDLycRoot.Controls.Add(Me.INDFileContainersGle)
        Me.INDLycRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLycRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDLycRoot.Name = "INDLycRoot"
        Me.INDLycRoot.Root = Me.INDLycRootGroup
        Me.INDLycRoot.Size = New System.Drawing.Size(451, 163)
        Me.INDLycRoot.TabIndex = 0
        Me.INDLycRoot.Text = "LayoutControl1"
        '
        'INDFilePathLbl
        '
        Me.INDFilePathLbl.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDFilePathLbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDFilePathLbl.Appearance.Options.UseFont = True
        Me.INDFilePathLbl.Appearance.Options.UseForeColor = True
        Me.INDFilePathLbl.AutoEllipsis = True
        Me.INDFilePathLbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDFilePathLbl.Location = New System.Drawing.Point(156, 24)
        Me.INDFilePathLbl.Name = "INDFilePathLbl"
        Me.INDFilePathLbl.Size = New System.Drawing.Size(271, 35)
        Me.INDFilePathLbl.StyleController = Me.INDLycRoot
        Me.INDFilePathLbl.TabIndex = 5
        Me.INDFilePathLbl.Text = "dsfsdfafsfsdafasdfsadfsdfasdfsdfdsffsdfsdfsdfsdfsdfsd"
        '
        'INDUseInformationRdg
        '
        Me.INDUseInformationRdg.Enabled = False
        Me.INDUseInformationRdg.Location = New System.Drawing.Point(156, 99)
        Me.INDUseInformationRdg.Name = "INDUseInformationRdg"
        Me.INDUseInformationRdg.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDUseInformationRdg.Properties.Appearance.Options.UseFont = True
        Me.INDUseInformationRdg.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDUseInformationRdg.Properties.AppearanceDisabled.Options.UseFont = True
        Me.INDUseInformationRdg.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDUseInformationRdg.Size = New System.Drawing.Size(254, 51)
        Me.INDUseInformationRdg.StyleController = Me.INDLycRoot
        Me.INDUseInformationRdg.TabIndex = 4
        '
        'INDFileContainersGle
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDFileContainersGle, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDFileContainersGle, False)
        Me.INDFileContainersGle.Location = New System.Drawing.Point(156, 63)
        Me.IndigoTextEdit1.SetMascara(Me.INDFileContainersGle, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDFileContainersGle.Name = "INDFileContainersGle"
        Me.INDFileContainersGle.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDFileContainersGle.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDFileContainersGle.Properties.Appearance.Options.UseBackColor = True
        Me.INDFileContainersGle.Properties.Appearance.Options.UseFont = True
        Me.INDFileContainersGle.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDFileContainersGle.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDFileContainersGle.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDFileContainersGle.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDFileContainersGle.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDFileContainersGle.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDFileContainersGle.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDFileContainersGle.Properties.DisplayMember = "FileContainer.Name"
        Me.INDFileContainersGle.Properties.NullText = "Selecciona un archivador"
        Me.INDFileContainersGle.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDFileContainersGle.Properties.ValueMember = "FileContainer.Id"
        Me.INDFileContainersGle.Size = New System.Drawing.Size(254, 28)
        Me.INDFileContainersGle.StyleController = Me.INDLycRoot
        Me.INDFileContainersGle.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDFileContainersGle, 0)
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
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nombre"
        Me.GridColumn1.FieldName = "FileContainer.Name"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "GridColumn2"
        Me.GridColumn2.FieldName = "FileContainer.Id"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'INDLycRootGroup
        '
        Me.INDLycRootGroup.CustomizationFormText = "LayoutControlGroup1"
        Me.INDLycRootGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLycRootGroup.GroupBordersVisible = False
        Me.INDLycRootGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1})
        Me.INDLycRootGroup.Name = "INDLycRootGroup"
        Me.INDLycRootGroup.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 2)
        Me.INDLycRootGroup.Size = New System.Drawing.Size(451, 163)
        Me.INDLycRootGroup.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 9, 9, 2)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(431, 151)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem1.Control = Me.INDFileContainersGle
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 39)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(407, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Archivador"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(120, 35)
        Me.LayoutControlItem1.TextToControlDistance = 12
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem3.Control = Me.INDFilePathLbl
        Me.LayoutControlItem3.CustomizationFormText = "Ruta Archivo"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(407, 39)
        Me.LayoutControlItem3.Text = "Ruta Archivo"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(120, 35)
        Me.LayoutControlItem3.TextToControlDistance = 12
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LayoutControlItem2.Control = Me.INDUseInformationRdg
        Me.LayoutControlItem2.CustomizationFormText = "Usa información del formulario"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 75)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 55)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 55)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(407, 59)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Usa información del formulario"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(120, 35)
        Me.LayoutControlItem2.TextToControlDistance = 12
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDLoadingpe
        '
        Me.INDLoadingpe.Location = New System.Drawing.Point(12, 12)
        Me.INDLoadingpe.Name = "INDLoadingpe"
        Me.INDLoadingpe.Size = New System.Drawing.Size(176, 76)
        Me.INDLoadingpe.TabIndex = 4
        '
        'INDLoadingPcc
        '
        Me.INDLoadingPcc.Controls.Add(Me.LayoutControl1)
        Me.INDLoadingPcc.Location = New System.Drawing.Point(96, 29)
        Me.INDLoadingPcc.Name = "INDLoadingPcc"
        Me.INDLoadingPcc.Size = New System.Drawing.Size(215, 155)
        Me.INDLoadingPcc.TabIndex = 9
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDLoadingPce)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup2
        Me.LayoutControl1.Size = New System.Drawing.Size(215, 155)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDLoadingPce
        '
        Me.INDLoadingPce.EditValue = CType(resources.GetObject("INDLoadingPce.EditValue"), Object)
        Me.INDLoadingPce.Location = New System.Drawing.Point(12, 12)
        Me.INDLoadingPce.Name = "INDLoadingPce"
        Me.INDLoadingPce.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.INDLoadingPce.Properties.ShowMenu = False
        Me.INDLoadingPce.Size = New System.Drawing.Size(191, 131)
        Me.INDLoadingPce.StyleController = Me.LayoutControl1
        Me.INDLoadingPce.TabIndex = 4
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.BorderColor = System.Drawing.Color.Black
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseBorderColor = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.BorderColor = System.Drawing.Color.Black
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseBorderColor = True
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(215, 155)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDLoadingPce
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(195, 135)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'FrmMetaData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 294)
        Me.Controls.Add(Me.INDLoadingPcc)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMetaData"
        Me.Opacity = 1.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Información Documento"
        Me.TopMost = True
        Me.ViewModeEditHold = True
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.INDLoadingPcc, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLycRoot.ResumeLayout(False)
        CType(Me.INDUseInformationRdg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDFileContainersGle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycRootGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLoadingpe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLoadingPcc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLoadingPcc.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDLoadingPce.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLycRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLycRootGroup As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDFileContainersGle As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDUseInformationRdg As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDFilePathLbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLoadingpe As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDLoadingPcc As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLoadingPce As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
End Class
