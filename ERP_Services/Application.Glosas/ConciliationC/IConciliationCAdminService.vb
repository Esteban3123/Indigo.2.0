'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del servicio de Conciliación Cabecera. 
''' </summary>
''' <remarks></remarks>
Public Interface IConciliationCAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una cabecera conciliación especifica.
    ''' </summary>
    ''' <param name="Id">Id Conciliación Cabecera</param>
    ''' <returns>Objeto Cabecera Conciliación</returns>
    Function GetConciliationC(ByVal Id As String, audit As AuditMessage) As ConciliationC

    ''' <summary>
    ''' Obtiene una cabecera conciliación especifica.
    ''' </summary>
    ''' <param name="Consecutive">Consecutive Conciliación Cabecera</param>
    ''' <returns>Objeto Cabecera Conciliación</returns>
    Function GetConciliationCByConsecutive(ByVal Consecutive As String, audit As AuditMessage) As ConciliationC

    ''' <summary>
    ''' Guarda una cabecera de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionC">Objeto Conciliación</param>
    ''' <returns>ActionResult</returns>
    Function SaveConciliationC(ByVal ConciliacionC As ConciliationC, ByVal audit As AuditMessage) As ActionResult(Of ConciliationC)

    ''' <summary>
    ''' Elimina una cabecera de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionC">Objeto Conciliación</param>
    ''' <returns>ActionResult</returns>
    Function DeleteConciliationC(ByVal ConciliacionC As ConciliationC, ByVal audit As AuditMessage) As ActionResult

End Interface
