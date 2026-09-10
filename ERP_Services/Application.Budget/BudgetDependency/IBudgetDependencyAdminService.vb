
'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBudgetDependencyAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una dependencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetDependency(code As String, validityId As Integer, audit As AuditMessage) As Dependency

    ''' <summary>
    ''' Obtiene una dependencia by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetDependencyByValidity(code As String, ValidityId As String, audit As AuditMessage) As Dependency

    ''' <summary>
    ''' Elimina una dependencia
    ''' </summary>
    ''' <param name="Dependency">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteBudgetDependency(Dependency As Dependency, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza una dependencia
    ''' </summary>
    ''' <param name="Dependency">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveBudgetDependency(Dependency As Dependency, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Dependency)
    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ChangeStateBudgetDependency(ByVal code As String, validityId As Integer, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Dependency)

    ''' <summary>
    ''' Obtiene una dependencia por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetDependencyById(id As Integer, audit As AuditMessage) As Dependency

End Interface
