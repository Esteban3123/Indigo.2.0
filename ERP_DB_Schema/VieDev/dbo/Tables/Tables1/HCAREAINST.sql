CREATE TABLE [dbo].[HCAREAINST] (
    [Id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]      VARCHAR (3)   NOT NULL,
    [Descripcion] VARCHAR (200) NOT NULL,
    [Estado]      INT           NOT NULL,
    [UsuCreacion] CHAR (20)     NOT NULL,
    [FecCreacion] DATETIME      NOT NULL,
    [UsuModifica] CHAR (20)     NULL,
    [FecModifica] DATETIME      NULL,
    CONSTRAINT [PK_HCAREAINST] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Area  1-Activo  2-Inactivo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREAINST', @level2type = N'COLUMN', @level2name = N'Estado';

