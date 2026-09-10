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
Public Interface ISEGpermirRepository
    Inherits IRepository(Of SEGpermir)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoRol"></param>
    ''' <returns></returns>
    Function GetPermisoRol(idMenu As String, codigoRol As String) As ActionResult(Of SEGpermir)

End Interface
