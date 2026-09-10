-- Stored Procedure
-- =============================================
-- Autor:		Jean Carlos Roldan Lozano
-- Fecha Creación: 30-10-2018
-- Descripción:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 365
-- Modificó:    Yezid Garcia Medina
-- Fecha Modificación : 21-01-2022
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila365]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)
AS
BEGIN
  SET NOCOUNT ON;
  
			Select
				CASE GRUPOSUSTANCIAS WHEN '1' THEN 'X' END AS 'MEDICAMENTOS', CASE GRUPOSUSTANCIAS WHEN '2' THEN 'X' END AS 'PLAGUICIDAS', CASE GRUPOSUSTANCIAS WHEN '3' THEN 'X' END AS 'METANOL',
				CASE GRUPOSUSTANCIAS WHEN '4' THEN 'X' END AS 'METALES', CASE GRUPOSUSTANCIAS WHEN '5' THEN 'X' END AS 'SOLVENTES', CASE GRUPOSUSTANCIAS WHEN '6' THEN 'X' END AS 'OTRASQUIMICAS',
				CASE GRUPOSUSTANCIAS WHEN '7' THEN 'X' END AS 'GASES', CASE GRUPOSUSTANCIAS WHEN '8' THEN 'X' END AS 'SUSTPSICOACT',
				Rtrim(CODIPRODUCTO) As 'CODPRODUC', CASE TIPOEXPO WHEN '1' THEN 'X' END AS 'OCUPACIONAL', CASE TIPOEXPO WHEN '2' THEN 'X' END AS 'ACCIDENTAL', CASE TIPOEXPO WHEN '3' THEN 'X' END AS 'SUICIDIO',
				CASE TIPOEXPO WHEN '4' THEN 'X' END AS 'ACTOHOMICIDA', CASE TIPOEXPO WHEN '5' THEN 'X' END AS 'ACTODELICTIVO', CASE TIPOEXPO WHEN '6' THEN 'X' END AS 'DESCONOCIDA',
				CASE TIPOEXPO WHEN '7' THEN 'X' END AS 'INTENCIONAL', CASE TIPOEXPO WHEN '8' THEN 'X' END AS 'AUTOMEDICACION',
				CASE LUGARINTOX WHEN '1' THEN 'X' END AS 'HOGAR', CASE LUGARINTOX WHEN '2' THEN 'X' END AS 'ESTEDUCATIVO', CASE LUGARINTOX WHEN '3' THEN 'X' END AS 'ESTMILITAR',
				CASE LUGARINTOX WHEN '4' THEN 'X' END AS 'ESTCOMERCIAL', CASE LUGARINTOX WHEN '5' THEN 'X' END AS 'ESTPENITEN', CASE LUGARINTOX WHEN '6' THEN 'X' END AS 'LUGARTRABAJO',
				CASE LUGARINTOX WHEN '7' THEN 'X' END AS 'VIAPUBLICA', CASE LUGARINTOX WHEN '8' THEN 'X' END AS 'BARES',
				convert(varchar(10),FECHAEXPO,103) As 'FECHAOCURRENCIA',
				CASE VIAEXPO WHEN '1' THEN 'X' END AS 'RESPIRATORIA', CASE VIAEXPO WHEN '2' THEN 'X' END AS 'ORAL', CASE VIAEXPO WHEN '3' THEN 'X' END AS 'DERMICA',
				CASE VIAEXPO WHEN '4' THEN 'X' END AS 'OCULAR', CASE VIAEXPO WHEN '5' THEN 'X' END AS 'DESCONOCIDAEXPO', CASE VIAEXPO WHEN '6' THEN 'X' END AS 'PARENTERAL',
				CASE VIAEXPO WHEN '7' THEN 'X' END AS 'TRANSPLACENTARIA',
				CASE ESCOLARIDAD WHEN '1' THEN 'X' END AS 'PREESCOLAR', CASE ESCOLARIDAD WHEN '2' THEN 'X' END AS 'PRIMARIA', CASE ESCOLARIDAD WHEN '3' THEN 'X' END AS 'SECUNDARIA',
				CASE ESCOLARIDAD WHEN '4' THEN 'X' END AS 'ACADEMICA', CASE ESCOLARIDAD WHEN '5' THEN 'X' END AS 'TECNICA', CASE ESCOLARIDAD WHEN '6' THEN 'X' END AS 'NORMALISTA',
				CASE ESCOLARIDAD WHEN '7' THEN 'X' END AS 'TECNIPROFE',
				CASE ESCOLARIDAD WHEN '8' THEN 'X' END AS 'TECNOLOGICA', CASE ESCOLARIDAD WHEN '9' THEN 'X' END AS 'PROFESIONAL', CASE ESCOLARIDAD WHEN '10' THEN 'X' END AS 'ESPECIALIZA',
				CASE ESCOLARIDAD WHEN '11' THEN 'X' END AS 'MAESTRIA', CASE ESCOLARIDAD WHEN '12' THEN 'X' END AS 'DOCTORADO', CASE ESCOLARIDAD WHEN '13' THEN 'X' END AS 'NINGUNO',
				CASE ESCOLARIDAD WHEN '14' THEN 'X' END AS 'SININFO',
				CASE AFILIADOARL WHEN '1' THEN 'X' END AS 'AFILIADO Si',CASE AFILIADOARL WHEN '0' THEN 'X' END AS 'AFILIADO No',
				Rtrim(CODIGOARL) As 'CODARL',CASE ESTADOCIVIL WHEN '1' THEN 'X' END AS 'SOLTERO', CASE ESTADOCIVIL WHEN '2' THEN 'X' END AS 'CASADO', CASE ESTADOCIVIL WHEN '3' THEN 'X' END AS 'UNIONLIBRE',
				CASE ESTADOCIVIL WHEN '4' THEN 'X' END AS 'VIUDO', CASE ESTADOCIVIL WHEN '5' THEN 'X' END AS 'DIVORCIADO',
				CASE CASOBROTE WHEN '1' THEN 'X' END AS 'BROTE Si',CASE CASOBROTE WHEN '0' THEN 'X' END AS 'BROTE No', Rtrim(NUMCASOS) As 'NUMCASOS',
				convert(varchar(10),FECHAINVEST,103) As 'FECHAINVEST', CASE SITUAALERTA WHEN '1' THEN 'X' END AS 'ALERTA Si', CASE SITUAALERTA WHEN '0' THEN 'X' END AS 'ALERTA No',
				CASE MUESTRASTOXICO WHEN '1' THEN 'X' END AS 'TOXICO Si', CASE MUESTRASTOXICO WHEN '0' THEN 'X' END AS 'TOXICO No',
				CASE SANGRETOTAL WHEN '1' THEN 'X' END AS 'SANGRETOTAL', CASE ORINA WHEN '1' THEN 'X' END AS 'ORINA', CASE TEJIDO WHEN '1' THEN 'X' END AS 'TEJIDO',
				CASE SUERO WHEN '1' THEN 'X' END AS 'SUERO', CASE AGUA WHEN '1' THEN 'X' END AS 'AGUA', CASE CABELLO WHEN '1' THEN 'X' END AS 'CABELLO',
				CASE EMPAQUE WHEN '1' THEN 'X' END AS 'EMPAQUE', CASE OTROS WHEN '1' THEN 'X' END AS 'OTROS', CASE UNAS WHEN '1' THEN 'X' END AS 'UÑAS',
				Rtrim(NOMBREPRUEBA) As 'NOMPRUEBA', Rtrim(VALORRESUL) As 'VALORRESUL', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON', 
				CONVERT(VARCHAR(5) ,CAST(JSON_VALUE(JSON,'$.HORAEXPOSICION') AS DATETIME) , 114) As 'HORAEXPOSICION' 
			From 
				HCFICHA365 
			Where 
				IDFICHANOTIFICACION  = @IdFicha 		
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de la Ficha 365 de SIVIGILA correspondiente a casos de intoxicación aguda, consultando la tabla HCFICHA365 por el identificador de la ficha de notificación. Devuelve en formato tabular todos los campos epidemiológicos requeridos por el sistema de vigilancia en salud pública: grupo de sustancia involucrada (medicamentos, plaguicidas, metanol, metales, solventes, gases, sustancias psicoactivas, etc.), tipo de exposición (ocupacional, accidental, suicidio, acto homicida, automedicación, etc.), lugar de la intoxicación, vía de exposición, fecha y hora del evento, nivel de escolaridad y estado civil del paciente, afiliación a ARL, si el caso hace parte de un brote, número de casos, fecha de investigación, situación de alerta, y los tipos de muestras toxicológicas tomadas (sangre, orina, tejido, suero, agua, cabello, empaque, uñas). También expone el nombre y resultado de pruebas de laboratorio, la versión del registro y el JSON extendido con datos adicionales como la hora de exposición. Se utiliza en historia clínica para diligenciar, imprimir y reportar los eventos de intoxicación ante el sistema de vigilancia epidemiológica nacional (SIVIGILA).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila365';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila365';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de una ficha de notificación SIVIGILA 365 (intoxicaciones) transformando códigos numéricos en marcas ''X'' por categoría para reporte/impresión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila365';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA365 cuyo IDFICHANOTIFICACION coincida con el identificador suministrado.; El campo JSON debe contener la propiedad ''HORAEXPOSICION'' parseable como DATETIME para que el formato de hora no falle.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila365';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas FECHAEXPO y FECHAINVEST se devuelven en formato dd/mm/yyyy (estilo 103).; La hora de exposición se extrae del campo JSON (''$.HORAEXPOSICION'') y se formatea como HH:MI (estilo 114).; Los códigos categóricos no se exponen como número; se traducen a una marca ''X'' en columnas-bandera mutuamente excluyentes por dimensión.; El parámetro @NumFicha se recibe pero no se usa en el filtrado; solo se filtra por IDFICHANOTIFICACION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila365';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA 365; Intoxicaciones por sustancias químicas; Grupo de sustancias (medicamentos, plaguicidas, metanol, metales, solventes, gases, psicoactivas); Tipo de exposición (ocupacional, accidental, suicidio, homicida, delictivo, automedicación); Lugar de intoxicación; Vía de exposición; Escolaridad del paciente; Afiliación a ARL; Estado civil; Caso de brote; Situación de alerta; Muestras toxicológicas (sangre, orina, tejido, suero, agua, cabello, empaque, uñas); Prueba toxicológica y resultado; Hora de exposición', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila365';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA365: Cuando IDFICHANOTIFICACION = parámetro recibido, retorna una fila con los campos de la ficha mapeados a indicadores ''X'' por categoría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila365';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si GRUPOSUSTANCIAS in (''1''..''8'') → Marca ''X'' en la columna correspondiente: 1=MEDICAMENTOS, 2=PLAGUICIDAS, 3=METANOL, 4=METALES, 5=SOLVENTES, 6=OTRASQUIMICAS, 7=GASES, 8=SUSTPSICOACT else Columnas quedan en NULL; si TIPOEXPO in (''1''..''8'') → Marca ''X'' según tipo de exposición: 1=OCUPACIONAL, 2=ACCIDENTAL, 3=SUICIDIO, 4=ACTOHOMICIDA, 5=ACTODELICTIVO, 6=DESCONOCIDA, 7=INTENCIONAL, 8=AUTOMEDICACION; si LUGARINTOX in (''1''..''8'') → Marca ''X'' según lugar de intoxicación: 1=HOGAR, 2=ESTEDUCATIVO, 3=ESTMILITAR, 4=ESTCOMERCIAL, 5=ESTPENITEN, 6=LUGARTRABAJO, 7=VIAPUBLICA, 8=BARES; si VIAEXPO in (''1''..''7'') → Marca ''X'' según vía de exposición: 1=RESPIRATORIA, 2=ORAL, 3=DERMICA, 4=OCULAR, 5=DESCONOCIDA, 6=PARENTERAL, 7=TRANSPLACENTARIA; si ESCOLARIDAD in (''1''..''14'') → Marca ''X'' según nivel educativo (preescolar, primaria, secundaria, académica, técnica, normalista, técnico-profesional, tecnológica, profesional, especialización, maestría, doctorado, ninguno, sin información); si AFILIADOARL = ''1'' / ''0'' → Marca ''X'' en ''AFILIADO Si'' o en ''AFILIADO No'' respectivamente; si ESTADOCIVIL in (''1''..''5'') → Marca ''X'' según estado civil: 1=SOLTERO, 2=CASADO, 3=UNIONLIBRE, 4=VIUDO, 5=DIVORCIADO; si CASOBROTE = ''1'' / ''0'' → Marca ''X'' en ''BROTE Si'' o ''BROTE No''; si SITUAALERTA = ''1'' / ''0'' → Marca ''X'' en ''ALERTA Si'' o ''ALERTA No''; si MUESTRASTOXICO = ''1'' / ''0'' → Marca ''X'' en ''TOXICO Si'' o ''TOXICO No''; si Cada flag de muestra (SANGRETOTAL, ORINA, TEJIDO, SUERO, AGUA, CABELLO, EMPAQUE, OTROS, UNAS) = ''1'' → Marca ''X'' en la columna del tipo de muestra correspondiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila365';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA365', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila365';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila365';
-- GO
