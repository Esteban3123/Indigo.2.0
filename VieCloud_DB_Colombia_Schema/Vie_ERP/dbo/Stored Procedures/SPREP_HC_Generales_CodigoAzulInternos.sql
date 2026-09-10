
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_CodigoAzulInternos]
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
          
SELECT RTRIM(C.DESPRODUC) AS 'DESCRIPCION DEL PRODUCTO',RTRIM(A.JUSCODAZU) AS JUSTIFICACION,B.CANENTPRO AS 'CANTIDADUTILIZADA DEL PRODUCTO',
	   A.NUMCODAZU AS 'CONSECUTIVO DEL CODIGO AZUL'
FROM HCCODAZUI AS A WITH(NOLOCK)
INNER JOIN HCCODAZDI AS B WITH(NOLOCK) ON A.CODCONSEC = B.CODCONSEC 
INNER JOIN IHLISTPRO AS C WITH(NOLOCK) ON B.CODPRODUC = C.CODPRODUC 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio AND CANENTPRO >'0'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de insumos y medicamentos utilizados durante un evento de Código Azul (emergencia crítica intrahospitalaria) para un paciente, ingreso y folio específicos. Consulta las órdenes de Código Azul registradas en historia clínica, cruza con el detalle de productos despachados y obtiene la descripción de cada medicamento o insumo desde el catálogo maestro de productos. Devuelve la descripción del producto, la justificación clínica del evento, la cantidad utilizada y el número consecutivo del Código Azul, filtrando únicamente los productos que efectivamente fueron entregados. Se usa para respaldar la documentación clínica y auditoría de insumos consumidos en reanimaciones o emergencias internas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CodigoAzulInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CodigoAzulInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos utilizados (con justificación y cantidad) durante un evento de Código Azul para un paciente, ingreso y folio específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros de Código Azul (cabecera y detalle) asociados al paciente, ingreso y folio indicados; Los productos del detalle deben existir en el catálogo de productos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan ítems con cantidad entregada del producto mayor a cero (CANENTPRO > ''0''); Se usa NOLOCK en todas las lecturas, permitiendo lecturas sucias; El cruce cabecera-detalle se realiza por consecutivo del código azul', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Código Azul; Paciente; Ingreso hospitalario; Folio; Producto/insumo; Justificación de uso; Cantidad utilizada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCCODAZUI: Devuelve descripción del producto, justificación, cantidad utilizada y consecutivo del código azul cuando paciente, ingreso y folio coinciden y la cantidad entregada es mayor a 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCODAZUI; dbo.HCCODAZDI; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzulInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CodigoAzulInternos';
-- GO
