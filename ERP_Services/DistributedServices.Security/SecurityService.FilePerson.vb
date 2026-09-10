'***********************************************************************
' Assembly         : DistributedService.Security
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
Imports Domain.Security
Imports Infrastructure.CrossCutting.IOC
Imports Application.Security
Imports Infrastructure.CrossCutting.Base
#End Region

Partial Public Class SecurityService

    ''' <summary>
    ''' Funcion  Para cargar Archivo Personales y validar 
    ''' </summary>
    ''' <param name="CodePerson">codigo de la persona</param>
    ''' <returns></returns>
    Public Function GetFilePerson(CodePerson As String) As FilePerson Implements ISecurityService.GetFilePerson
        Using obj As IFilePersonAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IFilePersonAdminService)()
            Return obj.GetFilePerson(CodePerson)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    Public Function GetFilePersonByUserCode(userCode As String) As FilePerson Implements ISecurityService.GetFilePersonByUserCode
        Using obj As IFilePersonAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IFilePersonAdminService)()
            Return obj.GetFilePersonByUserCode(userCode)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userId">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    Public Function GetFilePersonByUserId(userId As Integer) As FilePerson Implements ISecurityService.GetFilePersonByUserId
        Using obj As IFilePersonAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IFilePersonAdminService)()
            Return obj.GetFilePersonByUserId(userId)
        End Using
    End Function

End Class
