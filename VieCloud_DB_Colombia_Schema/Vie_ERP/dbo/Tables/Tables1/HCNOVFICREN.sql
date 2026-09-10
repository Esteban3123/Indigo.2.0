CREATE TABLE [dbo].[HCNOVFICREN] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHAR]  INT           NOT NULL,
    [MOTNOVEDA] TINYINT       NULL,
    [FECNOVEDA] DATETIME      NULL,
    [CAUMUERTE] TINYINT       NULL,
    [FECMUERTE] DATETIME      NULL,
    [OTROMOT]   VARCHAR (200) NULL,
    [USUREGNOV] CHAR (20)     NOT NULL,
    CONSTRAINT [PK_HCNOVFICREN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNOVFICREN_HCRENFICH] FOREIGN KEY ([IDFICHAR]) REFERENCES [dbo].[HCRENFICH] ([ID])
);


GO
ALTER TABLE [dbo].[HCNOVFICREN] NOCHECK CONSTRAINT [FK_HCNOVFICREN_HCRENFICH];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registra la novedad; profesional de salud que documenta el cambio de estado o evento del paciente en tratamiento renal. CHAR(20), clave de auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'USUREGNOV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que registra la novedad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'USUREGNOV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'USUREGNOV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otro motivo del cambio de estado; descripción textual complementaria cuando motivo de novedad es ''''99=Otro''''. VARCHAR(200), documenta razones adicionales de desafiliación, abandono o eventos no catalogados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'OTROMOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro motivo del cambio de estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'OTROMOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'OTROMOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de muerte del paciente; timestamp del deceso registrado en ficha renal. DATETIME NULL, asociado a causa de muerte (CAUMUERTE).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'FECMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'FECMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'FECMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa de muerte: 1=Enfermedad renal crónica, 2=Cardiovascular, 3=Cáncer, 4=Infección, 5=Otra causa, 6=Externa, 99=Sin dato. TINYINT, codificación clínica para RIPS/epidemiología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'CAUMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Causa de muerte:  1=Enfermedad renal crónica  2= Enfermedad Cardiovascular  3= Cáncer  4= Infección  5= Causa diferente a 1, 2, 3 y 4  6= Causa Externa  99= Sin dato.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'CAUMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'CAUMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de novedad; timestamp del evento de cambio de estado del paciente (fallecimiento, alta voluntaria, desafiliación, abandono, retorno a terapia). DATETIME NULL, vinculada a MOTNOVEDA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'FECNOVEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de novedad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'FECNOVEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'FECNOVEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de novedad/cambio de estado: 1=Fallecimiento, 5=Alta voluntaria, 6=Desafiliación, 7=Abandono terapia, 9=Retorno terapia, 99=Otro. TINYINT, categorización V79 RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'MOTNOVEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo del cambio de estado o novedad:  1= Falleció (Guarda como Novedad (V79))  5= Persona que firmó alta voluntaria del tratamiento prescrito (Guarda como Novedad (V79))  6= Persona que se desafilió (Guarda como Novedad (V79))  7= Persona que abandona la terapia (Guarda como Novedad (V79))  9= Persona que regresa  terapia (Guarda como Novedad (V79))  99= Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'MOTNOVEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'MOTNOVEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de ficha renal; clave foránea (FK) que referencia HCRENFICH.ID. INT NOT NULL, vincula novedad a historia clínica renal del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'IDFICHAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la ficha renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'IDFICHAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'IDFICHAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro de novedad en historia clínica renal. INT IDENTITY(1,1), clave primaria de auditoría de eventos del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novedades y eventos especiales registrados en fichas de historia clínica de enfermería, incluyendo motivos de novedad, causa y fecha de fallecimiento del paciente, y el usuario que registró la novedad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN';
