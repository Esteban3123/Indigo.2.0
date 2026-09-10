'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IBudgetConceptRepository
    Inherits IRepository(Of Concept)

    ''' <summary>
    ''' Obtiene un concepto
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetConcept(code As String, validityId As Integer, Optional tracking As Boolean = True) As Concept

    ''' <summary>
    ''' Obtiene un concepto by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetConceptByValidity(code As String, ValidityId As String, Optional tracking As Boolean = True) As Concept

    ''' <summary>
    ''' Obtiene todas las dependencias para copiarlos y agregarlos a otra vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListConceptForCopyBase(validityId As Integer) As List(Of Concept)

    ''' <summary>
    ''' Obtiene la dependencia por codigo y la vigencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="validityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConceptByCodeAndValidityForCopyBase(code As String, validityId As Integer) As Concept

End Interface