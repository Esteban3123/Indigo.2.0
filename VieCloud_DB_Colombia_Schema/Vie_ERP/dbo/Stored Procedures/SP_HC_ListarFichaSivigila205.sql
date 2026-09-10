-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,24-10-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila205]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON; 

	Select 
	SEMANASEMBARAZO,CASE CLASIFCASO  WHEN '1' THEN 'X' END AS 'Agudo',CASE CLASIFCASO   WHEN '0' THEN 'X' END AS 'Cronico',	
		CASE REACTIVACION WHEN '1' THEN 'X' END AS 'Reactivación Si',CASE REACTIVACION WHEN '0' THEN 'X' END AS 'Reactivación No',
		CASE FIEBRE WHEN '1' THEN 'X' END AS 'FIEBRE',CASE DISNEA WHEN '1' THEN 'X' END AS 'DISNEA', CASE EDEMAFACIAL WHEN '1' THEN 'X' END AS 'EDEMAFACIAL', CASE EDEMAMIEMINF WHEN '1' THEN 'X' END AS 'EDEMAMIEMINF',
		CASE DERRAPERICAR WHEN '1' THEN 'X' END AS 'DERRAPERICAR', CASE HEPATOESPLENOMEGALIA WHEN '1' THEN 'X' END AS 'HEPATOESPLENOMEGALIA', CASE ADENOPATIAS WHEN '1' THEN 'X' END AS 'ADENOPATIAS',
		CASE ROMANA WHEN '1' THEN 'X' END AS 'ROMANA',CASE CHAGOMA WHEN '1' THEN 'X' END AS 'CHAGOMA',CASE FALLACARDIA WHEN '1' THEN 'X' END AS 'FALLACARDIA',CASE DISFAGIA WHEN '1' THEN 'X' END AS 'DISFAGIA',CASE DOLORTORAX WHEN '1' THEN 'X' END AS 'DOLORTORAX',
		CASE BRADICARDIA WHEN '1' THEN 'X' END AS 'BRADICARDIA',CASE ARRITMIACARDIA WHEN '1' THEN 'X' END AS 'ARRITMIACARDIA',
		CASE MICROMETODO  WHEN '1' THEN 'X' END AS 'Positivo',CASE MICROMETODO WHEN '2' THEN 'X' END AS 'Negativo', CASE MICROMETODO WHEN '3' THEN 'X' END AS 'No se realizó',
		CASE GOTAGRUESA  WHEN '1' THEN 'X' END AS 'Positivo',CASE GOTAGRUESA WHEN '2' THEN 'X' END AS 'Negativo', CASE GOTAGRUESA WHEN '3' THEN 'X' END AS 'No se realizó',
		CASE MICROHEMATO  WHEN '1' THEN 'X' END AS 'Positivo',CASE MICROHEMATO WHEN '2' THEN 'X' END AS 'Negativo', CASE MICROHEMATO WHEN '3' THEN 'X' END AS 'No se realizó',
		CASE STROUT  WHEN '1' THEN 'X' END AS 'Positivo',CASE STROUT WHEN '2' THEN 'X' END AS 'Negativo', CASE STROUT WHEN '3' THEN 'X' END AS 'No se realizó',
		CASE ELISA  WHEN '1' THEN 'X' END AS 'Positivo',CASE ELISA WHEN '2' THEN 'X' END AS 'Negativo', CASE ELISA WHEN '3' THEN 'X' END AS 'No se realizó',
		CASE ELISACLIA  WHEN '1' THEN 'X' END AS 'Positivo',CASE ELISACLIA WHEN '2' THEN 'X' END AS 'Negativo', CASE ELISACLIA WHEN '3' THEN 'X' END AS 'No se realizó',
		CASE IFI  WHEN '1' THEN 'X' END AS 'Positivo',CASE IFI WHEN '2' THEN 'X' END AS 'Negativo', CASE IFI WHEN '3' THEN 'X' END AS 'No se realizó',
		CASE INMUNOBIOT  WHEN '1' THEN 'X' END AS 'Positivo',CASE INMUNOBIOT WHEN '2' THEN 'X' END AS 'Negativo', CASE INMUNOBIOT WHEN '3' THEN 'X' END AS 'No se realizó',
		CASE VIATRANSMI  WHEN '1' THEN 'X' END AS 'Vectorial',CASE VIATRANSMI WHEN '2' THEN 'X' END AS 'Transfuncional', CASE VIATRANSMI WHEN '3' THEN 'X' END AS 'Congénita', CASE VIATRANSMI  WHEN '4' THEN 'X' END AS 'Vía oral',CASE VIATRANSMI WHEN '5' THEN 'X' END AS 'Transplante', CASE VIATRANSMI WHEN '6' THEN 'X' END AS 'Accidente de laboratorio', 
		
		JSON_VALUE(ISNULL(JSON, '{}'), '$.PDR2') AS PDR2,
		CASE JSON_VALUE(ISNULL(JSON, '{}'), '$.PDR2') WHEN '1' THEN 'X' END AS PDR2_POS,
		CASE JSON_VALUE(ISNULL(JSON, '{}'), '$.PDR2') WHEN '2' THEN 'X' END AS PDR2_NEG,
		CASE JSON_VALUE(ISNULL(JSON, '{}'), '$.PDR2') WHEN '3' THEN 'X' END AS PDR2_NA,

		JSON_VALUE(ISNULL(JSON, '{}'), '$.NombreMadre') AS NOMBREMADRE,
	
		CASE JSON_VALUE(JSON, '$.TipoIdMadre')
			WHEN '1' THEN 'TI - Tarjeta de Identidad'
			WHEN '2' THEN 'CC - Cédula de Ciudadanía'
			WHEN '3' THEN 'CE - Cédula de Extranjería'
			WHEN '4' THEN 'PA - Pasaporte'
			WHEN '5' THEN 'MS - Menor Sin Identificación'
			WHEN '6' THEN 'AS - Adulto Sin Identificación'
			WHEN '7' THEN 'PE - Permiso Especial de Permanencia'
			WHEN '8' THEN 'CD - Carnet Diplomático'
			WHEN '9' THEN 'SC - Salvoconducto'
			WHEN '10' THEN 'NU - Número único de identificación personal'
			WHEN '11' THEN 'DE - Documento Extranjero'
			WHEN '12' THEN 'PT - Permiso Temporal de Permanencia'
		END AS [Tipo Identificación Madre],
	
		JSON_VALUE(JSON, '$.NumIdMadre') AS [Numero Id Madre],
		
		VERSION AS 'VERSION', 
		JSON AS 'JSON' 

	From 
		HCFICHA205 

	Where 
		IDFICHANOTIFICACION  = @IdFicha 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera la ficha epidemiológica SIVIGILA número 205, correspondiente a la notificación de enfermedad de Chagas. Dado un identificador de ficha, consulta la tabla HCFICHA205 y retorna la información clínica del paciente: semanas de embarazo, clasificación del caso (agudo o crónico), reactivación, síntomas presentes (fiebre, disnea, edema facial, edema de miembros inferiores, derrame pericárdico, hepatoesplenomegalia, adenopatías, signo de Romaña, chagoma, falla cardíaca, disfagia, dolor torácico, bradicardia, arritmia), resultados de pruebas diagnósticas (micrométodo, gota gruesa, microhematocrito, Strout, ELISA, ELISA-CLIA, IFI, inmunoblot) y vía de transmisión (vectorial, transfusional, congénita, oral, trasplante, accidente de laboratorio). Se utiliza para imprimir o consultar la ficha oficial de reporte al sistema de vigilancia epidemiológica nacional SIVIGILA en casos de Chagas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila205';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila205';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información de una ficha de notificación SIVIGILA 205 (Chagas) traduciendo códigos numéricos de síntomas, pruebas diagnósticas, vía de transmisión y datos de la madre a etiquetas legibles para reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila205';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA205 con el identificador de ficha de notificación suministrado; El campo JSON debe contener una estructura válida con claves PDR2, NombreMadre, TipoIdMadre y NumIdMadre cuando se requiera información de la madre', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila205';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores numéricos de síntomas y pruebas se presentan como ''X'' cuando aplican, dejando NULL cuando no coinciden con los códigos esperados; Los campos JSON ausentes se tratan como objeto vacío ''{}'' para evitar errores en JSON_VALUE; El resultado se filtra siempre por el identificador de ficha de notificación recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila205';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; Enfermedad de Chagas (evento 205); Clasificación de caso agudo/crónico; Reactivación; Síntomas clínicos (fiebre, disnea, edema, hepatoesplenomegalia, signo de Romaña, chagoma, etc.); Pruebas diagnósticas (micrométodo, gota gruesa, microhematocrito, Strout, ELISA, ELISA CLIA, IFI, inmunoblot); Vía de transmisión (vectorial, transfusional, congénita, oral, trasplante, accidente de laboratorio); Semanas de embarazo; Datos de identificación de la madre (tipo y número de documento)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila205';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA205: Cuando IDFICHANOTIFICACION coincide con el identificador recibido, se retorna una fila con los datos clínicos, resultados de pruebas, vía de transmisión, datos de la madre (extraídos del JSON) y la versión/JSON crudo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila205';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CLASIFCASO = ''1'' → Marca caso como Agudo (X) else Si CLASIFCASO = ''0'' marca como Crónico (X); si REACTIVACION = ''1'' → Marca Reactivación Sí else Si ''0'' marca Reactivación No; si Pruebas (MICROMETODO, GOTAGRUESA, MICROHEMATO, STROUT, ELISA, ELISACLIA, IFI, INMUNOBIOT) con valor ''1'',''2'',''3'' → Se traduce a Positivo, Negativo o No se realizó respectivamente; si VIATRANSMI con valores ''1'' a ''6'' → Se traduce a vía de transmisión: Vectorial, Transfuncional, Congénita, Vía oral, Transplante o Accidente de laboratorio; si JSON.PDR2 con valor ''1'',''2'',''3'' → Se marca PDR2_POS, PDR2_NEG o PDR2_NA respectivamente; si JSON.TipoIdMadre con valores ''1'' a ''12'' → Se traduce al código y descripción del tipo de documento de la madre (TI, CC, CE, PA, MS, AS, PE, CD, SC, NU, DE, PT)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila205';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA205', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila205';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila205';
-- GO
