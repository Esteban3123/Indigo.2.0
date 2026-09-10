Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer el tipo de perfil
''' </summary>
<DataContract()>
Public Enum eProfileType
    ''' <summary>
    ''' Indica que el usuario es de tipo administrativo
    ''' </summary>
    <EnumMember>
    Administrative = 1
    ''' <summary>
    ''' Indica que el usuario es de tipo asistencial
    ''' </summary>
    <EnumMember>
    CareCenter = 2
End Enum
