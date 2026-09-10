

CREATE VIEW [dbo].[ViewServicesHemocomponent]
AS
	SELECT	CONCAT('HCORHEMSER', '-', SER.ID) Id,
			'HCORHEMSER' EntityName,
			SER.ID AS Row,
			SOL.AuthorizationEventId,
			RTRIM(SER.CODSERIPS) AS CODSERIPS,
			IIF(SER.ORDSERVICIOID IS NULL, 0, 1) AS Seleccione,
			ISNULL(CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)), '') AS UnidadFuncional,
			ISNULL(CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)), '') AS Medico,
			SOL.OBSERVACI AS Observacion,
			SOL.FECORDMED AS Fecha,
			SOL.NUMEFOLIO AS Folio,
			'' AS FolioInterpreta,
			'' AS MedicoInterpreto,
			'' AS Interpretacion,
			ISNULL(C.CODPROSAL,'') AS CODPROSAL,
			ISNULL(C.CODESPEC1,'') AS CODESPEC1,
			1 AS CANSERIPS,
			ING.NUMINGRES,
			SOL.IPCODPACI,
			ISNULL(D.UFUCODIGO,'') AS UFUCODIGO,
			ISNULL(C.CODIGONIT,'') AS NitMedico,
			SER.ORDSERVICIOID AS GENSERVICEORDER,
			CASE SER.ESTADO
				WHEN 2 THEN 'Realizados'
				WHEN 3 THEN 'No Realizados'
			END AS EstadoServicio,
			COALESCE(BOL.PROFAPLICA, BOL1.PROFAPLICA, '') AS MedicoRealizo,
			CAST(REPLACE(CONVERT(VARCHAR, COALESCE(BOL.FECAPLICENF, BOL.FECAPLICMED), 126), 'T00:00:00', 'T23:59:59.998') AS DATETIME) AS FechaRealizacion,
			IIF(ISNULL(COALESCE(BOL.FECAPLICENF, BOL.FECAPLICMED), '') = '', 0, 1) AS Realizo,
			ISNULL(SOL.REARASANT, 0) AS REARASANT,
			CASE WHEN SER.TIPOSERVICIO = 1 THEN 'Rastreo Anticuerpos' WHEN SER.TIPOSERVICIO = 2 THEN 'En la reserva' WHEN SER.TIPOSERVICIO = 3 THEN 'En la transfusión' END AS TIPOSERVICIO,
			'Madre' AS TipoPaciente,
			COM.DESCOMSAM AS COMPONENTE,
			BOL.ID AS BOLSAID,
			P.IPNOMCOMP,
			B.DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId,
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM HCORHEMSER SER
	JOIN INCUPSIPS CUPS ON CUPS.CODSERIPS = SER.CODSERIPS
	JOIN HCORHEMCO SOL ON SOL.ID = SER.HCORHEMCOID
	JOIN ADINGRESO ING ON SOL.NUMINGRES = ING.NUMINGRES
	JOIN INPACIENT P ON P.IPCODPACI = ING.IPCODPACI
	JOIN INCUPSIPS B ON SER.CODSERIPS=B.CODSERIPS 
	LEFT JOIN 
	(
		SELECT ROW_NUMBER() OVER (PARTITION BY SER1.HCORHEMCOID ORDER BY SER1.HCORHEMBOLID ASC) ID_PARTICION, SER1.HCORHEMCOID, SER1.HCORHEMBOLID 
		FROM HCORHEMSER SER1 
		WHERE SER1.HCORHEMBOLID IS NOT NULL
	) SER2 ON SER2.HCORHEMCOID = SOL.ID AND SER2.ID_PARTICION = 1
	LEFT JOIN HCORHEMBOL BOL ON BOL.ID = SER.HCORHEMBOLID
	LEFT JOIN dbo.HCCOMSAN COM ON COM.ID = BOL.COMSAMID
	LEFT JOIN HCORHEMBOL BOL1 ON BOL1.ID = SER2.HCORHEMBOLID
	LEFT JOIN dbo.INUNIFUNC D ON D.UFUCODIGO = COALESCE(BOL.UFUAPLICA, BOL.UFUENTREGA, BOL.UFUSOLTRA, BOL.UFUSOLRES, BOL1.UFUAPLICA, BOL1.UFUENTREGA, BOL1.UFUSOLTRA, BOL1.UFUSOLRES)
	LEFT JOIN dbo.INPROFSAL C ON C.CODPROSAL = COALESCE(BOL.PROFAPLICA, BOL.PROFSOLTRA, BOL.PROFSOLRES, BOL1.PROFAPLICA, BOL1.PROFSOLTRA, BOL1.PROFSOLRES)
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = SER.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on SER.ID=acj.EntityId and acj.EntityName='HCORHEMSER' and acj.EntityTap='INDLcgHemo'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE SER.ESTADO IN (2, 3) AND SER.TIPOSERVICIO IN (1, 2, 3) AND (SOL.MANEXTPRO = 0 OR ISNULL(ING.TRATAESPECIA, 0) = 3)
UNION ALL
	SELECT	CONCAT('HCORHEMSER', '-', SER.ID) Id,
			'HCORHEMSER' EntityName,
			SER.ID AS Row,
			SOL.AuthorizationEventId,
			RTRIM(SER.CODSERIPS) AS CODSERIPS,
			IIF(SER.ORDSERVICIOID IS NULL, 0, 1) AS Seleccione,
			ISNULL(CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)), '') AS UnidadFuncional,
			ISNULL(CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)), '') AS Medico,
			SOL.OBSERVACI AS Observacion,
			SOL.FECORDMED AS Fecha,
			SOL.NUMEFOLIO AS Folio,
			'' AS FolioInterpreta,
			'' AS MedicoInterpreto,
			'' AS Interpretacion,
			ISNULL(C.CODPROSAL,'') AS CODPROSAL,
			ISNULL(C.CODESPEC1,'') AS CODESPEC1,
			1 AS CANSERIPS,
			INGMH.NUMINGRES,
			SOL.IPCODPACI,
			ISNULL(D.UFUCODIGO,'') AS UFUCODIGO,
			ISNULL(C.CODIGONIT,'') AS NitMedico,
			SER.ORDSERVICIOID AS GENSERVICEORDER,
			CASE SER.ESTADO
				WHEN 2 THEN 'Realizados'
				WHEN 3 THEN 'No Realizados'
			END AS Estado,
			COALESCE(BOL.PROFAPLICA, BOL1.PROFAPLICA, '') AS MedicoRealizo,
			CAST(REPLACE(CONVERT(VARCHAR, COALESCE(BOL.FECAPLICENF, BOL.FECAPLICMED), 126), 'T00:00:00', 'T23:59:59.998') AS DATETIME) AS FechaRealizacion,
			IIF(ISNULL(COALESCE(BOL.FECAPLICENF, BOL.FECAPLICMED), '') = '', 0, 1) AS Realizo,
			ISNULL(SOL.REARASANT,CAST(0 AS BIT)) AS REARASANT,
			CASE WHEN SER.TIPOSERVICIO = 1 THEN 'Rastreo Anticuerpos' WHEN SER.TIPOSERVICIO = 2 THEN 'En la reserva' WHEN SER.TIPOSERVICIO = 3 THEN 'En la transfusión' END AS TIPOSERVICIO,
			'Recien Nacido' AS TipoPaciente,
			COM.DESCOMSAM AS COMPONENTE,
			BOL.ID AS BOLSAID,
			P.IPNOMCOMP,
			B.DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId,
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM HCORHEMSER SER
	JOIN INCUPSIPS CUPS ON CUPS.CODSERIPS = SER.CODSERIPS
	JOIN HCORHEMCO SOL ON SOL.ID = SER.HCORHEMCOID
	JOIN HCINGRESORECNAC INGMH on SOL.NUMINGRES = INGMH.NUMINGRESHIJO
	JOIN HCRECINAC RN on INGMH.NUMINGRESHIJO  = RN.NUMINGRESHIJO  
	JOIN ADINGRESO AS ING on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	JOIN INPACIENT P ON P.IPCODPACI = ING.IPCODPACI
	JOIN dbo.INCUPSIPS B ON SER.CODSERIPS=B.CODSERIPS 
	LEFT JOIN 
	(
		SELECT ROW_NUMBER() OVER (PARTITION BY SER1.HCORHEMCOID ORDER BY SER1.HCORHEMBOLID ASC) ID_PARTICION, SER1.HCORHEMCOID, SER1.HCORHEMBOLID 
		FROM HCORHEMSER SER1 
		WHERE SER1.HCORHEMBOLID IS NOT NULL
	) SER2 ON SER2.HCORHEMCOID = SOL.ID AND SER2.ID_PARTICION = 1
	LEFT JOIN HCORHEMBOL BOL ON BOL.ID = SER.HCORHEMBOLID
	LEFT JOIN dbo.HCCOMSAN COM ON COM.ID = BOL.COMSAMID
	LEFT JOIN HCORHEMBOL BOL1 ON BOL1.ID = SER2.HCORHEMBOLID
	LEFT JOIN dbo.INUNIFUNC D ON D.UFUCODIGO = COALESCE(BOL.UFUAPLICA, BOL.UFUENTREGA, BOL.UFUSOLTRA, BOL.UFUSOLRES, BOL1.UFUAPLICA, BOL1.UFUENTREGA, BOL1.UFUSOLTRA, BOL1.UFUSOLRES)
	LEFT JOIN dbo.INPROFSAL C ON C.CODPROSAL = COALESCE(BOL.PROFAPLICA, BOL.PROFSOLTRA, BOL.PROFSOLRES, BOL1.PROFAPLICA, BOL1.PROFSOLTRA, BOL1.PROFSOLRES)
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = SER.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on SER.ID=acj.EntityId and acj.EntityName='HCORHEMSER' and acj.EntityTap='INDLcgHemo'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE SER.ESTADO IN (2, 3) AND SER.TIPOSERVICIO IN (1, 2, 3) AND (SOL.MANEXTPRO = 0 OR ISNULL(ING.TRATAESPECIA, 0) = 3)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todos los servicios de hemoterapia (transfusiones de sangre y hemocomponentes) realizados o no realizados, integrando las órdenes de hemoterapia, las bolsas de hemoderivados, los datos del paciente (cédula, nombre completo), el ingreso hospitalario, la unidad funcional donde se aplicó la transfusión, el médico que la realizó, el componente sanguíneo utilizado (plasma, glóbulos rojos, plaquetas, etc.) y el código de servicio CUPS. Cubre dos escenarios: servicios asociados a la madre (ingreso principal en ADINGRESO) y servicios asociados al recién nacido (ingreso pediátrico vinculado mediante HCINGRESORECNAC). Incluye información contractual (descripción del contrato, relación CUPS-contrato) y justificaciones de control de cartera o glosa (AccountControlJustification), lo que la hace útil para facturación, RIPS, auditoría clínica y seguimiento del proceso transfusional completo por episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewServicesHemocomponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewServicesHemocomponent';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los servicios de hemocomponentes (rastreo de anticuerpos, reserva y transfusión) realizados o no realizados, tanto para la madre como para el recién nacido, junto con datos de bolsa, profesional, unidad funcional, contrato y justificación de facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesHemocomponent';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El servicio (HCORHEMSER) debe tener ESTADO 2 (Realizados) o 3 (No Realizados); El TIPOSERVICIO debe ser 1 (Rastreo Anticuerpos), 2 (En la reserva) o 3 (En la transfusión); La solicitud debe cumplir SOL.MANEXTPRO = 0 o el ingreso debe tener TRATAESPECIA = 3; Para servicios del recién nacido debe existir relación HCINGRESORECNAC entre ingreso madre e hijo y registro en HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesHemocomponent';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen servicios con estado Realizado (2) o No Realizado (3); Solo se exponen los tipos de servicio 1, 2 y 3 de hemocomponentes; Se excluyen servicios cuya solicitud sea de manejo externo de productos (MANEXTPRO=1) salvo que el ingreso sea tratamiento especial tipo 3; EntityName siempre es ''HCORHEMSER'' y el Id se construye como ''HCORHEMSER-{ID}''; La fecha de realización se normaliza a 23:59:59.998 cuando la hora original es 00:00:00; La unidad funcional y el profesional se resuelven por COALESCE priorizando aplicación, entrega, solicitud de transfusión y solicitud de reserva, en ese orden, sobre la bolsa actual y luego sobre la primera bolsa asociada a la solicitud; La justificación de control de cuentas se filtra por EntityName=''HCORHEMSER'' y EntityTap=''INDLcgHemo''; Cuando hay múltiples bolsas en una solicitud, se selecciona como respaldo la de menor HCORHEMBOLID (ROW_NUMBER ORDER BY ASC, partición=1)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesHemocomponent';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hemocomponente; Transfusión; Reserva de sangre; Rastreo de anticuerpos; Bolsa de sangre; Componente sanguíneo; Unidad funcional; Profesional de la salud; Ingreso hospitalario; Paciente madre y recién nacido; Tratamiento especial; Manejo externo de productos; CUPS; Contrato; Justificación de facturación / control de cuentas; Glosa (SkipClearance/SkipLiquidation)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesHemocomponent';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve filas combinadas (UNION ALL) de servicios de hemocomponentes para la madre (vía ADINGRESO directo) y para el recién nacido (vía HCINGRESORECNAC/HCRECINAC), marcando TipoPaciente = ''Madre'' o ''Recien Nacido''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesHemocomponent';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SER.ESTADO = 2 → Etiqueta el servicio como ''Realizados'' else Si ESTADO = 3 etiqueta como ''No Realizados''; si SER.TIPOSERVICIO = 1/2/3 → Asigna etiqueta ''Rastreo Anticuerpos'' / ''En la reserva'' / ''En la transfusión'' respectivamente; si SER.ORDSERVICIOID IS NULL → Marca Seleccione = 0 else Marca Seleccione = 1; si COALESCE(BOL.FECAPLICENF, BOL.FECAPLICMED) está vacío → Realizo = 0 (no aplicado) else Realizo = 1; si SOL.MANEXTPRO = 0 OR ISNULL(ING.TRATAESPECIA,0) = 3 → Incluye el servicio en el resultado else Excluye el servicio; si acj.Id IS NULL → JustificationCodeName vacío y SkipLiquidation = 0 else Concatena Code-Description de la justificación y toma SkipClearance como SkipLiquidation', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesHemocomponent';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORHEMSER; dbo.INCUPSIPS; dbo.HCORHEMCO; dbo.ADINGRESO; dbo.INPACIENT; dbo.HCORHEMBOL; dbo.HCCOMSAN; dbo.INUNIFUNC; dbo.INPROFSAL; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.AccountControlJustification; Billing.BillingJustificationControl; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesHemocomponent';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesHemocomponent';
GO
