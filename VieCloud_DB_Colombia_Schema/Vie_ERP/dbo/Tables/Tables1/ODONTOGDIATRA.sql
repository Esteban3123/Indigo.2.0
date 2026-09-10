CREATE TABLE [dbo].[ODONTOGDIATRA] (
    [ID]          INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCTRDIETRA] INT         NOT NULL,
    [CONSECDIA]   INT         NOT NULL,
    [TIPO]        VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ODONTOGDIATRA__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGDIATRA_ODONTOGDIETRA1] FOREIGN KEY ([IDCTRDIETRA]) REFERENCES [dbo].[ODONTOGDIETRA] ([ID]),
    CONSTRAINT [FK_ODONTOGDIATRA_ODOPARDIA] FOREIGN KEY ([CONSECDIA]) REFERENCES [dbo].[ODOPARDIA] ([CONSECDIA])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diagnóstico odontológico: clasificación de ubicación/superficie dental (NoAplica=0, NivelDiente=1, VestibularArriba=2, Oclusal=3, Palatino=4, DistalIzquierda=5, DistalDerecha=6, MesialDerecha=7, MesialIzquierda=8, VestibularAbajo=9, Lingual=10). VARCHAR(1), tipo de tratamiento o hallazgo odontológico por ubicación anatómica del diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el tipo diagnóstico de tratamiento NoAplica = 0, NivelDiente = 1, VestibularArriba = 2, Oclusal = 3, Palatino = 4, DistalIzquierda = 5, DistalDerecha = 6, MesialDerecha = 7, MesialIzquierda = 8, VestibularAbajo = 9, Lingual = 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo de diagnóstico odontológico. Clave foránea que referencia ODOPARDIA.CONSECDIA, vinculando el diagnóstico con su correspondiente registro de parámetro o detalle diagnóstico en odontología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'CONSECDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con ODOPARDIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'CONSECDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'CONSECDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de control/tratamiento odontológico. Clave foránea que referencia ODONTOGDIETRA.ID, relacionando este diagnóstico con el tratamiento dental principal o registro de intervención odontológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'IDCTRDIETRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ODONTOGDIETRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'IDCTRDIETRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'IDCTRDIETRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo del diagnóstico odontológico (IDENTITY INT). Clave primaria de la tabla, autoincremental para cada registro de diagnóstico dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de los días de la semana o días asignados a cada dieta/control dietético del paciente odontológico. Asocia cada dieta con sus días de atención y el tipo correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA';
