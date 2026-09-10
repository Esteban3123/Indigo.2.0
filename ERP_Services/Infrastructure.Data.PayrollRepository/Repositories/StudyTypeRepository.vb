'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class StudyTypeRepository
    Inherits GenericRepository(Of StudyType)
    Implements IStudyTypeRepository

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
    ''' Obtiene un tipo de estudio
    ''' </summary>
    ''' <param name="code">Codigo del tipo de estudio</param>
    ''' <returns>Tipo de estudio</returns>
    ''' <remarks></remarks>
    Public Function GetStudyType(code As String, Optional tracking As Boolean = True) As StudyType Implements IStudyTypeRepository.GetStudyType
        Dim studyType = From e In _context.StudyType
                        Where e.Code = code
                        Select e
        If studyType.Count > 0 Then
            Dim objStudyType = Nothing
            If tracking = False Then
                objStudyType = (From e In _context.StudyType.AsNoTracking
                                Where e.Code = code
                                Select e).SingleOrDefault
            Else
                objStudyType = studyType.SingleOrDefault()
            End If
            Return objStudyType
        Else
            Return New StudyType()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los tipos de estudio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllStudyType() As List(Of StudyType) Implements IStudyTypeRepository.ListAllStudyType
        Dim studyType = From e In _context.StudyType
                        Select e
        Return studyType.ToList()
    End Function
End Class
