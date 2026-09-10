'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMarketingUnitAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza una unidad de mercadeo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveMarketingUnit(ByVal MarketingUnit As MarketingUnit, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of MarketingUnit)

    ''' <summary>
    ''' Elimina una unidad de mercadeo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteMarketingUnit(ByVal MarketingUnit As MarketingUnit, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una unidad de mercadeo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetMarketingUnit(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of MarketingUnit)

    ''' <summary>
    ''' Obtiene una unidad de mercadeo por id
    ''' </summary>
    ''' <returns></returns>
    Function GetMarketingUnitById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of MarketingUnit)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateMarketingUnit(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of MarketingUnit)

End Interface
