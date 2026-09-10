'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServiceBudgetDependency
    ''' <summary>
    ''' Obtiene una dependencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBudgetDependency(code As String, validityId As Integer, audit As AuditMessage) As Dependency

    ''' <summary>
    ''' Obtiene una dependencia by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBudgetDependencyByValidity(code As String, ValidityId As String, audit As AuditMessage) As Dependency

    ''' <summary>
    ''' Elimina una dependencia
    ''' </summary>
    ''' <param name="Dependency">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    <OperationContract()>
    Function DeleteBudgetDependency(Dependency As Dependency, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza una dependencia
    ''' </summary>
    ''' <param name="Dependency">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveBudgetDependency(Dependency As Dependency, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Dependency)
    ''' <summary>
    ''' cambia el estado de la entidad.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateBudgetDependency(code As String, validityId As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Dependency)

    ''' <summary>
    ''' Obtiene una dependencia por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBudgetDependencyById(id As Integer, audit As AuditMessage) As Dependency

End Interface

