CREATE TABLE [dbo].[HCCONFTABPARAESP] (
    [ID]               INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCCONFTABPARAC] INT      NOT NULL,
    [CODESPECI]        CHAR (3) NOT NULL,
    CONSTRAINT [PK_HCCONFTABPARAESP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCONFTABPARAESP_HCCONFTABPARAC] FOREIGN KEY ([IDHCCONFTABPARAC]) REFERENCES [dbo].[HCCONFTABPARAC] ([ID]),
    CONSTRAINT [FK_HCCONFTABPARAESP_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[HCCONFTABPARAESP] NOCHECK CONSTRAINT [FK_HCCONFTABPARAESP_HCCONFTABPARAC];


GO
ALTER TABLE [dbo].[HCCONFTABPARAESP] NOCHECK CONSTRAINT [FK_HCCONFTABPARAESP_INESPECIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Especialidad de Excepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla cabecera HCCONFTABPARAC = Configuración Tablas Paraclínicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'IDHCCONFTABPARAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'ID';

