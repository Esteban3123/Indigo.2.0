Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer el tipo de perfil
''' </summary>
<DataContract()>
Public Enum eRangeType
    <EnumMember>
    IntermediateAge = 0

    <EnumMember>
    MinimumAge = 1

    <EnumMember>
    MaximumAge = 2
End Enum
