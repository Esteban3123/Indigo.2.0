

CREATE PROCEDURE [dbo].[SPREP_HC_Generales_MedicamentosSuspendidosInternos]
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
          
SELECT RTRIM(B.DESPRODUC) AS 'DESCRIPCION DEL PRODUCTO',CASE WHEN FORMAPRESCRIBE IS NOT NULL THEN DESADMINI ELSE CASE WHEN DOSISPRFN IS NULL THEN DESADMINI WHEN FRECUENCI=0 THEN RTRIM(DOSISPRFN) + ' ' + RTRIM(ABRUNIMED) + ' ' + RTRIM(A.CODVIAADM) + ' (' + RTRIM(DURACIDOS) + ')' ELSE RTRIM(DOSISPRFN) + ' ' + RTRIM(ABRUNIMED) + ' Cada ' + RTRIM(CAST(FRECUENCI AS CHAR)) + 'H ' + RTRIM(A.CODVIAADM) + ' (' + RTRIM(DURACIDOS) + ')' END END AS ADMINISTRACION,RTRIM(MOTSUSMED) AS 'MOTIVO DE SUSPENSION',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
                   
FROM HCPRESCRI A WITH(NOLOCK)
INNER JOIN IHLISTPRO B WITH(NOLOCK) ON A.CODPRODUC=B.CODPRODUC 
LEFT OUTER JOIN INUNIMEDI C WITH(NOLOCK) ON A.CODUNIMFN=C.CODUNIMED
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMFOLSUS=@NumeroFolio AND PREESTADO='4' AND IDETIPHIS<>'CODIGOAZU'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de medicamentos suspendidos en hospitalización interna para un paciente específico. Dado el código del paciente, el número de ingreso y el número de folio, consulta las prescripciones médicas (HCPRESCRI) con estado de suspensión (PREESTADO=''4''), cruzando con el catálogo de productos farmacéuticos (IHLISTPRO) para obtener el nombre del medicamento y con las unidades de medida (INUNIMEDI) para construir la descripción completa de la dosis administrada (cantidad, unidad, vía de administración, frecuencia y duración). Devuelve la descripción del medicamento, la pauta de administración compuesta y el motivo de suspensión, siendo útil para la revisión clínica de la historia de medicación suspendida durante un ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MedicamentosSuspendidosInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MedicamentosSuspendidosInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los medicamentos suspendidos de un paciente hospitalizado para un ingreso y folio de suspensión específicos, mostrando producto, esquema de administración y motivo de suspensión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en HCPRESCRI con PREESTADO=''4'' para el paciente, ingreso y folio de suspensión indicados; Cada prescripción debe tener producto asociado en IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran prescripciones en estado ''4'' (suspendido); Se excluyen prescripciones con IDETIPHIS = ''CODIGOAZU''; El listado se restringe al paciente, ingreso y folio de suspensión indicados; Las consultas usan WITH(NOLOCK), aceptando lecturas sucias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de suspensión; Prescripción médica; Medicamento suspendido; Dosis; Frecuencia de administración; Vía de administración; Motivo de suspensión; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPRESCRI: Cuando PREESTADO=''4'' y IDETIPHIS<>''CODIGOAZU'' y coinciden paciente/ingreso/folio de suspensión, devuelve descripción del producto, cadena de administración compuesta, motivo de suspensión, centro de atención, unidad funcional y código de paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FORMAPRESCRIBE IS NOT NULL → Se reporta la descripción de administración (DESADMINI) sin componer dosis/frecuencia/vía else Se evalúa la dosis prescrita: si DOSISPRFN es NULL se usa DESADMINI; si FRECUENCI=0 se arma ''dosis + unidad + vía + (duración)''; en otro caso se arma ''dosis + unidad + Cada NH + vía + (duración)''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRI; dbo.IHLISTPRO; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidosInternos';
-- GO
