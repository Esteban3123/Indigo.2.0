
CREATE PROCEDURE [dbo].[SPHC_ListarProcedimientoTerapiasRealizadas]
(
      -- Add the parameters for the stored procedure here
	@Identificacion as varchar(25),
	@Ingreso as varchar(25)
)
AS
BEGIN

SELECT 
	ROW_NUMBER() OVER(ORDER BY Codigo) AS Id, * 
FROM
(

				SELECT * FROM ( 
					SELECT 	
							'Pendientes a realizar' AS Terapias,
							A.CODSERIPS AS Codigo,
							CONCAT(RTRIM(A.CODSERIPS),'_',(A.IDDESCRIPCIONRELACIONADA)) AS CodigoDescripcion, 
							RTRIM(DESSERIPS) as Descripcion
							,(SUM(A.CANSERIPS) - (SELECT COUNT(*) FROM  dbo.HCPROCTER Z  WHERE Z.IPCODPACI= @Identificacion  AND Z.NUMINGRES=@Ingreso AND Z.CODSERIPS = A.CODSERIPS AND ISNULL(Z.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0))) AS cantidadpendiente
							, A.IDDESCRIPCIONRELACIONADA as DescripcionRelacionada, CD.Name as ContractDescriptionName
					
						FROM 
							.dbo.HCORDPRON  A 
							INNER JOIN .dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
							LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA
							LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
						WHERE
							IPCODPACI=@Identificacion
							AND NUMINGRES= @Ingreso  
							AND TIPSERTER= '1'
							AND TIPSERIPS = '4'	
							AND ESTSERIPS <> 5
							AND MANEXTPRO = 0
						GROUP BY 
							B.DESSERIPS, 
							A.CODSERIPS,
							A.IDDESCRIPCIONRELACIONADA,
							CD.name

				) TMP WHERE TMP.cantidadpendiente <> 0

				UNION ALL 
						SELECT 
							'Todas' AS Terapias,
							CE.Code AS Codigo,
							CONCAT(RTRIM(CE.Code),'_',RTRIM(CDD.Id)) AS CodigoDescripcion,
							RTRIM(CE.Description) as Descripcion,
							'' AS cantidadrealizada, CDD.Id as DescripcionRelacionada, CD.Name as ContractDescriptionName
						from contract.CUPSEntity CE
							LEFT JOIN Contract.CUPSEntityContractDescriptions CDD ON CE.Id = CDD.CUPSEntityId
							LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
						WHERE 
							CE.Status = 1 
							AND CE.TherapyProcedure = 1	
							AND CE.ServiceType = 4

		) as TMP
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los procedimientos de terapia ordenados y realizados para un paciente en un ingreso específico, identificado por su cédula y número de ingreso. Combina dos conjuntos de información: primero, las terapias pendientes de realizar (órdenes médicas activas de tipo terapia cuya cantidad ordenada supera las ya ejecutadas en la historia clínica), y segundo, el catálogo completo de terapias disponibles en el sistema. Para cada terapia muestra el código CUPS, la descripción del servicio, la cantidad pendiente y el concepto de contrato asociado, cruzando las órdenes médicas (HCORDPRON), los procedimientos ya realizados (HCPROCTER), el maestro de servicios CUPS (INCUPSIPS) y las descripciones de contrato (ContractDescriptions / CUPSEntityContractDescriptions). Se usa en la historia clínica para controlar el cumplimiento de terapias prescritas versus ejecutadas durante la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProcedimientoTerapiasRealizadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProcedimientoTerapiasRealizadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, para un paciente e ingreso, las terapias pendientes por realizar (ordenadas pero no ejecutadas) junto con el catálogo completo de terapias disponibles activas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiasRealizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y su ingreso deben existir en las órdenes (HCORDPRON) para obtener pendientes; Existencia del catálogo CUPS (CUPSEntity) y sus descripciones de contrato para listar ''Todas''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiasRealizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes activas (estado distinto de 5) y no de manejo externo (MANEXTPRO=0); Solo se listan órdenes cuyo tipo de servicio terapia es ''1'' y tipo de servicio IPS es ''4''; La cantidad pendiente se calcula como cantidad ordenada total menos cantidad ya realizada para la misma combinación de código de servicio y descripción de contrato relacionada; El emparejamiento entre ordenado y realizado considera nulos equivalentes en IDDESCRIPCIONRELACIONADA (ISNULL=0); El catálogo ''Todas'' solo expone CUPS activos marcados como procedimiento de terapia y con tipo de servicio 4; Se entrega un identificador secuencial ordenado por Codigo del CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiasRealizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Terapias; Procedimientos CUPS; Órdenes de procedimientos; Procedimientos realizados; Ingreso del paciente; Contratos / descripciones de contrato', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiasRealizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDPRON: Cuando (SUM(CANSERIPS) - count en HCPROCTER) <> 0 → se retorna fila marcada como ''Pendientes a realizar'' con la cantidad pendiente; [RETURN_RESULT] contract.CUPSEntity: Cuando CUPSEntity.Status=1 AND TherapyProcedure=1 AND ServiceType=4 → se retorna fila marcada como ''Todas'' del catálogo de terapias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiasRealizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Subconjunto ''Pendientes a realizar'': cantidad ordenada (SUM CANSERIPS) menos las ya realizadas en HCPROCTER es distinta de cero → Se incluye el procedimiento como pendiente para el paciente e ingreso; si Subconjunto ''Todas'': CUPSEntity con Status=1, TherapyProcedure=1 y ServiceType=4 → Se incluye como terapia disponible en el catálogo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiasRealizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.INCUPSIPS; dbo.HCPROCTER; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; contract.CUPSEntity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiasRealizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiasRealizadas';
-- GO
