'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 19-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Class SchedulePaymentDetailRepository
    Inherits GenericRepository(Of SchedulePaymentDetail)
    Implements ISchedulePaymentDetailRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' obtiene el detalle de la programacion de pagos por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetSchedulePaymentDetailById(Id As Integer) As SchedulePaymentDetail Implements ISchedulePaymentDetailRepository.GetSchedulePaymentDetailById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From spd In _context.SchedulePaymentDetail Where spd.Id = Id Select spd).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From spd In _context.SchedulePaymentDetail.AsNoTracking() Where spd.Id = Id Select spd).FirstOrDefault()
            Return query
        Else
            Return New SchedulePaymentDetail()
        End If
    End Function

    ''' <summary>
    ''' Lists the schedule payment detail by schedule payment id1.
    ''' </summary>
    ''' <param name="SchedulePaymentId">The schedule payment identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">SchedulePaymentId</exception>
    Public Function ListSchedulePaymentDetailBySchedulePaymentId1(SchedulePaymentId As Integer) As List(Of SchedulePaymentDetail) Implements ISchedulePaymentDetailRepository.ListSchedulePaymentDetailBySchedulePaymentId
        If SchedulePaymentId = 0 Then
            Throw New ArgumentNullException("SchedulePaymentId")
        End If
        Return (From spd In _context.SchedulePaymentDetail.AsNoTracking().Include("ThirdParty").AsNoTracking() Where spd.SchedulePaymentId = SchedulePaymentId Select spd).ToList()
    End Function

    Public Function IsAccountPayableShareInSchedulePaymentDetailActive(AccountPayableShareId As Integer) As Domain.Base.Entities.ActionResult Implements ISchedulePaymentDetailRepository.IsAccountPayableShareInSchedulePaymentDetailActive
        Dim query = (From sp In _context.SchedulePayment.AsNoTracking()
                     Join spd In _context.SchedulePaymentDetail.AsNoTracking() On spd.SchedulePaymentId Equals sp.Id
                     Where spd.AccountPayableShareId = AccountPayableShareId And sp.Status = 1
                     Select sp).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            Return New ActionResult With {.StateResult = True, .Message = String.Join("-", query.Select(Function(x) x.Code).ToList())}
        Else
            Return New ActionResult With {.StateResult = False}
        End If
    End Function

End Class