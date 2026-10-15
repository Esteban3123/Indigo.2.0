-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[ListMedicalHistory_FOMAG]
@IdHispaca integer
AS
BEGIN


			declare @UrlSnorlax as varchar(500) ;
			set @UrlSnorlax = (--'https://col-dev-ehr-api-snorlax-report-staging.azurewebsites.net'
									select UrlBase from Security.endpoints a
										inner join Security.Containers b on b.Id = a.IdContainer
									where a.Code = 'Snorlax' and b.HISContainer = DB_NAME()  
							);


		with CTE_UltimaHC
		AS
		(
		select top 10 * from HCHISPACA h where id = @IdHispaca /*where exists (select 1 from HCORDLABO where numingres = h.numingres and numefolio = h.numefolio)*/  order by h.id desc 
		)

		SELECT
		  h.ID,	
		  TI.SIGLA as tipo_documento, 
		  P.IPCODPACI as numero_documento ,
		  P.IPNOMCOMP as nombres, 
		  concat(P.IPPRIAPEL, ' ', p.IPSEGAPEL)  apellidos, 
		  p.IPFECNACI fecha_nacimiento,
		  p.IPSEXOPAC sexo,
		  p.IPESTADOC estado_civil,
		  p.IPDIRECCI direccion,
		  p.IPTELEFON + ' ' + p.IPTELMOVI telefono,
		  p.CORELEPAC as email,
		  'Colombiana' as nacionalidad,
		  'Colombia' as pais_residencia, 
		  m.munnombre as municipio_residencia, 
		  p.IPIDENTSEXOTRO as identidad_genero, 
		  p.DISCCODIGO as discapacidad,
		  et.DESGRUPET as etnia,
		  p.ZONAPARTADA as zona_vivienda, 
		  e.NOMENTIDA as entidad_aseguradora,
		  rtrim(LTRIM(C.CODIPSSEC)) as codigo_habilitacion, 
		  h.FECHISPAC as fecha_hora_inicio_atencion, 
		  isnull(h.CAUSAEXTERNA,3) as causa_externa,
		  h.CODDIAGNO as codigo_diagnostico, 
		  d.NOMDIAGNO as  nombre_diagnostico ,
		  (select top 1 dp.TIPDIAGNO from INDIAGNOP dp where dp.NUMINGRES = h.NUMINGRES and dp.NUMEFOLIO = h.NUMEFOLIO) as tipo_diagnostico, 
		  '' as tipo_tecnologia_salud ,
		  '' as codigo_tecnologia_salud,
		  '' as nombre_tecnologia_salud ,
		  '' as finalidad_tecnologia_salud,
		  '' as descripcion_comun_medicamento_tecnologia_salud,
		  NULL as fecha_prescripcion_tecnologia_salud ,
		  '' as cantidad_prescrita,
		  '' as codigo_unidad_medida, 
		  '' as via_administracion_tecnologia,
		  '' as cantidad,
		  '' as codigo_unidad_tiempo,
		  '' as cantidad_administrada ,
		  NULL as fecha_entrega_tecnologia ,
		  '' as cantidad_entregada ,
		  '' as identificacion_empleado_entrega ,
		  TI.SIGLA as tipo_identificacion,
		  '' as numero_identificacion ,
		  '' as codigo_diagnostico2 ,
		  '' as nombre_diagnostico2 ,
		  '' as tipo_diagnostico2 ,
		  h.INDICAPAC as condicion_destino_paciente ,
		  i.IFECHAING as fecha_hora_finalizacion_atencion ,
		  rtrim(LTRIM(C.CODIPSSEC)) as codigo_prestador_referencia,  
		  '' as alcance,
		  '' as dias,
		  '' as dias_licencia_maternidad ,
		  '' as codigo_alergia ,
		  '' as nombre_alergeno, 
		  '' as enfermedad_familiar ,
		  '' as parentesco_familiar_enfermo,
		 -- 'https://releaseqa.blob.core.windows.net/historiaclinica/INDIGO031/004/1002727954_MARIA-VICTORIA-MONOSALVA-TORRES/Folio-1_Historia_HC-Morbilidad_02-01-2025.pdf' AS 'url_reporte',
		  H.ReportCreatedID,
		  DB_NAME() as DatabaseName	,
		  @UrlSnorlax as UrlSnorlax
		FROM CTE_UltimaHC h 
				inner join INPACIENT p on p.IPCODPACI = h.IPCODPACI
				inner join ADINGRESO i on i.NUMINGRES = h.NUMINGRES
				inner join INENTIDAD e on e.CODENTIDA = i.CODENTIDA
				inner join ADCENATEN c on c.CODCENATE = h.CODCENATE
				inner join ADTIPOIDENTIFICA ti on ti.ID = p.IPTIPODOC
				inner join inubicaci u on u.auubicaci = p.auubicaci
				inner join INMUNICIP m on m.DEPMUNCOD = u.DEPMUNCOD 
				inner join INDIAGNOS d on d.CODDIAGNO = h.CODDIAGNO
				left join ADGRUETNI et on et.CODGRUPOE = p.CODGRUPOE

END