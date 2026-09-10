'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

Public Interface IINUNIFUNCRepository
    Inherits IRepository(Of INUNIFUNC)

    Function GetINUNIFUNCByCode(code As String) As INUNIFUNC

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <param name="centroAtencion"></param>
    ''' <returns></returns>
    Function GetUnidadFuncionalAutorizado(usuario As String, grupo As String, centroAtencion As String) As ActionResult(Of List(Of SP_SEG_UnidadFuncional_Autorizado_Result))

End Interface