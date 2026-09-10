Imports System.Threading.Tasks
Imports Domain.Base
Imports Domain.Entities

Public Interface IBasicBillingRepository
    Inherits IRepository(Of BasicBilling)

    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBasicBillingById(id As Integer) As BasicBilling

    ''' <summary>
    ''' obtiene una factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBasicBillingByCode(code As String) As BasicBilling

    ''' <summary>
    ''' obtiene los recibos de caja vinculados a la factura por su id
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <returns></returns>
    Function GetCashReceiptsByBasicBillingInvoice(id As Integer) As List(Of CashReceipts)

    ''' <summary>
    ''' Copiar y pegar para el formulario
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SetBasicBillingDetailFromFile(addressId As Integer, wareHouseId As Integer, xmlObject As String) As List(Of SP_SetBasicBillingDetailFromFile_Result)

    ''' <summary>
    ''' Guarda una factura
    ''' </summary>
    ''' <param name="EntityXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_SaveBasicBilling(EntityXml As String, codeUser As String) As Task(Of SP_SaveBasicBilling_Result)

    ''' <summary>
    ''' Confirma una factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_ConfirmBasicBilling(id As Integer, codeUser As String, cashReceiptsXml As String, companyType As Integer) As Task(Of SP_ConfirmBasicBilling_Result)

    ''' <summary>
    ''' Reversa una factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_ReverseBasicBilling(id As Integer, codeUser As String, reversalReasonId As Integer, reversalReasonDescription As String, companyType As Integer) As SP_ReverseBasicBilling_Result

    ''' <summary>
    ''' Funcion para validar si ya esta asociada el Id de una direccion
    ''' </summary>
    ''' <param name="AdressId"></param>
    ''' <returns></returns>
    Function ValidateAdressInBasicBilling(AdressId As Integer) As Boolean

End Interface
