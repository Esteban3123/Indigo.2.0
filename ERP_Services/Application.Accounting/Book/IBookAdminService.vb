'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/11/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBookAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un libro
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveBook(ByVal Book As LegalBook, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of LegalBook)

    ''' <summary>
    ''' Elimina un libro
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteBook(ByVal Book As LegalBook, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un libro por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetBookByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of LegalBook)

    ''' <summary>
    ''' Obtiene un libro por id
    ''' </summary>
    ''' <returns></returns>
    Function GetBookById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of LegalBook)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of LegalBook)

    ''' <summary>
    ''' Valida si ya hay libro oficial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateOfficialBook() As ActionResult(Of LegalBook)

    ''' <summary>
    ''' Metodo para ejecutar el cierre contable por libro
    ''' </summary>
    ''' <param name="legalBookId"></param>
    ''' <param name="year"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="nitCompany"></param>
    ''' <param name="user"></param>
    ''' <returns></returns>
    Function FiscalYearClose(legalBookId As Integer, year As Integer, operativeUnitId As Integer, nitCompany As String, user As String) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface
