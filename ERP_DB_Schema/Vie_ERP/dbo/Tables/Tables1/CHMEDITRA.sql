CREATE TABLE [dbo].[CHMEDITRA] (
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODICAMAS] INT                                                                              NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [CODESPECI] CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_CHMEDITRA] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC, [IPCODPACI] ASC, [CODICAMAS] ASC, [CODPROSAL] ASC),
    CONSTRAINT [FK_CHMEDITRA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHMEDITRA_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHMEDITRA_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_CHMEDITRA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHMEDITRA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[CHMEDITRA] NOCHECK CONSTRAINT [FK_CHMEDITRA_ADINGRESO];


GO
ALTER TABLE [dbo].[CHMEDITRA] NOCHECK CONSTRAINT [FK_CHMEDITRA_CHCAMASHO];


GO
ALTER TABLE [dbo].[CHMEDITRA] NOCHECK CONSTRAINT [FK_CHMEDITRA_INESPECIA];


GO
ALTER TABLE [dbo].[CHMEDITRA] NOCHECK CONSTRAINT [FK_CHMEDITRA_INPacient];


GO
ALTER TABLE [dbo].[CHMEDITRA] NOCHECK CONSTRAINT [FK_CHMEDITRA_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMEDITRA', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMEDITRA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMEDITRA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMEDITRA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMEDITRA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMEDITRA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';

