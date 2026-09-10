'***********************************************************************
' Assembly         : Presentation.Reporter.ElectronicDocuments
' Adapted from Infrastructure.CrossCutting.Base
'***********************************************************************

Imports System.Globalization
Imports Microsoft.VisualBasic

Namespace Utilities

    ''' <summary>
    ''' Provee funciones utilitarias para los reportes
    ''' </summary>
    Public Class Utils

        ''' <summary>
        ''' Convierte un valor numérico a su representación en texto (español)
        ''' </summary>
        ''' <param name="value">Valor a convertir</param>
        ''' <returns>Número en letras</returns>
        Public Shared Function Num2Text(ByVal value As Double) As String
            Dim values = Math.Round(value, 0, MidpointRounding.AwayFromZero)
            Select Case values
                Case 0 : Num2Text = "CERO"
                Case 1 : Num2Text = "UN"
                Case 2 : Num2Text = "DOS"
                Case 3 : Num2Text = "TRES"
                Case 4 : Num2Text = "CUATRO"
                Case 5 : Num2Text = "CINCO"
                Case 6 : Num2Text = "SEIS"
                Case 7 : Num2Text = "SIETE"
                Case 8 : Num2Text = "OCHO"
                Case 9 : Num2Text = "NUEVE"
                Case 10 : Num2Text = "DIEZ"
                Case 11 : Num2Text = "ONCE"
                Case 12 : Num2Text = "DOCE"
                Case 13 : Num2Text = "TRECE"
                Case 14 : Num2Text = "CATORCE"
                Case 15 : Num2Text = "QUINCE"
                Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
                Case 20 : Num2Text = "VEINTE"
                Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
                Case 30 : Num2Text = "TREINTA"
                Case 40 : Num2Text = "CUARENTA"
                Case 50 : Num2Text = "CINCUENTA"
                Case 60 : Num2Text = "SESENTA"
                Case 70 : Num2Text = "SETENTA"
                Case 80 : Num2Text = "OCHENTA"
                Case 90 : Num2Text = "NOVENTA"
                Case Is < 100 : Num2Text = Num2Text(Math.Truncate(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
                Case 100 : Num2Text = "CIEN"
                Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
                Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Math.Truncate(value \ 100)) & "CIENTOS"
                Case 500 : Num2Text = "QUINIENTOS"
                Case 700 : Num2Text = "SETECIENTOS"
                Case 900 : Num2Text = "NOVECIENTOS"
                Case Is < 1000 : Num2Text = Num2Text(Math.Truncate(value \ 100) * 100) & " " & Num2Text(value Mod 100)
                Case 1000 : Num2Text = "MIL"
                Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
                Case Is < 1000000 : Num2Text = Num2Text(Math.Truncate(value \ 1000)) & " MIL"
                    If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
                Case 1000000 : Num2Text = "UN MILLON DE"
                Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
                Case Is < 1000000000000.0# : Num2Text = Num2Text(Math.Truncate(value / 1000000)) & " MILLONES"
                    If (value - Math.Truncate(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Math.Truncate(value / 1000000) * 1000000) Else Num2Text &= " DE"
                Case 1000000000000.0# : Num2Text = "UN BILLON DE"
                Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Math.Truncate(value / 1000000000000.0#) * 1000000000000.0#)
                Case Else : Num2Text = Num2Text(Math.Truncate(value / 1000000000000.0#)) & " BILLONES"
                    If (value - Math.Truncate(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Math.Truncate(value / 1000000000000.0#) * 1000000000000.0#) Else Num2Text &= " DE"
            End Select
        End Function

        ''' <summary>
        ''' Obtiene el valor formateado con el código ISO de la moneda
        ''' </summary>
        Public Shared Function GetMoneyWithISO4217(value As Decimal, currencyAbbreviation As String) As String
            Return String.Format("{0} {1:N2}", currencyAbbreviation, value)
        End Function

        ''' <summary>
        ''' Lista el nombre de decimales según la moneda
        ''' </summary>
        Public Shared Function ListDecimalCurrency(currencyAbbreviation As String) As String
            Select Case currencyAbbreviation?.ToUpper()
                Case "COP"
                    Return "CENTAVOS"
                Case "USD"
                    Return "CENTS"
                Case "EUR"
                    Return "CÉNTIMOS"
                Case Else
                    Return "CENTAVOS"
            End Select
        End Function

    End Class

    ''' <summary>
    ''' Módulo de extensiones para manejo de formatos numéricos
    ''' </summary>
    Public Module NumberFormatExtensions

        ''' <summary>
        ''' Obtiene el formato numérico basado en la abreviación de moneda
        ''' </summary>
        <System.Runtime.CompilerServices.Extension>
        Public Function GetNumberFormat(currencyAbbreviation As String) As NumberFormatInfo
            Dim nfi As New NumberFormatInfo()
            Select Case currencyAbbreviation?.ToUpper()
                Case "COP"
                    nfi.CurrencySymbol = "$"
                    nfi.CurrencyDecimalDigits = 2
                    nfi.CurrencyDecimalSeparator = ","
                    nfi.CurrencyGroupSeparator = "."
                Case "USD"
                    nfi.CurrencySymbol = "$"
                    nfi.CurrencyDecimalDigits = 2
                    nfi.CurrencyDecimalSeparator = "."
                    nfi.CurrencyGroupSeparator = ","
                Case "EUR"
                    nfi.CurrencySymbol = "€"
                    nfi.CurrencyDecimalDigits = 2
                    nfi.CurrencyDecimalSeparator = ","
                    nfi.CurrencyGroupSeparator = "."
                Case Else
                    nfi = CultureInfo.CurrentCulture.NumberFormat
            End Select
            Return nfi
        End Function

    End Module

End Namespace
