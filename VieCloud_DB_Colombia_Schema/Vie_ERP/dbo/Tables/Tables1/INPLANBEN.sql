CREATE TABLE [dbo].[INPLANBEN] (
    [CODPLABEN]  VARCHAR (10)  NOT NULL,
    [DESPLABEN]  VARCHAR (120) NOT NULL,
    [OBTCONTRA]  INT           NULL,
    [LIQPLABEN]  CHAR (1)      NOT NULL,
    [CANTUSUAR]  INT           NULL,
    [VALORUSUA]  MONEY         NULL,
    [PLAPERFAC]  CHAR (1)      NULL,
    [PLATOPIND]  MONEY         NULL,
    [PLATOPPER]  MONEY         NULL,
    [PLANREQUE]  INT           NULL,
    [CXCENTIDA]  INT           NULL,
    [PLAAFECAR]  CHAR (1)      NULL,
    [NUMCONEXE]  INT           NULL,
    [HORMINEXT]  INT           NULL,
    [PORDESCON]  MONEY         NULL,
    [PORDESURG]  MONEY         NULL,
    [CUOTMODER]  INT           NULL,
    [CUOTCOPAG]  INT           NULL,
    [DIAPLAFACT] INT           NULL,
    [PLAPORSER]  INT           NULL,
    [PLAPROMED]  INT           NULL,
    [PLACENCOS]  INT           NULL,
    [PLAPOSSUB]  BIT           NULL,
    [PLAAUTING]  BIT           NULL,
    [PLALIQPFGP] BIT           NULL,
    [MANCONEXT]  BIT           NULL,
    [TERLIQISS]  BIT           NULL,
    [ACTPREVEN]  BIT           NULL,
    [CXCPACIENT] INT           NULL,
    [CONRECAJA]  INT           NULL,
    [CONANTICI]  INT           NULL,
    [CODTERCER]  INT           NULL,
    [PLAPRPFGP]  INT           NULL,
    [PLATIPENT]  CHAR (1)      NULL,
    [PLAPROACC]  INT           NULL,
    [PORACTMED]  MONEY         NULL,
    [PLAPROMPP]  INT           NULL,
    [PORACTSUM]  MONEY         NULL,
    [LIQCIRART]  BIT           NULL,
    [CONCEPFAC]  CHAR (2)      NULL,
    [PLANINACT]  BIT           NULL,
    [CODMERCAD]  INT           NULL,
    [TIPPACPFGP] CHAR (1)      NULL,
    [PLAVENDED]  INT           NULL,
    [PLATIPLIQ]  CHAR (1)      NULL,
    [CARPERGLO]  VARCHAR (120) NULL,
    [NOMPERGLO]  VARCHAR (120) NULL,
    [CONDVENT]   INT           NULL,
    [CODASEGUR]  VARCHAR (120) NULL,
    [VIGPREFAC]  INT           NULL,
    [RUBROFAC]   INT           NULL,
    [TIPINGFAC]  INT           NULL,
    [VIGPREPAG]  INT           NULL,
    [RUBROPAG]   INT           NULL,
    [TIPINGPAG]  INT           NULL,
    [CUENTOBJE]  INT           NULL,
    [INTFAUTOR]  BIT           NULL,
    [DEPFACTUR]  INT           NULL,
    [DEPPAGARE]  INT           NULL,
    CONSTRAINT [PK_INPLANBEN_1] PRIMARY KEY CLUSTERED ([CODPLABEN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dependencia/unidad funcional asociada al pagaré; referencia contable de origen del pago, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DEPPAGARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dependencia Asociada al Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DEPPAGARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DEPPAGARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dependencia/unidad funcional asociada a la factura; centro de atención o unidad emisora, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DEPFACTUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dependencia Asociada a la Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DEPFACTUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DEPFACTUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interfaz/integración con sistema de autorización de servicios y procedimientos, BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'INTFAUTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interfaz Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'INTFAUTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'INTFAUTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de objeción; registro de reclamos y glosas del paciente o asegurador, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUENTOBJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Objecion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUENTOBJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUENTOBJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingresos/rubros registrados en pagaré; clasificación contable del pago, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPINGPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Ingresos Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPINGPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPINGPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rubro/clasificación presupuestal del pagaré; concepto financiero de ingreso, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'RUBROPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rubro Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'RUBROPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'RUBROPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vigencia presupuestal del pagaré; año fiscal de ejecución del pago, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VIGPREPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vigencia Presupuestal Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VIGPREPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VIGPREPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingresos/rubros registrados en factura; clasificación contable de facturas, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPINGFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Ingresos Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPINGFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPINGFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rubro/clasificación presupuestal de ingresos en factura; concepto financiero, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'RUBROFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rubro Ingresos Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'RUBROFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'RUBROFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vigencia presupuestal de la factura; año fiscal de ejecución de ingresos, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VIGPREFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vigencia Presupuestal de la Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VIGPREFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VIGPREFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la aseguradora/EPS/entidad administradora; identificador del tercero pagador, VARCHAR(120)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODASEGUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Aseguradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODASEGUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODASEGUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condiciones de venta; términos y plazos de pago entre el centro y la entidad, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONDVENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condiciones de Venta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONDVENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONDVENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la persona que glosa; profesional que realiza reclamación o ajuste, VARCHAR(120)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'NOMPERGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Persona que Glosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'NOMPERGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'NOMPERGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo/rol de la persona que recibe la glosa; especialista en cobro/facturación, VARCHAR(120)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CARPERGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo de la Persona que Recibe la Glosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CARPERGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CARPERGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación del plan: 1=ISS, 2=SOAT, 3=ISS 2004, 4=ISS 2001; esquema de cobro, CHAR(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATIPLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Liquidacion plan de beneficios  1:ISS;  2:SOAT;  3:ISS 2004;  4:ISS 2001', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATIPLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATIPLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vendedor/asesor comercial del plan de beneficios; responsable de la venta, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAVENDED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vendedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAVENDED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAVENDED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de paciente PFGP: 1=Vinculado, 2=Desplazado, 3=PyP Vinculado, 4=PyP Desplazado, CHAR(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPPACPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paciente PFGP   1= Vinculado  2=Desplazado  3=PyP Vinculado   4=PyP Desplazado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPPACPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPPACPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del mercado/segmento; clasificación de nicho comercial o población, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODMERCAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Mercado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODMERCAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODMERCAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desactivar el plan de beneficios; bandera para suspender o cancelar plan activo, BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLANINACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desactivar el Plan ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLANINACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLANINACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto a facturar; tipo de cargo o servicio en la factura (consulta, procedimiento, etc.), CHAR(2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONCEPFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto a Facturar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONCEPFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONCEPFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidación de cirugías artroscópicas; control de facturación para procedimientos de artroscopia, BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'LIQCIRART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidacion de cirugias Artroscopicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'LIQCIRART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'LIQCIRART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de actualización de suministros/insumos; ajuste de precios de materiales, MONEY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORACTSUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Suministros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORACTSUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORACTSUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantillas de procedimientos de metas PYP; modelos de facturación para PyP, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROMPP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantillas Procedimientos de Metas PYP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROMPP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROMPP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de actualización de medicamentos; ajuste de precios farmacéuticos, MONEY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Actualizacion Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantillas de procedimientos para accidentes de tránsito (tercera factura); modelos de RIPS, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantillas Procedimientos para tercera factura de accidentes de transito ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad: IPS, EPS, ARL, etc.; clasificación legal del prestador de servicios, CHAR(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATIPENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Entidad  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATIPENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATIPENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla de procedimientos PFGP; protocolo de facturación para población pobre y vulnerable, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPRPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla Procedimientos PFGP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPRPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPRPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tercero/entidad contratante; identificador del ente pagador o distribuidor, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODTERCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODTERCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODTERCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto contable de anticipo; rubro para registrar pagos anticipados de servicios, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONANTICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto Anticipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONANTICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONANTICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto contable de recibo de caja; clasificación de ingresos por cobros, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONRECAJA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto Recibo de Caja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONRECAJA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONRECAJA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta por cobrar al paciente; saldo pendiente de copago, cuota moderadora, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CXCPACIENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta por Cobrar al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CXCPACIENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CXCPACIENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualizar precios de venta; permitir revisión automática de tarifas, BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'ACTPREVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualizar Precios de Venta ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'ACTPREVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'ACTPREVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera factura accidentes de tránsito liquida ISS; control de facturación para SOAT, BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TERLIQISS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercera Factura Accidentes de Transito Liquida ISS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TERLIQISS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TERLIQISS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manejo de consultas exentas; control de servicios sin costo para paciente, BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'MANCONEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manejo de Consultas Exentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'MANCONEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'MANCONEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidar PFGP (1=Sí, 0=No); activar facturación de población pobre garantizada, BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLALIQPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidar PFGP 1=Si;0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLALIQPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLALIQPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pedir autorización en ingreso; requerir aprobación previa para atención, BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAAUTING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pedir Autorizacion En ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAAUTING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAAUTING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'POS Subsidiado; activar cobertura para régimen de subsidios, BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPOSSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'POS Subsidiado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPOSSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPOSSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centros de costo; asignación de gastos a unidades funcionales, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLACENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centros de Costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLACENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLACENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantillas de procedimientos medicamentos; modelos de facturación farmacéutica, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantillas Procedimientos Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla de procedimientos servicios; protocolos de facturación por prestación, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPORSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla Procedimientos Servios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPORSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPORSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo en días para facturación; días después del servicio para emitir factura, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DIAPLAFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Planzo en Dias de la Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DIAPLAFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DIAPLAFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta por cobrar a copago; rubro del saldo pendiente de copago del paciente, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUOTCOPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuota por Cobrar a Copago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUOTCOPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUOTCOPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta por cobrar cuota moderadora; rubro del saldo de cuota moderadora, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUOTMODER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta por Cobrar Cuota Moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUOTMODER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUOTMODER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje descuento urgencias; rebaja aplicable en servicios urgentes, MONEY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORDESURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'% Descuento Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORDESURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORDESURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje descuento medicamentos en consulta externa; rebaja farmacéutica ambulatoria, MONEY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORDESCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'% Descuento de Medicamentos en Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORDESCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORDESCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horas mínimas para liquidar estancia; requisito temporal para facturación de hospitalización, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'HORMINEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas Minimas Para Liquidar la Extancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'HORMINEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'HORMINEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de consultas exentas; cantidad de atenciones sin cargo incluidas en plan, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'NUMCONEXE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Consultas Excentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'NUMCONEXE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'NUMCONEXE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Afecta cartera (1=Por factura, 2=Cuenta cobro); modo de registro contable de ingresos, CHAR(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAAFECAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta Cartera  1:Por Factura  2:Cuenta Cobro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAAFECAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAAFECAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta por cobrar a la entidad; saldo pendiente de pago por asegurador/EPS, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CXCENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta por Cobrar a la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CXCENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CXCENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla de requerimientos; protocolo de datos obligatorios para servicio, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLANREQUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla Requeriminetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLANREQUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLANREQUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope por período; límite máximo de cobertura por vigencia (año/mes/trimestre), MONEY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATOPPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tope por Periodo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATOPPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATOPPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope individual; límite máximo de cobertura por beneficiario, MONEY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATOPIND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Topo Individual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATOPIND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATOPIND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de facturación (1=Semanal, 2=Quincenal, 3=Mensual, 4=Bimensual, 5=Semestral, 6=Anual), CHAR(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPERFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo Facturacion  1:Semanal;  2:Quincenal;  3:Mensual;  4:Bimensual;  5:Semestral;  6:Anual  6:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPERFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPERFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor usuario/prima; costo por beneficiario o póliza, MONEY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VALORUSUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VALORUSUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VALORUSUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad usuarios; número de afiliados o beneficiarios cubiertos, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CANTUSUAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CANTUSUAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CANTUSUAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidación (1=Pago por servicios, 2=Capitación, 3=Factura global, 4=Capitación global), CHAR(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'LIQPLABEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidacion  1:Pago por Servicios  2:Capitacion  3:Factura Global   4:Capitacion Global  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'LIQPLABEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'LIQPLABEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Objeto del contrato; propósito y alcance del acuerdo comercial, INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'OBTCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'OBTCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'OBTCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del plan de beneficios; nombre, cobertura y características del producto, VARCHAR(120)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DESPLABEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Plan Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DESPLABEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DESPLABEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de beneficios; identificador único del plan (PK), VARCHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODPLABEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODPLABEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODPLABEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Planes de beneficios o planes de cobertura contratados con aseguradoras, EPS o pagadores. Define las condiciones tarifarias, topes, cuotas moderadoras, copagos, descuentos y parámetros de liquidación y facturación para cada plan.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN';
