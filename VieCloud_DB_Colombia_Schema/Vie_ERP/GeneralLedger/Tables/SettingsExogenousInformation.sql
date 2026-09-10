CREATE TABLE [GeneralLedger].[SettingsExogenousInformation] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FileTemplate]     TINYINT      NOT NULL,
    [ConceptCode]      VARCHAR (20) NOT NULL,
    [MainAccountId]    INT          NOT NULL,
    [CreationUser]     VARCHAR (20) CONSTRAINT [DF_SettingsExogenousInformation_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME     CONSTRAINT [DF_SettingsExogenousInformation_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_SettingsExogenousInformation__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SettingsExogenousInformation__FileTemplate__ConceptCode__MainAccountId]
    ON [GeneralLedger].[SettingsExogenousInformation]([FileTemplate] ASC, [ConceptCode] ASC, [MainAccountId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP SQL Server). Registra automáticamente el instante exacto de creación, modificación o cambio de estado del registro de configuración exógena.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro. Tipo DATETIME, permite rastrear cuándo se actualizó la configuración de información exógena (RIPS, formatos tributarios).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación. VARCHAR(20), identifica quién cambió la configuración de la información exógena para auditoría contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro. Tipo DATETIME con valor por defecto [Common].[getdate](), marca cuándo se configuró inicialmente el mapeo exógeno.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro. VARCHAR(20), identifica al administrador o contable que configuró la información exógena (valor por defecto 999).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cuenta contable mayor. Vincula el concepto exógeno al plan de cuentas general para reportes RIPS, tributarios o información exógena del formato.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de concepto del formato exógeno (VARCHAR 20). Define qué tipo de gasto, ingreso o movimiento se reporta (ej: 5001=Salarios, 5015=Impuestos, 5063=Intereses financieros) según modelo 1001, 1003, 1004, etc.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'ConceptCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifcia el codigo del concepto del formato especifico de la exogena    por ejemplo para el formato 1001 los conceptos pueden ser los siguientes    5001 Salarios, prestaciones y demás pagos laborales  5002 Honorarios  5003 Comisiones  5004 Servicios  5005 Arrendamientos  5006 Intereses y rendimientos financieros  5007 Compra de activos movibles  5008 Compra de activos fijos  5010 Aportes parafiscales Sena, ICBF y Cajas de Compensación  5011 Aportes parafiscales de EPS e ISS o ARL  5012 Aportes obligatorios de pensiones al ISS y fondo de pensiones (Incluidos aportes del trabajador)  5013 Donaciones en dinero  5014 Donaciones en otros activos  5015 Impuestos  5016 demás costos y deducciones  5018 Importe de primas de reaseguros pagados o abonados en cuenta  5019 Amortizaciones realizadas durante el año  5020 Compra de activos fijos sobre sobre los cuales se solicito deducción  5022 Pensiones  5023 Cuenta al exterior por asistencia técnica  5024 Cuenta al exterior por marcas  5025 Cuenta al exterior por patentes  5026 Cuenta al exterior por regalías  5027 Cuenta al exterior por servicios técnicos  5028 El valor acumulado de la devolución de pagos o abonos en cuenta y retenciones en años ant  5029 Cargos diferidos y/o gtos pagados por anticipado por compras  5030 Cargos diferidos y/o gtos pagados por anticipado por honorarios  5031 Cargos diferidos y/o gtos pagados por anticipado por comisiones  5032 Cargos diferidos y/o gtos pagados por anticipado por servicios  5033 Cargos diferidos y/o gtos pagados por anticipado por arrendamientos  5034 Cargos diferidos y/o gtos pagados por anticipado por intereses y rendimientos financieros  5035 Cargos diferidos y/o gtos pagados por anticipado por  por otros conceptos  5036 Inversiones en control y mejoramiento del medio ambiente por compras  5037 Inversiones en control y mejoramiento del medio ambiente por honorarios  5038 Inversiones en control y mejoramiento del medio ambiente porcomisiones  5039 Inversiones en control y mejoramiento del medio ambiente por servicios  5040 Inversiones en control y mejoramiento del medio ambiente por arrendamientos  5041 Inversiones en control y mejoramiento del medio ambiente por intereses y rendimientos financieros  5042 Inversiones en control y mejoramiento del medio ambiente por otros conceptos  5043 Participaciones o dividendos pagados o abonados en cuenta en calidad de exigibles  5044 El pago por loterías, rifas, apuestas y similares  5045 Retención sobre ingresos de tarjetas debito y crédito  5046 Enajenación de activos fijos de personas naturales ante oficinas de transito u otras entidades  5047 Importe siniestros por lucro cesante pagados o abonados en cuenta  5048 Importe siniestros por  daño emergente pagados o abonados en cuenta  5049 Autoretenciones por ventas  5050 Autoretenciones por servicios  5051 Autoretenciones por rendimientos financieros  5052 Otras autoretenciones  5053 Retenciones practicadas a titulo de timbre  5054 Devolucones de retenciones a titulo de impuesto de timbre  5055 Viaticos no considerados  ingreso al trabajador  5056 Gastos de representación no considerados como ingresos del  trabajador  5057 Amortizaciones realizadas durante el año por cargos diferidos impuesto al patrimonio  5058 Aportes, tasas y contribuciones efectivamente pagados  5059 El pago o abono en cuenta a cada uno de  los cooperados del valor del fondo de protección  5060 Redención de inversiones en lo que corresponde a reembolso de capital  5062 Autoretenciones por CREE  5063 Intereses y rendimientso financieros pagados  5064 Devolución de saldos de aportes pensionales pagados  5065 excedentes pensionales de libre disponibilidad componente de capital pagado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'ConceptCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'ConceptCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de plantilla/formato de archivo exógeno (TINYINT 1-7). Define la estructura de datos esperada: 1=Modelo 1001, 2=Modelo 1007, 3=Modelo 1003, 4=Modelo 1004, 5=Modelo 1008, 6=Modelo 1009, 7=Modelo 1647 (información tributaria, RIPS, declarativos).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'FileTemplate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el modelo del archivo de datos para la informacion exogenoa  1 - Modelo 1001  2 - Modelo 1007  3 - Modelo 1003  4 - Modelo 1004  5 - Modelo 1008  6 - Modelo 1009  7 - Modelo 1647  ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'FileTemplate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'FileTemplate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY). Clave primaria clustered que identifica unívocamente cada configuración de información exógena en la tabla SettingsExogenousInformation.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de información exógena contable: relaciona cada concepto de reporte exógeno (DIAN) con la cuenta contable principal y la plantilla de archivo correspondiente. Permite parametrizar qué cuentas se informan en cada concepto exógeno.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'SettingsExogenousInformation';
