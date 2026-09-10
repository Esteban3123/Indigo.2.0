'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payments
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports NewRelic.Api.Agent

Partial Public Class PaymentsService

    Public Function ImportBillsToAccountPayable(data As List(Of List(Of String)), dataImport As List(Of ImportFileRow), ParamArray parameters() As Object) As ActionResult(Of List(Of AccountPayable)) Implements IPaymentsAccountPayable.ImportBillsToAccountPayable
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.ImportBillsToAccountPayable(data, dataImport, parameters)
        End Using
    End Function

    Public Function GetAccountPayableByBillNumberAndMainAccount(code As String, idSupplier As Integer, mainAccountId As Integer) As AccountPayable Implements IPaymentsAccountPayable.GetAccountPayableByBillNumberAndMainAccount
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableByBillNumberAndMainAccount(code, idSupplier, mainAccountId)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableByBillNumberAndMainAccount(code, idSupplier, mainAccountId)
    End Function

    ''' <summary>
    ''' Elimina una cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAccountPayable(ListAccountPayable As List(Of AccountPayable), audit As AuditMessage) As ActionResult Implements IPaymentsAccountPayable.DeleteAccountPayable
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.DeleteAccountPayable(ListAccountPayable, audit)
        End Using
        'Return Me._accountPayableAdminService.DeleteAccountPayable(ListAccountPayable, audit)
    End Function

    ''' <summary>
    ''' Obtiene una determinada cuenta por pagar
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayable(code As String, audit As AuditMessage) As AccountPayable Implements IPaymentsAccountPayable.GetAccountPayable
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayable(code, audit)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayable(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByBillNumber(code As String, idSupplier As Integer) As AccountPayable Implements IPaymentsAccountPayable.GetAccountPayableByBillNumber
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableByBillNumber(code, idSupplier)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableByBillNumber(code, idSupplier)
    End Function

    ''' <summary>
    ''' Obtiene el listado de facturas por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByCode(code As String, audit As AuditMessage) As List(Of AccountPayable) Implements IPaymentsAccountPayable.GetAccountPayableByCode
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableByCode(code, audit)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableByCode(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una lista de cuentas por pagar que tiene asociado la unidad de radicacion
    ''' </summary>
    ''' <param name="FilingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListAccountPayableByFilingUnitId(FilingUnitId As Integer) As ActionResult(Of List(Of AccountPayable)) Implements IPaymentsAccountPayable.GetListAccountPayableByFilingUnitId
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetListAccountPayableByFilingUnitId(FilingUnitId)
        End Using
        'Return Me._accountPayableAdminService.GetListAccountPayableByFilingUnitId(FilingUnitId)
    End Function

    ''' <summary>
    ''' Obtiene una cuota de factura por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetAccountPayableShareById(Id As Integer, audit As AuditMessage) As AccountPayableShares Implements IPaymentsAccountPayable.GetAccountPayableShareById
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableShareById(Id, audit)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableShareById(Id, audit)
    End Function

    ''' <summary>
    ''' Obtiene el listado de facturas por id proveedor
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByIdSupplier(idSupplier As Integer, audit As AuditMessage) As List(Of AccountPayable) Implements IPaymentsAccountPayable.GetAccountPayableByIdSupplier
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableByIdSupplier(idSupplier, audit)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableByIdSupplier(idSupplier, audit)
    End Function

    ''' <summary>
    ''' Obtiene las facturas de un proveedor y que esten confirmadas
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByIdSupplierAndState(idSupplier As Integer, status As Integer, audit As AuditMessage) As List(Of AccountPayable) Implements IPaymentsAccountPayable.GetAccountPayableByIdSupplierAndState
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableByIdSupplierAndState(idSupplier, status, audit)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableByIdSupplierAndState(idSupplier, status, audit)
    End Function

    ''' <summary>
    ''' Obtiene una factura por id del tercero y el estado
    ''' </summary>
    ''' <param name="IdThird">The identifier third.</param>
    ''' <param name="state">The state.</param>
    ''' <returns></returns>
    Public Function GetAccountPayableByIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, audit As AuditMessage) As List(Of AccountPayable) Implements IPaymentsAccountPayable.GetAccountPayableByIdThirdIdAccountAndState
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableByIdThirdIdAccountAndState(IdThird, IdAccount, state, audit)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableByIdThirdIdAccountAndState(IdThird, IdAccount, state, audit)
    End Function

    ''' <summary>
    ''' obtiene las cuotas de una factura por el id de la factura, id del tercero y el estado
    ''' </summary>
    ''' <param name="IdThird"></param>
    ''' <param name="IdAccount"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, audit As AuditMessage) As List(Of AccountPayableShares) Implements IPaymentsAccountPayable.GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(IdThird, IdAccount, state, audit)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(IdThird, IdAccount, state, audit)
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de cuentas asociadas a un tercero cuenta y estado
    ''' </summary>
    ''' <param name="IdThird"></param>
    ''' <param name="IdAccount"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, audit As AuditMessage) As Integer Implements IPaymentsAccountPayable.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(IdThird, IdAccount, state, audit)
        End Using
        'Return Me._accountPayableAdminService.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(IdThird, IdAccount, state, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAccountPayable(accountPayable As Domain.Entities.AccountPayable, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AccountPayable) Implements IPaymentsAccountPayable.SaveAccountPayable
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.SaveAccountPayable(accountPayable, audit, idSequense)
        End Using
        'Return Me._accountPayableAdminService.SaveAccountPayable(accountPayable, audit, idSequense)
    End Function

    ''' <summary>
    ''' Anula la CxP
    ''' </summary>
    ''' <param name="ListAccountPayable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AnnularAccountPayable(ListAccountPayable As List(Of Domain.Entities.AccountPayable), idSequense As Int64, audit As AuditMessage) As ActionResult(Of List(Of AccountPayable)) Implements IPaymentsAccountPayable.AnnularAccountPayable
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.AnnularAccountPayable(ListAccountPayable, audit, idSequense)
        End Using
        'Return Me._accountPayableAdminService.AnnularAccountPayable(ListAccountPayable, audit, idSequense)
    End Function

    ''' <summary>
    ''' Guarda las facturas de pagos
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveListAccountPayable(listAccountPayable As List(Of Domain.Entities.AccountPayable), listDeferredCausation As List(Of DeferredCausation), modeSaveAndConfirm As Boolean, idSequense As Int64, audit As AuditMessage) As ActionResult(Of List(Of String)) Implements IPaymentsAccountPayable.SaveListAccountPayable
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.SaveListAccountPayable(listAccountPayable, listDeferredCausation, modeSaveAndConfirm, audit, idSequense)
        End Using
        'Return Me._accountPayableAdminService.SaveListAccountPayable(listAccountPayable, listDeferredCausation, modeSaveAndConfirm, audit, idSequense)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateAccountPayable(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AccountPayable) Implements IPaymentsAccountPayable.ChangeStateAccountPayable
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._accountPayableAdminService.ChangeState(code, state, audit)
    End Function

    ''' <summary>
    ''' Centro de costo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterById(id As Integer, audit As AuditMessage) As CostCenter Implements IPaymentsAccountPayable.GetCostCenterById
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetCostCenterById(id, audit)
        End Using
        'Return Me._accountPayableAdminService.GetCostCenterById(id, audit)
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetAccountPayableById(Id As Integer, audit As AuditMessage) As AccountPayable Implements IPaymentsAccountPayable.GetAccountPayableById
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableById(Id, audit)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableById(Id, audit)
    End Function

    ''' <summary>
    ''' Obtiene una cxp por id para notas
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByIdForNotes(Id As Integer, audit As AuditMessage) As AccountPayable Implements IPaymentsAccountPayable.GetAccountPayableByIdForNotes
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableByIdForNotes(Id, audit)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableByIdForNotes(Id, audit)
    End Function

    ''' <summary>
    ''' Consulta las cuotas de la factura
    ''' </summary>
    ''' <param name="idAccountPayable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableSharesByIdAccountPayable(idAccountPayable As Integer, audit As AuditMessage) As List(Of AccountPayableShares) Implements IPaymentsAccountPayable.GetAccountPayableSharesByIdAccountPayable
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountPayableSharesByIdAccountPayable(idAccountPayable, audit)
        End Using
        'Return Me._accountPayableAdminService.GetAccountPayableSharesByIdAccountPayable(idAccountPayable, audit)
    End Function

    ''' <summary>
    ''' Obtiene la validacion si la cxp maneja causacion diferida
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValidationDeferredCausation(accountPayableId As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayable) Implements IPaymentsAccountPayable.GetValidationDeferredCausation
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetValidationDeferredCausation(accountPayableId)
        End Using
        'Return Me._accountPayableAdminService.GetValidationDeferredCausation(accountPayableId)
    End Function

    ''' <summary>
    ''' Consulta si hay registros de cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCheckExistAccountPayable() As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayable) Implements IPaymentsAccountPayable.GetCheckExistAccountPayable
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetCheckExistAccountPayable()
        End Using
        'Return Me._accountPayableAdminService.GetCheckExistAccountPayable()
    End Function

    ''' <summary>
    ''' Obtiene el registro que tenga el mas alto consecutivo de radicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUltimateRegisterConsecutiveFiling() As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayable) Implements IPaymentsAccountPayable.GetUltimateRegisterConsecutiveFiling
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetUltimateRegisterConsecutiveFiling()
        End Using
        'Return Me._accountPayableAdminService.GetUltimateRegisterConsecutiveFiling()
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountsPayableByBillNumber(code As String) As Domain.Entities.AccountPayable Implements IPaymentsAccountPayable.GetAccountsPayableByBillNumber
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAccountsPayableByBillNumber(code)
        End Using
        'Return Me._accountPayableAdminService.GetAccountsPayableByBillNumber(code)
    End Function

    ''' <summary>
    ''' Obtiene todas las lineas de distribución de los proveedores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllDistributionLines() As List(Of Domain.Entities.SuppliersDistributionLines) Implements IPaymentsAccountPayable.GetAllDistributionLines
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetAllDistributionLines()
        End Using
        'Return Me._accountPayableAdminService.GetAccountsPayableByBillNumber(code)
    End Function

    ''' <summary>
    ''' La función GetSupportPaymentSuppliers es una implementación de la interfaz IPaymentsAccountPayable y
    ''' se utiliza para obtener información relacionada con Soporte de pagos proveedores.
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    Public Function GetSupportPaymentSuppliers(xml As String) As ActionResult(Of List(Of SP_SupportPaymentSuppliers_Result)) Implements IPaymentsAccountPayable.GetSupportPaymentSuppliers
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.GetSupportPaymentSuppliers(xml)
        End Using
    End Function

    ''' <summary>
    ''' La función GetSupportPaymentSuppliers es una implementación de la interfaz IPaymentsAccountPayable y
    ''' se utiliza para obtener información relacionada con Soporte de pagos proveedores.
    ''' </summary>
    ''' <returns></returns>
    Public Function SetCopyPasteOrImportFileDeferredCausation(data As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), valueShare As Decimal) As ActionResult(Of List(Of Domain.Entities.DeferredCausationDetails)) Implements IPaymentsAccountPayable.SetCopyPasteOrImportFileDeferredCausation
        Using service As IAccountPayableAdminService = Container.Current.Resolve(Of IAccountPayableAdminService)()
            Return service.SetCopyPasteOrImportFileDeferredCausation(data, dataCopyPaste, valueShare)
        End Using
    End Function

End Class