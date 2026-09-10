'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Hector Rodriguez
' Created          : 28/01/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Public Interface ISEGpermiuRepository
    Inherits IRepository(Of SEGpermiu)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoUsuario"></param>
    ''' <returns></returns>
    Function GetPermisoUsuario(idMenu As String, codigoUsuario As String) As ActionResult(Of SEGpermiu)

End Interface
