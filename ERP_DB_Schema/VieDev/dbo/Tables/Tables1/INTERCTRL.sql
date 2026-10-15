CREATE TABLE [dbo].[INTERCTRL] (
    [AUTO]          INT                                                                               IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO]  VARCHAR (20)                                                                      NOT NULL,
    [NUMUESTRA]     TINYINT                                                                           NOT NULL,
    [ESTADOINT]     BIT                                                                               NOT NULL,
    [INTERPRET]     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [AUTOLABOR]     INT                                                                               NOT NULL,
    [FECGENERA]     VARCHAR (20)                                                                      NULL,
    [FECREGIST]     DATETIME                                                                          NULL,
    [FECSERIPS]     VARCHAR (20)                                                                      NULL,
    [CODPROSAL]     CHAR (70) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NULL,
    [MICROBIOLOGIA] BIT                                                                               NULL,
    [TIPORESULTADO] BIT                                                                               NULL,
    CONSTRAINT [PK_INTERCTRL] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_INTERCTRL_ORDEN_INDIGO_AUTOLABOR_FECREGIST]
    ON [dbo].[INTERCTRL]([ORDEN_INDIGO] ASC, [AUTOLABOR] ASC)
    INCLUDE([FECREGIST]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor  "0": Indica que el resultado enviado en el XML es preliminar  Valor "1": Indica que el resultado enviado en el XML es final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '''Valor "0": Indica que el estudio no es de microbiología, es decir que no maneja resultados preliminares     ''Valor "1": Indica que el estudio es de microbiología, es decir que maneja resultados preliminares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';

