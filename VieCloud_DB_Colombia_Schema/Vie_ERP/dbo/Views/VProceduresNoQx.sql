

CREATE VIEW [dbo].[VProceduresNoQx]
AS
		SELECT 
		datos.CODSERIPS,
		datos.Seleccione,
		datos.IPCODPACI,
		datos.NUMINGRES,
		datos.Tipo,
		datos.DESSERIPS,
		SUM(datos.TOTAL) TOTAL,
		datos.PersonName,
		datos.CUPSEntityContractDescriptionId,
		datos.ContractDescriptionId,
		datos.ContractDescriptionCodeName
	FROM 
	(
		SELECT	A.CODSERIPS,
				CAST(0 AS tinyint) AS Seleccione, 
				A.IPCODPACI, 
				A.NUMINGRES,
				case when MAX(A.ESTSERIPS) = '1' then 'No Realizados' when A.ESTSERIPS = '5' then 'Anulados' else 'Realizados' end as Tipo,
				RTRIM(B.DESSERIPS) AS DESSERIPS,
				SUM(A.CANSERIPS) AS TOTAL,
				i.IPNOMCOMP  as PersonName,
				ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
				ISNULL(cd.Id, 0) ContractDescriptionId,
				ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
		FROM dbo.ADINGRESO ing
		JOIN dbo.HCORDPRON A ON ing.NUMINGRES = a.NUMINGRES
		JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
		LEFT JOIN dbo.HCINFPROM AS Men ON A.CODSERIPS=Men.CODSERIPS and A.IPCODPACI=Men.IPCODPACI AND A.NUMINGRES = Men.NUMINGRES and A.NUMEFOLIO = Men.NUMEFOLIO 
		LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
		LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
		WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
		GROUP BY A.CODSERIPS,B.DESSERIPS,A.ESTSERIPS,SERREASIT,TIPSERIPS,A.IPCODPACI,A.NUMINGRES,i.IPNOMCOMP, cecd.Id, cd.Id, cd.Code, cd.Name
	UNION ALL
		SELECT	A.CODSERIPS,
				CAST(0 AS tinyint) AS Seleccione,
				A.IPCODPACI,
				INGMH.NUMINGRES,
				case when MAX(A.ESTSERIPS) = '1' then 'No Realizados' when A.ESTSERIPS = '5' then 'Anulados' else 'Realizados' end as Tipo,
				RTRIM(B.DESSERIPS) AS DESSERIPS,
				SUM(A.CANSERIPS) AS TOTAL,
				'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName,
				ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
				ISNULL(cd.Id, 0) ContractDescriptionId,
				ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
		FROM dbo.HCORDPRON A 
		JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
		JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
		JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
		LEFT JOIN dbo.HCINFPROM AS Men ON A.CODSERIPS=Men.CODSERIPS and A.IPCODPACI=Men.IPCODPACI AND A.NUMINGRES = Men.NUMINGRES and A.NUMEFOLIO = Men.NUMEFOLIO 
		LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
		LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
		WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
		GROUP BY A.CODSERIPS,B.DESSERIPS,A.ESTSERIPS,SERREASIT,TIPSERIPS,A.IPCODPACI,INGMH.NUMINGRES,rn.NUMHIJREG, cecd.Id, cd.Id, cd.Code, cd.Name
	) Datos
	GROUP BY datos.CODSERIPS, datos.Seleccione, datos.IPCODPACI, datos.NUMINGRES, datos.Tipo, datos.DESSERIPS, datos.PersonName, datos.CUPSEntityContractDescriptionId, datos.ContractDescriptionId, datos.ContractDescriptionCodeName
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolida los procedimientos no quirúrgicos (No-Qx) ordenados en la historia clínica de los pacientes, tanto para ingresos regulares como para recién nacidos. Combina las órdenes médicas de procedimientos (HCORDPRON) con el catálogo CUPS (INCUPSIPS), los datos del ingreso (ADINGRESO) y la información del paciente (INPACIENT), clasificando cada procedimiento según su estado: Realizado, No Realizado o Anulado. Incluye la descripción del concepto de contrato asociado al procedimiento (código CUPS vinculado a descripciones de contrato de facturación), lo que permite usar esta vista para reportería de facturación, auditoría de servicios ordenados versus ejecutados, y seguimiento de procedimientos por ingreso, paciente o tipo de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VProceduresNoQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VProceduresNoQx';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los procedimientos no quirúrgicos ordenados a un paciente (incluyendo los asociados a ingresos de recién nacido vinculados a la madre), clasificándolos por estado y enlazándolos con su descripción de contrato CUPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes en HCORDPRON deben tener un CODSERIPS existente en INCUPSIPS; Para el bloque de recién nacidos, debe existir vínculo madre-hijo en HCINGRESORECNAC y registro en HCRECINAC asociado al NUMINGRESHIJO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes con MANEXTPRO = 0 o con TRATAESPECIA = 3 (excluye procedimientos quirúrgicos/manejo externo salvo tratamiento especial 3); El estado del procedimiento se normaliza a tres categorías: ''No Realizados'', ''Anulados'' o ''Realizados''; Las cantidades (CANSERIPS) se agregan mediante SUM por código de servicio, paciente, ingreso, estado y descripción de contrato; Para ingresos de recién nacido, el nombre se reemplaza por la etiqueta ''Hijo N'' usando el número de hijo registrado, no el nombre real; Los identificadores de descripción de contrato (CUPSEntityContractDescriptionId, ContractDescriptionId) se devuelven como 0 cuando no existe relación contractual; La columna Seleccione siempre se inicializa en 0 (tinyint) para uso de la UI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso/admisión; orden de procedimiento; CUPS; procedimiento no quirúrgico; recién nacido; hijo de madre hospitalizada; contrato; tratamiento especial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Une dos conjuntos: (1) órdenes del paciente del ingreso y (2) órdenes de ingresos de recién nacido vinculados al ingreso de la madre vía HCINGRESORECNAC, ambos filtrados por MANEXTPRO=0 OR TRATAESPECIA=3, agregando totales por SUM(CANSERIPS)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MAX(ESTSERIPS) = ''1'' → Clasifica el procedimiento como ''No Realizados'' else Si ESTSERIPS = ''5'' clasifica como ''Anulados''; en cualquier otro caso clasifica como ''Realizados''; si MANEXTPRO = 0 OR ISNULL(TRATAESPECIA, 0) = 3 → Incluye la orden en el resultado (procedimiento no quirúrgico o tratamiento especial tipo 3) else Excluye la orden del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDPRON; dbo.INCUPSIPS; dbo.INPACIENT; dbo.HCINFPROM; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VProceduresNoQx';
GO
