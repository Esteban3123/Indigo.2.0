'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 03-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class ProfessionalRiskRepository

    Inherits GenericRepository(Of ProfessionalRisk)
    Implements IProfessionalRiskRepository

    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un Riesgo Profesional
    ''' </summary>
    ''' <param name="code">Código del Riesgo Profesional</param>
    ''' <returns>Riesgo Profesional</returns>
    ''' <remarks></remarks>
    Public Function GetProfessionalRisk(code As String, Optional tracking As Boolean = True) As ProfessionalRisk Implements IProfessionalRiskRepository.GetProfessionalRisk
        Dim professionalRisk = From e In _context.ProfessionalRisk
                     Where e.Code = code
                     Select e
        If professionalRisk.Count > 0 Then
            Dim objProfessionalRisk = Nothing
            If tracking = False Then
                objProfessionalRisk = (From e In _context.ProfessionalRisk.AsNoTracking
                                       Where e.Code = code
                                       Select e).SingleOrDefault
            Else
                objProfessionalRisk = professionalRisk.SingleOrDefault()
            End If
            Return objProfessionalRisk
        Else
            Return New ProfessionalRisk()
        End If
    End Function

    ''' <summary>
    ''' Lista Todos los Riesgos Profesionales
    ''' </summary>
    ''' <returns>Lista de Riesgos Profesionales</returns>
    ''' <remarks></remarks>
    Public Function ListAllProfessionalRisk() As List(Of ProfessionalRisk) Implements IProfessionalRiskRepository.ListAllProfessionalRisk
        Dim professionalRisk = From e In _context.ProfessionalRisk
                    Select e
        Return professionalRisk.ToList()
    End Function
End Class
