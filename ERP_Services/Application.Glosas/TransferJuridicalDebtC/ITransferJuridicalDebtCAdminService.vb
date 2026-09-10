#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Interfaz del servicio de Transferencia Cobro Jurídico Cabecera. 
''' </summary>
''' <remarks></remarks>
Public Interface ITransferJuridicalDebtCAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Metodo para pegar o importar detalles en la rejilla
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="customerId"></param>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function CopyAndPasteTransferJuridicalDebtCollectionDetail(ByVal operatingUnitId As Integer, customerId As Integer, transferJuridicalDebtCollectionCId As Integer, copyType As Integer, dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), session As SessionValues) As ActionResult(Of List(Of TransferJuridicalDebtCollectionD))

    ''' <summary>
    ''' Obtiene una cabecera transferencia cobro jurídico especifica.
    ''' </summary>
    ''' <param name="Id">Id Transferencia Cobro Juríco Cabecera</param>
    ''' <returns>Objeto Cabecera Transferencia Cobro Jurídico</returns>
    Function GetTransferJuridicalDebtC(ByVal Id As String) As TransferJuridicalDebtCollectionC

    ''' <summary>
    ''' Obtiene una cabecera transferencia cobro jurídico especifica.
    ''' </summary>
    ''' <param name="Consecutive">Consecutive Transferencia Cobro Jurídico Cabecera</param>
    ''' <returns>Objeto Cabecera Transferencia Cobro Jurídico</returns>
    Function GetTransferJuridicalDebtCByConsecutive(ByVal Consecutive As String) As TransferJuridicalDebtCollectionC

    ''' <summary>
    ''' Funcion para listar todas las cabeceras de transferencias cobro jurídico.
    ''' </summary>
    ''' <returns>Lista de cabeceras transferencias cobro jurídico</returns>
    Function ListAllTransferJuridicalDebtC() As List(Of TransferJuridicalDebtCollectionC)

    ''' <summary>
    ''' Confirma la transferencia cobro jurídico.
    ''' </summary>
    ''' <param name="JuridicalC">Objeto Transferencia Cobro Jurídico Cabecera</param>
    ''' <param name="IndigoSessionValues">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Function ConfirmTransferJuridicalDebt(JuridicalC As TransferJuridicalDebtCollectionC, IndigoSessionValues As SessionValues) As ActionResult

    ''' <summary>
    ''' Guarda una cabecera de tranferencia cobro jurídico.
    ''' </summary>
    ''' <param name="JuridicalC">Objeto Transferencia Cobro Jurídico</param>
    ''' <returns>ActionResult</returns>
    Function SaveTransferJuridicalDebtC(JuridicalC As TransferJuridicalDebtCollectionC, ByVal session As SessionValues) As ActionResult(Of TransferJuridicalDebtCollectionC)

    ''' <summary>
    ''' Reversa un traslado juridico
    ''' </summary>
    ''' <param name="idTransferJuridical"></param>
    ''' <param name="_IdUnitoperating"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    Function ReverseTransferJuridical(ByVal idTransferJuridical As Integer, ByVal _IdUnitoperating As Integer, ByVal IndigoSessionValues As SessionValues) As ActionResult(Of String)

    ''' <summary>
    ''' Elimina una cabecera de tranferencia cobro jurídico.
    ''' </summary>
    ''' <param name="JuridicalC">Objeto Transferencia Cobro Jurídico</param>
    ''' <returns>ActionResult</returns>
    Function DeleteTransferJuridicalDebtC(ByVal JuridicalC As TransferJuridicalDebtCollectionC, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="listInvoiceTransferDetailD"></param>
    ''' <param name="isConfirm"></param>
    ''' <param name="_unReconcileInvoice"></param>
    ''' <returns></returns>
    Function validateInvoice(ByVal listInvoiceTransferDetailD As List(Of String), ByVal isConfirm As Boolean, ByVal _unReconcileInvoice As Boolean) As ActionResult

End Interface
