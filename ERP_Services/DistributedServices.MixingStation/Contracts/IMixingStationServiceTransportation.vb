'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceTransportation

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveTransportation(ByVal Transportation As Transportation, ByVal audit As AuditMessage, operatingUnitId As Integer, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Transportation)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteTransportation(ByVal Transportation As Transportation, ByVal audit As AuditMessage, TransactionalContainer As String) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetTransportation(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of Transportation)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetTransportationById(ByVal id As Integer) As ActionResult(Of Transportation)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateTransportation(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of Transportation)

End Interface
