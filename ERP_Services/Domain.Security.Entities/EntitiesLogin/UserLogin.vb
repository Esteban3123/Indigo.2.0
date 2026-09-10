Imports System.Runtime.Serialization

''' <summary>
''' Clase Utilizada en el Login para la autenticacion del usuario - PermissionAdminCompany
''' </summary>
''' <remarks></remarks>
<DataContract(IsReference:=True)>
Public Class UserLogin

    <DataMember()> _
    Property Id As Integer
    <DataMember()> _
    Property GroupCode As String
    <DataMember()> _
    Property UserCode As String
    <DataMember()> _
    Property Password As String
    <DataMember()>
    Property UserType As String
    <DataMember()>
    Property ProfileType As String
    <DataMember()> _
    Property Position As String
    <DataMember()> _
    Property CodeInterface As String
    <DataMember()> _
    Property IdPerson As Integer
    <DataMember()> _
    Property ViewForm As Boolean
    <DataMember()> _
    Property DateExpiryAccount As Nullable(Of Date)
    <DataMember()> _
    Property IsLockedOut As Boolean
    <DataMember()> _
    Property FailedPasswordCount As Integer
    <DataMember()> _
    Property State As Boolean
    <DataMember()> _
    Property RollCode As String
    <DataMember()> _
    Property RollName As String
    <DataMember()> _
    Property Fullname As String
    <DataMember()> _
    Property Email As String
    <DataMember()> _
    Property CompanyCode As String
    <DataMember()> _
    Property CompanyPermission As Boolean
    <DataMember()>
    Property UserExists As Boolean
    <Obsolete("Esta propiedad esta obsoleta, use ListProductCatalog y IsAllowPermissionForm")>
    <DataMember()>
    Property ListPermission As List(Of String)
    <DataMember()>
    Property ListProductCatalog As List(Of ProductCatalog)
    <DataMember()> _
    Property ListCompanyPermissionCode As List(Of CompanyPermission)
    <DataMember()>
    Property TenantId As Short
    <DataMember>
    Property AccessToken As String
End Class
