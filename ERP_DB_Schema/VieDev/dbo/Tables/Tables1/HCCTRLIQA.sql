CREATE TABLE [dbo].[HCCTRLIQA] (
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [CODCENATE]          CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]          CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]          CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST]          DATETIME                                                                         NOT NULL,
    [NOMLIQADM]          VARCHAR (100)                                                                    NOT NULL,
    [CANLIQADM]          NUMERIC (6, 2)                                                                   NOT NULL,
    [VIAADMINI]          CHAR (40)                                                                        NOT NULL,
    [AutomaticLoadedMix] BIT                                                                              CONSTRAINT [DF_HCCTRLIQA_AutomaticLoadedMix] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_HCCTRLIQA_1] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [FECREGIST] ASC, [NOMLIQADM] ASC),
    CONSTRAINT [FK_HCCTRLIQA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRLIQA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRLIQA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRLIQA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCTRLIQA] NOCHECK CONSTRAINT [FK_HCCTRLIQA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCTRLIQA] NOCHECK CONSTRAINT [FK_HCCTRLIQA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRLIQA] NOCHECK CONSTRAINT [FK_HCCTRLIQA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCTRLIQA] NOCHECK CONSTRAINT [FK_HCCTRLIQA_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mezcla cargada automáticamente  ( 1:Si    -   0:No )    Identifica si el líquido es cargado desde el boton de cargar mezclas de la barra de botones identificando que la mezcla fue cargada automaticamente, este campo me va a servir para no dejar modificar los registros que aca esten en 1 que es que el liqudio se registro desde esta nueva opción.    Esta logica es para el formulario de Balance y liquidos    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQA', @level2type = N'COLUMN', @level2name = N'AutomaticLoadedMix';

