
CREATE VIEW [dbo].[VLaboratories]
AS
	SELECT	A.CODSERIPS,
			CAST(0 AS tinyint) AS Seleccione, 
			A.IPCODPACI, 
			A.NUMINGRES, 
			CASE  
				WHEN ESTSERIPS in('3','4') THEN 'Realizados' 
				WHEN ESTSERIPS = '4' THEN 'Realizados' 
				WHEN ESTSERIPS = '6' THEN 'Anulados' 
				WHEN ESTSERIPS ='1' THEN 'Solicitados' 
				ELSE 'No Realizados' 
			END AS  Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			SUM(A.CANSERIPS) AS TOTAL,
			i.IPNOMCOMP  as PersonName, 
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			a.ESTSERIPS
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDLABO A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
	GROUP BY A.CODSERIPS,B.DESSERIPS,A.IPCODPACI,A.NUMINGRES,A.ESTSERIPS,i.IPNOMCOMP,SERREASIT,ESTSERIPS, cecd.Id, cd.Id, cd.Code, cd.Name
UNION ALL
	SELECT	A.CODSERIPS,
			CAST(0 AS tinyint) AS Seleccione, 
			a.IPCODPACI, 
			INGMH.NUMINGRES, 
			CASE  
				WHEN ESTSERIPS in('3','4') THEN 'Realizados' 
				WHEN ESTSERIPS = '4' THEN 'Realizados' 
				WHEN ESTSERIPS = '6' THEN 'Anulados' 
				WHEN ESTSERIPS ='1' THEN 'Solicitados' 
				ELSE 'No Realizados' 
			END AS  Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			SUM(A.CANSERIPS) AS TOTAL,'
			Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName, 
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			a.ESTSERIPS
	FROM dbo.HCORDLABO A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO   
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
	GROUP BY A.CODSERIPS,B.DESSERIPS,a.IPCODPACI,INGMH.NUMINGRES, A.ESTSERIPS,RN.NUMHIJREG,SERREASIT,ESTSERIPS, cecd.Id, cd.Id, cd.Code, cd.Name
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de órdenes de laboratorio clínico solicitadas, realizadas, anuladas o no realizadas para cada paciente e ingreso. Combina dos grupos: los exámenes de laboratorio de pacientes adultos o ingresados bajo tratamiento especial, y los exámenes correspondientes a recién nacidos (vinculados mediante el ingreso del hijo y el registro de nacimiento). Para cada orden incluye el código y descripción del servicio CUPS, el nombre completo del paciente (o la identificación del recién nacido), la cantidad total de veces solicitada, el estado de la orden (Solicitados, Realizados, Anulados, No Realizados) y la descripción del concepto de contrato o grupo de facturación asociado. Se usa en módulos de seguimiento de órdenes de laboratorio, conciliación de servicios prestados, facturación y verificación de resultados pendientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VLaboratories';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VLaboratories';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista las órdenes de laboratorio clínico de pacientes, tanto las asociadas a su propio ingreso como las asociadas al ingreso de un recién nacido vinculado a la madre, clasificándolas por estado (Realizadas, Anuladas, Solicitadas, No Realizadas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VLaboratories';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de laboratorio (HCORDLABO) deben estar asociadas a un ingreso válido en ADINGRESO.; El paciente debe existir en INPACIENT y el código de servicio en INCUPSIPS.; Para el bloque de recién nacidos, debe existir el vínculo madre-hijo en HCINGRESORECNAC y el registro clínico del recién nacido en HCRECINAC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VLaboratories';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes que no son manejo externo (MANEXTPRO=0), salvo cuando el ingreso es tratamiento especial (TRATAESPECIA=3).; Toda orden se clasifica obligatoriamente en uno de los estados: Realizados, Anulados, Solicitados o No Realizados.; Si no existe descripción de contrato vinculada, se devuelve Id=0 y código-nombre vacío en lugar de NULL.; Las órdenes del recién nacido se reportan bajo el ingreso del hijo (NUMINGRESHIJO), no bajo el de la madre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VLaboratories';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de laboratorio clínico; Ingreso/admisión del paciente; Estado de la orden (Realizado, Anulado, Solicitado, No Realizado); CUPS / servicios; Descripción de contrato; Tratamiento especial; Manejo externo; Recién nacido vinculado a la madre; Paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VLaboratories';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] VLaboratories: Devuelve únicamente órdenes con MANEXTPRO=0 o cuyo ingreso tenga TRATAESPECIA=3 (tratamiento especial).; [RETURN_RESULT] VLaboratories: Clasifica el estado: ESTSERIPS in (3,4) → ''Realizados''; ESTSERIPS=6 → ''Anulados''; ESTSERIPS=1 → ''Solicitados''; cualquier otro → ''No Realizados''.; [RETURN_RESULT] VLaboratories: Para órdenes asociadas a un ingreso de recién nacido, el nombre que se muestra es ''Hijo '' + número del hijo (NUMHIJREG) en lugar del nombre del paciente.; [RETURN_RESULT] VLaboratories: Suma la cantidad solicitada (CANSERIPS) agrupando por servicio CUPS, paciente, ingreso, estado y descripción de contrato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VLaboratories';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS in (''3'',''4'') → Tipo = ''Realizados''; si ESTSERIPS = ''6'' → Tipo = ''Anulados''; si ESTSERIPS = ''1'' → Tipo = ''Solicitados'' else Tipo = ''No Realizados''; si A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA,0) = 3 → Se incluye la orden de laboratorio en el resultado else Se excluye la orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VLaboratories';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDLABO; dbo.INCUPSIPS; dbo.INPACIENT; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VLaboratories';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VLaboratories';
GO
