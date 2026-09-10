'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 15-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Payroll.Entities
Imports Domain.Base

#End Region

Public Interface ITradeUnionRepository
    Inherits IRepository(Of TradeUnion)

    ''' <summary>
    ''' obtiene una unidad de paquete por codigo
    ''' </summary>
    ''' <param name="code">codigo de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTradeUnionByCode(code As String, tracking As Boolean) As TradeUnion

    ''' <summary>
    ''' obtiene una unidad de paquete por id
    ''' </summary>
    ''' <param name="id">id de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTradeUnionById(id As Integer, tracking As Boolean) As TradeUnion

    ''' <summary>
    ''' Obtiene un Sindicato por el Id del Concepto
    ''' </summary>
    ''' <param name="ConceptId">Id Concepto</param>
    ''' <param name="tracking"></param>
    ''' <returns>TradeUnion</returns>
    ''' <remarks></remarks>
    Function GetTradeUnionByConceptId(id As Integer, tracking As Boolean) As TradeUnion

End Interface
