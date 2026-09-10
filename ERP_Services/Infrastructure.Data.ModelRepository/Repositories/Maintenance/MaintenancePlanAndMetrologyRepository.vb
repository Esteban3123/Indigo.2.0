Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class MaintenancePlanAndMetrologyRepository
    Inherits GenericRepository(Of MaintenancePlanAndMetrology)
    Implements IMaintenancePlanAndMetrologyRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Mantenimiento
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function RemoveRange(ids As List(Of Integer)) As Boolean Implements IMaintenancePlanAndMetrologyRepository.RemoveRange
        Try
            Me._context.MaintenancePlanAndMetrology.RemoveRange(_context.MaintenancePlanAndMetrology.Where(Function(m) ids.Contains(m.Id)).ToList())
            'Me._context.Commit()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SP_DeleteMaintenanceProgramed(maintenancePlanAndMetrologyIds As String) As SP_DeleteMaintenanceProgramed_Result Implements IMaintenancePlanAndMetrologyRepository.SP_DeleteMaintenanceProgramed
        Return Me._context.SP_DeleteMaintenanceProgramed(maintenancePlanAndMetrologyIds).FirstOrDefault()
    End Function

    Public Function GetMaintenancePlanAndMetrology(PhysicalAssetId As Integer, MaintenanceprotocolId As Integer) As MaintenancePlanAndMetrology Implements IMaintenancePlanAndMetrologyRepository.GetMaintenancePlanAndMetrology
        Dim query = (From m In _context.MaintenancePlanAndMetrology
                     Join p In _context.MaintenancePlanProgramated On p.MaintenancePlanAndMetrologyId Equals m.Id
                     Where p.FixedAssetPhysicalId = PhysicalAssetId Select m).FirstOrDefault()
        Return query
    End Function

    Public Function GetMaintenancePlanAndMetrologyById(PlanMaintenanceId As Integer) As MaintenancePlanAndMetrology Implements IMaintenancePlanAndMetrologyRepository.GetMaintenancePlanAndMetrologyById
        Dim query = (From m In _context.MaintenancePlanAndMetrology.Include("MaintenancePlanProgramated") Where m.Id = PlanMaintenanceId Select m).FirstOrDefault()
        If query IsNot Nothing Then
            If query.ProtocolMaintenanceId IsNot Nothing Then
                Dim protocolM = (From p In _context.MaintenanceProtocol.AsNoTracking() Where p.Id = query.ProtocolMaintenanceId Select p).FirstOrDefault()
                query.ProtocolMaintenanceCodeName = $"{protocolM.Code} - {protocolM.Name}"

                Dim responsibleM = (From r In _context.MaintenanceResponsible.AsNoTracking()
                                    Join t In _context.ThirdParty.AsNoTracking() On r.ThirdPartyId Equals t.Id
                                    Where r.Id = query.ResponsibleMaintenanceId Select t).FirstOrDefault()
                query.ResponsibleMaintenanceCodeName = $"{responsibleM.Nit} - {responsibleM.Name}"
            End If
            If query.ProtocolMetrologyId IsNot Nothing Then
                Dim protocolG = (From p In _context.MaintenanceProtocol.AsNoTracking() Where p.Id = query.ProtocolMetrologyId Select p).FirstOrDefault()
                query.ProtocolMetrologyCodeName = $"{protocolG.Code} - {protocolG.Name}"

                Dim responsibleG = (From r In _context.MaintenanceResponsible.AsNoTracking()
                                    Join t In _context.ThirdParty.AsNoTracking() On r.ThirdPartyId Equals t.Id
                                    Where r.Id = query.ResponsibleMetrologyId Select t).FirstOrDefault()
                query.ResponsibleMetrologyCodeName = $"{responsibleG.Nit} - {responsibleG.Name}"
            End If

            If query.MaintenancePlanProgramated IsNot Nothing Then
                For Each item In query.MaintenancePlanProgramated
                    Dim fixedAsset = (From pa In _context.FixedAssetPhysicalAsset.AsNoTracking()
                                      Where pa.Id = item.FixedAssetPhysicalId
                                      Select pa).FirstOrDefault()
                    Dim article = (From a In _context.FixedAssetItem.AsNoTracking() Where a.Id = fixedAsset.ItemId Select a).FirstOrDefault()
                    item.FixedAssetPhysicalAssetName = $"({fixedAsset.Plate}) {article.Code} - {article.Description}"
                    item.TypeName = If(item.ProgramType = 1, "Mantenimiento", "Metrologia")
                Next
            End If
        End If

        Return query
    End Function

End Class
