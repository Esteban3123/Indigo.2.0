'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/11/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad clase contable
''' </summary>
Public Interface IBookRepository
    Inherits IRepository(Of LegalBook)

    ''' <summary>
    ''' Gets the book by code.
    ''' </summary>
    ''' <param name="Code">The code.</param>
    ''' <returns></returns>
    Function GetBookByCode(ByVal Code As String) As LegalBook

    ''' <summary>
    ''' Gets the book by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetBookById(ByVal id As Integer) As LegalBook

    ''' <summary>
    ''' Valida si ya hay libro oficial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateOfficialBook() As LegalBook

    ''' <summary>
    ''' Metodo para ejecutar el cierre contable por libro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function FiscalYearClose(legalBookId As Integer, year As Integer, operativeUnitId As Integer, nitCompany As String, user As String) As List(Of SP_FiscalYearClose_Result)

End Interface