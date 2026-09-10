CREATE PROCEDURE [dbo].[SP_ONCO_ListarMedicamentosPorCicloModificarDias]
(
					@IdEsquema as integer,		
					@IDHCORDQUIMIO as integer,
					@CicloModificar as Integer,
					@INDPaciente as varchar(25),
					@PesoPaciente as numeric(18,2) = 0,			--Peso del paciente
					@IMC as numeric(18,2) = 0,					--Indice de Masa Corporal
					@SCT as numeric(18,2)= 0					--Superficie Corporal Total
)
AS
BEGIN
	SET NOCOUNT ON;

			Select	cast(0 as bit) as Seleccion,  A.ID as IdHCORDMEDICAM, a.SchemesId,a.IDHCORDQUIMIO,A.ATCEntityId,A.CICLO,A.DIA,A.HORAFRECUDIA,rtrim(A.CODPRODUC) as CODPRODUC,A.CODVIAADM,
						IIF(she.Dose IS NOT NULL AND A.State = 1,
							dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TIPOFACTOR,
								CASE
									WHEN FORMAT(A.HORAFRECUDIA, 'HH:mm') = FORMAT(F.HORA1, 'HH:mm') THEN F.DOSISPAR1
									WHEN FORMAT(A.HORAFRECUDIA, 'HH:mm') = FORMAT(F.HORA2, 'HH:mm') THEN F.DOSISPAR2
									WHEN FORMAT(A.HORAFRECUDIA, 'HH:mm') = FORMAT(F.HORA3, 'HH:mm') THEN F.DOSISPAR3
									WHEN FORMAT(A.HORAFRECUDIA, 'HH:mm') = FORMAT(F.HORA4, 'HH:mm') THEN F.DOSISPAR4
									WHEN FORMAT(A.HORAFRECUDIA, 'HH:mm') = FORMAT(F.HORA5, 'HH:mm') THEN F.DOSISPAR5
									ELSE she.Dose
								END,
								@SCT,@PesoPaciente,@IMC),							
								(select top 1 tmpOM.DOSISPROD from EHR.HCORDMEDICAM as tmpOM 
							WHERE tmpOM.IDHCORDQUIMIO = cabe.id and tmpOM.CODPRODUC = A.CODPRODUC and tmpOM.CICLO = @CicloModificar AND tmpOM.DIA = A.DIA AND tmpOM.ID = A.ID order by tmpOM.State desc)
						) As DOSISPROD, A.CODUNIMED, A.TIPOFACTOR,A.CANTIDAD,A.INDICE,A.DOSISTEORICA,A.INSTRUADMINIS,A.CODDILUYENTE,A.VOLUMENFINAL,A.CANTIDADDILU,A.MEDICAMENTOENCASA,
					   EstadoDia = (select distinct case z.ESTADODIA WHEN 1 then 'Días pendientes de aplicación' when 2 then 'Días aplicados' end as 'Estado día' from ehr.HCORDCICLOSD z where z.IDHCORDQUIMIO = @IDHCORDQUIMIO and z.CICLO = @CicloModificar and z.DIA = a.DIA),
					   CodigoEstado= (select distinct z.ESTADODIA  from ehr.HCORDCICLOSD z where z.IDHCORDQUIMIO = @IDHCORDQUIMIO and z.CICLO = @CicloModificar and z.DIA = a.DIA)
					   , I.TIPFORMED,I.CODUNIPES, I.CODUNIVOL,I.CODUNIADM,I.PESTOTMED, I.VOLTOTMED, I.CALCANAUT, U.DESUNIMED AS UnidaddeMedida, IIF(A.State IS NULL, 1, IIF(A.State = 4, 4, cast(1 As int))) as Estado, '' as Acciones, I.CODFORMED, convert(int, 0) as CantidadMedicamentoDia, convert(numeric(18,2), 0 ) as DosisPresentacion,
					   convert(bit, 0) as DiaFrecuencia, dbo.[CalcularConcentracionMedicamentoEsquemasOncologicos](I.TIPFORMED,I.PESTOTMED,I.CODUNIPES,I.VOLTOTMED,I.CODUNIVOL,A.CODUNIMED) as 'Concentracion', I.DESPRODUC,
					   CostMinimumUnitMeasure = (select Top 1 CostMinimumUnitMeasure from EHR.SchemesDrugs she WHERE she.SchemesId = @IdEsquema AND she.DrugCode = A.CODPRODUC), A.ProfessionalModification, A.DateModification,
					   IdSchemeDrug = (SELECT ID FROM EHR.SchemesDrugs SD WHERE SD.SchemesId = @IdEsquema AND SD.DrugCode = A.CODPRODUC AND SD.Dose = A.DOSISTEORICA),
					   TypePrescription = (SELECT TypePrescription FROM EHR.SchemesDrugs SD WHERE SD.SchemesId = @IdEsquema AND SD.DrugCode = A.CODPRODUC AND SD.Dose = A.DOSISTEORICA),
					   A.DateModification AS FechaModificacion, RTRIM(P.NOMMEDICO) AS UsuarioModifica,
					   CASE WHEN FORMAT(A.HORAFRECUDIA, 'HH:mm') = FORMAT(F.HORA1, 'HH:mm') THEN 1 WHEN FORMAT(A.HORAFRECUDIA, 'HH:mm') = FORMAT(F.HORA2, 'HH:mm') THEN 2 WHEN FORMAT(A.HORAFRECUDIA, 'HH:mm') = FORMAT(F.HORA3, 'HH:mm') THEN 3 WHEN FORMAT(A.HORAFRECUDIA, 'HH:mm') = FORMAT(F.HORA4, 'HH:mm') THEN 4 WHEN FORMAT(A.HORAFRECUDIA, 'HH:mm') = FORMAT(F.HORA5, 'HH:mm') THEN 5 END AS NumeroDosis
    		  from  [EHR].HCORDMEDICAM  A
	  			    LEFT JOIN EHR.HCORDFRECUEMED F ON F.IDHCORDQUIMIO = A.IDHCORDQUIMIO AND F.CICLO = A.CICLO AND F.DIA = A.DIA AND F.CODPRODUC = A.CODPRODUC AND F.State <> 4
					LEFT JOIN dbo.INPROFSAL P ON A.ProfessionalModification = P.CODPROSAL
					Inner Join EHR.HCORDQUIMIO Cabe ON Cabe.ID = A.IDHCORDQUIMIO AND Cabe.IPCODPACI = @INDPaciente and cabe.ESTADO IN (1,2) 	
					Inner Join EHR.Schemes Schemes on Schemes.Id = Cabe.SchemesId 
					left JOIN EHR.SchemesDrugs she ON she.DrugCode = A.CODPRODUC and she.SchemesId = @IdEsquema
					INNER JOIN ehr.HCORMEDICAMESQUEMA B ON A.IDHCORDQUIMIO = B.IDHCORDQUIMIO and A.SchemesId = B.SchemesId and A.CICLO = B.CICLO and A.CODPRODUC = B.CODPRODUC --AND she.TypePrescription = B.TypePrescription  
					Inner Join INUNIMEDI U ON u.CODUNIMED = A.CODUNIMED
					Inner Join IHLISTPRO I ON A.CODPRODUC = I.CODPRODUC
		     where   A.IDHCORDQUIMIO = @IDHCORDQUIMIO
						and a.SchemesId = @IdEsquema	
						and A.CICLO = @CicloModificar
						and cabe.ESTADO IN (1,2) 
						and cabe.IPCODPACI = @INDPaciente				 
						and A.State <> 4
			order by a.DIA ASC
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos de un ciclo específico de quimioterapia para un paciente dado, permitiendo la modificación de días de aplicación dentro del esquema oncológico. Combina la orden de quimioterapia (HCORDQUIMIO) con el detalle de medicamentos ordenados (HCORDMEDICAM), la frecuencia de dosificación por día (HCORDFRECUEMED) y el protocolo del esquema (SchemesDrugs y Schemes) para presentar cada fármaco del ciclo con su dosis calculada según el peso, IMC y superficie corporal del paciente. También incluye el estado de cada día del ciclo (pendiente o aplicado), la concentración del medicamento, el costo mínimo por unidad de medida, el tipo de prescripción y el profesional que realizó la última modificación, sirviendo como fuente de datos para la pantalla de modificación de días en el módulo de oncología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos prescritos de un ciclo específico de una orden de quimioterapia para su modificación, recalculando dosis según parámetros antropométricos del paciente y mostrando estado de aplicación por día.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de quimioterapia debe existir y estar en estado 1 o 2 (activa o en curso); La orden debe pertenecer al paciente indicado (IPCODPACI); El esquema (SchemesId) debe coincidir con el de la orden y los medicamentos; Solo se consideran medicamentos cuyo State sea distinto de 4 (no anulados/eliminados)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan medicamentos de órdenes en estado 1 o 2; Solo se listan medicamentos no anulados (State <> 4); El paciente y esquema del filtro deben corresponder a los de la orden y de cada medicamento; La frecuencia considerada (HCORDFRECUEMED) excluye registros con State = 4; El cálculo dinámico de dosis solo se aplica a medicamentos activos (State = 1) con dosis definida en el esquema; los demás conservan su dosis registrada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Quimioterapia; Esquema oncológico; Ciclo de tratamiento; Día de aplicación; Dosis teórica; Superficie corporal total (SCT); Índice de masa corporal (IMC); Peso del paciente; Concentración de medicamento; Diluyente; Vía de administración; Frecuencia de dosis; Profesional que modifica; Estado del día (pendiente/aplicado); Tipo de prescripción; Medicamento en casa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve el detalle de medicamentos del ciclo a modificar con dosis recalculada, estado del día, concentración, número de dosis y datos de modificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SchemesDrugs.Dose IS NOT NULL AND HCORDMEDICAM.State = 1 → Calcula DOSISPROD vía función ValorDosisMedicamentosEsquemasOncologicos usando TIPOFACTOR, dosis del esquema, SCT, peso e IMC else Toma la DOSISPROD ya registrada en HCORDMEDICAM para ese ciclo/día/medicamento (top 1 ordenando por State desc); si HCORDCICLOSD.ESTADODIA = 1 → Etiqueta el día como ''Días pendientes de aplicación'' else Si ESTADODIA = 2 etiqueta como ''Días aplicados''; si HCORDMEDICAM.State IS NULL → Estado se devuelve como 1 else Si State = 4 devuelve 4, en otro caso devuelve 1; si HORAFRECUDIA del medicamento coincide (HH:mm) con HORA1..HORA5 de HCORDFRECUEMED → Asigna NumeroDosis = 1..5 según la hora coincidente else NumeroDosis queda NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ValorDosisMedicamentosEsquemasOncologicos; dbo.CalcularConcentracionMedicamentoEsquemasOncologicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDMEDICAM; EHR.HCORDFRECUEMED; dbo.INPROFSAL; EHR.HCORDQUIMIO; EHR.Schemes; EHR.SchemesDrugs; EHR.HCORMEDICAMESQUEMA; dbo.INUNIMEDI; dbo.IHLISTPRO; EHR.HCORDCICLOSD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificarDias';
-- GO
