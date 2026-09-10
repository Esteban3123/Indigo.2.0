-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,27-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila430]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON; 
  			
			SELECT
				CASE MUESTRA WHEN '1' THEN 'Sangre Total' WHEN '2' THEN 'Tejido' WHEN '3' THEN 'Linfa' END AS 'MUESTRA',
				CASE PRUEBA  WHEN '1' THEN 'Estudio Directo' WHEN '2' THEN 'Titulo IFI' WHEN '3' THEN 'Aspirado Bazo' WHEN '4' THEN 'Aspirado Medula' WHEN '5' THEN'Prueba Montenegro' END AS 'PRUEBA',
				CASE AGENTE  WHEN '1' THEN 'Leishmania' END AS 'AGENTE',
				CASE RESULTADO  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'Compatible' WHEN '4' THEN 'No Compatible' END AS 'RESULTADO',
				CASE MUCOSA WHEN 1 THEN 'X' END AS 'Nasal',CASE MUCOSA WHEN 2 THEN 'X' END AS 'Cavidad Oral', CASE MUCOSA WHEN 3 THEN 'X' END AS 'Labios',
				CASE MUCOSA WHEN 4 THEN 'X' END AS 'Faringe',CASE MUCOSA WHEN 5 THEN 'X' END AS 'Laringe', CASE MUCOSA WHEN 6 THEN 'X' END AS 'Parpados',
				CASE MUCOSA WHEN 7 THEN 'X' END AS 'Genitales',
				convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),FECHARECE,103) As 'FECHARECE', convert(varchar(10),FECHARESUL,103) As 'FECHARESUL',
				Rtrim(PESOACT) As 'PESOACT', Rtrim(OTROCUAL) As 'OTROCUAL', Rtrim(NUMCAPS) As 'NUMCAPS', Rtrim(DIASTRAT) As 'DIASTRAT', Rtrim(TOTACAPS) As 'TOTACAPS', Rtrim(VALOR) As 'VALOR',
				CASE RECITRAT  WHEN '1' THEN 'X' END AS 'RECITRAT si',CASE RECITRAT WHEN '0' THEN 'X' END AS 'RECITRAT no',
				CASE TRATLOCAL  WHEN '1' THEN 'X' END AS 'Crioterapia',CASE TRATLOCAL WHEN '0' THEN 'X' END AS 'Termoterapia',
				CASE MEDIFORM WHEN 1 THEN 'X' END AS 'N-Metil',CASE MEDIFORM WHEN 2 THEN 'X' END AS 'Estibogluco', CASE MEDIFORM WHEN 3 THEN 'X' END AS 'Isotianato',
				CASE MEDIFORM WHEN 4 THEN 'X' END AS 'Anfotericina',CASE MEDIFORM WHEN 5 THEN 'X' END AS 'Otro', CASE MEDIFORM WHEN 6 THEN 'X' END AS 'Miltefosina',
				CASE MEDIFORM WHEN 7 THEN 'X' END AS 'Pentamidina',CASE MEDIFORM WHEN 8 THEN 'X' END AS 'SinTratamiento',
				CASE RINORREA WHEN '1' THEN 'X' END AS 'RINORREA',CASE EPISTAXIS WHEN '1' THEN 'X' END AS 'EPISTAXIS', CASE OBSTRNASAL WHEN '1' THEN 'X' END AS 'OBSTRNASAL',
				CASE DISFONIA WHEN '1' THEN 'X' END AS 'DISFONIA', CASE DISFAGIA WHEN '1' THEN 'X' END AS 'DISFAGIA', CASE HIPEREMIA WHEN '1' THEN 'X' END AS 'HIPEREMIA',
				CASE ULCERACION WHEN '1' THEN 'X' END AS 'ULCERACION',CASE PERFORACION WHEN '1' THEN 'X' END AS 'PERFORACION', CASE DESTRUCCION WHEN '1' THEN 'X' END AS 'DESTRUCCION',
				VERSION AS 'VERSION', 
				JSON AS 'JSON'

			FROM 
				HCFICHA430 
	
			WHERE
				IDFICHANOTIFICACION  = @IdFicha

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y presenta el detalle completo de la ficha SIVIGILA 430 correspondiente a la notificación epidemiológica de leishmaniasis. Consulta la tabla HCFICHA430 filtrando por el identificador de la ficha de notificación, y traduce los códigos numéricos almacenados a sus descripciones legibles: tipo de muestra tomada (sangre, tejido, linfa), prueba diagnóstica realizada (estudio directo, IFI, aspirado, prueba de Montenegro), agente etiológico identificado (Leishmania), resultado del laboratorio (positivo, negativo, compatible), mucosas comprometidas (nasal, oral, labios, faringe, laringe, párpados, genitales), síntomas clínicos presentes (rinorrea, epistaxis, obstrucción nasal, disfonía, disfagia, hiperemia, ulceración, perforación, destrucción), medicamento o esquema de tratamiento utilizado (N-Metil, Estibogluconato, Anfotericina, Miltefosina, Pentamidina, entre otros), tipo de tratamiento local (crioterapia, termoterapia), y datos del esquema terapéutico como peso actual del paciente, número de cápsulas, días de tratamiento, total de cápsulas y valor. Se usa para imprimir o visualizar la ficha de notificación obligatoria ante el SIVIGILA en casos de leishmaniasis cutánea y mucocutánea, dentro del módulo de historia clínica y epidemiología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila430';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila430';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y devuelve la información detallada de una ficha Sivigila (caso de Leishmaniasis) decodificando catálogos de muestra, prueba, agente, resultado, mucosa afectada, medicamentos y síntomas a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila430';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir en la tabla de fichas Sivigila 430 un registro cuyo identificador de notificación coincida con el valor de entrada; de lo contrario el resultado es vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila430';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros de la ficha Sivigila 430 cuyo identificador de notificación coincide con el parámetro recibido.; Las fechas se entregan en formato dd/mm/yyyy (estilo 103).; Los campos codificados (muestra, prueba, agente, resultado, mucosa, medicamento, tratamiento local, recibió tratamiento) se traducen a etiquetas legibles o marca ''X'' según el código almacenado.; Los valores de texto numérico/descriptivo se entregan sin espacios a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila430';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Leishmaniasis; Muestra clínica (sangre, tejido, linfa); Pruebas diagnósticas (IFI, Montenegro, aspirado bazo/médula); Compromiso de mucosas (nasal, oral, faríngea, laríngea, etc.); Tratamiento farmacológico (N-Metil, Estibogluconato, Anfotericina, Miltefosina, Pentamidina); Tratamiento local (crioterapia/termoterapia); Síntomas otorrinolaringológicos (rinorrea, epistaxis, disfonía, disfagia, ulceración, perforación)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila430';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA430: Cuando IDFICHANOTIFICACION coincide con el parámetro de ficha, se retorna un conjunto de resultados con los datos de la ficha decodificados (muestra, prueba, agente, resultado, mucosa, fechas en formato 103, medicamentos y síntomas marcados con ''X'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila430';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA430', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila430';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila430';
-- GO
