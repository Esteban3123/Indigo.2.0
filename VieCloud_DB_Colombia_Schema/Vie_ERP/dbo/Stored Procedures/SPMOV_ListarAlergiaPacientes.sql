CREATE Procedure [dbo].[SPMOV_ListarAlergiaPacientes]
(
 @Paciente varchar(25),
 @Ingreso varchar(50)
)
AS
Select
RTRIM(B.CODPRODUC) as 'CodigoProducto',
RTRIM(B.DESPRODUC) as 'Descripcion',
RTRIM(A.MOTSUSMED) as 'Observacion'
from HCMEDRIES A
inner join IHLISTPRO B on B.CODPRODUC = A.CODPRODUC
where A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las alergias y medicamentos suspendidos registrados para un paciente en un ingreso específico. Cruza el historial de riesgos médicos del paciente (HCMEDRIES) con el catálogo de productos farmacéuticos (IHLISTPRO) para devolver el código del medicamento, su descripción comercial y el motivo de suspensión o alerta. Se usa en la historia clínica para informar al equipo asistencial sobre qué fármacos representan un riesgo para el paciente, evitando prescripciones peligrosas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarAlergiaPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarAlergiaPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos/medicamentos identificados como alergias o riesgos médicos asociados a un paciente en un ingreso específico, junto con su observación de motivo de suspensión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAlergiaPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir en la tabla de riesgos médicos (HCMEDRIES); Los productos referenciados deben existir en el catálogo IHLISTPRO para resolver descripción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAlergiaPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros que tengan correspondencia (INNER JOIN) entre el riesgo médico y el catálogo de productos; El resultado se filtra siempre por la combinación paciente+ingreso (no expone datos de otros ingresos); Los valores de código y descripción se devuelven sin espacios a la derecha (RTRIM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAlergiaPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Alergia; Medicamento/Producto; Motivo de suspensión médica; Riesgo médico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAlergiaPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCMEDRIES: Cuando IPCODPACI = @Paciente y NUMINGRES = @Ingreso, retorna código de producto, descripción y motivo de suspensión médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAlergiaPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCMEDRIES; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAlergiaPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAlergiaPacientes';
-- GO
