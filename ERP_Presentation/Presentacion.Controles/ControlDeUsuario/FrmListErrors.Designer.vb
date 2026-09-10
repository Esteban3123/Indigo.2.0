<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmListErrors
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmListErrors))
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLabelQuestion = New System.Windows.Forms.Label()
        Me.INDBtnNot = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnYes = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcErrorList = New DevExpress.XtraGrid.GridControl()
        Me.INDgvErrorList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDcolError = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcgErrorList = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliErrorList = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLygQuestionControl = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciNot = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciYes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgcErrorList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvErrorList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgErrorList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliErrorList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLygQuestionControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciYes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDLabelQuestion)
        Me.LayoutControl1.Controls.Add(Me.INDBtnNot)
        Me.LayoutControl1.Controls.Add(Me.INDbtnYes)
        Me.LayoutControl1.Controls.Add(Me.INDgcErrorList)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.INDlcgErrorList
        Me.LayoutControl1.Size = New System.Drawing.Size(549, 440)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDLabelQuestion
        '
        Me.INDLabelQuestion.Location = New System.Drawing.Point(24, 395)
        Me.INDLabelQuestion.Name = "INDLabelQuestion"
        Me.INDLabelQuestion.Size = New System.Drawing.Size(401, 21)
        Me.INDLabelQuestion.TabIndex = 8
        Me.INDLabelQuestion.Text = "Pregunta de control"
        Me.INDLabelQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'INDBtnNot
        '
        Me.INDBtnNot.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnNot.Appearance.Options.UseFont = True
        Me.INDBtnNot.Location = New System.Drawing.Point(479, 395)
        Me.INDBtnNot.Name = "INDBtnNot"
        Me.INDBtnNot.Size = New System.Drawing.Size(46, 21)
        Me.INDBtnNot.StyleController = Me.LayoutControl1
        Me.INDBtnNot.TabIndex = 6
        Me.INDBtnNot.Text = "No"
        '
        'INDbtnYes
        '
        Me.INDbtnYes.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnYes.Appearance.Options.UseFont = True
        Me.INDbtnYes.Location = New System.Drawing.Point(429, 395)
        Me.INDbtnYes.Name = "INDbtnYes"
        Me.INDbtnYes.Size = New System.Drawing.Size(46, 21)
        Me.INDbtnYes.StyleController = Me.LayoutControl1
        Me.INDbtnYes.TabIndex = 5
        Me.INDbtnYes.Text = "Si"
        '
        'INDgcErrorList
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcErrorList, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcErrorList, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcErrorList, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcErrorList, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcErrorList, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcErrorList, False)
        Me.INDgcErrorList.Location = New System.Drawing.Point(12, 12)
        Me.INDgcErrorList.MainView = Me.INDgvErrorList
        Me.INDgcErrorList.Name = "INDgcErrorList"
        Me.INDgcErrorList.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.INDgcErrorList.Size = New System.Drawing.Size(525, 367)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcErrorList, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcErrorList.TabIndex = 4
        Me.INDgcErrorList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvErrorList})
        '
        'INDgvErrorList
        '
        Me.INDgvErrorList.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvErrorList.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvErrorList.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvErrorList.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvErrorList.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvErrorList.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvErrorList.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvErrorList.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvErrorList.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvErrorList.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvErrorList.Appearance.Row.Options.UseFont = True
        Me.INDgvErrorList.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvErrorList.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvErrorList.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.INDcolError})
        Me.INDgvErrorList.GridControl = Me.INDgcErrorList
        Me.INDgvErrorList.Name = "INDgvErrorList"
        Me.INDgvErrorList.OptionsFind.AlwaysVisible = True
        Me.INDgvErrorList.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvErrorList.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvErrorList.OptionsView.ShowAutoFilterRow = True
        Me.INDgvErrorList.OptionsView.ShowDetailButtons = False
        Me.INDgvErrorList.OptionsView.ShowFooter = True
        Me.INDgvErrorList.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvErrorList, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = " "
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.GridColumn1.FieldName = "Icon"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.OptionsColumn.AllowMove = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 45
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.Appearance.Options.UseTextOptions = True
        Me.RepositoryItemImageComboBox1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemImageComboBox1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 2)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        Me.RepositoryItemImageComboBox1.SmallImages = Me.ImageCollection1
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "Iconos correcto e incorrecto 24x24px-01.png")
        Me.ImageCollection1.Images.SetKeyName(1, "Iconos correcto e incorrecto 24x24px-02.png")
        Me.ImageCollection1.Images.SetKeyName(2, "Iconos correcto e incorrecto 24x24px-03.png")
        '
        'INDcolError
        '
        Me.INDcolError.Caption = "Mensaje"
        Me.INDcolError.FieldName = "Message"
        Me.INDcolError.Name = "INDcolError"
        Me.INDcolError.OptionsColumn.AllowEdit = False
        Me.INDcolError.OptionsColumn.AllowFocus = False
        Me.INDcolError.OptionsColumn.AllowMove = False
        Me.INDcolError.Visible = True
        Me.INDcolError.VisibleIndex = 1
        Me.INDcolError.Width = 1021
        '
        'INDlcgErrorList
        '
        Me.INDlcgErrorList.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgErrorList.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgErrorList.GroupBordersVisible = False
        Me.INDlcgErrorList.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliErrorList, Me.INDLygQuestionControl})
        Me.INDlcgErrorList.Name = "INDlcgErrorList"
        Me.INDlcgErrorList.Size = New System.Drawing.Size(549, 440)
        Me.INDlcgErrorList.TextVisible = False
        '
        'INDliErrorList
        '
        Me.INDliErrorList.Control = Me.INDgcErrorList
        Me.INDliErrorList.CustomizationFormText = "INDliErrorList"
        Me.INDliErrorList.Location = New System.Drawing.Point(0, 0)
        Me.INDliErrorList.Name = "INDliErrorList"
        Me.INDliErrorList.Size = New System.Drawing.Size(529, 371)
        Me.INDliErrorList.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliErrorList.TextVisible = False
        '
        'INDLygQuestionControl
        '
        Me.INDLygQuestionControl.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciNot, Me.INDLciYes, Me.LayoutControlItem4})
        Me.INDLygQuestionControl.Location = New System.Drawing.Point(0, 371)
        Me.INDLygQuestionControl.Name = "INDLygQuestionControl"
        Me.INDLygQuestionControl.Size = New System.Drawing.Size(529, 49)
        Me.INDLygQuestionControl.Text = "Pregunta de Control"
        Me.INDLygQuestionControl.TextVisible = False
        Me.INDLygQuestionControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciNot
        '
        Me.INDLciNot.Control = Me.INDBtnNot
        Me.INDLciNot.CustomizationFormText = "No"
        Me.INDLciNot.Location = New System.Drawing.Point(455, 0)
        Me.INDLciNot.MaxSize = New System.Drawing.Size(50, 25)
        Me.INDLciNot.MinSize = New System.Drawing.Size(50, 25)
        Me.INDLciNot.Name = "INDLciNot"
        Me.INDLciNot.Size = New System.Drawing.Size(50, 25)
        Me.INDLciNot.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNot.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNot.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciNot.TextToControlDistance = 0
        Me.INDLciNot.TextVisible = False
        '
        'INDLciYes
        '
        Me.INDLciYes.Control = Me.INDbtnYes
        Me.INDLciYes.CustomizationFormText = "SI"
        Me.INDLciYes.Location = New System.Drawing.Point(405, 0)
        Me.INDLciYes.MaxSize = New System.Drawing.Size(50, 25)
        Me.INDLciYes.MinSize = New System.Drawing.Size(50, 25)
        Me.INDLciYes.Name = "INDLciYes"
        Me.INDLciYes.Size = New System.Drawing.Size(50, 25)
        Me.INDLciYes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciYes.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciYes.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciYes.TextToControlDistance = 0
        Me.INDLciYes.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDLabelQuestion
        Me.LayoutControlItem4.CustomizationFormText = "Pregunta de control"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(405, 25)
        Me.LayoutControlItem4.Text = "Pregunta de control"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmListErrors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(549, 440)
        Me.Controls.Add(Me.LayoutControl1)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmListErrors"
        Me.ShowInTaskbar = False
        Me.Text = "Listado de Errores"
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgcErrorList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvErrorList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgErrorList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliErrorList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLygQuestionControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciYes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgErrorList As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcErrorList As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvErrorList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliErrorList As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDcolError As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents INDLabelQuestion As Label
    Friend WithEvents INDBtnNot As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnYes As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciNot As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciYes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Public WithEvents INDLygQuestionControl As DevExpress.XtraLayout.LayoutControlGroup
End Class
