CREATE TABLE [dbo].[INEMPRESU] (
    [INDCODEMP]            CHAR (3)        NOT NULL,
    [INDNOMEMP]            CHAR (90)       NOT NULL,
    [INDCODDGH]            VARCHAR (20)    NOT NULL,
    [INDSERIAL]            CHAR (20)       NOT NULL,
    [INDNITEMP]            CHAR (15)       NOT NULL,
    [INDTIPIDE]            CHAR (1)        NOT NULL,
    [INDNUMIDE]            CHAR (10)       NOT NULL,
    [INDDIGVER]            CHAR (1)        NOT NULL,
    [INDDIREMP]            VARCHAR (150)   NULL,
    [INDTE1EMP]            VARCHAR (15)    NOT NULL,
    [INDTE2EMP]            VARCHAR (15)    NOT NULL,
    [DEPMUNCOD]            CHAR (5)        NOT NULL,
    [LOGOPRINC]            VARBINARY (MAX) NULL,
    [LOGOSECUN]            VARBINARY (MAX) NULL,
    [INDVERDGH]            CHAR (1)        NULL,
    [INDIDERES]            VARCHAR (15)    NOT NULL,
    [INDNOMRES]            VARCHAR (250)   NOT NULL,
    [INDDIRRES]            VARCHAR (150)   NOT NULL,
    [INDCE1RES]            VARCHAR (15)    NOT NULL,
    [INDCE2RES]            VARCHAR (15)    NULL,
    [INDCOERES]            VARCHAR (50)    NOT NULL,
    [INDCOPRES]            VARCHAR (50)    NULL,
    [INDFIRRES]            VARBINARY (MAX) NULL,
    [EMPPRODUC]            BIT             CONSTRAINT [DFX_08D0D4B8] DEFAULT ((0)) NOT NULL,
    [IMPMODNIF]            BIT             NULL,
    [DGHPRIVADO]           BIT             NULL,
    [IDPAIS]               INT             NULL,
    [TOKENMIPRES]          VARCHAR (40)    NULL,
    [BD_DWH]               VARCHAR (100)   NULL,
    [URLINDIRA]            NVARCHAR (150)  NULL,
    [RISINDIRA]            BIT             NULL,
    [MIPRESTEST]           BIT             NULL,
    [BDFUNDACIONAL]        VARCHAR (100)   NULL,
    [RouteLogoReportLeft]  VARCHAR (MAX)   NULL,
    [RouteLogoReportRight] VARCHAR (MAX)   NULL,
    [InitialDateAnnexOne]  DATE            NULL,
    [ActivateSnorlax]      BIT             NULL,
    CONSTRAINT [PK_INempresu] PRIMARY KEY CLUSTERED ([INDCODEMP] ASC),
    CONSTRAINT [FK_INEMPRESU_INEMPRESU] FOREIGN KEY ([INDCODEMP]) REFERENCES [dbo].[INEMPRESU] ([INDCODEMP]),
    CONSTRAINT [FK_INEMPRESU_INMUNICIP] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD])
);


GO
ALTER TABLE [dbo].[INEMPRESU] NOCHECK CONSTRAINT [FK_INEMPRESU_INEMPRESU];


GO
ALTER TABLE [dbo].[INEMPRESU] NOCHECK CONSTRAINT [FK_INEMPRESU_INMUNICIP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial anexo 1
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'InitialDateAnnexOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruta del logo de los reportes del EHR - para la seccion derecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportRight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruta del logo de los reportes del EHR - para la seccion izquierda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportLeft';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si crystal esta integrado con el RIS de INDIRA.  False -> No esta integrado  True -> Si esta integrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RISINDIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Identificacion del Prestador  1: CC  2: Nit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTIPIDE';

