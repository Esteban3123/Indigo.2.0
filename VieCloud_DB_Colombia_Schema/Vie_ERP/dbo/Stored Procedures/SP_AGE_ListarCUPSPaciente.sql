-- =============================================
-- Author:		<Author,Juan David Patiño Cabrera,Name>
-- Create date: <Create Date,01-03-2019,>
-- Description:	<Description,Sp que me lista la Información por paciente si ya fue atentiendo en la Institución con el CUPS ,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AGE_ListarCUPSPaciente]
@Paciente varchar(25),
@CUPS  char(20),
@date as char(10)

AS
BEGIN
	SET NOCOUNT ON;

  	Select so.PatientCode, so.Status, so.OrderDate, ce.Code
		from Billing.ServiceOrder so
			inner join Billing.ServiceOrderDetail sod on sod.ServiceOrderId = so.Id
			inner join Contract.CUPSEntity ce on ce.Id = sod.CUPSEntityId 
	Where so.PatientCode = @Paciente And  so.Status in (1,2) And ce.Code = @CUPS and DATEPART(YEAR, so.OrderDate) = @date and (sod.ApplyRIAS is null or sod.ApplyRIAS = 0)
UNION ALL
	 Select IPCODPACI,CODESTCIT,FECHORAIN,D.CODSERIPS 
	    from AGASICITA a 
		Inner join AGACTIMED D  ON A.CODACTMED=D.CODACTMED
	  Where  a.IPCODPACI = @Paciente And  a.CODESTCIT in (0,1)  And  d.CODSERIPS = @CUPS  and DATEPART(YEAR, a.FECHORAIN) = @date

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta si un paciente ya fue atendido en la institución con un código CUPS (procedimiento o servicio específico) en un año determinado. Combina dos fuentes: las órdenes de servicio de facturación activas (estados 1 y 2, excluyendo ítems RIAS) y las citas agendadas vigentes (estados 0 y 1), unificando ambos registros para obtener un historial completo de atención. Se usa principalmente en el agendamiento para validar si el paciente ya tiene o tuvo una atención o cita con ese servicio CUPS en el año consultado, evitando duplicados o apoyando reglas de negocio de programación. Recibe como parámetros la cédula del paciente, el código CUPS y el año de búsqueda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarCUPSPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarCUPSPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Verifica si un paciente ya tiene registrada una orden o cita con un CUPS específico en un año dado, consolidando información de facturación y agendamiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarCUPSPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El año se compara contra DATEPART(YEAR, fecha) por lo que el parámetro de fecha debe representar un año válido; El paciente y el código CUPS deben existir en los sistemas consultados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarCUPSPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes de servicio en estados 1 o 2 (excluye otros estados); Solo se consideran citas en estados 0 o 1; Se excluyen detalles de orden marcados como RIAS (ApplyRIAS = 1); El filtrado temporal es a nivel de año, no de fecha exacta; Combina resultados de dos subsistemas (facturación y agenda) mediante UNION ALL sin deduplicar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarCUPSPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; CUPS (Clasificación Única de Procedimientos en Salud); Orden de servicio; Cita médica; RIAS (Rutas Integrales de Atención en Salud); Actividad médica; Servicio IPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarCUPSPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ServiceOrder: Devuelve órdenes con Status IN (1,2) cuyo detalle no aplique RIAS (ApplyRIAS IS NULL OR =0), filtradas por paciente, CUPS y año; [RETURN_RESULT] AGASICITA: Devuelve citas con CODESTCIT IN (0,1) filtradas por paciente, código de servicio IPS y año, unidas con AGACTIMED por CODACTMED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarCUPSPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrder; Billing.ServiceOrderDetail; Contract.CUPSEntity; dbo.AGASICITA; dbo.AGACTIMED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarCUPSPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarCUPSPaciente';
-- GO
