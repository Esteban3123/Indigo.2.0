'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 09-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base
Imports  Domain.Entities

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
Public Interface IObjectionsReception
    Inherits IcrudBase


    ''' <summary>
    ''' Propiedad que tiene el nombre del contenedor
    ''' </summary>
    Property NameContainer As String
    ''' <summary>
    ''' Factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceNumber As String
    ''' <summary>
    ''' Propiedad que tiene el objeto completo de eliminados del Detalle
    ''' </summary>
    Property DeleteObjectionsReceptionD As List(Of GlosaObjectionsReceptionD)
    ''' <summary>
    ''' Propiedad que tiene el objeto completo
    ''' </summary>
    Property ObjectionsReception As Object
    ''' <summary>
    ''' Propiedad que contiene el listado de empresa de parametros interfaces
    ''' </summary>
    Property DataSourceBranch As List(Of GlosasParametersInterface)
    ''' <summary>
    ''' Propiedad que contiene el listado de Facturas
    ''' </summary>
    Property DataSourceInvoices As List(Of SP_invoiceList_Result)
    ''' <summary>
    ''' Propiedad que contiene el listado de Facturas Objetadas
    ''' </summary>
    Property DataSourceObjectsInvoices As List(Of GlosaObjectionsReceptionD)
    ''' <summary>
    ''' propiedad que contiene el estado del Tercero
    ''' </summary>
    Property StatusDocument As String
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Propiedad que contiene el id del tercero
    ''' </summary>
    Property IdCustomer As String
    ''' <summary>
    ''' Propiedad que contiene la fecha del radicado
    ''' </summary>
    Property RadicatedDate As Date
    ''' <summary>
    ''' Propiedad que contiene la fecha de oficio
    ''' </summary>
    Property DocumentDate As Date
    ''' <summary>
    ''' Propiedad que contiene el nuero de oficio
    ''' </summary>
    Property DocumentNumber As String
    ''' <summary>
    ''' Propiedad que contiene el comentario del oficio
    ''' </summary>
    Property Comment As String
    ''' <summary>
    ''' Propiedad que contiene el tipo del documento
    ''' </summary>
    Property DocumentType As String
    ''' <summary>
    ''' Funcion para eliminar item del detalle del oficio
    ''' </summary>
    ''' <remarks></remarks>
    Sub DeleteItemDetail(ByVal _ObjectionsReceptionD As GlosaObjectionsReceptionD)
    ''' <summary>
    ''' Propiedad para la activacion de un registro
    ''' </summary>
    ''' <param name="_InvoiceNumber">numero factura</param>
    ''' <value></value>
    WriteOnly Property ActiveRecord(ByVal _InvoiceNumber As String, ByVal _ObjD As GlosaObjectionsReceptionD) As String
    ''' <summary>
    ''' Estado
    ''' </summary>
    ''' <param name="Stade"></param>
    ''' <param name="Row"></param>
    ''' <remarks></remarks>
    Sub ChangeStade(ByVal Stade As Stades, ByVal Row As Integer)
    ''' <summary>
    ''' propiedad para el control de la persistencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CompletedPersist As Boolean
    ''' <summary>
    ''' Porpiedad para asignar el numero de consecutivo cuando guarda
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property Consecutive() As String
    ''' <summary>
    ''' Asigna la secuencia numerica de cartera para la realizacion de documentos de reclasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Sequense As Domain.Entities.PortfolioSequence

End Interface

Public Enum Stades
    Procesando
    Falla
    Cofirmado
    SinCofirmar
    FacturaSinRadicar
    FacturaConfirmada
    FacturaSinConfirmar
    FacturaGlosada
    FacturaAnulada
    Guardado
    SinGuardar
End Enum
