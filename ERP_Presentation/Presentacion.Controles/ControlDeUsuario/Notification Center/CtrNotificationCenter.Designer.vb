<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrNotificationCenter
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
        Me.WpfSegmentControl = New Presentation.Controls.WpfSegmentControl()
        Me.INDpcNotificationControls = New DevExpress.XtraEditors.PanelControl()
        Me.INDgcProcess = New DevExpress.XtraGrid.GridControl()
        Me.INDgcvProcess = New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView()
        Me.gridBand5 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand3 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn4 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.INDxcMessages = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.INDxcTask = New DevExpress.XtraEditors.XtraScrollableControl()
        CType(Me.INDpcNotificationControls, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpcNotificationControls.SuspendLayout()
        CType(Me.INDgcProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcvProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ElementHost1
        '
        Me.ElementHost1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ElementHost1.Location = New System.Drawing.Point(0, 0)
        Me.ElementHost1.Name = "ElementHost1"
        Me.ElementHost1.Size = New System.Drawing.Size(416, 29)
        Me.ElementHost1.TabIndex = 0
        Me.ElementHost1.Text = "ElementHost"
        Me.ElementHost1.Child = Me.WpfSegmentControl
        '
        'INDpcNotificationControls
        '
        Me.INDpcNotificationControls.Controls.AddRange({INDgcProcess, INDxcMessages, INDxcTask})
        Me.INDpcNotificationControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDpcNotificationControls.Location = New System.Drawing.Point(0, 29)
        Me.INDpcNotificationControls.Name = "INDpcNotificationControls"
        Me.INDpcNotificationControls.Size = New System.Drawing.Size(416, 711)
        Me.INDpcNotificationControls.TabIndex = 1
        '
        'INDgcProcess
        '
        Me.INDgcProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDgcProcess.Location = New System.Drawing.Point(2, 2)
        Me.INDgcProcess.MainView = Me.INDgcvProcess
        Me.INDgcProcess.Name = "INDgcProcess"
        Me.INDgcProcess.Size = New System.Drawing.Size(412, 707)
        Me.INDgcProcess.TabIndex = 0
        Me.INDgcProcess.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgcvProcess})
        Me.INDgcProcess.Visible = False
        '
        'INDgcvProcess
        '
        Me.INDgcvProcess.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgcvProcess.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgcvProcess.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gridBand5, Me.gridBand2, Me.gridBand3})
        Me.INDgcvProcess.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.BandedGridColumn1, Me.BandedGridColumn2, Me.BandedGridColumn4})
        Me.INDgcvProcess.GridControl = Me.INDgcProcess
        Me.INDgcvProcess.Name = "INDgcvProcess"
        Me.INDgcvProcess.OptionsView.ShowBands = False
        Me.INDgcvProcess.OptionsView.ShowGroupPanel = False
        Me.INDgcvProcess.OptionsView.ShowIndicator = False
        Me.INDgcvProcess.RowHeight = 36
        '
        'gridBand5
        '
        Me.gridBand5.Caption = "gridBand5"
        Me.gridBand5.Columns.Add(Me.BandedGridColumn1)
        Me.gridBand5.Name = "gridBand5"
        Me.gridBand5.VisibleIndex = 0
        Me.gridBand5.Width = 185
        '
        'BandedGridColumn1
        '
        Me.BandedGridColumn1.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BandedGridColumn1.AppearanceCell.Options.UseFont = True
        Me.BandedGridColumn1.AutoFillDown = True
        Me.BandedGridColumn1.Caption = "Proceso"
        Me.BandedGridColumn1.FieldName = "Process"
        Me.BandedGridColumn1.Name = "BandedGridColumn1"
        Me.BandedGridColumn1.OptionsColumn.AllowEdit = False
        Me.BandedGridColumn1.OptionsColumn.AllowFocus = False
        Me.BandedGridColumn1.Visible = True
        Me.BandedGridColumn1.Width = 185
        '
        'gridBand2
        '
        Me.gridBand2.Caption = "gridBand2"
        Me.gridBand2.Columns.Add(Me.BandedGridColumn2)
        Me.gridBand2.Name = "gridBand2"
        Me.gridBand2.VisibleIndex = 1
        Me.gridBand2.Width = 103
        '
        'BandedGridColumn2
        '
        Me.BandedGridColumn2.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BandedGridColumn2.AppearanceCell.Options.UseFont = True
        Me.BandedGridColumn2.AutoFillDown = True
        Me.BandedGridColumn2.Caption = "Formulario"
        Me.BandedGridColumn2.FieldName = "Form"
        Me.BandedGridColumn2.Name = "BandedGridColumn2"
        Me.BandedGridColumn2.OptionsColumn.AllowEdit = False
        Me.BandedGridColumn2.OptionsColumn.AllowFocus = False
        Me.BandedGridColumn2.Visible = True
        Me.BandedGridColumn2.Width = 103
        '
        'gridBand3
        '
        Me.gridBand3.Caption = "gridBand3"
        Me.gridBand3.Columns.Add(Me.BandedGridColumn4)
        Me.gridBand3.Name = "gridBand3"
        Me.gridBand3.VisibleIndex = 2
        Me.gridBand3.Width = 123
        '
        'BandedGridColumn4
        '
        Me.BandedGridColumn4.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BandedGridColumn4.AppearanceCell.Options.UseFont = True
        Me.BandedGridColumn4.AutoFillDown = True
        Me.BandedGridColumn4.Caption = "Estado"
        Me.BandedGridColumn4.FieldName = "Status"
        Me.BandedGridColumn4.Name = "BandedGridColumn4"
        Me.BandedGridColumn4.OptionsColumn.AllowEdit = False
        Me.BandedGridColumn4.OptionsColumn.AllowFocus = False
        Me.BandedGridColumn4.Visible = True
        Me.BandedGridColumn4.Width = 123
        '
        'INDxcMessages
        '
        Me.INDxcMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDxcMessages.Location = New System.Drawing.Point(2, 2)
        Me.INDxcMessages.Name = "INDxcMessages"
        Me.INDxcMessages.Size = New System.Drawing.Size(412, 707)
        Me.INDxcMessages.TabIndex = 0
        Me.INDxcMessages.Visible = False
        '
        'INDxcTask
        '
        Me.INDxcTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDxcTask.Location = New System.Drawing.Point(2, 2)
        Me.INDxcTask.Name = "INDxcTask"
        Me.INDxcTask.Size = New System.Drawing.Size(412, 707)
        Me.INDxcTask.TabIndex = 0
        Me.INDxcTask.Visible = False
        '
        'CtrNotificationCenter
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDpcNotificationControls)
        Me.Controls.Add(Me.ElementHost1)
        Me.Name = "CtrNotificationCenter"
        Me.Size = New System.Drawing.Size(416, 740)
        CType(Me.INDpcNotificationControls, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpcNotificationControls.ResumeLayout(False)
        CType(Me.INDgcProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcvProcess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ElementHost1 As System.Windows.Forms.Integration.ElementHost
    Friend WpfSegmentControl As Presentation.Controls.WpfSegmentControl
    Friend WithEvents INDpcNotificationControls As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDgcProcess As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgcvProcess As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
    Friend WithEvents gridBand5 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents BandedGridColumn1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents BandedGridColumn2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gridBand3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents BandedGridColumn4 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Public WithEvents INDxcMessages As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents INDxcTask As DevExpress.XtraEditors.XtraScrollableControl

End Class
