'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Hector Rodriguez Rubiano
' Created          : 21/11/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICashFlowReclassificationAdminService
    Inherits IDisposable
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="oCashFlowReclassification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveCashFlowReclassification(ByVal oCashFlowReclassification As CashFlowReclassification, ByVal audit As AuditMessage) As ActionResult(Of CashFlowReclassification)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetCashFlowReclassificationById(ByVal id As Integer) As CashFlowReclassification
End Interface
