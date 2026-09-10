'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 19-08-2015
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
Public Interface IBudgetServiceAnnualizedCashFlowModification
    ''' <summary>
    ''' obtiene una modificacion del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAnnualizedCashFlowModificationByCode(code As String, type As Integer, audit As AuditMessage) As Domain.Entities.AnnualizedCashFlowModification
    ''' <summary>
    ''' obtiene una modificacion del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAnnualizedCashFlowModificationById(id As Integer) As AnnualizedCashFlowModification

    ''' <summary>
    ''' metodo para guardar una modificacion del pac
    ''' </summary>
    ''' <param name="AnnualizedCashFlowModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAnnualizedCashFlowModification(AnnualizedCashFlowModification As Domain.Entities.AnnualizedCashFlowModification, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AnnualizedCashFlowModification)
End Interface
