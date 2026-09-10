'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Juan Diego Díaz
' Created          : 30-08-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class SportPracticeRepository
    Inherits GenericRepository(Of SportPractice)
    Implements ISportPracticeRepository

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
    ''' obtiene un Deporte Practicadopor codigo
    ''' </summary>
    ''' <param name="code">codigo del Deporte Practicado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSportPracticeByCode(code As String, tracking As Boolean) As SportPractice Implements ISportPracticeRepository.GetSportPractice
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res As New SportPractice()
        If tracking Then
            res = (From d As SportPractice In Me._context.SportPractice
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        Else
            res = (From g In _context.SportPractice.AsNoTracking
                   Where g.Code.Equals(code.Trim())
                   Select g).FirstOrDefault
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un Deporte Practicado por ID
    ''' </summary>
    ''' <param name="ID">ID del Deporte Practicado</param>
    ''' <returns>Practica Deportiva</returns>
    ''' <remarks></remarks>
    Public Function GetSportPracticeById(id As Integer, tracking As Boolean) As SportPractice Implements ISportPracticeRepository.GetSportPracticeById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res As New SportPractice()
        If tracking Then
            res = (From d In Me._context.SportPractice Where d.Id = id Select d).FirstOrDefault
        Else
            res = (From d As SportPractice In Me._context.SportPractice.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
        End If
        Return res
    End Function

End Class
