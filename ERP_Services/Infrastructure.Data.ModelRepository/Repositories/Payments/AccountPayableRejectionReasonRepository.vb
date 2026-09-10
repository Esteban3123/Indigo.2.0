'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class AccountPayableRejectionReasonRepository
    Inherits GenericRepository(Of AccountPayableRejectionReason)
    Implements IAccountPayableRejectionReasonRepository

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
    ''' Obtiene un rechazo de facturas
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableRejectionReason(code As String, Optional tracking As Boolean = True) As AccountPayableRejectionReason Implements IAccountPayableRejectionReasonRepository.GetAccountPayableRejectionReason
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As AccountPayableRejectionReason In Me._context.AccountPayableRejectionReason Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From d As AccountPayableRejectionReason In Me._context.AccountPayableRejectionReason.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New AccountPayableRejectionReason()
        End If
    End Function

    ''' <summary>
    ''' Consulta el concepto de nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableRejectionReasonById(id As String, Optional tracking As Boolean = True) As AccountPayableRejectionReason Implements IAccountPayableRejectionReasonRepository.GetAccountPayableRejectionReasonById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.AccountPayableRejectionReason Where d.Id = id Select d)
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As AccountPayableRejectionReason In Me._context.AccountPayableRejectionReason.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New AccountPayableRejectionReason()
        End If
    End Function
    ''' <summary>
    ''' Lista las razones de rechazo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRejectionReason() As List(Of AccountPayableRejectionReason) Implements IAccountPayableRejectionReasonRepository.ListRejectionReason
        Dim busqueda = (From e In Me._context.AccountPayableRejectionReason
                        Select e).ToList()
        Return busqueda
    End Function
End Class
