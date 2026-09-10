'************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Kevin Garay Rodriguez
' Created          : 07-04-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Consecutivos.
''' </summary>
Public Class ConsecutiveBudgetRepository
    Inherits GenericRepository(Of ConsecutiveBudget)
    Implements IConsecutiveBudgetRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función para obtener un consecutivo segun el codigo.
    ''' </summary>
    ''' <param name="ValidityId">el id de la vigencia</param>
    ''' <param name="FormId">Id del formulario</param>
    ''' <returns>Objeto Consecutivo</returns>
    Public Function GetConsecutiveByCode(ValidityId As String, FormId As String) As ConsecutiveBudget Implements IConsecutiveBudgetRepository.GetConsecutiveByCode
        Dim Consecutive = (From e In _context.ConsecutiveBudget
     Where e.ValidityId = ValidityId And e.FormId = FormId
     Select e)
        If Consecutive.Count > 0 Then
            Return Consecutive.Single
        Else
            Dim objeto As New ConsecutiveBudget
            Return objeto
        End If
    End Function

    ''' <summary>
    ''' Función para obtener todas los consecutivos.
    ''' </summary>
    ''' <returns>Lista de Consecutivos</returns>
    Public Function ListAllConsecutives() As List(Of ConsecutiveBudget) Implements IConsecutiveBudgetRepository.ListAllConsecutives
        Dim Busqueda = From e In _context.ConsecutiveBudget
                  Select e

        Return Busqueda.ToList
    End Function

End Class
