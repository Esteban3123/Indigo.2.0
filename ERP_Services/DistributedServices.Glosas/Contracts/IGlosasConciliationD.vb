Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasConciliationD

#Region "ConciliationD"

    ''' <summary>
    ''' Elimina un detalle de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionD">Objeto Conciliación Detalle</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function DeleteConciliationD(ByVal ConciliacionD As List(Of ConciliationD), ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Guarda un detalle de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionD">Objeto Conciliación Detalle</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function SaveConciliationD(ByVal ConciliacionD As List(Of ConciliationD), ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Lista todos los detalles de conciliaciones.
    ''' </summary>
    ''' <returns>Lista de objetos de Conciliación Detalle</returns>
    <OperationContract>
    Function ListAllConciliationD(ByVal session As SessionValues) As List(Of ConciliationD)
    ''' <summary>
    ''' consulta un detalle de conciliacion especifico.
    ''' </summary>
    ''' <param name="code">El código de la conciliación cabecera</param>
    ''' <returns>Objeto Conciliación Detalle</returns>
    <OperationContract>
    Function ListConciliationDByIdConciliationC(ByVal code As String, ByVal session As SessionValues) As List(Of ConciliationD)
    ''' <summary>
    ''' consulta un detalle de conciliacion especifico.
    ''' </summary>
    ''' <param name="code">El código de la objeción detalle</param>
    ''' <returns>Objeto Conciliación Detalle</returns>
    <OperationContract>
    Function GetConciliationDByIdObjectionD(ByVal code As String, ByVal session As SessionValues) As ConciliationD

    ''' <summary>
    ''' Función que obtiene registros de facturas.
    ''' </summary>
    ''' <param name="Factura">Numero Factura</param>
    ''' <param name="Nit"></param>
    ''' <param name="session"></param>
    ''' <param name="listStatus"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ListInvoicesByNumber(Factura As String, Nit As String, ByVal session As SessionValues, ByVal listStatus As List(Of String)) As GlosaPortfolioGlosada
    ''' <summary>
    ''' obtiene una cartera glosada por Nit
    ''' </summary>
    ''' <param name="Nit">Nit de la objecion</param>
    ''' <returns>Objeto Cartera Glosa</returns>
    <OperationContract>
    Function ListConfirmGlosaPortfolio(Nit As String, ByVal session As SessionValues) As List(Of GlosaPortfolioGlosada)
    ''' <summary>
    ''' Guardar Objecion en el detalle de factura
    ''' </summary>
    ''' <param name="Movimientos">Objeto Movimiento</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Action Result</returns>
    <OperationContract>
    Function SaveConciliationInvoiceDetail(Movimientos As List(Of GlosaMovementGlosa), ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Confirmar Objecion en el detalle de factura
    ''' </summary>
    ''' <param name="Movimientos">Objeto Movimiento</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Action Result</returns>
    <OperationContract>
    Function ConfirmConciliationInvoiceDetail(Movimientos As List(Of GlosaMovementGlosa), ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Confirmar factura
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero Factura</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Action Result</returns>
    <OperationContract>
    Function ConfirmConciliationInvoice(conciliationId As Integer, InvoiceNumber As String, _IdUnitoperating As Integer, ByVal session As SessionValues) As ActionResult


#End Region

#Region "Subir Datos a Excel"
    ''' <summary>
    ''' 'Funcion para validar y subir conciliaciones desde excel
    ''' </summary>
    ''' <param name="dtSet"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ValidateExcelDataConciliation(ByVal dtSet As DataSet, ByVal ConciliationC As ConciliationC, ByVal Session As SessionValues) As ActionResult(Of ConciliationC)
#End Region

End Interface
