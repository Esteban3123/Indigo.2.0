'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/06/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IAuthorizationServiceTraceabilityPaperwork

    ''' <summary>
    ''' Obtiene una entidad por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetTraceabilityPaperworkById(ByVal id As Integer) As ActionResult(Of TraceabilityPaperwork)

    ''' <summary>
    ''' Asignación automática
    ''' </summary>
    ''' <param name="ListTraceabilityPaperwork">List entities.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function AssignTraceabilityPaperwork(ListTraceabilityPaperwork As List(Of TraceabilityPaperwork), audit As AuditMessage) As ActionResult(Of List(Of TraceabilityPaperwork))

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveTraceabilityPaperwork(ListTraceabilityPaperwork As List(Of TraceabilityPaperwork), audit As AuditMessage) As ActionResult(Of List(Of TraceabilityPaperwork))

    ''' <summary>
    ''' Actualiza el estado de aceptación
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SP_SaveAcceptanceAuthorization(listTuple As List(Of Tuple(Of Integer, Integer, String, Integer)), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Elimina 
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteTraceabilityPaperwork(ByVal TraceabilityPaperwork As TraceabilityPaperwork, TransactionalContainer As String, ByVal audit As AuditMessage) As ActionResult

End Interface
