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

Public Interface IBudgetTransferAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    '''<param name="Code">Código del traslado</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <param name="yearValidity">Vigencia</param>
    ''' <returns></returns>
    Function GetBudgetTransfer(Code As String, ItemType As Byte, yearValidity As Integer, audit As AuditMessage) As ActionResult(Of BudgetTransfer)

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    '''<param name="Id">Id del traslado</param>
    ''' <returns></returns>
    Function GetBudgetTransferById(Id As Integer, audit As AuditMessage) As ActionResult(Of BudgetTransfer)

    ''' <summary>
    ''' Guarda o Actualiza un traslado
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveBudgetTransfer(ByVal budgetTransfer As BudgetTransfer, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of BudgetTransfer)

    ''' <summary>
    ''' Elimina un traslado
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteBudgetTransfer(ByVal budgetTransfer As BudgetTransfer, ByVal audit As AuditMessage) As ActionResult

End Interface
