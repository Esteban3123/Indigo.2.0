Imports System.Data.Entity
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class ContractPackageProductRepository
    Inherits GenericRepository(Of ContractPackageProduct)
    Implements IContractPackageProductRepository

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

    Public Function GetAllContractPackageProductByContractPackageId(contractPackageId As Integer, Optional tracking As Boolean = True) As List(Of ContractPackageProduct) Implements IContractPackageProductRepository.GetAllContractPackageProductByContractPackageId
        Dim query = (From p In _context.ContractPackageProduct Where p.ContractPackageId = contractPackageId Select p)

        If Not tracking Then
            query = query.AsNoTracking()
        End If

        Return query.ToList()
    End Function
End Class
