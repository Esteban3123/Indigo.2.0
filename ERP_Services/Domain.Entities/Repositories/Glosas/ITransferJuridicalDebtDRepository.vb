'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 15-10-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de Transferencia Cobro Jurídico Detalles.
''' </summary>
Public Interface ITransferJuridicalDebtDRepository
    Inherits IRepository(Of TransferJuridicalDebtCollectionD)

    ''' <summary>
    ''' Lista todos los detalles de cobro jurídico.
    ''' </summary>
    ''' <returns>Lista de objetos de transferencia cobro jurídico detalle</returns>
    Function ListAllTransferJuridicalDebtD() As List(Of TransferJuridicalDebtCollectionD)
    ''' <summary>
    ''' Obtiene un detalle de transferencia cobro jurídico especifica.
    ''' </summary>
    ''' <param name="Id">El Id de la transferencia cobro jurídico detalle</param>
    ''' <returns>Objeto Transferencia Cobro Jurídico Detalle</returns>
    Function GetTransferJuridicalDebtD(ByVal Id As String, Optional tracking As Boolean = True) As TransferJuridicalDebtCollectionD
    ''' <summary>
    ''' Obtiene detalles de transferencia cobro jurídico especificos.
    ''' </summary>
    ''' <param name="Id">El Id de la transferencia cobro jurídico cabecera</param>
    ''' <returns>Lista Transferencia Cobro Jurídico Detalle</returns>
    Function ListTransferJuridicalDByIdTransferJuridicalC(ByVal Id As String, Optional OnlyEntity As Boolean = False) As List(Of TransferJuridicalDebtCollectionD)

    ''' <summary>
    ''' Obtiene un detalle de transferencia cobro jurídico especifica.
    ''' </summary>
    ''' <param name="Invoicenumber">numero de factura transferencia cobro jurídico detalle</param>
    ''' <returns>Objeto Transferencia Cobro Jurídico Detalle</returns>
    Function GetTransferJuridicalDebtDByInvoice(ByVal Invoicenumber As String) As TransferJuridicalDebtCollectionD


End Interface
