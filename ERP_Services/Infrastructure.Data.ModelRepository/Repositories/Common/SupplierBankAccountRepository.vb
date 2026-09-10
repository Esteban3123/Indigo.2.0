
Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SupplierBankAccountRepository
    Inherits GenericRepository(Of SupplierBankAccount)
    Implements ISupplierBankAccountRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuppliersBankAccountByIdSupplier(id As Integer, Optional tracking As Boolean = True) As List(Of SupplierBankAccount) Implements ISupplierBankAccountRepository.GetSuppliersBankAccountByIdSupplier
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim list = (From dl In Me._context.SupplierBankAccount Where dl.SupplierId = id Select dl).ToList
        Return list
    End Function

End Class
