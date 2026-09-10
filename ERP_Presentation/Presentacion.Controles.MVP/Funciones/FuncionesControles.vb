'***********************************************************************
' Assembly         : Presentacion.Admisiones.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 10-10-2011
'
' Last Modified By : Jorge Leonardo Vernaza
' Last Modified On : 10-10-2011
' Description      : 
' Copyright        : (c) . All rights reserved.

'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Infrastructure.CrossCutting.Base
Imports Domain.Security.Entities
#End Region
Public Class FuncionesControles
#Region "Varibles"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

    ''' <summary>
    ''' Consulta los permisos para permitir consultar.
    ''' </summary>
    ''' <param name="CodigoUsuario">codigo usuario.</param>
    ''' <param name="CodigoRol">codigo rol.</param>
    ''' <param name="CodigoMenu">codigo menu.</param>
    ''' <returns></returns>
    Shared Function ConsultarPemisosBusqueda(ByVal CodigoUsuario As String, ByVal CodigoRol As String, ByVal CodigoMenu As String) As List(Of PermissionUserToolbar)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbar(CodigoUsuario, CodigoRol, CodigoMenu, SessionValues.Instance)
    End Function
End Class
