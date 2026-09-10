'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICashReceiptConceptRepository
    Inherits IRepository(Of CashReceiptConcepts)

    ''' <summary>
    ''' Obtiene un concepto de recibo de caja
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCashReceiptConcept(ByVal code As String) As CashReceiptConcepts

    ''' <summary>
    ''' metodo para obtener un concepto de recibo de  caja por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetCashReceiptConceptById(id As Integer) As CashReceiptConcepts

    ''' <summary>
    ''' Obtiene los conceptos de recibo de caja que estan relacionados a un concepto de flujo de caja
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetCashReceiptConceptByFlowConcept(id As Integer) As List(Of CashReceiptConcepts)

End Interface
