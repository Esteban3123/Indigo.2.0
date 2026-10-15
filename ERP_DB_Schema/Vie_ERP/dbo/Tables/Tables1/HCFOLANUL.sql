CREATE TABLE [dbo].[HCFOLANUL] (
    [AUTO]      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODMOANFO] CHAR (3)                                                                         NOT NULL,
    [CRIJUSTIF] VARCHAR (500)                                                                    NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHDILIG] DATETIME                                                                         NOT NULL,
    [FECREGSIS] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_HCFOLANUL] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCFOLANUL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCFOLANUL_HCFOLANUL] FOREIGN KEY ([AUTO]) REFERENCES [dbo].[HCFOLANUL] ([AUTO]),
    CONSTRAINT [FK_HCFOLANUL_HCMOAFOLI] FOREIGN KEY ([CODMOANFO]) REFERENCES [dbo].[HCMOAFOLI] ([CODMOANFO]),
    CONSTRAINT [FK_HCFOLANUL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCFOLANUL_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCFOLANUL] NOCHECK CONSTRAINT [FK_HCFOLANUL_ADINGRESO];


GO
ALTER TABLE [dbo].[HCFOLANUL] NOCHECK CONSTRAINT [FK_HCFOLANUL_HCFOLANUL];


GO
ALTER TABLE [dbo].[HCFOLANUL] NOCHECK CONSTRAINT [FK_HCFOLANUL_HCMOAFOLI];


GO
ALTER TABLE [dbo].[HCFOLANUL] NOCHECK CONSTRAINT [FK_HCFOLANUL_INPACIENT];


GO
ALTER TABLE [dbo].[HCFOLANUL] NOCHECK CONSTRAINT [FK_HCFOLANUL_INPROFSAL];


GO
CREATE NONCLUSTERED INDEX [IX_HCFOLANUL_IPCODPACI_NUMINGRES]
    ON [dbo].[HCFOLANUL]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora que se Registra en el Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFOLANUL', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora que se diligengia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFOLANUL', @level2type = N'COLUMN', @level2name = N'FECHDILIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFOLANUL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de la Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFOLANUL', @level2type = N'COLUMN', @level2name = N'CRIJUSTIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del motivo de anulacion del folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFOLANUL', @level2type = N'COLUMN', @level2name = N'CODMOANFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso    Nota: Codigo 00000001', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFOLANUL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFOLANUL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFOLANUL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFOLANUL', @level2type = N'COLUMN', @level2name = N'AUTO';

