Imports System.Globalization
Imports Presentation.Base.Extension
Imports Infrastructure.CrossCutting.Base
Public Class CtrValue

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor de la cuenta por pagar y el valor de la factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function TotalValuesDelegate() As Tuple(Of Decimal, Decimal, Decimal)

    ''' <summary>
    ''' Apuntador del delegado
    ''' </summary>
    ''' <remarks></remarks>
    Private _functionTotalValues As TotalValuesDelegate

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para establecer el texto del valor principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LabelValue1 As String
        Get
            Return INDLciValueCxP.Text
        End Get
        Set(value As String)
            INDLciValueCxP.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para establecer el texto del valor secundario
    ''' </summary>
    ''' <remarks></remarks>
    Public Property LabelValue2 As String
        Get
            Return INDLciBillValue.Text
        End Get
        Set(value As String)
            INDLciBillValue.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para establecer el texto del valor del IVA
    ''' </summary>
    ''' <remarks></remarks>
    Public Property LabelValueIVA As String
        Get
            Return INDLciIVA.Text
        End Get
        Set(value As String)
            INDLciIVA.Text = value
        End Set
    End Property

    ''' <summary>
    ''' cultura
    ''' </summary>
    Private _culture As CultureInfo

    ''' <summary>
    ''' Abreviacion moneda
    ''' </summary>
    Private _currencyAbbreviation As String
    Public WriteOnly Property CurrencyAbbreviation As String
        Set(value As String)
            _currencyAbbreviation = value
            _culture = CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = value.GetNumberFormat
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
            Dim tuplaTotalValues = _functionTotalValues()
            INDlbValueCxP.Text = tuplaTotalValues.Item1.MoneyFormat(Culture:=_culture)
            INDlbValueBill.Text = tuplaTotalValues.Item2.MoneyFormat(Culture:=_culture)
            INDlbValueIVA.Text = tuplaTotalValues.Item3.MoneyFormat(Culture:=_culture)
        End If
    End Sub

#End Region

    Private Sub CtrDebitCredit_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciBillValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciValueCxP)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValueCxP)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValueBill)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValueIVA)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciIVA)
    End Sub

End Class
