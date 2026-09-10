CREATE TABLE [dbo].[HCLISDISP] (
    [AUTO]       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONSECC] INT       NOT NULL,
    [CODSERMED]  CHAR (20) NOT NULL,
    CONSTRAINT [PK_HCDISPLIS] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCDISPLIS_HCLISTACC] FOREIGN KEY ([CODCONSECC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Servicio o Medicamento (CHAR 20). Identificador del servicio médico o medicamento dispensado en la lista de chequeo clínico. Vinculado a catálogo de servicios/fármacos del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'CODSERMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Servicio o Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'CODSERMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'CODSERMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Lista de Chequeo Clínica (INT). Referencia a consec de HCLISTACC. Identifica la lista de verificación/chequeo médico asociada al paciente o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'CODCONSECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'CODCONSECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'CODCONSECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo automático de la tabla HCLISDISP (INT IDENTITY). Clave primaria de la disposición de servicio o medicamento en lista de chequeo clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de servicios médicos o elementos disponibles asociados a una consecuencia o concepto clínico. Permite definir qué servicios, procedimientos o ítems están habilitados o disponibles para una categoría o condición específica dentro de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP';
