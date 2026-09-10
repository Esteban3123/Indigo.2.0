'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IRetentionRepository

    Inherits IRepository(Of Retention)

    ''' <summary>
    ''' Obtiene Todas las Retenciones
    ''' </summary>
    ''' <returns>Retenciones</returns>
    ''' <remarks></remarks>
    Function ListAllRetention() As List(Of Retention)

    ''' <summary>
    ''' Obtiene una Retención
    ''' </summary>
    ''' <param name="code">Código de la Retención</param>
    ''' <returns>Retención</returns>
    ''' <remarks></remarks>
    Function GetRetention(ByVal code As String, Optional tracking As Boolean = True) As Retention

    ''' <summary>
    ''' Obtiene una Retención por Año
    ''' </summary>
    ''' <param name="year">Año</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRetentionByYear(ByVal year As String) As List(Of Retention)

    Function GetListRetentionRangeByRetentionConceptId(RetentionConceptId As Integer) As List(Of RetentionConceptRanges)
    Function GetListRetentionConceptById(RetentionConceptId As Integer) As RetentionConcepts

    Function GetListRetentionRangeRetentionConceptIdByNumber(ByVal Number As String) As List(Of RetentionConceptRanges)

End Interface
