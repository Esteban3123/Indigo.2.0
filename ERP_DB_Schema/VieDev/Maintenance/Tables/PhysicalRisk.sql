CREATE TABLE [Maintenance].[PhysicalRisk] (
    [Id]    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]  VARCHAR (100) NOT NULL,
    [Score] SMALLINT      NOT NULL,
    [State] BIT           NOT NULL,
    CONSTRAINT [PK_PhysicalRisk] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del riesgo fisico ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhysicalRisk', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Puntuacion del reisgo fisico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhysicalRisk', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del riesgo fisic del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhysicalRisk', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de riesgo fisico del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhysicalRisk', @level2type = N'COLUMN', @level2name = N'Id';

