-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,16-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila113]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 			
			Select
				Rtrim(PRIMNOMB) As 'PRIMNOMB', Rtrim(SEGNOMB) As 'SEGNOMB', Rtrim(PRIMAPEL) As 'PRIMAPEL', Rtrim(SEGAPEL) As 'SEGAPEL',
				CASE Rtrim(TIPOID)  WHEN '1' THEN 'RC' WHEN '2' THEN 'TI' WHEN '3' THEN 'CC' WHEN '4' THEN 'CE' WHEN '5' THEN 'PA' WHEN '6' THEN 'MS' WHEN '7' THEN 'AS' WHEN '8' THEN 'PE' WHEN 9 THEN 'PT' END AS 'TIPOID', 
				Rtrim(NUMIDENT) As 'NUMIDENT', Rtrim(NUMNINOS) As 'NUMNINOS', Rtrim(PESONACER) As 'PESONACER', Rtrim(TALLNACER) As 'TALLNACER', Rtrim(EDADGEST) As 'EDADGEST',
				Rtrim(TIEMREC) As 'TIEMREC', Rtrim(EDADINICIO) As 'EDADINICIO', Rtrim(PESOACT) As 'PESOACT', Rtrim(TALLACT) As 'TALLACT', Rtrim(CIRCUNMED) As 'CIRCUNMED',
				Rtrim(DIAGMEDI) As 'DIAGMEDI',
				CASE NIVELEDU WHEN '1' THEN 'X' END AS 'Primaria', CASE NIVELEDU WHEN '2' THEN 'X' END AS 'Secundaria', CASE NIVELEDU WHEN '3' THEN 'X' END AS 'Técnica',
				CASE NIVELEDU WHEN '4' THEN 'X' END AS 'Universitaria', CASE NIVELEDU WHEN '5' THEN 'X' END AS 'Ninguno',
				CASE INSCCREC WHEN '1' THEN 'X' END AS 'INSCCREC Si', CASE INSCCREC WHEN '0' THEN 'X' END AS 'INSCCREC No',
				CASE ESQVAC WHEN '1' THEN 'X' END AS 'ESQVAC Si', CASE ESQVAC WHEN '2' THEN 'X' END AS 'ESQVAC No', CASE ESQVAC WHEN '3' THEN 'X' END AS 'ESQVAC Desco',
				CASE REFECARN WHEN '1' THEN 'X' END AS 'REFECARN Si', CASE REFECARN WHEN '0' THEN 'X' END AS 'REFECARN No',
				CASE EDEMA WHEN '1' THEN 'X' END AS 'EDEMA Si', CASE EDEMA WHEN '0' THEN 'X' END AS 'EDEMA No',
				CASE DESNEMACIA WHEN '1' THEN 'X' END AS 'DESNEMACIA Si', CASE DESNEMACIA WHEN '0' THEN 'X' END AS 'DESNEMACIA No',
				CASE PIELRESEC WHEN '1' THEN 'X' END AS 'PIELRESEC Si', CASE PIELRESEC WHEN '0' THEN 'X' END AS 'PIELRESEC No',
				CASE HIPOHIPER WHEN '1' THEN 'X' END AS 'HIPOHIPER Si', CASE HIPOHIPER WHEN '0' THEN 'X' END AS 'HIPOHIPER No',
				CASE CAMBCABEL WHEN '1' THEN 'X' END AS 'CAMBCABEL Si', CASE CAMBCABEL WHEN '0' THEN 'X' END AS 'CAMBCABEL No',
				CASE ANEMDETEC WHEN '1' THEN 'X' END AS 'ANEMDETEC Si', CASE ANEMDETEC WHEN '0' THEN 'X' END AS 'ANEMDETEC No',
				CASE ACTRUT WHEN '1' THEN 'X' END AS 'ACTRUT Si', CASE ACTRUT WHEN '0' THEN 'X' END AS 'ACTRUT No',
				CASE TIPOATEN WHEN '1' THEN 'X' END AS 'TIPOATEN Si', CASE TIPOATEN WHEN '0' THEN 'X' END AS 'TIPOATEN No', 
				VERSION AS 'VERSION',
				JSON AS 'JSON',
				CASE RTRIM(JSON_VALUE(JSON, '$.RESUAPET')) WHEN 1 then 'Positiva' WHEN 2 then 'Negativa' WHEN 3 then 'No se realizó' END As 'RESUAPET'

			From 
				HCFICHA113 

			Where 
				IDFICHANOTIFICACION  = @IdFicha

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y presenta la información completa de la Ficha SIVIGILA número 113 para un caso específico de desnutrición infantil, identificado por su ID de ficha. Consulta la tabla HCFICHA113 y devuelve los datos del niño o niña: nombres completos, tipo y número de identificación (registro civil, tarjeta de identidad, cédula, etc.), medidas antropométricas al nacer y actuales (peso, talla, circunferencia), edad gestacional, diagnóstico médico, nivel educativo del cuidador, signos clínicos de desnutrición (edema, desnutrición emaciada, piel reseca, hipopigmentación, cambios en el cabello, anemia), esquema de vacunación, inscripción en programa de crecimiento y desarrollo, referencia al programa ICBF (carnetización), y tipo de atención recibida. Existe para alimentar la impresión o visualización oficial de la ficha epidemiológica de notificación obligatoria ante el SIVIGILA en casos de desnutrición infantil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila113';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila113';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea la información de una ficha de notificación Sivigila 113 (desnutrición/seguimiento) traduciendo códigos a etiquetas legibles para impresión o visualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila113';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA113 con IDFICHANOTIFICACION igual al identificador recibido; El campo JSON debe contener estructura válida con clave RESUAPET para que JSON_VALUE pueda extraerla', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila113';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los campos de texto se devuelven sin espacios en blanco a la derecha (RTRIM); Las columnas Si/No para variables clínicas son mutuamente excluyentes: solo una recibe ''X''; Los códigos no contemplados en los CASE se devuelven como NULL (sin mapeo); El parámetro NumFicha se recibe pero no se utiliza en el filtro; solo se filtra por IdFicha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila113';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Tipo de identificación del paciente; Nivel educativo; Esquema de vacunación; Inscripción en crecimiento y desarrollo; Carné de vacunación; Edema; Desnutrición/emaciación; Piel reseca; Hipopigmentación/hiperpigmentación; Cambios en cabello; Anemia detectada; Actividad rutinaria; Tipo de atención; Peso y talla al nacer y actuales; Edad gestacional; Diagnóstico médico; Resultado de prueba de apetito', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila113';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA113: Devuelve una fila con los datos de la ficha cuando IDFICHANOTIFICACION coincide con el parámetro de entrada, aplicando RTRIM y traducción de códigos a literales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila113';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOID entre ''1''..''9'' → Traduce a abreviatura del tipo de identificación: 1=RC, 2=TI, 3=CC, 4=CE, 5=PA, 6=MS, 7=AS, 8=PE, 9=PT; si NIVELEDU = ''1''..''5'' → Marca con ''X'' la columna correspondiente: 1=Primaria, 2=Secundaria, 3=Técnica, 4=Universitaria, 5=Ninguno; si ESQVAC = ''1'' | ''2'' | ''3'' → Marca esquema de vacunación como Si, No o Desconocido respectivamente; si Campos booleanos (INSCCREC, REFECARN, EDEMA, DESNEMACIA, PIELRESEC, HIPOHIPER, CAMBCABEL, ANEMDETEC, ACTRUT, TIPOATEN) = ''1'' o ''0'' → Marca con ''X'' la columna Si (=1) o No (=0); si JSON.RESUAPET = 1 | 2 | 3 → Traduce resultado de apetito: 1=Positiva, 2=Negativa, 3=No se realizó', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila113';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA113', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila113';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila113';
-- GO
