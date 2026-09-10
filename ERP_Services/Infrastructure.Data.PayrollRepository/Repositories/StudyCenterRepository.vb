'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class StudyCenterRepository
    Inherits GenericRepository(Of StudyCenter)
    Implements IStudyCenterRepository


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
    ''' Obtiene un Centro de Estudio
    ''' </summary>
    ''' <param name="code">Código del Centro de Estudio</param>
    ''' <returns>Centro de Estudio</returns>
    ''' <remarks></remarks>
    Public Function GetStudyCenter(code As String, Optional tracking As Boolean = True) As StudyCenter Implements IStudyCenterRepository.GetStudyCenter
        Dim studyCenter = From e In _context.StudyCenter
                      Where e.Code = code
                      Select e
        If studyCenter.Count > 0 Then
            Dim objStudyCenter = Nothing
            If tracking = False Then
                objStudyCenter = (From e In _context.StudyCenter.AsNoTracking
                                 Where e.Code = code
                                 Select e).SingleOrDefault
            Else
                objStudyCenter = studyCenter.SingleOrDefault()
            End If
            Return objStudyCenter
            Return New StudyCenter()
        End If
    End Function

    ''' <summary>
    ''' Lista Todos los Centros de Estudio
    ''' </summary>
    ''' <returns>Centros de Estudio</returns>
    ''' <remarks></remarks>
    Public Function ListAllStudyCenter() As List(Of StudyCenter) Implements IStudyCenterRepository.ListAllStudyCenter
        Dim studyCenter = From e In _context.StudyCenter
                       Select e
        Return studyCenter.ToList()
    End Function
End Class
