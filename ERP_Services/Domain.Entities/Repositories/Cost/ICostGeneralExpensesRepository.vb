'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICostGeneralExpensesRepository
    Inherits IRepository(Of CostGeneralExpense)

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetGeneralExpense(ByVal code As String) As CostGeneralExpense

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetGeneralExpenseById(id As Integer) As CostGeneralExpense

    ''' <summary>
    ''' Lista los gastos generales por id de cuenta contable
    ''' </summary>
    Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As CostGeneralExpense

    ''' <summary>
    ''' Lista los elementos del costo con estado en true
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCostGeneralExpenseByStatus(ByVal status As Boolean) As List(Of CostGeneralExpense)

    ''' <summary>
    ''' Valida el CopyPaste de los detalles a la base del elemento del costo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ImportDetailsToCostDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, xmlListDistributionBaseDetail As String, xmlData As String) As List(Of SP_ImportDetailsToCostDistributionBase_Result)

End Interface
