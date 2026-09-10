-- Stored Procedure
-- =============================================
-- Autor:		Jean Carlos Roldan Lozano
-- Fecha Creación: 03-12-2018
-- Descripción:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 357
-- Modificó:    Yezid Garcia Medina
-- Fecha Modificación : 27-01-2022
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila357]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
	
	SET NOCOUNT ON;
  
			Select
				
				CASE PACIREMITI  WHEN '1' THEN 'X' END AS 'PACIREMITI si',CASE PACIREMITI WHEN '0' THEN 'X' END AS 'PACIREMITI no',
				CASE CASOIAD  WHEN '1' THEN 'X' END AS 'CASOIAD si',CASE CASOIAD WHEN '0' THEN 'X' END AS 'CASOIAD no',
				CASE IADPOLIMICRO  WHEN '1' THEN 'X' END AS 'IADPOLIMICRO si',CASE IADPOLIMICRO WHEN '0' THEN 'X' END AS 'IADPOLIMICRO no',
				CASE VENTIMECANI  WHEN '1' THEN 'X' END AS 'VENTIMECANI si',CASE VENTIMECANI WHEN '0' THEN 'X' END AS 'VENTIMECANI no',
				CASE CATETECENTR  WHEN '1' THEN 'X' END AS 'CATETECENTR si',CASE CATETECENTR WHEN '0' THEN 'X' END AS 'CATETECENTR no',
				CASE CATETEURINA  WHEN '1' THEN 'X' END AS 'CATETEURINA si',CASE CATETEURINA WHEN '0' THEN 'X' END AS 'CATETEURINA no',
				convert(varchar(10),FECHAINGRES,103) As 'FECHAINGRES', convert(varchar(10),FECHADIAGN,103) As 'FECHADIAGN', convert(varchar(10),FECHAINSER1,103) As 'FECHAINSER1',
				convert(varchar(10),FECHAINSER2,103) As 'FECHAINSER2', convert(varchar(10),FECHAINSER3,103) As 'FECHAINSER3', convert(varchar(10),FECHARETIRO1,103) As 'FECHARETIRO1',
				convert(varchar(10),FECHARETIRO2,103) As 'FECHARETIRO2', convert(varchar(10),FECHARETIRO3,103) As 'FECHARETIRO3', convert(varchar(10),FECHATOMAMU1,103) As 'FECHATOMAMU1',
				convert(varchar(10),FECHATOMAMU2,103) As 'FECHATOMAMU2', convert(varchar(10),FECHATOMAMU3,103) As 'FECHATOMAMU3',
				Rtrim(NOMINSTIT) As 'NOMINSTIT', Rtrim(PARAUCIN) As 'PARAUCIN', Rtrim(OTRO2) As 'OTRO2',Rtrim(MICROORGA1) As 'MICROORGA1', Rtrim(MICROORGA2) As 'MICROORGA2', Rtrim(MICROORGA3) As 'MICROORGA3',
				CASE TIPOUCI WHEN '1' THEN 'X' END AS 'uci-a',CASE TIPOUCI WHEN '2' THEN 'X' END AS 'uci-p', CASE TIPOUCI WHEN '3' THEN 'X' END AS 'uci-n',
				CASE TIPOIAD WHEN '1' THEN 'X' END AS 'NAV',CASE TIPOIAD WHEN '2' THEN 'X' END AS 'ISTU-AC', CASE TIPOIAD WHEN '3' THEN 'X' END AS 'ITS-AC',
				CASE CRITCLASIFNAV WHEN '1' THEN 'X' END AS 'NEU1',CASE CRITCLASIFNAV WHEN '2' THEN 'X' END AS 'NEU2', CASE CRITCLASIFNAV WHEN '3' THEN 'X' END AS 'NEU3',
				CASE CRITCLASIFITSAC WHEN '1' THEN 'X' END AS 'Criterio1',CASE CRITCLASIFITSAC WHEN '2' THEN 'X' END AS 'Criterio2', CASE CRITCLASIFITSAC WHEN '3' THEN 'X' END AS 'Criterio3',
				CASE CRITCLASIFISTUAC WHEN '1' THEN 'X' END AS 'Criterio1a',CASE CRITCLASIFISTUAC WHEN '2' THEN 'X' END AS 'Criterio2a', CASE CRITCLASIFISTUAC WHEN '3' THEN 'X' END AS 'criterio3a',
				CASE CRITCLASIFISTUAC WHEN '4' THEN 'X' END AS 'criterio4a',
				CASE CANCER WHEN '1' THEN 'X' END AS 'CANCER',
				CASE DESNUTRICION WHEN '1' THEN 'X' END AS 'DESNUTRICION',
				CASE DIABETES WHEN '1' THEN 'X' END AS 'DIABETES',
				CASE ENFERENAL WHEN '1' THEN 'X' END AS 'ENFERENAL',
				CASE EPOC WHEN '1' THEN 'X' END AS 'EPOC',
				CASE INMUNOSUPRE WHEN '1' THEN 'X' END AS 'INMUNOSUPRE', 
				CASE VIHSIDA WHEN '1' THEN 'X' END AS 'VIHSIDA',
				CASE INFECCIPREV WHEN '1' THEN 'X' END AS 'INFECCIPREV',
				CASE TRAUMATIS WHEN '1' THEN 'X' END AS 'TRAUMATIS', 
				CASE OBESIDAD WHEN '1' THEN 'X' END AS 'OBESIDAD',
				CASE OTRO WHEN '1' THEN 'X' END AS 'OTRO',
				CASE CODMUESTRA1 WHEN '1' THEN '1' WHEN '2' THEN '2' WHEN '3' THEN '4' WHEN '4' THEN '10' WHEN '5' THEN '11' WHEN '6' THEN '31' WHEN '7' THEN '32' END AS 'CODMUESTRA1',
				CASE CODMUESTRA2 WHEN '1' THEN '1' WHEN '2' THEN '2' WHEN '3' THEN '4' WHEN '4' THEN '10' WHEN '5' THEN '11' WHEN '6' THEN '31' WHEN '7' THEN '32' END AS 'CODMUESTRA2',
				CASE CODMUESTRA3 WHEN '1' THEN '1' WHEN '2' THEN '2' WHEN '3' THEN '4' WHEN '4' THEN '10' WHEN '5' THEN '11' WHEN '6' THEN '31' WHEN '7' THEN '32' END AS 'CODMUESTRA3',
				CASE CODPRUEBA1 WHEN '1' THEN '3' WHEN '2' THEN '55' WHEN '3' THEN '73' WHEN '4' THEN '92' WHEN '5' THEN 'C7' WHEN '6' THEN 'G0' WHEN '7' THEN 'G1' 
				WHEN '8' THEN 'G2' WHEN '9' THEN 'G3' WHEN '10' THEN 'G4' WHEN '11' THEN 'G5' WHEN '12' THEN 'G6' WHEN '13' THEN 'G7' WHEN '14' THEN 'G8' 
				WHEN '15' THEN 'G9' WHEN '16' THEN 'H0' WHEN '17' THEN 'H1' WHEN '18' THEN 'H2' END AS 'CODPRUEBA1',
				CASE CODPRUEBA2 WHEN '1' THEN '3' WHEN '2' THEN '55' WHEN '3' THEN '73' WHEN '4' THEN '92' WHEN '5' THEN 'C7' WHEN '6' THEN 'G0' WHEN '7' THEN 'G1' 
				WHEN '8' THEN 'G2' WHEN '9' THEN 'G3' WHEN '10' THEN 'G4' WHEN '11' THEN 'G5' WHEN '12' THEN 'G6' WHEN '13' THEN 'G7' WHEN '14' THEN 'G8' 
				WHEN '15' THEN 'G9' WHEN '16' THEN 'H0' WHEN '17' THEN 'H1' WHEN '18' THEN 'H2' END AS 'CODPRUEBA2',
				CASE CODPRUEBA3 WHEN '1' THEN '3' WHEN '2' THEN '55' WHEN '3' THEN '73' WHEN '4' THEN '92' WHEN '5' THEN 'C7' WHEN '6' THEN 'G0' WHEN '7' THEN 'G1' 
				WHEN '8' THEN 'G2' WHEN '9' THEN 'G3' WHEN '10' THEN 'G4' WHEN '11' THEN 'G5' WHEN '12' THEN 'G6' WHEN '13' THEN 'G7' WHEN '14' THEN 'G8' 
				WHEN '15' THEN 'G9' WHEN '16' THEN 'H0' WHEN '17' THEN 'H1' WHEN '18' THEN 'H2' END AS 'CODPRUEBA3', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON', 
				CASE RTRIM( JSON_VALUE(JSON,'$.DIPOSITIVO_UCI') ) WHEN 'TRUE' THEN 'X' END AS 'DIPOSITIVO_UCI', 
				CASE RTRIM( JSON_VALUE(JSON,'$.EVENTO_48H') ) WHEN 'TRUE' THEN 'X' END AS 'EVENTO_48H' 				
			From 
				HCFICHA357 
			Where 
				IDFICHANOTIFICACION  = @IdFicha 					
					   
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve el contenido completo de la Ficha 357 de SIVIGILA para vigilancia epidemiológica de Infecciones Asociadas a la Atención en Salud (IAAS) en Unidades de Cuidados Intensivos (UCI). Recibe el identificador de la ficha y retorna todos los campos clínicos y epidemiológicos registrados: uso de dispositivos invasivos (ventilación mecánica, catéter central, catéter urinario), factores de riesgo del paciente (cáncer, diabetes, desnutrición, EPOC, VIH/SIDA, inmunosupresión, obesidad, entre otros), tipo de UCI (adultos, pediátrica, neonatal), tipo de infección (NAV, ISTU-AC, ITS-AC), criterios de clasificación, microorganismos identificados, códigos de muestra y prueba de laboratorio, fechas clave (ingreso, diagnóstico, inserción y retiro de dispositivos, toma de muestras) y datos adicionales almacenados en formato JSON. Se usa para imprimir o visualizar la ficha oficial de notificación obligatoria al SIVIGILA que deben reportar las instituciones de salud ante eventos de infección intrahospitalaria en UCI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila357';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila357';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de la ficha de notificación Sivigila 357 (infecciones asociadas a dispositivos en UCI) para presentación/impresión, traduciendo códigos internos a códigos oficiales y banderas booleanas a marcas ''X''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila357';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA357 con IDFICHANOTIFICACION igual al identificador recibido; El campo JSON debe ser un JSON válido para extraer las propiedades DIPOSITIVO_UCI y EVENTO_48H', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila357';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas se devuelven siempre en formato dd/mm/yyyy (estilo 103); Los campos de texto se devuelven sin espacios a la derecha (RTRIM); Las banderas booleanas (1/0) se traducen a ''X'' o NULL, nunca a otro valor; Los códigos internos de muestra y prueba se mapean a los códigos oficiales del Sivigila; Solo se retorna la ficha cuyo IDFICHANOTIFICACION coincide con el parámetro; @NumFicha no se utiliza en el filtro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila357';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha Sivigila 357; Infecciones asociadas a dispositivos (IAD); UCI adultos/pediátrica/neonatal; Neumonía asociada a ventilador (NAV); Infección sintomática del tracto urinario asociada a catéter (ISTU-AC); Infección del torrente sanguíneo asociada a catéter (ITS-AC); Ventilación mecánica; Catéter central; Catéter urinario; Microorganismos / polimicrobiano; Comorbilidades (cáncer, desnutrición, diabetes, enfermedad renal, EPOC, inmunosupresión, VIH/SIDA, infección previa, trauma, obesidad); Códigos de muestra y prueba de laboratorio; Evento dentro de las 48 horas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila357';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA357: Cuando IDFICHANOTIFICACION = @IdFicha → devuelve una fila con los datos transformados de la ficha 357', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila357';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Campos booleanos (PACIREMITI, CASOIAD, IADPOLIMICRO, VENTIMECANI, CATETECENTR, CATETEURINA) = ''1'' o ''0'' → Marca con ''X'' la columna ''si'' o ''no'' correspondiente else Deja la columna en NULL; si TIPOUCI = 1/2/3 → Clasifica como UCI adultos / pediátrica / neonatal respectivamente; si TIPOIAD = 1/2/3 → Clasifica el tipo de infección como NAV / ISTU-AC / ITS-AC; si CRITCLASIFNAV = 1/2/3 → Marca el criterio de clasificación de Neumonía (NEU1/NEU2/NEU3); si CRITCLASIFITSAC = 1/2/3 → Marca el criterio de clasificación de ITS-AC (Criterio1/2/3); si CRITCLASIFISTUAC = 1/2/3/4 → Marca el criterio de clasificación de ISTU-AC (Criterio1a/2a/3a/4a); si CODMUESTRA(1-3) en {1..7} → Traduce a código oficial de muestra: 1→1, 2→2, 3→4, 4→10, 5→11, 6→31, 7→32; si CODPRUEBA(1-3) en {1..18} → Traduce a código oficial de prueba: 1→3, 2→55, 3→73, 4→92, 5→C7, 6..15→G0..G9, 16→H0, 17→H1, 18→H2; si JSON.DIPOSITIVO_UCI = ''TRUE'' o JSON.EVENTO_48H = ''TRUE'' → Marca la columna correspondiente con ''X''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila357';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA357', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila357';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila357';
-- GO
