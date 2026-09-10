Imports System.ComponentModel
Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer los estados de los RIPS
''' </summary>
<DataContract()>
Public Enum EStatusERIPS
    ''' <summary>
    ''' Rips registrados o enviado
    ''' </summary>
    <EnumMember>
    Register = 1
    ''' <summary>
    ''' RIPS validado correctamente
    ''' </summary>
    <EnumMember>
    ValidateSuccess = 2
    ''' <summary>
    ''' RIPS validado con error
    ''' </summary>
    <EnumMember>
    ValidateWrong = 3
    ''' <summary>
    ''' Proceso de excepcion dentro de la generacion RIPS
    ''' </summary>
    <EnumMember>
    Exception = 99
End Enum
