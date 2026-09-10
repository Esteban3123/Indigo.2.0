'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 19-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IFunctionalUnitAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos las unidades funcionales
    ''' </summary>
    ''' <returns>Lista las unidades funcionales</returns>
    ''' <remarks></remarks>
    Function ListAllFunctionalUnit() As List(Of FunctionalUnit)

    ''' <summary>
    ''' Obtiene una unidad funcional
    ''' </summary>
    ''' <param name="code">Codigo de la unidad funcional</param>
    ''' <returns>Unidad funcional</returns>
    ''' <remarks></remarks>
    Function GetFunctionalUnit(ByVal code As String) As FunctionalUnit

    ''' <summary>
    ''' Obtiene una unidad funcional por id
    ''' </summary>
    ''' <param name="id">Id de la unidad funcional</param>
    ''' <returns>Unidad funcional</returns>
    ''' <remarks></remarks>
    Function GetFunctionalUnitById(ByVal id As String) As FunctionalUnit

    ''' <summary>
    ''' Graba o actualiza una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnit">unidad funcional a guardar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function SaveFunctionalUnit(ByVal functionalUnit As FunctionalUnit, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FunctionalUnit)

    Function UpdateStateFunctionalUnit(code As String, state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of FunctionalUnit)

    ''' <summary>
    ''' Elimina una unidad funcional
    ''' </summary>
    ''' <param name="FunctionalUnit">Unidad Funcional</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function DeleteFunctionalUnit(ByVal FunctionalUnit As FunctionalUnit, ByVal audit As AuditMessage) As ActionMessageResult(Of FunctionalUnit)

End Interface
