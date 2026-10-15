CREATE TABLE [dbo].[AGAGEMECE] (
    [CODAUTONU] INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODAUTOAG] INT                                                                           NULL,
    [FECHORAIN] DATETIME                                                                      NOT NULL,
    [FECHORAFI] DATETIME                                                                      NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE] CHAR (10)                                                                     NOT NULL,
    [TIPAGEMED] CHAR (1)                                                                      NOT NULL,
    [CODIGOCON] CHAR (6)                                                                      NULL,
    [CODESPECI] CHAR (3)                                                                      NULL,
    [CODUSUASI] CHAR (20)                                                                     NOT NULL,
    [FECREGSIS] DATETIME                                                                      NOT NULL,
    [CODUSUARI] CHAR (20)                                                                     NOT NULL,
    [FECHELIMI] DATETIME                                                                      NOT NULL,
    [CODCAUCAN] CHAR (3)                                                                      NOT NULL,
    [OBSCAUCAN] NCHAR (100)                                                                   NULL,
    CONSTRAINT [PK_AGAGEMECE] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGAGEMECE_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGAGEMECE_AGCAUCANA] FOREIGN KEY ([CODCAUCAN]) REFERENCES [dbo].[AGCAUCANA] ([CODCAUCAN]),
    CONSTRAINT [FK_AGAGEMECE_AGCONSULT] FOREIGN KEY ([CODIGOCON], [CODCENATE]) REFERENCES [dbo].[AGCONSULT] ([CODIGOCON], [CODCENATE]),
    CONSTRAINT [FK_AGAGEMECE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGAGEMECE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AGAGEMECE] NOCHECK CONSTRAINT [FK_AGAGEMECE_ADCENATEN];


GO
ALTER TABLE [dbo].[AGAGEMECE] NOCHECK CONSTRAINT [FK_AGAGEMECE_AGCAUCANA];


GO
ALTER TABLE [dbo].[AGAGEMECE] NOCHECK CONSTRAINT [FK_AGAGEMECE_AGCONSULT];


GO
ALTER TABLE [dbo].[AGAGEMECE] NOCHECK CONSTRAINT [FK_AGAGEMECE_INESPECIA];


GO
ALTER TABLE [dbo].[AGAGEMECE] NOCHECK CONSTRAINT [FK_AGAGEMECE_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Agendamiento   0:Cita Medica   1:Campaña de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMECE', @level2type = N'COLUMN', @level2name = N'TIPAGEMED';

