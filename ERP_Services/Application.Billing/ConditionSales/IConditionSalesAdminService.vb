'***********************************************************************
' Assembly         : Application.Billing
' Author           : Andres Alarcon
' Created          : 14-06-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IConditionSalesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Function ListConditionSalesByUserCode(userCode As String) As List(Of ConditionSales)


    ''' <summary>
    ''' elimina una autorización
    ''' </summary>
    ''' <param name="ConditionSales">The billing authorization.</param>
    ''' <returns></returns>
    Function DeleteConditionSales(ConditionSales As ConditionSales, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda una autorización
    ''' </summary>
    ''' <param name="ConditionSales">The billing authorization.</param>
    ''' <returns></returns>
    Function SaveConditionSales(ConditionSales As ConditionSales, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ConditionSales)

    ''' <summary>
    ''' Obtiene una autorizacion de factura por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConditionSalesById(ByVal Id As Integer, audit As AuditMessage) As ActionResult(Of ConditionSales)

    ''' <summary>
    ''' Obtiene una autorización de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConditionSalesByCode(code As String, audit As AuditMessage) As ActionResult(Of ConditionSales)

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateConditionSales(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ConditionSales)

End Interface
