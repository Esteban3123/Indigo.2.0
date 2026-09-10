CREATE PROCEDURE [dbo].[SPMOV_ListarAdministracionesMedicamentosPacientes]
(
@Paciente Varchar(25),
@Ingreso Char(10),
@Medicamento Char(25)
)
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
		RTRIM(C.UFUDESCRI) AS UnidadFuncional,
		RTRIM(D.DESPRODUC) AS NombreMedicamento,
		RTRIM(D.CODPRODUC) AS CodigoMedicamento,
		RTRIM(E.NOMMEDICO) AS NombreMedico,
		RTRIM(B.DESVIAADM) AS ViaAdministracion,
		dbo.ObtenerFechaFormateada(	CASE 
			WHEN A.FECAPLMED IS NULL THEN A.FECPROAPL ELSE A.FECAPLMED end ) as 'FechaFormateada',
		CASE 
			WHEN A.FECAPLMED IS NULL THEN A.FECPROAPL ELSE A.FECAPLMED 
		END AS Fecha,
		CASE 
			WHEN A.DOSISPROD IS NULL THEN DESADMINI ELSE RTRIM(A.DOSISPROD) 
		END AS Dosis,
		RTRIM(ABRUNIMED) AS UnidadMedida,
		CASE A.MEDESTADO WHEN '1' THEN 'Pendiente Aplicacion'  WHEN '2' THEN 'Aplicado' WHEN '3' THEN 'No Aplicado' END AS Estado, 
		RTRIM(G.NOMCENATE) AS CentroAtencion,
		[dbo].[ObtenerFechaFormateada](CASE WHEN A.FECAPLMED IS NULL THEN A.FECPROAPL ELSE A.FECAPLMED END) AS FechaFormateada,
		Case 
			WHEN A.MEDESTADO = '1' or A.MEDESTADO = '3'
			THEN CASE
					when (CASE WHEN A.FECAPLMED IS NULL THEN A.FECPROAPL ELSE A.FECAPLMED END) < [Common].[GETDATE]()
					then 'True'
					else 'False'
				END
			ELSE 'False'
		END as Alerta
	FROM HCHOJAMED AS A 
			INNER JOIN HCVIAADMI AS B ON A.CODVIAADM = B.CODVIAADM 
			INNER JOIN ADCENATEN AS G ON A.CODCENATE = G.CODCENATE 
			LEFT OUTER JOIN INUNIFUNC AS C ON A.UFUCODIGO = C.UFUCODIGO 
			LEFT OUTER JOIN INUNIMEDI AS F ON A.CODUNIMED = F.CODUNIMED 
			LEFT OUTER JOIN IHLISTPRO AS D ON A.CODPRODUC = D.CODPRODUC 
			LEFT OUTER JOIN INPROFSAL AS E ON A.CODPROAPL = E.CODPROSAL 
			LEFT OUTER JOIN CHTIPESTA AS H ON A.CODTIPEST = H.CODTIPEST
	WHERE A.MEDESTADO IN ('1','2','3') and  A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso AND A.CODPRODUC = @Medicamento
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial de administraciones de un medicamento específico para un paciente y un ingreso (hospitalización) determinados. Consolida información de la hoja de medicamentos (HCHOJAMED) con el catálogo de productos farmacéuticos, vías de administración, unidades funcionales, unidades de medida, profesional que aplicó el medicamento y centro de atención. Para cada administración muestra el nombre y código del medicamento, la dosis aplicada o programada, la vía de administración (oral, intravenosa, etc.), el profesional de salud que la ejecutó, la unidad funcional o servicio, la fecha de aplicación o programación formateada, y el estado (Pendiente Aplicación, Aplicado, No Aplicado), incluyendo una alerta cuando hay administraciones pendientes o no aplicadas con fecha vencida. Se usa en la visualización clínica del seguimiento farmacológico del paciente durante su estancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las administraciones de un medicamento específico para un paciente en un ingreso, mostrando estado, dosis, vía, profesional, fecha formateada y alerta de retraso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, ingreso y medicamento deben existir en HCHOJAMED; Solo se consideran registros con estado de administración 1 (Pendiente), 2 (Aplicado) o 3 (No Aplicado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan administraciones cuyo estado pertenece al dominio {1,2,3}; La alerta de retraso solo aplica a administraciones pendientes o no aplicadas, nunca a las ya aplicadas; Siempre se prioriza la fecha real de aplicación sobre la programada cuando existe', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Medicamento; Administración de medicamento; Vía de administración; Dosis; Unidad de medida; Unidad funcional; Centro de atención; Profesional de salud; Hoja de medicación; Alerta de aplicación vencida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve administraciones de medicamentos filtradas por paciente, ingreso y producto, con estado mapeado a texto y bandera Alerta cuando la administración está pendiente/no aplicada y la fecha programada/aplicación es anterior a la fecha actual', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FECAPLMED IS NULL → Se usa FECPROAPL (fecha programada) como fecha de referencia else Se usa FECAPLMED (fecha real de aplicación); si DOSISPROD IS NULL → Se muestra DESADMINI como dosis else Se muestra DOSISPROD; si MEDESTADO = ''1'' → Estado = ''Pendiente Aplicacion''; si MEDESTADO = ''2'' → Estado = ''Aplicado''; si MEDESTADO = ''3'' → Estado = ''No Aplicado''; si MEDESTADO IN (''1'',''3'') y la fecha de referencia < fecha actual → Alerta = ''True'' (administración vencida sin aplicar) else Alerta = ''False''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ObtenerFechaFormateada; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJAMED; dbo.HCVIAADMI; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INUNIMEDI; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.CHTIPESTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarAdministracionesMedicamentosPacientes';
-- GO
