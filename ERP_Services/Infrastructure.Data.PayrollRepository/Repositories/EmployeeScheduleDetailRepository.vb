'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-11-2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports System.Linq.Expressions

Public Class EmployeeScheduleDetailRepository

    Inherits GenericRepository(Of EmployeeScheduleDetail)
    Implements IEmployeeScheduleDetailRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub


    ''' <summary>
    ''' Obtiene un nivel de estudio especifico
    ''' </summary>
    ''' <returns>Nivel de estudio</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeScheduleDetail(ByVal Id As Integer) As EmployeeScheduleDetail Implements IEmployeeScheduleDetailRepository.GetEmployeeScheduleDetail
        Dim EmployeeScheduleDetail = From e In _context.EmployeeScheduleDetail
                                     Where e.Id = Id
                                     Select e
        If (EmployeeScheduleDetail.Count > 0) Then
            Return EmployeeScheduleDetail.FirstOrDefault()

        Else
            Return New EmployeeScheduleDetail()
        End If
    End Function

End Class
