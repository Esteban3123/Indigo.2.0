CREATE TABLE [dbo].[HCPLANVAI] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDPLAVACU] CHAR (4)                                                                         NOT NULL,
    [FECAPLVAC] DATETIME                                                                         NULL,
    [OBSVACUNA] CHAR (200)                                                                       NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCPLANVAI] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [IDPLAVACU] ASC),
    CONSTRAINT [FK_HCPLANVAI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPLANVAI_INPLANVAC] FOREIGN KEY ([IDPLAVACU]) REFERENCES [dbo].[INPLANVAC] ([IDPLAVACU])
);


GO
ALTER TABLE [dbo].[HCPLANVAI] NOCHECK CONSTRAINT [FK_HCPLANVAI_INPACIENT];


GO
ALTER TABLE [dbo].[HCPLANVAI] NOCHECK CONSTRAINT [FK_HCPLANVAI_INPLANVAC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CAmpo Auditoria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se realizo la aplicacion Opcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'FECAPLVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Plan de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'IDPLAVACU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

