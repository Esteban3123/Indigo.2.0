CREATE TABLE [dbo].[DiagnosticDisability] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [CODDIAGNO]   CHAR (4)     NULL,
    [IdHCINCAPAC] NUMERIC (18) NULL,
    CONSTRAINT [PK__Diagnost__3214EC077068491A] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DiagnosticDisability_HCINCAPAC] FOREIGN KEY ([IdHCINCAPAC]) REFERENCES [dbo].[HCINCAPAC] ([CODCONSEC]),
    CONSTRAINT [FK_DiagnosticDisability_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[DiagnosticDisability] NOCHECK CONSTRAINT [FK_DiagnosticDisability_HCINCAPAC];


GO
ALTER TABLE [dbo].[DiagnosticDisability] NOCHECK CONSTRAINT [FK_DiagnosticDisability_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para relacion con cabecera de incapacidades HCINCAPAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'IdHCINCAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'Id';

