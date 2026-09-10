-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,27-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila420]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;

Select
	CASE MUESTRA WHEN '1' THEN 'Sangre Total' WHEN '2' THEN 'Tejido' WHEN '3' THEN 'Linfa' END AS 'MUESTRA',
	CASE PRUEBA  WHEN '1' THEN 'Estudio Directo' WHEN '2' THEN 'Aspirado Bazo' WHEN '3' THEN 'Aspirado Medula' WHEN '4' THEN'Prueba Montenegro' WHEN '5' THEN 'Biopsia' END AS 'PRUEBA',
	CASE AGENTE  WHEN '1' THEN 'Leishmania' END AS 'AGENTE',
	CASE RESULTADO  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'Compatible' WHEN '4' THEN 'No Compatible' END AS 'RESULTADO',
	convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),FECHARECE,103) As 'FECHARECE', convert(varchar(10),FECHARESUL,103) As 'FECHARESUL',
	Rtrim(PESOACT) As 'PESOACT', Rtrim(OTROCUAL) As 'OTROCUAL', Rtrim(NUMCAPS) As 'NUMCAPS', Rtrim(DIASTRAT) As 'DIASTRAT', Rtrim(TOTACAPS) As 'TOTACAPS', Rtrim(VALOR) As 'VALOR',
	CASE RECITRAT  WHEN '1' THEN 'X' END AS 'RECITRAT si',CASE RECITRAT WHEN '0' THEN 'X' END AS 'RECITRAT no',
	CASE TRATLOCAL  WHEN '1' THEN 'X' END AS 'Crioterapia',CASE TRATLOCAL WHEN '0' THEN 'X' END AS 'Termoterapia',
	CASE MEDIFORM WHEN '1' THEN 'X' END AS 'N-Metil',CASE MEDIFORM WHEN '2' THEN 'X' END AS 'Estibogluco', CASE MEDIFORM WHEN '3' THEN 'X' END AS 'Isotianato',
	CASE MEDIFORM WHEN '4' THEN 'X' END AS 'Anfotericina',CASE MEDIFORM WHEN '5' THEN 'X' END AS 'Otro', CASE MEDIFORM WHEN '6' THEN 'X' END AS 'Miltefosina',
	CASE MEDIFORM WHEN '7' THEN 'X' END AS 'Pentamidina',CASE MEDIFORM WHEN '8' THEN 'X' END AS 'SinTratamiento',
	CASE CARA WHEN '1' THEN 'X' END AS 'CARA',CASE TRONCO WHEN '1' THEN 'X' END AS 'TRONCO', CASE MIEMSUP WHEN '1' THEN 'X' END AS 'MIEMSUP',
	CASE MIEMINF WHEN '1' THEN 'X' END AS 'MIEMINF',
	VERSION AS 'VERSION', JSON AS 'JSON'
	From HCFICHA420 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA420
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y retorna el detalle clínico-epidemiológico de la Ficha 420 de SIVIGILA correspondiente a Leishmaniasis, a partir del identificador de la ficha de notificación obligatoria. Recupera información de laboratorio como el tipo de muestra biológica (sangre total, tejido, linfa), la prueba diagnóstica realizada (estudio directo, biopsia, prueba Montenegro, entre otras), el agente causal (Leishmania) y el resultado de la prueba (positivo, negativo, compatible). También incluye datos del tratamiento aplicado: medicamento formulado (N-Metil glucamina, Anfotericina B, Miltefosina, Pentamidina, etc.), tratamiento local (crioterapia o termoterapia), si hubo reincidencia en el tratamiento, peso actual del paciente, número y duración del tratamiento en días, total de cápsulas administradas, y las zonas corporales afectadas (cara, tronco, miembros superiores e inferiores). Este procedimiento es utilizado para visualizar y reportar la notificación epidemiológica obligatoria de casos de Leishmaniasis ante el sistema de vigilancia en salud pública SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila420';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila420';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información detallada de la ficha epidemiológica de Leishmaniasis (SIVIGILA 420), decodificando catálogos de muestra, prueba, agente, resultado, tratamiento y zonas afectadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila420';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la tabla de fichas 420 cuyo identificador de ficha de notificación coincida con el parámetro de entrada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila420';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas se entregan formateadas como dd/mm/aaaa (estilo 103).; Los valores numéricos/texto de peso, cápsulas, días de tratamiento y valor se devuelven sin espacios a la derecha (RTRIM).; Solo se retorna información de la ficha cuyo identificador de notificación coincide con el parámetro recibido.; Los códigos de catálogo no contemplados explícitamente en los CASE se retornan como NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila420';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; Leishmaniasis; Muestra clínica (Sangre, Tejido, Linfa); Pruebas diagnósticas (Estudio Directo, Aspirado Bazo/Médula, Montenegro, Biopsia); Tratamiento local (Crioterapia/Termoterapia); Medicamentos antileishmaniásicos (N-Metil, Estibogluconato, Isotianato, Anfotericina, Miltefosina, Pentamidina); Zonas anatómicas con lesión (cara, tronco, miembros superiores/inferiores)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila420';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA420: Cuando IDFICHANOTIFICACION coincide con el parámetro de ficha, retorna las columnas decodificadas (muestra, prueba, agente, resultado, fechas, tratamiento, lesiones) junto con VERSION y JSON.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila420';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MUESTRA = ''1'' | ''2'' | ''3'' → Se traduce a ''Sangre Total'', ''Tejido'' o ''Linfa'' respectivamente; si PRUEBA en ''1''..''5'' → Se traduce a Estudio Directo, Aspirado Bazo, Aspirado Médula, Prueba Montenegro o Biopsia; si AGENTE = ''1'' → Se traduce a ''Leishmania''; si RESULTADO en ''1''..''4'' → Se traduce a Positivo, Negativo, Compatible o No Compatible; si RECITRAT = ''1'' / ''0'' → Marca ''X'' en columna de recibió tratamiento (sí/no); si TRATLOCAL = ''1'' / ''0'' → Marca ''X'' en Crioterapia o Termoterapia; si MEDIFORM en ''1''..''8'' → Marca ''X'' sobre el medicamento correspondiente: N-Metil, Estibogluco, Isotianato, Anfotericina, Otro, Miltefosina, Pentamidina o SinTratamiento; si CARA/TRONCO/MIEMSUP/MIEMINF = ''1'' → Marca ''X'' indicando la zona corporal con lesión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila420';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA420', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila420';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila420';
-- GO
