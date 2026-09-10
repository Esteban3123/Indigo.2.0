<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PopUpCampaignBatchRecord
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDgcBatch = New DevExpress.XtraGrid.GridControl()
        Me.INDviewBatchRecord = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptPic = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.INDBPrint = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewBatchRecord, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDgcBatch
        '
        Me.INDgcBatch.Location = New System.Drawing.Point(12, 3)
        Me.INDgcBatch.MainView = Me.INDviewBatchRecord
        Me.INDgcBatch.Name = "INDgcBatch"
        Me.INDgcBatch.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptPic})
        Me.INDgcBatch.Size = New System.Drawing.Size(444, 370)
        Me.INDgcBatch.TabIndex = 5
        Me.INDgcBatch.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewBatchRecord})
        '
        'INDviewBatchRecord
        '
        Me.INDviewBatchRecord.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewBatchRecord.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewBatchRecord.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewBatchRecord.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewBatchRecord.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewBatchRecord.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewBatchRecord.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewBatchRecord.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewBatchRecord.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewBatchRecord.Appearance.Row.Options.UseFont = True
        Me.INDviewBatchRecord.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewBatchRecord.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewBatchRecord.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.INDviewBatchRecord.GridControl = Me.INDgcBatch
        Me.INDviewBatchRecord.Name = "INDviewBatchRecord"
        Me.INDviewBatchRecord.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.INDviewBatchRecord.OptionsSelection.MultiSelect = True
        Me.INDviewBatchRecord.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDviewBatchRecord.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewBatchRecord.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewBatchRecord.OptionsView.ShowAutoFilterRow = True
        Me.INDviewBatchRecord.OptionsView.ShowDetailButtons = False
        Me.INDviewBatchRecord.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewBatchRecord, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Documento"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 374
        '
        'INDRptPic
        '
        Me.INDRptPic.Name = "INDRptPic"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'INDBPrint
        '
        Me.INDBPrint.Location = New System.Drawing.Point(353, 379)
        Me.INDBPrint.Name = "INDBPrint"
        Me.INDBPrint.Size = New System.Drawing.Size(103, 30)
        Me.INDBPrint.TabIndex = 6
        Me.INDBPrint.Text = "Imprimir"
        '
        'PopUpCampaignBatchRecord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(468, 421)
        Me.Controls.Add(Me.INDBPrint)
        Me.Controls.Add(Me.INDgcBatch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PopUpCampaignBatchRecord"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Batch Record "
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcBatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewBatchRecord, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDgcBatch As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewBatchRecord As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDBPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDRptPic As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
End Class
