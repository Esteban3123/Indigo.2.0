<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemToInclude
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmItemToInclude))
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleServiceOrderDetail = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDsleServiceOrderDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDsbAdd)
        Me.INDlcRoot.Controls.Add(Me.INDsleServiceOrderDetail)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(417, 123)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDsbAdd
        '
        Me.INDsbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsbAdd.Appearance.Options.UseFont = True
        Me.INDsbAdd.Location = New System.Drawing.Point(12, 72)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAdd, True)
        Me.INDsbAdd.Name = "INDsbAdd"
        Me.INDsbAdd.Size = New System.Drawing.Size(386, 28)
        Me.INDsbAdd.StyleController = Me.INDlcRoot
        Me.INDsbAdd.TabIndex = 5
        Me.INDsbAdd.Text = "Aceptar"
        '
        'INDsleServiceOrderDetail
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleServiceOrderDetail, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleServiceOrderDetail, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleServiceOrderDetail, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.INDsleServiceOrderDetail.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleServiceOrderDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleServiceOrderDetail.Name = "INDsleServiceOrderDetail"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleServiceOrderDetail, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.INDsleServiceOrderDetail.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleServiceOrderDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleServiceOrderDetail.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDsleServiceOrderDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleServiceOrderDetail.Properties.Appearance.Options.UseFont = True
        Me.INDsleServiceOrderDetail.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleServiceOrderDetail.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleServiceOrderDetail.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleServiceOrderDetail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleServiceOrderDetail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleServiceOrderDetail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleServiceOrderDetail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleServiceOrderDetail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleServiceOrderDetail.Properties.DisplayMember = "CodeNameIpsService"
        Me.INDsleServiceOrderDetail.Properties.NullText = ""
        Me.INDsleServiceOrderDetail.Properties.PopupSizeable = False
        Me.INDsleServiceOrderDetail.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleServiceOrderDetail.Properties.ShowFooter = False
        Me.INDsleServiceOrderDetail.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleServiceOrderDetail, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleServiceOrderDetail, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleServiceOrderDetail, True)
        Me.INDsleServiceOrderDetail.Size = New System.Drawing.Size(386, 28)
        Me.INDsleServiceOrderDetail.StyleController = Me.INDlcRoot
        Me.INDsleServiceOrderDetail.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleServiceOrderDetail, "977")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleServiceOrderDetail, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleServiceOrderDetail, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleServiceOrderDetail, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleServiceOrderDetail, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn3})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Servicio IPS"
        Me.GridColumn1.FieldName = "CodeNameIpsService"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 237
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Grupo Atención"
        Me.GridColumn3.FieldName = "CodeNameCareGroup"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 379
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, False)
        Me.INDlcgRoot.CustomizationFormText = "INDlcgRoot"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(417, 123)
        Me.INDlcgRoot.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDsleServiceOrderDetail
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(397, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Servicio IPS"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(75, 21)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDsbAdd
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 32)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(397, 43)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmItemToInclude
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(417, 123)
        Me.Controls.Add(Me.INDlcRoot)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmItemToInclude.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmItemToInclude"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Servicios IPS"
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDsleServiceOrderDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDsleServiceOrderDetail As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
End Class
