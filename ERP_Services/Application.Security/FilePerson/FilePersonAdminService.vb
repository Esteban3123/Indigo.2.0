'***********************************************************************
' Assembly         : Application.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' Servicio de archivo personal
''' </summary>
Public Class FilePersonAdminService
    Implements IFilePersonAdminService

    Private _filePersonRepository As IFileUserRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="repo">Repositorio</param>
    Public Sub New(ByVal repo As IFileUserRepository)
        If repo Is Nothing Then
            Throw New ArgumentNullException("repo")
        End If
        Me._filePersonRepository = repo
    End Sub

    ''' <summary>
    ''' Funcion  Para cargar Archivo Personales y validar 
    ''' </summary>
    ''' <param name="CodePerson">codigo de la persona</param>
    ''' <returns></returns>
    Public Function GetFilePerson(CodePerson As String) As FilePerson Implements IFilePersonAdminService.GetFilePerson
        If String.IsNullOrEmpty(CodePerson.Trim()) Then
            Throw New ArgumentNullException("CodePerson")
        End If
        Return Me._filePersonRepository.getFileUser(CodePerson.Trim())
    End Function

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    Public Function GetFilePersonByUserCode(userCode As String) As FilePerson Implements IFilePersonAdminService.GetFilePersonByUserCode
        If String.IsNullOrEmpty(userCode.Trim()) Then
            Throw New ArgumentNullException("userCode")
        End If
        Return Me._filePersonRepository.GetFileUserByUserCode(userCode.Trim())
    End Function

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userId">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    Public Function GetFilePersonByUserId(userId As Integer) As FilePerson Implements IFilePersonAdminService.GetFilePersonByUserId
        Return Me._filePersonRepository.GetFileUserByUserId(userId)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _filePersonRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
