
CREATE PROCEDURE [dbo].[SP_ONCO_ListarMedicamentosPorCicloModificar]
(
					@IdEsquema as integer,		
					@IDHCORDQUIMIO as integer,
					@CicloModificar as Integer,
					@INDPaciente as varchar(25),
					@PesoPaciente as numeric(18,2) = 0,			--Peso del paciente
					@IMC as numeric(18,2) = 0,					--Indice de Masa Corporal
					@SCT as numeric(18,2)= 0,					--Superficie Corporal Total
					@CentroAtencion AS char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

			--Consultamos los medicamentos que se guardaron en el ciclo seleccionado por el medico a modificar
			Select rtrim(inp.IPCODPACI ) as 'IdentificacionPaciente', rtrim(inp.IPNOMCOMP) as 'NombrePaciente',A.CICLO,  isnull(she.TypePrescription,1) As TypePrescription,Schemes.Description as 'Nombre Esquema', isnull(she.ID, atc.Id) AS SchemesDrugsID, 0 as 'IdRelacion',0 as CostMinimumUnitMeasure,A.SchemesId, Rtrim(B.CODPRODUC) 'Codigo', Rtrim(B.DESPRODUC) As 'Medicamento', Rtrim(A.CODUNIMED) As 'Codigo Unidad Medida', Rtrim(DESUNIMED) As 'Unidad Medida',Rtrim(C.CODVIAADM) As 'Codigo Via',Rtrim(DESVIAADM) 'Via Administracion' ,A.DIA As 'Dias',B.NOPOSPROD AS 'NO POS',B.TIPFORMED,B.CODGRUFAR,B.CODJUMEES As JustificacionMedicamentosEspeciales,B.CODFORMED,Rtrim(B.CONCENMED) As 'Concentracion 2',B.PESTOTMED,
					B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,B.CODUNIADM,B.CALCANAUT, 
					A.TIPOFACTOR as 'Tipo Factor', A.INSTRUADMINIS as 'Instrucciones',a.DOSIS As 'Maxima Dosis', --concat(A.DOSIS , ' - ', rtrim(d.DESUNIMED), ' - ', rtrim(c.DESVIAADM)) as 'Dosis',
					IIF(she.Dose IS NOT NULL,
						dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TIPOFACTOR,she.Dose,@SCT,@PesoPaciente,@IMC),
						(select top 1 tmpOM.DOSISTEORICA from EHR.HCORDMEDICAM as tmpOM 
						WHERE tmpOM.IDHCORDQUIMIO = cabe.id and tmpOM.CODPRODUC = A.CODPRODUC   and tmpOM.CICLO = @CicloModificar order by tmpOM.id asc )
						) As Dosis,
					(Select count(1) from [dbo].[SplitString](A.DIA)) As 'Cantidad Dias', A.CODDILUYENTE as 'DiluentDrugCode', A.VOLUMENFINAL as 'FinalVolume',A.INDICE AS 'Indice', convert(numeric(18,2), 0) as DosisReal,convert(numeric(18,2), A.DOSIS) as DosisTeorica, concat(isnull(she.Dose,A.DOSIS),' ', Rtrim(ABRUNIMED)) as 'DosisTeoricaText', a.CANTIDADDILU as CantidadDiluyente,
					ent.Name AS 'Nombre ATC',atc.ATCEntityId as 'IdATCEntity',FORM.DESFORMED AS 'Nombre Forma',convert(numeric(18,2), 0 ) as DosisPresentacion,
					Dil.DESPRODUC as 'Nombre Diluyente',dbo.[CalcularConcentracionMedicamento](B.TIPFORMED,B.PESTOTMED,B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL) as 'Concentracion',
					case B.TIPFORMED
							when   1 then
							  (select Rtrim(Ltrim(DESUNIMED)) FROM dbo.INUNIMEDI WHERE CODUNIMED = B.CODUNIPES)  
							when 2 then
							  (select Rtrim(Ltrim(DESUNIMED)) FROM dbo.INUNIMEDI WHERE CODUNIMED = B.CODUNIVOL)
							when 3 then
							   (select top 1 CASE WHEN CODUNIMED=B.CODUNIPES THEN Rtrim(Ltrim(DESUNIMED)) WHEN CODUNIMED=B.CODUNIVOL THEN Rtrim(Ltrim(DESUNIMED))  END FROM dbo.INUNIMEDI WHERE CODUNIMED IN (B.CODUNIPES,B.CODUNIVOL))
							when 4 then
							   (select Rtrim(Ltrim(DESUNIMED)) FROM dbo.INUNIMEDI WHERE CODUNIMED = B.CODUNIADM)
					end as 'Unidad Medidad Medicamento', A.IDHCORDQUIMIO, B.TODASPATO
					, HomeAdministration = (select  TOP 1 MEDICAMENTOENCASA from EHR.HCORDMEDICAM zv where zv.SchemesId = @IdEsquema and zv.IDHCORDQUIMIO = A.IDHCORDQUIMIO and zv.CODPRODUC = A.CODPRODUC and zv.CICLO = @CicloModificar)
					, atc.Conditioned, UNIRS = CASE WHEN atc.UNIRS = 1 THEN 'Si' ELSE 'No' END
					, PBS = CASE WHEN atc.Conditioned = 1 THEN 'Condicionado'
						WHEN B.NOPOSPROD = 1 THEN 'No'
						ELSE 'Si'
						END
					,A.DESCRIPCIONDIA AS 'DescriptionDays'
					,CantidadMedicamentoCiclo =  (select SUM(xy.CANTIDAD) from EHR.HCORDMEDICAM xy where xy.IDHCORDQUIMIO = @IDHCORDQUIMIO and xy.CICLO = @CicloModificar and xy.CODPRODUC = a.CODPRODUC)
					,CantidadMedicamentoDia = (select top 1 xy.CANTIDAD from EHR.HCORDMEDICAM xy where xy.IDHCORDQUIMIO = @IDHCORDQUIMIO and xy.CICLO = @CicloModificar  and xy.CODPRODUC = a.CODPRODUC order by xy.DIA ASC )
					, DiasSinCumplir = (
						SELECT TOP 1
							STUFF((
								SELECT ', ' + RTRIM(CAST(DiasUnicos.DIA AS VARCHAR(10)))
								FROM (
									SELECT DISTINCT HM.DIA
									FROM EHR.HCORDCICLOSD HD
									INNER JOIN EHR.HCORDMEDICAM HM
										ON HD.IDHCORDQUIMIO = HM.IDHCORDQUIMIO
										AND HD.CICLO = HM.CICLO
										AND HD.DIA = HM.DIA
									WHERE HD.IDHCORDQUIMIO = @IDHCORDQUIMIO
									  AND HD.CICLO = @CicloModificar
									  AND HM.CODPRODUC = B.CODPRODUC
									  AND HD.ESTADODIA IN (1)
									  AND HM.State <> 4
								) AS DiasUnicos
								ORDER BY DiasUnicos.DIA
								FOR XML PATH('')
							), 1, 2, '')
						FROM EHR.HCORDCICLOSD a
						WHERE a.IDHCORDQUIMIO = @IDHCORDQUIMIO
						  AND a.CICLO = @CicloModificar
						  AND a.ESTADODIA IN (1)
					)					
					,DiasCumplido = (SELECT top 1
						stuff((
								SELECT ', ' + rtrim(a.DIA)
								FROM EHR.HCORDCICLOSD a 
								WHERE a.IDHCORDQUIMIO  = @IDHCORDQUIMIO and a.CICLO = @CicloModificar and a.ESTADODIA in (2) --2:Dia Cumplido
								FOR XML PATH('')
								), 1, 1, '') 
					FROM EHR.HCORDCICLOSD a 
					WHERE a.IDHCORDQUIMIO  = @IDHCORDQUIMIO and a.CICLO = @CicloModificar and a.ESTADODIA in (2)), --2:Dia Cumplido
					IIF(A.State IS NULL, 1, IIF(A.State = 4, 4, cast(1 As int))) as Estado, A.IDHCORDCICLOS, A.JUSTIFICACIONPBS, A.SOLFARMACIAREALIZADA, A.IDHCFARMEPC, A.TraceabilityPaperworkId,
					A.TraceabilityPaperworkEventsId, A.DESCRIPADMIN, A.ProfessionalModification, A.DateModification, A.NUMEFOLIO, A.ID, she.ID As IdSchemeDrug, 
					dbo.MedicationAvailability(B.CODPRODUC, @CentroAtencion) as Disponibles
				From [EHR].[HCORMEDICAMESQUEMA]  A
					Inner Join EHR.HCORDQUIMIO Cabe ON Cabe.ID = A.IDHCORDQUIMIO AND Cabe.IPCODPACI = @INDPaciente and cabe.ESTADO IN (1,2) 
					Inner Join EHR.Schemes Schemes on Schemes.Id = Cabe.SchemesId 
					left JOIN EHR.SchemesDrugs she ON she.DrugCode = A.CODPRODUC and she.SchemesId = @IdEsquema
					inner join INPACIENT inp on inp.IPCODPACI = Cabe.IPCODPACI
					Inner Join IHLISTPRO B ON A.CODPRODUC = B.CODPRODUC
					Inner Join HCVIAADMI C ON A.CODVIAADM = C.CODVIAADM 
					Inner Join INUNIMEDI D ON A.CODUNIMED = D.CODUNIMED  
					Inner Join IHFORMEDI FORM ON FORM.CODFORMED = B.CODFORMED              
					Inner Join Inventory.ATC atc ON A.CODPRODUC = atc.Code              
					Inner Join Inventory.ATCEntity ent ON atc.ATCEntityId = ent.ID 
					left Join IHLISTPRO Dil on A.CODDILUYENTE = Dil.CODPRODUC 
				where   A.IDHCORDQUIMIO = @IDHCORDQUIMIO
						and a.SchemesId = @IdEsquema	
						and A.CICLO = @CicloModificar
						and cabe.ESTADO IN (1,2) 
						and A.State <> 4
						and cabe.IPCODPACI = @INDPaciente				 
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos asignados a un ciclo de quimioterapia específico que el médico desea modificar, combinando la información del esquema oncológico (protocolo, días de aplicación, dosis teórica) con los datos reales registrados en las órdenes de medicamentos del ciclo seleccionado. Para cada medicamento entrega la dosis calculada según el peso, IMC o superficie corporal del paciente, la vía de administración, diluyente, forma farmacéutica, unidad de medida, clasificación PBS/No PBS/Condicionado, disponibilidad en el centro de atención y el estado de cumplimiento por día (días cumplidos y días pendientes). Se usa en el módulo de oncología para que el profesional de salud revise y ajuste la prescripción de medicamentos antes de confirmar un ciclo de tratamiento, cruzando el esquema teórico (SchemesDrugs, Schemes) con lo ya ordenado (HCORDQUIMIO, HCORDMEDICAM) y el catálogo de productos farmacéuticos (IHLISTPRO, HCVIAADMI, INUNIMEDI, IHFORMEDI).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos de un ciclo específico de una orden de quimioterapia para edición, calculando dosis teórica/real, concentración, disponibilidad y trazabilidad de días cumplidos/incumplidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de quimioterapia debe existir y estar en estado 1 o 2 (activa/en curso).; El paciente indicado debe coincidir con el titular de la orden de quimioterapia.; Deben existir los medicamentos del esquema en HCORMEDICAMESQUEMA para el ciclo a modificar.; El medicamento del esquema no debe estar en estado 4 (anulado/eliminado).; Los códigos de producto, vía de administración, unidad de medida, forma farmacéutica y ATC deben existir en sus catálogos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se listan medicamentos cuya orden de quimioterapia esté en estado 1 o 2.; Nunca devuelve medicamentos del esquema con State=4.; El paciente del filtro debe coincidir con el de la cabecera de la orden (doble validación).; Los días cumplidos corresponden a ESTADODIA=2 y los pendientes a ESTADODIA=1.; El estado retornado al cliente solo puede ser 1 o 4.; PBS es excluyente: ''Condicionado'' prevalece sobre la marca NOPOS.; La dosis sólo se recalcula si no hay registro previo con State=3 para el ciclo (preserva dosis ajustadas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente oncológico; Esquema de quimioterapia; Ciclo de tratamiento; Día de administración; Dosis teórica vs dosis real; Tipo de factor (SCT, peso, IMC); Diluyente y volumen final; Vía de administración; Forma farmacéutica; Clasificación ATC; Medicamento POS/No POS; PBS (Plan Básico de Salud) condicionado; UNIRS; Medicamento en casa (HomeAdministration); Justificación de medicamentos especiales; Trazabilidad de trámites; Disponibilidad de medicamentos por centro de atención; Días cumplidos / días sin cumplir del ciclo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve el listado de medicamentos del ciclo solo cuando la orden está en estado 1 o 2, el medicamento no está en State=4 y pertenece al paciente y esquema indicados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si she.Dose IS NOT NULL y no existe HCORDMEDICAM con State=3 para ese producto/ciclo → Calcula dosis vía dbo.ValorDosisMedicamentosEsquemasOncologicos usando TIPOFACTOR, dosis del esquema, SCT, peso e IMC else Toma la DOSISPROD ya registrada en HCORDMEDICAM (primera fila por id ascendente) para ese producto y ciclo; si B.TIPFORMED = 1 (sólido) → Unidad de medida del medicamento se toma de CODUNIPES else Aplica otras ramas según TIPFORMED; si B.TIPFORMED = 2 (líquido) → Unidad de medida se toma de CODUNIVOL; si B.TIPFORMED = 3 (mixto) → Unidad de medida se toma de CODUNIPES o CODUNIVOL (la primera encontrada); si B.TIPFORMED = 4 (administración) → Unidad de medida se toma de CODUNIADM; si atc.Conditioned = 1 → PBS se reporta como ''Condicionado'' else Si NOPOSPROD=1 entonces PBS=''No'', en caso contrario PBS=''Si''; si atc.UNIRS = 1 → UNIRS se reporta como ''Si'' else UNIRS se reporta como ''No''; si A.State IS NULL → Estado del medicamento se devuelve como 1 (activo) else Si State=4 se devuelve 4, en cualquier otro caso 1; si ESTADODIA del ciclo = 1 y State del medicamento <> 4 → El día se incluye en la lista ''DiasSinCumplir''; si ESTADODIA del ciclo = 2 → El día se incluye en la lista ''DiasCumplido''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString; dbo.ValorDosisMedicamentosEsquemasOncologicos; dbo.CalcularConcentracionMedicamento; dbo.MedicationAvailability', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORMEDICAMESQUEMA; EHR.HCORDQUIMIO; EHR.Schemes; EHR.SchemesDrugs; EHR.HCORDMEDICAM; EHR.HCORDCICLOSD; INPACIENT; IHLISTPRO; HCVIAADMI; dbo.INUNIMEDI; IHFORMEDI; Inventory.ATC; Inventory.ATCEntity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosPorCicloModificar';
-- GO
