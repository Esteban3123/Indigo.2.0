'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 12-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class EducationLevelsRepository
    Inherits GenericRepository(Of EducationLevel)
    Implements IEducationLevelsRepository

    'Contexto del repositorio de EducationLevels
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicializa la nueva instancia del contexto
    ''' </summary>
    ''' <param name="context"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un nivel de estudio especifico
    ''' </summary>
    ''' <returns>Nivel de estudio</returns>
    ''' <remarks></remarks>
    Public Function GetEducationLevels(ByVal code As String, Optional tracking As Boolean = True) As Entities.EducationLevel Implements IEducationLevelsRepository.GetEducationLevels
        Dim EducationLevel = From e In _context.EducationLevel
                             Where e.Code = code
                             Select e
        If (EducationLevel.Count > 0) Then
            Dim objEducationLevel = Nothing
            If tracking = False Then
                objEducationLevel = (From e In _context.EducationLevel.AsNoTracking
                                     Where e.Code = code
                                     Select e).SingleOrDefault
            Else
                objEducationLevel = EducationLevel.SingleOrDefault()
            End If
            Return objEducationLevel

        Else
            Return New EducationLevel()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los niveles de estudio
    ''' </summary>
    ''' <returns>Lista de niveles de estudio</returns>
    ''' <remarks></remarks>
    Public Function ListAllEducationLevels() As List(Of Entities.EducationLevel) Implements IEducationLevelsRepository.ListAllEducationLevels
        Dim EducationLevels = From e In _context.EducationLevel
                              Select e
        Return EducationLevels.ToList()
    End Function
End Class
