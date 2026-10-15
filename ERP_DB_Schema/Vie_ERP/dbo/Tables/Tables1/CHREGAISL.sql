CREATE TABLE [dbo].[CHREGAISL] (
    [CODICAMAS] INT                                                                              NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECINIAIS] DATETIME                                                                         NOT NULL,
    [FECFINAIS] DATETIME                                                                         NOT NULL,
    [CODAISLAM] INT                                                                              NOT NULL,
    [REGANULAD] BIT                                                                              NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_CHREGAISL] PRIMARY KEY CLUSTERED ([CODICAMAS] ASC, [NUMINGRES] ASC, [IPCODPACI] ASC, [FECINIAIS] ASC),
    CONSTRAINT [FK_CHREGAISL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHREGAISL_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHREGAISL_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[CHREGAISL] NOCHECK CONSTRAINT [FK_CHREGAISL_ADINGRESO];


GO
ALTER TABLE [dbo].[CHREGAISL] NOCHECK CONSTRAINT [FK_CHREGAISL_CHCAMASHO];


GO
ALTER TABLE [dbo].[CHREGAISL] NOCHECK CONSTRAINT [FK_CHREGAISL_INPacient];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro de la auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGAISL', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el registro esta anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGAISL', @level2type = N'COLUMN', @level2name = N'REGANULAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Aislamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGAISL', @level2type = N'COLUMN', @level2name = N'CODAISLAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final del Aislamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGAISL', @level2type = N'COLUMN', @level2name = N'FECFINAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Inicio de Aislamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGAISL', @level2type = N'COLUMN', @level2name = N'FECINIAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGAISL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGAISL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGAISL', @level2type = N'COLUMN', @level2name = N'CODICAMAS';

