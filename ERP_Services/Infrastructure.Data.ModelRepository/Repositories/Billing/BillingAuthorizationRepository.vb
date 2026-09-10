'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-27
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class BillingAuthorizationRepository
    Inherits GenericRepository(Of BillingAuthorization)
    Implements IBillingAuthorizationRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListBillingAuthorizationByUserCode(userCode As String) As List(Of BillingAuthorization) Implements IBillingAuthorizationRepository.ListBillingAuthorizationByUserCode
        Dim res = (From a As BillingAuthorization
                   In Me._context.BillingAuthorization.Include("BillingAuthorizationUser")
                   Where a.Status = True And a.BillingAuthorizationUser.Any(Function(u) u.UserCode.Equals(userCode))
                   Select a).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of BillingAuthorization)()
        End If
    End Function

    ''' <summary>
    ''' Gets the billing authorization by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetBillingAuthorizationById(Id As Integer, Optional tracking As Boolean = True) As BillingAuthorization Implements IBillingAuthorizationRepository.GetBillingAuthorizationById
        Dim res As BillingAuthorization
        If tracking = True Then
            res = (From a As BillingAuthorization
                   In Me._context.BillingAuthorization
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        Else
            res = (From a As BillingAuthorization
                   In Me._context.BillingAuthorization.AsNoTracking()
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From b In _context.BillingAuthorization.AsNoTracking() Where b.Id = Id Select b).FirstOrDefault()
            Return res
        Else
            Return New BillingAuthorization
        End If
    End Function

    ''' <summary>
    ''' Obtiene una autorización de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingAuthorizationByCode(code As String) As BillingAuthorization Implements IBillingAuthorizationRepository.GetBillingAuthorizationByCode
        Dim res = (From ba In _context.BillingAuthorization.Include("BillingAuthorizationUser") Where ba.Code.Equals(code.Trim()) Select ba).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From ba In _context.BillingAuthorization.AsNoTracking.Include("BillingAuthorizationUser").AsNoTracking
                                 Where ba.Code.Equals(code.Trim()) Select ba).FirstOrDefault

            Return res
        Else
            Return New BillingAuthorization()
        End If

    End Function

End Class
