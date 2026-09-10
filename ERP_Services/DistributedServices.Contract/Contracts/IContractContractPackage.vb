'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Giovanny Plazas L
' Created          : 24/08/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IContractContractPackage
    ''' <summary>
    ''' Guarda o Actualiza un paquete
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveContractPackage(ContractPackage As Domain.Entities.ContractPackage, audit As AuditMessage, Optional idSequense As Int64 = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractPackage)

    ''' <summary>
    ''' Elimina una paquete
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteContractPackage(ContractPackage As Domain.Entities.ContractPackage, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un paquete por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractPackage(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractPackage)

    ''' <summary>
    ''' Obtiene un´paquete por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractPackageById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractPackage)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateContractPackage(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractPackage)

    ''' <summary>
    ''' Copia y pega los elementos de excel a la rejilla
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SetCopyPasteOrImportFile(dataCopyPaste As List(Of List(Of String)), Name As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractPackage)

End Interface
