CREATE TABLE [dbo].[INTANTIBI] (
    [AUTO]          INT                                                                       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO]  INT                                                                       NOT NULL,
    [CODSERIPS]     CHAR (20)                                                                 NOT NULL,
    [NOMUESTRA]     VARCHAR (50)                                                              NOT NULL,
    [NOMMICROR]     VARCHAR (150)                                                             NULL,
    [NOMANTIBI]     VARCHAR (150)                                                             NOT NULL,
    [CMI]           VARCHAR (50)                                                              NOT NULL,
    [RESULTADO]     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)') NULL,
    [NUMMUESTRA]    INT                                                                       NULL,
    [MICROBIOLOGIA] BIT                                                                       NULL,
    [TIPORESULTADO] BIT                                                                       NULL,
    [IDINTERCTRL]   INT                                                                       NULL,
    CONSTRAINT [PK_INTANTIBI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_CODSERIPSs] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ORDEN_INDIGO] FOREIGN KEY ([ORDEN_INDIGO]) REFERENCES [dbo].[INTERCABE] ([AUTO])
);


GO
ALTER TABLE [dbo].[INTANTIBI] NOCHECK CONSTRAINT [FK_CODSERIPSs];


GO
ALTER TABLE [dbo].[INTANTIBI] NOCHECK CONSTRAINT [FK_ORDEN_INDIGO];


GO
CREATE NONCLUSTERED INDEX [IX_INTANTIBI_ORDEN_INDIGO]
    ON [dbo].[INTANTIBI]([ORDEN_INDIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor  "0": Indica que el resultado enviado en el XML es preliminar  Valor "1": Indica que el resultado enviado en el XML es final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '''Valor "0": Indica que el estudio no es de microbiología, es decir que no maneja resultados preliminares     ''Valor "1": Indica que el estudio es de microbiología, es decir que maneja resultados preliminares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIBI', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';

