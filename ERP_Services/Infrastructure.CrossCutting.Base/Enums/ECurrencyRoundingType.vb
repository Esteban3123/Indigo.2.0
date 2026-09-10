Imports System.ComponentModel
Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer los tipos de redondeo de la moneda
''' </summary>
<DataContract()>
Public Enum ECurrencyRoundingType

    ''' <summary>
    ''' Dos decimales
    ''' </summary>
    <EnumMember>
    TwoDecimals = 1
    ''' <summary>
    ''' Un decimal
    ''' </summary>
    <EnumMember>
    OneDecimal = 2
    ''' <summary>
    ''' Ninguno
    ''' </summary>
    <EnumMember>
    None = 3
    ''' <summary>
    ''' Decenas
    ''' </summary>
    <EnumMember>
    Tens = 4
    ''' <summary>
    ''' Centena
    ''' </summary>
    <EnumMember>
    Hundreds = 5
    ''' <summary>
    ''' Milesima
    ''' </summary>
    <EnumMember>
    Thousands = 6
End Enum
