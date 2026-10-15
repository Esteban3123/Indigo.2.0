CREATE TABLE [dbo].[HCPCEINTACT] (
    [IDINTACT]       INT IDENTITY (1, 1) NOT NULL,
    [IDDIAGINT]      INT NOT NULL,
    [CODACTPCE]      INT NOT NULL,
    [IDHCPCECONTROL] INT NOT NULL,
    CONSTRAINT [PK_HCPCEINTACT] PRIMARY KEY CLUSTERED ([IDINTACT] ASC),
    CONSTRAINT [FK_HCPCEINTACT_HCACTIVIPCE] FOREIGN KEY ([CODACTPCE]) REFERENCES [dbo].[HCACTIVIPCE] ([CODACTPCE]),
    CONSTRAINT [FK_HCPCEINTACT_HCPCEDIAGINT] FOREIGN KEY ([IDDIAGINT]) REFERENCES [dbo].[HCPCEDIAGINT] ([IDDIAGINT])
);


GO
ALTER TABLE [dbo].[HCPCEINTACT] NOCHECK CONSTRAINT [FK_HCPCEINTACT_HCACTIVIPCE];


GO
ALTER TABLE [dbo].[HCPCEINTACT] NOCHECK CONSTRAINT [FK_HCPCEINTACT_HCPCEDIAGINT];


GO
CREATE NONCLUSTERED INDEX [IX_HCPCEINTACT_IDDIAGINT_IDHCPCECONTROL_CODACTPCE]
    ON [dbo].[HCPCEINTACT]([IDDIAGINT] ASC, [IDHCPCECONTROL] ASC)
    INCLUDE([CODACTPCE]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el id del control del plan de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda codigo actividades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'CODACTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id diagnostico intervesiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elId intervensiones actividades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDINTACT';

