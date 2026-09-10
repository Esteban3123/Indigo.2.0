#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceConditionSales

#Region "Methods"

    ''' <summary>
    ''' Lista todas las condiciones de venta por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    <OperationContract()>
    Function ListConditionSalesByUserCode(userCode As String) As List(Of ConditionSales)

    ''' <summary>
    ''' elimina una condicion de venta
    ''' </summary>
    ''' <param name="ConditionSales"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteConditionSales(ConditionSales As ConditionSales, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o actualiza una condicion de venta
    ''' </summary>
    ''' <param name="ConditionSales"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveConditionSales(ConditionSales As ConditionSales, idSequense As Int64, audit As AuditMessage) As ActionResult(Of ConditionSales)

    ''' <summary>
    ''' Obtiene una condicion de venta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetConditionSalesById(id As Integer, audit As AuditMessage) As ActionResult(Of ConditionSales)

    ''' <summary>
    ''' Obtiene una condicion de venta por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetConditionSalesByCode(code As String, audit As AuditMessage) As ActionResult(Of ConditionSales)

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateConditionSales(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ConditionSales)

#End Region
End Interface
