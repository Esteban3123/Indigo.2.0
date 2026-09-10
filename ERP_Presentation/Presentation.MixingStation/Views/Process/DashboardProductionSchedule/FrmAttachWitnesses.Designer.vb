Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAttachWitnesses
    Inherits FormBase

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
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPeFile = New DevExpress.XtraEditors.PictureEdit()
        Me.INDPdfFile = New DevExpress.XtraPdfViewer.PdfViewer()
        Me.INDGcDocuments = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDocuments = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbLoad = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTeName = New DevExpress.XtraEditors.TextEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgGeneralData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgPreview = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciPdfViewer = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciImageViewer = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        Me.XtraOpenFileDialog1 = New DevExpress.XtraEditors.XtraOpenFileDialog(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDPeFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcDocuments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDocuments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgGeneralData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPdfViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciImageViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1101, 522)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1101, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1101, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDPeFile)
        Me.INDLcRoot.Controls.Add(Me.INDPdfFile)
        Me.INDLcRoot.Controls.Add(Me.INDGcDocuments)
        Me.INDLcRoot.Controls.Add(Me.INDSbLoad)
        Me.INDLcRoot.Controls.Add(Me.INDTeName)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.Root
        Me.INDLcRoot.Size = New System.Drawing.Size(1097, 513)
        Me.INDLcRoot.TabIndex = 0
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDPeFile
        '
        Me.INDPeFile.Location = New System.Drawing.Point(438, 469)
        Me.INDPeFile.Name = "INDPeFile"
        Me.INDPeFile.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.INDPeFile.Properties.ShowMenu = False
        Me.INDPeFile.Size = New System.Drawing.Size(635, 20)
        Me.INDPeFile.StyleController = Me.INDLcRoot
        Me.INDPeFile.TabIndex = 8
        '
        'INDPdfFile
        '
        Me.INDPdfFile.Location = New System.Drawing.Point(438, 53)
        Me.INDPdfFile.Name = "INDPdfFile"
        Me.INDPdfFile.Size = New System.Drawing.Size(635, 412)
        Me.INDPdfFile.TabIndex = 7
        '
        'INDGcDocuments
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDocuments, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDocuments, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDocuments, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDocuments, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDocuments, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDocuments, False)
        Me.INDGcDocuments.Location = New System.Drawing.Point(24, 147)
        Me.INDGcDocuments.MainView = Me.INDGvDocuments
        Me.INDGcDocuments.Name = "INDGcDocuments"
        Me.INDGcDocuments.Size = New System.Drawing.Size(386, 342)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDocuments, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcDocuments.TabIndex = 6
        Me.INDGcDocuments.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDocuments})
        '
        'INDGvDocuments
        '
        Me.INDGvDocuments.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDocuments.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDocuments.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDocuments.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDocuments.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDocuments.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDocuments.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDocuments.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDocuments.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDocuments.Appearance.Row.Options.UseFont = True
        Me.INDGvDocuments.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvDocuments.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDocuments.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.INDGvDocuments.GridControl = Me.INDGcDocuments
        Me.INDGvDocuments.Name = "INDGvDocuments"
        Me.INDGvDocuments.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDocuments.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDocuments.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDocuments.OptionsView.ShowDetailButtons = False
        Me.INDGvDocuments.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDocuments, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nombre"
        Me.GridColumn1.FieldName = "Name"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDSbLoad
        '
        Me.INDSbLoad.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbLoad.Appearance.Options.UseFont = True
        Me.INDSbLoad.Location = New System.Drawing.Point(24, 107)
        Me.INDSbLoad.Name = "INDSbLoad"
        Me.INDSbLoad.Size = New System.Drawing.Size(146, 36)
        Me.INDSbLoad.StyleController = Me.INDLcRoot
        Me.INDSbLoad.TabIndex = 5
        Me.INDSbLoad.Text = "Cargar documento"
        '
        'INDTeName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeName, False)
        Me.INDTeName.Location = New System.Drawing.Point(24, 73)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeName.Name = "INDTeName"
        Me.INDTeName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeName.Properties.Appearance.Options.UseFont = True
        Me.INDTeName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeName.Size = New System.Drawing.Size(386, 28)
        Me.INDTeName.StyleController = Me.INDLcRoot
        Me.INDTeName.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeName, 0)
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgGeneralData, Me.INDLcgPreview})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1097, 513)
        Me.Root.TextVisible = False
        '
        'INDLcgGeneralData
        '
        Me.INDLcgGeneralData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralData.AppearanceGroup.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralData.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgGeneralData, False)
        Me.INDLcgGeneralData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciName, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.INDLcgGeneralData.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgGeneralData.Name = "INDLcgGeneralData"
        Me.INDLcgGeneralData.Size = New System.Drawing.Size(414, 493)
        Me.INDLcgGeneralData.Text = "Datos generales"
        '
        'INDLciName
        '
        Me.INDLciName.Control = Me.INDTeName
        Me.INDLciName.Location = New System.Drawing.Point(0, 0)
        Me.INDLciName.MaxSize = New System.Drawing.Size(390, 54)
        Me.INDLciName.MinSize = New System.Drawing.Size(390, 54)
        Me.INDLciName.Name = "INDLciName"
        Me.INDLciName.Size = New System.Drawing.Size(390, 54)
        Me.INDLciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciName.Text = "Nombre"
        Me.INDLciName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciName.TextSize = New System.Drawing.Size(51, 17)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSbLoad
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 54)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(150, 40)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(150, 40)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 40)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDGcDocuments
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 94)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(390, 346)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'INDLcgPreview
        '
        Me.INDLcgPreview.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPreview.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPreview.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPreview.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPreview.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreview.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPreview.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPreview.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPreview.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreview.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPreview.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreview.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPreview.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreview.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgPreview, False)
        Me.INDLcgPreview.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciPdfViewer, Me.INDLciImageViewer})
        Me.INDLcgPreview.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgPreview.Name = "INDLcgPreview"
        Me.INDLcgPreview.Size = New System.Drawing.Size(663, 493)
        Me.INDLcgPreview.Text = "Vista previa"
        '
        'INDLciPdfViewer
        '
        Me.INDLciPdfViewer.Control = Me.INDPdfFile
        Me.INDLciPdfViewer.Location = New System.Drawing.Point(0, 0)
        Me.INDLciPdfViewer.Name = "INDLciPdfViewer"
        Me.INDLciPdfViewer.Size = New System.Drawing.Size(639, 416)
        Me.INDLciPdfViewer.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciPdfViewer.TextVisible = False
        '
        'INDLciImageViewer
        '
        Me.INDLciImageViewer.Control = Me.INDPeFile
        Me.INDLciImageViewer.Location = New System.Drawing.Point(0, 416)
        Me.INDLciImageViewer.Name = "INDLciImageViewer"
        Me.INDLciImageViewer.Size = New System.Drawing.Size(639, 24)
        Me.INDLciImageViewer.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciImageViewer.TextVisible = False
        Me.INDLciImageViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoDocumentViewer1
        '
        Me.IndigoDocumentViewer1.ParentForm = Nothing
        Me.IndigoDocumentViewer1.Permissions = Nothing
        '
        'XtraOpenFileDialog1
        '
        Me.XtraOpenFileDialog1.Filter = "Image Files (.png)|*.png|Document Files (*.pdf)|*.pdf"
        '
        'FrmAttachWitnesses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1101, 657)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAttachWitnesses"
        Me.Opacity = 1.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Adjuntar testigos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDPeFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcDocuments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDocuments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgGeneralData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPdfViewer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciImageViewer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTeName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcDocuments As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDocuments As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSbLoad As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgGeneralData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgPreview As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoDocumentViewer1 As IndigoDocumentViewer
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents XtraOpenFileDialog1 As DevExpress.XtraEditors.XtraOpenFileDialog
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPdfFile As DevExpress.XtraPdfViewer.PdfViewer
    Friend WithEvents INDLciPdfViewer As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPeFile As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDLciImageViewer As DevExpress.XtraLayout.LayoutControlItem
End Class
