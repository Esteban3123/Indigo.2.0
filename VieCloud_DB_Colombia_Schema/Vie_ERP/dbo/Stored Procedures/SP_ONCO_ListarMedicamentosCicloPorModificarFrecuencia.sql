

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
	WITH UltimosFrecuencia AS (
    SELECT *
    FROM (
        SELECT *,
               ROW_NUMBER() OVER (
                   PARTITION BY IDHCORDQUIMIO, CAST(DIA AS int), RTRIM(CODPRODUC)
                   ORDER BY Id DESC
               ) AS rn
        FROM EHR.HCORDFRECUEMED
        WHERE SchemesId = @IdEsquema -- (o el filtro que necesites)
          AND CICLO = @CicloModificar
    ) AS sub
    WHERE rn = 1 AND State <> 4
	)
		Select  distinct
			z.IDHCORDQUIMIO,
			A.id as SchemesDrugsID,
			isnull(A.TypePrescription,1) As TypePrescription,
			A.SchemesId,
			she.Description as 'Nombre Esquema',
			Z.DIA as 'Day',
			IIF(z.State IN (2,3), z.DOSIS1, convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,Z.DOSISPAR1,@SCT,@PesoPaciente,@IMC))) As Dosis1, --Dosis Real o calculada
			IIF(z.State IN (2,3), z.DOSIS2, convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,Z.DOSISPAR2,@SCT,@PesoPaciente,@IMC))) As Dosis2, --Dosis Real o calculada
			IIF(z.State IN (2,3), z.DOSIS3, convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,Z.DOSISPAR3,@SCT,@PesoPaciente,@IMC))) As Dosis3, --Dosis Real o calculada
			IIF(z.State IN (2,3), z.DOSIS4, convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,Z.DOSISPAR4,@SCT,@PesoPaciente,@IMC))) As Dosis4, --Dosis Real o calculada
			IIF(z.State IN (2,3), z.DOSIS5, convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (a.TypeFactor,Z.DOSISPAR5,@SCT,@PesoPaciente,@IMC))) As Dosis5, --Dosis Real o calculada						
			Z.DOSISPAR1 as DOSISPAR1, --Dosis parametrizada
			Z.DOSISPAR2 as DOSISPAR2, --Dosis parametrizada
			Z.DOSISPAR3 as DOSISPAR3, --Dosis parametrizada
			Z.DOSISPAR4 as DOSISPAR4, --Dosis parametrizada
			Z.DOSISPAR5 as DOSISPAR5, --Dosis parametrizada
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
			Rtrim(A.MeasurementUnit) As 'Codigo Unidad Medida',
			A.TypeFactor as 'Tipo Factor',
			A.InstructionsAdministration as 'Instrucciones',
			Indice,
			convert(numeric(18,2), A.Dose ) as DosisTeorica,
			concat(convert(numeric(18,2), Z.DOSISPAR1 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText1',
			concat(convert(numeric(18,2), Z.DOSISPAR2 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText2',
			concat(convert(numeric(18,2), Z.DOSISPAR3 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText3',
			concat(convert(numeric(18,2), Z.DOSISPAR4 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText4',
			concat(convert(numeric(18,2), Z.DOSISPAR5 ),' ',  Rtrim(ABRUNIMED)) as 'DosisTeoricaText5',
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
		From UltimosFrecuencia z
			inner join EHR.HCORDQUIMIO X ON X.ID = Z.IDHCORDQUIMIO and Z.SchemesId = X.SchemesId
		    INNER join [EHR].SchemesDrugs A on z.CODPRODUC = A.DrugCode 
			--INNER JOIN EHR.SchemesDrugsFrequency fre ON A.Id = fre.SchemesDrugsID 
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
			2 As TypePrescription, --Frecuencia
			z.SchemesId,
			she.Description as 'Nombre Esquema',
			z.DIA as 'Day',
			IIF(z.State IN (2,3), z.DOSIS1, convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (X.TIPOFACTOR,Z.DOSISPAR1,@SCT,@PesoPaciente,@IMC))) As Dosis1, --Dosis Real o calculada
			IIF(z.State IN (2,3), z.DOSIS2, convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (X.TIPOFACTOR,Z.DOSISPAR2,@SCT,@PesoPaciente,@IMC))) As Dosis2, --Dosis Real o calculada
			IIF(z.State IN (2,3), z.DOSIS3, convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (X.TIPOFACTOR,Z.DOSISPAR3,@SCT,@PesoPaciente,@IMC))) As Dosis3, --Dosis Real o calculada
			IIF(z.State IN (2,3), z.DOSIS4, convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (X.TIPOFACTOR,Z.DOSISPAR4,@SCT,@PesoPaciente,@IMC))) As Dosis4, --Dosis Real o calculada
			IIF(z.State IN (2,3), z.DOSIS5, convert(numeric(18,2), dbo.[ValorDosisMedicamentosEsquemasOncologicos] (X.TIPOFACTOR,Z.DOSISPAR5,@SCT,@PesoPaciente,@IMC))) As Dosis5, --Dosis Real o calculada
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
		From UltimosFrecuencia z
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos correspondientes a un ciclo específico de quimioterapia que requieren modificación de frecuencia de dosificación, para un paciente y esquema oncológico determinados. Combina las frecuencias ya registradas en historia clínica (HCORDFRECUEMED) con los medicamentos del esquema terapéutico parametrizado (SchemesDrugs y Schemes), tomando siempre el registro más reciente por medicamento y día. Para cada fármaco devuelve hasta cinco dosis y horarios de administración, calculando las dosis reales según el peso, IMC y superficie corporal total del paciente cuando aún no han sido confirmadas, o usando las dosis ya registradas cuando el estado indica que fueron modificadas o aprobadas. Incluye también medicamentos adicionales incorporados directamente en la historia clínica que no forman parte del esquema original, enriqueciendo la información con nombre comercial del producto (IHLISTPRO), vía de administración (HCVIAADMI), unidad de medida (INUNIMEDI), clasificación ATC y datos del diluyente, para ser usado por el módulo de oncología al momento de ajustar o confirmar la prescripción de un ciclo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos de un ciclo de quimioterapia para modificar su frecuencia, devolviendo dosis reales o calculadas (según factor antropométrico) tanto para medicamentos parametrizados en el esquema como para los agregados ad hoc en la historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de quimioterapia (HCORDQUIMIO) debe estar en estado 1 o 2; La orden debe pertenecer al paciente indicado (IPCODPACI = @INDPaciente); Deben existir registros en HCORDFRECUEMED para el esquema y ciclo solicitados; Los medicamentos deben tener correspondencia con el catálogo ATC y demás catálogos (IHLISTPRO, HCVIAADMI, INUNIMEDI, IHFORMEDI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven medicamentos asociados a órdenes de quimioterapia activas (estado 1 o 2); Solo se devuelve la última versión por (orden, día, producto) según mayor Id; Las frecuencias en estado 4 (anuladas) nunca aparecen en el resultado; Los medicamentos parametrizados (rama 1) y no parametrizados (rama 2) son mutuamente excluyentes vía NOT IN sobre SchemesDrugs+SchemesDrugsFrequency; La dosis calculada depende del tipo de factor (peso, SCT, IMC) según dbo.ValorDosisMedicamentosEsquemasOncologicos; La concentración del medicamento se calcula según forma, peso, volumen y unidades vía dbo.CalcularConcentracionMedicamentoEsquemasOncologicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Esquema oncológico; Ciclo de quimioterapia; Medicamento; Dosis real vs dosis calculada; Dosis parametrizada (teórica); Frecuencia de administración; Peso del paciente; Índice de masa corporal (IMC); Superficie corporal total (SCT); Tipo de factor de dosificación; Vía de administración; Unidad de medida; Forma farmacéutica; Clasificación ATC; Diluyente; Concentración del medicamento; Paciente; Orden de quimioterapia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Retorna por cada medicamento del ciclo la dosis: si State IN (2,3) usa la dosis real almacenada (DOSIS1..5), de lo contrario calcula la dosis vía dbo.ValorDosisMedicamentosEsquemasOncologicos usando TypeFactor, dosis parametrizada, SCT, peso e IMC; [RETURN_RESULT] RESULTSET: El Estado devuelto es 4 si z.State=4, 1 si z.State es NULL, y 1 en cualquier otro caso; [RETURN_RESULT] RESULTSET: Para medicamentos no parametrizados en el esquema (CODPRODUC NOT IN SchemesDrugs+SchemesDrugsFrequency del esquema), se toman los datos desde HCORMEDICAMESQUEMA y se marca TypePrescription=2 (Frecuencia), HomeAdministration=2 y CostMinimumUnitMeasure=0; [RETURN_RESULT] RESULTSET: Solo se considera la última frecuencia por combinación (IDHCORDQUIMIO, DIA, CODPRODUC) ordenada por Id DESC y se excluyen las que tienen State=4', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rn = 1 AND State <> 4 en HCORDFRECUEMED → Se toma como última frecuencia vigente del medicamento en el día y ciclo else Se descarta la fila; si z.State IN (2,3) → Se devuelve la dosis real almacenada (DOSIS1..5) else Se calcula la dosis con dbo.ValorDosisMedicamentosEsquemasOncologicos a partir de la dosis parametrizada y datos antropométricos; si z.State IS NULL → Estado = 1 else Si z.State=4 entonces Estado=4, en otro caso Estado=1; si Z.CODPRODUC NOT IN (SchemesDrugs JOIN SchemesDrugsFrequency del esquema) → El medicamento se trata como agregado ad hoc en la HC y se obtienen sus datos de HCORMEDICAMESQUEMA else Se obtiene desde la parametrización del esquema (SchemesDrugs); si X.ESTADO IN (1,2) y X.IPCODPACI = @INDPaciente → Se incluye la orden de quimioterapia del paciente else Se excluye', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ValorDosisMedicamentosEsquemasOncologicos; dbo.CalcularConcentracionMedicamentoEsquemasOncologicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDFRECUEMED; EHR.HCORDQUIMIO; EHR.SchemesDrugs; EHR.SchemesDrugsFrequency; EHR.Schemes; EHR.HCORMEDICAMESQUEMA; IHLISTPRO; HCVIAADMI; INUNIMEDI; IHFORMEDI; Inventory.ATC; Inventory.ATCEntity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarMedicamentosCicloPorModificarFrecuencia';
-- GO
