'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Hector Rodriguez R
' Created          : 21/11/2019
'
' Copyright        : (c) . All rights reserved.
' About            : GRUPOMEDILASER PBI2958
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class CashFlowReclassificationRepository
    Inherits GenericRepository(Of CashFlowReclassification)
    Implements ICashFlowReclassificationRepository

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub


    ''' <summary>
    ''' Obtiene una solicitud de compra de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCashFlowReclassificationById(id As Integer) As CashFlowReclassification Implements ICashFlowReclassificationRepository.GetCashFlowReclassificationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From ir In Me._context.CashFlowReclassification.Include("CashFlowReclassificationDetail") Where ir.Id = id
                   Select ir).FirstOrDefault

        If res IsNot Nothing Then
            res.OriginalValue = (From ir In Me._context.CashFlowReclassification.AsNoTracking Where ir.Id = id Select ir).FirstOrDefault
            Return res
        Else
            Return New CashFlowReclassification
        End If

    End Function
End Class
