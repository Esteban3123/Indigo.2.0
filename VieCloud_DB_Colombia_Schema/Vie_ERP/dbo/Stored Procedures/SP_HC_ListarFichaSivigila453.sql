
-- Stored Procedure
-- =============================================
-- Autor:		Jean Carlos Roldan Lozano
-- Fecha Creación: 03-12-2018
-- Descripción:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 453
-- Modificó:    Yezid Garcia Medina
-- Fecha Modificación : 07-02-2022
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila453]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
			Select
				CASE LESIOCAUSAEXT  WHEN '1' THEN 'X' END AS 'AccidenteConsumo',CASE LESIOCAUSAEXT WHEN '0' THEN 'X' END AS 'ProcediEsteticos',
				CASE ASFIXIA WHEN '1' THEN 'X' END AS 'ASFIXIA',CASE ESTRANGULA WHEN '1' THEN 'X' END AS 'ESTRANGULA', CASE HERIDA WHEN '1' THEN 'X' END AS 'HERIDA',
				CASE TRAUMALEV WHEN '1' THEN 'X' END AS 'TRAUMALEV',CASE CHOQUEELEC WHEN '1' THEN 'X' END AS 'CHOQUEELEC', CASE FRACTURA WHEN '1' THEN 'X' END AS 'FRACTURA',
				CASE POLITRAUMA WHEN '1' THEN 'X' END AS 'POLITRAUMA',CASE AMPUTACION WHEN '1' THEN 'X' END AS 'AMPUTACION', CASE QUEMADURAS WHEN '1' THEN 'X' END AS 'QUEMADURAS',
				CASE INTOXICACION WHEN '1' THEN 'X' END AS 'INTOXICACION',CASE INFECCION WHEN '1' THEN 'X' END AS 'INFECCION', CASE SEPSIS WHEN '1' THEN 'X' END AS 'SEPSIS',
				CASE PERFORACION WHEN '1' THEN 'X' END AS 'PERFORACION',CASE HEMORRAGIA WHEN '1' THEN 'X' END AS 'HEMORRAGIA', CASE NECROSIS WHEN '1' THEN 'X' END AS 'NECROSIS',
				CASE EMBOLIA WHEN '1' THEN 'X' END AS 'EMBOLIA',CASE DEPRESION WHEN '1' THEN 'X' END AS 'DEPRESION', CASE CRANEO WHEN '1' THEN 'X' END AS 'CRANEO',
				CASE MANO WHEN '1' THEN 'X' END AS 'MANO',CASE MUSLOS WHEN '1' THEN 'X' END AS 'MUSLOS', CASE CARA WHEN '1' THEN 'X' END AS 'CARA',
				CASE DEDOSMANO WHEN '1' THEN 'X' END AS 'DEDOSMANO',CASE PIERNAS WHEN '1' THEN 'X' END AS 'PIERNAS', CASE OJOS WHEN '1' THEN 'X' END AS 'OJOS',
				CASE TORAXANTE WHEN '1' THEN 'X' END AS 'TORAXANTE',
				CASE PIES WHEN '1' THEN 'X' END AS 'PIES',CASE NARIZ WHEN '1' THEN 'X' END AS 'NARIZ', CASE TORAXPOST WHEN '1' THEN 'X' END AS 'TORAXPOST',
				CASE DEDOSPIES WHEN '1' THEN 'X' END AS 'DEDOSPIES',CASE OREJAS WHEN '1' THEN 'X' END AS 'OREJAS', CASE MAMAS WHEN '1' THEN 'X' END AS 'MAMAS',
				CASE ORGANINTER WHEN '1' THEN 'X' END AS 'ORGANINTER',CASE BOCADIENTES WHEN '1' THEN 'X' END AS 'BOCADIENTES', CASE ABDOMEN WHEN '1' THEN 'X' END AS 'ABDOMEN',
				CASE PIEL WHEN '1' THEN 'X' END AS 'PIEL',CASE CUELLO WHEN '1' THEN 'X' END AS 'CUELLO', CASE PELVISPERI WHEN '1' THEN 'X' END AS 'PELVISPERI',
				CASE BRAZO WHEN '1' THEN 'X' END AS 'BRAZO',CASE GENITALES WHEN '1' THEN 'X' END AS 'GENITALES', CASE ANTEBRAZO WHEN '1' THEN 'X' END AS 'ANTEBRAZO',
				CASE GLUTEOS WHEN '1' THEN 'X' END AS 'GLUTEOS',CASE MAQUINA WHEN '1' THEN 'X' END AS 'MAQUINA', CASE MEDIOSTRANS WHEN '1' THEN 'X' END AS 'MEDIOSTRANS',
				CASE PRODUCQUIM WHEN '1' THEN 'X' END AS 'PRODUCQUIM',CASE JUGUETES WHEN '1' THEN 'X' END AS 'JUGUETES', CASE EQUIPOSCONS WHEN '1' THEN 'X' END AS 'EQUIPOSCONS',
				CASE VESTIMENTA WHEN '1' THEN 'X' END AS 'VESTIMENTA',CASE MATERIALESC WHEN '1' THEN 'X' END AS 'MATERIALESC', CASE MUEBLES WHEN '1' THEN 'X' END AS 'MUEBLES',
				CASE ARTICULOSNIN WHEN '1' THEN 'X' END AS 'ARTICULOSNIN',
				CASE ARTICULOSDEP WHEN '1' THEN 'X' END AS 'ARTICULOSDEP',CASE EQUIPOSCOM WHEN '1' THEN 'X' END AS 'EQUIPOSCOM', CASE ARTICULOSBELLE WHEN '1' THEN 'X' END AS 'ARTICULOSBELLE',
				CASE MEDICAMENTOS WHEN '1' THEN 'X' END AS 'MEDICAMENTOS',CASE APARATOLOGIA WHEN '1' THEN 'X' END AS 'APARATOLOGIA', CASE EQUIPOSBIOMED WHEN '1' THEN 'X' END AS 'EQUIPOSBIOMED',
				CASE HOGAR WHEN '1' THEN 'X' END AS 'HOGAR',CASE ESTABLEDUCA WHEN '1' THEN 'X' END AS 'ESTABLEDUCA', CASE CALLE WHEN '1' THEN 'X' END AS 'CALLE',
				CASE LUGARECREA WHEN '1' THEN 'X' END AS 'LUGARECREA',
				CASE INDUSTRIA WHEN '1' THEN 'X' END AS 'INDUSTRIA',CASE ESTABLEPUBLI WHEN '1' THEN 'X' END AS 'ESTABLEPUBLI', CASE CENTROESTET WHEN '1' THEN 'X' END AS 'CENTROESTET',
				CASE SPA WHEN '1' THEN 'X' END AS 'SPA',CASE IPS WHEN '1' THEN 'X' END AS 'IPS', 
				CASE NUMPROCE WHEN '1' THEN 'X' END AS '1',CASE NUMPROCE WHEN '2' THEN 'X' END AS '2', CASE NUMPROCE WHEN '3' THEN 'X' END AS '3',
				CASE NUMPROCE WHEN '4' THEN 'X' END AS 'Masde3',
				CASE TIPOPROFES WHEN '1' THEN 'X' END AS 'ProfeSalud',CASE TIPOPROFES WHEN '2' THEN 'X' END AS 'CirujanoPlast', CASE TIPOPROFES WHEN '3' THEN 'X' END AS 'MedicoEsteti',
				CASE TIPOPROFES WHEN '4' THEN 'X' END AS 'MedicoEspecia',CASE TIPOPROFES WHEN '5' THEN 'X' END AS 'Esteticista', CASE TIPOPROFES WHEN '6' THEN 'X' END AS 'Cosmetologo',
				CASE HOSPITAL  WHEN '1' THEN 'X' END AS 'HOSPITAL si',CASE HOSPITAL WHEN '0' THEN 'X' END AS 'HOSPITAL no',
				CASE UCI  WHEN '1' THEN 'X' END AS 'UCI si',CASE UCI WHEN '0' THEN 'X' END AS 'UCI no', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON',	
				CASE TRY_CAST( JSON_VALUE(JSON,'$.SEAN_SSSN') AS BIT ) WHEN 1 THEN 'X' END AS 'SEAN_SSSN', 
				RTRIM( JSON_VALUE(JSON,'$.NOMBRE_ELEMENTO_LESION') ) AS 'NOMBRE_ELEMENTO_LESION', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.SUSTANCIA_NICOTINA') AS BIT ) WHEN 1 THEN 'X' END AS 'SUSTANCIA_NICOTINA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.SUSTANCIA_SABORIZANTE') AS BIT ) WHEN 1 THEN 'X' END AS 'SUSTANCIA_SABORIZANTE', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.SUSTANCIA_MARIHUANA') AS BIT ) WHEN 1 THEN 'X' END AS 'SUSTANCIA_MARIHUANA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.SUSTANCIA_OTRA') AS BIT ) WHEN 1 THEN 'X' END AS 'SUSTANCIA_OTRA', 
				RTRIM( JSON_VALUE(JSON,'$.SUSTANCIA_CUAL') ) AS 'SUSTANCIA_CUAL', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.FRECUENCIA') AS int ) WHEN 1 THEN 'X' END AS 'FRECUENCIA_DIARIO', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.FRECUENCIA') AS int ) WHEN 2 THEN 'X' END AS 'FRECUENCIA_DOS_TRES_VECES', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.FRECUENCIA') AS int ) WHEN 3 THEN 'X' END AS 'FRECUENCIA_UNA_VEZ', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.FRECUENCIA') AS int ) WHEN 4 THEN 'X' END AS 'FRECUENCIA_MENOS_UNA_VEZ', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.MC_TOS') AS BIT ) WHEN 1 THEN 'X' END AS 'MC_TOS', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.MC_DISNEA') AS BIT ) WHEN 1 THEN 'X' END AS 'MC_DISNEA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.MC_DIFICULTAD_RESPIRATORIA') AS BIT ) WHEN 1 THEN 'X' END AS 'MC_DIFICULTAD_RESPIRATORIA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.MC_DOLOR_TORACICO') AS BIT ) WHEN 1 THEN 'X' END AS 'MC_DOLOR_TORACICO', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.MC_NAUSEAS') AS BIT ) WHEN 1 THEN 'X' END AS 'MC_NAUSEAS', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.MC_VOMITO') AS BIT ) WHEN 1 THEN 'X' END AS 'MC_VOMITO', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.MC_DIARREA') AS BIT ) WHEN 1 THEN 'X' END AS 'MC_DIARREA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.MC_DOLOR_ABDOMINAL') AS BIT ) WHEN 1 THEN 'X' END AS 'MC_DOLOR_ABDOMINAL', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.MC_OTRA') AS BIT ) WHEN 1 THEN 'X' END AS 'MC_OTRA', 
				RTRIM( JSON_VALUE(JSON,'$.MC_CUAL') ) AS 'MC_CUAL', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.DX_SBO') AS BIT ) WHEN 1 THEN 'X' END AS 'DX_SBO', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.DX_ECA') AS BIT ) WHEN 1 THEN 'X' END AS 'DX_ECA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.DX_INTOXICACION') AS BIT ) WHEN 1 THEN 'X' END AS 'DX_INTOXICACION', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.DX_QUEMADURA') AS BIT ) WHEN 1 THEN 'X' END AS 'DX_QUEMADURA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.DX_ALERGIA') AS BIT ) WHEN 1 THEN 'X' END AS 'DX_ALERGIA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRA_CIGARRILLO') AS BIT ) WHEN 1 THEN 'X' END AS 'OTRA_CIGARRILLO', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRA_MARIHUANA') AS BIT ) WHEN 1 THEN 'X' END AS 'OTRA_MARIHUANA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRA_COCAINA') AS BIT ) WHEN 1 THEN 'X' END AS 'OTRA_COCAINA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRA_BAZUCO') AS BIT ) WHEN 1 THEN 'X' END AS 'OTRA_BAZUCO', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.OTRA_HEROINA') AS BIT ) WHEN 1 THEN 'X' END AS 'OTRA_HEROINA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.ANTECEDENTE_ASMA') AS BIT ) WHEN 1 THEN 'X' END AS 'ANTECEDENTE_ASMA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.ANTECEDENTE_EPOC') AS BIT ) WHEN 1 THEN 'X' END AS 'ANTECEDENTE_EPOC', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.ANTECEDENTE_ALERGIA_RESPIRATORIA') AS BIT ) WHEN 1 THEN 'X' END AS 'ANTECEDENTE_ALERGIA_RESPIRATORIA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.ANTECEDENTE_FIBROSIS_QUISTICA') AS BIT ) WHEN 1 THEN 'X' END AS 'ANTECEDENTE_FIBROSIS_QUISTICA', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.ANTECEDENTE_ENFERMEDAD_CORONARIA') AS BIT ) WHEN 1 THEN 'X' END AS 'ANTECEDENTE_ENFERMEDAD_CORONARIA' ,
				CASE TRY_CAST( JSON_VALUE(JSON,'$.BIOPOLIMERO') AS BIT ) WHEN 1 THEN 'X' END AS 'BIOPOLIMERO' ,
				CASE TRY_CAST( JSON_VALUE(JSON,'$.BROTE') AS BIT ) WHEN 1 THEN 'X' END AS 'BROTE si',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.BROTE') AS BIT ) WHEN 0 THEN 'X' END AS 'BROTE no',
				RTRIM( JSON_VALUE(JSON,'$.NOMBRE_ESTABLECIMIENTO') ) AS 'NOMBRE_ESTABLECIMIENTO'
			From 
				HCFICHA453 				
			Where 
				IDFICHANOTIFICACION  = @IdFicha
				AND ISJSON(JSON) > 0
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una Ficha 453 de SIVIGILA (vigilancia epidemiológica) identificada por su ID o número de ficha. Consolida y presenta en formato de marcas (X) todos los campos de la ficha: el tipo de lesión o causa externa (accidente por consumo, procedimiento estético), los tipos de lesión presentes (asfixia, estrangulamiento, herida, fractura, quemaduras, intoxicación, sepsis, hemorragia, necrosis, embolia, entre otros), las partes del cuerpo afectadas (cráneo, cara, tórax, abdomen, genitales, extremidades, etc.), los agentes o productos causantes (maquinaria, productos químicos, medicamentos, equipos biomédicos, juguetes, vestimenta, etc.), el lugar donde ocurrió el evento (hogar, calle, institución educativa, IPS, centro estético, SPA, etc.), el tipo de profesional que realizó el procedimiento y si el paciente requirió hospitalización o UCI. También extrae datos extendidos almacenados en formato JSON dentro del registro, como el nombre del elemento causante de la lesión, sustancias involucradas (nicotina, saborizantes, marihuana u otras) y la frecuencia de consumo. Este procedimiento se usa para imprimir o visualizar la ficha 453 de notificación obligatoria al SIVIGILA desde la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila453';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila453';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información detallada de una ficha epidemiológica Sivigila 453 (lesiones por procedimientos estéticos / consumo de cigarrillo electrónico-vapeadores) marcando con ''X'' los atributos clínicos, anatómicos, de exposición y antecedentes, incluyendo datos extraídos de un JSON anexo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila453';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La ficha solicitada debe existir en HCFICHA453 con el identificador suministrado; El campo JSON del registro debe contener un JSON válido (ISJSON(JSON) > 0); de lo contrario la fila no se retorna', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila453';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven fichas cuyo campo JSON sea JSON válido; Los campos booleanos del JSON se interpretan vía TRY_CAST a BIT, tolerando valores no convertibles (devuelve NULL); Las columnas de salida usan ''X'' como marcador booleano para indicadores afirmativos; El parámetro @NumFicha se recibe pero no se utiliza como filtro (el filtro real es @IdFicha sobre IDFICHANOTIFICACION)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila453';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila 453; Lesiones por procedimientos estéticos; Consumo de cigarrillo electrónico/vapeadores; Sustancias (nicotina, marihuana, cocaína, bazuco, heroína, saborizantes); Manifestaciones clínicas respiratorias y gastrointestinales; Diagnósticos (SBO, ECA, intoxicación, quemadura, alergia); Antecedentes (asma, EPOC, alergia respiratoria, fibrosis quística, enfermedad coronaria); Zonas anatómicas afectadas; Tipo de profesional que realizó el procedimiento; Hospitalización y UCI; Biopolímeros; Brote epidemiológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila453';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA453: Cuando IDFICHANOTIFICACION = parámetro y ISJSON(JSON) > 0, retorna una fila con los indicadores transformados a ''X'' y los valores extraídos del JSON', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila453';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LESIOCAUSAEXT = ''1'' → Marca ''AccidenteConsumo'' con ''X'' else Si LESIOCAUSAEXT=''0'' marca ''ProcediEsteticos'' con ''X''; si NUMPROCE in (''1'',''2'',''3'',''4'') → Marca columna correspondiente al número de procedimientos (1, 2, 3 o ''Masde3'' para 4); si TIPOPROFES in (''1''..''6'') → Marca el tipo de profesional: ProfeSalud, CirujanoPlast, MedicoEsteti, MedicoEspecia, Esteticista o Cosmetologo; si HOSPITAL = ''1'' / ''0'' → Marca ''HOSPITAL si'' u ''HOSPITAL no'' según el valor; si UCI = ''1'' / ''0'' → Marca ''UCI si'' o ''UCI no''; si JSON_VALUE(JSON,''$.FRECUENCIA'') in (1..4) → Marca FRECUENCIA_DIARIO, FRECUENCIA_DOS_TRES_VECES, FRECUENCIA_UNA_VEZ o FRECUENCIA_MENOS_UNA_VEZ; si TRY_CAST(JSON_VALUE(JSON,''$.BROTE'') AS BIT) = 1 / 0 → Marca ''BROTE si'' o ''BROTE no''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila453';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA453', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila453';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila453';
-- GO
