'***********************************************************************
' Assembly         : DistributedServices.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IMedicalFeesMedicalFeesContract

    ''' <summary>
    ''' Guarda o Actualiza la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMedicalFeesContract(MedicalFeesContract As Domain.Entities.MedicalFeesContract, idSequense As Int64, audit As AuditMessage) As Task(Of ActionResult(Of MedicalFeesContract))

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMedicalFeesContract(MedicalFeesContract As Domain.Entities.MedicalFeesContract, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMedicalFeesContract(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesContract)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMedicalFeesContractById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesContract)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateMedicalFeesContract(code As String, state As Integer, audit As AuditMessage) As Task(Of ActionResult(Of MedicalFeesContract))

End Interface
