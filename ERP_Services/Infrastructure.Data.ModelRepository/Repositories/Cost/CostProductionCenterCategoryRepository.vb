#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class CostProductionCenterCategoryRepository
    Inherits GenericRepository(Of CostProductionCenterCategory)
    Implements ICostProductionCenterCategoryRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetCostProductionCenterCategoryByCode(Code As String) As CostProductionCenterCategory Implements ICostProductionCenterCategoryRepository.GetCostProductionCenterCategoryByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As CostProductionCenterCategory In _context.CostProductionCenterCategory Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As CostProductionCenterCategory In Me._context.CostProductionCenterCategory.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New CostProductionCenterCategory()
        End If
    End Function

    Public Function ListAllCostProductionCenterCategory() As List(Of CostProductionCenterCategory) Implements ICostProductionCenterCategoryRepository.ListAllCostProductionCenterCategory
        Dim ListCostProductionCenterCategory = From e In _context.CostProductionCenterCategory
                                               Select e

        If ListCostProductionCenterCategory.Count() > 0 Then
            Return ListCostProductionCenterCategory.ToList()
        Else
            Return New List(Of CostProductionCenterCategory)
        End If
    End Function

#End Region

End Class
