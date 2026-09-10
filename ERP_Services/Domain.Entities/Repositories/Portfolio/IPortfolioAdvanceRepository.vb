'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IPortfolioAdvanceRepository
    Inherits IRepository(Of PortfolioAdvance)

    Function GetPortfolioAdvanceByIdSimple(id As Integer) As PortfolioAdvance

    Function GetAdvancePortfolioNote(thirdPartyId As Integer, codeAdvance As String, nature As Integer) As PortfolioAdvance

    ''' <summary>
    ''' Obtiene un anticipo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetPortfolioAdvance(ByVal code As String) As PortfolioAdvance
    
    ''' <summary>
    ''' Obtiene un anticipo por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetPortfolioAdvanceById(ByVal Id As Integer) As PortfolioAdvance

    ''' <summary>
    ''' Lista todos los anticipos por Id del tercero
    ''' </summary>
    ''' <returns></returns>
    Function ListPorfolioAdvanceByThirdId(ByVal ThirdId As Integer) As List(Of PortfolioAdvance)

    ''' <summary>
    ''' Lists the porfolio advance by third identifier with balance.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    Function ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance(ByVal ThirdId As Integer, admission As String) As List(Of PortfolioAdvance)
    ''' <summary>
    ''' obtiene un anticipo por el id del detalle del recibo de caja
    ''' </summary>
    ''' <param name="cashRecepitsDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioAdvanceByCashReceiptsDetailId(cashRecepitsDetailId As Integer) As PortfolioAdvance
    Function GetOnlyPortfolioAdvanceById(Id As Integer, tacking As Boolean) As PortfolioAdvance
End Interface