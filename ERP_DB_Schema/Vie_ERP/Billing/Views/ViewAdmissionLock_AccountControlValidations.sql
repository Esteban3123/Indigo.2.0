
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