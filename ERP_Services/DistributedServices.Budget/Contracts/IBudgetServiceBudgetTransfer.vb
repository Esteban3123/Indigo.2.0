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
Public Interface IBudgetServiceBudgetTransfer

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    '''<param name="Code">Código del traslado</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <param name="yearValidity">Año - Vigencia </param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBudgetTransfer(Code As String, ItemType As Byte, yearValidity As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetTransfer)

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    '''<param name="Id">Id del traslado</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBudgetTransferById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetTransfer)

    ''' <summary>
    ''' Guarda o Actualiza un traslado
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveBudgetTransfer(budgetTransfer As Domain.Entities.BudgetTransfer, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetTransfer)

    ''' <summary>
    ''' Elimina un traslado
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteBudgetTransfer(budgetTransfer As Domain.Entities.BudgetTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface

