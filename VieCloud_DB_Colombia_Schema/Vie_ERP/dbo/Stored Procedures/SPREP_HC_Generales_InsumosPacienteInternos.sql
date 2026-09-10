
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_InsumosPacienteInternos]
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
          
SELECT RTRIM(B.CODPRODUC) AS 'CODIGO DEL PRODUCTO',CANPEDPRO AS 'CANTIDAD PEDIDA DEL PRODUCTO',RTRIM(DESPRODUC) AS 'DESCRIPCION DEL PRODUCTO',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
                   
FROM HCSOLINCI A   with(noLock)
INNER JOIN HCSOLINDI B  with(noLock) ON A.CODCONCEC=B.CODCONCEC 
INNER JOIN IHLISTPRO C  with(noLock) ON B.CODPRODUC=C.CODPRODUC
              
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los insumos y medicamentos solicitados para un paciente hospitalizado (interno), combinando las órdenes de solicitud de insumos de la historia clínica con el catálogo maestro de productos farmacéuticos y dispositivos médicos. Recibe como parámetros la cédula del paciente, el número de ingreso hospitalario y el número de folio, y devuelve el código del producto, la cantidad pedida y la descripción de cada insumo, junto con el centro de atención y la unidad funcional. Se usa para visualizar en el reporte de historia clínica qué insumos o medicamentos fueron requeridos durante un ingreso específico de un paciente internado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_InsumosPacienteInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_InsumosPacienteInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los insumos/productos solicitados para un paciente internado, identificados por su folio e ingreso, mostrando código, cantidad pedida y descripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir solicitud de insumos en HCSOLINCI con coincidencia exacta de paciente, número de ingreso y folio; Cada detalle (HCSOLINDI) debe tener un producto válido en el maestro IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan productos cuyo código exista en el maestro IHLISTPRO (INNER JOIN filtra huérfanos); Solo se devuelven detalles cuya cabecera coincida en CODCONCEC (INNER JOIN cabecera-detalle); Lecturas con NOLOCK: tolera lecturas sucias en favor de no bloquear', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente internado; Ingreso hospitalario; Folio de solicitud; Insumos/productos médicos; Solicitud de insumos clínicos; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCSOLINCI: Cuando A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio, retorna los insumos asociados uniendo cabecera, detalle y maestro de productos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCSOLINCI; dbo.HCSOLINDI; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPacienteInternos';
-- GO
