
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo]
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
 FROM HCCRIINGU A WITH(NOLOCK)
 INNER JOIN HCCRIUNID B ON A.CODTIPCRI=B.CODTIPCRI
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los criterios clínicos de ingreso a UCI registrados para un paciente en un ingreso hospitalario y folio específicos de su historia clínica. Combina los registros de criterios asignados al paciente (HCCRIINGU) con el catálogo de tipos de criterio UCI (HCCRIUNID) para devolver la descripción legible de cada criterio. Se utiliza en la visualización de la historia clínica para mostrar qué condiciones clínicas justificaron la admisión del paciente a la unidad de cuidados intensivos. Recibe como parámetros la cédula o código del paciente, el número de ingreso hospitalario y el número de folio de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las descripciones de los tipos de criterio UCI registrados para un paciente en un ingreso y folio determinados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener registros de criterios UCI asociados al folio e ingreso indicados en HCCRIINGU.; Cada tipo de criterio referenciado debe existir en el catálogo HCCRIUNID.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan criterios cuyo CODTIPCRI exista en el catálogo HCCRIUNID (INNER JOIN).; Las consultas se realizan con NOLOCK, asumiendo lectura sucia tolerable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de atención; Criterios de UCI; Tipo de criterio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCCRIINGU: Devuelve DESTIPCRI (con RTRIM) cuando IPCODPACI, NUMINGRES y NUMEFOLIO coinciden con los parámetros, uniendo HCCRIINGU con HCCRIUNID por CODTIPCRI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCRIINGU; dbo.HCCRIUNID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarCriteriosUCIPacienteTodo';
-- GO
