CREATE TABLE [dbo].[HCSESHEM] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NULL,
    [NUMSILLA]        VARCHAR (20)                                                                     NULL,
    [PESOSECO]        NUMERIC (18, 2)                                                                  NULL,
    [PESOPREDIALISIS] NUMERIC (18, 2)                                                                  NULL,
    [ACCESOVASCULAR]  TINYINT                                                                          NULL,
    [FILTRO]          NUMERIC (18, 2)                                                                  NULL,
    [ULTRAFILTRACION] NUMERIC (18, 2)                                                                  NULL,
    [HEPARINA]        NUMERIC (18, 2)                                                                  NULL,
    [NOTENFINIID]     INT                                                                              NULL,
    [FECINISES]       DATETIME                                                                         NULL,
    [USUINISES]       CHAR (20)                                                                        NULL,
    [PESOPOST]        NUMERIC (18, 2)                                                                  NULL,
    [VOLSANGTRA]      NUMERIC (18, 2)                                                                  NULL,
    [NOTENFFINID]     INT                                                                              NULL,
    [FECFINSES]       DATETIME                                                                         NULL,
    [USUFINSES]       CHAR (20)                                                                        NULL,
    [FECCREREG]       DATETIME                                                                         NULL,
    [FECMODREG]       DATETIME                                                                         NULL,
    [HORASES]         INT                                                                              NULL,
    [MINSES]          INT                                                                              NULL,
    [ESTADO]          INT                                                                              NULL,
    [HOMGLU]          INT                                                                              NULL,
    [NUMEROFISTULA]   INT                                                                              NULL,
    CONSTRAINT [PK_HCSESHEM] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSESHEM_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCSESHEM_HCCTRNOTE] FOREIGN KEY ([NOTENFFINID]) REFERENCES [dbo].[HCCTRNOTE] ([ID]),
    CONSTRAINT [FK_HCSESHEM_HCCTRNOTE1] FOREIGN KEY ([NOTENFINIID]) REFERENCES [dbo].[HCCTRNOTE] ([ID]),
    CONSTRAINT [FK_HCSESHEM_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCSESHEM] NOCHECK CONSTRAINT [FK_HCSESHEM_ADINGRESO];


GO
ALTER TABLE [dbo].[HCSESHEM] NOCHECK CONSTRAINT [FK_HCSESHEM_HCCTRNOTE];


GO
ALTER TABLE [dbo].[HCSESHEM] NOCHECK CONSTRAINT [FK_HCSESHEM_HCCTRNOTE1];


GO
ALTER TABLE [dbo].[HCSESHEM] NOCHECK CONSTRAINT [FK_HCSESHEM_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la cantidad de Fistula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMEROFISTULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la cantidad de Glucomatria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HOMGLU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Heparina  Unidad: ui  desde 0 -10000', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HEPARINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ultra Filtración   Unidad:ml  desde 0 - 9999', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ULTRAFILTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acceso Vascular:  1-> Cateter  2-> Fístula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ACCESOVASCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pero predialisis  "validación de peso en gr o kr segun edad, <= 3 meses gr" 3 digitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOPREDIALISIS';

