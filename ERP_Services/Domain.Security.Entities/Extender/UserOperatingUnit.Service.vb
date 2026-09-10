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
Public Class UserOperatingUnit
    <DataMember()>
    Property OperatingUnitName As String
    <DataMember()>
    Property ByDefault As Boolean
End Class
