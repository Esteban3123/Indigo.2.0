

-- =============================================
-- Author:		Duván Mejia Cortes
-- Create date: 2021-08-20
-- Description:	Procedimiento para listar las campañas con los estados de segun la cantidad del los paquetes 
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_ListViewItemsCampaigns] 
	@campaignDetailId AS integer
AS
BEGIN
	SET NOCOUNT ON;
	/************************************* VARIABLES *************************************/

	--Tabla para devolver los resultados
	DECLARE @TableResult AS TABLE
	(
		Id INT IDENTITY(1,1),
		code VARCHAR(MAX) NOT NULL,
		RequestCode VARCHAR(MAX) NOT NULL,
		RequestType Int NOT NULL,
		RequestTypeName VARCHAR(MAX) NOT NULL,
		RequestDate DATETIME,
		ItemId INT NOT NULL,
		ItemCodeName VARCHAR(max) NOT NULL,	
		RequestQuantity INT, 
		CampaignDetailId INT,
		StatusPP  INT  NULL,
		StatusPT  INT  NULL,
		StatusPL  INT  NULL,
		StatusPPR INT  NULL,
		StatusPR  INT  NULL,
		StatusPA  INT  NULL,
		ItemType Int NOT NULL,
		ItemTypeName VARCHAR(MAX) NOT NULL,
		StatusHCPRESCRA INT NOT NULL,
		IsReadjustment INT NULL,
		UnitDoseTypeId int null,
		Msclass int null,
		HasDefectsProduction bit Null
	)

	BEGIN TRY

		/*****************************************  Pivot Table   *************************************************************/
 		DECLARE @TableCountStatusDX AS TABLE
		(
			Id INT IDENTITY(1,1),
			RequestMixingStationDetailId int,
			packageId int, 
			packagePersonalizedId int,
			CampaignDetailId Int,
			StatusPP  INT  NULL,
			StatusPT  INT  NULL,
			StatusPL  INT  NULL,
			StatusPPR INT  NULL,
			StatusPR  INT  NULL,
			StatusPA  INT  NULL
		)

		INSERT INTO @TableCountStatusDX
		SELECT * FROM
		(
			Select Count(rpds.Status) as Quantity
				, rpds.Status
				, rpd.Id as RequestMixingStationDetailId
				, rpds.PackageId
				, rpds.PackagePersonalizedId
				, rpd.CampaignDetailId
			From MixingStation.RequestPackageDetailStatus rpds with(nolock)
			Inner Join MixingStation.RequestMixingStationDetail rpd with(nolock) On rpd.id = rpds.RequestMixingStationDetailId 
			WHERE rpd.CampaignDetailId = @campaignDetailId
			Group By rpd.Id, rpds.[Status], rpds.PackageId, rpds.PackagePersonalizedId, rpd.CampaignDetailId
		) AS SourceTable Pivot(Max(Quantity) For [Status] In ([1],[2],[3],[4],[5],[6])) AS PivotTable
		
		/*****************************************  Consulta Principal   *****************************************************/

		INSERT Into @TableResult 
		--select CONCAT(r.Code, '-', IIF(rd.PackageId is not null, 'P', 'M'), '-', ISNULL(rd.PackageId, rd.ATCId)) Id, 
		select rd.Id Id, 
		r.Code RequestCode, rd.Source RequestType, 
		case rd.Source 
			when 1 then 'Orden Médica'
			when 2 then 'Solicitud Externa Paciente'
			when 3 then 'Solicitud Externa Maquila'
			when 4 then 'Solicitud Inventario'
		end RequestTypeName, r.RequestDate,
		ISNULL(pp.Id, ISNULL(p.Id, a.Id)) ItemId,
		CASE 
			WHEN udt.MSClass = 2 THEN
				p.Code + ' - ' + p.Name
			ELSE 
				ISNULL(pp.Code + ' - ' + pp.Description, ISNULL(p.Code + ' - ' + p.Description, a.Code + ' - ' + a.Name))
		END ItemCodeName,
		rd.Quantity RequestQuantity, 
		ISNULL(rdp.CampaignDetailId, rd.CampaignDetailId) CampaignDetailId,
		ISNULL(dx.StatusPP, 0) StatusPP,
		ISNULL(dx.StatusPT, 0) StatusPT,
		ISNULL(dx.StatusPL, 0) StatusPL,
		ISNULL(dx.StatusPPR, 0) StatusPPR,
		ISNULL(dx.StatusPR, 0) StatusPR,
		ISNULL(dx.StatusPA, 0) StatusPA,
		IIF(pp.Id is not null, 3, IIF(p.Id is not null, 1, 2)) ItemType, 
		IIF(pp.Id is not null, 'Paquete Personalizado', IIF(p.Id is not null, 'Paquete', 'Medicamento')) ItemTypeName,
		ISNULL(d.PREESTADO, 0) StatusHCPRESCRA,
	    Iif(re.IsReadjustment =1,1 ,0) as IsReadjustment,
		cd.UnitDoseTypeId,
		udt.MSClass,
		cast(iif((select count(di.ProductionChemical) from MixingStation.DefectClassificationItem di (nolock)
			JOIN MixingStation.DefectsUnitDoseType dut ON dut.Id_DefectsClassificationItem = di.Id
			JOIN MixingStation.UnitDoseType ud ON ud.Id = dut.Id_UnitDoseType ---
			where di.ProductionChemical = 1 and di.State = 1 and ud.MSClass = udt.MSClass) > 0, 1, 0) as bit) as HasDefectsProduction
		from MixingStation.RequestMixingStationDetail rd
		inner join MixingStation.RequestMixingStation r on r.Id = rd.RequestMixingStationId
		inner join MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
		left join (
			select RequestMixingStationDetailId, SUM(Quantity) Quantity, CampaignDetailId, string_agg(Id, ', ') StringIds
			from MixingStation.RequestMixingStationDetailPatients
			where CampaignDetailId = @campaignDetailId and Status <> 3
			group by RequestMixingStationDetailId, CampaignDetailId
		) rdp on rdp.RequestMixingStationDetailId = rd.Id
		LEFT join MixingStation.CampaignDetail cd ON cd.Id = @campaignDetailId
		left join MixingStation.Package p on p.Id = rd.PackageId
		left join Inventory.ATC a on a.Id = rd.ATCId
		left join MixingStation.PackagePersonalized pp on pp.Id = rd.PackagePersonalizedId
		left join .ADCENATEN cci on cci.CODCENATE = rd.CareCenterCode and rd.Source in (1, 4)
		left join MixingStation.ExternalCareCenter cce on cce.Code = rd.CareCenterCode and rd.Source in (2, 3)
		left join MixingStation.ProductionLine pl on pl.Id = rd.ProductionLineId
		left join (
			select p.RequestMixingStationDetailId, MAX(a.ID) HCPRESCRAId, p.CampaignDetailId
			from MixingStation.RequestMixingStationDetailPatients p
			inner join .HCFARMEPD h on h.ID = p.EntityId
			inner join .HCPRESCRA a on h.IdSourceTable = a.ID
			where p.CampaignDetailId = @campaignDetailId And p.EntityName = 'HCFARMEPD' And h.SourceTable = 'HCPRESCRA' And p.Status <> 3
			group by p.RequestMixingStationDetailId, p.CampaignDetailId
		) data on data.RequestMixingStationDetailId = rd.Id and data.CampaignDetailId = @campaignDetailId
		left join .HCPRESCRA d on d.ID = data.HCPRESCRAId
		left join(
		Select rpd.CampaignDetailId
				, Iif(r.IsReadjustment =1,1 ,0) as IsReadjustment
				,r.RequestPackageDetailStatusId
				,rpds.RequestMixingStationDetailId

			From MixingStation.RequestPackageDetailStatus rpds with(nolock)
			Inner Join MixingStation.RequestMixingStationDetail rpd with(nolock) On rpd.id = rpds.RequestMixingStationDetailId 
			inner join  MixingStation.Readjustments r WITH(NOLOCK) on r.RequestPackageDetailStatusId = rpds.Id and r.IsReadjustment=1
			Group By  rpd.CampaignDetailId, r.IsReadjustment, r.RequestPackageDetailStatusId, rpds.RequestMixingStationDetailId
		) as re on re.RequestMixingStationDetailId = rd.Id
		left join  @TableCountStatusDX dx On dx.CampaignDetailId = rd.CampaignDetailId And rd.Id = dx.RequestMixingStationDetailId
		where rdp.CampaignDetailId = @campaignDetailId or rd.CampaignDetailId = @campaignDetailId
		GROUP BY rd.Id,r.Code,rd.PackageId,rd.ATCId,rd.Source,r.RequestDate,pp.Id,p.Id,p.Name, a.Id,pp.Code,pp.Description,p.Code,p.Description, a.Code,a.Name,rdp.Quantity, rd.Quantity,
		rdp.CampaignDetailId, rd.CampaignDetailId,dx.StatusPP,dx.StatusPT,dx.StatusPL,dx.StatusPPR,dx.StatusPR,dx.StatusPA,d.PREESTADO, re.IsReadjustment, cd.UnitDoseTypeId, udt.MSClass

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT	*
	FROM @TableResult
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista todos los ítems (medicamentos, paquetes y paquetes personalizados) asociados a una campaña de preparación farmacéutica en la estación de mezclas, identificada por su ID de campaña. Para cada ítem devuelve el código y nombre del producto, la solicitud de origen (orden médica, solicitud externa de paciente, maquila o inventario), la cantidad solicitada y el conteo de paquetes agrupado por estado del ciclo de vida (en preparación, terminado, en laboratorio, pendiente de revisión, revisado y aprobado). Integra información de pacientes asignados por campaña desde RequestMixingStationDetailPatients, el detalle del tipo de dosis unitaria desde UnitDoseType, el estado de la prescripción médica desde HCPRESCRA, y si la campaña corresponde a un reajuste. También indica si el tipo de dosis tiene defectos de producción registrados, lo que permite a la farmacia monitorear el avance y calidad de cada lote de preparación magistral.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ListViewItemsCampaigns';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ListViewItemsCampaigns';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ítems (medicamentos, paquetes y paquetes personalizados) de las solicitudes asociadas a una campaña de la estación de mezclas, consolidando estados de los paquetes, información de prescripción HC, marca de reajuste y posibilidad de defectos de producción según el tipo de dosis unitaria.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListViewItemsCampaigns';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador de detalle de campaña debe existir en MixingStation.CampaignDetail para resolver UnitDoseTypeId y MSClass; Las solicitudes RequestMixingStationDetail deben estar vinculadas a la campaña directamente o a través de RequestMixingStationDetailPatients con Status<>3', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListViewItemsCampaigns';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran pacientes de la solicitud cuyo Status<>3 tanto para conteo de cantidades como para resolución de la prescripción HC; Los estados de paquete se pivotean sobre los valores fijos 1..6 generando las columnas StatusPP, StatusPT, StatusPL, StatusPPR, StatusPR y StatusPA; Los conteos de estado nulos se devuelven como 0 (ISNULL sobre cada StatusPx y sobre PREESTADO); La prescripción HC asociada se obtiene únicamente cuando EntityName=''HCFARMEPD'' y SourceTable=''HCPRESCRA'', tomando MAX(HCPRESCRA.ID) por detalle; Solo se incluyen filas cuya CampaignDetailId (directa en RequestMixingStationDetail o vía RequestMixingStationDetailPatients) coincida con el parámetro recibido; Los errores no se propagan: se imprimen pero el procedimiento continúa y devuelve la tabla resultado', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListViewItemsCampaigns';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de preparación farmacéutica; Estación de mezclas; Paquete / Paquete personalizado / Medicamento (ATC); Orden médica y solicitudes externas (paciente/maquila/inventario); Estados del paquete (PP, PT, PL, PPR, PR, PA); Prescripción clínica (HCPRESCRA) y su estado PREESTADO; Reajuste de preparación; Tipo de dosis unitaria (UnitDoseType / MSClass); Defectos de producción química; Centros de atención internos y externos', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListViewItemsCampaigns';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableResult: Devuelve un SELECT final desde la tabla variable @TableResult con un registro por cada RequestMixingStationDetail asociado a la campaña indicada; [RAISERROR] N/A: En caso de error, no relanza la excepción: ejecuta PRINT con ERROR_MESSAGE() y ERROR_LINE() dentro del CATCH (silencia el error para el llamador)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListViewItemsCampaigns';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rd.Source = 1/2/3/4 → Asigna RequestTypeName: 1=''Orden Médica'', 2=''Solicitud Externa Paciente'', 3=''Solicitud Externa Maquila'', 4=''Solicitud Inventario''; si udt.MSClass = 2 → ItemCodeName se construye como p.Code + '' - '' + p.Name (nombre del paquete) else ItemCodeName usa COALESCE entre PackagePersonalized (Code+Description), Package (Code+Description) y ATC (Code+Name); si pp.Id IS NOT NULL → ItemType=3, ItemTypeName=''Paquete Personalizado'' else Si p.Id IS NOT NULL → ItemType=1 ''Paquete''; en otro caso ItemType=2 ''Medicamento''; si rd.Source IN (1,4) → Se enlaza con ADCENATEN (centro de atención interno) por CareCenterCode else Si Source IN (2,3) se enlaza con MixingStation.ExternalCareCenter por Code; si Existe en DefectClassificationItem un registro con ProductionChemical=1 y State=1 ligado al MSClass del UnitDoseType → HasDefectsProduction=1 (bit) else HasDefectsProduction=0; si Existe registro en Readjustments con IsReadjustment=1 para el RequestPackageDetailStatus del detalle → IsReadjustment=1 else IsReadjustment=0', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListViewItemsCampaigns';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; MixingStation.RequestMixingStation; MixingStation.UnitDoseType; MixingStation.RequestMixingStationDetailPatients; MixingStation.CampaignDetail; MixingStation.Package; Inventory.ATC; MixingStation.PackagePersonalized; MixingStation.ExternalCareCenter; MixingStation.ProductionLine; MixingStation.Readjustments; MixingStation.DefectClassificationItem; MixingStation.DefectsUnitDoseType; ADCENATEN; HCFARMEPD; HCPRESCRA', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListViewItemsCampaigns';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListViewItemsCampaigns';
-- GO
