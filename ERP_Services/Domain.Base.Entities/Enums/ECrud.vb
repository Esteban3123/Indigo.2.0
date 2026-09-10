Imports System.Runtime.Serialization

''' <summary>
''' Enum utilizado para establecer el estado de la Clase
''' </summary>
<DataContract()>
Public Enum ECrud
    <EnumMember>
    Unchanged = 0
    <EnumMember>
    Added = 2
    <EnumMember>
    Modified = 4
    <EnumMember>
    Deleted = 8
End Enum