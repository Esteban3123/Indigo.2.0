Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer el tipo de Ejecucion de la Sesion define si se realiza Actualizacion
''' de Version de Indigo
''' </summary>
<DataContract()>
Public Enum ExcecutionSessionType
    ''' <summary>
    ''' Indica una ejecucion local
    ''' </summary>
    <EnumMember>
    Local = 1
    ''' <summary>
    ''' Indica una ejecucion remota
    ''' </summary>
    <EnumMember>
    Remote = 2
End Enum