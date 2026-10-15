

CREATE PROCEDURE [dbo].[SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia]
(
					@IdEsquema as INT,							--ID esquema Oncologico
					@PesoPaciente as numeric(18,2) = 0,			--Peso del paciente
					@IMC as numeric(18,2) = 0,					--Indice de Masa Corporal
					@SCT as numeric(18,2)= 0,					--Superficie Corporal Total
					@CicloModificar as Integer,
					@INDPaciente as varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

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
			convert(int, 0) as CantidadMedicamentoDia,convert(numeric(18,2), 0 ) as DosisPresentacion,convert(int, 0) as CantidadMedicamentoCiclo,
			D.DESUNIMED AS UnidadMedida, IIF(z.State IS NULL, 1, IIF(z.State = 4, 4, cast(1 As int))) as Estado, z.ID, z.ProfessionalModification, z.DateModification, z.CICLO, z.ID As IdHCORDFRECUEMED
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
			and Z.CICLO = @CicloModificar
			and X.ESTADO IN (1,2) 
			and X.IPCODPACI = @INDPaciente
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
			convert(int, 0) as CantidadMedicamentoDia,convert(numeric(18,2), 0 ) as DosisPresentacion,convert(int, 0) as CantidadMedicamentoCiclo,
			D.DESUNIMED AS UnidadMedida, IIF(z.State IS NULL, 1, IIF(z.State = 4, 4, cast(1 As int))) as Estado, z.ID, z.ProfessionalModification, z.DateModification, z.CICLO, z.ID As IdHCORDFRECUEMED
		From [EHR].[HCORDFRECUEMED] z
			inner join EHR.HCORDQUIMIO R ON R.ID = Z.IDHCORDQUIMIO 
            INNER JOIN EHR.HCORMEDICAMESQUEMA X ON X.IDHCORDQUIMIO = z.IDHCORDQUIMIO	and z.CODPRODUC = X.CODPRODUC and z.SchemesId = X.SchemesId AND X.CICLO = @CicloModificar
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
			and Z.CICLO = @CicloModificar
			and R.ESTADO IN (1,2) 
			and R.IPCODPACI = @INDPaciente
end