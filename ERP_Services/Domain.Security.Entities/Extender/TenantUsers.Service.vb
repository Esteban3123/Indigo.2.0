'**********************************************************************
' Assembly         : Domain.Security.Entities
' Author           : Hector Rodriguez Rubiano
' Created          : 15-04-2021
'
' Description      : Extiende las propiedades de TenantUsers
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Runtime.Serialization

Partial Public Class TenantUsers
    <DataMember()>
    Property TenantName As String
    <DataMember()>
    Property RoleName As String
    <DataMember()>
    Property GroupName As String
End Class
