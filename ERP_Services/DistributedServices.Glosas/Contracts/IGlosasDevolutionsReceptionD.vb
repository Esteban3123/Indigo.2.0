Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasDevolutionsReceptionD

#Region "DevolutionD"

       ''' <summary>
    ''' Elimina un detalle de devolución.
    ''' </summary>
    ''' <param name="DevolucionD">Objeto Devolución Detalle</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function DeleteDevolutionD(ByVal DevolucionD As List(Of GlosaDevolutionsReceptionD), ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Guarda un detalle de conciliación.
    ''' </summary>
    ''' <param name="DevolucionD">Objeto Devolución Detalle</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function SaveDevolutionD(ByVal DevolucionD As List(Of GlosaDevolutionsReceptionD), ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Lista todos los detalles de devoluciones.
    ''' </summary>
    ''' <returns>Lista Devolución Detalle</returns>
    <OperationContract>
    Function ListAllDevolutionD(ByVal session As SessionValues) As List(Of GlosaDevolutionsReceptionD)
    ''' <summary>
    ''' Obtiene detalles de devolución especificos.
    ''' </summary>
    ''' <param name="Id">El Id de la devolución cabecera</param>
    ''' <returns>Lista Devolución Detalle</returns>
    <OperationContract>
    Function ListDevolutionDByIdDevolutionnC(ByVal Id As String, ByVal session As SessionValues) As List(Of GlosaDevolutionsReceptionD)
    ''' <summary>
    ''' Obtiene un detalle de devolución especifico.
    ''' </summary>
    ''' <param name="Id">El Id de la devolución detalle</param>
    ''' <returns>Objeto Devolución Detalle</returns>
    <OperationContract>
    Function GetDevolutionDByIdDevolutionD(ByVal Id As String, ByVal session As SessionValues) As GlosaDevolutionsReceptionD


    ''' <summary>
    ''' Funcion para eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function DeleteListDevolution(ByVal tmpList As List(Of GlosaDevolutionsReceptionD), ByVal session As SessionValues) As ActionResult

    ''' <summary>
    ''' eliminar una devolucion
    ''' </summary>
    ''' <param name="GlosaDevolutionsReceptionD"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function DeleteDevolution(GlosaDevolutionsReceptionD As GlosaDevolutionsReceptionD, ByVal session As SessionValues) As ActionResult

    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    <OperationContract>
    Function ValidateListInvoiceDevolutionSp(ListInvoices As List(Of String), Nit As String, container As String, session As SessionValues) As ActionResult(Of List(Of GlosaDevolutionsReceptionD))

#End Region

End Interface
