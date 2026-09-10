-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,28-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila440]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
		
		Select
				CASE MUESTRA WHEN '1' THEN 'Sangre Total' WHEN '2' THEN 'Tejido' WHEN '3' THEN 'Linfa' END AS 'MUESTRA',
				CASE PRUEBA  WHEN '1' THEN 'Hematocrito' WHEN '2' THEN 'Hemoglobina' WHEN '3' THEN 'Plaquetas' WHEN '4' THEN 'Estudio Directo' WHEN '5' THEN'Titulo IFI' 
				WHEN '6' THEN 'Aspirado Bazo' WHEN '7' THEN 'Aspirado Médula' WHEN '8' THEN 'Prueba Montenegro' WHEN '9' THEN 'Albumina' END AS 'PRUEBA',
				CASE AGENTE  WHEN '1' THEN 'Leishmania' END AS 'AGENTE',
				CASE RESULTADO  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'Compatible' WHEN '4' THEN 'No Compatible' END AS 'RESULTADO',
				CASE DIAGCONF WHEN '1' THEN 'X' END AS 'Si',CASE DIAGCONF WHEN '2' THEN 'X' END AS 'No', CASE DIAGCONF WHEN '3' THEN 'X' END AS 'Desconocido',
				convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),FECHARECE,103) As 'FECHARECE', convert(varchar(10),FECHARESUL,103) As 'FECHARESUL',
				Rtrim(PESOACT) As 'PESOACT', Rtrim(OTROCUAL) As 'OTROCUAL', Rtrim(NUMCAPS) As 'NUMCAPS', Rtrim(DIASTRAT) As 'DIASTRAT', Rtrim(TOTACAPS) As 'TOTACAPS', Rtrim(VALOR) As 'VALOR',
				CASE RECITRAT  WHEN '1' THEN 'X' END AS 'RECITRAT si',CASE RECITRAT WHEN '0' THEN 'X' END AS 'RECITRAT no',
				CASE TRATLOCAL  WHEN '1' THEN 'X' END AS 'Crioterapia',CASE TRATLOCAL WHEN '0' THEN 'X' END AS 'Termoterapia',
				CASE MEDIFORM WHEN 1 THEN 'X' END AS 'N-Metil',CASE MEDIFORM WHEN 2 THEN 'X' END AS 'Estibogluco', CASE MEDIFORM WHEN 3 THEN 'X' END AS 'Isotianato',
				CASE MEDIFORM WHEN 4 THEN 'X' END AS 'Anfotericina',CASE MEDIFORM WHEN 5 THEN 'X' END AS 'Otro', CASE MEDIFORM WHEN 6 THEN 'X' END AS 'Miltefosina',
				CASE MEDIFORM WHEN 7 THEN 'X' END AS 'Pentamidina',CASE MEDIFORM WHEN 8 THEN 'X' END AS 'SinTratamiento',
				CASE FIEBRE WHEN '1' THEN 'X' END AS 'FIEBRE',CASE HEPATO WHEN '1' THEN 'X' END AS 'HEPATO', CASE ESPLENO WHEN '1' THEN 'X' END AS 'ESPLENO',
				CASE ANEMIA WHEN '1' THEN 'X' END AS 'ANEMIA', CASE LEUCOCI WHEN '1' THEN 'X' END AS 'LEUCOCI', CASE PLAQUET WHEN '1' THEN 'X' END AS 'PLAQUET', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
	
			From 
				HCFICHA440 

			Where 
				IDFICHANOTIFICACION  = @IdFicha 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de una ficha de notificación SIVIGILA código 440 (Leishmaniasis), dado el identificador o número de ficha. Consulta la tabla HCFICHA440 y traduce los códigos numéricos almacenados a sus descripciones legibles en español: tipo de muestra biológica (sangre total, tejido, linfa), prueba diagnóstica realizada (hematocrito, hemoglobina, plaquetas, gota gruesa, entre otras), agente etiológico identificado (Leishmania), resultado de laboratorio (positivo, negativo, compatible), diagnóstico confirmado, medicamento formulado (N-Metilglucamina, Estibogluconato, Anfotericina B, Miltefosina, Pentamidina, entre otros), tratamiento local aplicado (crioterapia o termoterapia), manifestaciones clínicas presentes (fiebre, hepatomegalia, esplenomegalia, anemia, leucocitosis, plaquetopenia), así como fechas de toma de muestra, recepción y resultado, peso actual del paciente, número de cápsulas, días de tratamiento y dosis. Se usa para imprimir o visualizar la ficha epidemiológica de Leishmaniasis en la historia clínica electrónica dentro del módulo de vigilancia en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila440';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila440';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para presentación los datos clínicos y de tratamiento de una ficha de notificación del evento Sivigila 440 (Leishmaniasis), traduciendo códigos a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila440';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA440 con IDFICHANOTIFICACION igual al identificador de ficha recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila440';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas FECHATOMA, FECHARECE y FECHARESUL se devuelven siempre formateadas como dd/mm/yyyy (estilo 103); Los campos de texto numérico (PESOACT, OTROCUAL, NUMCAPS, DIASTRAT, TOTACAPS, VALOR) se devuelven sin espacios a la derecha (RTRIM); Los códigos no contemplados en los CASE quedan como NULL en la salida (sin valor por defecto); Solo se reconoce un único agente etiológico (Leishmania); El parámetro NumFicha no se utiliza en el filtro; el filtrado se realiza únicamente por IDFICHANOTIFICACION', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila440';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Leishmaniasis; Muestra de laboratorio (Sangre/Tejido/Linfa); Pruebas diagnósticas (Hematocrito, Hemoglobina, IFI, Montenegro, Aspirado de bazo/médula); Diagnóstico confirmado; Tratamiento local (Crioterapia/Termoterapia); Medicamentos antileishmaniales (N-Metil glucamina, Estibogluconato, Isotianato, Anfotericina, Miltefosina, Pentamidina); Signos clínicos (fiebre, hepatomegalia, esplenomegalia, anemia, leucopenia, plaquetopenia); Esquema de tratamiento (cápsulas, días, dosis)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila440';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA440: Cuando IDFICHANOTIFICACION coincide con el parámetro de entrada, se retorna el conjunto de campos de la ficha con códigos traducidos a texto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila440';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MUESTRA = ''1'' / ''2'' / ''3'' → Se etiqueta como ''Sangre Total'', ''Tejido'' o ''Linfa'' respectivamente; si PRUEBA entre ''1''..''9'' → Se traduce a Hematocrito, Hemoglobina, Plaquetas, Estudio Directo, Titulo IFI, Aspirado Bazo, Aspirado Médula, Prueba Montenegro o Albumina; si AGENTE = ''1'' → Se etiqueta como ''Leishmania'' (único agente soportado); si RESULTADO = ''1''..''4'' → Se traduce a Positivo, Negativo, Compatible o No Compatible; si DIAGCONF = ''1'' / ''2'' / ''3'' → Marca con ''X'' la columna Si, No o Desconocido respectivamente; si RECITRAT = ''1'' vs ''0'' → Marca con ''X'' la columna ''RECITRAT si'' o ''RECITRAT no''; si TRATLOCAL = ''1'' vs ''0'' → Marca con ''X'' la columna ''Crioterapia'' o ''Termoterapia''; si MEDIFORM entre 1..8 → Marca con ''X'' la columna del medicamento correspondiente: N-Metil, Estibogluco, Isotianato, Anfotericina, Otro, Miltefosina, Pentamidina o SinTratamiento; si FIEBRE/HEPATO/ESPLENO/ANEMIA/LEUCOCI/PLAQUET = ''1'' → Marca con ''X'' la columna clínica correspondiente como signo/síntoma presente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila440';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA440', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila440';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila440';
-- GO
