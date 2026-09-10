#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region

Public Class BankConciliationConceptsRepository

    Inherits GenericRepository(Of BankConciliationConcepts)
    Implements IBankConciliationConceptsRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>        
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene un fabricantes por Código
    ''' </summary>
    ''' <param name="Code">Código del fabricante</param>
    ''' <returns>Manufacturers</returns>
    ''' <remarks></remarks>
    Public Function GetBankConciliationConceptsByCode(Code As String, Optional tracking As Boolean = True) As BankConciliationConcepts Implements IBankConciliationConceptsRepository.GetBankConciliationConceptsByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From t In _context.BankConciliationConcepts Where t.Code = Code Select t).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From t In _context.BankConciliationConcepts.AsNoTracking Where t.Code = Code Select t).FirstOrDefault
            Return res
        Else
            Return New BankConciliationConcepts
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas los conceptos de conculiación bancaria
    ''' </summary>
    ''' <returns>Lista de fabricantes</returns>
    ''' <remarks></remarks>
    Public Function ListAllManufacturers() As List(Of BankConciliationConcepts) Implements IBankConciliationConceptsRepository.ListAllBankConciliationConcepts
        Dim ListManufacturers = From e In _context.BankConciliationConcepts.Include("VoucherTransaction").Include("Consignment").Include("CashReceipts").Include("NoteConcepts")
                                Select e

        If ListManufacturers.Count() > 0 Then
            Return ListManufacturers.ToList()
        Else
            Return New List(Of BankConciliationConcepts)
        End If


    End Function
End Class
