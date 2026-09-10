
CREATE Procedure [dbo].[SPMOV_ListarPacientesUnidad]
(
@CentroAtencion Char(15),
@UnidadFuncional Char(10)
)
AS
Select 
	RTRIM(A.IPCODPACI) as IPCODPACI,
	RTRIM(B.IPNOMCOMP) as IPNOMCOMP,
	RTRIM(A.NUMINGRES) as NUMINGRES,
	RTRIM(A.NUMEFOLIO) as NUMEFOLIO,
	Case A.ESTAFOLIO
		when 1 then 'Activo'
		when 0 then 'Inactivo'
	end as 'ESTAFOLIO',
	B.IPGRUPSAN as 'GRUSANGUI',
	B.IPRHSANGR AS 'IPRHSANGR',
	RTRIM(A.CODCENATE) as CODCENATE,
	RTRIM(A.UFUCODIGO) as UFUCODIGO,
	RTRIM(C.NOMDIAGNO) as 'DIAGNOSTI',
	DATEDIFF(DAY,FECHISPAC, [Common].[GETDATE]()) AS 'DIASHOSPIT',
	RTRIM(D.DESESPECI) as 'ESPECIALI',
	E.CODCAMACT as 'CODCAMACT'
from HCHISPACA A
inner join INPACIENT B on B.IPCODPACI = A.IPCODPACI
inner join INESPECIA D on D.CODESPECI = A.CODESPTRA
inner join ADINGRESO E on e.NUMINGREI = A.NUMINGRES
LEFT join INDIAGNOS C on C.CODDIAGNO =   (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = B.IPCODPACI AND NUMINGRES = E.NUMINGRES AND CODDIAPRI = 1)
WHERE A.CODCENATE = @CentroAtencion and A.UFUCODIGO = @UnidadFuncional
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes actualmente ubicados en una unidad funcional (cama, sala o servicio) dentro de un centro de atención específico. Para cada paciente retorna su cédula, nombre completo, número de ingreso, número de folio de historia clínica, estado del folio (activo/inactivo), grupo sanguíneo y RH, diagnóstico principal CIE-10 del ingreso, especialidad médica tratante, código de cama actual y los días transcurridos desde el ingreso (días de hospitalización). Compone información de historia clínica (HCHISPACA), datos demográficos del paciente (INPACIENT), especialidad médica (INESPECIA), admisión o ingreso (ADINGRESO) y diagnóstico principal del episodio (INDIAGNOP/INDIAGNOS). Es utilizado típicamente en el censo de pacientes hospitalizados o en urgencias para el control de ocupación por unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarPacientesUnidad';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarPacientesUnidad';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes hospitalizados activos en una unidad funcional de un centro de atención, con datos clínicos básicos (diagnóstico principal, especialidad, días de hospitalización y cama).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarPacientesUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el centro de atención y la unidad funcional consultados con registros en la hoja de hospitalización; Las tablas maestras de paciente, especialidad e ingreso deben tener correspondencia con la hoja de hospitalización (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarPacientesUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna el diagnóstico marcado como principal (CODDIAPRI = 1) y únicamente el primero (TOP 1) por paciente/ingreso; Los días de hospitalización se calculan como diferencia en días entre FECHISPAC y la fecha actual del sistema vía Common.GETDATE(); Pacientes sin diagnóstico principal registrado igualmente se listan (LEFT JOIN sobre INDIAGNOS); Solo se incluyen pacientes con ingreso, especialidad tratante y datos maestros existentes (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarPacientesUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Hospitalización; Centro de atención; Unidad funcional; Folio; Ingreso; Diagnóstico principal; Especialidad tratante; Cama; Grupo sanguíneo; Factor RH; Días de hospitalización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarPacientesUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHISPACA: Devuelve filas filtrando por CODCENATE y UFUCODIGO recibidos como parámetros', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarPacientesUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTAFOLIO = 1 → Se retorna como ''Activo'' else Si ESTAFOLIO = 0 se retorna como ''Inactivo''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarPacientesUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarPacientesUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INESPECIA; dbo.ADINGRESO; dbo.INDIAGNOS; dbo.INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarPacientesUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarPacientesUnidad';
-- GO
