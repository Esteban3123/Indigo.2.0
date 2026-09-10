'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Rafael Eduardo Patiño
' Created          : 20-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IGlosasObjectionsReceptionD

#Region "ObjectionsReceptionC"


    ''' <summary>
    ''' obtiene el detalle de una recepcion
    ''' </summary>
    ''' <param name="InvoiceNumber">codigo de factura</param>
    ''' <param name="GlosaObjectionsReceptionCId">codigo de Cabecera</param>
    ''' <returns>un objecto detalle de oficio</returns>
    <OperationContract()>
    Function getObjectionReceptionD(ByVal InvoiceNumber As String, ByVal GlosaObjectionsReceptionCId As String, ByVal session As SessionValues) As GlosaObjectionsReceptionD

    ''' <summary>
    ''' lista el detalle duna objecion mediante el codigo consececutivo de la cabecera
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">codigo de la cabecera de la objecion</param>
    ''' <returns>lista detalle de una objecion</returns>
    <OperationContract()>
    Function ListAllObjectionsReceptionD(ByVal codeObjectionReceptionC As String, ByVal session As SessionValues) As List(Of GlosaObjectionsReceptionD)

    ''' <summary>
    ''' elimina un item del detalle de una objecion 
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">el detalle de la objecion</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>valor de confirmacion del eliminado</returns>
    <OperationContract()>
    Function DeleteObjectionsReceptionD(ByVal ObjectionsReceptionD As Domain.Entities.GlosaObjectionsReceptionD, ByVal session As SessionValues) As Boolean

    ''' <summary>
    ''' Funcion para eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteListObjectionReceptionD(ByVal tmpList As List(Of GlosaObjectionsReceptionD), ByVal session As SessionValues) As ActionResult


    ''' <summary>
    ''' obtiene una lista de detalles de una recepcion
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionCId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    <OperationContract()>
    Function ListObjectionReceptionD(ByVal GlosaObjectionsReceptionCId As String, ByVal session As SessionValues) As List(Of GlosaObjectionsReceptionD)

    ''' <summary>
    ''' obtiene una lista de detalles confirmados de una recepcion
    ''' </summary>
    ''' <param name="Nit">Nit de la objeción</param>
    ''' <returns>una lista detalle de oficio</returns>
    <OperationContract()>
    Function ListConfirmObjectionReceptionD(ByVal Nit As String, ByVal session As SessionValues) As List(Of GlosaObjectionsReceptionD)


    ''' <summary>
    ''' obtiene una lista de detalles, cargando la cartera glosada, detalles quirugicos
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionDId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    <OperationContract()>
    Function GetListObjectionReceptionD(ByVal GlosaObjectionsReceptionDId As String, ByVal session As SessionValues) As List(Of GlosaObjectionsReceptionD)

    ''' <summary>
    ''' Funcion Para Actualizar el estado de una factura
    ''' </summary>
    ''' <param name="ObjectionsReceptionDId"></param>
    ''' <param name="IdSecuense"></param>
    ''' <param name="Nit"></param>
    ''' <param name="RadicateConsecutive"></param>
    ''' <param name="valueGlosa"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmObjectionReceptionD(ObjectionsReceptionDId As Integer, IdSecuense As Integer, ByVal Nit As String, ByVal RadicateConsecutive As String, ByVal valueGlosa As Decimal, session As SessionValues) As actionresult

    ''' <summary>
    ''' Obtiene una lista de detalles confirmados de una recepcion segun un responsable
    ''' </summary>
    ''' <param name="CodeResponsible">Id Responsable</param>
    ''' <returns>una lista detalle de oficio</returns>
    <OperationContract()>
    Function ListObjectionReceptionDByResponsable(ByVal CodeResponsible As String, ByVal session As SessionValues, ByVal _IdIOperatingUnit As Integer) As List(Of GlosaObjectionsReceptionD)


    ''' <summary>
    ''' Funcion Para Actualizar un Objection receptionD
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">objeto detalle de oficio</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveObjectionReceptionD(ByVal ObjectionsReceptionD As GlosaObjectionsReceptionD, ByVal session As SessionValues) As Actionresult

    ''' <summary>
    ''' Cargar salod de factura de dinamica
    ''' </summary>
    ''' <param name="ObjD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function LoadBalanceInvoice(ByVal ObjD As GlosaObjectionsReceptionD, session As SessionValues) As Decimal

    ''' <summary>
    ''' Asignación de responsable de radicacion respuesta EAPB
    ''' </summary>
    ''' <param name="listObjetionReceptionDetail"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GlosaObjetionReceptionDetailAssignRadicateResponsible(listObjetionReceptionDetail As List(Of GlosaObjectionsReceptionD), session As SessionValues) As ActionResult(Of List(Of GlosaObjectionsReceptionD))

    ''' <summary>
    ''' Radicación de la respuesta EAPB
    ''' </summary>
    ''' <param name="listObjetionReceptionDetail"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GlosaObjetionReceptionDetailRadicate(listObjetionReceptionDetail As List(Of GlosaObjectionsReceptionD), session As SessionValues) As ActionResult(Of List(Of GlosaObjectionsReceptionD))

#End Region

#Region "import data excel"
    ''' <summary>
    ''' 'Funcion para validar y crear moviminetos glosas apartir de la carga masiva de datos desde excel
    ''' </summary>
    ''' <param name="dtSet"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ValidateExcelData(ByVal dtSet As DataSet, ByVal session As SessionValues) As ActionResult

#End Region
End Interface
