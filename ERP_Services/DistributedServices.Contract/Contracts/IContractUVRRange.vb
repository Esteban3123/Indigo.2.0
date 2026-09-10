'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractUVRRange

    ''' <summary>
    ''' Guarda o Actualiza un rango uvr
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveUVRRange(UVRRange As Domain.Entities.UVRRange, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.UVRRange)

    ''' <summary>
    ''' Elimina un rango uvr
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteUVRRange(UVRRange As Domain.Entities.UVRRange, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un rango uvr por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUVRRange(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.UVRRange)

    ''' <summary>
    ''' Obtiene un rango uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUVRRangeById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.UVRRange)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateUVRRange(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.UVRRange)

End Interface
