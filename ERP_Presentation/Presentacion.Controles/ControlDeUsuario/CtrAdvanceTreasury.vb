Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports DevExpress.LookAndFeel
Imports DevExpress.Skins

Public Class CtrAdvanceTreasury

#Region "Deletate"
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
    ''' Obtiene o establece el datasource para los anticipos
    ''' </summary>
    ''' <value>
    ''' The advance datasource.
    ''' </value>
    Public Property AdvanceDatasource As List(Of AdvancePayments)
        Get
            Return CType(INDgcAdvance.DataSource, List(Of AdvancePayments))
        End Get
        Set(value As List(Of AdvancePayments))
            INDgcAdvance.DataSource = value
        End Set
    End Property

    Public Property Title As String
        Get
            Return INDlblAdvanceTitle.Text
        End Get
        Set(value As String)
            INDlblAdvanceTitle.Text = value
        End Set
    End Property

    Private _codeISO4217 As String
    ''' <summary>
    ''' codigo ISO4217
    ''' </summary>
    Public WriteOnly Property CodeISO4217 As String
        Set(value As String)
            _codeISO4217 = value
        End Set
    End Property

    Public Property WithEvent As Boolean

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
        _codeISO4217 = If(String.IsNullOrEmpty(_codeISO4217), SessionValues.Instance.CurrencyISO4217, _codeISO4217)

        If _functionAdvance IsNot Nothing Then
            IndigoGridControl1.RefreshGrid(INDgcAdvance)
            Dim advance As Decimal = _functionAdvance()
            INDlblAdvanceValue.Text = Utils.GetMoneyWithISO4217(advance, _codeISO4217)

            If WithEvent Then
                AddHandler INDlcgAdvance.Click, AddressOf CtrAdvanceTreasury_Click
                AddHandler LayoutControl1.Click, AddressOf CtrAdvanceTreasury_Click
                AddHandler INDliAdvanceTitle.Click, AddressOf CtrAdvanceTreasury_Click
                AddHandler INDliAdvance.Click, AddressOf CtrAdvanceTreasury_Click
                AddHandler INDlblAdvanceValue.Click, AddressOf CtrAdvanceTreasury_Click
                AddHandler INDlblAdvanceTitle.Click, AddressOf CtrAdvanceTreasury_Click
                AddHandler MyBase.Click, AddressOf CtrAdvanceTreasury_Click
                AddHandler INDgvAdvance.DataSourceChanged, AddressOf INDgvAdvance_DataSourceChanged
                INDpceAdvanceDetail.Visible = WithEvent 'habilita las propiedades del container edit
            End If

        End If
    End Sub
#End Region

#Region "Events"

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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblAdvanceTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblAdvanceValue)
        'Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbCredit)

    End Sub

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
        If AdvanceDatasource IsNot Nothing AndAlso AdvanceDatasource.Count > 0 Then
            INDgvAdvance.OptionsView.ShowFooter = True
        Else
            INDgvAdvance.OptionsView.ShowFooter = False
        End If
    End Sub
#End Region

End Class
