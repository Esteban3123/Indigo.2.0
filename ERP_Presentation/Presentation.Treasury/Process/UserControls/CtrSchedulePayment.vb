Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.Payments.MVP
Imports Presentation.Portfolio.MVP
Imports DevExpress.XtraLayout

Public Class CtrSchedulePayment

#Region "Delegate"

    ''' <summary>
    ''' Gets or sets the third identifier.
    ''' </summary>
    ''' <value>
    ''' The third identifier.
    ''' </value>
    Property ThirdId As Integer

    ''' <summary>
    ''' Obtiene o establece el datasource de las facturas
    ''' </summary>
    Property InvoiceShareDatasource As List(Of SP_SchedulePayment_Result)
        Get
            Return CType(INDgcInvoice.DataSource, List(Of SP_SchedulePayment_Result))
        End Get
        Set(value As List(Of SP_SchedulePayment_Result))
            INDgcInvoice.DataSource = value
            LayoutControl1.BeginUpdate()
            DeleteControlsLabel()
            'deacuerdo a las monedas que hay en el documento se crea dinamicamente los labelcontrols
            If value IsNot Nothing Then
                Dim groupMoney = (From item In value
                                  Group By item.CurrencyAbbreviation Into Group
                                  Select CurrencyAbbreviation, SumCurrency = Group.Sum(Function(f) f.PayValue))

                Dim layoutControlBehind As DevExpress.XtraLayout.LayoutControlItem = Nothing

                For Each item In groupMoney

                    Dim newLabel As New DevExpress.XtraEditors.LabelControl()
                    newLabel.Appearance.Options.UseFont = True
                    newLabel.Appearance.Options.UseForeColor = True
                    newLabel.Text = Utils.GetMoneyWithISO4217(item.SumCurrency, item.CurrencyAbbreviation)
                    newLabel.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                    Dim newLayoutControl As New LayoutControlItem()
                    newLayoutControl.Control = newLabel
                    newLayoutControl.TextVisible = False
                    newLayoutControl.Padding = New DevExpress.XtraLayout.Utils.Padding(0)
                    Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(newLayoutControl)

                    Me.LayoutControl1.AddItem(newLayoutControl)
                Next
            End If
            LayoutControl1.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' limpia los controles previamente creados
    ''' </summary>
    Private Sub DeleteControlsLabel()
        For Each item As LayoutControlItem In LayoutControl1.Items.ToList().FindAll(Function(x) x.GetType.Name = "LayoutControlItem")
            ' Verificar si el item es un LayoutControlItem que deseas eliminar
            If item?.Control IsNot Nothing AndAlso String.IsNullOrEmpty(item.Control.Name) AndAlso item.Control.GetType.Name = "LabelControl" Then
                ' Eliminar el LayoutControlItem
                Me.LayoutControl1.Controls.Remove(item.Control)
                LayoutControl1.Remove(item)
            End If
        Next
    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets or sets the title.
    ''' </summary>
    ''' <value>
    ''' The title.
    ''' </value>
    Public Property Title As String
        Get
            Return INDlblAdvanceTitle.Text
        End Get
        Set(value As String)
            INDlblAdvanceTitle.Text = value
        End Set
    End Property

#End Region

#Region "Functions"

    ''' <summary>
    ''' Muestra el valor del avance en el control de usuario
    ''' </summary>
    Public Sub PrintAdvance()
        INDgvInvoice.ExpandAllGroups()
        'INDlblAdvanceValue.Text = _functionSchedule().ToString("C0", ConfigurationFile.Instance.Culture)
        IndigoGridControl1.RefreshGrid(INDgcInvoice)
        AddHandler INDlcgAdvance.Click, AddressOf CtrAdvanceTreasury_Click
        AddHandler LayoutControl1.Click, AddressOf CtrAdvanceTreasury_Click
        AddHandler INDliAdvanceTitle.Click, AddressOf CtrAdvanceTreasury_Click
        AddHandler INDlblAdvanceTitle.Click, AddressOf CtrAdvanceTreasury_Click
        AddHandler MyBase.Click, AddressOf CtrAdvanceTreasury_Click
        AddHandler INDgvInvoice.DataSourceChanged, AddressOf INDgvAdvance_DataSourceChanged
        'INDpceAdvanceDetail.Visible = True 'habilita las propiedades del container edit
    End Sub

#End Region

#Region "Events"
    ''' <summary>
    ''' Handles the Click event of the CtrAdvanceTreasury control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrAdvanceTreasury_Click(sender As Object, e As EventArgs)
        INDpceAdvanceDetail.ShowPopup()
    End Sub

    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgvAdvance control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgvAdvance_DataSourceChanged(sender As Object, e As EventArgs)
        If InvoiceShareDatasource IsNot Nothing AndAlso InvoiceShareDatasource.Count > 0 Then
            INDgvInvoice.OptionsView.ShowFooter = True
        Else
            INDgvInvoice.OptionsView.ShowFooter = False
        End If
    End Sub
#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.INDlblAdvanceTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDliAdvanceTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblAdvanceTitle)
    End Sub

End Class