'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractHealthAdministrator

    ''' <summary>
    ''' Guarda o Actualiza la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveHealthAdministrator(HealthAdministrator As Domain.Entities.HealthAdministrator, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.HealthAdministrator)

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteHealthAdministrator(HealthAdministrator As Domain.Entities.HealthAdministrator, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetHealthAdministrator(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.HealthAdministrator)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetHealthAdministratorById(ByVal id As Integer, audit As AuditMessage) As ActionResult(Of HealthAdministrator)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateHealthAdministrator(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.HealthAdministrator)

End Interface
