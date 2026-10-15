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