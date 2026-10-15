CREATE TABLE [dbo].[PRHCXANTECEDENTE] (
    [ID]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]     INT NOT NULL,
    [CODANTECEDENTE] INT NOT NULL,
    CONSTRAINT [PK_PRHCXANTECEDENTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXANTECEDENTE_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXANTECEDENTE] NOCHECK CONSTRAINT [FK_PRHCXANTECEDENTE_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Antecedente:  1-Médicos  2-Quirurgicio  3-Anestésico  4-Transfunsionales  5-Inmunológicos  6-Alérgicos  7-Traumáticos  8-Psicológicos  9-Farmacológicos  10-Familiares  11-Gineco-Obstétricos  12-Urológico-sexual  13-Perinatales  14-Tóxicos  15-Hábitos de Vida  16-Esquema de Vacunación  17-Escolores  18-Laborales  19-Nutricionales  20-Odontológicos  21-SocioEconómicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';

