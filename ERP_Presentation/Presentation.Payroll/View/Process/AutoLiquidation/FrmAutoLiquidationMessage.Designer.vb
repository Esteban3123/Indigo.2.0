<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAutoLiquidationMessage
    Inherits Presentation.Controls.FormBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAutoLiquidationMessage))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcMessageAutoliquidation = New DevExpress.XtraGrid.GridControl()
        Me.INDgvAutoliquidationMessage = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColIcon = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptIcbStatus = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGroupMessage = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcMessageAutoliquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvAutoliquidationMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptIcbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGroupMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(651, 295)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(651, 94)
        Me.ToolBars.Visible = False
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(651, 94)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcMessageAutoliquidation)
        Me.LayoutControl1.Controls.Add(Me.INDbtnCancel)
        Me.LayoutControl1.Controls.Add(Me.INDbtnOK)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(647, 286)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcMessageAutoliquidation
        '
        Me.INDGcMessageAutoliquidation.Location = New System.Drawing.Point(24, 59)
        Me.INDGcMessageAutoliquidation.MainView = Me.INDgvAutoliquidationMessage
        Me.INDGcMessageAutoliquidation.Name = "INDGcMessageAutoliquidation"
        Me.INDGcMessageAutoliquidation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptIcbStatus})
        Me.INDGcMessageAutoliquidation.Size = New System.Drawing.Size(599, 146)
        Me.INDGcMessageAutoliquidation.TabIndex = 7
        Me.INDGcMessageAutoliquidation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvAutoliquidationMessage})
        '
        'INDgvAutoliquidationMessage
        '
        Me.INDgvAutoliquidationMessage.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvAutoliquidationMessage.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvAutoliquidationMessage.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvAutoliquidationMessage.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvAutoliquidationMessage.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvAutoliquidationMessage.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvAutoliquidationMessage.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvAutoliquidationMessage.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvAutoliquidationMessage.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvAutoliquidationMessage.Appearance.Row.Options.UseFont = True
        Me.INDgvAutoliquidationMessage.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvAutoliquidationMessage.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvAutoliquidationMessage.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColIcon, Me.GridColumn2})
        Me.INDgvAutoliquidationMessage.GridControl = Me.INDGcMessageAutoliquidation
        Me.INDgvAutoliquidationMessage.Name = "INDgvAutoliquidationMessage"
        Me.INDgvAutoliquidationMessage.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvAutoliquidationMessage.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvAutoliquidationMessage.OptionsView.ShowAutoFilterRow = True
        Me.INDgvAutoliquidationMessage.OptionsView.ShowGroupPanel = False
        '
        'INDColIcon
        '
        Me.INDColIcon.Caption = "Ícono"
        Me.INDColIcon.ColumnEdit = Me.INDRptIcbStatus
        Me.INDColIcon.FieldName = "Item1"
        Me.INDColIcon.Name = "INDColIcon"
        Me.INDColIcon.Visible = True
        Me.INDColIcon.VisibleIndex = 0
        Me.INDColIcon.Width = 112
        '
        'INDRptIcbStatus
        '
        Me.INDRptIcbStatus.AutoHeight = False
        Me.INDRptIcbStatus.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptIcbStatus.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Error", CType(2, Byte), 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Advertencia", CType(1, Byte), 0)})
        Me.INDRptIcbStatus.LargeImages = Me.ImageCollection1
        Me.INDRptIcbStatus.Name = "INDRptIcbStatus"
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "amarillo_16x16.png")
        Me.ImageCollection1.Images.SetKeyName(1, "rojo_16x16.png")
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Mensaje"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 469
        '
        'INDbtnCancel
        '
        Me.INDbtnCancel.Location = New System.Drawing.Point(325, 226)
        Me.INDbtnCancel.MaximumSize = New System.Drawing.Size(0, 36)
        Me.INDbtnCancel.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDbtnCancel.Name = "INDbtnCancel"
        Me.INDbtnCancel.Size = New System.Drawing.Size(298, 36)
        Me.INDbtnCancel.StyleController = Me.LayoutControl1
        Me.INDbtnCancel.TabIndex = 6
        Me.INDbtnCancel.Text = "Cancelar"
        '
        'INDbtnOK
        '
        Me.INDbtnOK.Location = New System.Drawing.Point(24, 226)
        Me.INDbtnOK.MaximumSize = New System.Drawing.Size(0, 36)
        Me.INDbtnOK.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDbtnOK.Name = "INDbtnOK"
        Me.INDbtnOK.Size = New System.Drawing.Size(297, 36)
        Me.INDbtnOK.StyleController = Me.LayoutControl1
        Me.INDbtnOK.TabIndex = 5
        Me.INDbtnOK.Text = "Aceptar"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGroupMessage})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(647, 286)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGroupMessage
        '
        Me.INDlyGroupMessage.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupMessage.AppearanceGroup.Options.UseFont = True
        Me.INDlyGroupMessage.AppearanceGroup.Options.UseTextOptions = True
        Me.INDlyGroupMessage.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlyGroupMessage.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupMessage.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGroupMessage.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupMessage.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGroupMessage.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGroupMessage.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGroupMessage.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupMessage.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGroupMessage.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupMessage.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGroupMessage.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupMessage.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGroupMessage, False)
        Me.INDlyGroupMessage.CustomizationFormText = "LayoutControlGroup2"
        Me.INDlyGroupMessage.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.EmptySpaceItem1, Me.LayoutControlItem3})
        Me.INDlyGroupMessage.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGroupMessage.Name = "INDlyGroupMessage"
        Me.INDlyGroupMessage.Size = New System.Drawing.Size(627, 266)
        Me.INDlyGroupMessage.Text = "Mensajes de AutoLiquidación"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDbtnOK
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 167)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(301, 40)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDbtnCancel
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(301, 167)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(302, 40)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 150)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(603, 17)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDGcMessageAutoliquidation
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(620, 150)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(600, 150)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(603, 150)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'FrmAutoLiquidationMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(651, 413)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAutoLiquidationMessage"
        Me.Opacity = 1.0R
        Me.Text = ""
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcMessageAutoliquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvAutoliquidationMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptIcbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGroupMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDlyGroupMessage As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDbtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDGcMessageAutoliquidation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvAutoliquidationMessage As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColIcon As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents INDRptIcbStatus As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
End Class
