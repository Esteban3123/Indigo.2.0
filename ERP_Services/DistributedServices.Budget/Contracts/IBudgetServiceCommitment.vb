'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 02-09-2015
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
Public Interface IBudgetServiceCommitment

    ''' <summary>
    ''' obtiene un compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetCommitmentByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.Commitment
    ''' <summary>
    ''' obtiene un compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCommitmentById(id As Integer) As Commitment

    ''' <summary>
    ''' Guarda un compromiso
    ''' </summary>
    ''' <param name="commitment"></param>
    ''' <param name="listCommitmentDetailDelete"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveCommitment(commitment As Commitment, listCommitmentDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of Commitment)

End Interface
