-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarProcedimientoTerapiaOrdenes]
(
    -- Add the parameters for the stored procedure here
	@Identificacion as varchar(25),
	@Ingreso as varchar(25),
	@Centro as varchar(25),
	@Unidadfuncional as varchar(25)
)
AS
BEGIN
		SELECT 
			'Terapias ordenadas' Agrupacion, 
			RTRIM(A.CODSERIPS) as CodigoProcedimiento, 
			RTRIM(B.DESSERIPS) as DescripcionProcedimiento,
			(SELECT COUNT(*) FROM  .dbo.HCPROCTER Z 
			WHERE Z.IPCODPACI=@Identificacion
			AND Z.NUMINGRES=@Ingreso
			AND Z.CODSERIPS = A.CODSERIPS AND ISNULL(Z.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)) AS cantidadrealizada ,
			SUM(A.CANSERIPS) AS Cantidad, CD.Name as ContractDescriptionName, A.IDDESCRIPCIONRELACIONADA AS ContractDescriptionId
			
			FROM 
				.dbo.HCORDPRON A 
				INNER JOIN .dbo.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS 
				LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA
				LEFT JOIN contract.ContractDescriptions CD on CD.Id = CDD.ContractDescriptionId
			WHERE 
				A.IPCODPACI = @Identificacion 
				AND A.NUMINGRES = @Ingreso 
				AND A.CODCENATE = @Centro 
				AND TIPSERTER = '1'  and A.MANEXTPRO = 0
				AND ESTSERIPS <> 5
			GROUP BY 
				B.DESSERIPS, 
				A.CODSERIPS,
				CD.Name,
				A.IDDESCRIPCIONRELACIONADA

UNION ALL 
		SELECT 
		  'Terapias sin orden' Agrupacion, 
		  RTRIM(A.CODSERIPS) as CodigoProcedimiento, 
		  RTRIM(B.DESSERIPS) as DescripcionProcedimiento,
		  (SELECT COUNT(*) FROM  .dbo.HCPROCTER Z 
			WHERE Z.IPCODPACI=@Identificacion
			AND Z.NUMINGRES=@Ingreso
			AND Z.CODSERIPS = A.CODSERIPS AND ISNULL(Z.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)) AS cantidadrealizada ,
		  0 AS Cantidad, CD.Name as ContractDescriptionName, A.IDDESCRIPCIONRELACIONADA AS ContractDescriptionId

		FROM 
		  .dbo.HCPROCTER A 
		  INNER JOIN .dbo.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS 
		  LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA
		  LEFT JOIN contract.ContractDescriptions CD on CD.Id = CDD.ContractDescriptionId
		WHERE 
		  A.IPCODPACI = @Identificacion 
		  AND A.NUMINGRES = @Ingreso 
		  AND A.CODCENATE = @Centro 
		  --AND A.UFUCODIGO = @Unidadfuncional Se comenta porque no se está teniendo en cuenta todas las terapias del paciente y ocultaba algunas
		  AND B.TIPSERTER = '1' 
		  AND NOT EXISTS (SELECT CODSERIPS 
							FROM HCORDPRON X 
							WHERE X.CODSERIPS = A.CODSERIPS 
							AND ISNULL(X.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)
						    AND X.IPCODPACI = A.IPCODPACI 
							AND X.NUMINGRES = A.NUMINGRES
							AND X.MANEXTPRO = 0 --ODO Se agrega porque no se están listando las terapias realizadas si el médico ordena la misma terapia extramural 
						  ) 
		GROUP BY 
		  B.DESSERIPS, 
		  A.CODSERIPS,
		  CD.Name,
		  A.IDDESCRIPCIONRELACIONADA

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las terapias (procedimientos de tipo terapéutico, TIPSERTER=''1'') asociadas a un paciente y un ingreso específico, combinando dos grupos: las terapias que tienen una orden médica registrada en la historia clínica (HCORDPRON) y las terapias que fueron realizadas directamente sin orden previa (HCPROCTER). Para cada terapia muestra el código y descripción del servicio CUPS (INCUPSIPS), la cantidad ordenada, cuántas veces ya fue ejecutada o realizada, y la descripción de contrato aplicable (ContractDescriptions / CUPSEntityContractDescriptions). Se usa en la visualización clínica del ingreso del paciente para controlar el cumplimiento de terapias ordenadas versus terapias ejecutadas, identificando también aquellas realizadas sin orden médica previa. Recibe como parámetros la cédula del paciente (@Identificacion), el número de ingreso (@Ingreso), el centro de atención (@Centro) y la unidad funcional (@Unidadfuncional).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProcedimientoTerapiaOrdenes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProcedimientoTerapiaOrdenes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las terapias del paciente en un ingreso y centro, distinguiendo las que están ordenadas por el médico (con cantidad ordenada y realizada) de las que ya se realizaron sin orden médica asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiaOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir y corresponder a registros de órdenes/procedimientos en el centro indicado; Los procedimientos deben estar parametrizados en el catálogo CUPS (INCUPSIPS); La descripción de contrato relacionada (IDDESCRIPCIONRELACIONADA) debe estar registrada en las tablas de contratos para mostrar su nombre', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiaOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran procedimientos cuyo tipo de servicio es terapia (TIPSERTER=''1''); Las órdenes con estado ESTSERIPS=5 quedan excluidas del listado de terapias ordenadas; Las órdenes con manejo extramural (MANEXTPRO<>0) no cuentan como órdenes asociadas, ni para incluirlas en ''ordenadas'' ni para excluirlas de ''sin orden''; El conteo de realizadas siempre coincide en clave (paciente, ingreso, CODSERIPS, IDDESCRIPCIONRELACIONADA), tratando IDDESCRIPCIONRELACIONADA NULL como 0; Una misma terapia no aparece simultáneamente como ''ordenada'' y ''sin orden'' (la segunda excluye lo presente en HCORDPRON); El filtro por unidad funcional fue intencionalmente deshabilitado en ''sin orden'' para no ocultar terapias del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiaOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión hospitalaria; Centro de atención; Unidad funcional; Terapias; Procedimientos CUPS; Órdenes médicas (procedimientos ordenados); Procedimientos de terapia realizados; Contratos / Descripciones de contrato; Servicios extramurales (MANEXTPRO)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiaOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Cuando existen procedimientos en HCORDPRON con TIPSERTER=''1'', MANEXTPRO=0 y ESTSERIPS<>5 para el paciente/ingreso/centro, se retornan agrupados como ''Terapias ordenadas'' con la suma de CANSERIPS y el conteo de realizaciones en HCPROCTER; [RETURN_RESULT] ?: Cuando existen procedimientos realizados en HCPROCTER con TIPSERTER=''1'' para el paciente/ingreso/centro y NO existe una orden equivalente en HCORDPRON (mismo CODSERIPS e IDDESCRIPCIONRELACIONADA con MANEXTPRO=0), se retornan como ''Terapias sin orden'' con Cantidad=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiaOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Procedimiento de terapia (TIPSERTER=''1'') existe en órdenes médicas (HCORDPRON) con MANEXTPRO=0 y ESTSERIPS<>5 → Se agrupa bajo ''Terapias ordenadas'' sumando la cantidad ordenada; si Procedimiento de terapia ya realizado (HCPROCTER) sin que exista una orden médica equivalente en HCORDPRON (mismo CODSERIPS, IDDESCRIPCIONRELACIONADA, paciente e ingreso, con MANEXTPRO=0) → Se agrupa bajo ''Terapias sin orden'' con Cantidad=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiaOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPROCTER; dbo.HCORDPRON; dbo.INCUPSIPS; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiaOrdenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProcedimientoTerapiaOrdenes';
-- GO
