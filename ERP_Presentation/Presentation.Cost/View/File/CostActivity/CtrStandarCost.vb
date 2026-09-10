#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
#End Region

Public Class CtrStandarCost

#Region "Globals"
    ''' <summary>
    ''' Propiedad que asigna el valor del Costo Estándar Promedio
    ''' </summary>
    Private _standarCostValue As Decimal
    Public Property StandarCostValue As Decimal
        Get
            Return _standarCostValue
        End Get
        Set(value As Decimal)
            _standarCostValue = value
            UpdateStandarCostValueLable()
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene la abreviación de la moneda
    ''' </summary>
    Public CurrencyAbbreviation As String

    ''' <summary>
    ''' formato de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyNumberFormat As Globalization.NumberFormatInfo
#End Region

#Region "Methods"
    ''' <summary>
    ''' Me muestra u oculta la información del Ctr
    ''' </summary>
    ''' <param name="showInformation"></param>
    Public Sub PrintInfo(ByVal showInformation As Boolean)
        INDLciTitle.HideControl(Not showInformation)
        INDLciStandarCostValue.HideControl(Not showInformation)
    End Sub

    ''' <summary>
    ''' Método que limpia los controles del Ctr
    ''' </summary>
    Public Sub CleanControls()
        CurrencyAbbreviation = Nothing
        StandarCostValue = 0
    End Sub

    ''' <summary>
    ''' Muestra los valores con el símbolo de moneda correspondiente
    ''' </summary>
    Public Sub SetCurrencyUI()
        CurrencyAbbreviation = If(String.IsNullOrEmpty(CurrencyAbbreviation), SessionValues.Instance.CurrencyISO4217, CurrencyAbbreviation)
        CurrencyNumberFormat = If(CurrencyNumberFormat, CurrencyAbbreviation?.GetNumberFormat)
    End Sub

    ''' <summary>
    ''' Muestra el texto con los valores del Ctr
    ''' </summary>
    Private Sub UpdateStandarCostValueLable()
        If CurrencyAbbreviation Is Nothing Then SetCurrencyUI()
        INDLStandarCostValue.Text = String.Format("{0}", Utils.GetMoneyWithISO4217(_standarCostValue, CurrencyAbbreviation, CurrencyNumberFormat?.CurrencyDecimalDigits))
    End Sub
#End Region

End Class
