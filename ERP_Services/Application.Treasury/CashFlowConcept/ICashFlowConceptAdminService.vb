'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Hector Rodriguez Rubiano
' Created          : 05/11/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface ICashFlowConceptAdminService
    Inherits IDisposable

    ''' <summary>
    ''' metodo para obtener un concepto de flujo de efectivo por codigo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetCashFlowConceptByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CashFlowConcept)

    ''' <summary>
    ''' Obtiene un concepto de flujo de efectivo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCashFlowConceptById(id As Integer) As ActionResult(Of CashFlowConcept)

    ''' <summary>
    ''' metodo para guardar un concepto de flujo de efectivo
    ''' </summary>    
    Function SaveCashFlowConcept(ByVal CashFlowConcept As CashFlowConcept, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of CashFlowConcept)

    ''' <summary>
    ''' metodo para eliminar un concepto de flujo de efectivo
    ''' </summary>    
    Function DeleteCashFlowConcept(ByVal CashFlowConcept As CashFlowConcept, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStateCashFlowConcept(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CashFlowConcept)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="parameters"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ListCashFlowStatus(ByVal parameters As String, ByVal session As SessionValues) As DataSet

End Interface
