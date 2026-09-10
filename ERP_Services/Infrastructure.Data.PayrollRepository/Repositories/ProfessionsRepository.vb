'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.Data.Base

Public Class ProfessionsRepository
    Inherits GenericRepository(Of Profession)
    Implements IProfessionsRepository

    ' contexto de payroll
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia la instancia del contexto
    ''' </summary>
    ''' <param name="context">Contexto de payroll</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una profesion en especifico
    ''' </summary>
    ''' <param name="code">Codigo de la profesion</param>
    ''' <returns>Profesion</returns>
    ''' <remarks></remarks>
    Public Function GetProfessions(code As String, Optional tracking As Boolean = True) As Profession Implements IProfessionsRepository.GetProfessions
        Dim profession = From e In _context.Profession
                         Where e.Code = code
                         Select e
        If (profession.Count > 0) Then
            Dim objProfession = Nothing
            If tracking = False Then
                objProfession = (From e In _context.Profession.AsNoTracking
                                Where e.Code = code
                                Select e).SingleOrDefault
            Else
                objProfession = profession.SingleOrDefault
            End If
            Return objProfession
        Else
            Return New Profession()
        End If
    End Function

    ''' <summary>
    ''' Lista todas las profesiones
    ''' </summary>
    ''' <returns>Lista de profesiones</returns>
    ''' <remarks></remarks>
    Public Function ListAllProfessions() As List(Of Profession) Implements IProfessionsRepository.ListAllProfessions
        Dim profession = From e In _context.Profession
                         Select e
        Return profession.ToList()
    End Function
End Class
