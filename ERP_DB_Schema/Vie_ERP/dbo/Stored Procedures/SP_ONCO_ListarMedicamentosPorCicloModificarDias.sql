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
						IIF(she.Dose IS NOT NULL,
							dbo.[ValorDosisMedicamentosEsquemasOncologicos] (A.TIPOFACTOR,she.Dose,@SCT,@PesoPaciente,@IMC),
							(select top 1 tmpOM.DOSISTEORICA from EHR.HCORDMEDICAM as tmpOM 
							WHERE tmpOM.IDHCORDQUIMIO = cabe.id and tmpOM.CODPRODUC = A.CODPRODUC   and tmpOM.CICLO = @CicloModificar order by tmpOM.id asc )
						) As DOSISPROD, A.CODUNIMED, A.TIPOFACTOR,A.CANTIDAD,A.INDICE,A.DOSISTEORICA,A.INSTRUADMINIS,A.CODDILUYENTE,A.VOLUMENFINAL,A.CANTIDADDILU,A.MEDICAMENTOENCASA,
					   EstadoDia = (select distinct case z.ESTADODIA WHEN 1 then 'Días pendientes de aplicación' when 2 then 'Días aplicados' end as 'Estado día' from ehr.HCORDCICLOSD z where z.IDHCORDQUIMIO = @IDHCORDQUIMIO and z.CICLO = @CicloModificar and z.DIA = a.DIA),
					   CodigoEstado= (select distinct z.ESTADODIA  from ehr.HCORDCICLOSD z where z.IDHCORDQUIMIO = @IDHCORDQUIMIO and z.CICLO = @CicloModificar and z.DIA = a.DIA)
					   , I.TIPFORMED,I.CODUNIPES, I.CODUNIVOL,I.CODUNIADM,I.PESTOTMED, I.VOLTOTMED, I.CALCANAUT, U.DESUNIMED AS UnidaddeMedida, IIF(A.State IS NULL, 1, IIF(A.State = 4, 4, cast(1 As int))) as Estado, '' as Acciones, I.CODFORMED, convert(int, 0) as CantidadMedicamentoDia, convert(numeric(18,2), 0 ) as DosisPresentacion,
					   convert(bit, 0) as DiaFrecuencia, dbo.[CalcularConcentracionMedicamentoEsquemasOncologicos](I.TIPFORMED,I.PESTOTMED,I.CODUNIPES,I.VOLTOTMED,I.CODUNIVOL,A.CODUNIMED) as 'Concentracion', I.DESPRODUC,
					   CostMinimumUnitMeasure = (select Top 1 CostMinimumUnitMeasure from EHR.SchemesDrugs she WHERE she.SchemesId = @IdEsquema AND she.DrugCode = A.CODPRODUC), A.ProfessionalModification, A.DateModification,
					   IdSchemeDrug = (SELECT ID FROM EHR.SchemesDrugs SD WHERE SD.SchemesId = @IdEsquema AND SD.DrugCode = A.CODPRODUC AND SD.Dose = A.DOSISTEORICA),
					   TypePrescription = (SELECT TypePrescription FROM EHR.SchemesDrugs SD WHERE SD.SchemesId = @IdEsquema AND SD.DrugCode = A.CODPRODUC AND SD.Dose = A.DOSISTEORICA),
					   A.DateModification AS FechaModificacion, RTRIM(P.NOMMEDICO) AS UsuarioModifica
    		  from  [EHR].HCORDMEDICAM  A
					LEFT JOIN dbo.INPROFSAL P ON A.ProfessionalModification = P.CODPROSAL
					Inner Join EHR.HCORDQUIMIO Cabe ON Cabe.ID = A.IDHCORDQUIMIO AND Cabe.IPCODPACI = @INDPaciente and cabe.ESTADO IN (1,2) 	
					Inner Join EHR.Schemes Schemes on Schemes.Id = Cabe.SchemesId 
					left JOIN EHR.SchemesDrugs she ON she.DrugCode = A.CODPRODUC and she.SchemesId = @IdEsquema
					INNER JOIN ehr.HCORMEDICAMESQUEMA B ON A.IDHCORDQUIMIO = B.IDHCORDQUIMIO and A.SchemesId = B.SchemesId and A.CICLO = B.CICLO and A.CODPRODUC = B.CODPRODUC AND A.State <> 4 AND B.State <> 4 --AND she.TypePrescription = B.TypePrescription  
					Inner Join INUNIMEDI U ON u.CODUNIMED = A.CODUNIMED
					Inner Join IHLISTPRO I ON A.CODPRODUC = I.CODPRODUC
		     where   A.IDHCORDQUIMIO = @IDHCORDQUIMIO
						and a.SchemesId = @IdEsquema	
						and A.CICLO = @CicloModificar
						and cabe.ESTADO IN (1,2) 
						and cabe.IPCODPACI = @INDPaciente				 
			order by a.DIA ASC
end