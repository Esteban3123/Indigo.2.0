
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_CriteriosIngresoInternos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT RTRIM(DESTIPCRI) AS 'TIPOS DE CRITERIO'

FROM HCCRIINGI A WITH(NOLOCK)
INNER JOIN HCCRIUNID B WITH(NOLOCK) ON A.CODTIPCRI=B.CODTIPCRI
              
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve los criterios clínicos que justificaron el ingreso de un paciente a una unidad especial de cuidado intensivo (UCI, neonatal u otras unidades críticas), dentro de su historia clínica hospitalaria. Recibe como parámetros la cédula o código del paciente, el número de ingreso y el número de folio de la historia clínica, y cruza los registros de criterios aplicados al paciente (HCCRIINGI) con el catálogo de tipos de criterios de ingreso a unidades especiales (HCCRIUNID) para obtener la descripción legible de cada criterio. Se utiliza en la visualización de la historia clínica para mostrar cuáles fueron los criterios médicos que determinaron la admisión del paciente a cuidados críticos o intensivos durante un ingreso hospitalario específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CriteriosIngresoInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CriteriosIngresoInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar la descripción de los tipos de criterio de ingreso registrados para un paciente en un ingreso y folio específicos, con fines de reporte de historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngresoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en la tabla de criterios de ingreso para la combinación de paciente, ingreso y folio recibida.; Los códigos de tipo de criterio deben estar parametrizados en el catálogo de criterios por unidad para que aparezcan en el resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngresoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan tipos de criterio que tengan correspondencia en el catálogo de criterios por unidad (INNER JOIN por código de tipo de criterio).; El resultado se filtra siempre por la combinación paciente + ingreso + folio.; Se aplica NOLOCK en las lecturas, permitiendo lecturas sucias para reportería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngresoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; folio; criterios de ingreso; historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngresoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCCRIINGI: Devuelve la descripción (DESTIPCRI) de los tipos de criterio asociados al paciente, ingreso y folio indicados, uniendo criterios de ingreso con su catálogo por código de tipo de criterio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngresoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCRIINGI; dbo.HCCRIUNID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngresoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngresoInternos';
-- GO
