
CREATE PROCEDURE [dbo].[SP_ONCO_ListarMedicamentosCicloPorAutorizar]
(
					@ListarFrecuenciaDias as bit,               --Consultar los dias de frecuencia
					@IdEsquema as INT,							--ID esquema Oncologico
					@PesoPaciente as numeric(18,2) = 0,			--Peso del paciente
					@IMC as numeric(18,2) = 0,					--Indice de Masa Corporal
					@SCT as numeric(18,2)= 0,					--Superficie Corporal Total
					@CicloAutorizar as Integer,
					@INDPaciente as varchar(25),
					@CentroAtencion AS char(20)
)
AS
BEGIN
	SET NOCOUNT ON;	

if @ListarFrecuenciaDias  = 0 begin 

			--Consultamos los medicamentos que se guardaron en la HC del ciclo anterior
			Select isnull(A.TypePrescription,1) As TypePrescription,Schemes.Description as 'Nombre Esquema',ISNULL(She.Id,atc.Id) AS SchemesDrugsID ,0 as 'IdRelacion',she.CostMinimumUnitMeasure,A.SchemesId, Rtrim(B.CODPRODUC) 'Codigo', Rtrim(B.DESPRODUC) As 'Medicamento', Rtrim(A.CODUNIMED) As 'Codigo Unidad Medida', Rtrim(DESUNIMED) As 'Unidad Medida',Rtrim(C.CODVIAADM) As 'Codigo Via',Rtrim(DESVIAADM) 'Via Administracion' ,A.DIA As 'Dias',B.NOPOSPROD AS 'NO POS',B.TIPFORMED,B.CODGRUFAR,B.CODJUMEES As JustificacionMedicamentosEspeciales,B.CODFORMED,B.PESTOTMED,
					B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,B.CODUNIADM,B.CALCANAUT, 
					A.TIPOFACTOR as 'Tipo Factor', A.INSTRUADMINIS as 'Instrucciones',dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TIPOFACTOR,she.DoseMaximum,@SCT,@PesoPaciente,@IMC) As 'Maxima Dosis', 
					IIF(she.Dose IS NOT NULL,
						dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TIPOFACTOR,she.Dose,@SCT,@PesoPaciente,@IMC),
						(select top 1 tmpOM.DOSISTEORICA from EHR.HCORDMEDICAM as tmpOM 
						WHERE tmpOM.IDHCORDQUIMIO = cabe.id and tmpOM.CODPRODUC = A.CODPRODUC   and tmpOM.CICLO = @CicloAutorizar order by tmpOM.id asc )
						) As Dosis,
										(Select count(1) from [dbo].[SplitString](A.DIA)) As 'Cantidad Dias', A.CODDILUYENTE as 'DiluentDrugCode', A.VOLUMENFINAL as 'FinalVolume',A.INDICE AS 'Indice', convert(numeric(18,2), 0) as DosisReal,
					IIF(she.Dose IS NOT NULL,
							convert(numeric(18,2), isnull(she.Dose,A.DOSIS)),
						(select top 1 tmpOM.DOSISTEORICA from EHR.HCORDMEDICAM as tmpOM 
						  WHERE tmpOM.IDHCORDQUIMIO = cabe.id and tmpOM.CODPRODUC = A.CODPRODUC   and tmpOM.CICLO = @CicloAutorizar order by tmpOM.id asc )
					    ) As DosisTeorica,
					concat(isnull(she.Dose,A.DOSIS),' ', Rtrim(ABRUNIMED)) as 'DosisTeoricaText', a.CANTIDADDILU as CantidadDiluyente,
					ent.Name AS 'Nombre ATC',atc.ATCEntityId as 'IdATCEntity',FORM.DESFORMED AS 'Nombre Forma',convert(int, 0) as CantidadMedicamentoDia,convert(numeric(18,2), 0 ) as DosisPresentacion,convert(int, 0) as CantidadMedicamentoCiclo,
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
					--,she.HomeAdministration
					,HomeAdministration = iif(she.HomeAdministration is null, (select  TOP 1 MEDICAMENTOENCASA from EHR.HCORDMEDICAM zv where zv.SchemesId = @IdEsquema and zv.IDHCORDQUIMIO = A.IDHCORDQUIMIO and zv.CODPRODUC = A.CODPRODUC and zv.CICLO = @CicloAutorizar), she.HomeAdministration )
					, atc.Conditioned, UNIRS = CASE WHEN atc.UNIRS = 1 THEN 'Si' ELSE 'No' END
					, PBS = CASE WHEN atc.Conditioned = 1 THEN 'Condicionado'
						WHEN B.NOPOSPROD = 1 THEN 'No'
						ELSE 'Si'
						END
						, A.DESCRIPCIONDIA AS 'DescriptionDays', A.ProfessionalModification, A.CICLO + 1 As 'CICLO', IIF(A.State IS NULL, 1, IIF(A.State = 4, 4, cast(1 As int))) as Estado,
						dbo.MedicationAvailability(B.CODPRODUC, @CentroAtencion) as Disponibles
				From [EHR].[HCORMEDICAMESQUEMA]  A
					INNER JOIN EHR.HCORDQUIMIO Cabe ON Cabe.ID = A.IDHCORDQUIMIO
					INNER JOIN EHR.Schemes Schemes on Schemes.Id = Cabe.SchemesId 
					left JOIN EHR.SchemesDrugs she ON she.DrugCode = A.CODPRODUC and she.SchemesId = @IdEsquema 
					INNER JOIN IHLISTPRO B ON A.CODPRODUC = B.CODPRODUC
					INNER JOIN HCVIAADMI C ON A.CODVIAADM = C.CODVIAADM 
					INNER JOIN INUNIMEDI D ON A.CODUNIMED = D.CODUNIMED  
					INNER JOIN IHFORMEDI FORM ON FORM.CODFORMED = B.CODFORMED              
					INNER JOIN Inventory.ATC atc ON A.CODPRODUC = atc.Code              
					INNER JOIN Inventory.ATCEntity ent ON atc.ATCEntityId = ent.ID 
					left Join IHLISTPRO Dil on A.CODDILUYENTE = Dil.CODPRODUC 
				where A.SchemesId = @IdEsquema	and A.CICLO = @CicloAutorizar AND ESTADO IN (1,2) and  cabe.IPCODPACI = @INDPaciente and A.State <> 4
					  and not exists (select 1 from EHR.SchamesDrugsRelation x inner join EHR.SchemesDrugs b on x.IdSchemesDrugs = b.Id and b.SchemesId =  @IdEsquema  WHERE x.DrugCode = A.CODPRODUC )
					  and exists (select 1 from EHR.HCORDMEDICAM x WHERE x.IDHCORDQUIMIO = cabe.id and x.CODPRODUC = A.CODPRODUC   and x.CICLO = @CicloAutorizar and x.State <> 4 )
		union
		---Consultamos los medicamentos relacionados
				   Select isnull(A.TypePrescription,1) As TypePrescription,Schemes.Description as 'Nombre Esquema', ISNULL(She.Id,atc.Id) AS SchemesDrugsID,RELA.ID as 'IdRelacion',she.CostMinimumUnitMeasure,A.SchemesId, Rtrim(B.CODPRODUC) 'Codigo', Rtrim(B.DESPRODUC) As 'Medicamento', Rtrim(A.CODUNIMED) As 'Codigo Unidad Medida', Rtrim(DESUNIMED) As 'Unidad Medida',Rtrim(C.CODVIAADM) As 'Codigo Via',Rtrim(DESVIAADM) 'Via Administracion' ,A.DIA As 'Dias',B.NOPOSPROD AS 'NO POS',B.TIPFORMED,B.CODGRUFAR,B.CODJUMEES As JustificacionMedicamentosEspeciales,B.CODFORMED,B.PESTOTMED,
					B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,B.CODUNIADM,B.CALCANAUT, 
					A.TIPOFACTOR as 'Tipo Factor', A.INSTRUADMINIS as 'Instrucciones',dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TIPOFACTOR,she.DoseMaximum,@SCT,@PesoPaciente,@IMC) As 'Maxima Dosis', 
					IIF(she.Dose IS NOT NULL,
								dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TIPOFACTOR,she.Dose,@SCT,@PesoPaciente,@IMC),
								(select top 1 tmpOM.DOSISTEORICA from EHR.HCORDMEDICAM as tmpOM 
										WHERE tmpOM.IDHCORDQUIMIO = cabe.id and tmpOM.CODPRODUC = A.CODPRODUC   and tmpOM.CICLO = @CicloAutorizar order by tmpOM.id asc )) As Dosis,
					--dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TIPOFACTOR,isnull(she.Dose,A.DOSIS),@SCT,@PesoPaciente,@IMC) As Dosis,
					(Select count(1) from [dbo].[SplitString](A.DIA)) As 'Cantidad Dias', A.CODDILUYENTE as 'DiluentDrugCode', A.VOLUMENFINAL as 'FinalVolume',A.INDICE AS 'Indice', convert(numeric(18,2), 0) as DosisReal,convert(numeric(18,2), isnull(she.Dose,A.DOSIS)) as DosisTeorica,concat(isnull(she.Dose,A.DOSIS),' ', Rtrim(ABRUNIMED)) as 'DosisTeoricaText', a.CANTIDADDILU as CantidadDiluyente,
					ent.Name AS 'Nombre ATC',atc.ATCEntityId as 'IdATCEntity',FORM.DESFORMED AS 'Nombre Forma',convert(int, 0) as CantidadMedicamentoDia,convert(numeric(18,2), 0 ) as DosisPresentacion,convert(int, 0) as CantidadMedicamentoCiclo,
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
					end as 'Unidad Medidad Medicamento',A.IDHCORDQUIMIO, B.TODASPATO,she.HomeAdministration
					, atc.Conditioned, UNIRS = CASE WHEN atc.UNIRS = 1 THEN 'Si' ELSE 'No' END
					, PBS = CASE WHEN atc.Conditioned = 1 THEN 'Condicionado'
						WHEN B.NOPOSPROD = 1 THEN 'No'
						ELSE 'Si'
						END
						, A.DESCRIPCIONDIA AS 'DescriptionDays', A.ProfessionalModification, A.CICLO + 1 As 'CICLO', IIF(A.State IS NULL, 1, IIF(A.State = 4, 4, cast(1 As int))) as Estado
						, dbo.MedicationAvailability(B.CODPRODUC, @CentroAtencion) as Disponibles
				From [EHR].[HCORMEDICAMESQUEMA]  A
					INNER JOIN EHR.HCORDQUIMIO Cabe ON Cabe.ID = A.IDHCORDQUIMIO
					INNER JOIN EHR.Schemes Schemes on Schemes.Id = Cabe.SchemesId  
	    			LEFT JOIN EHR.SchemesDrugs she ON she.DrugCode = A.CODPRODUC and she.SchemesId = @IdEsquema 
					INNER JOIN EHR.SchamesDrugsRelation RELA ON she.Id = RELA.IdSchemesDrugs 
					INNER JOIN IHLISTPRO B ON RELA.DrugCode = B.CODPRODUC
					INNER JOIN HCVIAADMI C ON A.CODVIAADM = C.CODVIAADM 
					INNER JOIN INUNIMEDI D ON A.CODUNIMED = D.CODUNIMED  
					INNER JOIN IHFORMEDI FORM ON FORM.CODFORMED = B.CODFORMED              
					INNER JOIN Inventory.ATC atc ON A.CODPRODUC = atc.Code              
					INNER JOIN Inventory.ATCEntity ent ON atc.ATCEntityId = ent.ID     
					left Join IHLISTPRO Dil on A.CODDILUYENTE = Dil.CODPRODUC  
			where A.SchemesId = @IdEsquema and A.CICLO = @CicloAutorizar AND ESTADO IN (1,2) and cabe.IPCODPACI = @INDPaciente and A.State <> 4	 	   
end
else if @ListarFrecuenciaDias = 1 begin -- Listar Frecuencia
		Select  distinct
			z.IDHCORDQUIMIO,
			A.id as SchemesDrugsID,
			fre.ID,
			isnull(A.TypePrescription,1) As TypePrescription,
			A.SchemesId,
			she.Description as 'Nombre Esquema',
			fre.Day,
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,fre.Dose1,@SCT,@PesoPaciente,@IMC)) As Dosis1, --Dosis Real o calculada
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,fre.Dose2,@SCT,@PesoPaciente,@IMC)) As Dosis2, --Dosis Real o calculada
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,fre.Dose3,@SCT,@PesoPaciente,@IMC)) As Dosis3, --Dosis Real o calculada
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,fre.Dose4,@SCT,@PesoPaciente,@IMC)) As Dosis4, --Dosis Real o calculada
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,fre.Dose5,@SCT,@PesoPaciente,@IMC)) As Dosis5, --Dosis Real o calculada					
			fre.Dose1 as DOSISPAR1, --Dosis parametrizada
			fre.Dose2 as DOSISPAR2, --Dosis parametrizada
			fre.Dose3 as DOSISPAR3, --Dosis parametrizada
			fre.Dose4 as DOSISPAR4, --Dosis parametrizada
			fre.Dose5 as DOSISPAR5, --Dosis parametrizada
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
			A.InstructionsAdministration as 'Instrucciones',
			Indice,
			convert(numeric(18,2), A.Dose ) as DosisTeorica,
			concat(convert(numeric(18,2), fre.Dose1 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText1',
			concat(convert(numeric(18,2), fre.Dose2 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText2',
			concat(convert(numeric(18,2), fre.Dose3 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText3',
			concat(convert(numeric(18,2), fre.Dose4 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText4',
			concat(convert(numeric(18,2), fre.Dose5 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText5',
			DiluentDrugCode,
			FinalVolume,
			A.QuantityDiluent as CantidadDiluyente,
			B.TIPFORMED,B.CODGRUFAR,
			B.CODFORMED,
			dbo.[CalcularConcentracionMedicamentoEsquemasOncologicos](B.TIPFORMED,B.PESTOTMED,B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,A.MeasurementUnit) as 'Concentracion',
			B.PESTOTMED,B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,B.CODUNIADM,B.CALCANAUT, 
			A.CostMinimumUnitMeasure,
			A.HomeAdministration,
			convert(int, 0) as CantidadMedicamentoDia,convert(numeric(18,2), 0 ) as DosisPresentacion,convert(int, 0) as CantidadMedicamentoCiclo, Z.CICLO + 1 As 'CICLO', IIF(z.State IS NULL, 1, IIF(z.State = 4, 4, cast(1 As int))) as Estado, z.ProfessionalModification,
			dbo.MedicationAvailability(B.CODPRODUC, @CentroAtencion) as Disponibles
		From [EHR].[HCORDFRECUEMED] z
			inner join EHR.HCORDQUIMIO X ON X.ID = Z.IDHCORDQUIMIO and Z.SchemesId = X.SchemesId
		    INNER join [EHR].SchemesDrugs A on z.CODPRODUC = A.DrugCode 
			INNER JOIN EHR.SchemesDrugsFrequency fre ON A.Id = fre.SchemesDrugsID 
			INNER JOIN EHR.Schemes she ON she.Id = a.SchemesId 
			INNER JOIN IHLISTPRO B ON A.DrugCode = B.CODPRODUC
			INNER JOIN HCVIAADMI C ON A.RouteOfAdministration = C.CODVIAADM 
			INNER JOIN INUNIMEDI D ON A.MeasurementUnit = D.CODUNIMED  
		    INNER JOIN IHFORMEDI FORM ON FORM.CODFORMED = B.CODFORMED              
			INNER JOIN Inventory.ATC atc ON A.DrugCode = atc.Code              
			INNER JOIN Inventory.ATCEntity ent ON atc.ATCEntityId = ent.ID     
			left Join IHLISTPRO Dil on A.DiluentDrugCode = Dil.CODPRODUC  
		where 
			A.SchemesID = @IdEsquema 
			and z.schemesID = @IdEsquema 
			and Z.CICLO = @CicloAutorizar
			and X.ESTADO IN (1,2) 
			and X.IPCODPACI = @INDPaciente
			and z.State <> 4
	union all --- Consultar los medicamentos que fueron agregados como nuevos en la HC es decir no estan parametriazados en esquemas
		    Select  distinct 
			z.IDHCORDQUIMIO,
			atc.Id as SchemesDrugsID,
			0 as ID,
			2 As TypePrescription, --Frecuencia
			z.SchemesId,
			she.Description as 'Nombre Esquema',
			z.DIA,
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (X.TIPOFACTOR,Z.DOSISPAR1,@SCT,@PesoPaciente,@IMC)) As Dosis1, --Dosis Real o calculada
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (X.TIPOFACTOR,Z.DOSISPAR2,@SCT,@PesoPaciente,@IMC)) As Dosis2, --Dosis Real o calculada
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (X.TIPOFACTOR,Z.DOSISPAR3,@SCT,@PesoPaciente,@IMC)) As Dosis3, --Dosis Real o calculada
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (X.TIPOFACTOR,Z.DOSISPAR4,@SCT,@PesoPaciente,@IMC)) As Dosis4, --Dosis Real o calculada
			convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (X.TIPOFACTOR,Z.DOSISPAR5,@SCT,@PesoPaciente,@IMC)) As Dosis5, --Dosis Real o calculada	
			Z.DOSISPAR1, --Dosis parametrizada
			Z.DOSISPAR2, --Dosis parametrizada
			Z.DOSISPAR3, --Dosis parametrizada
			Z.DOSISPAR4, --Dosis parametrizada
			Z.DOSISPAR5, --Dosis parametrizada
			convert(char(5),Z.HORA1, 108) as Hour1,
			convert(char(5),Z.HORA2, 108) as Hour2,
			convert(char(5),Z.HORA3, 108) as Hour3,
			convert(char(5),Z.HORA4, 108) as Hour4,
			convert(char(5),Z.HORA5, 108) as Hour5,
			ent.Name AS 'Nombre ATC',
			atc.ATCEntityId as 'IdATCEntity',
			Rtrim(B.CODPRODUC) 'Codigo',
			Rtrim(B.DESPRODUC) As 'Medicamento',
			FORM.DESFORMED AS 'Nombre Forma',
			Rtrim(C.CODVIAADM) As 'Codigo Via',
			Rtrim(X.CODUNIMED) As 'Codigo Unidad Medida',
			X.TIPOFACTOR as 'Tipo Factor',
			X.INSTRUADMINIS as 'Instrucciones',
			Indice,
			convert(numeric(18,2), X.DOSIS ) as DosisTeorica,
			concat(convert(numeric(18,2), Z.DOSISPAR1 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText1',
			concat(convert(numeric(18,2), Z.DOSISPAR2 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText2',
			concat(convert(numeric(18,2), Z.DOSISPAR3 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText3',
			concat(convert(numeric(18,2), Z.DOSISPAR4 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText4',
			concat(convert(numeric(18,2), Z.DOSISPAR5 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText5',
			X.CODDILUYENTE,
			X.VOLUMENFINAL,
			X.CANTIDADDILU as CantidadDiluyente,
			B.TIPFORMED,B.CODGRUFAR,
			B.CODFORMED,
			dbo.[CalcularConcentracionMedicamentoEsquemasOncologicos](B.TIPFORMED,B.PESTOTMED,B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,X.CODUNIMED) as 'Concentracion',
			B.PESTOTMED,B.CODUNIPES,B.VOLTOTMED,B.CODUNIVOL,B.CODUNIADM,B.CALCANAUT, 
			0 as CostMinimumUnitMeasure,
			2 as HomeAdministration,
			convert(int, 0) as CantidadMedicamentoDia,convert(numeric(18,2), 0 ) as DosisPresentacion,convert(int, 0) as CantidadMedicamentoCiclo, Z.CICLO + 1 As 'CICLO', IIF(z.State IS NULL, 1, IIF(z.State = 4, 4, cast(1 As int))) as Estado, z.ProfessionalModification,
			dbo.MedicationAvailability(B.CODPRODUC, @CentroAtencion) as Disponibles
		From [EHR].[HCORDFRECUEMED] z
			inner join EHR.HCORDQUIMIO R ON R.ID = Z.IDHCORDQUIMIO 
            INNER JOIN EHR.HCORMEDICAMESQUEMA X ON X.IDHCORDQUIMIO = z.IDHCORDQUIMIO	and z.CODPRODUC = X.CODPRODUC and z.SchemesId = X.SchemesId AND X.CICLO = @CicloAutorizar
 			INNER JOIN EHR.Schemes she ON she.Id = Z.SchemesId 
			INNER JOIN IHLISTPRO B ON Z.CODPRODUC = B.CODPRODUC
			INNER JOIN HCVIAADMI C ON X.CODVIAADM = C.CODVIAADM 
			INNER JOIN INUNIMEDI D ON X.CODUNIMED = D.CODUNIMED  
		    INNER JOIN IHFORMEDI FORM ON FORM.CODFORMED = B.CODFORMED              
			INNER JOIN Inventory.ATC atc ON z.CODPRODUC = atc.Code              
			INNER JOIN Inventory.ATCEntity ent ON atc.ATCEntityId = ent.ID     
			left Join IHLISTPRO Dil on X.CODDILUYENTE = Dil.CODPRODUC  
		WHERE 
			Z.CODPRODUC NOT IN (SELECT A.DrugCode  FROM EHR.SchemesDrugs A INNER JOIN EHR.SchemesDrugsFrequency B ON A.Id = B.SchemesDrugsID AND A.SchemesId = @IdEsquema ) 
			and z.SchemesId = @IdEsquema
			and Z.CICLO = @CicloAutorizar
			and R.ESTADO IN (1,2) 
			and R.IPCODPACI = @INDPaciente
			and z.State <> 4	 
	end
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos de un ciclo de quimioterapia que están pendientes de autorización para un paciente específico. Combina la información del esquema oncológico (Schemes y SchemesDrugs) con el detalle de medicamentos registrados en la historia clínica del ciclo (HCORMEDICAMESQUEMA y HCORDQUIMIO), enriqueciendo cada fármaco con su nombre comercial, vía de administración, forma farmacéutica, unidad de medida, clasificación ATC y disponibilidad en el centro de atención. Calcula la dosis teórica según el tipo de factor (peso, IMC o superficie corporal total del paciente) y determina si el medicamento es POS, condicionado o No POS, incluyendo también los medicamentos relacionados o complementarios del esquema. Se utiliza en el módulo de oncología para presentar al autorizador o farmacéutico el listado completo de fármacos que requieren aprobación antes de preparar y dispensar un ciclo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos del siguiente ciclo de quimioterapia a autorizar para un paciente, presentando dosis teóricas/máximas calculadas, concentración, disponibilidad y datos del esquema, en modalidad por días o por frecuencia horaria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de quimioterapia (HCORDQUIMIO) del paciente debe existir con ESTADO en (1,2); Deben existir registros de medicamentos del esquema (HCORMEDICAMESQUEMA) o de frecuencia (HCORDFRECUEMED) para el ciclo indicado; Los medicamentos no deben estar en estado 4 (anulado/cancelado); El paciente (IPCODPACI) debe coincidir con la orden de quimioterapia; Para listado por días: la cabecera del esquema debe tener registros vigentes para el ciclo a autorizar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El ciclo retornado siempre es el ciclo actual + 1 (próximo ciclo a autorizar); Solo se incluyen órdenes de quimioterapia con ESTADO 1 o 2; Nunca se incluyen registros con State = 4 (anulados); La dosis se calcula con la función ValorDosisMedicamentosEsquemasOncologicos según TipoFactor, peso, IMC y SCT del paciente; En la rama por días, los medicamentos con relación en SchamesDrugsRelation se excluyen del primer bloque y se traen separadamente desde la relación; En la rama de frecuencia, los medicamentos no parametrizados en el esquema se listan aparte con TypePrescription=2; La disponibilidad del medicamento siempre se evalúa contra el centro de atención recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Esquema oncológico; Ciclo de quimioterapia; Dosis teórica; Dosis máxima; Superficie corporal total (SCT); Índice de masa corporal (IMC); Peso del paciente; Vía de administración; Diluyente; Volumen final; Concentración de medicamento; Clasificación ATC; Plan de Beneficios en Salud (PBS / NO POS); Administración en casa; Frecuencia horaria de administración; Medicamento condicionado; Justificación de medicamentos especiales; Disponibilidad de medicamento por centro de atención; Autorización de ciclo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando @ListarFrecuenciaDias=0: devuelve medicamentos del esquema por días (CICLO+1) excluyendo los que tienen relación en SchamesDrugsRelation y exigiendo que existan en HCORDMEDICAM del ciclo a autorizar; unido con los medicamentos relacionados desde SchamesDrugsRelation; [RETURN_RESULT] RESULTSET: Cuando @ListarFrecuenciaDias=1: devuelve medicamentos con frecuencia horaria (hasta 5 dosis/horas) provenientes de HCORDFRECUEMED unidos a SchemesDrugsFrequency, más los medicamentos agregados en HC que no están parametrizados en el esquema (NOT IN SchemesDrugs)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ListarFrecuenciaDias = 0 → Lista medicamentos del esquema por días: primer SELECT con medicamentos del esquema sin relación + UNION con medicamentos relacionados (SchamesDrugsRelation) else Si @ListarFrecuenciaDias = 1, lista medicamentos por frecuencia horaria; si she.Dose IS NOT NULL → Calcula la dosis usando ValorDosisMedicamentosEsquemasOncologicos sobre la dosis parametrizada del esquema (SchemesDrugs) else Toma DOSISTEORICA del primer registro de HCORDMEDICAM del ciclo a autorizar; si atc.Conditioned = 1 → PBS = ''Condicionado'' else Si NOPOSPROD=1 PBS=''No'', en otro caso PBS=''Si''; si B.TIPFORMED in (1,2,3,4) → Selecciona la unidad de medida del medicamento según el tipo de forma (peso, volumen, ambos o administración); si A.State IS NULL → Estado=1 else Si State=4 Estado=4, en otro caso Estado=1; si she.HomeAdministration IS NULL (rama por días) → Toma MEDICAMENTOENCASA desde EHR.HCORMEDICAM del ciclo a autorizar else Usa el valor parametrizado en SchemesDrugs.HomeAdministration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ValorDosisMedicamentosEsquemasOncologicos; dbo.CalcularConcentracionMedicamento; dbo.CalcularConcentracionMedicamentoEsquemasOncologicos; dbo.MedicationAvailability; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORMEDICAMESQUEMA; EHR.HCORDQUIMIO; EHR.Schemes; EHR.SchemesDrugs; EHR.SchamesDrugsRelation; EHR.HCORDMEDICAM; EHR.HCORDFRECUEMED; EHR.SchemesDrugsFrequency; dbo.IHLISTPRO; dbo.HCVIAADMI; dbo.INUNIMEDI; dbo.IHFORMEDI; Inventory.ATC; Inventory.ATCEntity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorAutorizar';
-- GO
