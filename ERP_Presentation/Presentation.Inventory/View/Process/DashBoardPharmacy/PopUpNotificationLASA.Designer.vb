Imports System.Windows.Forms
Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PopUpNotificationLASA
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PopUpNotificationLASA))
        Me.INDpnButtons = New System.Windows.Forms.Panel()
        Me.INDbtnYes = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnNo = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDlcLASAMedications = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcLASAMedication = New DevExpress.XtraGrid.GridControl()
        Me.INDgvLASAMedication = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolRequestedMedication = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolLikeness = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolSimilarMedicine = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlbMessage = New DevExpress.XtraEditors.LabelControl()
        Me.INDlcgLASAMedications = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciMessage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciLASAMedication = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.INDpnButtons.SuspendLayout()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcLASAMedications, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcLASAMedications.SuspendLayout()
        CType(Me.INDgcLASAMedication, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvLASAMedication, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgLASAMedications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciLASAMedication, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDpnButtons
        '
        Me.INDpnButtons.Controls.Add(Me.INDbtnYes)
        Me.INDpnButtons.Controls.Add(Me.INDbtnNo)
        Me.INDpnButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpnButtons.Location = New System.Drawing.Point(0, 284)
        Me.INDpnButtons.Name = "INDpnButtons"
        Me.INDpnButtons.Size = New System.Drawing.Size(976, 45)
        Me.INDpnButtons.TabIndex = 0
        '
        'INDbtnYes
        '
        Me.INDbtnYes.Location = New System.Drawing.Point(354, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnYes, False)
        Me.INDbtnYes.Name = "INDbtnYes"
        Me.INDbtnYes.Size = New System.Drawing.Size(130, 35)
        Me.INDbtnYes.TabIndex = 1
        Me.INDbtnYes.Text = "Si"
        '
        'INDbtnNo
        '
        Me.INDbtnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.INDbtnNo.Location = New System.Drawing.Point(520, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnNo, False)
        Me.INDbtnNo.Name = "INDbtnNo"
        Me.INDbtnNo.Size = New System.Drawing.Size(130, 35)
        Me.INDbtnNo.TabIndex = 0
        Me.INDbtnNo.Text = "No"
        '
        'INDlcLASAMedications
        '
        Me.INDlcLASAMedications.Controls.Add(Me.INDgcLASAMedication)
        Me.INDlcLASAMedications.Controls.Add(Me.INDlbMessage)
        Me.INDlcLASAMedications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcLASAMedications.Location = New System.Drawing.Point(0, 0)
        Me.INDlcLASAMedications.Name = "INDlcLASAMedications"
        Me.INDlcLASAMedications.Root = Me.INDlcgLASAMedications
        Me.INDlcLASAMedications.Size = New System.Drawing.Size(976, 284)
        Me.INDlcLASAMedications.TabIndex = 1
        Me.INDlcLASAMedications.Text = "LayoutControl1"
        '
        'INDgcLASAMedication
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcLASAMedication, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcLASAMedication, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcLASAMedication, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcLASAMedication, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcLASAMedication, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcLASAMedication, False)
        Me.INDgcLASAMedication.Location = New System.Drawing.Point(12, 58)
        Me.INDgcLASAMedication.MainView = Me.INDgvLASAMedication
        Me.INDgcLASAMedication.Name = "INDgcLASAMedication"
        Me.INDgcLASAMedication.Size = New System.Drawing.Size(952, 214)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcLASAMedication, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcLASAMedication.TabIndex = 5
        Me.INDgcLASAMedication.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvLASAMedication})
        '
        'INDgvLASAMedication
        '
        Me.INDgvLASAMedication.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvLASAMedication.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvLASAMedication.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvLASAMedication.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvLASAMedication.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvLASAMedication.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvLASAMedication.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvLASAMedication.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvLASAMedication.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvLASAMedication.Appearance.Row.Options.UseFont = True
        Me.INDgvLASAMedication.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvLASAMedication.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvLASAMedication.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolRequestedMedication, Me.INDcolLikeness, Me.INDcolSimilarMedicine})
        Me.INDgvLASAMedication.GridControl = Me.INDgcLASAMedication
        Me.INDgvLASAMedication.Name = "INDgvLASAMedication"
        Me.INDgvLASAMedication.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvLASAMedication.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvLASAMedication.OptionsView.ShowAutoFilterRow = True
        Me.INDgvLASAMedication.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvLASAMedication, False)
        '
        'INDcolRequestedMedication
        '
        Me.INDcolRequestedMedication.Caption = "Medicamento Solicitado"
        Me.INDcolRequestedMedication.FieldName = "RequestedMedication"
        Me.INDcolRequestedMedication.Name = "INDcolRequestedMedication"
        Me.INDcolRequestedMedication.OptionsColumn.AllowEdit = False
        Me.INDcolRequestedMedication.OptionsColumn.AllowFocus = False
        Me.INDcolRequestedMedication.Visible = True
        Me.INDcolRequestedMedication.VisibleIndex = 0
        Me.INDcolRequestedMedication.Width = 339
        '
        'INDcolLikeness
        '
        Me.INDcolLikeness.Caption = "Semejanza"
        Me.INDcolLikeness.FieldName = "Likeness"
        Me.INDcolLikeness.Name = "INDcolLikeness"
        Me.INDcolLikeness.OptionsColumn.AllowEdit = False
        Me.INDcolLikeness.OptionsColumn.AllowFocus = False
        Me.INDcolLikeness.Visible = True
        Me.INDcolLikeness.VisibleIndex = 1
        Me.INDcolLikeness.Width = 195
        '
        'INDcolSimilarMedicine
        '
        Me.INDcolSimilarMedicine.Caption = "Medicamento Semejante"
        Me.INDcolSimilarMedicine.FieldName = "SimilarMedicine"
        Me.INDcolSimilarMedicine.Name = "INDcolSimilarMedicine"
        Me.INDcolSimilarMedicine.OptionsColumn.AllowEdit = False
        Me.INDcolSimilarMedicine.OptionsColumn.AllowFocus = False
        Me.INDcolSimilarMedicine.Visible = True
        Me.INDcolSimilarMedicine.VisibleIndex = 2
        Me.INDcolSimilarMedicine.Width = 400
        '
        'INDlbMessage
        '
        Me.INDlbMessage.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDlbMessage.Appearance.Options.UseFont = True
        Me.INDlbMessage.Appearance.Options.UseTextOptions = True
        Me.INDlbMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlbMessage.Location = New System.Drawing.Point(310, 12)
        Me.INDlbMessage.Name = "INDlbMessage"
        Me.INDlbMessage.Size = New System.Drawing.Size(356, 42)
        Me.INDlbMessage.StyleController = Me.INDlcLASAMedications
        Me.INDlbMessage.TabIndex = 4
        Me.INDlbMessage.Text = "Precaución, existen medicamentos semejantes, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "¿Esta seguro de esta dispensación?" &
    ""
        '
        'INDlcgLASAMedications
        '
        Me.INDlcgLASAMedications.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgLASAMedications.GroupBordersVisible = False
        Me.INDlcgLASAMedications.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciMessage, Me.INDlciLASAMedication})
        Me.INDlcgLASAMedications.Name = "INDlcgLASAMedications"
        Me.INDlcgLASAMedications.Size = New System.Drawing.Size(976, 284)
        Me.INDlcgLASAMedications.TextVisible = False
        '
        'INDlciMessage
        '
        Me.INDlciMessage.Control = Me.INDlbMessage
        Me.INDlciMessage.ControlAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.INDlciMessage.Location = New System.Drawing.Point(0, 0)
        Me.INDlciMessage.Name = "INDlciMessage"
        Me.INDlciMessage.Size = New System.Drawing.Size(956, 46)
        Me.INDlciMessage.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciMessage.TextVisible = False
        '
        'INDlciLASAMedication
        '
        Me.INDlciLASAMedication.Control = Me.INDgcLASAMedication
        Me.INDlciLASAMedication.Location = New System.Drawing.Point(0, 46)
        Me.INDlciLASAMedication.Name = "INDlciLASAMedication"
        Me.INDlciLASAMedication.Size = New System.Drawing.Size(956, 218)
        Me.INDlciLASAMedication.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciLASAMedication.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PopUpNotificationLASA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.INDbtnNo
        Me.ClientSize = New System.Drawing.Size(976, 329)
        Me.ControlBox = False
        Me.Controls.Add(Me.INDlcLASAMedications)
        Me.Controls.Add(Me.INDpnButtons)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.SvgImage = CType(resources.GetObject("PopUpNotificationLASA.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.Name = "PopUpNotificationLASA"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Medicamentos LASA"
        Me.INDpnButtons.ResumeLayout(False)
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcLASAMedications, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcLASAMedications.ResumeLayout(False)
        CType(Me.INDgcLASAMedication, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvLASAMedication, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgLASAMedications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciLASAMedication, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDpnButtons As Panel
    Friend WithEvents INDbtnNo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDbtnYes As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlcLASAMedications As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlbMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlcgLASAMedications As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciMessage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcLASAMedication As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvLASAMedication As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciLASAMedication As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolRequestedMedication As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolLikeness As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolSimilarMedicine As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
End Class
