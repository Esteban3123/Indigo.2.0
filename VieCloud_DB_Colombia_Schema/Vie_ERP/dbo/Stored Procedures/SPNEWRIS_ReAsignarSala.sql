
/***********************************************************************************************
Modified By: Andres Ramirez
Date: 2020.01.22
Description: Consulta para sala con fecha y hora, y cambiar estado dentro de la tabla RISORDENES - Web 
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPNEWRIS_ReAsignarSala] 
(
@Auto int,
@Estado int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

UPDATE RISORDENES SET ESTADO = @Estado, IDSALA = NULL, FECHORAIN = NULL, FECHORAFI = NULL --,FECAGENDO = NULL, USUAGENDO = @Usuario
WHERE AUTO = @Auto

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reasigna una orden de imagen diagnóstica (radiología, ecografía, etc.) liberando la sala asignada y borrando las fechas y horas de inicio y fin programadas. Actualiza el estado de la orden en la tabla RISORDENES según el nuevo estado indicado, dejando el registro sin sala ni horario asociado. Se utiliza cuando una orden debe desagendarse o reasignarse a otra sala, tanto para pacientes hospitalizados como ambulatorios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_ReAsignarSala';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_ReAsignarSala';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reasigna una orden de imágenes liberando la sala y los horarios de inicio/fin asignados, dejando la orden en un nuevo estado para reagendamiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReAsignarSala';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una orden en RISORDENES con el identificador AUTO suministrado; El estado destino suministrado debe ser un valor válido del catálogo de estados de la orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReAsignarSala';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Al reasignar, la sala asignada (IDSALA) y las fechas/horas de inicio y fin (FECHORAIN, FECHORAFI) siempre quedan en NULL; El cambio se aplica únicamente a la orden identificada por AUTO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReAsignarSala';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imágenes (RIS); Sala; Estado de orden; Reasignación/Reagendamiento; Horario de inicio y fin', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReAsignarSala';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.RISORDENES: Para la orden cuyo AUTO coincide con el parámetro, se asigna el nuevo ESTADO y se anulan IDSALA, FECHORAIN y FECHORAFI (libera sala y horarios).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReAsignarSala';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReAsignarSala';
-- GO
