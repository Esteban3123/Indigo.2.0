#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class AttachmentRepository
    Inherits GenericRepository(Of Attachment)
    Implements IAttachmentRepository

#Region "Builder"

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

#End Region

#Region "Methods"

    Public Function GetAttachmentById(Id As Integer) As Attachment Implements IAttachmentRepository.GetAttachmentById
        Return (From a In _context.Attachment.AsNoTracking Where a.Id = Id).FirstOrDefault()
    End Function

    Public Function GetAttachmentsByFormAndEntity(FormId As Integer, EntityName As String, EntityId As Integer, ByVal withTop As Boolean) As List(Of Attachment) Implements IAttachmentRepository.GetAttachmentsByFormAndEntity
        If withTop Then
            Dim result = (From a In _context.Attachment.AsNoTracking
                          Where a.FormId = FormId AndAlso a.EntityName = EntityName AndAlso a.EntityId = EntityId
                          Order By a.CreationDate Descending
                          Take 5
                          Select New With
                          {
                              .Id = a.Id,
                              .Name = a.Name,
                              .Extension = a.Extension,
                              .CreationDate = a.CreationDate
                          }).ToList

            Return result.ToList.Select(Function(a) New Attachment With
            {
                .Id = a.Id,
                .Name = a.Name,
                .Extension = a.Extension,
                .CreationDate = a.CreationDate
            }).ToList
        Else
            Dim result = (From a In _context.Attachment.AsNoTracking
                          Where a.FormId = FormId AndAlso a.EntityName = EntityName AndAlso a.EntityId = EntityId
                          Select New With
                          {
                              .Id = a.Id,
                              .Name = a.Name,
                              .Extension = a.Extension,
                              .Description = a.Description,
                              .CreationDate = a.CreationDate
                          }).ToList

            Return result.ToList.Select(Function(a) New Attachment With
            {
                .Id = a.Id,
                .Name = a.Name,
                .Extension = a.Extension,
                .Description = a.Description,
                .CreationDate = a.CreationDate
            }).ToList
        End If
    End Function

#End Region

End Class
