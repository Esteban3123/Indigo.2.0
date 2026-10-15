CREATE TABLE [dbo].[HCREGTRARADIOT] (
    [Id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]      VARCHAR (3)   NOT NULL,
    [Descripcion] VARCHAR (200) NOT NULL,
    [Estado]      INT           NOT NULL,
    [UsuCreacion] CHAR (20)     NOT NULL,
    [FecCreacion] DATETIME      NOT NULL,
    [UsuModifica] CHAR (20)     NULL,
    [FecModifica] DATETIME      NULL,
    CONSTRAINT [PK_HCREGTRARADIOT] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'FecModifica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'UsuModifica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'FecCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'UsuCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Region Tratamiento Radioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Estado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Region Tratamiento Radioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Descripcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Region Tratamiento Radioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGTRARADIOT', @level2type = N'COLUMN', @level2name = N'Id';

