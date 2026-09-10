Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class CPCCatalogRepository
    Inherits GenericRepository(Of CPCCatalog)
    Implements ICPCCatalogRepository

    'Coexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un CPCCatalog por código
    ''' </summary>
    ''' <param name="Code">Código del CPCCatalog </param>
    ''' <returns></returns>
    Public Function GetCPCCatalogByCode(Code As String) As CPCCatalog Implements ICPCCatalogRepository.GetCPCCatalogByCode
        Dim result As CPCCatalog = (From e In _context.CPCCatalog
                                    Where e.Code = Code
                                    Select e).FirstOrDefault

        If result IsNot Nothing Then

            If result.CPCCatalogOwnerId IsNot Nothing Then
                Dim parent = (From p In _context.CPCCatalog.AsNoTracking Where p.Id = result.CPCCatalogOwnerId Select p).FirstOrDefault
                result.ParentDescription = parent.Code + " - " + parent.Name
            End If

            result.OriginalValue = (From e In _context.CPCCatalog.AsNoTracking Where e.Id = result.Id Select e).FirstOrDefault

            Return result
        Else
            Return New CPCCatalog
        End If
    End Function

    ''' <summary>
    ''' Consulta un rubro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCPCCatalogById(Id As Integer) As CPCCatalog Implements ICPCCatalogRepository.GetCPCCatalogById
        Return (From c In _context.CPCCatalog.AsNoTracking() Where c.Id = Id Select c).FirstOrDefault
    End Function

#End Region

End Class
