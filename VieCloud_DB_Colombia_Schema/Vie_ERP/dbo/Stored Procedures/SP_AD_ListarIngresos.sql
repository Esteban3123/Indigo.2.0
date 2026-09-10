

CREATE PROCEDURE [dbo].[SP_AD_ListarIngresos]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
--SELECT CAST(0 AS BIT) AS 'CHECK',A.NUMINGRES AS 'Numero del Ingreso', A.IPCODPACI AS 'Identificacion', B.IPPRINOMB AS 'Primer Nombre',B.IPSEGNOMB AS 'Segundo Nombre',B.IPPRIAPEL AS 'Primer Apellido',B.IPSEGAPEL AS 'Segundo Apellido', C.NOMENTIDA AS 'Entidad'
SELECT  A.IPCODPACI AS 'Identificacion'
FROM ADINGRESO A
INNER JOIN INPACIENT B ON B.IPCODPACI=A.IPCODPACI
INNER JOIN INENTIDAD C ON C.CODENTIDA=B.CODENTIDA

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los ingresos o admisiones de pacientes registrados en el sistema, combinando información del episodio de ingreso (urgencias, hospitalización, consulta externa u otras modalidades), los datos del paciente y la entidad aseguradora o pagadora asociada (EPS, ARS, empresa contratante). Actualmente retorna la identificación o cédula del paciente como resultado principal, cruzando las tablas de ingresos, pacientes y entidades. Se utiliza como base para consultas y reportes de admisiones, permitiendo identificar qué pacientes tienen ingresos registrados y a qué entidad pertenecen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarIngresos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarIngresos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las identificaciones de pacientes asociadas a los ingresos administrativos, cruzando datos del paciente y su entidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros relacionables entre ingresos, pacientes y entidades por las claves IPCODPACI y CODENTIDA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ingresos cuyo paciente exista y cuyo paciente tenga entidad registrada (por uso de INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso; Paciente; Entidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve la identificación del paciente para cada ingreso que tenga paciente y entidad asociados (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresos';
-- GO
