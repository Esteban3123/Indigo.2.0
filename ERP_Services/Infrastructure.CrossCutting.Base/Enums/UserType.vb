Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer el tipo de usuario define se se realiza consulta a tabla de 
''' Profesionales en el Login
''' </summary>
<DataContract()>
Public Enum UserType
    ''' <summary>
    ''' No es Administrador
    ''' </summary>
    <EnumMember>
    StandardUser = 0
    ''' <summary>
    ''' Administrador de la empresa
    ''' </summary>
    <EnumMember>
    CompanyAdmin = 1
    ''' <summary>
    ''' Administrador del tenant
    ''' </summary>
    <EnumMember>
    TenantAdmin = 2
    ''' <summary>
    ''' Administrador de Indigo
    ''' </summary>
    <EnumMember>
    GlobalAdmin = 3
    ''' <summary>
    ''' Global Interno Qa
    ''' </summary>
    <EnumMember>
    GlobalQa = 4
End Enum