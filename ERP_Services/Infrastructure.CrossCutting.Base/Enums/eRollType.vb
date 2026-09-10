Imports System.ComponentModel
Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer los tipos de rol
''' </summary>
<DataContract()>
Public Enum eRollType
    ''' <summary>
    ''' Indica que el tipo de rol es global
    ''' </summary>
    <EnumMember>
    <Description("Global")>
    GlobalType = 1
    ''' <summary>
    ''' Indica que el tipo de rol es por tenant
    ''' </summary>
    <EnumMember>
    <Description("Por tenant")>
    ByTenant = 2
End Enum
