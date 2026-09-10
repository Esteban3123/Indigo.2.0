'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 15-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del servicio de Traslado Cobro Jurídico Detalle. 
''' </summary>
''' <remarks></remarks>
Public Interface ITransferJuridicalDebtDAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Elimina un detalle de traslado cobro jurídico.
    ''' </summary>
    ''' <param name="JuridicalD">Objeto Traslado Cobro Juridico Detalle</param>
    ''' <returns>ActionResult</returns>
    Function DeleteJuridicalD(ByVal JuridicalD As List(Of TransferJuridicalDebtCollectionD), ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Guarda un detalle de traslado cobro jurídico.
    ''' </summary>
    ''' <param name="JuridicalD">Objeto Traslado Cobro Juridico Detalle</param>
    ''' <returns>ActionResult</returns>
    Function SaveJuridicalD(ByVal JuridicalD As List(Of TransferJuridicalDebtCollectionD), ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Lista todos los detalles de traslado cobro jurídico.
    ''' </summary>
    ''' <returns>Lista Traslado Cobro Juridico Detalle</returns>
    Function ListAllJuridicalD() As List(Of TransferJuridicalDebtCollectionD)
    ''' <summary>
    ''' Obtiene detalles de traslado cobro jurídico especificos.
    ''' </summary>
    ''' <param name="Id">El Id del traslado cobro jurídico cabecera</param>
    ''' <returns>Lista Traslado Cobro Juridico Detalle</returns>
    Function ListJuridicalDDByIdJuridicalC(ByVal Id As String) As List(Of TransferJuridicalDebtCollectionD)
    ''' <summary>
    ''' Obtiene un detalle de traslado cobro jurídico especifico.
    ''' </summary>
    ''' <param name="Id">El Id del traslado cobro jurídico detalle</param>
    ''' <returns>Objeto Traslado Cobro Juridico Detalle</returns>
    Function GetJuridicalDByIdJuridicalD(ByVal Id As String) As TransferJuridicalDebtCollectionD
    ''' <summary>
    ''' Función para validar y agregar facturas
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="_IdUnitoperating">Numero de Nit</param>
    ''' <returns>Lista de Conciliacion Detalle</returns>
    Function ValidateListInvoiceJuridical(ListInvoices As List(Of String), Nit As String, ByVal _IdUnitoperating As Integer) As List(Of TransferJuridicalDebtCollectionD)

End Interface
