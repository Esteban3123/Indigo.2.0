Imports Presentation.Base.Extension
Public Class CtrVoucherTransactionCrossing

#Region "Properties and variables"
    ''' <summary>
    ''' delegado de funcion que retorna el valor de las facturas y de los anticipos
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function DelegateFunctionCrossing() As Tuple(Of Decimal, Decimal)

    ''' <summary>
    ''' The _function crossing
    ''' </summary>
    Public _functionCrossing As DelegateFunctionCrossing

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
        End Set
    End Property

#End Region

#Region "Functions"
    ''' <summary>
    ''' se establece la función delegada
    ''' </summary>
    ''' <param name="functionCrossing">The function crossing.</param>
    Public Sub SetFunctionCrossing(functionCrossing As DelegateFunctionCrossing)
        _functionCrossing = functionCrossing
    End Sub

    ''' <summary>
    ''' Pinta los valores del cruce en el control
    ''' </summary>
    Public Sub PrintValueCrossing()
        If _functionCrossing IsNot Nothing Then
            Dim _result As Tuple(Of Decimal, Decimal) = _functionCrossing()
            INDlbCxPValue.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(_result.Item1, _codeISO4217)
            INDlbCxCValue.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(_result.Item2, _codeISO4217)
            INDlbDifference.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217((Math.Abs(_result.Item1 - _result.Item2)), _codeISO4217)

            If Not _result.Item1.MoneyFormat(0).Equals(_result.Item2.MoneyFormat(0)) Then
                CType(LayoutControl2, DevExpress.XtraLayout.LayoutControl).LookAndFeel.SetStyle(DevExpress.LookAndFeel.LookAndFeelStyle.Office2003, True, False)
                LayoutControl2.BackColor = Color.Orange
                INDlbDifference.BackColor = Color.Orange
            Else
                CType(LayoutControl2, DevExpress.XtraLayout.LayoutControl).LookAndFeel.SetStyle(DevExpress.LookAndFeel.LookAndFeelStyle.Office2003, True, False)
                LayoutControl2.BackColor = System.Drawing.Color.FromArgb(0, 148, 223)
                INDlbDifference.BackColor = System.Drawing.Color.FromArgb(0, 148, 223)
            End If
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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem2)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbCxPValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbCxCValue)
    End Sub

End Class
