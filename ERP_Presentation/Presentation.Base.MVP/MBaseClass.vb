'***********************************************************************
' Assembly         : Presentacion.Cliente.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 06-Marzo-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

#End Region
''' <summary>
''' 	Modelo que sirve para establecer los servicios que se van a consumir en la clase base
''' </summary>
''' 
Public NotInheritable Class MBaseClass

    ''' <summary>
    ''' Funcion para consultar formularios activos 
    ''' </summary>
    ''' <param name="CodigoRol"> codigo rol.</param>
    ''' <param name="CodigoUsuario"> codigo usuario.</param>
    ''' <returns></returns>
    Public Shared Async Function GetActiveForms(ByVal CodigoRol As String, ByVal CodigoUsuario As String) As Threading.Tasks.Task(Of List(Of PermissionsFormsActive))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsFormsActiveAsync(CodigoUsuario, CodigoRol, SessionValues.Instance)
    End Function
End Class
