'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 05-09-2015
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
Public Interface IBudgetServiceCommitmentModification

    ''' <summary>
    ''' obtiene una modificacion de compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetCommitmentModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.CommitmentModification
    ''' <summary>
    ''' obtiene una modificacion de compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCommitmentModificationById(id As Integer) As CommitmentModification

    ''' <summary>
    ''' Guarda una modificacion de compromiso
    ''' </summary>
    ''' <param name="commitmentModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveCommitmentModification(CommitmentModification As CommitmentModification, listCommitmentModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of CommitmentModification)

End Interface
