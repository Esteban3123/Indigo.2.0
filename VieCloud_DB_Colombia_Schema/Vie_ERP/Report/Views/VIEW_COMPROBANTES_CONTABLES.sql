

--/****** Object:  View [Report].[VIEW_COMPROBANTES_CONTABLES]    Script Date: 28/02/2023 2:26:28 p. m. ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

CREATE VIEW [Report].[VIEW_COMPROBANTES_CONTABLES] AS

select --TOP 100
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
ISNULL(BO.Name,'N/A') [SEDE],JV.Consecutive as [NUMERO COMPROBANTE],JVT.Code + ' - ' + JVT.Name [TIPO COMPROBANTE] ,JV.VoucherDate [FECHA COMPROBANTE] , 
case JV.Status when 1 then 'Registrado' when 2 then 'Confirmado' when 3 then 'Anulado' end [ESTADO] ,
JV.EntityCode [DOCUMENTO ORIGEN] ,JV.EntityName [ORIGEN],MA.Number [CUENTA CONTABLE],MA.Name [DESCRIPCION CUENTA],TP.Nit [NIT],TP.Name [TERCERO] ,
CC.Code AS [CODIGO CENTRO COSTO],CC.Name [CENTRO DE COSTO],JVD.DebitValue [VALOR DEBITO] ,JVD.CreditValue [VALOR CREDITO],JVD.Detail [DETALLE],
jv.creationuser AS [CODIGO USUARIO QUE CREO],
pcre.fullname AS [USUARIO QUE CREO],
JV.ConfirmationUser [CODIGO USUARIO QUE CONFIRMO], 
PER.Fullname [USUARIO QUE CONFIRMO],
JV.ConfirmationDate [FECHA CONFIRMACION],
1 as 'CANTIDAD',
  CAST(JV.ConfirmationDate AS date) AS 'FECHA BUSQUEDA',
  YEAR(JV.ConfirmationDate) AS 'AÑO BUSQUEDA',
  MONTH(JV.ConfirmationDate) AS 'MES BUSQUEDA',
  CONCAT(FORMAT(MONTH(JV.ConfirmationDate), '00') ,' - ', 
	   CASE MONTH(JV.ConfirmationDate) 
	    WHEN 1 THEN 'ENERO'
   	    WHEN 2 THEN 'FEBRERO'
	    WHEN 3 THEN 'MARZO'
	    WHEN 4 THEN 'ABRIL'
	    WHEN 5 THEN 'MAYO'
	    WHEN 6 THEN 'JUNIO'
	    WHEN 7 THEN 'JULIO'
	    WHEN 8 THEN 'AGOSTO'
	    WHEN 9 THEN 'SEPTIEMBRE'
	    WHEN 10 THEN 'OCTUBRE'
	    WHEN 11 THEN 'NOVIEMBRE'
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
from GeneralLedger .JournalVouchers JV 
JOIN GeneralLedger .JournalVoucherTypes AS JVT  ON JVT.Id =JV.IdJournalVoucher 
JOIN GeneralLedger .JournalVoucherDetails AS JVD  ON JV.Id =JVD.IdAccounting 
JOIN GeneralLedger .MainAccounts as MA  ON MA.Id =JVD.IdMainAccount 
LEFT JOIN Security .[User] AS USU  ON USU.UserCode =JV.ConfirmationUser 
LEFT JOIN Security .Person AS PER  ON PER.Id =USU.IdPerson 
LEFT JOIN Security .[User] AS ucre  ON jv.creationuser = ucre.usercode
LEFT JOIN Security .Person AS pcre  ON ucre.idperson = pcre.id
LEFT JOIN Common .ThirdParty AS TP  ON TP.Id =JVD.IdThirdParty 
LEFT JOIN Payroll .CostCenter AS CC  ON CC.Id =JVD.IdCostCenter 
LEFT JOIN Payroll .FunctionalUnit FU  ON FU.CostCenterId =CC.Id 
LEFT JOIN Payroll .BranchOffice BO   ON BO.Id  =FU.BranchOfficeId
WHERE
 CAST(JV.ConfirmationDate AS DATE) = CAST(DATEADD(d,-1,GETDATE()) AS DATE)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida los comprobantes contables confirmados del día anterior, uniendo encabezados de vouchers con sus líneas de detalle (débitos/créditos), cuentas contables, terceros (NIT/nombre), centros de costo y sede. Incluye información de auditoría de los usuarios que crearon y confirmaron cada comprobante, además de campos precalculados de fecha/mes/año para facilitar el consumo en herramientas de Business Intelligence o reportes financieros contables.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_COMPROBANTES_CONTABLES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_COMPROBANTES_CONTABLES';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los detalles de los comprobantes contables (encabezado y movimientos débito/crédito) confirmados el día anterior, enriquecidos con tercero, centro de costo, sede y usuarios de creación/confirmación, para consumo de reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_COMPROBANTES_CONTABLES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen comprobantes contables (JournalVouchers) con sus detalles en GeneralLedger.JournalVoucherDetails.; Las cuentas contables (MainAccounts), tipos de comprobante (JournalVoucherTypes), terceros, centros de costo, unidades funcionales y sedes están parametrizados.; Los usuarios de creación/confirmación están registrados en Security.User y Security.Person.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_COMPROBANTES_CONTABLES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY se obtiene de DB_NAME() truncado a 9 caracteres, identificando la base/empresa actual.; Cada fila representa un movimiento (detalle) del comprobante; siempre se emite CANTIDAD = 1 por fila.; La columna ULT_ACTUAL siempre se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; La sede se deriva por la cadena CostCenter → FunctionalUnit → BranchOffice; si algún eslabón no existe, la sede queda en ''N/A''.; Solo se incluyen movimientos cuya fecha de confirmación corresponde al día anterior a la ejecución (ventana fija D-1).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_COMPROBANTES_CONTABLES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Tipo de comprobante; Detalle contable (débito/crédito); Cuenta contable; Tercero (NIT); Centro de costo; Unidad funcional; Sede; Usuario de creación; Usuario de confirmación; Estado del comprobante (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_COMPROBANTES_CONTABLES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.VIEW_COMPROBANTES_CONTABLES: Devuelve únicamente filas cuya CAST(JV.ConfirmationDate AS DATE) sea igual al día anterior (DATEADD(d,-1,GETDATE())), una fila por cada detalle (JournalVoucherDetails) del comprobante.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_COMPROBANTES_CONTABLES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si JV.Status = 1 → Se reporta el estado como ''Registrado''; si JV.Status = 2 → Se reporta el estado como ''Confirmado''; si JV.Status = 3 → Se reporta el estado como ''Anulado''; si MONTH(JV.ConfirmationDate) entre 1 y 12 → Se traduce el número de mes a su nombre en español (ENERO..DICIEMBRE) concatenado con el número formateado a dos dígitos; si BO.Name es NULL (no hay sede asociada vía CostCenter→FunctionalUnit→BranchOffice) → Se reporta SEDE = ''N/A''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_COMPROBANTES_CONTABLES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts; Security.User; Security.Person; Common.ThirdParty; Payroll.CostCenter; Payroll.FunctionalUnit; Payroll.BranchOffice', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_COMPROBANTES_CONTABLES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_COMPROBANTES_CONTABLES';
GO
