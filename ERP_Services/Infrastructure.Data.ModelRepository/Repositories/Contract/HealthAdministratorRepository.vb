'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class HealthAdministratorRepository
    Inherits GenericRepository(Of HealthAdministrator)
    Implements IHealthAdministratorRepository

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
    ''' Obtiene una entidad administradora de salud
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHealthAdministrator(code As String) As HealthAdministrator Implements IHealthAdministratorRepository.GetHealthAdministrator
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As HealthAdministrator In Me._context.HealthAdministrator
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = res.ThirdPartyId Select t).FirstOrDefault
            res.ThirdPartyDescription = thirdParty.Nit + " - " + thirdParty.Name

            res.OriginalValue = (From g In _context.HealthAdministrator.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New HealthAdministrator()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una entidad administradora de salus por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHealthAdministratorById(id As Integer) As HealthAdministrator Implements IHealthAdministratorRepository.GetHealthAdministratorById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.HealthAdministrator Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            Dim thirdPartyId = res(0).ThirdPartyId
            Dim thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = thirdPartyId Select t).FirstOrDefault()
            res(0).ThirdPartyDescription = thirdParty.Nit + " - " + thirdParty.Name
            res(0).OriginalValue = (From d As HealthAdministrator In Me._context.HealthAdministrator.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New HealthAdministrator()
        End If
    End Function

    Public Function GetFirstHealthAdministratorByEntityType(entityType As Byte) As HealthAdministrator Implements IHealthAdministratorRepository.GetFirstHealthAdministratorByEntityType
        Return (From h In _context.HealthAdministrator.AsNoTracking() Where h.EntityType = entityType Select h).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el tercero de la entidad
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyHealthAdministratorById(id As Integer, Optional tracking As Boolean = True) As ThirdParty Implements IHealthAdministratorRepository.GetThirdPartyHealthAdministratorById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.HealthAdministrator.AsNoTracking Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            Dim thirdPartyId = res(0).ThirdPartyId
            Dim thirdParty As ThirdParty
            If tracking = True Then
                thirdParty = (From t In _context.ThirdParty Where t.Id = thirdPartyId Select t).FirstOrDefault()
            Else
                thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = thirdPartyId Select t).FirstOrDefault()
            End If
            Return thirdParty
        Else
            Return New ThirdParty()
        End If
    End Function
End Class
