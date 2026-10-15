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
ALTER TABLE [dbo].[CODEFPLAB] NOCHECK CONSTRAINT [FK_CODEFPLAB_INMUNICIP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Regimen  1- contributivo  2 - Subsidiado  3 - Excepcion  4 - especial  5 - No asegurado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPOREGIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion de Copago/ Cuota  1:Estandar  2:Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'LIQCOPCTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tomar Monto Facturado de:  1:Total Servicio   2:Valor Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'MONFACTDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Liquidacion plan de beneficios  1:ISS 2001;  2:SOAT;  3:ISS 2004;  4:CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLATIPLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Entidad  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLATIPENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Plan de Beneficios:  0: Bloqueado No    1: Bloqueado Si  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ESTADOPLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Forma como se Afecta la Cartera:  1=Por Factura    2=Cuenta de Cobro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'AFECTACAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo de Liquidacion para el control de Topes:  1=Semanal    2=Quincenal    3=Mensual    4=Bimensual    5=Semestral    6=Anual  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PERIODLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Liquidacion del Contrato / PB:  1: Pago por Evento - Servicios  2: Capitacion  3:Factura Global   4:Capitacion Global  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPLIQUID';

