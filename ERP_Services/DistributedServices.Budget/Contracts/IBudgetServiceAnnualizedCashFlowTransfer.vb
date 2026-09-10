'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 27-08-2015
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
Public Interface IBudgetServiceAnnualizedCashFlowTransfer

    ''' <summary>
    ''' obtiene un traslado del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAnnualizedCashFlowTransferByCode(code As String, type As Integer, audit As AuditMessage) As Domain.Entities.AnnualizedCashFlowTransfer
    ''' <summary>
    ''' obtiene un traslado del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAnnualizedCashFlowTransferById(id As Integer) As AnnualizedCashFlowTransfer

    ''' <summary>
    ''' Guarda un traslado del pac
    ''' </summary>
    ''' <param name="annualizedCashFlowTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAnnualizedCashFlowTransfer(annualizedCashFlowTransfer As AnnualizedCashFlowTransfer, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AnnualizedCashFlowTransfer)

End Interface
