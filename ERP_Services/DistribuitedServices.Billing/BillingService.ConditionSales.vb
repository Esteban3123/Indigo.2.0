'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Andres Alarcon
' Created          : 14-06-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Microsoft.Practices.Unity
Imports Application.Billing

#End Region

Partial Class BillingService
    Implements IBillingServiceConditionSales

    ''' <summary>
    ''' Lista todas las condiciones de venta por código de usuario
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListConditionSalesByUserCode(userCode As String) As List(Of ConditionSales) Implements IBillingServiceConditionSales.ListConditionSalesByUserCode
        Using service As IConditionSalesAdminService = Container.Current.Resolve(Of IConditionSalesAdminService)()
            Return service.ListConditionSalesByUserCode(userCode)
        End Using
    End Function

    ''' <summary>
    ''' elimina una condicion de venta
    ''' </summary>
    ''' <param name="ConditionSales"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteConditionSales(ConditionSales As ConditionSales, audit As AuditMessage) As ActionResult Implements IBillingServiceConditionSales.DeleteConditionSales
        Using service As IConditionSalesAdminService = Container.Current.Resolve(Of IConditionSalesAdminService)()
            Return service.DeleteConditionSales(ConditionSales, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza una condicion de venta
    ''' </summary>
    ''' <param name="ConditionSales"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveConditionSales(ConditionSales As ConditionSales, idSequense As Int64, audit As AuditMessage) As ActionResult(Of ConditionSales) Implements IBillingServiceConditionSales.SaveConditionSales
        Using service As IConditionSalesAdminService = Container.Current.Resolve(Of IConditionSalesAdminService)()
            Return service.SaveConditionSales(ConditionSales, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una condicion de venta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConditionSalesById(id As Integer, audit As AuditMessage) As ActionResult(Of ConditionSales) Implements IBillingServiceConditionSales.GetConditionSalesById
        Using service As IConditionSalesAdminService = Container.Current.Resolve(Of IConditionSalesAdminService)()
            Return service.GetConditionSalesById(id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una condicion de venta por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConditionSalesByCode(code As String, audit As AuditMessage) As ActionResult(Of ConditionSales) Implements IBillingServiceConditionSales.GetConditionSalesByCode
        Using service As IConditionSalesAdminService = Container.Current.Resolve(Of IConditionSalesAdminService)()
            Return service.GetConditionSalesByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateConditionSales(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ConditionSales) Implements IBillingServiceConditionSales.ChangeStateConditionSales
        Using service As IConditionSalesAdminService = Container.Current.Resolve(Of IConditionSalesAdminService)()
            Return service.ChangeStateConditionSales(code, state, audit)
        End Using
    End Function

End Class
