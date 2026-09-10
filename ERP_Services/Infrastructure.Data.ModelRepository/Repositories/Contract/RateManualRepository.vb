'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Base

Public Class RateManualRepository
    Inherits GenericRepository(Of RateManual)
    Implements IRateManualRepository

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
    ''' Obtiene un manual tarifario por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRateManual(code As String) As RateManual Implements IRateManualRepository.GetRateManual
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As RateManual In Me._context.RateManual.Include("SurgeriesPercentageManual").Include("RateManualDetail").Include("RateManualDetailSurgical")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault

        If res IsNot Nothing Then

            If res.ContractMinimumWageId IsNot Nothing Then
                Dim contractMinimumWage = (From cmw In _context.ContractMinimumWage.AsNoTracking Where cmw.Id = res.ContractMinimumWageId Select cmw).FirstOrDefault
                res.ContractMinimumWageDescription = contractMinimumWage.Code + " - " + contractMinimumWage.Name
            End If

            If res.MaterialNoBloodyIPSServiceId IsNot Nothing Then
                Dim ipsS = (From ips In _context.IPSService.AsNoTracking Where ips.Id = res.MaterialNoBloodyIPSServiceId Select ips).FirstOrDefault
                res.MaterialNoBloodyIPSServiceDescription = ipsS.Code + " - " + ipsS.Name
            End If

            If res.RateManualDetail IsNot Nothing AndAlso res.RateManualDetail.Count > 0 Then
                For Each item As RateManualDetail In res.RateManualDetail
                    Dim ipsService = (From ips In _context.IPSService.AsNoTracking.Include("GeneralLedgerIVA").AsNoTracking Where ips.Id = item.IPSServiceId Select ips).FirstOrDefault
                    item.IPSServiceDescription = ipsService.Code + " - " + ipsService.Name
                    item.IVAValue = If(ipsService.GeneralLedgerIVA Is Nothing, 0, ipsService.GeneralLedgerIVA.Percentage)
                Next
            End If

            If res.RateManualDetailSurgical IsNot Nothing AndAlso res.RateManualDetailSurgical.Count > 0 Then
                For Each item As RateManualDetailSurgical In res.RateManualDetailSurgical
                    Dim ipsService = (From ips In _context.IPSService.AsNoTracking Where ips.Id = item.IPSServiceId Select ips).FirstOrDefault
                    item.IPSServiceDescription = ipsService.Code + " - " + ipsService.Name

                    If res.Type <> eRateManuelType.Institutional Then
                        If item.SurgicalGroupId IsNot Nothing Then
                            Dim surgicalGroup = (From sg In _context.SurgicalGroup.AsNoTracking Where sg.Id = item.SurgicalGroupId Select sg).FirstOrDefault
                            item.SurgicalGroupDescription = surgicalGroup.Code + " - " + surgicalGroup.Name
                        Else
                            Dim uvrRange = (From ur In _context.UVRRange.AsNoTracking Where ur.Id = item.UVRRangeId Select ur).FirstOrDefault
                            item.UVRRangeDescription = uvrRange.Code + " - " + uvrRange.Name
                        End If
                    End If
                Next
            End If

            Return res
        Else
            Return New RateManual()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un manual tarifario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRateManualById(id As Integer, Optional tracking As Boolean = True) As RateManual Implements IRateManualRepository.GetRateManualById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res As List(Of RateManual)
        If tracking Then
            res = (From d In Me._context.RateManual Where d.Id = id Select d).ToList
        Else
            res = (From d In Me._context.RateManual.AsNoTracking Where d.Id = id Select d).ToList
        End If
        If res.Count > 0 Then
            res(0).OriginalValue = (From d As RateManual In Me._context.RateManual.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return res(0)
        Else
            Return New RateManual()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un manual tarifario por id con los agregados de rateManualDetail y rateManualDetailSurgical
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRateManualByIdWithAggregates(id As Integer) As RateManual Implements IRateManualRepository.GetRateManualByIdWithAggregates
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From rm In _context.RateManual.AsNoTracking.Include("RateManualDetail").AsNoTracking.Include("RateManualDetailSurgical").AsNoTracking Where rm.Id = id Select rm).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Valida el copiar y pegar de las rejillas del form de manual de tarifas
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <param name="ServiceManual"></param>
    ''' <param name="GridOption"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_CopyAndPasteRateManual(xmlObject As String, ServiceManual As Integer, GridOption As Integer) As List(Of SP_CopyAndPasteRateManual_Result) Implements IRateManualRepository.SP_CopyAndPasteRateManual
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteRateManual(xmlObject, ServiceManual, GridOption).ToList
    End Function

End Class
