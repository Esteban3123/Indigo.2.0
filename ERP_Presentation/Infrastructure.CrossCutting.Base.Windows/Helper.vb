'***********************************************************************
' Assembly         : Infraestructure.CrossCutting.Base
' Author           : Juan F. Tamayo
' Created          : 2013-12-26
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-12-26
' Description      : Provee servicios de ayuda
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
#End Region

Imports System.Configuration
Imports DevExpress.UnitConversion

''' <summary>
''' Provee servicios de ayuda en la capa trasversal
''' </summary>
Public NotInheritable Class Helper

    ''' <summary>
    ''' Nombre de la funte
    ''' </summary>
    Private Const SOURCE As String = "Indigo Vie - XPO"

#Region "Methods"

    ''' <summary>
    ''' Obtiene el nombre de la aplicacion
    ''' </summary>
    ''' <returns>Nombre de la aplicacion</returns>
    Public Shared Function GetAppName() As String
        Return My.Application.Info.ProductName
    End Function

    ''' <summary>
    ''' Obtiene el nombre de la compañia quien desarrollo la aplicación
    ''' </summary>
    ''' <returns>Nombre de la compañia</returns>
    Public Shared Function GetCompanyName() As String
        Return My.Application.Info.CompanyName
    End Function

    ''' <summary>
    ''' Obtiene la ruta de archivos comunes
    ''' </summary>
    ''' <returns>Ruta de archivos comunes</returns>
    Public Shared Function GetPathApplicationFiles() As String
        'Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
        Return String.Concat(Window.Utils.LocalFolder(), "\" & GetCompanyName() & "\" & GetAppName())
    End Function

    ''' <summary>
    ''' Obtiene la ruta de archivos del usuario
    ''' </summary>
    ''' <returns>Ruta de archivos del usuario</returns>
    Public Shared Function GetPathUserFiles() As String
        'Return String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\" & GetCompanyName() & "\" & GetAppName())
        Return String.Concat(Utils.UserFolder(), "\" & GetCompanyName() & "\" & GetAppName())
    End Function

#End Region

End Class
