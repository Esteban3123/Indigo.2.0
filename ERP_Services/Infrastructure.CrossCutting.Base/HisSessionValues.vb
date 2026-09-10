'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Base
' Author           : Walter Sierra
' Created          : 03-08-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-22
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Runtime.Serialization

''' <summary>
''' Encapsula los valores de sesión para el usuario en el sistema asistencial
''' </summary>
Public Class HisSessionValues

    ''' <summary>
    ''' Obtiene o asigna el nombre del usuario
    ''' </summary>
    ''' <value>Nombre del usuario</value>
    ''' <returns>El nombre del usuario</returns>
    Public Property UserName As String
    ''' <summary>
    ''' Obtiene o asigna el cargo del usuario
    ''' </summary>
    ''' <value>Cargo del usuario</value>
    ''' <returns>El cargo del usuario</returns>
    Public Property UserPosition As String
    ''' <summary>
    ''' Obtiene o asigna el perfil del usuario
    ''' </summary>
    ''' <value>Perfil del usuario</value>
    ''' <returns>El perfil del usuario</returns>
    Public Property UserRol As String
    ''' <summary>
    ''' Obtiene o asigna el grupo del usuario
    ''' </summary>
    ''' <value>Grupo del usuario</value>
    ''' <returns>El grupo del usuario</returns>
    Public Property UserGroup As String
    ''' <summary>
    ''' Obtiene o asigna el estado del usuario
    ''' </summary>
    ''' <value>Estado del usuario</value>
    ''' <returns>El estado del usuario</returns>
    Public Property UserStatus As Boolean
    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el login fue
    ''' satisfactorio
    ''' </summary>
    ''' <value>Valor que indica si el usuario esta logueado</value>
    ''' <returns>El valor que indica si el usuario esta logueado</returns>
    Public Property IsLogin As Boolean
    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se produjo algún erro en el proceso
    ''' </summary>
    ''' <value>Valor que indica si se producjo algun error</value>
    ''' <returns>Un valor que indica si ocurrio algun error</returns>
    Public Property IsError As Boolean
    ''' <summary>
    ''' Obtiene o asigna el mensaje de respuesta al proceso de inicio de sesión
    ''' </summary>
    ''' <value>Mensaje de respuesta</value>
    ''' <returns>El mensaje de respuesta</returns>
    Public Property ResponseMessage As String
    ''' <summary>
    ''' Obtiene o asigna la versión del ensamblado del sistema asistencial
    ''' </summary>
    ''' <value>Versión del ensamblado</value>
    ''' <returns>La versión del ensamblado</returns>
    Public Property Version As String

End Class