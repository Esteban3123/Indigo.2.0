Imports Domain.Entities
Imports Domain.Base
Imports System.Data

Public Interface IObjectionsReceptionDRepository
    Inherits IRepository(Of GlosaObjectionsReceptionD)

    ''' <summary>
    ''' Lista de todos los item del detalle de una objecion recepcionada
    ''' </summary>
    ''' <param name="objectionReceptionId">recibe el id de la recepción de objecion</param>
    ''' <returns>una lista de item del detalle de una objecion recepcionada</returns>
    Function ListAllObjectionsReceptionDWithIncludes(ByVal objectionReceptionId As Integer) As List(Of GlosaObjectionsReceptionD)

    ''' <summary>
    ''' Lista de todos los item del detalle de una objecion recepcionada
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">recibe el codigo de la objecion</param>
    ''' <returns>una lista de item del detalle de una objecion recepcionada</returns>
    Function ListAllObjectionsReceptionD(ByVal codeObjectionReceptionC As String) As List(Of GlosaObjectionsReceptionD)

    ''' <summary>
    ''' obtiene el detalle de una recepcion
    ''' </summary>
    ''' <param name="InvoiceNumber">codigo de factura</param>
    ''' <param name="GlosaObjectionsReceptionCId">codigo de Cabecera</param>
    ''' <returns>un objecto detalle de oficio</returns>
    Function getObjectionReceptionD(ByVal InvoiceNumber As String, ByVal GlosaObjectionsReceptionCId As String) As GlosaObjectionsReceptionD

    ''' <summary>
    ''' Retorna un Objeto factura
    ''' </summary>
    ''' <param name="Code">Codigo ID</param>
    ''' <param name="tracking">Bandera de Tracking</param>
    ''' <returns>Objeto Factura</returns>
    Function getObjectionReceptionDById(ByVal Code As String, Optional tracking As Boolean = True) As GlosaObjectionsReceptionD

    ''' <summary>
    ''' obtiene una lista de detalles confirmados de una recepcion
    ''' </summary>
    ''' <param name="Nit">Nit de la objecion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Function ListConfirmObjectionReceptionD(Nit As String) As List(Of GlosaObjectionsReceptionD)
    ''' <summary>
    ''' obtiene una lista de detalles de una recepcion
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionCId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Function ListObjectionReceptionD(GlosaObjectionsReceptionCId As String) As List(Of GlosaObjectionsReceptionD)
    ''' <summary>
    ''' obtiene una lista de detalles, cargando la cartera glosada, detalles quirugicos
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionDId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Function GetListObjectionReceptionD(GlosaObjectionsReceptionDId As String) As List(Of GlosaObjectionsReceptionD)
    ''' <summary>
    ''' Obtiene una lista de detalles confirmados de una recepcion segun un responsable
    ''' </summary>
    ''' <param name="CodeResponsible">Id Responsable</param>
    ''' <returns>una lista detalle de oficio</returns>
    Function ListObjectionReceptionDByResponsable(CodeResponsible As String) As List(Of GlosaObjectionsReceptionD)
    ''' <summary>
    ''' Obtiene un objection D personalizado
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function getObjectionDParametersTime(InvoiceNumber As String, type As String) As TrazabilityParametersTime
    ''' <summary>
    ''' Obtiene un objection D personalizado
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function getListObjectionDParametersTime(InvoiceNumbers As String(), type As String) As List(Of TrazabilityParametersTime)

    ''' <summary>
    ''' Retorna un Objeto factura
    ''' </summary>
    ''' <param name="Id">Codigo ID</param>
    ''' <returns>Objeto Factura</returns>
    Function getObjectionReceptionDByIdWithoutObjC(Id As String) As GlosaObjectionsReceptionD

    ''' <summary>
    ''' Objeto factura con agregado de parametros contable
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetObjDWithInterface(InvoiceNumber As String) As GlosaObjectionsReceptionD

End Interface
