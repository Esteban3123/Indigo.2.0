-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
create PROCEDURE [dbo].[ListOrdersMedication_FOMAG]
@IdHispaca integer
AS
BEGIN
    

			declare @UrlSnorlax as varchar(500) ;
			set @UrlSnorlax = (--'https://col-dev-ehr-api-snorlax-report-staging.azurewebsites.net'
							select UrlBase from Security.endpoints a
								inner join Security.Containers b on b.Id = a.IdContainer
							where a.Code = 'Snorlax' and b.HISContainer = DB_NAME()  
					);

	declare @Ingreso as varchar(20) 
	declare @Folio as varchar(20) 
	
	select @Ingreso = NUMINGRES , @Folio = NUMEFOLIO from HCHISPACA h where  Id = @IdHispaca;

	with CTE_ORDENES AS
	(
	select top 50 ID as AUTO,trim(p.CODPRODUC) as CODPRODUC, P.CODPROSAL, p.NUMINGRES, p.CODCENATE, p.canpedpro as Cantidad
	from hcprescra p order by p.ID desc
	--where p.numingres = @Ingreso AND p.NUMEFOLIO = @Folio
	union all
	select top 50 p.CODCONCEC as AUTO,trim(p.CODPRODUC) as CODPRODUC, P.CODPROSAL, p.NUMINGRES, p.CODCENATE, p.canpedpro as Cantidad
	from HCSOLINSD p 
	inner join HCSOLINSC c on c.CODCONCEC = p.CODCONCEC order by p.CODCONCEC desc
	--where p.numingres = @Ingreso AND c.NUMEFOLIO = @Folio
	)

	select  distinct top 50
	concat(o.NUMINGRES,'_',o.AUTO) as Orden_id,
	concat(o.NUMINGRES,'_',o.AUTO) as Orden_dispensacion_id,
	1 as Estado_Entrega,
--	Invp.CodeCUM as Cums,
	(select top 1 Invp.CodeCUM from Inventory.InventoryProduct Invp where InvP.atcId = atc.id ) as Cums,
	Pro.DESPRODUC as Nombre_Medicamento,
	pro.CONCENMED as Concentracion,
	trim(F.DESFORMED) as Forma_Farmaceutica,
	TRIM(V.DESVIAADM) as Via_Administracion, 
	PRO.NOPOSPROD as Medicamento_PBS,
	GETDATE() as Fecha_Entrega,
	o.Cantidad as Cantidad_Prescrita,
	0 as Cantidad_Entregada,
	o.Cantidad as Cantidad_Pendiente,
	convert(bit,0) as Entrega_Domicilio,
	rtrim(ltrim(left(c.CODIPSSEC,10))) as asegurador,
	'' as Codigo_Farmacia,
	'' as Nombre_Farmacia,
	'' as Direccion_Farmacia,
	'' as Ciudad_Farmacia,
	'' as Departamento_Farmacia
	from  CTE_ORDENES o
			inner join ADINGRESO i on i.NUMINGRES = o.NUMINGRES
			inner join ADCENATEN c on c.CODCENATE = o.CODCENATE
			inner join IHLISTPRO Pro on pro.CODPRODUC = o.CODPRODUC
			inner join HCVIAADMI V ON V.CODVIAADM = PRO.CODVIAADM
			inner join IHFORMEDI F on F.CODFORMED = Pro.CODFORMED
			inner join Inventory.atc atc on atc.code = Pro.CODPRODUC
			inner join Inventory.InventoryProduct Invp on InvP.atcId = atc.id 
	--where h.ID = @IdHispaca

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista las órdenes de medicamentos asociadas a una historia clínica específica (folio FOMAG), consultando tanto las prescripciones médicas (HCPRESCRA) como las solicitudes de insumos (HCSOLINSD/HCSOLINSC) registradas durante un ingreso hospitalario. Combina información del producto farmacéutico (nombre, concentración, forma farmacéutica, vía de administración, si es PBS), datos del ingreso del paciente (ADINGRESO), del centro de atención (ADCENATEN) y del inventario de productos (Inventory) para armar una respuesta estructurada de dispensación. Devuelve hasta 50 órdenes con cantidades prescritas, pendientes y entregadas, junto con el código asegurador, pensado para alimentar flujos de dispensación y reporte de medicamentos bajo el esquema FOMAG.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ListOrdersMedication_FOMAG';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ListOrdersMedication_FOMAG';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve un listado de órdenes de medicamentos (prescripciones e insumos) asociadas a un ingreso/folio para integración/reporte FOMAG, enriquecido con datos del producto, vía de administración y forma farmacéutica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrdersMedication_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCHISPACA con el Id recibido para obtener NUMINGRES y NUMEFOLIO.; Los productos deben tener correspondencia en IHLISTPRO, Inventory.atc (por code = CODPRODUC) e Inventory.InventoryProduct (por atcId).; Los ingresos y centros de atención deben existir en ADINGRESO y ADCENATEN.; Debe existir un endpoint configurado en Security.endpoints con Code=''Snorlax'' cuyo Container coincida con la BD actual (DB_NAME()).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrdersMedication_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Estado_Entrega siempre se reporta como 1.; Cantidad_Entregada siempre es 0 y Cantidad_Pendiente es igual a Cantidad_Prescrita (no hay control real de entregas).; Entrega_Domicilio siempre es false (bit 0).; Fecha_Entrega se fija como la fecha actual del servidor (GETDATE()).; Los campos de farmacia (Código, Nombre, Dirección, Ciudad, Departamento) siempre se devuelven vacíos.; El asegurador se obtiene truncando CODIPSSEC a los primeros 10 caracteres.; Solo se incluyen productos que tengan ATC y al menos un InventoryProduct asociado (joins internos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrdersMedication_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de medicamento; Dispensación; Prescripción; Solicitud de insumos; CUM (Código Único de Medicamento); ATC; Forma farmacéutica; Vía de administración; Medicamento PBS (Plan de Beneficios); Asegurador; Ingreso/Folio del paciente; FOMAG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrdersMedication_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve hasta 50 filas distinct combinando las últimas 50 prescripciones (HCPRESCRA) y las últimas 50 solicitudes de insumos (HCSOLINSD/HCSOLINSC), formateando Orden_id como NUMINGRES_AUTO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrdersMedication_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.endpoints; Security.Containers; dbo.HCHISPACA; dbo.hcprescra; dbo.HCSOLINSD; dbo.HCSOLINSC; dbo.ADINGRESO; dbo.ADCENATEN; dbo.IHLISTPRO; dbo.HCVIAADMI; dbo.IHFORMEDI; Inventory.atc; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrdersMedication_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrdersMedication_FOMAG';
-- GO
