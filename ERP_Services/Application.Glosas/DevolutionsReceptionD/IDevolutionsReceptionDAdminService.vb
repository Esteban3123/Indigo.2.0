'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 13-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del servicio de Devolución Detalle. 
''' </summary>
''' <remarks></remarks>
Public Interface IDevolutionsReceptionDAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Elimina un detalle de devolución.
    ''' </summary>
    ''' <param name="DevolucionD">Objeto Devolución Detalle</param>
    ''' <returns>ActionResult</returns>
    Function DeleteDevolutionD(ByVal DevolucionD As List(Of GlosaDevolutionsReceptionD), ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Guarda un detalle de conciliación.
    ''' </summary>
    ''' <param name="DevolucionD">Objeto Devolución Detalle</param>
    ''' <returns>ActionResult</returns>
    Function SaveDevolutionD(ByVal DevolucionD As List(Of GlosaDevolutionsReceptionD), ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Lista todos los detalles de devoluciones.
    ''' </summary>
    ''' <returns>Lista Devolución Detalle</returns>
    Function ListAllDevolutionD() As List(Of GlosaDevolutionsReceptionD)
    ''' <summary>
    ''' Obtiene detalles de devolución especificos.
    ''' </summary>
    ''' <param name="Id">El Id de la devolución cabecera</param>
    ''' <returns>Lista Devolución Detalle</returns>
    Function ListDevolutionDByIdDevolutionnC(ByVal Id As String) As List(Of GlosaDevolutionsReceptionD)
    ''' <summary>
    ''' Obtiene un detalle de devolución especifico.
    ''' </summary>
    ''' <param name="Id">El Id de la devolución detalle</param>
    ''' <returns>Objeto Devolución Detalle</returns>
    Function GetDevolutionDByIdDevolutionD(ByVal Id As String) As GlosaDevolutionsReceptionD


    ''' <summary>
    ''' Funcion para eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteListDevolution(ByVal tmpList As List(Of GlosaDevolutionsReceptionD), SessionValues As SessionValues) As ActionResult

    ''' <summary>
    ''' eliminar una devolucion
    ''' </summary>
    ''' <param name="GlosaDevolutionsReceptionD"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteDevolution(GlosaDevolutionsReceptionD As GlosaDevolutionsReceptionD, SessionValues As SessionValues) As ActionResult

End Interface
