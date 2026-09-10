'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Hector Rodriguez Rubiano
' Created          : 05/11/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface ICashFlowConceptRepository
    Inherits IRepository(Of CashFlowConcept)

#Region "Methods"

    ''' <summary>
    ''' metodo para obtener un concepto de flujo de efectivo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetCashFlowConceptByCode(ByVal code As String) As CashFlowConcept

    ''' <summary>
    ''' metodo para obtener un concepto de flujo de efectivo por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCashFlowConceptById(ByVal id As Integer) As CashFlowConcept

#End Region

End Interface
