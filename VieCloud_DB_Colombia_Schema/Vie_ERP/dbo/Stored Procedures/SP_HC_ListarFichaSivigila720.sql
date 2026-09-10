-- Stored Procedure
-- =============================================
-- Autor:		Jean Carlos Roldan Lozano
-- Fecha Creacion: 26-11-2018
-- Descripcion:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 720
-- Modifico:    Yezid Garcia Medina
-- Fecha Modificación : 03-01-2022
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila720]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
	SET NOCOUNT ON;

			Select
				CASE CLASIFINIC WHEN '1' THEN 'X' END AS 'Sospechoso Datos', CASE CLASIFINIC WHEN '0' THEN 'X' END AS 'Sospechoso Hijo',
				Rtrim(LUGNACIPAC) As 'LUGNACIPAC', 
				CASE FUENTENOTI WHEN '1' THEN 'X' END AS 'Pública', CASE FUENTENOTI WHEN '2' THEN 'X' END AS 'Privada', CASE FUENTENOTI WHEN '3' THEN 'X' END AS 'Laboratorio',
				CASE FUENTENOTI WHEN '4' THEN 'X' END AS 'Comunidad', CASE FUENTENOTI WHEN '5' THEN 'X' END AS 'Búsqueda Activa', CASE FUENTENOTI WHEN '6' THEN 'X' END AS 'Otras',
				CASE FUENTENOTI WHEN '7' THEN 'X' END AS 'Desconocida',
				Rtrim(NOMMADRE) As 'NOMMADRE', Rtrim(EDADMADRE) As 'EDADMADRE', Rtrim(EMBARAZOS) As 'EMBARAZOS', Rtrim(SEMEMBA) As 'SEMEMBA',
				Rtrim(PAISVIAJO) As 'PAISVIAJO', Rtrim(DEPAMUNIVIAJ2) As 'DEPAMUNIVIAJ2',
				CASE VIAJES WHEN '1' THEN 'X' END AS 'VIAJES si', CASE VIAJES WHEN '2' THEN 'X' END AS 'VIAJES No', CASE VIAJES WHEN '3' THEN 'X' END AS 'VIAJES Desconocido',
				Rtrim(APGAR) As 'APGAR', Rtrim(PESO) As 'PESO', Rtrim(SEMANAS) As 'SEMANAS',
				CASE BAJOPESNAC WHEN '1' THEN 'X' END AS 'BAJOPESNAC si', CASE BAJOPESNAC WHEN '2' THEN 'X' END AS 'BAJOPESNAC No', CASE BAJOPESNAC WHEN '3' THEN 'X' END AS 'BAJOPESNAC Desconocido',
				CASE PEQEDAGEST WHEN '1' THEN 'X' END AS 'PEQEDAGEST si', CASE PEQEDAGEST WHEN '2' THEN 'X' END AS 'PEQEDAGEST No', CASE PEQEDAGEST WHEN '3' THEN 'X' END AS 'PEQEDAGEST Desconocido',	
				CASE CATARATAS WHEN '1' THEN 'X' END AS 'CATARATAS si', CASE CATARATAS WHEN '2' THEN 'X' END AS 'CATARATAS No', CASE CATARATAS WHEN '3' THEN 'X' END AS 'CATARATAS Desconocido',
				CASE GLAUCOMA WHEN '1' THEN 'X' END AS 'GLAUCOMA si', CASE GLAUCOMA WHEN '2' THEN 'X' END AS 'GLAUCOMA No', CASE GLAUCOMA WHEN '3' THEN 'X' END AS 'GLAUCOMA Desconocido',
				CASE RETINOPA WHEN '1' THEN 'X' END AS 'RETINOPA si', CASE RETINOPA WHEN '2' THEN 'X' END AS 'RETINOPA No', CASE RETINOPA WHEN '3' THEN 'X' END AS 'RETINOPA Desconocido',
				Rtrim(OTRO1) As 'OTRO1', Rtrim(OTRO2) As 'OTRO2', Rtrim(OTRO3) As 'OTRO3', 
				CASE PERSISTENT WHEN '1' THEN 'X' END AS 'PERSISTENT si', CASE PERSISTENT WHEN '2' THEN 'X' END AS 'PERSISTENT No', CASE PERSISTENT WHEN '3' THEN 'X' END AS 'PERSISTENT Desconocido',
				CASE ESTENOSIS WHEN '1' THEN 'X' END AS 'ESTENOSIS si', CASE ESTENOSIS WHEN '2' THEN 'X' END AS 'ESTENOSIS No', CASE ESTENOSIS WHEN '3' THEN 'X' END AS 'ESTENOSIS Desconocido',
				CASE SORDERA WHEN '1' THEN 'X' END AS 'SORDERA si', CASE SORDERA WHEN '2' THEN 'X' END AS 'SORDERA No', CASE SORDERA WHEN '3' THEN 'X' END AS 'SORDERA Desconocido',
				CASE MICROCEF WHEN '1' THEN 'X' END AS 'MICROCEF si', CASE MICROCEF WHEN '2' THEN 'X' END AS 'MICROCEF No', CASE MICROCEF WHEN '3' THEN 'X' END AS 'MICROCEF Desconocido',
				CASE RETRASO WHEN '1' THEN 'X' END AS 'RETRASO si', CASE RETRASO WHEN '2' THEN 'X' END AS 'RETRASO No', CASE RETRASO WHEN '3' THEN 'X' END AS 'RETRASO Desconocido',
				CASE PURPURA WHEN '1' THEN 'X' END AS 'PURPURA si', CASE PURPURA WHEN '2' THEN 'X' END AS 'PURPURA No', CASE PURPURA WHEN '3' THEN 'X' END AS 'PURPURA Desconocido',
				CASE HIGADO WHEN '1' THEN 'X' END AS 'HIGADO si', CASE HIGADO WHEN '2' THEN 'X' END AS 'HIGADO No', CASE HIGADO WHEN '3' THEN 'X' END AS 'HIGADO Desconocido',
				CASE ICTERICIA WHEN '1' THEN 'X' END AS 'ICTERICIA si', CASE ICTERICIA WHEN '2' THEN 'X' END AS 'ICTERICIA No', CASE ICTERICIA WHEN '3' THEN 'X' END AS 'ICTERICIA Desconocido',
				CASE BAZO WHEN '1' THEN 'X' END AS 'BAZO si', CASE BAZO WHEN '2' THEN 'X' END AS 'BAZO No', CASE BAZO WHEN '3' THEN 'X' END AS 'BAZO Desconocido',
				CASE OSTEOPA WHEN '1' THEN 'X' END AS 'OSTEOPA si', CASE OSTEOPA WHEN '2' THEN 'X' END AS 'OSTEOPA No', CASE OSTEOPA WHEN '3' THEN 'X' END AS 'OSTEOPA Desconocido',
				CASE MENINGO WHEN '1' THEN 'X' END AS 'MENINGO si', CASE MENINGO WHEN '2' THEN 'X' END AS 'MENINGO No', CASE MENINGO WHEN '3' THEN 'X' END AS 'MENINGO Desconocido',
				convert(varchar(10),FECHAINICIO,103) As 'FECHAINICIO',
				CASE DIAGNOFIN WHEN '1' THEN 'X' END AS 'Infección Congénita', CASE DIAGNOFIN WHEN '2' THEN 'X' END AS 'Diagnos Otro', CASE DIAGNOFIN WHEN '3' THEN 'X' END AS 'Diagno Desconocido',
				Rtrim(INVESTPOR) As 'INVESTPOR', Rtrim(TELEFONO) As 'TELEFONO',
				convert(varchar(10),FECHATOMA1,103) As 'FECHATOMA1', convert(varchar(10),FECHATOMA2,103) As 'FECHATOMA2', convert(varchar(10),FECHATOMA3,103) As 'FECHATOMA3',
				convert(varchar(10),FECHATOMA4,103) As 'FECHATOMA4', convert(varchar(10),FECHARECE1,103) As 'FECHARECE1', convert(varchar(10),FECHARECE2,103) As 'FECHARECE2',
				convert(varchar(10),FECHARECE3,103) As 'FECHARECE3', convert(varchar(10),FECHARECE4,103) As 'FECHARECE4',
				CASE MUESTRA1 WHEN '1' THEN 'Orina' WHEN '2' THEN 'Hisopado' WHEN '3' THEN 'Tejido' WHEN '4' THEN 'Aspirado' WHEN '5' THEN 'Suero' END AS 'MUESTRA1',
				CASE MUESTRA2 WHEN '1' THEN 'Orina' WHEN '2' THEN 'Hisopado' WHEN '3' THEN 'Tejido' WHEN '4' THEN 'Aspirado' WHEN '5' THEN 'Suero' END AS 'MUESTRA2',
				CASE MUESTRA3 WHEN '1' THEN 'Orina' WHEN '2' THEN 'Hisopado' WHEN '3' THEN 'Tejido' WHEN '4' THEN 'Aspirado' WHEN '5' THEN 'Suero' END AS 'MUESTRA3',
				CASE MUESTRA4 WHEN '1' THEN 'Orina' WHEN '2' THEN 'Hisopado' WHEN '3' THEN 'Tejido' WHEN '4' THEN 'Aspirado' WHEN '5' THEN 'Suero' END AS 'MUESTRA4',
				CASE PRUEBA1 WHEN '1' THEN 'PCR' WHEN '2' THEN 'Patología' WHEN '3' THEN 'Elisa' WHEN '4' THEN 'Aislamiento Viral' END AS 'PRUEBA1',
				CASE PRUEBA2 WHEN '1' THEN 'PCR' WHEN '2' THEN 'Patología' WHEN '3' THEN 'Elisa' WHEN '4' THEN 'Aislamiento Viral' END AS 'PRUEBA2',
				CASE PRUEBA3 WHEN '1' THEN 'PCR' WHEN '2' THEN 'Patología' WHEN '3' THEN 'Elisa' WHEN '4' THEN 'Aislamiento Viral' END AS 'PRUEBA3',
				CASE PRUEBA4 WHEN '1' THEN 'PCR' WHEN '2' THEN 'Patología' WHEN '3' THEN 'Elisa' WHEN '4' THEN 'Aislamiento Viral' END AS 'PRUEBA4',
				CASE AGENTE1 WHEN '1' THEN 'Rubeola' WHEN '2' THEN 'Citomegalovirus' WHEN '3' THEN 'Toxoplasma' WHEN '4' THEN 'Sífilis' WHEN '5' THEN 'Virus Herpes' WHEN '6' THEN 'Desconocido' END AS 'AGENTE1',
				CASE AGENTE2 WHEN '1' THEN 'Rubeola' WHEN '2' THEN 'Citomegalovirus' WHEN '3' THEN 'Toxoplasma' WHEN '4' THEN 'Sífilis' WHEN '5' THEN 'Virus Herpes' WHEN '6' THEN 'Desconocido' END AS 'AGENTE2',
				CASE AGENTE3 WHEN '1' THEN 'Rubeola' WHEN '2' THEN 'Citomegalovirus' WHEN '3' THEN 'Toxoplasma' WHEN '4' THEN 'Sífilis' WHEN '5' THEN 'Virus Herpes' WHEN '6' THEN 'Desconocido' END AS 'AGENTE3',
				CASE AGENTE4 WHEN '1' THEN 'Rubeola' WHEN '2' THEN 'Citomegalovirus' WHEN '3' THEN 'Toxoplasma' WHEN '4' THEN 'Sífilis' WHEN '5' THEN 'Virus Herpes' WHEN '6' THEN 'Desconocido' END AS 'AGENTE4',
				CASE RESULTADO1 WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'No Procesado' WHEN '4' THEN 'Inadecuado' WHEN '5' THEN 'Dudoso' WHEN '6' THEN 'Valor Registrado' END AS 'RESULTADO1',
				CASE RESULTADO2 WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'No Procesado' WHEN '4' THEN 'Inadecuado' WHEN '5' THEN 'Dudoso' WHEN '6' THEN 'Valor Registrado' END AS 'RESULTADO2',
				CASE RESULTADO3 WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'No Procesado' WHEN '4' THEN 'Inadecuado' WHEN '5' THEN 'Dudoso' WHEN '6' THEN 'Valor Registrado' END AS 'RESULTADO3',
				CASE RESULTADO4 WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'No Procesado' WHEN '4' THEN 'Inadecuado' WHEN '5' THEN 'Dudoso' WHEN '6' THEN 'Valor Registrado' END AS 'RESULTADO4',
				convert(varchar(10),FECHARESUL1,103) As 'FECHARESUL1', convert(varchar(10),FECHARESUL2,103) As 'FECHARESUL2', convert(varchar(10),FECHARESUL3,103) As 'FECHARESUL3',
				convert(varchar(10),FECHARESUL4,103) As 'FECHARESUL4',
				Rtrim(VALREGIS1) As 'VALREGIS1', Rtrim(VALREGIS2) As 'VALREGIS2', Rtrim(VALREGIS3) As 'VALREGIS3', Rtrim(VALREGIS4) As 'VALREGIS4', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 

			From 
				HCFICHA720 
			Where 
				IDFICHANOTIFICACION  = @IdFicha
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera y presenta el detalle completo de la Ficha 720 de SIVIGILA (Sistema Nacional de Vigilancia en Salud Pública) a partir de un identificador y número de ficha. Consolida información clínica y epidemiológica de casos sospechosos o confirmados de infección congénita (como rubéola congénita), incluyendo datos de la madre (nombre, edad, número de embarazos, semanas de embarazo), datos del recién nacido (peso, semanas de gestación, puntaje APGAR, bajo peso al nacer, pequeño para la edad gestacional), antecedentes de viaje (país y municipio visitado), fuente de notificación (pública, privada, laboratorio, comunidad, búsqueda activa), manifestaciones clínicas congénitas (cataratas, glaucoma, retinopatía, sordera, microcefalia, retraso, púrpura, ictericia, daño hepático, bazo, osteopatía, meningitis, entre otras), muestras y pruebas de laboratorio tomadas (PCR, Elisa, patología, aislamiento viral con fechas de toma y recepción), y diagnóstico final del caso. Se usa para imprimir o visualizar la ficha epidemiológica oficial requerida por el INVIMA/INS para la notificación y seguimiento de eventos de interés en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila720';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila720';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para reporte la información de la ficha de notificación SIVIGILA 720 (infección congénita), traduciendo códigos a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila720';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA720 con IDFICHANOTIFICACION igual al identificador recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila720';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas se entregan formateadas como dd/mm/yyyy (estilo 103).; Los campos de texto se devuelven sin espacios finales (RTRIM).; Códigos numéricos no contemplados en los CASE retornan NULL en la columna correspondiente.; El parámetro NumFicha se recibe pero no participa en el filtro; la consulta filtra únicamente por IDFICHANOTIFICACION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila720';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'SIVIGILA; Ficha 720 de notificación; Infección congénita; Rubeola; Citomegalovirus; Toxoplasma; Sífilis; Virus Herpes; APGAR; Bajo peso al nacer; Pequeño para edad gestacional; Cataratas; Glaucoma; Retinopatía; Microcefalia; Sordera; Púrpura; Ictericia; Hepatomegalia; Esplenomegalia; Osteopatía; Meningoencefalitis; Embarazo / semanas de gestación; Muestras clínicas (orina, hisopado, tejido, aspirado, suero); Pruebas diagnósticas (PCR, Patología, Elisa, Aislamiento Viral)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila720';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA720: Cuando IDFICHANOTIFICACION coincide con el parámetro de entrada, retorna una fila con todos los campos de la ficha 720 transformados a marcas ''X'' o etiquetas de dominio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila720';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CLASIFINIC = ''1'' / ''0'' → Marca ''Sospechoso Datos'' o ''Sospechoso Hijo'' respectivamente; si FUENTENOTI entre ''1'' y ''7'' → Marca la fuente de notificación: Pública, Privada, Laboratorio, Comunidad, Búsqueda Activa, Otras o Desconocida; si VIAJES = ''1''/''2''/''3'' → Marca Sí / No / Desconocido sobre antecedente de viajes; si Banderas clínicas (BAJOPESNAC, PEQEDAGEST, CATARATAS, GLAUCOMA, RETINOPA, PERSISTENT, ESTENOSIS, SORDERA, MICROCEF, RETRASO, PURPURA, HIGADO, ICTERICIA, BAZO, OSTEOPA, MENINGO) = ''1''/''2''/''3'' → Marca Sí / No / Desconocido para cada hallazgo clínico; si DIAGNOFIN = ''1''/''2''/''3'' → Clasifica diagnóstico final como Infección Congénita, Otro o Desconocido; si MUESTRA1..4 entre ''1'' y ''5'' → Traduce a tipo de muestra: Orina, Hisopado, Tejido, Aspirado o Suero; si PRUEBA1..4 entre ''1'' y ''4'' → Traduce a tipo de prueba: PCR, Patología, Elisa o Aislamiento Viral; si AGENTE1..4 entre ''1'' y ''6'' → Traduce a agente etiológico: Rubeola, Citomegalovirus, Toxoplasma, Sífilis, Virus Herpes o Desconocido; si RESULTADO1..4 entre ''1'' y ''6'' → Traduce a resultado de laboratorio: Positivo, Negativo, No Procesado, Inadecuado, Dudoso o Valor Registrado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila720';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA720', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila720';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila720';
-- GO
