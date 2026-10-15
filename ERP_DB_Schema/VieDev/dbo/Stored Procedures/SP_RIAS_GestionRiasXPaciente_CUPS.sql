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