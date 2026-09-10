#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

''' <summary>
''' Repositorio Cabeceras Transferencia Cobro Jurídico
''' </summary>
Public Class TransferJuridicalDebtCRepository
    Inherits GenericRepository(Of TransferJuridicalDebtCollectionC)
    Implements ITransferJuridicalDebtCRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function SP_CopyPasteOrImportFileTransferJuridicalDebtCollection(XmlParameter As String, XmlObject As String) As List(Of SP_CopyAndPasteTransferJuridicalDebtCollectionDetail_Result) Implements ITransferJuridicalDebtCRepository.SP_CopyAndPasteTransferJuridicalDebtCollectionDetail
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteTransferJuridicalDebtCollectionDetail(XmlParameter, XmlObject).ToList
    End Function

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTransferJuridicalDebtCollectionCMassiveConfirm(listDocuments As List(Of String)) As List(Of TransferJuridicalDebtCollectionC) Implements ITransferJuridicalDebtCRepository.ListTransferJuridicalDebtCollectionCMassiveConfirm
        Return (From cr In _context.TransferJuridicalDebtCollectionC.Include("TransferJuridicalDebtCollectionD") Where listDocuments.Contains(cr.JuridicalTransferConsecutive) Select cr).ToList()
    End Function
    ''' <summary>
    ''' Obtiene una cabecera de transferencia cobro jurídico especifica.
    ''' </summary>
    ''' <param name="Id">El Id de la transferencia cobro jurídico cabecera</param>
    ''' <returns>Objeto Transferencia Cobro Jurídico Cabecera</returns>
    Public Function GetTransferJuridicalDebtC(Id As String) As TransferJuridicalDebtCollectionC Implements ITransferJuridicalDebtCRepository.GetTransferJuridicalDebtC
        Dim Juridical = From e In _context.TransferJuridicalDebtCollectionC.Include("Customer")
                        Where e.Id = CInt(Id)
                        Select e
        If Juridical.Count > 0 Then
            Dim JuridicalData = Juridical.SingleOrDefault
            JuridicalData.OriginalValue = (From e In _context.TransferJuridicalDebtCollectionC.AsNoTracking.Include("Customer").AsNoTracking
                                           Where e.Id = CInt(Id)
                                           Select e).SingleOrDefault
            Return JuridicalData
        End If

        Return New TransferJuridicalDebtCollectionC
    End Function

    ''' <summary>
    ''' Función que obtiene una cabecera de cobro jurídico segun consecutivo.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo Traslado Cobro Jurídico Cabecera</param>
    ''' <returns>Objeto Cabecera Traslado Cobro Jurídico</returns>
    Public Function GetTransferJuridicalDebtCByConsecutive(Consecutive As String) As TransferJuridicalDebtCollectionC Implements ITransferJuridicalDebtCRepository.GetTransferJuridicalDebtCByConsecutive
        Dim Juridical = From e In _context.TransferJuridicalDebtCollectionC.Include("Customer").Include("Lawyer").Include("DemandStatus")
                        Where e.JuridicalTransferConsecutive = Consecutive
                        Select e
        If Juridical.Count > 0 Then
            Dim JuridicalData = Juridical.SingleOrDefault
            JuridicalData.OriginalValue = (From e In _context.TransferJuridicalDebtCollectionC.AsNoTracking.Include("Customer").AsNoTracking.Include("Lawyer").AsNoTracking.Include("DemandStatus").AsNoTracking
                                           Where e.JuridicalTransferConsecutive = Consecutive
                                           Select e).SingleOrDefault
            If JuridicalData.Lawyer IsNot Nothing Then
                JuridicalData.Lawyer.ThirdParty = (From t In _context.ThirdParty Where t.Id = JuridicalData.Lawyer.ThirdPartyId).FirstOrDefault
            End If
            Return JuridicalData
        Else
            Return New TransferJuridicalDebtCollectionC
        End If
    End Function

    ''' <summary>
    ''' Lista todas las cabeceras de cobro jurídico.
    ''' </summary>
    ''' <returns>Lista de objetos de transferencia cobro jurídico cabecera</returns>
    Public Function ListAllTransferJuridicalDebtC() As List(Of TransferJuridicalDebtCollectionC) Implements ITransferJuridicalDebtCRepository.ListAllTransferJuridicalDebtC
        Dim Busqueda = From e In _context.TransferJuridicalDebtCollectionC.Include("Customer")
                       Select e

        Return Busqueda.ToList
    End Function

    Private Function SP_SaveTransferJuridicalDebtCollection(ByVal transferJuridicalDebtCollectionXml As String, ByVal userCode As String, ByVal indigoGlossesIntegration As Nullable(Of Byte), ByVal companyType As Nullable(Of Byte)) As SP_SaveTransferJuridicalDebtCollection_Result Implements ITransferJuridicalDebtCRepository.SP_SaveTransferJuridicalDebtCollection
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveTransferJuridicalDebtCollection(transferJuridicalDebtCollectionXml, userCode, indigoGlossesIntegration, companyType).SingleOrDefault()
    End Function

End Class

