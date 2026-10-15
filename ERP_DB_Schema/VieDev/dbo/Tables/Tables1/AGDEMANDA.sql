CREATE TABLE [dbo].[AGDEMANDA] (
    [CODAUTONU] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [FECHACITA] DATE                                                                             NOT NULL,
    [MOTIVODEM] CHAR (1)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI] CHAR (3)                                                                         NOT NULL,
    [FECREGSIS] DATETIME                                                                         NOT NULL,
    [ESTADODEM] BIT                                                                              NOT NULL,
    [CODUSUASI] CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_AGDEMANDA] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGEDEMINS_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGEDEMINS_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[AGDEMANDA] NOCHECK CONSTRAINT [FK_AGEDEMINS_INESPECIA];


GO
ALTER TABLE [dbo].[AGDEMANDA] NOCHECK CONSTRAINT [FK_AGEDEMINS_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mantiene el Estado de la Demanda   True = Visible   False = No Visible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'ESTADODEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la Demanda Insatisfecha.    0: Fisico  1: Humano  2: Financiero  3: Represamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDEMANDA', @level2type = N'COLUMN', @level2name = N'MOTIVODEM';

