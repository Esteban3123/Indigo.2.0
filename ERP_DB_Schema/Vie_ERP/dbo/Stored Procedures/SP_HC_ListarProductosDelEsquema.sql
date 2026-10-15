CREATE PROCEDURE [dbo].[SP_HC_ListarProductosDelEsquema]
					(
					@ListarFrecuenciaDias as bit,               --Consultar los dias de frecuencia
					@IdEsquema as INT,                          --ID esquema Oncologico
					@PesoPaciente as numeric(18,2) = 0,         --Peso del paciente
					@IMC as numeric(18,2) = 0,					--Indice de Masa Corporal
					@SCT as numeric(18,2)= 0					--Superficie Corporal Total
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
			, A.DescriptionDays
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
			, A.DescriptionDays
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
			convert(int, 0) as CantidadMedicamentoDia,convert(numeric(18,2), 0 ) as DosisPresentacion,convert(int, 0) as CantidadMedicamentoCiclo
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