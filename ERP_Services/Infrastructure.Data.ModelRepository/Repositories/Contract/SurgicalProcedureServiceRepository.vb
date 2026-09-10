'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SurgicalProcedureServiceRepository
    Inherits GenericRepository(Of SurgicalProcedureService)
    Implements ISurgicalProcedureServiceRepository

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
    ''' obtiene procedimiento quirurgicos del servcio IPS
    ''' </summary>
    ''' <param name="idIPSService"></param>
    ''' <returns></returns>
    Public Function GetSurgicalProcedureServiceByIPSServiceId(idIPSService As Integer) As List(Of SurgicalProcedureService) Implements ISurgicalProcedureServiceRepository.GetSurgicalProcedureServiceByIPSServiceId
        Dim res = (From sps In _context.SurgicalProcedureService Join ips In _context.IPSService.AsNoTracking() On sps.IPSServiceId Equals ips.Id Where sps.IPSServiceParentId = idIPSService Order By ips.ServiceClass Select sps).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim ipsService = (From ipss In _context.IPSService.AsNoTracking().Include("GeneralLedgerIVA").AsNoTracking() Where ipss.Id = item.IPSServiceId Select ipss).FirstOrDefault()

                item.CodeNameService = ipsService.Code + " - " + ipsService.Name
                item.TaxPercent = If(ipsService?.IVAId Is Nothing OrElse Not ipsService?.TaxedProduct, 0, ipsService?.IVAId)
                item.IPSService = ipsService

                Select Case ipsService.ServiceClass
                    Case 1
                        item.ClassService = "Ninguno"
                    Case 2
                        item.ClassService = "Cirujano"
                    Case 3
                        item.ClassService = "Anestesiólogo"
                    Case 4
                        item.ClassService = "Ayudante"
                    Case 5
                        item.ClassService = "Derecho Sala"
                    Case 6
                        item.ClassService = "Materiales Sutura"
                    Case 7
                        item.ClassService = "Instrumentación Quirúrgica"
                End Select
            Next
            Return res
        Else
            Return New List(Of SurgicalProcedureService)
        End If
    End Function

    ''' <summary>
    ''' Obtiene el listado de detalles de los procedimientos quirurgicos filtrando por el ipsServiceParent
    ''' y que el agregado de ipsService tenga la misma clase
    ''' </summary>
    ''' <param name="IPSServiceParentdId"></param>
    ''' <param name="ServiceClass"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListSurgicalProcedureService(IPSServiceParentdId As Integer, ServiceClass As Byte) As List(Of SurgicalProcedureService) Implements ISurgicalProcedureServiceRepository.GetListSurgicalProcedureService
        Return (From sps In _context.SurgicalProcedureService.AsNoTracking
                          Join ips In _context.IPSService.AsNoTracking On sps.IPSServiceId Equals ips.Id
                          Where sps.IPSServiceParentId = IPSServiceParentdId And ips.ServiceClass = ServiceClass
                          Select sps).ToList
    End Function

    ''' <summary>
    ''' Función que valida si item qx esta parametrizado en la tabla DefinitionRateDetailSurgicalProcedures
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateIPSServiceInProcedures(DefinitionRateId As Integer, IPSServiceId As Integer, IPSServiceSurgicalId As Integer, CupsEntityId As Integer) As Integer Implements ISurgicalProcedureServiceRepository.ValidateIPSServiceInProcedures
        Return (From drd In _context.DefinitionRateDetail.AsNoTracking()
                Join drdsp In _context.DefinitionRateDetailSurgicalProcedures.AsNoTracking() On drdsp.DefinitionRateDetailId Equals drd.Id
                Where drd.DefinitionRateId = DefinitionRateId AndAlso drd.IPSServiceId = IPSServiceId _
                    AndAlso drdsp.IPSServiceId = IPSServiceSurgicalId AndAlso drd.CUPSEntityId = CupsEntityId
                Select drd.Id).FirstOrDefault()
    End Function

End Class
