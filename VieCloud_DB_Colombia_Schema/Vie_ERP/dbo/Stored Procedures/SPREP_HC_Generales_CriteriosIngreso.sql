CREATE PROCEDURE [dbo].[SPREP_HC_Generales_CriteriosIngreso]
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

FROM HCCRIINGU A with(noLock)
INNER JOIN HCCRIUNID B with(noLock)  ON A.CODTIPCRI=B.CODCONCEC         
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los criterios clínicos de ingreso a unidades especiales (UCI, unidad neonatal, etc.) registrados para un paciente en un ingreso hospitalario y folio específicos de la historia clínica. Cruza los registros de criterios de ingreso del paciente (HCCRIINGU) con el catálogo de tipos de criterios clínicos (HCCRIUNID) para obtener la descripción legible de cada criterio que justificó la admisión a la unidad crítica. Se usa en reportes de historia clínica general para mostrar, dado un paciente (cédula/documento), un número de ingreso y un folio, cuáles fueron los criterios médicos que determinaron su ingreso a una unidad de cuidado intensivo u otra unidad especial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CriteriosIngreso';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CriteriosIngreso';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las descripciones de los tipos de criterios de ingreso registrados para un paciente en un folio e ingreso específicos de su historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre el paciente, número de ingreso y número de folio en la tabla de criterios de ingreso.; El código de tipo de criterio registrado debe existir en el catálogo de criterios por unidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven criterios de ingreso asociados simultáneamente al paciente, ingreso y folio indicados.; El tipo de criterio mostrado proviene del catálogo de criterios por unidad mediante el vínculo CODTIPCRI = CODCONCEC.; Las consultas usan NOLOCK, por lo que pueden incluir lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso; folio; criterios de ingreso; historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCCRIINGU: Cuando existen registros en HCCRIINGU que cumplen IPCODPACI, NUMINGRES y NUMEFOLIO, se retorna la descripción (DESTIPCRI) del tipo de criterio obtenida al unir con HCCRIUNID por CODTIPCRI=CODCONCEC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCRIINGU; dbo.HCCRIUNID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CriteriosIngreso';
-- GO
