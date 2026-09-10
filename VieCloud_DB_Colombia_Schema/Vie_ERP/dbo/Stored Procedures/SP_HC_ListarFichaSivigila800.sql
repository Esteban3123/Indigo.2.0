CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila800]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA800 where IDFICHANOTIFICACION  = 13

	Select
	convert(varchar(10),FECHAULTDOS1,103) As 'FECHAULTDOS1', convert(varchar(10),FECHAULTDOS2,103) As 'FECHAULTDOS2', convert(varchar(10),FECHAINVEST,103) As 'FECHAINVEST',
	convert(varchar(10),FECHATOMA1,103) As 'FECHATOMA1', convert(varchar(10),FECHATOMA2,103) As 'FECHATOMA2', convert(varchar(10),FECHARECE1,103) As 'FECHARECE1',
	convert(varchar(10),FECHARECE2,103) As 'FECHARECE2', convert(varchar(10),FECHARESUL1,103) As 'FECHARESUL1', convert(varchar(10),FECHARESUL2,103) As 'FECHARESUL2',
	CASE ANTEMATEVAC  WHEN '1' THEN 'X' END AS 'ANTEMATEVAC si',CASE ANTEMATEVAC WHEN '0' THEN 'X' END AS 'ANTEMATEVAC no',
	CASE INFERESPI  WHEN '1' THEN 'X' END AS 'INFERESPI si',CASE INFERESPI WHEN '0' THEN 'X' END AS 'INFERESPI no',
	CASE TOS  WHEN '1' THEN 'X' END AS 'TOS si',CASE TOS WHEN '0' THEN 'X' END AS 'TOS no',
	CASE TOSPARO  WHEN '1' THEN 'X' END AS 'TOSPARO si',CASE TOSPARO WHEN '0' THEN 'X' END AS 'TOSPARO no',
	CASE ESTRIDOR  WHEN '1' THEN 'X' END AS 'ESTRIDOR si',CASE ESTRIDOR WHEN '0' THEN 'X' END AS 'ESTRIDOR no',
	CASE APNEA  WHEN '1' THEN 'X' END AS 'APNEA si',CASE APNEA WHEN '0' THEN 'X' END AS 'APNEA no',
	CASE CIANOSIS  WHEN '1' THEN 'X' END AS 'CIANOSIS si',CASE CIANOSIS WHEN '0' THEN 'X' END AS 'CIANOSIS no',
	CASE VOMITO  WHEN '1' THEN 'X' END AS 'VOMITO si',CASE VOMITO WHEN '0' THEN 'X' END AS 'VOMITO no',
	CASE COMPLICA  WHEN '1' THEN 'X' END AS 'COMPLICA si',CASE COMPLICA WHEN '0' THEN 'X' END AS 'COMPLICA no',
	CASE TRATAANTIBIO  WHEN '1' THEN 'X' END AS 'TRATAANTIBIO si',CASE TRATAANTIBIO WHEN '0' THEN 'X' END AS 'TRATAANTIBIO no',
	CASE TIPOVACUNA WHEN '1' THEN 'X' END AS 'DPT',CASE TIPOVACUNA WHEN '2' THEN 'X' END AS 'PENTAVALENTE', CASE TIPOVACUNA WHEN '3' THEN 'X' END AS 'TADP',
	CASE CASOIDENT WHEN '1' THEN 'X' END AS 'ConsExt',CASE CASOIDENT WHEN '2' THEN 'X' END AS 'Urgencias', CASE CASOIDENT WHEN '3' THEN 'X' END AS 'Hospital',
	CASE CASOIDENT WHEN '4' THEN 'X' END AS 'BusquedaComuni',
	CASE DOSISAPLI WHEN '1' THEN 'X' END AS 'Ninguna',CASE DOSISAPLI WHEN '2' THEN 'X' END AS 'Una', CASE DOSISAPLI WHEN '3' THEN 'X' END AS 'Dos',
	CASE DOSISAPLI WHEN '4' THEN 'X' END AS 'Tres',CASE DOSISAPLI WHEN '5' THEN 'X' END AS 'PrimRefue', CASE DOSISAPLI WHEN '6' THEN 'X' END AS 'SegRefue',
	CASE ETAPAENFE WHEN '1' THEN 'X' END AS 'Catarral',CASE ETAPAENFE WHEN '2' THEN 'X' END AS 'Espasmodica', CASE ETAPAENFE WHEN '3' THEN 'X' END AS 'Convaleciente',
	CASE TIPOCOMPLI WHEN '1' THEN 'X' END AS 'Convulsiones',CASE TIPOCOMPLI WHEN '2' THEN 'X' END AS 'Atelectasia', CASE TIPOCOMPLI WHEN '3' THEN 'X' END AS 'Neumotorax',
	CASE TIPOCOMPLI WHEN '4' THEN 'X' END AS 'Neumonia',CASE TIPOCOMPLI WHEN '5' THEN 'X' END AS 'Otro',
	Rtrim(NOMMADRE) As 'NOMMADRE', 
	CASE Rtrim(TIPOID)  WHEN '1' THEN 'RC - Registro Civil' WHEN '2' THEN 'TI - Tarjeta de Identidad' WHEN '3' THEN 'CC - Cédula de Ciudadanía' WHEN '4' THEN 'CE - Cédula de Extranjería' WHEN '5' THEN 'PA - Pasaporte' WHEN '6' THEN 'MS - Menor Sin Identificación' WHEN '7' THEN 'AS - Adulto Sin Identificación' WHEN '8' THEN 'PE - Permiso Especial de Permanencia' WHEN '9' THEN 'PT - Permiso Temporal de Permanencia' END AS 'TIPOID',
	Rtrim(NUMIDENT) As 'NUMIDENT', Rtrim(DURATOS) As 'DURATOS', Rtrim(TIPOANTIBIO) As 'TIPOANTIBIO', Rtrim(DURATRATA) As 'DURATRATA',
	CASE MUESTRA1 WHEN '1' THEN 'HISOPADO NASOFARINGEO' WHEN '2' THEN 'ASPIRADO NASOFARINGEO' WHEN '3' THEN 'SUERO' WHEN '4' THEN 'TEJIDO' WHEN '5' THEN 'LAVADO BRONQUIAL' END AS 'MUESTRA1',
	CASE PRUEBA1  WHEN '1' THEN 'CULTIVO' WHEN '2' THEN 'PATOLOGIA' WHEN '3' THEN 'PCR' WHEN '4' THEN 'IgG' END AS 'PRUEBA1',
	CASE AGENTE1  WHEN '1' THEN 'BORDETELLA PERTUSSIS' WHEN '2' THEN 'BORDETELLA PARAPERTUSSIS' WHEN '3' THEN 'BORDETELLA SPP' WHEN '4' THEN 'BORDETELLA HOLMESII' END AS 'AGENTE1',
	CASE RESULTADO1  WHEN '1' THEN 'POSITIVO' WHEN '2' THEN 'NEGATIVO' WHEN '3' THEN 'NO PROCESADO' WHEN '4' THEN 'INADECUADO' WHEN '5' THEN 'BORDERLINE' END AS 'RESULTADO1',
	CASE MUESTRA2 WHEN '1' THEN 'HISOPADO NASOFARINGEO' WHEN '2' THEN 'ASPIRADO NASOFARINGEO' WHEN '3' THEN 'SUERO' WHEN '4' THEN 'TEJIDO' WHEN '5' THEN 'LAVADO BRONQUIAL' END AS 'MUESTRA2',
	CASE PRUEBA2 WHEN '1' THEN 'CULTIVO' WHEN '2' THEN 'PATOLOGIA' WHEN '3' THEN 'PCR' WHEN '4' THEN 'IgG' END AS 'PRUEBA2',
	CASE AGENTE2  WHEN '1' THEN 'BORDETELLA PERTUSSIS' WHEN '2' THEN 'BORDETELLA PARAPERTUSSIS' WHEN '3' THEN 'BORDETELLA SPP' WHEN '4' THEN 'BORDETELLA HOLMESII' END AS 'AGENTE2',
	CASE RESULTADO2  WHEN '1' THEN 'POSITIVO' WHEN '2' THEN 'NEGATIVO' WHEN '3' THEN 'NO PROCESADO' WHEN '4' THEN 'INADECUADO' WHEN '5' THEN 'BORDERLINE' END AS 'RESULTADO2',
	CASE INVESTCAMP  WHEN '1' THEN 'X' END AS 'INVESTCAMP si',CASE INVESTCAMP WHEN '0' THEN 'X' END AS 'INVESTCAMP no'

	From HCFICHA800 where IDFICHANOTIFICACION  = @IdFicha

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de una ficha de notificación epidemiológica de tosferina (código 800 del SIVIGILA) a partir de su identificador. Consulta la tabla HCFICHA800 y retorna los datos del caso listos para imprimir o exportar: fechas clave (última dosis de vacuna, investigación, toma y recepción de muestras, resultados de laboratorio), síntomas presentes (tos, tos paroxística, estridor, apnea, cianosis, vómito), complicaciones, tratamiento antibiótico, etapa de la enfermedad, tipo y dosis de vacuna aplicada, lugar donde se identificó el caso (consulta externa, urgencias, hospitalización, búsqueda comunitaria), tipo y número de identificación del paciente, nombre de la madre, y resultados de hasta dos muestras de laboratorio con su tipo, prueba realizada (cultivo, PCR, IgG, etc.) y agente etiológico detectado (Bordetella pertussis y variantes). Se usa en el módulo de vigilancia epidemiológica para visualizar, imprimir o revisar la ficha oficial de notificación de un caso de tosferina ante el SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila800';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila800';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para impresión los datos de una ficha de notificación epidemiológica de tos ferina (Sivigila ficha 800), traduciendo códigos a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila800';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA800 con IDFICHANOTIFICACION igual al identificador recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila800';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fecha se devuelve siempre en formato dd/mm/yyyy (estilo 103).; Los campos booleanos se representan como ''X'' en columnas separadas ''si''/''no'', nunca como 1/0.; Los catálogos de tipo de identificación, muestras, pruebas, agentes y resultados son fijos y cerrados; valores fuera de rango se devuelven como NULL.; El parámetro @NumFicha se recibe pero no se usa en la consulta; el filtro real es por IDFICHANOTIFICACION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila800';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila 800; Tos ferina / Bordetella pertussis; Vacunación (DPT, Pentavalente, TdaP); Dosis aplicadas y refuerzos; Etapas clínicas (catarral, espasmódica, convaleciente); Complicaciones respiratorias (apnea, cianosis, neumonía, atelectasia, neumotórax); Muestras y pruebas de laboratorio (hisopado/aspirado nasofaríngeo, cultivo, PCR, IgG); Tipos de documento de identidad colombianos; Investigación de campo epidemiológica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila800';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA800: Cuando IDFICHANOTIFICACION coincide con el parámetro, devuelve un único conjunto de resultados con fechas en formato dd/mm/yyyy (estilo 103) y códigos clínicos traducidos a marcas ''X'' o textos descriptivos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila800';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ANTEMATEVAC, INFERESPI, TOS, TOSPARO, ESTRIDOR, APNEA, CIANOSIS, VOMITO, COMPLICA, TRATAANTIBIO, INVESTCAMP = ''1'' o ''0'' → Marca con ''X'' la columna ''si'' o ''no'' correspondiente a cada síntoma/antecedente.; si TIPOVACUNA = 1/2/3 → Marca DPT, PENTAVALENTE o TADP respectivamente.; si CASOIDENT = 1/2/3/4 → Marca lugar de identificación: Consulta Externa, Urgencias, Hospital o Búsqueda Comunitaria.; si DOSISAPLI = 1..6 → Marca dosis aplicadas: Ninguna, Una, Dos, Tres, Primer Refuerzo o Segundo Refuerzo.; si ETAPAENFE = 1/2/3 → Marca etapa de enfermedad: Catarral, Espasmódica o Convaleciente.; si TIPOCOMPLI = 1..5 → Marca complicación: Convulsiones, Atelectasia, Neumotórax, Neumonía u Otro.; si TIPOID entre 1 y 9 → Traduce a etiqueta de tipo de documento (RC, TI, CC, CE, PA, MS, AS, PE, PT).; si MUESTRA1/MUESTRA2 = 1..5 → Traduce a tipo de muestra (Hisopado/Aspirado nasofaríngeo, Suero, Tejido, Lavado bronquial).; si PRUEBA1/PRUEBA2 = 1..4 → Traduce a tipo de prueba (Cultivo, Patología, PCR, IgG).; si AGENTE1/AGENTE2 = 1..4 → Traduce a especie de Bordetella (pertussis, parapertussis, spp, holmesii).; si RESULTADO1/RESULTADO2 = 1..5 → Traduce a resultado de laboratorio (Positivo, Negativo, No procesado, Inadecuado, Borderline).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila800';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA800', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila800';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila800';
-- GO
