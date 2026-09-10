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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una historia clínica específica de FOMAG a partir de su identificador interno (IdHispaca), consolidando en un único resultado los datos demográficos del paciente (nombre, documento, fecha de nacimiento, sexo, estado civil, dirección, teléfono, municipio de residencia, etnia, discapacidad), la información del ingreso o episodio de atención (fecha de inicio y finalización, causa externa, condición de destino), el diagnóstico principal CIE-10, la entidad aseguradora, el código de habilitación del centro de atención y la URL del reporte PDF generado en el servicio Snorlax. Este procedimiento existe para alimentar los reportes de historia clínica exigidos por FOMAG, integrando las tablas maestras de pacientes (INPACIENT), ingresos (ADINGRESO), entidades (INENTIDAD), centros de atención (ADCENATEN), tipos de documento (ADTIPOIDENTIFICA), municipios (INMUNICIP) y diagnósticos (INDIAGNOS) alrededor del folio de historia clínica (HCHISPACA) seleccionado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ListMedicalHistory_FOMAG';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ListMedicalHistory_FOMAG';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye un dataset plano de la historia clínica de un paciente (datos demográficos, atención y diagnóstico principal) para ser consumido por el reporte FOMAG vía servicio externo Snorlax.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListMedicalHistory_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCHISPACA con el identificador suministrado.; Debe existir el endpoint configurado con Code=''Snorlax'' en Security.endpoints asociado a un Container cuyo HISContainer coincida con el nombre de la base de datos actual (DB_NAME()).; El paciente referenciado debe tener registros relacionados en INPACIENT, ADINGRESO, INENTIDAD, ADCENATEN, ADTIPOIDENTIFICA, INUBICACI, INMUNICIP e INDIAGNOS (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListMedicalHistory_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La URL del servicio Snorlax se resuelve dinámicamente según la base de datos actual (DB_NAME()) y el código de endpoint ''Snorlax''.; Nacionalidad y país de residencia se fijan siempre como ''Colombiana'' y ''Colombia'' respectivamente.; Los campos de tecnología en salud, alergias, licencias y enfermedad familiar se devuelven siempre vacíos/NULL (placeholders no poblados en este SP).; Solo se procesa la historia clínica cuyo ID coincide con el parámetro (CTE limita a TOP 10 ordenado desc, pero el filtro por id la reduce a una fila).; El tipo de diagnóstico se obtiene del primer registro de INDIAGNOP que coincida en NUMINGRES y NUMEFOLIO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListMedicalHistory_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Paciente; Diagnóstico principal y secundario; Tipo de identificación; Entidad aseguradora; Causa externa de atención; Etnia; Discapacidad; Identidad de género; Zona de vivienda; Prestador de salud (código de habilitación); Tecnología en salud (medicamentos); Licencia de maternidad; Alergias; Antecedentes familiares; FOMAG; Reporte de morbilidad / historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListMedicalHistory_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila con los datos demográficos, de atención y diagnóstico principal del HCHISPACA filtrado, junto con la URL base del servicio Snorlax y el nombre de la base de datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListMedicalHistory_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si h.CAUSAEXTERNA IS NULL → Se reporta causa_externa = 3 (valor por defecto vía ISNULL). else Se conserva el valor original de CAUSAEXTERNA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListMedicalHistory_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.endpoints; Security.Containers; dbo.HCHISPACA; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN; dbo.ADTIPOIDENTIFICA; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDIAGNOS; dbo.ADGRUETNI; dbo.INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListMedicalHistory_FOMAG';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ListMedicalHistory_FOMAG';
-- GO
