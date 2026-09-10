'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities

Public Interface IAccountPayableRepository
    Inherits IRepository(Of AccountPayable)

    ''' <summary>
    ''' Valida el CopyPaste de facturas del form de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ImportBillsToAccountPayable(XmlObject As String, XmlParameters As String) As List(Of SP_ImportBillsToAccountPayable_Result)

    Function ListAccountPayableById(listId As List(Of Integer?)) As List(Of AccountPayable)

    Function ListAccountPayableMassiveConfirm(listDocuments As List(Of String)) As List(Of AccountPayable)

    ''' <summary>
    ''' Obtiene una cuenta por pagar
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayable(ByVal code As String, Optional tracking As Boolean = True) As AccountPayable

    ''' <summary>
    ''' obtiene las cuotas de una factura por el id de la factura, id del tercero y el estado
    ''' </summary>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(ByVal IdThird As Integer, ByVal IdAccount As Integer, ByVal state As Short, Optional tracking As Boolean = True) As List(Of AccountPayableShares)

    ''' <summary>
    ''' Obtiene la cantidad de cuentas asociadas a un tercero cuenta y estado
    ''' </summary>
    Function GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(ByVal IdThird As Integer, ByVal IdAccount As Integer, ByVal state As Short, Optional tracking As Boolean = True) As Integer

    ''' <summary>
    ''' Obtiene una cuota de factura por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetAccountPayableShareById(ByVal Id As Integer) As AccountPayableShares

    ''' <summary>
    ''' Obtiene una cuenta por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountPayableById(ByVal Id As Integer, Optional tracking As Boolean = True) As AccountPayable

    ''' <summary>
    ''' Obtiene una cuenta por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountPayableByIdForNotes(ByVal Id As Integer, Optional tracking As Boolean = True) As AccountPayable

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableByBillNumber(ByVal code As String, ByVal idSupplier As Integer, Optional formAccountPayable As Boolean = True) As AccountPayable

    ''' <summary>
    ''' Obtiene todas las lineas de distribución de un proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAllDistributionLines() As List(Of SuppliersDistributionLines)

    Function GetAccountPayableByBillNumberAndMainAccount(ByVal code As String, ByVal idSupplier As Integer, mainAccountId As Integer) As AccountPayable

    ''' <summary>
    ''' Obtiene todas las facturas por el codigo de cuenta por pagar
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableByCode(ByVal code As String, Optional tracking As Boolean = True) As List(Of AccountPayable)

    ''' <summary>
    ''' Valida si la cuenta por pagar esta asociada a un traslado
    ''' </summary>
    ''' <param name="AccountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetIfTransferContainsAccountPayable(AccountPayableId As Integer) As Boolean

    ''' <summary>
    ''' Obtiene una lista de cuentas por pagar por unidad de radicacion
    ''' </summary>
    ''' <param name="FilingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListAccountPayableByFilingUnitId(ByVal FilingUnitId As Integer) As List(Of AccountPayable)

    ''' <summary>
    ''' Obtiene todas las facturas que tiene un proveedor
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableByIdSupplier(ByVal idSupplier As Integer, Optional tracking As Boolean = True) As List(Of AccountPayable)

    ''' <summary>
    ''' Obtiene las facturas que tiene un proveedor y que esten confirmadas
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="status"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableByIdSupplierAndState(ByVal idSupplier As Integer, ByVal status As Integer, Optional tracking As Boolean = True) As List(Of AccountPayable)

    ''' <summary>
    ''' Obtiene una factura por id del tercero y el estado
    ''' </summary>
    ''' <param name="IdThird">The identifier third.</param>
    ''' <param name="state">The state.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountPayableByIdThirdIdAccountAndState(ByVal IdThird As Integer, ByVal IdAccount As Integer, ByVal state As Short, Optional tracking As Boolean = True) As List(Of AccountPayable)

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetCostCenterById(ByVal id As Integer, Optional tracking As Boolean = True) As CostCenter

    ''' <summary>
    ''' Obtiene las cuotas de la factura
    ''' </summary>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountPayableSharesByIdAccountPayable(ByVal idAccountPayable As Integer, Optional tracking As Boolean = True) As List(Of AccountPayableShares)

    ''' <summary>
    ''' Obtiene la validacion si la cxp escogida en las notas maneja causacion diferida
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetValidationDeferredCausation(accountPayableId As Integer) As Boolean

    ''' <summary>
    ''' Consulta si hay registros de cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCheckExistAccountPayable() As Boolean

    ''' <summary>
    ''' Obtiene el registro que tiene el mas alto numero consecutivo de radicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUltimateRegisterConsecutiveFiling() As AccountPayable

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountsPayableByBillNumber(ByVal BillNumber As String) As AccountPayable

    ''' <summary>
    ''' Se valida que la distribución no este dentro de otra cxp
    ''' </summary>
    ''' <param name="CostDistributionDirectCostId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateAccountPayableByCostDistributionDirectCostId(CostDistributionDirectCostId As Integer, AccountPayableId As Integer) As AccountPayable

    ''' <summary>
    ''' Se valida que la distribución este por el mismo valor de la factura
    ''' </summary>
    ''' <param name="AccountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateBillValueIsSameInAccountPayableAndCostDistributionDirectCost(AccountPayableId As Integer) As CostDistributionDirectCost

    ''' <summary>
    ''' Saves the account payable list.
    ''' </summary>
    ''' <param name="accountPayableList">The account payable list.</param>
    Sub SaveAccountPayableList(accountPayableList As List(Of AccountPayable))

    ''' <summary>
    ''' Confirma una cuenta por pagar
    ''' </summary>
    ''' <param name="AccountsPayableXml"></param>
    ''' <param name="codeUser"></param>
    ''' <param name="isMassiveConfirm"></param>
    ''' <returns></returns>
    Function SP_ConfirmAccountsPayable(AccountsPayableXml As String, codeUser As String, isMassiveConfirm As Boolean) As List(Of SP_ConfirmAccountsPayable_Result)

    ''' <summary>
    ''' Guarda la cuenta por pagar
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function SP_SaveAccountsPayable(Xml As String, UserCode As String) As SP_SaveAccountsPayable_Result

    ''' <summary>
    ''' Esta función se utiliza para recuperar información sobre Soporte de pagos proveedores
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <returns></returns>
    Function SP_SupportPaymentSuppliers(Xml As String) As List(Of SP_SupportPaymentSuppliers_Result)

    Function GetCostCenterByCode(Code As String) As CostCenter

End Interface