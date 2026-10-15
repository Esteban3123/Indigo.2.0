CREATE TABLE [dbo].[HCFISIPRO] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPRODUC] CHAR (20)                                                                        NOT NULL,
    [TIPPRODUC] CHAR (1)                                                                         NOT NULL,
    [CANACTPRO] INT                                                                              NOT NULL,
    [CANPEDPRO] INT                                                                              NOT NULL,
    [CANPENPRO] INT                                                                              NOT NULL,
    [TOTPROUNI] NUMERIC (18, 2)                                                                  NULL,
    [DOSPROACU] NUMERIC (18, 2)                                                                  NULL,
    [TOTHORACU] INT                                                                              NULL,
    [CODUNIMED] CHAR (3)                                                                         NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TIPREGIST] TINYINT                                                                          CONSTRAINT [DF_HCFISIPRO_TIPREGIST] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_HCFISIPRO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFISIPRO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCFISIPRO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCFISIPRO_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCFISIPRO_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCFISIPRO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCFISIPRO] NOCHECK CONSTRAINT [FK_HCFISIPRO_ADcenaten];


GO
ALTER TABLE [dbo].[HCFISIPRO] NOCHECK CONSTRAINT [FK_HCFISIPRO_ADINGRESO];


GO
ALTER TABLE [dbo].[HCFISIPRO] NOCHECK CONSTRAINT [FK_HCFISIPRO_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCFISIPRO] NOCHECK CONSTRAINT [FK_HCFISIPRO_INPacient];


GO
ALTER TABLE [dbo].[HCFISIPRO] NOCHECK CONSTRAINT [FK_HCFISIPRO_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCFISIPRO_CODCENATE_UFUCODIGO_CODPRODUC_IPCODPACI_CANACTPRO_NUMINGRES]
    ON [dbo].[HCFISIPRO]([CODCENATE] ASC, [UFUCODIGO] ASC, [CODPRODUC] ASC, [IPCODPACI] ASC, [CANACTPRO] ASC)
    INCLUDE([NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCFISIPRO_CODPRODUC]
    ON [dbo].[HCFISIPRO]([CODPRODUC] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCFISIPRO_NUMINGRES_CANACTPRO_CODPRODUC_IPCODPACI]
    ON [dbo].[HCFISIPRO]([NUMINGRES] ASC)
    INCLUDE([CANACTPRO], [CODPRODUC], [IPCODPACI]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de registro  0: Farmacia  1: Central de Mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TIPREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Producto  1: Medicamentos  2: Materiales e Insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIPRO', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';

