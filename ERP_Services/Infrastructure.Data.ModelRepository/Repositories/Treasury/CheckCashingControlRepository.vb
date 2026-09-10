'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Hector Rodriguez R
' Created          : 24-10-2019
'
' Copyright        : (c) . All rights reserved.
' About            : GRUPOMEDILASER PBI2613
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class CheckCashingControlRepository
    Inherits GenericRepository(Of CheckCashingControl)
    Implements ICheckCashingControlRepository

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
    Public Function GetCheckCashingControlById(id As Integer) As CheckCashingControl Implements ICheckCashingControlRepository.GetCheckCashingControlById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From ir In Me._context.CheckCashingControl.Include("CheckCashingControlDetail").Include("EntityBankAccounts").Include("EntityBankAccounts.Bank") Where ir.Id = id
                   Select ir).FirstOrDefault

        If res IsNot Nothing Then
            res.OriginalValue = (From ir In Me._context.CheckCashingControl.AsNoTracking Where ir.Id = id Select ir).FirstOrDefault
            Return res
        Else
            Return New CheckCashingControl
        End If

    End Function
End Class
