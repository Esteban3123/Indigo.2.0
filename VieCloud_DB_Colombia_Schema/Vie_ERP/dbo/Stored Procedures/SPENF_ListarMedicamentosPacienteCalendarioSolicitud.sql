

CREATE PROCEDURE [dbo].[SPENF_ListarMedicamentosPacienteCalendarioSolicitud]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT convert(varchar(20),A.CODCONCEC) as CODCONCEC, RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,'Orden Medica' AS Tipo 
FROM dbo.HCPRESCRA A 
INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND TIPPRODUC IN ('1','3') 
AND PROESTADO = 1 AND ESPDILPRO='0' AND MANEXTPRO='0' AND PREESTADO IN ('1','6') 
UNION 
SELECT DISTINCT '' as CODCONCEC, RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,'Emergencia' AS Tipo 
FROM dbo.HCCODAZUC A
INNER JOIN dbo.HCCODAZUD B ON A.CODCONSEC=B.CODCONSEC
INNER JOIN dbo.IHLISTPRO C ON B.CODPRODUC=C.CODPRODUC 
WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND C.TIPPRODUC IN ('1','3') 

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos e insumos activos asociados a un paciente en un ingreso hospitalario específico, combinando dos fuentes: las órdenes médicas de prescripción (recetas activas o dispensadas de medicamentos y soluciones, excluyendo productos de manejo externo o dilución especial) y las solicitudes de emergencia tipo código azul. Para cada medicamento devuelve el código del producto, su nombre comercial y si proviene de una orden médica normal o de una solicitud de emergencia. Se usa en el módulo de enfermería para armar el calendario o programación de administración de medicamentos del paciente durante su hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPENF_ListarMedicamentosPacienteCalendarioSolicitud';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPENF_ListarMedicamentosPacienteCalendarioSolicitud';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos asociados a un paciente en un ingreso, combinando prescripciones de orden médica vigentes y los registrados en órdenes de código azul (emergencia).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitud';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir y ser provistos como entrada.; Los productos deben existir en el catálogo IHLISTPRO para poder ser listados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitud';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen productos cuyo TIPPRODUC sea ''1'' o ''3'' (medicamentos/insumos válidos).; Las prescripciones de orden médica solo se incluyen si están activas (PROESTADO=1) y en estados PREESTADO ''1'' o ''6''.; Se excluyen productos marcados como dilución especial (ESPDILPRO<>''0'') o de manejo extra (MANEXTPRO<>''0'') en la rama de orden médica.; Las filas provenientes de código azul no exponen CODCONCEC (se devuelve cadena vacía).; El UNION elimina duplicados entre ambas fuentes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitud';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Medicamento; Prescripción/Orden médica; Código azul (emergencia); Catálogo de productos farmacéuticos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitud';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve los productos tipo ''1'' o ''3'' prescritos al paciente/ingreso con PROESTADO=1, ESPDILPRO=''0'', MANEXTPRO=''0'' y PREESTADO IN (''1'',''6''), etiquetados como ''Orden Medica''.; [RETURN_RESULT] RESULTSET: Devuelve, en UNION, los productos tipo ''1'' o ''3'' asociados a órdenes de código azul (HCCODAZUC/HCCODAZUD) del paciente/ingreso, etiquetados como ''Emergencia'', sin CODCONCEC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitud';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.IHLISTPRO; dbo.HCCODAZUC; dbo.HCCODAZUD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitud';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitud';
-- GO
