
CREATE VIEW [Billing].[ViewAdmissionLock_AccountControlValidations]
AS
SELECT PACA.NUMINGRES, PACA.IPCODPACI, MIN(PACA.ID) ID FROM dbo.HCHISPACA PACA
LEFT JOIN(
	SELECT TOP 1 A.NUMINGRES, A.GENSERVICEORDER 
	FROM dbo.HCHOGASIN A 
	JOIN dbo.HCACTENFE B ON A.CODACTENF=B.CODACTENF 
	LEFT JOIN INCUPSIPS C ON B.CODSERIPS=C.CODSERIPS 
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.CONSECUTI=acj.EntityId and acj.EntityName='HCHOGASIN' and acj.EntityTap='INDlcgNurseProcedure'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE C.CODSERIPS IS NOT NULL AND A.GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)
) AS GASIN ON GASIN.NUMINGRES = PACA.NUMINGRES AND PACA.GENSERVICEORDER IS NULL
LEFT JOIN(
	SELECT TOP 1 NUMINGRES 
	FROM dbo.HCCONOXIG A
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.IDCONOXIG=acj.EntityId and acj.EntityName='HCCONOXIG' and acj.EntityTap='INDlcgOxigenConsumer'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE  GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)
) AS XIG ON XIG.NUMINGRES = PACA.NUMINGRES 
LEFT JOIN ( 
	select TOP 1 A.NUMINGRES
	from dbo.HCPROCTER A
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.CODCONSEC=acj.EntityId and acj.EntityName='HCPROCTER' and acj.EntityTap='INDlcgTerapy'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id 
	where B.TIPSERTER = 1 AND A.GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0))as hc on hc.NUMINGRES = PACA.NUMINGRES  
LEFT JOIN (
	select TOP 1 SOL.NUMINGRES, SER.ORDSERVICIOID 
	from HCORHEMSER SER 
	JOIN INCUPSIPS CUPS ON CUPS.CODSERIPS = SER.CODSERIPS 
	JOIN HCORHEMCO SOL ON SOL.ID = SER.HCORHEMCOID 
	JOIN ADINGRESO ING ON SOL.NUMINGRES = ING.NUMINGRES 
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on SER.ID=acj.EntityId and acj.EntityName='HCORHEMSER' and acj.EntityTap='INDLcgHemo'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE SER.ESTADO IN (2, 3) AND SER.TIPOSERVICIO IN (1, 2, 3) AND (SOL.MANEXTPRO = 0 OR ISNULL(ING.TRATAESPECIA, 0) = 3) AND SER.ORDSERVICIOID IS NULL
	AND (isnull(bjc.SkipClearance,0)=0))  as ser2 on ser2.NUMINGRES= PACA.NUMINGRES 
LEFT JOIN(
	SELECT TOP 1 NUMINGRES 
	FROM HCORDINTE A
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDINTE' and acj.EntityTap='INDlcgInterSearch'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE NUMFOLINT IS NOT NULL AND GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)
) AS INTE ON INTE.NUMINGRES=PACA.NUMINGRES
LEFT JOIN(
	SELECT TOP 1 H.NUMINGRES
	FROM dbo.HCORDPRON H 
	JOIN dbo.ADINGRESO I ON I.NUMINGRES = H.NUMINGRES 
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON H.AUTO =acj.EntityId and acj.EntityName='HCORDPRON' and acj.EntityTap='INDlcgProceduresNoQX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE (H.MANEXTPRO = 0 OR ISNULL(I.TRATAESPECIA, 0) = 3) AND H.ESTSERIPS NOT IN ('1', '5')  AND H.GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)
) AS PRON ON PRON.NUMINGRES = PACA.NUMINGRES
LEFT JOIN(
	SELECT top 1 v.NUMINGRES FROM ViewSurgeriesPerformed v
	where v.GENSERVICEORDER IS NULL AND v.SkipLiquidation =0
) AS PROQ ON PROQ.NUMINGRES =PACA.NUMINGRES
LEFT JOIN(
	SELECT TOP 1 H.NUMINGRES
	FROM HCORDIMAG H 
	JOIN ADINGRESO I ON H.NUMINGRES = I.NUMINGRES 
	JOIN dbo.INCUPSIPS B ON H.CODSERIPS = B.CODSERIPS 
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON H.AUTO =acj.EntityId and acj.EntityName='HCORDPRON' and acj.EntityTap='INDlcgImagesDX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE (H.MANEXTPRO = 0  OR ISNULL(I.TRATAESPECIA, 0) = 3) AND (B.SERREASIT = 1 OR H.ESTSERIPS IN ('1', '2', '3', '4')) AND H.GENSERVICEORDER  IS NULL AND (isnull(bjc.SkipClearance,0)=0)
) AS MAG ON MAG.NUMINGRES = PACA.NUMINGRES
LEFT JOIN(
	SELECT TOP 1 H.NUMINGRES
	FROM HCORDPATO H 
	JOIN dbo.ADINGRESO I ON H.NUMINGRES = I.NUMINGRES
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON H.AUTO =acj.EntityId and acj.EntityName='HCORDPRON' and acj.EntityTap='INDlcgPathology'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE (H.MANEXTPRO = 0 OR ISNULL(I.TRATAESPECIA, 0) = 3) AND H.ESTSERIPS IN ('2','3','4') AND H.GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)
) as PATO ON PATO.NUMINGRES = PACA.NUMINGRES
LEFT JOIN(
	SELECT TOP 1 H.NUMINGRES
	FROM HCORDLABO H 
	JOIN ADINGRESO I ON H.NUMINGRES = I.NUMINGRES 
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON H.AUTO =acj.EntityId and acj.EntityName='HCORDPRON' and acj.EntityTap='INDlcgLaboratories'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE (H.MANEXTPRO = 0 OR ISNULL(I.TRATAESPECIA, 0) = 3) AND H.ESTSERIPS IN ('3', '4') AND H.GENSERVICEORDER IS NULL AND (isnull(bjc.SkipClearance,0)=0)
) AS LABO ON LABO.NUMINGRES = PACA.NUMINGRES
LEFT JOIN(
	SELECT TOP 1 Ingreso
	FROM dbo.VMedicinesSupplies 
	WHERE Fisico <> 0
) AS SUPP ON SUPP.Ingreso = PACA.NUMINGRES
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on PACA.ID =acj.EntityId and acj.EntityName='HCHISPACA' and acj.EntityTap='INDlcgValoraciones'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
WHERE PACA.GENSERVICEORDER IS NULL and (isnull(bjc.SkipClearance,0)=0)
GROUP BY PACA.NUMINGRES, PACA.IPCODPACI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de validación de bloqueo de admisión para control de cuentas de facturación. Identifica los ingresos de pacientes que tienen servicios clínicos pendientes de liquidar — incluyendo notas de enfermería, oxigenoterapia, terapias, hemoderivados, interconsultas, procedimientos no quirúrgicos, cirugías, imágenes diagnósticas, patología, laboratorios, medicamentos e insumos, y valoraciones médicas — que aún no tienen una orden de servicio generada (sin facturar) y no cuentan con una justificación aprobada que permita omitir la liquidación (SkipClearance). Consolida por número de ingreso y cédula del paciente el ID mínimo de historia clínica afectado, sirviendo como semáforo de bloqueo para impedir el cierre o facturación de una cuenta hospitalaria mientras existan items sin procesar. Se usa en el proceso de auditoría y control de cuentas para garantizar que ningún servicio prestado quede sin facturar antes de cerrar el ingreso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewAdmissionLock_AccountControlValidations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewAdmissionLock_AccountControlValidations';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Identifica admisiones (ingresos) de pacientes con ítems clínicos pendientes de generar orden de servicio para facturación, que no cuenten con justificación que permita omitir el control (SkipClearance), bloqueando así el cierre/liquidación de la cuenta.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionLock_AccountControlValidations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en HCHISPACA con GENSERVICEORDER NULL para el ingreso evaluado.; Las justificaciones en Billing.AccountControlJustification deben relacionarse a la entidad y EntityTap correspondiente al tipo de ítem clínico.; La tabla Billing.BillingJustificationControl debe contener el flag SkipClearance para que el ítem se considere exonerado del control.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionLock_AccountControlValidations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan ingresos cuyo registro principal en HCHISPACA tiene GENSERVICEORDER IS NULL.; Cualquier ítem con justificación cuyo BillingJustificationControl.SkipClearance=1 queda excluido del control de bloqueo.; La vinculación justificación↔ítem se hace por (EntityId, EntityName, EntityTap) específicos por tipo de ítem clínico.; Se devuelve un único ID mínimo por combinación NUMINGRES/IPCODPACI (MIN(ID) con GROUP BY).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionLock_AccountControlValidations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/admisión hospitalaria; Orden de servicio (GENSERVICEORDER); Justificación de control de cuentas; Glosa/SkipClearance; Procedimientos de enfermería; Oxigenoterapia; Terapias; Hemocomponentes; Interconsultas; Procedimientos no quirúrgicos; Cirugías; Imágenes diagnósticas; Patología; Laboratorios; Medicamentos e insumos; Tratamiento especial (TRATAESPECIA); Manejo extra-procedimiento (MANEXTPRO); Liquidación de cuenta', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionLock_AccountControlValidations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewAdmissionLock_AccountControlValidations: Devuelve NUMINGRES, IPCODPACI y MIN(ID) de HCHISPACA agrupados por ingreso/paciente cuando existen valoraciones u otros ítems clínicos sin GENSERVICEORDER y sin justificación con SkipClearance=1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionLock_AccountControlValidations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCHOGASIN: CODSERIPS no nulo y GENSERVICEORDER IS NULL y (SkipClearance,0)=0 → El ingreso queda marcado por procedimientos de enfermería pendientes (INDlcgNurseProcedure).; si HCCONOXIG: GENSERVICEORDER IS NULL y SkipClearance=0 → Ingreso marcado por consumo de oxígeno pendiente (INDlcgOxigenConsumer).; si HCPROCTER: INCUPSIPS.TIPSERTER=1, GENSERVICEORDER IS NULL y SkipClearance=0 → Ingreso marcado por terapias pendientes (INDlcgTerapy).; si HCORHEMSER.ESTADO IN (2,3) y TIPOSERVICIO IN (1,2,3) y (SOL.MANEXTPRO=0 OR ADINGRESO.TRATAESPECIA=3) y ORDSERVICIOID IS NULL y SkipClearance=0 → Ingreso marcado por hemoderivados/hemocomponentes pendientes (INDLcgHemo).; si HCORDINTE: NUMFOLINT IS NOT NULL y GENSERVICEORDER IS NULL y SkipClearance=0 → Ingreso marcado por interconsultas pendientes (INDlcgInterSearch).; si HCORDPRON: (MANEXTPRO=0 OR TRATAESPECIA=3) y ESTSERIPS NOT IN (''1'',''5'') y GENSERVICEORDER IS NULL y SkipClearance=0 → Ingreso marcado por procedimientos no quirúrgicos pendientes (INDlcgProceduresNoQX).; si ViewSurgeriesPerformed: GENSERVICEORDER IS NULL y SkipLiquidation=0 → Ingreso marcado por cirugías realizadas pendientes de orden.; si HCORDIMAG: (MANEXTPRO=0 OR TRATAESPECIA=3) y (SERREASIT=1 OR ESTSERIPS IN (''1'',''2'',''3'',''4'')) y GENSERVICEORDER IS NULL y SkipClearance=0 → Ingreso marcado por imágenes diagnósticas pendientes (INDlcgImagesDX).; si HCORDPATO: (MANEXTPRO=0 OR TRATAESPECIA=3) y ESTSERIPS IN (''2'',''3'',''4'') y GENSERVICEORDER IS NULL y SkipClearance=0 → Ingreso marcado por patología pendiente (INDlcgPathology).; si HCORDLABO: (MANEXTPRO=0 OR TRATAESPECIA=3) y ESTSERIPS IN (''3'',''4'') y GENSERVICEORDER IS NULL y SkipClearance=0 → Ingreso marcado por laboratorios pendientes (INDlcgLaboratories).; si VMedicinesSupplies.Fisico <> 0 → Ingreso marcado por insumos/medicamentos físicos pendientes.; si HCHISPACA con justificación y BillingJustificationControl.SkipClearance=1 → Se excluye el registro del bloqueo (la justificación exime del control).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionLock_AccountControlValidations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.HCHOGASIN; dbo.HCACTENFE; dbo.INCUPSIPS; Billing.AccountControlJustification; Billing.BillingJustificationControl; dbo.HCCONOXIG; dbo.HCPROCTER; dbo.HCORHEMSER; dbo.HCORHEMCO; dbo.ADINGRESO; dbo.HCORDINTE; dbo.HCORDPRON; dbo.ViewSurgeriesPerformed; dbo.HCORDIMAG; dbo.HCORDPATO; dbo.HCORDLABO; dbo.VMedicinesSupplies', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionLock_AccountControlValidations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewAdmissionLock_AccountControlValidations';
GO
