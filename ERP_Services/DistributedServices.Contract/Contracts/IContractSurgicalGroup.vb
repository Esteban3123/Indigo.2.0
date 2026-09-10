'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractSurgicalGroup

    ''' <summary>
    ''' Guarda o Actualiza un grupo quirurgico
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSurgicalGroup(SurgicalGroup As Domain.Entities.SurgicalGroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SurgicalGroup)

    ''' <summary>
    ''' Elimina un grupo quirurgico
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteSurgicalGroup(SurgicalGroup As Domain.Entities.SurgicalGroup, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un grupo quirurgico por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSurgicalGroup(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SurgicalGroup)

    ''' <summary>
    ''' Obtiene un grupo quirurgico
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSurgicalGroupById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SurgicalGroup)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateSurgicalGroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SurgicalGroup)

End Interface
