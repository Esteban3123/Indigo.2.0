Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class InventoryContractAssignmentRepository
    Inherits GenericRepository(Of InventoryContractAssignment)
    Implements IInventoryContractAssignmentRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

#End Region

#Region "Methods"

    Public Function GetInventoryContractAssignment(code As String) As InventoryContractAssignment Implements IInventoryContractAssignmentRepository.GetInventoryContractAssignment
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As InventoryContractAssignment In Me._context.InventoryContractAssignment
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault

        If res IsNot Nothing Then
            Dim contract = (From s In _context.InventoryContract.AsNoTracking Where res.ContractId = s.Id Select s).FirstOrDefault
            res.ContractNumber = contract.ContractNumber

            Dim transferor = (From sdl In _context.SuppliersDistributionLines.AsNoTracking().Include("Supplier").AsNoTracking().Include("DistributionLines").AsNoTracking() Where res.SupplierDistributionLineTransferorId = sdl.Id Select sdl).FirstOrDefault
            res.DescriptionSupplierTransferor = transferor.Supplier.Code + " - " + transferor.Supplier.Name + " - " + transferor.DistributionLines.Code + " - " + transferor.DistributionLines.Name

            Dim assignee = (From sdl In _context.SuppliersDistributionLines.AsNoTracking().Include("Supplier").AsNoTracking().Include("DistributionLines").AsNoTracking() Where res.SupplierDistributionLineAssigneeId = sdl.Id Select sdl).FirstOrDefault
            res.DescriptionSupplierAssignee = assignee.Supplier.Code + " - " + assignee.Supplier.Name + " - " + assignee.DistributionLines.Code + " - " + assignee.DistributionLines.Name

            res.OriginalValue = (From g In _context.InventoryContractAssignment.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault
            Return res
        Else
            Return New InventoryContractAssignment
        End If
    End Function

    Public Function GetInventoryContractAssignmentById(id As Integer) As InventoryContractAssignment Implements IInventoryContractAssignmentRepository.GetInventoryContractAssignmentById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As InventoryContractAssignment In Me._context.InventoryContractAssignment
                   Where d.Id.Equals(id)
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.InventoryContractAssignment.AsNoTracking
                                 Where g.Id.Equals(id)
                                 Select g).FirstOrDefault
            Return res
        Else
            Return New InventoryContractAssignment
        End If
    End Function

    Public Function SP_SaveInventoryContractAssignment(inventoryContractAssignmentXML As String, userCode As String) As SP_SaveInventoryContractAssignment_Result Implements IInventoryContractAssignmentRepository.SP_SaveInventoryContractAssignment
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveInventoryContractAssignment(inventoryContractAssignmentXML, userCode).SingleOrDefault()
    End Function

#End Region

End Class
