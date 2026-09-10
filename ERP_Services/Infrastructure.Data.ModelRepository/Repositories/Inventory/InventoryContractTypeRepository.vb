'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Henry Alejandro Vargas Polania
' Created          : 26-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class InventoryContractTypeRepository
    Inherits GenericRepository(Of InventoryContractType)
    Implements IInventoryContractTypeRepository

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

    Public Function GetInventoryContractType(code As String) As InventoryContractType Implements IInventoryContractTypeRepository.GetInventoryContractType
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As InventoryContractType In Me._context.InventoryContractType
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then
            Select Case res.Type
                Case 1
                    res.TypenName = "Fijo"
                Case 2
                    res.TypenName = "Variable"
                Case Else
                    res.TypenName = String.Empty
            End Select


            res.OriginalValue = (From g In _context.InventoryContractType.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault
            Return res
        Else
            Return New InventoryContractType()
        End If
    End Function

    Public Function GetInventoryContractTypeById(id As Integer) As InventoryContractType Implements IInventoryContractTypeRepository.GetInventoryContractTypeById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As InventoryContractType In Me._context.InventoryContractType
                   Where d.Id.Equals(id)
                   Select d).FirstOrDefault
        If res IsNot Nothing Then
            Select Case res.Type
                Case 1
                    res.TypenName = "Fijo"
                Case 2
                    res.TypenName = "Variable"
                Case Else
                    res.TypenName = String.Empty
            End Select

            res.OriginalValue = (From g In _context.InventoryContractType.AsNoTracking
                                  Where g.Id.Equals(id)
                                  Select g).FirstOrDefault
            Return res
        Else
            Return New InventoryContractType()
        End If
    End Function
End Class
