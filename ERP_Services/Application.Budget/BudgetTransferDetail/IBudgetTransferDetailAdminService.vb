'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBudgetTransferDetailAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un detalle de traslado por id
    ''' </summary>
    '''<param name="Id">Id del traslado</param>
    ''' <returns></returns>
    Function GetBudgetTransferDetailById(Id As Integer, audit As AuditMessage) As ActionResult(Of BudgetTransferDetail)

    ''' <summary>
    ''' Guarda o Actualiza un detalle de traslado
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveBudgetTransferDetail(ByVal BudgetTransferDetail As BudgetTransferDetail, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of BudgetTransferDetail)

    ''' <summary>
    ''' Elimina un traslado
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteBudgetTransferDetail(ByVal BudgetTransferDetail As BudgetTransferDetail, ByVal audit As AuditMessage) As ActionResult

End Interface
