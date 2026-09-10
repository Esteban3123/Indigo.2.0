Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion que determina el tipo de interfaz a realizar
''' </summary>
''' <remarks></remarks>
<DataContract()>
Public Enum eTypeInterface As Integer
    ''' <summary>
    ''' representa metodo de interfaz para fox metodo privado
    ''' </summary>
    <EnumMember()>
    FoxPrivate = 1
    ''' <summary>
    ''' representa metodo de interfaz para fox metodo publico
    ''' </summary>
    <EnumMember()>
    FoxPublic = 2
    ''' <summary>
    ''' representa metodo de interfaz para NET metodo publico
    ''' </summary>
    <EnumMember()>
    NETPrivate = 3
    ''' <summary>
    ''' representa metodo de interfaz para fox metodo privado
    ''' </summary>
    <EnumMember()>
    NETPublic = 4

End Enum