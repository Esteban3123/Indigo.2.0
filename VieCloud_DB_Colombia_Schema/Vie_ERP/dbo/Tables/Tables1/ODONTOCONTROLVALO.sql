CREATE TABLE [dbo].[ODONTOCONTROLVALO] (
    [ID]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOCONTROL] INT     NOT NULL,
    [CEO_C]           TINYINT NOT NULL,
    [CEO_E]           TINYINT NOT NULL,
    [CEO_O]           TINYINT NOT NULL,
    [CPO_C]           TINYINT NOT NULL,
    [CPO_P]           TINYINT NOT NULL,
    [CPO_O]           TINYINT NOT NULL,
    CONSTRAINT [PK_ODONTOVALORACION] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOCONTROLVALO_ODONTOCONTROL] FOREIGN KEY ([IDODONTOCONTROL]) REFERENCES [dbo].[ODONTOCONTROL] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CPO de dientes obturados (tratados, restaurados). CPO = Cariados, Perdidos, Obturados. Valor numérico (0-32) para evaluación odontológica de salud dental en adultos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CPO_O';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice Obturados del CPO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CPO_O';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CPO_O';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CPO de dientes perdidos (ausentes, extraídos). CPO = Cariados, Perdidos, Obturados. Cantidad de piezas dentales no presentes por caries o extracción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CPO_P';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice Perdidos del CPO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CPO_P';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CPO_P';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CPO de dientes cariados (afectados por caries dental). CPO = Cariados, Perdidos, Obturados. Medida de prevalencia de lesiones cariosas no tratadas en adultos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CPO_C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice Cariados del CPO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CPO_C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CPO_C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CEO de dientes obturados (tratados, restaurados). CEO = Cariados, Extraídos, Obturados. Piezas temporales con restauración odontológica en dentición infantil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CEO_O';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice Obturados del CEO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CEO_O';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CEO_O';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CEO de dientes extraídos (perdidos, ausentes). CEO = Cariados, Extraídos, Obturados. Aplicable a dentición temporal infantil; dientes extraídos por caries u otra causa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CEO_E';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice Extraidos del CEO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CEO_E';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CEO_E';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CEO de dientes cariados (afectados por caries). CEO = Cariados, Extraídos, Obturados. Evaluación de lesiones cariosas activas en dentición temporal de menores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CEO_C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice Cariados del CEO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CEO_C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'CEO_C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la valoración odontológica. Referencias tabla ODONTOCONTROL. Vincula resultados de examen (CEO/CPO) con control dental específico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ODONTOCONTROL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (IDENTITY, 1-step). Clave primaria de la valoración odontológica. Tipo INT, autoincrementable para auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los valores de los índices odontológicos CEO (dientes de leche: Cariados, con Extracción indicada, Obturados) y CPO (dientes permanentes: Cariados, Perdidos, Obturados) asociados a un control odontológico. Permite cuantificar la salud bucal del paciente en cada valoración dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO';
