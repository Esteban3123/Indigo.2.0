Imports Presentation.Base.Extension

Public Class CtrValueAccountReceivableDocuments

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor de la cuenta por pagar, el valor de pagos y el valor de deducciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function TotalValuesDelegate() As Decimal

    ''' <summary>
    ''' Apuntador del delegado
    ''' </summary>
    ''' <remarks></remarks>
    Private _functionTotalValues As TotalValuesDelegate

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

#Region "Methods"

    ''' <summary>
    ''' Asigna el delegado que se va ejecutar para obtener el valor cxp y el valor factura
    ''' </summary>
    ''' <param name="functionTotalValues"></param>
    ''' <remarks></remarks>
    Public Sub SetTotalValues(functionTotalValues As TotalValuesDelegate)
        _functionTotalValues = functionTotalValues
    End Sub

    ''' <summary>
    ''' Metodo para refrescar los valores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshTotalValues()
        If _functionTotalValues IsNot Nothing Then
            Dim ValueTotal = _functionTotalValues()
            INDlblAdvanceValue.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(ValueTotal, _codeISO4217)
        End If
    End Sub


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
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblAdvanceValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblAdvanceTitle)
    End Sub
#End Region

End Class
