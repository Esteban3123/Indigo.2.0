'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo 
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class WorkCenterRepository

    Inherits GenericRepository(Of WorkCenter)
    Implements IWorkCenterRepository

    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un Centro de Trabajo determinado
    ''' </summary>
    ''' <param name="code">Codigo del Centro de Trabajo</param>
    ''' <returns>Centro de Trabajo</returns>
    Public Function GetWorkCenter(code As String, Optional tracking As Boolean = True) As WorkCenter Implements IWorkCenterRepository.GetWorkCenter
        Dim workCenter = From e In _context.WorkCenter
                     Where e.Code = code
                     Select e
        If workCenter.Count > 0 Then
            Dim objWorkCenter = Nothing
            If tracking = False Then
                objWorkCenter = (From e In _context.WorkCenter.AsNoTracking
                                 Where e.Code = code
                                 Select e).SingleOrDefault
            Else
                objWorkCenter = workCenter.SingleOrDefault()
            End If
            Return objWorkCenter
        Else
            Return New WorkCenter()
        End If
    End Function

    ''' <summary>
    ''' Obtiene todos los centros de Trabajo
    ''' </summary>
    ''' <returns>Lista de Compañías</returns>
    Public Function ListAllWorkCenter() As List(Of WorkCenter) Implements IWorkCenterRepository.ListAllWorkCenter
        Dim workCenter = From e In _context.WorkCenter
                    Select e
        Return workCenter.ToList()
    End Function

    ''' <summary>
    ''' Obtiene un centro de trabajo a través del ID
    ''' </summary>
    ''' <param name="workCenterId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWorkCenterById(workCenterId As Integer) As WorkCenter Implements IWorkCenterRepository.GetWorkCenterById
        Dim queryWorkCenter = From e In _context.WorkCenter
                              Where e.Id = workCenterId
                              Select e
        If queryWorkCenter.Count > 0 Then
            Return queryWorkCenter.SingleOrDefault()
        Else
            Return New WorkCenter()
        End If
    End Function

End Class
