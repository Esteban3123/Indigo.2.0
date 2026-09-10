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

Public Interface ICostGeneralExpenseAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Function SaveGeneralExpense(ByVal generalExpense As CostGeneralExpense, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CostGeneralExpense)

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    Function DeleteGeneralExpense(ByVal generalExpense As CostGeneralExpense, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista los gastos generales por id de cuenta contable
    ''' </summary>
    Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As CostGeneralExpense

    ' ''' <summary>
    ' ''' Updates the state.
    ' ''' </summary>
    'Function UpdateStateGeneralExpense(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CostGeneralExpense)

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetGeneralExpense(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CostGeneralExpense)

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetGeneralExpenseById(id As Integer) As CostGeneralExpense

    ''' <summary>
    ''' Actualiza el estado
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateStateCostGeneralExpense(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostGeneralExpense)

    ''' <summary>
    ''' Importa detalles al elemento de costo
    ''' </summary>
    ''' <param name="DistributionType"></param>
    ''' <param name="MeasurementUnit"></param>
    ''' <param name="ListDistributionBaseDetail"></param>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Function ImportDetailsToCostDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, ListDistributionBaseDetail As List(Of CostDistributionBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionBaseDetail))

End Interface