'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IMonthlyAmortizationRepository
    Inherits IRepository(Of DeferredCausation)

    ''' <summary>
    ''' Obtiene un listado de causaciones diferidas dependiendo de la fecha escogida
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeferredCausationByDate(year As Integer, month As Integer, Optional tracking As Boolean = True) As List(Of DeferredCausationShare)

    ''' <summary>
    ''' Obtiene una causacion diferida por id
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeferredCausationById(id As String, Optional tracking As Boolean = True) As DeferredCausation

    ''' <summary>
    ''' Genera el ajuste diferencial por amortizacion mensual de diferidos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function GenerateDifferentialAdjustment(data As DataRevaluation, userCode As String) As List(Of SPResultModelDiffAdjustment)

End Interface
