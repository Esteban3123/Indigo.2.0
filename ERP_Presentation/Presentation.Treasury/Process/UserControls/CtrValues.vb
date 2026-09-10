Imports Presentation.Base.Extension
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities

Public Class CtrValues

#Region "Delegate"

    ''' <summary>
    ''' Delegado para especificar una funcion que me devuelva el valor de la cuenta por pagar y el valor de la factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function TotalValuesDelegate() As Tuple(Of Decimal, Decimal)

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
    Public Property ValueSchedule As String
        Get
            Return INDlbValueSchedule.Text
        End Get
        Set(value As String)
            INDlbValueSchedule.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para establecer el texto del valor secundario
    ''' </summary>
    ''' <remarks></remarks>
    Public Property TotalValue As String
        Get
            Return INDlbValueTotal.Text
        End Get
        Set(value As String)
            INDlbValueTotal.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para establecer el valor del descuento
    ''' </summary>
    ''' <returns></returns>
    Public Property ValueDiscount As String
        Get
            Return INDlbValueDiscount.Text
        End Get
        Set(value As String)
            INDlbValueDiscount.Text = value
        End Set
    End Property

    Public Property _culture As Globalization.CultureInfo = Nothing

    ''' <summary>
    ''' Obtiene o establece el datasource de las facturas
    ''' </summary>
    Property InvoiceShareDatasource As List(Of SP_SchedulePayment_Result)
        Get
            Return CType(INDgcInvoice.DataSource, List(Of SP_SchedulePayment_Result))
        End Get
        Set(value As List(Of SP_SchedulePayment_Result))
            INDgcInvoice.DataSource = value
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
            If _culture IsNot Nothing Then
                ValueDiscount = tuplaTotalValues.Item2
                ValueSchedule = tuplaTotalValues.Item1
                TotalValue = Convert.ToDecimal(Me.ValueSchedule - Me.ValueDiscount).MoneyFormat(_culture?.NumberFormat?.CurrencyDecimalDigits, _culture)
                ValueDiscount = tuplaTotalValues.Item2.MoneyFormat(_culture?.NumberFormat?.CurrencyDecimalDigits, _culture)
                ValueSchedule = tuplaTotalValues.Item1.MoneyFormat(_culture?.NumberFormat?.CurrencyDecimalDigits, _culture)
            Else
                ValueDiscount = tuplaTotalValues.Item2.MoneyFormat(0)
                ValueSchedule = tuplaTotalValues.Item1.MoneyFormat(0)
                TotalValue = Convert.ToDecimal(Me.ValueSchedule - Me.ValueDiscount).MoneyFormat(0)
            End If
        End If
    End Sub

#End Region

    Private Sub CtrValues_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciValueCxP)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciLabelTotalValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciValueTotal)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciLabelSchedule)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciLabelDiscount)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlcTotalDiscount)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValueDiscount)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciValueDiscount)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemValueSchedule)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValueTotal)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbItemTotalValue)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlbValueSchedule)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLbTitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLciSubtitle)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDLcSubtitle)
    End Sub

    ''' <summary>
    ''' evento para mostrar el popup de las facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDlbItemValueSchedule_Click(sender As Object, e As EventArgs) Handles INDlbItemValueSchedule.Click, INDlbValueSchedule.Click, INDlcTotalDiscount.Click, INDlbValueDiscount.Click, INDlbItemTotalValue.Click, INDlbValueTotal.Click, INDLbTitle.Click, LabelControl1.Click, INDLcSubtitle.Click
        INDpceAdvanceDetail.ShowPopup()
    End Sub

End Class
