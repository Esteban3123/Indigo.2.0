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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Liquidacion plan de beneficios  1:ISS;  2:SOAT;  3:ISS 2004;  4:ISS 2001', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATIPLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente PFGP   1= Vinculado  2=Desplazado  3=PyP Vinculado   4=PyP Desplazado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPPACPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Entidad  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATIPENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta Cartera  1:Por Factura  2:Cuenta Cobro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAAFECAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo Facturacion  1:Semanal;  2:Quincenal;  3:Mensual;  4:Bimensual;  5:Semestral;  6:Anual  6:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPERFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion  1:Pago por Servicios  2:Capitacion  3:Factura Global   4:Capitacion Global  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'LIQPLABEN';

