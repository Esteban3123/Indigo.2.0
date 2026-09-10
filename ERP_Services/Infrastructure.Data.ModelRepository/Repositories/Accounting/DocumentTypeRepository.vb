'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Payroll.Entities.HumanTalentParametrization
Imports Infrastructure.Data.Base

#End Region
''' <summary>
''' Repositorio de la entidad tipo de documento
''' </summary>
Public Class DocumentTypeRepository
    Inherits GenericRepository(Of JournalVoucherTypes)
    Implements IDocumentTypeRepository, Inject
#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseAccountingDRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork, ByVal secuenseDRepository As ISequenseAccountingDRepository)
        MyBase.New(acountingContext)
        Me._context = acountingContext
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "IDocumentTypeRepository"
    ''' <summary>
    ''' funcion asyncrona para obtener el tipo de documento contable y secuncia sql por id
    ''' </summary>
    ''' <param name="code">String</param>
    ''' <returns></returns>
    Public Async Function GetDocumentTypeAsync(code As String) As Task(Of JournalVoucherTypes) Implements IDocumentTypeRepository.GetDocumentTypeAsync
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As JournalVoucherTypes In Me._context.JournalVoucherTypes.Include("JournalVoucherTypeConsecutive") Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res.Count() > 0 Then

            If res(0).JournalVoucherTypeConsecutive IsNot Nothing AndAlso res(0).JournalVoucherTypeConsecutive.Count > 0 Then
                ' 1. Obtener los Ids únicos de libros
                Dim legalBookIds = res(0).JournalVoucherTypeConsecutive.
                    Select(Function(c) c.LegalBookId).Distinct().ToList()

                ' 2. Consultar todos los libros necesarios en una sola consulta
                Dim books = (From x In _context.LegalBook.AsNoTracking()
                             Where legalBookIds.Contains(x.Id)
                             Select x.Id, Description = String.Concat(x.Code, " - ", x.Name)).ToList()

                ' 3. Crear un diccionario para acceso rápido
                Dim bookDescriptions = books.ToDictionary(Function(b) b.Id, Function(b) b.Description)

                ' 4. Asignar la descripción a cada consecutivo usando el diccionario
                For Each item In res(0).JournalVoucherTypeConsecutive
                    If bookDescriptions.ContainsKey(item.LegalBookId) Then
                        item.LegalBookDescription = bookDescriptions(item.LegalBookId)
                    End If
                    Dim Sequense = $"Seq_JV_T{item.JournalVoucherTypeId}_L{item.LegalBookId}_Y{item.Year}"
                    item.Consecutive = Await _secuenseDRepository.GetCurrentSequenceValueAsync(Sequense)
                Next
            End If

            res(0).OriginalValue = (From d As JournalVoucherTypes In Me._context.JournalVoucherTypes.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New JournalVoucherTypes()
        End If
    End Function
    ''' <summary>
    ''' funcion para obtener el tipo de documento  contable por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetJournalVoucherById(id As Long) As JournalVoucherTypes Implements IDocumentTypeRepository.GetJournalVoucherById
        Dim query = From e In _context.JournalVoucherTypes.AsNoTracking()
                    Where e.Id = id
                    Select e

        If query.Count > 0 Then
            Return query.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

#End Region

End Class
