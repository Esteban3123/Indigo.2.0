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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que obtiene el listado completo de órdenes médicas asociadas a una historia clínica específica de FOMAG, identificada por su ID interno. Consolida en un único resultado todas las órdenes de laboratorio, imágenes diagnósticas, patología, pronóstico, procedimientos quirúrgicos e interconsultas solicitadas por el médico en un folio de historia clínica, enriqueciendo cada orden con el código CUPS del servicio, nombre y registro profesional del médico que ordenó, diagnóstico CIE-10, datos del afiliado (cédula del paciente), centro de atención y fechas. El resultado está estructurado en formato de interoperabilidad para envío a servicios externos (Snorlax), incluyendo la URL dinámica del API de reportes obtenida desde la configuración de seguridad del contenedor de la base de datos activa. Se usa principalmente para generar y remitir órdenes médicas electrónicas al asegurador FOMAG.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ListOrders_FOMAG';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ListOrders_FOMAG';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye y retorna el listado consolidado de órdenes médicas (laboratorio, imágenes, patología, pronóstico, procedimientos quirúrgicos e interconsultas) asociadas a una historia clínica para integración con FOMAG.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrders_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCHISPACA con el Id recibido para obtener NUMINGRES y NUMEFOLIO.; Debe existir un endpoint con Code=''Snorlax'' en Security.endpoints cuyo Container coincida con DB_NAME() para resolver la URL base.; Cada orden debe tener CODSERIPS válido en INCUPSIPS y CODPROSAL válido en INPROFSAL (joins internos).; El ingreso debe existir en ADINGRESO y la entidad asociada en INENTIDAD; el centro de atención en ADCENATEN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrders_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'client_id siempre es 1.; ambito siempre es 2.; cantidad siempre es 1.; afiliado_tipo_documento siempre es 1.; estado siempre es 1 y autorizacion siempre es 0 (false).; fecha_hora_final = fecha_hora_inicio + 25 minutos.; fecha_vigencia = fecha_hora_inicio + 45 días.; numero_orden e interoperabilidad_id se construyen como concat(IdHispaca,''_'',AUTO de la orden).; prestador_codigo y rep_codigo_habilitacion se obtienen de los primeros 10 caracteres de CODIPSSEC del centro de atención, sin espacios.; Solo procesa órdenes cuyo NUMINGRES y NUMEFOLIO coinciden con los de la historia clínica del IdHispaca.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrders_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Órdenes médicas (laboratorio, imágenes, patología, pronóstico, procedimientos quirúrgicos, interconsultas); Médico ordenante y firma médica; Diagnóstico CIE-10; Especialidad médica homologada; Afiliado/paciente; Prestador y código de habilitación; Ingreso y entidad responsable; Integración FOMAG/Snorlax; Vigencia de la orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrders_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve una fila por cada orden encontrada en HCORDLABO/HCORDIMAG/HCORDPATO/HCORDPRON/HCORDPROQ/HCORDINTE para el NUMINGRES y NUMEFOLIO de la historia clínica indicada, formateada para FOMAG.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrders_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Especialidad de la orden (CODESPEC1) o de la historia (CODESPTRA) no encuentra homologación en Integrations.HomologationSpecialties → Asigna especialidad = 1 por defecto else Usa el CodeIntegration homologado; si La orden no tiene CODDIAGNO → Usa el CODDIAGNO de la historia clínica (HCHISPACA) como cie10_codigo; si FINALIDAD de la historia clínica es NULL → Asigna finalidad = 10 por defecto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrders_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.endpoints; Security.Containers; dbo.HCHISPACA; dbo.HCORDLABO; dbo.HCORDIMAG; dbo.HCORDPATO; dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCORDINTE; dbo.INCUPSIPS; dbo.INPROFSAL; Integrations.HomologationSpecialties; dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrders_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListOrders_FOMAG';
-- GO
