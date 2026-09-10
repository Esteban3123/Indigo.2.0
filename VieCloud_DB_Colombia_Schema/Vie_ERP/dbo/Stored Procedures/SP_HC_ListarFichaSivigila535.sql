-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,05-12-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila535]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA535 where IDFICHANOTIFICACION  = 13

	Select
	CASE USOANTIBIO  WHEN '1' THEN 'X' END AS 'USOANTIBIO si',CASE USOANTIBIO WHEN '0' THEN 'X' END AS 'USOANTIBIO no',
	CASE TIPOAGENTE WHEN '1' THEN 'X' END AS 'Haemophilus',CASE TIPOAGENTE WHEN '2' THEN 'X' END AS 'Neisseria', CASE TIPOAGENTE WHEN '3' THEN 'X' END AS 'Streptococcus',
	CASE TIPOAGENTE WHEN '4' THEN 'X' END AS 'OtrosAgentes',CASE TIPOAGENTE WHEN '5' THEN 'X' END AS 'AgentsinDet',
	CASE VACUANTIHIB WHEN '1' THEN 'X' END AS 'Si1',CASE VACUANTIHIB WHEN '2' THEN 'X' END AS 'No1', CASE VACUANTIHIB WHEN '3' THEN 'X' END AS 'Desconocido1',
	CASE VACUANTIMENINGO WHEN '1' THEN 'X' END AS 'Si2',CASE VACUANTIMENINGO WHEN '2' THEN 'X' END AS 'No2', CASE VACUANTIMENINGO WHEN '3' THEN 'X' END AS 'Desconocido2',
	CASE VACUANTINEUMO WHEN '1' THEN 'X' END AS 'Si3',CASE VACUANTINEUMO WHEN '2' THEN 'X' END AS 'No3', CASE VACUANTINEUMO WHEN '3' THEN 'X' END AS 'Desconocido3',
	CASE DOSIS1 WHEN '1' THEN 'X' END AS 'DOSIS1 1',CASE DOSIS1 WHEN '2' THEN 'X' END AS 'DOSIS1 2', CASE DOSIS1 WHEN '3' THEN 'X' END AS 'DOSIS1 3',
	CASE DOSIS2 WHEN '1' THEN 'X' END AS 'DOSIS2 1',CASE DOSIS2 WHEN '2' THEN 'X' END AS 'DOSIS2 2', CASE DOSIS2 WHEN '3' THEN 'X' END AS 'DOSIS2 3',
	CASE DOSIS3 WHEN '1' THEN 'X' END AS 'DOSIS3 1',CASE DOSIS3 WHEN '2' THEN 'X' END AS 'DOSIS3 2', CASE DOSIS3 WHEN '3' THEN 'X' END AS 'DOSIS3 3',
	convert(varchar(10),FECHAULTIDO1,103) As 'FECHAULTIDO1', convert(varchar(10),FECHAULTIDO2,103) As 'FECHAULTIDO2', convert(varchar(10),FECHAULTIDO3,103) As 'FECHAULTIDO3',
	convert(varchar(10),FECHAULTIDO4,103) As 'FECHAULTIDO4', convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),FECHARECE,103) As 'FECHARECE',
	convert(varchar(10),FECHARESUL,103) As 'FECHARESUL',
	CASE CLASIFCASO WHEN '1' THEN 'X' END AS 'Meningitis',CASE CLASIFCASO WHEN '2' THEN 'X' END AS 'MeningMeningo', CASE CLASIFCASO WHEN '3' THEN 'X' END AS 'MeningosinMeningi',
	CASE MUESTRA WHEN '1' THEN 'Sangre Total' WHEN '2' THEN 'LCR' END AS 'MUESTRA',
	CASE PRUEBA  WHEN '1' THEN 'Aislamiento' WHEN '2' THEN 'Cultivo' WHEN '3' THEN 'Coloración de gram' WHEN '4' THEN'Antigenemia' WHEN '5' THEN 'RT/PCR' END AS 'PRUEBA',
	CASE AGENTE  WHEN '1' THEN 'Otro' WHEN '2' THEN 'Haemophilus Influenzae' WHEN '3' THEN 'Neisseria Meningitidis' WHEN '4' THEN 'Streptococus pneumonie' 
	WHEN '6' THEN 'Staphylococcus Aereus' WHEN '7' THEN 'Listeria Monocytogenes' WHEN '8' THEN 'E. Coli' WHEN '9' THEN 'Enterobacter clocae' WHEN '10' THEN 'Staphylococcus Epidermis' WHEN '11' THEN 'Streptococcus beta hemiolítico'
	WHEN '12' THEN 'Streptococcus Agalactiae' WHEN '13' THEN 'Streptococcus Pyogenes' END AS 'AGENTE',
	Rtrim(VALOR) As 'VALOR',
	CASE RESULTADO  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' END AS 'RESULTADO'

	From HCFICHA535 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA535
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que recupera y formatea el detalle de la ficha epidemiológica 535 de Sivigila para casos de meningitis bacteriana, dado un identificador de ficha (@IdFicha). Consulta la tabla HCFICHA535 y traduce todos los códigos numéricos a etiquetas legibles: uso de antibióticos (sí/no), tipo de agente causante (Haemophilus, Neisseria, Streptococcus, otros), estado de vacunación antiHib, antimeningocócica y antineumocócica con sus dosis, fechas clave (última dosis por vacuna, toma de muestra, recepción y resultado de laboratorio), clasificación del caso (meningitis, meningococcemia, meningococcemia sin meningitis), tipo de muestra (sangre total o LCR), prueba diagnóstica (aislamiento, cultivo, Gram, antigenemia, RT/PCR), agente identificado y resultado (positivo/negativo). Se usa para imprimir o visualizar el formulario oficial de notificación obligatoria de meningitis bacteriana ante el sistema de vigilancia epidemiológica nacional (Sivigila).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila535';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila535';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para presentación los datos de la ficha epidemiológica SIVIGILA 535 (meningitis bacteriana) asociada a una notificación específica, traduciendo códigos a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila535';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA535 cuyo IDFICHANOTIFICACION coincida con el identificador recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila535';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas se presentan siempre en formato británico/europeo dd/mm/yyyy (estilo 103).; Códigos numéricos no contemplados en los CASE producen NULL en la columna traducida.; El valor ''5'' no está mapeado para AGENTE (se omite intencionalmente).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila535';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'SIVIGILA; Ficha de notificación epidemiológica; Meningitis bacteriana; Vacunación (Hib, Meningococo, Neumococo); Uso de antibióticos previos; Muestra clínica (Sangre, LCR); Pruebas de laboratorio (Cultivo, Gram, Antigenemia, RT/PCR); Agente etiológico; Clasificación del caso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila535';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA535: Devuelve las filas de HCFICHA535 filtradas por IDFICHANOTIFICACION = parámetro, con columnas codificadas convertidas a ''X'' o a descripciones textuales (tipo de agente, muestra, prueba, agente etiológico, resultado) y fechas formateadas en estilo 103 (dd/mm/yyyy).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila535';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si USOANTIBIO = ''1'' / ''0'' → Marca ''X'' en columna ''USOANTIBIO si'' o ''USOANTIBIO no'' respectivamente; si TIPOAGENTE entre ''1'' y ''5'' → Marca ''X'' en la columna correspondiente: Haemophilus, Neisseria, Streptococcus, OtrosAgentes o AgentsinDet; si VACUANTIHIB / VACUANTIMENINGO / VACUANTINEUMO = ''1'',''2'',''3'' → Marca ''X'' en Si/No/Desconocido para cada vacuna (Hib, Meningococo, Neumococo); si MUESTRA = ''1'' o ''2'' → Traduce a ''Sangre Total'' o ''LCR''; si PRUEBA en ''1''..''5'' → Traduce a Aislamiento, Cultivo, Coloración de gram, Antigenemia o RT/PCR; si AGENTE en ''1'',''2'',''3'',''4'',''6''..''13'' → Traduce al nombre del microorganismo (Haemophilus Influenzae, Neisseria Meningitidis, Streptococcus pneumoniae, Staphylococcus aureus, Listeria monocytogenes, E. Coli, Enterobacter cloacae, etc.); si CLASIFCASO = ''1'',''2'',''3'' → Clasifica como Meningitis, MeningMeningo o MeningosinMeningi; si RESULTADO = ''1'' o ''2'' → Traduce a ''Positivo'' o ''Negativo''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila535';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA535', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila535';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila535';
-- GO
