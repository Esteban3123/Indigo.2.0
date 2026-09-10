Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class InventoryContractModificationRepository
    Inherits GenericRepository(Of InventoryContractModification)
    Implements IInventoryContractModificationRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

#End Region

#Region "Methods"

    Public Function GetInventoryContractModification(code As String) As InventoryContractModification Implements IInventoryContractModificationRepository.GetInventoryContractModification
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As InventoryContractModification In Me._context.InventoryContractModification.Include("InventoryContractModificationAvailability")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault

        If res IsNot Nothing Then
            Dim contract = (From s In _context.InventoryContract.AsNoTracking Where res.ContractId = s.Id Select s).FirstOrDefault
            res.ContractNumber = contract.ContractNumber

            If res.InventoryContractModificationAvailability IsNot Nothing AndAlso res.InventoryContractModificationAvailability.Any() Then
                For Each item In res.InventoryContractModificationAvailability
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

            res.OriginalValue = (From g In _context.InventoryContractModification.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault
            Return res
        Else
            Return New InventoryContractModification
        End If
    End Function

    Public Function GetInventoryContractModificationById(id As Integer) As InventoryContractModification Implements IInventoryContractModificationRepository.GetInventoryContractModificationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As InventoryContractModification In Me._context.InventoryContractModification
                   Where d.Id.Equals(id)
                   Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From g In _context.InventoryContractModification.AsNoTracking
                                 Where g.Id.Equals(id)
                                 Select g).FirstOrDefault
            Return res
        Else
            Return New InventoryContractModification
        End If
    End Function

    Public Function SP_SaveInventoryContractModification(inventoryContractModificationXML As String, userCode As String) As SP_SaveInventoryContractModification_Result Implements IInventoryContractModificationRepository.SP_SaveInventoryContractModification
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveInventoryContractModification(inventoryContractModificationXML, userCode).SingleOrDefault()
    End Function

#End Region

End Class
