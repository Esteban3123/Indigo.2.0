'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Jhossept K. Garay
' Created          : 19-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface ICrystalEntityRepository
    Inherits IRepository(Of INENTIDAD)
    ''' <summary>
    ''' metodo para obtener una entidad por nit
    ''' </summary>
    ''' <param name="nit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEntityByNit(nit As String) As INENTIDAD

    ''' <summary>
    ''' metodo para obtener una entidad por codigo
    ''' </summary>
    ''' <param name="nit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEntityByCode(code As String) As INENTIDAD
End Interface
