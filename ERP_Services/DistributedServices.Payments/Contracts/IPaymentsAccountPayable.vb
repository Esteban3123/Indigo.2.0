'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IPaymentsAccountPayable

    ''' <summary>
    ''' Importa facturas a la cuenta por pagar
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportBillsToAccountPayable(data As List(Of List(Of String)), dataImport As List(Of ImportFileRow), ParamArray parameters As Object()) As ActionResult(Of List(Of AccountPayable))

    <OperationContract()>
    Function GetAccountPayableByBillNumberAndMainAccount(ByVal code As String, ByVal idSupplier As Integer, mainAccountId As Integer) As AccountPayable

    ''' <summary>
    ''' Guarda o Actualiza una cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAccountPayable(accountPayable As Domain.Entities.AccountPayable, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayable)

    ''' <summary>
    ''' Guarda las facturas de pagos
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveListAccountPayable(listAccountPayable As List(Of Domain.Entities.AccountPayable), listDeferredCausation As List(Of DeferredCausation), modeSaveAndConfirm As Boolean, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of String))

    ''' <summary>
    ''' Elimina una cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteAccountPayable(ListAccountPayable As List(Of AccountPayable), audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene una determinada cuenta por pagar
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayable(code As String, audit As AuditMessage) As Domain.Entities.AccountPayable

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountPayableByBillNumber(ByVal code As String, ByVal idSupplier As Integer) As AccountPayable

    ''' <summary>
    ''' Obtiene todas las facturas por el codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountPayableByCode(code As String, audit As AuditMessage) As List(Of Domain.Entities.AccountPayable)

    ''' <summary>
    ''' Obtiene una lista de cuentas por pagar que estan asociadas a la unidad de radicacion
    ''' </summary>
    ''' <param name="FilingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetListAccountPayableByFilingUnitId(ByVal FilingUnitId As Integer) As ActionResult(Of List(Of AccountPayable))

    ''' <summary>
    ''' Obtiene todas las facturas que tiene un proveedor
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountPayableByIdSupplier(idSupplier As Integer, audit As AuditMessage) As List(Of Domain.Entities.AccountPayable)

    ''' <summary>
    ''' Obtiene todas las facturas que tiene un proveedor y que esten confirmadas
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountPayableByIdSupplierAndState(idSupplier As Integer, status As Integer, audit As AuditMessage) As List(Of Domain.Entities.AccountPayable)

    ''' <summary>
    ''' Obtiene una factura por id del tercero y el estado
    ''' </summary>
    ''' <param name="IdThird">The identifier third.</param>
    ''' <param name="state">The state.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayableByIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, audit As AuditMessage) As List(Of Domain.Entities.AccountPayable)

    ''' <summary>
    ''' obtiene las cuotas de una factura por el id de la factura, id del tercero y el estado
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, audit As AuditMessage) As List(Of Domain.Entities.AccountPayableShares)

    ''' <summary>
    ''' Obtiene la cantidad de cuentas asociadas a un tercero cuenta y estado
    ''' </summary>
    <OperationContract()>
    Function GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, audit As AuditMessage) As Integer

    ''' <summary>
    ''' Obtiene una determinada cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostCenterById(id As Integer, audit As AuditMessage) As Domain.Entities.CostCenter

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateAccountPayable(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayable)

    ''' <summary>
    ''' Obtiene una cuenta por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayableById(Id As Integer, audit As AuditMessage) As Domain.Entities.AccountPayable

    ''' <summary>
    ''' Obtiene una cuenta por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayableByIdForNotes(Id As Integer, audit As AuditMessage) As Domain.Entities.AccountPayable

    ''' <summary>
    ''' Obtiene una cuota de factura por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayableShareById(Id As Integer, audit As AuditMessage) As Domain.Entities.AccountPayableShares

    ''' <summary>
    ''' Obtiene las cuotas de la factura
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayableSharesByIdAccountPayable(idAccountPayable As Integer, audit As AuditMessage) As List(Of Domain.Entities.AccountPayableShares)

    ''' <summary>
    ''' Anula la cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function AnnularAccountPayable(ListAccountPayable As List(Of Domain.Entities.AccountPayable), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.AccountPayable))

    ''' <summary>
    ''' Obtiene la validacion de si la cxp maneja causacion diferida
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetValidationDeferredCausation(accountPayableId As Integer) As ActionResult(Of AccountPayable)

    ''' <summary>
    ''' Consulta si hay registros de cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetCheckExistAccountPayable() As ActionResult(Of AccountPayable)

    ''' <summary>
    ''' Obtiene el registro que tenga el mas alto consecutivo de radicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetUltimateRegisterConsecutiveFiling() As ActionResult(Of AccountPayable)

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountsPayableByBillNumber(ByVal code As String) As AccountPayable

    ''' <summary>
    ''' Obtiene todas las lineas de distribución de un proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAllDistributionLines() As List(Of Domain.Entities.SuppliersDistributionLines)

    ''' <summary>
    ''' obtener información relacionada con soporte de pago proveedores. La operación toma un parámetro llamado xml,
    ''' que se espera que contenga datos necesarios para la consulta de soporte de pago proveedores. 
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSupportPaymentSuppliers(xml As String) As ActionResult(Of List(Of SP_SupportPaymentSuppliers_Result))

    ''' <summary>
    ''' obtener información relacionada con soporte de pago proveedores. La operación toma un parámetro llamado xml,
    ''' que se espera que contenga datos necesarios para la consulta de soporte de pago proveedores. 
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SetCopyPasteOrImportFileDeferredCausation(data As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), valueShare As Decimal) As ActionResult(Of List(Of Domain.Entities.DeferredCausationDetails))
End Interface