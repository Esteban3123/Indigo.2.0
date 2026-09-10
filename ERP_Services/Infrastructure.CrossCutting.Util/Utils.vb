'***********************************************************************
' Assembly         : Infraestructure.CrossCutting.Base
' Author           : Juan F. Tamayo
' Created          : 2013-08-06
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-08-06
' Description      : Conjunto de funciones utilitarias
'
' Last Modified By : Cristhian Salazar
' Last Modified On : 2014-01-15
' Description      : Creo la funcion de String Pad y RoundValueNearestThousand
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Management
Imports System.Security.Cryptography
Imports System.IO
Imports System.Globalization
Imports System.Configuration
Imports System.ComponentModel
Imports System.Dynamic
Imports System.Reflection
Imports System.Text

#End Region

''' <summary>
''' Provee funciones utilitarias para distintas necesidades
''' </summary>
Public Class Utils

    ''' <summary>
    ''' Tipo de redondeo
    ''' </summary>
    Public Enum RoundLevel As Integer
        ''' <summary>
        ''' Redondea al peso o unidad
        ''' </summary>
        Unit = 0
        ''' <summary>
        ''' Redondea a la decena
        ''' </summary>
        Ten = -1
        ''' <summary>
        ''' Redondea a la centena
        ''' </summary>
        Hundred = -2
        ''' <summary>
        ''' Redondea a los miles
        ''' </summary>
        Thousands = -3
    End Enum

    ''' <summary>
    ''' Función para Redondear Valores de acuerdo la Tasa de Aproximación
    ''' </summary>
    ''' <param name="ValueIn">Valor a Redondear</param>
    ''' <param name="Rate">Factor a Redondear (0, 10, 100, 1000)</param>
    ''' <returns>Valor Redondeado</returns>
    ''' <remarks></remarks>
    Public Shared Function RoundedValuesByRate(ByVal ValueIn As Double, Rate As Integer) As Double

        Dim ValueReturn As Double

        If ValueIn > 0 Then
            Select Case Rate
                Case 0
                    ValueReturn = ValueIn
                Case 1
                    ValueReturn = Math.Round(ValueIn)
                Case 10
                    If Right(ValueIn, 1) = "0" Then
                        ValueReturn = ValueIn
                    Else
                        'ValueReturn = Math.Round(ValueIn, 1)
                        ValueReturn = Math.Round((ValueIn / 10) * 10, 0)
                    End If
                Case 100
                    If Right(ValueIn, 2) = "00" Then
                        ValueReturn = ValueIn
                    Else
                        'ValueReturn = Math.Round(ValueIn, 2)
                        ValueReturn = Math.Round((ValueIn / 100) * 100, 0)
                    End If
                Case 1000
                    If Right(ValueIn, 3) = "000" Then
                        ValueReturn = ValueIn
                    Else
                        'ValueReturn = Math.Round(ValueIn, 3)
                        ValueReturn = Math.Round((ValueIn / 1000) * 1000, 0)
                    End If

            End Select
        Else
            ValueReturn = 0
        End If

        Return ValueReturn

    End Function

    ''' <summary>
    ''' Redondea valores a 1,10,100,1000
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="roundLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RoundValue(ByVal value As Double, ByVal roundLevel As Int32) As Double
        Select Case roundLevel
            Case 1
                Return RoundValue(value, Utils.RoundLevel.Unit)
            Case 10
                Return RoundValue(value, Utils.RoundLevel.Ten)
            Case 100
                Return RoundValue(value, Utils.RoundLevel.Hundred)
            Case 1000
                Return RoundValue(value, Utils.RoundLevel.Thousands)
            Case Else
                Return 0
        End Select
    End Function

End Class
