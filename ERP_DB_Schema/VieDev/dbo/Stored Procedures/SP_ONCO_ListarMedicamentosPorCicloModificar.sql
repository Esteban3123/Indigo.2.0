
CREATE PROCEDURE [dbo].[SP_ONCO_ListarMedicamentosPorCicloModificar]
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

			--Consultamos los medicamentos que se guardaron en el ciclo seleccionado por el medico a modificar
			Select rtrim(inp.IPCODPACI ) as 'IdentificacionPaciente', rtrim(inp.IPNOMCOMP) as 'NombrePaciente',A.CICLO,  isnull(she.TypePrescription,1) As TypePrescription,Schemes.Description as 'Nombre Esquema',atc.Id AS SchemesDrugsID ,0 as 'IdRelacion',0 as CostMinimumUnitMeasure,A.SchemesId, Rtrim(B.CODPRODUC) 'Codigo', Rtrim(B.DESPRODUC) As 'Medicamento', Rtrim(A.CODUNIMED) As 'Codigo Unidad Medida', Rtrim(DESUNIMED) As 'Unidad Medida',Rtrim(C.CODVIAADM) As 'Codigo Via',Rtrim(DESVIAADM) 'Via Administracion' ,A.DIA As 'Dias',B.NOPOSPROD AS 'NO POS',B.TIPFORMED,B.CODGRUFAR,B.CODJUMEES As JustificacionMedicamentosEspeciales,B.CODFORMED,Rtrim(B.CONCENMED) As 'Concentracion 2',B.PESTOTMED,
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
					A.TraceabilityPaperworkEventsId, A.DESCRIPADMIN, A.ProfessionalModification, A.DateModification, A.NUMEFOLIO, A.ID, she.ID As IdSchemeDrug 
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