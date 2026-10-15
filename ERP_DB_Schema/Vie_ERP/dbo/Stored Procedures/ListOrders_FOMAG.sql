-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[ListOrders_FOMAG]
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

	--with CTE_UltimaHC
	--AS
	--(
	----select top 15 * from HCHISPACA h /*where exists (select 1 from HCORDLABO where numingres = h.numingres and numefolio = h.numefolio)*/  order by h.id desc 
	--select top 15 * from HCHISPACA h where  Id = @IdHispaca
	--),
	With CTE_ORDENES AS
	(
	select AUTO,s.CODSERIPS,s.DESSERIPS, P.CODPROSAL, P.CODESPEC1,p.TARJETAPR, p.MEDIFIRMA, P.NOMMEDICO, O.CODDIAGNO, O.OBSSERIPS, o.IPCODPACI, O.NUMINGRES, O.NUMEFOLIO, O.FECORDMED    
	from HCORDLABO o 
	inner join INCUPSIPS s on o.CODSERIPS = s.CODSERIPS 
	inner join INPROFSAL p on p.CODPROSAL = o.CODPROSAL 
	where o.numingres = @Ingreso AND o.NUMEFOLIO = @Folio          
	union all
	select AUTO,s.CODSERIPS,s.DESSERIPS, P.CODPROSAL, P.CODESPEC1,p.TARJETAPR, p.MEDIFIRMA, P.NOMMEDICO, O.CODDIAGNO, O.OBSSERIPS, o.IPCODPACI, O.NUMINGRES, O.NUMEFOLIO , O.FECORDMED
	from HCORDIMAG o 
	inner join INCUPSIPS s on o.CODSERIPS = s.CODSERIPS 
	inner join INPROFSAL p on p.CODPROSAL = o.CODPROSAL
	where o.numingres = @Ingreso AND o.NUMEFOLIO = @Folio          
	union all
	select AUTO,s.CODSERIPS,s.DESSERIPS, P.CODPROSAL, P.CODESPEC1,p.TARJETAPR, p.MEDIFIRMA, P.NOMMEDICO, O.CODDIAGNO, O.OBSSERIPS, o.IPCODPACI, O.NUMINGRES, O.NUMEFOLIO , O.FECORDMED
	from HCORDPATO o 
	inner join INCUPSIPS s on o.CODSERIPS = s.CODSERIPS 
	inner join INPROFSAL p on p.CODPROSAL = o.CODPROSAL
	where o.numingres = @Ingreso AND o.NUMEFOLIO = @Folio   
	union all
	select AUTO,s.CODSERIPS,s.DESSERIPS, P.CODPROSAL, P.CODESPEC1,p.TARJETAPR, p.MEDIFIRMA, P.NOMMEDICO, O.CODDIAGNO, O.OBSSERIPS, o.IPCODPACI, O.NUMINGRES  , O.NUMEFOLIO, O.FECORDMED
	from HCORDPRON o 
	inner join INCUPSIPS s on o.CODSERIPS = s.CODSERIPS
	inner join INPROFSAL p on p.CODPROSAL = o.CODPROSAL
	where o.numingres = @Ingreso AND o.NUMEFOLIO = @Folio
	union all
	select AUTO,s.CODSERIPS,s.DESSERIPS, P.CODPROSAL, P.CODESPEC1,p.TARJETAPR, p.MEDIFIRMA, P.NOMMEDICO, O.CODDIAGNO, O.OBSSERIPS, o.IPCODPACI, O.NUMINGRES , O.NUMEFOLIO, O.FECORDMED
	from HCORDPROQ o 
	inner join INCUPSIPS s on o.CODSERIPS = s.CODSERIPS 
	inner join INPROFSAL p on p.CODPROSAL = o.CODPROSAL
	where o.numingres = @Ingreso AND o.NUMEFOLIO = @Folio 
	union all
	select AUTO,s.CODSERIPS,s.DESSERIPS, P.CODPROSAL, O.CODESPECI as CODESPEC1,p.TARJETAPR, p.MEDIFIRMA, P.NOMMEDICO, O.CODDIAGNO, O.OBSSERIPS, o.IPCODPACI, O.NUMINGRES, O.NUMEFOLIO, O.FECORDMED
	from HCORDINTE o 
	inner join INCUPSIPS s on o.CODSERIPS = s.CODSERIPS 
	inner join INPROFSAL p on p.CODPROSAL = o.CODPROSAL
	where o.numingres = @Ingreso AND o.NUMEFOLIO = @Folio
	)




	select
	1 as client_id,
	concat(H.ID,'_',o.AUTO) as numero_orden,
	2 as ambito,
	o.NOMMEDICO as medico_ordeno,
	isnull(h.FINALIDAD,10) as finalidad,
	o.OBSSERIPS as observaciones,
	rtrim(ltrim(left(c.CODIPSSEC,10))) as prestador_codigo,
	ISNULL(o.CODDIAGNO,H.CODDIAGNO) as cie10_codigo,
	ISNULL((select top 1 CodeIntegration from Integrations.HomologationSpecialties where CodeIndigo = isnull(o.CODESPEC1,h.CODESPTRA) ),1) as especialidad,
	o.CODPROSAL as documento_medico_ordena,
	o.TARJETAPR as registro_medico_ordena,
	o.MEDIFIRMA as firma_medico_base64,
	isnull(h.FINALIDAD,10) as finalidad,
	h.FECHISPAC  as fecha_hora_inicio,
	dateadd (minute,25,h.FECHISPAC)  as fecha_hora_final,
	1 as afiliado_tipo_documento,
	h.IPCODPACI as afiliado_numero_documento,
	o.CODSERIPS as cup_codigo,
	rtrim(ltrim(left(c.CODIPSSEC,10))) as rep_codigo_habilitacion,
	1 as cantidad,
	dateadd (day,45,h.FECHISPAC)  as fecha_vigencia,
	o.OBSSERIPS as observacion,
	----'https://releaseqa.blob.core.windows.net/historiaclinica/INDIGO031/004/1002727954_MARIA-VICTORIA-MONOSALVA-TORRES/Folio-1_Historia_HC-Morbilidad_02-01-2025.pdf' AS 'url_reporte',
	H.ReportCreatedID, 
	DB_NAME() as DatabaseName,
	@UrlSnorlax as UrlSnorlax,
	1 as estado,
	convert(bit,0) as autorizacion,
	convert(varchar(20),concat(H.ID,'_',o.AUTO)) interoperabilidad_id
	from HCHISPACA h 
			left join CTE_ORDENES o on o.NUMINGRES = h.NUMINGRES
			inner join ADINGRESO i on i.NUMINGRES = o.NUMINGRES
			inner join INENTIDAD e on e.CODENTIDA = i.CODENTIDA
			inner join ADCENATEN c on c.CODCENATE = h.CODCENATE
	where h.ID = @IdHispaca

END