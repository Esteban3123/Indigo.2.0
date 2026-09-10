-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarCicloEsquemasModificados]
(
@CentroAtencion char(20),
@UnidadFuncional char(20),
@FechaInicial datetime,
@FechaFinal datetime
)
AS
BEGIN
    SET NOCOUNT ON

	
SELECT   S.FECHAREGISTRO as 'fechaModificacion', S.IPCODPACI as 'identificacion', RTRIM(I.IPNOMCOMP) as 'paciente',
		[dbo].[Edad](convert(date,IPFECNACI),convert(date,getdate())) as 'edad' , 
		Rtrim(b.NOMENTIDA) as 'entidad', S.NUMINGRES as 'ingreso', RTRIM(sch.Code) +'-'+ RTRIM(sch.Description) as 'nombreEsquema',
		S.CICLO as 'ciclo', RTRIM(diag.CODDIAGNO)+'-'+ RTRIM(diag.NOMDIAGNO) as 'diagnostico', Rtrim(G.CODMOTANU)+'-'+ Rtrim(G.DESMOTANU) as 'motivoModificacion',
		Rtrim(S.OBSERVACIONMOD) as 'observacion', RTRIM(K.NOMMEDICO) as 'medico', rtrim(p.DESESPECI) as 'especialidad',
		CASE I.IPSEXOPAC WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END  AS 'genero'
FROM EHR.HCORDCICLOS S
	inner join EHR.HCORDQUIMIO c on S.IDHCORDQUIMIO = c.ID 
	inner join EHR.Schemes sch on sch.id = c.SchemesId
	inner join dbo.INDIAGNOS diag on diag.CODDIAGNO = c.CODDIAGNO
	inner join dbo.HCMOANULB G on G.CODMOTANU = S.IDHCMOANULB
	inner join dbo.INPROFSAL K on K.CODPROSAL= S.CODPROSAL
	inner join dbo.INESPECIA p on p.CODESPECI= S.CODESPECI
	inner join dbo.INPACIENT I on I.IPCODPACI  = S.IPCODPACI 
	inner join dbo.ADINGRESO H on H.NUMINGRES = S.NUMINGRES 
	inner join dbo.INENTIDAD b on b.CODENTIDA = H.CODENTIDA
WHERE (s.IDHCMOANULB IS NOT NULL  AND s.OBSERVACIONMOD IS NOT NULL) 
	   and S.CODCENATE = @CentroAtencion AND S.UFUCODIGO = @UnidadFuncional AND S.FECHAREGISTRO between @FechaInicial AND @FechaFinal 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los ciclos de esquemas de quimioterapia que fueron modificados (con motivo de anulación y observación registrados) dentro de un rango de fechas, para un centro de atención y unidad funcional específicos. Compone información del ciclo oncológico (esquema, número de ciclo, diagnóstico CIE-10) con datos del paciente (cédula, nombre, edad, sexo, entidad aseguradora), el médico y su especialidad, el número de ingreso y el motivo de modificación con su observación. Sirve para auditoría y seguimiento clínico-administrativo de tratamientos oncológicos cuyos ciclos fueron alterados o anulados, permitiendo identificar quién modificó, por qué y a qué paciente afectó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarCicloEsquemasModificados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarCicloEsquemasModificados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ciclos de esquemas de quimioterapia que han sido modificados (con motivo y observación de modificación) en un centro de atención y unidad funcional dentro de un rango de fechas, enriqueciendo con datos del paciente, médico, diagnóstico y entidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCicloEsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ciclo debe tener registrado un motivo de modificación/anulación (IDHCMOANULB no nulo); El ciclo debe tener una observación de modificación (OBSERVACIONMOD no nulo); Deben existir relaciones íntegras entre ciclo, orden de quimio, esquema, diagnóstico, motivo, profesional, especialidad, paciente, ingreso y entidad (joins internos); La fecha de registro del ciclo debe estar dentro del rango solicitado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCicloEsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven ciclos que tengan simultáneamente motivo de modificación y observación registrada; El filtro siempre está acotado por centro de atención, unidad funcional y rango de fechas de registro; La edad se calcula dinámicamente con la fecha actual usando la función dbo.Edad sobre la fecha de nacimiento; El nombre del esquema, diagnóstico y motivo se presentan concatenados como ''código-descripción''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCicloEsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ciclo de quimioterapia; Esquema de tratamiento oncológico; Modificación de ciclo; Motivo de anulación/modificación; Diagnóstico (CIE); Paciente; Ingreso/Admisión; Entidad pagadora; Profesional de la salud; Especialidad médica; Centro de atención; Unidad funcional; Edad; Género del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCicloEsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] EHR.HCORDCICLOS: Cuando IDHCMOANULB IS NOT NULL y OBSERVACIONMOD IS NOT NULL y coincide centro de atención, unidad funcional y rango de fechas, retorna la lista de ciclos modificados con datos clínicos y administrativos asociados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCicloEsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = 1 → Género se reporta como ''Masculino'' else Si IPSEXOPAC = 2 se reporta ''Femenino''; cualquier otro valor queda nulo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCicloEsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCicloEsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDCICLOS; EHR.HCORDQUIMIO; EHR.Schemes; dbo.INDIAGNOS; dbo.HCMOANULB; dbo.INPROFSAL; dbo.INESPECIA; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCicloEsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCicloEsquemasModificados';
-- GO
