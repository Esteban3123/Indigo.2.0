Imports System.ComponentModel
Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer los tipos de grupo
''' </summary>
<DataContract()>
Public Enum eGroupType
    ''' <summary>
    ''' Indica que el tipo de grupo es global
    ''' </summary>
    <EnumMember>
    <Description("Global")>
    GlobalType = 1
    ''' <summary>
    ''' Indica que el tipo de grupo es por tenant
    ''' </summary>
    <EnumMember>
    <Description("Por tenant")>
    ByTenant = 2
End Enum
