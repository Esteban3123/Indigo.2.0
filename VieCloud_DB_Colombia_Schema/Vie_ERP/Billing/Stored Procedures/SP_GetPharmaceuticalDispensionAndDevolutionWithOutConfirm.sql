
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que devuelve las dispensaciones y devoluciones no confirmadas por un ingreso
-- =============================================

CREATE PROCEDURE [Billing].[SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm]
	@admissionNumber varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	
	select 1 as [TypeDispensing], (
		SELECT STUFF((
			SELECT '-' + Code
			FROM Inventory.PharmaceuticalDispensing with(nolock)
			where AdmissionNumber = @admissionNumber And [Status] = 1
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')
	) as Codes

	union all

	select 2 as [TypeDispensing], (
		SELECT STUFF((
			SELECT '-' + Code
			FROM Inventory.PharmaceuticalDispensingDevolution with(nolock)
			where AdmissionNumber = @admissionNumber And [Status] = 1
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')
	) as Codes

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Devuelve los códigos de dispensaciones y devoluciones farmacéuticas que aún no han sido confirmadas para un número de ingreso hospitalario específico. Consulta dos fuentes: los despachos de medicamentos pendientes de confirmar (tipo 1) y las devoluciones de medicamentos pendientes de confirmar (tipo 2), ambas filtradas por número de ingreso y estado pendiente (Status = 1). Los códigos de cada tipo se concatenan en una sola cadena separada por guiones, facilitando la verificación previa a facturación de si existen movimientos farmacéuticos sin cierre que puedan afectar la cuenta del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, para un ingreso hospitalario, los códigos concatenados de dispensaciones y devoluciones farmacéuticas que aún no han sido confirmadas, agrupados por tipo.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El número de ingreso debe corresponder a registros existentes en Inventory.PharmaceuticalDispensing y/o Inventory.PharmaceuticalDispensingDevolution para obtener resultados.; Los registros relevantes son aquellos con Status = 1 (estado considerado ''sin confirmar'').', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran registros con Status = 1 (sin confirmar); cualquier otro estado se excluye.; El filtro siempre se hace por AdmissionNumber (un único ingreso por ejecución).; TypeDispensing=1 siempre corresponde a dispensaciones y TypeDispensing=2 siempre a devoluciones.; Los códigos se concatenan separados por ''-'' usando STUFF+FOR XML PATH; si no hay registros, Codes es NULL.; Las lecturas usan WITH(NOLOCK), por lo que pueden verse filas no confirmadas transaccionalmente (lecturas sucias).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Devolución de dispensación; Ingreso hospitalario (AdmissionNumber); Confirmación de documentos (Status)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result set: Devuelve dos filas: TypeDispensing=1 con la concatenación (separada por ''-'') de Code de PharmaceuticalDispensing donde AdmissionNumber=@admissionNumber AND Status=1; y TypeDispensing=2 con la misma concatenación sobre PharmaceuticalDispensingDevolution bajo idénticos filtros.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDevolution', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm';
-- GO
