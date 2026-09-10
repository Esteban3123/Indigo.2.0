-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,25-10-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila356]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;

  Select convert(varchar(10),FECHAOCURRENCIA,103) As 'Fecha Ocurrencia',
	CASE INTENPREVIOS WHEN '1' THEN 'X' END AS 'Intento Si',CASE INTENPREVIOS WHEN '0' THEN 'X' END AS 'Intento No',
	CASE NUMINTENTOS  WHEN '1' THEN 'X' END AS 'Una Vez',CASE NUMINTENTOS WHEN '2' THEN 'X' END AS 'Dos Veces', CASE NUMINTENTOS WHEN '3' THEN 'X' END AS 'Tres Veces', CASE NUMINTENTOS  WHEN '4' THEN 'X' END AS 'Más de Tres Veces',CASE NUMINTENTOS WHEN '5' THEN 'X' END AS 'Sin Datos',
	CASE ESTADOCIVIL  WHEN '1' THEN 'X' END AS 'Soltero(a)',CASE ESTADOCIVIL WHEN '2' THEN 'X' END AS 'Casado(a)', CASE ESTADOCIVIL WHEN '3' THEN 'X' END AS 'Unión Libre', CASE ESTADOCIVIL  WHEN '4' THEN 'X' END AS 'Viudo(a)',CASE ESTADOCIVIL WHEN '5' THEN 'X' END AS 'Divorciado(a)',
	CASE ESCOLARIDAD  WHEN '1' THEN 'X' END AS 'Preescolar',CASE ESCOLARIDAD WHEN '2' THEN 'X' END AS 'Básica Primaria', CASE ESCOLARIDAD WHEN '3' THEN 'X' END AS 'Básica Secundaria', CASE ESCOLARIDAD  WHEN '4' THEN 'X' END AS 'Media Técnica',CASE ESCOLARIDAD WHEN '5' THEN 'X' END AS 'Técnica Profesional',CASE ESCOLARIDAD  WHEN '6' THEN 'X' END AS 'Tecnológica o Técnica',CASE ESCOLARIDAD WHEN '7' THEN 'X' END AS 'Profesional', CASE ESCOLARIDAD WHEN '8' THEN 'X' END AS 'Especialización', CASE ESCOLARIDAD  WHEN '9' THEN 'X' END AS 'Maestría',CASE ESCOLARIDAD WHEN '10' THEN 'X' END AS 'Doctorado',CASE ESCOLARIDAD  WHEN '11' THEN 'X' END AS 'Ninguno',CASE ESCOLARIDAD WHEN '12' THEN 'X' END AS 'Sin Información',
	CASE CONFLIPAREJA WHEN '1' THEN 'X' END AS 'CONFLIPAREJA', CASE PROBLEJURIDICOS WHEN '1' THEN 'X' END AS 'PROBLEJURIDICOS', CASE ENFERCRONICA WHEN '1' THEN 'X' END AS 'ENFERCRONICA',
	CASE SUICIDIOFAMI WHEN '1' THEN 'X' END AS 'SUICIDIOFAMI',CASE PROBLEECONO WHEN '1' THEN 'X' END AS 'PROBLEECONO',CASE MALTRAFISICO WHEN '1' THEN 'X' END AS 'MALTRAFISICO',CASE MUERTEFAMI WHEN '1' THEN 'X' END AS 'MUERTEFAMI',CASE PROBLELABOR WHEN '1' THEN 'X' END AS 'PROBLELABOR',
	CASE ESCOLAR WHEN '1' THEN 'X' END AS 'ESCOLAR',CASE CONSUMOSPA WHEN '1' THEN 'X' END AS 'CONSUMOSPA', CASE ANTECEFAMI WHEN '1' THEN 'X' END AS 'ANTECEFAMI', CASE IDEASUICIDA WHEN '1' THEN 'X' END AS 'IDEASUICIDA',
	CASE PLANSUICIDIO WHEN '1' THEN 'X' END AS 'PLANSUICIDIO',CASE ANTEVIOLENCIA WHEN '1' THEN 'X' END AS 'ANTEVIOLENCIA',CASE ABUSOALCOHOL WHEN '1' THEN 'X' END AS 'ABUSOALCOHOL',CASE ANTEPSQUIATRI WHEN '1' THEN 'X' END AS 'ANTEPSQUIATRI',
	CASE  WHEN CASOTRASTOR = '1' OR TRY_CAST( JSON_VALUE(JSON,'$.TRASTORNODEPRESIVO') AS BIT) = 1 THEN 'X' END AS 'Trastorno Depresivo',
	CASE  WHEN CASOTRASTOR = '2' OR TRY_CAST( JSON_VALUE(JSON,'$.TRASTORNOSPERSONALIDAD') AS BIT) = 1 THEN 'X' END AS 'Trastorno de Personalidad', 
	CASE  WHEN CASOTRASTOR = '3' OR TRY_CAST( JSON_VALUE(JSON,'$.TRASTORNOBIPOLAR') AS BIT) = 1 THEN 'X' END AS 'Trastorno Bipolar', 
	CASE  WHEN CASOTRASTOR = '4' OR TRY_CAST( JSON_VALUE(JSON,'$.ESQUIZOFRENIA') AS BIT) = 1 THEN 'X' END AS 'Esquizofrenia',
	CASE AHORCAMI WHEN '1' THEN 'X' END AS 'AHORCAMI',CASE ELEMCORTO WHEN '1' THEN 'X' END AS 'ELEMCORTO', CASE ARMAFUEGO WHEN '1' THEN 'X' END AS 'ARMAFUEGO', CASE INMOLACION WHEN '1' THEN 'X' END AS 'INMOLACION',
	CASE LANZAVACIO WHEN '1' THEN 'X' END AS 'LANZAVACIO',CASE LANZAVEHI WHEN '1' THEN 'X' END AS 'LANZAVEHI', CASE LANZACUERPO WHEN '1' THEN 'X' END AS 'LANZACUERPO', CASE INTOXICACION WHEN '1' THEN 'X' END AS 'INTOXICACION',
	CASE CASOINTOX WHEN '1' THEN 'X' END AS 'Medicamentos',CASE CASOINTOX WHEN '2' THEN 'X' END AS 'Plaguicidas', CASE CASOINTOX WHEN '3' THEN 'X' END AS 'Metanol', CASE CASOINTOX WHEN '4' THEN 'X' END AS 'Metales',
	CASE CASOINTOX WHEN '5' THEN 'X' END AS 'Solventes',CASE CASOINTOX WHEN '6' THEN 'X' END AS 'Otras sustancias Quimicas', CASE CASOINTOX WHEN '7' THEN 'X' END AS 'Gases', CASE CASOINTOX WHEN '8' THEN 'X' END AS 'Sustancias Psicoactivas',
	CASE VIAEXPO WHEN '1' THEN 'X' END AS 'Respiratoria',CASE VIAEXPO WHEN '2' THEN 'X' END AS 'Oral', CASE VIAEXPO WHEN '3' THEN 'X' END AS 'Dermica/Mucosa', CASE VIAEXPO WHEN '4' THEN 'X' END AS 'Ocular',
	CASE VIAEXPO WHEN '5' THEN 'X' END AS 'Desconocida',CASE VIAEXPO WHEN '6' THEN 'X' END AS 'Parenteral',CASE VIAEXPO WHEN '7' THEN 'X' END AS 'Transplacentaria',CASE LUGARINTOX WHEN '1' THEN 'X' END AS 'Hogar',CASE LUGARINTOX WHEN '2' THEN 'X' END AS 'Establecimiento Educativo',
	CASE LUGARINTOX WHEN '3' THEN 'X' END AS 'Establecimiento Militar',CASE LUGARINTOX WHEN '4' THEN 'X' END AS 'Establecimiento Comercial', CASE LUGARINTOX WHEN '5' THEN 'X' END AS 'Establecimiento Penitenciario', CASE LUGARINTOX WHEN '6' THEN 'X' END AS 'Lugar de Trabajo',
	CASE LUGARINTOX WHEN '7' THEN 'X' END AS 'Vía Publica/parque',CASE LUGARINTOX WHEN '8' THEN 'X' END AS 'Bares/Tabernas/Discotecas',
	CASE PSIQUIATRIA WHEN '1' THEN 'X' END AS 'PSIQUIATRIA',CASE PSICOLOGIA WHEN '1' THEN 'X' END AS 'PSICOLOGIA', CASE TRABAJSOCIAL WHEN '1' THEN 'X' END AS 'TRABAJSOCIAL',
	Rtrim(CODPRODUC) As 'Codigo Producto',Rtrim(NOMBREPRODUC) As 'Nombre Producto',
	VERSION AS 'VERSION', JSON AS 'JSON',
	CASE TRY_CAST( JSON_VALUE(JSON,'$.PROBLEFAMILIAR') AS BIT) WHEN 1 THEN 'X' END AS 'PROBLEFAMILIAR'
		
	From HCFICHA356 
	where IDFICHANOTIFICACION  = @IdFicha AND ISJSON(JSON) > 0

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera y presenta el detalle completo de la ficha SIVIGILA 356 de intento de suicidio o conducta suicida, a partir del identificador interno de la ficha. Consulta la tabla HCFICHA356 y devuelve todos los campos decodificados en formato legible: fecha de ocurrencia, intentos previos, número de intentos, estado civil, escolaridad, factores de riesgo psicosocial (conflictos de pareja, problemas jurídicos, económicos, laborales, escolares, maltrato físico, duelo familiar, antecedentes familiares de suicidio, consumo de SPA y alcohol, ideas y plan suicida, violencia, antecedentes psiquiátricos), métodos utilizados (ahorcamiento, arma blanca, arma de fuego, inmolación, lanzamiento al vacío o a vehículo, intoxicación), tipo de sustancia y vía de exposición en casos de intoxicación, lugar del evento, tipo de atención requerida (psiquiatría, psicología, trabajo social) y diagnósticos de trastorno mental asociados. Combina columnas relacionales y datos almacenados en formato JSON dentro del campo JSON de la ficha, lo que permite obtener la información completa para el diligenciamiento y reporte de la notificación obligatoria ante el SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila356';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila356';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información detallada de una ficha de notificación SIVIGILA 356 (intento de suicidio) traduciendo códigos numéricos y banderas JSON a marcas ''X'' para impresión/visualización del formato oficial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila356';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La ficha referenciada debe existir en la tabla de fichas 356.; El campo JSON de la ficha debe ser un JSON válido (ISJSON(JSON) > 0); de lo contrario no se retorna fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila356';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna fichas cuyo campo JSON sea válido (ISJSON > 0).; La traducción de códigos siempre produce ''X'' o NULL, nunca otros valores.; Para los trastornos mentales se combinan datos legacy (columna CASOTRASTOR) y datos nuevos (campos del JSON) usando OR.; Los campos CODPRODUC y NOMBREPRODUC se entregan sin espacios a la derecha (RTRIM).; FECHAOCURRENCIA se entrega formateada como dd/mm/aaaa (estilo 103).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila356';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'SIVIGILA; Ficha 356 (intento de suicidio); Factores desencadenantes (conflictos pareja, problemas jurídicos, económicos, laborales, escolares); Antecedentes psiquiátricos; Trastornos mentales (depresivo, personalidad, bipolar, esquizofrenia); Mecanismo del intento (ahorcamiento, arma cortopunzante, arma de fuego, inmolación, lanzamiento, intoxicación); Intoxicación: tipo de sustancia, vía de exposición, lugar de ocurrencia; Atención profesional (psiquiatría, psicología, trabajo social); Estado civil y escolaridad; Producto/sustancia involucrada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila356';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA356: Cuando IDFICHANOTIFICACION coincide con el id solicitado e ISJSON(JSON) > 0, retorna un resultset con los campos de la ficha mapeando códigos a ''X'' según los catálogos del formato SIVIGILA 356.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila356';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INTENPREVIOS = ''1'' / ''0'' → Marca ''Intento Si'' / ''Intento No'' respectivamente.; si NUMINTENTOS en 1..5 → Marca columna correspondiente (Una Vez, Dos Veces, Tres Veces, Más de Tres Veces, Sin Datos).; si ESTADOCIVIL en 1..5 → Marca el estado civil (Soltero, Casado, Unión Libre, Viudo, Divorciado).; si ESCOLARIDAD en 1..12 → Marca el nivel educativo correspondiente del catálogo.; si CASOTRASTOR = ''1'' OR JSON.TRASTORNODEPRESIVO = 1 → Marca ''Trastorno Depresivo''.; si CASOTRASTOR = ''2'' OR JSON.TRASTORNOSPERSONALIDAD = 1 → Marca ''Trastorno de Personalidad''.; si CASOTRASTOR = ''3'' OR JSON.TRASTORNOBIPOLAR = 1 → Marca ''Trastorno Bipolar''.; si CASOTRASTOR = ''4'' OR JSON.ESQUIZOFRENIA = 1 → Marca ''Esquizofrenia''.; si CASOINTOX en 1..8 → Marca tipo de sustancia de intoxicación (Medicamentos, Plaguicidas, Metanol, Metales, Solventes, Otras Quimicas, Gases, Psicoactivas).; si VIAEXPO en 1..7 → Marca la vía de exposición (Respiratoria, Oral, Dérmica/Mucosa, Ocular, Desconocida, Parenteral, Transplacentaria).; si LUGARINTOX en 1..8 → Marca el lugar de la intoxicación (Hogar, Educativo, Militar, Comercial, Penitenciario, Trabajo, Vía Pública, Bares).; si JSON.PROBLEFAMILIAR = 1 → Marca ''PROBLEFAMILIAR'' (factor desencadenante adicional almacenado en JSON).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila356';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA356', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila356';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila356';
-- GO
