'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 12-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del servicio de Devolución Cabecera. 
''' </summary>
''' <remarks></remarks>
Public Interface IDevolutionsReceptionCAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Funcion para listar todas las cabeceras de devoluciones.
    ''' </summary>
    ''' <returns>Lista de cabeceras devoluciones</returns>
    Function ListAllDevolutionC() As List(Of GlosaDevolutionsReceptionC)
    ''' <summary>
    ''' Elimina una cabecera de devolución.
    ''' </summary>
    ''' <param name="DevolutionC">Objeto Devolución</param>
    ''' <returns>ActionResult</returns>
    Function DeleteDevolutionC(ByVal DevolutionC As GlosaDevolutionsReceptionC, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Guarda una cabecera de devolución.
    ''' </summary>
    ''' <param name="DevolutionC">Objeto Devolución</param>
    ''' <returns>ActionResult</returns>
    Function SaveDevolutionC(ByVal DevolutionC As GlosaDevolutionsReceptionC, ByVal ListDevolutionD As List(Of GlosaDevolutionsReceptionD), ByVal audit As AuditMessage) As ActionResult(Of GlosaDevolutionsReceptionC)
    ''' <summary>
    ''' Obtiene una cabecera devolución especifica.
    ''' </summary>
    ''' <param name="Consecutive">Consecutive Devolución Cabecera</param>
    ''' <returns>Objeto Cabecera Devolución</returns>
    Function GetDevolutionCByConsecutive(ByVal Consecutive As String) As GlosaDevolutionsReceptionC
    ''' <summary>
    ''' Obtiene una cabecera devolución especifica.
    ''' </summary>
    ''' <param name="Id">Id Devolución Cabecera</param>
    ''' <returns>Objeto Cabecera Devolución</returns>
    Function GetDevolutionnC(ByVal Id As String) As GlosaDevolutionsReceptionC
    ''' <summary>
    ''' Confirma la Conciliación
    ''' </summary>
    ''' <param name="ConciliacionC">Objeto Devolución Cabecera</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Function ConfirmDevolutionC(ConciliacionC As GlosaDevolutionsReceptionC, audit As AuditMessage) As ActionResult

End Interface
