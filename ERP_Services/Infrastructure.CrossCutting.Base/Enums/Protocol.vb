Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion necesaria para definir el protocolo de comunicaciones con el sitio WCF
''' </summary>
<DataContract()>
Public Enum Protocol As Integer
    ''' <summary>
    ''' representa un endpoint en WCF
    ''' </summary>
    <EnumMember()>
    basicHttp = 0
    ''' <summary>
    ''' representa un endpoint en WCF
    ''' </summary>
    <EnumMember()>
    wsHttp = 1
    ''' <summary>
    ''' representa un endpoint  en WCF
    ''' </summary>
    <EnumMember()>
    netTcp = 2
    ''' <summary>
    ''' Ninguno seleccionado
    ''' </summary>
    <EnumMember()>
    Ninguno = 3
    ' ''' <summary>
    ' ''' representa un endpoint en WCF
    ' ''' </summary>
    '<EnumMember()> _
    'wsDualHttp = 4
    ' ''' <summary>
    ' ''' representa un endpoint en WCF
    ' ''' </summary>
    '<EnumMember()> _
    'netHttp = 5
    ''' <summary>
    ''' representa un endpoint en WCF
    ''' </summary>
    <EnumMember()>
    basicHttps = 6
End Enum