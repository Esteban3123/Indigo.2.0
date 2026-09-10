'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BudgetTransferDetailRepository
    Inherits GenericRepository(Of BudgetTransferDetail)
    Implements IBudgetTransferDetailRepository
    'Coexto 
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el detalle del traslado por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetTransferDetailById(Id As Integer) As BudgetTransferDetail Implements IBudgetTransferDetailRepository.GetBudgetTransferDetailById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.BudgetTransferDetail Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As BudgetTransferDetail In Me._context.BudgetTransferDetail.AsNoTracking() Where d.Id = Id Select d).FirstOrDefault

            Return res
        Else
            Return New BudgetTransferDetail
        End If
    End Function

#End Region

End Class
