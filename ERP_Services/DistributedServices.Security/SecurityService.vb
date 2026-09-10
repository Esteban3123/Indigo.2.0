'***********************************************************************
' Assembly         : DistributedService.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Application.Security
Imports Infrastructure.CrossCutting.IOC
Imports Application.Base
Imports DistributedServices.Security
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel.Activation
Imports System.ServiceModel
#End Region

''' <summary>
''' Clase que implementa los metodos de seguridad 
''' a ser publicados en los servicios
''' usuarios - Grupos - Roles - barra de Botones - Login
''' </summary>
<UnityMessageInspectorServiceBehavior()>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class SecurityService
    Implements ISecurityService

    ''' <summary>
    ''' Obtiene el numero total de impresiones y exportación de una entidad
    ''' </summary>
    ''' <param name="entityName">Nombre de la entidad</param>
    ''' <param name="entityKey">Id de la entidad</param>
    ''' <returns>Número total de impresiones</returns>
    Public Function GetTotalPrint(entityName As String, entityKey As Integer, session As SessionValues) As Integer Implements ISecurityService.GetTotalPrint
        Dim audit As New Application.Base.IntegratorAudit()
        Return audit.GetTotalPrint(entityName, entityKey, session.IndigoCompany)
    End Function

End Class
