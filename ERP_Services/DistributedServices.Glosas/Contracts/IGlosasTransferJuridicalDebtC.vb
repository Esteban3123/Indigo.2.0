#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()> _
Public Interface IGlosasTransferJuridicalDebtC

    ''' <summary>
    ''' Metodo para pegar o importar detalles en la rejilla
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="customerId"></param>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function CopyAndPasteTransferJuridicalDebtCollectionDetail(ByVal operatingUnitId As Integer, customerId As Integer, transferJuridicalDebtCollectionCId As Integer, copyType As Integer, dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), session As SessionValues) As ActionResult(Of List(Of TransferJuridicalDebtCollectionD))

    ''' <summary>
    ''' Obtiene una cabecera transferencia cobro jurídico especifica.
    ''' </summary>
    ''' <param name="Id">Id Transferencia Cobro Juríco Cabecera</param>
    ''' <returns>Objeto Cabecera Transferencia Cobro Jurídico</returns>
    <OperationContract()>
    Function GetTransferJuridicalDebtC(ByVal Id As String, session As SessionValues) As TransferJuridicalDebtCollectionC

    ''' <summary>
    ''' Obtiene una cabecera transferencia cobro jurídico especifica.
    ''' </summary>
    ''' <param name="Consecutive">Consecutive Transferencia Cobro Jurídico Cabecera</param>
    ''' <returns>Objeto Cabecera Transferencia Cobro Jurídico</returns>
    <OperationContract()>
    Function GetTransferJuridicalDebtCByConsecutive(ByVal Consecutive As String, session As SessionValues) As TransferJuridicalDebtCollectionC

    ''' <summary>
    ''' Funcion para listar todas las cabeceras de transferencias cobro jurídico.
    ''' </summary>
    ''' <returns>Lista de cabeceras transferencias cobro jurídico</returns>
    <OperationContract()>
    Function ListAllTransferJuridicalDebtC(session As SessionValues) As List(Of TransferJuridicalDebtCollectionC)

    ''' <summary>
    ''' Guarda una cabecera de tranferencia cobro jurídico.
    ''' </summary>
    ''' <param name="JuridicalC">Objeto Transferencia Cobro Jurídico</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function SaveTransferJuridicalDebtC(JuridicalC As TransferJuridicalDebtCollectionC, ByVal session As SessionValues) As ActionResult(Of TransferJuridicalDebtCollectionC)

    ''' <summary>
    ''' Confirma la transferencia cobro jurídico.
    ''' </summary>
    ''' <param name="JuridicalC">Objeto Transferencia Cobro Jurídico Cabecera</param>
    ''' <param name="session">Objeto Session</param>
    ''' <returns>Action Result</returns>
    <OperationContract()>
    Function ConfirmTransferJuridicalDebt(JuridicalC As TransferJuridicalDebtCollectionC, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Reversa un traslado juridico
    ''' </summary>
    ''' <param name="idTransferJuridical"></param>
    ''' <param name="_IdUnitoperating"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ReverseTransferJuridical(ByVal idTransferJuridical As Integer, ByVal _IdUnitoperating As Integer, ByVal IndigoSessionValues As SessionValues) As ActionResult(Of String)

    ''' <summary>
    ''' Elimina una cabecera de tranferencia cobro jurídico.
    ''' </summary>
    ''' <param name="JuridicalC">Objeto Transferencia Cobro Jurídico</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function DeleteTransferJuridicalDebtC(ByVal JuridicalC As TransferJuridicalDebtCollectionC, ByVal session As SessionValues) As ActionResult

End Interface
