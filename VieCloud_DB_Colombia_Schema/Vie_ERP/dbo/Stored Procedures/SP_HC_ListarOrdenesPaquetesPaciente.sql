

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los servicios y procedimientos ordenados en paquetes de atención del paciente dentro de la historia clínica, filtrando por diagnósticos CIE-10, tipo de servicio, procedimientos quirúrgicos y no quirúrgicos. Combina el encabezado de la orden con su detalle de ítems (laboratorios, imágenes diagnósticas, procedimientos Qx y no Qx, medicamentos, insumos e interconsultas), enriqueciendo cada ítem con el nombre del servicio CUPS, el producto farmacéutico o la especialidad médica según corresponda. También incorpora la descripción del contrato asociada al servicio, permitiendo identificar el concepto de facturación o cobro aplicable. Se usa para presentar al usuario clínico o administrativo el conjunto de órdenes activas de un paquete, habilitando la selección de ítems para su procesamiento, prescripción o facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los detalles de paquetes de órdenes clínicas activos aplicables a un paciente, filtrando por tipo de servicio y por coincidencia con sus diagnósticos o con procedimientos quirúrgicos/no quirúrgicos del paquete.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las listas de diagnósticos, procedimientos Qx/no Qx, descripciones relacionadas y tipos de servicio deben venir como cadenas delimitadas compatibles con dbo.splitstring.; Deben existir paquetes (HCPAQORDENESC) en estado activo (ESTADO=1) para retornar resultados.; Los códigos de servicio deben existir en INCUPSIPS, IHLISTPRO o INESPECIA según el tipo de servicio para resolver el nombre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran paquetes activos (ESTADO = 1 en el encabezado).; Se filtra siempre por los tipos de servicio recibidos en parámetro.; El resultado se entrega sin duplicados (DISTINCT) y con la columna ''Seleccion'' inicializada en 0.; Un detalle se incluye si coincide al menos uno: diagnóstico, procedimiento Qx o procedimiento no Qx del paquete.; Cuando el procedimiento del paquete no tiene IDDESCRIPCIONRELACIONADA, se considera coincidencia sin exigir descripción contractual.; El resultado se ordena por TIPOSERVICIO ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paquete de órdenes clínicas; Diagnóstico del paciente; Procedimientos quirúrgicos; Procedimientos no quirúrgicos; Tipo de servicio (Laboratorios, Imágenes Dx, Patologías, Medicamentos, Interconsultas, Insumos); CUPS; Descripción contractual del servicio; DCI (medicamento)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando A.ESTADO=1 y TIPOSERVICIO está en la lista parametrizada y se cumple alguna condición de diagnóstico, procedimiento Qx o procedimiento no Qx, retorna el detalle del paquete con código, DCI, descripción relacionada, nombre del servicio (según tipo) y etiqueta del tipo de orden.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOSERVICIO IN (1,2,3,4,5) → El nombre del servicio se toma de INCUPSIPS (DESSERIPS) else Si TIPOSERVICIO IN (6,8) se toma de IHLISTPRO (DESPRODUC); en otro caso (ej. 7) se toma de INESPECIA (DESESPECI); si TIPOSERVICIO según valor 1..8 → Se etiqueta como Laboratorios/Imágenes Dx/Procedimientos Qx/Procedimientos no Qx/Patologías/Medicamentos/Interconsultas/Insumos respectivamente; si Diagnóstico del paquete coincide con la lista de diagnósticos del paciente → Se incluye el detalle del paquete en el resultado else Se evalúan coincidencias por procedimientos Qx o no Qx asociados al paquete; si Procedimiento Qx del paquete coincide y (IDDESCRIPCIONRELACIONADA es NULL o está en la lista filtrada) → Se incluye el detalle del paquete por coincidencia de procedimiento quirúrgico; si Procedimiento no Qx del paquete coincide y (IDDESCRIPCIONRELACIONADA es NULL o está en la lista filtrada) → Se incluye el detalle del paquete por coincidencia de procedimiento no quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPAQORDENESC; dbo.HCPAQORDENESD; dbo.HCPAQDIAGNOSD; dbo.INCUPSIPS; dbo.IHLISTPRO; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.HCPAQORDENPROQXD; dbo.HCPAQORDENPRONOQXD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarOrdenesPaquetesPaciente';
-- GO
