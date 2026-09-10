

CREATE PROCEDURE [dbo].[SP_HC_ListarProductosControlPaciente]
(
@Paciente varchar(25),
@Ingreso char(10),
@Folio char(10)
)
AS
BEGIN
 SET NOCOUNT ON;
 SELECT RTRIM(A.CODPRODUC) AS CODPRODUC
 FROM dbo.HCPRESCRD A 
	INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC
 WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND NUMEFOLIO=@Folio AND INCONSECUCONTROL is not null -- PROCONTRO='1'
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos bajo control (controlados o de seguimiento especial) que fueron prescritos a un paciente en un ingreso y folio de historia clínica específicos. Consulta las prescripciones registradas en la historia clínica (HCPRESCRD) filtrando únicamente aquellas líneas que tienen un número de consecutivo de control asignado, y cruza con el catálogo maestro de productos farmacéuticos (IHLISTPRO) para validar que el producto exista. Recibe como parámetros la cédula del paciente, el número de ingreso y el número de folio, devolviendo los códigos de los productos controlados prescritos en ese episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosControlPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosControlPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener los códigos de productos prescritos a un paciente en un ingreso y folio específicos que están sujetos a control de medicamentos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosControlPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere identificar al paciente, su ingreso y el folio de la atención para acotar la consulta.; Las prescripciones deben estar registradas en HCPRESCRD y los productos deben existir en el catálogo IHLISTPRO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosControlPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan productos prescritos que existen en el catálogo maestro de productos (INNER JOIN con IHLISTPRO).; Solo se consideran prescripciones marcadas como sujetas a control, identificadas por tener INCONSECUCONTROL no nulo.; El código del producto se devuelve sin espacios a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosControlPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso; folio; prescripción; producto farmacéutico; control de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosControlPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPRESCRD: Cuando IPCODPACI=@Paciente, NUMINGRES=@Ingreso, NUMEFOLIO=@Folio y INCONSECUCONTROL no es nulo, retorna el listado de códigos de producto (CODPRODUC) prescritos bajo control.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosControlPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRD; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosControlPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosControlPaciente';
-- GO
