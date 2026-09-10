Imports System.Globalization
Imports Domain.Billing.POCO
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.BillingRepository

Public Class CtrTaxDevolution

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor a pagar por metodo de pago parametrizado en el iva devuelto
    ''' el primer item de la tupla especifica la lista, de valores apagar dependiendo el metodo de pago
    ''' el segundo item especifica la abreviacion de la moneda para establecer el simbolo de la moneda en los datos que viene
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function TaxDevolutionDelegate() As Tuple(Of List(Of TaxDevolution), String)

    Private _taxDevolutionDelegate As TaxDevolutionDelegate
#End Region

#Region "Properties"

    ''' <summary>
    ''' cultura
    ''' </summary>
    Private _culture As CultureInfo

#End Region

#Region "Methods"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="taxDevolutionDelegate"></param>
    Public Sub SetDataSourceTaxDevolution(taxDevolutionDelegate As TaxDevolutionDelegate)
        _taxDevolutionDelegate = taxDevolutionDelegate
    End Sub

    ''' <summary>
    ''' Carga del data source a la rejilla refrescando el simbolo de la moneda
    ''' </summary>
    Public Sub LoadDataSourceTaxDevolution()
        Dim DelegateFunction = _taxDevolutionDelegate()
        If DelegateFunction Is Nothing OrElse DelegateFunction?.Item1 Is Nothing Then
            Exit Sub
        End If
        INDgcTaxDevolution.DataSource = DelegateFunction.Item1
        INDgcTaxDevolution.RefreshDataSource()
        Me.SetCurrencyFormatGrid(DelegateFunction.Item2)
    End Sub

    ''' <summary>
    ''' establece el simbolo de la moneda
    ''' </summary>
    ''' <param name="CurrencyAbbreviation"></param>
    Private Sub SetCurrencyFormatGrid(CurrencyAbbreviation As String)
        Me.INDColValue = Window.Utils.FormatGrid(Me.INDColValue, CurrencyAbbreviation)
    End Sub

#End Region

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDgcTaxDevolution)
    End Sub

End Class
