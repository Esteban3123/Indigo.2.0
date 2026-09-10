
-- =============================================
-- Author:		<Juan Gilberto Montealegre>
-- Create date: <12/12/2013>
-- Description:	<Procedimiento para listar los diganosticos del recien nacido>
-- =============================================
CREATE PROCEDURE [dbo].[SPREP_HC_ListarDiagnosticosDelRecienNacido]
	-- Add the parameters for the stored procedure here
	@CodigoPaciente varchar(25),
	@NumeroIngreso char(10),
	@NumeroFolio char(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
			D.CONSECREC 'CONSECUTIVO',  ----- D.NUMCONSEC
			D.CODDIAGNO 'CODIGO DIAGNOSTICO',
			E.NOMDIAGNO 'NOMBRE DIAGNOSTICO' 
		FROM
			HCRECNADI D WITH(NOLOCK)
			INNER JOIN
			HCRECINAC  A WITH(NOLOCK)
			ON D.CONSECREC = A.NUMCONSEC   ---D.NUMCONSEC
			INNER JOIN 
			INDIAGNOS E WITH(NOLOCK)
			ON D.CODDIAGNO =  E.CODDIAGNO 

			WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMEFOLIO =@NumeroFolio AND A.NUMINGRES=@NumeroIngreso
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los diagnósticos CIE-10 registrados en la historia clínica del recién nacido para un paciente, ingreso y folio específicos. Cruza la tabla de diagnósticos asociados a la receta del recién nacido (HCRECNADI) con el registro de nacimiento (HCRECINAC) para filtrar por cédula del paciente, número de ingreso y número de folio, y luego enriquece cada código diagnóstico con su nombre oficial desde el catálogo maestro CIE-10 (INDIAGNOS). Se usa en la consulta de historia clínica perinatal para visualizar los diagnósticos que justifican las indicaciones médicas del neonato durante su hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarDiagnosticosDelRecienNacido';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_ListarDiagnosticosDelRecienNacido';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los diagnósticos registrados para el recién nacido asociado a un paciente, ingreso y folio determinados, mostrando el consecutivo, código y nombre del diagnóstico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosDelRecienNacido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de recién nacido para la combinación paciente + folio + ingreso.; Los códigos de diagnóstico almacenados deben existir en el catálogo de diagnósticos para que se incluyan en el resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosDelRecienNacido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan diagnósticos asociados al registro de recién nacido que coincide en paciente, folio e ingreso.; Los diagnósticos se vinculan al registro del recién nacido mediante el consecutivo (CONSECREC = NUMCONSEC).; Solo se listan diagnósticos cuyo código existe en el catálogo de diagnósticos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosDelRecienNacido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; recién nacido; diagnóstico; ingreso hospitalario; folio; historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosDelRecienNacido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCRECNADI: Cuando existe un registro de recién nacido para el paciente, folio e ingreso indicados, se devuelven sus diagnósticos enriquecidos con el nombre desde el catálogo (INNER JOIN INDIAGNOS).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosDelRecienNacido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRECNADI; dbo.HCRECINAC; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosDelRecienNacido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_ListarDiagnosticosDelRecienNacido';
-- GO
