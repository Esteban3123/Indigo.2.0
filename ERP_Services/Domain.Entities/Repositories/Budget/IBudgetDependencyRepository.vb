'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IBudgetDependencyRepository
    Inherits IRepository(Of Dependency)

    ''' <summary>
    ''' Obtiene una dependencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetDependency(code As String, validityId As Integer, Optional tracking As Boolean = True) As Dependency

    ''' <summary>
    ''' Obtiene una dependencia by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetDependencyByValidity(code As String, ValidityId As String, Optional tracking As Boolean = True) As Dependency

    ''' <summary>
    ''' Obtiene una dependencia por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetDependencyById(id As Integer, Optional tracking As Boolean = True) As Dependency

    ''' <summary>
    ''' Obtiene todas las dependencias para copiarlos y agregarlos a otra vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListDependencyForCopyBase(validityId As Integer) As List(Of Dependency)

    ''' <summary>
    ''' Obtiene la dependencia por codigo y la vigencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="validityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDependencyByCodeAndValidityForCopyBase(code As String, validityId As Integer) As Dependency

End Interface