CREATE TABLE [dbo].[HCPARPACS] (
    [CODCENATE]           CHAR (10)     NOT NULL,
    [INTPACSAC]           BIT           NOT NULL,
    [NOMSERPAC]           VARCHAR (256) NULL,
    [INTERFAZHK]          INT           NULL,
    [NOMSERPACEXT]        VARCHAR (256) NULL,
    [INTERFAZONCO]        BIT           NULL,
    [URLINTERFAZONCO]     VARCHAR (256) NULL,
    [CLAVECOMPARTIDAONCO] VARCHAR (256) NULL,
    [USUARIOCREACION]     CHAR (20)     NULL,
    [FECHACREACION]       DATETIME      NULL,
    [USUARIOMODIFICACION] CHAR (20)     NULL,
    [FECHAMODIFICACION]   DATETIME      NULL,
    [ServiceEndpoint]     VARCHAR (200) NULL,
    [Topic]               VARCHAR (200) NULL,
    CONSTRAINT [PK_HCPARPACS] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_HCPARPACS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARPACS_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARPACS_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCPARPACS] NOCHECK CONSTRAINT [FK_HCPARPACS_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARPACS] NOCHECK CONSTRAINT [FK_HCPARPACS_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCPARPACS] NOCHECK CONSTRAINT [FK_HCPARPACS_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Topic del servico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'Topic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Endpoint del servicio ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'ServiceEndpoint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servidor PSC externo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'NOMSERPACEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el proveedor de imagenologia    1: Hiruko   2: Visual Medica   3: Indira       4: AQUILA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARPACS', @level2type = N'COLUMN', @level2name = N'INTERFAZHK';

