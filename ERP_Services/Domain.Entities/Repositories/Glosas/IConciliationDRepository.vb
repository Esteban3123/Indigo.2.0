'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-06-28
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de Conciliación Detalles.
''' </summary>
Public Interface IConciliationDRepository
    Inherits IRepository(Of ConciliationD)

    ''' <summary>
    ''' Lista todos los detalles de conciliaciones.
    ''' </summary>
    ''' <returns>Lista Conciliación Detalle</returns>
    Function ListAllConciliationD() As List(Of ConciliationD)
    ''' <summary>
    ''' Obtiene detalles de conciliacion especificos.
    ''' </summary>
    ''' <param name="Id">El Id de la conciliación cabecera</param>
    ''' <returns>Objeto Conciliación Detalle</returns>
    Function ListConciliationDByIdConciliationC(ByVal Id As String) As List(Of ConciliationD)
    ''' <summary>
    ''' Obtiene un detalle de conciliacion especifico.
    ''' </summary>
    ''' <param name="Id">El Id de la objeción detalle</param>
    ''' <returns>Objeto Conciliación Detalle</returns>
    Function GetConciliationDByIdObjectionD(ByVal Id As String) As ConciliationD

    ''' <summary>
    ''' Funcion Que Retorna la Cantidad de Conciliacion Detalle Por el Numero de Cartera Glosada
    ''' </summary>
    ''' <param name="GlosaPortfolioId">Codigo de cartera Glosada</param>
    ''' <returns>Numero de Conciliacion detalle</returns>
    ''' <remarks></remarks>
    Function CountConciliationD(GlosaPortfolioId As String) As Integer
    ''' <summary>
    ''' Función que obtiene un detalle de conciliación segun numero factura.
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero factura</param>
    ''' <returns>Objeto Cabecera Conciliacion</returns>
    Function GetConciliationDByInvoiceNumber(ConciliationId As Integer, InvoiceNumber As String) As ConciliationD

    ''' <summary>
    ''' Funcion Que Retorna un objeto de conciliacion detalle personalizado
    ''' </summary>
    ''' <returns>Objeto</returns>
    ''' <remarks></remarks>
    Function getConciliationDParametersTime(InvoiceNumber As String) As TrazabilityParametersTime

    ''' <summary>
    ''' Funcion Que Retorna un objeto de conciliacion detalle personalizado
    ''' </summary>
    ''' <returns>Objeto</returns>
    ''' <remarks></remarks>
    Function getListConciliationDParametersTime(InvoiceNumbers As String()) As List(Of TrazabilityParametersTime)

End Interface
