CREATE TABLE [dbo].[PRHCEXAMENFISICO] (
    [ID]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]   INT          NOT NULL,
    [CODIGO]       INT          NOT NULL,
    [SIGVITDESCRI] VARCHAR (50) NULL,
    [SIGVITOBLIG]  BIT          NULL,
    CONSTRAINT [PK_PRHCEXAMENFISICO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCEXAMENFISICO_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCEXAMENFISICO] NOCHECK CONSTRAINT [FK_PRHCEXAMENFISICO_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obligatoriedad del signo vital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'SIGVITOBLIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del signo vital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'SIGVITDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del signo vital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de modelo de HC parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'ID';

