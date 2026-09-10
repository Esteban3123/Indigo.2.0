Imports Presentation.CloudAgent
Imports Infrastructure.CrossCutting.Base

Public Class PermisosFormularios

    ''' <summary>
    ''' Gets the permission user.
    ''' </summary>
    ''' <param name="CodeUser">The code user.</param>
    ''' <param name="CodeMenu">The code menu.</param>
    ''' <returns></returns>
    Public Shared Function GetPermissionUser(CodeUser As String, CodeMenu As String) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserForm(CodeUser, CodeMenu, "40", SessionValues.Instance)
    End Function


End Class
