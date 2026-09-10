
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_MezclasyLiquidosMedicamentosInternos]
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
          
SELECT CODCONCEC AS CONSECUTIVO, A.CODPRODUC AS 'CODIGO MEDICAMENTO MEZCLAS Y LIQUIDOS', DESPRODUC AS 'MEDICAMENTO MEZCLAS Y LIQUIDOS', CANPROCAL AS 'MEDICAMENTO MEZCLAS Y LIQUIDOS CANTIDAD',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
                   
FROM HCINFLIDI A WITH(NOLOCK)INNER JOIN IHLISTPRO B WITH(NOLOCK) ON A.CODPRODUC=B.CODPRODUC
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio
UNION 
SELECT CODCONCEC AS CONSECUTIVO, A.CODPRODUC AS 'CODIGO MEDICAMENTO MEZCLAS Y LIQUIDOS', DESPRODUC AS 'MEDICAMENTO MEZCLAS Y LIQUIDOS', CANPROCAL AS 'MEDICAMENTO MEZCLAS Y LIQUIDOS CANTIDAD',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCINFCONI A WITH(NOLOCK) INNER JOIN IHLISTPRO B WITH(NOLOCK) ON A.CODPRODUC=B.CODPRODUC 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el listado de medicamentos en mezclas y líquidos de infusión prescritos a un paciente durante un ingreso hospitalario específico, identificado por su cédula, número de ingreso y número de folio de historia clínica. Combina los registros de líquidos de infusión (HCINFLIDI) y los componentes de infusión continua (HCINFCONI), enriqueciendo cada ítem con el nombre y descripción del medicamento desde el catálogo de productos farmacéuticos (IHLISTPRO). Se utiliza para imprimir o visualizar en reportes clínicos las mezclas intravenosas, diluyentes y bolos ordenados al paciente dentro de su historia clínica de hospitalización o urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidosMedicamentosInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidosMedicamentosInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los medicamentos tipo mezclas y líquidos administrados a un paciente en una atención específica, combinando registros de infusiones líquidas e infusiones continuas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente, ingreso y folio indicados con registros en HCINFLIDI o HCINFCONI; Los códigos de producto referenciados deben existir en IHLISTPRO para obtener la descripción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan medicamentos cuyo CODPRODUC exista en IHLISTPRO (INNER JOIN); El resultado siempre se filtra simultáneamente por paciente, ingreso y folio; Se usa UNION (no UNION ALL), por lo que filas idénticas entre ambas fuentes se consolidan en una sola; Lecturas con NOLOCK: puede leer datos no confirmados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de historia clínica; Medicamentos de mezclas y líquidos; Infusiones continuas; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCINFLIDI: Cuando IPCODPACI, NUMINGRES y NUMEFOLIO coinciden con los parámetros, devuelve los medicamentos de mezclas/líquidos con su consecutivo, código, descripción y cantidad; [RETURN_RESULT] HCINFCONI: Cuando IPCODPACI, NUMINGRES y NUMEFOLIO coinciden con los parámetros, devuelve los medicamentos de infusión continua unidos al resultado anterior eliminando duplicados (UNION)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIDI; dbo.HCINFCONI; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosMedicamentosInternos';
-- GO
