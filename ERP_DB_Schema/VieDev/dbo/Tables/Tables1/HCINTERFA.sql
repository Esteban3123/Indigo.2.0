CREATE TABLE [dbo].[HCINTERFA] (
    [CONSECUTI] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCONCEC] NUMERIC (18)                                                                     NULL,
    [CODCONCES] CHAR (10)                                                                        NULL,
    [CODENTIDA] CHAR (9)                                                                         NULL,
    [CODCONTRA] CHAR (6)                                                                         NULL,
    [CODPANATE] CHAR (2)                                                                         NULL,
    [NUMSUMINI] CHAR (20)                                                                        NOT NULL,
    [INTFECHAD] DATETIME                                                                         NOT NULL,
    [INTUSUIND] CHAR (20)                                                                        NULL,
    [INTUSUDGH] CHAR (20)                                                                        NULL,
    [INTUSUWIN] CHAR (60)                                                                        NOT NULL,
    [INTESTRED] CHAR (60)                                                                        NOT NULL,
    [INTORIGEN] CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCINTERFA] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCINTERFA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINTERFA_HCPRESCRC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCPRESCRC] ([CODCONCEC]),
    CONSTRAINT [FK_HCINTERFA_INENTIDAD] FOREIGN KEY ([CODENTIDA]) REFERENCES [dbo].[INENTIDAD] ([CODENTIDA]),
    CONSTRAINT [FK_HCINTERFA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCINTERFA] NOCHECK CONSTRAINT [FK_HCINTERFA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINTERFA] NOCHECK CONSTRAINT [FK_HCINTERFA_HCPRESCRC];


GO
ALTER TABLE [dbo].[HCINTERFA] NOCHECK CONSTRAINT [FK_HCINTERFA_INENTIDAD];


GO
ALTER TABLE [dbo].[HCINTERFA] NOCHECK CONSTRAINT [FK_HCINTERFA_INPacient];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen de la Interfaz:  H: Historias Clinicas  E: Enfermeria  I: Inventarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de registro  Identificador: 00000015', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

