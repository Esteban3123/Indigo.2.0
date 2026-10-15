-- =============================================
-- Author:		Diego A. Roldán Lozano
-- Create date: 2018-10-09
-- Description:	Sp que elimina la programación de un activo
-- =============================================
CREATE PROCEDURE [Maintenance].[SP_DeleteMaintenanceProgramed]
	-- Add the parameters for the stored procedure here
	@ProgramatedDateIdsXml Xml
AS
BEGIN

	SET NOCOUNT ON;

	begin try
		declare @tmpProgramatedDateIds Table(RowId Int Identity(1, 1) primary key, ProgramatedDateId Int, MaintenancePlanAndMetrologyId Int)

		Insert Into @tmpProgramatedDateIds
		select t.x.value('Id[1]', 'Int'), mp.MaintenancePlanAndMetrologyId
		from @ProgramatedDateIdsXml.nodes('ProgramatedDateIds/Item') as t(x)
		inner join Maintenance.MaintenancePlanProgramated mp with(nolock) on mp.Id = t.x.value('Id[1]', 'Int')
		
		-- Validamos primero si existe programaciones con orden de trabajo o no
		declare @msgValidation varchar(max) = ''

		SELECT @msgValidation = Stuff((
			select N', ' + concat('Existe una órden de trabajo (', wo.Consecutive, ') para la programación del activo ', pa.Plate)
			from Maintenance.MaintenancePlanProgramated mp with(nolock)
			Inner Join @tmpProgramatedDateIds mid On mid.ProgramatedDateId = mp.Id
			inner join FixedAsset.FixedAssetPhysicalAsset pa with(nolock) on mp.FixedAssetPhysicalId = pa.id
			inner join Maintenance.MaintenancePlanAndMetrology mpm with(nolock) on mpm.Id = mp.MaintenancePlanAndMetrologyId
			inner join Maintenance.WorkOrder wo WITH(NOLOCK) ON wo.PhysicalAssetId = pa.Id			
		  FOR XML PATH(''),TYPE
		).value('text()[1]','nvarchar(max)'),1,2,N'')

		if IsNull(@msgValidation, '') <> '' begin
			select CAST(0 as bit) as StatusResult, @msgValidation as MessageResult
			return
		end
				
		Delete mp2
		From Maintenance.MaintenancePlanProgramated mp
		Inner Join @tmpProgramatedDateIds mid On mid.ProgramatedDateId = mp.Id
		Inner Join Maintenance.MaintenancePlanAndMetrology mpm On mpm.Id = mp.MaintenancePlanAndMetrologyId
		inner join Maintenance.MaintenancePlanProgramated mp2 on mp2.MaintenancePlanAndMetrologyId = mpm.Id And mp2.FixedAssetPhysicalId = mp.FixedAssetPhysicalId
		
		--miramos si el registro del plan metrology queda huerfano lo eliminamos		
		if exists (
			select mm.Id
			From Maintenance.MaintenancePlanAndMetrology mm
			left join Maintenance.MaintenancePlanProgramated mp on mp.MaintenancePlanAndMetrologyId = mm.Id
			left join (select distinct MaintenancePlanAndMetrologyId from @tmpProgramatedDateIds) ids on ids.MaintenancePlanAndMetrologyId = mm.Id
			where mp.Id is null
		) begin

			Delete mm
			From Maintenance.MaintenancePlanAndMetrology mm
			left join Maintenance.MaintenancePlanProgramated mp on mp.MaintenancePlanAndMetrologyId = mm.Id
			left join (select distinct MaintenancePlanAndMetrologyId from @tmpProgramatedDateIds) ids on ids.MaintenancePlanAndMetrologyId = mm.Id
			where mp.Id is null

		end

		select CAST(1 as bit) as StatusResult, '' as MessageResult
	end try
	begin catch
		select CAST(0 as bit) as StatusResult, ERROR_MESSAGE() as MessageResult
	end catch
END