CREATE PROCEDURE [dbo].[SPREP_HC_Generales_MedicamentosSuspendidos]
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
                   
FROM HCPRESCRA A  with(noLock)
INNER JOIN IHLISTPRO B  with(noLock) ON A.CODPRODUC=B.CODPRODUC 
LEFT OUTER JOIN INUNIMEDI C  with(noLock) ON A.CODUNIMFN=C.CODUNIMED
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMFOLSUS=@NumeroFolio AND PREESTADO='4' AND IDETIPHIS<>'CODIGOAZU' AND A.IDHCORDQUIMIO IS NULL 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de medicamentos suspendidos en la historia clínica de un paciente hospitalizado. Dado el código del paciente (cédula), el número de ingreso y el número de folio, devuelve el listado de prescripciones que fueron suspendidas (estado ''4''), excluyendo órdenes de quimioterapia y registros de tipo historia clínica especial. Para cada medicamento suspendido muestra el nombre del producto (tomado del catálogo farmacéutico IHLISTPRO), el esquema de administración compuesto (dosis, unidad de medida, vía de administración y duración), y el motivo de suspensión registrado por el profesional de salud. Se utiliza típicamente para imprimir o visualizar en la historia clínica los fármacos que fueron retirados del tratamiento del paciente durante su estancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MedicamentosSuspendidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MedicamentosSuspendidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos suspendidos de un paciente en un ingreso y folio determinados, mostrando descripción del producto, esquema de administración y motivo de suspensión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, ingreso y folio de suspensión deben existir en HCPRESCRA; Las prescripciones deben estar en estado ''4'' (suspendido)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye prescripciones cuyo tipo de historia sea ''CODIGOAZU''; Excluye prescripciones asociadas a órdenes de quimioterapia (IDHCORDQUIMIO IS NULL); Solo retorna prescripciones efectivamente suspendidas (PREESTADO=''4''); Asocia el folio de suspensión NUMFOLSUS al parámetro de folio recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Prescripción médica; Medicamento suspendido; Motivo de suspensión; Dosis; Frecuencia de administración; Vía de administración; Quimioterapia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCPRESCRA: Devuelve prescripciones cuando IPCODPACI=@paciente, NUMINGRES=@ingreso, NUMFOLSUS=@folio, PREESTADO=''4'', IDETIPHIS<>''CODIGOAZU'' y IDHCORDQUIMIO IS NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FORMAPRESCRIBE IS NOT NULL → Muestra DESADMINI como administración else Evalúa la dosis prescrita; si FORMAPRESCRIBE IS NULL y DOSISPRFN IS NULL → Muestra DESADMINI como administración; si FORMAPRESCRIBE IS NULL, DOSISPRFN no nula y FRECUENCI=0 → Construye administración como ''dosis unidad vía (duración)'' sin frecuencia; si FORMAPRESCRIBE IS NULL, DOSISPRFN no nula y FRECUENCI<>0 → Construye administración como ''dosis unidad Cada NH vía (duración)''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.IHLISTPRO; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosSuspendidos';
-- GO
