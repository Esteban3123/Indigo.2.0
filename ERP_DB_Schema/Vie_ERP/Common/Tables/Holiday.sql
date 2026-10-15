CREATE TABLE [Common].[Holiday] (
    [Id]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Holiday]     DATE         NOT NULL,
    [Description] VARCHAR (50) NULL,
    [State]       BIT          NOT NULL,
    CONSTRAINT [PK_Holiday__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Holiday__Holiday] UNIQUE NONCLUSTERED ([Holiday] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1 Activo, 0 Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Holiday', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del dia festivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Holiday', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del festivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Holiday', @level2type = N'COLUMN', @level2name = N'Holiday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del dia festivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Holiday', @level2type = N'COLUMN', @level2name = N'Id';

