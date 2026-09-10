'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 27-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
#End Region

''' <summary>
''' 	Modelo que sirve para declarar los servicios que se van a consumir
''' </summary>
Public Class MDesbloquearUsuario

#Region "Fields"

    ''' <summary>
    ''' Variable de session
    ''' </summary>
    Private _indigoSession As SessionValues = SessionValues.Instance

#End Region


#Region "Funciones"

    '''' <summary>
    '''' Funcion para consultar el USUARIO
    '''' </summary>
    'Friend Async Function ConsultarUsuario(ByVal codigoUsuario As String) As Threading.Tasks.Task(Of User)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByCodeUserAsync(codigoUsuario, Me._indigoSession)
    'End Function

    'Friend Async Function ConsultarUsuarioCommand(ByVal codigoUsuario As String) As Threading.Tasks.Task(Of User)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserCommandAsync(codigoUsuario, Me._indigoSession)
    'End Function

    ''' <summary>
    ''' Funcion para Desloquear el usuario.
    ''' </summary>
    ''' <param name="codigoUsuario">el codigo.</param>
    ''' <returns></returns>
    Friend Async Function DesbloquearUsuario(ByVal codigoUsuario As String) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.UnlockUserAsync(codigoUsuario, Me._indigoSession)
    End Function

    ''' <summary>
    ''' Funcion para desbloquear registros
    ''' </summary>
    ''' <param name="codigoUsuario">cod. usaurio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Async Function DesbloquearRegistros(ByVal codigoUsuario As String) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SP_UnlockBlockRecordAsync(codigoUsuario, Me._indigoSession)
    End Function
#End Region

End Class
