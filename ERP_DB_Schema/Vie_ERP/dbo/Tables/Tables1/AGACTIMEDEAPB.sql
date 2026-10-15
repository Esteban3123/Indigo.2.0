CREATE TABLE [dbo].[AGACTIMEDEAPB] (
    [Id]        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODACTMED] CHAR (3) NOT NULL,
    [Codigo]    INT      NOT NULL,
    [Intervalo] INT      NOT NULL,
    CONSTRAINT [PK_AGACTIMEDEAPB] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AGACTIMEDEAPB_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED])
);


GO
ALTER TABLE [dbo].[AGACTIMEDEAPB] NOCHECK CONSTRAINT [FK_AGACTIMEDEAPB_AGACTIMED];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Intervalo en días', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Intervalo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código (Id) de la EAPB o Entidad Administradora de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Actividad Médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id o Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Id';

