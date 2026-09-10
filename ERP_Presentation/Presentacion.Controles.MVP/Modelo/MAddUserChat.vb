'***********************************************************************
' Assembly         : Presentacion.Controles.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 1-11-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.CloudAgent
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data.Linq

#End Region
''' <summary>
''' Clase con el modelo a datos del formulario para agregar usuarios un grupo
''' </summary>
Public Class MAddUserChat

#Region "Variables Globales"
    ''' <summary>
    ''' Objeto con la instancia de las variables de session
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo para guardar un grupo.
    ''' </summary>
    ''' <param name="GroupUser">The group user.</param>
    Public Async Function SaveGroup(ByVal GroupUser As GroupUser) As Threading.Tasks.Task
        Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SaveGroupUserAsync(GroupUser, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Metodo para agregar un usuario a un grupo
    ''' </summary>
    ''' <param name="User">The user.</param>
    Public Async Function AddUserGroup(ByVal User As UsersGroupUser) As Threading.Tasks.Task
        Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SaveUsersGroupUserAsync(User, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion que retorna el listado de usuarios por nombre.
    ''' </summary>
    Public Function GetUserByName(ByVal Name As String) As LinqInstantFeedbackSource
        'Return XpoServiceEx.Instance(Indigo.SecurityContainer).SecurityService.ListUserByPersonName(Name, Indigo.UserIndigo)
        Return XpoServiceEx.Instance(Indigo.SecurityContainer).SecurityService.ListUserByUser(Indigo.UserIndigoId, Indigo.UserType)
    End Function

    Public Async Function ListGroupsAsync() As Threading.Tasks.Task(Of List(Of GroupUser))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListByUsercodeAsync(Indigo.UserIndigo)
    End Function
#End Region

End Class
