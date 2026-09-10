

CREATE VIEW [dbo].[ViewOxygenConsumption]
AS

SELECT	 A.IDCONOXIG as Row
		,A.FECHREGIS AS DIAS
		,E.CODSERIPS
		--,CASE WHEN A.GENSERVICEORDER is null THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS Seleccione
		,IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
		,A.LITRXMINUT
		,A.HORAINICIA
		,A.HORAFINAL
		,A.TOTHORAS
		,A.TOTLITADM
		,C.NOMMEDICO
		,C.CODIGONIT as NitMedico
		,C.CODPROSAL
		,C.CODESPEC1
		,A.IPCODPACI
		,A.NUMINGRES
		,A.CODVIAADM
		,A.CODCENATE
		,A.UFUCODIGO
		,D.UFUDESCRI
		,E.DESVIAADM
		,A.GENSERVICEORDER
		,i.IPNOMCOMP  as PersonName,
		acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCCONOXIG' EntityName
FROM dbo.HCCONOXIG AS A 
INNER JOIN dbo.INPROFSAL AS C ON A.CODPROSAL=C.CODPROSAL 
INNER JOIN dbo.INUNIFUNC AS D ON A.UFUCODIGO=D.UFUCODIGO 
INNER JOIN dbo.HCPARCONO E ON A.CODVIAADM=E.CODVIAADM
inner join dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.IDCONOXIG=acj.EntityId and acj.EntityName='HCCONOXIG' and acj.EntityTap='INDlcgOxigenConsumer'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id

union all

SELECT   A.IDCONOXIG as Row
		,A.FECHREGIS AS DIAS
		,E.CODSERIPS
		--,CASE WHEN A.GENSERVICEORDER is null THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS Seleccione
		,IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
		,A.LITRXMINUT
		,A.HORAINICIA
		,A.HORAFINAL
		,A.TOTHORAS
		,A.TOTLITADM
		,C.NOMMEDICO
		,C.CODIGONIT as NitMedico
		,C.CODPROSAL
		,C.CODESPEC1
		,A.IPCODPACI
		,INGMH.NUMINGRES
		,A.CODVIAADM
		,A.CODCENATE
		,A.UFUCODIGO
		,D.UFUDESCRI
		,E.DESVIAADM
		,A.GENSERVICEORDER
		,'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName,
		acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCCONOXIG' EntityName
FROM dbo.HCCONOXIG AS A 
INNER JOIN dbo.INPROFSAL AS C ON A.CODPROSAL=C.CODPROSAL 
INNER JOIN dbo.INUNIFUNC AS D ON A.UFUCODIGO=D.UFUCODIGO 
INNER JOIN dbo.HCPARCONO E ON A.CODVIAADM=E.CODVIAADM
inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.IDCONOXIG=acj.EntityId and acj.EntityName='HCCONOXIG' and acj.EntityTap='INDlcgOxigenConsumer'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
WHERE ING.IESTADOIN = 'C'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolidado de consumo de oxigenoterapia y gases medicinales administrados a pacientes hospitalizados, incluyendo recién nacidos. Integra los registros de cada sesión de suministro (litros por minuto, horas de inicio y fin, total de horas y litros administrados) con el profesional que la ordenó, la unidad funcional donde se prestó, el servicio CUPS asociado a la vía de administración y los datos del paciente o del hijo en caso de parto. Incluye además la justificación de facturación vinculada a cada registro de consumo, indicando el código y descripción de la causal y si permite omitir el proceso de liquidación o glosa. Sirve como fuente principal para la liquidación, auditoría y control de cuentas de oxigenoterapia en el módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewOxygenConsumption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewOxygenConsumption';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los registros de consumo de oxígeno de pacientes (adultos y recién nacidos vinculados al ingreso de la madre) junto con sus justificaciones de control de facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada consumo de oxígeno debe tener profesional, unidad funcional y vía de administración válidos en sus catálogos.; Para mostrar consumos de recién nacidos debe existir la relación madre-hijo en HCINGRESORECNAC y el registro del recién nacido en HCRECINAC.; El ingreso del recién nacido debe estar en estado ''C'' (cerrado/confirmado) en ADINGRESO para incluirse en la segunda rama.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La justificación de control de cuenta solo se vincula cuando EntityName=''HCCONOXIG'' y EntityTap=''INDlcgOxigenConsumer''.; EntityName retornado siempre es ''HCCONOXIG''.; Los consumos del paciente principal usan su NUMINGRES; los del recién nacido reemplazan el NUMINGRES por el del hijo (NUMINGRESHIJO).; Solo se concatena código y descripción de la justificación cuando existe el registro de justificación.; La rama de recién nacido excluye ingresos del hijo que no estén cerrados (IESTADOIN<>''C'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consumo de oxígeno; Vía de administración; Profesional de la salud; Unidad funcional; Paciente; Ingreso/admisión; Recién nacido; Vínculo madre-hijo; Orden de servicio; Justificación de control de cuenta; Justificación de facturación; Omisión de liquidación (SkipClearance)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve la unión de consumos de oxígeno asociados directamente al paciente y los asociados a recién nacidos cuyo ingreso (NUMINGRESHIJO) está en estado ''C''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.GENSERVICEORDER IS NULL → Seleccione = 0 (no se ha generado orden de servicio) else Seleccione = 1 (orden de servicio ya generada); si acj.Id IS NULL (no existe justificación de control de cuenta) → JustificationCodeName = '''' y SkipLiquidation = 0 else JustificationCodeName = Code + '' - '' + Description y SkipLiquidation toma el valor configurado en la justificación; si Segunda rama: ING.IESTADOIN = ''C'' → Incluye consumos de oxígeno de recién nacidos asociados al ingreso de la madre, mostrando ''Hijo N'' como nombre', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCONOXIG; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.HCPARCONO; dbo.INPACIENT; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO; Billing.AccountControlJustification; Billing.BillingJustificationControl', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOxygenConsumption';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOxygenConsumption';
GO
