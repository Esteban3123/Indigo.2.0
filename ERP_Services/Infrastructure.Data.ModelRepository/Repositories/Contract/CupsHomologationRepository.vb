'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CupsHomologationRepository
    Inherits GenericRepository(Of CupsHomologation)
    Implements ICupsHomologationRepository

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
    ''' lista las homologaciones del CUPS
    ''' </summary>
    ''' <param name="CupsId"></param>
    ''' <returns></returns>
    Public Function ListCupsHomologationByCupsId(CupsId As Integer, serviceType As Integer) As List(Of CupsHomologation) Implements ICupsHomologationRepository.ListCupsHomologationByCupsId
        Dim res = (From ch In _context.CupsHomologation.AsNoTracking()
                   Join ipss In _context.IPSService On ch.IPSServiceId Equals ipss.Id
                   Where ch.CupsEntityId = CupsId
                   Select ch).ToList()
        For Each item In res
            Dim cups = (From ce In _context.CUPSEntity.AsNoTracking() Where ce.Id = item.CupsEntityId Select ce).FirstOrDefault()
            item.CodeNameCupsEntity = cups.Code + " - " + cups.Description
            Dim ips = (From ipss In _context.IPSService.AsNoTracking() Where ipss.Id = item.IPSServiceId Select ipss).FirstOrDefault()
            item.IPSService = ips
            item.CodeNameIpsService = ips.Code + " - " + ips.Name
            item.Presentation = ips.Presentation
        Next
        Return res
    End Function

    ''' <summary>
    ''' lista los homologaciones del servicio IPS
    ''' </summary>
    ''' <param name="IpsServiceId"></param>
    ''' <returns></returns>
    Public Function ListCupsHomologationByIpsServiceId(IpsServiceId As Integer) As List(Of CupsHomologation) Implements ICupsHomologationRepository.ListCupsHomologationByIpsServiceId
        Dim res = (From ch In _context.CupsHomologation.AsNoTracking() Where ch.IPSServiceId = IpsServiceId Select ch).ToList()
        For Each item In res
            Dim cups = (From ce In _context.CUPSEntity.AsNoTracking() Where ce.Id = item.CupsEntityId Select ce).FirstOrDefault()
            item.CodeNameCupsEntity = cups.Code + " - " + cups.Description
            Dim ips = (From ipss In _context.IPSService.AsNoTracking() Where ipss.Id = item.IPSServiceId Select ipss).FirstOrDefault()
            item.CodeNameIpsService = ips.Code + " - " + ips.Name
        Next
        Return res
    End Function

    ''' <summary>
    ''' Gets the cups homologation ips service by cups entity identifier and service manual.
    ''' </summary>
    ''' <param name="cupsEntityId">The cups entity identifier.</param>
    ''' <param name="serviceManual">The service manual.</param>
    ''' <returns></returns>
    Public Function GetCupsHomologationIpsServiceByCupsEntityIdAndServiceManual(cupsEntityId As Integer, serviceManual As Byte) As List(Of CupsHomologation) Implements ICupsHomologationRepository.GetCupsHomologationIpsServiceByCupsEntityIdAndServiceManual
        Return (From co In _context.CupsHomologation.AsNoTracking().Include("IPSService").AsNoTracking()
                Join ips In _context.IPSService On co.IPSServiceId Equals ips.Id
                Where co.CupsEntityId = cupsEntityId AndAlso ips.ServiceManual = serviceManual
                Select co).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el listado de homologaciones por cupsEntityId y serviceManual
    ''' Este metodo es utilizado en medicalFeesCausation al momento de activar el check
    ''' del valor causado
    ''' </summary>
    ''' <param name="cupsEntityId"></param>
    ''' <param name="serviceManual"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsHomologationIpsServiceByCupsEntityIdAndServiceManualMedicalFeesCausation(cupsEntityId As Integer, serviceManual As Byte, isQx As Boolean) As List(Of CupsHomologation) Implements ICupsHomologationRepository.GetCupsHomologationIpsServiceByCupsEntityIdAndServiceManualMedicalFeesCausation
        Dim list As List(Of CupsHomologation)
        If isQx Then 'Si viene Qx
            list = (From co In _context.CupsHomologation.AsNoTracking.Include("IPSService").AsNoTracking()
                    Where co.CupsEntityId = cupsEntityId And co.IPSService.Presentation = 2
                    Select co).ToList
        Else 'Si viene NoQx
            list = (From co In _context.CupsHomologation.AsNoTracking.Include("IPSService").AsNoTracking()
                    Where co.CupsEntityId = cupsEntityId And (co.IPSService.Presentation = 1 Or co.IPSService.Presentation = 3)
                    Select co).ToList
        End If

        Dim listReturn = (From r In list Where r.IPSService.ServiceManual = serviceManual).ToList
        If listReturn IsNot Nothing AndAlso listReturn.Count > 0 Then
            For Each item As CupsHomologation In listReturn
                Dim cupsEntity = (From ce In _context.CUPSEntity.AsNoTracking Where ce.Id = item.CupsEntityId Select ce).FirstOrDefault
                item.CodeNameCupsEntity = cupsEntity.Code + " - " + cupsEntity.Description
                Dim ipsService = (From ips In _context.IPSService.AsNoTracking Where ips.Id = item.IPSServiceId Select ips).FirstOrDefault
                item.CodeNameIpsService = ipsService.Code + " - " + ipsService.Name
            Next
        End If
        Return listReturn
    End Function

    ''' <summary>
    ''' Obtiene el listado que tiene asociado
    ''' la entidad CUPS
    ''' </summary>
    ''' <param name="cupsEntityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListCupsHomologationByCupsEntityId(cupsEntityId As Integer) As List(Of CupsHomologation) Implements ICupsHomologationRepository.GetListCupsHomologationByCupsEntityId
        If cupsEntityId = 0 Then
            Throw New ArgumentNullException("cupsEntityId")
        End If
        Dim list = (From c In Me._context.CupsHomologation.AsNoTracking
                  Where c.CupsEntityId = cupsEntityId Select c).ToList()
        If list IsNot Nothing AndAlso list.Count > 0 Then

            For Each item As CupsHomologation In list
                Dim ipsService = (From i In _context.IPSService.AsNoTracking Where i.Id = item.IPSServiceId Select i).FirstOrDefault
                item.IPSServiceDescription = ipsService.Code + " - " + ipsService.Name

                Select Case ipsService.ServiceManual
                    Case 1
                        item.ServiceManualName = "ISS"
                    Case 3
                        item.ServiceManualName = "SOAT"
                End Select
            Next

            Return list
        Else
            Return Nothing
        End If
    End Function

End Class
