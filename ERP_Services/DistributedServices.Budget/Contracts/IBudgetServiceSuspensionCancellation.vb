'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 19-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServiceSuspensionCancellation

    ''' <summary>
    ''' obtiene un levantamiento de suspencion presupuestal por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetSuspensionCancellationByCode(code As String, audit As AuditMessage) As Domain.Entities.SuspensionCancellation
    ''' <summary>
    ''' obtiene un levantamiento de suspencion presupuestal por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSuspensionCancellationById(id As Integer) As SuspensionCancellation

    ''' <summary>
    ''' Guarda un levantamiento de suspencion presupuestal
    ''' </summary>
    ''' <param name="suspensionCancellation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveSuspensionCancellation(suspensionCancellation As SuspensionCancellation, idSequense As Int64, audit As AuditMessage) As ActionResult(Of SuspensionCancellation)

End Interface
