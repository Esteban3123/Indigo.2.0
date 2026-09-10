'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Juan Carlos Bermudez
' Created          : 16/07/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPayrollTradeUnion

    ''' <summary>
    ''' Guarda o Actualiza un sindicato
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveTradeUnion(tradeUnion As TradeUnion, session As SessionValues, idSequense As Int64) As ActionResult(Of TradeUnion)

    ''' <summary>
    ''' Elimina un sindicato
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteTradeUnion(ByVal tradeUnion As TradeUnion, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Obtiene un determinado sindicato
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetTradeUnionByCode(ByVal code As String, tracking As Boolean, session As SessionValues) As ActionResult(Of TradeUnion)

    ''' <summary>
    ''' Obtiene un determinado sindicato
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetTradeUnionById(ByVal id As Integer, tracking As Boolean, session As SessionValues) As ActionResult(Of TradeUnion)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ChangeStateTradeUnion(ByVal code As String, ByVal state As Boolean, session As SessionValues) As ActionResult(Of TradeUnion)

End Interface
