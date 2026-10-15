CREATE TABLE [dbo].[PADORDENTERAPIAS] (
    [ID]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADCONTROL]             INT           NOT NULL,
    [CODSERIPS]                CHAR (20)     NOT NULL,
    [CANTIDAD]                 INT           NOT NULL,
    [DURACION]                 INT           NOT NULL,
    [UNIDADMEDIDA]             INT           NOT NULL,
    [MOTIVO]                   VARCHAR (MAX) NULL,
    [IDDESCRIPCIONRELACIONADA] INT           NULL,
    CONSTRAINT [PK_PHDORDTERAPIAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PHDORDTERAPIAS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_PHDORDTERAPIAS_PHDCONTROL] FOREIGN KEY ([IDPADCONTROL]) REFERENCES [dbo].[PADCONTROL] ([ID])
);


GO
ALTER TABLE [dbo].[PADORDENTERAPIAS] NOCHECK CONSTRAINT [FK_PHDORDTERAPIAS_INCUPSIPS];


GO
ALTER TABLE [dbo].[PADORDENTERAPIAS] NOCHECK CONSTRAINT [FK_PHDORDTERAPIAS_PHDCONTROL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'undiad de medidad de la cantidad de duracion: 5 terapias durante 2 meses.    1 - Dias  2 - Meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'UNIDADMEDIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad de duracion de la terapia ej:    10 sesiones de terapia durante 30 dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'DURACION';

