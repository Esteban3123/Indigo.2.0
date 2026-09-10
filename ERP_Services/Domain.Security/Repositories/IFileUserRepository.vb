'***********************************************************************
' Assembly         : Domain.Security
' Author           : RafaelPatiño
' Created          : 17-06-2013
'
' Last Modified By : 
' Last Modified On : 
' Copyright        : (c) . All rights reserved.
'***********************************************************************


#Region "Imports"
Imports Domain.Base
Imports Domain.Security.Entities
#End Region

Public Interface IFileUserRepository
    Inherits IRepository(Of FilePerson)

    ''' <summary>
    ''' Funcion  Para cargar Archivo Personales y validar 
    ''' </summary>
    ''' <param name="CodePerson">codigo de la persona</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getFileUser(ByVal CodePerson As String) As FilePerson

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    Function GetFileUserByUserCode(ByVal userCode As String) As FilePerson

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Archivo personal</returns>
    Function GetFileUserByUserId(ByVal userId As Integer) As FilePerson

End Interface
