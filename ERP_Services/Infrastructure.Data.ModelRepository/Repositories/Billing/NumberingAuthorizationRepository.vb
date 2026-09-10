'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Andres Alarcon
' Created          : 2022-08-02
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class NumberingAuthorizationRepository
    Inherits GenericRepository(Of NumberingAuthorization)
    Implements INumberingAuthorizationRepository

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
    Public Function ListNumberingAuthorizationByUserCode(userCode As String) As List(Of NumberingAuthorization) Implements INumberingAuthorizationRepository.ListNumberingAuthorizationByUserCode
        Dim res = (From a As NumberingAuthorization
                   In Me._context.NumberingAuthorization.Include("NumberingAuthorizationUser")
                   Where a.Status = True And a.NumberingAuthorizationUser.Any(Function(u) u.UserCode.Equals(userCode))
                   Select a).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of NumberingAuthorization)()
        End If
    End Function

    ''' <summary>
    ''' Gets the billing authorization by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetNumberingAuthorizationById(Id As Integer, Optional tracking As Boolean = True) As NumberingAuthorization Implements INumberingAuthorizationRepository.GetNumberingAuthorizationById
        Dim res As NumberingAuthorization
        If tracking = True Then
            res = (From a As NumberingAuthorization
                   In Me._context.NumberingAuthorization
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        Else
            res = (From a As NumberingAuthorization
                   In Me._context.NumberingAuthorization.AsNoTracking()
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From b In _context.NumberingAuthorization.AsNoTracking() Where b.Id = Id Select b).FirstOrDefault()
            Return res
        Else
            Return New NumberingAuthorization
        End If
    End Function

    ''' <summary>
    ''' Obtiene una autorización de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNumberingAuthorizationByCode(code As String) As NumberingAuthorization Implements INumberingAuthorizationRepository.GetNumberingAuthorizationByCode
        Dim res = (From ba In _context.NumberingAuthorization.Include("NumberingAuthorizationUser") Where ba.Code.Equals(code.Trim()) Select ba).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From ba In _context.NumberingAuthorization.AsNoTracking.Include("NumberingAuthorizationUser").AsNoTracking
                                 Where ba.Code.Equals(code.Trim()) Select ba).FirstOrDefault

            Return res
        Else
            Return New NumberingAuthorization()
        End If

    End Function

End Class
