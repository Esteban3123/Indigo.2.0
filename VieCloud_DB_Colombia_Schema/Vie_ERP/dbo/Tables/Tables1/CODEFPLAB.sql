CREATE TABLE [dbo].[CODEFPLAB] (
    [CODCONTRA]         CHAR (6)        NOT NULL,
    [CODPLANBE]         CHAR (2)        NOT NULL,
    [CODENTADM]         CHAR (9)        NOT NULL,
    [NOMPLANBE]         VARCHAR (120)   NULL,
    [OBTCONTRA]         INT             NULL,
    [TIPLIQUID]         INT             NOT NULL,
    [CANTUSUAR]         INT             NULL,
    [VALORUSUA]         MONEY           NULL,
    [PERIODLIQ]         INT             NOT NULL,
    [TOPEINDVI]         NUMERIC (18, 2) NOT NULL,
    [TOPEPERIO]         NUMERIC (18, 2) NOT NULL,
    [AFECTACAR]         INT             NOT NULL,
    [CCSERVICI]         CHAR (15)       NOT NULL,
    [CCCOPAGOS]         CHAR (15)       NOT NULL,
    [CCCTAMODE]         CHAR (15)       NOT NULL,
    [CCCXCPACI]         CHAR (15)       NOT NULL,
    [CODCENCOS]         CHAR (14)       NOT NULL,
    [PLAZOFACT]         INT             NOT NULL,
    [GPPCODIGZ]         CHAR (3)        NOT NULL,
    [GPICODIGZ]         CHAR (3)        NOT NULL,
    [CODPLANRE]         CHAR (3)        NOT NULL,
    [CODPLAHOS]         CHAR (3)        NULL,
    [CONRECPAC]         CHAR (3)        NULL,
    [CONRECENT]         CHAR (3)        NULL,
    [ESTADOPLA]         INT             NOT NULL,
    [NUMCONEXE]         INT             NULL,
    [HORMINEXT]         INT             NULL,
    [PORDESCON]         MONEY           NULL,
    [PORDESURG]         MONEY           NULL,
    [PLAPOSSUB]         BIT             NULL,
    [PLAAUTING]         BIT             NULL,
    [PLALIQPFGP]        BIT             NULL,
    [MANCONEXT]         CHAR (1)        NULL,
    [TERLIQISS]         BIT             NULL,
    [ACTPREVEN]         BIT             NULL,
    [CODTERCER]         CHAR (15)       NULL,
    [PLAPRPFGP]         CHAR (3)        NULL,
    [PLATIPENT]         CHAR (2)        NULL,
    [PLAPROACC]         INT             NULL,
    [PORACTMED]         MONEY           NULL,
    [PLAPROMPP]         INT             NULL,
    [PORACTSUM]         MONEY           NULL,
    [LIQCIRART]         BIT             NULL,
    [CONCEPFAC]         CHAR (4)        NULL,
    [CODMERCAD]         CHAR (2)        NULL,
    [TIPPACPFGP]        CHAR (1)        NULL,
    [PLAVENDED]         VARCHAR (20)    NULL,
    [PLATIPLIQ]         CHAR (1)        NULL,
    [CARPERGLO]         VARCHAR (120)   NULL,
    [NOMPERGLO]         VARCHAR (120)   NULL,
    [CONDVENT]          INT             NULL,
    [CODASEGUR]         VARCHAR (120)   NULL,
    [VIGPREFAC]         INT             NULL,
    [RUBROFAC]          INT             NULL,
    [TIPINGFAC]         INT             NULL,
    [VIGPREPAG]         INT             NULL,
    [RUBROPAG]          INT             NULL,
    [TIPINGPAG]         INT             NULL,
    [CUENTOBJE]         CHAR (15)       NULL,
    [DEPFACTUR]         INT             NULL,
    [INTFAUTOR]         BIT             NULL,
    [DEPPAGARE]         INT             NULL,
    [MONFACTDE]         CHAR (1)        NULL,
    [LIQCOPCTA]         CHAR (1)        NULL,
    [CCSERVICR]         CHAR (15)       NULL,
    [INDAUDFOR]         NUMERIC (18, 9) NOT NULL,
    [CCXPERCAP]         CHAR (15)       NULL,
    [TIPOREGIM]         CHAR (1)        NULL,
    [CCNTRAMITE]        CHAR (16)       NULL,
    [CCCOJURIDICO]      CHAR (16)       NULL,
    [CCRADCONCIL]       CHAR (16)       NULL,
    [DEPMUNCOD]         CHAR (5)        NULL,
    [NOEXIGIRJUSTNOPOS] BIT             NULL,
    [ENTETERRITORIAL]   BIT             NULL,
    CONSTRAINT [PK_INPLANBEN] PRIMARY KEY CLUSTERED ([CODCONTRA] ASC, [CODPLANBE] ASC),
    CONSTRAINT [FK_CODEFPLAB_INMUNICIP] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT si la entidad administradora es ente territorial (departamento, municipio, distrito) que gestiona salud pública', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ENTETERRITORIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si es o no ente territorial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ENTETERRITORIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ENTETERRITORIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT que indica si se exime la justificación de servicios NO-POS (fuera del plan obligatorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOEXIGIRJUSTNOPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si exige o no justificacion no pos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOEXIGIRJUSTNOPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOEXIGIRJUSTNOPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(5) del municipio y departamento; relación FK a [INMUNICIP] para localización territorial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de municipios ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable CHAR(16) asociada a cuentas por cobrar radiadas y conciliadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCRADCONCIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable por Radicada Conciliada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCRADCONCIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCRADCONCIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable CHAR(16) para gestión de cobro jurídico y acciones de cobranza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCOJURIDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable por Cobro Jurídico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCOJURIDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCOJURIDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable CHAR(16) para trámites, diligencias y procesos en curso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCNTRAMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable por Cuenta trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCNTRAMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCNTRAMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de régimen de afiliación CHAR(1): 1=Contributivo, 2=Subsidiado, 3=Excepción, 4=Especial, 5=No asegurado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPOREGIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Regimen  1- contributivo  2 - Subsidiado  3 - Excepcion  4 - especial  5 - No asegurado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPOREGIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPOREGIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable CHAR(15) para Cuentas x Cobrar por pérdida de capítación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCXPERCAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta por Cobrar Perd. Capitacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCXPERCAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCXPERCAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice numérico NUMERIC(18,9) de auditoría INDIGO para trazabilidad y control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auditoria INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable CHAR(15) para servicios IPS radiados (Cuentas x Cobrar Radicadas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCSERVICR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable para Servicios IPS CxC Radicados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCSERVICR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCSERVICR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación CHAR(1) de copagos/cuotas: 1=Estándar, 2=Por tabla de valores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'LIQCOPCTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidacion de Copago/ Cuota  1:Estandar  2:Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'LIQCOPCTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'LIQCOPCTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuente CHAR(1) para tomar monto facturado: 1=Total del servicio, 2=Valor según entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'MONFACTDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tomar Monto Facturado de:  1:Total Servicio   2:Valor Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'MONFACTDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'MONFACTDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT de dependencia/unidad funcional asociada al pagaré o documento de pago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPPAGARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dependencia Asociada al Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPPAGARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPPAGARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT de integración con interfaz de autorización de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'INTFAUTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interfaz Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'INTFAUTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'INTFAUTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT de dependencia/unidad funcional responsable de la facturación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPFACTUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dependencia Asociada a la Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPFACTUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPFACTUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable CHAR(15) para registrar objeciones y glosas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CUENTOBJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Objecion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CUENTOBJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CUENTOBJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT de tipo de ingreso presupuestal para el pagaré', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPINGPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Ingresos Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPINGPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPINGPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT de rubro presupuestal para el pagaré', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'RUBROPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rubro Pago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'RUBROPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'RUBROPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año INT de vigencia presupuestal del pagaré', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VIGPREPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vigencia Presupuestal Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VIGPREPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VIGPREPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT de tipo de ingreso presupuestal para la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPINGFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Ingresos Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPINGFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPINGFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT de rubro presupuestal para ingresos de factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'RUBROFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rubro Ingresos Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'RUBROFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'RUBROFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año INT de vigencia presupuestal de la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VIGPREFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vigencia Presupuestal de la Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VIGPREFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VIGPREFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(120) identificador de la aseguradora, entidad administradora de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODASEGUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Aseguradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODASEGUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODASEGUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT de condiciones comerciales de venta (plazo, descuentos, forma pago)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONDVENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condiciones de Venta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONDVENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONDVENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre VARCHAR(120) del responsable que recibe, revisa o formula glosas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOMPERGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Persona que Glosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOMPERGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOMPERGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo VARCHAR(120) o puesto del responsable de glosa en la organización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CARPERGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo de la Persona que Recibe la Glosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CARPERGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CARPERGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo CHAR(1) de liquidación del plan: 1=ISS 2001, 2=SOAT, 3=ISS 2004, 4=CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLATIPLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Liquidacion plan de beneficios  1:ISS 2001;  2:SOAT;  3:ISS 2004;  4:CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLATIPLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLATIPLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(20) del vendedor o asesor comercial del plan de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAVENDED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vendedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAVENDED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAVENDED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo CHAR(1) de paciente en PFGP (personas en formación, ganancias permanentes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPPACPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Paciente PFGP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPPACPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPPACPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(2) del segmento de mercado o línea de negocio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODMERCAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Mercado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODMERCAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODMERCAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(4) del concepto contable a facturar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONCEPFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto a Facturar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONCEPFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONCEPFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT para incluir cirugías artroscópicas en la liquidación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'LIQCIRART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidacion de cirugias Artroscopicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'LIQCIRART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'LIQCIRART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje MONEY de actualización anual de precios en suministros y medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORACTSUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Actualizacion de Suministros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORACTSUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORACTSUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT de plantilla de procedimientos y metas para Planes y Programas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPROMPP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantillas Procedimientos de Metas PYP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPROMPP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPROMPP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje MONEY de actualización anual de medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Actualizacion Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT de plantilla de procedimientos para tercera factura en accidentes de tránsito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPROACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantillas Procedimientos para tercera factura de accidentes de transito ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPROACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPROACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo CHAR(2) de entidad: IPS, EPS, ARL, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLATIPENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Entidad  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLATIPENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLATIPENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(3) de plantilla de procedimientos PFGP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPRPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla Procedimientos PFGP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPRPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPRPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(15) identificador del tercero/proveedor en la relación contractual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODTERCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODTERCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODTERCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT para activar actualización automática de precios de venta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ACTPREVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualizar Precios de Venta ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ACTPREVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ACTPREVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT para liquidación en tercera factura de accidentes de tránsito con ISS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TERLIQISS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercera Factura Accidentes de Transito Liquida ISS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TERLIQISS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TERLIQISS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manejo CHAR(1) de consultas exentas: control de exoneración de copagos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'MANCONEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manejo de Consultas Exentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'MANCONEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'MANCONEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT de aplicabilidad de plantilla PFGP en liquidación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLALIQPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla PFGP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLALIQPFGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLALIQPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT que requiere autorización previa en el ingreso/admisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAAUTING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pedir Autorizacion En ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAAUTING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAAUTING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT de aplicabilidad de POS Subsidiado en el plan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPOSSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'POS Subsidiado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPOSSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPOSSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje MONEY de descuento sobre servicios de urgencia/emergencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORDESURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'% Descuento Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORDESURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORDESURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje MONEY de descuento en medicamentos durante consulta externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORDESCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'% Descuento de Medicamentos en Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORDESCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORDESCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número INT mínimo de horas para liquidar una estancia hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'HORMINEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas Minimas Para Liquidar la Extancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'HORMINEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'HORMINEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad INT de consultas exentas de copago permitidas en el período', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NUMCONEXE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Consultas Excentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NUMCONEXE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NUMCONEXE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado INT del plan: 0=No bloqueado, 1=Bloqueado; control de disponibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ESTADOPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Plan de Beneficios:  0: Bloqueado No    1: Bloqueado Si  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ESTADOPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ESTADOPLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(3) del concepto en recibo de caja para recuperación de cuotas a entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONRECENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Concepto del Recibo de Caja para la Cancelacion de las Cuotas de Recuperacion de las Facturas a Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONRECENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONRECENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(3) del concepto en recibo de caja para cancelación de facturas a paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONRECPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Concepto del Recibo de Caja para la Cancelacion de Facturas a Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONRECPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONRECPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(3) de plantilla de hospitalización con tarifas de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(3) de plantilla de requerimientos/protocolos para atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLANRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Requerimientos para la atencion de pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLANRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLANRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(3) de plantilla de medicamentos con valorización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'GPICODIGZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Medicamentos - Trae el valor de los medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'GPICODIGZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'GPICODIGZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(3) de plantilla de procedimientos/servicios IPS con valorización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'GPPCODIGZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla para Servicios IPS - Trae el valor de los servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'GPPCODIGZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'GPPCODIGZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo INT en días para vencimiento de facturas o cuentas de cobro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAZOFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plazo para el vencimiento de facturas o Cuentas de Cobro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAZOFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAZOFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(14) del centro de costos, unidad funcional generadora de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable CHAR(15) para Cuentas x Cobrar a pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCXCPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable para las Cuenas por Cobrar a Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCXCPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCXCPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable CHAR(15) para recaudos de cuotas moderadoras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCTAMODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable para el recaudo de Cuotas moderadoras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCTAMODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCTAMODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable CHAR(15) para recaudos de copagos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCOPAGOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable para el recaudo de Copagos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCOPAGOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCOPAGOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable CHAR(15) para servicios IPS sin radicar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCSERVICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable para Servicios IPS CxC sin Radicar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCSERVICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCSERVICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma INT de impacto en cartera: 1=Por factura individual, 2=Cuenta de cobro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'AFECTACAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma como se Afecta la Cartera:  1=Por Factura    2=Cuenta de Cobro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'AFECTACAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'AFECTACAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite NUMERIC(18,2) máximo de liquidación permitido en el período', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TOPEPERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tope Maximo para la liquidacion en el periodo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TOPEPERIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TOPEPERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite NUMERIC(18,2) máximo por factura o liquidación individual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TOPEINDVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tope maximo para la liquidacion individual o Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TOPEINDVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TOPEINDVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período INT de control de topes: 1=Semanal, 2=Quincenal, 3=Mensual, 4=Bimensual, 5=Semestral, 6=Anual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PERIODLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo de Liquidacion para el control de Topes:  1=Semanal    2=Quincenal    3=Mensual    4=Bimensual    5=Semestral    6=Anual  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PERIODLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PERIODLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor MONEY unitario por usuario/afiliado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VALORUSUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VALORUSUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VALORUSUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad INT de usuarios/afiliados contratados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CANTUSUAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CANTUSUAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CANTUSUAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo INT de liquidación: 1=Por evento, 2=Capítación, 3=Factura global, 4=Capítación global', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPLIQUID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Liquidacion del Contrato / PB:  1: Pago por Evento - Servicios  2: Capitacion  3:Factura Global   4:Capitacion Global  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPLIQUID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPLIQUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INT del objeto/propósito del contrato (servicios a prestar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'OBTCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Objeto del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'OBTCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'OBTCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción VARCHAR(120) del plan de beneficios y cobertura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOMPLANBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del plan de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOMPLANBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOMPLANBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(9) de la entidad administradora (EPS, ARL, fondos de pensión)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad Administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(2) identificador del plan de beneficios dentro del contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLANBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLANBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLANBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(6) identificador único del contrato de servicios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Planes de beneficios asociados a contratos de facturación con entidades administradoras (EPS, aseguradoras, pagadores). Contiene la configuración financiera, contable y operativa de cada plan: tipo de liquidación, topes, copagos, cuentas contables, plazos de facturación y condiciones de venta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB';
