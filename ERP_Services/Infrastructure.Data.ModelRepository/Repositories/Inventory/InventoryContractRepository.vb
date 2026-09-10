'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Henry Alejandro Vargas Polania
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class InventoryContractRepository
    Inherits GenericRepository(Of InventoryContract)
    Implements IInventoryContractRepository

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

    Public Function GetInventoryContract(code As String) As InventoryContract Implements IInventoryContractRepository.GetInventoryContract
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As InventoryContract In Me._context.InventoryContract.Include("InventoryContractDetail").Include("InventoryContractAvailability").Include("Currency").AsNoTracking()
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault

        If res IsNot Nothing Then
            Dim contractType = (From t In _context.InventoryContractType.AsNoTracking() Where res.ContractTypeId = t.Id Select t).FirstOrDefault()
            res.DescriptionContractType = contractType.Code + " - " + contractType.Name

            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where res.SupplierDistributionLineId = sdl.Id Select sdl).FirstOrDefault
            Dim supplier = (From s In _context.Supplier.AsNoTracking Where supplierDistributionLine.IdSupplier = s.Id Select s).FirstOrDefault
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking Where supplierDistributionLine.IdDistributionLine = dl.Id Select dl).FirstOrDefault
            res.DescriptionSupplier = supplier.Code + " - " + supplier.Name + " - " + distributionLine.Code + " - " + distributionLine.Name

            If res.InventoryContractDetail IsNot Nothing AndAlso res.InventoryContractDetail.Count > 0 Then
                For Each item As InventoryContractDetail In res.InventoryContractDetail
                    Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where item.ProductId = p.Id Select p).FirstOrDefault()
                    item.InventoryProduct = product
                Next
            End If

            If res.InventoryContractAvailability IsNot Nothing AndAlso res.InventoryContractAvailability.Any() Then
                For Each item In res.InventoryContractAvailability
                    Dim availabilityDetail = (From x In _context.AvailabilityDetail.AsNoTracking().Include("Availability").AsNoTracking().Include("Budget").AsNoTracking().
                                                  Include("Budget.Category").AsNoTracking().Include("Budget.Category.FinancialSource").AsNoTracking().Include("Budget.RevenueType").AsNoTracking()
                                              Where x.Id = item.AvailabilityDetailId
                                              Select x).FirstOrDefault()

                    item.AvailabilityCode = availabilityDetail.Availability.Code
                    item.CategoryCodeName = String.Format("{0} - {1}", availabilityDetail.Budget.Category.Code, availabilityDetail.Budget.Category.Name)
                    item.FinancialSourceCodeName = String.Format("{0} - {1}", availabilityDetail.Budget.Category.FinancialSource.Code, availabilityDetail.Budget.Category.FinancialSource.Name)
                    item.RevenueTypeCodeName = String.Format("{0} - {1}", availabilityDetail.Budget.RevenueType.Code, availabilityDetail.Budget.RevenueType.Name)
                    item.Balance = availabilityDetail.Balance

                    If res.BudgetaryValidityId Is Nothing Then
                        Dim budgetaryValidity = (From bv In _context.BudgetaryValidity.AsNoTracking() Where bv.Id = availabilityDetail.Availability.BudgetaryValidityId Select bv).FirstOrDefault()

                        If budgetaryValidity IsNot Nothing Then
                            res.BudgetaryValidityId = budgetaryValidity.Id
                            res.BudgetaryValidityDescription = budgetaryValidity.Year

                            res.BudgetaryEntityId = budgetaryValidity.BudgetaryEntityId
                            res.BudgetaryEntityDescription = (From be In _context.BudgetaryEntity.AsNoTracking Where be.Id = budgetaryValidity.BudgetaryEntityId Select String.Concat(be.Code, " - ", be.Name)).FirstOrDefault
                        End If
                    End If
                Next
            End If

            res.OriginalValue = (From g In _context.InventoryContract.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault
            Return res
        Else
            Return New InventoryContract
        End If
    End Function

    Public Function GetInventoryContractById(id As Integer) As InventoryContract Implements IInventoryContractRepository.GetInventoryContractById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As InventoryContract In Me._context.InventoryContract.Include("InventoryContractDetail")
                   Where d.Id.Equals(id)
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.InventoryContract.AsNoTracking
                                 Where g.Id.Equals(id)
                                 Select g).FirstOrDefault
            Return res
        Else
            Return New InventoryContract
        End If
    End Function

    ''' <summary>
    ''' Guarda el contrato de inventarios
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveInventoryContract(Xml As String, UserCode As String) As SP_SaveInventoryContract_Result Implements IInventoryContractRepository.SP_SaveInventoryContract
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveInventoryContract(Xml, UserCode).SingleOrDefault()
    End Function

End Class
