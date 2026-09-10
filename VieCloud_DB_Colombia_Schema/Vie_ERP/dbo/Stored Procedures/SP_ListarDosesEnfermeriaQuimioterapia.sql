/**** 
SP para listar las dosis que se crean al confirmar la cita de quimioterapia para la construcción de los objetos Doses de UNIHEALTH
****/
CREATE PROCEDURE [dbo].[SP_ListarDosesEnfermeriaQuimioterapia]
(
  @Paciente as varchar(25),
  @Ingreso as char(10),
  @Producto as char(20),
  @IdCita as int
)
WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON;
		
			SELECT 
				DOSISPROD as 'CANTIDAD DOSIS', A.FECPROAPL AS 'FECHA INICIO',
				RTRIM(A.CODUNIMED) AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA', A.DOSISPROD AS 'DOSIS', 'Medicamento' as 'Tipo', CODPRODUC as 'CODPRODUC'
				,IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA'
			FROM HCHOJAMED A
			INNER JOIN INUNIMEDI H ON A.CODUNIMED = H.CODUNIMED
			WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND CODPRODUC = @Producto AND A.IDCITA = @IdCita AND MEDESTADO = 1

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las dosis de medicamentos de quimioterapia generadas al confirmar una cita, para un paciente, ingreso, producto y cita específicos. Consulta la hoja de medicamentos (HCHOJAMED) filtrando únicamente los medicamentos activos (MEDESTADO=1), y complementa la información con la descripción de la unidad de medida desde el maestro de unidades (INUNIMEDI). Devuelve datos como cantidad de dosis, fecha de inicio, unidad de medida, indicador de dosis única y código del producto, con el propósito de construir los objetos ''Doses'' requeridos por la integración con el sistema UNIHEALTH para la administración de quimioterapia en enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDosesEnfermeriaQuimioterapia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDosesEnfermeriaQuimioterapia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las dosis activas de medicamento generadas al confirmar una cita de quimioterapia, para construir los objetos Doses en la integración con UNIHEALTH.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeriaQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente, ingreso, producto y cita indicados en HCHOJAMED.; La unidad de medida del registro debe existir en INUNIMEDI.; Solo se consideran registros con MEDESTADO = 1 (medicamento activo/vigente).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeriaQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone medicamentos en estado activo (MEDESTADO = 1).; El tipo retornado siempre es ''Medicamento''.; Cada dosis listada está asociada a una unidad de medida válida (INNER JOIN con INUNIMEDI).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeriaQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Quimioterapia; Cita; Paciente; Ingreso; Dosis; Medicamento; Unidad de medida; Dosis única; Hoja de medicación; Integración UNIHEALTH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeriaQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHOJAMED: Cuando MEDESTADO = 1 y coinciden paciente, ingreso, producto e IDCITA, se retorna la dosis con su cantidad, fecha de inicio, unidad de medida y marca de dosis única.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeriaQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DURACIDOS = ''Dosis Unica'' → Marca el registro como dosis única (1) else Marca como dosis no única (0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeriaQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJAMED; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeriaQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeriaQuimioterapia';
-- GO
