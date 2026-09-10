'***********************************************************************
' Assembly         : Infraestructure.CrossCutting.Base
' Author           : Juan F. Tamayo
' Created          : 2013-12-27
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-12-27
' Description      : Administra los recursos de idioma para cada uno
'                    de los formularios en de la interface windows
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Resources

#End Region

''' <summary>
''' Administra los recursos de idioma para cada uno
''' de los formularios en de la interface windows
''' </summary>
Public NotInheritable Class ResourceManager

#Region "Properties"

    ''' <summary>
    ''' Obtiene un recurso de tipo cadena de texto
    ''' </summary>
    ''' <param name="name">Nombre del recurso</param>
    ''' <returns>Cadena almacenada en el recurso</returns>
    Public Shared ReadOnly Property GetString(ByVal name As String) As String
        Get
            Return ResourceManager.GetString(name, "Commons")
        End Get
    End Property

    ''' <summary>
    ''' Obtiene un recurso de tipo cadena de texto
    ''' </summary>
    ''' <param name="name">Nombre del recurso</param>
    ''' <param name="myType">Tipo del objeto con quien se relaciona el recurso</param>
    ''' <returns>Cadena almacenada en el recurso</returns>
    Public Shared ReadOnly Property GetString(ByVal name As String, ByVal myType As Type) As String
        Get
            Return ResourceManager.GetString(name, myType.Name)
        End Get
    End Property

    ''' <summary>
    ''' Obtiene un recurso de tipo cadena de texto
    ''' </summary>
    ''' <param name="name">Nombre del recurso</param>
    ''' <param name="nameResFile">Nombre del archivo de recursos</param>
    ''' <returns>Cadena almacenada en el recurso</returns>
    Public Shared ReadOnly Property GetString(ByVal name As String, ByVal nameResFile As String) As String
        Get
            Dim resMan As New System.Resources.ResourceManager(GetType(Infrastructure.CrossCutting.Resources.ResourceManager).Namespace & "." & nameResFile.Trim(), GetType(Infrastructure.CrossCutting.Resources.ResourceManager).Assembly)
            Return resMan.GetString(name.Trim())
        End Get
    End Property

#End Region

End Class