'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IMarketingUnitRepository
    Inherits IRepository(Of MarketingUnit)

    ''' <summary>
    ''' Obtiene una unida de mercadeo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMarketingUnit(code As String) As MarketingUnit

    ''' <summary>
    ''' Obtiene una unidad de mercadeo por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMarketingUnitById(id As Integer) As MarketingUnit

End Interface
