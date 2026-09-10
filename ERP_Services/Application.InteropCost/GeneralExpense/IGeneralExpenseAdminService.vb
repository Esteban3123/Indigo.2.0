'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IGeneralExpenseAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Function SaveGeneralExpense(ByVal generalExpense As GeneralExpense, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of GeneralExpense)

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    Function DeleteGeneralExpense(ByVal generalExpense As GeneralExpense, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista los gastos generales por id de cuenta contable
    ''' </summary>
    Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As GeneralExpense

    Function ListGeneralExpenseByStatus(status As Boolean) As List(Of GeneralExpense)

    ' ''' <summary>
    ' ''' Updates the state.
    ' ''' </summary>
    'Function UpdateStateGeneralExpense(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of GeneralExpense)

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetGeneralExpense(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of GeneralExpense)

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetGeneralExpenseById(id As Integer) As GeneralExpense

    Function UpdateStateGeneralExpense(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of GeneralExpense)
End Interface