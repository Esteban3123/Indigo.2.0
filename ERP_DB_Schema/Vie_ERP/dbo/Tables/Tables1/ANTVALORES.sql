CREATE TABLE [dbo].[ANTVALORES] (
    [ID]             INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA]    INT            NOT NULL,
    [CODANTECEDENTE] INT            NOT NULL,
    [IDANTVARIABLE]  INT            NOT NULL,
    [VALOR]          VARCHAR (5000) NOT NULL,
    [IDITEMLISTA]    INT            NULL,
    CONSTRAINT [PK_ANTVALORES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ANTVALORES_ANTVARIABLES] FOREIGN KEY ([IDANTVARIABLE]) REFERENCES [dbo].[ANTVARIABLES] ([ID]),
    CONSTRAINT [FK_ANTVALORES_ANTVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[ANTVARIABLESL] ([ID]),
    CONSTRAINT [FK_ANTVALORES_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_ANTVALORES_RSVALORES] FOREIGN KEY ([ID]) REFERENCES [dbo].[ANTVALORES] ([ID]),
    CONSTRAINT [FK_ANTVALORES_RSVARIABLES] FOREIGN KEY ([IDANTVARIABLE]) REFERENCES [dbo].[ANTVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[ANTVALORES] NOCHECK CONSTRAINT [FK_ANTVALORES_ANTVARIABLES];


GO
ALTER TABLE [dbo].[ANTVALORES] NOCHECK CONSTRAINT [FK_ANTVALORES_ANTVARIABLESL];


GO
ALTER TABLE [dbo].[ANTVALORES] NOCHECK CONSTRAINT [FK_ANTVALORES_HCHISPACA];


GO
ALTER TABLE [dbo].[ANTVALORES] NOCHECK CONSTRAINT [FK_ANTVALORES_RSVALORES];


GO
ALTER TABLE [dbo].[ANTVALORES] NOCHECK CONSTRAINT [FK_ANTVALORES_RSVARIABLES];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Si la variable es una lista guarda aqui el ID del Item seleccioado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del variable Dinamico Antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDANTVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Antecedente:  1-Médicos  2-Quirurgicio  3-Anestésico  4-Transfunsionales  5-Inmunológicos  6-Alérgicos  7-Traumáticos  8-Psicológicos  9-Farmacológicos  10-Familiares  11-Gineco-Obstétricos  12-Urológico-sexual  13-Perinatales  14-Tóxicos  15-Hábitos de Vida  16-Esquema de Vacunación  17-Escolores  18-Laborales  19-Nutricionales  20-Odontológicos  21-SocioEconómicos  22- Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del registro que guarda en la tabla HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'ID';

