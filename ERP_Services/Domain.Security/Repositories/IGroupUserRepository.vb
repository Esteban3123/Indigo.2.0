'***********************************************************************
' Assembly         : Domain.Security
' Author           : Juan F. Tamayo
' Created          : 2013-10-30
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-10-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Base
#End Region

''' <summary>
''' Repositorio de los grupos en el chat del usuario
''' </summary>
Public Interface IGroupUserRepository
    Inherits IRepository(Of GroupUser)

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Lista de grupos</returns>
    Function ListByUsercode(ByVal userCode As String) As List(Of GroupUser)

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su id
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Lista de grupos</returns>
    Function ListByUserId(ByVal userId As Integer) As List(Of GroupUser)

    ''' <summary>
    ''' Obtiene un grupo por su id
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>El grupo buscado</returns>
    Function GetGroupById(ByVal id As Integer) As GroupUser

End Interface
