CREATE TABLE [dbo].[HCPARTGRA] (
    [IDPARTGRA]     INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECPARTOG]     INT                                                                              NOT NULL,
    [FECREGIST]     DATETIME                                                                         NULL,
    [TENSSARTE]     VARCHAR (10)                                                                     NULL,
    [FRECUCARD]     INT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [SMFRERESP]     INT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [TEMPERATU]     FLOAT (53) MASKED WITH (FUNCTION = 'default()')                                  NULL,
    [AUFRECUEN]     VARCHAR (10) MASKED WITH (FUNCTION = 'partial(0, "MaternalSigns_Ofuscado", 0)')  NULL,
    [AUINTESID]     INT                                                                              NULL,
    [AUDURACIO]     INT                                                                              NULL,
    [BFFETOCAR]     INT                                                                              NULL,
    [BFDESACEL]     INT                                                                              NULL,
    [BFMOVFETA]     INT                                                                              NULL,
    [TVDILATAC]     INT                                                                              NULL,
    [TVBORRAMI]     NUMERIC (18)                                                                     NULL,
    [TVESTACIO]     INT                                                                              NULL,
    [TVMEMBRAN]     INT                                                                              NULL,
    [TVLIQANMI]     INT                                                                              NULL,
    [TVVARPOSI]     INT                                                                              NULL,
    [OBSERVACI]     VARCHAR (500)                                                                    NULL,
    [FECINIREG]     DATETIME                                                                         NULL,
    [PARPELVIS]     CHAR (1)                                                                         NULL,
    [ESTREGPAR]     BIT                                                                              NULL,
    [PARIDAD]       TINYINT                                                                          NULL,
    [POSMATERNA]    TINYINT                                                                          NULL,
    [IDENTIFICADOR] BIT                                                                              NULL,
    CONSTRAINT [PK_HCPARTGRA] PRIMARY KEY CLUSTERED ([IDPARTGRA] ASC),
    CONSTRAINT [FK_HCPARTGRA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARTGRA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPARTGRA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPARTGRA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPARTGRA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPARTGRA] NOCHECK CONSTRAINT [FK_HCPARTGRA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARTGRA] NOCHECK CONSTRAINT [FK_HCPARTGRA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPARTGRA] NOCHECK CONSTRAINT [FK_HCPARTGRA_INPACIENT];


GO
ALTER TABLE [dbo].[HCPARTGRA] NOCHECK CONSTRAINT [FK_HCPARTGRA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCPARTGRA] NOCHECK CONSTRAINT [FK_HCPARTGRA_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCPARTGRA__IPCODPACI]
    ON [dbo].[HCPARTGRA]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En esta tabla se crean por paciente siempre 15 registros, este campo es para identificar que registros se diligenciaron de todos esos 10 registros.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IDENTIFICADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posición De la Materna  1: Horizontal  2: Vertical  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'POSMATERNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paridad de la materna  1: Nulípara  2: Multípara', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'PARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pelvis  1:Adecuada  2:Dudosa  3:No Adecuada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'PARPELVIS';

