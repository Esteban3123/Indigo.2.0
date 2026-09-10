'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Carlos Cordoba
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Public Interface ISEGusuaruRepository
    Inherits IRepository(Of SEGusuaru)

    ''' <summary>
    ''' Funcion que retorna un objeto tipo usaurio de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetusuarioCrystal(ByVal code As String) As SEGusuaru

    ''' <summary>
    ''' Cambiar la contraseña
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="newPassword">la contraseña nueva.</param>
    ''' <returns></returns>
    Function ChangePassword(ByVal codeUser As String, ByVal newPassword As String) As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    Function GetPerfilUbicacion(usuario As String) As ActionResult(Of SP_SEG_AutenticarUsuario_Result)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    Function GetProfesional(usuario As String) As ActionResult(Of SP_SEG_AutenticarDatosProfesional_Result)

End Interface
