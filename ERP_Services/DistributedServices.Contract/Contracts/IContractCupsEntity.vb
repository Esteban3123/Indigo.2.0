'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractCupsEntity
    ''' <summary>
    ''' Guarda o Actualiza una entidad cups
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCupsEntity(CupsEntity As Domain.Entities.CupsEntity, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsEntity)

    ''' <summary>
    ''' Elimina una entidad cups
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCupsEntity(CupsEntity As Domain.Entities.CupsEntity, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene una entidad cups por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCupsEntity(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsEntity)

    ''' <summary>
    ''' Obtiene una entidad cups por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCupsEntityById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsEntity)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateCupsEntity(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsEntity)

    ''' <summary>
    ''' Valida la descripción antes de eliminarse
    ''' </summary>
    ''' <param name="CUPSEntityContractDescriptionId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SP_ValidateDescriptionsInCrystal(CUPSEntityContractDescriptionId As Integer) As ActionResult(Of SP_ValidateDescriptionsInCrystal_Result)

    ''' <summary>
    ''' Valida el cups cuando se agrega una descripción
    ''' </summary>
    ''' <param name="CUPSEntityCode"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SP_ValidateCUPSInCrystal(CUPSEntityCode As String) As ActionResult(Of SP_ValidateCUPSInCrystal_Result)

End Interface
