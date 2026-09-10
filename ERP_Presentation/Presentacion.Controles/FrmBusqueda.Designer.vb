<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBusqueda
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBusqueda))
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGridBusquedas = New DevExpress.XtraGrid.GridControl()
        Me.INDGridBusquedasView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDbtnAceptar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnCancelar = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDpcButtons = New DevExpress.XtraEditors.PanelControl()
        Me.INDpcGrid = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblTitle = New DevExpress.XtraEditors.LabelControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDGridBusquedas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGridBusquedasView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpcButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpcButtons.SuspendLayout()
        CType(Me.INDpcGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpcGrid.SuspendLayout()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDGridBusquedas
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGridBusquedas, Nothing)
        resources.ApplyResources(Me.INDGridBusquedas, "INDGridBusquedas")
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGridBusquedas, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGridBusquedas, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGridBusquedas, False)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGridBusquedas, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGridBusquedas, False)
        Me.INDGridBusquedas.MainView = Me.INDGridBusquedasView
        Me.INDGridBusquedas.Name = "INDGridBusquedas"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGridBusquedas, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGridBusquedas.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGridBusquedasView})
        '
        'INDGridBusquedasView
        '
        Me.INDGridBusquedasView.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGridBusquedasView.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGridBusquedasView.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGridBusquedasView.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGridBusquedasView.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGridBusquedasView.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGridBusquedasView.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGridBusquedasView.Appearance.GroupRow.Font = CType(resources.GetObject("INDGridBusquedasView.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGridBusquedasView.Appearance.GroupRow.Options.UseFont = True
        Me.INDGridBusquedasView.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGridBusquedasView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGridBusquedasView.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGridBusquedasView.Appearance.Row.Font = CType(resources.GetObject("INDGridBusquedasView.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGridBusquedasView.Appearance.Row.Options.UseFont = True
        Me.INDGridBusquedasView.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGridBusquedasView.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGridBusquedasView.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGridBusquedasView.GridControl = Me.INDGridBusquedas
        Me.INDGridBusquedasView.Name = "INDGridBusquedasView"
        Me.INDGridBusquedasView.OptionsBehavior.Editable = False
        Me.INDGridBusquedasView.OptionsBehavior.ReadOnly = True
        Me.INDGridBusquedasView.OptionsFind.AllowFindPanel = False
        Me.INDGridBusquedasView.OptionsPrint.EnableAppearanceEvenRow = True
        Me.INDGridBusquedasView.OptionsPrint.EnableAppearanceOddRow = True
        Me.INDGridBusquedasView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGridBusquedasView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGridBusquedasView.OptionsView.EnableAppearanceOddRow = True
        Me.INDGridBusquedasView.OptionsView.ShowAutoFilterRow = True
        Me.INDGridBusquedasView.OptionsView.ShowFooter = True
        Me.INDGridBusquedasView.OptionsView.ShowGroupPanel = False
        Me.INDGridBusquedasView.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Panel
        Me.INDGridBusquedasView.Tag = 474
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGridBusquedasView, False)
        '
        'INDbtnAceptar
        '
        Me.INDbtnAceptar.Appearance.Font = CType(resources.GetObject("INDbtnAceptar.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnAceptar.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDbtnAceptar, "INDbtnAceptar")
        Me.INDbtnAceptar.Name = "INDbtnAceptar"
        '
        'INDbtnCancelar
        '
        Me.INDbtnCancelar.Appearance.Font = CType(resources.GetObject("INDbtnCancelar.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnCancelar.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDbtnCancelar, "INDbtnCancelar")
        Me.INDbtnCancelar.Name = "INDbtnCancelar"
        '
        'INDpcButtons
        '
        Me.INDpcButtons.Controls.Add(Me.INDbtnAceptar)
        Me.INDpcButtons.Controls.Add(Me.INDbtnCancelar)
        resources.ApplyResources(Me.INDpcButtons, "INDpcButtons")
        Me.INDpcButtons.Name = "INDpcButtons"
        '
        'INDpcGrid
        '
        Me.INDpcGrid.Controls.Add(Me.INDGridBusquedas)
        resources.ApplyResources(Me.INDpcGrid, "INDpcGrid")
        Me.INDpcGrid.Name = "INDpcGrid"
        '
        'INDlblTitle
        '
        Me.INDlblTitle.Appearance.Font = CType(resources.GetObject("INDlblTitle.Appearance.Font"), System.Drawing.Font)
        Me.INDlblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlblTitle.Appearance.Options.UseFont = True
        Me.INDlblTitle.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.INDlblTitle, "INDlblTitle")
        Me.INDlblTitle.Name = "INDlblTitle"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'FrmBusqueda
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDpcGrid)
        Me.Controls.Add(Me.INDlblTitle)
        Me.Controls.Add(Me.INDpcButtons)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBusqueda"
        Me.ShowInTaskbar = False
        CType(Me.INDGridBusquedas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGridBusquedasView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpcButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpcButtons.ResumeLayout(False)
        CType(Me.INDpcGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpcGrid.ResumeLayout(False)
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    ''' <summary>
    ''' Le da estilo al panel de busqueda de la regilla
    ''' </summary>
    Private Sub SetStyleFindPanel()
        'For Each c As Control In Me.INDGridBusquedas.Controls
        '    If c.GetType().Equals(GetType(DevExpress.XtraGrid.Controls.FindControl)) Then
        '        CType(c, DevExpress.XtraGrid.Controls.FindControl).Appearance.BackColor = Color.White
        '        CType(c, DevExpress.XtraGrid.Controls.FindControl).FindEdit.Font = New Font("Segoe UI Light", 12.0!)
        '        CType(c, DevExpress.XtraGrid.Controls.FindControl).FindButton.Font = New Font("Segoe UI Light", 12.0!)
        '        CType(c, DevExpress.XtraGrid.Controls.FindControl).FindButton.Size = New Size(100, 36)
        '        CType(c, DevExpress.XtraGrid.Controls.FindControl).ClearButton.Font = New Font("Segoe UI Light", 12.0!)
        '        CType(c, DevExpress.XtraGrid.Controls.FindControl).ClearButton.Size = New Size(100, 36)
        '        CType(c, DevExpress.XtraGrid.Controls.FindControl).MinimumSize = New Size(CType(c, DevExpress.XtraGrid.Controls.FindControl).Size.Width, CType(c, DevExpress.XtraGrid.Controls.FindControl).Size.Height + 30)
        '        CType(c, DevExpress.XtraGrid.Controls.FindControl).MaximumSize = New Size(CType(c, DevExpress.XtraGrid.Controls.FindControl).Size.Width, CType(c, DevExpress.XtraGrid.Controls.FindControl).Size.Height + 30)
        '    End If
        'Next
    End Sub

    Friend WithEvents INDGridBusquedas As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGridBusquedasView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDbtnAceptar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnCancelar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDpcButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpcGrid As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
End Class
