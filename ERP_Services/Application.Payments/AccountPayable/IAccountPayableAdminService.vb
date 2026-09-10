'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IAccountPayableAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Importa facturas a la cuenta por pagar
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Function ImportBillsToAccountPayable(data As List(Of List(Of String)), dataImport As List(Of ImportFileRow), ParamArray parameters As Object()) As ActionResult(Of List(Of AccountPayable))


    Function GetAccountPayableByBillNumberAndMainAccount(ByVal code As String, ByVal idSupplier As Integer, mainAccountId As Integer) As AccountPayable

    ''' <summary>
    ''' Guarda o Actualiza una cuenta por pagar
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAccountPayable(ByVal accountPayable As AccountPayable, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0, Optional ByVal withCommit As Boolean = True) As ActionResult(Of AccountPayable)

    ''' <summary>
    ''' Anula la cuenta por pagar
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function AnnularAccountPayable(ByVal ListAccountPayable As List(Of AccountPayable), ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of List(Of AccountPayable))

    ''' <summary>
    ''' Guarda las facturas de pagos asociados a la misma cuenta por pagar
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveListAccountPayableWithReturn(ByVal listAccountPayable As List(Of AccountPayable), ByVal listDeferredCausation As List(Of DeferredCausation), ByVal modeSaveAndConfirm As Boolean, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of List(Of AccountPayable))

    ''' <summary>
    ''' Guarda las facturas de pagos
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveListAccountPayable(ByVal listAccountPayable As List(Of AccountPayable), ByVal listDeferredCausation As List(Of DeferredCausation), ByVal modeSaveAndConfirm As Boolean, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of List(Of String))

    ''' <summary>
    ''' Guarda las facturas de pagos
    ''' </summary>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmAccountPayable(ByVal listAccountPayable As List(Of AccountPayable), ByVal audit As AuditMessage, Optional isMassiveConfirm As Boolean = False) As ActionResult(Of List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' Elimina una cuenta por pagar
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteAccountPayable(ByVal ListAccountPayable As List(Of AccountPayable), ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un centro de costo por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCostCenterById(ByVal id As Integer, ByVal audit As AuditMessage) As CostCenter

    ''' <summary>
    ''' Obtiene una determinada dependencia
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetAccountPayable(ByVal code As String, ByVal audit As AuditMessage) As AccountPayable

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableByBillNumber(ByVal code As String, ByVal idSupplier As Integer) As AccountPayable

    ''' <summary>
    ''' Obtiene todas las facturas por el codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableByCode(ByVal code As String, ByVal audit As AuditMessage) As List(Of AccountPayable)

    ''' <summary>
    ''' Obtiene una lista de cuentas por pagar que tenga la unidad de radicacion
    ''' </summary>
    ''' <param name="FilingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListAccountPayableByFilingUnitId(ByVal FilingUnitId As Integer) As ActionResult(Of List(Of AccountPayable))

    ''' <summary>
    ''' Obtiene todas las facturas que tiene un proveedor
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableByIdSupplier(ByVal idSupplier As Integer, ByVal audit As AuditMessage) As List(Of AccountPayable)

    ''' <summary>
    ''' Obtiene las facturas que tiene un proveedor y que esten confirmadas
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableByIdSupplierAndState(ByVal idSupplier As Integer, ByVal status As Integer, ByVal audit As AuditMessage) As List(Of AccountPayable)

    ''' <summary>
    ''' Obtiene una factura por id del tercero y el estado
    ''' </summary>
    ''' <param name="IdThird">The identifier third.</param>
    ''' <param name="state">The state.</param>
    ''' <returns></returns>
    Function GetAccountPayableByIdThirdIdAccountAndState(ByVal IdThird As Integer, ByVal IdAccount As Integer, ByVal state As Short, ByVal audit As AuditMessage) As List(Of AccountPayable)

    ''' <summary>
    ''' obtiene las cuotas de una factura por el id de la factura, id del tercero y el estado
    ''' </summary>
    ''' <returns></returns>
    Function GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(ByVal IdThird As Integer, ByVal IdAccount As Integer, ByVal state As Short, ByVal audit As AuditMessage) As List(Of AccountPayableShares)

    ''' <summary>
    ''' Obtiene la cantidad de cuentas asociadas a un tercero cuenta y estado
    ''' </summary>
    Function GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(ByVal IdThird As Integer, ByVal IdAccount As Integer, ByVal state As Short, ByVal audit As AuditMessage) As Integer

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AccountPayable)

    ''' <summary>
    ''' Obtiene una cuenta por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountPayableById(ByVal Id As Integer, ByVal audit As AuditMessage, Optional tracking As Boolean = True) As AccountPayable

    ''' <summary>
    ''' Obtiene una cuenta por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountPayableByIdForNotes(ByVal Id As Integer, ByVal audit As AuditMessage, Optional tracking As Boolean = True) As AccountPayable

    ''' <summary>
    ''' Obtiene una cuota de factura por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetAccountPayableShareById(ByVal Id As Integer, ByVal audit As AuditMessage) As AccountPayableShares

    ''' <summary>
    ''' Obtiene las cuotas de la factura
    ''' </summary>
    ''' <returns></returns>
    Function GetAccountPayableSharesByIdAccountPayable(ByVal idAccountPayable As Integer, ByVal audit As AuditMessage) As List(Of AccountPayableShares)

    ''' <summary>
    ''' Obtiene la validacion de si la cxp maneja causacion diferida
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetValidationDeferredCausation(ByVal accountPayableId As Integer) As ActionResult(Of AccountPayable)

    ''' <summary>
    ''' Consulta si hay registros de cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCheckExistAccountPayable() As ActionResult(Of AccountPayable)

    ''' <summary>
    ''' Obtiene el registro que tiene el mayor valor de consecutivo de radicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUltimateRegisterConsecutiveFiling() As ActionResult(Of AccountPayable)

    ''' <summary>
    ''' Obtiene la Cuenta por Pagar teniendo en cuenta el número de la Factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountsPayableByBillNumber(BillNumber As String) As AccountPayable

    ''' <summary>
    ''' Obtiene todas las lineas de distribución de un proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAllDistributionLines() As List(Of Domain.Entities.SuppliersDistributionLines)

    ''' <summary>
    ''' Esta función se utiliza para recuperar información sobre Soporte de pagos proveedores
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    Function GetSupportPaymentSuppliers(xml As String) As ActionResult(Of List(Of SP_SupportPaymentSuppliers_Result))


    ''' <summary>
    ''' Esta función se utiliza para recuperar información sobre Soporte de pagos proveedores
    ''' </summary>
    ''' <returns></returns>
    Function SetCopyPasteOrImportFileDeferredCausation(data As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), valueShare As Decimal) As ActionResult(Of List(Of Domain.Entities.DeferredCausationDetails))

End Interface