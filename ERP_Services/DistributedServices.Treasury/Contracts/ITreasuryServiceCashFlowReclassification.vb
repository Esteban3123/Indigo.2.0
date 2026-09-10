'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Hector Rodriguez Rubiano
' Created          : 22/11/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Entities
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface ITreasuryServiceCashFlowReclassification
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="oCashFlowReclassification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCashFlowReclassification(ByVal oCashFlowReclassification As CashFlowReclassification, ByVal audit As AuditMessage) As ActionResult(Of CashFlowReclassification)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashFlowReclassificationById(ByVal id As Integer) As CashFlowReclassification
End Interface
