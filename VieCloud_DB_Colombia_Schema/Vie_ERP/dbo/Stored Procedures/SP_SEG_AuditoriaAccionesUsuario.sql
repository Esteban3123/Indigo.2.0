
-- =============================================
-- Author:		Juan Montealegre
-- Create date: 01/10/2013
-- Description:	Registro de Auditoria
-- =============================================
CREATE PROCEDURE  [dbo].[SP_SEG_AuditoriaAccionesUsuario]
	-- Add the parameters for the stored procedure here
	@CodigoUsuario char(20),
	@NombreEquipo char(20),
	@DescripcionMensaje varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO INDAUDITOR (CODUSUARI, FECREGISTRO, NOMEQUIPO, MENSAJEAUD)  VALUES ( @CodigoUsuario, getdate(), @NombreEquipo, @DescripcionMensaje)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra en el historial de auditoría del sistema una acción o evento realizado por un usuario. Recibe el código del usuario que ejecutó la acción, el nombre del equipo desde donde se realizó y una descripción del evento o mensaje, y los guarda con la fecha y hora exacta del momento del registro en la tabla INDAUDITOR. Se utiliza para llevar trazabilidad y control de seguridad sobre las acciones de los usuarios en el sistema, permitiendo saber quién hizo qué, cuándo y desde qué máquina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_AuditoriaAccionesUsuario';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_AuditoriaAccionesUsuario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra en la bitácora de auditoría una acción ejecutada por un usuario, dejando traza del equipo origen, mensaje descriptivo y fecha del evento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaAccionesUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario y el equipo deben estar identificados al invocar el registro; Debe proporcionarse un mensaje descriptivo de la acción a auditar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaAccionesUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de registro se asigna en el servidor con GETDATE(), nunca proviene del invocador; Cada llamada produce exactamente un registro de auditoría', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaAccionesUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Auditoría de acciones de usuario; Trazabilidad de seguridad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaAccionesUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.INDAUDITOR: Siempre inserta una fila en INDAUDITOR con el usuario, nombre de equipo y mensaje recibidos, asignando la fecha de registro mediante GETDATE()', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaAccionesUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AuditoriaAccionesUsuario';
-- GO
