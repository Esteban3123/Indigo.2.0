<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAttach
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAttach))
        Me.INDImgFormats = New DevExpress.Utils.ImageCollection()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.IdDoc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TipoDoc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDImageDocumentsRepositoryIcb = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.NombreDoc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FechaDoc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.MetaData = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMetaDataRepositoryMee = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.INDAttachGc = New DevExpress.XtraGrid.GridControl()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.INDAttachSmb = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDImgFormats, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDImageDocumentsRepositoryIcb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMetaDataRepositoryMee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDAttachGc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(555, 348)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(555, 94)
        Me.ToolBars.Visible = False
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(555, 94)
        Me.BarraBotones.Visible = False
        '
        'INDImgFormats
        '
        Me.INDImgFormats.ImageSize = New System.Drawing.Size(24, 24)
        Me.INDImgFormats.ImageStream = CType(resources.GetObject("INDImgFormats.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDImgFormats.Images.SetKeyName(0, "BMP.png")
        Me.INDImgFormats.Images.SetKeyName(1, "doc.png")
        Me.INDImgFormats.Images.SetKeyName(2, "docx.png")
        Me.INDImgFormats.Images.SetKeyName(3, "GIF.png")
        Me.INDImgFormats.Images.SetKeyName(4, "JPEG.png")
        Me.INDImgFormats.Images.SetKeyName(5, "JPG.png")
        Me.INDImgFormats.Images.SetKeyName(6, "pdf.png")
        Me.INDImgFormats.Images.SetKeyName(7, "png.png")
        Me.INDImgFormats.Images.SetKeyName(8, "TIF.png")
        Me.INDImgFormats.Images.SetKeyName(9, "xls.png")
        Me.INDImgFormats.Images.SetKeyName(10, "xlsx.png")
        Me.INDImgFormats.Images.SetKeyName(11, "otros.png")
        '
        'GridView1
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.IdDoc, Me.TipoDoc, Me.NombreDoc, Me.FechaDoc, Me.MetaData})
        Me.GridView1.GridControl = Me.INDAttachGc
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.Tag = 255
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'IdDoc
        '
        Me.IdDoc.Caption = "Id"
        Me.IdDoc.FieldName = "Id"
        Me.IdDoc.Name = "IdDoc"
        Me.IdDoc.OptionsColumn.AllowEdit = False
        '
        'TipoDoc
        '
        Me.TipoDoc.Caption = "Formato"
        Me.TipoDoc.ColumnEdit = Me.INDImageDocumentsRepositoryIcb
        Me.TipoDoc.FieldName = "Type"
        Me.TipoDoc.Name = "TipoDoc"
        Me.TipoDoc.OptionsColumn.AllowEdit = False
        Me.TipoDoc.OptionsFilter.AllowAutoFilter = False
        Me.TipoDoc.OptionsFilter.AllowFilter = False
        Me.TipoDoc.Visible = True
        Me.TipoDoc.VisibleIndex = 0
        '
        'INDImageDocumentsRepositoryIcb
        '
        Me.INDImageDocumentsRepositoryIcb.AutoHeight = False
        Me.INDImageDocumentsRepositoryIcb.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDImageDocumentsRepositoryIcb.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDImageDocumentsRepositoryIcb.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("PNG", ".png", 7), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("PDF", ".pdf", 6), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("JPG", ".jpg", 5), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("DOC", ".doc", 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("XLS", ".xls", 9), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("XLSX", ".xlsx", 10), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("DOCX", ".docx", 2), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("JPEG", ".jpeg", 4), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("TIF", ".tif", 8), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("BMP", ".bmp", 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("GIF", ".gif", 3), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("OTRO", ".otro", 11)})
        Me.INDImageDocumentsRepositoryIcb.LargeImages = Me.INDImgFormats
        Me.INDImageDocumentsRepositoryIcb.Name = "INDImageDocumentsRepositoryIcb"
        Me.INDImageDocumentsRepositoryIcb.ReadOnly = True
        '
        'NombreDoc
        '
        Me.NombreDoc.Caption = "Nombre"
        Me.NombreDoc.FieldName = "Name"
        Me.NombreDoc.Name = "NombreDoc"
        Me.NombreDoc.OptionsColumn.AllowEdit = False
        Me.NombreDoc.OptionsColumn.ReadOnly = True
        Me.NombreDoc.OptionsFilter.AllowAutoFilter = False
        Me.NombreDoc.OptionsFilter.AllowFilter = False
        Me.NombreDoc.Visible = True
        Me.NombreDoc.VisibleIndex = 1
        '
        'FechaDoc
        '
        Me.FechaDoc.Caption = "Fecha"
        Me.FechaDoc.DisplayFormat.FormatString = "g"
        Me.FechaDoc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FechaDoc.FieldName = "AttachDate"
        Me.FechaDoc.Name = "FechaDoc"
        Me.FechaDoc.OptionsColumn.AllowEdit = False
        Me.FechaDoc.OptionsColumn.ReadOnly = True
        Me.FechaDoc.OptionsFilter.AllowAutoFilter = False
        Me.FechaDoc.OptionsFilter.AllowFilter = False
        Me.FechaDoc.Visible = True
        Me.FechaDoc.VisibleIndex = 2
        '
        'MetaData
        '
        Me.MetaData.Caption = "Info Adicional"
        Me.MetaData.ColumnEdit = Me.INDMetaDataRepositoryMee
        Me.MetaData.FieldName = "MetaData"
        Me.MetaData.Name = "MetaData"
        Me.MetaData.OptionsColumn.ReadOnly = True
        Me.MetaData.Visible = True
        Me.MetaData.VisibleIndex = 3
        '
        'INDMetaDataRepositoryMee
        '
        Me.INDMetaDataRepositoryMee.AutoHeight = False
        Me.INDMetaDataRepositoryMee.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDMetaDataRepositoryMee.Name = "INDMetaDataRepositoryMee"
        Me.INDMetaDataRepositoryMee.ReadOnly = True
        '
        'INDAttachGc
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDAttachGc, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDAttachGc, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDAttachGc, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDAttachGc, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDAttachGc, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDAttachGc, False)
        Me.INDAttachGc.Location = New System.Drawing.Point(12, 44)
        Me.INDAttachGc.MainView = Me.GridView1
        Me.INDAttachGc.Name = "INDAttachGc"
        Me.INDAttachGc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDImageDocumentsRepositoryIcb, Me.INDMetaDataRepositoryMee})
        Me.INDAttachGc.Size = New System.Drawing.Size(527, 283)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDAttachGc, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDAttachGc.TabIndex = 4
        Me.INDAttachGc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'INDAttachSmb
        '
        Me.INDAttachSmb.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDAttachSmb.Appearance.Options.UseFont = True
        Me.INDAttachSmb.Location = New System.Drawing.Point(12, 12)
        Me.INDAttachSmb.Name = "INDAttachSmb"
        Me.INDAttachSmb.Size = New System.Drawing.Size(527, 28)
        Me.INDAttachSmb.StyleController = Me.LayoutControl1
        Me.INDAttachSmb.TabIndex = 3
        Me.INDAttachSmb.Text = "Adjuntar"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDAttachGc)
        Me.LayoutControl1.Controls.Add(Me.INDAttachSmb)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(551, 339)
        Me.LayoutControl1.TabIndex = 5
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(551, 339)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDAttachSmb
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(531, 32)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDAttachGc
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(531, 287)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'FrmAttach
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 465)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmAttach"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Adjuntar Documentos"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDImgFormats, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDImageDocumentsRepositoryIcb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMetaDataRepositoryMee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDAttachGc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDImgFormats As DevExpress.Utils.ImageCollection
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDAttachGc As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IdDoc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TipoDoc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDImageDocumentsRepositoryIcb As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents NombreDoc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FechaDoc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDAttachSmb As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents MetaData As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDMetaDataRepositoryMee As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
End Class
