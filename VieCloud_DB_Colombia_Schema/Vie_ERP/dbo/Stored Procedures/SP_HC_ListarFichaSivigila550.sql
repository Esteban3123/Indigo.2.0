-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,10-12-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila550]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA550 where IDFICHANOTIFICACION  = 13

	Select
	CASE NINGUNO WHEN '1' THEN 'X' END AS 'NINGUNO',CASE HIPERTCRON WHEN '1' THEN 'X' END AS 'HIPERTCRON', CASE CARDIOPATIAS WHEN '1' THEN 'X' END AS 'CARDIOPATIAS',
	CASE DIABETES WHEN '1' THEN 'X' END AS 'DIABETES',CASE MOLAHIDATI WHEN '1' THEN 'X' END AS 'MOLAHIDATI', CASE RNPRETERMINO WHEN '1' THEN 'X' END AS 'RNPRETERMINO',
	CASE RNBAJOPESO WHEN '1' THEN 'X' END AS 'RNBAJOPESO',CASE RNMASOCROMI WHEN '1' THEN 'X' END AS 'RNMASOCROMI', CASE TRASTORMENT WHEN '1' THEN 'X' END AS 'TRASTORMENT',
	CASE OBESIDAD WHEN '1' THEN 'X' END AS 'OBESIDAD',CASE DESNUTRICRON WHEN '1' THEN 'X' END AS 'DESNUTRICRON', CASE INTERGEMENOR WHEN '1' THEN 'X' END AS 'INTERGEMENOR',
	CASE ITS WHEN '1' THEN 'X' END AS 'ITS',CASE VIHSIDA WHEN '1' THEN 'X' END AS 'VIHSIDA', CASE OTRASINFECC WHEN '1' THEN 'X' END AS 'OTRASINFECC',
	CASE RHNEGATIVO WHEN '1' THEN 'X' END AS 'RHNEGATIVO',CASE TABAQUISMO WHEN '1' THEN 'X' END AS 'TABAQUISMO', CASE ALCOHOLIS WHEN '1' THEN 'X' END AS 'ALCOHOLIS',
	CASE SUSTPSICO WHEN '1' THEN 'X' END AS 'SUSTPSICO',CASE DEFICISOCIO WHEN '1' THEN 'X' END AS 'DEFICISOCIO', CASE SIFILIS WHEN '1' THEN 'X' END AS 'SIFILIS',
	CASE HEPATITISB WHEN '1' THEN 'X' END AS 'HEPATITISB',CASE OTROFACTRIES WHEN '1' THEN 'X' END AS 'OTROFACTRIES', CASE GINGIVITIS WHEN '1' THEN 'X' END AS 'GINGIVITIS',
	CASE PREECLAMPSIA WHEN '1' THEN 'X' END AS 'PREECLAMPSIA',CASE ECLAMPSIA WHEN '1' THEN 'X' END AS 'ECLAMPSIA', CASE SINDROHELLP WHEN '1' THEN 'X' END AS 'SINDROHELLP',
	CASE DIABETESGEST WHEN '1' THEN 'X' END AS 'DIABETESGEST',CASE SEPSIS WHEN '1' THEN 'X' END AS 'SEPSIS', CASE HEMO1ERSEME WHEN '1' THEN 'X' END AS 'HEMO1ERSEME',
	CASE HEMO2DOSEME WHEN '1' THEN 'X' END AS 'HEMO2DOSEME',CASE HEMO3ERTRIME WHEN '1' THEN 'X' END AS 'HEMO3ERTRIME', CASE DESPROPCEFALO WHEN '1' THEN 'X' END AS 'DESPROPCEFALO',
	CASE RETARCRECINTR WHEN '1' THEN 'X' END AS 'RETARCRECINTR',CASE ENFEAUTOIN WHEN '1' THEN 'X' END AS 'ENFEAUTOIN', CASE MALARIA WHEN '1' THEN 'X' END AS 'MALARIA',
	CASE EMBANODESEA WHEN '1' THEN 'X' END AS 'EMBANODESEA',CASE VIOLECONTRA WHEN '1' THEN 'X' END AS 'VIOLECONTRA', CASE OTRACOMPLICA WHEN '1' THEN 'X' END AS 'OTRACOMPLICA',
	CASE GESTPRODVIOL WHEN '1' THEN 'X' END AS 'GESTPRODVIOL',CASE FETOINCOMPA WHEN '1' THEN 'X' END AS 'FETOINCOMPA', CASE SINTODEPRES WHEN '1' THEN 'X' END AS 'SINTODEPRES',
	CASE SITIODEFUN WHEN '1' THEN 'X' END AS 'IPSHOSP',CASE SITIODEFUN WHEN '2' THEN 'X' END AS 'IPSCENTRO', CASE SITIODEFUN WHEN '3' THEN 'X' END AS 'LUGTRABAJO',
	CASE SITIODEFUN WHEN '4' THEN 'X' END AS 'VIAPUBLI',CASE SITIODEFUN WHEN '5' THEN 'X' END AS 'DURANTRASLA', CASE SITIODEFUN WHEN '6' THEN 'X' END AS 'DOMICILIO',
	CASE SITIODEFUN WHEN '7' THEN 'X' END AS 'SITIODEFOTRO', CASE SITIODEFUN WHEN '8' THEN 'X' END AS 'INSTITUCIONAL',
	CASE CONVIVENCIA WHEN '1' THEN 'X' END AS 'CONYUGE',CASE CONVIVENCIA WHEN '2' THEN 'X' END AS 'FAMILIA', CASE CONVIVENCIA WHEN '3' THEN 'X' END AS 'SOLA',
	CASE CONVIVENCIA WHEN '4' THEN 'X' END AS 'CONVIVENCIAOTRO',
	CASE ESCOLARIDAD WHEN '1' THEN 'X' END AS 'NINGUNA',CASE ESCOLARIDAD WHEN '2' THEN 'X' END AS 'PRIMARIA', CASE ESCOLARIDAD WHEN '3' THEN 'X' END AS 'SECUNDARIA',
	CASE ESCOLARIDAD WHEN '4' THEN 'X' END AS 'SUPERIOR',CASE ESCOLARIDAD WHEN '5' THEN 'X' END AS 'SININFO',
	CASE REGULAFECUNDI WHEN '1' THEN 'X' END AS 'NOUSMETDESCONO',CASE REGULAFECUNDI WHEN '2' THEN 'X' END AS 'NOUSMETACCE', CASE REGULAFECUNDI WHEN '3' THEN 'X' END AS 'NOUSMETNODESE',
	CASE REGULAFECUNDI WHEN '4' THEN 'X' END AS 'NATURAL',CASE REGULAFECUNDI WHEN '5' THEN 'X' END AS 'DISPOINTRAU', CASE REGULAFECUNDI WHEN '6' THEN 'X' END AS 'HORMONAL',
	CASE REGULAFECUNDI WHEN '7' THEN 'X' END AS 'BARRERA',CASE REGULAFECUNDI WHEN '8' THEN 'X' END AS 'QUIRURGICO', CASE REGULAFECUNDI WHEN '9' THEN 'X' END AS 'REGULAFECUOTRO', CASE REGULAFECUNDI WHEN '10' THEN 'X' END AS 'NOUSMETANTICON',
	CASE CONTROREALIZA WHEN '1' THEN 'X' END AS 'CONTMEDGENE',CASE CONTROREALIZA WHEN '2' THEN 'X' END AS 'CONTMEDOBSTE', CASE CONTROREALIZA WHEN '3' THEN 'X' END AS 'CONTMEDENFER',
	CASE CONTROREALIZA WHEN '4' THEN 'X' END AS 'CONTMEDAUXENFE',CASE CONTROREALIZA WHEN '5' THEN 'X' END AS 'CONTMEDPROMOT',
	CASE NIVELATENC1 WHEN '1' THEN 'X' END AS 'NIVELATENC1 I',CASE NIVELATENC1 WHEN '2' THEN 'X' END AS 'NIVELATENC1 II', CASE NIVELATENC1 WHEN '3' THEN 'X' END AS 'NIVELATENC1 III',
	CASE NIVELATENC1 WHEN '4' THEN 'X' END AS 'NIVELATENC1 IV',
	CASE NIVELATENC2 WHEN '1' THEN 'X' END AS 'NIVELATENC2 I',CASE NIVELATENC2 WHEN '2' THEN 'X' END AS 'NIVELATENC2 II', CASE NIVELATENC2 WHEN '3' THEN 'X' END AS 'NIVELATENC2 III',
	CASE NIVELATENC2 WHEN '4' THEN 'X' END AS 'NIVELATENC2 IV',
	CASE REMISIOPORTU WHEN '1' THEN 'X' END AS 'REMISIO SI',CASE REMISIOPORTU WHEN '2' THEN 'X' END AS 'REMISIO NO', CASE REMISIOPORTU WHEN '3' THEN 'X' END AS 'REMISIONOAPLI',
	CASE MOMENOCURMUE WHEN '1' THEN 'X' END AS 'GESTACION',CASE MOMENOCURMUE WHEN '2' THEN 'X' END AS 'PARTO', CASE MOMENOCURMUE WHEN '3' THEN 'X' END AS 'PUERPERIO <',
	CASE MOMENOCURMUE WHEN '4' THEN 'X' END AS 'PUERPERIO >', CASE MOMENOCURMUE WHEN '5' THEN 'X' END AS 'DURAGESTACION', CASE MOMENOCURMUE WHEN '6' THEN 'X' END AS 'ULTI42DIAS', CASE MOMENOCURMUE WHEN '7' THEN 'X' END AS 'ENTRE43DIAS',
	CASE TIPOPARTO WHEN '1' THEN 'X' END AS 'VAGINAL',CASE TIPOPARTO WHEN '2' THEN 'X' END AS 'CESAREA', CASE TIPOPARTO WHEN '3' THEN 'X' END AS 'INSTRUMENTADO',
	CASE TIPOPARTO WHEN '4' THEN 'X' END AS 'IGNORADO', CASE TIPOPARTO WHEN '5' THEN 'X' END AS 'NONACIO',
	CASE PARTOATENDI WHEN '1' THEN 'X' END AS 'PARTOMEDGENE',CASE PARTOATENDI WHEN '2' THEN 'X' END AS 'PARTOMEDOBSTE', CASE PARTOATENDI WHEN '3' THEN 'X' END AS 'PARTOENFERME',
	CASE PARTOATENDI WHEN '4' THEN 'X' END AS 'PARTOAUXENFE',CASE PARTOATENDI WHEN '5' THEN 'X' END AS 'PARTOPROMOT', CASE PARTOATENDI WHEN '6' THEN 'X' END AS 'PARTOPARTERA',
	CASE PARTOATENDI WHEN '7' THEN 'X' END AS 'PARTOOTRO', CASE PARTOATENDI WHEN '8' THEN 'X' END AS 'ELLAMISMA',
	CASE CAUSAMUERTE WHEN '1' THEN 'X' END AS 'HISTCLINICA',CASE CAUSAMUERTE WHEN '2' THEN 'X' END AS 'AUTOPSIA', CASE CAUSAMUERTE WHEN '3' THEN 'X' END AS 'NECROPSIA',
	cast(FechaHora as datetime) As 'FECHAHORA', cast(hora as time) As 'HORA',
	Rtrim(MARCOTROCUAL) As 'MARCOTROCUAL', Rtrim(GESTACIONES) As 'GESTACIONES', Rtrim(PARTVAGINA) As 'PARTVAGINA', Rtrim(CESAREAS) As 'CESAREAS', Rtrim(MUERTOS) As 'MUERTOS', Rtrim(VIVOS) As 'VIVOS',
	Rtrim(ABORTOS) As 'ABORTOS', Rtrim(MARCOTROFAC) As 'MARCOTROFAC', Rtrim(MARCOTRACOMP) As 'MARCOTRACOMP', Rtrim(NOCPN) As 'NOCPN', Rtrim(SEMAINICCPN) As 'SEMAINICCPN', Rtrim(COMPLIFETORNCIE) As 'COMPLIFETORNCIE',
	Rtrim(SEMAGESTMORT) As 'SEMAGESTMORT', Rtrim(OTROQUIEN) As 'OTROQUIEN', Rtrim(CAUSADEFUN) As 'CAUSADEFUN', Rtrim(DEMORA1) As 'DEMORA1', Rtrim(DEMORA2) As 'DEMORA2', Rtrim(DEMORA3) As 'DEMORA3',
	Rtrim(DEMORA4) As 'DEMORA4',
	VERSION AS 'VERSION', 
	JSON AS 'JSON' ,				 
	JSON_VALUE(JSON,'$.OTRO_REGULACION_FECUNDIDAD') AS 'OTROREGULACIONFECUNDIDAD',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.TUVO_CONTROL_PRENATAL') AS BIT) WHEN 0 THEN 'X' END AS 'NOCONTROLPRENATAL',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.TUVO_CONTROL_PRENATAL') AS BIT) WHEN 1 THEN 'X' END AS 'SICONTROLPRENATAL',
	JSON_VALUE(JSON,'$.NOMBRE_APELLIDO') AS 'NOMBREAPELLIDO',
	CONCAT(JSON_VALUE(JSON,'$.PAIS_RESIDENCIA_HABITUAL'),' - ',JSON_VALUE(JSON,'$.DEPARTAMETO_RESIDENCIA_HABITUAL'),' - ',JSON_VALUE(JSON,'$.MUNICIPIO_RESIDENCIA_HABITUAL')) AS 'PAISDEPMUN',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.AREA_RESIDENCIA_HABITUAL') AS INT) WHEN 1 THEN 'X' END AS 'CABECEMUNICI',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.AREA_RESIDENCIA_HABITUAL') AS INT) WHEN 2 THEN 'X' END AS 'CENTROPOBLADO',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.AREA_RESIDENCIA_HABITUAL') AS INT) WHEN 3 THEN 'X' END AS 'RURALDISPERSO',
	JSON_VALUE(JSON,'$.LOCALIDAD_RESIDENCIA_HABITUAL') AS 'LOCALIRESIHABI',
	JSON_VALUE(JSON,'$.BARRIO_RESIDENCIA_HABITUAL') AS 'BARRIORESIHABI',
	JSON_VALUE(JSON,'$.CABECERA_MUNICIPAL') AS 'CABECERAMUNICIPAL',
	JSON_VALUE(JSON,'$.VEREDA_ZONA') AS 'VEREDAZONA',
	JSON_VALUE(JSON,'$.DIRECCION_RESIDENCIA_HABITUAL') AS 'DIRERESIHABI',
	JSON_VALUE(JSON,'$.TELEFONO') AS 'TELEFONO',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.PARENTESCO') AS INT) WHEN 1 THEN 'X' END AS 'PADRE',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.PARENTESCO') AS INT) WHEN 2 THEN 'X' END AS 'MADRE',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.PARENTESCO') AS INT) WHEN 3 THEN 'X' END AS 'HERMANO',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.PARENTESCO') AS INT) WHEN 4 THEN 'X' END AS 'ESPOSO',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.PARENTESCO') AS INT) WHEN 5 THEN 'X' END AS 'HIJO',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.PARENTESCO') AS INT) WHEN 6 THEN 'X' END AS 'AMIGO',
	CASE TRY_CAST(JSON_VALUE(JSON,'$.PARENTESCO') AS INT) WHEN 7 THEN 'X' END AS 'OTRO'

	FROM HCFICHA550 
	
	WHERE IDFICHANOTIFICACION  = @IdFicha AND ISJSON(JSON) > 0

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA550
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y formatea los datos de la ficha epidemiológica SIVIGILA 550 (Mortalidad Materna) registrada en la historia clínica, dado un identificador y número de ficha. Consulta la tabla HCFICHA550 y transforma los campos indicadores (0/1) de antecedentes, factores de riesgo y complicaciones del embarazo —como hipertensión crónica, diabetes, preeclampsia, eclampsia, síndrome HELLP, VIH/SIDA, sífilis, tabaquismo, violencia de pareja, sepsis, entre otros— en marcas visuales ''X'' listas para imprimir o renderizar en el formulario oficial de notificación obligatoria al SIVIGILA. También desglosa variables codificadas como lugar de defunción, convivencia, escolaridad, método de regulación de la fecundidad, tipo de parto, profesional que realizó el control prenatal y momento de ocurrencia de la muerte materna, expandiéndolas en columnas separadas por cada opción de respuesta. Este procedimiento es utilizado para la generación del reporte de mortalidad materna exigido por el sistema de vigilancia epidemiológica en salud pública de Colombia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila550';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila550';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y transforma los datos de una ficha de notificación SIVIGILA (mortalidad materna, ficha 550) para presentación, mapeando códigos numéricos a marcas ''X'' y extrayendo campos del JSON asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila550';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA550 cuyo IDFICHANOTIFICACION coincida con el identificador recibido.; El campo JSON del registro debe ser un JSON válido (ISJSON(JSON) > 0); de lo contrario la fila se excluye.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila550';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan fichas cuyo campo JSON sea un JSON sintácticamente válido.; Los códigos numéricos almacenados se traducen a marcas ''X'' o NULL, nunca a otros valores.; Los campos de texto retornados se entregan sin espacios finales (RTRIM).; El filtro por IDFICHANOTIFICACION garantiza retornar como máximo las filas asociadas a una sola ficha.; El parámetro @NumFicha se recibe pero no se utiliza en el filtrado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila550';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA 550 (mortalidad materna); Antecedentes y factores de riesgo obstétricos (preeclampsia, eclampsia, síndrome HELLP, diabetes gestacional, sepsis, hemorragias por trimestre); Antecedentes personales (hipertensión crónica, cardiopatías, diabetes, ITS, VIH/SIDA, hepatitis B, sífilis); Hábitos y factores socioeconómicos (tabaquismo, alcoholismo, sustancias psicoactivas, déficit socioeconómico, violencia contra la mujer); Sitio y momento de la defunción (gestación, parto, puerperio); Convivencia y escolaridad de la gestante; Métodos de regulación de la fecundidad / planificación familiar; Control prenatal y nivel de atención (I-IV); Tipo de parto y personal que atendió el parto; Fuente de causa de muerte (historia clínica, autopsia, necropsia); Demoras en mortalidad materna (DEMORA1..DEMORA4); Datos de residencia habitual y parentesco del informante', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila550';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA550: Cuando IDFICHANOTIFICACION = @IdFicha y el campo JSON es JSON válido, devuelve un único conjunto de resultados con los campos transformados (códigos a ''X'', JSON parseado y campos con RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila550';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Para cada factor de riesgo/antecedente (NINGUNO, HIPERTCRON, DIABETES, ITS, VIHSIDA, etc.) cuando el valor es ''1'' → Se devuelve ''X'' como marca de selección else Se devuelve NULL; si SITIODEFUN según valor 1..8 → Marca con ''X'' la categoría correspondiente al sitio de defunción (IPSHOSP, IPSCENTRO, LUGTRABAJO, VIAPUBLI, DURANTRASLA, DOMICILIO, SITIODEFOTRO, INSTITUCIONAL); si CONVIVENCIA según valor 1..4 → Marca cónyuge, familia, sola u otro tipo de convivencia; si ESCOLARIDAD según valor 1..5 → Marca nivel educativo: ninguna, primaria, secundaria, superior o sin información; si REGULAFECUNDI según valor 1..10 → Marca el método de regulación de fecundidad correspondiente; si CONTROREALIZA según valor 1..5 → Marca quién realizó el control (médico general, obstetra, enfermería, auxiliar, promotor); si NIVELATENC1 y NIVELATENC2 según valor 1..4 → Marca el nivel de atención I, II, III o IV; si REMISIOPORTU según valor 1..3 → Marca remisión oportuna SI, NO o NO APLICA; si MOMENOCURMUE según valor 1..7 → Marca el momento en que ocurrió la muerte (gestación, parto, puerperio temprano/tardío, etc.); si TIPOPARTO según valor 1..5 → Marca el tipo de parto (vaginal, cesárea, instrumentado, ignorado, no nació); si PARTOATENDI según valor 1..8 → Marca quién atendió el parto (médico general, obstetra, enfermera, auxiliar, promotor, partera, otro, ella misma); si CAUSAMUERTE según valor 1..3 → Marca la fuente de la causa de muerte (historia clínica, autopsia, necropsia); si JSON.TUVO_CONTROL_PRENATAL convertido a BIT → Si 1 marca SICONTROLPRENATAL=''X''; si 0 marca NOCONTROLPRENATAL=''X''; si JSON.AREA_RESIDENCIA_HABITUAL convertido a INT (1..3) → Marca cabecera municipal, centro poblado o rural disperso; si JSON.PARENTESCO convertido a INT (1..7) → Marca el parentesco del informante (padre, madre, hermano, esposo, hijo, amigo, otro)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila550';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA550', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila550';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila550';
-- GO
