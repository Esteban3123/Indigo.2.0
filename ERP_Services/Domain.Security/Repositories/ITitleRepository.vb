'***********************************************************************
' Assembly         : Domain.Security
' Author           : Jhon Tovar
' Created          : 05-04-2022
'
' Last Modified By :
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Security.Entities

Public Interface ITitleRepository

    ''' <summary>
    ''' Guardar Titulo
    ''' </summary>
    ''' <param name="title"></param>
    ''' <returns></returns>
    Function SaveTitle(ByVal title As Title) As Boolean

    ''' <summary>
    ''' Actualizar Titulo
    ''' </summary>
    ''' <param name="title"></param>
    ''' <returns></returns>
    Function UpdateTitle(ByVal title As Title) As Boolean

    ''' <summary>
    ''' Consulta un Titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <returns></returns>
    Function GetTitle(ByVal IdTitle As Integer) As Title

    ''' <summary>
    ''' Elimina el Titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <returns></returns>
    Function DeleteTitle(ByVal IdTitle As Integer) As Boolean

    ''' <summary>
    '''  Cambia estado del Titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Function ChangeStateTitle(ByVal IdTitle As Integer, ByVal state As Byte) As Boolean

End Interface
