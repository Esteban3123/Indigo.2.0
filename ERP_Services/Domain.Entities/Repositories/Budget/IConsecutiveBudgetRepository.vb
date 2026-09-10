'************************************************************
' Assembly         : Domain.Budget
' Author           : Kevin Garay Rodriguez
' Created          : 07-04-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Interfaz del repositorio Consecutivos.
''' </summary>
Public Interface IConsecutiveBudgetRepository
    Inherits IRepository(Of ConsecutiveBudget)

    ''' <summary>
    ''' Función que obtiene una lista de consecutivos.
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllConsecutives() As List(Of ConsecutiveBudget)
    ''' <summary>
    ''' Consulta un consecutivo especifico segun código.
    ''' </summary>
    ''' <param name="ValidityId">el id de la vigencia</param>
    ''' <param name="FormId">Id del formulario</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetConsecutiveByCode(ValidityId As String, FormId As String) As ConsecutiveBudget


End Interface
