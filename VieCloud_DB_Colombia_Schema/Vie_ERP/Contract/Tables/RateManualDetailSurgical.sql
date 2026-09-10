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



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_RateManualDetailSurgical__RateManualId]
    ON [Contract].[RateManualDetailSurgical]([RateManualId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18,2) del servicio quirúrgico que se cobrará en el rango de fechas vigente del manual, sin recargo adicional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del servicio que se va cobrar en el rango de fechas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18,2) del servicio quirúrgico con recargo incluido, que se cobrará en el rango de fechas vigente del manual tarifario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con recargo que se va cobrar en el rango de fechas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a Contract.UVRRange (INT, nullable). Identificador del rango de UVR (Unidad de Valor de Referencia); aplica exclusivamente a manuales ISS para variables económicas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'UVRRangeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rango uvr - Aplica solo a ISS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'UVRRangeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'UVRRangeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a Contract.SurgicalGroup (INT, nullable). Identificador del grupo quirúrgico; obligatorio solo si el manual tarifario es SOAT, habilita restricción por tipo de cirugía', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo quirurgico  -- Solo se habilita si el manual es SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a Contract.IPSService (INT). Identificador del servicio IPS quirúrgico: Cirujano, Anestesiólogo, Ayudante, Derecho a Sala o Materiales. Nota: en manual ISS solo aplica Derecho a Sala y Materiales; otros se calculan por puntaje', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS del detalle quirurgico es decir los de tipo (Cirujano, Anestesiologo, Ayudante, Derecho a sala y Materiales)    Nota: Cuando el manual sea ISS solo se deben agregar los Tipos de Derecho a sala y Materiales ya que los otros tipos se calculan dependiendo del puntaje', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a Contract.RateManual (INT). Identificador del manual tarifario padre al que pertenece este detalle de servicio quirúrgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del manual tarifario al que pertenece', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT) único de la fila de detalle quirúrgico en el manual tarifario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de tarifas manuales quirúrgicas definidas en contratos: registra el valor de venta de servicios IPS de tipo quirúrgico, con y sin recargo, según el grupo quirúrgico y el rango de UVR aplicable.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualDetailSurgical';
