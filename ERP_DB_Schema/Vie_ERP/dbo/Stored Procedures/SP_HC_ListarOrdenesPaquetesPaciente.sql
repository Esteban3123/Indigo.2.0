

CREATE PROCEDURE [dbo].[SP_HC_ListarOrdenesPaquetesPaciente] 
(
@DiagnosticosPaciente varchar(MAX) ,
@TipoServicio VARCHAR(20),
@ProcedimientosQx varchar(MAX) ,
@ProcedimientosNoQx varchar(MAX),
@IDDESCRIPCIONRELACIONADAQx varchar(MAX) ,
@IDDESCRIPCIONRELACIONADANoQx varchar(MAX) 
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT distinct  Convert(BIT,0) As 'Seleccion',Rtrim(C.CODIGOSERVICIO) as 'CodigoServicio',D.SERIPSDASH,D.SERREASIT as 'ServicioRealizaSitio', CODDCIMED AS 'DCI', C.IDDESCRIPCIONRELACIONADA, Descriptions.Code + ' - ' + Descriptions.Name as DescripcionRelacionada,
		/*Rtrim(ISNULL(D.DESSERIPS ,E.DESPRODUC)) AS 'Nombre Servicio'*/ CASE WHEN TIPOSERVICIO  IN (1,2,3,4,5) THEN RTRIM(D.DESSERIPS) WHEN TIPOSERVICIO IN (6,8) THEN RTRIM(E.DESPRODUC) ELSE RTRIM(F.DESESPECI) END AS 'Nombre Servicio'  ,case TIPOSERVICIO when 1 Then 'Laboratorios' when 2 then 'Imágenes Dx' when 3 then 'Procedimientos Qx' when 4 then 'Procedimientos no Qx' when 5 then 'Patologías' when 6 then 'Medicamentos' when 7 then 'Interconsultas' when 8 then 'Insumos' end as 'Descripcion Tipo Orden',C.TIPOSERVICIO AS 'Tipo Servicio'
	FROM HCPAQORDENESC A 
		INNER JOIN  HCPAQORDENESD C ON A.ID = C.IDHCPAQORDENESC 
		LEFT JOIN  HCPAQDIAGNOSD B ON A.ID = B.IDHCPAQORDENESC 
		LEFT JOIN	INCUPSIPS d ON C.CODIGOSERVICIO = D.CODSERIPS
		LEFT JOIN	IHLISTPRO E ON C.CODIGOSERVICIO = E.CODPRODUC
		LEFT JOIN	INESPECIA F ON C.CODIGOSERVICIO = F.CODESPECI 
		left join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  CupsDescriptions.Id = C.IDDESCRIPCIONRELACIONADA 
		left join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId
		left join HCPAQORDENPROQXD G0 ON A.ID = G0.IDHCPAQORDENESC 
		left Join INCUPSIPS G1 ON G1.CODSERIPS = G0.CODIGOSERVICIO
		left join contract.CUPSEntityContractDescriptions G2 with(nolock) on  G2.Id = G0.IDDESCRIPCIONRELACIONADA 
		left join contract.ContractDescriptions G3 with(nolock) on G3.id = G2.ContractDescriptionId
		left join HCPAQORDENPRONOQXD H0 ON A.ID = H0.IDHCPAQORDENESC 
		left Join INCUPSIPS H1 ON H1.CODSERIPS = H0.CODIGOSERVICIO
		left join contract.CUPSEntityContractDescriptions H2 with(nolock) on  H2.Id = H0.IDDESCRIPCIONRELACIONADA 
		left join contract.ContractDescriptions H3 with(nolock) on H3.id = H2.ContractDescriptionId
		
	where 
	A.ESTADO = 1 
	AND	C.TIPOSERVICIO in  (SELECT Value FROM dbo.splitstring(@TipoServicio))
	AND ( 	B.CODDIAGNO in  (SELECT Value FROM dbo.splitstring(@DiagnosticosPaciente)) 
			Or ( G0.CODIGOSERVICIO in  (SELECT Value FROM dbo.splitstring(@ProcedimientosQx))  AND (G0.IDDESCRIPCIONRELACIONADA IS NULL OR G0.IDDESCRIPCIONRELACIONADA in  (SELECT Value FROM dbo.splitstring(@IDDESCRIPCIONRELACIONADAQx)) ) )
			Or ( H0.CODIGOSERVICIO in  (SELECT Value FROM dbo.splitstring(@ProcedimientosNoQx))  AND (H0.IDDESCRIPCIONRELACIONADA IS NULL OR H0.IDDESCRIPCIONRELACIONADA in  (SELECT Value FROM dbo.splitstring(@IDDESCRIPCIONRELACIONADANoQx)) ) )
		)

	order by  C.TIPOSERVICIO asc

END