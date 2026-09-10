Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasTransferJuridicalDebtD

    ''' <summary>
    ''' Elimina un detalle de traslado cobro jurídico.
    ''' </summary>
    ''' <param name="JuridicalD">Objeto Traslado Cobro Juridico Detalle</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function DeleteJuridicalD(ByVal JuridicalD As List(Of TransferJuridicalDebtCollectionD), ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Guarda un detalle de traslado cobro jurídico.
    ''' </summary>
    ''' <param name="JuridicalD">Objeto Traslado Cobro Juridico Detalle</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function SaveJuridicalD(ByVal JuridicalD As List(Of TransferJuridicalDebtCollectionD), ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Lista todos los detalles de traslado cobro jurídico.
    ''' </summary>
    ''' <returns>Lista Traslado Cobro Juridico Detalle</returns>
    <OperationContract>
    Function ListAllJuridicalD(ByVal session As SessionValues) As List(Of TransferJuridicalDebtCollectionD)
    ''' <summary>
    ''' Obtiene detalles de traslado cobro jurídico especificos.
    ''' </summary>
    ''' <param name="Id">El Id del traslado cobro jurídico cabecera</param>
    ''' <returns>Lista Traslado Cobro Juridico Detalle</returns>
    <OperationContract>
    Function ListJuridicalDDByIdJuridicalC(ByVal Id As String, ByVal session As SessionValues) As List(Of TransferJuridicalDebtCollectionD)
    ''' <summary>
    ''' Obtiene un detalle de traslado cobro jurídico especifico.
    ''' </summary>
    ''' <param name="Id">El Id del traslado cobro jurídico detalle</param>
    ''' <returns>Objeto Traslado Cobro Juridico Detalle</returns>
    <OperationContract>
    Function GetJuridicalDByIdJuridicalD(ByVal Id As String, ByVal session As SessionValues) As TransferJuridicalDebtCollectionD
    ''' <summary>
    ''' Función para validar y agregar facturas
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="session">Objeto session</param>
    ''' <param name="_IdUnitoperating">Unidad operativa</param>
    ''' <returns>Lista de Conciliacion Detalle</returns>
    <OperationContract>
    Function ValidateListInvoiceJuridical(ListInvoices As List(Of String), Nit As String, session As SessionValues, ByVal _IdUnitoperating As Integer) As List(Of TransferJuridicalDebtCollectionD)

End Interface
