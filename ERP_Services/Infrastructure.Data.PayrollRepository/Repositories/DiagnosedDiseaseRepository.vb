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
Public Class DiagnosedDiseaseRepository
    Inherits GenericRepository(Of DiagnosedDisease)
    Implements IDiagnosedDiseaseRepository

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
    ''' obtiene una Enfermedad Diagnosticada por codigo
    ''' </summary>
    ''' <param name="code">codigo de la Enfermedad Diagnosticada</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDiagnosedDiseaseByCode(code As String, tracking As Boolean) As DiagnosedDisease Implements IDiagnosedDiseaseRepository.GetDiagnosedDisease
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res As New DiagnosedDisease()
        If tracking Then
            res = (From d As DiagnosedDisease In Me._context.DiagnosedDisease
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        Else
            res = (From g In _context.DiagnosedDisease.AsNoTracking
                   Where g.Code.Equals(code.Trim())
                   Select g).FirstOrDefault
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene una Enfermedad Diagnosticada por ID
    ''' </summary>
    ''' <param name="ID">ID de la Enfermedad Diagnosticada</param>
    ''' <returns>Actividad en Tiempo Libre</returns>
    ''' <remarks></remarks>
    Public Function GetDiagnosedDiseaseById(id As Integer, tracking As Boolean) As DiagnosedDisease Implements IDiagnosedDiseaseRepository.GetDiagnosedDiseaseById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res As New DiagnosedDisease()
        If tracking Then
            res = (From d In Me._context.DiagnosedDisease Where d.Id = id Select d).FirstOrDefault
        Else
            res = (From d As DiagnosedDisease In Me._context.DiagnosedDisease.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
        End If
        Return res
    End Function

End Class
