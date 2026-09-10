'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IExpenseConceptRepository
    Inherits IRepository(Of ExpenseConcepts)

    ''' <summary>
    ''' Obtiene un concepto de egreso
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetExpenseConcept(ByVal code As String) As ExpenseConcepts

    ''' <summary>
    ''' Obtiene un concepto de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetExpenseConceptById(ByVal Id As Integer, Optional tracking As Boolean = True) As ExpenseConcepts

    ''' <summary>
    ''' Obtiene los conceptos de egreso que estan relacionados a un concepto de flujo de caja
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetExpenseConceptByFlowConcept(id As Integer) As List(Of ExpenseConcepts)

End Interface
