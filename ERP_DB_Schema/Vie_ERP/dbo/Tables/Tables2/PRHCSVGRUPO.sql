CREATE TABLE [dbo].[PRHCSVGRUPO] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [IDMODELOHC]     INT NOT NULL,
    [CODSALUDVISUAL] INT NOT NULL,
    CONSTRAINT [PK_PRHCSVGRUPO] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PRMODELOHC_PRHCSVGRUPO] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCSVGRUPO] NOCHECK CONSTRAINT [FK_PRMODELOHC_PRHCSVGRUPO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la pestaña de valoracion de salud visual  1: Anamnesis / Antecedentes  2: Exámen externo  3: Agudeza visual  4: Refreacción  5: Valoracion complementaria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'CODSALUDVISUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la relacion con el modelo de HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'Id';

