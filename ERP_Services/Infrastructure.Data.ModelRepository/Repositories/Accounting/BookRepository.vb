'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/11/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Class BookRepository
    Inherits GenericRepository(Of LegalBook)
    Implements IBookRepository, Inject

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Functions"

    ''' <summary>
    ''' Obtiene un libro por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBookByCode(Code As String) As LegalBook Implements IBookRepository.GetBookByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As LegalBook In Me._context.LegalBook Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            ''en caso de que tenga al menos un comporbante, se agrega para que entre a validación en presentación para cambio de moneda
            If (From e In _context.JournalVouchers Where e.LegalBookId = res.Id Select e)?.Any() Then
                res.hasJournalVouchers = True
            End If
            res.OriginalValue = (From d As LegalBook In Me._context.LegalBook.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New LegalBook()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un libro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBookById(id As Integer) As LegalBook Implements IBookRepository.GetBookById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.LegalBook.Include("Currency").AsNoTracking() Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As LegalBook In Me._context.LegalBook.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res
        Else
            Return New LegalBook()
        End If
    End Function

    ''' <summary>
    ''' Valida si ya hay libro oficial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateOfficialBook() As LegalBook Implements IBookRepository.ValidateOfficialBook
        Return (From b In _context.LegalBook.AsNoTracking Where b.OfficialBook = True).FirstOrDefault
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
    Public Function FiscalYearClose(legalBookId As Integer, year As Integer, operativeUnitId As Integer, nitCompany As String, user As String) As List(Of SP_FiscalYearClose_Result) Implements IBookRepository.FiscalYearClose
        Dim result = Me.ExecuteStoredProcedure(Of SP_FiscalYearClose_Result)("[GeneralLedger].[SP_FiscalYearClose]",
                                                                    {("@LegalBookId", legalBookId),
                                                                    ("@Year", year),
                                                                    ("@IdOperatingUnit", operativeUnitId),
                                                                    ("@NitCompany", nitCompany),
                                                                    ("@User", user)})
        Return result
    End Function

#End Region

End Class
