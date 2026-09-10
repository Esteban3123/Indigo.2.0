'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class LanguageRepository
    Inherits GenericRepository(Of Language)
    Implements ILanguageRepository

    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un idioma en especifico
    ''' </summary>
    ''' <param name="code">Codigo del idioma</param>
    ''' <returns>Idioma</returns>
    ''' <remarks></remarks>
    Public Function GetLanguage(code As String, Optional tracking As Boolean = True) As Language Implements ILanguageRepository.GetLanguage
        Dim language = From e In _context.Language
                       Where e.Code = code
                       Select e
        If language.Count > 0 Then
            Dim objLanguage = Nothing
            If tracking = False Then
                objLanguage = (From e In _context.Language.AsNoTracking
                               Where e.Code = code
                               Select e).SingleOrDefault
            Else
                objLanguage = language.SingleOrDefault
            End If
            Return objLanguage
        Else
            Return New Language()
        End If
    End Function

    ''' <summary>
    ''' Obtiene todos los idiomas
    ''' </summary>
    ''' <returns>Lista de idiomas</returns>
    ''' <remarks></remarks>
    Public Function ListAllLanguage() As List(Of Language) Implements ILanguageRepository.ListAllLanguage
        Dim language = From e In _context.Language
                       Select e
        Return language.ToList()
    End Function
End Class
