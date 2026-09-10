
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Sol_ListarSolicitudes]
	(
	@CodUsuario char(20)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COMAUTON,COMFECHA,COMESTADO,COMOBSERV,COMPRIORI,CODUSUARI,(a.UFUCODIGO ) AS CodUni,(B.UFUDESCRI ) AS UFUNOM
	from dbo .SOLCOMPRA  A INNER JOIN dbo .INUNIFUNC AS B ON A.UFUCODIGO = B.UFUCODIGO
		WHERE CODUSUARI = @CodUsuario 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las solicitudes de compra registradas por un usuario específico, combinando los datos de la solicitud (número, fecha, estado, observaciones, prioridad) con el nombre de la unidad funcional o área que realizó el pedido. Consulta la tabla de solicitudes de compra junto al catálogo de unidades funcionales para mostrar la descripción legible del servicio o área solicitante. Se usa para que cada usuario pueda ver y hacer seguimiento a sus propios requerimientos de adquisición de bienes o servicios pendientes, aprobados o rechazados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Sol_ListarSolicitudes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Sol_ListarSolicitudes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de compra registradas por un usuario específico, junto con la descripción de su unidad funcional asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un código de usuario válido; La unidad funcional asociada a la solicitud debe existir en el catálogo de unidades funcionales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan solicitudes de compra cuyo usuario creador coincida con el usuario consultado; Cada solicitud retornada debe tener una unidad funcional existente en INUNIFUNC (INNER JOIN obliga la correspondencia)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de compra; Unidad funcional; Usuario solicitante', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.SOLCOMPRA: Cuando CODUSUARI coincide con el usuario indicado, retorna el conjunto de solicitudes de compra con datos de la unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SOLCOMPRA; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolicitudes';
-- GO
