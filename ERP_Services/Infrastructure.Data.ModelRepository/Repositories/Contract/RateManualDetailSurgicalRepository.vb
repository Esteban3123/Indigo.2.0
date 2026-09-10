'***********************************************************************
' Assembly         : Infrastructure.Data.ContractRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 24/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class RateManualDetailSurgicalRepository
    Inherits GenericRepository(Of RateManualDetailSurgical)
    Implements IRateManualDetailSurgicalRepository


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
    ''' metodo para obtener un detalla del manual de tarifas quirurgico
    ''' </summary>
    Public Function GetSurgicalDetailServiceOrder(rateManualId As Integer, ipsService As Integer, surgicalGrouopId As Integer?, UVRNumber As Integer?, serviceManual As Integer) As RateManualDetailSurgical Implements IRateManualDetailSurgicalRepository.GetSurgicalDetailServiceOrder
        If serviceManual <= 2 Then
            Return (From rmds In _context.RateManualDetailSurgical.Include("RateManual").AsNoTracking().Include("UVRRange").AsNoTracking()
                       Where rmds.RateManualId = rateManualId And rmds.IPSServiceId = ipsService And
                       UVRNumber >= rmds.UVRRange.InitialUVR And UVRNumber <= rmds.UVRRange.EndUVR Select rmds).FirstOrDefault()
        Else
            Return (From rmds In _context.RateManualDetailSurgical.Include("RateManual").AsNoTracking()
                       Where rmds.RateManualId = rateManualId And rmds.IPSServiceId = ipsService And rmds.SurgicalGroupId = surgicalGrouopId Select rmds).FirstOrDefault()

        End If
    End Function

    
    ''' <summary>
    ''' obtiene una tarifa del detalle quirurgico por id
    ''' </summary>
    ''' <param name="rateManualDetailSurgicalId"></param>
    ''' <returns></returns>
    Public Function GetRateManualDetailSurgicalById(rateManualDetailSurgicalId As Integer) As RateManualDetailSurgical Implements IRateManualDetailSurgicalRepository.GetRateManualDetailSurgicalById
        Return (From rmds In _context.RateManualDetailSurgical.Include("RateManual").AsNoTracking() Where rmds.Id = rateManualDetailSurgicalId Select rmds).FirstOrDefault()
    End Function
End Class
