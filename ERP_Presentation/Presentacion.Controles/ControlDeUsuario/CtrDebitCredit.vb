Imports Presentation.Base.Extension
Public Class CtrDebitCredit

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor del debito y credito
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function DelegateGetDebitAndCredit() As Tuple(Of Decimal, Decimal)

    Private _functionDebitAndCredit As DelegateGetDebitAndCredit

    ''' <summary>
    ''' variable que guarda el codigo de 3 digitos de la moneda
    ''' </summary>
    Private _codeISO4217 As String

    ''' <summary>
    ''' propiedad de escritura que establece la abreviacion de la moneda
    ''' </summary>
    Public WriteOnly Property CodeISO4217 As String
        Set(value As String)
            _codeISO4217 = value
            INDlbAbbreviation.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna el delegado que se ca ejecutar para obtener el debito y el credito
    ''' </summary>
    ''' <param name="functionDebitAndCredit"></param>
    ''' <remarks></remarks>
    Public Sub SetDebitAndCredit(functionDebitAndCredit As DelegateGetDebitAndCredit)
        _functionDebitAndCredit = functionDebitAndCredit
    End Sub

    ''' <summary>
    ''' funcion para customizar el control 
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <param name="NameLeft"></param>
    ''' <param name="NameRight"></param>
    Public Sub SetNewNames(Title As String, NameLeft As String, NameRight As String)
        INDlbBalance.Text = Title
        LabelControl2.Text = NameLeft
        LabelControl3.Text = NameRight
    End Sub

    Public Sub RefreshDebitCredit()
        If _functionDebitAndCredit IsNot Nothing Then
            Dim tuplaDebitCredit = _functionDebitAndCredit()
            If tuplaDebitCredit.Item1 <> tuplaDebitCredit.Item2 Then
                INDlbBalance.BackColor = Color.Orange
                LayoutControlItem1.AppearanceItemCaption.BackColor = Color.Orange
            Else
                INDlbBalance.BackColor = System.Drawing.Color.FromArgb(0, 148, 223)
                LayoutControlItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(0, 148, 223)
            End If
            INDlbDebit.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(tuplaDebitCredit.Item1, _codeISO4217)
            INDlbCredit.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(tuplaDebitCredit.Item2, _codeISO4217)
        End If
    End Sub
    Private Sub CtrDebitCredit_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.INDlbAbbreviation.Text = _codeISO4217
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LabelControl2)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LabelControl3)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbDebit)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbCredit)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbAbbreviation)
    End Sub
End Class
