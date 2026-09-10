'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 15-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ITradeUnionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un concepto de pago
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveTradeUnion(ByVal tradeUnion As TradeUnion, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of TradeUnion)

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteTradeUnion(ByVal tradeUnion As TradeUnion, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un determinado concepto de pago 
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetTradeUnionByCode(ByVal code As String, tracking As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TradeUnion)

    ''' <summary>
    ''' Obtiene un determinado concepto de pago 
    ''' </summary>
    ''' <returns></returns>
    Function GetTradeUnionById(ByVal id As Integer, tracking As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TradeUnion)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TradeUnion)

End Interface
