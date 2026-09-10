CREATE PROCEDURE [dbo].[SPREP_HC_Generales_MezclasyLiquidosMedicamentos]
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
          
SELECT CODCONCEC AS CONSECUTIVO, A.CODPRODUC AS 'CODIGO MEDICAMENTO MEZCLAS Y LIQUIDOS', DESPRODUC AS 'MEDICAMENTO MEZCLAS Y LIQUIDOS', CANPROCAL AS 'MEDICAMENTO MEZCLAS Y LIQUIDOS CANTIDAD',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI,RTRIM(D.DESDCIMED) AS 'DCI' 
FROM HCINFLIQD A with(nolock)
INNER JOIN IHLISTPRO B with(nolock) ON A.CODPRODUC=B.CODPRODUC
INNER JOIN IHDCIMEDI D WITH(NOLOCK) ON B.CODDCIMED = D.CODDCIMED 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio 
UNION ALL
SELECT CODCONCEC AS CONSECUTIVO, A.CODPRODUC AS 'CODIGO MEDICAMENTO MEZCLAS Y LIQUIDOS', DESPRODUC AS 'MEDICAMENTO MEZCLAS Y LIQUIDOS', CANPROCAL AS 'MEDICAMENTO MEZCLAS Y LIQUIDOS CANTIDAD',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI,RTRIM(D.DESDCIMED) AS 'DCI MEZCLAS' 
FROM HCINFCONC A with(nolock)
INNER JOIN IHLISTPRO B with(nolock) ON A.CODPRODUC=B.CODPRODUC 
INNER JOIN IHDCIMEDI D WITH(NOLOCK) ON B.CODDCIMED = D.CODDCIMED 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de medicamentos en mezclas y líquidos registrados en la historia clínica de un paciente para un ingreso y folio específicos. Combina dos fuentes de información clínica: los líquidos administrados (HCINFLIQD) y las concentraciones de preparaciones magistrales (HCINFCONC), unificando ambos conjuntos mediante UNION ALL. Para cada medicamento retorna el consecutivo, el código y nombre del producto farmacéutico (desde el catálogo IHLISTPRO), la cantidad calculada y la denominación común internacional DCI (desde IHDCIMEDI). Se utiliza en la visualización del módulo de historia clínica para consultar todas las mezclas y líquidos medicamentosos aplicados a un paciente durante su ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidosMedicamentos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidosMedicamentos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta consolidada de medicamentos en mezclas y líquidos administrados a un paciente en un ingreso y folio clínico específicos, incluyendo su denominación común internacional (DCI).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, ingreso y folio deben existir en las tablas de historia clínica de líquidos y/o concentrados; Los productos referenciados deben existir en el maestro de productos (IHLISTPRO) y tener una DCI asociada en IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna medicamentos cuyo producto tenga DCI asociada (INNER JOIN con IHDCIMEDI); Solo retorna registros que coincidan simultáneamente en paciente, número de ingreso y número de folio; Permite duplicados entre líquidos y concentrados al usar UNION ALL (no elimina repetidos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; folio de historia clínica; medicamentos; mezclas y líquidos; DCI (Denominación Común Internacional); centro de atención; unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCINFLIQD + HCINFCONC: Devuelve UNION ALL de los medicamentos registrados como líquidos (HCINFLIQD) y como concentrados/mezclas (HCINFCONC) filtrados por paciente, ingreso y folio, enriquecidos con descripción de producto y DCI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIQD; dbo.HCINFCONC; dbo.IHLISTPRO; dbo.IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentos';
-- GO
