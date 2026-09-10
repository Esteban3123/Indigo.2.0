CREATE PROCEDURE [dbo].[SP_HC_ListarProductosDelEsquema]
					(
					@ListarFrecuenciaDias as bit,               --Consultar los dias de frecuencia
					@IdEsquema as INT,                          --ID esquema Oncologico
					@PesoPaciente as numeric(18,2) = 0,         --Peso del paciente
					@IMC as numeric(18,2) = 0,					--Indice de Masa Corporal
					@SCT as numeric(18,2)= 0,					--Superficie Corporal Total
					@CentroAtencion AS char(20)
					)
					AS

	BEGIN
	 SET NOCOUNT ON;			 
	 
if @ListarFrecuenciaDias  = 0 begin 

	Select isnull(A.TypePrescription,1) As TypePrescription,she.Description as 'Nombre Esquema',A.Id AS SchemesDrugsID,0 as 'IdRelacion',A.CostMinimumUnitMeasure,A.SchemesId, Rtrim(B.CODPRODUC) 'Codigo', Rtrim(B.DESPRODUC) As 'Medicamento', Rtrim(A.MeasurementUnit) As 'Codigo Unidad Medida', Rtrim(DESUNIMED) As 'Unidad Medida',Rtrim(C.CODVIAADM) As 'Codigo Via',Rtrim(DESVIAADM) 'Via Administracion' ,A.Days As 'Dias',B.NOPOSPROD AS 'NO POS',B.TIPFORMED,B.CODGRUFAR,B.CODJUMEES As JustificacionMedicamentosEspeciales,B.CODFORMED,Rtrim(B.CONCENMED) As 'Concentracion 2',B.PESTOTMED,
			B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,B.CODUNIADM,B.CALCANAUT, 
			A.TypeFactor as 'Tipo Factor', A.InstructionsAdministration as 'Instrucciones',dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TypeFactor,A.DoseMaximum,@SCT,@PesoPaciente,@IMC) As 'Maxima Dosis', dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TypeFactor,A.Dose,@SCT,@PesoPaciente,@IMC) As Dosis,
			(Select count(1) from [dbo].[SplitString](A.Days)  ) As 'Cantidad Dias', DiluentDrugCode,FinalVolume,Indice, convert(numeric(18,2), 0) as DosisReal,convert(numeric(18,2), A.Dose ) as DosisTeorica,concat(convert(numeric(18,2), A.Dose ),' ', Rtrim(ABRUNIMED)) as 'DosisTeoricaText', A.QuantityDiluent as CantidadDiluyente,
			ent.Name AS 'Nombre ATC',atc.ATCEntityId as 'IdATCEntity',FORM.DESFORMED AS 'Nombre Forma',convert(int, 0) as CantidadMedicamentoDia,convert(numeric(18,2), 0 ) as DosisPresentacion,convert(int, 0) as CantidadMedicamentoCiclo,
			Dil.DESPRODUC as 'Nombre Diluyente',dbo.[CalcularConcentracionMedicamentoEsquemasOncologicos](B.TIPFORMED,B.PESTOTMED,B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,A.MeasurementUnit) as 'Concentracion',
			case B.TIPFORMED
					when   1 then
					  (select Rtrim(Ltrim(DESUNIMED)) FROM dbo.INUNIMEDI WHERE CODUNIMED = B.CODUNIPES)  
					when 2 then
					  (select Rtrim(Ltrim(DESUNIMED)) FROM dbo.INUNIMEDI WHERE CODUNIMED = B.CODUNIVOL)
					when 3 then
					   (select top 1 CASE WHEN CODUNIMED=B.CODUNIPES THEN Rtrim(Ltrim(DESUNIMED)) WHEN CODUNIMED=B.CODUNIVOL THEN Rtrim(Ltrim(DESUNIMED))  END FROM dbo.INUNIMEDI WHERE CODUNIMED IN (B.CODUNIPES,B.CODUNIVOL))
					when 4 then
					   (select Rtrim(Ltrim(DESUNIMED)) FROM dbo.INUNIMEDI WHERE CODUNIMED = B.CODUNIADM)
			end as 'Unidad Medidad Medicamento',A.HomeAdministration
			, B.TODASPATO
			, atc.Conditioned, UNIRS = CASE WHEN atc.UNIRS = 1 THEN 'Si' ELSE 'No' END
			, PBS = CASE WHEN atc.Conditioned = 1 THEN 'Condicionado'
				WHEN B.NOPOSPROD = 1 THEN 'No'
				ELSE 'Si'
				END
			, A.DescriptionDays,
			dbo.MedicationAvailability(B.CODPRODUC, @CentroAtencion) as Disponibles, convert(int, 0) as CICLO
		From [EHR].SchemesDrugs  A
	     	INNER JOIN EHR.Schemes she ON she.Id = a.SchemesId 
			INNER JOIN IHLISTPRO B ON A.DrugCode = B.CODPRODUC
			INNER JOIN HCVIAADMI C ON A.RouteOfAdministration = C.CODVIAADM 
			INNER JOIN INUNIMEDI D ON A.MeasurementUnit = D.CODUNIMED  
			INNER JOIN IHFORMEDI FORM ON FORM.CODFORMED = B.CODFORMED              
			INNER JOIN Inventory.ATC atc ON A.DrugCode = atc.Code              
			INNER JOIN Inventory.ATCEntity ent ON atc.ATCEntityId = ent.ID 
			left Join IHLISTPRO Dil on A.DiluentDrugCode = Dil.CODPRODUC 
		where A.SchemesId = @IdEsquema		
union 
	Select isnull(A.TypePrescription,1) As TypePrescription, she.Description as 'Nombre Esquema',A.Id AS SchemesDrugsID,RELA.ID as 'IdRelacion',RELA.CostMinimumUnitMeasure, A.SchemesId, Rtrim(B.CODPRODUC) 'Codigo', Rtrim(B.DESPRODUC) As 'Medicamento', Rtrim(A.MeasurementUnit) As 'Codigo Unidad Medida', Rtrim(DESUNIMED) As 'Unidad Medida',Rtrim(C.CODVIAADM) As 'Codigo Via',Rtrim(DESVIAADM) 'Via Administracion' ,A.Days As 'Dias',B.NOPOSPROD AS 'NO POS',B.TIPFORMED,B.CODGRUFAR,B.CODJUMEES As JustificacionMedicamentosEspeciales,B.CODFORMED,Rtrim(B.CONCENMED) As 'Concentracion 2',B.PESTOTMED,
			B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,B.CODUNIADM,B.CALCANAUT, 
			A.TypeFactor as 'Tipo Factor', A.InstructionsAdministration as 'Instrucciones',dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TypeFactor,A.DoseMaximum,@SCT,@PesoPaciente,@IMC) As 'Maxima Dosis', dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TypeFactor,A.Dose,@SCT,@PesoPaciente,@IMC) As Dosis,
			(Select count(1) from [dbo].[SplitString](A.Days)  ) As 'Cantidad Dias', DiluentDrugCode,FinalVolume,Indice, convert(numeric(18,2), 0) as DosisReal,convert(numeric(18,2), A.Dose ) as DosisTeorica, concat(convert(numeric(18,2), A.Dose ), ' ', Rtrim(ABRUNIMED)) as 'DosisTeoricaText', A.QuantityDiluent as CantidadDiluyente,
			ent.Name AS 'Nombre ATC',atc.ATCEntityId as 'IdATCEntity',FORM.DESFORMED AS 'Nombre Forma',convert(int, 0) as CantidadMedicamentoDia,convert(numeric(18,2), 0 ) as DosisPresentacion,convert(int, 0) as CantidadMedicamentoCiclo,
      		Dil.DESPRODUC as 'Nombre Diluyente',dbo.[CalcularConcentracionMedicamentoEsquemasOncologicos](B.TIPFORMED,B.PESTOTMED,B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,A.MeasurementUnit) as 'Concentracion',
			case B.TIPFORMED
					when   1 then
					  (select Rtrim(Ltrim(DESUNIMED)) FROM dbo.INUNIMEDI WHERE CODUNIMED = B.CODUNIPES) 
					when 2 then
					  (select Rtrim(Ltrim(DESUNIMED)) FROM dbo.INUNIMEDI WHERE CODUNIMED = B.CODUNIVOL)
					when 3 then
					   (select top 1 CASE WHEN CODUNIMED=B.CODUNIPES THEN Rtrim(Ltrim(DESUNIMED)) WHEN CODUNIMED=B.CODUNIVOL THEN Rtrim(Ltrim(DESUNIMED))  END FROM dbo.INUNIMEDI WHERE CODUNIMED IN (B.CODUNIPES,B.CODUNIVOL))
					when 4 then
					   (select Rtrim(Ltrim(DESUNIMED)) FROM dbo.INUNIMEDI WHERE CODUNIMED = B.CODUNIADM)
			end as 'Unidad Medidad Medicamento',A.HomeAdministration
			, B.TODASPATO
			, atc.Conditioned, UNIRS = CASE WHEN atc.UNIRS = 1 THEN 'Si' ELSE 'No' END
			, PBS = CASE WHEN atc.Conditioned = 1 THEN 'Condicionado'
				WHEN B.NOPOSPROD = 1 THEN 'No'
				ELSE 'Si'
				END
			, A.DescriptionDays,
			dbo.MedicationAvailability(B.CODPRODUC, @CentroAtencion) as Disponibles, convert(int, 0) as CICLO
		From [EHR].SchemesDrugs  A
		    INNER JOIN EHR.Schemes she ON she.Id = a.SchemesId 
			INNER JOIN EHR.SchamesDrugsRelation RELA ON A.Id = RELA.IdSchemesDrugs 
			INNER JOIN IHLISTPRO B ON RELA.DrugCode = B.CODPRODUC
			INNER JOIN HCVIAADMI C ON A.RouteOfAdministration = C.CODVIAADM 
			INNER JOIN INUNIMEDI D ON A.MeasurementUnit = D.CODUNIMED  
		    INNER JOIN IHFORMEDI FORM ON FORM.CODFORMED = B.CODFORMED              
			INNER JOIN Inventory.ATC atc ON A.DrugCode = atc.Code              
			INNER JOIN Inventory.ATCEntity ent ON atc.ATCEntityId = ent.ID     
			left Join IHLISTPRO Dil on A.DiluentDrugCode = Dil.CODPRODUC  
	where A.SchemesId = @IdEsquema	
end
else if @ListarFrecuenciaDias = 1 begin -- Listar Frecuencia

     	Select 
			A.id as SchemesDrugsID,
			fre.ID,
			isnull(A.TypePrescription,1) As TypePrescription,
			A.SchemesId,
			she.Description as 'Nombre Esquema',
			fre.Day,
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,fre.Dose1,@SCT,@PesoPaciente,@IMC)) As Dosis1,
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,fre.Dose2,@SCT,@PesoPaciente,@IMC)) As Dosis2,
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,fre.Dose3,@SCT,@PesoPaciente,@IMC)) As Dosis3,
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,fre.Dose4,@SCT,@PesoPaciente,@IMC)) As Dosis4,
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,fre.Dose5,@SCT,@PesoPaciente,@IMC)) As Dosis5,
			fre.dose1 as DOSISPAR1,fre.dose2 as DOSISPAR2,fre.dose3 as DOSISPAR3,fre.dose4 as DOSISPAR4,fre.dose5 as DOSISPAR5,
			convert(char(5),fre.Hour1, 108) as Hour1,
			convert(char(5),fre.Hour2, 108) as Hour2,
			convert(char(5),fre.Hour3, 108) as Hour3,
			convert(char(5),fre.Hour4, 108) as Hour4,
			convert(char(5),fre.Hour5, 108) as Hour5,
			ent.Name AS 'Nombre ATC',
			atc.ATCEntityId as 'IdATCEntity',
			Rtrim(B.CODPRODUC) 'Codigo',
			Rtrim(B.DESPRODUC) As 'Medicamento',
			FORM.DESFORMED AS 'Nombre Forma',
			Rtrim(C.CODVIAADM) As 'Codigo Via',
			Rtrim(A.MeasurementUnit) As 'Codigo Unidad Medida',
			A.TypeFactor as 'Tipo Factor',
			A.InstructionsAdministration as 'Instrucciones', Indice,
			convert(numeric(18,2), A.Dose ) as DosisTeorica,
			concat(convert(numeric(18,2), fre.Dose1 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText1',
			concat(convert(numeric(18,2), fre.Dose2 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText2',
			concat(convert(numeric(18,2), fre.Dose3 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText3',
			concat(convert(numeric(18,2), fre.Dose4 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText4',
			concat(convert(numeric(18,2), fre.Dose5 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText5',
			DiluentDrugCode, FinalVolume, A.QuantityDiluent as CantidadDiluyente,
			B.TIPFORMED,B.CODGRUFAR, B.CODFORMED, dbo.[CalcularConcentracionMedicamentoEsquemasOncologicos](B.TIPFORMED,B.PESTOTMED,B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,A.MeasurementUnit) as 'Concentracion',
			B.PESTOTMED,B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,B.CODUNIADM,B.CALCANAUT,  A.CostMinimumUnitMeasure, A.HomeAdministration,
			convert(int, 0) as CantidadMedicamentoDia,convert(numeric(18,2), 0 ) as DosisPresentacion,convert(int, 0) as CantidadMedicamentoCiclo,
			dbo.MedicationAvailability(B.CODPRODUC, @CentroAtencion) as Disponibles, convert(int, 0) as CICLO
		From [EHR].SchemesDrugs  A
		    INNER JOIN EHR.Schemes she ON she.Id = a.SchemesId 
			INNER JOIN EHR.SchemesDrugsFrequency fre ON A.Id = fre.SchemesDrugsID 
			INNER JOIN IHLISTPRO B ON A.DrugCode = B.CODPRODUC
			INNER JOIN HCVIAADMI C ON A.RouteOfAdministration = C.CODVIAADM 
			INNER JOIN INUNIMEDI D ON A.MeasurementUnit = D.CODUNIMED  
		    INNER JOIN IHFORMEDI FORM ON FORM.CODFORMED = B.CODFORMED              
			INNER JOIN Inventory.ATC atc ON A.DrugCode = atc.Code              
			INNER JOIN Inventory.ATCEntity ent ON atc.ATCEntityId = ent.ID     
			left Join IHLISTPRO Dil on A.DiluentDrugCode = Dil.CODPRODUC  
		where A.SchemesID = @IdEsquema AND A.TypePrescription = 2

	end
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos (fármacos y diluyentes) que componen un esquema oncológico específico, calculando la dosis teórica y máxima de cada producto según el peso, IMC o superficie corporal del paciente. Combina información del catálogo de medicamentos (IHLISTPRO), vías de administración (HCVIAADMI), unidades de medida (INUNIMEDI), formas farmacéuticas (IHFORMEDI) y la clasificación ATC de cada fármaco para devolver un detalle completo de cada línea del protocolo de quimioterapia. Cuando el parámetro de frecuencia de días está activo, retorna adicionalmente el desglose día a día de la programación del esquema. Se usa en la gestión de esquemas oncológicos para previsualizar, prescribir y calcular dosis de los medicamentos de un protocolo de tratamiento, mostrando también disponibilidad en el centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosDelEsquema';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosDelEsquema';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos de un esquema oncológico, calculando dosis teóricas/máximas según factor (peso, IMC, SCT), concentración, disponibilidad y, opcionalmente, la frecuencia diaria de administración.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosDelEsquema';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el esquema oncológico identificado por el ID recibido en EHR.Schemes con sus medicamentos en EHR.SchemesDrugs.; Para listar frecuencia, los medicamentos del esquema deben tener TypePrescription = 2 y registros en EHR.SchemesDrugsFrequency.; Los medicamentos referenciados deben existir en IHLISTPRO; vías en HCVIAADMI; unidades en INUNIMEDI; formas en IHFORMEDI; y clasificación ATC en Inventory.ATC e Inventory.ATCEntity.; Si se requieren dosis ajustadas a paciente, deben suministrarse Peso, IMC y SCT (de lo contrario quedan en 0 y la dosis se calcula con esos valores).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosDelEsquema';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan medicamentos pertenecientes al esquema oncológico solicitado.; El cálculo de dosis (teórica y máxima) siempre se delega a la función ValorDosisMedicamentosEsquemasOncologicos usando el TypeFactor del medicamento y los valores antropométricos del paciente.; La concentración del medicamento siempre se calcula vía CalcularConcentracionMedicamentoEsquemasOncologicos en función del tipo de forma farmacéutica.; TypePrescription nunca es nulo en la salida (default 1 si no está definido).; El listado de frecuencia se restringe exclusivamente a medicamentos con TypePrescription = 2.; La disponibilidad del medicamento se evalúa siempre contra el centro de atención recibido.; Los campos calculados DosisReal, CantidadMedicamentoDia, DosisPresentacion, CantidadMedicamentoCiclo y CICLO se devuelven inicializados en cero (se completan posteriormente fuera del SP).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosDelEsquema';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Esquema oncológico; Medicamento; Dosis teórica y máxima; Tipo de factor de dosificación (Peso, IMC, SCT); Vía de administración; Unidad de medida; Forma farmacéutica; Diluyente; Concentración del medicamento; Clasificación ATC; PBS (Plan de Beneficios en Salud / NO POS); Justificación de medicamentos especiales; Frecuencia de administración (días y horas); Administración domiciliaria; Disponibilidad por centro de atención; Ciclo de tratamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosDelEsquema';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] EHR.SchemesDrugs: Cuando ListarFrecuenciaDias = 0, retorna los medicamentos del esquema (filtrado por SchemesId) unidos con sus relaciones de SchamesDrugsRelation, calculando dosis, dosis máxima, concentración, unidad de medida del medicamento, indicador PBS y disponibilidad.; [RETURN_RESULT] EHR.SchemesDrugsFrequency: Cuando ListarFrecuenciaDias = 1, retorna por cada medicamento del esquema con TypePrescription = 2 las hasta 5 dosis y horas diarias programadas, junto con datos del medicamento y disponibilidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosDelEsquema';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ListarFrecuenciaDias = 0 → Devuelve los medicamentos del esquema mediante UNION: primero los registros base de SchemesDrugs y luego los relacionados en SchamesDrugsRelation (medicamentos sustitutos/relacionados).; si ListarFrecuenciaDias = 1 → Devuelve la frecuencia diaria de dosis y horas (1 a 5) solo para medicamentos del esquema con TypePrescription = 2.; si B.TIPFORMED = 1 (sólido/peso) → La unidad de medida del medicamento se toma del CODUNIPES (unidad de peso).; si B.TIPFORMED = 2 (líquido/volumen) → La unidad de medida del medicamento se toma del CODUNIVOL (unidad de volumen).; si B.TIPFORMED = 3 (mixto) → La unidad se obtiene del primero que coincida entre CODUNIPES o CODUNIVOL.; si B.TIPFORMED = 4 → La unidad se toma del CODUNIADM (unidad de administración).; si atc.Conditioned = 1 → PBS se reporta como ''Condicionado''. else Si NOPOSPROD = 1 entonces PBS = ''No''; en caso contrario PBS = ''Si''.; si atc.UNIRS = 1 → Marca UNIRS = ''Si''. else UNIRS = ''No''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosDelEsquema';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ValorDosisMedicamentosEsquemasOncologicos; dbo.CalcularConcentracionMedicamentoEsquemasOncologicos; dbo.MedicationAvailability; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosDelEsquema';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.SchemesDrugs; EHR.Schemes; EHR.SchamesDrugsRelation; EHR.SchemesDrugsFrequency; dbo.IHLISTPRO; dbo.HCVIAADMI; dbo.INUNIMEDI; dbo.IHFORMEDI; Inventory.ATC; Inventory.ATCEntity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosDelEsquema';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosDelEsquema';
-- GO
