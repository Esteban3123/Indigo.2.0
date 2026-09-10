'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DeferredCausationShareRepository
    Inherits GenericRepository(Of DeferredCausationShare)
    Implements IDeferredCausationShareRepository

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

    ''' <summary>
    ''' Obtiene una cuota de causacion diferida por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationShareById(id As Integer) As DeferredCausationShare Implements IDeferredCausationShareRepository.GetDeferredCausationShareById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In _context.DeferredCausationShare.AsNoTracking
                   Where d.Id = id
                   Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New DeferredCausationShare
        End If
    End Function

    ''' <summary>
    ''' Obtiene el listado de las cuotas de causacion que tiene asociado la cxp
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationShareByAccountPayableId(accountPayableId As Integer) As List(Of DeferredCausationShare) Implements IDeferredCausationShareRepository.GetDeferredCausationShareByAccountPayableId
        If accountPayableId = 0 Then
            Throw New ArgumentNullException("accountPayableId")
        End If
        Dim res = (From dc In _context.DeferredCausation.Include("DeferredCausationShare") Where dc.IdAccountPayable = accountPayableId Select dc).FirstOrDefault
        If res IsNot Nothing Then
            Dim ListDeferredCausationShare As List(Of DeferredCausationShare) = Nothing
            If res.DeferredCausationShare IsNot Nothing AndAlso res.DeferredCausationShare.Count > 0 Then
                ListDeferredCausationShare = res.DeferredCausationShare.ToList.FindAll(Function(item) item.Amortized = False)

                Dim mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.IdMainAccount Select ma).FirstOrDefault
                Dim costCenter As CostCenter = Nothing
                If res.IdCostCenter IsNot Nothing Then
                    CostCenter = (From cc In _context.CostCenter.AsNoTracking Where cc.Id = res.IdCostCenter Select cc).FirstOrDefault
                End If

                For Each itemShare As DeferredCausationShare In ListDeferredCausationShare
                    Select Case itemShare.PaymentMonth
                        Case 1
                            itemShare.MonthName = "Enero"
                        Case 2
                            itemShare.MonthName = "Febrero"
                        Case 3
                            itemShare.MonthName = "Marzo"
                        Case 4
                            itemShare.MonthName = "Abril"
                        Case 5
                            itemShare.MonthName = "Mayo"
                        Case 6
                            itemShare.MonthName = "Junio"
                        Case 7
                            itemShare.MonthName = "Julio"
                        Case 8
                            itemShare.MonthName = "Agosto"
                        Case 9
                            itemShare.MonthName = "Septiembre"
                        Case 10
                            itemShare.MonthName = "Octubre"
                        Case 11
                            itemShare.MonthName = "Noviembre"
                        Case 12
                            itemShare.MonthName = "Diciembre"
                    End Select
                    itemShare.MainAccountDescription = mainAccount.Number + " - " + mainAccount.Name
                    If costCenter IsNot Nothing Then
                        itemShare.CostCenterDescription = costCenter.Code + " - " + costCenter.Name
                    End If
                Next
            End If
            Return ListDeferredCausationShare
        Else
            Return Nothing
        End If
    End Function

End Class
