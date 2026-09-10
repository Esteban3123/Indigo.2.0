'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class AdministrationRouteRepository
    Inherits GenericRepository(Of AdministrationRoute)
    Implements IAdministrationRouteRepository

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
    ''' Obtiene una via de admistracion
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdministrationRoute(code As String) As AdministrationRoute Implements IAdministrationRouteRepository.GetAdministrationRoute
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As AdministrationRoute In Me._context.AdministrationRoute
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            'Dim pharma = (From p In _context.PharmaceuticalForm.AsNoTracking Where p.Id = res.PharmaceuticalFormId Select p).FirstOrDefault
            'res.PharmaceuticalFormDescription = pharma.Code + " - " + pharma.Name

            res.OriginalValue = (From g In _context.AdministrationRoute.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New AdministrationRoute()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una via de administracion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdministrationRouteById(id As Integer) As AdministrationRoute Implements IAdministrationRouteRepository.GetAdministrationRouteById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.AdministrationRoute Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As AdministrationRoute In Me._context.AdministrationRoute.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New AdministrationRoute()
        End If
    End Function

    Public Function SaveAdministrationRoute(PharmaceuticalFormId As Integer?, Code As String, Description As String, Status As Boolean, CodeUser As String) As SP_SaveAdministrationRoute_Result Implements IAdministrationRouteRepository.SaveAdministrationRoute
        Return _context.SP_SaveAdministrationRoute(PharmaceuticalFormId, Code, Description, Status, CodeUser).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function SP_DeleteAdministrationRoute(Id As Integer) As SP_DeleteAdministrationRoute_Result Implements IAdministrationRouteRepository.SP_DeleteAdministrationRoute
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeleteAdministrationRoute(Id).SingleOrDefault
    End Function

End Class
