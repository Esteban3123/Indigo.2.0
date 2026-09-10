#Region "Imports"
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class AccountingService

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado del libro
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateBook(code As String, state As Boolean) As Domain.Base.Entities.ActionResult(Of Domain.Entities.LegalBook) Implements IAccountingBook.ChangeStateBook
        Using service As IBookAdminService = Container.Current.Resolve(Of IBookAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._bookAdminService.ChangeState(code, state, audit)
    End Function

    ''' <summary>
    ''' Elimina un libro
    ''' </summary>
    ''' <param name="Book"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBook(Book As Domain.Entities.LegalBook) As Domain.Base.Entities.ActionResult Implements IAccountingBook.DeleteBook
        Using service As IBookAdminService = Container.Current.Resolve(Of IBookAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.DeleteBook(Book, audit)
        End Using
        'Return Me._bookAdminService.DeleteBook(Book, audit)
    End Function

    ''' <summary>
    ''' Obtiene un libro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBookByCode(code As String) As Domain.Base.Entities.ActionResult(Of Domain.Entities.LegalBook) Implements IAccountingBook.GetBookByCode
        Using service As IBookAdminService = Container.Current.Resolve(Of IBookAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.GetBookByCode(code, audit)
        End Using
        'Return Me._bookAdminService.GetBookByCode(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un libro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBookById(id As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.LegalBook) Implements IAccountingBook.GetBookById
        Using service As IBookAdminService = Container.Current.Resolve(Of IBookAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.GetBookById(id, audit)
        End Using
        'Return Me._bookAdminService.GetBookById(id, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un libro
    ''' </summary>
    ''' <param name="Book"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBook(Book As Domain.Entities.LegalBook) As Domain.Base.Entities.ActionResult(Of Domain.Entities.LegalBook) Implements IAccountingBook.SaveBook
        Using service As IBookAdminService = Container.Current.Resolve(Of IBookAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveBook(Book, audit, idSequense)
        End Using
        'Return Me._bookAdminService.SaveBook(Book, audit, idSequense)
    End Function

    ''' <summary>
    ''' Valida si el libro es oficial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateOfficialBook() As Domain.Base.Entities.ActionResult(Of Domain.Entities.LegalBook) Implements IAccountingBook.ValidateOfficialBook
        Using service As IBookAdminService = Container.Current.Resolve(Of IBookAdminService)()
            Return service.ValidateOfficialBook()
        End Using
        'Return Me._bookAdminService.ValidateOfficialBook()
    End Function

    ''' <summary>
    ''' Metodo para ejecutar el cierre contable por libro
    ''' </summary>
    ''' <param name="legalBookId"></param>
    ''' <param name="year"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="nitCompany"></param>
    ''' <param name="user"></param>
    ''' <returns></returns>
    Public Function FiscalYearClose(legalBookId As Integer, year As Integer, operativeUnitId As Integer, nitCompany As String, user As String) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IAccountingBook.FiscalYearClose
        Using service As IBookAdminService = Container.Current.Resolve(Of IBookAdminService)()
            Return service.FiscalYearClose(legalBookId, year, operativeUnitId, nitCompany, user)
        End Using
    End Function

#End Region

End Class
