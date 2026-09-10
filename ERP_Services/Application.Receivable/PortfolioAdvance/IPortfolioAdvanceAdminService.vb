'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPortfolioAdvanceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un anticipo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Function SavePortfolioAdvance(ByVal portfolioAdvance As PortfolioAdvance, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0, Optional ByVal withCommit As Boolean = True) As ActionResult(Of PortfolioAdvance)

    ''' <summary>
    ''' Elimina un anticipo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePortfolioAdvance(ByVal portfolioAdvance As PortfolioAdvance, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un anticipo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetPortfolioAdvance(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of PortfolioAdvance)

    ''' <summary>
    ''' Obtiene un anticipo por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetPortfolioAdvanceById(ByVal Id As Integer) As ActionResult(Of PortfolioAdvance)

    ''' <summary>
    ''' Lista todos los anticipos por Id del tercero
    ''' </summary>
    ''' <returns></returns>
    Function ListPorfolioAdvanceByThirdId(ByVal ThirdId As Integer) As ActionResult(Of List(Of PortfolioAdvance))

    ''' <summary>
    ''' Lists the porfolio advance by third identifier with balance.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    Function ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance(ThirdId As Integer, admission As String) As ActionResult(Of List(Of PortfolioAdvance))

    ''' <summary>
    ''' Get the Balance of advance in diferent Currency
    ''' </summary>
    ''' <param name="portfolioAdvanceId"></param>
    ''' <param name="toCurrencyId"></param>
    ''' <param name="dateTRM"></param>
    ''' <param name="moduleTRM"></param>
    ''' <returns></returns>
    Function GetBalanceAdvanceByCurrency(portfolioAdvanceId As Integer,
                                            Optional toCurrencyId As Integer? = Nothing,
                                            Optional dateTRM As Date? = Nothing,
                                            Optional moduleTRM As EModuleTRM = EModuleTRM.CommonTRM) As ActionResult(Of PortfolioAdvance)

End Interface
