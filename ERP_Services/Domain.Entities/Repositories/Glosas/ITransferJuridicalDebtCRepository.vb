'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 15-10-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de Transferencia Cobro Jurídico Cabeceras.
''' </summary>
Public Interface ITransferJuridicalDebtCRepository
    Inherits IRepository(Of TransferJuridicalDebtCollectionC)

    ''' <summary>
    ''' Metodo para pegar o importar detalles en la rejilla
    ''' </summary>
    ''' <param name="XmlParameter"></param>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SP_CopyAndPasteTransferJuridicalDebtCollectionDetail(XmlParameter As String, XmlObject As String) As List(Of SP_CopyAndPasteTransferJuridicalDebtCollectionDetail_Result)

    Function ListTransferJuridicalDebtCollectionCMassiveConfirm(listDocuments As List(Of String)) As List(Of TransferJuridicalDebtCollectionC)

    ''' <summary>
    ''' Lista todas las cabeceras de cobro jurídico.
    ''' </summary>
    ''' <returns>Lista de objetos de transferencia cobro jurídico cabecera</returns>
    Function ListAllTransferJuridicalDebtC() As List(Of TransferJuridicalDebtCollectionC)
    ''' <summary>
    ''' Obtiene una cabecera de transferencia cobro jurídico especifica.
    ''' </summary>
    ''' <param name="Id">El Id de la transferencia cobro jurídico cabecera</param>
    ''' <returns>Objeto Transferencia Cobro Jurídico Cabecera</returns>
    Function GetTransferJuridicalDebtC(ByVal Id As String) As TransferJuridicalDebtCollectionC
    ''' <summary>
    ''' Obtiene una cabecera de transferencia cobro jurídico especifica.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo de la transferencia cobro jurídico cabecera</param>
    ''' <returns>Objeto Transferencia Cobro Jurídico Cabecera</returns>
    Function GetTransferJuridicalDebtCByConsecutive(ByVal Consecutive As String) As TransferJuridicalDebtCollectionC

    ''' <summary>
    ''' Metodo para guardar traslados a cobro jurídico
    ''' </summary>
    ''' <param name="transferJuridicalDebtCollectionXml"></param>
    ''' <param name="userCode"></param>
    ''' <param name="indigoGlossesIntegration"></param>
    ''' <param name="companyType"></param>
    ''' <returns></returns>
    Function SP_SaveTransferJuridicalDebtCollection(ByVal transferJuridicalDebtCollectionXml As String, ByVal userCode As String, ByVal indigoGlossesIntegration As Nullable(Of Byte), ByVal companyType As Nullable(Of Byte)) As SP_SaveTransferJuridicalDebtCollection_Result

End Interface
