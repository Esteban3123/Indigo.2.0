'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 30-05-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IObjectionsReceptionDAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista de todos los item del detalle de una objecion recepcionada
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">recibe el codigo de la objecion</param>
    ''' <returns>una lista de item del detalle de una objecion recepcionada</returns>
    Function ListAllObjectionsReceptionD(ByVal codeObjectionReceptionC As String) As List(Of GlosaObjectionsReceptionD)


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
    ''' obtiene el detalle de una recepcion
    ''' </summary>
    ''' <param name="InvoiceNumber">codigo de factura</param>
    ''' <param name="GlosaObjectionsReceptionCId">codigo de Cabecera</param>
    ''' <returns>un objecto detalle de oficio</returns>
    Function getObjectionReceptionD(ByVal InvoiceNumber As String, ByVal GlosaObjectionsReceptionCId As String) As GlosaObjectionsReceptionD


    ''' <summary>
    ''' elimina un item del detalle de una objecion 
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">el detalle de la objecion</param>
    ''' <param name="audit">mensaje auditoria</param>
    ''' <returns>valor de confirmacion del eliminado</returns>
    Function DeleteObjectionsReceptionD(ObjectionsReceptionD As Domain.Entities.GlosaObjectionsReceptionD, ByVal audit As AuditMessage, ByVal company As String) As Boolean


    ''' <summary>
    ''' obtiene una lista de detalles, cargando la cartera glosada, detalles quirugicos
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionDId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Function GetListObjectionReceptionD(GlosaObjectionsReceptionDId As String) As List(Of GlosaObjectionsReceptionD)


    ''' <summary>
    ''' Funcion Para Actualizar el estado de una factura
    ''' </summary>
    ''' <param name="ObjectionsReceptionDId">objeto detalle de oficio</param>
    ''' <param name="IndigoSessionValues">Valores de sesion</param>
    ''' <returns></returns>
    Function ConfirmObjectionReceptionD(ObjectionsReceptionDId As Integer, ByVal IdSecuense As Integer, ByVal Nit As String, ByVal RadicateConsecutive As String, ByVal valueGlosa As Decimal, IndigoSessionValues As SessionValues) As ActionResult

    ''' <summary>
    ''' Obtiene el detalle de una recepcion según un responsable
    ''' </summary>
    ''' <param name="CodeResponsible">Id del responsable</param>
    ''' <returns>Una lista de detalle de oficio</returns>
    Function ListObjectionReceptionDByResponsable(ByVal CodeResponsible As String, ByVal timeParameterAdmin As ITimeParametersAdminService, ByVal _IdIOperatingUnit As Integer) As List(Of GlosaObjectionsReceptionD)

    ''' <summary>
    ''' Funcion Para Actualizar un Objection receptionD
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">objeto detalle de oficio</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveObjectionReceptionD(ObjectionsReceptionD As GlosaObjectionsReceptionD, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Actionresult

    ''' <summary>
    ''' Funcion para eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteListObjectionReceptionD(ByVal tmpList As List(Of GlosaObjectionsReceptionD), audit As AuditMessage, company As String) As ActionResult

    ''' <summary>
    ''' Cargar salod de factura de dinamica
    ''' </summary>
    ''' <param name="ObjD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadBalanceInvoice(ByVal ObjD As GlosaObjectionsReceptionD, SessionValues As SessionValues) As Decimal

    ''' <summary>
    ''' Asignación de responsable de radicacion respuesta EAPB
    ''' </summary>
    ''' <param name="listObjetionReceptionDetail"></param>
    ''' <returns></returns>
    Function GlosaObjetionReceptionDetailAssignRadicateResponsible(listObjetionReceptionDetail As List(Of GlosaObjectionsReceptionD)) As ActionResult(Of List(Of GlosaObjectionsReceptionD))

    ''' <summary>
    ''' Radicación de la respuesta EAPB
    ''' </summary>
    ''' <param name="listObjetionReceptionDetail"></param>
    ''' <returns></returns>
    Function GlosaObjetionReceptionDetailRadicate(listObjetionReceptionDetail As List(Of GlosaObjectionsReceptionD)) As ActionResult(Of List(Of GlosaObjectionsReceptionD))

    ''' <summary>
    ''' 'Funcion para validar y crear moviminetos glosas apartir de la carga masiva de datos desde excel
    ''' </summary>
    ''' <param name="dtSet"></param>
    ''' <param name="SessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateExcelData(ByVal dtSet As DataSet, SessionValues As SessionValues) As ActionResult
End Interface
