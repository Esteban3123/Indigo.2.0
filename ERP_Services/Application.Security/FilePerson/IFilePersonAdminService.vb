'***********************************************************************
' Assembly         : Application.Security
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
#End Region

''' <summary>
''' Interface de archivo personal
''' </summary>
Public Interface IFilePersonAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Funcion  Para cargar Archivo Personales y validar 
    ''' </summary>
    ''' <param name="CodePerson">codigo de la persona</param>
    ''' <returns></returns>
    Function GetFilePerson(CodePerson As String) As FilePerson

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    Function GetFilePersonByUserCode(userCode As String) As FilePerson

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userId">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    Function GetFilePersonByUserId(userId As Integer) As FilePerson

End Interface
