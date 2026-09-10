-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,23-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila740]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 			
			Select
				CASE CONDMOMEDIAG WHEN 1 THEN 'X' END AS 'Embarazo', CASE CONDMOMEDIAG WHEN 2 THEN 'X' END AS 'Parto', CASE CONDMOMEDIAG WHEN 3 THEN 'X' END AS 'Puerperio',				
				CASE CTRLPRENEMBACT WHEN '1' THEN 'X' END AS 'CTRLPRENEMBACT Si', CASE CTRLPRENEMBACT WHEN '0' THEN 'X' END AS 'CTRLPRENEMBACT No',
				Rtrim(EDADGESTPRIMCTRL) As 'EDADGESTPRIMCTRL',
				CASE DIAGEMBACT WHEN '1' THEN 'X' END AS 'Primera Vez', CASE DIAGEMBACT WHEN '0' THEN 'X' END AS 'Reinfección',
				CASE PRUEBATREPO WHEN '1' THEN 'X' END AS 'PRUEBATREPO Si', CASE PRUEBATREPO WHEN '0' THEN 'X' END AS 'PRUEBATREPO No',
				Rtrim(EDGESTREAPRUE) As 'EDGESTREAPRUE',
				CASE CUAL WHEN 3 THEN 'X' END AS 'Prueba Rapida', CASE CUAL WHEN 4 THEN 'X' END AS 'Otra',
				CASE RESULTADO WHEN '1' THEN 'X' END AS 'RESULTADO Si', CASE RESULTADO WHEN '0' THEN 'X' END AS 'RESULTADO No',
				CASE PRUEBANOTREPO WHEN '1' THEN 'X' END AS 'PRUEBANOTREPO Si', CASE PRUEBANOTREPO WHEN '0' THEN 'X' END AS 'PRUEBANOTREPO No',
				Rtrim(EDGESTREAPRUE2) As 'EDGESTREAPRUE2',
				CASE RESULTADO2 WHEN '1' THEN 'X' END AS '2 DILS', CASE RESULTADO2 WHEN '2' THEN 'X' END AS '4 DILS', CASE RESULTADO2 WHEN '3' THEN 'X' END AS '8 DILS',
				CASE RESULTADO2 WHEN '4' THEN 'X' END AS '16 DILS', CASE RESULTADO2 WHEN '5' THEN 'X' END AS '32 DILS', CASE RESULTADO2 WHEN '6' THEN 'X' END AS '64 DILS',
				CASE RESULTADO2 WHEN '7' THEN 'X' END AS '128 DILS', CASE RESULTADO2 WHEN '8' THEN 'X' END AS '256 DILS', CASE RESULTADO2 WHEN '9' THEN 'X' END AS '512 DILS',
				CASE RESULTADO2 WHEN '10' THEN 'X' END AS '1024 DILS', CASE RESULTADO2 WHEN '11' THEN 'X' END AS '2048 DILS',
				CASE PENICILINA WHEN '1' THEN 'X' END AS '0 Dosis', CASE PENICILINA WHEN '2' THEN 'X' END AS '1 Dosis', CASE PENICILINA WHEN '3' THEN 'X' END AS '2 Dosis', CASE PENICILINA WHEN '4' THEN 'X' END AS '3 Dosis',
				convert(varchar(10),FECHAAPLI,103) As 'FECHAAPLI',
				CASE TRATCONTAC WHEN '1' THEN 'X' END AS 'TRATCONTAC Si', CASE TRATCONTAC WHEN '0' THEN 'X' END AS 'TRATCONTAC No',
				Rtrim(NOMAPEMAD) As 'NOMAPEMAD',
				CASE Rtrim(TIPOID)  WHEN '1' THEN 'RC - Registro Civil' WHEN '2' THEN 'TI - Tarjeta de Identidad' WHEN '3' THEN 'CC - Cédula de Ciudadanía' WHEN '4' THEN 'CE - Cédula de Extranjería' WHEN '5' THEN 'PA - Pasaporte' WHEN '6' THEN 'MS - Menor Sin Identificación' WHEN '7' THEN 'AS - Adulto Sin Identificación' WHEN '8' THEN 'PE - Permiso Especial de Permanencia' WHEN '9' THEN 'PT - Permiso Temporal de Permanencia' END AS 'TIPOID',
				Rtrim(NUMIDENT) As 'NUMIDENT',
				Rtrim(NUMPROD) As 'NUMPROD', Rtrim(EDADGEST) As 'EDADGEST',
				CASE RESGEST WHEN '1' THEN 'X' END AS 'RESGEST Si', CASE RESGEST WHEN '0' THEN 'X' END AS 'RESGEST No',
				CASE RESSEREOMAD WHEN '1' THEN 'X' END AS '2 DILS2', CASE RESSEREOMAD WHEN '2' THEN 'X' END AS '4 DILS2', CASE RESSEREOMAD WHEN '3' THEN 'X' END AS '8 DILS2',
				CASE RESSEREOMAD WHEN '4' THEN 'X' END AS '16 DILS2', CASE RESSEREOMAD WHEN '5' THEN 'X' END AS '32 DILS2', CASE RESSEREOMAD WHEN '6' THEN 'X' END AS '64 DILS2',
				CASE RESSEREOMAD WHEN '7' THEN 'X' END AS '128 DILS2', CASE RESSEREOMAD WHEN '8' THEN 'X' END AS '256 DILS2', CASE RESSEREOMAD WHEN '9' THEN 'X' END AS '512 DILS2',
				CASE RESSEREOMAD WHEN '10' THEN 'X' END AS '1024 DILS2', CASE RESSEREOMAD WHEN '11' THEN 'X' END AS '2048 DILS2',
				CASE RESSEREORECNA WHEN '1' THEN 'X' END AS '2 DILS3', CASE RESSEREORECNA WHEN '2' THEN 'X' END AS '4 DILS3', CASE RESSEREORECNA WHEN '3' THEN 'X' END AS '8 DILS3',
				CASE RESSEREORECNA WHEN '4' THEN 'X' END AS '16 DILS3', CASE RESSEREORECNA WHEN '5' THEN 'X' END AS '32 DILS3', CASE RESSEREORECNA WHEN '6' THEN 'X' END AS '64 DILS3',
				CASE RESSEREORECNA WHEN '7' THEN 'X' END AS '128 DILS3', CASE RESSEREORECNA WHEN '8' THEN 'X' END AS '256 DILS3', CASE RESSEREORECNA WHEN '9' THEN 'X' END AS '512 DILS3',
				CASE RESSEREORECNA WHEN '10' THEN 'X' END AS '1024 DILS3', CASE RESSEREORECNA WHEN '11' THEN 'X' END AS '2048 DILS3', CASE RESSEREORECNA WHEN '12' THEN 'X' END AS 'No Reactiva', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.TIEMPO_RESIDENCIA') AS BIT) WHEN 1 THEN 'X' END AS 'Tiempo_Mayor6', CASE TRY_CAST( JSON_VALUE(JSON,'$.TIEMPO_RESIDENCIA') AS BIT) WHEN 0 THEN 'X' END AS 'Tiempo_Menor6' 
						
			From 
				HCFICHA740 

			Where 
				IDFICHANOTIFICACION  = @IdFicha				
				AND ISJSON(JSON) > 0
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y presenta el detalle completo de una ficha de notificación SIVIGILA 740, utilizada para la vigilancia epidemiológica de sífilis gestacional y congénita. A partir del identificador único de la ficha (@IdFicha) y su número (@NumFicha), consulta la tabla HCFICHA740 y devuelve en formato legible los datos clínicos y de laboratorio de la madre y el recién nacido: condición en el momento del diagnóstico (embarazo, parto o puerperio), controles prenatales, pruebas treponémicas y no treponémicas con sus diluciones serológicas, tratamiento con penicilina, resultado gestacional y serología materna y neonatal. También extrae información adicional almacenada en formato JSON dentro de la ficha, como el tiempo de residencia. Se usa para imprimir o visualizar la ficha epidemiológica oficial de notificación obligatoria de sífilis ante las autoridades de salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila740';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila740';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de la ficha de notificación Sivigila 740 (sífilis gestacional/congénita) formateados con marcas ''X'' por opción para impresión/visualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila740';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La ficha debe existir en HCFICHA740 con el identificador suministrado.; El campo JSON de la ficha debe contener un JSON válido (ISJSON(JSON) > 0).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila740';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan fichas cuyo campo JSON sea válido.; Las casillas no aplicables quedan en NULL (solo se marca ''X'' la opción seleccionada).; La fecha de aplicación se entrega en formato dd/mm/yyyy (estilo 103).; El parámetro @NumFicha se recibe pero no participa en el filtro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila740';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha Sivigila 740; Sífilis gestacional; Sífilis congénita; Control prenatal; Edad gestacional; Prueba treponémica; Prueba no treponémica; Diluciones serológicas; Aplicación de penicilina; Tipo de identificación del paciente; Tiempo de residencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila740';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA740: Cuando IDFICHANOTIFICACION coincide y el campo JSON es válido, retorna una fila con todos los campos de la ficha transformados a marcas ''X'' según los códigos almacenados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila740';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CONDMOMEDIAG = 1 / 2 / 3 → Marca ''X'' en Embarazo, Parto o Puerperio respectivamente.; si CTRLPRENEMBACT = ''1'' o ''0'' → Marca ''X'' en control prenatal Si o No.; si DIAGEMBACT = ''1'' o ''0'' → Marca ''X'' en Primera Vez o Reinfección.; si PRUEBATREPO = ''1'' o ''0'' → Marca ''X'' en prueba treponémica Si o No.; si CUAL = 3 o 4 → Marca ''X'' en Prueba Rápida u Otra.; si RESULTADO2 entre ''1'' y ''11'' → Marca ''X'' en la dilución correspondiente (2, 4, 8, 16, 32, 64, 128, 256, 512, 1024 o 2048 DILS).; si PENICILINA entre ''1'' y ''4'' → Marca ''X'' en 0, 1, 2 o 3 dosis aplicadas.; si TIPOID entre ''1'' y ''9'' → Traduce al texto del tipo de identificación (RC, TI, CC, CE, PA, MS, AS, PE, PT).; si RESSEREOMAD / RESSEREORECNA entre ''1'' y ''11'' (y ''12'' para RESSEREORECNA) → Marca ''X'' en la dilución serológica de madre/recién nacido o ''No Reactiva''.; si TRY_CAST(JSON_VALUE(JSON,''$.TIEMPO_RESIDENCIA'') AS BIT) = 1 o 0 → Marca ''X'' en Tiempo_Mayor6 o Tiempo_Menor6.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila740';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA740', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila740';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila740';
-- GO
