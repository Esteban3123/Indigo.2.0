CREATE TABLE [dbo].[HCEVEADVD] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCEVENADVID] INT NOT NULL,
    [HCVALORTIPO] INT NOT NULL,
    CONSTRAINT [PK_HCEVEADVD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCEVEADVD_HCEVENADV] FOREIGN KEY ([HCEVENADVID]) REFERENCES [dbo].[HCEVENADV] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de motivo adverso o evento adverso: 1=Vacunación, 2=Transfusión de componentes sanguíneos, 3=Medicamentos. Clasificación del tipo de reacción adversa o efecto secundario reportado en la atención clínica (INT, sin máscara).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'HCVALORTIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Motivo adverso:  1-Vacunacion.  2-Transfución componontes sanguineos.  3-Medicamentos.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'HCVALORTIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'HCVALORTIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del registro de evento adverso en tabla HCEVENADV. Referencia única al motivo adverso o reacción adversa documentada en historia clínica (INT, relación 1:N con HCEVENADV.ID).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'HCEVENADVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del valor del registro del motivo adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'HCEVENADVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'HCEVENADVID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la línea o detalle de clasificación del evento adverso en la tabla HCEVEADVD. Clave primaria de incremento automático (INT IDENTITY, no replicable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de valores asociados a eventos adversos de historia clínica. Registra los tipos de valor vinculados a cada evento adverso documentado en la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVEADVD';
