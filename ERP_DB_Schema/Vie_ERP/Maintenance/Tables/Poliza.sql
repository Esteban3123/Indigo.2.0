CREATE TABLE [Maintenance].[Poliza] (
    [Id]           TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]         VARCHAR (200) NOT NULL,
    [InsuranceId]  TINYINT       NOT NULL,
    [Name]         VARCHAR (60)  NOT NULL,
    [InitialDate]  DATETIME      NOT NULL,
    [EndDate]      DATETIME      NOT NULL,
    [Object]       VARCHAR (MAX) NOT NULL,
    [IdPolizaType] TINYINT       NOT NULL,
    [State]        BIT           NOT NULL,
    [TimeStamp]    ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Poliza__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Poliza_Insurance] FOREIGN KEY ([InsuranceId]) REFERENCES [Maintenance].[Insurance] ([Id]),
    CONSTRAINT [FK_Poliza_PolizaType] FOREIGN KEY ([IdPolizaType]) REFERENCES [Maintenance].[PolizaType] ([Id])
);


GO
ALTER TABLE [Maintenance].[Poliza] NOCHECK CONSTRAINT [FK_Poliza_Insurance];


GO
ALTER TABLE [Maintenance].[Poliza] NOCHECK CONSTRAINT [FK_Poliza_PolizaType];


GO
CREATE NONCLUSTERED INDEX [IX_Poliza__State]
    ON [Maintenance].[Poliza]([State] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Poliza__Code]
    ON [Maintenance].[Poliza]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna para controlar la concurrencia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Poliza', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la poliza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Poliza', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del tipo de poliza relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Poliza', @level2type = N'COLUMN', @level2name = N'IdPolizaType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objeto', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Poliza', @level2type = N'COLUMN', @level2name = N'Object';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Poliza', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Poliza', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la poliza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Poliza', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la aseguradora relacionada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Poliza', @level2type = N'COLUMN', @level2name = N'InsuranceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la poliza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Poliza', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de polizas', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Poliza', @level2type = N'COLUMN', @level2name = N'Id';

