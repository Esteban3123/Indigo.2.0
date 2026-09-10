'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IPortfolioServicePortfolioAdvance

    ''' <summary>
    ''' Guarda un anticipo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePortfolioAdvance(portfolioAdvance As Domain.Entities.PortfolioAdvance, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioAdvance)

    ''' <summary>
    ''' Elimina un anticipo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePortfolioAdvance(portfolioAdvance As Domain.Entities.PortfolioAdvance, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un anticipo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPortfolioAdvance(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioAdvance)

    ''' <summary>
    ''' Obtiene un anticipo por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetPortfolioAdvanceById(ByVal Id As Integer) As ActionResult(Of PortfolioAdvance)

    ''' <summary>
    ''' Lista todos los anticipos por Id del tercero
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function ListPorfolioAdvanceByThirdId(ByVal ThirdId As Integer) As ActionResult(Of List(Of PortfolioAdvance))

    ''' <summary>
    ''' Lists the porfolio advance by third identifier with balance.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance(ByVal ThirdId As Integer, admission As String) As ActionResult(Of List(Of PortfolioAdvance))

    ''' <summary>
    ''' Get the Balance of advance in diferent Currency
    ''' </summary>
    ''' <param name="portfolioAdvanceId"></param>
    ''' <param name="toCurrencyId"></param>
    ''' <param name="dateTRM"></param>
    ''' <param name="moduleTRM"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBalanceAdvanceByCurrency(portfolioAdvanceId As Integer,
                                            Optional toCurrencyId As Integer? = Nothing,
                                            Optional dateTRM As Date? = Nothing,
                                            Optional moduleTRM As EModuleTRM = EModuleTRM.CommonTRM) As ActionResult(Of PortfolioAdvance)

End Interface