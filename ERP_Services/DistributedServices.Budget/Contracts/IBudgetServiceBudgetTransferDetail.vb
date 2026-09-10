'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/08/2015
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
Public Interface IBudgetServiceBudgetTransferDetail

    ''' <summary>
    ''' Obtiene un detalle de traslado por id
    ''' </summary>
    '''<param name="Id">Id del traslado</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBudgetTransferDetailById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetTransferDetail)

    ''' <summary>
    ''' Guarda o Actualiza un detalle de traslado
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveBudgetTransferDetail(BudgetTransferDetail As Domain.Entities.BudgetTransferDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetTransferDetail)

    ''' <summary>
    ''' Elimina un detalle de traslado
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteBudgetTransferDetail(BudgetTransferDetail As Domain.Entities.BudgetTransferDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface

