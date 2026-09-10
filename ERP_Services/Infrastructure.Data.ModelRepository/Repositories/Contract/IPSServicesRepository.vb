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

Public Class IPSServicesRepository
    Inherits GenericRepository(Of IPSService)
    Implements IIPSServicesRepository


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
    ''' Obtiene un servicio ips
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIPSService(code As String) As IPSService Implements IIPSServicesRepository.GetIPSService
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As IPSService In Me._context.IPSService
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.AssociatedMaterialIPSServiceId IsNot Nothing Then
                Dim ips = (From ipss In _context.IPSService.AsNoTracking Where ipss.Id = res.AssociatedMaterialIPSServiceId Select ipss).FirstOrDefault
                res.AssociatedMaterialIPSServiceDescription = ips.Code + " - " + ips.Name
            End If

            If res.SurgicalGroupId IsNot Nothing Then
                Dim surgicalGroup = (From sg In _context.SurgicalGroup.AsNoTracking Where res.SurgicalGroupId = sg.Id Select sg).FirstOrDefault
                res.CodeNameSurgicalGroup = surgicalGroup.Code + " - " + surgicalGroup.Name
            End If

            If res.BillingConceptId IsNot Nothing Then
                Dim billingConcept = (From sg In _context.BillingConcept.AsNoTracking Where res.BillingConceptId = sg.Id Select sg).FirstOrDefault
                res.CodeNameBillingConcept = billingConcept.Code + " - " + billingConcept.Name
            End If

            res.OriginalValue = (From g In _context.IPSService.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New IPSService()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un servicio ips por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIPSServiceById(id As Integer, Optional tracking As Boolean = True) As IPSService Implements IIPSServicesRepository.GetIPSServiceById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res
        If tracking Then
            res = (From d In Me._context.IPSService.Include("GeneralLedgerIVA") Where d.Id = id Select d).FirstOrDefault
        Else
            res = (From d In Me._context.IPSService.AsNoTracking.Include("GeneralLedgerIVA").AsNoTracking() Where d.Id = id Select d).FirstOrDefault
        End If
        If res IsNot Nothing Then
            If tracking Then
                res.OriginalValue = (From d As IPSService In Me._context.IPSService.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            End If
            Return res
        Else
            Return New IPSService()
        End If
    End Function

    ''' <summary>
    ''' obtiene las homologaciones del servicio IPS
    ''' </summary>
    ''' <param name="idIPSService"></param>
    ''' <returns></returns>
    Public Function GetCupsHomologationByIPSServiceId(idIPSService As Integer) As List(Of CupsHomologation) Implements IIPSServicesRepository.GetCupsHomologationByIPSServiceId
        Dim res = (From ch In _context.CupsHomologation Where ch.IPSServiceId = idIPSService Select ch).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim cupsEntity = (From ce In _context.CUPSEntity.AsNoTracking() Where ce.Id = item.CupsEntityId Select ce).FirstOrDefault()
                item.CodeNameCupsEntity = cupsEntity.Code + " - " + cupsEntity.Description
            Next
            Return res
        Else
            Return New List(Of CupsHomologation)
        End If
    End Function

    ''' <summary>
    ''' Valida el copy paste de la rejilla de servicios ips
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <param name="ServiceManual"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_CopyAndPasteIPSService(xmlObject As String, ServiceManual As Integer) As List(Of SP_CopyAndPasteIPSService_Result) Implements IIPSServicesRepository.SP_CopyAndPasteIPSService
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteIPSService(xmlObject, ServiceManual).ToList
    End Function

End Class
