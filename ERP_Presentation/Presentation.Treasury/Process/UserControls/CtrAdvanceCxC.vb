Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.Payments.MVP
Imports Presentation.Portfolio.MVP
Imports Presentation.Base.Extension
Imports DevExpress.XtraEditors

Public Class CtrAdvanceCxC

#Region "Delegate"

    ''' <summary>
    ''' Gets or sets the third identifier.
    ''' </summary>
    ''' <value>
    ''' The third identifier.
    ''' </value>
    Property ThirdId As Integer

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor del anticipo
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function AdvanceDelegate() As Decimal

    ''' <summary>
    ''' funcion delegada para obtener el valor del anticipo
    ''' </summary>
    Private _functionAdvance As AdvanceDelegate

#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece el listado de los anticipos de CxC
    ''' </summary>
    ''' <value>
    ''' The advance portfolio.
    ''' </value>
    Property AdvancePortfolio As List(Of PortfolioAdvance)

    ''' <summary>
    ''' Obtiene o establece el datasource para los anticipos
    ''' </summary>
    ''' <value>
    ''' The advance datasource.
    ''' </value>
    Public Property AdvancePaymentDatasource As List(Of AdvancePayments)

    ''' <summary>
    ''' Une las lista de los anticipos de pagos y presupuesto
    ''' </summary>
    Private Sub ListAdvancePaymentPortfolioUnion()
        Dim listDatasource = New List(Of AdvanceObject)
        If AdvancePaymentDatasource IsNot Nothing AndAlso AdvancePaymentDatasource.Count > 0 Then
            Dim listPayment As List(Of AdvanceObject) = AdvancePaymentDatasource.Where(Function(a) a.Balance > 0).Cast(Of AdvancePayments).Select(Function(x) New AdvanceObject With {.Number = x.Code, .DateAdvance = x.DocumentDate, .MainAccount = x.FullNameMainAccount, .Observation = x.Comments, .Balance = x.Balance, .Type = "CxC"}).Cast(Of AdvanceObject)().ToList()
            listDatasource.AddRange(listPayment)
        End If
        If AdvancePortfolio IsNot Nothing AndAlso AdvancePortfolio.Count > 0 Then
            Dim listPortfolio As List(Of AdvanceObject) = AdvancePortfolio.Where(Function(a) a.Balance > 0).Cast(Of PortfolioAdvance).Select(Function(x) New AdvanceObject With {.Number = x.Code, .DateAdvance = x.DocumentDate, .MainAccount = x.FullNameMainAccount, .Observation = x.Observations, .Balance = x.Balance, .Type = "Anticipos"}).Cast(Of AdvanceObject)().ToList()
            listDatasource.AddRange(listPortfolio)
        End If
        INDgcAdvance.DataSource = listDatasource
    End Sub

    ''' <summary>
    ''' Gets the label value.
    ''' </summary>
    ''' <value>
    ''' The label value.
    ''' </value>
    Public ReadOnly Property LabelValue As LabelControl
        Get
            Return INDlblAdvanceTitle
        End Get
    End Property

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

    Private _codeISO4217 As String
    Public Property CurrencyCodeISO4217 As String
        Get
            Return If(String.IsNullOrEmpty(_codeISO4217), SessionValues.Instance.CurrencyISO4217, _codeISO4217)
        End Get
        Set(value As String)
            _codeISO4217 = value
        End Set
    End Property

#End Region

#Region "Functions"
    ''' <summary>
    ''' Asigna el delegado que se da al ejecutar para obtener el valor del avance
    ''' </summary>
    Public Sub SetAdvance(functionAdvance As AdvanceDelegate)
        _functionAdvance = functionAdvance
    End Sub

    ''' <summary>
    ''' Muestra el valor del avance en el control de usuario
    ''' </summary>
    Public Sub PrintAdvance()
        IndigoGridControl1.RefreshGrid(INDgcAdvance)
        If _functionAdvance IsNot Nothing Then
            LoadAdvance()
            AddHandler INDlcgAdvance.Click, AddressOf CtrAdvanceTreasury_Click
            AddHandler LayoutControl1.Click, AddressOf CtrAdvanceTreasury_Click
            AddHandler INDliAdvanceTitle.Click, AddressOf CtrAdvanceTreasury_Click
            AddHandler INDliAdvance.Click, AddressOf CtrAdvanceTreasury_Click
            AddHandler INDlblAdvanceValue.Click, AddressOf CtrAdvanceTreasury_Click
            AddHandler INDlblAdvanceTitle.Click, AddressOf CtrAdvanceTreasury_Click
            AddHandler MyBase.Click, AddressOf CtrAdvanceTreasury_Click
            AddHandler INDgvAdvance.DataSourceChanged, AddressOf INDgvAdvance_DataSourceChanged
            'INDpceAdvanceDetail.Visible = True 'habilita las propiedades del container edit
        End If
    End Sub

    ''' <summary>
    ''' Prints the value.
    ''' </summary>
    Public Sub PrintValue()
        If _functionAdvance IsNot Nothing Then
            INDlblAdvanceValue.Text = _functionAdvance().ToString("C2", CurrencyCodeISO4217.GetNumberFormat)
        End If
    End Sub

    ''' <summary>
    ''' carga el control de anticipos girados al tercero
    ''' </summary>
    Private Sub LoadAdvance()
        System.Threading.Tasks.Task.Factory.StartNew(Async Function() As Task
                                                         If ThirdId > 0 Then
                                                             Dim _advanceValue As Decimal
                                                             Using Model As New MAdvancePayments(Me.Tag), ModelPortfolio As New MPortfolioAdvance(Me.Tag)
                                                                 Dim _listAdvancePayment As List(Of AdvancePayments) = (Await Model.ListAdvancePaymentByThirdId(Convert.ToInt32(ThirdId))).ObjectEmbbeded
                                                                 If _listAdvancePayment IsNot Nothing AndAlso _listAdvancePayment.Count > 0 Then
                                                                     AdvancePaymentDatasource = _listAdvancePayment
                                                                     _advanceValue = _listAdvancePayment.Sum(Function(x) x.Balance)
                                                                 End If
                                                                 Dim _listPortfolioAdvance As List(Of PortfolioAdvance) = (Await ModelPortfolio.ListPorfolioAdvanceByThirdId(Convert.ToInt32(ThirdId))).ObjectEmbbeded
                                                                 If _listPortfolioAdvance IsNot Nothing AndAlso _listPortfolioAdvance.Count > 0 Then
                                                                     AdvancePortfolio = _listPortfolioAdvance
                                                                     _advanceValue += _listPortfolioAdvance.Sum(Function(x) x.Balance)
                                                                 End If
                                                                 ListAdvancePaymentPortfolioUnion()
                                                                 If INDlblAdvanceValue.InvokeRequired Then
                                                                     INDlblAdvanceValue.BeginInvoke(Sub()
                                                                                                        INDlblAdvanceValue.Text = _advanceValue.ToString("C2", New Globalization.CultureInfo(CurrencyCodeISO4217.GetCultureId()))
                                                                                                    End Sub)
                                                                 Else
                                                                     INDlblAdvanceValue.Text = _advanceValue.ToString("C2", New Globalization.CultureInfo(CurrencyCodeISO4217.GetCultureId()))
                                                                 End If
                                                                 If INDgcAdvance.InvokeRequired Then
                                                                     INDgcAdvance.BeginInvoke(Sub()
                                                                                                  INDgvAdvance.ExpandAllGroups()
                                                                                              End Sub)
                                                                 Else
                                                                     INDgvAdvance.ExpandAllGroups()
                                                                 End If
                                                             End Using
                                                         End If
                                                     End Function)
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
        If AdvancePaymentDatasource IsNot Nothing AndAlso AdvancePaymentDatasource.Count > 0 Then
            INDgvAdvance.OptionsView.ShowFooter = True
        Else
            INDgvAdvance.OptionsView.ShowFooter = False
        End If
    End Sub
#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDliAdvanceTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDliAdvance)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblAdvanceValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblAdvanceTitle)
    End Sub

End Class

Public Class AdvanceObject
    Public Property Number As String
    Public Property DateAdvance As Date
    Public Property MainAccount As String
    Public Property Observation As String
    Public Property Balance As Decimal
    Public Property Type As String
End Class
