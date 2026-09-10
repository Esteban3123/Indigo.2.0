'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class PositionRepository
    Inherits GenericRepository(Of Position)
    Implements IPositionRepository

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
    ''' Obtiene un cargo especifico
    ''' </summary>
    ''' <param name="code">Codigo del cargo</param>
    ''' <returns>Cargo</returns>
    ''' <remarks></remarks>
    Public Function GetPosition(code As String, Optional tracking As Boolean = True) As Position Implements IPositionRepository.GetPosition
        Dim position = From e In _context.Position
                       Where e.Code = code
                       Select e
        If position.Count > 0 Then
            Dim ObjPosition = Nothing
            If tracking = False Then
                ObjPosition = (From e In _context.Position.AsNoTracking
                               Where e.Code = code
                               Select e).SingleOrDefault
            Else
                ObjPosition = position.SingleOrDefault
            End If
            Return ObjPosition
        End If
        Return New Position()
    End Function

    ''' <summary>
    ''' Lista todos los cargos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPosition() As List(Of Position) Implements IPositionRepository.ListAllPosition
        Dim position = From e In _context.Position
                       Select e
        Return position.ToList()
    End Function

    ''' <summary>
    ''' Lista los cargos por Id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPositionById(IdPosition As Integer) As Position Implements IPositionRepository.GetPositionById
        Dim position = From e In _context.Position
                       Select e
        Return position.FirstOrDefault()
    End Function
End Class
