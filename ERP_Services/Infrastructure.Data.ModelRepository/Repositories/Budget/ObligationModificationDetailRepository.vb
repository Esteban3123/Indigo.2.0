'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ObligationModificationDetailRepository
    Inherits GenericRepository(Of ObligationModificationDetail)
    Implements IObligationModificationDetailRepository

    'Contexto 
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de una modificacion de obligacion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObligationModificationDetailById(Id As Integer) As ObligationModificationDetail Implements IObligationModificationDetailRepository.GetObligationModificationDetailById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.ObligationModificationDetail Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As ObligationModificationDetail In Me._context.ObligationModificationDetail.AsNoTracking() Where d.Id = Id Select d).FirstOrDefault

            Return res
        Else
            Return New ObligationModificationDetail
        End If
    End Function

#End Region

End Class
