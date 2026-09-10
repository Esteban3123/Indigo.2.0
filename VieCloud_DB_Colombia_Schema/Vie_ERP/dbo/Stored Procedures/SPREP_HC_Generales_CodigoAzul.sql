
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_CodigoAzul]
(
@CodigoPaciente Varchar(25),
@NumeroFolio Char(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT RTRIM(C.DESPRODUC) AS 'DESCRIPCION DEL PRODUCTO',RTRIM(A.JUSCODAZU) AS JUSTIFICACION,B.CANENTPRO AS 'CANTIDAD UTILIZADA DEL PRODUCTO',
	   A.NUMCODAZU AS 'CONSECUTIVO DEL CODIGO AZUL'
FROM HCCODAZUC AS A With(Nolock)
INNER JOIN HCCODAZUD AS B With(Nolock) ON A.CODCONSEC = B.CODCONSEC 
INNER JOIN IHLISTPRO AS C With(Nolock) ON B.CODPRODUC = C.CODPRODUC 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND CANENTPRO >'0'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de órdenes de Código Azul (alertas críticas de emergencia) asociadas a un paciente específico durante un ingreso hospitalario. Combina las órdenes emitidas (cabecera con justificación clínica y consecutivo), los productos o insumos efectivamente entregados en cada orden, y el catálogo maestro de productos para mostrar la descripción comercial de cada insumo utilizado. Recibe como parámetros la cédula del paciente, el número de ingreso y el número de folio, y devuelve únicamente los productos que tuvieron cantidad entregada mayor a cero, permitiendo auditar qué insumos o medicamentos se consumieron durante una emergencia o evento crítico de código azul.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CodigoAzul';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CodigoAzul';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos utilizados durante un evento de Código Azul (con su justificación, cantidad y consecutivo) para un paciente, ingreso y folio determinados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una cabecera de Código Azul (HCCODAZUC) que coincida con el paciente, ingreso y folio indicados.; El detalle (HCCODAZUD) debe estar enlazado a la cabecera por CODCONSEC.; Los productos del detalle deben estar registrados en el maestro de productos (IHLISTPRO).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan productos con cantidad entregada mayor a cero (CANENTPRO > ''0'').; El cruce entre cabecera y detalle de Código Azul se realiza por el consecutivo CODCONSEC.; Cada producto del detalle debe existir en el maestro de productos (IHLISTPRO) para ser reportado.; El reporte queda restringido al paciente, ingreso y folio especificados (combinación obligatoria de los tres).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Código Azul; Paciente; Ingreso; Folio de atención; Productos/insumos utilizados; Justificación clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCCODAZUC: Cuando existe cabecera de Código Azul para el paciente/ingreso/folio dado y el detalle tiene CANENTPRO > ''0'', se retorna la descripción del producto, justificación, cantidad utilizada y consecutivo del código azul.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCODAZUC; dbo.HCCODAZUD; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzul';
-- GO
