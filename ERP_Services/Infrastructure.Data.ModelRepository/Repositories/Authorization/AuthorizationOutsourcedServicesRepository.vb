'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class AuthorizationOutsourcedServicesRepository
    Inherits GenericRepository(Of AuthorizationOutsourcedServices)
    Implements IAuthorizationOutsourcedServicesRepository

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
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationOutsourcedServices(Code As String) As AuthorizationOutsourcedServices Implements IAuthorizationOutsourcedServicesRepository.GetAuthorizationOutsourcedServices
        Dim res = (From bg In _context.AuthorizationOutsourcedServices Where bg.Code = Code Select bg).FirstOrDefault()
        If res IsNot Nothing Then

            If res.ThirdPartyId IsNot Nothing Then
                res.ThirdPartyDescription = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = res.ThirdPartyId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            End If

            res.OriginalValue = (From bg In _context.AuthorizationOutsourcedServices.AsNoTracking() Where bg.Code = Code Select bg).FirstOrDefault()
            Return res
        Else
            Return New AuthorizationOutsourcedServices
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationOutsourcedServicesById(Id As Integer) As AuthorizationOutsourcedServices Implements IAuthorizationOutsourcedServicesRepository.GetAuthorizationOutsourcedServicesById
        Dim res = (From bg In _context.AuthorizationOutsourcedServices Where bg.Id = Id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New AuthorizationOutsourcedServices
        End If
    End Function

    ''' <summary>
    ''' Proceso de autorización de servicios tercerizados
    ''' </summary>
    ''' <param name="xmlData"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveAuthorizationOutsourcedServices(xmlData As String, codeUser As String) As SP_SaveAuthorizationOutsourcedServices_Result Implements IAuthorizationOutsourcedServicesRepository.SP_SaveAuthorizationOutsourcedServices
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveAuthorizationOutsourcedServices(xmlData, codeUser).SingleOrDefault
    End Function

End Class
