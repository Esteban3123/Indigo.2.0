-- =============================================
-- Author:		Rafael Eduardo patiño Cabrera
-- Create date: 26/03/2019
-- Description:	retorna los CUPS asociados a las rias , y informacion de rias cups x paciente
-- =============================================
CREATE PROCEDURE [dbo].[SP_RIAS_GestionRiasXPaciente_CUPS]
	@Identificacion Varchar(25),
	@IdRias Varchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	select IDRIASCUPS,IDRIAS,CodigoCUPS,NombreCUPS, ISNULL(CantidadRealizada,0) as CantidadRealizada,UltimaFechaRealizacion,RealizaraPartirDe,RealizarAntesDe,FechaCita  from (
				select RC.Id as IDRIASCUPS,RC.IDRIAS,   RTRIM(S.CODSERIPS) as CodigoCUPS, RTRIM(S.DESSERIPS ) as NombreCUPS,
						(select SUM(CANTIDAD) as CantidadRealizadas  from RIASCUPSPACIENTE where IPCODPACI = @Identificacion and ESTADO = 2 and IDRIASCUPS = RC.ID  group by IDRIASCUPS) as CantidadRealizada,
						(select top 1 convert(varchar(20),FECHAREALIZACION,103)   from RIASCUPSPACIENTE where IPCODPACI = @Identificacion and ESTADO = 2 and IDRIASCUPS = RC.ID  order by FECHACREACION desc) as UltimaFechaRealizacion,
						(select top 1 convert(varchar(20),FECHAMINREALIZAR,103)   from RIASCUPSPACIENTE where IPCODPACI = @Identificacion and ESTADO = 1 and IDRIASCUPS = RC.ID  order by FECHACREACION desc) as RealizaraPartirDe,
						(select top 1 convert(varchar(20),FECHAMAXREALIZAR,103)   from RIASCUPSPACIENTE where IPCODPACI = @Identificacion and ESTADO = 1 and IDRIASCUPS = RC.ID  order by FECHACREACION desc) as RealizarAntesDe,
						(select top 1 IIF(C.FECHORAFI is null,'Sin cita',C.FECHORAFI)   from RIASCUPSPACIENTE RCP inner join AGASICITA C on RCP.IDCITA = C.CODAUTONU  where C.IPCODPACI = @Identificacion and C.FECHORAIN > [Common].[GETDATE]() and C.CODESTCIT =0 and RCP.ESTADO = 1 and RCP.IDRIASCUPS = 2 AND IDCITA is not null  order by FECHACREACION desc)  as FechaCita 
				from RIASCUPS RC inner join 
						INCUPSIPS S on RC.CODSERIPS = S.CODSERIPS
				WHERE RC.IDRIAS in (select * from dbo.SplitString(@IdRias))
	) as tmp

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve los servicios CUPS asociados a una o varias Rutas Integrales de Atención en Salud (RIAS) para un paciente específico, identificado por su cédula o documento. Por cada CUPS de la ruta, retorna el código y nombre del procedimiento, la cantidad total de veces que ya fue realizado, la última fecha de realización, el rango de fechas en que debe ejecutarse (fecha mínima y máxima), y la próxima cita agendada relacionada. Compone información de la tabla de relación RIAS-CUPS, el catálogo de servicios CUPS/IPS y el historial de cumplimiento por paciente, permitiendo visualizar el estado de avance de cada paciente dentro de su ruta de atención preventiva o de seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los CUPS asociados a una o varias RIAS y, para un paciente específico, retorna su estado de ejecución: cantidad realizada, última fecha de realización, ventana de fechas para realizar y próxima cita programada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la función dbo.SplitString para parsear la lista de identificadores RIAS; El identificador del paciente debe corresponder a IPCODPACI en RIASCUPSPACIENTE/AGASICITA; Los IDs de RIAS recibidos deben existir en la tabla RIASCUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las cantidades realizadas se calculan solo sobre registros con ESTADO=2 (realizado); Las fechas mínima/máxima de realización se obtienen solo de registros con ESTADO=1 (pendiente); Solo se consideran citas activas (CODESTCIT=0) con fecha futura respecto a la fecha del sistema; Si no hay cantidad realizada se retorna 0 por defecto; Las fechas se entregan en formato dd/mm/yyyy (estilo 103)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); CUPS (Códigos Únicos de Procedimientos en Salud); Paciente; Cita médica; Estado de cita; Estado de procedimiento (pendiente/realizado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.RIASCUPS: Retorna resultset con CUPS de las RIAS indicadas en @IdRias (filtradas vía SplitString) cruzando información de ejecución del paciente identificado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTADO = 2 en RIASCUPSPACIENTE → Se considera CUPS realizado: suma cantidades y toma la última fecha de realización; si ESTADO = 1 en RIASCUPSPACIENTE → Se considera CUPS pendiente: toma fechas mínima y máxima para realización; si Cita con FECHORAIN > fecha actual y CODESTCIT = 0 y RCP.ESTADO = 1 → Devuelve fecha de la cita programada else Devuelve ''Sin cita'' cuando FECHORAFI es null', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RIASCUPS; dbo.INCUPSIPS; dbo.RIASCUPSPACIENTE; dbo.AGASICITA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente_CUPS';
-- GO
