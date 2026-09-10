CREATE TABLE [dbo].[INESPECIA] (
    [CODESPECI]  CHAR (3)     NOT NULL,
    [DESESPECI]  CHAR (60)    NULL,
    [TIPATENCI]  BIT          NOT NULL,
    [INDAUDFOR]  NUMERIC (18) NOT NULL,
    [ESTADO]     BIT          NOT NULL,
    [TIPESPECI]  TINYINT      NULL,
    [MOSTRARWEB] BIT          NULL,
    [TIPCONWEB]  INT          NULL,
    CONSTRAINT [PK_INESPECIA] PRIMARY KEY CLUSTERED ([CODESPECI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de consulta web (INT). Modalidad de atención telemédica o cita virtual disponible en plataforma web para la especialidad. Búsqueda: consulta virtual, cita web, telemedicina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPCONWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el tipo de consulta web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPCONWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPCONWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si mostrar especialidad en citas web (BIT: 1=Sí, 0=No). Controla visibilidad de la especialidad en portal de agendamiento web. Búsqueda: disponibilidad web, visualización citas, especialidad activa web.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se guarda el mostrar especialidad en citas web (si o no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de especialidad (TINYINT: 0=Ninguna, 1=Oftalmología, 2=Nutrición... 22=Trabajo Social). Categorización para reportes RIPS, facturación y validación de prestaciones. Búsqueda: oncología, cirugía, ginecología, fisioterapia, radiología, especialidades clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Especialidad  0- Ninguna  1- Oftalmología  2- Nutrición  3- Psicología  5- Mastología   6- Oncologia  7- Hematología  8- Cx oncológica  9- Ortopedia oncológica  10- Cx Plastica oncológica  11- Urológia oncológica  12-dermatología oncológica  13- Cuidados paliativos  14- Fisioterapia  15- Radioterapia  16-Cirugía  17-Especialidades Clínicas  18-Ginecología  19-Ginecología Oncológica  20-Oftamología Oncológica  21-Radiología  22-Trabajo social', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (BIT: 1=Activo, 0=Inactivo). Determina si la especialidad está disponible para nuevas atenciones, citas y facturación. Búsqueda: especialidad activa, vigencia, habilitación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Diagnostico 1-Activo 2- Inactivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría (NUMERIC 18). Campo de control para auditoría interna y trazabilidad de cambios en especialidades. Búsqueda: auditoría, control interno, regulación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de atención (BIT: 1=Oftalmología, 2=Nutrición, 3=Psicología). Clasificación de modalidad de servicio para consulta externa, procedimientos ambulatorios y atención especializada. Búsqueda: atención especializada, servicio clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPATENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de atención  1=Oftalmología  2=Nutrición  3=Psicología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPATENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'TIPATENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la especialidad (CHAR 60). Nombre completo y detalles de la especialidad médica para reportes, RIPS, contratos y facturación. Búsqueda: nombre especialidad, denominación profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'DESESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'DESESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'DESESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad (CHAR 3, PK). Identificador único alfanumérico de la especialidad en el sistema ERP. Necesario para consultas, facturación y RIPS. Búsqueda: código especialidad, identificador especialidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de especialidades médicas disponibles en el sistema. Permite clasificar cada especialidad por tipo de atención, controlar su visibilidad en el portal web y gestionar su estado activo/inactivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESPECIA';
