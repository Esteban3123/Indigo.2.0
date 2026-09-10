'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 16-08-2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class BlockScheduleRepository

    Inherits GenericRepository(Of BlockSchedule)
    Implements IBlockScheduleRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todos los Bloqueos de las Unidades Funcionales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllBlockSchedule() As Tuple(Of BlockScheduleC, List(Of BlockSchedule)) Implements IBlockScheduleRepository.ListAllBlockSchedule

        Dim ListBlockSchedule = From e In _context.BlockSchedule
                                Select e

        Dim BlockScheduleC = From e In _context.BlockScheduleC
                             Select e

        Dim ListReturnBlockSchedule = ListBlockSchedule.ToList()
        Dim ObjBlockScheduleC = BlockScheduleC.FirstOrDefault()

        Dim ResultTuple As Tuple(Of BlockScheduleC, List(Of BlockSchedule)) = New Tuple(Of BlockScheduleC, List(Of BlockSchedule))(ObjBlockScheduleC, ListReturnBlockSchedule)

        Return ResultTuple
    End Function


End Class
