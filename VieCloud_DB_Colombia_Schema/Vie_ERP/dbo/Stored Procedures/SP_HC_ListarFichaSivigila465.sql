-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,15-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila465]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON; 

			Select
				CASE A.ESPEPLASM WHEN 1 THEN 'X' END AS 'P_Vivax', CASE A.ESPEPLASM WHEN 2 THEN 'X' END AS 'P_Falciparum', CASE A.ESPEPLASM WHEN 3 THEN 'X' END AS 'P_Malariae', CASE A.ESPEPLASM WHEN 4 THEN 'X' END AS 'Infeccion_Mixta',
				RTRIM(A.PAISRESIDE) AS 'PAISRESIDE',
				CASE A.VIGIACT WHEN '1' THEN 'X' END AS 'VIGIACT si', CASE A.VIGIACT WHEN '0' THEN 'X' END AS 'VIGIACT no',
				CASE A.SINTOMATICO WHEN '1' THEN 'X' END AS 'SINTOMATICO si', CASE A.SINTOMATICO WHEN '0' THEN 'X' END AS 'SINTOMATICO no',
				CASE A.CLASIORIGE WHEN '1' THEN 'X' END AS 'Autoctono', CASE A.CLASIORIGE WHEN '0' THEN 'X' END AS 'Importado',				
				CASE A.RECRUDESCEN WHEN '1' THEN 'X' END AS 'RECRUDESCEN si', CASE A.RECRUDESCEN WHEN '0' THEN 'X' END AS 'RECRUDESCEN no',
				CASE A.TRIMESGEST WHEN '1' THEN 'X' END AS 'Primtri', CASE A.TRIMESGEST WHEN '2' THEN 'X' END AS 'SegTri', CASE A.TRIMESGEST WHEN '3' THEN 'X' END AS 'TerTri',
				CASE A.TIPOEXA WHEN '1' THEN 'X' END AS 'GG', CASE A.TIPOEXA WHEN '2' THEN 'X' END AS 'PDR', CASE A.TIPOEXA WHEN '3' THEN 'X' END AS 'PCR',
				Rtrim(A.RECUPARASI) As 'RECUPARASI',
				CASE A.GAMETOCI WHEN '1' THEN 'X' END AS 'GAMETOCI si', CASE A.GAMETOCI WHEN '0' THEN 'X' END AS 'GAMETOCI no',
				CASE A.COMPLICACIONES WHEN '1' THEN 'X' END AS 'COMPLICACIONES si', CASE A.COMPLICACIONES WHEN '0' THEN 'X' END AS 'COMPLICACIONES no',
				CASE A.CEREBRAL WHEN '1' THEN 'X' END AS 'CEREBRAL',
				CASE A.RENAL WHEN '1' THEN 'X' END AS 'RENAL', 
				CASE A.HEPATICA WHEN '1' THEN 'X' END AS 'HEPATICA',
				CASE A.PULMONAR WHEN '1' THEN 'X' END AS 'PULMONAR',
				CASE A.HEMATOLO WHEN '1' THEN 'X' END AS 'HEMATOLO', 
				CASE A.OTRAS WHEN '1' THEN 'X' END AS 'OTRAS',
				CASE A.TRATAMIENTO WHEN '1' THEN 'X' END AS 'Arte+Lume', CASE A.TRATAMIENTO WHEN '2' THEN 'X' END AS 'Cloro+Prima', CASE A.TRATAMIENTO WHEN '3' THEN 'X' END AS 'Cloro',
				CASE A.TRATAMIENTO WHEN '4' THEN 'X' END AS 'QuiniOral', CASE A.TRATAMIENTO WHEN '5' THEN 'X' END AS 'QuiniIntra', CASE A.TRATAMIENTO WHEN '6' THEN 'X' END AS 'ArteIntra',
				CASE A.TRATAMIENTO WHEN '7' THEN 'X' END AS 'Otro_Tratamiento', CASE A.TRATAMIENTO WHEN '8' THEN 'X' END AS 'ArteRectal', CASE A.TRATAMIENTO WHEN '9' THEN 'X' END AS 'QuiniOral+Cli+Pri',
				CASE A.TRATAMIENTO WHEN '10' THEN 'X' END AS 'QuiniOral+Doxi+Pri', CASE A.TRATAMIENTO WHEN '11' THEN 'X' END AS 'Arte+Lume+Pri', CASE A.TRATAMIENTO WHEN '12' THEN 'X' END AS 'QuiniIntra+Clin',
				CASE A.TRATAMIENTO WHEN '13' THEN 'X' END AS 'QuiniIntra+Doxi', CASE A.TRATAMIENTO WHEN '14' THEN 'X' END AS 'QuiniOral+Clin', CASE A.TRATAMIENTO WHEN '15' THEN 'X' END AS 'SinTrat', 
				CASE A.TRATAMIENTO WHEN '16' THEN 'X' END AS 'Arte+Lume+Pri+DosisU', CASE A.TRATAMIENTO WHEN '17' THEN 'X' END AS 'QuininaOral+Clin',
				Convert(varchar(10),A.FECHINITRAT,103) As 'FECHINITRAT',
				Rtrim(A.RESPONDIAGN) As 'RESPONDIAGN',				
				Convert(varchar(10),A.FECHRESU,103) As 'FECHRESU',
				Rtrim(A.NOMBREPACI) As 'NOMBREPACI', 
				Rtrim(A.APELLPACI) As 'APELLPACI', 
				CASE A.ESPECIE WHEN 1 THEN 'X' END AS 'EI_P_Vivax', CASE A.ESPECIE WHEN 2 THEN 'X' END AS 'EI_P_Falciparum', CASE A.ESPECIE WHEN 3 THEN 'X' END AS 'EI_P_Malariae', CASE A.ESPECIE WHEN 4 THEN 'X' END AS 'EI_Infeccion_Mixta',
				A.VERSION AS 'VERSION', 
				A.JSON AS 'JSON',
				RTRIM( JSON_VALUE(A.JSON,'$.NACIONALIDAD') ) AS 'NACIONALIDAD',				
				CASE TRY_CAST( JSON_VALUE(A.JSON,'$.DESPLAZAMIENTO15') AS BIT) WHEN 1 THEN 'X' END AS 'Desplazamiento_SI', CASE TRY_CAST( JSON_VALUE(A.JSON,'$.DESPLAZAMIENTO15') AS BIT) WHEN 0 THEN 'X' END AS 'Desplazamiento_NO',
				CASE WHEN U.AUUBICACI IS NOT NULL THEN CONCAT( RTRIM(Q.StandardCode), '-', RTRIM(Q.Name), ' / ', RTRIM(O.depcodigo), '-', RTRIM(O.nomdepart), ' / ', RTRIM(R.MUNCODIGO), '-', RTRIM(R.MUNNOMBRE) ) END as 'LUGAR_DESPLAZAMIENTO',
				RTRIM( JSON_VALUE(A.JSON,'$.RAZON_SOCIAL') ) AS 'RAZON_SOCIAL',
				RTRIM( JSON_VALUE(A.JSON,'$.NOMBRE_EVENTO') ) AS 'NOMBRE_EVENTO',
				RTRIM( JSON_VALUE(A.JSON,'$.CODIGO_EVENTO') ) AS 'CODIGO_EVENTO',
				RTRIM( JSON_VALUE(A.JSON,'$.BARRIO') ) AS 'BARRIO',
				RTRIM( JSON_VALUE(A.JSON,'$.CENTRORURAL') )AS 'CENTRORURAL',
				RTRIM( JSON_VALUE(A.JSON,'$.VEREDA') ) AS 'VEREDA',
				JSON_VALUE(A.JSON,'$.SEMANAS_GESTACION') AS 'SEMANAS_GESTACION',				
				CASE JSON_VALUE(A.JSON,'$.CLASIFICACION_CASO') WHEN 1 THEN 'X' END AS 'Sospechoso', CASE JSON_VALUE(A.JSON,'$.CLASIFICACION_CASO') WHEN 2 THEN 'X' END AS 'Probable',  CASE JSON_VALUE(A.JSON,'$.CLASIFICACION_CASO') WHEN 3 THEN 'X' END AS 'laboratorio',
				CASE JSON_VALUE(A.JSON,'$.CLASIFICACION_CASO') WHEN 4 THEN 'X' END AS 'Conf clinica',  CASE JSON_VALUE(A.JSON,'$.CLASIFICACION_CASO') WHEN 5 THEN 'X' END AS 'Conf epidemiológico',
				CASE JSON_VALUE(A.JSON,'$.CONDICION_FINAL') WHEN 1 THEN 'X' END AS 'Vivo', CASE JSON_VALUE(A.JSON,'$.CONDICION_FINAL') WHEN 2 THEN 'X' END AS 'Muerto', CASE JSON_VALUE(A.JSON,'$.CONDICION_FINAL') WHEN 3 THEN 'X' END AS 'No Sabe',
				RTRIM( JSON_VALUE(A.JSON,'$.NOMBRE_PROFESIONAL') ) AS 'NOMBRE_PROFESIONAL',
				RTRIM( JSON_VALUE(A.JSON,'$.TELEFONO_NOTIFICA') ) AS 'TELEFONO_NOTIFICA'
							   				 

			From 
				HCFICHA465 A
				Left Join INUBICACI U on JSON_VALUE(A.JSON,'$.CODLUGAR_DESPLAZAMIENTO') = U.AUUBICACI 
				Left Join INMUNICIP R on U.DEPMUNCOD = R.DEPMUNCOD 
				Left Join INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
				Left Join Common.Country Q ON O.IDPAIS = Q.Id 
			Where 
				A.IDFICHANOTIFICACION  = @IdFicha 
				AND ISJSON(A.JSON) > 0

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una ficha epidemiológica de malaria (SIVIGILA código 465) a partir de su identificador o número de ficha. Consolida los datos clínicos y de laboratorio del paciente —especie de plasmodium, tipo de examen (gota gruesa, PDR, PCR), recuento de parásitos, presencia de gametocitos, complicaciones (cerebral, renal, hepática, pulmonar, hematológica), esquema de tratamiento antimalárico y condición final— almacenados en la tabla HCFICHA465. Complementa la información con datos de ubicación geográfica del lugar de desplazamiento (municipio, departamento y país) cruzando los catálogos INUBICACI, INMUNICIP, INDEPARTA y Common.Country. Extrae además campos adicionales del campo JSON de la ficha, como clasificación del caso, nacionalidad, barrio, vereda, semanas de gestación, nombre del profesional notificador y teléfono de contacto, formateando cada atributo para su impresión directa en el formulario oficial de notificación a SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila465';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila465';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información detallada de una ficha de notificación Sivigila (evento 465 - Malaria), transformando códigos numéricos en marcas ''X'' para impresión/visualización del formato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila465';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La ficha identificada debe existir en HCFICHA465.; El campo JSON de la ficha debe contener un JSON válido (ISJSON > 0).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila465';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información de fichas con JSON válido.; Las marcas devueltas siempre son ''X'' o NULL (formato tipo formulario).; Las fechas FECHINITRAT y FECHRESU se entregan en formato dd/MM/yyyy (estilo 103).; El lugar de desplazamiento solo se arma si existe la ubicación en INUBICACI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila465';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Malaria (Plasmodium Vivax, Falciparum, Malariae, Infección mixta); Vigilancia activa; Caso autóctono/importado; Recrudescencia; Trimestre de gestación; Tipo de examen diagnóstico (Gota Gruesa, PDR, PCR); Recuento de parásitos; Gametocitos; Complicaciones (cerebral, renal, hepática, pulmonar, hematológica); Esquemas de tratamiento antimalárico; Clasificación del caso (sospechoso, probable, confirmado); Condición final del paciente; Desplazamiento del paciente; Nacionalidad; Profesional notificador', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila465';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA465: Cuando IDFICHANOTIFICACION coincide con el parámetro y JSON es válido, retorna el detalle de la ficha con campos codificados traducidos a ''X'' y datos extraídos del JSON.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila465';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESPEPLASM = 1/2/3/4 → Marca como P_Vivax / P_Falciparum / P_Malariae / Infección Mixta respectivamente; si ESPECIE = 1/2/3/4 → Marca infección inicial como EI_P_Vivax / EI_P_Falciparum / EI_P_Malariae / EI_Infección_Mixta; si CLASIORIGE = 1 / 0 → Clasifica el caso como Autóctono / Importado; si TRIMESGEST = 1/2/3 → Marca trimestre de gestación primero/segundo/tercero; si TIPOEXA = 1/2/3 → Marca tipo de examen GG / PDR / PCR; si TRATAMIENTO entre 1 y 17 → Marca el esquema terapéutico correspondiente (Arte+Lume, Cloro+Prima, Quinina, SinTrat, etc.); si JSON.CLASIFICACION_CASO = 1..5 → Marca caso como Sospechoso / Probable / Laboratorio / Confirmado clínica / Confirmado epidemiológico; si JSON.CONDICION_FINAL = 1/2/3 → Marca condición final como Vivo / Muerto / No Sabe; si JSON.DESPLAZAMIENTO15 castable a BIT = 1/0 → Marca Desplazamiento_SI / Desplazamiento_NO; si U.AUUBICACI IS NOT NULL → Construye cadena ''LUGAR_DESPLAZAMIENTO'' concatenando país, departamento y municipio del lugar de desplazamiento else Devuelve NULL en LUGAR_DESPLAZAMIENTO; si VIGIACT / SINTOMATICO / RECRUDESCEN / GAMETOCI / COMPLICACIONES = 1 ó 0 → Marca columna ''si'' o ''no'' según corresponda', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila465';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA465; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; Common.Country', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila465';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila465';
-- GO
