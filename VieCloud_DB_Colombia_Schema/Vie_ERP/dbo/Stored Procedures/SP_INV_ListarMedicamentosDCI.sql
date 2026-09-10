

CREATE PROCEDURE [dbo].[SP_INV_ListarMedicamentosDCI]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT CODDCIMED AS 'CODIGO DCI', RTRIM(DESDCIMED) AS DESCRIPCION,RTRIM(CODDCIMED) +' - '+ RTRIM(DESDCIMED) AS DCI
       FROM IHDCIMEDI

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos registrados bajo su Denominación Común Internacional (DCI), que es el nombre genérico oficial del medicamento. Retorna el código DCI, la descripción del principio activo y una combinación de ambos para facilitar la selección en formularios o buscadores. Se usa para poblar catálogos o listas desplegables de medicamentos por nombre genérico en el módulo de inventario y prescripción médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarMedicamentosDCI';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_INV_ListarMedicamentosDCI';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el catálogo de medicamentos por Denominación Común Internacional (DCI) con su código y descripción concatenados para selección.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarMedicamentosDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La descripción y el código se devuelven sin espacios a la derecha (RTRIM).; El campo DCI se forma siempre como ''CODDCIMED - DESDCIMED''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarMedicamentosDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; Denominación Común Internacional (DCI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarMedicamentosDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] IHDCIMEDI: Devuelve todos los registros de IHDCIMEDI con código DCI, descripción y la concatenación ''código - descripción'' como DCI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarMedicamentosDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarMedicamentosDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_INV_ListarMedicamentosDCI';
-- GO
