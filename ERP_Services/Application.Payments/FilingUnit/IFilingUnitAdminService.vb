'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IFilingUnitAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza una unidad de radicacion
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveFilingUnit(ByVal FilingUnit As FilingUnit, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FilingUnit)

    ''' <summary>
    ''' Elimina una unidad de radicacion
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteFilingUnit(ByVal FilingUnit As FilingUnit, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una unidad de radicacion
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetFilingUnit(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of FilingUnit)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of FilingUnit)

    ''' <summary>
    ''' Obtiene una unidad de radicacion
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFilingUnitById(id As String, ByVal audit As AuditMessage) As ActionResult(Of FilingUnit)

    ''' <summary>
    ''' Obtiene las unidades operativas a las cuales tiene permiso un usuario, tambien carga las unidades funcionales padres
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFilingUnitByUser(userCode As String) As ActionResult(Of List(Of FilingUnit))
    ''' <summary>
    ''' Obtiene las unidades de radicacion que tiene permiso un usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFilingUnitByUserPermission(userCode As String) As ActionResult(Of List(Of FilingUnitUser))

End Interface
