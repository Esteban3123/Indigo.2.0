'**********************************************************************
' Assembly         : Domain.Security.Entities
' Author           : Hector Rodriguez Rubiano
' Created          : 21-04-2021
'
' Description      : Extiende las propiedades de TenantUsers
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Runtime.Serialization
Partial Public Class TenantRoll
    <DataMember()>
    Property TenantName As String
End Class
