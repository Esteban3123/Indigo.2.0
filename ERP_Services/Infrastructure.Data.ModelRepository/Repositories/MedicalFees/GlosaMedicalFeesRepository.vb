#Region "Importar"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class GlosaMedicalFeesRepository

    Inherits GenericRepository(Of GlosaMedicalFees)
    Implements IGlosaMedicalFeesRepository

    'Devuelve el contexto en este repositorio
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context"></param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene un registro por Codidgo
    ''' </summary>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetGlosaMedicalFeesByCode(Code As String) As GlosaMedicalFees Implements IGlosaMedicalFeesRepository.GetGlosaMedicalFeesByCode
        If Code Is Nothing OrElse Code Is String.Empty Then
            Throw New ArgumentNullException("Code")
        End If

        Dim res = (From d As GlosaMedicalFees In _context.GlosaMedicalFees.Include("GlosaMedicalFeesDetail").Include("GlosaMedicalFeesDetail.GlosaMedicalFeesEvaluation") Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault

        If res IsNot Nothing Then

            Dim objSupplier = (From a In _context.Supplier.AsNoTracking Where a.Id = res.SupplierId Select a).FirstOrDefault()
            res.DescriptionSupplier = objSupplier.Code & " - " & objSupplier.Name

            For Each detail In res.GlosaMedicalFeesDetail
                Dim AccountPayable = (From a In _context.AccountPayable.AsNoTracking Where a.Id = detail.AccountPayableId Select a).FirstOrDefault()
                Dim GlosaMedicalFeesConcepts = (From a In _context.GlosaMedicalFeesConcepts.AsNoTracking Where a.Id = detail.GlosaMedicalFeesConceptsId Select a).FirstOrDefault()
                detail.InvoiceNumber = AccountPayable.BillNumber
                detail.InvoiceValue = AccountPayable.InvoiceValue
                detail.AccountPayableDescription = AccountPayable.Code & " - " & AccountPayable.BillNumber
                detail.ConceptDescription = GlosaMedicalFeesConcepts.Code & " - " & GlosaMedicalFeesConcepts.Name
            Next

            res.OriginalValue = (From d As GlosaMedicalFees In Me._context.GlosaMedicalFees.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New GlosaMedicalFees()
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todos los registros de la tabla glosasMedicalFees
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllGlosaMedicalFees() As List(Of GlosaMedicalFees) Implements IGlosaMedicalFeesRepository.ListAllGlosaMedicalFees
        Dim ListGlosaMedicalFees = From e In _context.GlosaMedicalFees
                                   Select e

        If ListGlosaMedicalFees.Count() > 0 Then
            Return ListGlosaMedicalFees.ToList()
        Else
            Return New List(Of GlosaMedicalFees)
        End If
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetAccountPayableById(Id As Integer) As AccountPayable Implements IGlosaMedicalFeesRepository.GetAccountPayableById
        Dim AccountPayable = From e In _context.AccountPayable
                             Where e.Id = Id
                             Select e

        If AccountPayable IsNot Nothing Then
            Return AccountPayable.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

End Class