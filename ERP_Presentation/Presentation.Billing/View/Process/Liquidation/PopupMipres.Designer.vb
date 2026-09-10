<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PopupMipres
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
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PopupMipres))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcMipres = New DevExpress.XtraGrid.GridControl()
        Me.INDGvMipres = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTeCode = New DevExpress.XtraEditors.TextEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDSbAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTeIdMipres = New DevExpress.XtraEditors.TextEdit()
        Me.IndigoTextEdit11 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDLciId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcMipres, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvMipres, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeIdMipres.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciId, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcMipres)
        Me.LayoutControl1.Controls.Add(Me.INDSbAdd)
        Me.LayoutControl1.Controls.Add(Me.INDTeCode)
        Me.LayoutControl1.Controls.Add(Me.INDTeIdMipres)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(780, 285)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcMipres
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcMipres, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcMipres, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcMipres, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcMipres, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcMipres, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcMipres, False)
        Me.INDGcMipres.Location = New System.Drawing.Point(12, 44)
        Me.INDGcMipres.MainView = Me.INDGvMipres
        Me.INDGcMipres.Name = "INDGcMipres"
        Me.INDGcMipres.Size = New System.Drawing.Size(756, 229)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcMipres, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcMipres.TabIndex = 6
        Me.INDGcMipres.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvMipres})
        '
        'INDGvMipres
        '
        Me.INDGvMipres.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvMipres.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvMipres.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvMipres.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvMipres.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvMipres.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvMipres.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvMipres.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvMipres.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvMipres.Appearance.Row.Options.UseFont = True
        Me.INDGvMipres.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvMipres.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvMipres.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.INDGvMipres.GridControl = Me.INDGcMipres
        Me.INDGvMipres.Name = "INDGvMipres"
        Me.INDGvMipres.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvMipres.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvMipres.OptionsView.ShowAutoFilterRow = True
        Me.INDGvMipres.OptionsView.ShowDetailButtons = False
        Me.INDGvMipres.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvMipres, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Número Mipres"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDSbAdd
        '
        Me.INDSbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAdd.Appearance.Options.UseFont = True
        Me.INDSbAdd.Location = New System.Drawing.Point(692, 12)
        Me.INDSbAdd.Name = "INDSbAdd"
        Me.INDSbAdd.Size = New System.Drawing.Size(76, 28)
        Me.INDSbAdd.StyleController = Me.LayoutControl1
        Me.INDSbAdd.TabIndex = 5
        Me.INDSbAdd.Text = "Agregar"
        '
        'INDTeCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeCode, True)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDTeCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeCode, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDTeCode, False)
        Me.INDTeCode.EnterMoveNextControl = True
        Me.INDTeCode.Location = New System.Drawing.Point(126, 12)
        Me.IndigoTextEdit11.SetMascara(Me.INDTeCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTeCode.Name = "INDTeCode"
        Me.INDTeCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeCode.Properties.Appearance.Options.UseFont = True
        Me.INDTeCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeCode.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTeCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTeCode.Properties.MaxLength = 20
        Me.INDTeCode.Size = New System.Drawing.Size(222, 28)
        Me.INDTeCode.StyleController = Me.LayoutControl1
        Me.INDTeCode.TabIndex = 4
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDTeCode, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeCode, 0)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.INDLciId})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(780, 285)
        Me.Root.TextVisible = False
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDTeCode
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(340, 32)
        Me.INDLciCode.MinSize = New System.Drawing.Size(340, 32)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.Size = New System.Drawing.Size(340, 32)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Número Mipres"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextSize = New System.Drawing.Size(99, 17)
        Me.INDLciCode.TextToControlDistance = 15
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSbAdd
        Me.LayoutControlItem2.Location = New System.Drawing.Point(680, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(80, 32)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(80, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(80, 32)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDGcMipres
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(760, 233)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'INDSbAccept
        '
        Me.INDSbAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAccept.Appearance.Options.UseFont = True
        Me.INDSbAccept.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDSbAccept.Location = New System.Drawing.Point(0, 285)
        Me.INDSbAccept.Name = "INDSbAccept"
        Me.INDSbAccept.Size = New System.Drawing.Size(780, 36)
        Me.INDSbAccept.TabIndex = 1
        Me.INDSbAccept.Text = "Aceptar"
        '
        'INDTeIdMipres
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeIdMipres, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDTeIdMipres, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeIdMipres, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDTeIdMipres, False)
        Me.INDTeIdMipres.EnterMoveNextControl = True
        Me.INDTeIdMipres.Location = New System.Drawing.Point(466, 12)
        Me.IndigoTextEdit11.SetMascara(Me.INDTeIdMipres, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeIdMipres, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeIdMipres.Name = "INDTeIdMipres"
        Me.INDTeIdMipres.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeIdMipres.Properties.Appearance.Options.UseFont = True
        Me.INDTeIdMipres.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeIdMipres.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeIdMipres.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTeIdMipres.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTeIdMipres.Properties.MaxLength = 20
        Me.INDTeIdMipres.Size = New System.Drawing.Size(222, 28)
        Me.INDTeIdMipres.StyleController = Me.LayoutControl1
        Me.INDTeIdMipres.TabIndex = 4
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDTeIdMipres, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeIdMipres, 0)
        '
        'INDLciId
        '
        Me.INDLciId.Control = Me.INDTeIdMipres
        Me.INDLciId.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciId.CustomizationFormText = "Número Mipres"
        Me.INDLciId.Location = New System.Drawing.Point(340, 0)
        Me.INDLciId.MaxSize = New System.Drawing.Size(340, 32)
        Me.INDLciId.MinSize = New System.Drawing.Size(340, 32)
        Me.INDLciId.Name = "INDLciId"
        Me.INDLciId.Size = New System.Drawing.Size(340, 32)
        Me.INDLciId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciId.Text = "ID Mipres"
        Me.INDLciId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciId.TextSize = New System.Drawing.Size(99, 17)
        Me.INDLciId.TextToControlDistance = 15
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "ID Mipres"
        Me.GridColumn2.FieldName = "IdMipres"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'PopupMipres
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 321)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.INDSbAccept)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Image = CType(resources.GetObject("PopupMipres.IconOptions.Image"), System.Drawing.Image)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PopupMipres"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cambiar Número Mipres"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcMipres, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvMipres, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeIdMipres.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciId, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTeCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcMipres As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvMipres As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
    Friend WithEvents INDSbAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoTextEdit11 As Controls.IndigoTextEdit
    Friend WithEvents INDTeIdMipres As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
