'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 15-10-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Detalles Transferencia Cobro Jurídico
''' </summary>
Public Class TransferJuridicalDebtDRepository
    Inherits GenericRepository(Of TransferJuridicalDebtCollectionD)
    Implements ITransferJuridicalDebtDRepository


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
    ''' <summary>
    ''' Obtiene una cabecera de transferencia cobro jurídico especifica.
    ''' </summary>
    ''' <param name="Id">El Id de la transferencia cobro jurídico cabecera</param>
    ''' <returns>Objeto Transferencia Cobro Jurídico Cabecera</returns>
    Public Function GetTransferJuridicalDebtD(Id As String, Optional tracking As Boolean = True) As TransferJuridicalDebtCollectionD Implements ITransferJuridicalDebtDRepository.GetTransferJuridicalDebtD
        If tracking Then
            Dim Juridical = From e In _context.TransferJuridicalDebtCollectionD
           Where e.Id = CInt(Id)
           Select e
            If Juridical.Count > 0 Then
                Return Juridical.Single
            End If
        Else
            Dim Juridical = (From e In _context.TransferJuridicalDebtCollectionD.AsNoTracking
                  Where e.Id = CInt(Id)
                  Select e).SingleOrDefault
            If Juridical IsNot Nothing Then
                Return Juridical
            End If
        End If
        Return New TransferJuridicalDebtCollectionD
    End Function

    ''' <summary>
    ''' Función que obtiene detalles de devolución segun código.
    ''' </summary>
    ''' <param name="Id">Id Devolución Cabecera</param>
    ''' <returns>Lista Devolución Cabecera</returns>
    Public Function ListTransferJuridicalDByIdTransferJuridicalC(Id As String, Optional OnlyEntity As Boolean = False) As List(Of TransferJuridicalDebtCollectionD) Implements ITransferJuridicalDebtDRepository.ListTransferJuridicalDByIdTransferJuridicalC
        Dim JuridicalD As List(Of TransferJuridicalDebtCollectionD)

        If OnlyEntity Then
            JuridicalD = (From e In _context.TransferJuridicalDebtCollectionD Where e.TransferJuridicalDebtCollectionCId = CInt(Id) Select e).ToList()
        Else
            JuridicalD = (From e In _context.TransferJuridicalDebtCollectionD.Include("GlosaPortfolioGlosada") Where e.TransferJuridicalDebtCollectionCId = CInt(Id) Select e).ToList()
        End If

        Return JuridicalD
    End Function

    ''' <summary>
    ''' Lista todas las cabeceras de cobro jurídico.
    ''' </summary>
    ''' <returns>Lista de objetos de transferencia cobro jurídico cabecera</returns>
    Public Function ListAllTransferJuridicalDebtD() As List(Of TransferJuridicalDebtCollectionD) Implements ITransferJuridicalDebtDRepository.ListAllTransferJuridicalDebtD
        Dim Busqueda = From e In _context.TransferJuridicalDebtCollectionD
                              Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Detalle de transferencia a cobreo juridico
    ''' </summary>
    ''' <param name="Invoicenumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTransferJuridicalDebtDByInvoice(Invoicenumber As String) As TransferJuridicalDebtCollectionD Implements ITransferJuridicalDebtDRepository.GetTransferJuridicalDebtDByInvoice
        DirectCast(_context, System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim Juridical = (From e In _context.TransferJuridicalDebtCollectionD.Include("TransferJuridicalDebtCollectionC").AsNoTracking
                         Where e.InvoiceNumber = Invoicenumber
                         Select e).FirstOrDefault()
        If Juridical IsNot Nothing Then
            Return Juridical
        Else
            Return New TransferJuridicalDebtCollectionD
        End If
    End Function
End Class


