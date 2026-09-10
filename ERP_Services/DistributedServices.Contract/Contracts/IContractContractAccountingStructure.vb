'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractContractAccountingStructure

    ''' <summary>
    ''' Obtiene un grupo de atencion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetContractAccountingStructure(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractAccountingStructure)

    ''' <summary>
    ''' Obtiene un grupo de atencion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetContractAccountingStructureById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractAccountingStructure)

    ''' <summary>
    ''' Guarda o Actualiza un grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveContractAccountingStructure(ContractAccountingStructure As Domain.Entities.ContractAccountingStructure, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractAccountingStructure)

    ''' <summary>
    ''' Elimina una entidad cups
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteContractAccountingStructure(ContractAccountingStructure As Domain.Entities.ContractAccountingStructure, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateContractAccountingStructure(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractAccountingStructure)

End Interface
