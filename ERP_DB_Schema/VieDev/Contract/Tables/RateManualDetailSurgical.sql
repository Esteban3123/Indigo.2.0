CREATE TABLE [Contract].[RateManualDetailSurgical] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RateManualId]            INT             NOT NULL,
    [IPSServiceId]            INT             NOT NULL,
    [SurgicalGroupId]         INT             NULL,
    [UVRRangeId]              INT             NULL,
    [SalesValueWithSurcharge] NUMERIC (18, 2) NOT NULL,
    [SalesValue]              NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_RateManualDetailSurgical__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RateManualDetailSurgical_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_RateManualDetailSurgical_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id]),
    CONSTRAINT [FK_RateManualDetailSurgical_SurgicalGroup] FOREIGN KEY ([SurgicalGroupId]) REFERENCES [Contract].[SurgicalGroup] ([Id]),
    CONSTRAINT [FK_RateManualDetailSurgical_UVRRange] FOREIGN KEY ([UVRRangeId]) REFERENCES [Contract].[UVRRange] ([Id])
);


GO
ALTER TABLE [Contract].[RateManualDetailSurgical] NOCHECK CONSTRAINT [FK_RateManualDetailSurgical_IPSService];


GO
ALTER TABLE [Contract].[RateManualDetailSurgical] NOCHECK CONSTRAINT [FK_RateManualDetailSurgical_RateManual];


GO
ALTER TABLE [Contract].[RateManualDetailSurgical] NOCHECK CONSTRAINT [FK_RateManualDetailSurgical_SurgicalGroup];


GO
ALTER TABLE [Contract].[RateManualDetailSurgical] NOCHECK CONSTRAINT [FK_RateManualDetailSurgical_UVRRange];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo quirurgico  -- Solo se habilita si el manual es SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio IPS del detalle quirurgico es decir los de tipo (Cirujano, Anestesiologo, Ayudante, Derecho a sala y Materiales)    Nota: Cuando el manual sea ISS solo se deben agregar los Tipos de Derecho a sala y Materiales ya que los otros tipos se calculan dependiendo del puntaje', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';

