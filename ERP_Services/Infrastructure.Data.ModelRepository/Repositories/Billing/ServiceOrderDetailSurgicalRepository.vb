'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class ServiceOrderDetailSurgicalRepository
    Inherits GenericRepository(Of ServiceOrderDetailSurgical)
    Implements IServiceOrderDetailSurgicalRepository

#Region "Fields"
    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' lista los detalles quirurgicos de la ordern de servicio
    ''' </summary>
    ''' <param name="idServiceOrderDetail"></param>
    ''' <returns></returns>
    Public Function ListServiceOrderDetailSurgicalByIdDerviceOrderDetail(idServiceOrderDetail As Integer) As List(Of ServiceOrderDetailSurgical) Implements IServiceOrderDetailSurgicalRepository.ListServiceOrderDetailSurgicalByIdDerviceOrderDetail
        Dim res = (From sods In _context.ServiceOrderDetailSurgical Where sods.ServiceOrderDetailId = idServiceOrderDetail Select sods).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim ips = (From ipss In _context.IPSService.AsNoTracking() Where ipss.Id = item.IPSServiceId Select ipss).FirstOrDefault()
                item.CodeNameIpsService = ips.Code + " - " + ips.Name
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' Valida que la causacion no exista en alguna liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId As Integer) As Boolean Implements IServiceOrderDetailSurgicalRepository.ValidateDeleteServiceOrderDetailSurgical
        Dim ListDetail As List(Of MedicalFeesLiquidationDetail) = (From mfld In _context.MedicalFeesLiquidationDetail.AsNoTracking.Include("MedicalFeesLiquidation").AsNoTracking
                                                                   Where mfld.MedicalFeesCausationId = MedicalFeesCausationId
                                                                   Select mfld).ToList
        If ListDetail IsNot Nothing AndAlso ListDetail.Count > 0 Then
            For Each item As MedicalFeesLiquidationDetail In ListDetail
                If item.MedicalFeesLiquidation.Status <> 3 Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    ''' <summary>
    ''' Obtiene el detalle quirurgico por id
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgicalId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceOrderDetailSurgicalById(ServiceOrderDetailSurgicalId As Integer, Optional tracking As Boolean = True) As ServiceOrderDetailSurgical Implements IServiceOrderDetailSurgicalRepository.GetServiceOrderDetailSurgicalById
        If ServiceOrderDetailSurgicalId = 0 Then
            Throw New ArgumentNullException("ServiceOrderDetailSurgicalId")
        End If
        Dim res
        If tracking Then
            res = (From sods In _context.ServiceOrderDetailSurgical Where sods.Id = ServiceOrderDetailSurgicalId Select sods).FirstOrDefault
        Else
            res = (From sods In _context.ServiceOrderDetailSurgical.AsNoTracking().Include("ServiceOrderDetail").AsNoTracking().Include("ServiceOrderDetail.RateManual").AsNoTracking() Where sods.Id = ServiceOrderDetailSurgicalId Select sods).FirstOrDefault
        End If
        Return res
    End Function

End Class
