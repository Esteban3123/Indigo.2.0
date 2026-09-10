'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Juan Diego Díaz
' Created          : 05-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class FreeTimeUseRepository
    Inherits GenericRepository(Of FreeTimeUse)
    Implements IFreeTimeUseRepository

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
    ''' obtiene una Actividad en Tiempo Libre por codigo
    ''' </summary>
    ''' <param name="code">codigo de la Actividad en Tiempo Libre</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFreeTimeUseByCode(code As String, tracking As Boolean) As FreeTimeUse Implements IFreeTimeUseRepository.GetFreeTimeUse
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res As New FreeTimeUse()
        If tracking Then
            res = (From d As FreeTimeUse In Me._context.FreeTimeUse
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        Else
            res = (From g In _context.FreeTimeUse.AsNoTracking
                   Where g.Code.Equals(code.Trim())
                   Select g).FirstOrDefault
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene una Actividad en Tiempo Libre por ID
    ''' </summary>
    ''' <param name="ID">ID de la Actividad en Tiempo Libre </param>
    ''' <returns>Actividad en Tiempo Libre</returns>
    ''' <remarks></remarks>
    Public Function GetFreeTimeUseById(id As Integer, tracking As Boolean) As FreeTimeUse Implements IFreeTimeUseRepository.GetFreeTimeUseById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res As New FreeTimeUse()
        If tracking Then
            res = (From d In Me._context.FreeTimeUse Where d.Id = id Select d).FirstOrDefault
        Else
            res = (From d As FreeTimeUse In Me._context.FreeTimeUse.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
        End If
        Return res
    End Function

End Class
