CREATE TABLE [Maintenance].[Insurance] (
    [Id]        TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit]       VARCHAR (15) NOT NULL,
    [PersonId]  INT          NOT NULL,
    [Name]      VARCHAR (60) NOT NULL,
    [WebSite]   VARCHAR (80) NULL,
    [IdCity]    INT          NOT NULL,
    [State]     BIT          NOT NULL,
    [TimeStamp] ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Insurance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Insurance_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Insurance_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [UQ_Insurance__PersonId] UNIQUE NONCLUSTERED ([PersonId] ASC)
);


GO
ALTER TABLE [Maintenance].[Insurance] NOCHECK CONSTRAINT [FK_Insurance_City];


GO
ALTER TABLE [Maintenance].[Insurance] NOCHECK CONSTRAINT [FK_Insurance_Person];


GO
CREATE NONCLUSTERED INDEX [IX_Insurance__State]
    ON [Maintenance].[Insurance]([State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna para controlar la concurrencia.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la ciudad relacionada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pagina web de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'WebSite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la persona relacionada al proveedor.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit del tercero.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de aseguradora', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Insurance', @level2type = N'COLUMN', @level2name = N'Id';

